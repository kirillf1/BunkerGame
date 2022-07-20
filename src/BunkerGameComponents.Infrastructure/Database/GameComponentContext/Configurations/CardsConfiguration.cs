using BunkerGameComponents.Domain.CharacterComponents.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class CardsConfiguration : IEntityTypeConfiguration<CharacterCard>
    {
        public void Configure(EntityTypeBuilder<CharacterCard> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Cards").Property(c => c.Description).HasMaxLength(1000);
            builder.OwnsOne(c => c.CardMethod).Property(c => c.MethodDirection).HasConversion<string>().HasMaxLength(50);
            builder.OwnsOne(c => c.CardMethod).Property(c => c.MethodType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
