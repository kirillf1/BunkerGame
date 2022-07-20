using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterTrait : CharacterComponentAggregate
    {
        public CharacterTrait(ComponentId id) : base(id)
        {
            TraitType = TraitType.Negative;
        }

        [JsonInclude]
        public TraitType TraitType { get; private set; }
        public void UpdateTraitType(TraitType traitType) => TraitType = traitType; 
    }
}
