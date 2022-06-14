using BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentCollectionHandlers
{
    public class ChangeBunkerComponentCollectionCommandHandler<T> : BunkerComponentCommandHandler<T>,
        IRequestHandler<ChangeBunkerComponentCollectionCommand<T>, IReadOnlyCollection<T>>
        where T : BunkerComponentEntity
    {
        private readonly IMediator mediator;

        public ChangeBunkerComponentCollectionCommandHandler(IBunkerComponentRepository<T> bunkerComponentRepository,
            IGameSessionRepository gameSessionRepository, IMediator mediator) : base(bunkerComponentRepository, gameSessionRepository)
        {
            this.mediator = mediator;
        }

        public async Task<IReadOnlyCollection<T>> Handle(ChangeBunkerComponentCollectionCommand<T> request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            var newComponents = await UpdateBunkerComponents(gameSession.Bunker, request.BunkerComponentId);

            await SaveChanges();
            await mediator.Publish(new BunkerComponentCollectionChangedNotification<T>(gameSession.Id, newComponents));
            return newComponents;

        }
        private async Task<IReadOnlyCollection<T>> UpdateBunkerComponents(Bunker bunker, int? componentId)
        {
            var bunkerProxy = new BunkerProxy(bunker);
            var components = bunkerProxy.GetBunkerComponentCollection<T>();
            if (componentId.HasValue)
            {
                await UpdateFirstComponent(componentId.Value, components);
            }
            else
            {
                var componentsCount = components.Components.Count;
                var newComponents = new List<T>(componentsCount);
                switch (componentsCount)
                {
                    case 1:
                        newComponents.Add(await bunkerComponentRepository.GetBunkerComponent(true));
                        break;
                    case int count when count > 1:
                        newComponents.AddRange(await bunkerComponentRepository.GetBunkerComponents(count, true));
                        break;
                }
                components.Components = newComponents;
            }
            return components.Components;
        }
        private async Task UpdateFirstComponent(int componentId, IBunkerComponentCollection<T> components)
        {
            var newComponent = await bunkerComponentRepository.GetBunkerComponent(true, c => c.Id == componentId);
            var newComponents = new List<T>(components.Components);
            newComponents.RemoveAt(0);
            newComponents.Add(newComponent);
            components.Components = newComponents;
        }
    }
}

