using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public interface IBunkerComponentRepository<T> where T: BunkerComponent
    {
        public Task<T> GetBunkerComponent(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null);
        public Task<IEnumerable<T>> GetBunkerComponents(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null);
        public Task AddComponent(T component);
        public Task RemoveComponent(T component);
        public Task CommitChanges(CancellationToken cancellationToken);
    }
}
