using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWMagicKey : DWItem
    {
        public DWMagicKey()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Magic Key";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = true;
            ShowCount = true;
            Count = 0;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("key-grey.png", "Magic Key", 0),
                ("key.png", "Magic Key", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return memData.ReadU8(0xBF);
        }
    }
}
