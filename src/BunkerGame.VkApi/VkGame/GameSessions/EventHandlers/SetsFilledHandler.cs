using BunkerGame.Domain.GameSessions;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class SetsFilledHandler : EventHandlerBase<Events.SeatsFilled>
    {
        public SetsFilledHandler(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task Handle(Events.SeatsFilled notification, CancellationToken cancellationToken)
        {
            await Notify(notification.GameSessionId, "Бункер полностью заполнен игроками, необходимо закончить игру!");
        }
    }
}
