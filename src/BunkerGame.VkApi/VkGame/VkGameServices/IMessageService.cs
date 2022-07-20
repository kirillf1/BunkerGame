using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VkGameServices
{
    public interface IMessageService
    {
        public Task SendMessage(Message message);
    }
}
