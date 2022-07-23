namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public record Bunker : Value<Bunker>
    {
        public static Bunker DefaultBunker = new BunkerBuilder().Build();
        private Bunker() { }
        public Bunker(Size size, Supplies supplies, Condition condition, IEnumerable<Item> items,
             IEnumerable<Building> buildings, Enviroment enviroment)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            Supplies = supplies ?? throw new ArgumentNullException(nameof(supplies));
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            Items = new List<Item>(items);
            Buildings = new List<Building>(buildings);
            Enviroment = enviroment ?? throw new ArgumentNullException(nameof(enviroment));
        }
        public Size Size { get; }
        public Supplies Supplies { get; }
        public Condition Condition { get; }
        public IReadOnlyCollection<Item> Items { get; }
        public  IReadOnlyCollection<Building> Buildings { get; }
        public Enviroment Enviroment { get; }
    }

}
