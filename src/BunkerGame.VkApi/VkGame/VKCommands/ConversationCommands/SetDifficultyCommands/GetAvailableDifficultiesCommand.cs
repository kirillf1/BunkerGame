using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetDifficultyCommands
{
    public class GetAvailableDifficultiesCommand : DifficultyCommand
    {
        public GetAvailableDifficultiesCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.PeerId.HasValue)
                return false;
            var peerId = message.PeerId.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var keyboard = VkKeyboardFactory.CreateOptionsButtoms(Difficulties.Select(c => c.Key).ToList(), "!сложность: ");
            var currentDifficultyString = Difficulties.First(c => c.Value == conversation.Difficulty).Key;
            await SendVkMessage($"Выберете сложность (текущая: {currentDifficultyString})", peerId, keyboard);
            await conversationService.AddLastUsedKeyboard(conversation, keyboard);
            return true;
        }
    }
}
