using BunkerGameComponents.Domain.BunkerComponents;
using BunkerGameComponents.Domain.Catastrophes;
using BunkerGameComponents.Domain.CharacterComponents;
using BunkerGameComponents.Domain.CharacterComponents.Cards;
using BunkerGameComponents.Domain.ExternalSurroundings;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext
{
    public class GameComponentDbContext : DbContext
    {
        public GameComponentDbContext(DbContextOptions<GameComponentDbContext> options) : base(options)
        {
        }
        public DbSet<CharacterAdditionalInformation> AdditionalInformations => Set<CharacterAdditionalInformation>();
        public DbSet<BunkerWall> BunkerWalls => Set<BunkerWall>();
        public DbSet<BunkerEnviroment> BunkerEnviroments => Set<BunkerEnviroment>();
        public DbSet<BunkerObject> BunkerObjects => Set<BunkerObject>();
        public DbSet<CharacterCard> Cards => Set<CharacterCard>();
        public DbSet<CharacterItem> CharacterItems => Set<CharacterItem>();
        public DbSet<CharacterHealth> Healths => Set<CharacterHealth>();
        public DbSet<CharacterHobby> Hobbies => Set<CharacterHobby>();
        public DbSet<ItemBunker> ItemBunkers => Set<ItemBunker>();
        public DbSet<CharacterPhobia> Phobias => Set<CharacterPhobia>();
        public DbSet<CharacterProfession> Professions => Set<CharacterProfession>();
        public DbSet<CharacterTrait> Traits => Set<CharacterTrait>();
        public DbSet<GameCatastrophe> Catastrophes => Set<GameCatastrophe>();
        public DbSet<GameExternalSurrounding> ExternalSurroundings => Set<GameExternalSurrounding>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new AddInfConfiguration());
            modelBuilder.ApplyConfiguration(new BunkerEnviromentConfiguration());
            modelBuilder.ApplyConfiguration(new BunkerObjectConfiguration());
            modelBuilder.ApplyConfiguration(new BunkerWallConfiguration());
            modelBuilder.ApplyConfiguration(new CardsConfiguration());
            modelBuilder.ApplyConfiguration(new CatastropheConfiguration());
            modelBuilder.ApplyConfiguration(new CharacterItemConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalSurroundingConfiguration());
            modelBuilder.ApplyConfiguration(new HealthConfiguration());
            modelBuilder.ApplyConfiguration(new ItemBunkerConfiguration());
            modelBuilder.ApplyConfiguration(new PhobiaConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessionsConfiguration());
            modelBuilder.ApplyConfiguration(new TraitConfiguration());

        }
    }
}
