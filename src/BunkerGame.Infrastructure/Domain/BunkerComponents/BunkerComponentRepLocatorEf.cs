using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Infrastructure.Database;
using System.Linq.Expressions;

namespace BunkerGame.Infrastructure.Domain.BunkerComponents
{
    public class BunkerComponentRepLocatorEf : IBunkerComponentRepositoryLocator
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public BunkerComponentRepLocatorEf(BunkerGameDbContext bunkerGameDbContext )
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }
        public async Task AddComponent<T>(T component) where T : BunkerComponentEntity
        {
           await new BunkerComponentRepositoryEf<T>(bunkerGameDbContext).AddComponent(component);
        }

        public async Task CommitChanges(CancellationToken cancellationToken)
        {
           await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<T> GetBunkerComponent<T>(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : BunkerComponentEntity
        {
          return  await new BunkerComponentRepositoryEf<T>(bunkerGameDbContext).GetBunkerComponent(needShuffle, predicate);
        }

        public async Task<IEnumerable<T>> GetBunkerComponents<T>(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : BunkerComponentEntity
        {
            return await new BunkerComponentRepositoryEf<T>(bunkerGameDbContext).GetBunkerComponents(count,needShuffle, predicate);
        }

        public async Task RemoveComponent<T>(T component) where T : BunkerComponentEntity
        {
             await new BunkerComponentRepositoryEf<T>(bunkerGameDbContext).RemoveComponent(component);
        }
    }
}
