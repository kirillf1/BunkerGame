using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Infrastructure.Domain.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.DatabaseTests
{
    public class EfRepositoryTests
    {
        [Fact]
        public async void GetCharacteristicBySpecificExpression()
        {
            using var context = DbCreator.CreateInMemoryContext();
            var efRepository = new CharacterComponentRepositoryEFBase<AdditionalInformation>(context);

            var addInf = new AdditionalInformation("test1", true);
            await efRepository.AddComponent(addInf);
            await efRepository.AddComponent(new AdditionalInformation("test2", true));
            await efRepository.CommitChanges(default);
            int? addInfId = addInf.Id;
            var addInfFromDb = await efRepository.GetCharacterComponent(false,c=>addInfId.HasValue ? c.Id == addInfId.Value : c.Id != addInf.Id);

            Assert.Equal(addInfId.Value, addInfFromDb.Id);

       
        }
    }
}
