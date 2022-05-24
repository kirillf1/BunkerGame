using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.SetTargetConversationCommands
{
    public class SetTargetConversationCommand : VkCommand
    {
        private readonly IUserOptionsService userOptionsService;
        private readonly IConversationRepository conversationRepository;

        public SetTargetConversationCommand(IVkApi vkApi, IUserOptionsService userOptionsService, IConversationRepository conversationRepository) : base(vkApi)
        {
            this.userOptionsService = userOptionsService;
            this.conversationRepository = conversationRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var text = message.Text;
            var userId = message.FromId.Value;
            var conversationName = text.Replace("Беседа: ", "", StringComparison.OrdinalIgnoreCase);
            var conversation = await conversationRepository.GetConversation(conversationName);
            if (conversation == null)
            {
                await SendVkMessage("Не удалось найти такую беседу, введите имя беседы правильно", userId);
            }
            else
            {
                await userOptionsService.SetCurrentGame(conversation.ConversationId, userId);
                await SendVkMessage($"Теперь вы играете в беседе {conversation.ConversationName}", userId);
            }
            return true;
        }
    }
}
