using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public interface IGameSessionRepository
    {
        public Task<GameSession> GetGameSession(GameSessionId gameSessionId);
        public Task AddGameSession(GameSession gameSession);
        public Task RemoveGameSession(GameSession gameSession);
    }
}
