using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.ExchangeCharacteristic;
using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Application.GameSessions.KickCharacter;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;

namespace BunkerGame.Application.Characters.UserCard
{
    public class UseCardOnOtherCharacterCommandHandler : UseCardCommandHandler, IRequestHandler<UseCardOnOtherCharacterCommand>
    {
        private readonly IMediator mediator;

        public UseCardOnOtherCharacterCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Find by card method and card direction commandHandler with execution on target character.Than change card state on used.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If card used</exception>
        public async Task<Unit> Handle(UseCardOnOtherCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await GetCharacter(request.UseCardCharacterId);
            var cardNumber = request.CardNumber;
            if (character.CheckCardUsed(cardNumber))
                throw new InvalidOperationException($"Card №{cardNumber} is used");
            var card = character.GetCardByNumber(cardNumber);
            await mediator.Publish(new CardUsedNotificationMessage(character.GameSessionId!.Value, card.Description, character,request.TargetCharacterId), cancellationToken);
            //await TryActivateCard(card, character, request.TargetCharacterId);
            object? command = CommandExplorerByCardMethod.GetTargetCharacterCommands(
                new TargetCharacterCardArgs(character.Id, request.TargetCharacterId, character.GameSessionId.Value, card.CardMethod));
            if(command != null)
                await mediator.Send(command);
            await base.UseCard(character, cardNumber);
            return Unit.Value;
        }
        //private async Task<bool> TryActivateCard(Card card, Character cardUserCharacter,int targetCharacterId)
        //{
        //    var cardMethod = card.CardMethod;
        //    var directionGroup = cardMethod.DefineDirectionGroup();
        //    if (cardMethod.MethodType == MethodType.Exchange && directionGroup == DirectionGroup.Character)
        //    {
        //        await ExchangeCharacterComponents(cardUserCharacter.Id, targetCharacterId, cardMethod.MethodDirection);
        //        return true;
        //    }
        //    else if (cardMethod.MethodType == MethodType.Change && directionGroup == DirectionGroup.Character)
        //    {
        //        await ChangeCharacterCharacteristic(targetCharacterId, cardMethod.ItemId, cardMethod.MethodDirection);
        //        return true;
        //    }
        //    else if (cardMethod.MethodType == MethodType.Remove && cardMethod.MethodDirection == MethodDirection.Character)
        //    {
        //        await KickCharacter(cardUserCharacter.GameSessionId!.Value, targetCharacterId);
        //        return true;
        //    }
        //    else if (cardMethod.MethodType == MethodType.Spy && directionGroup == DirectionGroup.Character)
        //    {
        //        await SpyCharacterComponent(targetCharacterId, cardUserCharacter.GameSessionId!.Value, cardMethod.MethodDirection);
        //        return true;
        //    }
        //    return false;
        //}
        //private async Task KickCharacter(long gameSessionId, int targetCharacterId)
        //{
        //    var kickedCharacter = await mediator.Send(new KickCharacterCommand(gameSessionId, targetCharacterId));
        //    await mediator.Publish(new CharacterKickedNotification(gameSessionId, kickedCharacter));
        //}
        //private async Task SpyCharacterComponent(int characterId,long gameSessionId,MethodDirection methodDirection)
        //{
        //    if (methodDirection != MethodDirection.Character)
        //    {
        //        var characterComponent = await mediator.Send(new SpyCharacterComponentCommand(characterId, methodDirection));
        //        await mediator.Publish(new SpiedCharacterComponentNotification(characterId, gameSessionId, characterComponent));
        //    }
        //    else
        //    {
        //       // add spy all character
        //    }
        //}
        //private async Task ExchangeCharacterComponents(int characterFirst, int characterSecond, MethodDirection methodDirection)
        //{
        //    if (methodDirection == MethodDirection.Character)
        //    {
        //        // add change Character
        //    }
        //    else
        //    {
        //        var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(methodDirection.ToString());
        //        var updatedCharacters = await mediator.Send(new ExchangeCharacteristicCommand(characterFirst, characterSecond, characterComponentType!));
        //        await mediator.Publish(new CharactersExchangedNotification(updatedCharacters.Item1, updatedCharacters.Item2));
        //    }
        //}
        //private async Task ChangeCharacterCharacteristic(int targetCharacter, int? componentId, MethodDirection methodDirection)
        //{
        //    if (methodDirection != MethodDirection.Character)
        //    {
        //        var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(methodDirection.ToString());
        //        var updatedCharacter = await mediator.Send(new ChangeCharacteristicCommand(targetCharacter, characterComponentType!)
        //        {
        //            CharacteristicId = componentId
        //        });
        //        await mediator.Publish(new CharacterUpdatedNotificationMessage(updatedCharacter));
        //    }
        //    else
        //    {
        //        // Change Character 
        //    }
        //}
    }
}
