using BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class BunkerComponentChangedNotificationHandler<T> : INotificationHandler<BunkerComponentChangedNotification<T>> where T : BunkerComponent
    {
        private readonly IVkApi vkApi;

        public BunkerComponentChangedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(BunkerComponentChangedNotification<T> notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Компонент бункера изменился: " +
                notification.Component.ToString(), notification.GameSessionId));
        }
    }
}
