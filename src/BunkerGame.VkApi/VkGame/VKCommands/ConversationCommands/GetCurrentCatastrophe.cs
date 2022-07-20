using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class GetCurrentCatastrophe : ConversationCommandBase
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public GetCurrentCatastrophe(IVkApi vkApi, ConversationService conversationService,IGameSessionRepository gameSessionRepository) : base(vkApi, conversationService)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var gameSession = await gameSessionRepository.GetGameSession(conversation.GameSessionId);
            await SendVkMessage(GameComponentsConventer.ConvertCatastrophe(gameSession.Catastrophe), peerId);
            return true;
        }
    }
}
