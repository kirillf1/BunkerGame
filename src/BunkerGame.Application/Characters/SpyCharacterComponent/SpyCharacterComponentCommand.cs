using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.SpyCharacterComponent
{
    public class SpyCharacterComponentCommand : IRequest<CharacterComponent>
    {
        public SpyCharacterComponentCommand(int characterId, MethodDirection methodDirection)
        {
            CharacterId = characterId;
            var type = GameComponentTypeTextConventer.ConvertTextToCharacteristicTypeEn(methodDirection.ToString());
            if (type == null)
                throw new InvalidOperationException(nameof(methodDirection));
            MethodDirection = methodDirection;
            ComponentType = type;
        }
        public Type ComponentType { get; }
        public int CharacterId { get; }
        public MethodDirection MethodDirection { get; }
    }
}
