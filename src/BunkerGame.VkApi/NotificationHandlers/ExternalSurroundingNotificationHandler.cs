using BunkerGame.Application.GameSessions.AddExternalSurroundingInGame;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class ExternalSurroundingNotificationHandler : INotificationHandler<ExternalSurroundingUpdatedNotification>
    {
        private readonly IVkApi vkApi;

        public ExternalSurroundingNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(ExternalSurroundingUpdatedNotification notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Вы узнаете, что рядом с бункером находится: " +
                $"{notification.ExternalSurrounding.Description}", notification.GameSessionId));
        }
    }
}
