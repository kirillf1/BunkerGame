using BunkerGame.Application.GameSessions.EmptyFreePlaceEvent;
using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ChangeFreePlace
{
    public class ChangeFreePlaceCommandHandler : IRequestHandler<ChangeFreePlaceCommand,byte>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IMediator mediator;

        public ChangeFreePlaceCommandHandler(IGameSessionRepository gameSessionRepository,IMediator mediator)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.mediator = mediator;
        }
        public async Task<byte> Handle(ChangeFreePlaceCommand request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(request.GameSessionId, true);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            if (request.SizeIncreased)
                gameSession.AddFreePlace();
            else
                gameSession.RemoveFreePlace();
            if (gameSession.Characters.Count(c => c.IsAlive) <= gameSession.FreePlaceSize)
               await mediator.Publish(new EmptyFreePlaceNotificationMessage(gameSession.Id, gameSession.FreePlaceSize), cancellationToken);

            return gameSession.FreePlaceSize;
        }
    }
}
