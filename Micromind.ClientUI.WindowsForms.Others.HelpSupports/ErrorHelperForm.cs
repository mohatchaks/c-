using DevExpress.XtraEditors;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.SoftReg;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others.HelpSupports
{
	public class ErrorHelperForm : Form
	{
		private Exception ex;

		private string summary = "";

		private MenuItem menuItemCopy;

		private MenuItem menuItemEmailSupport;

		private MenuItem menuItem3;

		private MenuItem menuItemViewSource;

		private ToolTip toolTip1;

		private XPButton buttonCancel;

		private XPButton buttonSourceSummary;

		private XPButton xpButton1;

		private Label label1;

		private PictureBox pictureBox1;

		private TextBox textBoxMessage;

		private SimpleButton buttonSend;

		private BackgroundWorker backgroundWorker1;

		private IContainer components;

		private ErrorEvent errorEvent;

		private bool isShowSendButton = true;

		public string Message
		{
			set
			{
				summary = value;
				ViewDetails();
			}
		}

		public Exception Error
		{
			set
			{
				ex = value;
			}
		}

		public ErrorEvent ErrorEventData
		{
			set
			{
				if (value.sqlException != null)
				{
					Error = value.sqlException;
				}
				else
				{
					Error = value.exception;
				}
				Message = value.Memo;
				if (value.IsRightToLeft)
				{
					textBoxMessage.RightToLeft = RightToLeft.Yes;
				}
				errorEvent = value;
			}
		}

		public bool IsShowSendButton
		{
			set
			{
				if (!value)
				{
					buttonSend.Visible = false;
					buttonCancel.Text = "Close";
					buttonCancel.Left = buttonSend.Left;
				}
			}
		}

		public ErrorHelperForm()
		{
			InitializeComponent();
			base.Activated += ErrorHelperForm_Activated;
		}

		private void ErrorHelperForm_Activated(object sender, EventArgs e)
		{
			if (Global.ConStatus != 0)
			{
				buttonSend.Enabled = false;
			}
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
			menuItemCopy = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemViewSource = new System.Windows.Forms.MenuItem();
			menuItemEmailSupport = new System.Windows.Forms.MenuItem();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonSourceSummary = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			textBoxMessage = new System.Windows.Forms.TextBox();
			buttonSend = new DevExpress.XtraEditors.SimpleButton();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			menuItemCopy.Index = -1;
			menuItemCopy.Text = "";
			menuItem3.Index = -1;
			menuItem3.Text = "";
			menuItemViewSource.Index = -1;
			menuItemViewSource.Text = "";
			menuItemEmailSupport.Index = -1;
			menuItemEmailSupport.Text = "";
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(229, 199);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(102, 31);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Don't Send";
			toolTip1.SetToolTip(buttonCancel, "Do not send error report");
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonSourceSummary.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSourceSummary.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonSourceSummary.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSourceSummary.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSourceSummary.Location = new System.Drawing.Point(8, 199);
			buttonSourceSummary.Name = "buttonSourceSummary";
			buttonSourceSummary.Size = new System.Drawing.Size(79, 31);
			buttonSourceSummary.TabIndex = 1;
			buttonSourceSummary.Text = "S&ummary";
			toolTip1.SetToolTip(buttonSourceSummary, "Summary or source for this error");
			buttonSourceSummary.Click += new System.EventHandler(buttonSourceSummary_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.Location = new System.Drawing.Point(93, 199);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(79, 31);
			xpButton1.TabIndex = 2;
			xpButton1.Text = "&Copy";
			toolTip1.SetToolTip(xpButton1, "Summary or source for this error");
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			label1.Location = new System.Drawing.Point(42, 4);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(382, 32);
			label1.TabIndex = 20;
			label1.Text = "An error has occured in the application. Please send the error to us to review.  The error message is:";
			textBoxMessage.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxMessage.BackColor = System.Drawing.Color.White;
			textBoxMessage.Location = new System.Drawing.Point(-2, 39);
			textBoxMessage.Multiline = true;
			textBoxMessage.Name = "textBoxMessage";
			textBoxMessage.ReadOnly = true;
			textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxMessage.Size = new System.Drawing.Size(467, 150);
			textBoxMessage.TabIndex = 22;
			buttonSend.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSend.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			buttonSend.Appearance.Options.UseFont = true;
			buttonSend.Image = Micromind.ClientUI.Properties.Resources.send;
			buttonSend.Location = new System.Drawing.Point(337, 200);
			buttonSend.LookAndFeel.SkinName = "Money Twins";
			buttonSend.LookAndFeel.UseDefaultLookAndFeel = false;
			buttonSend.Name = "buttonSend";
			buttonSend.Size = new System.Drawing.Size(116, 29);
			buttonSend.TabIndex = 23;
			buttonSend.Text = "Send Error";
			buttonSend.Click += new System.EventHandler(buttonSend_Click);
			pictureBox1.Image = Micromind.ClientUI.Properties.Resources.ErrorIcon;
			pictureBox1.Location = new System.Drawing.Point(6, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(32, 32);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 21;
			pictureBox1.TabStop = false;
			backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
			base.AcceptButton = buttonSend;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(465, 235);
			base.Controls.Add(buttonSend);
			base.Controls.Add(textBoxMessage);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(label1);
			base.Controls.Add(xpButton1);
			base.Controls.Add(buttonSourceSummary);
			base.Controls.Add(buttonCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(416, 248);
			base.Name = "ErrorHelperForm";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Error";
			base.Closing += new System.ComponentModel.CancelEventHandler(ErrorHelperForm_Closing);
			base.Load += new System.EventHandler(ErrorHelperForm_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void buttonDetails_Click(object sender, EventArgs e)
		{
			ViewDetails();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EmailSupport()
		{
			try
			{
				Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
				string text = "Error Message:" + this.ex.Message;
				if (this.ex.GetType() == typeof(CompanyException))
				{
					text = text + "\nCompany Ex NO:" + ((CompanyException)this.ex).Number;
				}
				else if (this.ex.GetType() == typeof(SqlException))
				{
					text = text + "\nSQL Ex NO:" + ((SqlException)this.ex).Number;
				}
				text = text + "\nSource Object:" + this.ex.Source;
				text = text + "\nDetailed Message:" + this.ex.ToString();
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Attribute.GetCustomAttribute(executingAssembly, typeof(AssemblyInformationalVersionAttribute));
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				text = text + "\nException Type:" + this.ex.GetType();
				text = text + "\nF.V." + versionInfo.FileVersion;
				text = text + "\nProduct Version:" + UIGlobal.GetCurrentClientVersion().GetVersionString();
				text = text + "\nUserID:" + Global.CurrentUser;
				text = text + "\nDate:" + DateTime.Now;
				MethodBase targetSite = this.ex.TargetSite;
				if (targetSite != null)
				{
					text = text + "\nModule Name:" + targetSite.Module.Name;
					text = text + "\nMethod Name:" + targetSite.Name;
				}
				softReg.SendError(Global.ProductID.ToString(), Global.GetProductKey(), Global.CompanyName + ":" + Global.ComputerName, text);
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage("Cannot send Error Message.\n" + ex.Message);
			}
		}

		private void ErrorHelperForm_Load(object sender, EventArgs e)
		{
			buttonSourceSummary.Text = "More Info";
			if (Global.ConStatus != 0)
			{
				buttonSend.Enabled = false;
			}
		}

		private void ViewDetails()
		{
			if (buttonSourceSummary.Tag != null && buttonSourceSummary.Tag.ToString() == "1")
			{
				if (ex != null)
				{
					string[] lines = ex.ToString().Split('\n', '\r');
					textBoxMessage.Lines = lines;
					buttonSourceSummary.Text = "Summary";
				}
				buttonSourceSummary.Tag = 0;
			}
			else
			{
				string[] lines2 = summary.Split('\n', '\r');
				textBoxMessage.Lines = lines2;
				buttonSourceSummary.Text = "More Info";
				buttonSourceSummary.Tag = 1;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonSourceSummary_Click(object sender, EventArgs e)
		{
			ViewDetails();
		}

		private void ErrorHelperForm_Closing(object sender, CancelEventArgs e)
		{
			Global.CompanySettings.SaveFormProperties(this);
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(textBoxMessage.Text);
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			backgroundWorker1.RunWorkerAsync();
			Close();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			EmailSupport();
		}
	}
}
