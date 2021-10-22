using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWHerb : DWItem
    {
        public DWHerb()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Herb";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = true;
            ShowCount = true;
            Count = 0;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("herb-grey.png", "Herb", 0),
                ("herb.png", "Herb", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
