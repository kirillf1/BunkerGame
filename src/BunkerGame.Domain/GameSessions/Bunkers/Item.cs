using BunkerGame.GameTypes.BunkerTypes;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public record Item : BunkerComponentValue<Item>
    {
        private Item() { }
        public Item(double value, string description, ItemBunkerType itemBunkerType = ItemBunkerType.Useless) : base(description, value)
        {
            ItemBunkerType = itemBunkerType;
        }
        public ItemBunkerType ItemBunkerType { get; }
        public override string ToString()
        {
            return Description;
        }
    }
}
