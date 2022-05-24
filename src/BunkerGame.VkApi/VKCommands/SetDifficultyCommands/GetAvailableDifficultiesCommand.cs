using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.SetDifficultyCommands
{
    public class GetAvailableDifficultiesCommand : DifficultyCommand
    {
        public GetAvailableDifficultiesCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.PeerId.HasValue)
                return false;
            var peerId = message.PeerId.Value;
            var keyboard = VkKeyboardFactory.BuildOptionsButtoms(Difficulties.Select(c=>c.Key).ToList(), "!сложность: ");
            var conversation = await GetOrCreateConversation(peerId);
            conversation.PushKeyboard(VkKeyboardFactory.BuildConversationButtons(false));
            var currentDifficultyString = Difficulties.First(c => c.Value == conversation.Difficulty).Key;
            await SendVkMessage($"Выберете сложность (текущая: {currentDifficultyString})", peerId, keyboard);
            return true;
        }
    }
}
