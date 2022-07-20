using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.GameSessions.EventHandlers
{
    public class CharactersChangedHandler : EventHandlerBase<Events.CharacterAdded>, INotificationHandler<Events.CharacterKicked>
    {
        private readonly IPlayerRepository playerRepository;

        public CharactersChangedHandler(IVkApi vkApi, IConversationRepository conversationRepository, IPlayerRepository playerRepository) : base(vkApi, conversationRepository)
        {
            this.playerRepository = playerRepository;
        }

        public override async Task Handle(Events.CharacterAdded notification, CancellationToken cancellationToken)
        {
            var playerName = await GetPlayerName(notification.GameSessionId, notification.CharacterId);
            var text = $"Игрок {playerName} в игре!";
            await Notify(notification.GameSessionId, text);
        }

        public async Task Handle(Events.CharacterKicked notification, CancellationToken cancellationToken)
        {
            var playerName = await GetPlayerName(notification.GameSessionId, notification.CharacterId);
            var text = $"Игрок {playerName} исключен!";
            await Notify(notification.GameSessionId, text);
        }

        private async Task<string> GetPlayerName(GameSessionId gameSessionId, CharacterId characterId)
        {
            var conversation = await conversationRepository.GetConversation(gameSessionId)
                ?? throw new ArgumentNullException(nameof(Conversation));
            var user = conversation.Users.FirstOrDefault(c => c.CharacterId == characterId)
                ?? throw new ArgumentNullException(nameof(User));
            var player = await playerRepository.GetPlayer(user.PlayerId);
            return player.FirstName + " " + player.LastName;
        }
    }
}
