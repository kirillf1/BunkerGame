using BunkerGame.Application.Characters.UserCard.Notifications;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CatastropheUpdatedNotificationHandler : INotificationHandler<CatastropheUpdatedNotification>
    {
        private readonly IVkApi vkApi;
        public CatastropheUpdatedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(CatastropheUpdatedNotification notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Катаклизм изменился: " +
                $"{GameComponentsConventer.ConvertCatastrophe(notification.Catastrophe)}", notification.GameSessionId));
        }
    }
}
