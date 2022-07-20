using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Shared;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame
{
    public class Conversation
    {
        public Conversation(long conversationId, GameSessionId gameSessionId, string conversationName)
        {
            ConversationId = conversationId;
            ConversationName = conversationName;
            GameSessionId = gameSessionId;
            Users = new();
            Difficulty = Difficulty.Easy;
            PlayersCount = 5;
        }
        public byte PlayersCount { get; private set; }
        public Difficulty Difficulty { get; set; }
        public long ConversationId { get; }
        public void SetPlayersCount(byte count)
        {
            if (count < 5 || count > 12)
                throw new ArgumentException("Count players must be more than 5 and less than 13");
            PlayersCount = count;
        }
        public void AddUsers(IEnumerable<User> users)
        {
            Users.AddRange(users);
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public GameSessionId GameSessionId { get; private set; }
        public void RestartGame()
        {
            Users.ForEach(u => u.RecreateCharacterId());
        }
        public string ConversationName { get; set; }
        public List<User> Users { get; }
        private readonly LinkedList<MessageKeyboard> KeyboardList = new LinkedList<MessageKeyboard>();

        public MessageKeyboard? CurrentKeyboard => KeyboardList.Last?.Value;
        public void PushKeyboard(MessageKeyboard messageKeyboard)
        {
            if (KeyboardList.Count > 5)
            {
                KeyboardList.RemoveFirst();
            }
            KeyboardList.AddLast(messageKeyboard);
        }
        public MessageKeyboard? GetPreviosKeyboard()
        {
            if (KeyboardList.Last == null)
                return null;
            var keyboard = KeyboardList.Last.Previous;
            KeyboardList.RemoveLast();
            return keyboard?.Value;
        }
        //public static async Task<Conversation> CreateConversation(IVkApi vkApi, long peerId)
        //{
        //    var usersTask = vkApi.Messages.GetConversationMembersAsync(peerId);
        //    var convNameTask = vkApi.Messages.GetConversationsByIdAsync(new List<long> { peerId });
        //    await Task.WhenAll(usersTask, convNameTask);
        //    var users = usersTask.Result.Profiles.Select(c => new User(c.Id, c.FirstName, c.LastName));
        //    var conversationName = convNameTask.Result.Items.First().ChatSettings.Title;
        //    return new Conversation(peerId, conversationName, users, (byte)users.Count());
        //}

    }
}
