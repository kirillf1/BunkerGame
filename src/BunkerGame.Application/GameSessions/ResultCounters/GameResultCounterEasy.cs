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
    public class GameResultCounterEasy : IGameResultCounter
    {
        Random random;
        private const int withoutDebuffGiveBirthAge = 55;
        private const double oldAgeCanBirthDebuffPercent = 0.6;
        public GameResultCounterEasy()
        {
            random = new Random();
        }
        public Task<ResultGameReport> CalculateResult(GameSession gameSession)
        {
            return Task.Run(() =>
             {

                 var resultTextBuilder = new StringBuilder();
                 var aliveCharacters = gameSession.Characters.Where(c => c.IsAlive).ToList();
                 aliveCharacters.UsePassiveCards(resultTextBuilder);
                 aliveCharacters.RemoveCharactersWithDeathDesease(resultTextBuilder);
                 aliveCharacters.TrySpreadDesease(resultTextBuilder);
                 var totalValue = CalculateAllValues(aliveCharacters, gameSession.Bunker, gameSession.Catastrophe,gameSession.ExternalSurroundings, resultTextBuilder);
                 bool isWinGame = totalValue > 0 && CheckCanGiveBirth(aliveCharacters, gameSession.ExternalSurroundings, resultTextBuilder);
                 return new ResultGameReport(resultTextBuilder.ToString(), totalValue, totalValue, totalValue, isWinGame, gameSession.GameName);
             });
        }
        private bool CheckCanGiveBirth(List<Character> characters, IEnumerable<ExternalSurrounding> externalSurroundings, StringBuilder stringBuilder)
        {
            // стоит доработать, сделать проверку приоритетов, если 1 старый, то лучше с вшнешними продолжать род
            bool canGiveBirth = false;
            var canGiveLifeCharacters = characters.FindGiveBirthCharacters();
            var canGiveLifeMen = canGiveLifeCharacters.canGiveBirthMen;
            var canGiveLifeWomen = canGiveLifeCharacters.canGiveBirthWomen;
            if (canGiveLifeMen.Count > 0 && canGiveLifeWomen.Count > 0)
            {
                var youngerMen = canGiveLifeMen.MinBy(c => c.Age.Count)!;
                var youngerWomen = canGiveLifeWomen.MinBy(c => c.Age.Count)!;
                canGiveBirth = calculateGiveBirthChance(youngerMen.Age.Count, youngerWomen.Age.Count);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Игроки под номерами ").Append(youngerMen.CharacterNumber)
                        .Append(" и ").Append(youngerWomen.CharacterNumber).AppendLine(" дадут потомство");
                    return true;
                }
            }
            else if (canGiveLifeMen.Count > 0 && canGiveLifeWomen.Count == 0
                && externalSurroundings.Any(c => c.SurroundingType == SurroundingType.PeacefulWomen))
            {
                var man = canGiveLifeMen.MinBy(c => c.Age.Count)!;
                var surrounding = externalSurroundings.First(c => c.SurroundingType == SurroundingType.PeacefulWomen);
                canGiveBirth = calculateGiveBirthChance(man.Age.Count, withoutDebuffGiveBirthAge - 1);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Хоть нет в бункере плодовитых женщин, но вы знаете что есть ")
                        .Append(surrounding.Description).Append(" Игрок №").Append(man.CharacterNumber).AppendLine(" продолжает потомство");
                    return true;
                }
            }
            else if (canGiveLifeMen.Count == 0 && canGiveLifeWomen.Count == 1
                && externalSurroundings.Any(c => c.SurroundingType == SurroundingType.PeacefulMen))
            {
                var woman = canGiveLifeWomen.MinBy(c => c.Age.Count)!;
                var surrounding = externalSurroundings.First(c => c.SurroundingType == SurroundingType.PeacefulWomen);
                canGiveBirth = calculateGiveBirthChance(woman.Age.Count, withoutDebuffGiveBirthAge - 1);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Хоть нет в бункере плодовитых мужчин, но вы знаете что есть ")
                        .Append(surrounding.Description).Append(" Игрок №").Append(woman.CharacterNumber).AppendLine(" продолжает потомство");
                    return true;
                }
            }

            stringBuilder.AppendLine("В бункере нет плодовитых пар!");
            return false;
        }
        private bool calculateGiveBirthChance(int characterFirstAge, int characterSecondAge)
        {
            double canGiveBirthChance = 100;
            if (characterFirstAge > withoutDebuffGiveBirthAge)
                canGiveBirthChance *= oldAgeCanBirthDebuffPercent - ((double)(characterFirstAge - withoutDebuffGiveBirthAge) / 50);
            if (characterSecondAge > withoutDebuffGiveBirthAge)
                canGiveBirthChance *= oldAgeCanBirthDebuffPercent - ((double)(characterSecondAge - withoutDebuffGiveBirthAge) / 50);
            return random.Next(0, 100) <= canGiveBirthChance;
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
        private double CalculateCatastropheValue(Catastrophe catastrophe, bool bunkerIsBroken)
        {
            return bunkerIsBroken ? catastrophe.Value * 1.5 : catastrophe.Value;
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
        private double GetTotalExternalSurroundingValue(Bunker bunker,bool canFixBunker, IEnumerable<ExternalSurrounding> externalSurroundings)
        {
            double totalValue = 0;
            var bunkerIsBroken = bunker.BunkerWall.BunkerState == BunkerState.Broken && !canFixBunker;
            foreach (var externalSurrounding in externalSurroundings)
            {
                var surroundingType = externalSurrounding.SurroundingType;
                if (surroundingType == SurroundingType.AgressiveCreatures || surroundingType == SurroundingType.AgressivePeople)
                {
                    totalValue += bunkerIsBroken ? externalSurrounding.Value * 1.5 : externalSurrounding.Value;
                }
                else
                    totalValue += externalSurrounding.Value;
            }
            return totalValue;
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
