using BunkerGame.Domain.Characters.Cards;
using BunkerGame.Domain.GameSessions.Bunkers;
using BunkerGame.GameTypes.BunkerTypes;
using BunkerGame.GameTypes.CharacterTypes;
using System.Text;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public static class ResultCounterExtensions
    {
        public static void UsePassiveCards(this List<CharacterWithName> characters, StringBuilder stringBuilder)
        {
            List<CharacterWithName> charactersForDelete = new List<CharacterWithName>();
            foreach (CharacterWithName characterWithName in characters)
            {
                var character = characterWithName.Character;
                var passiveCards = character.Cards.Where(c => c.Card.CardMethod.MethodType == MethodType.PassiveRemove && c.Card.CardMethod.MethodDirection == MethodDirection.Character);
                foreach (Card card in passiveCards.Select(c => c.Card))
                {
                    var characterForDelete = characters.Find(c => c.Character.Id != character.Id);
                    if (characterForDelete != null)
                    {
                        stringBuilder.Append("У игрока ").Append(characterWithName.Name).Append(" была пассивная карта ").Append(card.Description).Append(". ")
                            .Append("Игрок ").Append(characterForDelete.Name).AppendLine(" не попадает в бункер");
                        charactersForDelete.Add(characterForDelete);
                    }
                }
            }
            characters.RemoveAll(c => charactersForDelete.Any(d => d.Character.Id == c.Character.Id));

        }
        public static void RemoveCharactersWithDeathDesease(this List<CharacterWithName> characters, StringBuilder stringBuilder)
        {

            for (int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].Character.Health.HealthType == HealthType.DeadDesease)
                {
                    stringBuilder.Append("Игрок ").Append(characters[i].Name).Append(" умирает от болезни ").Append(characters[i].Character.Health.Description).AppendLine(" через день после входа в бункер");
                    characters.RemoveAt(i);
                }
            }
        }
        public static void RemoveCharactersWithDeathDesease(this List<CharacterWithName> characters)
        {

            for (int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].Character.Health.HealthType == HealthType.DeadDesease)
                {
                    characters.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Find woman and men with positive Childbearing
        /// </summary>
        /// <returns>if exists two characters different sex return true</returns>
        public static bool CanCharactersGiveBirth(this List<CharacterWithName> characters)
        {
            bool manCanGiveBirth = characters.Any(c => c.Character.Sex.Name == "мужчина" && c.Character.Childbearing.CanGiveBirth);
            bool womanCanGiveBirth = characters.Any(c => c.Character.Sex.Name == "женщина" && c.Character.Childbearing.CanGiveBirth);
            return manCanGiveBirth && womanCanGiveBirth;
        }
        public static (List<CharacterWithName> canGiveBirthMen, List<CharacterWithName> canGiveBirthWomen) FindGiveBirthCharacters(this List<CharacterWithName> characters)
        {
            var womenCanGiveBirth = new List<CharacterWithName>();
            var menCanGiveBirth = new List<CharacterWithName>();
            foreach (var character in characters)
            {
                if (character.Character.Sex.Name == "мужчина" && character.Character.Childbearing.CanGiveBirth)
                    menCanGiveBirth.Add(character);
                else if (character.Character.Sex.Name == "женщина" && character.Character.Childbearing.CanGiveBirth)
                    womenCanGiveBirth.Add(character);
            }
            return new(menCanGiveBirth, womenCanGiveBirth);
        }
        public static bool TrySpreadDesease(this List<CharacterWithName> characters, StringBuilder stringBuilder)
        {
            var characterWithSpreadDesease = characters.Where(c => c.Character.Health.HealthType == HealthType.SpreadDisease)
                .OrderBy(c => c.Character.Health.Value).FirstOrDefault();
            if (characterWithSpreadDesease == null)
                return false;
            characters.ForEach(c => c.Character.UpdateHealth(characterWithSpreadDesease.Character.Health));
            stringBuilder.Append("Игрок ").Append(characterWithSpreadDesease.Name).Append(" заразил всех болезнью ").
                AppendLine(characterWithSpreadDesease.Character.Health.Description);
            return true;

        }
        public static bool TrySpreadDesease(this List<CharacterWithName> characters)
        {
            var characterWithSpreadDesease = characters.Where(c => c.Character.Health.HealthType == HealthType.SpreadDisease).
                OrderBy(c => c.Character.Health.Value).FirstOrDefault();
            if (characterWithSpreadDesease == null)
                return false;
            characters.ForEach(c => c.Character.UpdateHealth(characterWithSpreadDesease.Character.Health));
            return true;

        }
        public static bool CanHealDesease(this IEnumerable<CharacterWithName> characters, Bunker bunker, StringBuilder stringBuilder)
        {
            var doctor = characters.FirstOrDefault(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Healing && c.Character.Profession.Experience > 0)
            || c.Character.AdditionalInformation.AddInfType == AddInfType.Healing || (c.Character.Hobby.HobbyType == HobbyType.Healing && c.Character.Hobby.Experience > 0));
            if (doctor == null)
                return false;
            if (characters.Any(c => c.Character.Items.Any(c => c.CharacterItemType == CharacterItemType.Medicines))
                || bunker.Buildings.Any(c => c.BunkerObjectType == BunkerObjectType.HealPlace))
            {
                stringBuilder.Append("Игрок ").Append(doctor.Name).AppendLine(" может вылечить болезни, штрафы за болезни убраны!");
                return true;
            }
            return false;
        }
        public static bool CanHealDesease(this IEnumerable<CharacterWithName> characters, Bunker bunker)
        {
            var doctor = characters.FirstOrDefault(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Healing
            && c.Character.Profession.Experience > 0)
            || c.Character.AdditionalInformation.AddInfType == AddInfType.Healing || (c.Character.Hobby.HobbyType == HobbyType.Healing
            && c.Character.Hobby.Experience > 0));
            if (doctor == null)
                return false;
            if (characters.Any(c => c.Character.Items.Any(c => c.CharacterItemType == CharacterItemType.Medicines))
                || bunker.Buildings.Any(c => c.BunkerObjectType == BunkerObjectType.HealPlace))
            {
                return true;
            }
            return false;
        }
        public static bool CanHealPhobia(this IEnumerable<CharacterWithName> characters, StringBuilder stringBuilder)
        {
            var doctor = characters.FirstOrDefault(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.HealingPhobia
            && c.Character.Profession.Experience > 0)
           || c.Character.AdditionalInformation.AddInfType == AddInfType.HealingPhobia 
           || (c.Character.Hobby.HobbyType == HobbyType.HealingPhobia && c.Character.Hobby.Experience > 0));
            if (doctor == null)
                return false;
            stringBuilder.Append("игрок под номером ").Append(doctor.Name).AppendLine(" может вылечить фобии, штрафы за фобии убраны!");
            return true;
        }
        public static bool CanHealPhobia(this IEnumerable<CharacterWithName> characters)
        {
            return characters.Any(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.HealingPhobia 
            && c.Character.Profession.Experience > 0)
            || c.Character.AdditionalInformation.AddInfType == AddInfType.HealingPhobia 
            || (c.Character.Hobby.HobbyType == HobbyType.HealingPhobia && c.Character.Hobby.Experience > 0));

        }
        public static bool CanFixBunker(this IEnumerable<CharacterWithName> characters, IEnumerable<Item> bunkerItems)
        {
            bool existsRepairCharacter = characters.Any(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Repairing && c.Character.Profession.Experience > 0)
             || c.Character.AdditionalInformation.AddInfType == AddInfType.Repairing 
             || (c.Character.Hobby.HobbyType == HobbyType.Repairing 
             && c.Character.Hobby.Experience > 0));
            bool existsTools = characters.Any(c => c.Character.Items.Any(c => c.CharacterItemType == CharacterItemType.Tools))
                || bunkerItems.Any(c => c.ItemBunkerType == ItemBunkerType.Tools);
            return existsRepairCharacter && existsTools;
        }
        public static bool CanFixBunker(this IEnumerable<CharacterWithName> characters, IEnumerable<Item> bunkerItems, StringBuilder stringBuilder)
        {
            var repairCharacter = characters.FirstOrDefault(c => (c.Character.Profession.ProfessionSkill == ProfessionSkill.Repairing 
            && c.Character.Profession.Experience > 0)
             || c.Character.AdditionalInformation.AddInfType == AddInfType.Repairing 
             || (c.Character.Hobby.HobbyType == HobbyType.Repairing && c.Character.Hobby.Experience > 0));
            if (repairCharacter == null)
                return false;
            bool existsTools = characters.Any(c => c.Character.Items.Any(c => c.CharacterItemType == CharacterItemType.Tools))
                || bunkerItems.Any(c => c.ItemBunkerType == ItemBunkerType.Tools);
            if (!existsTools)
                return false;
            stringBuilder.Append("Игрок ").Append(repairCharacter.Name).AppendLine(" может починить бункер, шансы на выживание повысились");
            return true;
        }
    }
}

