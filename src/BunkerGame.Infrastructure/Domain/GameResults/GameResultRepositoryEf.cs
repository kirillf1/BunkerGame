using BunkerGame.Domain.GameResults;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BunkerGame.Infrastructure.Domain.GameResults
{
    public sealed class GameResultRepositoryEf : IGameResultRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;
        DbSet<GameResult> gameResults;
        public GameResultRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
            gameResults = bunkerGameDbContext.GameResults;
        }
        public async Task AddGameResult(GameResult gameResult)
        {
            await gameResults.AddAsync(gameResult);
        }

        public async Task CommitChanges()
        {
           await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<GameResult?> GetGameResult(long id)
        {
            return await gameResults.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<GameResult>> GetResults(int skipCount, int count, Expression<Func<GameResult, bool>>? predicate = null)
        {
            var query = gameResults.AsQueryable();
            if(predicate != null)
                query = query.Where(predicate);
            return await query.Skip(skipCount).Take(count).ToListAsync();
        }

        public Task RemoveGameResult(GameResult gameResult)
        {
            gameResults.Remove(gameResult);
            return Task.CompletedTask;
        }
    }
}
