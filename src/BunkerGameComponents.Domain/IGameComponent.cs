using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain
{
    public interface IGameComponent
    {
        public double Value { get; }
        public string Description { get; }
        public ComponentId Id { get; }
        public void UpdateValue(double value);
        public void UpdateDescription(string description);
    }
}
