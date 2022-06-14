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
    public class ChangeCharacteristicCommand<T> : IRequest<Character> where T : CharacterComponent
    {
        public ChangeCharacteristicCommand(int characterId,int? characterComponentId, 
            Func<Character,int?, ICharacterComponentRepository<T>,Task<T>> changeMethod)
        {
            CharacterId = characterId;
            CharacterComponentId = characterComponentId;
            ChangeMethod = changeMethod;
        }
        public Func<Character,int?,ICharacterComponentRepository<T>,Task<T>> ChangeMethod { get; }
        public int CharacterId { get; }
        public int? CharacterComponentId { get; }
    }
}
