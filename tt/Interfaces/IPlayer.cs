namespace tt.Interfaces
{
    public interface IPlayer
    {
        string PlayerId { get; }

        string User { get; }

        string CurrentGameId { get;  }

        bool IsAi { get; set; }
    }
}
