using BunkerGame.Application.Characters.ChangeCharacteristic;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class CharacterUpdatedNotificationHandler : INotificationHandler<CharacterUpdatedNotificationMessage>
    {
        private readonly IVkApi vkApi;

        public CharacterUpdatedNotificationHandler(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }

        public async Task Handle(CharacterUpdatedNotificationMessage notification, CancellationToken cancellationToken)
        {
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams("Ваш персонаж изменился:" +
                $" {GameComponentsConventer.ConvertCharacter(notification.Character)}", notification.Character.PlayerId!.Value));
        }
    }
}
