using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BunkerGame.VkApi.Infrastructure.Database.GameDbContext.DbConfiguration
{
    internal class CharactersConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.Property(c => c.Id).HasConversion(c => c.Value, c => new CharacterId(c));
            builder.OwnsOne(c => c.PlayerId);
            builder.OwnsOne(c => c.GameSessionId);
            builder.OwnsOne(c => c.Childbearing);
            builder.OwnsOne(c => c.Sex);
            builder.OwnsOne(c => c.Size);
            builder.OwnsOne(c => c.Age);
            builder.OwnsOne(c => c.AdditionalInformation);
            builder.OwnsMany(c => c.Items);
            builder.OwnsMany(c => c.Cards, c => c.OwnsOne(c => c.Card)); ;
            builder.OwnsOne(c => c.Health);
            builder.OwnsOne(c => c.Hobby);
            builder.OwnsOne(c => c.Phobia);
            builder.OwnsOne(c => c.Profession);
            builder.OwnsOne(c => c.Trait);

        }
    }
}
