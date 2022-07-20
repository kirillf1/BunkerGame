using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public class GameResultCounterMedium : GameResultCounterBase
    {
        public GameResultCounterMedium(ResultCounterParams resultCounterParams) : base(resultCounterParams)
        {
        }

        protected record TotalGameValue(double PsychologicalValue, double SurvivalValue, bool CanGiveBirth);
        public override ResultReport CalculateGameResut()
        {
            ExecuteAllCharacterEvents(resultTextBuilder);
            var result = CalculateResult();
            bool isWinGame = result.CanGiveBirth && result.SurvivalValue > 0 && result.PsychologicalValue > 0;
            var gameReport = new ResultReport(resultTextBuilder.ToString(), result.PsychologicalValue, result.SurvivalValue, 0,
                isWinGame);
            resultTextBuilder.Clear();
            return gameReport;
        }
        protected TotalGameValue CalculateResult()
        {
            var isBunkerBroken = IsBunkerBroken();
            double psychologicalTotalValue = 0;
            double survivalTotalValue = 0;
            CalculateBunkerTotalValue(ref psychologicalTotalValue, ref survivalTotalValue);
            survivalTotalValue += GetTotalExternalSurroundingValue( isBunkerBroken);
            survivalTotalValue += CalculateCatastropheValue(isBunkerBroken);
            CalculateCharactersTotalValue(ref psychologicalTotalValue, ref survivalTotalValue);
            var canGiveChild = base.CheckCanGiveBirth(resultTextBuilder);
            if(survivalTotalValue < 0)
            {
                resultTextBuilder.AppendLine("Игроки которые попали в бункер имеют недостаточно навыков для выживания, " +
                    "поэтому они обречены на смерть");
            }
            if(psychologicalTotalValue < 0)
            {
                resultTextBuilder.AppendLine(GetLoseDescriptionByPsychological());
            }
            return new TotalGameValue(psychologicalTotalValue, survivalTotalValue, canGiveChild);
        }
        private string GetLoseDescriptionByPsychological()
        {
            return characters.Count switch
            {
                > 1 => "Игроки в бункере находятся под постоянным психологическим напряжением. Во время очередного спора " +
                $"{characters[0].Name} убивает в порыве ярости {characters[random.Next(1,characters.Count)].Name}",
                1 => $"Игрок {characters[0].Name} не выдержал психологического напряжения и покончил жизнь самоубийством",
                _ => "",
            };
        }
        private void CalculateBunkerTotalValue(ref double psychologicalValue, ref double survivalTotalValue)
        {
            survivalTotalValue += bunker.Condition.Value;
            survivalTotalValue += bunker.Enviroment.Value;
            CalculateItemsBunkerValue(bunker.Items, ref psychologicalValue, ref survivalTotalValue);
            CalculateBunkerBuildingsValue(bunker.Buildings, ref psychologicalValue, ref survivalTotalValue);

        }
        private void CalculateCharactersTotalValue(ref double psychologicalValue, ref double survivalTotalValue)
        {
            var canHealDesease = characters.CanHealDesease(bunker,resultTextBuilder);
            var canHealPhobia = characters.CanHealPhobia(resultTextBuilder);
            foreach (var character in characters.Select(c=>c.Character))
            {
                GetTotalProfessionsValue(character, ref psychologicalValue, ref survivalTotalValue);
                if (!canHealDesease)
                    CalculateHealthValue(character.Health, ref psychologicalValue, ref survivalTotalValue);
                if (!canHealPhobia)
                    CalculatePhobiaValue(character.Phobia, ref psychologicalValue, ref survivalTotalValue);
                CalculateHobbyValue(character.Hobby, ref psychologicalValue, ref survivalTotalValue);
                CalculateTraitValue(character.Trait, ref psychologicalValue, ref survivalTotalValue);
                CalculateAdditionalInfValue(character.AdditionalInformation, ref psychologicalValue, ref survivalTotalValue);
                CalculateCharacterItemsValue(character.Items, ref psychologicalValue, ref survivalTotalValue);
            }
        }
        private void CalculateCharacterItemsValue(IEnumerable<Domain.Characters.CharacterComponents.Item> items, ref double psychologicalValue, ref double survivalTotalValue)
        {
            foreach (var item in items)
            {
                switch (item.CharacterItemType)
                {
                    case CharacterItemType.Communication:
                    case CharacterItemType.Food:
                    case CharacterItemType.Entertainment:
                        psychologicalValue += item.Value;
                        break;
                    case CharacterItemType.Seeds:
                    case CharacterItemType.Surviving:
                    case CharacterItemType.Tools:
                    case CharacterItemType.Medicines:
                    case CharacterItemType.Gun:
                    case CharacterItemType.Hunting:
                        survivalTotalValue += item.Value;
                        break;
                }
            }
        }
        private void CalculateAdditionalInfValue(AdditionalInformation additionalInformation, ref double psychologicalValue, ref double survivalTotalValue)
        {
            switch (additionalInformation.AddInfType)
            {
                case AddInfType.Entertainment:
                case AddInfType.HealingPhobia:
                case AddInfType.Communication:
                    psychologicalValue += additionalInformation.Value;
                    break;
                case AddInfType.Surviving:
                case AddInfType.Driving:
                case AddInfType.Healing:
                case AddInfType.Cooking:
                case AddInfType.Repairing:
                case AddInfType.Shooting:
                case AddInfType.Planting:
                case AddInfType.AnimalBreeding:
                case AddInfType.Programming:
                    survivalTotalValue += additionalInformation.Value;
                    break;

            }
        }
        private void CalculateTraitValue(Trait trait, ref double psychologicalValue, ref double survivalTotalValue)
        {
            switch (trait.TraitType)
            {
                case TraitType.Surviving:
                case TraitType.Negative:
                    survivalTotalValue += trait.Value;
                    break;
                case TraitType.Entertainment:
                    psychologicalValue += trait.Value;
                    break;
            }
        }
        private void CalculateHobbyValue(Hobby hobby, ref double psychologicalValue, ref double survivalTotalValue)
        {
            var hobbyValue = hobby.Experience == 0 ? 0 : hobby.Value;
            switch (hobby.HobbyType)
            {
                case HobbyType.Entertainment:
                case HobbyType.HealingPhobia:
                case HobbyType.Communication:
                    psychologicalValue += hobbyValue;
                    break;
                case HobbyType.Surviving:
                case HobbyType.Driving:
                case HobbyType.Healing:
                case HobbyType.Cooking:
                case HobbyType.Repairing:
                case HobbyType.Shooting:
                case HobbyType.Planting:
                case HobbyType.AnimalBreeding:
                case HobbyType.Programming:
                    survivalTotalValue += hobbyValue;
                    break;
            }
        }
        private void CalculateHealthValue(Health health, ref double psychologicalValue, ref double survivalTotalValue)
        {
            switch (health.HealthType)
            {
                case HealthType.Psychological:
                    psychologicalValue += health.Value;
                    break;
                case HealthType.SpreadDisease:
                case HealthType.LiteDesease:
                    survivalTotalValue += health.Value;
                    break;
            }
        }
        private void CalculatePhobiaValue(Phobia phobia, ref double psychologicalValue, ref double survivalTotalValue)
        {
            switch (phobia.PhobiaDebuffType)
            {
                case PhobiaDebuffType.Surviving:
                    survivalTotalValue += phobia.Value;
                    break;
                case PhobiaDebuffType.Entertainment:
                    psychologicalValue += phobia.Value;
                    break;
            }

        }
        private void GetTotalProfessionsValue(Character character, ref double psychologicalValue, ref double survivalTotalValue)
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
            double professionValue = character.Profession.Experience > 0 ? profession.Value : 0 * 
                (character.Age.Years > withoutDebuffGiveBirthAge ? 0.6 : 1);
            switch (profession.ProfessionType)
            {
                case ProfessionType.Entertaining:
                    psychologicalValue += professionValue;
                    break;
                case ProfessionType.Scientific:
                    var halfValue = professionValue / 2;
                    psychologicalValue += halfValue;
                    survivalTotalValue += halfValue;
                    break;
                case ProfessionType.Surviving:
                    survivalTotalValue += professionValue * indexBodyMultiplier;
                    break;
            }
        }
        private static void CalculateItemsBunkerValue(IEnumerable<Domain.GameSessions.Bunkers.Item> itemBunkers, ref double psychologicalValue, ref double survivalTotalValue)
        {
            foreach (var item in itemBunkers)
            {
                switch (item.ItemBunkerType)
                {
                    case ItemBunkerType.Surviving:
                    case ItemBunkerType.Tools:
                    case ItemBunkerType.EducationElectric:
                    case ItemBunkerType.EducationBuilding:
                        survivalTotalValue += item.Value;
                        break;
                    case ItemBunkerType.Entertainment:
                    case ItemBunkerType.Education:
                        psychologicalValue += item.Value;
                        break;
                    case ItemBunkerType.Communication:
                        var halfValue = item.Value / 2;
                        psychologicalValue += halfValue;
                        survivalTotalValue += halfValue;
                        break;
                }
            }
        }
        private static void CalculateBunkerBuildingsValue(IEnumerable<Building> buildings, ref double psychologicalValue, ref double survivalTotalValue)
        {
            foreach (var obj in buildings)
            {
                switch (obj.BunkerObjectType)
                {
                    case BunkerObjectType.Entertainment:
                    case BunkerObjectType.Education:
                        psychologicalValue += obj.Value;
                        break;
                    case BunkerObjectType.HealPlace:
                    case BunkerObjectType.CombatPotential:
                    case BunkerObjectType.Transport:
                    case BunkerObjectType.Surviving:
                    case BunkerObjectType.ToolPlace:
                        survivalTotalValue += obj.Value;
                        break;
                }
            }
        }
        private bool IsBunkerBroken()
        {
            return bunker.Condition.BunkerState != BunkerState.Broken || characters.CanFixBunker(bunker.Items, resultTextBuilder);
        }

    }
}
