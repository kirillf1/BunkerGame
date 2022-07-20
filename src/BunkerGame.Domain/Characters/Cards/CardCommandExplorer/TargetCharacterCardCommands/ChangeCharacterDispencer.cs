using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class ChangeCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public void GiveCommandHandler(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;

            if (methodDirection != MethodDirection.Character)
            {
                var component = cardArgs.CardMethod.Item;
                ChangeCharacterComponentCommandFactory.CreateChangeCharacterComponentCommand(targetCharacterId, component, methodDirection,
                    resultBuilder);
            }
            else
            {

            }
        }
    }
}
