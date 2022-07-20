using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.GameResults;
using BunkerGame.VkApi.VkGame.GameSessions.ResultCounters;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class GameEndedHandler : EventHandlerBase<Events.GameEnded>
    {
        private readonly ResultCounterService resultCounterService;
        private readonly GameResultService gameResultService;

        public GameEndedHandler(IVkApi vkApi, IConversationRepository conversationRepository,
            ResultCounterService resultCounterService,GameResultService gameResultService) : base(vkApi, conversationRepository)
        {
            this.resultCounterService = resultCounterService;
            this.gameResultService = gameResultService;
        }

        public override async Task Handle(Events.GameEnded notification, CancellationToken cancellationToken)
        {
            var result = await resultCounterService.CalculateResult(notification.GameSessionId);
            await Notify(notification.GameSessionId, $"игра окончена! {Environment.NewLine}" + result.GameReport);
            IRequest gameResultChangedCommand;
            if (result.IsWinGame)
                gameResultChangedCommand = new Domain.GameResults.Commands.AddWinGame(notification.GameSessionId);
            else
                gameResultChangedCommand = new Domain.GameResults.Commands.AddLoseGame(notification.GameSessionId);
            await gameResultService.Handle(gameResultChangedCommand);
        }
    }
}
