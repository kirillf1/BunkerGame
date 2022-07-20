using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using System.Text.RegularExpressions;
using VkNet.Abstractions;

namespace BunkerGame.VkApi.VkGame.VKCommands.ConversationCommands.SetDifficultyCommands
{
    public abstract class DifficultyCommand : ConversationCommandBase
    {
        protected readonly static Dictionary<string, Difficulty> Difficulties;


        static DifficultyCommand()
        {
            Difficulties = new Dictionary<string, Difficulty>();
            Difficulties["простая"] = Difficulty.Easy;
            Difficulties["средняя"] = Difficulty.Medium;
            Difficulties["тяжелая"] = Difficulty.Hard;
        }
        protected DifficultyCommand(IVkApi vkApi, ConversationService conversationService) : base(vkApi, conversationService)
        {
        }

        protected static Difficulty? GetDifficultyFromString(string text)
        {
            var key = Difficulties.Keys.FirstOrDefault(c => Regex.IsMatch(text, c, RegexOptions.IgnoreCase));
            return key == null ? null : Difficulties[key];
        }

    }
}
