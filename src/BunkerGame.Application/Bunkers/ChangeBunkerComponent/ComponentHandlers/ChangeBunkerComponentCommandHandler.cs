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
    public class ChangeBunkerComponentCommandHandler<T> : BunkerComponentCommandHandler<T>,
        IRequestHandler<ChangeBunkerComponentCommand<T>, T> where T : BunkerComponentEntity
    {

        private readonly IMediator mediator;

        public ChangeBunkerComponentCommandHandler(IBunkerComponentRepository<T> bunkerComponentRepository,
            IGameSessionRepository gameSessionRepository, IMediator mediator) : base(bunkerComponentRepository, gameSessionRepository)
        {
            this.mediator = mediator;
        }

        public async Task<T> Handle(ChangeBunkerComponentCommand<T> request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            var newComponent = await UpdateBunkerComponent(gameSession.Bunker, request.BunkerComponentId);
            await SaveChanges();
            await mediator.Publish(new BunkerComponentChangedNotification<T>(gameSession.Id, newComponent));
            return newComponent;
        }
        private async Task<T> UpdateBunkerComponent(Bunker bunker, int? componentId)
        {
            var bunkerProxy = new BunkerProxy(bunker);
            var component = bunkerProxy.GetBunkerComponent<T>();
            component.Component = await GetComponentById(componentId, component.Component.Id);
            return component.Component;
        }

        private async Task<T> GetComponentById(int? componentId, int oldComponentId)
        {
            return await bunkerComponentRepository.GetBunkerComponent(false,
                c => componentId.HasValue ? c.Id == componentId.Value : c.Id != oldComponentId);
        }
        //public async Task<Bunker> Handle(ChangeBunkerComponentCommand request, CancellationToken cancellationToken)
        //{
        //    var gameSession = await gameSessionRepository.GetGameSessionWithBunker(request.GameSessionId);
        //    if (gameSession == null)
        //        throw new ArgumentNullException(nameof(gameSession));
        //    var bunker = gameSession.Bunker;
        //    object? bunkerComponent = default;
        //    var targetComponentId = request.BunkerComponentId;
        //    switch (request.BunkerComponentType)
        //    {
        //        case Type t when t == typeof(BunkerWall):
        //            bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerWall>(true,
        //               b=> targetComponentId.HasValue ? b.Id == targetComponentId.Value :  b.Id != bunker.BunkerWall.Id );
        //            break;
        //        case Type t when t == typeof(BunkerObject):
        //            bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerObject>(true,
        //                b => targetComponentId.HasValue ? b.Id == targetComponentId.Value : !bunker.BunkerObjects.Any(c => c.Id != b.Id));
        //            break;
        //        case Type t when t == typeof(BunkerEnviroment):
        //            bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<BunkerEnviroment>(true,
        //                b => targetComponentId.HasValue ? b.Id == targetComponentId.Value: b.Id != bunker.BunkerEnviroment.Id);
        //            break;
        //        case Type t when t == typeof(ItemBunker):
        //            bunkerComponent = await bunkerComponentRepositoryLocator.GetBunkerComponent<ItemBunker>(true,
        //                b => targetComponentId.HasValue ? b.Id == targetComponentId.Value : !bunker.ItemBunkers.Any(c => c.Id != b.Id));
        //            break;
        //        case Type t when t == typeof(Supplies):
        //            bunkerComponent = new Supplies(new Random().Next(5, 10));
        //            break;
        //        case Type t when t == typeof(BunkerSize):
        //            var bunkerSizeValue = new Random().Next(200, 600);
        //            bunkerComponent = new BunkerSize(bunkerSizeValue);
        //            gameSession.RefreshFreePlaceSize(bunkerSizeValue);
        //            break;
        //    }

        //    bunker.UpdateBunkerComponent(bunkerComponent ?? throw new ArgumentNullException("Can't find BunkerComponent"));
        //    await gameSessionRepository.CommitChanges();
        //    return bunker;
        //}

        //public Task<Bunker> Handle(ChangeBunkerComponentCommand<T> request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
