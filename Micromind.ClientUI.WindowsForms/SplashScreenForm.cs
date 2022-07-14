using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using Micromind.ClientLibraries;
using Micromind.Securities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class SplashScreenForm : SplashScreen
	{
		public enum SplashScreenCommand
		{

		}

		private bool isTrial;

		private IContainer components;

		private MarqueeProgressBarControl marqueeProgressBarControl1;

		private LabelControl labelControl1;

		private LabelControl labelControl2;

		private PictureEdit pictureEdit1;

		private PictureEdit pictureEdit2;

		private LabelControl labelTrial;
        private PictureBox pictureBox1;
        private Label label4;
        private Label label3;
        private Label label1;
        private LabelControl labelEdition;

		public SplashScreenForm()
		{
			InitializeComponent();
		}

		public override void ProcessCommand(Enum cmd, object arg)
		{
			base.ProcessCommand(cmd, arg);
		}

		private void SplashScreenForm_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void Init()
		{
			if (GlobalRules.Edition == Editions.Professional)
			{
				if (GlobalRules.IsMultiUser)
				{
					labelEdition.Text = "Professional Edition: Multi-User";
				}
				else
				{
					labelEdition.Text = "Professional Edition";
				}
			}
			else if (GlobalRules.Edition == Editions.Basic)
			{
				if (GlobalRules.IsMultiUser)
				{
					labelEdition.Text = "Basic Edition: Multi-User";
				}
				else
				{
					labelEdition.Text = "Basic Edition";
				}
			}
            label1.Text = " Copyright Starasoft © 2019-2020";

            labelEdition.BringToFront();
			Application.DoEvents();
			if (!isTrial)
			{
				labelTrial.Visible = false;
			}
			labelTrial.BringToFront();
			Application.DoEvents();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelTrial = new DevExpress.XtraEditors.LabelControl();
            this.labelEdition = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(31, 284);
            this.marqueeProgressBarControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(539, 15);
            this.marqueeProgressBarControl1.TabIndex = 5;
            this.marqueeProgressBarControl1.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(31, 352);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(135, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Copyright © 1998-2011";
            this.labelControl1.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 254);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 16);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Starting...";
            this.labelControl2.Visible = false;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Location = new System.Drawing.Point(16, 15);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureEdit2.Name = "pictureEdit2";
            // 
            // 
            // 
            this.pictureEdit2.Properties.AllowFocused = false;
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowMenu = false;
            this.pictureEdit2.Size = new System.Drawing.Size(568, 222);
            this.pictureEdit2.TabIndex = 9;
            this.pictureEdit2.Visible = false;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(371, 330);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureEdit1.Name = "pictureEdit1";
            // 
            // 
            // 
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(207, 59);
            this.pictureEdit1.TabIndex = 8;
            this.pictureEdit1.Visible = false;
            // 
            // labelTrial
            // 
            this.labelTrial.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelTrial.Appearance.Options.UseFont = true;
            this.labelTrial.Location = new System.Drawing.Point(69, 272);
            this.labelTrial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelTrial.Name = "labelTrial";
            this.labelTrial.Size = new System.Drawing.Size(128, 17);
            this.labelTrial.TabIndex = 16;
            this.labelTrial.Text = "Evaluation Version";
            this.labelTrial.Visible = false;
            // 
            // labelEdition
            // 
            this.labelEdition.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEdition.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(65)))), ((int)(((byte)(113)))));
            this.labelEdition.Appearance.Options.UseFont = true;
            this.labelEdition.Appearance.Options.UseForeColor = true;
            this.labelEdition.Location = new System.Drawing.Point(69, 246);
            this.labelEdition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelEdition.Name = "labelEdition";
            this.labelEdition.Size = new System.Drawing.Size(0, 18);
            this.labelEdition.TabIndex = 17;
            this.labelEdition.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 31);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(260, 242);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(321, 25);
            this.label4.TabIndex = 136;
            this.label4.Text = "Business Managment System";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(231, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(347, 39);
            this.label3.TabIndex = 135;
            this.label3.Text = "Starasoft © StarERP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(661, 441);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 17);
            this.label1.TabIndex = 137;
            this.label1.Text = "Copyright Starasoft © 2019-2020";
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 432);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelEdition);
            this.Controls.Add(this.labelTrial);
            this.Controls.Add(this.pictureEdit2);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SplashScreenForm";
            this.ShowMode = DevExpress.XtraSplashScreen.ShowMode.Image;
            this.SplashImage = ((System.Drawing.Image)(resources.GetObject("$this.SplashImage")));
            this.Text = "Splassh";
            this.Load += new System.EventHandler(this.SplashScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
