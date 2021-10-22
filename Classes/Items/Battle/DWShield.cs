using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWShield : DWItem
    {
        public int DefensePower = 0;

        public DWShield()
        {
            string basePath = DWGlobals.DWImagePath + "Shields.";

            Name = "Nothing";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[4]
            {
                ("shield-no.png", "Nothing", 0),
                ("small.png", "Small Shield", 4),
                ("large.png", "Large Shield", 10),
                ("silver.png", "Silver Shield", 20)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return memData.ReadU8(0xBE) & 0x3;
        }
        public override void Update(int value, bool force = false)
        {
            DefensePower = ItemInfo[value].ExtraValue;
            base.Update(value, force);
        }

    }
}
