using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.Helpers
{
    public static class GameSessionFactory
    {
        public static GameSession CreateGameSession()
        {
            return new GameSession(DateTime.Now.Millisecond, "TestGameName", BunkerCreator.CreateBunker(),
                 new Catastrophe(CatastropheType.None, 10, 10, "Catastrophe", 10, 10), CharacterCreator.CreateCharacters(6).ToList());
        }
    }
}
