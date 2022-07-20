using BunkerGame.Domain.GameSessions;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameSessions.CommandHandlers
{
    public class ChangeMaxCharacterSizeHandler : GameSessionCommandHandlerBase<Commands.ChangeMaxCharacterSize>
    {
        public ChangeMaxCharacterSizeHandler(IGameSessionRepository gameSessionRepository, IEventStore eventStore) : base(gameSessionRepository, eventStore)
        {
        }

        public override async Task<Unit> Handle(Commands.ChangeMaxCharacterSize request, CancellationToken cancellationToken)
        {
            var gameSession = await GetGameSession(request.GameSessionId);
            gameSession.ChangeMaxCharactersInGame(request.CharacterCount);
            await SaveEvents(gameSession);
            return Unit.Value;
        }
    }
}
