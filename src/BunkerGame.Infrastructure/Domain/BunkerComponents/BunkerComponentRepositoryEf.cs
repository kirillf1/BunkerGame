using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.BunkerComponents
{
    public class BunkerComponentRepositoryEf<T> : IBunkerComponentRepository<T> where T : BunkerComponentEntity
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;
        DbSet<T> components;
        public BunkerComponentRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
            components = bunkerGameDbContext.Set<T>();
            
        }
        public async Task AddComponent(T component)
        {
            await components.AddAsync(component);
        }

        public async Task CommitChanges(CancellationToken cancellationToken)
        {
            await bunkerGameDbContext.SaveChangesAsync();

        }

        public async Task<T> GetBunkerComponent(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null)
        {
            var query = components.AsQueryable();
            if(predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c=>Guid.NewGuid());
            return await query.FirstAsync();
        }

        public async Task<IEnumerable<T>> GetBunkerComponents(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null)
        {
            var query = components.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return await query.Take(count).ToListAsync();
        }

        public Task RemoveComponent(T component)
        {
            components.Remove(component);
            return Task.CompletedTask;
        }
    }
}
