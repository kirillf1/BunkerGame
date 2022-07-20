using BunkerGame.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGame.VkApi.Infrastructure.Database.GameDbContext.DbConfiguration
{
    internal class PlayersConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(c => c.Id).HasConversion(c => c.Value, c => new Domain.Shared.PlayerId(c));
            builder.Property(c => c.FirstName).HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);
        }
    }
}
