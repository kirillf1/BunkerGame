using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class ChangeDifficultyCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;

        public ChangeDifficultyCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var messageText = message.Text;
            var peerId = message.PeerId!.Value;
            if (messageText.Contains("установить сложность", StringComparison.OrdinalIgnoreCase))
            {
                var keyboard = VkKeyboardFactory.BuildOptionsButtoms(new List<string> { "легкая", "средняя", "сложная" }, "!сложность: ");
                await SendVkMessage("Выберете сложность", peerId, keyboard);
                return true;
            }
            else if (messageText.Contains("сложность:", StringComparison.OrdinalIgnoreCase))
            {
                var difficultyString = Regex.Match(messageText, "легкая|средняя|сложная", RegexOptions.IgnoreCase);
                var difficulty = GetDifficultyFromString(difficultyString.Value);
                if (!difficulty.HasValue)
                {
                    await SendVkMessage("Введите сложность правильно", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                    return true;
                }
                var conversation = await conversationRepository.GetConversation(peerId);
                if (conversation == null)
                {
                    conversation = await ConversationRepositories.Conversation.CreateConversation(vkApi, peerId);
                    conversation.Difficulty = difficulty.Value;
                    await conversationRepository.AddConversation(conversation);
                }
                else
                {
                    conversation.Difficulty = difficulty.Value;
                    await conversationRepository.UpdateConversation(conversation);
                }
                await SendVkMessage("Сложность установлена!", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return true;
            }
            return false;
        }
        private static Difficulty? GetDifficultyFromString(string text)
        {
            if (text.Contains("легкая", StringComparison.OrdinalIgnoreCase))
            {
                return Difficulty.Easy;
            }
            else if (text.Contains("средняя", StringComparison.OrdinalIgnoreCase))
            {
                return Difficulty.Medium;
            }
            else if (text.Contains("сложная", StringComparison.OrdinalIgnoreCase))
            {
                return Difficulty.Hard;
            }
            return null;
        }
    }
}
