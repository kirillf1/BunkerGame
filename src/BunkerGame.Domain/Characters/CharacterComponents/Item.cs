using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public record Item : CharacterComponent<Item>
    {
        private Item() { }
        public Item(string description, double value, CharacterItemType characterItemType, bool fromProfession)
            : base(description, value)
        {
            CharacterItemType = characterItemType;
            FromProfession = fromProfession;
        }
        public CharacterItemType CharacterItemType { get; }
        public bool FromProfession { get; }
        public override string ToString()
        {
            return Description;
        }
    }

}
