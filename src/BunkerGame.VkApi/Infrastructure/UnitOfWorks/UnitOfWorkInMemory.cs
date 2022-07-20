using BunkerGame.Domain;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.Infrastructure.UnitOfWorks
{
    public class UnitOfWorkInMemory : IUnitOfWork
    {
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;

        public UnitOfWorkInMemory(IMediator mediator, IEventStore eventStore)
        {
            this.mediator = mediator;
            this.eventStore = eventStore;
        }
        public async Task Save(CancellationToken cancellationToken)
        {
            var events = await eventStore.ExtractNotifications();
            foreach (var @event in events)
            {
                await mediator.Publish(@event, cancellationToken);
            }
        }
    }
}
