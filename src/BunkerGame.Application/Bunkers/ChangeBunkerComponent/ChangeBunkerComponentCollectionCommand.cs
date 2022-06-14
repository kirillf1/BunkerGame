using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent
{
    public class ChangeBunkerComponentCollectionCommand<T> : IRequest<IReadOnlyCollection<T>> where T : BunkerComponent
    {
        public ChangeBunkerComponentCollectionCommand(long gameSessionId, int? bunkerComponentId = null)
        {
            GameSessionId = gameSessionId;
            BunkerComponentId = bunkerComponentId;
        }
        public int? BunkerComponentId { get; }
        public long GameSessionId { get; }
    }
}
