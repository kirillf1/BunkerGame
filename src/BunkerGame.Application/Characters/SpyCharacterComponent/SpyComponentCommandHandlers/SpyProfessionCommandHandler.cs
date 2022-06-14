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
    public class SpyProfessionCommandHandler : SpyCharacterComponentCommandHandler<Profession>
    {
        public SpyProfessionCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public override async Task<Profession> Handle(SpyCharacterComponentCommand<Profession> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var profession = character.Profession;
            await Notify(profession, character);
            return profession;
        }
    }
}
