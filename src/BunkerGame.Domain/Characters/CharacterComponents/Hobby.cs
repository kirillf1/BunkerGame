using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Hobby : CharacterComponent<Hobby>
    {
        
        public static readonly Hobby DefaultHobby = new Hobby("unknown", 0, HobbyType.Useless, 0);
        private Hobby() { }
        public Hobby(string description, double value, HobbyType hobbyType, byte hobbyExperience) : base(description, value)
        {
            HobbyType = hobbyType;
            if (hobbyExperience > 10)
                throw new ArgumentException("Hobby Experience must be less then 10");
            Experience = hobbyExperience;
        }
        public HobbyType HobbyType { get; }
        public byte Experience { get; }
        public Hobby UpdateExperience(byte years)
        {
            return new Hobby(Description, Value, HobbyType, years);
        }
    }

}
