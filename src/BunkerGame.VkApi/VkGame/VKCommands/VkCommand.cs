using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame.VKCommands
{
    public abstract class VkCommand
    {
        protected readonly IVkApi vkApi;

        public abstract Task<bool> SendAsync(Message message);
        protected VkCommand(IVkApi vkApi)
        {
            this.vkApi = vkApi;
        }
        protected async Task<long> SendVkMessage(string text, long peerId, MessageKeyboard? keyboard = null)
        {
            return await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams(text, peerId, keyboard));
        }

    }
}
