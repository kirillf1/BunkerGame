using BunkerGame.Domain.GameSessions;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class GameSessionParamsChangedHandler : EventHandlerBase<Events.MaxCharacterCountChanged>,
        INotificationHandler<Events.DifficultyChanged>
    {
        public GameSessionParamsChangedHandler(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task Handle(Events.MaxCharacterCountChanged notification, CancellationToken cancellationToken)
        {
            await Notify(notification.GameSessionId, $"Максимальное количество игроков установлено на {notification.MaxCharacterCount}");
        }

        public async Task Handle(Events.DifficultyChanged notification, CancellationToken cancellationToken)
        {
            var text = $"Сложность игры изменилась на {GetDifficultyString(notification.Difficulty)}";
            await Notify(notification.GameSessionId, text);
        }
        private static string GetDifficultyString(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return "Простая";
                case Difficulty.Medium:
                    return "Средняя";
                case Difficulty.Hard:
                    return "Тяжелая";
                default:
                    return "неизвестная";
            }
        }
    }
}
