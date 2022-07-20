using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters
{
    public interface ICharacterRepository
    {
        public Task<Character> GetCharacter(CharacterId characterId);
        public Task<IEnumerable<Character>> GetCharacters(Expression<Func<Character, bool>>? predicate = null);
        public Task AddCharacter(Character character);
        public Task RemoveCharacter(Character character);
        public Task RemoveCharacters(IEnumerable<Character> characters);
    }
}
