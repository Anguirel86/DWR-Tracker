using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWDragonsScale : DWItem
    {
        public int DefensePower = 0;

        public DWDragonsScale()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Dragon's Scale";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("dragon_scale-grey.png", "Dragon's Scale", 0),
                ("dragon_scale.png", "Dragon's Scale", 2)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return (memData.ReadU8(0xCF) & 0x10) > 0 ? 1 : 0;
        }

        public override void Update(int value, bool force = false)
        {
            DefensePower = ItemInfo[value].ExtraValue;
            base.Update(value, force);
        }
    }
}
