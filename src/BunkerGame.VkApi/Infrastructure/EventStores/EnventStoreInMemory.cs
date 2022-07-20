using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.Infrastructure.EventStores
{
    public class EnventStoreInMemory : IEventStore
    {
        List<INotification> Notifications;
        public EnventStoreInMemory()
        {
            Notifications = new();
        }
        public async Task<IEnumerable<INotification>> ExtractNotifications()
        {
            return await Task.Run(() =>
             {
                 var notifications = Notifications.GetRange(0, Notifications.Count);
                 Notifications.Clear();
                 return notifications;

             });
        }
        public Task Save<TId>(AggregateRoot<TId> aggregate)
        {
            Notifications.AddRange(aggregate.GetEvents());
            aggregate.ClearEvents();
            return Task.CompletedTask;
        }
    }
}
