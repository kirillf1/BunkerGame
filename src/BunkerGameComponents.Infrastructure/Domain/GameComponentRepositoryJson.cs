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
    public class GameComponentRepositoryJson<T> : IGameComponentRepository<T> where T : class, IGameComponent
    {
        private readonly GameComponentJsonContext gameComponentJsonContext;
        private List<T> Components => entities ??= gameComponentJsonContext.Set<T>();
        private List<T>? entities { get; set; }
        public GameComponentRepositoryJson(GameComponentJsonContext gameComponentJsonContext)
        {
            this.gameComponentJsonContext = gameComponentJsonContext;
        }
        public Task AddComponent(T component)
        {
            if (Components.Any(c => c.Id == component.Id))
                return Task.CompletedTask;
            Components.Add(component);
            return Task.CompletedTask;
        }

        public Task<T> GetComponent(ComponentId id)
        {
            var component = Components.Find(c => c.Id == id);
            if (component == null)
                throw new ArgumentNullException(nameof(T));
            return Task.FromResult(component);
        }

        public Task<T> GetComponent(bool needShuffle, Expression<Func<T, bool>>? predicate = null)
        {
            var query = Components.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return Task.FromResult(query.First());
        }

        public Task<IEnumerable<T>> GetComponents(int skipCount, int count, bool needShuffle, Expression<Func<T, bool>>? predicate = null)
        {
            var query = Components.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (needShuffle)
                query = query.OrderBy(c => Guid.NewGuid());
            return Task.FromResult(query.Skip(skipCount).Take(count).AsEnumerable());
        }

        public Task RemoveComponent(T component)
        {
            Components.Remove(component);
            return Task.CompletedTask;
        }
    }
}
