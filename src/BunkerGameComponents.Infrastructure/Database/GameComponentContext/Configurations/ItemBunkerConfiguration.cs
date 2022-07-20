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
    internal class ItemBunkerConfiguration : IEntityTypeConfiguration<ItemBunker>
    {
        public void Configure(EntityTypeBuilder<ItemBunker> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("ItemBunkers");
            builder.Property(c => c.ItemBunkerType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
