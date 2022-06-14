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
    public class SpyHobbyCommandHandler : SpyCharacterComponentCommandHandler<Hobby>
    {
        public SpyHobbyCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public async override Task<Hobby> Handle(SpyCharacterComponentCommand<Hobby> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var hobby = character.Hobby;
            await Notify(hobby, character);
            return hobby;
        }
    }
}
