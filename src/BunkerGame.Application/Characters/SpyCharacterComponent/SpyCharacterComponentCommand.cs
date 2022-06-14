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
    public class SpyCharacterComponentCommand<T> : IRequest<T> where T : CharacterComponent
    {
        public SpyCharacterComponentCommand(int characterId)
        {
            CharacterId = characterId;
            
        }
        public int CharacterId { get; }
    }
}
