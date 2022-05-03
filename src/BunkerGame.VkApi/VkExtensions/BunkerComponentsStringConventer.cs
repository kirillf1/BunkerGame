using BunkerGame.Domain.Bunkers.BunkerComponents;

namespace BunkerGame.VkApi.VkExtensions
{
    public static class BunkerComponentsStringConventer
    {
        public static string ConvertBunkerEnviroment(BunkerEnviroment bunkerEnviroment)
            => "&#128495; В убежище живут:" + bunkerEnviroment.Description;
        public static string ConvertBunkerWall(BunkerWall bunkerWall)
            => bunkerWall.Description;
        public static string ConvertBunkerObjects(IEnumerable<BunkerObject> bunkerObjects)
        {
            var str = string.Empty;
            foreach (var item in bunkerObjects)
            {
                str+= "&#127968; В убежище оборудовано: "+ item.Description+Environment.NewLine;
            }
            return str;
        }
        public static string ConvertBunkerItems(IEnumerable<ItemBunker> itemBunkers)
        {
            var str = string.Empty;
            foreach (var item in itemBunkers)
            {
                str += "&#128093; В убежище есть: "  + item.Description + Environment.NewLine;
            }
            return str;
        }
    }
}
