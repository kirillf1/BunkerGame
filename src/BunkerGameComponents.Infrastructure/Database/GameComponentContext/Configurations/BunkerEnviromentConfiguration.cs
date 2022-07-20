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
    internal class BunkerEnviromentConfiguration : IEntityTypeConfiguration<BunkerEnviroment>
    {
        public void Configure(EntityTypeBuilder<BunkerEnviroment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("BunkerEnviroments");
            builder.Property(c => c.EnviromentBehavior).HasConversion<string>().HasMaxLength(50);
            builder.Property(c => c.EnviromentType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
