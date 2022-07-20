using BunkerGameComponents.Domain;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BunkerGameComponents.Infrastructure.Domain
{
    public class GameComponentRepositoryEFBase<T> : IGameComponentRepository<T> where T : class, IGameComponent
    {
        private readonly GameComponentDbContext ComponentDbContext;
        protected DbSet<T> dbComponent;
        public GameComponentRepositoryEFBase(GameComponentDbContext ComponentDbContext)
        {
            this.ComponentDbContext = ComponentDbContext;
            dbComponent = ComponentDbContext.Set<T>();
        }
        public async Task AddComponent(T component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(T));
            await dbComponent.AddAsync(component);
        }
        public Task<T> GetComponent(ComponentId id)
        {
            return dbComponent.FirstAsync(c => c.Id == id);
        }

        public async Task<T> GetComponent(bool needShuffle, Expression<Func<T, bool>>? predicate = null)
        {
            var query = dbComponent.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return await query.FirstAsync();
        }

        public async Task<IEnumerable<T>> GetComponents(int skipCount, int count, bool needShuffle, Expression<Func<T, bool>>? predicate = null)
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
