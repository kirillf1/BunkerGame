using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.ExchangeCharacter;

public class ExchangeCharacteristicCommand<T> : IRequest<Tuple<Character, Character>> where T : CharacterComponent
{
    public ExchangeCharacteristicCommand(int characterFirstId, int characterSecondId)
    {
        CharacterFirstId = characterFirstId;
        CharacterSecondId = characterSecondId;
    }
    public int CharacterFirstId { get; }
    public int CharacterSecondId { get; }
}
