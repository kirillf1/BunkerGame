using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterItem : CharacterComponentAggregate
    {
        public CharacterItem(ComponentId id) : base(id)
        {
            CharacterItemType = CharacterItemType.None;
        }
        [JsonInclude]
        public CharacterItemType CharacterItemType { get; set; }
        public void UpdateCharacterItemType(CharacterItemType characterItemType)
        {
            CharacterItemType = characterItemType;
        }
        public override string ToString()
        {
            return Description;
        }
    }
}
