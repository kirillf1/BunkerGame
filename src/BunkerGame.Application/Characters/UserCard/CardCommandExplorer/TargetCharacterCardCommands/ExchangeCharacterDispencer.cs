using BunkerGame.Application.Characters.ExchangeCharacter;
using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal class ExchangeCharacterDispencer : ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs)
        {
            var methodDirection = cardArgs.CardMethod.MethodDirection;
            var targetCharacterId = cardArgs.TargetCharacterId;
            var cardUserId = cardArgs.CardUserCharacterId;
            if (methodDirection == MethodDirection.Character)
            {
                //TODO
                // add change Character
                return null;
            }
            else
            {
                return GetExchangeCharactesticCommand(methodDirection,cardUserId, targetCharacterId);
            }
        }
        private object GetExchangeCharactesticCommand(MethodDirection methodDirection,int characterFirst,int characterSecond)
        {
            Func<int, int, object> command = methodDirection switch
            {
                MethodDirection.AdditionalInformation => GetCharacteristicCommand<AdditionalInformation>,
                MethodDirection.Health => GetCharacteristicCommand<Health>,
                MethodDirection.Profession => GetCharacteristicCommand<Profession>,
                MethodDirection.Phobia => GetCharacteristicCommand<Phobia>,
                MethodDirection.Sex => GetCharacteristicCommand<Sex>,
                MethodDirection.Size => GetCharacteristicCommand<Size>,
                MethodDirection.Trait => GetCharacteristicCommand<Trait>,
                MethodDirection.Hobby => GetCharacteristicCommand<Hobby>,
                MethodDirection.Age => GetCharacteristicCommand<Age>,
                MethodDirection.CharacterItem => GetCharacteristicCommand<CharacterItem>,
                MethodDirection.Childbearing => command = GetCharacteristicCommand<Childbearing>,
                _ => throw new NotImplementedException($"Character does not contain a property {methodDirection}"),
            };
            return command.Invoke(characterFirst,characterSecond);
        }
        private static ExchangeCharacteristicCommand<T> GetCharacteristicCommand<T>(int characterFirst,int characterSecond) where T : CharacterComponent
        {
            return new ExchangeCharacteristicCommand<T>(characterFirst, characterSecond);
        }
    }
}
