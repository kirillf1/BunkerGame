using BunkerGame.Application.Characters.ExchangeCharacter;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    public class ExchangeCharacterComponentCommandTests
    {
        [Fact]
        public async void ExecuteExchangeCharacterComponentCommand_ShouldExchangeComponent()
        {
            await ExecuteExchangeCharacterComponentCommand<AdditionalInformation>();
            await ExecuteExchangeCharacterComponentCommand<Hobby>();
            await ExecuteExchangeCharacterComponentCommand<Card>();
        }
        private async Task ExecuteExchangeCharacterComponentCommand<T>() where T : CharacterComponent
        {
            var characterFirst = CharacterCreator.CreateCharacter();
            var characterSecond = CharacterCreator.CreateCharacter();
            var mediator = new Mock<IMediator>().Object;
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.SetupSequence(c => c.GetCharacterById(characterFirst.Id, true)).ReturnsAsync(characterFirst).ReturnsAsync(characterSecond);
            var exchangeCharacterComponentCommandHandler = new ExchangeCharacteristicCommandHandler<T>(characterRepository.Object, mediator);

            var componentFirst = GetCharacterComponent<T>(characterFirst);
            var componentSecond = GetCharacterComponent<T>(characterSecond);
            
            var characters = await exchangeCharacterComponentCommandHandler.Handle(
                new ExchangeCharacteristicCommand<T>(characterFirst.Id, characterSecond.Id), default);

            Assert.Equal(characterFirst, characters.Item1);
            Assert.Equal(characterSecond, characters.Item2);
            Assert.Equal(GetCharacterComponent<T>(characterFirst), componentSecond);
            Assert.Equal(GetCharacterComponent<T>(characterSecond), componentFirst);
        }
        private object GetCharacterComponent<T>(Character character)
        {
            var propertyName = typeof(T).Name;
            object characterComponent;
            if (typeof(T) == typeof(CharacterItem))
                characterComponent = character.CharacterItems;
            else if (typeof(T) == typeof(Card))
                characterComponent = character.Cards;
            else
                characterComponent = character.GetType().GetProperty(propertyName)?.GetValue(character)!;
            return characterComponent;
        }
    }
}
