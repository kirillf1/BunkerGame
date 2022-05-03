using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class CharacterComponentRepLocatorEf : ICharacterComponentRepLocator
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public CharacterComponentRepLocatorEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;

        }
        public async Task AddComponent<T>(T component) where T : CharacterComponent
        {
             await GetRepository<T>().RemoveComponent(component);
        }

        public async Task CommitChanges(CancellationToken cancellationToken)
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<T> GetCharacterComponent<T>(bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : CharacterComponent
        {
            return await GetRepository<T>().GetCharacterComponent(needShuffle, predicate);
        }

        public async Task<IEnumerable<T>> GetCharacterComponents<T>(int count, bool needShuffle = true, Expression<Func<T, bool>>? predicate = null) where T : CharacterComponent
        {
            return await GetRepository<T>().GetCharacterComponents(count,needShuffle, predicate);
        }

        public async Task RemoveComponent<T>(T component) where T : CharacterComponent
        {
            await GetRepository<T>().RemoveComponent(component);
        }
        private ICharacterComponentRepository<T> GetRepository<T>() where T : CharacterComponent
        {
            ICharacterComponentRepository<T> repository;
            if (typeof(T) == typeof(Sex))
            {
                repository = (ICharacterComponentRepository<T>)new SexMockRepository();
            }
            else if(typeof(T) == typeof(Size))
            {
                repository = (ICharacterComponentRepository<T>)new SizeMockRepository();
            }
            else if (typeof(T) == typeof(Age))
            {
                repository = (ICharacterComponentRepository<T>)new AgeMockRepository();
            }
            else if (typeof(T) == typeof(Childbearing))
            {
                repository = (ICharacterComponentRepository<T>)new ChildbearingMockRepository();
            }
            else
            {
                repository = new CharacterComponentRepositoryEFBase<T>(bunkerGameDbContext);
            }
            return repository;
        }
    }
}
