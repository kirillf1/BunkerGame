using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.GameSessions.CreateGameSession
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameSession>
    {
        private readonly IGameSessionFactory gameSessionFactory;
        private readonly IGameSessionRepository gameSessionRepository;

        public CreateGameCommandHandler(IGameSessionFactory gameSessionFactory,
            IGameSessionRepository gameSessionRepository)
        {
            this.gameSessionFactory = gameSessionFactory;
            this.gameSessionRepository = gameSessionRepository;
        }
        public async Task<GameSession> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            if (command.GameId.HasValue && await gameSessionRepository.GetGameSession(command.GameId!.Value, false) != null)
                throw new InvalidOperationException($"{nameof(GameSession)} is exsists. Delete gameSession if you want to recreate game");
            var gameSession = await gameSessionFactory.CreateGameSession(new GameSessionCreateOptions(command.GameName, command.PlayersCount) { GameId = command.GameId, CharactersAlive= command.CharactersAlive });

            await gameSessionRepository.AddGameSession(gameSession);
            await gameSessionRepository.CommitChanges();
            return gameSession;

        }
    }
}
