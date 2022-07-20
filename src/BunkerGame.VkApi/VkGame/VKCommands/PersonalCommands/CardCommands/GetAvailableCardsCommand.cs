using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands
{
    public class GetAvailableCardsCommand : CardCommand
    {
        public GetAvailableCardsCommand(IVkApi vkApi, IUserService userOptionsService, ICharacterRepository characterRepository,IGameSessionRepository gameSessionRepository) 
            : base(vkApi, userOptionsService, characterRepository,gameSessionRepository)
        {
        }
        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var userId = message.FromId.Value;
            var characterConv = await ValidateCardRequest(userId);
            if (!characterConv.HasValue)
                return false;
            var character = characterConv.Value.Item1;
            var availableCardNumbers = character.Cards.Where(c => !c.IsUsed)
                    .Select(c => c.Id.Value.ToString()).ToList();
            switch (availableCardNumbers.Count)
            {
                case 0:
                    await SendVkMessage("Все карты использованы!", userId, VkKeyboardFactory.CreatePersonalButtons());
                    break;
                default:
                    await SendVkMessage("Выберете карту", userId,
                        VkKeyboardFactory.CreateOptionsButtoms(availableCardNumbers, "использовать карту №"));
                    break;
            }
            return true;
        }
    }
}
