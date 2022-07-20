using System.Linq.Expressions;

namespace BunkerGame.Domain.GameResults
{
    public interface IGameResultRepository
    {
        public Task<GameResult?> GetGameResult(GameSessionId id);
        public Task AddGameResult(GameResult gameResult);
        public Task RemoveGameResult(GameResult gameResult);
    }
}
