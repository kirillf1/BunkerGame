using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications
{
    public class BunkerComponentCollectionChangedNotification<T> : INotification where T : BunkerComponent
    {
        public BunkerComponentCollectionChangedNotification(long gameSessionId, IReadOnlyCollection<T> components)
        {
            GameSessionId = gameSessionId;
            Components = components;
        }

        public long GameSessionId { get; }
        public IReadOnlyCollection<T> Components { get; }
    }
}
