namespace BunkerGame.VkApi.ConversationRepositories
{
    public interface IConversationRepository
    {
       
        public Task<IEnumerable<Conversation>> GetConversationsByUserId(long userId);
        public Task<Conversation?> GetConversation(string name);
        public Task<Conversation?> GetConversation(long id);
        public Task UpdateConversation(Conversation conversation);
        public Task DeleteConversation(long id);
        public Task AddConversation(Conversation conversation);
    }
}
