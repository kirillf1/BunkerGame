using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ChangeFreePlace
{
    public class BunkerSizeChangedNotification : INotification
    {
        public BunkerSizeChangedNotification(long gameSessionId, byte currentSize)
        {
            GameSessionId = gameSessionId;
            CurrentSize = currentSize;
        }

        public long GameSessionId { get; }
        public byte CurrentSize { get; }
    }
}
