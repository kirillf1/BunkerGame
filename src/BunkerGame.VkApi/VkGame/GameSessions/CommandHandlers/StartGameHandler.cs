using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.Framework;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.Catastrophes;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class StartGameHandler : GameSessionCommandHandlerBase<Commands.StartGame>
    {
        private readonly IGameComponentRepository<GameCatastrophe> catastropheRepository;
        private readonly IBunkerFactory bunkerFactory;

        public StartGameHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore,
            IGameComponentRepository<GameCatastrophe> catastropheRepository, IBunkerFactory bunkerFactory) : base(gameSessionRepository, eventStore)
        {
            this.catastropheRepository = catastropheRepository;
            this.bunkerFactory = bunkerFactory;
        }

        public override async Task<Unit> Handle(Commands.StartGame request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            gameSession.StartGame();
            if (gameSession.GameState != GameState.Started && gameSession.Characters.Count < 5)
                throw new InvalidOperationException("Can't start game");
            await CreateCatastrophe(gameSession);
            await CreateBunker(gameSession);
            await SaveEvents(gameSession);
            return Unit.Value;
        }
        private async Task CreateCatastrophe(GameSession gameSession)
        {
            var catastropheComponent = await catastropheRepository.GetComponent(true);
            var catastrophe = new Catastrophe(catastropheComponent.CatastropheType, catastropheComponent.DestructionPercent,
                catastropheComponent.SurvivedPopulationPercent, catastropheComponent.Description, catastropheComponent.Value, catastropheComponent.HidingTerm);
            gameSession.UpdateCatastrophe(catastrophe);
        }
        private async Task CreateBunker(GameSession gameSession)
        {
            var bunker = await bunkerFactory.CreateBunker();
            gameSession.UpdateBunker(bunker);
        }
    }
}
