using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.SpyCharacterComponent.SpyComponentCommandHandlers
{
    public class SpyTraitCommandHandler : SpyCharacterComponentCommandHandler<Trait>
    {
        public SpyTraitCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public async override Task<Trait> Handle(SpyCharacterComponentCommand<Trait> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var trait = character.Trait;
            await Notify(trait, character);
            return trait;
        }
    }
}
