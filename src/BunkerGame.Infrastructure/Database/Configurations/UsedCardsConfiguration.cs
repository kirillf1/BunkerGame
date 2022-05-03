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
    public class UsedCardsConfiguration : IEntityTypeConfiguration<UsedCard>
    {
        public void Configure(EntityTypeBuilder<UsedCard> builder)
        {
            builder.HasNoKey();
        }
    }
}
