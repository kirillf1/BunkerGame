using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class GameSessionCreatedHandler : EventHandlerBase<Events.GameCreated>, INotificationHandler<Events.GameRestarted>
    {
        private readonly IGameSessionRepository gameSessionRepository;

        public GameSessionCreatedHandler(IVkApi vkApi, IConversationRepository conversationRepository, IGameSessionRepository gameSessionRepository) : base(vkApi, conversationRepository)
        {
            this.gameSessionRepository = gameSessionRepository;
        }

        public override async Task Handle(Events.GameCreated notification, CancellationToken cancellationToken)
        {
            await NotifyNewGame(notification.GameSessionId, true);
        }
        public async Task Handle(Events.GameRestarted notification, CancellationToken cancellationToken)
        {
            await NotifyNewGame(notification.GameSessionId, false);
        }
      
        private async Task NotifyNewGame(GameSessionId gameSessionId,bool isCreated)
        {
            var gameStateText = isCreated ? "Игра создана" : "Игра началась заново";
            var gameSession = await gameSessionRepository.GetGameSession(gameSessionId);
            var text = $"{gameStateText}! Сложность: {GetDifficultyString(gameSession.Difficulty)} Количество игроков: {gameSession.CurrentMaxCharactersInGame}";
            await Notify(gameSessionId, text);
        }
        private static string GetDifficultyString(Difficulty difficulty)
        {
            return difficulty switch
            {
                Difficulty.Easy => "Простая",
                Difficulty.Medium => "Средняя",
                Difficulty.Hard => "Тяжелая",
                _ => "неизвестная",
            };
        }

    }
}
