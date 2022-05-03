using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public interface IGameSessionRepository
    {
        public Task<IEnumerable<string>> GetGameSessionNames(int count,Expression<Func<GameSession, bool>>? predicate = null);
        public Task<GameSession?> GetGameSession(long id,bool withComponents = true);
        public Task<GameSession?> GetGameSessionWithCharacters(long id);
        public Task<GameSession?> GetGameSessionWithBunker(long id);
        public Task<GameSession?> GetGameSessionWithCatastrophe(long id);
        public Task<IEnumerable<GameSession>> GetGameSessions(int skipCount,int count, bool withComponents = true, Expression<Func<GameSession, bool>>? predicate = null);
        public Task AddGameSession(GameSession gameSession);
        public Task RemoveGameSession(GameSession gameSession);
        public Task CommitChanges();
    }
}
