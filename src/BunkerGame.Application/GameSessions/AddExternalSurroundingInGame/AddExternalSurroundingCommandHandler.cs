using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.AddExternalSurroundingInGame
{
    public class AddExternalSurroundingCommandHandler : IRequestHandler<AddExternalSurroundingCommand, ExternalSurrounding>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IExternalSurroundingRepository externalSurroundingRepository;

        public AddExternalSurroundingCommandHandler(IGameSessionRepository gameSessionRepository, IExternalSurroundingRepository externalSurroundingRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.externalSurroundingRepository = externalSurroundingRepository;
        }
        public async Task<ExternalSurrounding> Handle(AddExternalSurroundingCommand request, CancellationToken cancellationToken)
        {
            var surrounding = await externalSurroundingRepository.GetExternalSurrounding(request.SurroundingId);
            if (surrounding == null)
                throw new ArgumentNullException(nameof(surrounding));
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            gameSession.AddSurrounding(surrounding);
            await gameSessionRepository.CommitChanges();
            return surrounding;
        }
    }
}
