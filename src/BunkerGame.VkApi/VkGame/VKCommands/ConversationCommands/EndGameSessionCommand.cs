using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class EndGameSessionCommand : ConversationCommandBase
    {
        public EndGameSessionCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            await conversationService.HandleCommand(peerId, (id) => new Commands.EndGame(id));
            var keyboard = VkKeyboardFactory.CreateStartGameButtons();
            await SendVkMessage("Игра завершена!", peerId, keyboard);
            await conversationService.AddLastUsedKeyboard(peerId, keyboard);
            return true;
        }
    }
}
