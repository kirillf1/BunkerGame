
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent
{
    public class ChangeBunkerComponentCommandHandler : IRequestHandler<ChangeBunkerComponentCommand, Bunker>
    {
        private readonly IBunkerComponentRepositoryLocator bunkerComponentRepositoryLocator;
        private readonly IGameSessionRepository gameSessionRepository;

        public ChangeBunkerComponentCommandHandler(IBunkerComponentRepositoryLocator bunkerComponentRepositoryLocator,
            IGameSessionRepository gameSessionRepository)
        {
            this.bunkerComponentRepositoryLocator = bunkerComponentRepositoryLocator;
            this.gameSessionRepository = gameSessionRepository;
        }
        public async Task<Bunker> Handle(ChangeBunkerComponentCommand request, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithBunker(request.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            var bunker = gameSession.Bunker;
            object? bunkerComponent = default;
            var targetComponentId = request.BunkerComponentId;
            switch (request.BunkerComponentType)
            {
                case Type t when t == typeof(BunkerWall):
                    bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerWall>(true,
                       b=> targetComponentId.HasValue ? b.Id == targetComponentId.Value :  b.Id != bunker.BunkerWall.Id );
                    break;
                case Type t when t == typeof(BunkerObject):
                    bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerObject>(true,
                        b => targetComponentId.HasValue ? b.Id == targetComponentId.Value : !bunker.BunkerObjects.Any(c => c.Id != b.Id));
                    break;
                case Type t when t == typeof(BunkerEnviroment):
                    bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerEnviroment>(true,
                        b => targetComponentId.HasValue ? b.Id == targetComponentId.Value: b.Id != bunker.BunkerEnviroment.Id);
                    break;
                case Type t when t == typeof(ItemBunker):
                    bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<ItemBunker>(true,
                        b => targetComponentId.HasValue ? b.Id == targetComponentId.Value : !bunker.ItemBunkers.Any(c => c.Id != b.Id));
                    break;
            }
            
            bunker.UpdateBunkerComponent(bunkerComponent ?? throw new ArgumentNullException("Can't find BunkerComponent"));
            return bunker;
        }
        
    }
}
