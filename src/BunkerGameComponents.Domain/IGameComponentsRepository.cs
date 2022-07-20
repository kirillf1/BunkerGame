using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Domain
{
    public interface IGameComponentsRepository
    {
        public Task<T> GetComponent<T>(ComponentId id) where T : class, IGameComponent;
        public Task<T> GetComponent<T>(bool needShuffle, Expression<Func<T, bool>>? predicate = null) where T : class, IGameComponent;
        public Task<IEnumerable<T>> GetComponents<T>(int skipCount, int count, bool needShuffle, Expression<Func<T, bool>>? predicate = null) where T : class, IGameComponent;
        public Task AddComponent<T>(T component) where T : class, IGameComponent;
        public Task RemoveComponent<T>(T component) where T : class, IGameComponent;
    }
}
