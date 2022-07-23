using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public record Trait : CharacterComponent<Trait>
    {
        public static Trait DefaultTrait = new Trait("unknown", 0, TraitType.Negative);
        private Trait() { }
        public Trait(string description, double value, TraitType traitType) : base(description, value)
        {
            TraitType = traitType;
        }
        public TraitType TraitType { get; }
        public override string ToString()
        {
            return "Черта характера: " + Description;
        }
    }
}
