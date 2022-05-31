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
                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicType(methodDirection.ToString());
                return new ChangeCharacteristicCommand(targetCharacterId, characterComponentType!)
                {
                    CharacteristicId = componentId
                };
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
