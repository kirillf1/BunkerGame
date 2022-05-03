using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public abstract class MockRepository<T> : ICharacterComponentRepository<T> where T : CharacterComponent
    {
        public virtual Task AddComponent(T component)
        {
            return Task.CompletedTask;
        }

        public virtual Task CommitChanges(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public abstract Task<T> GetCharacterComponent(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null);


        public abstract Task<IEnumerable<T>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null);

        public virtual Task RemoveComponent(T component)
        {
            return Task.CompletedTask;
        }
    }
}
