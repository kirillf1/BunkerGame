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
    public class SpyPhobiaCommandHandler : SpyCharacterComponentCommandHandler<Phobia>
    {
        public SpyPhobiaCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public async override Task<Phobia> Handle(SpyCharacterComponentCommand<Phobia> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var phobia = character.Phobia;
            await Notify(phobia, character);
            return phobia;
        }
    }
}
