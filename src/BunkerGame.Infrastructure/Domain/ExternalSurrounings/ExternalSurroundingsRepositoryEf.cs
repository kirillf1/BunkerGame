using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BunkerGame.Infrastructure.Domain.ExternalSurrounings
{
    public class ExternalSurroundingsRepositoryEf : IExternalSurroundingRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;
        DbSet<ExternalSurrounding> externalSurroundings;
        public ExternalSurroundingsRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
            externalSurroundings = bunkerGameDbContext.Set<ExternalSurrounding>();
        }
        public async Task AddExternalSurrounding(ExternalSurrounding externalSurrounding)
        {
            await externalSurroundings.AddAsync(externalSurrounding);
        }

        public async Task CommitChanges()
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<ExternalSurrounding?> GetExternalSurrounding(int id)
        {

            return await externalSurroundings.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ExternalSurrounding>> GetExternalSurroundings(int skipCount, int count, Expression<Func<ExternalSurrounding, bool>>? predicate = null)
        {
            var query = externalSurroundings.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.Skip(skipCount).Take(count).ToListAsync();
        }

        public Task RemoveGameSession(ExternalSurrounding externalSurrounding)
        {
            externalSurroundings.Remove(externalSurrounding);
            return Task.CompletedTask;
        }
    }
}
