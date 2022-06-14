using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers
{
    public interface IBunkerComponent<T> where T : BunkerComponent
    {
        T Component { get; set; }
    }
    public interface IBunkerComponentCollection<T> where T: BunkerComponent
    {
        IReadOnlyCollection<T> Components { get; set; }
    }
    public class BunkerProxy : IBunkerComponent<BunkerWall>, IBunkerComponent<BunkerEnviroment>, IBunkerComponent<Supplies>,
        IBunkerComponent<BunkerSize>, IBunkerComponentCollection<ItemBunker>,IBunkerComponentCollection<BunkerObject>
    {
        public BunkerProxy(Bunker bunker)
        {
            Bunker = bunker;
        }
        public Bunker Bunker { get; }
        BunkerWall IBunkerComponent<BunkerWall>.Component { get => Bunker.BunkerWall; set => Bunker.UpdateBunkerWall(value); }
        BunkerEnviroment IBunkerComponent<BunkerEnviroment>.Component { get => Bunker.BunkerEnviroment;
            set => Bunker.UpdateBunkerEnviroment(value); }
        BunkerSize IBunkerComponent<BunkerSize>.Component { get => Bunker.BunkerSize; set => Bunker.UpdateBunkerSize(value); }
        Supplies IBunkerComponent<Supplies>.Component { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IReadOnlyCollection<ItemBunker> IBunkerComponentCollection<ItemBunker>.Components { get => Bunker.ItemBunkers;
            set => Bunker.UpdateItemsBunker(value); }
        IReadOnlyCollection<BunkerObject> IBunkerComponentCollection<BunkerObject>.Components { get => Bunker.BunkerObjects;
            set => Bunker.UpdateBunkerObjects(value); }
        public bool IsBunkerComponent<T>() where T : BunkerComponent
        {
            return this is IBunkerComponent<T>;
        }
        public bool IsContainsBunkerComponentCollection<T>() where T : BunkerComponent
        {
            return this is IBunkerComponentCollection<T>;
        }
        public IBunkerComponent<T> GetBunkerComponent<T>() where T : BunkerComponent
        {
            return this as IBunkerComponent<T> ??
                throw new NotImplementedException($"Bunker does not contain a property {typeof(T).Name}");

        }
        public IBunkerComponentCollection<T> GetBunkerComponentCollection<T>() where T : BunkerComponent
        {
            return this as IBunkerComponentCollection<T> ??
                throw new NotImplementedException($"Bunker does not contain a property {typeof(T).Name}");
        }
    }
}
