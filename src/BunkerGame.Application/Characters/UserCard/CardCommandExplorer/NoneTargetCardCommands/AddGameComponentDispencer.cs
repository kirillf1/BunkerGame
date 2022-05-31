using BunkerGame.Application.GameSessions.AddExternalSurroundingInGame;
using BunkerGame.Application.GameSessions.ChangeFreePlace;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{
    internal class AddGameComponentDispencer : INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs)
        {
            var cardMethod = cardArgs.CardMethod;
            var directionGroup = cardMethod.DefineDirectionGroup();
            var gameSessionId = cardArgs.GameSessionId;
            switch (directionGroup)
            {
                case DirectionGroup.GameSession:
                    return GetAddGameSessionComponentCommand(gameSessionId, cardMethod);
            }
            return null;
        }
        private static object? GetAddGameSessionComponentCommand(long gameSessionId, CardMethod cardMethod)
        {
            if (cardMethod.MethodDirection == MethodDirection.ExternalSurrounding)
            {
                return new AddExternalSurroundingCommand(cardMethod.ItemId.GetValueOrDefault(), gameSessionId);
            }
            else if (cardMethod.MethodDirection == MethodDirection.FreePlace)
            {
                return new ChangeFreePlaceCommand(gameSessionId, true);
            }
            return null;
        }
    }
}
