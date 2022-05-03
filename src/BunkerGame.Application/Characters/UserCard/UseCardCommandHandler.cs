using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.ExchangeCharacteristic;
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
    public class UseCardCommandHandler : IRequestHandler<UseCardCommand, Unit>
    {
        private readonly IMediator mediator;
        private readonly ICharacterRepository characterRepository;

        public UseCardCommandHandler(IMediator mediator, ICharacterRepository characterRepository)
        {
            this.mediator = mediator;
            this.characterRepository = characterRepository;

        }


        public async Task<Unit> Handle(UseCardCommand request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacterById(request.CharacterId);
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            if (character.CheckCardUsed(request.CardNumber))
                throw new InvalidOperationException("Card is Used");
            var usedCard = character.UsedCards.First(c => c.CardNumber == request.CardNumber);
            var card = character.Cards.First(c => c.Id == usedCard.CardId);
            var cardMethod = card.CardMethod;
            //Tuple<Type, object>? result = new Tuple<Type, object>(typeof(object),new object());
            Character? targetCharacter = null;
            if (request.CardUseParams.TargetCharacterId.HasValue)
                character = await characterRepository.GetCharacterById(request.CardUseParams.TargetCharacterId.Value);
            await mediator.Publish(new CardUsedNotificationMessage(character!.GameSessionId!.Value, card.Description, character, targetCharacter?.PlayerId));
            var directionGroup = cardMethod.DefineDirectionGroup();
            switch (cardMethod.MethodType)
            {

                case MethodType.Update:
                    if (directionGroup == DirectionGroup.Character && cardMethod.MethodDirection != MethodDirection.Character)
                    {
                        await UpdateCharacterCharacteristic(character,
                         GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(cardMethod.MethodDirection.ToString())!, cardMethod.ItemId);
                    }
                    else
                    {
                        // add update all characteristic
                    }
                    break;
                case MethodType.Change:
                    switch (directionGroup)
                    {
                        case DirectionGroup.Character:
                            if (cardMethod.MethodDirection != MethodDirection.Character)
                            {
                                var targetCharacterId = request.CardUseParams.TargetCharacterId;
                                if (!targetCharacterId.HasValue || targetCharacter == null)
                                    throw new ArgumentNullException(nameof(targetCharacter));
                                await UpdateCharacterCharacteristic(targetCharacter!,
                                      GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(cardMethod.MethodDirection.ToString())!, cardMethod.ItemId);
                                await mediator.Publish(new CharacterUpdatedNotificationMessage(character), cancellationToken);
                            }
                            else
                            {
                                // add change all characteristic
                            }
                            break;
                        case DirectionGroup.Bunker:
                            Bunker bunker;
                            if (cardMethod.MethodDirection == MethodDirection.Bunker)
                            {
                                bunker = await mediator.Send(new ChangeBunkerCommand(character.GameSessionId!.Value));
                            }
                            else
                            {
                                bunker = await mediator.Send(new ChangeBunkerComponentCommand(character.GameSessionId!.Value,
                                    GameComponentTypeTextConventer.ConvertTextToBunkerComponentTypeEn(cardMethod.MethodDirection.ToString())!, cardMethod.ItemId));
                            }
                            await mediator.Publish(new BunkerUpdatedNotification(character.GameSessionId.Value, bunker));
                            break;
                        case DirectionGroup.Catastrophe:
                            var updatedCatastrophe = await mediator.Send(new ChangeCatastropheCommand(character.GameSessionId!.Value), cancellationToken);
                            await mediator.Publish(new CatastropheUpdatedNotification(character.GameSessionId.Value, updatedCatastrophe));
                            break;
                        case DirectionGroup.GameSession:
                            break;
                    }

                    break;
                case MethodType.Add:
                    switch (directionGroup)
                    {
                        case DirectionGroup.GameSession:
                            if (cardMethod.MethodDirection == MethodDirection.ExternalSurrounding)
                            {
                                var surrounding = await mediator.Send(new AddExternalSurroundingCommand(cardMethod.ItemId.GetValueOrDefault(), character.GameSessionId!.Value));
                                await mediator.Publish(new ExternalSurroundingUpdatedNotification(character.GameSessionId.Value, surrounding));
                            }
                            else if (cardMethod.MethodDirection == MethodDirection.FreePlace)
                            {
                                var freePlace = await mediator.Send(new ChangeFreePlaceCommand(character.GameSessionId!.Value, true), cancellationToken);
                                await mediator.Publish(new BunkerSizeChangedNotification(character.GameSessionId.Value, freePlace), cancellationToken);
                            }
                            break;

                    }
                    break;
                case MethodType.Exchange:
                    switch (directionGroup)
                    {
                        case DirectionGroup.Character:
                            if (cardMethod.MethodDirection != MethodDirection.Character)
                            {
                                var updatedCharacters = await mediator.Send(new ExchangeCharacteristicCommand(character.Id, request.CardUseParams.TargetCharacterId!.Value,
                                  GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(cardMethod.MethodDirection.ToString())!), cancellationToken);
                                await mediator.Publish(new CharactersExchangedNotification(updatedCharacters.Item1, updatedCharacters.Item2), cancellationToken);
                            }
                            else
                            {
                                // addExchange characters 
                            }
                            break;
                    }
                    break;
                case MethodType.Remove:
                    if (cardMethod.MethodDirection == MethodDirection.Character)
                    {
                        var kickedCharacter = await mediator.Send(new KickCharacterCommand(request.CardUseParams.GameSessionId!.Value, request.CardUseParams.TargetCharacterId!.Value));
                        await mediator.Publish(new CharacterKickedNotification(kickedCharacter.GameSessionId!.Value, kickedCharacter));
                    }
                    else if (cardMethod.MethodDirection == MethodDirection.FreePlace)
                    {
                        var freePlace = await mediator.Send(new ChangeFreePlaceCommand(character.GameSessionId!.Value, false), cancellationToken);
                        await mediator.Publish(new BunkerSizeChangedNotification(character.GameSessionId.Value, freePlace), cancellationToken);
                    }
                    break;
            }
            usedCard.ChangeCardUsage(true);
            await characterRepository.CommitChanges();
            return Unit.Value;
        }

        private async Task<Character> UpdateCharacterCharacteristic(Character character, Type characteristicType, int? targetId)
        {
            var updatedCharacter = await mediator.Send(new ChangeCharacteristicCommand(character.Id, characteristicType) { CharacteristicId = targetId });
            await mediator.Publish(new CharacterUpdatedNotificationMessage(updatedCharacter));
            return updatedCharacter;
        }
    }
}
