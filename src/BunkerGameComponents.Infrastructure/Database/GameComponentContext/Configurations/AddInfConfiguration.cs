using BunkerGameComponents.Domain.CharacterComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class AddInfConfiguration : IEntityTypeConfiguration<CharacterAdditionalInformation>
    {


        public void Configure(EntityTypeBuilder<CharacterAdditionalInformation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("AdditionalInformations").Property(c => c.AddInfType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
