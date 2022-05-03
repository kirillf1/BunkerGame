using BunkerGame.Domain.GameSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ResultCounters
{
    public interface IResultCounterFactory
    {
        public IGameResultCounter CreateResultCounter(Difficulty difficulty);
    }
}
