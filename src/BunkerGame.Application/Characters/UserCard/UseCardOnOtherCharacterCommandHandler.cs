using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.ExchangeCharacteristic;
using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Application.GameSessions.KickCharacter;
using BunkerGame.Domain.Characters;
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
        public async Task<Unit> Handle(UseCardOnOtherCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await GetCharacter(request.UseCardCharacterId);
            var cardNumber = request.CardNumber;
            if (character.CheckCardUsed(cardNumber))
                throw new InvalidOperationException($"Card №{cardNumber} is used");
            var card = character.GetCardByNumber(cardNumber);
            var cardMethod = card.CardMethod;
            var directionGroup = cardMethod.DefineDirectionGroup();
            await mediator.Publish(new CardUsedNotificationMessage(character.GameSessionId!.Value, card.Description, character,request.TargetCharacterId), cancellationToken);
            
            if (cardMethod.MethodType == MethodType.Exchange && directionGroup == DirectionGroup.Character)
            {
                await ExchangeCharacterComponents(character.Id, request.UseCardCharacterId, cardMethod.MethodDirection);
            }
            else if (cardMethod.MethodType == MethodType.Change && directionGroup == DirectionGroup.Character)
            {
                await ChangeCharacterCharacteristic(request.UseCardCharacterId, cardMethod.ItemId, cardMethod.MethodDirection);
            }
            else if(cardMethod.MethodType == MethodType.Remove && cardMethod.MethodDirection == MethodDirection.Character)
            {
                await KickCharacter(character.GameSessionId!.Value, request.TargetCharacterId);
            }
            else if(cardMethod.MethodType == MethodType.Spy && directionGroup == DirectionGroup.Character)
            {
                await SpyCharacterComponent(request.TargetCharacterId, character.GameSessionId!.Value, cardMethod.MethodDirection);
            }
            await base.UseCard(character, cardNumber);
            return Unit.Value;
        }

        private async Task KickCharacter(long gameSessionId, int targetCharacterId)
        {
            var kickedCharacter = await mediator.Send(new KickCharacterCommand(gameSessionId, targetCharacterId));
            await mediator.Publish(new CharacterKickedNotification(gameSessionId, kickedCharacter));
        }
        private async Task SpyCharacterComponent(int characterId,long gameSessionId,MethodDirection methodDirection)
        {
            if (methodDirection != MethodDirection.Character)
            {
                var characterComponent = await mediator.Send(new SpyCharacterComponentCommand(characterId, methodDirection));
                await mediator.Publish(new SpiedCharacterComponentNotification(characterId, gameSessionId, characterComponent));
            }
            else
            {
               // add spy all character
            }
        }
        private async Task ExchangeCharacterComponents(int characterFirst, int characterSecond, MethodDirection methodDirection)
        {
            if (methodDirection == MethodDirection.Character)
            {
                // add change Character
            }
            else
            {
                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(methodDirection.ToString());
                var updatedCharacters = await mediator.Send(new ExchangeCharacteristicCommand(characterFirst, characterSecond, characterComponentType!));
                await mediator.Publish(new CharactersExchangedNotification(updatedCharacters.Item1, updatedCharacters.Item2));
            }
        }
        private async Task ChangeCharacterCharacteristic(int targetCharacter, int? componentId, MethodDirection methodDirection)
        {
            if (methodDirection != MethodDirection.Character)
            {
                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(methodDirection.ToString());
                var updatedCharacter = await mediator.Send(new ChangeCharacteristicCommand(targetCharacter, characterComponentType!)
                {
                    CharacteristicId = componentId
                });
                await mediator.Publish(new CharacterUpdatedNotificationMessage(updatedCharacter));
            }
            else
            {
                // Change Character 
            }
        }
    }
}
