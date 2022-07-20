using BunkerGame.Domain.GameResults;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameResults.CommandHandlers
{
    public class AddLoseGameHandler : IRequestHandler<Commands.AddLoseGame>
    {
        private readonly IGameResultRepository gameResultRepository;
        public AddLoseGameHandler(IGameResultRepository gameResultRepository)
        {
            this.gameResultRepository = gameResultRepository;
        }
        public async Task<Unit> Handle(Commands.AddLoseGame request, CancellationToken cancellationToken)
        {
            var gameResult = await gameResultRepository.GetGameResult(request.GameSessionId);
            if (gameResult == null)
            {
                gameResult = new GameResult(request.GameSessionId, "unknown" + request.GameSessionId.Value.ToString());
                await gameResultRepository.AddGameResult(gameResult);
            }
            gameResult.LoseGame();
            return Unit.Value;
        }
    }
}
