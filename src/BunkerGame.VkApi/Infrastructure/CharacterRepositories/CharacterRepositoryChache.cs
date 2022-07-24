using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
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
            if (memoryCache.TryGetValue(_СharactersKey, out ConcurrentDictionary<CharacterId, Character> characters))
            {
                characters.TryAdd(character.Id, character);
                return Task.CompletedTask;
            }
            characters = new ConcurrentDictionary<CharacterId, Character>();
            characters.TryAdd(character.Id, character);
            memoryCache.Set(_СharactersKey, characters);
            return Task.CompletedTask;
        }

        public Task<Character> GetCharacter(CharacterId characterId)
        {

            if (memoryCache.TryGetValue(_СharactersKey, out ConcurrentDictionary<CharacterId, Character> characters))
            {
                characters.TryGetValue(characterId, out var character);
                if (character != null)
                    return Task.FromResult(character);
            }
            throw new ArgumentNullException(nameof(Character));
        }

        public Task<IEnumerable<Character>> GetCharacters(Expression<Func<Character, bool>>? predicate = null)
        {

            if (memoryCache.TryGetValue(_СharactersKey, out ConcurrentDictionary<CharacterId, Character> characters))
            {
                var query = characters.Select(c => c.Value).AsQueryable();
                if (predicate != null)
                    query = query.Where(predicate);
                return Task.FromResult(query.AsEnumerable());
            }
            throw new ArgumentNullException(nameof(Character));
        }

    public Task RemoveCharacter(Character character)
    {
        if (memoryCache.TryGetValue(_СharactersKey, out ConcurrentDictionary<CharacterId, Character> characters))
        {
            characters.TryRemove(character.Id, out _);
        }
        return Task.CompletedTask;
    }

    public Task RemoveCharacters(IEnumerable<Character> characters)
    {
        if (memoryCache.TryGetValue(_СharactersKey, out ConcurrentDictionary<CharacterId, Character> charactersDict))
        {
            foreach (var character in characters)
            {
                charactersDict.TryRemove(character.Id, out _);
            }
        }
        return Task.CompletedTask;
    }
}
}
