namespace BunkerGame.VkApi.ConversationRepositories
{
    public class User
    {
        public User(long userId,string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public long UserId { get; }
        public string UserName { get; }
    }
}
