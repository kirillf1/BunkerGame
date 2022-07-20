using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.Infrastructure.Database.GameDbContext;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.VkApi.Infrastructure.GameSessionRepositories
{
    public class GameSessionRepositoryEf : IGameSessionRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public GameSessionRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }
        public async Task AddGameSession(GameSession gameSession)
        {
            await bunkerGameDbContext.GameSessions.AddAsync(gameSession);
        }

        public Task<GameSession> GetGameSession(GameSessionId gameSessionId)
        {
            return bunkerGameDbContext.GameSessions.FirstAsync(c => c.Id == gameSessionId);
        }

        public Task RemoveGameSession(GameSession gameSession)
        {
            bunkerGameDbContext.Remove(gameSession);
            return Task.CompletedTask;
        }
    }
}