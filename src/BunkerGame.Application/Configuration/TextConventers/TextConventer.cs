using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Configuration.TextConventers
{
    public static class TextConventer
    {
        public static string ConvertNumberToYears(int yearCount)
        {
            if (yearCount > 10 && yearCount < 20)
                return "лет";
            string year = (yearCount % 10) switch
            {
                1 => "год",
                int e when (e > 1 && e < 5) => "года",
                _ => "лет",
            };
            return year;
        }
    }
}
