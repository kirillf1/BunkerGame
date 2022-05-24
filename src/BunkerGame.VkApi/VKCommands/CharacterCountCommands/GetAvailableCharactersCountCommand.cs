using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.CharacterCountCommands
{
    public class GetAvailableCharactersCountCommand : CharacterCountCommand
    {
        public GetAvailableCharactersCountCommand(IVkApi vkApi, IConversationRepository conversationRepository) : base(vkApi, conversationRepository)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var peerId = message.PeerId;
            if (!peerId.HasValue)
                return false;
            var keyboard = VkKeyboardFactory.BuildOptionsButtoms(new List<string> { "6", "7", "8", "9", "10", "11", "12" }, "!Игроков: ");
            var conversation = await GetOrCreateConversation(peerId.Value);
            conversation.PushKeyboard(VkKeyboardFactory.BuildConversationButtons(false));
            await SendVkMessage("Установите количество игроков", peerId.Value, keyboard);
            return true;
        }
    }
}
