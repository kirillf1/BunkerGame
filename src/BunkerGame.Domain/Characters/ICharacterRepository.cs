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
        public Task<Character?> GetCharacterById(int id,bool withComponents = true);
        public Task<Character?> GetCharacter(long gameSessionId,long playerId, bool withComponents = true);
        public Task<IEnumerable<Character>> GetCharacters(int count, bool withComponents = true, Expression<Func<Character, bool>>? predicate = null);
        public Task CommitChanges();
    }
}
