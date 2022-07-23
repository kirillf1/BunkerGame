namespace BunkerGame.Domain.Shared
{
    public record GameSessionId : Value<GameSessionId>
    {
        public Guid Value { get; private set; }
        public GameSessionId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Game session id cannot be empty");
            Value = value;
        }
        public static implicit operator Guid(GameSessionId gameSessionId)
        {
            return gameSessionId.Value;
        }
        public static implicit operator GameSessionId(Guid guid)
        {
            return new GameSessionId(guid);
        }
    }
}
