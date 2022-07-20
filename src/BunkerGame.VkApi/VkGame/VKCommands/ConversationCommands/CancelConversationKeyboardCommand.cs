using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class CancelConversationKeyboardCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;

        public CancelConversationKeyboardCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var isConversation = message.PeerId > 2000000000;
            if (!isConversation)
                return false;
            var conversation = await conversationRepository.GetConversation(message.PeerId!.Value);
            var previousKeyboard = conversation?.GetPreviosKeyboard() ?? VkKeyboardFactory.CreateStartGameButtons();
            await SendVkMessage("Переключаю меню", message.PeerId.Value, previousKeyboard);
            return false;
        }
    }
}
