using BunkerGameComponents.Domain.BunkerComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext.Configurations
{
    internal class BunkerObjectConfiguration : IEntityTypeConfiguration<BunkerObject>
    {
        public void Configure(EntityTypeBuilder<BunkerObject> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("BunkerObjects").Property(c => c.BunkerObjectType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
