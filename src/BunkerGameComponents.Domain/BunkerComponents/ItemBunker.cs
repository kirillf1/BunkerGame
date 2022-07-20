using BunkerGame.GameTypes.BunkerTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.BunkerComponents
{
    public class ItemBunker : AggregateRoot<ComponentId>, IGameComponent
    {

        public ItemBunker(ComponentId id)
        {
            Id = id;
            Value = 0;
            Description = "unknown";
            ItemBunkerType = ItemBunkerType.Useless;
        }
        [JsonInclude]
        public ItemBunkerType ItemBunkerType { get; private set; }
        [JsonInclude]
        public double Value { get; private set; }
        [JsonInclude]
        public string Description { get; private set; }
        public void UpdateType(ItemBunkerType bunkerType)
        {
            ItemBunkerType = bunkerType;
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
