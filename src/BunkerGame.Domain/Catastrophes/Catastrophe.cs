using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Catastrophes
{
    public class Catastrophe
    {
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
        // можно добавить поля с процентом выжившего населения и т.д.(применить для расчетов)
        public int Id { get; set; }
        public string Description { get; set; }
        public CatastropheType CatastropheType { get; set; } = CatastropheType.None;
        public int HidingTerm { get; set; }
        public short DestructionPercent { get; set; }
        public short SurvivedPopulationPercent { get; set; }
        
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

    public enum CatastropheType
    {
        AgressiveEnemy,
        BadEcosystem,
        None
    }
}

