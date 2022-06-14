using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{
    internal class UpdateGameComponentDispencer : INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs)
        {
            switch (cardArgs.CardMethod.DefineDirectionGroup())
            {
                case DirectionGroup.Character:
                    return GetUpdateCharacterCommand(cardArgs.CardMethod, cardArgs.CardUserCharacterId);
            }
            return null;
        }
        private object? GetUpdateCharacterCommand(CardMethod cardMethod, int characterId)
        {
            if (cardMethod.MethodDirection != MethodDirection.Character)
            {
                var componentId = cardMethod.ItemId;
                return ChangeCharacterComponentCommandFactory.CreateChangeCharacterComponentCommand(characterId,componentId,cardMethod.MethodDirection);
            }
            else
            {
                //TODO
                //add UpdateCharacter
                return null;
            }
        }
    }
}
