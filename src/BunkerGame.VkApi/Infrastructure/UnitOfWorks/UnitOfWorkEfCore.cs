using BunkerGame.Domain;
using BunkerGame.Framework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.VkApi.Infrastructure.UnitOfWorks
{
    public class UnitOfWorkEfCore<T> : IUnitOfWork where T : DbContext
    {
        private readonly T context;
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;

        public UnitOfWorkEfCore(T context, IMediator mediator, IEventStore eventStore)
        {
            this.context = context;
            this.mediator = mediator;
            this.eventStore = eventStore;
        }
        public async Task Save(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
            var events = await eventStore.ExtractNotifications();
            foreach (var @event in events)
            {
                await mediator.Publish(@event, cancellationToken);
            }
        }
    }
}
