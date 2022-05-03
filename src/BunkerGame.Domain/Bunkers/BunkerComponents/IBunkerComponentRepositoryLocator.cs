using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public interface IBunkerComponentRepositoryLocator
    {
        public Task<T> GetBunkerComponent<T>(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : BunkerComponentEntity;
        public Task<IEnumerable<T>> GetBunkerComponents<T>(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : BunkerComponentEntity;
        public Task AddComponent<T>(T component) where T : BunkerComponentEntity;
        public Task RemoveComponent<T>(T component) where T : BunkerComponentEntity;
        public Task CommitChanges(CancellationToken cancellationToken);
    }
}
