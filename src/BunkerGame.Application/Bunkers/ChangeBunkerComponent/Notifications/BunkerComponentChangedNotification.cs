using BunkerGame.Domain.Bunkers.BunkerComponents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Bunkers.ChangeBunkerComponent.Notifications
{
    public class BunkerComponentChangedNotification<T> : INotification where T : BunkerComponent
    {
        public BunkerComponentChangedNotification(long gameSessionId, T component)
        {
            GameSessionId = gameSessionId;
            Component = component;
        }

        public long GameSessionId { get; }
        public T Component { get; }
    }
}
