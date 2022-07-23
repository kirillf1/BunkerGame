namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public record Size : Value<Size>
    {
        private Size() { }
        public Size(double value)
        {
            Value = value;
        }
        public double Value { get; }
        public byte CalculateAvailableObjectsInBunker()
        {
            return Value switch
            {
                double size when size > 500 => 3,
                double size when size < 200 => 1,
                _ => 2
            };
        }
        public override string ToString()
        {
            return $"Размер бункера: {Value}";
        }
    }
}
