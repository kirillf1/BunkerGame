using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.Infrastructure.GameResultRepositories
{
    public class GameResultRepositoryInMemory : IGameResultRepository
    {
        private readonly IMemoryCache memoryCache;
        private const string gameResultKey = "gameResult";
        public GameResultRepositoryInMemory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public Task AddGameResult(GameResult gameResult)
        {
            if (memoryCache.TryGetValue<GameResult>(GetGameResultKey(gameResult.Id), out var newGameReuslt))
            {
                return Task.CompletedTask;
            }
            memoryCache.Set(GetGameResultKey(gameResult.Id), gameResult);
            return Task.CompletedTask;
        }

        public Task<GameResult?> GetGameResult(GameSessionId id)
        {
            GameResult? gameResult = null;
            memoryCache.TryGetValue(GetGameResultKey(id), out gameResult);
            return Task.FromResult(gameResult);
        }
        public Task RemoveGameResult(GameResult gameResult)
        {
            memoryCache.Remove(GetGameResultKey(gameResult.Id));
            return Task.CompletedTask;
        }
        private static string GetGameResultKey(GameSessionId gameSessionId) => gameResultKey + gameSessionId.Value.ToString();
    }
}
