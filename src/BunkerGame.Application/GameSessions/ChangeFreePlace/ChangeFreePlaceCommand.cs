using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ChangeFreePlace
{
    public class ChangeFreePlaceCommand : IRequest<byte>
    {
        public bool SizeIncreased { get; }
        public long GameSessionId { get; }

        public ChangeFreePlaceCommand(long gameSessionId, bool sizeIncreased)
        {
            GameSessionId = gameSessionId;
            SizeIncreased = sizeIncreased;
        }
    }
}
