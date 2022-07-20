using BunkerGame.Domain.GameSessions;
using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;
using BunkerGame.GameTypes.GameComponentTypes;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public class GameResultCounterHard : GameResultCounterMedium
    {
        public GameResultCounterHard(ResultCounterParams resultCounterParams) : base(resultCounterParams)
        {
        }
        public override ResultReport CalculateGameResut()
        {
            var result = base.CalculateGameResut();
            var foodValue = 0;
            bool isWinGame = result.IsWinGame;
            if (IsEnoughFood())
            {
                foodValue = 100;
                resultTextBuilder.AppendLine("У вас достаточно еды для проживания в бункере");
            }
            else
                isWinGame = false;
            return new ResultReport(result.GameReport.Replace("Вы победили", "").Replace("Вы проиграли", "") + resultTextBuilder.ToString(),
                result.EntertainmentValue,result.SurvivingValue,foodValue,isWinGame);
        }
        private bool IsEnoughFood()
        {
            if (CheckEnoughSuppliesToEndHiding(bunker.Supplies.Years))
                return true;
            var hasCook = HasCook();
            if (hasCook && CheckEnoughSuppliesToEndHiding(bunker.Supplies.Years * 1.25))
                return true;
            if (CanPlaceFood())
            {
                resultTextBuilder.AppendLine("Хоть у вас мало запасов еды, но вы сможете выращивать растения");
                return true;
            }
            if (CanHunt())
            {
                resultTextBuilder.AppendLine("Хоть у вас мало запасов еды, но у вас есть охотник и оружие");
                return true;
            }
            return false;
        }
        private bool CanHunt()
        {
            var hunter = characters.Find(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Hunting && c.Character.Profession.Experience > 0)
           || c.Character.AdditionalInformation.AddInfType == AddInfType.Shooting
           || (c.Character.Hobby.HobbyType == HobbyType.Shooting && c.Character.Hobby.Experience > 0));
            return CanLiveCreaturesOutsideBunker() && hunter != null && HasGuns();
        }
        private bool HasGuns()
        {
            var guns = characters.SelectMany(c => c.Character.Items).FirstOrDefault(c => c.CharacterItemType == CharacterItemType.Gun);
            if (guns != null)
                return true;
            var bunkerGuns = bunker.Buildings.FirstOrDefault(b => b.BunkerObjectType == BunkerObjectType.CombatPotential);
            if (bunkerGuns != null)
                return true;
            var gunsFromOutsideBunker = externalSurroundings.FirstOrDefault(c => c.SurroundingType == SurroundingType.Guns);
            return gunsFromOutsideBunker != null;

        }
        private bool CanPlaceFood()
        {
            var hasSeeds = HasSeeds();
            return hasSeeds && HasPlantSkills() && CanLiveCreaturesOutsideBunker() || CanPlantInsideBunker();
        }
        private bool CanLiveCreaturesOutsideBunker()
        {
            return catastrophe.CatastropheType != CatastropheType.BadEcosystem && catastrophe.DestructionPercent < 20;
        }
        private bool HasPlantSkills()
        {
            var farmer = characters.Find(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Planting && c.Character.Profession.Experience > 0)
            || c.Character.AdditionalInformation.AddInfType == AddInfType.Planting
            || (c.Character.Hobby.HobbyType == HobbyType.Planting && c.Character.Hobby.Experience > 0));
            if (farmer != null)
            {
                resultTextBuilder.AppendLine($"Игрок {farmer.Name} будет фермером");
                return true;
            }
            return false;
        }
        private bool CanPlantInsideBunker()
        {
            var plantPlace = bunker.Buildings.FirstOrDefault(b => b.BunkerObjectType == BunkerObjectType.PlantPlace);
            if (plantPlace != null)
            {
                resultTextBuilder.Append("Вы можете посадить семена в ").AppendLine(plantPlace.Description);
                return true;
            }
            return false;
        }
        private bool HasSeeds()
        {
            var seedFromCharacter = characters.SelectMany(c => c.Character.Items).FirstOrDefault(i => i.CharacterItemType == CharacterItemType.Seeds);
            if (seedFromCharacter != null)
            {
                resultTextBuilder.Append("У вас есть семена ").Append(seedFromCharacter.Description).AppendLine(" для посадки!");
                return true;
            }
            return false;
        }
        private bool CheckEnoughSuppliesToEndHiding(double suppliesYears)
        {
            return suppliesYears >= catastrophe.HidingTerm;
        }
        private bool HasCook()
        {
            var cook = characters.Find(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Cooking
            && c.Character.Profession.Experience > 0)
            || c.Character.AdditionalInformation.AddInfType == AddInfType.Cooking
            || (c.Character.Hobby.HobbyType == HobbyType.Cooking && c.Character.Hobby.Experience > 0));
            if (cook != null)
            {
                resultTextBuilder.Append("Игрок ").Append(cook.Name).
                    AppendLine(" может заниматься готовкой(шансы на выживание повышены и затраты на еду будут снижены)");
                return true;
            }
            return false;
        }

    }
}
