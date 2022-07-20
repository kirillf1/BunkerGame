using BunkerGame.Domain.GameResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGame.VkApi.Infrastructure.Database.GameDbContext.DbConfiguration
{
    public class GameResultsConfiguration : IEntityTypeConfiguration<GameResult>
    {
        public void Configure(EntityTypeBuilder<GameResult> builder)
        {
            builder.Property(c => c.Id).HasConversion(c => c.Value, c => new Domain.Shared.GameSessionId(c));
        }
    }


}
