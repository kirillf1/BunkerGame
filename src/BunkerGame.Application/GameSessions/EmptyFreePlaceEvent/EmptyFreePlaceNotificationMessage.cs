using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.EmptyFreePlaceEvent
{
    public class EmptyFreePlaceNotificationMessage: INotification
    {
        public EmptyFreePlaceNotificationMessage(long gameSessionId, byte currentSize)
        {
            GameSessionId = gameSessionId;
            CurrentSize = currentSize;
        }
        public long GameSessionId { get; }
        public byte CurrentSize { get; }
    }
}
