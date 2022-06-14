using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Application.GameSessions.ChangeBunker;
using BunkerGame.Application.GameSessions.ChangeCatastophe;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{
    internal class ChangeGameComponentDispencer : INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs)
        {
            var cardMethod = cardArgs.CardMethod;
            var directionGroup = cardMethod.DefineDirectionGroup();
            var gameSessionId = cardArgs.GameSessionId;
            switch (directionGroup)
            {
                case DirectionGroup.Bunker:
                    return GetBunkerUpdateCommand(gameSessionId, cardMethod);
                case DirectionGroup.Catastrophe:
                    return new ChangeCatastropheCommand(gameSessionId);
            }
            return null;
        }
        private object? GetBunkerUpdateCommand(long gameSessionId, CardMethod cardMethod)
        {
            var methodDirection = cardMethod.MethodDirection;
            if (methodDirection == MethodDirection.Bunker)
            {
                return new ChangeBunkerCommand(gameSessionId);
            }
            else
            {
                var componentId = cardMethod.ItemId;
                return ChangeBunkerComponentCommandFactory.CreateChangeBunkerComponentCommand(gameSessionId,componentId,methodDirection);
            }
        }
    }
}
