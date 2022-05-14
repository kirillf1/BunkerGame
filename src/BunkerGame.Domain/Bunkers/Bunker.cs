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
        //public int BunkerWallId { get; private set; }
        public BunkerWall BunkerWall { get; private set; }
        public List<ItemBunker> ItemBunkers { get; private set; } = new List<ItemBunker>();
        public List<BunkerObject> BunkerObjects { get; private set; } = new List<BunkerObject>();
        //public int BunkerEnviromentId { get; private set; }
        public BunkerEnviroment BunkerEnviroment { get; private set; }
        //public int GameSessionId { get; private set; }

        public void UpdateBunkerComponent<T>(T bunkerComponent) where T : BunkerComponent
        {
            UpdateBunkerComponent(bunkerComponent);
        }
        public void UpdateBunkerComponent(object bunkerComponent)
        {
            if (bunkerComponent is null)
            {
                throw new ArgumentNullException(nameof(bunkerComponent));
            }
            else if (bunkerComponent is BunkerWall bunkerWall)
            {
                BunkerWall = bunkerWall ?? throw new ArgumentNullException(nameof(bunkerWall));
            }
            else if (bunkerComponent is BunkerEnviroment enviroment)
            {
                BunkerEnviroment = enviroment ?? throw new ArgumentNullException(nameof(enviroment));
            }
            else if (bunkerComponent is ItemBunker itemBunker)
            {
                if (ItemBunkers.Count > 0)
                {
                    ItemBunkers.Remove(ItemBunkers.First());
                }
                ItemBunkers.Add(itemBunker);
                return;
            }
            else if (bunkerComponent is BunkerObject bunkerObject)
            {
                if (ItemBunkers.Count > 0)
                {
                    BunkerObjects.Remove(BunkerObjects.First());
                }
                BunkerObjects.Add(bunkerObject);
                return;
            }
        }
        public void RegisterBunkerInGame(long gameId)
        {
            GameSessionId = gameId;
        }
        public void UpdateSupplies(Supplies supplies)
        {
            Supplies = supplies ?? throw new ArgumentNullException(nameof(supplies));
        }
        public void UpdateBunkerSize(BunkerSize bunkerSize)
        {
            BunkerSize = bunkerSize ?? throw new ArgumentNullException(nameof(bunkerSize));
        }

    }
}
