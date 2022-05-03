using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public class GameSessionCreateOptions
    {
        public GameSessionCreateOptions(string gameName, byte charactersCount)
        {
            CharactersCount = charactersCount;
            GameName = gameName;
        }
        public long? GameId { get; set; }
        public string GameName { get; set; }
        public byte CharactersCount { get; set; } = 10;
        public bool CharactersAlive { get; set; } = false;
    }
}
