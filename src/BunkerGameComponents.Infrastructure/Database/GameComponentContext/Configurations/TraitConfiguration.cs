using BunkerGameComponents.Domain.CharacterComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class TraitConfiguration : IEntityTypeConfiguration<CharacterTrait>
    {
        public void Configure(EntityTypeBuilder<CharacterTrait> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Traits").Property(c => c.TraitType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
