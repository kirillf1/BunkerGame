using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.Framework;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.BunkerComponents;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class UpdateBunkerHandler : GameSessionCommandHandlerBase<Commands.UpdateToRandomBunker>
    {
        private readonly IBunkerFactory bunkerFactory;

        public UpdateBunkerHandler(IBunkerFactory bunkerFactory, IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
            this.bunkerFactory = bunkerFactory;
        }

        public async override Task<Unit> Handle(Commands.UpdateToRandomBunker request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            var bunker = await bunkerFactory.CreateBunker();
            gameSession.UpdateBunker(bunker);
            await SaveEvents(gameSession);
            return Unit.Value;
        }
    }
    public class UpdateBunkerComponentByRepositoryHandler : IRequestHandler<Commands.UpdateBunkerBuildings>,
        IRequestHandler<Commands.UpdateBunkerEnviroment>, IRequestHandler<Commands.UpdateBunkerItems>, IRequestHandler<Commands.UpdateBunkerCondition>
    {
        private readonly IGameComponentsRepository gameComponentsRepository;
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IEventStore eventStore;

        public UpdateBunkerComponentByRepositoryHandler(IGameComponentsRepository gameComponentsRepository, IGameSessionRepository gameSessionRepository, IEventStore eventStore)
        {
            this.gameComponentsRepository = gameComponentsRepository;
            this.gameSessionRepository = gameSessionRepository;
            this.eventStore = eventStore;
        }
        public async Task<Unit> Handle(Commands.UpdateBunkerBuildings request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var buildingsCount = gameSession.Bunker.Buildings.Count;
            var newBuildings = new List<Building>(buildingsCount);

            if (request.Building == null)
            {
                var buildingsComponents = await gameComponentsRepository.GetComponents<BunkerObject>(0, buildingsCount, true);
                newBuildings.AddRange(buildingsComponents.Select(c => new Building(c.Value, c.Description, c.BunkerObjectType)));
            }
            else
            {

                newBuildings.AddRange(gameSession.Bunker.Buildings.Skip(1));
                newBuildings.AddRange(newBuildings);
            }
            await gameSession.UpdateBunker((builder, component) => builder.BuildBuildings(component), newBuildings, eventStore);
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UpdateBunkerEnviroment request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var bunkerEnviroment = request.BunkerEnviroment;
            if (bunkerEnviroment == null)
            {
                var enviromentComponent = await gameComponentsRepository.GetComponent<BunkerEnviroment>(true);
                bunkerEnviroment = new Enviroment(enviromentComponent.Description, enviromentComponent.Value,
                    enviromentComponent.EnviromentBehavior, enviromentComponent.EnviromentType);
            }
            await gameSession.UpdateBunker((builder, enviroment) => builder.BuildEnviroment(enviroment), bunkerEnviroment, eventStore);
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UpdateBunkerItems request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var itemsCount = gameSession.Bunker.Buildings.Count;
            var newItems = new List<Item>(itemsCount);

            if (request.Item == null)
            {
                var buildingsComponents = await gameComponentsRepository.GetComponents<ItemBunker>(0, itemsCount, true);
                newItems.AddRange(buildingsComponents.Select(c => new Item(c.Value, c.Description, c.ItemBunkerType)));
            }
            else
            {

                newItems.AddRange(gameSession.Bunker.Items.Skip(1));
                newItems.AddRange(newItems);
            }
            await gameSession.UpdateBunker((builder, component) => builder.BuildItems(component), newItems, eventStore);
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UpdateBunkerCondition request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var bunkerCondition = request.Condition;
            if (bunkerCondition == null)
            {
                var conditionComponent = await gameComponentsRepository.GetComponent<BunkerWall>(true);
                bunkerCondition = new Condition(conditionComponent.Value, conditionComponent.Description, conditionComponent.BunkerState);
            }
            await gameSession.UpdateBunker((builder, condition) => builder.BuildCondition(condition), bunkerCondition, eventStore);
            return Unit.Value;
        }

    }
    public class UpdateBunkerComponentValueHandler : IRequestHandler<Commands.UpdateBunkerSize>, IRequestHandler<Commands.UpdateBunkerSupplies>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IEventStore eventStore;
        Random Random;
        public UpdateBunkerComponentValueHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.eventStore = eventStore;
            Random = new Random();
        }
        public async Task<Unit> Handle(Commands.UpdateBunkerSize request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var bunkerSize = request.BunkerSize;
            if (bunkerSize == null)
                bunkerSize = new Size(Random.Next(150, 600));
            await gameSession.UpdateBunker((builder, size) => builder.BuildSize(size), bunkerSize, eventStore);
            return Unit.Value;
        }

        public async Task<Unit> Handle(Commands.UpdateBunkerSupplies request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            var supplies = request.Supplies;
            if (supplies == null)
                supplies = new Supplies(Random.Next(4, 10));
            await gameSession.UpdateBunker((builder, supplies) => builder.BuildSupplies(supplies), supplies, eventStore);
            return Unit.Value;
        }
    }
    public static class BunkerUpdateExtensions
    {
        public static async Task UpdateBunker<T>(this GameSession gameSession, Action<BunkerBuilder, T> update, T component, IEventStore eventStore)
        {
            var bunkerBuilder = new BunkerBuilder(gameSession.Bunker);
            update(bunkerBuilder, component);
            var newBunker = bunkerBuilder.Build();
            gameSession.UpdateBunker(newBunker);
            await eventStore.Save(gameSession);
        }
    }
}
