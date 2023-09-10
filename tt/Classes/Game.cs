using tt.Interfaces;
using shortid;
using shortid.Configuration;

namespace tt.Classes
{
    public class Game : IGame
    {

        public string Id
        { get; } = ShortId.Generate(options: new GenerationOptions(useNumbers: true, length: 8));

        public IReadOnlyList<IPlayer> Players => _players.AsReadOnly();

        public bool IsFull => _players.Count(p => !p.IsAi) >= 5;

        public List<IMessage> Messages { get; set; } = new List<IMessage>();

        public void AddPlayer(IPlayer player)
        {
            if (!IsFull)
            {
                _players.Add(player);
            }
            else
            {
                throw new InvalidOperationException("The game is full.");
            }
        }
        public void AddMessage(IMessage message)
        {
            Messages.Add(message);
        }
        public void RemovePlayer(IPlayer player)
        {
            _players.Remove(player);
        }
        public IPlayer GetPlayer(string playerId)
        {
            var player = _players.FirstOrDefault(p => p.PlayerId == playerId);
            if (player == null)
            {
                throw new ArgumentException("The player does not exist.", nameof(playerId));
            }

            return player;
        }
        private List<IPlayer> _players = new List<IPlayer>();

        public List<IPlayer> GetAiPlayers()
        {
            List<IPlayer> aiPlayers = new List<IPlayer>();
            foreach (var player in _players)
            {
                if (player.IsAi == true)
                {
                    aiPlayers.Add(player);
                }
            }
            return aiPlayers;
        }   
    }

}


