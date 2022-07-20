using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using System;
using System.Linq;

namespace DDDTest.Domain.GameSessions
{
    public class UpdateBunkerComponentTests
    {
        [Fact]
        public void StartGame_CorrectCharactersCount_GameStarted()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            foreach (var character in GameSessionHelper.CreateCharacters(GameSession.MinCharactersInGame))
            {
                gameSession.AddCharacter(character);
            }

            gameSession.StartGame();

            Assert.True(gameSession.GameState == GameState.Started);
            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.GameStarted));
        }
        [Fact]
        public void StartGame_InvalidCharactersCount_GameNotStarted()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            var invalidCharacterCount = 4;
            foreach (var character in GameSessionHelper.CreateCharacters(invalidCharacterCount))
            {
                gameSession.AddCharacter(character);
            }

            gameSession.StartGame();

            Assert.False(gameSession.GameState == GameState.Started);
            Assert.DoesNotContain(gameSession.GetEvents(), c => c.GetType() == typeof(Events.GameStarted));
        }
        [Fact]
        public void RestartGame_GameStateStarted_GameStatePreparationAndEmptyCharacters()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            foreach (var character in GameSessionHelper.CreateCharacters(GameSession.MinCharactersInGame))
            {
                gameSession.AddCharacter(character);
            }

            gameSession.StartGame();
            gameSession.Restart();

            Assert.True(gameSession.GameState == GameState.Preparation);
            Assert.Empty(gameSession.Characters);
            Assert.Empty(gameSession.ExternalSurroundings);
            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.GameRestarted));
        }
        [Fact]
        public void UpdateBunker_NewBunker_BunkerUpdated()
        {
            var gameSession = GameSessionHelper.CreateGameSessionReadyToStart();
            var newBunker = GameSessionHelper.CreateBunker();

            gameSession.UpdateBunker(newBunker);

            Assert.Equal(gameSession.Bunker, newBunker);
            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.BunkerUpdated));
        }
        [Fact]
        public void KickCharacters_ToFreeSeatsFiled_CharactersKicked()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            var seats = gameSession.FreePlaceSize.GetAvailableSeats();
            var characters = GameSessionHelper.CreateCharacters(5).ToList();
            foreach (var character in characters)
            {
                gameSession.AddCharacter(character);
            }
            
            gameSession.StartGame();
            for (int i = 0; i < gameSession.Characters.Count - seats; i++)
            {
                gameSession.KickCharacter(characters[i].Id);
            }

            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.CharacterKicked));
            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.SeatsFilled));
        }
        [Fact]
        public void KickCharacter_CharacterForKick_KickFromGameSession_AddChacterKickedEvent()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            foreach (var character in GameSessionHelper.CreateCharacters(6))
            {
                gameSession.AddCharacter(character);
            }
            gameSession.StartGame();
            gameSession.KickCharacter(gameSession.Characters.First().Id);

            Assert.Contains(gameSession.GetEvents(), c => c.GetType() == typeof(Events.CharacterKicked));
            Assert.DoesNotContain(gameSession.GetEvents(), c => c.GetType() == typeof(Events.SeatsFilled));
        }
        [Fact]
        public void KickCharacter_CharacterForKick_GameNotStarted_CharacterNotKicked()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            foreach (var character in GameSessionHelper.CreateCharacters(6))
            {
                gameSession.AddCharacter(character);
            }
            gameSession.KickCharacter(gameSession.Characters.First().Id);

            Assert.Empty(gameSession.Characters.Where(c => c.IsKicked));
        }
    }
}