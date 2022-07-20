using System.Linq.Expressions;

namespace BunkerGameComponents.Domain
{
    public interface IGameComponentRepository<T> where T : IGameComponent
    {
        public Task<T> GetComponent(ComponentId id);
        public Task<T> GetComponent(bool needShuffle, Expression<Func<T, bool>>? predicate = null);
        public Task<IEnumerable<T>> GetComponents(int skipCount, int count, bool needShuffle, Expression<Func<T, bool>>? predicate = null);
        public Task AddComponent(T component);
        public Task RemoveComponent(T component);
    }
}
