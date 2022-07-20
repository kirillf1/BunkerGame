using BunkerGame.GameTypes.CharacterTypes;
using static BunkerGame.Domain.Characters.Cards.CardMethod;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands
{
    internal class UpdateGameComponentDispencer : INoneTargetCardCommandDispenser
    {
        public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            switch (cardArgs.CardMethod.DefineDirectionGroup())
            {
                case DirectionGroup.Character:
                    GetUpdateCharacterCommand(cardArgs.CardMethod, cardArgs.CardUserCharacterId, resultBuilder);
                    break;
            }
        }

        private static void GetUpdateCharacterCommand(CardMethod cardMethod, CharacterId characterId, CardCommandResultBuilder resultBuilder)
        {
            if (cardMethod.MethodDirection != MethodDirection.Character)
            {
                var component = cardMethod.Item;
                ChangeCharacterComponentCommandFactory.CreateChangeCharacterComponentCommand(characterId, component, cardMethod.MethodDirection, resultBuilder);
            }
            else
            {
                //TODO
                //add UpdateCharacter

            }
        }
    }
}
