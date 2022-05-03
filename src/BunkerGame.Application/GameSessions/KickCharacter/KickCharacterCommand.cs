using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.KickCharacter
{
    public class KickCharacterCommand: IRequest<Character>
    {
        public KickCharacterCommand(long gameSessionId, int characterId)
        {
            GameSessionId = gameSessionId;
            CharacterId = characterId;
        }

        public long GameSessionId { get; }
        public int CharacterId { get; }
    }
}
