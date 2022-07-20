using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace BunkerGame.VkApi.Infrastructure.PlayersRepository
{
    public class PlayersRepositoryChache : IPlayerRepository
    {
        public const string _PlayersKey = "players";
        private readonly IMemoryCache memoryChache;
        public PlayersRepositoryChache(IMemoryCache memoryChache)
        {
            this.memoryChache = memoryChache;
        }
        public Task AddPlayer(Player player)
        {
            List<Player> players;
            if (memoryChache.TryGetValue(_PlayersKey, out players))
            {
                var oldPlayer = players.Find(c => c.Id == player.Id);
                if (oldPlayer != null)
                    return Task.CompletedTask;
                players.Add(player);
                return Task.CompletedTask;
            }
            players = new List<Player>();
            players.Add(player);
            memoryChache.Set(_PlayersKey, players);
            return Task.CompletedTask;
        }

        public async Task<Player> GetPlayer(PlayerId playerId)
        {
            return await Task.Run(() =>
            {
                if (memoryChache.TryGetValue(_PlayersKey, out List<Player> players))
                {
                    var player = players.Find(c => c.Id == playerId);
                    if (player != null)
                        return player;
                }
                throw new ArgumentNullException(nameof(Player));
            });
        }

        public async Task<Player> GetPlayer(string firstName, string? lastName)
        {
            return await Task.Run(() =>
            {
                if (memoryChache.TryGetValue(_PlayersKey, out List<Player> players))
                {
                    var player = players.Find(c => c.FirstName == firstName && c.LastName == lastName);
                    if (player != null)
                        return player;
                }
                throw new ArgumentNullException(nameof(Player));
            });
        }

        public async Task<IEnumerable<Player>> GetPlayers(int skipCount, int count, Expression<Func<Player, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                if (memoryChache.TryGetValue(_PlayersKey, out List<Player> players))
                {
                    var query = players.AsQueryable();
                    if (predicate != null)
                        query = query.Where(predicate);
                    return query.Skip(skipCount).Take(count);
                }
                throw new ArgumentNullException(nameof(Player));
            });
        }

        public async Task<bool> IsUniqueName(string firstName, string? lastName)
        {
            return await Task.Run(() =>
            {
                if (memoryChache.TryGetValue(_PlayersKey, out List<Player> players))
                {
                    var player = players.Find(c => c.FirstName == firstName && c.LastName == lastName);
                    if (player != null)
                        return true;
                    return false;
                }
                return true;
            });
        }

        public async Task RemovePlayer(Player player)
        {
            await Task.Run(() =>
           {
               if (memoryChache.TryGetValue(_PlayersKey, out List<Player> players))
               {
                   players.Remove(player);
               }
           });
        }
    }
}
