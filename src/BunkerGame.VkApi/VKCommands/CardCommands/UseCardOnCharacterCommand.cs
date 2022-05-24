using BunkerGame.Application.Characters.UserCard;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using BunkerGame.VkApi.Services.UserOptionsServices;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;

namespace BunkerGame.VkApi.VKCommands.CardCommands
{
    public class UseCardOnCharacterCommand : CardCommand
    {
        private readonly IMediator mediator;
        public UseCardOnCharacterCommand(IVkApi vkApi, IMediator mediator, IUserOptionsService userOptionsService,
            ICharacterRepository characterRepository) : base(vkApi, userOptionsService, characterRepository)
        {
            this.mediator = mediator;
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
            var conversation = characterConversation.Value.Item2;
            var messageText = message.Text;

            var targetUserId = GetTargetUserIdFromText(messageText, conversation);
            if (!targetUserId.HasValue)
            {
                await SendVkMessage("Введите имя игрока правильно!", userId);
                return false;
            }
            var targetCharacter = await characterRepository.GetCharacter(character.GameSessionId!.Value, targetUserId.Value, false);
            var cardNumber = await TryGetCardNumber(userId);
            if (!cardNumber.HasValue || targetCharacter == null)
            {
                await SendVkMessage("Ошибка операции, попробуйте снова", userId, VkKeyboardFactory.BuildPersonalButtons());
                return false;
            }
            await mediator.Send(new UseCardOnOtherCharacterCommand(cardNumber.Value, character.Id, targetCharacter.Id));
            await SendVkMessage("Карта использована!", userId, VkKeyboardFactory.BuildPersonalButtons());
            return true;
        }
        private async Task<byte?> TryGetCardNumber(long userId)
        {
            var cardOperationValue = await userOptionsService.GetOperationValue(userId, UserOperationType.CardNumber);
            if (!byte.TryParse(cardOperationValue, out var cardNumber))
                return cardNumber;
            return null;
        }
        private static long? GetTargetUserIdFromText(string text, ConversationRepositories.Conversation conversation)
        {
            var userName = text.Replace("карта на: ", "");
            return conversation.Users.Find(c => string.Equals(c.FirstName + " " + c.LastName, userName, StringComparison.OrdinalIgnoreCase))?.UserId;
        }
    }
}
