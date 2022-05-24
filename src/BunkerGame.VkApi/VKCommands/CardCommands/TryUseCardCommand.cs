using BunkerGame.Application.Characters.UserCard;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model;
using BunkerGame.VkApi.ConversationRepositories;

namespace BunkerGame.VkApi.VKCommands.CardCommands
{
    public sealed class TryUseCardCommand : CardCommand
    {
        private readonly IMediator mediator;

        public TryUseCardCommand(IVkApi vkApi, IUserOptionsService userOptionsService,
            ICharacterRepository characterRepository,IMediator mediator) : base(vkApi, userOptionsService, characterRepository)
        {
            this.mediator = mediator;
        }

        public async override Task<bool> SendAsync(Message message)
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
            if (!byte.TryParse(messageText.Replace("использовать карту №", ""), out byte cardNumber) || cardNumber > 3)
            {
                await SendVkMessage("Введите номер карты корректно", userId, VkKeyboardFactory.BuildPersonalButtons());
                return false;
            }
            if (character.CheckCardUsed(cardNumber))
            {
                await SendVkMessage("Карта уже использована!", userId);
                return true;
            }
            var card = character.GetCardByNumber(cardNumber);
            if (!card.IsTargetCharacterCard())
            {
                await mediator.Send(new UseCardNoneTargetCommand(character.Id, cardNumber));
                return true;
            }
            var names = await GetUserNames(userId, conversation);
            if(names.Count == 0)
            {
                await SendVkMessage("Нет доступных игроков", userId,VkKeyboardFactory.BuildPersonalButtons());
                return true;
            }
            await SendVkMessage("Выберете игрока на которого будет применена карта",
                userId, VkKeyboardFactory.BuildOptionsButtoms(names, "карта на: "));
            await userOptionsService.SetOperation(userId, UserOperationType.CardNumber, cardNumber.ToString());
            return true;
        }
        private async Task< List<string>> GetUserNames(long userId, ConversationRepositories.Conversation conversation)
        {
            var aliveCharacters = await characterRepository.GetCharacters(16, false, c => c.GameSessionId == conversation.ConversationId);
            return aliveCharacters.Join(conversation!.Users.Where(c => c.UserId != userId),
                c => c.PlayerId, u => u.UserId,
                (_, c) => c.FirstName + " " + c.LastName).ToList();
        }
    }
}
