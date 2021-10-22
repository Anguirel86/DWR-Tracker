using DWR_Tracker.Classes.Items;
using DWR_Tracker.Classes.Stats;
using System;
using System.Collections.Generic;

namespace DWR_Tracker.Classes
{
  public class DWHero
  {
    // stats
    public string Name;
    public DWStat Level = new DWStat("lv", 0xC7);
    public DWStat Strength = new DWStat("st", 0xC8);
    public DWStat Agility = new DWStat("ag", 0xC9);
    public DWStat MaxHP = new DWStat("hp", 0xCA);
    public DWStat CurrentHP = new DWStat("chp", 0xC5);
    public DWStat MaxMP = new DWStat("mp", 0xCB);
    public DWStat CurrentMP = new DWStat("cmp", 0xC6);
    public DWStat AttackPower = new DWStat("ap", 0xCC);
    public DWStat DefensePower = new DWStat("dp", 0xCD);
    public DWStat Gold = new DWStat("g", 0xBC, 16);
    public DWStat XP = new DWStat("e", 0xBA, 16);
    // TODO: grab this from RAM once I figure out how to find the pointer 
    // path to it. I know the table starts at 0xB35B, but need the pointer.
    public DWStatNextLevel NextLevel = new DWStatNextLevel("e+", 0);
    public DWStat[] DisplayStats;

    // battle gear
    public DWSword Sword = new DWSword();
    public DWArmor Armor = new DWArmor();
    public DWShield Shield = new DWShield();
    public DWFightersRing FightersRing = new DWFightersRing();
    public DWDragonsScale DragonsScale = new DWDragonsScale();
    public DWDeathNecklace DeathNecklace = new DWDeathNecklace();

    // quest items
    public DWHarpOrStaff HarpOrStaff = new DWHarpOrStaff();
    public DWStonesOfSunlight StonesOfSunlight = new DWStonesOfSunlight();
    public DWErdricksToken ErdricksToken = new DWErdricksToken();
    public DWRainbowDrop RainbowDrop = new DWRainbowDrop();
    public DWBridge Bridge = new DWBridge();
    public DWBallOfLight BallOfLight = new DWBallOfLight();

    // other quest items
    public DWMagicKey MagicKey = new DWMagicKey();
    public DWFairyFlute FairyFlute = new DWFairyFlute();
    public DWGwaelinsLove GwaelinsLove = new DWGwaelinsLove();

    // inventory items
    public DWTorch Torch = new DWTorch();
    public DWFairyWater FairyWater = new DWFairyWater();
    public DWWings Wings = new DWWings();
    public DWCursedBelt CursedBelt = new DWCursedBelt();
    public DWHerb Herb = new DWHerb();

    // item collections
    public DWItem[] BattleGear;
    public DWItem[] QuestItems;
    public DWItem[] OtherItems;
    public DWItem[] Inventory;
    public DWItem[] AllItems;

    // spells
    public DWSpell[] Spells = new DWSpell[10]
    {
            new DWSpell("heal", 0xCE, 0x1),
            new DWSpell("hurt", 0xCE, 0x2),
            new DWSpell("sleep", 0xCE, 0x4),
            new DWSpell("radiant", 0xCE, 0x8),
            new DWSpell("stopspell", 0xCE, 0x10),
            new DWSpell("outside", 0xCE, 0x20),
            new DWSpell("return", 0xCE, 0x40),
            new DWSpell("repel", 0xCE, 0x80),
            new DWSpell("healmore", 0xCF, 0x1),
            new DWSpell("hurtmore", 0xCF, 0x2)
    };

    public event EventHandler NameChanged;

    public DWHero()
    {
      // arrange stats
      DisplayStats = new DWStat[10]
      {
                Level, Strength, Agility, MaxHP, MaxMP, AttackPower,
                DefensePower, Gold, XP, NextLevel
      };
      NextLevel.LevelStat = Level;

      // arrange battle gear
      BattleGear = new DWItem[6]
      {
                Sword, Armor, Shield, FightersRing, DragonsScale, DeathNecklace
      };

      // arrange quest items
      QuestItems = new DWItem[6]
      {
                HarpOrStaff, StonesOfSunlight, ErdricksToken, RainbowDrop,
                Bridge, BallOfLight
      };

      // arrange other quest items
      OtherItems = new DWItem[3]
      {
                MagicKey, FairyFlute, GwaelinsLove
      };

      // arrange inventory items
      Inventory = new DWItem[5]
      {
                Torch, FairyWater, Wings, CursedBelt, Herb
      };

      // create array to hold all items
      AllItems = new DWItem[BattleGear.Length + QuestItems.Length + OtherItems.Length + Inventory.Length];
      BattleGear.CopyTo(AllItems, 0);
      QuestItems.CopyTo(AllItems, BattleGear.Length);
      OtherItems.CopyTo(AllItems, BattleGear.Length + QuestItems.Length);
      Inventory.CopyTo(AllItems, BattleGear.Length + QuestItems.Length + OtherItems.Length);
    }

