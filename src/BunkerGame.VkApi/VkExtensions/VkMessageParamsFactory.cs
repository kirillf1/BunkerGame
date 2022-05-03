using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace BunkerGame.VkApi.VkExtensions
{
    public static class VkMessageParamsFactory
    {
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
