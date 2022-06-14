using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Characters.UserCard.CardCommandExplorer;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    public class ChangeCharacterCharactericitcCommandTests
    {
        [Fact]
        public async void ExecuteChangeCharacteristicCommand_Should_ChangeComponentToRandom()
        {
            await ExecuteChangeCharacteristicCommand(new AdditionalInformation("test", false),MethodDirection.AdditionalInformation);
            await ExecuteChangeCharacteristicCommand(new Trait("test", true),MethodDirection.Trait);
            await ExecuteChangeCharacteristicCommand(new Health("test", true), MethodDirection.Health);
            await ExecuteChangeCharacteristicCommand(new Size(150, 100), MethodDirection.Size);
            await ExecuteChangeCharacteristicCommand(new Profession("test",true), MethodDirection.Profession);
            //await ExecuteChangeCharacteristicCommand(new CharacterItem("test",true), MethodDirection.CharacterItem);
            await ExecuteChangeCharacteristicCommand(new Age(30), MethodDirection.Age);
            await ExecuteChangeCharacteristicCommand(new Phobia("test", true), MethodDirection.Phobia);
        }
        private async Task ExecuteChangeCharacteristicCommand<T>(T component,MethodDirection methodDirection) where T: CharacterComponent
        {
            var character = CharacterCreator.CreateCharacter();
            var characterRepository = GetCharacterRepository(character);
            var newCharacterComponent = component;
            var componentRepository = GetCharacterComponentRepository(newCharacterComponent);
            var mediator = new Mock<IMediator>().Object;

            var updateCharacterCommandHandler = new ChangeCharacteristicCommandHandler<T>(componentRepository,
                characterRepository, mediator);
            var command = ChangeCharacterComponentCommandFactory.CreateChangeCharacterComponentCommand(character.Id, null, methodDirection);
            var updatedCharacter = await updateCharacterCommandHandler.Handle((ChangeCharacteristicCommand<T>)command, default);

            Assert.Equal(GetCharacterComponent<T>(updatedCharacter), newCharacterComponent);
        }
        private ICharacterComponentRepository<T> GetCharacterComponentRepository<T>(T componentForReturn) where T : CharacterComponent
        {
            var repository = new Mock<ICharacterComponentRepository<T>>();
            repository.Setup(x => x.GetCharacterComponent(It.IsAny<bool>(), It.IsAny<Expression<Func<T, bool>>>())).ReturnsAsync(componentForReturn);
            return repository.Object;
        }
        private ICharacterRepository GetCharacterRepository(Character returnCharacter)
        {
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(c => c.GetCharacterById(returnCharacter.Id, true)).ReturnsAsync(returnCharacter);
            return characterRepository.Object;
        }
        private object GetCharacterComponent<T>(Character character)
        {
            var propertyName = typeof(T).Name;
            object characterComponent;
            if (typeof(T) == typeof(CharacterItem))
                characterComponent = character.CharacterItems;
            if (typeof(T) == typeof(Card))
                characterComponent = character.Cards;
            else
                characterComponent = character.GetType().GetProperty(propertyName)?.GetValue(character)!;
            return characterComponent;
        }
    }
}
