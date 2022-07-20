using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.BunkerComponents;

namespace BunkerGame.VkApi.VkGame.GameSessions
{
    public class BunkerFactory : IBunkerFactory
    {
        private readonly IGameComponentsRepository gameComponentsRepository;
        private Random random;
        public BunkerFactory(IGameComponentsRepository gameComponentsRepository)
        {
            this.gameComponentsRepository = gameComponentsRepository;
            random = new Random();
        }
        public async Task<Bunker> CreateBunker()
        {
            var size = GetRandomSize();
            var availableObjectsCount = size.CalculateAvailableObjectsInBunker();
            return new BunkerBuilder()
                 .BuildSize(size)
                 .BuildSupplies(GetRandomSupplies())
                 .BuildCondition(await ConvertComponent<BunkerWall, Condition>(
                         c => new Condition(c.Value, c.Description, c.BunkerState)))
                 .BuildItems(await ConvertComponents<ItemBunker, Item>(availableObjectsCount,
                          c => new Item(c.Value, c.Description, c.ItemBunkerType)))
                 .BuildBuildings(await ConvertComponents<BunkerObject, Building>(availableObjectsCount,
                         c => new Building(c.Value, c.Description, c.BunkerObjectType)))
                 .BuildEnviroment(await ConvertComponent<BunkerEnviroment, Enviroment>(
                         c => new Enviroment(c.Description, c.Value, c.EnviromentBehavior, c.EnviromentType)))
                 .Build();
        }
        private Supplies GetRandomSupplies()
        {
            return new Supplies(random.Next(3, 10));
        }
        private Size GetRandomSize()
        {
            var bunkerSize = random.Next(150, 600);
            return new Size(bunkerSize);
        }
        private async Task<V> ConvertComponent<T, V>(Func<T, V> convert) where T : class, IGameComponent
        {
            var component = await gameComponentsRepository.GetComponent<T>(true);
            return convert(component);
        }
        private async Task<IEnumerable<V>> ConvertComponents<T, V>(int count, Func<T, V> convert) where T : class, IGameComponent
        {
            var components = await gameComponentsRepository.GetComponents<T>(0, count, true);
            var bunkerComponents = new List<V>();
            foreach (var component in components)
            {
                bunkerComponents.Add(convert(component));
            }
            return bunkerComponents;
        }
    }
}
