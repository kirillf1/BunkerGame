using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Players;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.Players
{
    public class PlayerRepositoryEf : IPlayerRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;
        DbSet<Player> players;
        public PlayerRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
            players = bunkerGameDbContext.Players;
        }
        public async Task AddPlayer(Player player)
        {
            await players.AddAsync(player);
        }

        public async Task CommitChanges()
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetCharactersIds(long playerId, Expression<Func<Character, bool>>? predicate = null)
        {
            var query = players.AsQueryable();
            if (predicate != null)
                query = query.Include(c => c.Characters.Where(predicate.Compile()));
            var player = await query.FirstAsync(c => c.Id == playerId);
            return player.Characters.Select(c => c.Id);
        }

        public async Task<IEnumerable<long>> GetNoExistingIds(IEnumerable<long> ids)
        {
            var exsistingIds = await players.Select(c => c.Id).Where(p => ids.Contains(p)).ToListAsync();
            return ids.Where(p=> !exsistingIds.Contains(p));
        }

        public async Task<Player> GetPlayer(long id)
        {
            return await players.FirstAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Player>> GetPlayers(Expression<Func<Player, bool>>? predicate = null)
        {
            var query = players.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<bool> PlayerAny(Expression<Func<Player, bool>>? predicate = null)
        {
            if (predicate != null)
                return await players.AnyAsync(predicate);
            return await players.AnyAsync();
        }

        public Task RemovePlayer(Player player)
        {
            players.Remove(player);
            return Task.CompletedTask;
        }
    }
}
