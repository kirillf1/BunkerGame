using BunkerGame.GameTypes.GameComponentTypes;

namespace BunkerGame.Domain.GameSessions
{
    public record ExternalSurrounding : Value<ExternalSurrounding>
    {
        private ExternalSurrounding() { }
        public ExternalSurrounding(string description, double value = 0, SurroundingType surroundingType = SurroundingType.Unknown)
        {
            Description = description;
            Value = value;
            SurroundingType = surroundingType;
        }
        public int Id { get; }
        public string Description { get; }
        public double Value { get; }
        public SurroundingType SurroundingType { get; }
    }
}
