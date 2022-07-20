using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Players
{
    public interface IPlayerRepository
    {
        public Task<Player> GetPlayer(PlayerId playerId);
        public Task<Player> GetPlayer(string firstName, string? lastName);
        public Task<IEnumerable<Player>> GetPlayers(int skipCount, int count, Expression<Func<Player, bool>>? predicate = null);
        public Task<bool> IsUniqueName(string firstName, string? lastName);
        public Task AddPlayer(Player player);
        public Task RemovePlayer(Player player);
    }
}
