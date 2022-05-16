using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
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
            var text = message.Text;
            var userId = message.FromId.GetValueOrDefault();
            if (text.Contains("Выбрать игру",StringComparison.OrdinalIgnoreCase))
            {
                var conversations = await conversationRepository.GetConversationsByUserId(userId);
                if (!conversations.Any())
                {
                    await SendVkMessage("Вы не состоите не в одной игре", userId);
                    return true;
                }
                else
                {
                    await SendVkMessage("Вот ваши игры", userId,
                        VkKeyboardFactory.BuildOptionsButtoms(conversations.Select(c => c.ConversationName).ToList(), "Беседа: "));
                    return true;
                }
            }
            else if (text.Contains("Беседа: ", StringComparison.OrdinalIgnoreCase))
            {
                var conversationName = text.Replace("Беседа: ", "",StringComparison.OrdinalIgnoreCase);
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
            }
            return false;
        }
    }
}
