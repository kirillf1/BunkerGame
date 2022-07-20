using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class AddExternalSurroundingHandler : GameSessionCommandHandlerBase<Commands.AddExternalSurrounding>
    {
        public AddExternalSurroundingHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public override async Task<Unit> Handle(Commands.AddExternalSurrounding request, CancellationToken cancellationToken)
        {
            await Change(request.GameSessionId, (g) => g.AddExternalSurrounding(request.ExternalSurrounding));
            return Unit.Value;
        }
    }
}
