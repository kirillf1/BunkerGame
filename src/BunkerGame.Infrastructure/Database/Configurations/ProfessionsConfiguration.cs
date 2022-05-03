using BunkerGame.Domain.Characters.CharacterComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Database.Configurations
{
    internal class ProfessionsConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            builder.ToTable("Professions").Property(c => c.ProfessionSkill).HasConversion<string>().HasMaxLength(50);
            builder.Property(c => c.ProfessionType).HasConversion<string>();
            builder.HasOne(c => c.CharacterItem).WithMany();
            builder.HasOne(c => c.Card).WithMany();
            builder.Navigation(c => c.Card).AutoInclude();
            builder.Navigation(c => c.CharacterItem).AutoInclude();
            
        }
    }
}
