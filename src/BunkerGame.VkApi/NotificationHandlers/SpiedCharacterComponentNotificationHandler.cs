using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Players;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class SpiedCharacterComponentNotificationHandler : INotificationHandler<SpiedCharacterComponentNotification>
    {
        private readonly IVkApi vkApi;
        private readonly IPlayerRepository playerRepository;

        public SpiedCharacterComponentNotificationHandler(IVkApi vkApi, IPlayerRepository playerRepository)
        {
            this.vkApi = vkApi;
            this.playerRepository = playerRepository;
        }

        public async Task Handle(SpiedCharacterComponentNotification notification, CancellationToken cancellationToken)
        {
            var description = GetCharacterComponentDescription(notification.CharacterComponent);
            var player = await playerRepository.GetPlayerByCharacterId(notification.CharacterId);
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams($"Характеристика игрока: {Environment.NewLine}" +
                description,
                notification.GameSessionId, VkKeyboardFactory.BuildConversationButtons(false)));
        }
        private string GetCharacterComponentDescription(CharacterComponent characterComponent)
        {
            if (characterComponent is CharacterEntity entity)
                return entity.Description;
            else if (characterComponent is Sex sex)
                return CharacterComponentStringConventer.ConvertSex(sex);
            else if (characterComponent is Childbearing childbearing)
                return CharacterComponentStringConventer.ConvertChildbearing(childbearing);
            else if (characterComponent is Age age)
                return CharacterComponentStringConventer.ConvertAge(age);
            else if (characterComponent is Size size)
                return CharacterComponentStringConventer.ConvertSize(size);
            else
                throw new ArgumentException(nameof(CharacterComponent));
        }
    }
}
