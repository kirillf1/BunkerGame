using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.GameSessions.ResultCounters
{
    internal static class ResultCounterExtensions
    {
        public static void UsePassiveCards(this List<Character> characters,StringBuilder stringBuilder)
        {
            List<Character> charactersForDelete = new List<Character>();
            foreach (Character character in characters)
            {
                var passiveCards = character.Cards.Where(c => c.CardMethod.MethodType == MethodType.PassiveRemove && c.CardMethod.MethodDirection == MethodDirection.Character);
                foreach (Card card in passiveCards)
                {
                    var characterForDelete = characters.Find(c => c.Id != character.Id);
                    if (characterForDelete != null)
                    {
                        stringBuilder.Append("У игрока №").Append(character.CharacterNumber).Append(" была пассивная карта ").Append(card.Description).Append(". ")
                            .Append("Игрок под номером ").Append(characterForDelete.CharacterNumber).AppendLine(" не попадает в бункер");
                        charactersForDelete.Add(characterForDelete);
                    }
                }
            }
            characters.RemoveAll(c => charactersForDelete.Any(d => d.Id == c.Id));
            
        }
        public static void RemoveCharactersWithDeathDesease(this List<Character> characters, StringBuilder stringBuilder)
        {

            for (int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].Health.HealthType == HealthType.DeadDesease)
                {
                    stringBuilder.Append("Игрок под номером ").Append(characters[i].CharacterNumber).Append(" умирает от болезни ").Append(characters[i].Health.Description).AppendLine(" через день после входа в бункер");
                    characters.RemoveAt(i);
                }
            }
        }
        public static void RemoveCharactersWithDeathDesease(this List<Character> characters)
        {

            for (int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].Health.HealthType == HealthType.DeadDesease)
                {
                    characters.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Find woman and men with positive Childbearing
        /// </summary>
        /// <returns>if exists two characters different sex return true</returns>
        public static bool CanCharactersGiveBirth(List<Character> characters)
        {
            bool manCanGiveBirth = characters.Any(c => c.Sex.Name == "мужчина" && c.Childbearing.CanGiveBirth);
            bool womanCanGiveBirth = characters.Any(c => c.Sex.Name == "женщина" && c.Childbearing.CanGiveBirth);
            return manCanGiveBirth && womanCanGiveBirth;
        }
        public static (List<Character> canGiveBirthMen, List<Character> canGiveBirthWomen) FindGiveBirthCharacters(this List<Character> characters)
        {
            var womenCanGiveBirth = new List<Character>();
            var menCanGiveBirth = new List<Character>();
            foreach (var character in characters)
            {
                if (character.Sex.Name == "мужчина" && character.Childbearing.CanGiveBirth)
                    menCanGiveBirth.Add(character);
                else if (character.Sex.Name == "женщина" && character.Childbearing.CanGiveBirth)
                    womenCanGiveBirth.Add(character);
            }
            return new(menCanGiveBirth, womenCanGiveBirth);
        }
        public static bool TrySpreadDesease(this List<Character> characters, StringBuilder stringBuilder)
        {
            var characterWithSpreadDesease = characters.Where(c => c.Health.HealthType == HealthType.SpreadDisease).OrderBy(c => c.Health.Value).FirstOrDefault();
            if (characterWithSpreadDesease == null)
                return false;
            characters.ForEach(c => c.UpdateHealth(characterWithSpreadDesease.Health));
            stringBuilder.Append("Игрок №").Append(characterWithSpreadDesease.CharacterNumber).Append(" заразил всех болезнью ").
                AppendLine(characterWithSpreadDesease.Health.Description);
            return true;

        }
        public static bool TrySpreadDesease(this List<Character> characters)
        {
            var characterWithSpreadDesease = characters.Where(c => c.Health.HealthType == HealthType.SpreadDisease).OrderBy(c => c.Health.Value).FirstOrDefault();
            if (characterWithSpreadDesease == null)
                return false;
            characters.ForEach(c => c.UpdateHealth(characterWithSpreadDesease.Health));
            return true;

        }
        public static bool CanHealDesease(IEnumerable<Character> characters, Bunker bunker, StringBuilder stringBuilder)
        {
            var doctor = characters.FirstOrDefault(c => (c.Profession.ProfessionSkill == ProfessionSkill.Healing && c.ExperienceProfession > 0)
            || c.AdditionalInformation.AddInfType == AddInfType.Healing || (c.Hobby.HobbyType == HobbyType.Healing && c.ExperienceHobby > 0));
            if (doctor == null)
                return false;
            if (characters.Any(c => c.CharacterItems.Any(c => c.CharacterItemType == CharacterItemType.Medicines))
                || bunker.BunkerObjects.Any(c => c.BunkerObjectType == BunkerObjectType.HealPlace))
            {
                stringBuilder.Append("Игрок под номером ").Append(doctor.CharacterNumber).AppendLine(" может вылечить болезни, штрафы за болезни убраны!");
                return true;
            }
            return false;
        }
        public static bool CanHealDesease(IEnumerable<Character> characters, Bunker bunker)
        {
            var doctor = characters.FirstOrDefault(c => (c.Profession.ProfessionSkill == ProfessionSkill.Healing && c.ExperienceProfession > 0)
            || c.AdditionalInformation.AddInfType == AddInfType.Healing || (c.Hobby.HobbyType == HobbyType.Healing && c.ExperienceHobby > 0));
            if (doctor == null)
                return false;
            if (characters.Any(c => c.CharacterItems.Any(c => c.CharacterItemType == CharacterItemType.Medicines))
                || bunker.BunkerObjects.Any(c => c.BunkerObjectType == BunkerObjectType.HealPlace))
            {
                return true;
            }
            return false;
        }
        public static bool CanHealPhobia(IEnumerable<Character> characters, StringBuilder stringBuilder)
        {
            var doctor = characters.FirstOrDefault(c => (c.Profession.ProfessionSkill == ProfessionSkill.HealingPhobia && c.ExperienceProfession > 0)
           || c.AdditionalInformation.AddInfType == AddInfType.HealingPhobia || (c.Hobby.HobbyType == HobbyType.HealingPhobia && c.ExperienceHobby > 0));
            if (doctor == null)
                return false;
            stringBuilder.Append("игрок под номером ").Append(doctor.CharacterNumber).AppendLine(" может вылечить фобии, штрафы за фобии убраны!");
            return true;
        }
        public static bool CanHealPhobia(IEnumerable<Character> characters)
        {
            return characters.Any(c => (c.Profession.ProfessionSkill == ProfessionSkill.HealingPhobia && c.ExperienceProfession > 0)
            || c.AdditionalInformation.AddInfType == AddInfType.HealingPhobia || (c.Hobby.HobbyType == HobbyType.HealingPhobia && c.ExperienceHobby > 0));

        }
        public static bool CanFixBunker(IEnumerable<Character> characters, IEnumerable<ItemBunker> itemBunkers)
        {
            bool existsRepairCharacter = characters.Any(c => (c.Profession.ProfessionSkill == ProfessionSkill.Repairing && c.ExperienceProfession > 0)
             || c.AdditionalInformation.AddInfType == AddInfType.Repairing || (c.Hobby.HobbyType == HobbyType.Repairing && c.ExperienceHobby > 0));
            bool existsTools = characters.Any(c => c.CharacterItems.Any(c => c.CharacterItemType == CharacterItemType.Tools))
                || itemBunkers.Any(c => c.ItemBunkerType == ItemBunkerType.Tools);
            return existsRepairCharacter && existsTools;
        }
        public static bool CanFixBunker(IEnumerable<Character> characters, IEnumerable<ItemBunker> itemBunkers, StringBuilder stringBuilder)
        {
            var repairCharacter = characters.FirstOrDefault(c => (c.Profession.ProfessionSkill == ProfessionSkill.Repairing && c.ExperienceProfession > 0)
             || c.AdditionalInformation.AddInfType == AddInfType.Repairing || (c.Hobby.HobbyType == HobbyType.Repairing && c.ExperienceHobby > 0));
            if (repairCharacter == null)
                return false;
            bool existsTools = characters.Any(c => c.CharacterItems.Any(c => c.CharacterItemType == CharacterItemType.Tools))
                || itemBunkers.Any(c => c.ItemBunkerType == ItemBunkerType.Tools);
            if (!existsTools)
                return false;
            stringBuilder.Append("игрок №").Append(repairCharacter.CharacterNumber).AppendLine(" может починить бункер, шансы на выживание повысились");
            return true;
        }
    }
}
