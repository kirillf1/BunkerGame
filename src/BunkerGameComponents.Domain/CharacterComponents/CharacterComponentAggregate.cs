using BunkerGameComponents.Domain;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public abstract class CharacterComponentAggregate : AggregateRoot<ComponentId>, IGameComponent
    {
        protected CharacterComponentAggregate(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
        }
        [JsonInclude]
        public double Value { get; private set; }
        [JsonInclude]
        public string Description { get; private set; }

        public virtual void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"\"{nameof(description)}\" не может быть пустым или содержать только пробел.", nameof(description));
            }
            Description = description;
        }
        public virtual void UpdateValue(double value)
        {
            Value = value;
        }
    }
}
