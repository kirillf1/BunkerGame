using BunkerGame.Domain.Players;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VkGameServices.ActionServices
{
    public class UpdateConversationNameService
    {
        private readonly IConversationRepository conversationRepository;
        private readonly ILogger<UpdateConversationNameService> logger;

        public UpdateConversationNameService(IConversationRepository conversationRepository, ILogger<UpdateConversationNameService> logger)
        {
            this.conversationRepository = conversationRepository;
            this.logger = logger;
        }
        public async Task UpdateName(string newName, long peerId)
        {
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
                return;
            conversation.ConversationName = newName;
            await conversationRepository.UpdateConversation(conversation);
            logger.LogInformation("Conversation with id {id} changed name to {newName}", peerId, newName);
        }
    }
}
