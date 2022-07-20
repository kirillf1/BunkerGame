namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Age : Value<Age>
    {
        public Age()
        {
            Years = 16;
        }
        public Age(int years)
        {
            if (years <= 16)
                throw new ArgumentException("Age must be more than 16");
            Years = years;
        }
        public int Years { get; }
    }
}
