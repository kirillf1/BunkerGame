using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent
{
    public class ChangeBunkerComponentCommand<T> : IRequest<T> where T : BunkerComponent
    {
        public ChangeBunkerComponentCommand(long gameSessionId, int? bunkerComponentId = null)
        {
            GameSessionId = gameSessionId;
            BunkerComponentId= bunkerComponentId;
        }
        public int? BunkerComponentId { get; }
        public long GameSessionId { get; }
    }
}
