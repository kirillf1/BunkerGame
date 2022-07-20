using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class StartGameCommand : ConversationCommandBase
    {
        public StartGameCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            if (!await TryStartGame(conversation))
                return true;
            var keyboard = VkKeyboardFactory.CreateConversationButtons(true);
            await SendVkMessage("Теперь вы можете раскрывать характеристики(у бота в лс) и исключать персонажей",
                peerId, keyboard);
            await conversationService.AddLastUsedKeyboard(conversation,keyboard);
            return true;
        }
        private async Task<bool> TryStartGame(Conversation conversation)
        {
            try
            {
                await conversationService.HandleCommand(new Commands.StartGame(conversation.GameSessionId));
                return true;
            }
            catch
            {
                await SendVkMessage("Не получается начать игру, недостаточное количество игроков либо игра уже закончена", conversation.ConversationId);
                return false;
            }
        }
    }
}
