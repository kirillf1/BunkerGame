using BunkerGameComponents.Domain.ExternalSurroundings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class ExternalSurroundingConfiguration : IEntityTypeConfiguration<GameExternalSurrounding>
    {

        public void Configure(EntityTypeBuilder<GameExternalSurrounding> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("ExternalSurroundings").Property(e => e.SurroundingType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
