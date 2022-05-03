using BunkerGame.Domain.GameSessions;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.GameSessions
{
    public class GameSessionRepositoryEf : IGameSessionRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public GameSessionRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }
        public async Task AddGameSession(GameSession gameSession)
        {
           await bunkerGameDbContext.GameSessions.AddAsync(gameSession);
        }

        public async Task CommitChanges()
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<GameSession?> GetGameSession(long id, bool withComponents = true)
        {
            var query = bunkerGameDbContext.GameSessions.AsQueryable();
            if (!withComponents)
                query = query.IgnoreAutoIncludes();
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<string>> GetGameSessionNames(int count, Expression<Func<GameSession, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.GameSessions.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await bunkerGameDbContext.GameSessions.Select(c => c.GameName).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<GameSession>> GetGameSessions(int skipCount, int count, bool withComponents = true, Expression<Func<GameSession, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.GameSessions.AsQueryable();
            if (!withComponents)
                query = query.IgnoreAutoIncludes();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.Skip(skipCount).Take(count).ToListAsync();

        }

        public async Task<GameSession?> GetGameSessionWithBunker(long id)
        {
            var query = IncludeBunker(bunkerGameDbContext.GameSessions.IgnoreAutoIncludes());
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<GameSession?> GetGameSessionWithCatastrophe(long id)
        {
            var query = IncludeCatastrophe(bunkerGameDbContext.GameSessions.IgnoreAutoIncludes());
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<GameSession?> GetGameSessionWithCharacters(long id)
        {
            var query = IncludeCharacters(bunkerGameDbContext.GameSessions.IgnoreAutoIncludes());
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task RemoveGameSession(GameSession gameSession)
        {
            bunkerGameDbContext.Remove(gameSession);
            return Task.CompletedTask;
        }
        private IQueryable<GameSession> IncludeCatastrophe(IQueryable<GameSession> gameSessions)
        {
            return bunkerGameDbContext.GameSessions.AsQueryable().Include(c => c.Catastrophe);
        }
        private IQueryable<GameSession> IncludeCharacters(IQueryable<GameSession> gameSessions)
        {
            return bunkerGameDbContext.GameSessions.AsQueryable().AsSplitQuery().Include(c => c.Characters).ThenInclude(c => c.AdditionalInformation)
                .Include(c => c.Characters).ThenInclude(c => c.Cards).
             Include(c => c.Characters).ThenInclude(c => c.CharacterItems).
             Include(c => c.Characters).ThenInclude(c => c.Health).Include(c => c.Characters).ThenInclude(c => c.Hobby)
            .Include(c => c.Characters).ThenInclude(c => c.Phobia)
            .Include(c => c.Characters).ThenInclude(c => c.Profession)
            .Include(c => c.Characters).ThenInclude(c => c.Trait);
        }
        private IQueryable<GameSession> IncludeBunker(IQueryable<GameSession> gameSessions)
        {
            return bunkerGameDbContext.GameSessions.AsQueryable().AsSplitQuery()
            .Include(b => b.Bunker).ThenInclude(b => b.BunkerEnviroment)
            .Include(b => b.Bunker).ThenInclude(b => b.BunkerObjects)
            .Include(b => b.Bunker).ThenInclude(b => b.BunkerWall)
            .Include(b => b.Bunker).ThenInclude(b => b.ItemBunkers);
        }
    }
}