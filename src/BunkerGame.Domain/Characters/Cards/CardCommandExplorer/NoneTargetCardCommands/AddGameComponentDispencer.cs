using BunkerGame.Domain.GameSessions;
using BunkerGame.GameTypes.CharacterTypes;
using static BunkerGame.Domain.Characters.Cards.CardMethod;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands;

internal class AddGameComponentDispencer : INoneTargetCardCommandDispenser
{

    public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
    {
        var cardMethod = cardArgs.CardMethod;
        var directionGroup = cardMethod.DefineDirectionGroup();
        var gameSessionId = cardArgs.GameSessionId;
        switch (directionGroup)
        {
            case DirectionGroup.GameSession:
                GetAddGameSessionComponentCommand(gameSessionId, cardMethod, resultBuilder);
                break;
            default:
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                break;
        }
    }
    private static void GetAddGameSessionComponentCommand(GameSessionId gameSessionId, CardMethod cardMethod, CardCommandResultBuilder resultBuilder)
    {
        if (cardMethod.MethodDirection == MethodDirection.ExternalSurrounding)
        {
            var component = cardMethod.Item;
            if (component == default || component is not ExternalSurrounding)
            {
                resultBuilder.AddError(CardExecuteError.InvalidComponentType);
                return;
            }
            resultBuilder.AddCommand(new GameSessions.Commands.AddExternalSurrounding(gameSessionId, (ExternalSurrounding)component));
            return;
        }
        else if (cardMethod.MethodDirection == MethodDirection.FreePlace)
        {
            resultBuilder.AddCommand(new GameSessions.Commands.AddSeats(gameSessionId, 1));
            return;
        }
        resultBuilder.AddError(CardExecuteError.NoSuchCommand);
    }

}
