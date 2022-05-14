using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Characters.CharacterComponents;

namespace BunkerGame.VkApi.VkExtensions
{
    public static class CharacterComponentStringConventer
    {
       
        public static string CovertCharacterItem(IEnumerable<CharacterItem> characterItems)
        {
            int itemCount = 1;
            string str = string.Empty;
            foreach (var characterItem in characterItems)
            {
                str += $"&#128093; Багаж №{itemCount}: {characterItem.Description}";
                itemCount++;
            }
            return str;
        }
        public static string CovertCharacterCards(IEnumerable<Card> characterCards)
        {
            int itemCount = 1;
            string str = string.Empty;
            foreach (var characterCard in characterCards)
            {
                str += $"&#128179; Карта №{itemCount}: {characterCard.Description} {Environment.NewLine}";
                itemCount++;
            }
            return str;
        }
        public static string ConvertAge(Age age)
        {
            return $"&#128197; Возраст:{age.Count} {TextConventer.ConvertNumberToYears(age.Count)}";
        }
        public static string ConvertAddInf(AdditionalInformation additionalInformation)
        {
            return "&#128221; Дополнительная информация: " + additionalInformation.Description;
        }
        public static string ConvertChildbearing(Childbearing childbearing)
        {
            return "&#128107; Деторождение: " + (childbearing.CanGiveBirth ? "не childfree" : "childfree");
        }
        public static string ConvertHealth(Health health)
        {
            return "&#128154; Здоровье: " + health.Description;
        }
        public static string ConvertHobby(Hobby hobby, int expHobby)
            => $"&#128161; Хобби: {hobby.Description} опыт {expHobby} {TextConventer.ConvertNumberToYears(expHobby)}";
        public static string ConvertPhobia(Phobia phobia) => "&#128526; Фобия: " + phobia.Description;
        public static string ConvertProfession(Profession profession, int expProf)
            => $"&#128084; Профессия {profession.Description} стаж: {expProf} {TextConventer.ConvertNumberToYears(expProf)}";
        public static string ConvertSex(Sex sex)
        {
            var sexEmojii = sex.Name == "мужчина" ? "&#128102;" : "&#128105;";
            return $"{sexEmojii} Пол: {sex.Name}";
        }
        public static string ConvertSize(Size size)
        {
            return $"&#128170; Телосложение: вес: {size.Weight} кг., рост: {size.Height} см. - {size.GetAvagereIndexBody()}";
        }
        public static string ConvertTrait(Trait trait) => "&#128540; Черта характера: " + trait.Description;

    }
}
