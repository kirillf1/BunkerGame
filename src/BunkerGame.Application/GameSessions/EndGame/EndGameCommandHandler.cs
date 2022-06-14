using BunkerGame.Application.GameSessions.ResultCounters;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using MediatR;

namespace BunkerGame.Application.GameSessions.EndGame
{
    public class EndGameCommandHandler : IRequestHandler<EndGameCommand,ResultGameReport>
    {
        private readonly IResultCounterFactory resultCounterFactory;
        private readonly IGameSessionRepository gameSessionRepository;
        private readonly IGameResultRepository gameResultRepository;


        public EndGameCommandHandler(IResultCounterFactory resultCounterFactory, IGameSessionRepository gameSessionRepository,
            IGameResultRepository gameResultRepository)
        {
            this.resultCounterFactory = resultCounterFactory;
            this.gameSessionRepository = gameSessionRepository;
            this.gameResultRepository = gameResultRepository;
        }
        public async Task<ResultGameReport> Handle(EndGameCommand command, CancellationToken cancellationToken)
        {
            var gameSession = await gameSessionRepository.GetGameSession(command.GameSessionId);
            if (gameSession == null)
                throw new ArgumentNullException(nameof(gameSession));
            var resultCounter = resultCounterFactory.CreateResultCounter(gameSession);
            var result = await gameSession.EndGame(resultCounter);
            var gameResult = await gameResultRepository.GetGameResult(gameSession.Id);
            if (gameResult == null)
            {
                gameResult = new GameResult(gameSession.Id, gameSession.GameName);
                await gameResultRepository.AddGameResult(gameResult);
            }
            if (result.IsWinGame)
                gameResult.WinGames++;
            else
                gameResult.LostGames++;
            await gameSessionRepository.RemoveGameSession(gameSession);
            await gameResultRepository.CommitChanges();
             
            return result;
        }
    }
}
