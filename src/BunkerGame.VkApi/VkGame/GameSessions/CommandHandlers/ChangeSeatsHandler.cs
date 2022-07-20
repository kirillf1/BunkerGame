using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class ChangeSeatsHandler : GameSessionCommandHandlerBase<Commands.AddSeats>, IRequestHandler<Commands.RemoveSeats>
    {
        public ChangeSeatsHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public async Task<Unit> Handle(Commands.RemoveSeats request, CancellationToken cancellationToken)
        {
            await Change(request.GameSessionId, (g) => g.RemoveFreePlace());
            return Unit.Value;
        }

        public override async Task<Unit> Handle(Commands.AddSeats request, CancellationToken cancellationToken)
        {
            await Change(request.GameSessionId, (g) => g.AddFreePlace());
            return Unit.Value;
        }
    }
}
