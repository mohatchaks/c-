using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraTab;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.Main;
using Micromind.Common.Data;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class formHome : Form
	{
		private bool isFirstActivated;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem resetLayoutToolStripMenuItem;

		public XtraTabControl tabControlMain;

		private ToolStripMenuItem toolStripMenuItem1;

		private NavBarGroup navBarGroup9;

		private ImageList imageListGroups;

		private StyleController styleController1;

		private XtraTabPage tabHome;

		private Dashboard dashboard1;

		private LabelControl labelControl1;

		private XtraTabControl xtraTabControl1;

		private XtraTabPage tabPageHome;

		private XRDesignPanel xrDesignPanel1;

		private XRDesignPanel xrDesignPanel2;

		private LabelControl labelCustomize;

		private IContainer components;

		public formHome(Form parent)
		{
			InitializeComponent();
			base.MdiParent = parent;
			base.Load += formHome_Load;
			base.FormClosing += formHome_FormClosing;
			EventHelper.RefreshApplicationRequested += EventHelper_RefreshApplicationRequested;
			tabControlMain.ShowTabHeader = DefaultBoolean.False;
		}

		private void EventHelper_RefreshApplicationRequested(object sender, EventArgs e)
		{
			Refresh();
		}

		private void formHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveLayout();
		}

		private void SaveLayout()
		{
			dashboard1.SaveLayout();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.formHome));
			defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(components);
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			tabControlMain = new DevExpress.XtraTab.XtraTabControl();
			tabHome = new DevExpress.XtraTab.XtraTabPage();
			labelCustomize = new DevExpress.XtraEditors.LabelControl();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			tabPageHome = new DevExpress.XtraTab.XtraTabPage();
			dashboard1 = new Micromind.DataControls.Dashboard();
			xrDesignPanel1 = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
			xrDesignPanel2 = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
			imageListGroups = new System.Windows.Forms.ImageList(components);
			navBarGroup9 = new DevExpress.XtraNavBar.NavBarGroup();
			styleController1 = new DevExpress.XtraEditors.StyleController(components);
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tabControlMain).BeginInit();
			tabControlMain.SuspendLayout();
			tabHome.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
			xtraTabControl1.SuspendLayout();
			tabPageHome.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)xrDesignPanel1).BeginInit();
			((System.ComponentModel.ISupportInitialize)xrDesignPanel2).BeginInit();
			((System.ComponentModel.ISupportInitialize)styleController1).BeginInit();
			SuspendLayout();
			defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				resetLayoutToolStripMenuItem,
				toolStripMenuItem1
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(142, 48);
			resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
			resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			resetLayoutToolStripMenuItem.Text = "Reset Layout";
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
			toolStripMenuItem1.Text = "Customize...";
			tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControlMain.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
			tabControlMain.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
			tabControlMain.Location = new System.Drawing.Point(0, 0);
			tabControlMain.LookAndFeel.SkinName = "Blue";
			tabControlMain.MultiLine = DevExpress.Utils.DefaultBoolean.False;
			tabControlMain.Name = "tabControlMain";
			tabControlMain.SelectedTabPage = tabHome;
			tabControlMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			tabControlMain.Size = new System.Drawing.Size(1062, 780);
			tabControlMain.TabIndex = 14;
			tabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[1]
			{
				tabHome
			});
			tabHome.Controls.Add(labelCustomize);
			tabHome.Controls.Add(labelControl1);
			tabHome.Controls.Add(xtraTabControl1);
			tabHome.Controls.Add(xrDesignPanel1);
			tabHome.Controls.Add(xrDesignPanel2);
			tabHome.Name = "tabHome";
			tabHome.Size = new System.Drawing.Size(1057, 775);
			tabHome.Text = "Dashboard";
			labelCustomize.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			labelCustomize.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
			labelCustomize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			labelCustomize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			labelCustomize.Location = new System.Drawing.Point(861, 6);
			labelCustomize.Name = "labelCustomize";
			labelCustomize.Size = new System.Drawing.Size(192, 13);
			labelCustomize.TabIndex = 64;
			labelCustomize.Text = "Customize Dashboards...";
			labelCustomize.Click += new System.EventHandler(labelCustomize_Click);
			labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10f, System.Drawing.FontStyle.Bold);
			labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(26, 88, 134);
			labelControl1.Location = new System.Drawing.Point(11, 4);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(100, 16);
			labelControl1.TabIndex = 5;
			labelControl1.Text = "My Dashboards";
			xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			xtraTabControl1.Location = new System.Drawing.Point(0, 28);
			xtraTabControl1.Name = "xtraTabControl1";
			xtraTabControl1.SelectedTabPage = tabPageHome;
			xtraTabControl1.Size = new System.Drawing.Size(1057, 728);
			xtraTabControl1.TabIndex = 2;
			xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[1]
			{
				tabPageHome
			});
			tabPageHome.Controls.Add(dashboard1);
			tabPageHome.Name = "tabPageHome";
			tabPageHome.Size = new System.Drawing.Size(1052, 703);
			tabPageHome.Text = "Home";
			dashboard1.AllowDrop = true;
			dashboard1.BackColor = System.Drawing.Color.White;
			dashboard1.DashboardKey = "DSHBDashboard";
			dashboard1.Dock = System.Windows.Forms.DockStyle.Fill;
			dashboard1.Location = new System.Drawing.Point(0, 0);
			dashboard1.Name = "dashboard1";
			dashboard1.Size = new System.Drawing.Size(1052, 703);
			dashboard1.TabIndex = 0;
			xrDesignPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			xrDesignPanel1.Location = new System.Drawing.Point(0, 0);
			xrDesignPanel1.Name = "xrDesignPanel1";
			xrDesignPanel1.Size = new System.Drawing.Size(1057, 28);
			xrDesignPanel1.TabIndex = 3;
			xrDesignPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			xrDesignPanel2.Location = new System.Drawing.Point(0, 756);
			xrDesignPanel2.Name = "xrDesignPanel2";
			xrDesignPanel2.Size = new System.Drawing.Size(1057, 19);
			xrDesignPanel2.TabIndex = 4;
			imageListGroups.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListGroups.ImageStream");
			imageListGroups.TransparentColor = System.Drawing.Color.Transparent;
			imageListGroups.Images.SetKeyName(0, "reports32.png");
			navBarGroup9.Caption = "Accounts";
			navBarGroup9.Expanded = true;
			navBarGroup9.Name = "navBarGroup9";
			styleController1.LookAndFeel.SkinName = "VS2010";
			styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			AutoScroll = true;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(1062, 780);
			base.ControlBox = false;
			base.Controls.Add(tabControlMain);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "formHome";
			base.ShowInTaskbar = false;
			Text = "Home Page";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)tabControlMain).EndInit();
			tabControlMain.ResumeLayout(false);
			tabHome.ResumeLayout(false);
			tabHome.PerformLayout();
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
			xtraTabControl1.ResumeLayout(false);
			tabPageHome.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)xrDesignPanel1).EndInit();
			((System.ComponentModel.ISupportInitialize)xrDesignPanel2).EndInit();
			((System.ComponentModel.ISupportInitialize)styleController1).EndInit();
			ResumeLayout(false);
		}

		private void formHome_Load(object sender, EventArgs e)
		{
			try
			{
				Init();
				RegistryHelper registryHelper = new RegistryHelper();
				string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
				if (stringValue != "")
				{
					defaultLookAndFeel1.LookAndFeel.SkinName = stringValue;
				}
				else
				{
					defaultLookAndFeel1.LookAndFeel.SkinName = "iMaginary";
				}
				LoadUserSettings();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadUserSettings()
		{
			dashboard1.LoadLayout();
		}

		private void Init()
		{
		}

		public void OnActivated()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					if (!isFirstActivated)
					{
						isFirstActivated = true;
					}
				}
				finally
				{
					Global.ChangeApplicationStatusMessage(Text);
				}
			}
		}

		private void CompanyMessage()
		{
		}

		public void Reset()
		{
			CompanyMessage();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.General;
		}

		public static int GetScreenID()
		{
			return 0;
		}

		public void RefreshData()
		{
			OnActivated();
		}

		private void navBarControl2_Click(object sender, EventArgs e)
		{
		}

		private void labelCustomize_Click(object sender, EventArgs e)
		{
			new CustomizeDashboardsForm().ShowDialog(this);
		}
	}
}
