namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands
{
    internal interface ITargetCharacterCardCommandDispenser
    {
        public void GiveCommandHandler(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder);
    }
}
