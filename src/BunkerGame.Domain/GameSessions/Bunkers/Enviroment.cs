using BunkerGame.GameTypes.BunkerTypes;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public record Enviroment : BunkerComponentValue<Enviroment>
    {
        private Enviroment() { }
        public Enviroment(string description, double value, EnviromentBehavior enviromentBehavior,
            EnviromentType enviromentType) : base(description, value)
        {
            EnviromentType = enviromentType;
            EnviromentBehavior = enviromentBehavior;
        }
        public EnviromentBehavior EnviromentBehavior { get; }
        public EnviromentType EnviromentType { get; }
        public override string ToString()
        {
            return $"В бункере живут: {Description}";
        }
    }

}
