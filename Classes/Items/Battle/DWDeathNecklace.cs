﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWR_Tracker.Classes.Items
{
    class DWDeathNecklace : DWItem
    {
        public DWDeathNecklace()
        {
            string basePath = DWGlobals.DWImagePath + "Items.";

            Name = "Death Necklace";
            ImagePath = "";
            IsBattleGear = true;
            IsRequiredItem = false;
            allowsMultiple = false;
            Count = 1;

            ItemInfo = new (string ImagePath, string Name)[2]
            {
                ("reckless_necklace_th-grey.png", "Death Necklace"),
                ("reckless_necklace_th.png", "Death Necklace")
            }.Select(s => (basePath + s.ImagePath, s.Name)).ToArray();
        }

        public override int ReadValue()
        {
            return (DWGlobals.ProcessReader.ReadByte(0xCF) & 0x80) > 0 ? 1 : 0;
        }
    }
}
