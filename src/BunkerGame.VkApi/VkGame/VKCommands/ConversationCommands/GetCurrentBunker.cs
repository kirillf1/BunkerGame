using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands
{
    public class GetCurrentBunker : ConversationCommandBase
    {
        private readonly IGameSessionRepository gameSessionRepository;
        public GetCurrentBunker(IVkApi vkApi, ConversationService conversationService,IGameSessionRepository gameSessionRepository) 
            : base(vkApi, conversationService)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public async override Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId!.Value;
            var conversation = await IsValidConversation(peerId);
            if (conversation == null)
                return false;
            var gameSession = await gameSessionRepository.GetGameSession(conversation.GameSessionId);
            await SendVkMessage(GameComponentsConventer.ConvertBunker(gameSession.Bunker), peerId);
            return true;
        }
    }
}
