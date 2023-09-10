using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using tt.helpers;
using tt.Hubs;
using tt.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace tt.Classes
{
    public class GameService
    {
        public ConcurrentDictionary<string, IGame> _games = new ConcurrentDictionary<string, IGame>();
        public ConcurrentDictionary<string, IPlayer> _allPlayers = new ConcurrentDictionary<string, IPlayer>();
        private readonly AIService _aiService;
        private readonly IHubContext<ChatHub> _hubContext;
        public GameService(AIService aiService, IHubContext<ChatHub> hubContext)
        {
            _aiService = aiService;
            _hubContext = hubContext;
        }
        public IGame CreateGame()
        {
            var game = new Game();
            if (_games.TryAdd(game.Id, game) == true)
            {
                AddAiPlayersToGame(game.Id);
                return game;
            }
            else
            {
                throw new Exception("didn't find the game right or something");
            }
        }

        public IGame GetGame(string id, IPlayer player)
        {
            if (_games.TryGetValue(id, out var game) && game.Players.Contains(player))
            {
                return game;
            }
            throw new Exception("Game not found or player is not part of the game.");
        }

        public IGame FindOpenGame()
        {
            // use linq to find an open game
            var game = _games.Values.FirstOrDefault(g => !g.IsFull);
            if (game == null)
            {
                game = this.CreateGame();
                _games.TryAdd(game.Id, game);

                return game;

            }
            return game;

        }

        public void AddPlayerToGame(string gameId, IPlayer player)
        {
            try
            {
                bool success = _games.TryGetValue(gameId, out IGame game);
                if (success)
                {
                    game.AddPlayer(player);
                    _allPlayers.TryAdd(player.PlayerId, player);
                }
            }
            catch
            {
                throw new ArgumentException("The game does not exist.", nameof(gameId));
            }
        }
        public void AddMessageToGame(string gameId, IMessage message)
        {
            try
            {
                bool success = _games.TryGetValue(gameId, out IGame game);
                if (success)
                {
                    game.AddMessage(message);
                }
            }
            catch
            {
                throw new ArgumentException("The game does not exist.", nameof(gameId));
            }
        }

        public void AddAiPlayersToGame(string gameId)
        {
            try
            {
                bool success = _games.TryGetValue(gameId, out IGame game);
                if (success)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var aiPlayer = new AIPlayer(gameId);
                        game.AddPlayer(aiPlayer);
                    }
                }
            }
            catch
            {
                throw new ArgumentException("The game does not exist.", nameof(gameId));
            }
        }
        public event Func<string, IMessage, Task> AIGeneratedResponse;


        public async Task HandleAIResponses(string gameId)
        {
            try
            {
                bool success = _games.TryGetValue(gameId, out IGame game);
                if (success)
                {
                    List<IPlayer> aiPlayers = game.GetAiPlayers();
                    if (aiPlayers == null || aiPlayers.Count == 0)
                    { throw new Exception("No AI players in game."); }

                    // Shuffle AI players and select a random number of them to respond
                    Random rnd = new Random();
                    aiPlayers = aiPlayers.OrderBy(x => rnd.Next()).ToList();
                    int numberOfRespondingPlayers = rnd.Next(aiPlayers.Count + 1);

                    int accumulatedDelay = 0;

                    // Send updated messages to the AI API at dynamic intervals based on response length
                    foreach (AIPlayer aiPlayer in aiPlayers.Take(numberOfRespondingPlayers))
                    {
                        string response = await _aiService.GetResponse(game.Messages, aiPlayer);
                        IMessage message = new Message(response, aiPlayer.User);
                        game.AddMessage(message);

                        int delay = GetDynamicDelay.GetResponseDelay(response);
                        accumulatedDelay += delay;

                        await Task.Delay(accumulatedDelay);
                        await _hubContext.Clients.Group(gameId).SendAsync("ReceiveMessage", message);
                    }
                }
            }
            catch
            {
                throw new ArgumentException("The game does not exist.", nameof(gameId));
            }


        }
    }
}

