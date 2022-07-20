using BunkerGame.Domain.GameSessions;
using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;
using System.Text;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public class GameResultCounterEasy : GameResultCounterBase
    {
        public GameResultCounterEasy(ResultCounterParams resultCounterParams) : base(resultCounterParams)
        {
        }

        public override ResultReport CalculateGameResut()
        {
            base.ExecuteAllCharacterEvents(resultTextBuilder);
            var totalValue = CalculateAllValues();
            bool isWinGame = totalValue > 0 && CheckCanGiveBirth(resultTextBuilder);
            var report = new ResultReport(resultTextBuilder.ToString(), totalValue, totalValue, totalValue, isWinGame);
            resultTextBuilder.Clear();
            return report;
        }
        /// <summary>
        /// Calculate all values in game
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="bunker"></param>
        /// <param name="catastrophe"></param>
        /// <returns>total value</returns>
        private double CalculateAllValues()
        {
            double charactersSum = 0;
            foreach (var character in characters.Select(c => c.Character))
            {
                charactersSum += character.AdditionalInformation.Value
                    + character.Items.Sum(c => c.Value)
                    + character.Hobby.Experience > 0 ? character.Hobby.Value : 0 + character.Trait.Value;
            }
            double bunkerSum = bunker.Buildings.Sum(b => b.Value);
            bunkerSum += bunker.Condition.Value;
            bunkerSum += bunker.Enviroment.Value;
            bunkerSum += bunker.Items.Sum(b => b.Value);
            bool bunkerIsBroken = bunker.Condition.BunkerState == BunkerState.Broken ? characters.CanFixBunker(bunker.Items, resultTextBuilder) : true;
            return bunkerSum + charactersSum
                + GetTotalProfessionValue() + GetTotalHealthValue()
                + GetTotalPhobiaValue()
                + GetTotalExternalSurroundingValue(bunkerIsBroken)
                + CalculateCatastropheValue(bunkerIsBroken);

        }
        private double GetTotalHealthValue()
        {
            if (characters.CanHealDesease(bunker, resultTextBuilder))
            {
                return 0;
            }
            return characters.Sum(c => c.Character.Health.Value);
        }
        private double GetTotalPhobiaValue()
        {
            if (characters.CanHealPhobia(resultTextBuilder))
            {
                return 0;
            }
            return characters.Sum(c => c.Character.Phobia.Value);
        }

        private double GetTotalProfessionValue()
        {
            double professionTotalValue = 0;
            foreach (var character in characters.Select(c => c.Character))
            {
                var profession = character.Profession;
                var indexBody = character.Size.GetAvagereIndexBody();
                double indexBodyMultiplier = indexBody switch
                {
                    string index when index.Contains("Ожирение III") => 0.3,
                    string index when index.Contains("Ожирение II") => 0.5,
                    string index when index.Contains("Ожирение I") || index.Contains("Избыточный") || index.Contains("Недостаток") => 0.7,
                    _ => 1
                };
                double professionValue = character.Profession.Experience > 0 ? profession.Value : 0 * (character.Age.Years > withoutDebuffGiveBirthAge ? 0.6 : 1);
                professionTotalValue += profession.ProfessionType == ProfessionType.Surviving ? professionValue * indexBodyMultiplier : professionValue;
            }
            return professionTotalValue;
        }
    }
}
