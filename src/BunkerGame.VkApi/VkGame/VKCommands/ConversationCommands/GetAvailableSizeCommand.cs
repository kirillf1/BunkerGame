using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class GetAvailableSizeCommand : ConversationCommandBase
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public GetAvailableSizeCommand(IVkApi vkApi, IGameSessionRepository gameSessionRepository, ConversationService conversationService) : base(vkApi, conversationService)
        {
            this.gameSessionRepository = gameSessionRepository;
        }
        // если не создана отправлять кнопку на пересоздание игры
        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
            {
                return true;
            }
            var gameSession = await gameSessionRepository.GetGameSession(conversation.GameSessionId);
            var seats = gameSession.FreePlaceSize.GetAvailableSeats();
            await SendVkMessage($"Бункер может вместить: {seats} игроков!", peerId);
            return true;
        }

    }
}
