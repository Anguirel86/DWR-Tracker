using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWBridge : DWItem
    {
        public DWBridge()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Charlock Bridge";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = true;
            allowsMultiple = false;
            forceOwnRead = true;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("bridge-grey.png", "Charlock Bridge", 0),
                ("bridge.png", "Charlock Bridge", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return (memData.ReadU8(0xCF) & 0x8) > 0 ? 1 : 0;
        }
    }
}
