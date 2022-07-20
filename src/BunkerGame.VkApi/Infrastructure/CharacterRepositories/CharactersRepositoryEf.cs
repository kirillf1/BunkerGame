using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.Infrastructure.Database.GameDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.Infrastructure.CharacterRepositories
{
    public class CharactersRepositoryEf : ICharacterRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public CharactersRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }

        public async Task AddCharacter(Character character)
        {
            await bunkerGameDbContext.Characters.AddAsync(character);
        }
        public Task<Character> GetCharacter(CharacterId characterId)
        {
            return bunkerGameDbContext.Characters.FirstAsync(c => c.Id == characterId);
        }


        public async Task<IEnumerable<Character>> GetCharacters(Expression<Func<Character, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.Characters.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public Task RemoveCharacter(Character character)
        {
            bunkerGameDbContext.Characters.Remove(character);
            return Task.CompletedTask;
        }

        public Task RemoveCharacters(IEnumerable<Character> characters)
        {
            bunkerGameDbContext.Characters.RemoveRange(characters);
            return Task.CompletedTask;
        }
    }
}
