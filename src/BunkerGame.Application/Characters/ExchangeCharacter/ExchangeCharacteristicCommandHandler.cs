using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Application.Characters.ExchangeCharacter
{
    public class ExchangeCharacteristicCommandHandler<T> : IRequestHandler<ExchangeCharacteristicCommand<T>, Tuple<Character, Character>>
        where T : CharacterComponent
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IMediator mediator;
        public ExchangeCharacteristicCommandHandler(ICharacterRepository characterRepository,IMediator mediator)
        {
            this.characterRepository = characterRepository;
            this.mediator = mediator;
        }
        public async Task<Tuple<Character, Character>> Handle(ExchangeCharacteristicCommand<T> command,
            CancellationToken cancellationToken)
        {
            var characterFirst = await characterRepository.GetCharacterById(command.CharacterFirstId);
            var characterSecond = await characterRepository.GetCharacterById(command.CharacterSecondId);
            if (characterFirst == null || characterSecond == null)
                throw new ArgumentNullException(nameof(characterFirst));
            ExchangeCharacterComponents(characterFirst, characterSecond);
            await characterRepository.CommitChanges();
            await mediator.Publish(new CharactersExchangedNotification(characterFirst, characterSecond), cancellationToken);
            return new Tuple<Character, Character>(characterFirst, characterSecond);
        }
        private void ExchangeCharacterComponents(Character characterFirst, Character characterSecond)
        {
            var characterFirstProxy = new CharacterProxy(characterFirst);
            var characterSecondProxy = new CharacterProxy(characterSecond);
            if (characterFirstProxy.IsContainsCharacterComponent<T>() && characterSecondProxy.IsContainsCharacterComponent<T>())
            {
                ExchangeComponent(characterFirstProxy, characterSecondProxy);
            }
            else if (characterFirstProxy.IsContainsCharacterComponentCollection<T>()
                && characterSecondProxy.IsContainsCharacterComponentCollection<T>())
            {
                ExchangeComponentCollection(characterFirstProxy, characterSecondProxy);
            }
            else
            {
                throw new NotImplementedException($"Character does not contain a property {typeof(T).Name}");
            }
        }
        private void ExchangeComponent(CharacterProxy characterFirst, CharacterProxy characterSecond)
        {
            var componentsFirst = characterFirst.GetCharacterComponent<T>();
            var componentsSecond = characterSecond.GetCharacterComponent<T>();
            Swap(componentsSecond, componentsFirst);
        }
        private void ExchangeComponentCollection(CharacterProxy characterFirst, CharacterProxy characterSecond)
        {
            var componentsFirst = characterFirst.GetCharacterComponentCollection<T>();
            var componentsSecond = characterSecond.GetCharacterComponentCollection<T>();
            Swap(componentsFirst, componentsSecond);

        }
        private static void Swap(ICharacterComponent<T> componentFirst, ICharacterComponent<T> componentSecond)
        {
            var componentTemp = componentFirst.Component;
            componentFirst.Component = componentSecond.Component;
            componentSecond.Component = componentTemp;
        }
        private static void Swap(ICharacterComponentCollection<T> componentFirst, ICharacterComponentCollection<T> componentSecond)
        {
            var componentTemp = componentFirst.Components;
            componentFirst.Components = componentSecond.Components;
            componentSecond.Components = componentTemp;
        }
    }
}
