using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VkGameServices.ActionServices
{
    public class InvitedInConversationService
    {
        private readonly IVkApi vkApi;
        private readonly ILogger<InvitedInConversationService> logger;
        private readonly IConversationRepository conversationRepository;

        public InvitedInConversationService(IVkApi vkApi, ILogger<InvitedInConversationService> logger, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.logger = logger;
            this.conversationRepository = conversationRepository;
        }
        public async Task SendGreeting(long peerId)
        {
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation != null)
                return;
            await vkApi.SendVKMessage("Привет, я бот для игры в бункер. Отправляю свои команды. Также дайте права администратора в беседе, чтоб я мог создать игру",peerId, VkKeyboardFactory.CreateStartGameButtons());
            logger.LogInformation("Bot added in conversation with id {id}", peerId);
            
        }
    }
}
