using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.ExchangeCharacteristic
{
    public class ExchangeCharacteristicCommand : IRequest<Tuple<Character, Character>>
    {
        public ExchangeCharacteristicCommand(int characterFirstId,int characterSecondId, Type characteristicType)
        {
            CharacterFirstId = characterFirstId;
            CharacterSecondId = characterSecondId;
            CharacteristicType = characteristicType;
        }

        public int CharacterFirstId { get; }
        public int CharacterSecondId { get; }
        public Type CharacteristicType { get; }
    }
}
