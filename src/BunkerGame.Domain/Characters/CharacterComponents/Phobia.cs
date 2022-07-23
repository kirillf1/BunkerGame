using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public record Phobia : CharacterComponent<Phobia>
    {
        private Phobia() { }
        public static readonly Phobia DefaultPhobia = new Phobia("unknown", 0, PhobiaDebuffType.None);
        public Phobia(string description, double value, PhobiaDebuffType phobiaDebuffType) : base(description, value)
        {
            PhobiaDebuffType = phobiaDebuffType;
        }
        public PhobiaDebuffType PhobiaDebuffType { get; }

    }

}
