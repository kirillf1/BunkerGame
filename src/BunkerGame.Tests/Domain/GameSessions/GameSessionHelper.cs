
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Domain.GameSessions
{
    internal static class GameSessionHelper
    {
        private static Random Random = new Random();
        public static GameSession CreateGameSessionReadyToStart()
        {
            var gameSession = new GameSession(new GameSessionId(Guid.NewGuid()), new PlayerId(Guid.NewGuid()));
            var characters = CreateCharacters(6);
            foreach (var character in characters)
            {
                gameSession.AddCharacter(character);
            }
            gameSession.ClearEvents();
            return gameSession;

        }
        public static Bunker CreateBunker()
        {
            var bunkerBuilder = new BunkerBuilder();
            return bunkerBuilder.BuildCondition(new Condition(Random.Next(0, 10), $"Wall {Random.Next()}"))
                 .BuildSize(new Size(Random.Next(200, 400)))
                 .BuildSupplies(new Supplies(Random.Next(10, 20)))
                 .Build();
        }
        public static IEnumerable<CharacterGame> CreateCharacters(int count)
        {
            var characterList = new List<CharacterGame>();
            for (int i = 0; i < count; i++)
            {
                var characterId = new CharacterId(Guid.NewGuid());
                var playerId = new PlayerId(Guid.NewGuid());
                characterList.Add(new CharacterGame(characterId, playerId));
            }
            return characterList;
        }
    }
}
