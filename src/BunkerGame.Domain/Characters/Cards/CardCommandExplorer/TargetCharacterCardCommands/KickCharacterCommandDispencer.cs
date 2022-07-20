namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class KickCharacterCommandDispencer : ITargetCharacterCardCommandDispenser
    {
        public void GiveCommandHandler(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var gameSessionId = cardArgs.GameSessionId;
            var targetCharacterId = cardArgs.TargetCharacterId;
            resultBuilder.AddCommand(new GameSessions.Commands.KickCharacter(gameSessionId, targetCharacterId));
        }
    }
}
