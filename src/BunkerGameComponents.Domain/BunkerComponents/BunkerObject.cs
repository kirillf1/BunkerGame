using BunkerGame.GameTypes.BunkerTypes;
using BunkerGameComponents.Domain;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.BunkerComponents
{
    public class BunkerObject : AggregateRoot<ComponentId>, IGameComponent
    {
        public BunkerObject(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
            BunkerObjectType = BunkerObjectType.Useless;
        }
        [JsonInclude]
        public BunkerObjectType BunkerObjectType { get; private set; }
        [JsonInclude]

        public double Value { get; private set; }
        [JsonInclude]

        public string Description { get; private set; }

        public void UpdateType(BunkerObjectType bunkerObjectType)
        {
            BunkerObjectType = bunkerObjectType;
        }
        public override string ToString()
        {
            return Description;
        }

        public void UpdateValue(double value)
        {
            Value = value;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
   
}
