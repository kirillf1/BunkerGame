using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class ChangeCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            if (methodDirection != MethodDirection.Character)
            {
                int? componentId = cardArgs.CardMethod.ItemId;
                return ChangeCharacterComponentCommandFactory.CreateChangeCharacterComponentCommand(targetCharacterId, componentId, methodDirection);
            }
            else
            {
                //TODO
                // Change Character 
                return null;
            }
        }
    }
}
