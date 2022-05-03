using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.ChangeCharacteristic
{
    public class ChangeCharacteristicCommand : IRequest<Character>
    {
        public ChangeCharacteristicCommand(int characterId, Type characteristicType)
        { 
            //if((characteristicType != typeof(CharacterComponent)) )
            //    throw new ArgumentException( nameof(characteristicType));
            CharacterId = characterId;
            CharacteristicType = characteristicType;
            
        }

        public int CharacterId { get; }
        public Type CharacteristicType { get; }
        public int? CharacteristicId { get; set; }
    }
}
