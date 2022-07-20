using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.Infrastructure.Database.GameDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.Infrastructure.PlayersRepository
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
        public Task<Player> GetPlayer(PlayerId playerId)
        {
            return players.FirstAsync(p => p.Id == playerId);
        }

        public Task<Player> GetPlayer(string firstName, string? lastName)
        {
            return players.FirstAsync(p => p.FirstName == firstName && p.LastName == lastName);
        }


        public async Task<IEnumerable<Player>> GetPlayers(Expression<Func<Player, bool>>? predicate = null)
        {
            var query = players.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayers(int skipCount, int count, Expression<Func<Player, bool>>? predicate = null)
        {
            var query = players.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.Skip(skipCount).Take(count).ToListAsync();
        }

        public Task<bool> IsUniqueName(string firstName, string? lastName)
        {
            return bunkerGameDbContext.Players.AnyAsync(c => c.FirstName == firstName && c.LastName == lastName);
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
