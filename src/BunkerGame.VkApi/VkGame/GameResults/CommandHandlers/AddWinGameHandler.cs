using BunkerGame.Domain.GameResults;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameResults.CommandHandlers
{
    public class AddWinGameHandler : IRequestHandler<Commands.AddWinGame>
    {
        private readonly IGameResultRepository gameResultRepository;

        public AddWinGameHandler(IGameResultRepository gameResultRepository)
        {
            this.gameResultRepository = gameResultRepository;
        }

        public async Task<Unit> Handle(Commands.AddWinGame request, CancellationToken cancellationToken)
        {
            var gameResult = await gameResultRepository.GetGameResult(request.GameSessionId);
            if (gameResult == null) 
            {
                gameResult = new GameResult(request.GameSessionId, "unknown" + request.GameSessionId.Value.ToString());
                await gameResultRepository.AddGameResult(gameResult);
            }
            gameResult.WinGame();
            return Unit.Value;
        }
    }
}
