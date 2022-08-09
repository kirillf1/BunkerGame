using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterHealth : CharacterComponentBase
    {
        public CharacterHealth(ComponentId id) : base(id)
        {
            HealthType = HealthType.FullHealth;
        }
        [JsonInclude]
        public HealthType HealthType { get;  set; }
        public void UpdateHealthType(HealthType healthType)
        {
            HealthType = healthType;
        }
    }
}
