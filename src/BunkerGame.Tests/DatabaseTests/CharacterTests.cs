using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.DatabaseTests
{
    public class CharacterTests
    {
        [Fact]
        public async void AddCharacter_Should_AddInDb()
        {
            var character = CharacterCreator.CreateCharacter();
            using var context = DbCreator.CreateInMemoryContext();

            context.Add(character);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            var characterFromDb = context.Characters.Include(c=>c.Cards).FirstOrDefault(c=>c.Id == character.Id);

            Assert.NotNull(characterFromDb);
            Assert.True(character.Phobia.Id > 0);
            Assert.True(characterFromDb!.Cards.Count >= 2);

        }
        [Fact]
        public async void UpdateCharacterCharacteristic_Should_Change()
        {
            var character = CharacterCreator.CreateCharacter();
            using var context = DbCreator.CreateInMemoryContext();
            context.Add(character);
            await context.SaveChangesAsync();

            character.UpdatePhobia(new Phobia("testPhobia", true));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            var characterFromDb = context.Characters.Include(c => c.Phobia).FirstOrDefault(c => c.Id == character.Id);

            Assert.NotNull(characterFromDb);
            Assert.True(characterFromDb!.Phobia.Id == character.Phobia.Id && characterFromDb.Phobia.Description == character.Phobia.Description);
        }
        [Fact]
        public async void UpdateCharacterCharacteristic_WithoutSave_ShouldNotChange()
        {
            var character = CharacterCreator.CreateCharacter();
            using var context = DbCreator.CreateInMemoryContext();
            context.Add(character);
            await context.SaveChangesAsync();

            character.UpdatePhobia(new Phobia("testPhobia", true));
            context.ChangeTracker.Clear();
            var characterFromDb = context.Characters.Include(c => c.Phobia).FirstOrDefault(c => c.Id == character.Id);

            Assert.NotNull(characterFromDb);
            Assert.False(characterFromDb!.Phobia.Id == character.Phobia.Id && characterFromDb.Phobia.Description == character.Phobia.Description);
        }
        [Fact]
        public async void CreateCharacter_CheckUsedCards_ShouldReturnFalse()
        {
            var characterFirst = CharacterCreator.CreateCharacter();
            using var context = DbCreator.CreateInMemoryContext();
            context.Characters.AddRange(characterFirst);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var characterFromDb = context.Characters.First(c => c.Id == characterFirst.Id);
            Assert.False(characterFirst.CheckCardUsed(1));
            Assert.False(characterFirst.CheckCardUsed(2));
        }
        [Fact]
        public async void CreateCharacter_CheckUsedCards_OutRange_ShouldThrowEx()
        {
            var characterFirst = CharacterCreator.CreateCharacter();
            using var context = DbCreator.CreateInMemoryContext();
            context.Characters.Add(characterFirst);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var characterFromDb = context.Characters.First(c => c.Id == characterFirst.Id);

            Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>characterFromDb.CheckCardUsed(10));
        }
    }
}
