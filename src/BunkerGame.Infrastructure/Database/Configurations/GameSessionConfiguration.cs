using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Bunkers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BunkerGame.Infrastructure.Database.Configurations
{
    internal class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(c => c.Bunker).WithOne().HasForeignKey<Bunker>(c=>c.GameSessionId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Catastrophe).WithMany();
            builder.HasMany(c => c.Characters).WithOne("GameSession").HasForeignKey(c => c.GameSessionId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.GameName).HasMaxLength(50);
            builder.Property(c => c.GameState).HasConversion<string>().HasMaxLength(50);
            builder.HasMany(c=>c.ExternalSurroundings).WithMany("GameSessions");

            builder.Navigation(b => b.Bunker).AutoInclude();
            builder.Navigation(b => b.Characters).AutoInclude();
            builder.Navigation(b => b.Catastrophe).AutoInclude();
            builder.Navigation(b => b.ExternalSurroundings).AutoInclude();
        }
    }
}
