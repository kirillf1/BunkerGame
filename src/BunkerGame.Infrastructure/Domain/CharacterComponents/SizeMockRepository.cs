using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.Domain.CharacterComponents
{
    public class SizeMockRepository : MockRepository<Size>
    {
        Random Random = new Random();
        public override async Task<Size> GetCharacterComponent(bool needShuffle = true, Expression<Func<Size, bool>>? predicate = null)
        {
            return await Task.Run(() =>
             {
                 var height = Random.Next(140, 200);
                 var weight = GenerateWeight(height);
                 return new Size(height, weight);
             });
        }


        public override async Task<IEnumerable<Size>> GetCharacterComponents(int count, bool needShuffle = true, Expression<Func<Size, bool>>? predicate = null)
        {
            return await Task.Run(() =>
             {
                 var sizeList = new List<Size>(count);
                 for (int i = 0; i <= count; i++)
                 {
                     var height = Random.Next(140, 200);
                     var weight = GenerateWeight(height);
                     sizeList.Add(new Size(height, weight));
                 }
                 return sizeList;
             });
        }
        int GenerateWeight(int height)
        {
            int minWeight;
            int maxWeight;
            switch (height)
            {
                case int h when (h < 160):
                    maxWeight = 90;
                    minWeight = 40;
                    break;
                case int h when (h > 160 && h < 190):
                    maxWeight = 130;
                    minWeight = 60;
                    break;
                default:
                    minWeight = 70;
                    maxWeight = 170;
                    break;
            }
            return Random.Next(minWeight, maxWeight);
        }
    }
}
