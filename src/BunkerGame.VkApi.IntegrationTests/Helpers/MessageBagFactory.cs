using BunkerGame.VkApi.IntegrationTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet.Model;

namespace BunkerGame.VkApi.IntegrationTests.Helpers
{
    internal static class MessageBagFactory
    {
        static readonly Random random;
        static readonly List<string> firstNames;
        static readonly List<string> lastNames;
        static MessageBagFactory()
        {
            random = new Random();
            firstNames = new List<string> { "David", "Kirill", "Denis", "Andrey,Alexandr", "Maxim", "Katya", "Lena" };
            lastNames = new List<string> { "Pupkin", "Smith", "Gorbachev", "Lenin", "Stalin" };
        }
        public static MessageBag CreateMessageBug(long peerId,byte userCount)
        {
            List<User> users = new(userCount);
            for (int i = 0; i < userCount; i++)
            {
                users.Add(new User()
                {
                    Id = random.Next(1, 100000),
                    FirstName = firstNames[random.Next(0, firstNames.Count)],
                    LastName = lastNames[random.Next(0, lastNames.Count)]
                });
            }
            return new MessageBag(new List<VkConversationData> { new VkConversationData(peerId, users, "Conversation" + random.Next())});
        }
    }
}
