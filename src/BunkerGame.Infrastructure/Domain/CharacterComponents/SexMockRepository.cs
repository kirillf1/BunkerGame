using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class SexMockRepository : MockRepository<Sex>
    {
        Random Random = new Random();

        public override async Task<Sex> GetCharacterComponent(bool needShuffle = true, Expression<Func<Sex, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                if (!needShuffle)
                    return new Sex();
                return new Sex(Random.Next(0, 100) >= 50 ? "мужчина" : "женщина");
            });
        }
        public override async Task<IEnumerable<Sex>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<Sex, bool>>? predicate = null)
        {
            return await Task.Run(() =>
            {
                var list = new List<Sex>(count);
                for (int i = 0; i <= count; i++)
                {
                    list.Add(new Sex(Random.Next(0, 100) >= 50 ? "мужчина" : "женщина"));
                }
                return list;
               
            });
        }
    }
}
