using BunkerGame.Domain.Characters;
using BunkerGame.Framework;
using MediatR;

namespace BunkerGame.VkApi.VkGame.Characters.CommandHandlers
{
    public class UseCardHandler : CharacterCommandHandlerBase<Commands.UseCard>
    {
        private readonly VkSenderByCharacter vkSenderByCharacter;
        private readonly IMediator mediator;

        public UseCardHandler(ICharacterRepository characterRepository, IEventStore eventStore,
            VkSenderByCharacter vkSenderByCharacter, IMediator mediator) : base(characterRepository, eventStore)
        {
            this.vkSenderByCharacter = vkSenderByCharacter;
            this.mediator = mediator;
        }

        public override async Task<Unit> Handle(Commands.UseCard request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.GetCharacter(request.CharacterId);
            var result = character.UseCard(request.CardNumber, request.TargetCharacter);
            if (result.IsValid)
            {
                await mediator.Send(result.GetCommand());
                await eventStore.Save(character);
                return Unit.Value;
            }
            foreach (var error in result.Errors.Distinct())
            {
                switch (error)
                {
                    case Domain.Characters.Cards.CardExecuteError.NoTargetCharacter:
                        throw new NoTargetCharacterExpection();
                    case Domain.Characters.Cards.CardExecuteError.NoSuchCommand:
                    case Domain.Characters.Cards.CardExecuteError.InvalidComponentType:
                        await vkSenderByCharacter.SendMessageToCharacter(character, "На данный момент карта не работает! (Попробуйте ее реализовать посредством общения между игроками)");
                        break;
                    case Domain.Characters.Cards.CardExecuteError.CardUsed:
                        await vkSenderByCharacter.SendMessageToCharacter(character, "Карта уже использована");
                        break;
                }
            }
            return Unit.Value;
        }
    }
    public class NoTargetCharacterExpection : Exception
    {
        public NoTargetCharacterExpection() : base()
        {
        }

        public NoTargetCharacterExpection(string? message) : base(message)
        {
        }

        public NoTargetCharacterExpection(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
