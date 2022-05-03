using BunkerGame.Domain.Bunkers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class BunkerUpdatedNotification : INotification
    {
        public BunkerUpdatedNotification(long gameSessionId, Bunker bunker)
        {
            GameSessionId = gameSessionId;
            Bunker = bunker;
        }
        public long GameSessionId { get; }
        public Bunker Bunker { get; }
    }
}
