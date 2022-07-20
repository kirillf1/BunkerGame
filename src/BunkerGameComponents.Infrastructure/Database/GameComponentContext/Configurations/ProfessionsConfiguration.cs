using BunkerGameComponents.Domain.CharacterComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations;

internal class ProfessionsConfiguration : IEntityTypeConfiguration<CharacterProfession>
{
    public void Configure(EntityTypeBuilder<CharacterProfession> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Professions").Property(c => c.ProfessionSkill).HasConversion<string>().HasMaxLength(50);
        builder.Property(c => c.ProfessionType).HasConversion<string>();
        builder.HasOne(c => c.CharacterItem).WithMany();
        builder.HasOne(c => c.Card).WithMany();
        builder.Navigation(c => c.Card).AutoInclude();
        builder.Navigation(c => c.CharacterItem).AutoInclude();

    }
}
