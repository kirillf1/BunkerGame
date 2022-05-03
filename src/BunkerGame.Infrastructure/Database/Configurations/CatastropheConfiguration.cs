using BunkerGame.Domain.Catastrophes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Database.Configurations
{
    internal class CatastropheConfiguration : IEntityTypeConfiguration<Catastrophe>
    {
        public void Configure(EntityTypeBuilder<Catastrophe> builder)
        {
            builder.Property(c => c.CatastropheType).HasConversion<string>().HasMaxLength(50);
        }
    }
}
