using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Shared;
using VkNet.Abstractions;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame.Characters
{
    public class VkSenderByCharacter
    {
        private readonly IVkApi vkApi;
        private readonly IConversationRepository conversationRepository;

        public VkSenderByCharacter(IVkApi vkApi, IConversationRepository conversationRepository)
        {
            this.vkApi = vkApi;
            this.conversationRepository = conversationRepository;
        }
        public async Task SendCharacter(Character character)
        {
            var userId = await GetUserId(character);
            if (!userId.HasValue)
                return;
            var text = $"Ваш персонаж: {Environment.NewLine}{GameComponentsConventer.ConvertCharacter(character)}";
            var messageParams = VkMessageParamsFactory.CreateMessageSendParams(text, userId.Value);
            await vkApi.Messages.SendAsync(messageParams);
        }
        public async Task SendMessageToCharacter(Character character, string message, MessageKeyboard? messageKeyboard = null)
        {
            var userId = await GetUserId(character);
            if (!userId.HasValue)
                return;
            await vkApi.SendVKMessage(message, userId.Value, messageKeyboard);
        }
        public async Task SendMessageToCharacter(CharacterId characterId, string message, MessageKeyboard? messageKeyboard = null)
        {
            var conversation = await conversationRepository.GetConversationByCharacterId(characterId);
            if (conversation == null)
                return;
            var userId = conversation.Users.Find(c => c.CharacterId == characterId)!.UserId;
            await vkApi.SendVKMessage(message, userId, messageKeyboard);
        }
        private async Task<long?> GetUserId(Character character)
        {
            var conversation = await conversationRepository.GetConversation(character.GameSessionId);
            if (conversation == null)
                return null;
            var user = conversation.Users.FirstOrDefault(c => c.CharacterId == character.Id);
            return user?.UserId;
        }
    }
}
