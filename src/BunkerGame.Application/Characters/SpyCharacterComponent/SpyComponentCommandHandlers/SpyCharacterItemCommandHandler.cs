using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.SpyCharacterComponent.SpyComponentCommandHandlers
{
    public class SpyCharacterItemCommandHandler : SpyCharacterComponentCommandHandler<CharacterItem>
    {
        public SpyCharacterItemCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public override async Task<CharacterItem> Handle(SpyCharacterComponentCommand<CharacterItem> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var itemIndex = new Random().Next(0, character.CharacterItems.Count);
            var item = character.CharacterItems.ElementAt(itemIndex);
            await Notify(item, character);
            return item;
        }
    }
}
