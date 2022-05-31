using BunkerGame.Application.GameSessions.KickCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class KickCharacterCommandDispencer : ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs)
        {
            var gameSessionId = cardArgs.GameSessionId;
            var targetCharacterId = cardArgs.TargetCharacterId;
            return new KickCharacterCommand(gameSessionId, targetCharacterId);
        }
    }
}
