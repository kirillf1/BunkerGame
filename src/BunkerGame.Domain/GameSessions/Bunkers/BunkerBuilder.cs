

using BunkerGame.GameTypes.BunkerTypes;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public class BunkerBuilder
    {
        private Supplies Supplies;
        private IEnumerable<Building> Buildings;
        private IEnumerable<Item> Items;
        private Condition Condition;
        private Size BunkerSize;
        private Enviroment Enviroment;
        public BunkerBuilder(Bunker oldBunker)
        {
            Supplies = oldBunker.Supplies;
            Condition = oldBunker.Condition;
            Buildings = oldBunker.Buildings;
            Items = oldBunker.Items;
            Enviroment = oldBunker.Enviroment;
            BunkerSize = oldBunker.Size;
        }
        public BunkerBuilder()
        {
            Supplies = new Supplies(0);
            Condition = new Condition(0, "unknown");
            Buildings = new List<Building>();
            Items = new List<Item>();
            Enviroment = new Enviroment("unknown", 0, EnviromentBehavior.Unknown, EnviromentType.Unknown);
            BunkerSize = new Size(200);
        }
        public BunkerBuilder BuildSupplies(Supplies supplies)
        {
            Supplies = supplies;
            return this;
        }
        public BunkerBuilder BuildEnviroment(Enviroment bunkerEnviroment)
        {
            Enviroment = bunkerEnviroment;
            return this;
        }
        public BunkerBuilder BuildCondition(Condition condition)
        {
            Condition = condition;
            return this;
        }
        public BunkerBuilder BuildSize(Size bunkerSize)
        {
            BunkerSize = bunkerSize;
            return this;
        }
        public BunkerBuilder BuildBuildings(IEnumerable<Building> buildings)
        {
            Buildings = buildings;
            return this;
        }
        public BunkerBuilder BuildItems(IEnumerable<Item> item)
        {
            Items = item;
            return this;
        }
        public Bunker Build()
        {
            return new Bunker(BunkerSize, Supplies, Condition, Items, Buildings, Enviroment);
        }
    }
}
