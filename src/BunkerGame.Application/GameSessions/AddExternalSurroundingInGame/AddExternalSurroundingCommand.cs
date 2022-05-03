using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.AddExternalSurroundingInGame
{
    public class AddExternalSurroundingCommand : IRequest<ExternalSurrounding>
    {
        public AddExternalSurroundingCommand(int surroundingId,long gameSessionId)
        {
            SurroundingId = surroundingId;
            GameSessionId = gameSessionId;
        }

        public int SurroundingId { get; }
        public long GameSessionId { get; }
    }
}
