using BunkerGame.GameTypes.BunkerTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.BunkerComponents
{
    public class BunkerEnviroment : AggregateRoot<ComponentId>, IGameComponent
    {
        public BunkerEnviroment(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
            EnviromentBehavior = EnviromentBehavior.Unknown;
            EnviromentType = EnviromentType.Unknown;
        }
        [JsonInclude]
        public EnviromentBehavior EnviromentBehavior { get; private set; }
        [JsonInclude]
        public EnviromentType EnviromentType { get; private set; }
        [JsonInclude]
        public double Value { get; private set; }
        [JsonInclude]
        public string Description { get; private set; }

        public void UpdateEnviromentBehavior(EnviromentBehavior enviromentBehavior)
        {
            EnviromentBehavior = enviromentBehavior;
        }
        public void UpdateEnviromentType(EnviromentType enviromentType)
        {
            EnviromentType = enviromentType;
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
