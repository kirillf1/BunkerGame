using BunkerGame.Domain.GameSessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BunkerGame.Domain.Shared;

namespace BunkerGame.VkApi.Infrastructure.Database.GameDbContext.DbConfiguration
{
    internal class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.Property<long>("_Id");
            builder.HasKey("_Id");
            builder.Property(c => c.Id).HasConversion(c => c.Value, c => new GameSessionId(c));
            builder.OwnsOne(c => c.Catastrophe);
            builder.OwnsOne(c => c.Bunker, c =>
            {
                c.OwnsMany(c => c.Items).ToTable("BunkerItems");
                c.OwnsMany(c => c.Buildings);
                c.OwnsOne(c => c.Supplies);
                c.OwnsOne(c => c.Condition);
                c.OwnsOne(c => c.Size);
                c.OwnsOne(c => c.Enviroment);
            });
            builder.OwnsOne(x => x.FreePlaceSize);
            builder.OwnsMany(c => c.Characters, c =>
            {
                c.Property(c => c.Id).HasConversion(c => c.Value, c => new CharacterId(c));
                c.Property(c => c.PlayerId).HasConversion(c => c.Value, c => new PlayerId(c));
            });
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.GameState).HasConversion<string>().HasMaxLength(50);
            builder.Property(c => c.Difficulty).HasConversion<string>().HasMaxLength(50);
            builder.OwnsMany(c => c.ExternalSurroundings);
            builder.Property(c => c.CreatorId).HasConversion(c => c.Value, c => new PlayerId(c));
        }
    }
}
