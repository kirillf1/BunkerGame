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
    public class SpySizeCommandHandler : SpyCharacterComponentCommandHandler<Size>
    {
        public SpySizeCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public override async Task<Size> Handle(SpyCharacterComponentCommand<Size> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var size = character.Size;
            await Notify(size, character);
            return size;
        }
    }
}
