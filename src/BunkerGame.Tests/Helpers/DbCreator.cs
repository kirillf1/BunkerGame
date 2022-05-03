using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.Helpers
{
    public static class DbCreator
    {

        public static BunkerGameDbContext CreateLocalContext()
        {
            var random = new Random();
            string databaseName = "database_" + random.Next(0, 100000);
            var options = new DbContextOptionsBuilder<BunkerGameDbContext>().UseNpgsql($"User ID =tester;Password=12345678;Server=localhost;Port=5432;Database={databaseName};Integrated Security=true;Pooling=true").Options;
            var context = new BunkerGameDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
        public static BunkerGameDbContext CreateInMemoryContext()
        {
            var name = $"{DateTime.Now.Millisecond}";
            var context = new BunkerGameDbContext(new DbContextOptionsBuilder<BunkerGameDbContext>().UseInMemoryDatabase(name, new InMemoryDatabaseRoot()).Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
