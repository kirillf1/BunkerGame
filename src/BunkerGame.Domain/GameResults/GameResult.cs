using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Domain.GameResults
{
    public class GameResult : AggregateRoot<GameSessionId>
    {
        public GameResult(GameSessionId id, string conversationName)
        {
            Id = id;
            ConversationName = conversationName;
            WinGames = 0;
            LostGames = 0;
            AddEvent(new Events.GameResultCreated(this));
        }
        public string ConversationName { get; }
        public int WinGames { get; private set; }
        public int LostGames { get; private set; }
        public long GetGamesCount()
        {
            return WinGames + LostGames;
        }
        public void LoseGame()
        {
            LostGames++;
            AddEvent(new Events.GameLost(Id, WinGames));
        }
        public void WinGame()
        {
            WinGames++;
            AddEvent(new Events.GameWon(Id, WinGames));
        }
    }
}
