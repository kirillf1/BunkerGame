using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterPhobia : CharacterComponentBase
    {
        public CharacterPhobia(ComponentId id) : base(id)
        {
            PhobiaDebuffType = PhobiaDebuffType.None;
        }
        [JsonInclude]
        public PhobiaDebuffType PhobiaDebuffType { get;  set; }
        public void UpdatePhobiaDebuffType(PhobiaDebuffType phobiaDebuffType)
        {
            PhobiaDebuffType = phobiaDebuffType;
        }
    }
}
