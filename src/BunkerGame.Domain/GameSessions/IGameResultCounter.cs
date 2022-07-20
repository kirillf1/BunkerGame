using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.GameSessions
{
    public interface IGameResultCounter
    {
        public ResultReport CalculateGameResut();
    }
    public class ResultReport
    {
        public ResultReport()
        {
            gameReport = String.Empty;
        }
        public ResultReport(string gameReport, double entertaimentVal, double survivingValue, double foodValue, bool isWinGame)
        {
            this.gameReport = gameReport;
            EntertainmentValue = entertaimentVal;
            SurvivingValue = survivingValue;
            FoodValue = foodValue;
            IsWinGame = isWinGame;
        }
        public string GameReport
        {
            get
            {
                return $"Итоги игры: {Environment.NewLine}{gameReport}{Environment.NewLine}" + (IsWinGame ? "Вы победили": "Вы проиграли"); 
            }
        }
        private string gameReport;
        public double FoodValue { get; private set; }
        public double EntertainmentValue { get; private set; }
        public double SurvivingValue { get; private set; }
        public bool IsWinGame { get; private set; }
    }
}
