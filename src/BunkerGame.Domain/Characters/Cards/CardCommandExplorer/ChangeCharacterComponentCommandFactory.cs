using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.GameTypes.CharacterTypes;
using MediatR;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer
{
    public static class ChangeCharacterComponentCommandFactory
    {
        private static Dictionary<MethodDirection, Func<CharacterId, object?, CardCommandResultBuilder, IRequest>> ChangeCharacterComponentCommands;
        static ChangeCharacterComponentCommandFactory()
        {
            ChangeCharacterComponentCommands = new();
            InitChangeComponentCommands();
        }
        public static void CreateChangeCharacterComponentCommand(CharacterId characterId, object? component, MethodDirection methodDirection
            , CardCommandResultBuilder resultBuilder)
        {
            if (ChangeCharacterComponentCommands.TryGetValue(methodDirection, out var command))
            {
                resultBuilder.AddCommand(command.Invoke(characterId, component, resultBuilder));
                return;
            }
            resultBuilder.AddError(CardExecuteError.NoSuchCommand);

        }

        private static void InitChangeComponentCommands()
        {
            ChangeCharacterComponentCommands[MethodDirection.Trait] =
                (characterId, component, resultBuilder) => new Commands.UpdateTrait(characterId, TryConvertComponent<Trait>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.AdditionalInformation] =
                (characterId, component, resultBuilder) => new Commands.UpdateAdditionalInformation(characterId, TryConvertComponent<AdditionalInformation>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Health] =
                (characterId, component, resultBuilder) => new Commands.UpdateHealth(characterId, TryConvertComponent<Health>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Hobby] =
                (characterId, component, resultBuilder) => new Commands.UpdateHobby(characterId, TryConvertComponent<Hobby>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.CharacterItem] =
                (characterId, component, resultBuilder) => new Commands.UpdateItem(characterId, TryConvertComponent<Item>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Profession] =
                (characterId, component, resultBuilder) => new Commands.UpdateProfession(characterId, TryConvertComponent<Profession>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Phobia] =
                (characterId, component, resultBuilder) => new Commands.UpdatePhobia(characterId, TryConvertComponent<Phobia>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Sex] =
                (characterId, component, resultBuilder) => new Commands.UpdateSex(characterId, TryConvertComponent<Sex>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Age] =
                (characterId, component, resultBuilder) => new Commands.UpdateAge(characterId, TryConvertComponent<Age>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Childbearing] =
               (characterId, component, resultBuilder) => new Commands.UpdateChildbearing(characterId, TryConvertComponent<Childbearing>(resultBuilder, component));
            ChangeCharacterComponentCommands[MethodDirection.Size] =
                (characterId, component, resultBuilder) => new Commands.UpdateSize(characterId, TryConvertComponent<Size>(resultBuilder, component));
        }
        private static T? TryConvertComponent<T>(CardCommandResultBuilder resultBuilder, object? component)
        {
            if (component == null)
                return default;
            if (component is T t)
                return t;
            resultBuilder.AddError(CardExecuteError.InvalidComponentType);
            return default;

        }
    }
}
