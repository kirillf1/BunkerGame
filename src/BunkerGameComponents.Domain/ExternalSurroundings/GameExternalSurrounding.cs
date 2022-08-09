using BunkerGame.GameTypes.GameComponentTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.ExternalSurroundings
{
    public class GameExternalSurrounding : IGameComponent
    {
        public GameExternalSurrounding(ComponentId id)
        {
            Id = id;
            Description = "unknown";
            Value = 0;
            SurroundingType = SurroundingType.Unknown;
        }
        [JsonInclude]
        public string Description { get; set; }
        [JsonInclude]
        public double Value { get; set; }
        [JsonInclude]
        public SurroundingType SurroundingType { get; set; }

        public ComponentId Id { get; }

        public void UpdateSurroundingType(SurroundingType surroundingType)
        {
            SurroundingType = surroundingType;
        }
        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateValue(double value)
        {
            Value = value;
        }
    }
}
