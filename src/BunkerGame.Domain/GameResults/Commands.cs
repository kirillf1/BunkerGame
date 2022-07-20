using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameResults
{
    public static class Commands
    {
        public record CreateGameResult(GameSessionId GameSessionId,string Name) : IRequest;
        public record AddWinGame(GameSessionId GameSessionId) : IRequest;
        public record AddLoseGame(GameSessionId GameSessionId) : IRequest;
    }
}
