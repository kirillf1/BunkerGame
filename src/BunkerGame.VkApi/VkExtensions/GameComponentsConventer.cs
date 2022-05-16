using BunkerGame.Application.Configuration.TextConventers;
using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using System.Text;

namespace BunkerGame.VkApi.VkExtensions
{
    public static class GameComponentsConventer
    {
        public static string ConvertCatastrophe(Catastrophe catastrophe)
        {
            var builder = new StringBuilder();
            builder.Append("&#128163; Катаклизм:\n").AppendLine(catastrophe.Description);
            builder.Append("Остаток выжившего населения: ").Append(catastrophe.SurvivedPopulationPercent).AppendLine("%");
            builder.Append("Разрушения на поверхности: ").Append(catastrophe.DestructionPercent).AppendLine("%");
            builder.Append("Необходимое время проживания в бункере: ").Append(catastrophe.HidingTerm).Append(' ').AppendLine(TextConventer.ConvertNumberToYears(catastrophe.HidingTerm));
            return builder.ToString();
        }
        public static string ConvertBunker(Bunker bunker)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _ = stringBuilder.AppendLine("&#128333; Убежище:\n");
            stringBuilder.Append("Площадь убежища ").Append(bunker.BunkerSize.Value).AppendLine(" квадратных метров");
            stringBuilder.Append("Припасов хватит на ").Append(bunker.Supplies.SuplliesYears).Append(' ').AppendLine(TextConventer.ConvertNumberToYears(bunker.Supplies.SuplliesYears));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerWall(bunker.BunkerWall));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerObjects(bunker.BunkerObjects));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerItems(bunker.ItemBunkers));
            stringBuilder.AppendLine(BunkerComponentsStringConventer.ConvertBunkerEnviroment(bunker.BunkerEnviroment));
            return stringBuilder.ToString();
        }
        public static string ConvertCharacter(Character character)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("&#128100; Игрок № ").Append(character.CharacterNumber).AppendLine();
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertProfession(character.Profession, character.ExperienceProfession));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertSex(character.Sex));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertAge(character.Age));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertChildbearing(character.Childbearing));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertSize(character.Size));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertHealth(character.Health));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertTrait(character.Trait));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertPhobia(character.Phobia));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertHobby(character.Hobby, character.ExperienceHobby));
            stringBuilder.AppendLine(CharacterComponentStringConventer.ConvertAddInf(character.AdditionalInformation));
            stringBuilder.AppendLine(CharacterComponentStringConventer.CovertCharacterItem(character.CharacterItems));
            stringBuilder.AppendLine(CharacterComponentStringConventer.CovertCharacterCards(
                character.Cards.Join(character.UsedCards,c=>c.Id, usedCard => usedCard.CardId, (c, u) => new { card = c,u.CardNumber}).
                OrderBy(c=>c.CardNumber).Select(c=> c.card )));
            return stringBuilder.ToString();
        }
        public static string ConvertGameSession(GameSession gameSession)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ConvertCatastrophe(gameSession.Catastrophe));
            stringBuilder.AppendLine(ConvertBunker(gameSession.Bunker));
            stringBuilder.Append("Количество мест: ").Append(gameSession.FreePlaceSize);
            return stringBuilder.ToString();
        }
    }
}
