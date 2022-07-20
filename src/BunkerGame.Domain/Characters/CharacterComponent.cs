namespace BunkerGame.Domain.Characters
{
    public abstract class CharacterComponent<T> : Value<T> where T : Value<T>
    {
        protected CharacterComponent(){}
        public CharacterComponent(string description, double value)
        {
            Description = description;
            Value = value;
        }
        public double Value { get; }
        public string Description { get; }
    }
}
