using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Domain.Characters
{
    public static class Commands
    {
        public record CreateCharacter(CharacterId CharacterId,GameSessionId GameSessionId,PlayerId PlayerId) : IRequest;
        public record UncoverCharacter(CharacterId CharacterId) : IRequest;
        public record UseCard(CharacterId CharacterId, byte CardNumber,CharacterId? TargetCharacter) : IRequest;
        #region UncoverCharacterComponent
        public record UncoverAge(CharacterId CharacterId) : IRequest;
        public record UncoverAdditionalInformation(CharacterId CharacterId) : IRequest;
        public record UncoverChildbearing(CharacterId CharacterId) : IRequest;
        public record UncoverHealth(CharacterId CharacterId) : IRequest;
        public record UncoverHobby(CharacterId CharacterId) : IRequest;
        public record UncoverSex(CharacterId CharacterId) : IRequest;
        public record UncoverSize(CharacterId CharacterId) : IRequest;
        public record UncoverPhobia(CharacterId CharacterId) : IRequest;
        public record UncoverTrait(CharacterId CharacterId) : IRequest;
        public record UncoverItems(CharacterId CharacterId) : IRequest;
        public record UncoverCards(CharacterId CharacterId) : IRequest;
        #endregion
        #region UpdateCharacterComponent
        public record UpdateAge(CharacterId CharacterId, Age? Age) : IRequest;
        public record UpdateAdditionalInformation(CharacterId CharacterId, AdditionalInformation? AdditionalInformation) : IRequest;
        public record UpdateHealth(CharacterId CharacterId, Health? Health) : IRequest;
        public record UpdateChildbearing(CharacterId CharacterId, Childbearing? Childbearing) : IRequest;
        public record UpdateHobby(CharacterId CharacterId, Hobby? Hobby) : IRequest;
        public record UpdatePhobia(CharacterId CharacterId, Phobia? Phobia) : IRequest;
        public record UpdateProfession(CharacterId CharacterId, Profession? Profession) : IRequest;
        public record UpdateSex(CharacterId CharacterId, Sex? Sex) : IRequest;
        public record UpdateSize(CharacterId CharacterId, Size? Size) : IRequest;
        public record UpdateTrait(CharacterId CharacterId, Trait? Trait) : IRequest;
        public record UpdateItem(CharacterId CharacterId, Item? Item) : IRequest;
        #endregion
        #region ExchangeCharacter
        public record ExchangeCharacter(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeAge(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeAdditionalInformation(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeChildbearing(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeHealth(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeHobby(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeItem(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangePhobia(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeProfession(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeSex(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeSize(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        public record ExchangeTrait(CharacterId CharacterFirstId, CharacterId CharacterSecondId) : IRequest;
        #endregion
    }
}
