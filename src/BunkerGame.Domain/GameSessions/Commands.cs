using BunkerGame.Domain.GameSessions.Bunkers;
using MediatR;

namespace BunkerGame.Domain.GameSessions
{
    public static class Commands
    {
        public record CreateGame(GameSessionId GameSessionId,PlayerId PlayerId) : IRequest;
        public record RestartGame(GameSessionId GameSessionId) : IRequest;
        public record StartGame(GameSessionId GameSessionId) : IRequest;
        public record UpdateCatastrophe(GameSessionId GameSessionId, Catastrophe? Catastrophe) : IRequest;
        public record ChangeMaxCharacterSize(GameSessionId GameSessionId, byte CharacterCount) : IRequest;
        public record AddSeats(GameSessionId GameSessionId, int SeatsCount) : IRequest;
        public record RemoveSeats(GameSessionId GameSessionId, int SeatsCount) : IRequest;
        public record KickCharacter(GameSessionId GameSessionId, CharacterId CharacterId) : IRequest;
        public record UpdateToRandomBunker(GameSessionId GameSessionId) : IRequest;
        public record UpdateBunkerEnviroment(GameSessionId GameSessionId, Enviroment? BunkerEnviroment) : IRequest;
        public record UpdateBunkerCondition(GameSessionId GameSessionId, Condition? Condition) : IRequest;
        public record UpdateBunkerSize(GameSessionId GameSessionId, Size? BunkerSize) : IRequest;
        public record UpdateBunkerItems(GameSessionId GameSessionId, Item? Item) : IRequest;
        public record UpdateBunkerSupplies(GameSessionId GameSessionId, Supplies? Supplies) : IRequest;
        public record UpdateBunkerBuildings(GameSessionId GameSessionId, Building? Building) : IRequest;
        public record AddExternalSurrounding(GameSessionId GameSessionId, ExternalSurrounding ExternalSurrounding) : IRequest;
        public record EndGame(GameSessionId GameSessionId) : IRequest;
        public record ChangeDifficulty(GameSessionId GameSessionId, Difficulty Difficulty) : IRequest;
    }

}
