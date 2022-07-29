using BunkerGame.Domain.Players;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VkGameServices.ActionServices
{
    public class KickFromConversationService
    {
        private readonly IConversationRepository conversationRepository;
        private readonly ILogger<KickFromConversationService> logger;

        public KickFromConversationService(IConversationRepository conversationRepository,
            ILogger<KickFromConversationService> logger)
        {
            this.conversationRepository = conversationRepository;
            this.logger = logger;
        }
        public async Task KickFromConversation(long peerId, long userId)
        {
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
                return;
            var user = conversation.Users.FirstOrDefault(c => c.UserId == userId);
            if (user != null)
            {
                conversation.Users.Remove(user);
                logger.LogInformation("User with id {id} kicked in conversation with id {conversationId}", userId, peerId);
                await conversationRepository.UpdateConversation(conversation);
            }
        }
    }
}
