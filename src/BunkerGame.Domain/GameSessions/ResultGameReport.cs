using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public class ResultGameReport
    {
        public string GameReport { get { return "Итоги игры:" +Environment.NewLine +
                    gameReport + (IsWinGame ? "Вы выиграли" : "Вы проиграли") + " игру"; } }
        private string gameReport;
        public double FoodValue { get; private set; }
        public double EntertainmentValue { get; private set;  }
        public double SurvivingValue { get; private set;  }
        public string ConversationName { get; private set; }
        public bool IsWinGame { get; private set; }
        public ResultGameReport()
        {
            gameReport = String.Empty;
            ConversationName = String.Empty;
        }
        public ResultGameReport(string gameReport, double entertaimentVal, double survivingValue, double foodValue, bool isWinGame, string conversationName = "unknown")
        {
            this.gameReport = gameReport;
            EntertainmentValue = entertaimentVal;
            SurvivingValue = survivingValue;
            FoodValue = foodValue;
            ConversationName = conversationName;
            IsWinGame = isWinGame;
        }
    }
}
