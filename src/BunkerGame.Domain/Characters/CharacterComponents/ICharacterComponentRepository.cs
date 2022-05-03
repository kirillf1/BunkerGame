using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public interface ICharacterComponentRepository<T>  where T: CharacterComponent
    {
        public Task<T> GetCharacterComponent(bool needShuffle = true,Expression<Func<T, bool>>? predicate = null);
        public Task<IEnumerable<T>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null);
        public Task AddComponent(T component);
        public Task CommitChanges(CancellationToken cancellationToken);
        public Task RemoveComponent(T component);
    }
}
