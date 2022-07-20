using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class ChangeDifficultyHandler : GameSessionCommandHandlerBase<Commands.ChangeDifficulty>
    {
        public ChangeDifficultyHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public override async Task<Unit> Handle(Commands.ChangeDifficulty request, CancellationToken cancellationToken)
        {
            await Change(request.GameSessionId, (g) => g.ChangeDifficulty(request.Difficulty));
            return Unit.Value;
        }
    }
}
