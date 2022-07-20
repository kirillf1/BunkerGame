using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetDifficultyCommands
{
    public class ChangeDifficultyCommand : DifficultyCommand
    {
        public ChangeDifficultyCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
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
            var difficulty = GetDifficultyFromString(message.Text);
            if (!difficulty.HasValue)
            {
                await SendVkMessage("Введите сложность правильно", peerId);
                return true;
            }
            await conversationService.ChangeDifficulty(conversation.ConversationId, difficulty.Value);
            return true;
        }

    }
}
