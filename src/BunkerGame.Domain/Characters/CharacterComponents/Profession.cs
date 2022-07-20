using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Profession : CharacterComponent<Profession>
    {
        private Profession() { }
        public static readonly Profession DefaultProfession = new Profession("unknown", 0, ProfessionSkill.None, ProfessionType.Unknown, 0);
        public Profession(string description, double value, ProfessionSkill professionSkill,
            ProfessionType professionType, byte profExp) : base(description, value)
        {
            ProfessionSkill = professionSkill;
            ProfessionType = professionType;
            if (profExp > 20)
                throw new ArgumentException("Experience must be less then 20");
            Experience = profExp;
        }
        public ProfessionSkill ProfessionSkill { get; }
        public ProfessionType ProfessionType { get; }
        public byte Experience { get; }
        public Profession UpdateExperience(byte years)
        {
            return new Profession(Description, Value, ProfessionSkill, ProfessionType, years);
        }
    }

}
