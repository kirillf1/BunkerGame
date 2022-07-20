using BunkerGame.GameTypes.BunkerTypes;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public class Condition : BunkerComponentValue<Condition>
    {
        private Condition() { }
        public Condition(double value, string description, BunkerState bunkerState = BunkerState.Unbroken) : base(description, value)
        {
            BunkerState = bunkerState;
        }
        public BunkerState BunkerState { get; }

        public override string ToString()
        {
            return Description;
        }
    }

}
