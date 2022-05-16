using BunkerGame.Application.Characters.UserCard.Notifications;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CharacterKickedNotificationHandler : INotificationHandler<CharacterKickedNotification>
    {
        private readonly IVkApi vkApi;
        private readonly IConversationRepository conversationRepository;

        public CharacterKickedNotificationHandler(IVkApi vkApi, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
        }
        public async Task Handle(CharacterKickedNotification notification, CancellationToken cancellationToken)
        {
            var character = notification.Character;
            var conversation = await conversationRepository.GetConversation(notification.GameSessionId);
            var user = conversation!.Users.Find(c => c.UserId == character.PlayerId)!;
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams($"Игрок: {user.FirstName} исключен",
                notification.GameSessionId));
        }
    }
}
