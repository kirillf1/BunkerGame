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
    public class SpyChildbearingCommandHandler : SpyCharacterComponentCommandHandler<Childbearing>
    {
        public SpyChildbearingCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public async override Task<Childbearing> Handle(SpyCharacterComponentCommand<Childbearing> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var childbearing = character.Childbearing;
            await Notify(childbearing, character);
            return childbearing;
        }
    }
}
