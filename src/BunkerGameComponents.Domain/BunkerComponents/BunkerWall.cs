using BunkerGame.GameTypes.BunkerTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.BunkerComponents
{
    public class BunkerWall : AggregateRoot<ComponentId>, IGameComponent
    {
        public BunkerWall(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
            BunkerState = BunkerState.Unbroken;
        }
        [JsonInclude]
        public BunkerState BunkerState { get; private set; }
        [JsonInclude]

        public double Value { get; private set; }
        [JsonInclude]

        public string Description { get; private set; }

        public void UpdateBukerState(BunkerState bunkerState)
        {
            BunkerState = bunkerState;
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
