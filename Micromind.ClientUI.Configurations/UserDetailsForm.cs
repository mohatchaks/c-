using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class UserDetailsForm : Form, IForm
	{
		private UserData currentData;

		private const string TABLENAME_CONST = "Users";

		private const string IDFIELD_CONST = "UserID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private LocationComboBox comboBoxLocation;

		private MMTextBox textBoxPhone;

		private MMLabel mmLabel5;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private XPButton buttonGroups;

		private MMTextBox textBoxPassword;

		private MMTextBox textBoxConfirmPassword;

		private MMLabel mmLabel2;

		private MMLabel mmLabel7;

		private XPButton buttonChangePassword;

		private XPButton buttonAccessRight;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxDefaultSalesperson;

		private LocationComboBox comboBoxDefaultInvLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMTextBox textBoxDefaultInvLocation;

		private UltraGroupBox ultraGroupBox3;

		private LocationComboBox comboBoxDefaultTransLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMTextBox textBoxDefaultTransLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private MMTextBox textBoxLocationName;

		private CheckBox checkBoxIsAdmin;

		private SalespersonComboBox comboBoxDefaultSalesperson;

		private RegisterComboBox comboBoxDefaultTransRegister;

		private MMTextBox textBoxDefaultTransRegister;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private CheckBox checkBoxCLPass;

		private MMTextBox textBoxCLPass;

		private MMLabel labelCLPass;

		private XPButton buttonAddLocation;

		private XPButton buttonCreateHomeDashboard;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8003;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
					buttonChangePassword.Visible = false;
					MMTextBox mMTextBox = textBoxPassword;
					bool readOnly = textBoxConfirmPassword.ReadOnly = false;
					mMTextBox.ReadOnly = readOnly;
					MMTextBox mMTextBox2 = textBoxPassword;
					readOnly = (textBoxConfirmPassword.TabStop = true);
					mMTextBox2.TabStop = readOnly;
					Color color2 = textBoxPassword.BackColor = (textBoxConfirmPassword.BackColor = Color.White);
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					buttonChangePassword.Visible = true;
					textBoxCode.ReadOnly = true;
					MMTextBox mMTextBox3 = textBoxPassword;
					bool readOnly = textBoxConfirmPassword.ReadOnly = true;
					mMTextBox3.ReadOnly = readOnly;
					MMTextBox mMTextBox4 = textBoxPassword;
					readOnly = (textBoxConfirmPassword.TabStop = false);
					mMTextBox4.TabStop = readOnly;
					Color color2 = textBoxPassword.BackColor = (textBoxConfirmPassword.BackColor = Color.WhiteSmoke);
				}
				buttonAccessRight.Enabled = !value;
				buttonGroups.Enabled = !value;
				buttonCreateHomeDashboard.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public UserDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += UserDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new UserData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.UserTable.Rows[0] : currentData.UserTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UserID"] = textBoxCode.Text.Trim();
				dataRow["UserName"] = textBoxName.Text.Trim();
				dataRow["Password"] = textBoxPassword.Text.Trim();
				dataRow["IsCLUser"] = checkBoxCLPass.Checked;
				if (checkBoxCLPass.Checked && !textBoxCLPass.Text.IsNullOrEmpty())
				{
					dataRow["CLUserPass"] = Factory.Encrypt(textBoxCLPass.Text.Trim());
				}
				else
				{
					dataRow["CLUserPass"] = DBNull.Value;
				}
				if (comboBoxEmployee.SelectedID != "")
				{
					dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				}
				else
				{
					dataRow["EmployeeID"] = DBNull.Value;
				}
				if (comboBoxLocation.SelectedID != "")
				{
					dataRow["LocationID"] = comboBoxLocation.SelectedID;
				}
				else
				{
					dataRow["LocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultSalesperson.SelectedID != "")
				{
					dataRow["DefaultSalesPersonID"] = comboBoxDefaultSalesperson.SelectedID;
				}
				else
				{
					dataRow["DefaultSalesPersonID"] = DBNull.Value;
				}
				if (comboBoxDefaultInvLocation.SelectedID != "")
				{
					dataRow["DefaultInventoryLocationID"] = comboBoxDefaultInvLocation.SelectedID;
				}
				else
				{
					dataRow["DefaultInventoryLocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultTransLocation.SelectedID != "")
				{
					dataRow["DefaultTransactionLocationID"] = comboBoxDefaultTransLocation.SelectedID;
				}
				else
				{
					dataRow["DefaultTransactionLocationID"] = DBNull.Value;
				}
				if (comboBoxDefaultTransRegister.SelectedID != "")
				{
					dataRow["DefaultTransactionRegisterID"] = comboBoxDefaultTransRegister.SelectedID;
				}
				else
				{
					dataRow["DefaultTransactionRegisterID"] = DBNull.Value;
				}
				dataRow["Phone"] = textBoxPhone.Text;
				dataRow["Email"] = textBoxEmail.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["IsAdmin"] = checkBoxIsAdmin.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.UserTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				textBoxCode.Focus();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.UserSystem.GetUserByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["UserID"].ToString();
				textBoxName.Text = dataRow["UserName"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
				comboBoxDefaultSalesperson.SelectedID = dataRow["DefaultSalesPersonID"].ToString();
				comboBoxDefaultInvLocation.SelectedID = dataRow["DefaultInventoryLocationID"].ToString();
				comboBoxDefaultTransLocation.SelectedID = dataRow["DefaultTransactionLocationID"].ToString();
				comboBoxDefaultTransRegister.SelectedID = dataRow["DefaultTransactionRegisterID"].ToString();
				if (!dataRow["IsCLUser"].IsDBNullOrEmpty())
				{
					checkBoxCLPass.Checked = bool.Parse(dataRow["IsCLUser"].ToString());
				}
				else
				{
					checkBoxCLPass.Checked = false;
				}
				if (!dataRow["CLUserPass"].IsNullOrEmpty())
				{
					textBoxCLPass.Text = Factory.Decrypt(dataRow["CLUserPass"].ToString());
				}
				textBoxPhone.Text = dataRow["Phone"].ToString();
				textBoxEmail.Text = dataRow["Email"].ToString();
				textBoxPassword.Text = "1234567890";
				textBoxConfirmPassword.Text = "1234567890";
				textBoxNote.Text = dataRow["Note"].ToString();
				if (dataRow["Inactive"] != DBNull.Value)
				{
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				}
				else
				{
					checkBoxInactive.Checked = false;
				}
				if (dataRow["IsAdmin"] != DBNull.Value)
				{
					checkBoxIsAdmin.Checked = bool.Parse(dataRow["IsAdmin"].ToString());
				}
				else
				{
					checkBoxIsAdmin.Checked = false;
				}
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = (!isNewRecord) ? Factory.UserSystem.UpdateUser(currentData) : Factory.UserSystem.CreateUser(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.User, needRefresh: true);
					IsNewRecord = true;
					ClearForm();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (textBoxPassword.Text.Trim() == "" && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Creating a user with a blank password is a security threat.", "Are you sure you want to continue?") == DialogResult.No)
			{
				return false;
			}
			if (textBoxPassword.Text != textBoxConfirmPassword.Text)
			{
				ErrorHelper.ErrorMessage("Password does not match with the confirm password.", "Please try again.");
				textBoxConfirmPassword.Clear();
				textBoxPassword.Clear();
				textBoxPassword.Focus();
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Users", "UserID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("User ID already exist. Please enter another user ID.");
				textBoxCode.Focus();
				return false;
			}
			if (isNewRecord && Factory.UserSystem.ExistSQLLogin(textBoxCode.Text) && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "A SQL login ID with this name already exists.", "Click 'Yes' to use this login ID or click 'No' and enter another user ID.") == DialogResult.No)
			{
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxPassword.Clear();
			textBoxConfirmPassword.Clear();
			comboBoxEmployee.Clear();
			comboBoxLocation.Clear();
			textBoxEmployeeName.Clear();
			textBoxEmail.Clear();
			checkBoxCLPass.Checked = false;
			textBoxCLPass.Clear();
			comboBoxDefaultSalesperson.Clear();
			textBoxPhone.Clear();
			comboBoxDefaultTransLocation.Clear();
			comboBoxDefaultInvLocation.Clear();
			comboBoxDefaultTransLocation.Clear();
			comboBoxDefaultTransRegister.Clear();
			textBoxLocationName.Clear();
			textBoxDefaultTransLocation.Clear();
			textBoxDefaultSalesperson.Clear();
			textBoxDefaultInvLocation.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void UserGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void UserGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.UserSystem.DeleteUser(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Users", "UserID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Users", "UserID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Users", "UserID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Users", "UserID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Users", "UserID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void UserDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDefaultTransRegister.ShowDefaultRegisterOnly = false;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.User);
		}

		private void textBoxCode_Validated(object sender, EventArgs e)
		{
			if (textBoxCode.Text.Trim() == "")
			{
				return;
			}
			for (int i = 0; i < textBoxCode.Text.Length; i = checked(i + 1))
			{
				char c = textBoxCode.Text[i];
				if (!char.IsLetterOrDigit(c))
				{
					textBoxCode.Text = textBoxCode.Text.Replace(c, '_');
				}
			}
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
			if (!(textBoxCode.Text.Trim() == "") && !char.IsLetter(textBoxCode.Text[0]))
			{
				ErrorHelper.InformationMessage("User ID must start with letter.");
				e.Cancel = true;
			}
		}

		private void buttonGroups_Click(object sender, EventArgs e)
		{
			UserGroupAssignForm userGroupAssignForm = new UserGroupAssignForm();
			userGroupAssignForm.UserID = textBoxCode.Text;
			userGroupAssignForm.ShowDialog();
		}

		private void buttonAccessRight_Click(object sender, EventArgs e)
		{
			AccessLevelAssignForm accessLevelAssignForm = new AccessLevelAssignForm();
			accessLevelAssignForm.UserID = textBoxCode.Text;
			accessLevelAssignForm.ShowDialog();
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxDefaultSalesperson.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxDefaultInvLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxDefaultTransLocation.SelectedID);
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
		}

		private void comboBoxDefaultSalesperson_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultSalesperson.Text = comboBoxDefaultSalesperson.SelectedName;
		}

		private void comboBoxDefaultInvLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultInvLocation.Text = comboBoxDefaultInvLocation.SelectedName;
		}

		private void comboBoxDefaultTransLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDefaultTransLocation.Text = comboBoxDefaultTransLocation.SelectedName;
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxLocationName.Text = comboBoxLocation.SelectedName;
		}

		private void buttonChangePassword_Click(object sender, EventArgs e)
		{
			ChangePasswordDialog changePasswordDialog = new ChangePasswordDialog();
			changePasswordDialog.UserID = textBoxCode.Text;
			changePasswordDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(comboBoxDefaultTransRegister.SelectedID);
		}

		private void checkBoxCLPass_CheckedChanged(object sender, EventArgs e)
		{
			MMLabel mMLabel = labelCLPass;
			bool visible = textBoxCLPass.Visible = checkBoxCLPass.Checked;
			mMLabel.Visible = visible;
		}

		private void buttonAddLocation_Click(object sender, EventArgs e)
		{
			LocationAssignForm locationAssignForm = new LocationAssignForm();
			locationAssignForm.UserID = textBoxCode.Text;
			locationAssignForm.ShowDialog();
		}

		private void buttonCreateHomeDashboard_Click(object sender, EventArgs e)
		{
			SelectDashboardTemplateDialog selectDashboardTemplateDialog = new SelectDashboardTemplateDialog();
			selectDashboardTemplateDialog.UserID = textBoxCode.Text;
			selectDashboardTemplateDialog.ShowDialog();
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDetailsForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.buttonDelete = new Micromind.UISupport.XPButton();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.buttonNew = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.labelCode = new Micromind.UISupport.MMLabel();
            this.textBoxCode = new Micromind.UISupport.MMTextBox();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.textBoxName = new Micromind.UISupport.MMTextBox();
            this.mmLabel4 = new Micromind.UISupport.MMLabel();
            this.textBoxNote = new Micromind.UISupport.MMTextBox();
            this.formManager = new Micromind.DataControls.FormManager();
            this.checkBoxInactive = new System.Windows.Forms.CheckBox();
            this.comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
            this.textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
            this.comboBoxLocation = new Micromind.DataControls.LocationComboBox();
            this.textBoxPhone = new Micromind.UISupport.MMTextBox();
            this.mmLabel5 = new Micromind.UISupport.MMLabel();
            this.textBoxEmail = new Micromind.UISupport.MMTextBox();
            this.mmLabel6 = new Micromind.UISupport.MMLabel();
            this.ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.buttonGroups = new Micromind.UISupport.XPButton();
            this.textBoxPassword = new Micromind.UISupport.MMTextBox();
            this.textBoxConfirmPassword = new Micromind.UISupport.MMTextBox();
            this.mmLabel2 = new Micromind.UISupport.MMLabel();
            this.mmLabel7 = new Micromind.UISupport.MMLabel();
            this.buttonChangePassword = new Micromind.UISupport.XPButton();
            this.buttonAccessRight = new Micromind.UISupport.XPButton();
            this.ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxDefaultSalesperson = new Micromind.UISupport.MMTextBox();
            this.comboBoxDefaultInvLocation = new Micromind.DataControls.LocationComboBox();
            this.ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxDefaultInvLocation = new Micromind.UISupport.MMTextBox();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.buttonAddLocation = new Micromind.UISupport.XPButton();
            this.ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxDefaultTransRegister = new Micromind.UISupport.MMTextBox();
            this.comboBoxDefaultTransRegister = new Micromind.DataControls.RegisterComboBox();
            this.comboBoxDefaultSalesperson = new Micromind.DataControls.SalespersonComboBox();
            this.comboBoxDefaultTransLocation = new Micromind.DataControls.LocationComboBox();
            this.ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxDefaultTransLocation = new Micromind.UISupport.MMTextBox();
            this.ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxLocationName = new Micromind.UISupport.MMTextBox();
            this.checkBoxIsAdmin = new System.Windows.Forms.CheckBox();
            this.checkBoxCLPass = new System.Windows.Forms.CheckBox();
            this.textBoxCLPass = new Micromind.UISupport.MMTextBox();
            this.labelCLPass = new Micromind.UISupport.MMLabel();
            this.buttonCreateHomeDashboard = new Micromind.UISupport.XPButton();
            this.toolStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultInvLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultTransRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultSalesperson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultTransLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripButtonFirst,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripButtonLast,
            this.toolStripSeparator1,
            this.toolStripButtonOpenList,
            this.toolStripSeparator3,
            this.toolStripTextBoxFind,
            this.toolStripButtonFind,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(598, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonPrint.Image = global::Micromind.ClientUI.Properties.Resources.printer;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
            this.toolStripButtonPrint.Text = "&Print";
            this.toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
            this.toolStripButtonPrint.Visible = false;
            // 
            // toolStripButtonFirst
            // 
            this.toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirst.Image = global::Micromind.ClientUI.Properties.Resources.first;
            this.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirst.Name = "toolStripButtonFirst";
            this.toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonFirst.Text = "First";
            this.toolStripButtonFirst.Click += new System.EventHandler(this.toolStripButtonFirst_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Image = global::Micromind.ClientUI.Properties.Resources.prev;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPrevious.Text = "Previous";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Image = global::Micromind.ClientUI.Properties.Resources.next;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonNext.Text = "Next";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonLast
            // 
            this.toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLast.Image = global::Micromind.ClientUI.Properties.Resources.last;
            this.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLast.Name = "toolStripButtonLast";
            this.toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLast.Text = "Last";
            this.toolStripButtonLast.Click += new System.EventHandler(this.toolStripButtonLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonOpenList
            // 
            this.toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenList.Image = global::Micromind.ClientUI.Properties.Resources.list;
            this.toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenList.Name = "toolStripButtonOpenList";
            this.toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonOpenList.Text = "Open List";
            this.toolStripButtonOpenList.Click += new System.EventHandler(this.toolStripButtonOpenList_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
            this.toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxFind_KeyPress);
            // 
            // toolStripButtonFind
            // 
            this.toolStripButtonFind.Image = global::Micromind.ClientUI.Properties.Resources.find;
            this.toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFind.Name = "toolStripButtonFind";
            this.toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
            this.toolStripButtonFind.Text = "Find";
            this.toolStripButtonFind.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.xpButton1);
            this.panelButtons.Controls.Add(this.buttonNew);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 435);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(598, 40);
            this.panelButtons.TabIndex = 16;
            // 
            // linePanelDown
            // 
            this.linePanelDown.BackColor = System.Drawing.Color.White;
            this.linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.linePanelDown.DrawWidth = 1;
            this.linePanelDown.IsVertical = false;
            this.linePanelDown.LineBackColor = System.Drawing.Color.Silver;
            this.linePanelDown.Location = new System.Drawing.Point(0, 0);
            this.linePanelDown.Name = "linePanelDown";
            this.linePanelDown.Size = new System.Drawing.Size(598, 1);
            this.linePanelDown.TabIndex = 14;
            this.linePanelDown.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonDelete.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDelete.Location = new System.Drawing.Point(216, 8);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(96, 24);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "De&lete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // xpButton1
            // 
            this.xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.xpButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xpButton1.BackColor = System.Drawing.Color.DarkGray;
            this.xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.xpButton1.Location = new System.Drawing.Point(488, 8);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(96, 24);
            this.xpButton1.TabIndex = 3;
            this.xpButton1.Text = "&Close";
            this.xpButton1.UseVisualStyleBackColor = false;
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonNew.BackColor = System.Drawing.Color.DarkGray;
            this.buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNew.Location = new System.Drawing.Point(114, 8);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(96, 24);
            this.buttonNew.TabIndex = 1;
            this.buttonNew.Text = "Ne&w...";
            this.buttonNew.UseVisualStyleBackColor = false;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSave.BackColor = System.Drawing.Color.Silver;
            this.buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSave.Location = new System.Drawing.Point(12, 8);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 24);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCode.IsFieldHeader = false;
            this.labelCode.IsRequired = true;
            this.labelCode.Location = new System.Drawing.Point(8, 38);
            this.labelCode.Name = "labelCode";
            this.labelCode.PenWidth = 1F;
            this.labelCode.ShowBorder = false;
            this.labelCode.Size = new System.Drawing.Size(54, 13);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "User ID:";
            // 
            // textBoxCode
            // 
            this.textBoxCode.BackColor = System.Drawing.Color.White;
            this.textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCode.CustomReportFieldName = "";
            this.textBoxCode.CustomReportKey = "";
            this.textBoxCode.CustomReportValueType = ((byte)(1));
            this.textBoxCode.IsComboTextBox = false;
            this.textBoxCode.IsModified = false;
            this.textBoxCode.Location = new System.Drawing.Point(126, 36);
            this.textBoxCode.MaxLength = 15;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(164, 20);
            this.textBoxCode.TabIndex = 0;
            this.textBoxCode.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCode_Validating);
            this.textBoxCode.Validated += new System.EventHandler(this.textBoxCode_Validated);
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = true;
            this.mmLabel1.Location = new System.Drawing.Point(8, 60);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(73, 13);
            this.mmLabel1.TabIndex = 3;
            this.mmLabel1.Text = "User Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.White;
            this.textBoxName.CustomReportFieldName = "";
            this.textBoxName.CustomReportKey = "";
            this.textBoxName.CustomReportValueType = ((byte)(1));
            this.textBoxName.IsComboTextBox = false;
            this.textBoxName.IsModified = false;
            this.textBoxName.Location = new System.Drawing.Point(126, 58);
            this.textBoxName.MaxLength = 64;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(461, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // mmLabel4
            // 
            this.mmLabel4.AutoSize = true;
            this.mmLabel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel4.IsFieldHeader = false;
            this.mmLabel4.IsRequired = false;
            this.mmLabel4.Location = new System.Drawing.Point(8, 211);
            this.mmLabel4.Name = "mmLabel4";
            this.mmLabel4.PenWidth = 1F;
            this.mmLabel4.ShowBorder = false;
            this.mmLabel4.Size = new System.Drawing.Size(33, 13);
            this.mmLabel4.TabIndex = 9;
            this.mmLabel4.Text = "Note:";
            // 
            // textBoxNote
            // 
            this.textBoxNote.BackColor = System.Drawing.Color.White;
            this.textBoxNote.CustomReportFieldName = "";
            this.textBoxNote.CustomReportKey = "";
            this.textBoxNote.CustomReportValueType = ((byte)(1));
            this.textBoxNote.IsComboTextBox = false;
            this.textBoxNote.IsModified = false;
            this.textBoxNote.Location = new System.Drawing.Point(126, 208);
            this.textBoxNote.MaxLength = 255;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(458, 20);
            this.textBoxNote.TabIndex = 11;
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.Dock = System.Windows.Forms.DockStyle.Left;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 31);
            this.formManager.MaximumSize = new System.Drawing.Size(20, 20);
            this.formManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(20, 20);
            this.formManager.TabIndex = 16;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            // 
            // checkBoxInactive
            // 
            this.checkBoxInactive.AutoSize = true;
            this.checkBoxInactive.Location = new System.Drawing.Point(293, 38);
            this.checkBoxInactive.Name = "checkBoxInactive";
            this.checkBoxInactive.Size = new System.Drawing.Size(64, 17);
            this.checkBoxInactive.TabIndex = 1;
            this.checkBoxInactive.Text = "Inactive";
            this.checkBoxInactive.UseVisualStyleBackColor = true;
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.Assigned = false;
            this.comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxEmployee.CustomReportFieldName = "";
            this.comboBoxEmployee.CustomReportKey = "";
            this.comboBoxEmployee.CustomReportValueType = ((byte)(1));
            this.comboBoxEmployee.DescriptionTextBox = null;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxEmployee.DisplayLayout.Appearance = appearance1;
            this.comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance8;
            this.comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance11;
            this.comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxEmployee.Editable = true;
            this.comboBoxEmployee.FilterString = "";
            this.comboBoxEmployee.HasAllAccount = false;
            this.comboBoxEmployee.HasCustom = false;
            this.comboBoxEmployee.IsDataLoaded = false;
            this.comboBoxEmployee.Location = new System.Drawing.Point(126, 142);
            this.comboBoxEmployee.MaxDropDownItems = 12;
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.ShowInactiveItems = false;
            this.comboBoxEmployee.ShowQuickAdd = true;
            this.comboBoxEmployee.ShowTerminatedEmployees = true;
            this.comboBoxEmployee.Size = new System.Drawing.Size(164, 20);
            this.comboBoxEmployee.TabIndex = 6;
            this.comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(this.comboBoxEmployee_SelectedIndexChanged);
            // 
            // textBoxEmployeeName
            // 
            this.textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxEmployeeName.CustomReportFieldName = "";
            this.textBoxEmployeeName.CustomReportKey = "";
            this.textBoxEmployeeName.CustomReportValueType = ((byte)(1));
            this.textBoxEmployeeName.ForeColor = System.Drawing.Color.Black;
            this.textBoxEmployeeName.IsComboTextBox = false;
            this.textBoxEmployeeName.IsModified = false;
            this.textBoxEmployeeName.Location = new System.Drawing.Point(293, 142);
            this.textBoxEmployeeName.MaxLength = 64;
            this.textBoxEmployeeName.Name = "textBoxEmployeeName";
            this.textBoxEmployeeName.ReadOnly = true;
            this.textBoxEmployeeName.Size = new System.Drawing.Size(291, 20);
            this.textBoxEmployeeName.TabIndex = 7;
            this.textBoxEmployeeName.TabStop = false;
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.Assigned = false;
            this.comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxLocation.CustomReportFieldName = "";
            this.comboBoxLocation.CustomReportKey = "";
            this.comboBoxLocation.CustomReportValueType = ((byte)(1));
            this.comboBoxLocation.DescriptionTextBox = null;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxLocation.DisplayLayout.Appearance = appearance13;
            this.comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance20;
            this.comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance23;
            this.comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxLocation.Editable = true;
            this.comboBoxLocation.FilterString = "";
            this.comboBoxLocation.HasAllAccount = false;
            this.comboBoxLocation.HasCustom = false;
            this.comboBoxLocation.IsDataLoaded = false;
            this.comboBoxLocation.Location = new System.Drawing.Point(126, 164);
            this.comboBoxLocation.MaxDropDownItems = 12;
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.ShowAll = false;
            this.comboBoxLocation.ShowConsignIn = false;
            this.comboBoxLocation.ShowConsignOut = false;
            this.comboBoxLocation.ShowDefaultLocationOnly = false;
            this.comboBoxLocation.ShowInactiveItems = false;
            this.comboBoxLocation.ShowNormalLocations = true;
            this.comboBoxLocation.ShowPOSOnly = false;
            this.comboBoxLocation.ShowQuickAdd = true;
            this.comboBoxLocation.ShowWarehouseOnly = false;
            this.comboBoxLocation.Size = new System.Drawing.Size(164, 20);
            this.comboBoxLocation.TabIndex = 8;
            this.comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.BackColor = System.Drawing.Color.White;
            this.textBoxPhone.CustomReportFieldName = "";
            this.textBoxPhone.CustomReportKey = "";
            this.textBoxPhone.CustomReportValueType = ((byte)(1));
            this.textBoxPhone.IsComboTextBox = false;
            this.textBoxPhone.IsModified = false;
            this.textBoxPhone.Location = new System.Drawing.Point(126, 186);
            this.textBoxPhone.MaxLength = 255;
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(164, 20);
            this.textBoxPhone.TabIndex = 9;
            // 
            // mmLabel5
            // 
            this.mmLabel5.AutoSize = true;
            this.mmLabel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel5.IsFieldHeader = false;
            this.mmLabel5.IsRequired = false;
            this.mmLabel5.Location = new System.Drawing.Point(8, 190);
            this.mmLabel5.Name = "mmLabel5";
            this.mmLabel5.PenWidth = 1F;
            this.mmLabel5.ShowBorder = false;
            this.mmLabel5.Size = new System.Drawing.Size(41, 13);
            this.mmLabel5.TabIndex = 21;
            this.mmLabel5.Text = "Phone:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.Color.White;
            this.textBoxEmail.CustomReportFieldName = "";
            this.textBoxEmail.CustomReportKey = "";
            this.textBoxEmail.CustomReportValueType = ((byte)(1));
            this.textBoxEmail.IsComboTextBox = false;
            this.textBoxEmail.IsModified = false;
            this.textBoxEmail.Location = new System.Drawing.Point(340, 186);
            this.textBoxEmail.MaxLength = 255;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(244, 20);
            this.textBoxEmail.TabIndex = 10;
            // 
            // mmLabel6
            // 
            this.mmLabel6.AutoSize = true;
            this.mmLabel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel6.IsFieldHeader = false;
            this.mmLabel6.IsRequired = false;
            this.mmLabel6.Location = new System.Drawing.Point(299, 189);
            this.mmLabel6.Name = "mmLabel6";
            this.mmLabel6.PenWidth = 1F;
            this.mmLabel6.ShowBorder = false;
            this.mmLabel6.Size = new System.Drawing.Size(35, 13);
            this.mmLabel6.TabIndex = 23;
            this.mmLabel6.Text = "Email:";
            // 
            // ultraFormattedLinkLabel3
            // 
            appearance25.FontData.BoldAsString = "False";
            appearance25.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel3.Appearance = appearance25;
            this.ultraFormattedLinkLabel3.AutoSize = true;
            this.ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 143);
            this.ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
            this.ultraFormattedLinkLabel3.Size = new System.Drawing.Size(56, 15);
            this.ultraFormattedLinkLabel3.TabIndex = 138;
            this.ultraFormattedLinkLabel3.TabStop = true;
            this.ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel3.Value = "Employee:";
            appearance26.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance26;
            this.ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel3_LinkClicked);
            // 
            // buttonGroups
            // 
            this.buttonGroups.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonGroups.BackColor = System.Drawing.Color.DarkGray;
            this.buttonGroups.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonGroups.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonGroups.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGroups.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonGroups.Location = new System.Drawing.Point(126, 405);
            this.buttonGroups.Name = "buttonGroups";
            this.buttonGroups.Size = new System.Drawing.Size(109, 24);
            this.buttonGroups.TabIndex = 14;
            this.buttonGroups.Text = "User Groups...";
            this.buttonGroups.UseVisualStyleBackColor = false;
            this.buttonGroups.Click += new System.EventHandler(this.buttonGroups_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxPassword.CustomReportFieldName = "";
            this.textBoxPassword.CustomReportKey = "";
            this.textBoxPassword.CustomReportValueType = ((byte)(1));
            this.textBoxPassword.ForeColor = System.Drawing.Color.Black;
            this.textBoxPassword.IsComboTextBox = false;
            this.textBoxPassword.IsModified = false;
            this.textBoxPassword.Location = new System.Drawing.Point(126, 80);
            this.textBoxPassword.MaxLength = 64;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(164, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxConfirmPassword
            // 
            this.textBoxConfirmPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxConfirmPassword.CustomReportFieldName = "";
            this.textBoxConfirmPassword.CustomReportKey = "";
            this.textBoxConfirmPassword.CustomReportValueType = ((byte)(1));
            this.textBoxConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.textBoxConfirmPassword.IsComboTextBox = false;
            this.textBoxConfirmPassword.IsModified = false;
            this.textBoxConfirmPassword.Location = new System.Drawing.Point(126, 102);
            this.textBoxConfirmPassword.MaxLength = 64;
            this.textBoxConfirmPassword.Name = "textBoxConfirmPassword";
            this.textBoxConfirmPassword.Size = new System.Drawing.Size(164, 20);
            this.textBoxConfirmPassword.TabIndex = 4;
            this.textBoxConfirmPassword.UseSystemPasswordChar = true;
            // 
            // mmLabel2
            // 
            this.mmLabel2.AutoSize = true;
            this.mmLabel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel2.IsFieldHeader = false;
            this.mmLabel2.IsRequired = false;
            this.mmLabel2.Location = new System.Drawing.Point(8, 83);
            this.mmLabel2.Name = "mmLabel2";
            this.mmLabel2.PenWidth = 1F;
            this.mmLabel2.ShowBorder = false;
            this.mmLabel2.Size = new System.Drawing.Size(56, 13);
            this.mmLabel2.TabIndex = 18;
            this.mmLabel2.Text = "Password:";
            // 
            // mmLabel7
            // 
            this.mmLabel7.AutoSize = true;
            this.mmLabel7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mmLabel7.IsFieldHeader = false;
            this.mmLabel7.IsRequired = false;
            this.mmLabel7.Location = new System.Drawing.Point(8, 105);
            this.mmLabel7.Name = "mmLabel7";
            this.mmLabel7.PenWidth = 1F;
            this.mmLabel7.ShowBorder = false;
            this.mmLabel7.Size = new System.Drawing.Size(94, 13);
            this.mmLabel7.TabIndex = 18;
            this.mmLabel7.Text = "Confirm Password:";
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonChangePassword.BackColor = System.Drawing.Color.DarkGray;
            this.buttonChangePassword.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonChangePassword.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChangePassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonChangePassword.Location = new System.Drawing.Point(293, 79);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(114, 23);
            this.buttonChangePassword.TabIndex = 5;
            this.buttonChangePassword.Text = "Change Password...";
            this.buttonChangePassword.UseVisualStyleBackColor = false;
            this.buttonChangePassword.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // 
            // buttonAccessRight
            // 
            this.buttonAccessRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonAccessRight.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAccessRight.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonAccessRight.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonAccessRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAccessRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAccessRight.Location = new System.Drawing.Point(241, 405);
            this.buttonAccessRight.Name = "buttonAccessRight";
            this.buttonAccessRight.Size = new System.Drawing.Size(109, 24);
            this.buttonAccessRight.TabIndex = 15;
            this.buttonAccessRight.Text = "Access Rights...";
            this.buttonAccessRight.UseVisualStyleBackColor = false;
            this.buttonAccessRight.Click += new System.EventHandler(this.buttonAccessRight_Click);
            // 
            // ultraFormattedLinkLabel1
            // 
            appearance27.FontData.BoldAsString = "False";
            appearance27.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel1.Appearance = appearance27;
            this.ultraFormattedLinkLabel1.AutoSize = true;
            this.ultraFormattedLinkLabel1.Location = new System.Drawing.Point(4, 19);
            this.ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
            this.ultraFormattedLinkLabel1.Size = new System.Drawing.Size(68, 15);
            this.ultraFormattedLinkLabel1.TabIndex = 141;
            this.ultraFormattedLinkLabel1.TabStop = true;
            this.ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel1.Value = "Salesperson:";
            appearance28.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance28;
            this.ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel1_LinkClicked);
            // 
            // textBoxDefaultSalesperson
            // 
            this.textBoxDefaultSalesperson.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDefaultSalesperson.CustomReportFieldName = "";
            this.textBoxDefaultSalesperson.CustomReportKey = "";
            this.textBoxDefaultSalesperson.CustomReportValueType = ((byte)(1));
            this.textBoxDefaultSalesperson.ForeColor = System.Drawing.Color.Black;
            this.textBoxDefaultSalesperson.IsComboTextBox = false;
            this.textBoxDefaultSalesperson.IsModified = false;
            this.textBoxDefaultSalesperson.Location = new System.Drawing.Point(286, 16);
            this.textBoxDefaultSalesperson.MaxLength = 64;
            this.textBoxDefaultSalesperson.Name = "textBoxDefaultSalesperson";
            this.textBoxDefaultSalesperson.ReadOnly = true;
            this.textBoxDefaultSalesperson.Size = new System.Drawing.Size(291, 20);
            this.textBoxDefaultSalesperson.TabIndex = 1;
            this.textBoxDefaultSalesperson.TabStop = false;
            // 
            // comboBoxDefaultInvLocation
            // 
            this.comboBoxDefaultInvLocation.Assigned = false;
            this.comboBoxDefaultInvLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxDefaultInvLocation.CustomReportFieldName = "";
            this.comboBoxDefaultInvLocation.CustomReportKey = "";
            this.comboBoxDefaultInvLocation.CustomReportValueType = ((byte)(1));
            this.comboBoxDefaultInvLocation.DescriptionTextBox = null;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxDefaultInvLocation.DisplayLayout.Appearance = appearance29;
            this.comboBoxDefaultInvLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxDefaultInvLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance30.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.Appearance = appearance30;
            appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
            this.comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance32.BackColor2 = System.Drawing.SystemColors.Control;
            appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultInvLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
            this.comboBoxDefaultInvLocation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxDefaultInvLocation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.ActiveCellAppearance = appearance33;
            appearance34.BackColor = System.Drawing.SystemColors.Highlight;
            appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.ActiveRowAppearance = appearance34;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.CardAreaAppearance = appearance35;
            appearance36.BorderColor = System.Drawing.Color.Silver;
            appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.CellAppearance = appearance36;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.CellPadding = 0;
            appearance37.BackColor = System.Drawing.SystemColors.Control;
            appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance37.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.GroupByRowAppearance = appearance37;
            appearance38.TextHAlignAsString = "Left";
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderAppearance = appearance38;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance39.BackColor = System.Drawing.SystemColors.Window;
            appearance39.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.RowAppearance = appearance39;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxDefaultInvLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
            this.comboBoxDefaultInvLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxDefaultInvLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxDefaultInvLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxDefaultInvLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxDefaultInvLocation.Editable = true;
            this.comboBoxDefaultInvLocation.FilterString = "";
            this.comboBoxDefaultInvLocation.HasAllAccount = false;
            this.comboBoxDefaultInvLocation.HasCustom = false;
            this.comboBoxDefaultInvLocation.IsDataLoaded = false;
            this.comboBoxDefaultInvLocation.Location = new System.Drawing.Point(120, 38);
            this.comboBoxDefaultInvLocation.MaxDropDownItems = 12;
            this.comboBoxDefaultInvLocation.Name = "comboBoxDefaultInvLocation";
            this.comboBoxDefaultInvLocation.ShowAll = false;
            this.comboBoxDefaultInvLocation.ShowConsignIn = false;
            this.comboBoxDefaultInvLocation.ShowConsignOut = false;
            this.comboBoxDefaultInvLocation.ShowDefaultLocationOnly = false;
            this.comboBoxDefaultInvLocation.ShowInactiveItems = false;
            this.comboBoxDefaultInvLocation.ShowNormalLocations = true;
            this.comboBoxDefaultInvLocation.ShowPOSOnly = false;
            this.comboBoxDefaultInvLocation.ShowQuickAdd = true;
            this.comboBoxDefaultInvLocation.ShowWarehouseOnly = false;
            this.comboBoxDefaultInvLocation.Size = new System.Drawing.Size(163, 20);
            this.comboBoxDefaultInvLocation.TabIndex = 2;
            this.comboBoxDefaultInvLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxDefaultInvLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultInvLocation_SelectedIndexChanged);
            // 
            // ultraFormattedLinkLabel2
            // 
            appearance41.FontData.BoldAsString = "False";
            appearance41.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel2.Appearance = appearance41;
            this.ultraFormattedLinkLabel2.AutoSize = true;
            this.ultraFormattedLinkLabel2.Location = new System.Drawing.Point(4, 39);
            this.ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
            this.ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
            this.ultraFormattedLinkLabel2.TabIndex = 141;
            this.ultraFormattedLinkLabel2.TabStop = true;
            this.ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel2.Value = "Inventory Location:";
            appearance42.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance42;
            this.ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel2_LinkClicked);
            // 
            // textBoxDefaultInvLocation
            // 
            this.textBoxDefaultInvLocation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDefaultInvLocation.CustomReportFieldName = "";
            this.textBoxDefaultInvLocation.CustomReportKey = "";
            this.textBoxDefaultInvLocation.CustomReportValueType = ((byte)(1));
            this.textBoxDefaultInvLocation.ForeColor = System.Drawing.Color.Black;
            this.textBoxDefaultInvLocation.IsComboTextBox = false;
            this.textBoxDefaultInvLocation.IsModified = false;
            this.textBoxDefaultInvLocation.Location = new System.Drawing.Point(286, 38);
            this.textBoxDefaultInvLocation.MaxLength = 64;
            this.textBoxDefaultInvLocation.Name = "textBoxDefaultInvLocation";
            this.textBoxDefaultInvLocation.ReadOnly = true;
            this.textBoxDefaultInvLocation.Size = new System.Drawing.Size(200, 20);
            this.textBoxDefaultInvLocation.TabIndex = 3;
            this.textBoxDefaultInvLocation.TabStop = false;
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.ultraGroupBox3.Controls.Add(this.buttonAddLocation);
            this.ultraGroupBox3.Controls.Add(this.ultraFormattedLinkLabel6);
            this.ultraGroupBox3.Controls.Add(this.textBoxDefaultTransRegister);
            this.ultraGroupBox3.Controls.Add(this.comboBoxDefaultTransRegister);
            this.ultraGroupBox3.Controls.Add(this.comboBoxDefaultSalesperson);
            this.ultraGroupBox3.Controls.Add(this.ultraFormattedLinkLabel1);
            this.ultraGroupBox3.Controls.Add(this.comboBoxDefaultTransLocation);
            this.ultraGroupBox3.Controls.Add(this.comboBoxDefaultInvLocation);
            this.ultraGroupBox3.Controls.Add(this.ultraFormattedLinkLabel4);
            this.ultraGroupBox3.Controls.Add(this.textBoxDefaultSalesperson);
            this.ultraGroupBox3.Controls.Add(this.textBoxDefaultTransLocation);
            this.ultraGroupBox3.Controls.Add(this.ultraFormattedLinkLabel2);
            this.ultraGroupBox3.Controls.Add(this.textBoxDefaultInvLocation);
            this.ultraGroupBox3.Location = new System.Drawing.Point(6, 268);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(577, 106);
            this.ultraGroupBox3.TabIndex = 12;
            this.ultraGroupBox3.Text = "Defaults";
            // 
            // buttonAddLocation
            // 
            this.buttonAddLocation.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonAddLocation.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddLocation.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonAddLocation.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonAddLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddLocation.Location = new System.Drawing.Point(487, 38);
            this.buttonAddLocation.Name = "buttonAddLocation";
            this.buttonAddLocation.Size = new System.Drawing.Size(90, 20);
            this.buttonAddLocation.TabIndex = 148;
            this.buttonAddLocation.Text = "Add Location";
            this.buttonAddLocation.UseVisualStyleBackColor = false;
            this.buttonAddLocation.Click += new System.EventHandler(this.buttonAddLocation_Click);
            // 
            // ultraFormattedLinkLabel6
            // 
            appearance43.FontData.BoldAsString = "False";
            appearance43.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel6.Appearance = appearance43;
            this.ultraFormattedLinkLabel6.AutoSize = true;
            this.ultraFormattedLinkLabel6.Location = new System.Drawing.Point(4, 84);
            this.ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
            this.ultraFormattedLinkLabel6.Size = new System.Drawing.Size(109, 15);
            this.ultraFormattedLinkLabel6.TabIndex = 145;
            this.ultraFormattedLinkLabel6.TabStop = true;
            this.ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel6.Value = "Transaction Register:";
            appearance44.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance44;
            this.ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel6_LinkClicked);
            // 
            // textBoxDefaultTransRegister
            // 
            this.textBoxDefaultTransRegister.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDefaultTransRegister.CustomReportFieldName = "";
            this.textBoxDefaultTransRegister.CustomReportKey = "";
            this.textBoxDefaultTransRegister.CustomReportValueType = ((byte)(1));
            this.textBoxDefaultTransRegister.ForeColor = System.Drawing.Color.Black;
            this.textBoxDefaultTransRegister.IsComboTextBox = false;
            this.textBoxDefaultTransRegister.IsModified = false;
            this.textBoxDefaultTransRegister.Location = new System.Drawing.Point(286, 82);
            this.textBoxDefaultTransRegister.MaxLength = 64;
            this.textBoxDefaultTransRegister.Name = "textBoxDefaultTransRegister";
            this.textBoxDefaultTransRegister.ReadOnly = true;
            this.textBoxDefaultTransRegister.Size = new System.Drawing.Size(291, 20);
            this.textBoxDefaultTransRegister.TabIndex = 7;
            this.textBoxDefaultTransRegister.TabStop = false;
            // 
            // comboBoxDefaultTransRegister
            // 
            this.comboBoxDefaultTransRegister.Assigned = false;
            this.comboBoxDefaultTransRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxDefaultTransRegister.CustomReportFieldName = "";
            this.comboBoxDefaultTransRegister.CustomReportKey = "";
            this.comboBoxDefaultTransRegister.CustomReportValueType = ((byte)(1));
            this.comboBoxDefaultTransRegister.DescriptionTextBox = this.textBoxDefaultTransRegister;
            appearance45.BackColor = System.Drawing.SystemColors.Window;
            appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxDefaultTransRegister.DisplayLayout.Appearance = appearance45;
            this.comboBoxDefaultTransRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxDefaultTransRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance46.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.Appearance = appearance46;
            appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
            this.comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance48.BackColor2 = System.Drawing.SystemColors.Control;
            appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultTransRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
            this.comboBoxDefaultTransRegister.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxDefaultTransRegister.DisplayLayout.MaxRowScrollRegions = 1;
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.ActiveCellAppearance = appearance49;
            appearance50.BackColor = System.Drawing.SystemColors.Highlight;
            appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.ActiveRowAppearance = appearance50;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance51.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.CardAreaAppearance = appearance51;
            appearance52.BorderColor = System.Drawing.Color.Silver;
            appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.CellAppearance = appearance52;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.CellPadding = 0;
            appearance53.BackColor = System.Drawing.SystemColors.Control;
            appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance53.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.GroupByRowAppearance = appearance53;
            appearance54.TextHAlignAsString = "Left";
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderAppearance = appearance54;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.RowAppearance = appearance55;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxDefaultTransRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
            this.comboBoxDefaultTransRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxDefaultTransRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxDefaultTransRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxDefaultTransRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxDefaultTransRegister.Editable = true;
            this.comboBoxDefaultTransRegister.FilterString = "";
            this.comboBoxDefaultTransRegister.HasAllAccount = false;
            this.comboBoxDefaultTransRegister.HasCustom = false;
            this.comboBoxDefaultTransRegister.IsDataLoaded = false;
            this.comboBoxDefaultTransRegister.Location = new System.Drawing.Point(120, 82);
            this.comboBoxDefaultTransRegister.MaxDropDownItems = 12;
            this.comboBoxDefaultTransRegister.Name = "comboBoxDefaultTransRegister";
            this.comboBoxDefaultTransRegister.ShowDefaultRegisterOnly = false;
            this.comboBoxDefaultTransRegister.ShowInactiveItems = false;
            this.comboBoxDefaultTransRegister.ShowQuickAdd = true;
            this.comboBoxDefaultTransRegister.Size = new System.Drawing.Size(163, 20);
            this.comboBoxDefaultTransRegister.TabIndex = 6;
            this.comboBoxDefaultTransRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxDefaultSalesperson
            // 
            this.comboBoxDefaultSalesperson.Assigned = false;
            this.comboBoxDefaultSalesperson.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxDefaultSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxDefaultSalesperson.CustomReportFieldName = "";
            this.comboBoxDefaultSalesperson.CustomReportKey = "";
            this.comboBoxDefaultSalesperson.CustomReportValueType = ((byte)(1));
            this.comboBoxDefaultSalesperson.DescriptionTextBox = null;
            appearance57.BackColor = System.Drawing.SystemColors.Window;
            appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxDefaultSalesperson.DisplayLayout.Appearance = appearance57;
            this.comboBoxDefaultSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxDefaultSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance58.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.Appearance = appearance58;
            appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
            this.comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance60.BackColor2 = System.Drawing.SystemColors.Control;
            appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
            this.comboBoxDefaultSalesperson.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxDefaultSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance61;
            appearance62.BackColor = System.Drawing.SystemColors.Highlight;
            appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance62;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance63.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance63;
            appearance64.BorderColor = System.Drawing.Color.Silver;
            appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.CellAppearance = appearance64;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.CellPadding = 0;
            appearance65.BackColor = System.Drawing.SystemColors.Control;
            appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance65.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance65;
            appearance66.TextHAlignAsString = "Left";
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderAppearance = appearance66;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance67.BackColor = System.Drawing.SystemColors.Window;
            appearance67.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.RowAppearance = appearance67;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxDefaultSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
            this.comboBoxDefaultSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxDefaultSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxDefaultSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxDefaultSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxDefaultSalesperson.Editable = true;
            this.comboBoxDefaultSalesperson.FilterString = "";
            this.comboBoxDefaultSalesperson.HasAllAccount = false;
            this.comboBoxDefaultSalesperson.HasCustom = false;
            this.comboBoxDefaultSalesperson.IsDataLoaded = false;
            this.comboBoxDefaultSalesperson.Location = new System.Drawing.Point(120, 16);
            this.comboBoxDefaultSalesperson.MaxDropDownItems = 12;
            this.comboBoxDefaultSalesperson.Name = "comboBoxDefaultSalesperson";
            this.comboBoxDefaultSalesperson.ShowInactiveItems = false;
            this.comboBoxDefaultSalesperson.ShowQuickAdd = true;
            this.comboBoxDefaultSalesperson.Size = new System.Drawing.Size(163, 20);
            this.comboBoxDefaultSalesperson.TabIndex = 0;
            this.comboBoxDefaultSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxDefaultSalesperson.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultSalesperson_SelectedIndexChanged);
            // 
            // comboBoxDefaultTransLocation
            // 
            this.comboBoxDefaultTransLocation.Assigned = false;
            this.comboBoxDefaultTransLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxDefaultTransLocation.CustomReportFieldName = "";
            this.comboBoxDefaultTransLocation.CustomReportKey = "";
            this.comboBoxDefaultTransLocation.CustomReportValueType = ((byte)(1));
            this.comboBoxDefaultTransLocation.DescriptionTextBox = null;
            appearance69.BackColor = System.Drawing.SystemColors.Window;
            appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxDefaultTransLocation.DisplayLayout.Appearance = appearance69;
            this.comboBoxDefaultTransLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxDefaultTransLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance70.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.Appearance = appearance70;
            appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
            this.comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance72.BackColor2 = System.Drawing.SystemColors.Control;
            appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDefaultTransLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
            this.comboBoxDefaultTransLocation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxDefaultTransLocation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance73.BackColor = System.Drawing.SystemColors.Window;
            appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.ActiveCellAppearance = appearance73;
            appearance74.BackColor = System.Drawing.SystemColors.Highlight;
            appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.ActiveRowAppearance = appearance74;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance75.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.CardAreaAppearance = appearance75;
            appearance76.BorderColor = System.Drawing.Color.Silver;
            appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.CellAppearance = appearance76;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.CellPadding = 0;
            appearance77.BackColor = System.Drawing.SystemColors.Control;
            appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance77.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.GroupByRowAppearance = appearance77;
            appearance78.TextHAlignAsString = "Left";
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderAppearance = appearance78;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance79.BackColor = System.Drawing.SystemColors.Window;
            appearance79.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.RowAppearance = appearance79;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxDefaultTransLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
            this.comboBoxDefaultTransLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxDefaultTransLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxDefaultTransLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxDefaultTransLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxDefaultTransLocation.Editable = true;
            this.comboBoxDefaultTransLocation.FilterString = "";
            this.comboBoxDefaultTransLocation.HasAllAccount = false;
            this.comboBoxDefaultTransLocation.HasCustom = false;
            this.comboBoxDefaultTransLocation.IsDataLoaded = false;
            this.comboBoxDefaultTransLocation.Location = new System.Drawing.Point(120, 60);
            this.comboBoxDefaultTransLocation.MaxDropDownItems = 12;
            this.comboBoxDefaultTransLocation.Name = "comboBoxDefaultTransLocation";
            this.comboBoxDefaultTransLocation.ShowAll = false;
            this.comboBoxDefaultTransLocation.ShowConsignIn = false;
            this.comboBoxDefaultTransLocation.ShowConsignOut = false;
            this.comboBoxDefaultTransLocation.ShowDefaultLocationOnly = false;
            this.comboBoxDefaultTransLocation.ShowInactiveItems = false;
            this.comboBoxDefaultTransLocation.ShowNormalLocations = true;
            this.comboBoxDefaultTransLocation.ShowPOSOnly = false;
            this.comboBoxDefaultTransLocation.ShowQuickAdd = true;
            this.comboBoxDefaultTransLocation.ShowWarehouseOnly = false;
            this.comboBoxDefaultTransLocation.Size = new System.Drawing.Size(163, 20);
            this.comboBoxDefaultTransLocation.TabIndex = 4;
            this.comboBoxDefaultTransLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxDefaultTransLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultTransLocation_SelectedIndexChanged);
            // 
            // ultraFormattedLinkLabel4
            // 
            appearance81.FontData.BoldAsString = "False";
            appearance81.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel4.Appearance = appearance81;
            this.ultraFormattedLinkLabel4.AutoSize = true;
            this.ultraFormattedLinkLabel4.Location = new System.Drawing.Point(4, 61);
            this.ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
            this.ultraFormattedLinkLabel4.Size = new System.Drawing.Size(109, 15);
            this.ultraFormattedLinkLabel4.TabIndex = 141;
            this.ultraFormattedLinkLabel4.TabStop = true;
            this.ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel4.Value = "Transaction Location:";
            appearance82.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance82;
            this.ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel4_LinkClicked);
            // 
            // textBoxDefaultTransLocation
            // 
            this.textBoxDefaultTransLocation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDefaultTransLocation.CustomReportFieldName = "";
            this.textBoxDefaultTransLocation.CustomReportKey = "";
            this.textBoxDefaultTransLocation.CustomReportValueType = ((byte)(1));
            this.textBoxDefaultTransLocation.ForeColor = System.Drawing.Color.Black;
            this.textBoxDefaultTransLocation.IsComboTextBox = false;
            this.textBoxDefaultTransLocation.IsModified = false;
            this.textBoxDefaultTransLocation.Location = new System.Drawing.Point(286, 60);
            this.textBoxDefaultTransLocation.MaxLength = 64;
            this.textBoxDefaultTransLocation.Name = "textBoxDefaultTransLocation";
            this.textBoxDefaultTransLocation.ReadOnly = true;
            this.textBoxDefaultTransLocation.Size = new System.Drawing.Size(291, 20);
            this.textBoxDefaultTransLocation.TabIndex = 5;
            this.textBoxDefaultTransLocation.TabStop = false;
            // 
            // ultraFormattedLinkLabel5
            // 
            appearance83.FontData.BoldAsString = "False";
            appearance83.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel5.Appearance = appearance83;
            this.ultraFormattedLinkLabel5.AutoSize = true;
            this.ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 164);
            this.ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
            this.ultraFormattedLinkLabel5.Size = new System.Drawing.Size(50, 15);
            this.ultraFormattedLinkLabel5.TabIndex = 144;
            this.ultraFormattedLinkLabel5.TabStop = true;
            this.ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel5.Value = "Location:";
            appearance84.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance84;
            this.ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel5_LinkClicked);
            // 
            // textBoxLocationName
            // 
            this.textBoxLocationName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxLocationName.CustomReportFieldName = "";
            this.textBoxLocationName.CustomReportKey = "";
            this.textBoxLocationName.CustomReportValueType = ((byte)(1));
            this.textBoxLocationName.ForeColor = System.Drawing.Color.Black;
            this.textBoxLocationName.IsComboTextBox = false;
            this.textBoxLocationName.IsModified = false;
            this.textBoxLocationName.Location = new System.Drawing.Point(293, 164);
            this.textBoxLocationName.MaxLength = 64;
            this.textBoxLocationName.Name = "textBoxLocationName";
            this.textBoxLocationName.ReadOnly = true;
            this.textBoxLocationName.Size = new System.Drawing.Size(290, 20);
            this.textBoxLocationName.TabIndex = 7;
            this.textBoxLocationName.TabStop = false;
            // 
            // checkBoxIsAdmin
            // 
            this.checkBoxIsAdmin.AutoSize = true;
            this.checkBoxIsAdmin.Location = new System.Drawing.Point(126, 382);
            this.checkBoxIsAdmin.Name = "checkBoxIsAdmin";
            this.checkBoxIsAdmin.Size = new System.Drawing.Size(153, 17);
            this.checkBoxIsAdmin.TabIndex = 13;
            this.checkBoxIsAdmin.Text = "User is Admin (Full Access)";
            this.checkBoxIsAdmin.UseVisualStyleBackColor = true;
            // 
            // checkBoxCLPass
            // 
            this.checkBoxCLPass.AutoSize = true;
            this.checkBoxCLPass.Location = new System.Drawing.Point(126, 234);
            this.checkBoxCLPass.Name = "checkBoxCLPass";
            this.checkBoxCLPass.Size = new System.Drawing.Size(247, 17);
            this.checkBoxCLPass.TabIndex = 145;
            this.checkBoxCLPass.Text = "Can approve over credit limit sales transactions";
            this.checkBoxCLPass.UseVisualStyleBackColor = true;
            this.checkBoxCLPass.CheckedChanged += new System.EventHandler(this.checkBoxCLPass_CheckedChanged);
            // 
            // textBoxCLPass
            // 
            this.textBoxCLPass.BackColor = System.Drawing.Color.White;
            this.textBoxCLPass.CustomReportFieldName = "";
            this.textBoxCLPass.CustomReportKey = "";
            this.textBoxCLPass.CustomReportValueType = ((byte)(1));
            this.textBoxCLPass.ForeColor = System.Drawing.Color.Black;
            this.textBoxCLPass.IsComboTextBox = false;
            this.textBoxCLPass.IsModified = false;
            this.textBoxCLPass.Location = new System.Drawing.Point(461, 232);
            this.textBoxCLPass.MaxLength = 10;
            this.textBoxCLPass.Name = "textBoxCLPass";
            this.textBoxCLPass.Size = new System.Drawing.Size(122, 20);
            this.textBoxCLPass.TabIndex = 146;
            this.textBoxCLPass.UseSystemPasswordChar = true;
            this.textBoxCLPass.Visible = false;
            // 
            // labelCLPass
            // 
            this.labelCLPass.AutoSize = true;
            this.labelCLPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCLPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelCLPass.IsFieldHeader = false;
            this.labelCLPass.IsRequired = false;
            this.labelCLPass.Location = new System.Drawing.Point(399, 234);
            this.labelCLPass.Name = "labelCLPass";
            this.labelCLPass.PenWidth = 1F;
            this.labelCLPass.ShowBorder = false;
            this.labelCLPass.Size = new System.Drawing.Size(56, 13);
            this.labelCLPass.TabIndex = 147;
            this.labelCLPass.Text = "Password:";
            this.labelCLPass.Visible = false;
            // 
            // buttonCreateHomeDashboard
            // 
            this.buttonCreateHomeDashboard.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCreateHomeDashboard.BackColor = System.Drawing.Color.DarkGray;
            this.buttonCreateHomeDashboard.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCreateHomeDashboard.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCreateHomeDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreateHomeDashboard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCreateHomeDashboard.Location = new System.Drawing.Point(356, 405);
            this.buttonCreateHomeDashboard.Name = "buttonCreateHomeDashboard";
            this.buttonCreateHomeDashboard.Size = new System.Drawing.Size(136, 24);
            this.buttonCreateHomeDashboard.TabIndex = 148;
            this.buttonCreateHomeDashboard.Text = "Create Home Dashboard";
            this.buttonCreateHomeDashboard.UseVisualStyleBackColor = false;
            this.buttonCreateHomeDashboard.Click += new System.EventHandler(this.buttonCreateHomeDashboard_Click);
            // 
            // UserDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(598, 475);
            this.Controls.Add(this.buttonCreateHomeDashboard);
            this.Controls.Add(this.labelCLPass);
            this.Controls.Add(this.textBoxCLPass);
            this.Controls.Add(this.checkBoxCLPass);
            this.Controls.Add(this.checkBoxIsAdmin);
            this.Controls.Add(this.ultraFormattedLinkLabel5);
            this.Controls.Add(this.ultraGroupBox3);
            this.Controls.Add(this.buttonAccessRight);
            this.Controls.Add(this.buttonChangePassword);
            this.Controls.Add(this.buttonGroups);
            this.Controls.Add(this.ultraFormattedLinkLabel3);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxLocationName);
            this.Controls.Add(this.mmLabel6);
            this.Controls.Add(this.textBoxPhone);
            this.Controls.Add(this.mmLabel5);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.mmLabel7);
            this.Controls.Add(this.mmLabel2);
            this.Controls.Add(this.comboBoxEmployee);
            this.Controls.Add(this.checkBoxInactive);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.textBoxConfirmPassword);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxEmployeeName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.mmLabel4);
            this.Controls.Add(this.mmLabel1);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserDetailsForm";
            this.Text = "User Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountGroupDetailsForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultInvLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            this.ultraGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultTransRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultSalesperson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDefaultTransLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
