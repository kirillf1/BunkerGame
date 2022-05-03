using BunkerGame.Domain.Catastrophes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ChangeCatastophe
{
    public class ChangeCatastropheCommand : IRequest<Catastrophe>
    {
        public ChangeCatastropheCommand(long gameSessionId)
        {
            GameSessionId = gameSessionId;
        }

        public long GameSessionId { get; }
    }
}
