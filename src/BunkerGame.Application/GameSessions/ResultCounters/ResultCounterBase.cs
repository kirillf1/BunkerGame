using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ResultCounters
{
    public abstract class ResultCounterBase
    {
        protected int withoutDebuffGiveBirthAge = 55;
        protected double oldAgeCanBirthDebuffPercent = 0.6;
        protected Random random = new Random();
        protected readonly GameSession gameSession;
        protected StringBuilder gameResultStringBuilder;
        protected ResultCounterBase(GameSession gameSession)
        {
            this.gameSession = gameSession;
            gameResultStringBuilder = new StringBuilder();
        }
        protected virtual double CalculateCatastropheValue(Catastrophe catastrophe, bool bunkerIsBroken)
        {
            return bunkerIsBroken ? catastrophe.Value * 1.5 : catastrophe.Value;
        }
        protected virtual double GetTotalExternalSurroundingValue(Bunker bunker, bool canFixBunker, IEnumerable<ExternalSurrounding> externalSurroundings)
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
        protected virtual List<Character> ExecuteAllCharacterEvents(StringBuilder stringBuilder,IEnumerable<Character> characters)
        {
            var aliveCharacters = characters.Where(c => c.IsAlive).ToList();
            aliveCharacters.UsePassiveCards(stringBuilder);
            aliveCharacters.RemoveCharactersWithDeathDesease(stringBuilder);
            aliveCharacters.TrySpreadDesease(stringBuilder);
            return aliveCharacters;
        }
        protected virtual bool CheckCanGiveBirth(List<Character> characters, IEnumerable<ExternalSurrounding> externalSurroundings, StringBuilder stringBuilder)
        {
            bool canGiveBirth = false;
            var canGiveLifeCharacters = characters.FindGiveBirthCharacters();
            var canGiveLifeMen = canGiveLifeCharacters.canGiveBirthMen;
            var canGiveLifeWomen = canGiveLifeCharacters.canGiveBirthWomen;
            if (canGiveLifeMen.Count > 0 && canGiveLifeWomen.Count > 0)
            {
                var youngerMen = canGiveLifeMen.MinBy(c => c.Age.Count)!;
                var youngerWomen = canGiveLifeWomen.MinBy(c => c.Age.Count)!;
                canGiveBirth = CalculateGiveBirthChance(youngerMen.Age.Count, youngerWomen.Age.Count);
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
                canGiveBirth = CalculateGiveBirthChance(man.Age.Count, withoutDebuffGiveBirthAge - 1);
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
                canGiveBirth = CalculateGiveBirthChance(woman.Age.Count, withoutDebuffGiveBirthAge - 1);
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
        private bool CalculateGiveBirthChance(int characterFirstAge, int characterSecondAge)
        {
            double canGiveBirthChance = 100;
            if (characterFirstAge > withoutDebuffGiveBirthAge)
                canGiveBirthChance *= oldAgeCanBirthDebuffPercent - ((double)(characterFirstAge - withoutDebuffGiveBirthAge) / 50);
            if (characterSecondAge > withoutDebuffGiveBirthAge)
                canGiveBirthChance *= oldAgeCanBirthDebuffPercent - ((double)(characterSecondAge - withoutDebuffGiveBirthAge) / 50);
            return random.Next(0, 100) <= canGiveBirthChance;
        }
    }
}
