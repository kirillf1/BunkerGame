using BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentHandlers
{
    public class ChangeBunkerSizeCommandHandler : BunkerComponentCommandHandler<BunkerSize>,
        IRequestHandler<ChangeBunkerComponentCommand<BunkerSize>, BunkerSize>
    {
        private readonly IMediator mediator;

        public ChangeBunkerSizeCommandHandler(IBunkerComponentRepository<BunkerSize> bunkerComponentRepository,
            IGameSessionRepository gameSessionRepository, IMediator mediator) : base(bunkerComponentRepository, gameSessionRepository)
        {
            this.mediator = mediator;
        }

        public async Task<BunkerSize> Handle(ChangeBunkerComponentCommand<BunkerSize> request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            var newBunkerSize = await UpdateBunkerSize(gameSession.Bunker, request.BunkerComponentId);
            gameSession.RefreshFreePlaceSize(newBunkerSize.Value);
            await SaveChanges();
            await mediator.Publish(new BunkerComponentChangedNotification<BunkerSize>(gameSession.Id, newBunkerSize));
            return newBunkerSize;
        }

        private async Task<BunkerSize> UpdateBunkerSize(Bunker bunker, int? bunkerComponentId)
        {
            var newBunkerSize = await bunkerComponentRepository.GetBunkerComponent();
            bunker.UpdateBunkerSize(newBunkerSize);
            return newBunkerSize;
        }
    }
}
