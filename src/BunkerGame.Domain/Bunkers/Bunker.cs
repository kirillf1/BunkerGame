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
        public Bunker(double bunkerSize, int suppliesYear, BunkerWall bunkerWall, IEnumerable<ItemBunker> itemBunkers,
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

            BunkerSize = bunkerSize;
            SuppliesYear = suppliesYear;
            BunkerWall = bunkerWall ?? throw new ArgumentNullException(nameof(bunkerWall));
            ItemBunkers = new(itemBunkers);
            BunkerObjects = new(bunkerObjects);
            BunkerEnviroment = bunkerEnviroment ?? throw new ArgumentNullException(nameof(bunkerEnviroment));
        }
        public int Id { get; private set; }
        public double BunkerSize { get; private set; }
        public long? GameSessionId { get; private set; }
        public int SuppliesYear { get; private set; }
        //public int BunkerWallId { get; private set; }
        public BunkerWall BunkerWall { get; private set; }
        public List<ItemBunker> ItemBunkers { get; private set; } = new List<ItemBunker>();
        public List<BunkerObject> BunkerObjects { get; private set; } = new List<BunkerObject>();
        //public int BunkerEnviromentId { get; private set; }
        public BunkerEnviroment BunkerEnviroment { get; private set; }
        //public int GameSessionId { get; private set; }

        public void UpdateBunkerComponent<T>(T bunkerComponent) where T : BunkerComponentEntity
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
                BunkerWall = bunkerWall;

            }
            else if (bunkerComponent is BunkerEnviroment enviroment)
            {
                BunkerEnviroment = enviroment;

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
        public void UpdateSuppliesYear(int years)
        {
            SuppliesYear = years;
        }
        public void UpdateBunkerSize(double size)
        {
            BunkerSize = Math.Round(size, 2);
        }

    }
}
