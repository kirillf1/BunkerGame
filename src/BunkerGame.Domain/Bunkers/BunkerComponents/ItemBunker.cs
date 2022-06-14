using System;
using System.Collections.Generic;
using System.Text;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class ItemBunker : BunkerComponentEntity
    {

        public ItemBunker(double value, string description, ItemBunkerType itemBunkerType = ItemBunkerType.Useless) : base(value, description)
        {
            ItemBunkerType = itemBunkerType;
        }
        public ItemBunkerType ItemBunkerType { get; private set; }
        private List<Bunker> Bunkers = new List<Bunker>();
        public void UpdateType(ItemBunkerType bunkerType)
        {
            ItemBunkerType = bunkerType;
        }
        ////to 5

        //public List<Bunker> Bunkers { get; set; }
        public override string ToString()
        {
            return Description;
        }
    }
    public enum ItemBunkerType
    {

        Entertainment,
        Surviving,
        Tools,
        Education,
        Communication,
        Useless,
        EducationElectric,
        EducationBuilding
    }
}
