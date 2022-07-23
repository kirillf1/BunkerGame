using BunkerGame.Framework;
using MediatR;
using System.Collections.Concurrent;

namespace BunkerGame.VkApi.Infrastructure.EventStores
{
    public class EnventStoreInMemory : IEventStore
    {
        readonly ConcurrentBag<INotification> Notifications;
        private object syncObj = new object();
        public EnventStoreInMemory()
        {
            Notifications = new();
            
        }
        public async Task<IEnumerable<INotification>> ExtractNotifications()
        {
            return await Task.Run(() =>
             {
                 var notifications = Notifications.ToArray();
                 Notifications.Clear();
                 return notifications;

             });
        }
        public Task Save<TId>(AggregateRoot<TId> aggregate)
        {
            lock (syncObj)
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
}
