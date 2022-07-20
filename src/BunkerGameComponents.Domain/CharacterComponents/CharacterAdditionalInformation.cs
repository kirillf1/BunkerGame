using BunkerGame.GameTypes.CharacterTypes;
using BunkerGameComponents.Domain;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterAdditionalInformation : CharacterComponentAggregate
    {
        public CharacterAdditionalInformation(ComponentId id) : base(id)
        {
            AddInfType = AddInfType.Useless;
        }
        [JsonInclude]
        public AddInfType AddInfType { get; private set; }
        public void UpdateAddInfType(AddInfType addInfType)
        {
            AddInfType = addInfType;
        }
        public override string ToString()
        {
            return "Дополнительная информация: " + Description;
        }
    }
}
