using BunkerGame.Domain.GameSessions;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class FreeSeatsChangedHandler : EventHandlerBase<Events.FreeSeatsChanged>
    {
        public FreeSeatsChangedHandler(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task Handle(Events.FreeSeatsChanged notification, CancellationToken cancellationToken)
        {
            await Notify(notification.GameSessionId, "Количество мест в бункере: " + notification.SizeCount);
        }
    }
}
