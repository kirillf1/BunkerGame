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
    internal class CharacterItemConfiguration : IEntityTypeConfiguration<CharacterItem>
    {

        public void Configure(EntityTypeBuilder<CharacterItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("CharacterItems").Property(c => c.CharacterItemType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
