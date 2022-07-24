using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace BunkerGame.VkApi.Infrastructure.GameSessionRepositories
{
    public class GameSessionRepositoryChache : IGameSessionRepository
    {
        private readonly IMemoryCache memoryCache;
        private readonly TimeSpan gameSessionLifeTime;

        public GameSessionRepositoryChache(IMemoryCache memoryCache,TimeSpan gameSessionLifeTime)
        {
            this.memoryCache = memoryCache;
            this.gameSessionLifeTime = gameSessionLifeTime;
        }
        public Task AddGameSession(GameSession gameSession)
        {
            if (memoryCache.TryGetValue(gameSession.Id,out _))
            {
                return Task.CompletedTask;
            }
            memoryCache.Set(gameSession.Id, gameSession,gameSessionLifeTime);
            return Task.CompletedTask;
        }

        public Task<GameSession> GetGameSession(GameSessionId gameSessionId)
        {
            return Task.FromResult(memoryCache.Get<GameSession>(gameSessionId));
        }

        public Task RemoveGameSession(GameSession gameSession)
        {
            if (memoryCache.TryGetValue(gameSession.Id, out gameSession))
            {
                return Task.CompletedTask;
            }
            memoryCache.Remove(gameSession.Id);
            return Task.CompletedTask;
        }
    }
}
