using BunkerGame.Application.Characters.SpyCharacterComponent;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class SpyOnCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            if (methodDirection != MethodDirection.Character)
            {
               return new SpyCharacterComponentCommand(targetCharacterId, methodDirection);
               
            }
            else
            {
                // add spy all character
                return null;
            }
        }
    }
}
