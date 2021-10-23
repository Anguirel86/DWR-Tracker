namespace DWR_Tracker
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.DWMenuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.readRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.streamerModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.autoTrackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DWStatusStrip = new System.Windows.Forms.StatusStrip();
      this.EmulatorStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.EmulatorConnectionWorker = new System.ComponentModel.BackgroundWorker();
      this.DWContentPanel = new System.Windows.Forms.Panel();
      this.CombatPanel = new DWR_Tracker.Controls.DWPanel();
      this.CoordsPanel = new DWR_Tracker.Controls.DWPanel();
      this.CoordsEWCharTextBox = new System.Windows.Forms.TextBox();
      this.CoordsNSCharTextBox = new System.Windows.Forms.TextBox();
      this.CoordsEWTextBox = new System.Windows.Forms.TextBox();
      this.CoordsNSTextBox = new System.Windows.Forms.TextBox();
      this.EnemyPanel = new System.Windows.Forms.Panel();
      this.EnemyInfoTable = new System.Windows.Forms.TableLayoutPanel();
      this.EnemyPanelPictureBox = new System.Windows.Forms.PictureBox();
      this.EnemyStatsTable = new System.Windows.Forms.TableLayoutPanel();
      this.MapPanel = new System.Windows.Forms.Panel();
      this.MapPictureBox = new System.Windows.Forms.PictureBox();
      this.BattlePanel = new DWR_Tracker.Controls.DWPanel();
      this.BattleItemFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.OptionalItemPanel = new DWR_Tracker.Controls.DWPanel();
      this.OptionalItemFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.QuestPanel = new DWR_Tracker.Controls.DWPanel();
      this.RequiredItemFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.StatPanel = new DWR_Tracker.Controls.DWPanel();
      this.StatTableLayout = new System.Windows.Forms.TableLayoutPanel();
      this.SpellPanel = new DWR_Tracker.Controls.DWPanel();
      this.DWMenuStrip.SuspendLayout();
      this.DWStatusStrip.SuspendLayout();
      this.DWContentPanel.SuspendLayout();
      this.CombatPanel.SuspendLayout();
      this.CoordsPanel.SuspendLayout();
      this.EnemyPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.EnemyPanelPictureBox)).BeginInit();
      this.MapPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
      this.BattlePanel.SuspendLayout();
      this.OptionalItemPanel.SuspendLayout();
      this.QuestPanel.SuspendLayout();
      this.StatPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // DWMenuStrip
      // 
      this.DWMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem});
      this.DWMenuStrip.Location = new System.Drawing.Point(0, 0);
      this.DWMenuStrip.Name = "DWMenuStrip";
      this.DWMenuStrip.Size = new System.Drawing.Size(505, 24);
      this.DWMenuStrip.TabIndex = 11;
      this.DWMenuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.readRomToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // connectToolStripMenuItem
      // 
      this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
      this.connectToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
      this.connectToolStripMenuItem.Text = "Connect...";
      this.readRomToolStripMenuItem.Name = "readRomToolStripMenuItem";
      this.readRomToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
      this.readRomToolStripMenuItem.Text = "Read Rom...";
      this.readRomToolStripMenuItem.Click += new System.EventHandler(this.readRomToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamerModeToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // streamerModeToolStripMenuItem
      // 
      this.streamerModeToolStripMenuItem.Name = "streamerModeToolStripMenuItem";
      this.streamerModeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
      this.streamerModeToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
      this.streamerModeToolStripMenuItem.Text = "Streamer Mode";
      this.streamerModeToolStripMenuItem.Click += new System.EventHandler(this.streamerModeToolStripMenuItem_Click);
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoTrackingToolStripMenuItem});
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.optionsToolStripMenuItem.Text = "Options";
      // 
      // autoTrackingToolStripMenuItem
      // 
      this.autoTrackingToolStripMenuItem.Checked = true;
      this.autoTrackingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.autoTrackingToolStripMenuItem.Name = "autoTrackingToolStripMenuItem";
      this.autoTrackingToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
      this.autoTrackingToolStripMenuItem.Text = "Auto Tracking";
      this.autoTrackingToolStripMenuItem.Click += new System.EventHandler(this.autoTrackingToolStripMenuItem_Click);
      // 
      // DWStatusStrip
      // 
      this.DWStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EmulatorStatusLabel});
      this.DWStatusStrip.Location = new System.Drawing.Point(0, 927);
      this.DWStatusStrip.Name = "DWStatusStrip";
      this.DWStatusStrip.Size = new System.Drawing.Size(505, 22);
      this.DWStatusStrip.SizingGrip = false;
      this.DWStatusStrip.TabIndex = 12;
      this.DWStatusStrip.Text = "statusStrip1";
      // 
      // EmulatorStatusLabel
      // 
      this.EmulatorStatusLabel.BackColor = System.Drawing.Color.Transparent;
      this.EmulatorStatusLabel.Name = "EmulatorStatusLabel";
      this.EmulatorStatusLabel.Size = new System.Drawing.Size(86, 17);
      this.EmulatorStatusLabel.Text = "Not connected";
      // 
      // EmulatorConnectionWorker
      // 
      //this.EmulatorConnectionWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.EmulatorConnectionWorker_DoWork);
      //this.EmulatorConnectionWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.EmulatorConnectionWorker_RunWorkerCompleted);
      // 
      // DWContentPanel
      // 
      this.DWContentPanel.Controls.Add(this.CombatPanel);
      this.DWContentPanel.Controls.Add(this.BattlePanel);
      this.DWContentPanel.Controls.Add(this.OptionalItemPanel);
      this.DWContentPanel.Controls.Add(this.QuestPanel);
      this.DWContentPanel.Controls.Add(this.StatPanel);
      this.DWContentPanel.Controls.Add(this.SpellPanel);
      this.DWContentPanel.Location = new System.Drawing.Point(0, 27);
      this.DWContentPanel.Name = "DWContentPanel";
      this.DWContentPanel.Size = new System.Drawing.Size(504, 897);
      this.DWContentPanel.TabIndex = 13;
      // 
      // CombatPanel
      // 
      this.CombatPanel.BackColor = System.Drawing.Color.Black;
      this.CombatPanel.Controls.Add(this.CoordsPanel);
      this.CombatPanel.Controls.Add(this.EnemyPanel);
      this.CombatPanel.Controls.Add(this.MapPanel);
      this.CombatPanel.Location = new System.Drawing.Point(3, 386);
      this.CombatPanel.MinimumSize = new System.Drawing.Size(112, 56);
      this.CombatPanel.Name = "CombatPanel";
      this.CombatPanel.Size = new System.Drawing.Size(498, 510);
      this.CombatPanel.TabIndex = 14;
      this.CombatPanel.Title = "INFO";
      // 
      // CoordsPanel
      // 
      this.CoordsPanel.BackColor = System.Drawing.Color.Transparent;
      this.CoordsPanel.Controls.Add(this.CoordsEWCharTextBox);
      this.CoordsPanel.Controls.Add(this.CoordsNSCharTextBox);
      this.CoordsPanel.Controls.Add(this.CoordsEWTextBox);
      this.CoordsPanel.Controls.Add(this.CoordsNSTextBox);
      this.CoordsPanel.Location = new System.Drawing.Point(349, 434);
      this.CoordsPanel.MinimumSize = new System.Drawing.Size(40, 56);
      this.CoordsPanel.Name = "CoordsPanel";
      this.CoordsPanel.Size = new System.Drawing.Size(149, 76);
      this.CoordsPanel.TabIndex = 4;
      this.CoordsPanel.Title = "";
      // 
      // CoordsEWCharTextBox
      // 
      this.CoordsEWCharTextBox.BackColor = System.Drawing.Color.Black;
      this.CoordsEWCharTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.CoordsEWCharTextBox.Font = new System.Drawing.Font("Dragon Warrior (NES)", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CoordsEWCharTextBox.ForeColor = System.Drawing.Color.White;
      this.CoordsEWCharTextBox.Location = new System.Drawing.Point(22, 40);
      this.CoordsEWCharTextBox.Name = "CoordsEWCharTextBox";
      this.CoordsEWCharTextBox.Size = new System.Drawing.Size(53, 19);
      this.CoordsEWCharTextBox.TabIndex = 5;
      this.CoordsEWCharTextBox.Text = "E/W";
      // 
      // CoordsNSCharTextBox
      // 
      this.CoordsNSCharTextBox.BackColor = System.Drawing.Color.Black;
      this.CoordsNSCharTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.CoordsNSCharTextBox.Font = new System.Drawing.Font("Dragon Warrior (NES)", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CoordsNSCharTextBox.ForeColor = System.Drawing.Color.White;
      this.CoordsNSCharTextBox.Location = new System.Drawing.Point(22, 17);
      this.CoordsNSCharTextBox.Name = "CoordsNSCharTextBox";
      this.CoordsNSCharTextBox.Size = new System.Drawing.Size(53, 19);
      this.CoordsNSCharTextBox.TabIndex = 4;
      this.CoordsNSCharTextBox.Text = "N/S";
      // 
      // CoordsEWTextBox
      // 
      this.CoordsEWTextBox.BackColor = System.Drawing.Color.Black;
      this.CoordsEWTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.CoordsEWTextBox.Font = new System.Drawing.Font("Dragon Warrior (NES)", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CoordsEWTextBox.ForeColor = System.Drawing.Color.White;
      this.CoordsEWTextBox.Location = new System.Drawing.Point(81, 40);
      this.CoordsEWTextBox.Name = "CoordsEWTextBox";
      this.CoordsEWTextBox.Size = new System.Drawing.Size(53, 19);
      this.CoordsEWTextBox.TabIndex = 3;
      this.CoordsEWTextBox.Text = "??";
      // 
      // CoordsNSTextBox
      // 
      this.CoordsNSTextBox.BackColor = System.Drawing.Color.Black;
      this.CoordsNSTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.CoordsNSTextBox.Font = new System.Drawing.Font("Dragon Warrior (NES)", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CoordsNSTextBox.ForeColor = System.Drawing.Color.White;
      this.CoordsNSTextBox.Location = new System.Drawing.Point(81, 17);
      this.CoordsNSTextBox.Name = "CoordsNSTextBox";
      this.CoordsNSTextBox.Size = new System.Drawing.Size(53, 19);
      this.CoordsNSTextBox.TabIndex = 2;
      this.CoordsNSTextBox.Text = "??";
      // 
      // EnemyPanel
      // 
      this.EnemyPanel.Controls.Add(this.EnemyInfoTable);
      this.EnemyPanel.Controls.Add(this.EnemyPanelPictureBox);
      this.EnemyPanel.Controls.Add(this.EnemyStatsTable);
      this.EnemyPanel.Location = new System.Drawing.Point(9, 15);
      this.EnemyPanel.Name = "EnemyPanel";
      this.EnemyPanel.Size = new System.Drawing.Size(480, 483);
      this.EnemyPanel.TabIndex = 1;
      // 
      // EnemyInfoTable
      // 
      this.EnemyInfoTable.ColumnCount = 2;
      this.EnemyInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
      this.EnemyInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
      this.EnemyInfoTable.Location = new System.Drawing.Point(26, 227);
      this.EnemyInfoTable.Name = "EnemyInfoTable";
      this.EnemyInfoTable.RowCount = 9;
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.EnemyInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.EnemyInfoTable.Size = new System.Drawing.Size(192, 143);
      this.EnemyInfoTable.TabIndex = 3;
      // 
      // EnemyPanelPictureBox
      // 
      this.EnemyPanelPictureBox.BackColor = System.Drawing.Color.LightSteelBlue;
      this.EnemyPanelPictureBox.Location = new System.Drawing.Point(26, 11);
      this.EnemyPanelPictureBox.Name = "EnemyPanelPictureBox";
      this.EnemyPanelPictureBox.Size = new System.Drawing.Size(192, 192);
      this.EnemyPanelPictureBox.TabIndex = 1;
      this.EnemyPanelPictureBox.TabStop = false;
      // 
      // EnemyStatsTable
      // 
      this.EnemyStatsTable.ColumnCount = 2;
      this.EnemyStatsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.42105F));
      this.EnemyStatsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.57895F));
      this.EnemyStatsTable.Location = new System.Drawing.Point(236, 11);
      this.EnemyStatsTable.Name = "EnemyStatsTable";
      this.EnemyStatsTable.RowCount = 14;
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.EnemyStatsTable.Size = new System.Drawing.Size(231, 384);
      this.EnemyStatsTable.TabIndex = 0;
      // 
      // MapPanel
      // 
      this.MapPanel.AutoScroll = true;
      this.MapPanel.Controls.Add(this.MapPictureBox);
      this.MapPanel.Location = new System.Drawing.Point(9, 18);
      this.MapPanel.Name = "MapPanel";
      this.MapPanel.Size = new System.Drawing.Size(480, 480);
      this.MapPanel.TabIndex = 2;
      // 
      // MapPictureBox
      // 
      this.MapPictureBox.Location = new System.Drawing.Point(-1, 0);
      this.MapPictureBox.Name = "MapPictureBox";
      this.MapPictureBox.Size = new System.Drawing.Size(480, 480);
      this.MapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.MapPictureBox.TabIndex = 0;
      this.MapPictureBox.TabStop = false;
      // 
      // BattlePanel
      // 
      this.BattlePanel.BackColor = System.Drawing.Color.Black;
      this.BattlePanel.Controls.Add(this.BattleItemFlowPanel);
      this.BattlePanel.Location = new System.Drawing.Point(399, 3);
      this.BattlePanel.MinimumSize = new System.Drawing.Size(40, 56);
      this.BattlePanel.Name = "BattlePanel";
      this.BattlePanel.Size = new System.Drawing.Size(102, 379);
      this.BattlePanel.TabIndex = 9;
      this.BattlePanel.Title = "";
      // 
      // BattleItemFlowPanel
      // 
      this.BattleItemFlowPanel.BackColor = System.Drawing.Color.Transparent;
      this.BattleItemFlowPanel.Location = new System.Drawing.Point(24, 15);
      this.BattleItemFlowPanel.Margin = new System.Windows.Forms.Padding(0);
      this.BattleItemFlowPanel.Name = "BattleItemFlowPanel";
      this.BattleItemFlowPanel.Size = new System.Drawing.Size(69, 350);
      this.BattleItemFlowPanel.TabIndex = 3;
      // 
      // OptionalItemPanel
      // 
      this.OptionalItemPanel.BackColor = System.Drawing.Color.Black;
      this.OptionalItemPanel.Controls.Add(this.OptionalItemFlowPanel);
      this.OptionalItemPanel.Location = new System.Drawing.Point(203, 298);
      this.OptionalItemPanel.MinimumSize = new System.Drawing.Size(40, 56);
      this.OptionalItemPanel.Name = "OptionalItemPanel";
      this.OptionalItemPanel.Size = new System.Drawing.Size(190, 85);
      this.OptionalItemPanel.TabIndex = 8;
      this.OptionalItemPanel.Title = "";
      // 
      // OptionalItemFlowPanel
      // 
      this.OptionalItemFlowPanel.BackColor = System.Drawing.Color.Transparent;
      this.OptionalItemFlowPanel.Location = new System.Drawing.Point(7, 15);
      this.OptionalItemFlowPanel.Margin = new System.Windows.Forms.Padding(0);
      this.OptionalItemFlowPanel.Name = "OptionalItemFlowPanel";
      this.OptionalItemFlowPanel.Size = new System.Drawing.Size(186, 55);
      this.OptionalItemFlowPanel.TabIndex = 5;
      // 
      // QuestPanel
      // 
      this.QuestPanel.BackColor = System.Drawing.Color.Black;
      this.QuestPanel.Controls.Add(this.RequiredItemFlowPanel);
      this.QuestPanel.Location = new System.Drawing.Point(3, 299);
      this.QuestPanel.MinimumSize = new System.Drawing.Size(40, 56);
      this.QuestPanel.Name = "QuestPanel";
      this.QuestPanel.Size = new System.Drawing.Size(194, 83);
      this.QuestPanel.TabIndex = 10;
      this.QuestPanel.Title = "";
      // 
      // RequiredItemFlowPanel
      // 
      this.RequiredItemFlowPanel.BackColor = System.Drawing.Color.Transparent;
      this.RequiredItemFlowPanel.Location = new System.Drawing.Point(16, 16);
      this.RequiredItemFlowPanel.Margin = new System.Windows.Forms.Padding(0);
      this.RequiredItemFlowPanel.Name = "RequiredItemFlowPanel";
      this.RequiredItemFlowPanel.Size = new System.Drawing.Size(359, 61);
      this.RequiredItemFlowPanel.TabIndex = 4;
      // 
      // StatPanel
      // 
      this.StatPanel.BackColor = System.Drawing.Color.Black;
      this.StatPanel.Controls.Add(this.StatTableLayout);
      this.StatPanel.Location = new System.Drawing.Point(3, 3);
      this.StatPanel.MinimumSize = new System.Drawing.Size(128, 56);
      this.StatPanel.Name = "StatPanel";
      this.StatPanel.Size = new System.Drawing.Size(194, 295);
      this.StatPanel.TabIndex = 7;
      this.StatPanel.Text = "dwPanel1";
      this.StatPanel.Title = "STATS";
      // 
      // StatTableLayout
      // 
      this.StatTableLayout.ColumnCount = 2;
      this.StatTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.80981F));
      this.StatTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.19019F));
      this.StatTableLayout.Location = new System.Drawing.Point(17, 25);
      this.StatTableLayout.Name = "StatTableLayout";
      this.StatTableLayout.RowCount = 7;
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.43299F));
      this.StatTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.40206F));
      this.StatTableLayout.Size = new System.Drawing.Size(163, 260);
      this.StatTableLayout.TabIndex = 2;
      // 
      // SpellPanel
      // 
      this.SpellPanel.BackColor = System.Drawing.Color.Black;
      this.SpellPanel.Location = new System.Drawing.Point(203, 3);
      this.SpellPanel.MinimumSize = new System.Drawing.Size(144, 56);
      this.SpellPanel.Name = "SpellPanel";
      this.SpellPanel.Size = new System.Drawing.Size(190, 295);
      this.SpellPanel.TabIndex = 6;
      this.SpellPanel.Text = "dwPanel1";
      this.SpellPanel.Title = "SPELLS";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(505, 949);
      this.Controls.Add(this.DWContentPanel);
      this.Controls.Add(this.DWStatusStrip);
      this.Controls.Add(this.DWMenuStrip);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.DWMenuStrip;
      this.Name = "MainForm";
      this.Text = "Dragon Warrior Randomizer Auto-Tracker";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.DWMenuStrip.ResumeLayout(false);
      this.DWMenuStrip.PerformLayout();
      this.DWStatusStrip.ResumeLayout(false);
      this.DWStatusStrip.PerformLayout();
      this.DWContentPanel.ResumeLayout(false);
      this.CombatPanel.ResumeLayout(false);
      this.CombatPanel.PerformLayout();
      this.CoordsPanel.ResumeLayout(false);
      this.CoordsPanel.PerformLayout();
      this.EnemyPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.EnemyPanelPictureBox)).EndInit();
      this.MapPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
      this.BattlePanel.ResumeLayout(false);
      this.BattlePanel.PerformLayout();
      this.OptionalItemPanel.ResumeLayout(false);
      this.OptionalItemPanel.PerformLayout();
      this.QuestPanel.ResumeLayout(false);
      this.QuestPanel.PerformLayout();
      this.StatPanel.ResumeLayout(false);
      this.StatPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TableLayoutPanel StatTableLayout;
    private System.Windows.Forms.FlowLayoutPanel BattleItemFlowPanel;
    private System.Windows.Forms.FlowLayoutPanel RequiredItemFlowPanel;
    private System.Windows.Forms.FlowLayoutPanel OptionalItemFlowPanel;
    private Controls.DWPanel SpellPanel;
    private Controls.DWPanel StatPanel;
    private Controls.DWPanel OptionalItemPanel;
    private Controls.DWPanel BattlePanel;
    private Controls.DWPanel QuestPanel;
    private System.Windows.Forms.MenuStrip DWMenuStrip;
    private System.Windows.Forms.StatusStrip DWStatusStrip;
    private System.Windows.Forms.ToolStripStatusLabel EmulatorStatusLabel;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem readRomToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.ComponentModel.BackgroundWorker EmulatorConnectionWorker;
    private System.Windows.Forms.Panel DWContentPanel;
    private System.Windows.Forms.ToolStripMenuItem streamerModeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem autoTrackingToolStripMenuItem;
    private Controls.DWPanel CombatPanel;
    private System.Windows.Forms.Panel EnemyPanel;
    private System.Windows.Forms.TableLayoutPanel EnemyStatsTable;
    private System.Windows.Forms.PictureBox EnemyPanelPictureBox;
    private System.Windows.Forms.TableLayoutPanel EnemyInfoTable;
    private System.Windows.Forms.Panel MapPanel;
    private System.Windows.Forms.PictureBox MapPictureBox;
    private Controls.DWPanel CoordsPanel;
    private System.Windows.Forms.TextBox CoordsEWTextBox;
    private System.Windows.Forms.TextBox CoordsNSTextBox;
    private System.Windows.Forms.TextBox CoordsEWCharTextBox;
    private System.Windows.Forms.TextBox CoordsNSCharTextBox;
  }
}

