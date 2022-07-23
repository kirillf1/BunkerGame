using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public record AdditionalInformation : CharacterComponent<AdditionalInformation>
    {
        private AdditionalInformation() { }
        public static AdditionalInformation DefaultAdditionalInformation = new AdditionalInformation("unknown", 0, AddInfType.Useless);
        public AdditionalInformation(string description, double value, AddInfType addInfType) : base(description, value)
        {
            AddInfType = addInfType;
        }
        public AddInfType AddInfType { get; set; }
        public override string ToString()
        {
            return "Дополнительная информация: " + Description;
        }
    }

}
