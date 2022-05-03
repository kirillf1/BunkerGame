using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using BunkerGame.Domain.Players;
using BunkerGame.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Infrastructure.Database
{
    public class BunkerGameDbContext : DbContext
    {
        public BunkerGameDbContext(DbContextOptions<BunkerGameDbContext> options) : base(options)
        {

        }

        public DbSet<AdditionalInformation> AdditionalInformations => Set<AdditionalInformation>();
        public DbSet<BunkerWall> BunkerWalls => Set<BunkerWall>();
        public DbSet<BunkerEnviroment> BunkerEnviroments => Set<BunkerEnviroment>();
        public DbSet<BunkerObject> BunkerObjects => Set<BunkerObject>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<CharacterItem> CharacterItems => Set<CharacterItem>();
        public DbSet<Health> Healths => Set<Health>();
        public DbSet<Hobby> Hobbies => Set<Hobby>();
        public DbSet<ItemBunker> ItemBunkers => Set<ItemBunker>();
        public DbSet<Phobia> Phobias => Set<Phobia>();
        public DbSet<Profession> Professions => Set<Profession>();
        public DbSet<GameSession> GameSessions => Set<GameSession>();
        public DbSet<Bunker> Bunkers => Set<Bunker>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Trait> Traits => Set<Trait>();
        public DbSet<Catastrophe> Catastrophes => Set<Catastrophe>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameResult> GameResults => Set<GameResult>();
        public DbSet<ExternalSurrounding> ExternalSurroundings => Set<ExternalSurrounding>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            #region configure bunker components
            modelBuilder.Entity<ItemBunker>().Property(c => c.ItemBunkerType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<BunkerEnviroment>().Property(c => c.EnviromentBehavior).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<BunkerEnviroment>().Property(c => c.EnviromentType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<BunkerObject>().Property(c => c.BunkerObjectType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<BunkerWall>().Property(c => c.BunkerState).HasConversion<string>().HasMaxLength(50);

            #endregion
            #region configure character components
            modelBuilder.ApplyConfiguration(new ProfessionsConfiguration());
            modelBuilder.ApplyConfiguration(new CardsConfiguration());
            modelBuilder.Entity<ExternalSurrounding>().Property(e=>e.SurroundingType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<Phobia>().ToTable("Phobias").Property(p => p.PhobiaDebuffType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<AdditionalInformation>().ToTable("AdditionalInformations").Property(c => c.AddInfType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<Health>().ToTable("Healths").Property(c => c.HealthType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<Hobby>().ToTable("Hobbies").Property(c => c.HobbyType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<CharacterItem>().ToTable("CharacterItems").Property(c => c.CharacterItemType).HasConversion<string>().HasMaxLength(50);
            modelBuilder.Entity<Trait>().ToTable("Traits").Property(c => c.TraitType).HasConversion<string>().HasMaxLength(50);
          
          
            #endregion
          
            modelBuilder.ApplyConfiguration(new BunkersConfiguration());
            modelBuilder.ApplyConfiguration(new CatastropheConfiguration());
            modelBuilder.ApplyConfiguration(new CharactersConfiguration());
            modelBuilder.ApplyConfiguration(new GameSessionConfiguration());
            modelBuilder.ApplyConfiguration(new PlayersConfiguration());
            modelBuilder.ApplyConfiguration(new GameResultConfiguration());
        }
    }
}
