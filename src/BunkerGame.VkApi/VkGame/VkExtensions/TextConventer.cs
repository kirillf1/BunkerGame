namespace BunkerGame.VkApi.VkGame.VkExtensions
{
    public static class TextConventer
    {
        public static string ConvertNumberToYears(int yearCount)
        {
            if (yearCount > 10 && yearCount < 20)
                return "лет";
            return (yearCount % 10) switch
            {
                1 => "год",
                int e when e > 1 && e < 5 => "года",
                _ => "лет",
            };
        }
    }
}
