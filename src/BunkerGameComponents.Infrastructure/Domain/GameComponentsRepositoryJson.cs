using BunkerGameComponents.Domain;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Infrastructure.Domain
{
    public class GameComponentsRepositoryJson : IGameComponentsRepository
    {
        private readonly GameComponentJsonContext context;

        public GameComponentsRepositoryJson(GameComponentJsonContext context)
        {
            this.context = context;
        }
        public Task AddComponent<T>(T component) where T : class, IGameComponent
        {
            return GetRepository<T>().AddComponent(component);
        }

        public Task<T> GetComponent<T>(ComponentId id) where T : class, IGameComponent
        {
            return GetRepository<T>().GetComponent(id);
        }

        public Task<T> GetComponent<T>(bool needShuffle, Expression<Func<T, bool>>? predicate = null) where T : class, IGameComponent
        {
            return GetRepository<T>().GetComponent(needShuffle, predicate);
        }

        public Task<IEnumerable<T>> GetComponents<T>(int skipCount, int count, bool needShuffle, Expression<Func<T, bool>>? predicate = null) where T : class, IGameComponent
        {
            return GetRepository<T>().GetComponents(skipCount, count, needShuffle, predicate);
        }

        public Task RemoveComponent<T>(T component) where T : class, IGameComponent
        {
            return GetRepository<T>().RemoveComponent(component);
        }
        private GameComponentRepositoryJson<T> GetRepository<T>() where T : class, IGameComponent
        {
            return new GameComponentRepositoryJson<T>(context);
        }

    }
}
