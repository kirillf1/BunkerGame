using BunkerGame.Application.Characters.UserCard.Notifications;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CharactersExchangedNotifiactionHandler : INotificationHandler<CharactersExchangedNotification>
    {
        private readonly IVkApi vkApi;

        public CharactersExchangedNotifiactionHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        public async Task Handle(CharactersExchangedNotification notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Ваш персонаж изменился:" +
               $" {GameComponentsConventer.ConvertCharacter(notification.CharacterFist)}", notification.CharacterFist.PlayerId!.Value));
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Ваш персонаж изменился:" +
               $" {GameComponentsConventer.ConvertCharacter(notification.CharacterSecond)}", notification.CharacterSecond.PlayerId!.Value));
        }
    }
}
