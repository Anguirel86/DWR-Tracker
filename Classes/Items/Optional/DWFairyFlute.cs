using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWFairyFlute : DWItem
    {
        public DWFairyFlute()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Fairy Flute";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("flute-grey.png", "Fairy Flute", 0),
                ("flute.png", "Fairy Flute", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
