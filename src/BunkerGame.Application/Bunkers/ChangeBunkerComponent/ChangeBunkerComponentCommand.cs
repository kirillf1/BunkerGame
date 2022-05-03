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
    public class ChangeBunkerComponentCommand : IRequest<Bunker>
    {
        public ChangeBunkerComponentCommand(long gameSessionId, Type bunkerComponentType, int? bunkerComponentId = null)
        {
            //if (bunkerComponentType != typeof(BunkerComponentEntity))
            //    throw new ArgumentException("Incorerrect bunkerCopmonentType");
            GameSessionId = gameSessionId;
            BunkerComponentType = bunkerComponentType;
            BunkerComponentId= bunkerComponentId;
        }
        public int? BunkerComponentId { get; }
        public long GameSessionId { get; }
        public Type BunkerComponentType { get; }
    }
}
