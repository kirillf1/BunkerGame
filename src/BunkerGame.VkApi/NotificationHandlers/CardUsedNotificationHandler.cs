using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Domain.Players;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CardUsedNotificationHandler : INotificationHandler<CardUsedNotificationMessage>
    {
        private readonly IVkApi vkApi;
        private readonly IPlayerRepository playerRepository;

        public CardUsedNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository)
        {
            this.vkApi = vkApi;
            this.playerRepository = playerRepository;
        }
        public async Task Handle(CardUsedNotificationMessage notification, CancellationToken cancellationToken)
        {
            var cardUserPlayer = await playerRepository.GetPlayerByCharacterId(notification.UsedCardCharacter.Id);
            if (cardUserPlayer == null)
                throw new ArgumentNullException(nameof(cardUserPlayer));

            var targetPlayer =  notification.TargetCharacterId.HasValue
                ? await playerRepository.GetPlayerByCharacterId(notification.TargetCharacterId.Value)
                : default;
            var text = $"&#128266; Игрок {cardUserPlayer.FirstName} {cardUserPlayer.LastName} использовал карту {notification.CardDescription}" +
                (targetPlayer != default ? $" на игрока {targetPlayer.FirstName} {targetPlayer.LastName}" : "");
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams(text, notification.GameSessionId));
        }
    }
}
