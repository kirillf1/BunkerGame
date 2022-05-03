using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions.ExternalSurroundings
{
    public interface IExternalSurroundingRepository
    {
        public Task<ExternalSurrounding?> GetExternalSurrounding(int id);
        public Task<IEnumerable<ExternalSurrounding>> GetExternalSurroundings(int skipCount, int count,Expression<Func<ExternalSurrounding, bool>>? predicate = null);
        public Task AddExternalSurrounding(ExternalSurrounding externalSurrounding);
        public Task RemoveGameSession(ExternalSurrounding externalSurrounding);
        public Task CommitChanges();
    }
}
