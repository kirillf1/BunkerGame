using BunkerGame.Application.GameSessions.ResultCounters;
using BunkerGame.Domain.GameSessions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.EndGame
{
    public class EndGameCommand : IRequest<ResultGameReport>
    {
        public EndGameCommand(long gameSessionId)
        {
            GameSessionId = gameSessionId;
         
        }

        public long GameSessionId { get; }
        
    }
}
