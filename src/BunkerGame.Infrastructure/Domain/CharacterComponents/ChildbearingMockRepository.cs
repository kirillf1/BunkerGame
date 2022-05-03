using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class ChildbearingMockRepository : MockRepository<Childbearing>
    {
        Random Random = new Random();
        public override async Task<Childbearing> GetCharacterComponent(bool needShuffle = true, Expression<Func<Childbearing, bool>>? predicate = null)
        {
           return await  Task.Run(() =>  new Childbearing(Random.Next(0, 100) >= 35));
        }

        public override async Task<IEnumerable<Childbearing>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<Childbearing, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                var list = new List<Childbearing>(count);
                for (int i = 0; i <= count; i++)
                {
                    list.Add(new Childbearing(Random.Next(0, 100) >= 35));
                }
                return list;
            });
        }
    }
}
