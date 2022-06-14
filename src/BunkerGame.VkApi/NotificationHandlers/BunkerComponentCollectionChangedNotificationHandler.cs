using BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class BunkerComponentCollectionChangedNotificationHandler<T> : INotificationHandler<BunkerComponentCollectionChangedNotification<T>> where T : BunkerComponent
    {
        private readonly IVkApi vkApi;

        public BunkerComponentCollectionChangedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(BunkerComponentCollectionChangedNotification<T> notification, CancellationToken cancellationToken)
        {
            string message;
            if (typeof(T) == typeof(ItemBunker))
               message = BunkerComponentsStringConventer.ConvertBunkerItems((IEnumerable<ItemBunker>)notification.Components);
            else if (typeof(T) == typeof(BunkerObject))
                message = BunkerComponentsStringConventer.ConvertBunkerObjects((IEnumerable<BunkerObject>)notification.Components);
            else
                message = String.Join(Environment.NewLine, notification.Components);
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Компоненты бункера изменились: " +
                 message, notification.GameSessionId));
        }
    }
}
