using BunkerGame.Application.GameSessions.ChangeFreePlace;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class BunkerSizeChangedNotificationHandler : INotificationHandler<BunkerSizeChangedNotification>
    {
        private readonly IVkApi vkApi;

        public BunkerSizeChangedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(BunkerSizeChangedNotification notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams($"Количество мест в бункере изменилось на {notification.CurrentSize}",
                notification.GameSessionId));
        }
    }
}
