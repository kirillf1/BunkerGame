namespace BunkerGame.VkApi.ConversationRepositories
{
    public class User
    {
        public User(long userId,string nameFirst, string nameLast)
        {
            UserId = userId;
            FirstName = nameFirst;
            LastName = nameLast;
        }

        public long UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
