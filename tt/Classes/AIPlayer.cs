using tt.enums;
using tt.helpers;
using tt.Interfaces;

namespace tt.Classes
{
    public class AIPlayer : IPlayer
    {
            public string PlayerId { get; private set; }

            public string User { get; private set; }

            public string CurrentGameId { get; private set; }

            public SystemMessage SystemMessage { get; private set; }

            public bool IsAi { get; set; }

            // Constructor
            public AIPlayer(string gameId)
            {
                PlayerId = GeneratePlayerId.Generate(); // Method to generate a unique player ID
                User = GenerateUserName.Generate(); // Method to generate a random username
                CurrentGameId = gameId;
                SystemMessage = AIPersonality.GenerateSystemMessage(User);
            IsAi = true;
                
            }

        }
}
