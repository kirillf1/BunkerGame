using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.BunkerComponents
{
    public class BunkerSizeRepository : IBunkerComponentRepository<BunkerSize>
    {
        private readonly Random Random;
        public BunkerSizeRepository()
        {
            Random = new Random();
        }
        // Does nothing as there is no table in the database yet
        public Task AddComponent(BunkerSize component)
        {
            return Task.CompletedTask;
        }

        public Task CommitChanges(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<BunkerSize> GetBunkerComponent(bool needShuffle = true, Expression<Func<BunkerSize, bool>>? predicate = null)
        {
            return Task.Run(() =>
              {
                  var bunkerSizeValue = Random.Next(200, 600);
                  return new BunkerSize(bunkerSizeValue);
              });
        }

        public async Task<IEnumerable<BunkerSize>> GetBunkerComponents(int count, bool needShuffle = true, Expression<Func<BunkerSize, bool>>? predicate = null)
        {
            var bunkerSizeList = new List<BunkerSize>(count);
            for (int i = 0; i < count; i++)
            {
                bunkerSizeList.Add(await GetBunkerComponent(needShuffle, predicate));
            }
            return bunkerSizeList;
        }

        public Task RemoveComponent(BunkerSize component)
        {
            return Task.CompletedTask;
        }
    }
}
