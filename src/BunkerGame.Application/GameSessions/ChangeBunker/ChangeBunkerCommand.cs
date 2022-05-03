using BunkerGame.Domain.Bunkers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ChangeBunker
{
    public class ChangeBunkerCommand : IRequest<Bunker>
    {
        public ChangeBunkerCommand(long gameSessionId)
        {
            GameSessionId = gameSessionId;
        }

        public long GameSessionId { get; }
    }
}
