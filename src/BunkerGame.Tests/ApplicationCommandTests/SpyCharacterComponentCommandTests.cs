using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    public class SpyCharacterComponentCommandTests
    {
        [Theory]
        [InlineData(MethodDirection.Health)]
        [InlineData(MethodDirection.Sex)]
        [InlineData(MethodDirection.Size)]
        [InlineData(MethodDirection.Phobia)]
        [InlineData(MethodDirection.Profession)]
        [InlineData(MethodDirection.Hobby)]
        [InlineData(MethodDirection.Trait)]
        [InlineData(MethodDirection.CharacterItem)]
        [InlineData(MethodDirection.Age)]
        [InlineData(MethodDirection.AdditionalInformation)]
        
        public async Task SpyCharacterComponent_ShouldReturnSame(MethodDirection methodDirection)
        {
            var character = CharacterCreator.CreateCharacter();
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(c=>c.GetCharacterById(character.Id,true)).ReturnsAsync(character);

            var spyCharacterComponentCommandHandler = new SpyCharacterComponentCommandHandler(characterRepository.Object);
            var result = await spyCharacterComponentCommandHandler.Handle(new SpyCharacterComponentCommand(character.Id, methodDirection),default);

            Assert.Equal(result.ToString(), GetCharacterComponentDescription(character, methodDirection).ToString());
        }
        public static CharacterComponent GetCharacterComponentDescription(Character character, MethodDirection methodDirection)
        {
           return methodDirection switch
            {
                MethodDirection.AdditionalInformation => character.AdditionalInformation,
                MethodDirection.Health => character.Health,
                MethodDirection.Profession => character.Profession,
                MethodDirection.Phobia => character.Phobia,
                MethodDirection.Sex => character.Sex,
                MethodDirection.Size => character.Size,
                MethodDirection.Trait => character.Trait,
                MethodDirection.Hobby => character.Hobby,
                MethodDirection.Age => character.Age,
                MethodDirection.CharacterItem => character.CharacterItems.First(),
                MethodDirection.Childbearing => character.Childbearing,
                _ => throw new ArgumentException(),
            };
            
        }
    }
}
