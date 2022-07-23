namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public abstract record BunkerComponentValue<T> : Value<T> where T : Value<T>
    {
        protected BunkerComponentValue() { }
        protected BunkerComponentValue(string description, double value)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; }
        public double Value { get; }
    }
}
