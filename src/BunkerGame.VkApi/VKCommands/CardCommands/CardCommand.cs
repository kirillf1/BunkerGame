using BunkerGame.Domain.Characters;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VKCommands.CardCommands
{
    public abstract class CardCommand : VkCommand
    {
        protected readonly IUserOptionsService userOptionsService;
        protected readonly ICharacterRepository characterRepository;
        protected CardCommand(IVkApi vkApi, IUserOptionsService userOptionsService, ICharacterRepository characterRepository) : base(vkApi)
        {
            this.userOptionsService = userOptionsService;
            this.characterRepository = characterRepository;
        }
        /// <summary>
        /// Get character from characterRepository. If user play in two or more games and not configured or not playing notify user about error. 
        /// </summary>
        /// <returns>If success return character else null</returns>
        protected async virtual Task<(Character,Conversation)?> TryGetCharacterWithConversation(long userId)
        {
            var conversation = await userOptionsService.GetUserGame(userId);
            if (conversation == null)
            {
                await SendVkMessage("Вы не в игре или состоите в нескольких играх, настройте конфигурацию! ", userId);
                return null;
            }
            var character = await characterRepository.GetCharacter(conversation.ConversationId, userId);
            if (character?.IsAlive != true)
            {
                await SendVkMessage("Вы не состоите в игре или вы исключены", userId);
                return null;
            }
            return (character,conversation);
        }
    }
}
