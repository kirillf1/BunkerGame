namespace BunkerGame.VkApi.ConversationRepositories
{
    public class Conversation
    {
        public long ConversationId { get; set; }

        public Conversation(long conversationId, string conversationName, IEnumerable<User> users, byte playersCount)
        {
            ConversationId = conversationId;
            ConversationName = conversationName;
            Users = new(users);
            PlayersCount = playersCount;
        }
        public byte PlayersCount { get; set; }
        public string ConversationName { get; set; }
        public List<User> Users { get; set; }
    }
}
