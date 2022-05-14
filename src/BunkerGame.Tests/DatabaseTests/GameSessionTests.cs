using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.DatabaseTests
{
    public class GameSessionTests
    {
        [Fact]
        public async void AddGameSession_ShouldSaveInDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            var gameSession = new GameSession(DateTime.Now.Millisecond, "TestGameName", BunkerCreator.CreateBunker(),
                new Catastrophe(CatastropheType.None, 10, 10, "Catastrophe", 10, 10), CharacterCreator.CreateCharacters(6).ToList());
            context.Add(gameSession);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var gameSessionFromDb = context.GameSessions.Include(c=>c.Bunker).Include(c=>c.Catastrophe).
                Include(c=>c.Characters).FirstOrDefault(g=>g.Id == gameSession.Id);
            
            Assert.NotNull(gameSessionFromDb);
            Assert.NotEmpty(gameSessionFromDb!.Characters);
            Assert.Equal(gameSessionFromDb.GameName, gameSession.GameName);

        }
        [Fact]
        public async void AddGameSession_And_RemoveCharacter_ChangeBunker_ShouldDeleteCharacterFromDb()
        {
            using var context = DbCreator.CreateInMemoryContext();
            var gameSession = new GameSession("TestGameName", BunkerCreator.CreateBunker(),
                new Catastrophe(CatastropheType.None, 10, 10, "Catastrophe", 10, 10), new List<Character>(CharacterCreator.CreateCharacters(6)));
            context.Add(gameSession);
            await context.SaveChangesAsync();

            var characterForDelete = gameSession.Characters.First();
            var bunkerForDelete = gameSession.Bunker;
            var oldCatastrophe = gameSession.Catastrophe;
            gameSession.RemoveCharacterFromGame(characterForDelete);
            gameSession.UpdateBunker(BunkerCreator.CreateBunker());
            gameSession.UpdateСatastrophe(new Catastrophe(CatastropheType.None, 10, 10, "testCatastophe", 10, 11));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
           
            var gameSessionFromDb = context.GameSessions.Include(c=>c.Bunker).Include(c=>c.Characters).Include(c=>c.Catastrophe)
                .FirstOrDefault(g => g.Id == gameSession.Id);
            Assert.NotNull(gameSessionFromDb);
            Assert.False(gameSessionFromDb!.Bunker.Id == bunkerForDelete.Id);
            Assert.False(gameSessionFromDb.Catastrophe.Id == oldCatastrophe.Id);
            Assert.NotNull(context.Catastrophes.FirstOrDefault(c => c.Id == oldCatastrophe.Id));
            Assert.DoesNotContain(gameSessionFromDb!.Characters, c => c.Id == characterForDelete.Id);
            Assert.DoesNotContain(context.Characters,c=>c.Id == characterForDelete.Id);
            
        }
     
    }
}
