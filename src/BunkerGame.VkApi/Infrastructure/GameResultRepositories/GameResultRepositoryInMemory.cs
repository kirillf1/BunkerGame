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

        public async Task<GameResult?> GetGameResult(GameSessionId id)
        {
            return await Task.Run(() =>
            {
                GameResult? gameResult = null;
                memoryCache.TryGetValue(GetGameResultKey(id), out gameResult);
                return gameResult;
                
            });
        }
        public async Task RemoveGameResult(GameResult gameResult)
        {
            await Task.Run(() =>
            {
                memoryCache.Remove(GetGameResultKey(gameResult.Id));
            });
        }
        private static string GetGameResultKey(GameSessionId gameSessionId) => gameResultKey + gameSessionId.Value.ToString();
    }
}
