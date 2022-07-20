using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class SpyOnCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public void GiveCommandHandler(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            if (methodDirection != MethodDirection.Character)
            {
                FindCommandForCharacterComponent(targetCharacterId, methodDirection, resultBuilder);

            }
            else
            {
                resultBuilder.AddCommand(new Commands.UncoverCharacter(cardArgs.TargetCharacterId));
            }
        }
        private static void FindCommandForCharacterComponent(CharacterId characterId, MethodDirection methodDirection, CardCommandResultBuilder resultBuilder)
        {
            SpyCharacterComponentCommandFactory.CreateSpyCharacterComponentCommand(characterId, methodDirection, resultBuilder);
        }
    }
}
