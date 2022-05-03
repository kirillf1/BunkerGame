using BunkerGame.Application.Characters.UserCard.Notifications;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CardUsedNotificationHandler : INotificationHandler<CardUsedNotificationMessage>
    {
        private readonly IVkApi vkApi;
        private readonly IConversationRepository conversationRepository;

        public CardUsedNotificationHandler(IVkApi vkApi, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
        }
        public async Task Handle(CardUsedNotificationMessage notification, CancellationToken cancellationToken)
        {
            var conversation = await conversationRepository.GetConversation(notification.GameSessionId);
            if (conversation == null)
                throw new ArgumentNullException(nameof(conversation));
            var usedCardCharacter = conversation!.Users.Find(c => c.UserId == notification.UsedCardCharacter!.PlayerId);
            var targetUser = notification.TargetPlayerId.HasValue ? conversation.Users.Find(u => u.UserId == notification.TargetPlayerId) : null;
            var text = $"&#128266; Игрок {usedCardCharacter!.UserName} использовал карту {notification.CardDescription}" +
                (targetUser != null ? $" на игрока {targetUser!.UserName}" : "");
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams(text, notification.GameSessionId));
        }
    }
}
