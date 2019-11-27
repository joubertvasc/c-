/*
 * NETShots - by Alessandro Fragnani
 * NETShoot.cs
 * Created: 28 march 2005
 * 
 * Main form (configuration also) for NETShots
 * 
 * Originaly has:
 *  - The Settings TabSheet
 *  - The Folders TabSheet
 *  - The NotifyIcon
 * 
 * Thanks to:
 *  - Rakesh Rajan for his article "How to develop a screen saver in C#"
 *    (http://www.codeproject.com/csharp/scrframework.asp)
 *  - Jason Henderson for his article "WallpaperQ: A Wallpaper Management Tool"
 *    (http://www.codeproject.com/tools/wallpaperq.asp)
 *  - Zenicom for his article "Load and Save objects to XML using serialization"
 *    (http://www.codeproject.com/csharp/xml_serializationasp.asp)
 * 
 */ 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Win32;
using System.Reflection;


namespace NetShots
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItemNewWallpaper;
		private System.Windows.Forms.MenuItem menuItemStartScreensaver;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItemSettings;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.TabPage tabPageFolders;
		private System.Windows.Forms.TextBox textBoxPathToAdd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAddPath;
		private System.Windows.Forms.Button buttonUpdateImageList;
		private System.Windows.Forms.ListBox listBoxPathList;
		private System.Windows.Forms.Button buttonShoot;
		private System.Windows.Forms.ListBox listBoxFileList;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBoxUseAsWallpaper;
		private System.Windows.Forms.ComboBox comboBoxCycleWallpaper;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBoxUseAsScreenshot;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDownWallpaperStart;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox checkBoxTray;
		private System.Windows.Forms.TextBox textBoxShortcutScreensaver;
		private System.Windows.Forms.TextBox textBoxShortcutWallpaper;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown numericUpDownWallpaperCycle;
		private System.Windows.Forms.Timer timerScreenshot;
		private System.Windows.Forms.Timer timerWallpaper;
		private System.Windows.Forms.Button buttonRemovePath;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemDisableScreensaver;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}



		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItemNewWallpaper = new System.Windows.Forms.MenuItem();
			this.menuItemStartScreensaver = new System.Windows.Forms.MenuItem();
			this.menuItemDisableScreensaver = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemSettings = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.textBoxShortcutWallpaper = new System.Windows.Forms.TextBox();
			this.textBoxShortcutScreensaver = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.checkBoxTray = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownWallpaperCycle = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownWallpaperStart = new System.Windows.Forms.NumericUpDown();
			this.checkBoxUseAsScreenshot = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBoxCycleWallpaper = new System.Windows.Forms.ComboBox();
			this.checkBoxUseAsWallpaper = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPageFolders = new System.Windows.Forms.TabPage();
			this.buttonRemovePath = new System.Windows.Forms.Button();
			this.buttonShoot = new System.Windows.Forms.Button();
			this.listBoxFileList = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonUpdateImageList = new System.Windows.Forms.Button();
			this.listBoxPathList = new System.Windows.Forms.ListBox();
			this.textBoxPathToAdd = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonAddPath = new System.Windows.Forms.Button();
			this.timerScreenshot = new System.Windows.Forms.Timer(this.components);
			this.timerWallpaper = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWallpaperCycle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWallpaperStart)).BeginInit();
			this.tabPageFolders.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "NETShots";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItemNewWallpaper,
																						 this.menuItemStartScreensaver,
																						 this.menuItemDisableScreensaver,
																						 this.menuItem1,
																						 this.menuItemSettings,
																						 this.menuItem5,
																						 this.menuItemAbout,
																						 this.menuItem3,
																						 this.menuItemExit});
			// 
			// menuItemNewWallpaper
			// 
			this.menuItemNewWallpaper.Index = 0;
			this.menuItemNewWallpaper.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftW;
			this.menuItemNewWallpaper.Text = "New Wallpaper";
			this.menuItemNewWallpaper.Click += new System.EventHandler(this.menuItemNewWallpaper_Click);
			// 
			// menuItemStartScreensaver
			// 
			this.menuItemStartScreensaver.Index = 1;
			this.menuItemStartScreensaver.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.menuItemStartScreensaver.Text = "Start Screensaver";
			this.menuItemStartScreensaver.Click += new System.EventHandler(this.menuItemStartScreensaver_Click);
			// 
			// menuItemDisableScreensaver
			// 
			this.menuItemDisableScreensaver.Index = 2;
			this.menuItemDisableScreensaver.Text = "Disable Screensaver";
			this.menuItemDisableScreensaver.Click += new System.EventHandler(this.menuItemDisableScreensaver_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Index = 4;
			this.menuItemSettings.Text = "Settings...";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "-";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 6;
			this.menuItemAbout.Text = "About...";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 7;
			this.menuItem3.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 8;
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPageSettings,
																					  this.tabPageFolders});
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(472, 352);
			this.tabControl1.TabIndex = 11;
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.textBoxShortcutWallpaper,
																						  this.textBoxShortcutScreensaver,
																						  this.label10,
																						  this.label9,
																						  this.checkBoxTray,
																						  this.label8,
																						  this.label7,
																						  this.numericUpDownWallpaperCycle,
																						  this.label6,
																						  this.numericUpDownWallpaperStart,
																						  this.checkBoxUseAsScreenshot,
																						  this.label5,
																						  this.label4,
																						  this.comboBoxCycleWallpaper,
																						  this.checkBoxUseAsWallpaper,
																						  this.label2});
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Size = new System.Drawing.Size(464, 326);
			this.tabPageSettings.TabIndex = 0;
			this.tabPageSettings.Text = "Settings";
			// 
			// textBoxShortcutWallpaper
			// 
			this.textBoxShortcutWallpaper.Enabled = false;
			this.textBoxShortcutWallpaper.Location = new System.Drawing.Point(224, 272);
			this.textBoxShortcutWallpaper.Name = "textBoxShortcutWallpaper";
			this.textBoxShortcutWallpaper.Size = new System.Drawing.Size(34, 20);
			this.textBoxShortcutWallpaper.TabIndex = 16;
			this.textBoxShortcutWallpaper.Text = "W";
			// 
			// textBoxShortcutScreensaver
			// 
			this.textBoxShortcutScreensaver.Enabled = false;
			this.textBoxShortcutScreensaver.Location = new System.Drawing.Point(224, 246);
			this.textBoxShortcutScreensaver.Name = "textBoxShortcutScreensaver";
			this.textBoxShortcutScreensaver.Size = new System.Drawing.Size(34, 20);
			this.textBoxShortcutScreensaver.TabIndex = 15;
			this.textBoxShortcutScreensaver.Text = "S";
			// 
			// label10
			// 
			this.label10.Enabled = false;
			this.label10.Location = new System.Drawing.Point(10, 274);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(212, 23);
			this.label10.TabIndex = 18;
			this.label10.Text = "Cycle wallpaper by pressing Ctrl + Shift + ";
			// 
			// label9
			// 
			this.label9.Enabled = false;
			this.label9.Location = new System.Drawing.Point(10, 250);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(220, 23);
			this.label9.TabIndex = 17;
			this.label9.Text = "Start screensaver by pressing Ctrl + Shift + ";
			// 
			// checkBoxTray
			// 
			this.checkBoxTray.Location = new System.Drawing.Point(12, 210);
			this.checkBoxTray.Name = "checkBoxTray";
			this.checkBoxTray.Size = new System.Drawing.Size(432, 24);
			this.checkBoxTray.TabIndex = 12;
			this.checkBoxTray.Text = "Place NETShots in System Tray";
			this.toolTip1.SetToolTip(this.checkBoxTray, "Select this option if you want have NETShots on your System Tray");
			this.checkBoxTray.CheckedChanged += new System.EventHandler(this.checkBoxTray_CheckedChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(198, 174);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 23);
			this.label8.TabIndex = 11;
			this.label8.Text = "seconds";
			this.label8.Click += new System.EventHandler(this.label8_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(198, 148);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 23);
			this.label7.TabIndex = 10;
			this.label7.Text = "minutes";
			// 
			// numericUpDownWallpaperCycle
			// 
			this.numericUpDownWallpaperCycle.Location = new System.Drawing.Point(138, 172);
			this.numericUpDownWallpaperCycle.Maximum = new System.Decimal(new int[] {
																						60,
																						0,
																						0,
																						0});
			this.numericUpDownWallpaperCycle.Minimum = new System.Decimal(new int[] {
																						5,
																						0,
																						0,
																						0});
			this.numericUpDownWallpaperCycle.Name = "numericUpDownWallpaperCycle";
			this.numericUpDownWallpaperCycle.Size = new System.Drawing.Size(54, 20);
			this.numericUpDownWallpaperCycle.TabIndex = 9;
			this.toolTip1.SetToolTip(this.numericUpDownWallpaperCycle, "How much time each image stays visible");
			this.numericUpDownWallpaperCycle.Value = new System.Decimal(new int[] {
																					  5,
																					  0,
																					  0,
																					  0});
			this.numericUpDownWallpaperCycle.ValueChanged += new System.EventHandler(this.numericUpDownWallpaperCycle_ValueChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(28, 174);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 23);
			this.label6.TabIndex = 8;
			this.label6.Text = "Display each image";
			// 
			// numericUpDownWallpaperStart
			// 
			this.numericUpDownWallpaperStart.Location = new System.Drawing.Point(138, 146);
			this.numericUpDownWallpaperStart.Maximum = new System.Decimal(new int[] {
																						60,
																						0,
																						0,
																						0});
			this.numericUpDownWallpaperStart.Minimum = new System.Decimal(new int[] {
																						1,
																						0,
																						0,
																						0});
			this.numericUpDownWallpaperStart.Name = "numericUpDownWallpaperStart";
			this.numericUpDownWallpaperStart.Size = new System.Drawing.Size(54, 20);
			this.numericUpDownWallpaperStart.TabIndex = 7;
			this.toolTip1.SetToolTip(this.numericUpDownWallpaperStart, "How much time to wait til staring the Screensaver");
			this.numericUpDownWallpaperStart.Value = new System.Decimal(new int[] {
																					  5,
																					  0,
																					  0,
																					  0});
			this.numericUpDownWallpaperStart.ValueChanged += new System.EventHandler(this.numericUpDownWallpaperStart_ValueChanged);
			// 
			// checkBoxUseAsScreenshot
			// 
			this.checkBoxUseAsScreenshot.Location = new System.Drawing.Point(12, 122);
			this.checkBoxUseAsScreenshot.Name = "checkBoxUseAsScreenshot";
			this.checkBoxUseAsScreenshot.Size = new System.Drawing.Size(244, 24);
			this.checkBoxUseAsScreenshot.TabIndex = 6;
			this.checkBoxUseAsScreenshot.Text = "Use NETShots as Screensaver";
			this.toolTip1.SetToolTip(this.checkBoxUseAsScreenshot, "Select this option if you want have NETShots as your Screensaver");
			this.checkBoxUseAsScreenshot.CheckedChanged += new System.EventHandler(this.checkBoxUseAsScreenshot_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 148);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "Screensaver starts in";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(442, 40);
			this.label4.TabIndex = 4;
			this.label4.Text = "You can use NETShots as your Wallpaper and/or Screensaver managers. Just select y" +
				"our options and have fun.";
			// 
			// comboBoxCycleWallpaper
			// 
			this.comboBoxCycleWallpaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCycleWallpaper.Items.AddRange(new object[] {
																		"Never",
																		"15 minutes",
																		"30 minutes",
																		"Hour",
																		"Day"});
			this.comboBoxCycleWallpaper.Location = new System.Drawing.Point(142, 80);
			this.comboBoxCycleWallpaper.Name = "comboBoxCycleWallpaper";
			this.comboBoxCycleWallpaper.Size = new System.Drawing.Size(121, 21);
			this.comboBoxCycleWallpaper.TabIndex = 3;
			this.toolTip1.SetToolTip(this.comboBoxCycleWallpaper, "Determine the time between each Wallpaper change");
			this.comboBoxCycleWallpaper.SelectedIndexChanged += new System.EventHandler(this.comboBoxCycleWallpaper_SelectedIndexChanged);
			// 
			// checkBoxUseAsWallpaper
			// 
			this.checkBoxUseAsWallpaper.Location = new System.Drawing.Point(12, 58);
			this.checkBoxUseAsWallpaper.Name = "checkBoxUseAsWallpaper";
			this.checkBoxUseAsWallpaper.Size = new System.Drawing.Size(244, 24);
			this.checkBoxUseAsWallpaper.TabIndex = 2;
			this.checkBoxUseAsWallpaper.Text = "Use NETShots as Wallpaper";
			this.toolTip1.SetToolTip(this.checkBoxUseAsWallpaper, "Select this option if you want that NETShots selects a random images as your Wall" +
				"paper");
			this.checkBoxUseAsWallpaper.CheckedChanged += new System.EventHandler(this.checkBoxUseAsWallpaper_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Cycle Wallpaper every";
			// 
			// tabPageFolders
			// 
			this.tabPageFolders.Controls.AddRange(new System.Windows.Forms.Control[] {
																						 this.buttonRemovePath,
																						 this.buttonShoot,
																						 this.listBoxFileList,
																						 this.label3,
																						 this.buttonUpdateImageList,
																						 this.listBoxPathList,
																						 this.textBoxPathToAdd,
																						 this.label1,
																						 this.buttonAddPath});
			this.tabPageFolders.Location = new System.Drawing.Point(4, 22);
			this.tabPageFolders.Name = "tabPageFolders";
			this.tabPageFolders.Size = new System.Drawing.Size(464, 326);
			this.tabPageFolders.TabIndex = 1;
			this.tabPageFolders.Text = "Folders";
			// 
			// buttonRemovePath
			// 
			this.buttonRemovePath.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonRemovePath.Location = new System.Drawing.Point(304, 164);
			this.buttonRemovePath.Name = "buttonRemovePath";
			this.buttonRemovePath.Size = new System.Drawing.Size(75, 20);
			this.buttonRemovePath.TabIndex = 16;
			this.buttonRemovePath.Text = "Remove";
			this.toolTip1.SetToolTip(this.buttonRemovePath, "Remove selected folder from NETShots");
			this.buttonRemovePath.Click += new System.EventHandler(this.buttonRemovePath_Click);
			// 
			// buttonShoot
			// 
			this.buttonShoot.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.buttonShoot.Location = new System.Drawing.Point(382, 296);
			this.buttonShoot.Name = "buttonShoot";
			this.buttonShoot.TabIndex = 14;
			this.buttonShoot.Text = "Shoot";
			this.toolTip1.SetToolTip(this.buttonShoot, "Shoot a random image");
			this.buttonShoot.Click += new System.EventHandler(this.button2_Click);
			// 
			// listBoxFileList
			// 
			this.listBoxFileList.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listBoxFileList.Location = new System.Drawing.Point(8, 192);
			this.listBoxFileList.Name = "listBoxFileList";
			this.listBoxFileList.Size = new System.Drawing.Size(448, 95);
			this.listBoxFileList.TabIndex = 13;
			this.toolTip1.SetToolTip(this.listBoxFileList, "The list of files");
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 176);
			this.label3.Name = "label3";
			this.label3.TabIndex = 15;
			this.label3.Text = "Files";
			// 
			// buttonUpdateImageList
			// 
			this.buttonUpdateImageList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonUpdateImageList.Location = new System.Drawing.Point(384, 164);
			this.buttonUpdateImageList.Name = "buttonUpdateImageList";
			this.buttonUpdateImageList.Size = new System.Drawing.Size(75, 20);
			this.buttonUpdateImageList.TabIndex = 12;
			this.buttonUpdateImageList.Text = "Update";
			this.toolTip1.SetToolTip(this.buttonUpdateImageList, "Update image database");
			this.buttonUpdateImageList.Click += new System.EventHandler(this.buttonUpdateImageList_Click);
			// 
			// listBoxPathList
			// 
			this.listBoxPathList.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listBoxPathList.Location = new System.Drawing.Point(8, 52);
			this.listBoxPathList.Name = "listBoxPathList";
			this.listBoxPathList.Size = new System.Drawing.Size(448, 108);
			this.listBoxPathList.TabIndex = 11;
			this.toolTip1.SetToolTip(this.listBoxPathList, "The list of folders");
			// 
			// textBoxPathToAdd
			// 
			this.textBoxPathToAdd.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.textBoxPathToAdd.Location = new System.Drawing.Point(8, 24);
			this.textBoxPathToAdd.Name = "textBoxPathToAdd";
			this.textBoxPathToAdd.Size = new System.Drawing.Size(376, 20);
			this.textBoxPathToAdd.TabIndex = 6;
			this.textBoxPathToAdd.Text = "add a path";
			this.toolTip1.SetToolTip(this.textBoxPathToAdd, "Select a folder to be used on NETShots");
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Select a folder to be added";
			// 
			// buttonAddPath
			// 
			this.buttonAddPath.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.buttonAddPath.Location = new System.Drawing.Point(384, 24);
			this.buttonAddPath.Name = "buttonAddPath";
			this.buttonAddPath.Size = new System.Drawing.Size(75, 20);
			this.buttonAddPath.TabIndex = 7;
			this.buttonAddPath.Text = "Add Folder";
			this.toolTip1.SetToolTip(this.buttonAddPath, "Add the folder to NETShots");
			this.buttonAddPath.Click += new System.EventHandler(this.buttonAddPath_Click);
			// 
			// timerWallpaper
			// 
			this.timerWallpaper.Tick += new System.EventHandler(this.timerWallpaper_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 366);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 400);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "NETShots";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPageSettings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWallpaperCycle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWallpaperStart)).EndInit();
			this.tabPageFolders.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{
			// /s (screensaver)
			// /c (settings)
			// /p (preview)

			// the objects are created here, no matter if firing the manager of the
			// screenshot

			SettingsManager sm = new SettingsManager();
			FileManager fm = new FileManager();

			foreach (string path in sm.settings.Paths) 
			{
				fm.AddDirectory(path);
			}
			fm.UpdateDirectories();
			Console.WriteLine("Processed files '{0}'.", fm.images.Count);       

//			System.Windows.Forms.Application.Run(new Shoot(sm, fm));

			if (args.Length > 0)
			{
				if (args[0].ToLower().Trim().Substring(0,2) == "/c")
				{
					SettingsManager.executionKind = SettingsManager.ExecutionKind.ekSetup;
					Application.Run(new MainForm(sm, fm));
				}
				else if (args[0].ToLower() == "/s")
				{
					SettingsManager.executionKind = SettingsManager.ExecutionKind.ekScreensaver;
					System.Windows.Forms.Application.Run(new Shoot(sm, fm));
				}
			}
			else
			{
				SettingsManager.executionKind = SettingsManager.ExecutionKind.ekApplication;
				Application.Run(new MainForm(sm, fm));
			}


		}


		/// <summary>
		/// The default initialization, receiving the SettingsManager and FileManager
		/// from the Main() method
		/// </summary>
		/// <param name="sm"></param>
		/// <param name="fm"></param>
		public MainForm(SettingsManager sm, FileManager fm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			if (SettingsManager.executionKind == SettingsManager.ExecutionKind.ekApplication) 
			{
				this.WindowState = FormWindowState.Minimized;
				MainForm_Resize(null, null);
			} 
			else 
			{
				this.WindowState = FormWindowState.Normal;
			}


			this.settingsManager = sm;
			this.fileManager = fm;

			listBoxPathList.BeginUpdate();
			try
			{
				foreach(string path in settingsManager.settings.Paths) 
				{
					listBoxPathList.Items.Add(path);
				}
			} 
			finally
			{
				listBoxPathList.EndUpdate();
			}

			BeginApplySettings();
			ApplySettings(true);
			EndApplySettings();
		}


		private bool Applying = false;

		private void BeginApplySettings()
		{
			Applying = true;
		}
		
		private void EndApplySettings() 
		{	
			Applying = false;
		}


		// managers 
		private SettingsManager settingsManager = null;
		private FileManager fileManager = null;


		/// <summary>
		/// Add a path to the queue, to be updated latter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddPath_Click(object sender, System.EventArgs e)
		{
			if (Directory.Exists(textBoxPathToAdd.Text)) 			
			{
				listBoxPathList.Items.Add(textBoxPathToAdd.Text);
				//\\
				fileManager.AddDirectory(textBoxPathToAdd.Text);
			} 
			else 
			{
				MessageBox.Show(this, "Invalid path");
			}
		}



		private void button2_Click(object sender, System.EventArgs e)
		{
			StartScreensaver();
		}

		/// <summary>
		/// Fires to update the image list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateImageList_Click(object sender, System.EventArgs e)
		{
			UpdateImageList(false);
		}


		/// <summary>
		/// Update the image list
		/// </summary>
		private void UpdateImageList(bool updateAnyway) 
		{
			if (fileManager.needUpdate || updateAnyway) 
			{
				listBoxFileList.BeginUpdate();

				try
				{
					// clear
					listBoxFileList.Items.Clear();

					fileManager.UpdateDirectories();
			
					foreach (string path in fileManager.images) 
					{
						listBoxFileList.Items.Add(path);
					}

				} 
				finally
				{
					listBoxFileList.EndUpdate();
				}
			}
		}


		private void MainForm_Load(object sender, System.EventArgs e)
		{
			if (settingsManager.settings.PlaceOnTray) 
			{
				this.Hide();				
				notifyIcon1.Visible = true;
				notifyIcon1.Text = "NETShots(c) Alessandro Fragnani\n" + 
					settingsManager.settings.Paths.Count.ToString() + " Folders and " +
					fileManager.images.Count.ToString() + " Images";					
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SaveSettings();
		}


		/// <summary>
		/// Save the settings CHANGED ON GUI
		/// </summary>
		private void SaveSettings() 
		{
			if (Applying) 
			{
				return;
			}

			// set
			//settings.ScreenshotTimeInterval = 15;
			settingsManager.settings.Paths.Clear();
			settingsManager.settings.Paths.AddRange(listBoxPathList.Items);

			settingsManager.settings.PlaceOnTray = checkBoxTray.Checked;

			settingsManager.settings.UseAsScreenshot = checkBoxUseAsScreenshot.Checked;
			settingsManager.settings.ScreenshotStartsIn = (int)numericUpDownWallpaperStart.Value;
			settingsManager.settings.ScreenshotCycleEach = (int)numericUpDownWallpaperCycle.Value;

			settingsManager.settings.UseAsWallpaper = checkBoxUseAsWallpaper.Checked;
			/// a
			/// Never
			/// 15 minutes
			/// 30 minutes
			/// Hour
			/// Day
			switch(comboBoxCycleWallpaper.SelectedIndex)       
			{
				case 0: settingsManager.settings.WallpaperCycleEach = 0;break;
				case 1: settingsManager.settings.WallpaperCycleEach = 15;break;
				case 2: settingsManager.settings.WallpaperCycleEach = 30;break;
				case 3: settingsManager.settings.WallpaperCycleEach = 60;break;
				default: settingsManager.settings.WallpaperCycleEach = 60 * 24;break;
			}
				
			// NO TRAY ??? RUN !!!
			if (SettingsManager.executionKind == SettingsManager.ExecutionKind.ekApplication) 
			{
				if (settingsManager.settings.PlaceOnTray) 
				{
					// add to registry
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
						@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue(
						"NETShots", Application.ExecutablePath);
				} 
				else
				{
					// remove from registry
					if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
						@"Software\Microsoft\Windows\CurrentVersion\Run", true).GetValue(
						"NETShots") != null) 
					{
						RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
							@"Software\Microsoft\Windows\CurrentVersion\Run", true);
						rk.DeleteValue("NETShots");
					}
				}
			}


			// SCREENSAVER ??? 
			if (checkBoxUseAsScreenshot.Checked) 
			{
				IDictionary dict = System.Environment.GetEnvironmentVariables();
				System.Collections.IDictionaryEnumerator enumerator = dict.GetEnumerator();				
				while (enumerator.MoveNext()) 
				{

					if (enumerator.Key.ToString().ToLower().Equals("windir")) 
					{
						RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
						rk.SetValue("SCRNSAVE.EXE", enumerator.Value + "\\NETShots.scr");
						rk.SetValue("ScreenSaveTimeOut", (settingsManager.settings.ScreenshotStartsIn * 60).ToString());


						break;
					}
				}
			} 
			else // should be "disable" or "isn´t me anyway"
			{
				RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
				if ((rk.GetValue("SCRNSAVE.EXE") != null) & (rk.GetValue("SCRNSAVE.EXE").ToString().IndexOf("NETShots.scr") > 0)) 
				{
					rk.DeleteValue("SCRNSAVE.EXE");
				}
			}

			settingsManager.SaveSettings();
		}


		/// <summary>
		/// Apply the settings TO GUI
		/// </summary>
		private void ApplySettings(bool updateAnyway) 
		{
			checkBoxTray.Checked = settingsManager.settings.PlaceOnTray;

			checkBoxUseAsScreenshot.Checked = settingsManager.settings.UseAsScreenshot;
			numericUpDownWallpaperStart.Value = settingsManager.settings.ScreenshotStartsIn;
			numericUpDownWallpaperCycle.Value = settingsManager.settings.ScreenshotCycleEach;

			checkBoxUseAsWallpaper.Checked = settingsManager.settings.UseAsWallpaper;

			if (settingsManager.settings.UseAsWallpaper) 
			{
				/// a
				/// Never
				/// 15 minutes
				/// 30 minutes
				/// Hour
				/// Day
				switch (settingsManager.settings.WallpaperCycleEach)
				{
					case 0 : comboBoxCycleWallpaper.SelectedIndex = 0; break;
					case 15: comboBoxCycleWallpaper.SelectedIndex = 1; break;
					case 30: comboBoxCycleWallpaper.SelectedIndex = 2; break;
					case 60: comboBoxCycleWallpaper.SelectedIndex = 3; break;
					default: comboBoxCycleWallpaper.SelectedIndex = 4; break;
				}		
			} 
			else 
			{
				comboBoxCycleWallpaper.Enabled = false;
			}
			
			UpdateImageList(updateAnyway);
			
			// wallpaper
			timerWallpaper.Enabled = false;
			if (settingsManager.settings.UseAsWallpaper)
			{
				timerWallpaper.Enabled = settingsManager.settings.WallpaperCycleEach > 0;
				timerWallpaper.Interval = settingsManager.settings.WallpaperCycleEach * 1000 * 60;
				// if daily, check each our, against the settings.LastWallpaperCycle value
				if (settingsManager.settings.WallpaperCycleEach > 60) 
				{
					timerWallpaper.Interval = settingsManager.settings.WallpaperCycleEach * 1000 * 60;
				}
			}

			// screenshot

			// tray
			notifyIcon1.Visible = settingsManager.settings.PlaceOnTray;
			notifyIcon1.Text = "NETShots(c) Alessandro Fragnani\n" + 
				settingsManager.settings.Paths.Count.ToString() + " Folders and " +
				fileManager.images.Count.ToString() + " Images";					
		}


		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Open the Settings window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemSettings_Click(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			this.BringToFront();
		}

		private void menuItemNewWallpaper_Click(object sender, System.EventArgs e)
		{
			ShootNewWallpaper();
		}

		/*
		 * Defines a new Wallpaper
		 */
		private void ShootNewWallpaper() 
		{
			if (fileManager.images.Count > 0) 
			{
				// random
				Random r = new Random();
				int rr = r.Next(fileManager.images.Count);
				string s = fileManager.images[rr].ToString();

				// set wallpaper
				Wallpaper.Set(new Uri(s), Wallpaper.Style.Centered);
			}
		}

		private void label8_Click(object sender, System.EventArgs e)
		{
		
		}

		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			if ((sender != null) && (this.WindowState == FormWindowState.Minimized)) 
			{
				checkBoxTray.Checked = true;
				settingsManager.settings.PlaceOnTray = true;

				SaveSettings();
				ApplySettings(false);

				this.Hide();
			}
																
		}

		private void timerWallpaper_Tick(object sender, System.EventArgs e)
		{
			// 06/04/2005 - for "Daily" it check if already is tomorrow :)	
			if (settingsManager.settings.WallpaperCycleEach > 60) 
			{
				if (settingsManager.settings.LastWallpaperCycle.AddDays(1) == DateTime.Now) 
				{
					settingsManager.settings.LastWallpaperCycle = DateTime.Now;
				} 
				else 
				{
					return;
				}
			}

			ShootNewWallpaper();
		}

		private void menuItemStartScreensaver_Click(object sender, System.EventArgs e)
		{
			StartScreensaver();		
		}

		private void StartScreensaver() 
		{
			Shoot f = new Shoot(settingsManager, fileManager);
			f.Show();
		}

		private void numericUpDownWallpaperStart_ValueChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
		}

		private void numericUpDownWallpaperCycle_ValueChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
		}

		private void checkBoxTray_CheckedChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
		}

		private void checkBoxUseAsScreenshot_CheckedChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
		}

		private void checkBoxUseAsWallpaper_CheckedChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
			comboBoxCycleWallpaper.Enabled = checkBoxUseAsWallpaper.Checked;
			if (comboBoxCycleWallpaper.SelectedIndex < 0) 
			{
				comboBoxCycleWallpaper.SelectedIndex = 1;
			}
		}

		private void comboBoxCycleWallpaper_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SaveSettings();
		}

		private void buttonRemovePath_Click(object sender, System.EventArgs e)
		{
			fileManager.DelDirectory(listBoxPathList.SelectedItem.ToString());
			listBoxPathList.Items.RemoveAt(listBoxPathList.SelectedIndex);
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			About about = new About();
			about.ShowDialog(this);
			about.Dispose();
		}

		private void menuItemDisableScreensaver_Click(object sender, System.EventArgs e)
		{
			bool activate = menuItemDisableScreensaver.Text.Equals("Enable Screensaver");
			ToggleEnableScreensaver(activate);
			if (activate) 
			{
				menuItemDisableScreensaver.Text = "Disable Screensaver";
			} 
			else 
			{
				menuItemDisableScreensaver.Text = "Enable Screensaver";
			}
		}

		/// <summary>
		/// Toggles the Screensaver active property. It allows to Enable/Disable it for a while
		/// </summary>
		/// <param name="enable"></param>
		private void ToggleEnableScreensaver(bool enable) 
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey( @"Control Panel\Desktop", true ) ;
			if (enable)
			{
				key.SetValue(@"ScreenSaveActive", 1.ToString( ) ) ;
			} 
			else 
			{
				key.SetValue(@"ScreenSaveActive", 0.ToString( ) ) ;
			}
		}

	}
}
