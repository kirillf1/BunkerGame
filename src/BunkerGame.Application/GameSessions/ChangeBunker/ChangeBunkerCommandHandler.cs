using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.GameSessions.ChangeBunker
{
    public class ChangeBunkerCommandHandler : IRequestHandler<ChangeBunkerCommand, Bunker>
    {
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IBunkerFactory bunkerFactory;

        public ChangeBunkerCommandHandler(IGameSessionRepository gameSessionRepository,IBunkerFactory bunkerFactory)
        {
            this.gameSessionRepository = gameSessionRepository;
            this.bunkerFactory = bunkerFactory;
        }
        public async Task<Bunker> Handle(ChangeBunkerCommand command, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSessionWithBunker(command.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            gameSession.UpdateBunker(await bunkerFactory.CreateBunker(new BunkerCreateOptions()));
            return gameSession.Bunker;
        }
    }
}
