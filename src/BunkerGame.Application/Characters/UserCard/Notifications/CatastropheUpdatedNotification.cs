using BunkerGame.Domain.Catastrophes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class CatastropheUpdatedNotification : INotification
    {
        public CatastropheUpdatedNotification(long gameSessionId, Catastrophe catastrophe)
        {
            GameSessionId = gameSessionId;
            Catastrophe = catastrophe;
        }

        public long GameSessionId { get; }
        public Catastrophe Catastrophe { get; }
    }
}
