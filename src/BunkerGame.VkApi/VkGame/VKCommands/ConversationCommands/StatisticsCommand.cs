using BunkerGame.Domain.GameResults;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class StatisticsCommand : ConversationCommandBase
    {
        private readonly IGameResultRepository gameResultRepository;

        public StatisticsCommand(IVkApi vkApi, ConversationService conversationService,
            IGameResultRepository gameResultRepository) : base(vkApi, conversationService)
        {
            this.gameResultRepository = gameResultRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId.GetValueOrDefault();
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var gameResult = await gameResultRepository.GetGameResult(conversation.GameSessionId);
            if (gameResult == null)
            {
                await SendVkMessage("Вы не сыграли ни одной игры", peerId);
                return true;
            }
            await SendVkMessage($"Игр всего: {gameResult.GetGamesCount()}. Выиграно {gameResult.WinGames} игр, проиграно:{gameResult.LostGames} игр", peerId);
            return true;
        }

        
    }
}
