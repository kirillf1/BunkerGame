using BunkerGame.Domain.GameSessions;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public class GameResultCounterFactory
    {
        public IGameResultCounter GetGameResultCounter(ResultCounterParams resultCounterParams, Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return new GameResultCounterEasy(resultCounterParams);
                case Difficulty.Medium:
                    return new GameResultCounterMedium(resultCounterParams);
                case Difficulty.Hard:
                    return new GameResultCounterHard(resultCounterParams);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
