using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.CharacterCountCommands
{
    public class GetAvailableCharactersCountCommand : ConversationCommandBase
    {
        public GetAvailableCharactersCountCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId;
            if (!peerId.HasValue)
                return false;
            var conversation = await IsValidConversation(peerId.Value);
            if (conversation == null)
                return false;
            var keyboard = VkKeyboardFactory.CreateOptionsButtoms(new List<string> {"5", "6", "7", "8", "9", "10", "11", "12" }, "!Игроков: ");
            await SendVkMessage("Установите количество игроков", peerId.Value, keyboard);
            await conversationService.AddLastUsedKeyboard(conversation, keyboard);
            return true;
        }
    }
}
