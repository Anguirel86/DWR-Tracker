using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWDeathNecklace : DWItem
    {
        public int AttackPower = 0;

        public DWDeathNecklace()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Death Necklace";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("reckless_necklace_th-grey.png", "Death Necklace", 0),
                ("reckless_necklace_th.png", "Death Necklace", 10)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return (memData.ReadU8(0xCF) & 0x80) > 0 ? 1 : 0;
        }

        public override void Update(int value, bool force = false)
        {
            AttackPower = ItemInfo[value].ExtraValue;
            base.Update(value, force);
        }
    }
}
