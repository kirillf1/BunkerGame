using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameResults
{
    public interface IGameResultRepository
    {
        public Task<IEnumerable<GameResult>> GetResults(int skipCount, int count, Expression<Func<GameResult, bool>>? predicate = null);
        public Task<GameResult?> GetGameResult(long id);
        public Task AddGameResult(GameResult gameResult);
        public Task RemoveGameResult(GameResult gameResult);
        public Task CommitChanges();
    }
}
