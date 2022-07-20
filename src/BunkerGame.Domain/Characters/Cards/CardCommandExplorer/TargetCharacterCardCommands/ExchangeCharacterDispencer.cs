using BunkerGame.GameTypes.CharacterTypes;
using MediatR;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class ExchangeCharacterDispencer : ITargetCharacterCardCommandDispenser
    {

        private static IRequest? GetExchangeCharactesticCommand(MethodDirection methodDirection, CharacterId characterFirst, CharacterId characterSecond)
        {
            return methodDirection switch
            {
                MethodDirection.AdditionalInformation => new Commands.ExchangeAdditionalInformation(characterFirst, characterSecond),
                MethodDirection.Health => new Commands.ExchangeHealth(characterFirst, characterSecond),
                MethodDirection.Profession => new Commands.ExchangeProfession(characterFirst, characterSecond),
                MethodDirection.Phobia => new Commands.ExchangePhobia(characterFirst, characterSecond),
                MethodDirection.Sex => new Commands.ExchangeSex(characterFirst, characterSecond),
                MethodDirection.Size => new Commands.ExchangeSize(characterFirst, characterSecond),
                MethodDirection.Trait => new Commands.ExchangeTrait(characterFirst, characterSecond),
                MethodDirection.Hobby => new Commands.ExchangeHobby(characterFirst, characterSecond),
                MethodDirection.Age => new Commands.ExchangeAge(characterFirst, characterSecond),
                MethodDirection.CharacterItem => new Commands.ExchangeItem(characterFirst, characterSecond),
                MethodDirection.Childbearing => new Commands.ExchangeChildbearing(characterFirst, characterSecond),
                _ => default,
            };

        }


        public void GiveCommandHandler(TargetCharacterCardArgs cardArgs, CardCommandResultBuilder resultBuilder)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            var cardUserId = cardArgs.CardUserCharacterId;
            if (methodDirection == MethodDirection.Character)
            {
                resultBuilder.AddCommand(new Commands.ExchangeCharacter(targetCharacterId, cardUserId));
            }
            else
            {
                var command = GetExchangeCharactesticCommand(methodDirection, cardUserId, targetCharacterId);
                if (command == null)
                    resultBuilder.AddError(CardExecuteError.NoSuchCommand);
                else
                    resultBuilder.AddCommand(command);
            }
        }
    }
}
