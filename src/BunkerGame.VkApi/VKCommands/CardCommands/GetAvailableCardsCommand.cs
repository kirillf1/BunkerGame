using BunkerGame.Domain.Characters;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.CardCommands
{
    public class GetAvailableCardsCommand : CardCommand
    {
        public GetAvailableCardsCommand(IVkApi vkApi, IUserOptionsService userOptionsService, ICharacterRepository characterRepository) : base(vkApi, userOptionsService, characterRepository)
        {
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var userId = message.FromId.Value;
            var characterConversation = await TryGetCharacterWithConversation(userId);
            if (!characterConversation.HasValue)
                return true;
            var character = characterConversation.Value.Item1;
            var availableCardNumbers = character.UsedCards.Where(c => !c.CardUsed)
                    .Select(c => c.CardNumber.ToString()).ToList();
            switch (availableCardNumbers.Count)
            {
                case 0:
                    await SendVkMessage("Все карты использованы!", userId, VkKeyboardFactory.BuildPersonalButtons());
                    break;
                default:
                    await SendVkMessage("Выберете карту", userId,
                        VkKeyboardFactory.BuildOptionsButtoms(availableCardNumbers, "использовать карту №"));
                    break;
            }
            return true;
        }
    }
}
