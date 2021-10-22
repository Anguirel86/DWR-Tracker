using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWArmor : DWItem
    {
        public int DefensePower = 0;
        public float HurtResist = 0;
        public float BreathResist = 0;
        public bool StopspellImmunity = false;

        public DWArmor()
        {
            string basePath = DWGlobals.DWImagePath + "Armors.";

            Name = "Nothing";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[8]
            {
                ("armor-no.png", "Nothing", 0),
                ("clothes.png", "Clothes", 2),
                ("leather.png", "Leather Armor", 4),
                ("chain.png", "Chain Mail", 10),
                ("half.png", "Half Plate", 16),
                ("full.png", "Full Plate", 24),
                ("magic.png", "Magic Armor", 24),
                ("erdricks.png", "Erdrick's Armor", 28)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return (memData.ReadU8(0xBE) >> 2) & 0x7;
        }

        public override void Update(int value, bool force = false)
        {
            DefensePower = ItemInfo[value].ExtraValue;
            base.Update(value, force);
            HurtResist = value >= 6 ? 1 / 3 : 0;
            BreathResist = value == 7 ? 1 / 3 : 0;
            StopspellImmunity = value == 7;
        }
    }
}
