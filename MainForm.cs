using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using DWR_Tracker.Classes;
using DWR_Tracker.Classes.Maps;
using DWR_Tracker.Controls;
using Newtonsoft.Json;

namespace DWR_Tracker
{
  public partial class MainForm : Form, IMemoryReadListener, IConnectionStatusListener
  {
    private DWConfiguration Config = DWGlobals.DWConfiguration;
    private DWHero Hero = DWGlobals.Hero;
    private DWOverworldMap Overworld = new DWOverworldMap();
    private AutotrackerConnection autoTracker;

    private bool inBattle = false;
    private int currentMapindex = 0;
    private int heightChrome = 988;
    private int heightChromeless = 942;

    // Store the most recent memory blocks for autotracker processing.
    MemoryBlock heroMemoryBlock;


    public MainForm()
    {
      InitializeComponent();
      autoTracker = new AutotrackerConnection(this);

      // try to find a suitable emulator automatically
      if (!EmulatorConnectionWorker.IsBusy)
      {
        EmulatorConnectionWorker.RunWorkerAsync();
      }

      // TODO: initialize map

    }

    private delegate void UpdateEnemyDelegate(DWEnemy enemy);
    private void UpdateEnemy(DWEnemy enemy)
    {
      if (EnemyPanel.InvokeRequired)
      {
        var d = new UpdateEnemyDelegate(UpdateEnemy);
        EnemyPanel.Invoke(d, new object[] { enemy });
      }
      else
      {
        // update enemy image 
        EnemyPanelPictureBox.Image = enemy.GetImage();

        // clear enemy stats table
        while (EnemyStatsTable.Controls.Count > 0)
        {
          EnemyStatsTable.Controls[0].Dispose();
        }
        while (EnemyInfoTable.Controls.Count > 0)
        {
          EnemyInfoTable.Controls[0].Dispose();
        }

        // add enemy stats to table
        string[,] stats = enemy.GetBattleInfo(Hero);
        for (int i = 0; i < stats.GetLength(0); i++)
        {
          string name = stats[i, 0];
          string value = stats[i, 1];

          if (i == 0)
          {
            CombatPanel.Title = name;
          }
          else
          {
            TableLayoutPanel table = i < 6 ? EnemyInfoTable : EnemyStatsTable;
            bool isHeader = value == "";
            int row = i < 6 ? i - 1 : i - 6;

            DWLabel nameLabel = new DWLabel { TextAlign = ContentAlignment.MiddleLeft };
            table.Controls.Add(nameLabel, 0, row);
            nameLabel.FitText(name, true, isHeader ? FontStyle.Bold : FontStyle.Regular);

            DWLabel valueLabel = new DWLabel { TextAlign = ContentAlignment.MiddleRight };
            table.Controls.Add(valueLabel, 1, row);
            valueLabel.FitText(value, true);
          }
        }
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      // update UI based on config
      streamerModeToolStripMenuItem.Checked = Config.StreamerMode;
      autoTrackingToolStripMenuItem.Checked = Config.AutoTrackingEnabled;
      MainFormLayoutUpdate();

      // hide enemy panel initially
      EnemyPanel.Visible = false;

      // draw rounded background box for enemy image
      PictureBox pb = EnemyPanelPictureBox;
      Rectangle r = new Rectangle(0, 0, pb.Width, pb.Height);
      GraphicsPath gp = new GraphicsPath();
      int d = 20;
      gp.AddArc(r.X, r.Y, d, d, 180, 90);
      gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
      gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
      gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
      pb.Region = new Region(gp);

      // set the hero's name
      Hero.NameChanged += (sender, e) =>
      {
        this.Invoke(() => StatPanel.Title = Hero.Name);
      };

      //Set initial UI for hero stats
      for (int i = 0; i < Hero.DisplayStats.Length; i++)
      {
        DWStat stat = Hero.DisplayStats[i];
        DWLabel nameLabel = new DWLabel { Text = stat.Name.ToUpper() };
        DWLabel valueLabel = new DWLabel
        {
          Text = stat.Value.ToString(),
          TextAlign = ContentAlignment.MiddleRight
        };
        StatTableLayout.Controls.Add(nameLabel, 0, i);
        StatTableLayout.Controls.Add(valueLabel, 1, i);
        stat.ValueChanged += (sender, e) =>
        {
          this.Invoke(() => valueLabel.Text = stat.Value.ToString());
        };
      }

      // create spell labels
      for (int i = 0; i < Hero.Spells.Length; i++)
      {
        DWSpell spell = Hero.Spells[i];
        DWSpellLabel label = new DWSpellLabel(spell);

        // position spell in panel
        SpellPanel.Controls.Add(label);
        label.Top = i * 26 + 26;
        label.Left = 15;
        label.Width = SpellPanel.Width;

        // update color on spell value change
        spell.ValueChanged += (sender, e) =>
        {
          this.Invoke(() => label.ForeColor = spell.HasSpell ?
                      Color.FromArgb(255, 255, 255) :
                      Color.FromArgb(60, 60, 60));
        };
      }

      // set initial UI for all items
      foreach (DWItem item in Hero.QuestItems)
      {
        DWItemBox itemBox = new DWItemBox(item);
        // Add a value changed listener to the rainbow drop.  When the rainbow drop is found
        // then hide the harp/staff, stone, and token
        if (item.IsFirstHalfQuestItem)
        {
          Hero.RainbowDrop.ValueChanged += (sender, e) =>
          {
            this.Invoke(() => itemBox.Visible = Hero.RainbowDrop.Value == 0);
          };
        }
        RequiredItemFlowPanel.Controls.Add(itemBox);
      }

      foreach (DWItem item in Hero.BattleGear)
      {
        BattleItemFlowPanel.Controls.Add(new DWItemBox(item));
      }

      foreach (DWItem item in Hero.OtherItems)
      {
        OptionalItemFlowPanel.Controls.Add(new DWItemBox(item));
      }

      // Force an initial update of the GUI with dummy autotracking data.
      // This forces the initial state of the item icons to be displayed correctly.
      MemoryBlock dummy = new MemoryBlock(0x3A, 171, ReadType.HERO_AND_MAP_DATA);
      dummy.SetMemoryBlockData(new byte[171]);
      Hero.Update(dummy, true);

      autoTracker.StartAutotracker();

      // game state update timer
      System.Timers.Timer timer = new System.Timers.Timer(500);
      timer.Elapsed += RequestAutotrackingData;
      timer.Start();
    }

    private void MainFormLayoutUpdate()
    {
      DWMenuStrip.Visible = DWStatusStrip.Visible = !Config.StreamerMode;
      DWContentPanel.Top = 4 + (DWMenuStrip.Visible ? DWMenuStrip.Height : 0);

      if (Config.StreamerMode)
      {
        this.Height = heightChromeless;
        this.FormBorderStyle = FormBorderStyle.None;
      }
      else
      {
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Height = heightChrome;
      }
    }

    private void RequestAutotrackingData(Object source, ElapsedEventArgs e)
    {
      // Order is important here.  Map index and location data are part of the first memory
      // read, but it isn't used until the battle status is checked.
      //
      // These will be queued up and run sequentially.  This class will be notified when
      // the emulator returns the requested memory data.
      autoTracker.ReadMemory(0x3A, 171, ReadType.HERO_AND_MAP_DATA, this);
      autoTracker.ReadMemory(0x6003, 1, ReadType.BATTLE_STATUS, this);
    }

    public void HandleMemoryRead(MemoryBlock memData)
    {
      UpdateGameState(memData);
    }

    public void ConnectionStatusChanged(bool connected)
    {
      // update emulator connection status in the status bar
      EmulatorStatusLabel.Text = connected ? "Connection established" : "Connection lost";
    }

    private void Stat_ValueChanged(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void HandleBattleAndMapCheck(MemoryBlock memData)
    {
      int statusByte = memData.ReadU8(0x6003);
      int mapIndex = heroMemoryBlock.ReadU8(0x45) - 1;
      bool inBattleCheck = statusByte == 3;


      if (!inBattle && inBattleCheck)
      {
        // We just transitioned into a battle scene.
        inBattle = true;
        int enemyIndex = heroMemoryBlock.ReadU8(0xE0);
        int location = heroMemoryBlock.ReadU8(0x45);
        if (location != 0)
        {
          UpdateEnemy(DWGlobals.Enemies[enemyIndex]);
        }
        this.Invoke(() =>
        {
          EnemyPanel.Visible = true;
          MapPanel.Visible = false;
        });
      }
      else if ((inBattle && !inBattleCheck) || currentMapindex != mapIndex)
      {
        inBattle = false;
        currentMapindex = mapIndex;

        if (mapIndex >= 0)
        {
          DWMap map = DWGlobals.Maps[mapIndex];
          this.Invoke(() =>
          {
            EnemyPanel.Visible = false;
            CombatPanel.Title = map.Name;
            MapPanel.Visible = true;
            int x = heroMemoryBlock.ReadU8(0x3A);
            int y = heroMemoryBlock.ReadU8(0x3B);
            MapPictureBox.Image = mapIndex == 0 ? Overworld.GetImage(x, y) : map.GetImage();
          });
        }
      }

      // are we discovering on the overworld?
      if (mapIndex == 0)
      {
        int x = heroMemoryBlock.ReadU8(0x3A);
        int y = heroMemoryBlock.ReadU8(0x3B);
        Overworld.Discover(x, y);
        Image img = MapPictureBox.Image;
        MapPictureBox.Image = Overworld.GetImage(x, y);
        if (img != null) { img.Dispose(); }
        GC.Collect();
        GC.WaitForPendingFinalizers();
      }
    }

    /// <summary>
    /// Update the state of the game with the memory data returned by the autotracker.
    /// </summary>
    /// <param name="memData">NES memory block containing game data</param>
    private void UpdateGameState(MemoryBlock memData)
    {
      if (Config.AutoTrackingEnabled)
      {
        switch (memData.GetReadType())
        {
          case ReadType.HERO_AND_MAP_DATA:
            heroMemoryBlock = memData;
            Hero.Update(memData);
            break;
          case ReadType.BATTLE_STATUS:
            HandleBattleAndMapCheck(memData);
            break;
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void ReadMonsterAbilityData(byte[] romData)
    {
      for (int i =0; i < DWGlobals.Enemies.Length; i++)
      {
        int pointer = 0x5e5b + (16 * i);
        DWEnemy enemy = DWGlobals.Enemies[i];
        // It looks like it's 4 bits per ability, Top 2 are the move id, bottom 2 are the chance
        enemy.Skill2 = enemy.GetSkill2((romData[pointer + 3] >> 2) & 0x3);
        enemy.Skill2Chance = (romData[pointer + 3]) & 0x3;
        enemy.Skill1 = enemy.GetSkill1((romData[pointer + 3] >> 6) & 0x3);
        enemy.Skill1Chance = (romData[pointer + 3] >> 4) & 0x3;
      }
    }

    private void streamerModeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem mi = (ToolStripMenuItem)sender;
      Config.StreamerMode = mi.Checked = !mi.Checked;
      MainFormLayoutUpdate();
    }

    private void readRomToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem mi = (ToolStripMenuItem)sender;
      
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          String filePath = openFileDialog.FileName;
          byte[] fileData = File.ReadAllBytes(filePath);
          Overworld.DecodeMap(fileData);
          ReadMonsterAbilityData(fileData);
        }
      }

      MainFormLayoutUpdate();
    }

    private void autoTrackingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem mi = (ToolStripMenuItem)sender;
      Config.AutoTrackingEnabled = mi.Checked = !mi.Checked;
    }
  }

  public class ItemPictureBox
  {
    public DWItem Item;
    public DWItemBox ItemBox;

    public ItemPictureBox(DWItem item, DWItemBox itemBox)
    {
      Item = item;
      ItemBox = itemBox;
    }
  }
}
