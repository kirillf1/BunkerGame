namespace BunkerGame.Domain.Players
{
    public class Player : AggregateRoot<PlayerId>
    {
        private Player()
        {

        }
        public Player(PlayerId id, string firstName)
        {
            Id = id;
            FirstName = firstName;
            CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreationTime { get; }
        public int WinGamesCount { get; private set; }
        public int LoseGamesCount { get; private set; }
    }
}
