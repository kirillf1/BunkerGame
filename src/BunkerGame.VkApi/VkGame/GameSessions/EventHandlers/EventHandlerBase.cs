using BunkerGame.Domain.Shared;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public abstract class EventHandlerBase<T> : INotificationHandler<T> where T : INotification
    {
        protected readonly IVkApi vkApi;
        protected readonly IConversationRepository conversationRepository;

        protected EventHandlerBase(IVkApi vkApi, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
        }
        public abstract Task Handle(T notification, CancellationToken cancellationToken);
        protected async Task<long> SendVkMessage(string text, long peerId, MessageKeyboard? keyboard = null)
        {
            return await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams(text, peerId, keyboard));
        }
        protected async Task Notify(GameSessionId gameSessionId, string text)
        {
            var conversation = await conversationRepository.GetConversation(gameSessionId);
            if (conversation == null)
            {
                return;
            }
            var peerId = conversation.ConversationId;
            await SendVkMessage(text, peerId);
        }
    }
}
