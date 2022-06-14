using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ResultCounters
{
    public class GameResultCounterEasy : ResultCounterBase, IGameResultCounter
    {
        public GameResultCounterEasy(GameSession gameSession) : base(gameSession)
        {
        }

        public Task<ResultGameReport> CalculateResult()
        {
            return Task.Run(() =>
             {
                 var resultTextBuilder = new StringBuilder();
                 var aliveCharacters = base.ExecuteAllCharacterEvents(resultTextBuilder, gameSession.Characters);
                 var totalValue = CalculateAllValues(aliveCharacters, gameSession.Bunker, gameSession.Catastrophe,gameSession.ExternalSurroundings, resultTextBuilder);
                 bool isWinGame = totalValue > 0 && CheckCanGiveBirth(aliveCharacters, gameSession.ExternalSurroundings, resultTextBuilder);
                 return new ResultGameReport(resultTextBuilder.ToString(), totalValue, totalValue, totalValue, isWinGame, gameSession.GameName);
             });
        }
        
        /// <summary>
        /// Calculate all values in game
        /// </summary>
        /// <param name="characters"></param>
        /// <param name="bunker"></param>
        /// <param name="catastrophe"></param>
        /// <returns>total value</returns>
        private double CalculateAllValues(IEnumerable<Character> characters, Bunker bunker, Catastrophe catastrophe,IEnumerable<ExternalSurrounding> externalSurroundings, 
            StringBuilder stringBuilder)
        {

            double charactersSum = 0;
            foreach (var character in characters)
            {
                charactersSum += character.AdditionalInformation.Value
                    + character.CharacterItems.Sum(c => c.Value)
                    + character.ExperienceHobby > 0 ? character.Hobby.Value : 0 + character.Trait.Value;
            }
            double bunkerSum = bunker.BunkerObjects.Sum(b => b.Value);
            bunkerSum += bunker.BunkerWall.Value;
            bunkerSum += bunker.BunkerEnviroment.Value;
            bunkerSum += bunker.ItemBunkers.Sum(b => b.Value);
            bool bunkerIsBroken = bunker.BunkerWall.BunkerState == BunkerState.Broken ? ResultCounterExtensions.CanFixBunker(characters, bunker.ItemBunkers, stringBuilder) : true;
            return bunkerSum + charactersSum
                + GetTotalProfessionValue(characters) + GetTotalHealthValue(characters, bunker, stringBuilder)
                + GetTotalPhobiaValue(characters, stringBuilder)
                + GetTotalExternalSurroundingValue(bunker,bunkerIsBroken,externalSurroundings)
                + CalculateCatastropheValue(catastrophe,bunkerIsBroken);

        }
        private double GetTotalHealthValue(IEnumerable<Character> characters, Bunker bunker, StringBuilder stringBuilder)
        {
            if (ResultCounterExtensions.CanHealDesease(characters, bunker, stringBuilder))
            {
                return 0;
            }
            return characters.Sum(c => c.Health.Value);
        }
        private double GetTotalPhobiaValue(IEnumerable<Character> characters, StringBuilder stringBuilder)
        {
            if (ResultCounterExtensions.CanHealPhobia(characters, stringBuilder))
            {
                return 0;
            }
            return characters.Sum(c => c.Phobia.Value);
        }
       
        private double GetTotalProfessionValue(IEnumerable<Character> characters)
        {
            double professionTotalValue = 0;
            foreach (var character in characters)
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
                double professionValue = character.ExperienceProfession > 0 ? profession.Value : 0 *(character.Age.Count > withoutDebuffGiveBirthAge ? 0.6 : 1);
                professionTotalValue += profession.ProfessionType == ProfessionType.Surviving ? professionValue * indexBodyMultiplier : professionValue;
            }
            return professionTotalValue;
        }
    }
}
