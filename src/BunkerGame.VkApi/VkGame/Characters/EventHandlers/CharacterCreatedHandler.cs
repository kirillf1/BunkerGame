using BunkerGame.Domain.Characters;

namespace BunkerGame.VkApi.VkGame.Characters.EventHandlers
{
    public class CharacterCreatedHandler : CharacterEventHandlerBase<Events.CharacterCreated>
    {
        public CharacterCreatedHandler(VkSenderByCharacter vkSenderByCharacter) : base(vkSenderByCharacter)
        {
        }

        public override async Task Handle(Events.CharacterCreated notification, CancellationToken cancellationToken)
        {
            await vkSenderByCharacter.SendCharacter(notification.Character);
        }
    }
}
