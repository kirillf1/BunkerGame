using BunkerGame.Domain.Characters;
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
        public Task<Player> GetPlayer(long id);
        public Task<Player?> GetPlayerByCharacterId(int characterId);
        public Task<IEnumerable<Player>> GetPlayers(Expression<Func<Player, bool>>? predicate = null);
        public Task<IEnumerable<int>> GetCharactersIds(long playerId, Expression<Func<Character, bool>>? predicate = null);
        /// <summary>
        /// Get not added player ids
        /// </summary>
        /// <param name="ids">Player ids for check</param>
        /// <returns>Ids that don't exist</returns>
        public Task<IEnumerable<long>> GetNoExistingIds(IEnumerable<long> ids);
        public Task<bool> PlayerAny(Expression<Func<Player, bool>>? predicate = null);
        public Task AddPlayer(Player player);
        public Task RemovePlayer(Player player);
        public Task CommitChanges();
    }
}
