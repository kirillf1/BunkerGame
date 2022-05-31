using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.ExchangeCharacteristic;
using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Application.GameSessions.AddExternalSurroundingInGame;
using BunkerGame.Application.GameSessions.ChangeBunker;
using BunkerGame.Application.GameSessions.ChangeCatastophe;
using BunkerGame.Application.GameSessions.ChangeFreePlace;
using BunkerGame.Application.GameSessions.KickCharacter;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using BunkerGame.Domain.Players;
using MediatR;

namespace BunkerGame.Application.Characters.UserCard
{
    public class UseCardNoneTargetCommandHandler : UseCardCommandHandler, IRequestHandler<UseCardNoneTargetCommand, Unit>
    {
        private readonly IMediator mediator;

        public UseCardNoneTargetCommandHandler(IMediator mediator, ICharacterRepository characterRepository) : base(characterRepository)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Find by card method and card direction commandHandler with execution.Than change card state on used.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If card used</exception>
        public async Task<Unit> Handle(UseCardNoneTargetCommand request, CancellationToken cancellationToken)
        {
            var character = await GetCharacter(request.CharacterId);
            if (character.CheckCardUsed(request.CardNumber))
                throw new InvalidOperationException("Card is Used " + nameof(character));
            var usedCard = character.UsedCards.First(c => c.CardNumber == request.CardNumber);
            var card = character.Cards.First(c => c.Id == usedCard.CardId);
            var cardMethod = card.CardMethod;
            await mediator.Publish(new CardUsedNotificationMessage(character!.GameSessionId!.Value, card.Description, character), cancellationToken);
            //await ActivateCard(character.Id, character.GameSessionId!.Value, cardMethod);
            object? command = CommandExplorerByCardMethod.GetNoneTargetCommands(
                new NoneTargetCardArgs(character.Id,character.GameSessionId.Value, card.CardMethod));
            if (command != null)
                await mediator.Send(command);
            await UseCard(character, request.CardNumber);
            return Unit.Value;
        }
        //private async Task ActivateCard(int cardUserCharacterId, long gameSessionId, CardMethod cardMethod)
        //{
        //    var directionGroup = cardMethod.DefineDirectionGroup();
        //    switch (cardMethod.MethodType)
        //    {
        //        case MethodType.Update:
        //            await UpdateGameComponents(cardMethod, directionGroup, cardUserCharacterId, gameSessionId);
        //            return;
        //        case MethodType.Change:
        //            await ChangeGameComponents(cardMethod, directionGroup, gameSessionId);
        //            return;
        //        case MethodType.Add:
        //            await AddGameComponents(cardMethod, directionGroup, cardUserCharacterId, gameSessionId);
        //            return;
        //        case MethodType.SpyYourself:
        //            await SpyCharacterComponent(cardUserCharacterId, gameSessionId, cardMethod.MethodDirection);
        //            return;
        //        case MethodType.Remove:
        //            await RemoveGameComponents(cardMethod, gameSessionId);
        //            return;
        //    }
        //}
        //private async Task SpyCharacterComponent(int characterId, long gameSessionId, MethodDirection methodDirection)
        //{
        //    if (methodDirection != MethodDirection.Character)
        //    {
        //        var characterComponent = await mediator.Send(new SpyCharacterComponentCommand(characterId, methodDirection));
        //        await mediator.Publish(new SpiedCharacterComponentNotification(characterId, gameSessionId, characterComponent));
        //    }
        //    else
        //    {
        //        // add spy all character
        //    }
        //}
        //private async Task RemoveGameComponents(CardMethod cardMethod, long gameSessionId)
        //{
        //    switch (cardMethod.MethodDirection)
        //    {
        //        case MethodDirection.FreePlace:
        //            await ChangeFreePlaceSize(gameSessionId, false);
        //            break;

        //    }
        //}
       
        //private async Task AddGameComponents(CardMethod cardMethod, DirectionGroup directionGroup, int characterId, long gameSessionId)
        //{
        //    switch (directionGroup)
        //    {
        //        case DirectionGroup.GameSession:
        //            if (cardMethod.MethodDirection == MethodDirection.ExternalSurrounding)
        //            {
        //                var surrounding = await mediator.Send(new AddExternalSurroundingCommand(cardMethod.ItemId.GetValueOrDefault(), gameSessionId));
        //                await mediator.Publish(new ExternalSurroundingUpdatedNotification(gameSessionId, surrounding));
        //            }
        //            else if (cardMethod.MethodDirection == MethodDirection.FreePlace)
        //            {
        //                await ChangeFreePlaceSize(gameSessionId, true);
        //            }
        //            break;

        //    }
        //}
        //private async Task UpdateGameComponents(CardMethod cardMethod, DirectionGroup directionGroup, int characterId, long gameSessionId)
        //{
        //    switch (directionGroup)
        //    {
        //        case DirectionGroup.Character:
        //            if (cardMethod.MethodDirection != MethodDirection.Character)
        //            {
        //                // Update character component
        //                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(cardMethod.MethodDirection.ToString());
        //                await UpdateCharacterCharacteristic(characterId, characterComponentType!, cardMethod.ItemId);
        //                return;
        //            }
        //            // add changeCharacter
        //            break;
        //        case DirectionGroup.Bunker:
        //        case DirectionGroup.Catastrophe:
        //        case DirectionGroup.GameSession:
        //            break;

        //    }
        //}
        //private async Task ChangeGameComponents(CardMethod cardMethod, DirectionGroup directionGroup, long gameSessionId)
        //{
        //    switch (directionGroup)
        //    {
        //        case DirectionGroup.Bunker:
        //            await UpdateBunker(gameSessionId, cardMethod.MethodDirection, cardMethod.ItemId);
        //            break;
        //        case DirectionGroup.Catastrophe:
        //            var updatedCatastrophe = await mediator.Send(new ChangeCatastropheCommand(gameSessionId));
        //            await mediator.Publish(new CatastropheUpdatedNotification(gameSessionId, updatedCatastrophe));
        //            break;
        //    }
        //}
        //private async Task<byte> ChangeFreePlaceSize(long gameSessionId, bool needAdd)
        //{
        //    var freePlace = await mediator.Send(new ChangeFreePlaceCommand(gameSessionId, needAdd));
        //    await mediator.Publish(new BunkerSizeChangedNotification(gameSessionId, freePlace));
        //    return freePlace;
        //}
        //private async Task<Bunker> UpdateBunker(long gameSessionId, MethodDirection methodDirection, int? itemId)
        //{
        //    Bunker bunker;
        //    if (methodDirection == MethodDirection.Bunker)
        //    {
        //        bunker = await mediator.Send(new ChangeBunkerCommand(gameSessionId));
        //    }
        //    else
        //    {
        //        bunker = await mediator.Send(new ChangeBunkerComponentCommand(gameSessionId,
        //            GameComponentTypeTextConventer.ConvertTextToBunkerComponentTypeEn(methodDirection.ToString())!, itemId));
        //    }
        //    await mediator.Publish(new BunkerUpdatedNotification(gameSessionId, bunker));
        //    return bunker;

        //}
        //private async Task<Character> UpdateCharacterCharacteristic(int characterId, Type characteristicType, int? targetId)
        //{
        //    var updatedCharacter = await mediator.Send(new ChangeCharacteristicCommand(characterId, characteristicType)
        //    {
        //        CharacteristicId = targetId
        //    });
        //    await mediator.Publish(new CharacterUpdatedNotificationMessage(updatedCharacter));
        //    return updatedCharacter;
        //}
    }
}
