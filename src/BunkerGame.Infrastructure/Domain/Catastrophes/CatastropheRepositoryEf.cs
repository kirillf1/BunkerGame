using BunkerGame.Domain.Catastrophes;
using BunkerGame.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.Catastrophes
{
    public class CatastropheRepositoryEf : ICatastropheRepository
    {
        private readonly BunkerGameDbContext bunkerGameDbContext;

        public CatastropheRepositoryEf(BunkerGameDbContext bunkerGameDbContext)
        {
            this.bunkerGameDbContext = bunkerGameDbContext;
        }
        public async Task AddCatastrophe(Catastrophe catastrophe)
        {
           await  bunkerGameDbContext.Catastrophes.AddAsync(catastrophe);
        }

        public async Task CommitChanges()
        {
            await bunkerGameDbContext.SaveChangesAsync();
        }

        public async Task<Catastrophe> GetCatastropheById(int id)
        {
            return await bunkerGameDbContext.Catastrophes.FirstAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Catastrophe>> GetCatastrophes(int skipCount, int count, Expression<Func<Catastrophe, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.Catastrophes.AsQueryable();
            if(predicate!= null)
                query = query.Where(predicate);
            return await query.Skip(skipCount).Take(count).ToListAsync();
        }

        public async Task<Catastrophe> GetRandomCatastrophe(Expression<Func<Catastrophe, bool>>? predicate = null)
        {
            var query = bunkerGameDbContext.Catastrophes.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.OrderBy(c=> Guid.NewGuid()).FirstAsync();
        }

        public Task RemoveCatastrophe(Catastrophe catastrophe)
        {
            bunkerGameDbContext.Catastrophes.Remove(catastrophe);
            return Task.CompletedTask;
        }
    }
}
