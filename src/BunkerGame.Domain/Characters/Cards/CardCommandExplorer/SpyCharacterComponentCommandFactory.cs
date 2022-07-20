using BunkerGame.GameTypes.CharacterTypes;
using MediatR;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer
{
    internal static class SpyCharacterComponentCommandFactory
    {
        private static Dictionary<MethodDirection, Func<CharacterId, IRequest>> spyCommands;

        static SpyCharacterComponentCommandFactory()
        {
            spyCommands = new();
            InitSpyCommands();
        }

        public static void CreateSpyCharacterComponentCommand(CharacterId characterId, MethodDirection methodDirection, CardCommandResultBuilder resultBuilder)
        {
            if (!spyCommands.TryGetValue(methodDirection, out var command))
            {
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                return;
            }
            resultBuilder.AddCommand(command.Invoke(characterId));
        }


        private static void InitSpyCommands()
        {
            spyCommands[MethodDirection.Trait] = (characterId) => new Commands.UncoverTrait(characterId);
            spyCommands[MethodDirection.Sex] = (id) => new Commands.UncoverSex(id);
            spyCommands[MethodDirection.CharacterItem] = (id) => new Commands.UncoverItems(id);
            spyCommands[MethodDirection.Childbearing] = (id) => new Commands.UncoverChildbearing(id);
            spyCommands[MethodDirection.AdditionalInformation] = (id) => new Commands.UncoverAdditionalInformation(id);
            spyCommands[MethodDirection.Phobia] = (id) => new Commands.UncoverPhobia(id);
            spyCommands[MethodDirection.Age] = (id) => new Commands.UncoverAge(id);
            spyCommands[MethodDirection.Size] = (id) => new Commands.UncoverSize(id);
            spyCommands[MethodDirection.Health] = (id) => new Commands.UncoverHealth(id);
            spyCommands[MethodDirection.Hobby] = (id) => new Commands.UncoverHobby(id);
        }
    }
}
