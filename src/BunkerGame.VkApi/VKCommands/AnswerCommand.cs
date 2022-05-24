using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands
{
    public class AnswerCommand : VkCommand
    {
        public AnswerCommand(IVkApi vkApi) : base(vkApi)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var isConversation = message.PeerId > 2000000000;
            var text = message.Text;
            if (text.Contains("правила", StringComparison.OrdinalIgnoreCase))
            {
                await SendVkMessage("Пока не кайф писать правила", isConversation ? message.PeerId!.Value : message.FromId!.Value);
                return true;
            }
            else
            {
                if (!isConversation)
                    await SendVkMessage("Посмотри на мои кнопки, это мои возможности", message.FromId!.Value,
                        VkKeyboardFactory.BuildPersonalButtons());
                return true;
            }
        }
    }
}
