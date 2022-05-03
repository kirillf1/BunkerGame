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

namespace BunkerGame.VkApi.VKCommands
{
    public class CardUseCommand : VkCommand
    {
        private readonly IMediator mediator;
        private readonly IUserOptionsService userOptionsService;
        private readonly ICharacterRepository characterRepository;

        public CardUseCommand(IVkApi vkApi, IMediator mediator, IUserOptionsService userOptionsService,
            ICharacterRepository characterRepository) : base(vkApi)
        {
            this.mediator = mediator;
            this.userOptionsService = userOptionsService;
            this.characterRepository = characterRepository;
        }

        public override async Task<bool> SendAsync(Message message)
        {
            var text = message.Text.ToLower();
            var userId = message.FromId.GetValueOrDefault();
            if (userId == 0)
                return false;
            var conversation = await userOptionsService.GetUserGame(userId);
            if (conversation == null)
            {
                await SendVkMessage("Вы не в игре или состоите в нескольких играх настройте конфигурацию! ", userId);
                return true;
            }
            if (text.Contains("использовать карты", StringComparison.OrdinalIgnoreCase))
            {

                var character = await characterRepository.GetCharacter(conversation.ConversationId, userId);
                if (character == null || !character.IsAlive)
                {
                    await SendVkMessage("Вы не состоите в игре или вы исключены", userId);
                    return true;
                }
                await SendVkMessage("Выберете карту", userId,
                     VkKeyboardFactory.BuildOptionsButtoms(character.UsedCards.Where(c => !c.CardUsed).
                     Select(c => c.CardNumber.ToString()).ToList(), "использовать карту №"));
                return true;

            }
            else if (text.Contains("использовать карту №", StringComparison.OrdinalIgnoreCase))
            {
                if (!byte.TryParse(text.Replace("использовать карту №", ""), out byte cardNumber) || cardNumber > 3)
                {
                    await SendVkMessage("Введите номер карты корректно", userId, VkKeyboardFactory.BuildPersonalButtons());
                    return false;
                }
                await TryUseCard(userId, conversation, cardNumber, new CardUseParams());
                return true;
            }
            else if (text.Contains("карта на:"))
            {
                var userName = text.Replace("карта на: ", "");
                var targetUserId = conversation.Users.Find(c => string.Equals(c.UserName, userName, StringComparison.OrdinalIgnoreCase))?.UserId;
                if (targetUserId == null)
                    await SendVkMessage("Введите имя игрока правильно!", userId);
                var targetCharacter = await characterRepository.GetCharacter(conversation.ConversationId, targetUserId!.Value, false);
                var cardOperationValue = await userOptionsService.GetOperationValue(userId, UserOperationType.CardNumber);
                if (!byte.TryParse(cardOperationValue, out var cardNumber) || targetCharacter == null)
                {
                    await SendVkMessage("Ошибка операции, попробуйте снова", userId, VkKeyboardFactory.BuildPersonalButtons());
                }

                await TryUseCard(userId, conversation, cardNumber,
                    new CardUseParams() { TargetCharacterId = targetCharacter!.Id, GameSessionId = targetCharacter.GameSessionId });
                return true;
            }
            return false;
        }




        private async Task TryUseCard(long userId, ConversationRepositories.Conversation conversation, byte cardNumber, CardUseParams cardUseParams)
        {
            var conversationId = conversation.ConversationId;
            var character = await characterRepository.GetCharacter(conversationId, userId);
            if (character?.IsAlive != true)
            {
                await SendVkMessage("Вы не состоите в игре или вы исключены", userId);
                return;
            }
            if (character.CheckCardUsed(cardNumber))
            {
                await SendVkMessage("Карта уже использована!", userId);
                return;
            }
            var card = character.GetCardByNumber(cardNumber);
            var requirements = card.GetActivateRequirements();
            if (!requirements.Any())
            {
                await mediator.Send(new UseCardCommand(character.Id, cardNumber, cardUseParams));

                //await SendCardExecuteResult(result, conversationId, card, conversation.Users.First(c => c.UserId == userId).UserName);
            }
            else
            {
                if (cardUseParams.IsEmpty())
                {
                    foreach (var requirement in requirements)
                    {
                        if (requirement == CardActivateRequirement.GameSessionId)
                        {
                            cardUseParams.GameSessionId = conversation.ConversationId;
                        }
                        else if (requirement == CardActivateRequirement.CharacterId)
                        {
                            var aliveCharacters = await characterRepository.GetCharacters(16, false, c => c.GameSessionId == conversationId);
                            await SendVkMessage("Выберете игрока на которого будет применена карта",
                                userId, VkKeyboardFactory.BuildOptionsButtoms(aliveCharacters.Join(conversation.Users.Where(c => c.UserId != userId), c => c.PlayerId, u => u.UserId, (_, c) => c.UserName).ToList(), "карта на: "));
                            await userOptionsService.SetOperation(userId, UserOperationType.CardNumber, cardNumber.ToString());
                            return;
                        }

                    }
                }
                var result = await mediator.Send(new UseCardCommand(character.Id, cardNumber, cardUseParams));
                //await SendCardExecuteResult(result, conversationId, card, conversation.Users.First(c => c.UserId == userId).UserName);

            }
        }
        //private async Task<bool> SendCardExecuteResult(Tuple<Type, object> cardResult, long gameId, Card card, string userName)
        //{
        //    await SendVkMessage($"Игрок {userName} использовал карту: {card.Description}", gameId);
        //    if (cardResult.Item1 == typeof(Character))
        //    {
        //        var character = (Character)cardResult.Item2;
        //        await SendVkMessage($"Ваши характеристики: {GameComponentsConventer.ConvertCharacter(character)}", character.PlayerId!.Value);
        //    }
        //    else if (cardResult.Item1 == typeof(Tuple<Character, Character>))
        //    {
        //        var characters = (Tuple<Character, Character>)cardResult.Item2;
        //        await SendVkMessage($"Ваши характеристики: {GameComponentsConventer.ConvertCharacter(characters.Item1)}", characters.Item1.PlayerId!.Value,
        //            VkKeyboardFactory.BuildPersonalButtons());
        //        await SendVkMessage($"Ваши характеристики: {GameComponentsConventer.ConvertCharacter(characters.Item2)}",
        //            characters.Item2.PlayerId!.Value);
        //    }
        //    else if (cardResult.Item1 == typeof(byte))
        //    {
        //        await SendVkMessage($"Игрок под номером {(byte)cardResult.Item2} исключен", gameId);
        //    }
        //    else if (cardResult.Item1 == typeof(Bunker))
        //    {
        //        await SendVkMessage($"Бункер изменился: {GameComponentsConventer.ConvertBunker((Bunker)cardResult.Item2)}", gameId);
        //    }
        //    else if (cardResult.Item1 == typeof(Catastrophe))
        //    {
        //        await SendVkMessage($"Катаклизм изменился: {GameComponentsConventer.ConvertCatastrophe((Catastrophe)cardResult.Item2)}", gameId);
        //    }
        //    return true;
        //}
    }
}
