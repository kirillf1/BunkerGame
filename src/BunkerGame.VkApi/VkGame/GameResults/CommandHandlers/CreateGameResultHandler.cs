using BunkerGame.Domain.GameResults;
using MediatR;

namespace BunkerGame.VkApi.VkGame.GameResults.CommandHandlers
{
    public class CreateGameResultHandler : IRequestHandler<Commands.CreateGameResult>
    {
        private readonly IGameResultRepository gameResultRepository;

        public CreateGameResultHandler(IGameResultRepository gameResultRepository)
        {
            this.gameResultRepository = gameResultRepository;
        }
        public async Task<Unit> Handle(Commands.CreateGameResult request, CancellationToken cancellationToken)
        {
            await gameResultRepository.AddGameResult(new GameResult(request.GameSessionId, request.Name));
            return Unit.Value;
        }
    }
}
