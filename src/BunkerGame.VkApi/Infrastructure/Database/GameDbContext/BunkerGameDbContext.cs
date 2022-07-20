using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.VkApi.Infrastructure.Database.GameDbContext.DbConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.VkApi.Infrastructure.Database.GameDbContext
{
    public class BunkerGameDbContext : DbContext
    {
        public BunkerGameDbContext(DbContextOptions<BunkerGameDbContext> options) : base(options)
        {

        }
        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameSession> GameSessions => Set<GameSession>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<GameResult> GameResults => Set<GameResult>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.ApplyConfiguration(new PlayersConfiguration());
            modelBuilder.ApplyConfiguration(new GameResultsConfiguration());
            modelBuilder.ApplyConfiguration(new CharactersConfiguration());
            modelBuilder.ApplyConfiguration(new GameSessionConfiguration());
        }
    }
}
