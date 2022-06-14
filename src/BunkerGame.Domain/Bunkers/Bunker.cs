using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers
{
    public class Bunker
    {
#pragma warning disable CS8618 
        private Bunker()
#pragma warning restore CS8618
        {

        }
        public Bunker(BunkerSize bunkerSize, Supplies supplies, BunkerWall bunkerWall, IEnumerable<ItemBunker> itemBunkers,
            IEnumerable<BunkerObject> bunkerObjects, BunkerEnviroment bunkerEnviroment)
        {
            if (itemBunkers is null)
            {
                throw new ArgumentNullException(nameof(itemBunkers));
            }

            if (bunkerObjects is null)
            {
                throw new ArgumentNullException(nameof(bunkerObjects));
            }

            BunkerSize = bunkerSize ?? throw new ArgumentNullException(nameof(bunkerSize));
            Supplies = supplies ?? throw new ArgumentNullException(nameof(supplies));
            BunkerWall = bunkerWall ?? throw new ArgumentNullException(nameof(bunkerWall));
            ItemBunkers = new(itemBunkers);
            BunkerObjects = new(bunkerObjects);
            BunkerEnviroment = bunkerEnviroment ?? throw new ArgumentNullException(nameof(bunkerEnviroment));
        }
        public int Id { get; private set; }
        public BunkerSize BunkerSize { get; private set; }
        public long? GameSessionId { get; private set; }
        public Supplies Supplies { get; private set; }
        public BunkerWall BunkerWall { get; private set; }
        public List<ItemBunker> ItemBunkers { get; private set; } = new List<ItemBunker>();
        public List<BunkerObject> BunkerObjects { get; private set; } = new List<BunkerObject>();
        public BunkerEnviroment BunkerEnviroment { get; private set; }
        public void UpdateBunkerSize(BunkerSize bunkerSize)
        {
            BunkerSize = bunkerSize ?? throw new ArgumentNullException(nameof(bunkerSize));
        }
        public void UpdateBunkerObjects(IEnumerable<BunkerObject> bunkerObjects)
        {
            UpdateComponentCollection(BunkerObjects, bunkerObjects);
        }
        public void UpdateItemsBunker(IEnumerable<ItemBunker> items)
        {
            UpdateComponentCollection(ItemBunkers, items);
        }
        public void UpdateBunkerWall(BunkerWall bunkerWall)
        {
            BunkerWall = bunkerWall ?? throw new ArgumentNullException(nameof(bunkerWall));
        }
        public void UpdateBunkerEnviroment(BunkerEnviroment bunkerEnviroment)
        {
            BunkerEnviroment = bunkerEnviroment ?? throw new ArgumentNullException(nameof(bunkerEnviroment));
        }
        public void UpdateSupplies(Supplies supplies)
        {
            Supplies = supplies ?? throw new ArgumentNullException(nameof(supplies));
        }
        public void RegisterBunkerInGame(long gameId)
        {
            GameSessionId = gameId;
        }
        private static void UpdateComponentCollection<T>(List<T> oldComponents, IEnumerable<T> newComponents) where T : BunkerComponent
        {
            var newComponentsCount = newComponents.Count();
            if (oldComponents.Count == newComponentsCount || newComponentsCount > oldComponents.Count)
            {
                oldComponents.Clear();
                oldComponents.AddRange(newComponents);
            }
            else if (oldComponents.Count > newComponentsCount)
            {
                oldComponents.RemoveRange(0, newComponentsCount);
                oldComponents.AddRange(newComponents);
            }
        }


    }
}
