using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public abstract class ConversationCommandBase : VkCommand
    {
        protected readonly ConversationService conversationService;
        protected ConversationCommandBase(IVkApi vkApi, ConversationService conversationService) : base(vkApi)
        {
            this.conversationService = conversationService;
        }
        protected async Task<Conversation?> IsValidConversation(long peerId)
        {
            return await TryGetConversation(peerId);
        }
        private async Task<Conversation?> TryGetConversation(long peerId)
        {
            try
            {
                return await conversationService.GetConversation(peerId);
            }
            catch
            {
                await SendVkMessage("Игра не создана", peerId, VkKeyboardFactory.CreateStartGameButtons());
                return null;
            }
        }
    }
}
