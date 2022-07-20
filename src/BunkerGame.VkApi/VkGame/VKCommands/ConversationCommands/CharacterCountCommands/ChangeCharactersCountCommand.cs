using BunkerGame.VkApi.VkGame.VkGameServices;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.CharacterCountCommands
{
    public class ChangeCharactersCountCommand : ConversationCommandBase
    {
        public ChangeCharactersCountCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.PeerId.HasValue)
                return false;
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var messageText = message.Text;
            var match = Regex.Match(messageText, @"\d+");
            if (!match.Success || !byte.TryParse(match.Value, out var charactersCount))
            {
                await SendVkMessage("Введите количество правильно", peerId);
                return false;
            }
            await TryChangeCharactersCount(conversation.ConversationId, charactersCount);
            return true;
        }
        private async Task TryChangeCharactersCount(long conversationId, byte count)
        {
            try
            {
                await conversationService.ChangePlayersCount(conversationId, count);
            }
            catch
            {
                await SendVkMessage("Количество игроков должно быть от 5 до 13 ", conversationId);
            }
        }
    }
}
