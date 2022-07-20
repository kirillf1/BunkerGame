using MediatR;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer
{
    public static class ChangeBunkerComponentCommandFactory
    {
        public static void CreateChangeBunkerComponentCommand(GameSessionId gameSessionId, object? component, MethodDirection methodDirection
            , CardCommandResultBuilder resultBuilder)
        {
            IRequest? command = methodDirection switch
            {
                MethodDirection.BunkerWall => new GameSessions.Commands.UpdateBunkerCondition(gameSessionId, TryConvertComponent<Condition>(resultBuilder, component)),
                MethodDirection.BunkerSize => new GameSessions.Commands.UpdateBunkerSize(gameSessionId, TryConvertComponent<Size>(resultBuilder, component)),
                MethodDirection.Supplies => new GameSessions.Commands.UpdateBunkerSupplies(gameSessionId, TryConvertComponent<Supplies>(resultBuilder, component)),
                MethodDirection.ItemBunker => new GameSessions.Commands.UpdateBunkerItems(gameSessionId, TryConvertComponent<Item>(resultBuilder, component)),
                MethodDirection.BunkerObject => new GameSessions.Commands.UpdateBunkerBuildings(gameSessionId, TryConvertComponent<Building>(resultBuilder, component)),
                MethodDirection.BunkerEnviroment => new GameSessions.Commands.UpdateBunkerEnviroment(gameSessionId, TryConvertComponent<Enviroment>(resultBuilder, component)),
                _ => null
            };
            if (command == null)
            {
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                return;
            }
            resultBuilder.AddCommand(command);
        }
        private static T? TryConvertComponent<T>(CardCommandResultBuilder resultBuilder, object? component)
        {
            if (component == null)
                return default;
            if (component is T t)
                return t;
            resultBuilder.AddError(CardExecuteError.InvalidComponentType);
            return default;

        }
    }

}
