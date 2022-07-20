using VkNet.Abstractions;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace BunkerGame.VkApi.VkGame.VkExtensions
{
    public static class VkMessageParamsFactory
    {
        public static async Task SendVKMessage(this IVkApi vkApi, string text, long peerId, MessageKeyboard? keyboard = null)
        {
            await vkApi.Messages.SendAsync(CreateMessageSendParams(text, peerId, keyboard));
        }
        public static MessagesSendParams CreateMessageSendParams(string text, long peerId, MessageKeyboard? keyboard = null)
        {
            MessagesSendParams mParams = new MessagesSendParams();
            mParams.Message = text;
            mParams.PeerId = peerId;
            mParams.Keyboard = keyboard;
            mParams.RandomId = DateTime.Now.Millisecond;
            return mParams;
        }
    }
}
