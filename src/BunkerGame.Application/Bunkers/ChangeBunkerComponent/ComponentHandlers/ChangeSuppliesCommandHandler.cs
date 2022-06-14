using BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentHandlers
{
    public class ChangeSuppliesCommandHandler : BunkerComponentCommandHandler<Supplies>,
        IRequestHandler<ChangeBunkerComponentCommand<Supplies>, Supplies>
    {
        private readonly IMediator mediator;

        public ChangeSuppliesCommandHandler(IBunkerComponentRepository<Supplies> bunkerComponentRepository, IGameSessionRepository gameSessionRepository,
            IMediator mediator) : base(bunkerComponentRepository, gameSessionRepository)
        {
            this.mediator = mediator;
        }

        public async Task<Supplies> Handle(ChangeBunkerComponentCommand<Supplies> request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            var newSupplies = await UpdateSupplies(gameSession.Bunker, gameSession.Catastrophe);
            await SaveChanges();
            await mediator.Publish(new BunkerComponentChangedNotification<Supplies>(gameSession.Id, newSupplies));
            return newSupplies;
        }
        public async Task<Supplies> UpdateSupplies(Bunker bunker, Catastrophe catastrophe)
        {
            var useCatastropheYears = new Random().Next(0, 100) < 35;
            Supplies newSupplies;
            if (useCatastropheYears)
                newSupplies = new Supplies(catastrophe.HidingTerm);
            else
                newSupplies = await bunkerComponentRepository.GetBunkerComponent();
            bunker.UpdateSupplies(newSupplies);
            return newSupplies;
        }
    }
}
