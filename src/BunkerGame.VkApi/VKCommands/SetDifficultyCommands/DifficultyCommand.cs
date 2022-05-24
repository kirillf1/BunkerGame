using BunkerGame.Domain.GameSessions;
using System.Text.RegularExpressions;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VKCommands.SetDifficultyCommands
{
    public abstract class DifficultyCommand : VkCommand
    {
        protected readonly static Dictionary<string, Difficulty> Difficulties;
        protected readonly IConversationRepository conversationRepository;

        static DifficultyCommand()
        {
            Difficulties = new Dictionary<string, Difficulty>();
            Difficulties["легкая"] = Difficulty.Easy;
            Difficulties["средняя"] = Difficulty.Medium;
            Difficulties["сложная"] = Difficulty.Hard;
        }
        protected DifficultyCommand(IVkApi vkApi,IConversationRepository conversationRepository) : base(vkApi)
        {
            this.conversationRepository = conversationRepository;
        }
        protected static Difficulty? GetDifficultyFromString(string text)
        {
            var key = Difficulties.Keys.FirstOrDefault(c => Regex.IsMatch(text,c,RegexOptions.IgnoreCase));
            return key == null ? null : Difficulties[key];
        }
        protected async Task<Conversation> GetOrCreateConversation(long peerId)
        {
            var conversation = await conversationRepository.GetConversation(peerId);
            if (conversation == null)
            {
                conversation = await Conversation.CreateConversation(vkApi, peerId);
                await conversationRepository.AddConversation(conversation);
            }
            return conversation;
        }
    }
}
