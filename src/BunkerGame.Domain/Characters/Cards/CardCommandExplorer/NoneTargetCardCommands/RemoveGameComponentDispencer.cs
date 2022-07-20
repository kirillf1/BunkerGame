using BunkerGame.Domain.Characters.Cards.CardCommandExplorer;
using BunkerGame.GameTypes.CharacterTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands;

internal class RemoveGameComponentDispencer : INoneTargetCardCommandDispenser
{
    public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
    {
        var methodDirection = cardArgs.CardMethod.MethodDirection;
        var gameSessionId = cardArgs.GameSessionId;
        switch (methodDirection)
        {
            case MethodDirection.FreePlace:
                resultBuilder.AddCommand(new GameSessions.Commands.RemoveSeats(gameSessionId, 1));
                break;
            default:
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                break;
        }

    }
}
