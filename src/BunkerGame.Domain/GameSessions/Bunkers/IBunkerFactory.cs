using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions.Bunkers
{
    public interface IBunkerFactory
    {
        public Task<Bunker> CreateBunker();
    }
}
