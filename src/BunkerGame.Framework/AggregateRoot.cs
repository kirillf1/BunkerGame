using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Framework
{
    public abstract class AggregateRoot<TId>
    {
        private List<INotification> events;
        protected AggregateRoot(TId id)
        {
            Id = id;
            events = new();
        }
        protected AggregateRoot()
        {
            events = new();
        }
        public void AddEvent(INotification @event)
        {
            events.Add(@event);
        }
        public void RemoveEvent(INotification @event)
        {
            events.Remove(@event);
        }
        public void ClearEvents()
        {
            events.Clear();
        }
        public IReadOnlyCollection<INotification> GetEvents()
        {
            return events;
        }
        public TId Id { get; protected set; }
    }
}
