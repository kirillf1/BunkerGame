using BunkerGame.Application.Players.AddNewPlayers;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using MediatR;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.SetDifficultyCommands
{
    public class ChangeDifficultyCommand : DifficultyCommand
    {
        public ChangeDifficultyCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.PeerId.HasValue)
                return false;
            var peerId = message.PeerId.Value;
            var difficulty = GetDifficultyFromString(message.Text);
            if (!difficulty.HasValue)
            {
                await SendVkMessage("Введите сложность правильно", peerId, VkKeyboardFactory.BuildConversationButtons(false));
                return true;
            }
            var conversation = await GetOrCreateConversation(peerId);
            conversation.Difficulty = difficulty.Value;
            await conversationRepository.UpdateConversation(conversation);
            await SendVkMessage("Сложность установлена!", peerId, VkKeyboardFactory.BuildConversationButtons(false));
            return true;
        }

    }
}
