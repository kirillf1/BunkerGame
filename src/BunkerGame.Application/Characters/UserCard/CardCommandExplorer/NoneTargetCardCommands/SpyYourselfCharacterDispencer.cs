using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{
    internal class SpyYourselfCharacterDispencer : INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var characterId = cardArgs.CardUserCharacterId;
            if (methodDirection != MethodDirection.Character)
            {
                return SpyCharacterComponentCommandFactory.CreateSpyCharacterComponentCommand(characterId, methodDirection);
            }
            else
            {
                return null;
                // add spy all character
            }
        }
    }
}
