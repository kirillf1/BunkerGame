using BunkerGameComponents.Domain;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public abstract class CharacterComponentBase : IGameComponent
    {
        protected CharacterComponentBase(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
        }
        [JsonInclude]
        public double Value { get; set; }
        [JsonInclude]
        public string Description { get; set; }

        public ComponentId Id { get; }

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
