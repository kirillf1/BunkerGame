using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.BunkerComponents
{
    public class SuppliesRepository : IBunkerComponentRepository<Supplies>
    {
        private readonly Random Random;
        public SuppliesRepository()
        {
            Random = new Random();
        }
        // Does nothing as there is no table in the database yet
        public Task AddComponent(Supplies component)
        {
            return Task.CompletedTask;
        }

        public Task CommitChanges(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<Supplies> GetBunkerComponent(bool needShuffle = true, Expression<Func<Supplies, bool>>? predicate = null)
        {
            return await Task.Run(() => new Supplies(Random.Next(5, 10)));
         }
        public async Task<IEnumerable<Supplies>> GetBunkerComponents(int count, bool needShuffle = true, Expression<Func<Supplies, bool>>? predicate = null)
        {
            var supplies = new List<Supplies>();
            for (int i = 0; i < count; i++)
            {
                supplies.Add(await GetBunkerComponent(needShuffle, predicate));
            }
            return supplies;
        }

        public Task RemoveComponent(Supplies component)
        {
            return Task.CompletedTask;
        }
    }
}
