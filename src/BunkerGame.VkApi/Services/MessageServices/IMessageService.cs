using VkNet.Model;

namespace BunkerGame.VkApi.Services.MessageServices
{
    public interface IMessageService
    {
        public Task SendMessage(Message message);
    }
}
