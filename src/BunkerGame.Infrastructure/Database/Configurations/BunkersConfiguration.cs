using BunkerGame.Domain.Bunkers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Database.Configurations
{
    internal class BunkersConfiguration : IEntityTypeConfiguration<Bunker>
    {
        public void Configure(EntityTypeBuilder<Bunker> builder)
        {
            builder.ToTable("Bunkers").HasOne(c => c.BunkerEnviroment).WithMany();
            builder.HasOne(c => c.BunkerWall).WithMany();
            builder.HasMany(c => c.BunkerObjects).WithMany("Bunkers");
            builder.HasMany(c => c.ItemBunkers).WithMany("Bunkers");
            builder.OwnsOne(c => c.Supplies);
            builder.OwnsOne(b => b.BunkerSize);
            builder.Navigation(c => c.BunkerEnviroment).AutoInclude();
            builder.Navigation(c => c.BunkerObjects).AutoInclude();
            builder.Navigation(c => c.BunkerWall).AutoInclude();
            builder.Navigation(c => c.ItemBunkers).AutoInclude();
        }
    }
}
