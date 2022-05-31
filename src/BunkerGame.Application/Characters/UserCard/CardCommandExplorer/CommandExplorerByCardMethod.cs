using BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands;
using BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;

namespace BunkerGame.Application.Characters.UserCard
{
    internal record NoneTargetCardArgs(int CardUserCharacterId, long GameSessionId, CardMethod CardMethod);
    internal record TargetCharacterCardArgs(int CardUserCharacterId, int TargetCharacterId, long GameSessionId, CardMethod CardMethod);
    internal record ExploreResult(object? Request);

    internal static class CommandExplorerByCardMethod
    {
        private static readonly Dictionary<MethodType, INoneTargetCardCommandDispenser> NoneTargetCommands;
        private static readonly Dictionary<MethodType, ITargetCharacterCardCommandDispenser> CharacterTargetCommands;
        static CommandExplorerByCardMethod()
        {
            NoneTargetCommands = GetNoneTargetCommands();
            CharacterTargetCommands = GetTargetCharacterCommands();

        }
        public static object? GetNoneTargetCommands(NoneTargetCardArgs cardArgs)
        {
            var methodType = cardArgs.CardMethod.MethodType;
            if (!NoneTargetCommands.TryGetValue(methodType, out var commandDispencer))
                return null;
            return commandDispencer.GiveCommandHandler(cardArgs);
        }
        public static object? GetTargetCharacterCommands(TargetCharacterCardArgs cardArgs)
        {
            var methodType = cardArgs.CardMethod.MethodType;
            if (!CharacterTargetCommands.TryGetValue(methodType, out var commandDispencer))
                return null;
            return commandDispencer.GiveCommandHandler(cardArgs);
        }
        private static Dictionary<MethodType, INoneTargetCardCommandDispenser> GetNoneTargetCommands()
        {
            return new Dictionary<MethodType, INoneTargetCardCommandDispenser>
            {
                [MethodType.Add] = new AddGameComponentDispencer(),
                [MethodType.Remove] = new RemoveGameComponentDispencer(),
                [MethodType.Change] = new ChangeGameComponentDispencer(),
                [MethodType.SpyYourself] = new SpyYourselfCharacterDispencer(),
                [MethodType.Update] = new UpdateGameComponentDispencer()
            };
        }
        private static Dictionary<MethodType, ITargetCharacterCardCommandDispenser> GetTargetCharacterCommands()
        {
            return new Dictionary<MethodType, ITargetCharacterCardCommandDispenser>
            {
                [MethodType.Change] = new ChangeCharacterDispencer(),
                [MethodType.Exchange] = new ExchangeCharacterDispencer(),
                [MethodType.Remove] = new KickCharacterCommandDispencer(),
                [MethodType.Spy] = new SpyOnCharacterDispencer()
            };
        }
    }
}
