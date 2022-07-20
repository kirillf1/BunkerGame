using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Framework
{
    public abstract class Entity<TId> where TId : Value<TId>
    {
        protected Entity() { }
        protected Entity(TId id)
        {
            Id = id;
        }
        public TId Id { get; protected set; }
    }
}
