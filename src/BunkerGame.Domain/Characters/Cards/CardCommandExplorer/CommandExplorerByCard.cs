using BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands;
using BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands;
using BunkerGame.Domain.Shared;
using BunkerGame.GameTypes.CharacterTypes;
using MediatR;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer
{
    internal record NoneTargetCardArgs(CharacterId CardUserCharacterId, GameSessionId GameSessionId, CardMethod CardMethod);
    internal record TargetCharacterCardArgs(CharacterId CardUserCharacterId, CharacterId TargetCharacterId, GameSessionId GameSessionId, CardMethod CardMethod);
    internal static class CommandExplorerByCard
    {
        private static readonly Dictionary<MethodType, INoneTargetCardCommandDispenser> NoneTargetCommands;
        private static readonly Dictionary<MethodType, ITargetCharacterCardCommandDispenser> CharacterTargetCommands;
        static CommandExplorerByCard()
        {
            NoneTargetCommands = GetNoneTargetCommands();
            CharacterTargetCommands = GetTargetCharacterCommands();

        }
        public static void SetNoTargetCommand(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodType = cardArgs.CardMethod.MethodType;
            if (!NoneTargetCommands.TryGetValue(methodType, out var commandDispencer))
            {
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                return;
            }
            commandDispencer.GiveCommandHandler(cardArgs, resultBuilder);
        }
        public static void SetTargetCharacterCommand(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodType = cardArgs.CardMethod.MethodType;
            if (!CharacterTargetCommands.TryGetValue(methodType, out var commandDispencer))
            {
                resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                return;
            }
            commandDispencer.GiveCommandHandler(cardArgs, resultBuilder);
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
