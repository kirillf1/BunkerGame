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
                // Update character component
                var characterComponentType = GameComponentTypeTextConventer.ConvertTextToCharacteristicType(cardMethod.MethodDirection.ToString());
                return new ChangeCharacteristicCommand(characterId, characterComponentType!)
                {
                    CharacteristicId = cardMethod.ItemId
                };
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
