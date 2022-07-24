using BunkerGame.Framework;
using MediatR;
using System.Collections.Concurrent;

namespace BunkerGame.VkApi.Infrastructure.EventStores
{
    public class EnventStoreInMemory : IEventStore
    {
        readonly ConcurrentBag<INotification> Notifications;
        public EnventStoreInMemory()
        {
            Notifications = new();
        }
        public Task<IEnumerable<INotification>> ExtractNotifications()
        {
            var notifications = Notifications.ToArray();
            Notifications.Clear();
            return Task.FromResult(notifications.AsEnumerable());
        }
        public Task Save<TId>(AggregateRoot<TId> aggregate)
        {
            foreach (var notification in aggregate.GetEvents())
            {
                Notifications.Add(notification);
            }
            aggregate.ClearEvents();
            return Task.CompletedTask;
        }
    }
}
