using BunkerGame.Domain.Shared;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.EventHandlers
{
    public abstract class CharacterEventHandlerBase<T> : INotificationHandler<T> where T : INotification
    {
        protected readonly VkSenderByCharacter vkSenderByCharacter;
        protected CharacterEventHandlerBase(VkSenderByCharacter vkSenderByCharacter)
        {
            this.vkSenderByCharacter = vkSenderByCharacter;
        }
        public abstract Task Handle(T notification, CancellationToken cancellationToken);
        protected async Task Notify(CharacterId characterId, string message)
        {
            await vkSenderByCharacter.SendMessageToCharacter(characterId, message);
        }
    }
}
