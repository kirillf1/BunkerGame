using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.Players;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class ChangeCharactersCountCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;

        public ChangeCharactersCountCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var messageText = message.Text;
            if (messageText.Contains("количество игроков", StringComparison.OrdinalIgnoreCase))
            {
                var keyboard = VkKeyboardFactory.BuildOptionsButtoms(new List<string> { "6", "7", "8", "9", "10", "11", "12" }, "!Игроков: ");
                await SendVkMessage("Установите количество игроков", peerId, keyboard);
            }
            else if (messageText.Contains("Игроков:", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(messageText, @"\d+");
                if (!match.Success || !byte.TryParse(match.Value,out var charactersCount))
                {
                    await SendVkMessage("Введите количество правильно", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                    return true;
                }
                var conversation = await conversationRepository.GetConversation(peerId);
                if (conversation == null)
                {
                    conversation = await ConversationRepositories.Conversation.CreateConversation(vkApi, peerId);
                    conversation.PlayersCount = charactersCount;
                    await conversationRepository.AddConversation(conversation);
                }
                else
                {
                    conversation.PlayersCount = charactersCount;
                    await conversationRepository.UpdateConversation(conversation);
                }
                await SendVkMessage("Количество игроков установлено", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return true;
            }
            return false;
        }
    }
}
