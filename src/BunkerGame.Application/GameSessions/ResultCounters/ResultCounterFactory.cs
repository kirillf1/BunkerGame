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
        public IGameResultCounter CreateResultCounter(GameSession gameSession)
        {
            switch (gameSession.Difficulty)
            {
                case Difficulty.Easy:
                    return new GameResultCounterEasy(gameSession);
                case Difficulty.Medium:
                    return new GameResultCounterMedium(gameSession);
                case Difficulty.Hard:
                    return new GameResultCounterMedium(gameSession);
                default:
                    throw new NotImplementedException($"No such difficullty in {nameof(gameSession)}");
            }
        }
    }
}
