using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent
{
    public abstract class BunkerComponentCommandHandler<T> where T : BunkerComponent
    {
        protected readonly IBunkerComponentRepository<T> bunkerComponentRepository;
        protected readonly IGameSessionRepository gameSessionRepository;

        protected BunkerComponentCommandHandler(IBunkerComponentRepository<T> bunkerComponentRepository,
            IGameSessionRepository gameSessionRepository)
        {
            this.bunkerComponentRepository = bunkerComponentRepository;
            this.gameSessionRepository = gameSessionRepository;
        }
        protected async Task<GameSession> GetGameSession(long id)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithBunker(id);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            return gameSession;
        }
        protected async Task SaveChanges()
        {
            await gameSessionRepository.CommitChanges();
        }
    }
}

