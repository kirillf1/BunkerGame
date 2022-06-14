using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.KickCharacter
{
    public class CharacterKickedNotification : INotification
    {
        public CharacterKickedNotification(long gameSessionId, Character character)
        {
            GameSessionId = gameSessionId;
            Character = character;
        }

        public long GameSessionId { get; }
        public Character Character { get; }
    }
}
