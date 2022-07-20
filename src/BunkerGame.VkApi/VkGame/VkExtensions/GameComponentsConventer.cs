using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;
using System.Text;

namespace BunkerGame.VkApi.VkGame.VkExtensions
{
    public static class GameComponentsConventer
    {
        public static string ConvertCatastrophe(Catastrophe catastrophe)
        {
            var builder = new StringBuilder();
            builder.Append("&#128163; Катаклизм:\n").AppendLine(catastrophe.Description);
            builder.Append("Остаток выжившего населения: ").Append(catastrophe.SurvivedPopulationPercent).AppendLine("%");
            builder.Append("Разрушения на поверхности: ").Append(catastrophe.DestructionPercent).AppendLine("%");
            builder.Append("Необходимое время проживания в бункере: ").Append(catastrophe.HidingTerm).Append(' ')
                .AppendLine(TextConventer.ConvertNumberToYears(catastrophe.HidingTerm));
            return builder.ToString();
        }
        public static string ConvertBunker(Bunker bunker)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _ = stringBuilder.AppendLine("&#128333; Убежище:\n");
            stringBuilder.Append("Площадь убежища ").Append(bunker.Size.Value).AppendLine(" квадратных метров");
            stringBuilder.Append("Припасов хватит на ").Append(bunker.Supplies.Years).Append(' ')
                .AppendLine($"{TextConventer.ConvertNumberToYears(bunker.Supplies.Years)}");
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerWall(bunker.Condition));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerObjects(bunker.Buildings));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerItems(bunker.Items));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerEnviroment(bunker.Enviroment));
            return stringBuilder.ToString();
        }
        public static string ConvertCharacter(Character character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertProfession(character.Profession));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertSex(character.Sex));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertAge(character.Age));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertChildbearing(character.Childbearing));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertSize(character.Size));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertHealth(character.Health));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertTrait(character.Trait));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertPhobia(character.Phobia));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertHobby(character.Hobby));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertAddInf(character.AdditionalInformation));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertCharacterItem(character.Items));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertCharacterCards(character.Cards));
            return stringBuilder.ToString();
        }
        public static string ConvertGameSession(GameSession gameSession)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(ConvertCatastrophe(gameSession.Catastrophe));
            stringBuilder.AppendLine(ConvertBunker(gameSession.Bunker));
            stringBuilder.Append("Количество мест: ").Append(gameSession.FreePlaceSize);
            return stringBuilder.ToString();
        }
    }
}
