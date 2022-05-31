using BunkerGame.Application.GameSessions.ChangeFreePlace;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{
    internal class RemoveGameComponentDispencer : INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var gameSessionId = cardArgs.GameSessionId;
            switch (methodDirection)
            {
                case MethodDirection.FreePlace:
                    return new ChangeFreePlaceCommand(gameSessionId, false);
            }
            return null;
        }
    }
}
