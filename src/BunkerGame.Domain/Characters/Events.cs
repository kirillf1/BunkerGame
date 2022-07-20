using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.Characters.CharacterComponents;
using MediatR;

namespace BunkerGame.Domain.Characters
{
    public static class Events
    {
        public record CharacterCreated(Character Character) : INotification;
        public record AdditionalInformationUpdated(CharacterId CharacterId, AdditionalInformation AdditionalInformation) : INotification;
        public record AgeUpdated(CharacterId CharacterId, Age Age) : INotification;
        public record CharacterItemsUpdated(CharacterId CharacterId, IEnumerable<Item> Items) : INotification;
        public record ChildbearingUpdated(CharacterId CharacterId, Childbearing Childbearing) : INotification;
        public record HealthUpdated(CharacterId CharacterId, Health Health) : INotification;
        public record HobbyUpdated(CharacterId CharacterId, Hobby Hobby) : INotification;
        public record ProfessionUpdated(CharacterId CharacterId, Profession Profession) : INotification;
        public record SexUpdated(CharacterId CharacterId, Sex Sex) : INotification;
        public record SizeUpdated(CharacterId CharacterId, Size Size) : INotification;
        public record TraitUpdated(CharacterId CharacterId, Trait Trait) : INotification;
        public record CardsUpdated(CharacterId CharacterId, IEnumerable<CardState> Cards) : INotification;
        public record CardUsed(CharacterId CharacterId, GameSessionId GameSessionId, Card Card, CharacterId? TargetCharacter) : INotification;
        public record PhobiaUpdated(CharacterId CharacterId, Phobia Phobia) : INotification;
    }
}
