using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using BunkerGameComponents.Domain;
using BunkerGameComponents.Domain.Catastrophes;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class UpdateCatastropheHandler : GameSessionCommandHandlerBase<Commands.UpdateCatastrophe>
    {
        private readonly IGameComponentRepository<GameCatastrophe> catastropheRepository;

        public UpdateCatastropheHandler(IGameComponentRepository<GameCatastrophe> catastropheRepository, IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
            this.catastropheRepository = catastropheRepository;
        }

        public override async Task<Unit> Handle(Commands.UpdateCatastrophe request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            await UpdateCatastrophe(gameSession, request.Catastrophe);
            await SaveEvents(gameSession);
            return Unit.Value;
        }
        private async Task UpdateCatastrophe(GameSession gameSession, Catastrophe? catastrophe)
        {
            if (catastrophe == null)
            {
                var gameCatastrophe = await catastropheRepository.GetComponent(true);
                catastrophe = new Catastrophe(gameCatastrophe.CatastropheType, gameCatastrophe.DestructionPercent,
                    gameCatastrophe.SurvivedPopulationPercent, gameCatastrophe.Description, gameCatastrophe.Value, gameCatastrophe.HidingTerm);
            }
            gameSession.UpdateCatastrophe(catastrophe);

        }
    }
}
