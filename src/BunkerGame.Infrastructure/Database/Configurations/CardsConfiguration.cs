using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Database.Configurations
{
    internal class CardsConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards").Property(c => c.Description).HasMaxLength(1000);
            builder.OwnsOne(c => c.CardMethod).Property(c=>c.MethodDirection).HasConversion<string>().HasMaxLength(50);
            builder.OwnsOne(c => c.CardMethod).Property(c => c.MethodType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
