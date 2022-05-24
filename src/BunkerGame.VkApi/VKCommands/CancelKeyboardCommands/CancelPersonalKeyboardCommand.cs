using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.CancelKeyboardCommands
{
    public class CancelPersonalKeyboardCommand : VkCommand
    {
        public CancelPersonalKeyboardCommand(IVkApi vkApi) : base(vkApi)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var isConversation = message.PeerId > 2000000000;
            if (isConversation)
                return false;
            await SendVkMessage("Переключаю меню", message.FromId!.Value,VkKeyboardFactory.BuildPersonalButtons());
            return true;
        }
    }
}
