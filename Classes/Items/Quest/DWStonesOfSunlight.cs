using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWStonesOfSunlight : DWItem
    {
        public DWStonesOfSunlight()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Stones of Sunlight";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = true;
            IsFirstHalfQuestItem = true;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("stones_of_sunlight-grey.png", "Stones of Sunlight", 0),
                ("stones_of_sunlight.png", "Stones of Sunlight", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
