using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Application.Characters.SpyCharacterComponent.SpyComponentCommandHandlers;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    public class SpyCharacterComponentCommandTests
    {

        public async Task SpyCharacterComponentHelper<T>(SpyCharacterComponentCommandHandler<T> commandHandler, Character character) where T : CharacterComponent
        {
            var result = await commandHandler.Handle(new SpyCharacterComponentCommand<T>(character.Id), default);
            var propertyName = typeof(T).Name;
            object? characterComponent;
            // we can spy only one component
            if (typeof(T) == typeof(CharacterItem))
                characterComponent = character.CharacterItems.FirstOrDefault(c => result == c);
            else if (typeof(T) == typeof(Card))
                characterComponent = character.Cards.FirstOrDefault(c => result == c);
            else
                characterComponent = character.GetType().GetProperty(propertyName)?.GetValue(character);

            Assert.NotNull(characterComponent);
            Assert.Equal(result, characterComponent);
        }
        [Fact]
        public async Task SpyCharacterComponents_ShouldReturnSameComponent()
        {
            var character = CharacterCreator.CreateCharacter();
            var characterRepository = new Mock<ICharacterRepository>();
            characterRepository.Setup(c => c.GetCharacterById(character.Id, true)).ReturnsAsync(character);
            var mediator = new Mock<IMediator>().Object;

            await SpyCharacterComponentHelper(new SpySexCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpySizeCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyAgeCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyCardCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyCharacterItemCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyProfessionCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyHobbyCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyTraitCommandHandler(characterRepository.Object,mediator), character);
            await SpyCharacterComponentHelper(new SpyHealthCommandHandler(characterRepository.Object,mediator), character);
        }
    }
}
