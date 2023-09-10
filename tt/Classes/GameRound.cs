using tt.enums;

namespace tt.Classes
{
    public class GameRound
    {
            public int RoundNumber { get; private set; }

            public Phase CurrentPhase { get; private set; }

            // Constructor
            public GameRound(int roundNumber)
            {
                RoundNumber = roundNumber;
                CurrentPhase = Phase.Discussion; // Start with the discussion phase
            }

            public void StartDiscussionPhase()
            {
                // Code to start the discussion phase...
            }

            public void StartTaskPhase()
            {
                // Code to start the task phase...
            }

            public void StartVotingPhase()
            {
                // Code to start the voting phase...
            }

            public void EndRound()
            {
                // Code to end the round...
            }
        }
}
