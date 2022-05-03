using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public interface IGameSessionFactory
    {
        public Task<GameSession> CreateGameSession(GameSessionCreateOptions gameSessionOptions);
    }
}
