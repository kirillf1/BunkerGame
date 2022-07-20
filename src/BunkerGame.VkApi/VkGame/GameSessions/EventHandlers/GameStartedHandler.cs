using BunkerGame.Domain.GameSessions;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class GameStartedHandler : EventHandlerBase<Events.GameStarted>
    {
        public GameStartedHandler(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task Handle(Events.GameStarted notification, CancellationToken cancellationToken)
        {
            await Notify(notification.GameSession.Id, "Игра началась!");
        }

    }
}
