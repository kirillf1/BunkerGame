using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using MediatR;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.Characters.EventHandlers
{
    public class CardUsedHandler : INotificationHandler<Events.CardUsed>
    {
        private readonly IVkApi vkApi;
        private readonly IPlayerRepository playerRepository;
        private readonly IConversationRepository conversationRepository;

        public CardUsedHandler(IVkApi vkApi, IPlayerRepository playerRepository, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.playerRepository = playerRepository;
            this.conversationRepository = conversationRepository;
        }
        public async Task Handle(Events.CardUsed notification, CancellationToken cancellationToken)
        {
            var conversation = await conversationRepository.GetConversation(notification.GameSessionId);
            if (conversation == null)
                return;
            string text = $"{await GetPlayerName(conversation, notification.CharacterId)} использовал карту {notification.Card.Description}";
            if (notification.TargetCharacter != null)
                text += $" на {await GetPlayerName(conversation, notification.TargetCharacter)}";
            await vkApi.SendVKMessage(text, conversation.ConversationId);
        }
        private async Task<string> GetPlayerName(Conversation conversation, CharacterId characterId)
        {
            var playerId = conversation.Users.Find(c => c.CharacterId == characterId)?.PlayerId;
            if (playerId == null)
                return "неизвестный";
            var player = await playerRepository.GetPlayer(playerId);
            return player.FirstName + " " + player.LastName;
        }
    }
}
