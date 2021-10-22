using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWSword : DWItem
    {
        public int AttackPower = 0;

        public DWSword()
        {
            string basePath = DWGlobals.DWImagePath + "Swords.";

            Name = "Nothing";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;
            
            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[8]
            {
                ("weapon-no.png", "Nothing", 0),
                ("bamboo.png", "Bamboo Pole", 2),
                ("club.png", "Club", 4),
                ("copper.png", "Copper Sword", 10),
                ("axe.png", "Hand Axe", 15),
                ("broad.png", "Broad Sword", 20),
                ("flame.png", "Flame Sword", 28),
                ("erdricks.png", "Erdrick's Sword", 40)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return (memData.ReadU8(0xBE) >> 5) & 0x7;
        }

        public override void Update(int value, bool force = false)
        {
            AttackPower = ItemInfo[value].ExtraValue;
            base.Update(value, force);
        }
    }
}
