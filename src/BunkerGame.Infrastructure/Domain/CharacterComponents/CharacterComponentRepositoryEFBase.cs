using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class CharacterComponentRepositoryEFBase<T> : ICharacterComponentRepository<T> where T : CharacterComponent
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;
        protected DbSet<T> dbComponent;
        public CharacterComponentRepositoryEFBase(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
            dbComponent = bunkerGameDbContext.Set<T>();
        }
        public async Task AddComponent(T component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(T));
            await dbComponent.AddAsync(component);
        }

        public async Task CommitChanges(CancellationToken cancellationToken)
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetCharacterComponent(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null)
        {
            var query = dbComponent.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return await query.FirstAsync();
        }

        public virtual async Task<IEnumerable<T>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null)
        {
            var query = dbComponent.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return await query.Take(count).ToListAsync();
        }

        public Task RemoveComponent(T component)
        {
            dbComponent.Remove(component);
            return Task.CompletedTask;
        }
    }
}
