using BunkerGame.Domain.Characters;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.Characters
{
    public class CharactersRepositoryEf : ICharacterRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public CharactersRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }
        public async Task CommitChanges()
        {
           await  bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<Character?> GetCharacter(long gameSessionId, long playerId, bool withComponents = true)
        {
            var query = bunkerGameDbContext.Characters.AsQueryable();
            if (!withComponents)
            {
                query = query.IgnoreAutoIncludes();
            }
            return await query.FirstOrDefaultAsync(c=>c.GameSessionId == gameSessionId && c.PlayerId == playerId);
        }

        public Task<Character?> GetCharacterById(int id, bool withComponents = true)
        {
            var query = bunkerGameDbContext.Characters.AsQueryable();
            if (!withComponents)
            {
                query = query.IgnoreAutoIncludes();
            }
            return query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Character>> GetCharacters(int count, bool withComponents = true, Expression<Func<Character, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.Characters.AsQueryable();
            if (!withComponents)
            {
                query = query.IgnoreAutoIncludes();
            }
            if (predicate != null)
                query = query.Where(predicate);
            return await query.Take(count).ToListAsync();
        }
    }
}
