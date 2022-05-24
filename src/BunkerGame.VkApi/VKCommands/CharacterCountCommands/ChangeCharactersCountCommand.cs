using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.Players;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.CharacterCountCommands
{
    public class ChangeCharactersCountCommand : CharacterCountCommand
    {
        public ChangeCharactersCountCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
         
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.PeerId.HasValue)
                return false;
            var peerId = message.PeerId!.Value;
            var messageText = message.Text;
            var match = Regex.Match(messageText, @"\d+");
            if (!match.Success || !byte.TryParse(match.Value, out var charactersCount))
            {
                await SendVkMessage("Введите количество правильно", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return false;
            }
            var conversation = await GetOrCreateConversation(peerId);
            conversation.PlayersCount = charactersCount;
            await conversationRepository.UpdateConversation(conversation);
            await SendVkMessage("Количество игроков установлено", peerId, VkKeyboardFactory.BuildConversationButtons(false));
            return true;
        }
    }
}
