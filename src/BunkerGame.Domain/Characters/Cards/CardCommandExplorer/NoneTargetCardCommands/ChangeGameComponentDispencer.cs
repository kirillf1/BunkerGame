using BunkerGame.GameTypes.CharacterTypes;
using static BunkerGame.Domain.Characters.Cards.CardMethod;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands;

internal class ChangeGameComponentDispencer : INoneTargetCardCommandDispenser
{
    public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
    {
        var cardMethod = cardArgs.CardMethod;
        var directionGroup = cardMethod.DefineDirectionGroup();
        var gameSessionId = cardArgs.GameSessionId;
        switch (directionGroup)
        {
            case DirectionGroup.Bunker:
                GetBunkerUpdateCommand(gameSessionId, cardMethod, resultBuilder);
                break;
            case DirectionGroup.Catastrophe:
                resultBuilder.AddCommand(new GameSessions.Commands.UpdateCatastrophe(gameSessionId, null));
                break;
            default:
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                break;
        }

    }

    private static void GetBunkerUpdateCommand(GameSessionId gameSessionId, CardMethod cardMethod, CardCommandResultBuilder resultBuilder)
    {
        var methodDirection = cardMethod.MethodDirection;
        if (methodDirection == MethodDirection.Bunker)
        {
            resultBuilder.AddCommand(new GameSessions.Commands.UpdateToRandomBunker(gameSessionId));
        }
        else
        {
            var component = cardMethod.Item;
            ChangeBunkerComponentCommandFactory.CreateChangeBunkerComponentCommand(gameSessionId, component, methodDirection, resultBuilder);
        }
    }
}
