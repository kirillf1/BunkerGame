using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.Infrastructure.CharacterRepositories
{
    public class CharacterRepositoryChache : ICharacterRepository
    {

        private readonly IMemoryCache memoryCache;
        public const string _СharactersKey = "characters";
        public CharacterRepositoryChache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public Task AddCharacter(Character character)
        {
            List<Character> characters;
            if (memoryCache.TryGetValue(_СharactersKey, out characters))
            {
                var oldCharacter = characters.Find(c => c.Id == character.Id);
                if (oldCharacter != null)
                    return Task.CompletedTask;
                characters.Add(character);
                return Task.CompletedTask;
            }
            characters = new List<Character>();
            characters.Add(character);
            memoryCache.Set(_СharactersKey, characters);
            return Task.CompletedTask;
        }

        public async Task<Character> GetCharacter(CharacterId characterId)
        {
            return await Task.Run(() =>
             {
                 if (memoryCache.TryGetValue(_СharactersKey, out List<Character> characters))
                 {
                     var character = characters.Find(c => c.Id == characterId);
                     if (character != null)
                         return character;
                 }
                 throw new ArgumentNullException(nameof(Character));
             });
        }

        public async Task<IEnumerable<Character>> GetCharacters(Expression<Func<Character, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                if (memoryCache.TryGetValue(_СharactersKey, out List<Character> characters))
                {
                    var query = characters.AsQueryable();
                    if(predicate != null)
                        query = query.Where(predicate);
                    return query;
                }
                throw new ArgumentNullException(nameof(Character));
            });
        }

        public Task RemoveCharacter(Character character)
        {
            if (memoryCache.TryGetValue(_СharactersKey, out List<Character> characters))
            {
                characters.Remove(character);
            }
            return Task.CompletedTask;
        }

        public Task RemoveCharacters(IEnumerable<Character> characters)
        {
            if (memoryCache.TryGetValue(_СharactersKey, out List<Character> charactersList))
            {
                foreach (var character in characters)
                {
                    charactersList.Remove(character);
                }
            }
            return Task.CompletedTask;
        }
    }
}
