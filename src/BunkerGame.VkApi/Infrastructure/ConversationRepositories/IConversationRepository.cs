using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame;

namespace BunkerGame.VkApi.Infrastructure.ConversationRepositories
{
    public interface IConversationRepository
    {

        public Task<IEnumerable<Conversation>> GetConversationsByUserId(long userId);
        public Task<Conversation?> GetConversationByCharacterId(CharacterId characterId);
        public Task<Conversation?> GetConversation(string name);
        public Task<Conversation?> GetConversation(long id);
        public Task<Conversation?> GetConversation(GameSessionId gameSessionId);
        public Task UpdateConversation(Conversation conversation);
        public Task DeleteConversation(long id);
        public Task AddConversation(Conversation conversation);
    }
}
