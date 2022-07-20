using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public abstract class GameSessionCommandHandlerBase<T> : IRequestHandler<T> where T : IRequest
    {
        protected readonly IGameSessionRepository gameSessionRepository;
        protected readonly IEventStore eventStore;
        protected GameSessionCommandHandlerBase(IGameSessionRepository gameSessionRepository, IEventStore eventStore)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.eventStore = eventStore;
        }
        protected async Task SaveEvents(GameSession gameSession)
        {
            await eventStore.Save(gameSession);
        }
        protected async Task<GameSession> GetGameSession(GameSessionId gameSessionId)
        {
            return await gameSessionRepository.GetGameSession(gameSessionId);
        }
        protected async Task Change(GameSessionId gameSessionId, Action<GameSession> update)
        {
            var gameSession = await GetGameSession(gameSessionId);
            update(gameSession);
            await SaveEvents(gameSession);
        }
        public abstract Task<Unit> Handle(T request, CancellationToken cancellationToken);
    }
}
