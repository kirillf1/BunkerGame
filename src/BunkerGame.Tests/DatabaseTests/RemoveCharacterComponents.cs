using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BunkerGame.Tests.DatabaseTests
{
    public class RemoveCharacterComponents
    {
        [Fact]
        public async void Add_And_Remove_ProfessionWithCards_Card_ShouldNotDelete()
        {
            using var context = DbCreator.CreateInMemoryContext();
            try
            {
                var profession = new Profession("testProf", true);
                profession.UpdateCard(new Card("TestCard", new Domain.Characters.CharacterComponents.Cards.CardMethod(),true, false));
                var card = profession.Card;
                context.Add(profession);
                await context.SaveChangesAsync();
                context.Professions.Remove(profession);
                await context.SaveChangesAsync();

                var cardFromDb = context.Cards.FirstOrDefault(c => c.Id == card!.Id);
                var professionFromDb = context.Professions.FirstOrDefault(c => c.Id == profession.Id);
                Assert.NotNull(cardFromDb);
                Assert.Null(professionFromDb);
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
