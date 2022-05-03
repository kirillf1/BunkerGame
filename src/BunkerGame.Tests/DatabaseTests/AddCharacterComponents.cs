using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System.Linq;
using Xunit;

namespace BunkerGame.Tests.DatabaseTests
{
    public class AddCharacterComponents
    {
        [Fact]
        public async void AddProfession_Should_SaveInDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            try
            {
                var profession = new Profession("test", true);
                
                context.Professions.Add(profession);
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear();
                var professionFromDb = context.Professions.FirstOrDefault(c => c.Id == profession.Id);
                Assert.True(professionFromDb != null);
                Assert.True(professionFromDb!.Description == "test");
            }
            catch
            {
                Assert.True(false);
            }
            finally
            {
                context.Database.EnsureDeleted();
            }
        }
        [Fact]
        public async void AddProfession_With_Card_and_CharacterItem_Should_SaveInDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            try
            {
                var profession = new Profession("test", true);
                profession.UpdateCard(new Card("testCard", new CardMethod(), true, true));
                profession.UpdateCharacterItem(new CharacterItem("testItem", true, CharacterItemType.None));
                context.Professions.Add(profession);
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear();
                var professionFromDb = context.Professions.Include(c=>c.Card).Include(c=>c.CharacterItem).FirstOrDefault(c => c.Id == profession.Id);
                
                Assert.True(professionFromDb != null);
                Assert.True(professionFromDb!.CharacterItem != null);
                Assert.True(professionFromDb!.Card != null);
                Assert.Equal(professionFromDb!.Card!.Description, profession!.Card!.Description);
                Assert.Equal(profession.Card.Description,profession.Card!.Description);
               
            }
            catch
            {
                Assert.True(false);
            }
            finally
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
