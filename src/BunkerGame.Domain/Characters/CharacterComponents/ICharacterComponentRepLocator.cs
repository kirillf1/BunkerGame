using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public interface ICharacterComponentRepLocator
    {
        public Task<T> GetCharacterComponent<T>(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : CharacterComponent;
        public Task<IEnumerable<T>> GetCharacterComponents<T>(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : CharacterComponent;
        public Task AddComponent<T>(T component) where T : CharacterComponent;
        public Task CommitChanges(CancellationToken cancellationToken);
        public Task RemoveComponent<T>(T component) where T : CharacterComponent;
    }
}
