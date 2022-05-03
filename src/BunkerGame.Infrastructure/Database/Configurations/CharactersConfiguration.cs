using BunkerGame.Domain.Characters;
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
    internal class CharactersConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.OwnsOne(c => c.Childbearing);
            builder.OwnsOne(c => c.Sex);
            builder.OwnsOne(c => c.Size);
            builder.OwnsOne(c => c.Age);
            builder.HasOne(c => c.AdditionalInformation).WithMany();
            builder.HasMany(c => c.CharacterItems).WithMany("Characters");
            builder.HasMany(c => c.Cards).WithMany("Characters");
            builder.HasOne(c => c.Health).WithMany();
            builder.HasOne(c => c.Hobby).WithMany();
            builder.HasOne(c => c.Phobia).WithMany();
            builder.HasOne(c => c.Profession).WithMany();
            builder.HasOne(c => c.Trait).WithMany();
            builder.OwnsMany(c => c.UsedCards);

            builder.Navigation(c => c.AdditionalInformation).AutoInclude();
            builder.Navigation(c => c.CharacterItems).AutoInclude();
            builder.Navigation(c => c.Cards).AutoInclude();
            builder.Navigation(c => c.Health).AutoInclude();
            builder.Navigation(c => c.Hobby).AutoInclude();
            builder.Navigation(c => c.Phobia).AutoInclude();
            builder.Navigation(c => c.Profession).AutoInclude();
            builder.Navigation(c => c.Trait).AutoInclude();
            builder.Navigation("UsedCards").AutoInclude();
        }
    }
}
