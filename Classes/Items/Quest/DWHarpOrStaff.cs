using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWHarpOrStaff : DWItem
    {
        public DWHarpOrStaff()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Silver Harp";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = true;
            IsFirstHalfQuestItem = true;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[3]
            {
                ("harp-grey.png", "Silver Harp", 0),
                ("harp.png", "Silver Harp", 0),
                ("staff_of_rain.png", "Staff of Rain", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
