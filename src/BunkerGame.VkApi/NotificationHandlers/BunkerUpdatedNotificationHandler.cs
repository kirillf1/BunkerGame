using BunkerGame.Application.GameSessions.ChangeBunker;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class BunkerUpdatedNotificationHandler : INotificationHandler<BunkerUpdatedNotification>
    {
        private readonly IVkApi vkApi;

        public BunkerUpdatedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(BunkerUpdatedNotification notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Бункер изменился: " +
                 $"{GameComponentsConventer.ConvertBunker(notification.Bunker)}", notification.GameSessionId));
        }
    }
}
