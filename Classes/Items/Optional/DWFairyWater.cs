using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWFairyWater : DWItem
    {
        public DWFairyWater()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Fairy Water";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = true;
            ShowCount = true;
            Count = 0;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("fairy_water-grey.png", "Fairy Water", 0),
                ("fairy_water.png", "Fairy Water", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
