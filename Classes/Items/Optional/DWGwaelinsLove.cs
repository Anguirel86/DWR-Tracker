using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWGwaelinsLove : DWItem
    {
        public DWGwaelinsLove()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Gwaelin's Love";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("gwaelin-grey.png", "Gwaelin's Love", 0),
                ("gwaelin.png", "Gwaelin's Love", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
