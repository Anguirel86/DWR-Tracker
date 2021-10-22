using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWErdricksToken : DWItem
    {
        public DWErdricksToken()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Erdrick's Token";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = true;
            IsFirstHalfQuestItem = true;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("token-grey.png", "Erdrick's Token", 0),
                ("token.png", "Erdrick's Token", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
