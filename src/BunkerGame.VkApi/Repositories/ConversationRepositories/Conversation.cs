using BunkerGame.Domain.GameSessions;
using VkNet.Abstractions;
using VkNet.Model.Keyboard;

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
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;
        private LinkedList<MessageKeyboard> KeyboardList = new LinkedList<MessageKeyboard>();

       
        public void PushKeyboard(MessageKeyboard messageKeyboard)
        {
            if(KeyboardList.Count > 5)
            {
                KeyboardList.RemoveFirst();
            }
            KeyboardList.AddLast(messageKeyboard);
        }
       
        public MessageKeyboard? PopLastKeyboard()
        {
            if (KeyboardList.Last == null)
                return null;
            var keyboard = KeyboardList.Last;
            KeyboardList.RemoveLast();
            return keyboard.Value;
        }
        public static async Task<Conversation> CreateConversation(IVkApi vkApi,long peerId)
        {
            var usersTask = vkApi.Messages.GetConversationMembersAsync(peerId);
            var convNameTask = vkApi.Messages.GetConversationsByIdAsync(new List<long> { peerId });
            await Task.WhenAll(usersTask, convNameTask);
            var users = usersTask.Result.Profiles.Select(c => new User(c.Id, c.FirstName,c.LastName));
            var conversationName = convNameTask.Result.Items.First().ChatSettings.Title;
            return new Conversation(peerId, conversationName, users, (byte)users.Count());
        }
       
    }
}
