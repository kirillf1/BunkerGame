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
    public class SpyCardCommandHandler : SpyCharacterComponentCommandHandler<Card>
    {
        public SpyCardCommandHandler(ICharacterRepository characterRepository, IMediator mediator) : base(characterRepository, mediator)
        {
        }

        public override async Task<Card> Handle(SpyCharacterComponentCommand<Card> request, CancellationToken cancellationToken)
        {
            var character = await GetCharacterAsync(request.CharacterId);
            var index = new Random().Next(0, character.Cards.Count);
            var card = character.Cards.ElementAt(index);
            await Notify(card, character);
            return card;
        }
    }
}