    /// <summary>
    /// Read the hero name from memory.  Update the GUI if needed.
    /// </summary>
    /// <param name="memData">NES memory block to read</param>
    public void UpdateHeroName(MemoryBlock memData)
    {
      // Read the name data from memory
      byte[] nameCharacters = new byte[8];
      for (int i = 0; i < 8; i++)
      {
        nameCharacters[i] = memData.ReadU8(0x6056 + i);
      }

      string name = "";
      bool foundStart = false;
      for (int i = 7; i >= 0; i--)
      {
        if (nameCharacters[i] == 0x60 && !foundStart) { continue; }
        foundStart = true;
        name += DWGlobals.Characters[nameCharacters[i]];
      }
      name = name.Trim();

      if (name != Name)
      {
        Name = name;
        OnNameChanged();
      }
    }

    /// <summary>
    /// Update items from general inventory. Items are stored in four bytes starting
    /// at 0xC1 where each nibble represents an item.
    /// </summary>
    /// <param name="memData">NES memory block to read</param>
    /// <param name="force">If true, force an update even if no change is detected</param>
    private void UpdateInventoryItems(MemoryBlock memData, bool force = false)
    {
      // get all items that appear in inventory (quest & other)
      Dictionary<string, int> items = new Dictionary<string, int>();
      byte[] data = new byte[] { 
        memData.ReadU8(0xC1), memData.ReadU8(0xC2), 
        memData.ReadU8(0xC3), memData.ReadU8(0xC4) };
      int itemData = BitConverter.ToInt32(data, 0);
      for (int i = 0; i < 8; i++)
      {
        int itemValue = (itemData >> (i * 4) & 0xF);
        string itemName = DWGlobals.InventoryItems[itemValue];
        if (itemName == "Nothing") { continue; }

        if (items.ContainsKey(itemName))
        {
          items[itemName]++;
        }
        else
        {
          items.Add(itemName, 1);
        }
      }

      // update all quest items
      foreach (DWItem item in QuestItems)
      {
        if (item is DWHarpOrStaff)
        {
          int value = 0;
          if (items.ContainsKey("Staff of Rain"))
          {
            value = 2;
          }
          else if (items.ContainsKey("Silver Harp"))
          {
            value = 1;
          }
          item.Update(value, 1, force);
        }
        else if (item is DWBridge || item is DWBallOfLight)
        {
          item.Update(item.ReadValue(memData), 1, force);
        }
        else if (items.ContainsKey(item.Name))
        {
          if (item.forceOwnRead)
          {
            item.Update(item.ReadValue(memData), 1, force);
          }
          else
          {
            item.Update(1, items[item.Name], force);
          }
        }
        else
        {
          item.Update(0, 0, force);
        }
      }

      // udpate other quest items
      foreach (DWItem item in OtherItems)
      {
        if (item is DWMagicKey) { continue; }

        if (items.ContainsKey(item.Name))
        {
          item.Update(1, items[item.Name], force);
        }
        else
        {
          item.Update(0, 0, force);
        }
      }
    }

    /// <summary>
    /// This method updates the items on the tracker from NES memory.
    /// </summary>
    /// <param name="memData">NES memory block to read</param>
    /// <param name="force">If true, force an update even if no change is detected</param>
    public void Update(MemoryBlock memData, bool force = false)
    {
      //HandleHeroName(memData); // This is actually later on in RAM (0x6056-0x6063

      // Update stats
      foreach (DWStat stat in DisplayStats)
      {
        stat.Update(memData, force);
      }

      // update spells
      foreach (DWSpell spell in Spells)
      {
        spell.Update(memData, force);
      }

      // update battle gear
      foreach (DWItem item in BattleGear)
      {
        item.Update(memData, force);
      }

      UpdateInventoryItems(memData, force);

      // Special handling for key count
      int keys = MagicKey.ReadValue(memData);
      if (keys != MagicKey.Count || force)
      {
        MagicKey.Update(keys > 0 ? 1 : 0, keys, force);
      }
    }

    protected virtual void OnNameChanged()
    {
      EventHandler handler = NameChanged;
      handler?.Invoke(this, new EventArgs());
    }
  }
}
