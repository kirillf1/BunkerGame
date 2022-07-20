using BunkerGameComponents.Domain.CharacterComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class PhobiaConfiguration : IEntityTypeConfiguration<CharacterPhobia>
    {
        public void Configure(EntityTypeBuilder<CharacterPhobia> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Phobias").Property(p => p.PhobiaDebuffType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
