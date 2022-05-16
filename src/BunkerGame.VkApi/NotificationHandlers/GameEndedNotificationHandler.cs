using BunkerGame.Application.GameSessions.EmptyFreePlaceEvent;
using BunkerGame.Application.GameSessions.EndGame;
using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VKCommands;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace BunkerGame.VkApi.NotificationHandlers
{
    public class GameEndedNotificationHandler : INotificationHandler<EmptyFreePlaceNotificationMessage>
    {
        private readonly IVkApi vkApi;
        private readonly IRequestHandler<EndGameCommand, ResultGameReport> requestHandler;

        public GameEndedNotificationHandler(IVkApi vkApi, IRequestHandler<EndGameCommand, ResultGameReport> requestHandler)
        {
            this.vkApi = vkApi;
            this.requestHandler = requestHandler;
        }
        public async Task Handle(EmptyFreePlaceNotificationMessage notification, CancellationToken cancellationToken)
        {
            var messageSendTask = vkApi.Messages.SendAsync(VkMessageParamsFactory
                .CreateMessageSendParams("Игра закончилась, т.к. нет лишних мест в бункере", notification.GameSessionId));
            var gameResultTask = requestHandler.Handle(new EndGameCommand(notification.GameSessionId),cancellationToken);
            await Task.WhenAll(messageSendTask, gameResultTask);
            await vkApi.Messages.SendAsync(VkMessageParamsFactory.CreateMessageSendParams(gameResultTask.Result.GameReport, notification.GameSessionId, VkKeyboardFactory.BuildConversationButtons(false)));
            
        }
    }
}
