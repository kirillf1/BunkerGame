using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters
{
    public interface ICharacterFactory
    {
        public Task<Character> CreateCharacter(CharacterId characterId, PlayerId playerId,GameSessionId gameSessionId);
        public Task<IEnumerable<Character>> CreateCharacters(IEnumerable<(CharacterId,PlayerId)> characterIds,GameSessionId gameSessionId);
    }
}
