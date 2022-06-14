using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;

namespace BunkerGame.Application.Characters.SpyCharacterComponent.SpyComponentCommandHandlers
{
    public class SpySexCommandHandler : SpyCharacterComponentCommandHandler<Sex>
    {
        public SpySexCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public override async Task<Sex> Handle(SpyCharacterComponentCommand<Sex> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var sex = character.Sex;
            await Notify(sex, character);
            return sex;
        }

    }
}
