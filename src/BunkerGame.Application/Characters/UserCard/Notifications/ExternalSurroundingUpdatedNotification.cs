using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class ExternalSurroundingUpdatedNotification : INotification
    {
        public ExternalSurroundingUpdatedNotification(long gameSessionId, ExternalSurrounding externalSurrounding)
        {
            GameSessionId = gameSessionId;
            ExternalSurrounding = externalSurrounding;
        }

        public long GameSessionId { get; }
        public ExternalSurrounding ExternalSurrounding { get; }
    }
}
