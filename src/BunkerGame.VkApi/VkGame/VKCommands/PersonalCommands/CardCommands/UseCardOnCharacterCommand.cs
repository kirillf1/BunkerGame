using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using BunkerGame.Domain.Shared;
using BunkerGame.VkApi.VkGame.VkGameServices;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VkGame.VKCommands.PersonalCommands.CardCommands
{
    public class UseCardOnCharacterCommand : CardCommand
    {
        private readonly IPlayerRepository playerRepository;

        public UseCardOnCharacterCommand(IVkApi vkApi, IUserService userOptionsService,
            ICharacterRepository characterRepository, IPlayerRepository playerRepository,IGameSessionRepository gameSessionRepository) 
            : base(vkApi, userOptionsService, characterRepository,gameSessionRepository)
        {
            this.playerRepository = playerRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            if (!message.FromId.HasValue)
                return false;
            var userId = message.FromId.Value;
            var characterConversation = await ValidateCardRequest(userId);
            if (!characterConversation.HasValue)
                return true;
            var character = characterConversation.Value.Item1;
            var conversation = characterConversation.Value.Item2;
            var messageText = message.Text;
            var targetCharacterId = await GetTargetCharacterIdFromText(messageText, conversation);
            if (targetCharacterId == null)
            {
                await SendVkMessage("Введите имя игрока правильно!", userId);
                return false;
            }

            var cardNumber = await TryGetCardNumber(userId);
            if (!cardNumber.HasValue || character.Cards.First(c => c.Id.Value == cardNumber).IsUsed)
            {
                await SendVkMessage("Ошибка операции, попробуйте снова", userId, VkKeyboardFactory.CreatePersonalButtons());
                return false;
            }
            await userService.HandleCharacterCommand(new Domain.Characters.Commands.UseCard(character.Id, cardNumber.Value, targetCharacterId));
            await SendVkMessage("Карта использована!", userId, VkKeyboardFactory.CreatePersonalButtons());
            return true;
        }
        private async Task<byte?> TryGetCardNumber(long userId)
        {
            var cardOperationValue = await userService.GetOperationValue(userId, UserOperationType.CardNumber);
            if (byte.TryParse(cardOperationValue, out var cardNumber))
                return cardNumber;
            return null;
        }
        private async Task<CharacterId?> GetTargetCharacterIdFromText(string text, Conversation conversation)
        {
            var userName = text.Replace("карта на: ", "").TrimStart().TrimEnd().Split(" ");
            try
            {
                var player = await playerRepository.GetPlayer(userName[0], userName[1]);
                return conversation.Users.First(c => c.PlayerId == player.Id).CharacterId;
            }
            catch
            {
                return null;
            }
        }
    }
}
