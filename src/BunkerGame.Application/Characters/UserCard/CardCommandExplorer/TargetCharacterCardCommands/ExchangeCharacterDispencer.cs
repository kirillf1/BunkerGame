using BunkerGame.Application.Characters.ExchangeCharacteristic;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class ExchangeCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            var cardUserId = cardArgs.CardUserCharacterId;
            if (methodDirection == MethodDirection.Character)
            {
                //TODO
                // add change Character
                return null;
            }
            else
            {
                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicType(methodDirection.ToString());
                return new ExchangeCharacteristicCommand(targetCharacterId, cardUserId, characterComponentType!);
            }
        }
    }
}
