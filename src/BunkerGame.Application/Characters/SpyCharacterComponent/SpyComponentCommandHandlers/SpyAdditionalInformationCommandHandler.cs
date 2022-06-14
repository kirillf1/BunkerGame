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
    public class SpyAdditionalInformationCommandHandler : SpyCharacterComponentCommandHandler<AdditionalInformation>
    {
        public SpyAdditionalInformationCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public async override Task<AdditionalInformation> Handle(SpyCharacterComponentCommand<AdditionalInformation> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var addInf = character.AdditionalInformation;
            await Notify(addInf, character);
            return addInf;
        }
    }
}
