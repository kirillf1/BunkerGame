

using BunkerGame.GameTypes.BunkerTypes;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public class Building : BunkerComponentValue<Building>
    {
        private Building() { }
        public Building(double value, string description, BunkerObjectType bunkerObjectType) : base(description, value)
        {
            BunkerObjectType = bunkerObjectType;
        }

        public BunkerObjectType BunkerObjectType { get; }

        public override string ToString()
        {
            return Description;
        }
    }
}
