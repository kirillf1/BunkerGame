using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.SpyCharacterComponent
{
    public class SpiedCharacterComponentNotification<T> : INotification
    {
        public SpiedCharacterComponentNotification(int characterId, long gameSessionId, T characterComponent)
        {
            CharacterId = characterId;
            GameSessionId = gameSessionId;
            CharacterComponent = characterComponent;
        }

        public int CharacterId { get; }
        public long GameSessionId { get; }
        public T CharacterComponent { get; }
    }
}
