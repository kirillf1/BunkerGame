using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.CharacterTests
{
    public class CharacterComponentTests
    {
        [Fact]
        public void UpcastCharacterComponent_ThenDowncast_ShouldReturnComponentValue()
        {
            var character = CharacterCreator.CreateCharacter();
            var addInf = character.AdditionalInformation;

            CharacterComponent upcastedCharacterComponent = addInf;
            var downcastedCharacterComponent = upcastedCharacterComponent as CharacterEntity;

            Assert.NotNull(downcastedCharacterComponent);
            Assert.Equal(downcastedCharacterComponent, addInf);

        }
        [Fact]
        public void UpcastCharacterComponent_ThenInvalidDowncast_ShouldReturnNull()
        {
            var character = CharacterCreator.CreateCharacter();
            var addInf = character.AdditionalInformation;

            CharacterComponent upcastedCharacterComponent = addInf;
            var downcastedCharacterComponent = upcastedCharacterComponent as Sex;

            Assert.Null(downcastedCharacterComponent);
            

        }
    }
}
