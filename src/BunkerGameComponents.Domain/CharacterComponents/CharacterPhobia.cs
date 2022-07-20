using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterPhobia : CharacterComponentAggregate
    {
        public CharacterPhobia(ComponentId id) : base(id)
        {
            PhobiaDebuffType = PhobiaDebuffType.None;
        }
        [JsonInclude]
        public PhobiaDebuffType PhobiaDebuffType { get; private set; }
        public void UpdatePhobiaDebuffType(PhobiaDebuffType phobiaDebuffType)
        {
            PhobiaDebuffType = phobiaDebuffType;
        }
    }
}
