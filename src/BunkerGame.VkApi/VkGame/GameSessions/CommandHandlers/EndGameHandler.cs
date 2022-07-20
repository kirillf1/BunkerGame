using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class EndGameHandler : GameSessionCommandHandlerBase<Commands.EndGame>
    {
        public EndGameHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public override async Task<Unit> Handle(Commands.EndGame request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            if (gameSession.GameState != GameState.Started)
                throw new InvalidOperationException("Игра не начата!");
            gameSession.EndGame();
            await eventStore.Save(gameSession);
            return Unit.Value;
        }
    }
}
