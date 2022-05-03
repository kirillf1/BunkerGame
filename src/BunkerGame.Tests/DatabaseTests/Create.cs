using BunkerGame.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BunkerGame.Tests.DatabaseTests
{
    public class Create
    {
        [Fact]
        public void CreateDatabaseInMemory()
        {
            using var db = DbCreator.CreateInMemoryContext();
            db.Database.EnsureDeleted();
        }
        [Fact]
        public void CreateLocalDatabase()
        {
            using var db = DbCreator.CreateLocalContext();
            db.Database.EnsureCreated();
        }
    }
}
