using BunkerGame.Domain.GameSessions.Bunkers;

namespace BunkerGame.VkApi.VkGame.VkExtensions
{
    public static class BunkerComponentsStringConventer
    {
        public static string ConvertBunkerEnviroment(Enviroment bunkerEnviroment)
            => "&#128495; В убежище живут:" + bunkerEnviroment.Description;
        public static string ConvertBunkerWall(Condition bunkerWall)
            => bunkerWall.Description;
        public static string ConvertBunkerObjects(IEnumerable<Building> bunkerObjects)
        {
            var str = string.Empty;
            foreach (var item in bunkerObjects)
            {
                str += "&#127968; В убежище оборудовано: " + item.Description + Environment.NewLine;
            }
            return str;
        }
        public static string ConvertBunkerItems(IEnumerable<Item> itemBunkers)
        {
            var str = string.Empty;
            foreach (var item in itemBunkers)
            {
                str += "&#128093; В убежище есть: " + item.Description + Environment.NewLine;
            }
            return str;
        }
    }
}
