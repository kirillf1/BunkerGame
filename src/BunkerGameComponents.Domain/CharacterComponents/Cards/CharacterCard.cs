using BunkerGameComponents.Domain;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents.Cards
{
    public class CharacterCard : CharacterComponentAggregate
    {
        public CharacterCard(ComponentId id) : base(id)
        {
            CardMethod = new Method();
        }
        [JsonInclude]
        public Method CardMethod { get; private set; }
        [JsonInclude]
        public bool IsSpecial { get; private set; }
        public void UpdateSpeciality(bool isSpecial)
        {
            IsSpecial = isSpecial;
        }
        public void UpdateCardMethod(Method method)
        {
            CardMethod = method;
        }
        public override string ToString()
        {
            return Description;
        }
    }
}
