
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.BunkerFactories
{
    public class BunkerFactoryBase : IBunkerFactory
    {
        private readonly IBunkerComponentRepositoryLocator bunkerComponentRepositoryLocator;
        private Random random;
        public BunkerFactoryBase(IBunkerComponentRepositoryLocator bunkerComponentRepositoryLocator)
        {
            this.bunkerComponentRepositoryLocator = bunkerComponentRepositoryLocator;
            random = new Random();
        }

        public virtual async Task<Bunker> CreateBunker(BunkerCreateOptions options)
        {
            var bunkerSize = random.Next(150, 600);
            int bunkerItemscount = bunkerSize switch
            {
                int size when size > 500 => 3,
                int size when size < 200 => 1,
                _ => 2
            };
            var bunkerItems = await bunkerComponentRepositoryLocator.GetBunkerComponents<ItemBunker>(bunkerItemscount);
            var bunkerObjects = await bunkerComponentRepositoryLocator.GetBunkerComponents<BunkerObject>(bunkerItemscount);
            var bunkerEnviroment = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerEnviroment>();
            var bunkerWall = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerWall>();

            return new Bunker(bunkerSize, random.Next(5,10), bunkerWall, bunkerItems, bunkerObjects, bunkerEnviroment);
        }
    }
}
