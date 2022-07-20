using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Framework
{
    public interface IEventStore
    {
        public Task<IEnumerable<INotification>> ExtractNotifications();
        public Task Save<TId>(AggregateRoot<TId> aggregate);
    }
}
