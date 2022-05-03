using BunkerGame.Domain.Bunkers.BunkerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.DatabaseTests
{
    public class BunkerTests
    {
        [Fact]
        public async void AddBunker_AndCheckComponents_ShouldSaveInDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            var bunker = BunkerCreator.CreateBunker();
            
            context.Add(bunker);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            var bunkerFromDb = context.Bunkers.Include(c => c.BunkerEnviroment).FirstOrDefault(c=>c.Id == bunker.Id);
            
            Assert.NotNull(bunkerFromDb);
            Assert.True(bunker.BunkerSize == bunkerFromDb!.BunkerSize);
            Assert.NotNull(bunkerFromDb!.BunkerEnviroment);
            Assert.Equal(bunker.BunkerEnviroment.Description, bunkerFromDb.BunkerEnviroment.Description);
        }
        [Fact]
        public async void AddBunker_AndChangeComponent_ShouldChangeInDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            var bunker = BunkerCreator.CreateBunker();
            
            context.Add(bunker);
            await context.SaveChangesAsync();
            var bunkerWallBeforeChange = bunker.BunkerWall;
            bunker.UpdateBunkerComponent(new BunkerWall(10, "testWall"));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            var bunkerFromDb = context.Bunkers.Include(b=>b.BunkerWall).FirstOrDefault(b => b.Id == bunker.Id);

            Assert.NotNull(bunkerFromDb);
            Assert.Null(bunkerFromDb!.BunkerEnviroment);
            Assert.False(bunkerWallBeforeChange.Id == bunkerFromDb.BunkerWall.Id);
        }
    }
}
