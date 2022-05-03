using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters
{
    public interface ICharacterFactory
    {
        public Task<Character> CreateCharacter(CharacterOptions options);
        public Task<IEnumerable<Character>> CreateCharacters(int count, CharacterOptions options);
    }
}
