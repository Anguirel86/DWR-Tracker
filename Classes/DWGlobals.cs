﻿namespace DWR_Tracker.Classes
{
    static class DWGlobals
    {
        public static DWConfiguration DWConfiguration = new DWConfiguration();
        public static DWFont DWFont = new DWFont();
        public static string DWImagePath = "DWR_Tracker.Images.";
        public static DWProcessReader ProcessReader;

        public static DWHero Hero = new DWHero();

        // This only covers fast leveling
        public static int[] LevelNexts = new int[30]
        {
            0, 5, 17, 35, 82, 165,337, 600, 975, 1500, 2175, 3000, 4125, 5625, 7500,
            9750, 12000, 14250, 16500, 19500, 22500, 25500, 28500, 31500, 34500,
            37500, 40500, 43500, 46500, 49151
        };

        public static string[] InventoryItems = new string[16]
        {
            "Nothing",
            "Torch",
            "Fairy Water",
            "Wings",
            "Dragon's Scale",
            "Fairy Flute",
            "Fighter's Ring",
            "Erdrick's Token",
            "Gwaelin's Love",
            "Cursed Belt",
            "Silver Harp",
            "Death Necklace",
            "Stones of Sunlight",
            "Staff of Rain",
            "Rainbow Drop",
            "Herb"
        };

        public static DWEnemy[] Enemies = new DWEnemy[40]
        {
            new DWEnemy("Blue Slime", new int[2] { 2, 2 }, 5, 3, 1, 2, 0, 0, 1/64f, 1, 0),
            new DWEnemy("Red Slime", new int[2] { 3, 3 }, 7, 3, 2, 4, 0, 0, 1/64f, 1, 0),
            new DWEnemy("Drakee", new int[2] { 4, 5 }, 9, 6, 3, 6, 0, 0, 1/64f, 1, 0),
            new DWEnemy("Ghost", new int[2] { 6, 7 }, 11, 8, 4, 8, 0, 0, 1/16f, 1, 1/16f),
            new DWEnemy("Magician", new int[2] { 9, 12 }, 11, 12, 8, 16, 0, 0, 1/64f, 1, 1/16f),
            new DWEnemy("Magidrakee", new int[2] { 10, 13 }, 14, 14, 12, 20, 0, 0, 1/64f, 1, 1/16f),
            new DWEnemy("Scorpion", new int[2] { 10, 13 }, 18, 16, 16, 25, 0, 0, 1/64f, 1, 1/8f),
            new DWEnemy("Druin", new int[2] { 17, 22 }, 20, 18, 14, 21, 0, 0, 1/32f, 1, 1/8f),
            new DWEnemy("Poltergeist", new int[2] { 18, 23 }, 18, 20, 15, 19, 0, 0, 3/32f, 1, 1/8f),
            new DWEnemy("Droll", new int[2] { 15, 20 }, 24, 24, 18, 30, 0, 0, 1/32f, 1, 3/16f),
            new DWEnemy("Drakeema", new int[2] { 12, 16 }, 22, 26, 20, 25, 0, 1/8f, 3/32f, 1, 3/16f),
            new DWEnemy("Skeleton", new int[2] { 18, 24 }, 28, 22, 25, 42, 0, 0, 1/16f, 1, 3/16f),
            new DWEnemy("Warlock", new int[2] { 21, 28 }, 28, 22, 28, 50, 0, 3/16f, 1/32f, 1, 1/4f),
            new DWEnemy("Metal Scorpion", new int[2] { 14, 18 }, 36, 42, 31, 48, 0, 0, 1/32f, 1, 1/4f),
            new DWEnemy("Wolf", new int[2] { 25, 33 }, 40, 30, 40, 60, 0, 1/16f, 1/32f, 1, 1/4f),
            new DWEnemy("Wraith", new int[2] { 30, 39 }, 44, 34, 42, 62, 0, 7/16f, 1/16f, 1, 5/16f),        
            new DWEnemy("Metal Slime", new int[2] { 3, 3 }, 10, 255, 255, 6, 15/16f, 15/16f, 1/64f, 1, 5/16f),
            new DWEnemy("Specter", new int[2] { 25, 33 }, 40, 38, 47, 75, 0, 3/16f, 1/16f, 1, 5/16f),
            new DWEnemy("Wolflord", new int[2] { 28, 37 }, 50, 36, 52, 80, 0, 1/4f, 1/32f, 1, 3/8f),
            new DWEnemy("Druinlord", new int[2] { 27, 35 }, 47, 40, 58, 95, 0, 15/16f, 1/16f, 1, 3/8f),
            new DWEnemy("Drollmagi", new int[2] { 33, 44 }, 52, 50, 58, 110, 0, 1/8f, 1/64f, 2, 3/8f),
            new DWEnemy("Wyvern", new int[2] { 28, 37 }, 56, 48, 64, 105, 0, 1/4f, 1/32f, 2, 7/16f),
            new DWEnemy("Rogue Scorpion", new int[2] { 30, 40 }, 60, 90, 70, 110, 0, 7/16f, 1/32f, 2, 7/16f),
            new DWEnemy("Wraith Knight", new int[2] { 30, 40 }, 68, 56, 72, 120, 3/16f, 5/16f, 1/16f, 2, 7/16f),
            new DWEnemy("Golem", new int[2] { 115, 153 }, 120, 60, 255, 10, 15/16f, 15/16f, 0, 2, 1/2f),
            new DWEnemy("Goldman", new int[2] { 27, 35 }, 48, 40, 6, 255, 0, 13/16f, 1/64f, 2, 1/2f),
            new DWEnemy("Knight", new int[2] { 36, 47 }, 76, 78, 78, 150, 0, 3/8f, 1/64f, 2, 1/2f),
            new DWEnemy("Magiwyvern", new int[2] { 36, 48 }, 78, 68, 83, 135, 0, 1/8f, 3/32f, 2, 1/2f),
            new DWEnemy("Demon Knight", new int[2] { 29, 38 }, 79, 64, 90, 148, 15/16f, 15/16f, 15/64f, 2, 9/16f),
            new DWEnemy("Werewolf", new int[2] { 53, 70 }, 86, 70, 95, 155, 0, 7/16f, 7/64f, 2, 9/16f),
            new DWEnemy("Green Dragon", new int[2] { 54, 72 }, 88, 74, 135, 160, 1/8f, 7/16f, 1/32f, 3, 5/8f),
            new DWEnemy("Starwyvern", new int[2] { 56, 74 }, 86, 80, 105, 169, 1/16f, 1/2f, 1/32f, 3, 5/8f),
            new DWEnemy("Wizard", new int[2] { 49, 65 }, 80, 70, 120, 185, 15/16f, 15/16f, 1/32f, 3, 5/8f),
            new DWEnemy("Axe Knight", new int[2] { 51, 67 }, 94, 82, 130, 165, 1/16f, 15/16f, 1/32f, 3, 11/16f),
            new DWEnemy("Blue Dragon", new int[2] { 74, 98 }, 98, 84, 180, 150, 7/16f, 15/16f, 1/32f, 3, 11/16f),
            new DWEnemy("Stoneman", new int[2] { 102, 135 }, 100, 40, 155, 148, 7/16f, 1/8f, 1/64f, 4, 11/16f),
            new DWEnemy("Armored Knight", new int[2] { 75, 99 }, 105, 86, 172, 152, 1/16f, 15/16f, 1/32f, 4, 3/4f),  
            new DWEnemy("Red Dragon", new int[2] { 80, 106 }, 120, 90, 255, 143, 15/16f, 15/16f, 1/32f, 4, 3/4f),
            new DWEnemy("Dragonlord 1", new int[2] { 75, 100 }, 90, 75, 0, 0, 15/16f, 15/16f, 0, 4, 15/16f),
            new DWEnemy("Dragonlord 2", new int[2] { 150, 165 }, 140, 200, 0, 0, 15/16f, 15/16f, 0, 4, 15/16f)
        };
    }
}
