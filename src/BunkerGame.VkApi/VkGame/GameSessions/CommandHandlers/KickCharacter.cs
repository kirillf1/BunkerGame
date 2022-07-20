using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class KickCharacter : GameSessionCommandHandlerBase<Commands.KickCharacter>
    {
        public KickCharacter(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public override async Task<Unit> Handle(Commands.KickCharacter request, CancellationToken cancellationToken)
        {
            await Change(request.GameSessionId, (g) => g.KickCharacter(request.CharacterId));
            return Unit.Value;
        }
    }
}
