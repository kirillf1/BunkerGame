namespace BunkerGame.Domain.Shared
{
    public record PlayerId : Value<PlayerId>
    {
        private PlayerId() { }
        public Guid Value { get; }
        public PlayerId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Player id cannot be empty");
            Value = value;
        }
    }
}
