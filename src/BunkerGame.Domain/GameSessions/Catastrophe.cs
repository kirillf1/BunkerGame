using BunkerGame.GameTypes.GameComponentTypes;
using System.Text;

namespace BunkerGame.Domain.GameSessions
{
    public class Catastrophe : Value<Catastrophe>
    {
        public static readonly Catastrophe DefaultCatastrophe = new(CatastropheType.None, 10, 10, "unknown", 10, 10);
        private Catastrophe() { }
        public Catastrophe(CatastropheType catastropheType, short destructionPercent,
            short survivedPopulationPercent, string description, double value, int hidingTerm)
        {
            CatastropheType = catastropheType;
            DestructionPercent = destructionPercent;
            SurvivedPopulationPercent = survivedPopulationPercent;
            Description = description;
            Value = value;
            HidingTerm = hidingTerm;
        }
        public string Description { get; }
        public CatastropheType CatastropheType { get; }
        public int HidingTerm { get; }
        public short DestructionPercent { get; }
        public short SurvivedPopulationPercent { get; }
        public double Value { get; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("&#128163; Катаклизм:\n" + Description);
            builder.AppendLine($"Остаток выжившего населения: {SurvivedPopulationPercent}%");
            builder.AppendLine($"Разрушения на поверхности: {DestructionPercent}%");
            builder.AppendLine($"Необходимое время проживания в бункере: {HidingTerm} {getYearString(HidingTerm)}");
            return builder.ToString();

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

