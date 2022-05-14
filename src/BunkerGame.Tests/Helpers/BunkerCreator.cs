using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.Helpers
{
    public static class BunkerCreator
    {
        public static Bunker CreateBunker()
        {
            var random = new Random();
            return new Bunker(new BunkerSize( random.Next(100, 400)),new Supplies( random.Next(0, 10)), 
                new BunkerWall(random.Next(0, 10), $"BunkerWall:{random.Next()}"),
                new List<ItemBunker>() { new ItemBunker(random.Next(0, 10), $"Itembunker:{random.Next()}"), new ItemBunker(random.Next(0, 10), $"Itembunker:{random.Next()}") },
                new List<BunkerObject> { new BunkerObject(random.Next(0, 10),$"BunkerObject:{random.Next()}"), new BunkerObject(random.Next(0, 10), $"BunkerObject:{random.Next()}") },
                new BunkerEnviroment(random.Next(0,10),$"EnvBunker:{random.Next()}"));

        }
    }
}
