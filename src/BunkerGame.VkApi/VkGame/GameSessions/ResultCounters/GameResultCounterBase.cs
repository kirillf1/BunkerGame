using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.GameComponentTypes;
using System.Text;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public abstract class GameResultCounterBase : IGameResultCounter
    {
        protected const int withoutDebuffGiveBirthAge = 55;
        protected const double oldAgeCanBirthDebuffPercent = 0.6;
        protected Random random;
        protected readonly List<CharacterWithName> characters;
        protected readonly List<ExternalSurrounding> externalSurroundings;
        protected readonly Bunker bunker;
        protected readonly Catastrophe catastrophe;
        protected StringBuilder resultTextBuilder;
        protected GameResultCounterBase(ResultCounterParams resultCounterParams)
        {
            bunker = resultCounterParams.Bunker;
            characters = new(resultCounterParams.NotKickedCharacters);
            catastrophe = resultCounterParams.Catastrophe;
            externalSurroundings = new(resultCounterParams.ExternalSurroundings);
            resultTextBuilder = new();
            random = new();
        }
        public abstract ResultReport CalculateGameResut();
        protected virtual double CalculateCatastropheValue(bool bunkerIsBroken)
        {
            return bunkerIsBroken ? catastrophe.Value * 1.5 : catastrophe.Value;
        }
        protected virtual double GetTotalExternalSurroundingValue(bool canFixBunker)
        {
            double totalValue = 0;
            var bunkerIsBroken = bunker.Condition.BunkerState == BunkerState.Broken && !canFixBunker;
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
        protected virtual void ExecuteAllCharacterEvents(StringBuilder stringBuilder)
        {
            characters.UsePassiveCards(stringBuilder);
            characters.RemoveCharactersWithDeathDesease(stringBuilder);
            characters.TrySpreadDesease(stringBuilder);
        }
        protected virtual bool CheckCanGiveBirth(StringBuilder stringBuilder)
        {
            bool canGiveBirth = false;
            var canGiveLifeCharacters = characters.FindGiveBirthCharacters();
            var canGiveLifeMen = canGiveLifeCharacters.canGiveBirthMen;
            var canGiveLifeWomen = canGiveLifeCharacters.canGiveBirthWomen;
            if (canGiveLifeMen.Count > 0 && canGiveLifeWomen.Count > 0)
            {
                var youngerMen = canGiveLifeMen.MinBy(c => c.Character.Age.Years)!;
                var youngerWomen = canGiveLifeWomen.MinBy(c => c.Character.Age.Years)!;
                canGiveBirth = CalculateGiveBirthChance(youngerMen.Character.Age.Years, youngerWomen.Character.Age.Years);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Игроки ").Append(youngerMen.Name)
                        .Append(" и ").Append(youngerWomen.Name).AppendLine(" дадут потомство");
                    return true;
                }
            }
            else if (canGiveLifeMen.Count > 0 && canGiveLifeWomen.Count == 0
                && externalSurroundings.Any(c => c.SurroundingType == SurroundingType.PeacefulWomen))
            {
                var man = canGiveLifeMen.MinBy(c => c.Character.Age.Years)!;
                var surrounding = externalSurroundings.First(c => c.SurroundingType == SurroundingType.PeacefulWomen);
                canGiveBirth = CalculateGiveBirthChance(man.Character.Age.Years, withoutDebuffGiveBirthAge - 1);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Хоть нет в бункере плодовитых женщин, но вы знаете что есть ")
                        .Append(surrounding.Description).Append(" Игрок ").Append(man.Name).AppendLine(" продолжает потомство");
                    return true;
                }
            }
            else if (canGiveLifeMen.Count == 0 && canGiveLifeWomen.Count == 1
                && externalSurroundings.Any(c => c.SurroundingType == SurroundingType.PeacefulMen))
            {
                var woman = canGiveLifeWomen.MinBy(c => c.Character.Age.Years)!;
                var surrounding = externalSurroundings.First(c => c.SurroundingType == SurroundingType.PeacefulWomen);
                canGiveBirth = CalculateGiveBirthChance(woman.Character.Age.Years, withoutDebuffGiveBirthAge - 1);
                if (canGiveBirth)
                {
                    stringBuilder.Append("Хоть нет в бункере плодовитых мужчин, но вы знаете что есть ")
                        .Append(surrounding.Description).Append(" Игрок ").Append(woman.Name).AppendLine(" продолжает потомство");
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
