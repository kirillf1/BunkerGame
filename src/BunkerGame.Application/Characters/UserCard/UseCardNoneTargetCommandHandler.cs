using BunkerGame.Application.Characters.UserCard.Notifications;
using BunkerGame.Domain.Characters;
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
                new NoneTargetCardArgs(character.Id, character.GameSessionId.Value, card.CardMethod));
            if (command != null)
                await mediator.Send(command);
            await UseCard(character, request.CardNumber);
            return Unit.Value;
        }
    }
}
