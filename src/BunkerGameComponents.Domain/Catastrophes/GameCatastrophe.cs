using BunkerGame.GameTypes.GameComponentTypes;
using System.Text;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.Catastrophes
{
    public class GameCatastrophe : AggregateRoot<ComponentId>, IGameComponent
    {
        public GameCatastrophe(ComponentId id)
        {
            Id = id;
            Description = "unkwnown";
        }
        [JsonInclude]
        public string Description { get; set; }
        [JsonInclude]
        public CatastropheType CatastropheType { get; set; } = CatastropheType.None;
        [JsonInclude]
        public int HidingTerm { get; set; }
        [JsonInclude]
        public short DestructionPercent { get; set; }
        [JsonInclude]
        public short SurvivedPopulationPercent { get; set; }
        [JsonInclude]
        public double Value { get; set; } = -10;
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("&#128163; Катаклизм:\n" + Description);
            builder.AppendLine($"Остаток выжившего населения: {SurvivedPopulationPercent}%");
            builder.AppendLine($"Разрушения на поверхности: {DestructionPercent}%");
            builder.AppendLine($"Необходимое время проживания в бункере: {HidingTerm} {getYearString(HidingTerm)}");
            return builder.ToString();

        }
        public void UpdateDestructionPercent()
        {

        }
        public void UpdateValue(double value)
        {
            Value = value;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException($"\"{nameof(description)}\" не может быть неопределенным или пустым.", nameof(description));
            }
            Description = description;
        }
        private static string getYearString(int yearCount)
        {
            string year;
            if (yearCount > 10 && yearCount < 20)
                return "лет";
            switch (yearCount % 10)
            {
                case 1:
                    year = "год";
                    break;
                case int e when e > 1 && e < 5:
                    year = "года";
                    break;
                default:
                    year = "лет";
                    break;
            }
            return year;
        }

    }
}

