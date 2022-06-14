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
    public class GameResultCounterMedium : ResultCounterBase, IGameResultCounter
    {
        protected record TotalGameValue(double PsychologicalValue, double SurvivalValue, bool CanGiveBirth);
        private List<Character> aliveCharacters;
        public GameResultCounterMedium(GameSession gameSession) : base(gameSession)
        {
            aliveCharacters = base.ExecuteAllCharacterEvents(gameResultStringBuilder, gameSession.Characters);
        }
        public Task<ResultGameReport> CalculateResult()
        {
           return Task.Run(() =>
            {
                var result = CalculateResult(gameSession, gameResultStringBuilder);
                bool isWinGame = result.CanGiveBirth && result.SurvivalValue > 0 && result.PsychologicalValue > 0;
                var gameReport = new ResultGameReport(gameResultStringBuilder.ToString(), result.PsychologicalValue, result.SurvivalValue, 0,
                    isWinGame, gameSession.GameName);
                gameResultStringBuilder.Clear();
                return gameReport;
            });
        }
        protected TotalGameValue CalculateResult(GameSession gameSession, StringBuilder stringBuilder)
        {
            var aliveCharacters = base.ExecuteAllCharacterEvents(stringBuilder, gameSession.Characters);
            var isBunkerBroken = IsBunkerBroken();
            var catastrophe = gameSession.Catastrophe;
            var externalSurroundings = gameSession.ExternalSurroundings;
            double psychologicalTotalValue = 0;
            double survivalTotalValue = 0;
            CalculateBunkerTotalValue(ref psychologicalTotalValue, ref survivalTotalValue);
            survivalTotalValue += GetTotalExternalSurroundingValue(gameSession.Bunker, isBunkerBroken, externalSurroundings);
            survivalTotalValue += CalculateCatastropheValue(catastrophe, isBunkerBroken);
            CalculateCharactersTotalValue(ref psychologicalTotalValue, ref survivalTotalValue);
            var canGiveChild = base.CheckCanGiveBirth(aliveCharacters, gameSession.ExternalSurroundings, stringBuilder);
            return new TotalGameValue(psychologicalTotalValue, survivalTotalValue, canGiveChild);
        }
        private void CalculateBunkerTotalValue(ref double psychologicalValue, ref double survivalTotalValue)
        {
            var bunker = gameSession.Bunker;
            survivalTotalValue += bunker.BunkerWall.Value;
            survivalTotalValue += bunker.BunkerEnviroment.Value;
            CalculateItemsBunkerValue(bunker.ItemBunkers, ref psychologicalValue, ref survivalTotalValue);
            CalculateBunkerObjectsValue(bunker.BunkerObjects, ref psychologicalValue, ref survivalTotalValue);
        
        }
        private void CalculateCharactersTotalValue(ref double psychologicalValue, ref double survivalTotalValue)
        {
            var canHealDesease = ResultCounterExtensions.CanHealDesease(aliveCharacters, gameSession.Bunker);
            var canHealPhobia = ResultCounterExtensions.CanHealPhobia(aliveCharacters, gameResultStringBuilder);
            foreach (var character in aliveCharacters)
            {
                GetTotalProfessionsValue(character, ref psychologicalValue, ref survivalTotalValue);
                if (!canHealDesease)
                    CalculateHealthValue(character.Health, ref psychologicalValue, ref survivalTotalValue);
                if (!canHealPhobia)
                    CalculatePhobiaValue(character.Phobia, ref psychologicalValue, ref survivalTotalValue);
                CalculateHobbyValue(character.Hobby, character.ExperienceHobby, ref psychologicalValue, ref survivalTotalValue);
                CalculateTraitValue(character.Trait, ref psychologicalValue, ref survivalTotalValue);
                CalculateAdditionalInfValue(character.AdditionalInformation, ref psychologicalValue, ref survivalTotalValue);
                CalculateCharacterItemsValue(character.CharacterItems, ref psychologicalValue, ref survivalTotalValue);
            }
        }
        private void CalculateCharacterItemsValue(IEnumerable<CharacterItem> items, ref double psychologicalValue, ref double survivalTotalValue)
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
        private void CalculateHobbyValue(Hobby hobby, int hobbyExp, ref double psychologicalValue, ref double survivalTotalValue)
        {
            var hobbyValue = hobbyExp == 0 ? 0 : hobby.Value;
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
            double professionValue = character.ExperienceProfession > 0 ? profession.Value : 0 * (character.Age.Count > withoutDebuffGiveBirthAge ? 0.6 : 1);
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
        private static void CalculateItemsBunkerValue(IEnumerable<ItemBunker> itemBunkers, ref double psychologicalValue, ref double survivalTotalValue)
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
        private static void CalculateBunkerObjectsValue(IEnumerable<BunkerObject> bunkerObjects, ref double psychologicalValue, ref double survivalTotalValue)
        {
            foreach (var obj in bunkerObjects)
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
            var bunker = gameSession.Bunker;
            return bunker.BunkerWall.BunkerState != BunkerState.Broken || ResultCounterExtensions.CanFixBunker(aliveCharacters, bunker.ItemBunkers, gameResultStringBuilder);
        }

    }

}
