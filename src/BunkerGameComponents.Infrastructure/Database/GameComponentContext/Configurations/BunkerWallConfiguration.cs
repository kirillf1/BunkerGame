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
    internal class BunkerWallConfiguration : IEntityTypeConfiguration<BunkerWall>
    {
        public void Configure(EntityTypeBuilder<BunkerWall> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("BunkerWalls").Property(c => c.BunkerState).HasConversion<string>().HasMaxLength(50);
        }
    }
}
