using BunkerGame.Domain.GameSessions.Bunkers;
using MediatR;

namespace BunkerGame.Domain.GameSessions
{
    public static class Events
    {
        public record BunkerUpdated(GameSessionId GameSessionId, Bunker Bunker) : INotification;
        public record CatastropheChanged(GameSessionId GameSessionId, Catastrophe Catastrophe) : INotification;
        public record CharacterAdded(GameSessionId GameSessionId, CharacterId CharacterId) : INotification;
        public record CharacterKicked(GameSessionId GameSessionId, CharacterId CharacterId) : INotification;
        public record DifficultyChanged(GameSessionId GameSessionId, Difficulty Difficulty) : INotification;
        public record ExternalSurroundigAdded(GameSessionId GameSessionId, ExternalSurrounding ExternalSurrounding) : INotification;
        public record FreeSeatsChanged(GameSessionId GameSessionId, int SizeCount) : INotification;
        public record SeatsFilled(GameSessionId GameSessionId) : INotification;
        public record GameCreated(GameSessionId GameSessionId, PlayerId PlayerId) : INotification;
        public record GameRestarted(GameSessionId GameSessionId) : INotification;
        public record GameEnded(GameSessionId GameSessionId) : INotification;
        public record GameStarted(GameSession GameSession) : INotification;
        public record NameChanged(GameSessionId GameSessionId, string Name) : INotification;
        public record MaxCharacterCountChanged(GameSessionId GameSessionId, byte MaxCharacterCount) : INotification;
    }
}
