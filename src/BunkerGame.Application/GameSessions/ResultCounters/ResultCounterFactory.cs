using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ResultCounters
{
    public class ResultCounterFactory : IResultCounterFactory
    {
        public IGameResultCounter CreateResultCounter(Difficulty difficulty)
        {
            return new GameResultCounterEasy();
        }
    }
}
