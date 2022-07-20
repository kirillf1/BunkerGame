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
    public class HealthConfiguration : IEntityTypeConfiguration<CharacterHealth>
    {
        public void Configure(EntityTypeBuilder<CharacterHealth> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Healths").Property(c => c.HealthType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
