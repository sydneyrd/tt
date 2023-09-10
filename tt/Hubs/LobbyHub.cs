using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using tt.Classes;
using tt.Interfaces;

namespace tt.Hubs
{
    public class LobbyHub : Hub
    {
        private GameService _gameService;
        public LobbyHub(GameService gameService)
        {
            _gameService = gameService;
        }
        public async Task<string> CreateGame()
        {
            var game = _gameService.CreateGame();
            var player = new Player(game.Id);
            _gameService.AddPlayerToGame(game.Id, player);
            await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
            await Clients.Caller.SendAsync("Player", player);
            await Clients.Group(game.Id).SendAsync("Roomcode", game.Id);
            return game.Id;
        }

        public async Task JoinGame(string roomCode)
        {
            if (!_gameService._games.ContainsKey(roomCode))
            {
                // Handle invalid room code
                return;
            }

            var player = new Player(roomCode);
            _gameService.AddPlayerToGame(roomCode, player);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
            await Clients.Caller.SendAsync("Player", player);
            IGame game = _gameService.GetGame(roomCode, player);
            if (game.IsFull)
            {
                await StartGame(roomCode);
            }

        }
        public async Task JoinOpenGame()
        {
            var game = _gameService.FindOpenGame();
            var player = new Player(game.Id);
            _gameService.AddPlayerToGame(game.Id, player);
            await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
            await Clients.Group(game.Id).SendAsync("PlayerJoined", player);
            await Clients.Caller.SendAsync("Player", player);
            if (game.IsFull)
            {
                await StartGame(game.Id);
            }
        }
        public async Task StartGame(string gameId)
        {
            try
            {
                if (_gameService._games.TryGetValue(gameId, out _))
                {
                    string serverMessage = "Game is starting now hopefully";
                    await Clients.Group(gameId).SendAsync("GameStart", serverMessage);
                }

            }
            catch
            {
                throw new Exception("Game or player not found idk");
            }
        }
    }
}

