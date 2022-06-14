using BunkerGame.Application.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.CharacterTests
{
    public class CharacterProxyTests
    {
        [Fact]
        public void ExtractCharacterComponent_FromCharacterProxy_ShouldEqualComponentFromCharacter()
        {
            var character = CharacterCreator.CreateCharacter();
            var characterProxy = new CharacterProxy(character);

            var hobby = characterProxy.GetCharacterComponent<Hobby>();
            var additionalInformation = characterProxy.GetCharacterComponent<AdditionalInformation>();
            var profession = characterProxy.GetCharacterComponent<Profession>();
            var cards = characterProxy.GetCharacterComponentCollection<Card>();

            Assert.Equal(hobby.Component, character.Hobby);
            Assert.Equal(additionalInformation.Component, character.AdditionalInformation);
            Assert.Equal(profession.Component, character.Profession);
            Assert.Equal(cards.Components, character.Cards);
        }
        [Fact]
        public void TryExtractNotExisting_CharacterComponent_FromCharacterProxy_ShouldNotImplementedException()
        {
            var character = CharacterCreator.CreateCharacter();
            
            var characterProxy = new CharacterProxy(character);

            Assert.Throws<NotImplementedException>(() => characterProxy.GetCharacterComponent<NotExistingCharacterComponent>());
        }
        private class NotExistingCharacterComponent : CharacterComponent
        {

        }
    }
}
