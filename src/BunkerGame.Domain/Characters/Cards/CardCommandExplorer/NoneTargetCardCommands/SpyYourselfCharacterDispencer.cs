using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.GameTypes.CharacterTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands
{
    internal class SpyYourselfCharacterDispencer : INoneTargetCardCommandDispenser
    {

        public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var characterId = cardArgs.CardUserCharacterId;
            if (methodDirection != MethodDirection.Character)
            {
                SpyCharacterComponentCommandFactory.CreateSpyCharacterComponentCommand(characterId, methodDirection, resultBuilder);
            }
            else
            {
                resultBuilder.AddCommand(new Commands.UncoverCharacter(characterId));
            }
        }
    }
}
