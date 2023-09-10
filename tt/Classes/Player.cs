
using tt.Interfaces;
using tt.helpers;

namespace tt.Classes;

public class Player : IPlayer
{
    public string PlayerId { get; set; }
    // Add other properties as needed
    public string User { get; set; }

    public string CurrentGameId { get; set; }

    public bool IsAi  { get; set; }

    public Player(string gameId) 
    {
        PlayerId = GeneratePlayerId.Generate();
        User = GenerateUserName.Generate();
        CurrentGameId = gameId;
        IsAi = false;
    }
}

