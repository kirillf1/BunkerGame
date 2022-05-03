using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class AgeMockRepository : MockRepository<Age>
    {
        Random Random = new Random();
        public override async Task<Age> GetCharacterComponent(bool needShuffle = true, Expression<Func<Age, bool>>? predicate = null)
        {
            return await Task.Run(() => new Age(Random.Next(15, 63)));
        }

        public async override Task<IEnumerable<Age>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<Age, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                var list = new List<Age>(count);
                for (int i = 0; i <= count; i++)
                {
                    list.Add(new Age(Random.Next(18, 70)));
                }
                return list;
            });
        }
    }
}
