using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Catastrophes
{
    public interface ICatastropheRepository
    {
        public Task<Catastrophe> GetCatastropheById(int id);
        public Task<Catastrophe> GetRandomCatastrophe(Expression<Func<Catastrophe, bool>>? predicate = null);
        public Task<IEnumerable<Catastrophe>> GetCatastrophes(int skipCount, int count, Expression<Func<Catastrophe, bool>>? predicate = null);
        public Task AddCatastrophe(Catastrophe catastrophe);
        public Task RemoveCatastrophe(Catastrophe catastrophe);
        public Task CommitChanges();
    }
}
