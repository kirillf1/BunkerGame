using MediatR;

namespace BunkerGame.Domain.GameResults
{
    public static class Events
    {
        public record GameResultCreated(GameResult GameResult) : INotification;
        public record GameWon(GameSessionId GameSessionId,int WinCount) : INotification;
        public record GameLost(GameSessionId GameSessionId,int LoseCount) : INotification;
    }
}
