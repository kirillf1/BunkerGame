using BunkerGameComponents.Domain.Catastrophes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class CatastropheConfiguration : IEntityTypeConfiguration<GameCatastrophe>
    {
        public void Configure(EntityTypeBuilder<GameCatastrophe> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CatastropheType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
