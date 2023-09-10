namespace tt.Interfaces
{
    public interface IGame
    {
        string Id { get; }
        IReadOnlyList<IPlayer> Players { get; }
        bool IsFull { get; }
        void AddPlayer(IPlayer player);
        void RemovePlayer(IPlayer player);

        void AddMessage(IMessage message);

        List<IMessage> Messages { get; set; }

        IPlayer GetPlayer(string connectionId);

        List<IPlayer> GetAiPlayers();

    }
}
