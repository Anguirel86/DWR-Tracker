using System.Linq;

namespace DWR_Tracker.Classes.Items
{
    public class DWTorch : DWItem
    {
        public DWTorch()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Torch";
            ImagePath = "";
            IsBattleGear = false;
            IsRequiredItem = false;
            allowsMultiple = true;
            ShowCount = true;
            Count = 0;

            ItemInfo = new (string ImagePath, string Name, int ExtraValue)[2]
            {
                ("torch-grey.png", "Torch", 0),
                ("torch.png", "Torch", 0)
            }.Select(s => (basePath + s.ImagePath, s.Name, s.ExtraValue)).ToArray();
        }

        public override int ReadValue(MemoryBlock memData)
        {
            return 0;
        }
    }
}
