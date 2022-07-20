using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetTargetConversationCommands
{
    public class GetUserConversationsCommand : VkCommand
    {
        private readonly IConversationRepository conversationRepository;

        public GetUserConversationsCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }

        public async override Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var userId = message.FromId.Value;
            var conversations = await conversationRepository.GetConversationsByUserId(userId);
            if (!conversations.Any())
            {
                await SendVkMessage("Вы не состоите не в одной игре", userId);
                return true;
            }
            else
            {
                await SendVkMessage("Вот ваши игры", userId,
                    VkKeyboardFactory.CreateOptionsButtoms(conversations.Select(c => c.ConversationName).ToList(), "Беседа: "));
                return true;
            }
        }
    }
}
