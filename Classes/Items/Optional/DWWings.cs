using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWWings : DWItem
    {
        public DWWings()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Wings";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = true;
            ShowCount = true;
            Count = 0;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("wings-grey.png", "Wings", 0),
                ("wings.png", "Wings", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
