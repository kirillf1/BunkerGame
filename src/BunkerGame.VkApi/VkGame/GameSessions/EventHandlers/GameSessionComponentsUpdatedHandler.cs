using BunkerGame.Domain.GameSessions;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class GameSessionComponentsUpdatedHandler : EventHandlerBase<Events.BunkerUpdated>,
        INotificationHandler<Events.CatastropheChanged>, INotificationHandler<Events.ExternalSurroundigAdded>
    {
        public GameSessionComponentsUpdatedHandler(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task Handle(Events.BunkerUpdated notification, CancellationToken cancellationToken)
        {
            var text = $"Ваш бункер: {Environment.NewLine}" + GameComponentsConventer.ConvertBunker(notification.Bunker);
            await Notify(notification.GameSessionId, text);
        }

        public async Task Handle(Events.CatastropheChanged notification, CancellationToken cancellationToken)
        {
            var text = $"Ваш катаклизм: {Environment.NewLine}" + GameComponentsConventer.ConvertCatastrophe(notification.Catastrophe);
            await Notify(notification.GameSessionId, text);
        }

        public async Task Handle(Events.ExternalSurroundigAdded notification, CancellationToken cancellationToken)
        {
            var text = notification.ExternalSurrounding.Description;
            await Notify(notification.GameSessionId, text);
        }
    }
}
