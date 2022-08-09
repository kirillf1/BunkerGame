using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.CharacterComponents.Cards;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterProfession : CharacterComponentBase
    {
        public CharacterProfession(ComponentId id) : base(id)
        {
            ProfessionSkill = ProfessionSkill.None;
            ProfessionType = ProfessionType.Unknown;
        }
        [JsonInclude]
        public CharacterCard? Card { get;  set; }
        [JsonInclude]
        public ProfessionSkill ProfessionSkill { get; set; }
        [JsonInclude]
        public ProfessionType ProfessionType { get; set; }
        [JsonInclude]
        public CharacterItem? CharacterItem { get; set; }

        public void UpdateProfessionSkill(ProfessionSkill professionSkill)
        {
            ProfessionSkill = professionSkill;
        }
        public void UpdateProfessionType(ProfessionType professionType)
        {
            ProfessionType = professionType;
        }
        public void RemoveCharacterItem()
        {
            CharacterItem = default;
        }
        public void RemoveCard()
        {
            Card = default;
        }
        public void UpdateCharacterItem(CharacterItem characterItem)
        {
            CharacterItem = characterItem ?? throw new ArgumentNullException(nameof(characterItem));
        }
        public void UpdateCard(CharacterCard card)
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));
        }
    }
}
