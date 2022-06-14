using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Domain.Characters;
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
    }
}
