using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class AccessLevelAssignForm : Form, IForm
	{
		private bool isGridDirty;

		private bool isLoading;

		private bool isUserLevel = true;

		private DataSet currentData;

		private const string TABLENAME_CONST = "Menu_Security";

		private const string IDFIELD_CONST = "MenuID";

		private bool isNewRecord = true;

		private string userID = "";

		private ProductSelector productObj;

		private Form productFormObj;

		private GroupBox groupBoxCC;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private MMLabel labelUserID;

		private MMTextBox textBoxUserName;

		private FormManager formManager;

		private UserComboBox comboBoxUser;

		private Label labelUserName;

		private Label label2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private DataEntryGrid dataGridMenu;

		private AmountTextBox textBoxTotalSalary;

		private UltraTabPageControl tabPageDetails;

		private DataEntryGrid dataGridForms;

		private Label label3;

		private Label label5;

		private UserGroupComboBox comboBoxUserGroup;

		private UltraTabPageControl tabPageControlOther;

		private Label label1;

		private DataEntryGrid dataGridGeneral;

		private UltraTabPageControl tabPageDashboard;

		private Label label4;

		private DataEntryGrid dataGridGadgets;

		private UltraTabPageControl tabPageCustomReports;

		private Label label6;

		private DataEntryGrid dataGridCustomReport;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private Label label7;

		private DataEntryGrid dataGridSmartList;

		private Label label8;

		private DataEntryGrid dataGridPivotReport;

		private XPButton buttonCopy;

		private UltraTabPageControl ultraTabPageControl3;

		private Label label9;

		private DataEntryGrid dataGridExternalReport;

		private XPButton buttonReport;

		private CheckBox checkBoxReport;

		private CheckBox checkBoxFull;

		private UltraTabPageControl ultraTabPageControl4;

		private Label label10;

		private DataEntryGrid dataGridCards;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8004;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public string UserID
		{
			set
			{
				userID = value;
			}
		}

		private bool IsDirty
		{
			get
			{
				if (!formManager.GetDirtyStatus())
				{
					return isGridDirty;
				}
				return true;
			}
		}

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public bool IsUserLevel
		{
			get
			{
				return isUserLevel;
			}
			set
			{
				isUserLevel = value;
				if (!isUserLevel)
				{
					labelUserID.Text = "Group ID:";
					labelUserName.Text = "Group Name:";
					comboBoxUserGroup.Visible = true;
					comboBoxUser.Visible = false;
				}
			}
		}

		public AccessLevelAssignForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += AccessLevelAssignForm_Load;
			comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
			comboBoxUserGroup.SelectedIndexChanged += comboBoxUserGroup_SelectedIndexChanged;
			dataGridForms.CellChange += dataGridForms_CellChange;
			dataGridMenu.CellChange += dataGridMenu_CellChange;
			dataGridGeneral.CellChange += dataGridGeneral_CellChange;
			dataGridGeneral.AfterCellUpdate += dataGridGeneral_AfterCellUpdate;
			dataGridGeneral.AfterRowUpdate += dataGridGeneral_AfterRowUpdate;
			dataGridCards.ClickCellButton += dataGridCards_ClickCellButton;
			dataGridCards.InitializeRow += dataGridCards_InitializeRow;
		}

		private void comboBoxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxUserName.Text = comboBoxUserGroup.SelectedName;
			LoadData(comboBoxUserGroup.SelectedID);
		}

		private void dataGridMenu_AfterCellUpdate(object sender, CellEventArgs e)
		{
			_ = isLoading;
		}

		private void dataGridGeneral_AfterRowUpdate(object sender, RowEventArgs e)
		{
		}

		private void dataGridGeneral_AfterCellUpdate(object sender, CellEventArgs e)
		{
		}

		private void dataGridGeneral_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || !(e.Cell.Column.Key == "IsAllowed") || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if (e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1)
				{
					for (int i = 1; i < 2; i++)
					{
						foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
						{
							if (item.ParentRow == e.Cell.Row)
							{
								item.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (item.HasChild())
							{
								for (int j = 0; j < item.ChildBands.Count; j++)
								{
									bool flag = bool.Parse(e.Cell.Text);
									string a = item.Cells["ItemID"].Value.ToString();
									if (!flag && a == "701")
									{
										dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].CellActivation = Activation.AllowEdit;
									}
									else if (flag)
									{
										_ = (a == "701");
									}
								}
							}
						}
					}
				}
			}
		}

		private void dataGridMenu_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || !(e.Cell.Column.Key == "Visible") || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if ((e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1) && ErrorHelper.QuestionMessageYesNo("Apply to child items?") == DialogResult.Yes)
				{
					for (int i = 0; i < e.Cell.Row.ChildBands.Count; i++)
					{
						foreach (UltraGridRow row in e.Cell.Row.ChildBands[i].Rows)
						{
							if (row.ParentRow == e.Cell.Row)
							{
								row.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (row.HasChild())
							{
								for (int j = 0; j < row.ChildBands.Count; j++)
								{
									foreach (UltraGridRow row2 in row.ChildBands[j].Rows)
									{
										if (row2.ParentRow == row)
										{
											row2.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void dataGridForms_AfterCellUpdate(object sender, CellEventArgs e)
		{
			_ = isLoading;
		}

		private void dataGridForms_CellChange(object sender, CellEventArgs e)
		{
			if (isLoading || (!(e.Cell.Column.Key == "View") && !(e.Cell.Column.Key == "New") && !(e.Cell.Column.Key == "Delete") && !(e.Cell.Column.Key == "Edit")) || !e.Cell.Row.HasChild())
			{
				return;
			}
			isGridDirty = true;
			checked
			{
				if ((e.Cell.Band.Index == 0 || e.Cell.Band.Index == 1) && ErrorHelper.QuestionMessageYesNo("Apply to child items?") == DialogResult.Yes)
				{
					for (int i = 0; i < e.Cell.Row.ChildBands.Count; i++)
					{
						foreach (UltraGridRow row in e.Cell.Row.ChildBands[i].Rows)
						{
							if (row.ParentRow == e.Cell.Row)
							{
								row.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
							}
							if (row.HasChild())
							{
								for (int j = 0; j < row.ChildBands.Count; j++)
								{
									foreach (UltraGridRow row2 in row.ChildBands[j].Rows)
									{
										if (row2.ParentRow == row)
										{
											row2.Cells[e.Cell.Column.Key].Value = e.Cell.Text;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
		{
			checkBoxReport.Checked = false;
			textBoxUserName.Text = comboBoxUser.SelectedName;
			LoadData(comboBoxUser.SelectedID);
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					SecurityData securityData = new SecurityData();
					DataRow dataRow = null;
					object obj = null;
					object obj2 = null;
					if (IsUserLevel)
					{
						obj = comboBoxUser.SelectedID;
					}
					else
					{
						obj2 = comboBoxUserGroup.SelectedID;
					}
					foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212")
						{
							decimal.TryParse(item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value.ToString(), out decimal result);
							securityData.GeneralSecurityTable.Rows.Add(item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, result);
						}
						else
						{
							securityData.GeneralSecurityTable.Rows.Add(item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2);
						}
					}
					foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item2.Cells["ItemID"].Value.ToString() == "702" || item2.Cells["ItemID"].Value.ToString() == "703")
						{
							securityData.GeneralSecurityTable.Rows.Add(item2.Cells["ItemID"].Value.ToString(), item2.Cells["IsAllowed"].Value.ToString(), obj, obj2);
						}
					}
					for (int i = 0; i < 3; i++)
					{
						foreach (UltraGridRow item3 in dataGridMenu.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item3.Cells["Visible"].Value.ToString()))
							{
								dataRow = securityData.MenuSecurityTable.NewRow();
								dataRow["MenuID"] = item3.Cells["MenuID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["Enable"] = true;
								dataRow["Visible"] = item3.Cells["Visible"].Value.ToString();
								dataRow.EndEdit();
								securityData.MenuSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item4 in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item4.Cells["IsAllowed"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item4.Cells["ItemID"].Value.ToString();
							dataRow["ReportType"] = 4;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item4.Cells["IsAllowed"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item5 in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item5.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item5.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 1;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item5.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item6 in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item6.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item6.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 2;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item6.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item7 in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item7.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item7.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 3;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item7.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item8 in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item8.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["MenuID"] = item8.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 5;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item8.Cells["Visible"].Value.ToString();
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					for (int j = 0; j < 4; j++)
					{
						foreach (UltraGridRow item9 in dataGridForms.DisplayLayout.Bands[j].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item9.Cells["View"].Value.ToString()) || bool.Parse(item9.Cells["New"].Value.ToString()) || bool.Parse(item9.Cells["Delete"].Value.ToString()) || bool.Parse(item9.Cells["Edit"].Value.ToString()))
							{
								dataRow = securityData.ScreenSecurityTable.NewRow();
								dataRow["ScreenID"] = item9.Cells["ScreenID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["ViewRight"] = item9.Cells["View"].Value.ToString();
								dataRow["NewRight"] = item9.Cells["New"].Value.ToString();
								dataRow["EditRight"] = item9.Cells["Edit"].Value.ToString();
								dataRow["DeleteRight"] = item9.Cells["Delete"].Value.ToString();
								dataRow.EndEdit();
								securityData.ScreenSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item10 in dataGridCards.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item10.Cells["ConditionalQuery"].Value.ToString() != "")
						{
							dataRow = securityData.CardSecurityTable.NewRow();
							dataRow["CardID"] = item10.Cells["CardID"].Value.ToString();
							dataRow["ConditionalQuery"] = item10.Cells["ConditionalQuery"].Value.ToString();
							dataRow["FilterControl"] = item10.Cells["FilterControl"].Value.ToString();
							dataRow["FilterFrom"] = item10.Cells["FilterFrom"].Value.ToString();
							dataRow["FilterTo"] = item10.Cells["FilterTo"].Value.ToString();
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow.EndEdit();
							securityData.CardSecurityTable.Rows.Add(dataRow);
						}
					}
					currentData = securityData;
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void LoadData(string id)
		{
			LoadData(id, isUserLevel);
		}

		public void LoadData(string id, bool isUserLevelRight)
		{
			try
			{
				if (!base.IsDisposed)
				{
					isLoading = true;
					if (!(id.Trim() == ""))
					{
						id = ((!isUserLevelRight) ? comboBoxUserGroup.SelectedID : comboBoxUser.SelectedID);
						if (!checkBoxReport.Checked)
						{
							currentData = Factory.SecuritySystem.GetSecurityDataByID(id, !isUserLevelRight);
						}
						else
						{
							currentData = Factory.SecuritySystem.GetReportSecurityDataByID(id, !isUserLevelRight, checkBoxFull.Checked);
						}
						FillData(currentData);
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isLoading = false;
			}
		}

		private void FillData(DataSet data)
		{
			FillGeneralAccess(data);
			FillMenuObjects(data);
			FillFormObjects(data);
			FillGadgetObjects(data);
			FillCustomReports(data);
			FillPivotReports(data);
			FillSmartLists(data);
			FillExternalReports(data);
			FillCardsSettings(data);
			DataEntryGrid dataEntryGrid = dataGridCustomReport;
			DataEntryGrid dataEntryGrid2 = dataGridForms;
			DataEntryGrid dataEntryGrid3 = dataGridGadgets;
			DataEntryGrid dataEntryGrid4 = dataGridGeneral;
			DataEntryGrid dataEntryGrid5 = dataGridMenu;
			DataEntryGrid dataEntryGrid6 = dataGridPivotReport;
			DataEntryGrid dataEntryGrid7 = dataGridExternalReport;
			ContextMenuStrip contextMenuStrip2 = dataGridSmartList.ContextMenuStrip = null;
			ContextMenuStrip contextMenuStrip4 = dataEntryGrid7.ContextMenuStrip = contextMenuStrip2;
			ContextMenuStrip contextMenuStrip6 = dataEntryGrid6.ContextMenuStrip = contextMenuStrip4;
			ContextMenuStrip contextMenuStrip8 = dataEntryGrid5.ContextMenuStrip = contextMenuStrip6;
			ContextMenuStrip contextMenuStrip10 = dataEntryGrid4.ContextMenuStrip = contextMenuStrip8;
			ContextMenuStrip contextMenuStrip12 = dataEntryGrid3.ContextMenuStrip = contextMenuStrip10;
			ContextMenuStrip contextMenuStrip15 = dataEntryGrid.ContextMenuStrip = (dataEntryGrid2.ContextMenuStrip = contextMenuStrip12);
		}

		private bool GetGeneralSecurityRoleValue(string roleID)
		{
			DataRow[] array = currentData.Tables["General_Security"].Select("SecurityRoleID= " + roleID.ToString());
			if (array.Length == 0)
			{
				return true;
			}
			return bool.Parse(array[0]["IsAllowed"].ToString());
		}

		private void FillGadgetObjects(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Category");
			dataTable.Columns.Add("CategoryID", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("IsAllowed", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("Item");
			dataTable2.Columns.Add("ItemID", typeof(string));
			dataTable2.Columns.Add("CategoryID", typeof(string));
			dataTable2.Columns.Add("Description", typeof(string));
			dataTable2.Columns.Add("IsAllowed", typeof(bool));
			dataTable.Rows.Add("1", "General", false);
			dataTable.Rows.Add("2", "Accounts", false);
			dataTable.Rows.Add("3", "Sales & Customers", false);
			dataTable.Rows.Add("4", "Purchase & Vendors", false);
			dataTable.Rows.Add("5", "Inventory", false);
			dataTable.Rows.Add("6", "HR & Admin", false);
			dataTable.Rows.Add("7", "Miscellaneous", false);
			dataTable2.Rows.Add(5011, "1", "Shortcut to card screens", false);
			dataTable2.Rows.Add(5010, "1", "Shortcut to transaction screens", false);
			dataTable2.Rows.Add(5012, "1", "Shortcut to reports", false);
			dataTable2.Rows.Add(5002, "3", "Monthly Sales Chart", false);
			dataTable2.Rows.Add(5006, "3", "Top Customers Chart", false);
			dataTable2.Rows.Add(5007, "3", "Top Invoices Chart", false);
			dataTable2.Rows.Add(5008, "3", "Top Products by Sale Chart", false);
			dataTable2.Rows.Add(5009, "3", "Top Salesperson Chart", false);
			dataTable2.Rows.Add(5013, "3", "Daily Sales Chart", false);
			dataTable2.Rows.Add(5001, "2", "Favorite Bank Accounts List", false);
			dataTable2.Rows.Add(5003, "2", "PDCs Issued Near to Maturity", false);
			dataTable2.Rows.Add(5004, "2", "PDCs Onhand Near to Maturity", false);
			dataTable2.Rows.Add(5005, "5", "Items near Reorder Level", false);
			foreach (DataRow row in Factory.CustomGadgetSystem.GetCustomGadgetComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable2.NewRow();
				dataRow2["ItemID"] = row["Code"].ToString();
				if (row["CategoryID"] == DBNull.Value || row["CategoryID"].ToString() == "")
				{
					dataRow2["CategoryID"] = "7";
				}
				else
				{
					dataRow2["CategoryID"] = row["CategoryID"].ToString();
				}
				dataRow2["Description"] = row["Name"].ToString();
				dataRow2["IsAllowed"] = false;
				dataTable2.Rows.Add(dataRow2);
			}
			dataSet.Relations.Add("Rel", dataSet.Tables["Category"].Columns["CategoryID"], dataSet.Tables["Item"].Columns["CategoryID"]);
			dataGridGadgets.SetupUI();
			dataGridGadgets.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].FilterOperandStyle = FilterOperandStyle.None;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].LockedWidth = true;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellActivation = Activation.Disabled;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["CategoryID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["ItemID"].Hidden = true;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGadgets.DisplayLayout.Bands[1].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGadgets.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridGadgets.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridGadgets.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridGadgets.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGadgets.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridGadgets.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			foreach (UltraGridRow item in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 4 AND MenuID = '" + item.Cells["ItemID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["IsAllowed"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellDisplayStyle = CellDisplayStyle.PlainText;
			dataGridGadgets.DisplayLayout.Bands[0].Columns["IsAllowed"].CellAppearance.ForeColorDisabled = Color.White;
		}

		private void FillGeneralAccess(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Category");
			dataTable.Columns.Add("CategoryID", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("IsAllowed", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("Item");
			dataTable2.Columns.Add("ItemID", typeof(string));
			dataTable2.Columns.Add("CategoryID", typeof(string));
			dataTable2.Columns.Add("Description", typeof(string));
			dataTable2.Columns.Add("IsAllowed", typeof(bool));
			dataTable.Rows.Add("1", "Transactions", false);
			dataTable.Rows.Add("2", "Sales", false);
			dataTable.Rows.Add("3", "Purchase", false);
			dataTable.Rows.Add("4", "Customers", false);
			dataTable.Rows.Add("5", "Vendors", false);
			dataTable.Rows.Add("6", "Inventory", false);
			dataTable.Rows.Add("7", "HR & Admin", false);
			dataTable.Rows.Add("8", "Reports", false);
			dataTable2.Rows.Add("100", "1", "Allow to change transaction numbers", true);
			dataTable2.Rows.Add("101", "1", "Allow to enter back-dated transactions", true);
			dataTable2.Rows.Add("102", "1", "Allow to change document ID", true);
			dataTable2.Rows.Add("103", "1", "Allow to change currency", true);
			dataTable2.Rows.Add("104", "1", "Allow to change currency rate", true);
			dataTable2.Rows.Add("105", "1", "Allow to enter future-dated transactions", true);
			dataTable2.Rows.Add("106", "1", "Allow to change transaction register", true);
			dataTable2.Rows.Add("107", "1", "Allow to change customer in delivery notes", false);
			dataTable2.Rows.Add("108", "1", "Allow to change vendor in GRN", false);
			dataTable2.Rows.Add("109", "1", "Allow to change vendor in Purchase Return", false);
			dataTable2.Rows.Add("110", "1", "Allow to view item details", false);
			dataTable2.Rows.Add("111", "1", "Allow to access supplier balance and due", false);
			dataTable2.Rows.Add("112", "1", "Allow to access customer balance", false);
			dataTable2.Rows.Add("113", "1", "Allow to access account balance", false);
			dataTable2.Rows.Add("114", "1", "Allow to access employee balance", false);
			dataTable2.Rows.Add("115", "1", "Allow to edit card created by other user", false);
			dataTable2.Rows.Add("116", "1", "Allow to edit transaction created by other user", false);
			dataTable2.Rows.Add("117", "1", "Allow to create/edit transaction made in another location", false);
			dataTable2.Rows.Add("118", "1", "Allow to change Tax settings", false);
			dataTable2.Rows.Add("119", "1", "Allow to edit Tax Total", false);
			dataTable2.Rows.Add("200", "2", "Allow to give discount", true);
			dataTable2.Rows.Add("201", "2", "Allow to change the price", true);
			dataTable2.Rows.Add("202", "2", "Allow to change salesperson", true);
			dataTable2.Rows.Add("204", "2", "Allow to change inventory location in transactions", true);
			dataTable2.Rows.Add("205", "2", "Allow to change payment term and due date in invoicing", true);
			dataTable2.Rows.Add("206", "2", "Allow to take sales return upto Days", true);
			dataTable2.Rows.Add("209", "2", "Allow to change Bill to Address on DN & Sale Invoice(Local & Export)", false);
			dataTable2.Rows.Add("210", "2", "Allow to create from Default Location Only", false);
			dataTable2.Rows.Add("211", "2", "Allow to change Description", true);
			dataTable2.Rows.Add("212", "2", "Allow to give price discount", true);
			dataTable2.Rows.Add("213", "2", "Block Copy Print", false);
			dataTable2.Rows.Add("214", "2", "Block Discount Modification", false);
			dataTable2.Rows.Add("300", "3", "Allow to create multiple POs on single BOL", false);
			dataTable2.Rows.Add("301", "3", "Allow to create from Default Location Only", false);
			dataTable2.Rows.Add("203", "6", "Allow to access item cost", false);
			dataTable2.Rows.Add("600", "6", "Allow to access item unit price 1", true);
			dataTable2.Rows.Add("601", "6", "Allow to access item unit price 2", true);
			dataTable2.Rows.Add("602", "6", "Allow to access item unit price 3", true);
			dataTable2.Rows.Add("603", "6", "Allow to access item min price", true);
			dataTable2.Rows.Add("604", "6", "Allow to flag items", false);
			dataTable2.Rows.Add("700", "7", "Allow to access salary information", false);
			dataTable2.Rows.Add("701", "7", "Allow only Self leave Request", false);
			dataTable2.Rows.Add("750", "8", "Allow to edit smart list reports", false);
			dataTable2.Rows.Add("751", "8", "Allow to edit pivot reports", false);
			dataTable2.Rows.Add("207", "8", "Allow to edit Print Template", false);
			dataTable2.Rows.Add("208", "8", "Allow to Export Print", false);
			dataTable2.Rows.Add("401", "4", "Allow flag customers", false);
			dataTable2.Rows.Add("402", "4", "Show only customers assigned to user for collection", false);
			dataTable2.Rows.Add("501", "5", "Allow flag vendors", false);
			DataTable dataTable3 = dataSet.Tables.Add("Itemnew");
			dataTable3.Columns.Add("ItemID", typeof(string));
			dataTable3.Columns.Add("Days/Percentage Allowed", typeof(decimal));
			dataTable3.Rows.Add("101", 0);
			dataTable3.Rows.Add("206", 0);
			dataTable3.Rows.Add("200", 0);
			dataTable3.Rows.Add("212", 0);
			dataSet.Relations.Add("Rel", dataSet.Tables["Category"].Columns["CategoryID"], dataSet.Tables["Item"].Columns["CategoryID"]);
			dataSet.Relations.Add("Rel_new", dataSet.Tables["Item"].Columns["ItemID"], dataSet.Tables["Itemnew"].Columns["ItemID"]);
			DataTable dataTable4 = dataSet.Tables.Add("HRnew");
			dataTable4.Columns.Add("ItemID", typeof(string));
			dataTable4.Columns.Add("ItemIDRel", typeof(string));
			dataTable4.Columns.Add("CategoryID", typeof(string));
			dataTable4.Columns.Add("IsAllowed", typeof(bool));
			dataTable4.Columns.Add("Description", typeof(string));
			dataTable4.Rows.Add("702", "701", "7", false, "Allow only Intermediate Suboridnates");
			dataTable4.Rows.Add("703", "701", "7", false, "Allow all Suboridnates");
			dataSet.Relations.Add("Rel_HR", dataSet.Tables["Item"].Columns["ItemID"], dataSet.Tables["HRnew"].Columns["ItemIDRel"]);
			dataGridGeneral.SetupUI();
			dataGridGeneral.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].LockedWidth = true;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellActivation = Activation.Disabled;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["IsAllowed"].Header.VisiblePosition = 0;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].Header.Caption = "C";
			dataGridGeneral.DisplayLayout.Bands[3].Columns["CategoryID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["ItemIDRel"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].MaxWidth = 36;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["IsAllowed"].LockedWidth = true;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["ItemID"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["ItemID"].Hidden = true;
			dataGridGeneral.DisplayLayout.Bands[2].Columns["Days/Percentage Allowed"].Width = 500;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[1].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[3].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridGeneral.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridGeneral.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridGeneral.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridGeneral.DisplayLayout.Bands[0].Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGeneral.DisplayLayout.Bands[1].Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridGeneral.DisplayLayout.Bands[2].Override.AllowColSizing = AllowColSizing.Default;
			dataGridGeneral.DisplayLayout.Bands[3].Override.AllowColSizing = AllowColSizing.Default;
			dataGridGeneral.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridGeneral.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
			foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["General_Security"].Select("SecurityRoleID='" + item.Cells["ItemID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["IsAllowed"].Value = bool.Parse(array[0]["IsAllowed"].ToString());
				}
				if (array.Length != 0 && (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212"))
				{
					item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value = decimal.Parse(array[0]["intVal"].ToString());
				}
				if (checkBoxReport.Checked && array.Length != 0)
				{
					if (array.Length > 1)
					{
						item.Cells["IsAllowed"].Value = bool.Parse(array[1]["IsAllowed"].ToString());
					}
					else
					{
						item.Cells["IsAllowed"].Value = bool.Parse(array[0]["IsAllowed"].ToString());
					}
				}
			}
			foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array2 = gData.Tables["General_Security"].Select("SecurityRoleID='" + item2.Cells["ItemID"].Value.ToString() + "'");
				if (array2.Length != 0)
				{
					item2.Cells["IsAllowed"].Value = bool.Parse(array2[0]["IsAllowed"].ToString());
				}
				if (checkBoxReport.Checked && array2.Length != 0)
				{
					if (array2.Length > 1)
					{
						item2.Cells["IsAllowed"].Value = bool.Parse(array2[1]["IsAllowed"].ToString());
					}
					else
					{
						item2.Cells["IsAllowed"].Value = bool.Parse(array2[0]["IsAllowed"].ToString());
					}
				}
			}
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellDisplayStyle = CellDisplayStyle.PlainText;
			dataGridGeneral.DisplayLayout.Bands[0].Columns["IsAllowed"].CellAppearance.ForeColorDisabled = Color.White;
		}

		private void FillMenuObjects(DataSet data)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("Menu");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			DataTable dataTable2 = dataSet.Tables.Add("SubMenu");
			dataTable2.Columns.Add("MenuID", typeof(string));
			dataTable2.Columns.Add("ParentID", typeof(string));
			dataTable2.Columns.Add("MenuName", typeof(string));
			dataTable2.Columns.Add("Visible", typeof(bool));
			DataTable dataTable3 = dataSet.Tables.Add("SubSubMenu");
			dataTable3.Columns.Add("MenuID", typeof(string));
			dataTable3.Columns.Add("ParentID", typeof(string));
			dataTable3.Columns.Add("MenuName", typeof(string));
			dataTable3.Columns.Add("Visible", typeof(bool));
			foreach (ToolStripItem item in ((formMain)FormActivator.MainForm).MainMenu.Items)
			{
				if (item.Tag == null || !(item.Tag.ToString() == "1"))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["MenuID"] = item.Name;
					dataRow["MenuName"] = item.Text.Replace("&&", "and").Replace("&", "");
					dataRow["Visible"] = false;
					dataTable.Rows.Add(dataRow);
					foreach (ToolStripItem dropDownItem in (item as ToolStripDropDownItem).DropDownItems)
					{
						if (dropDownItem.Tag == null || !(dropDownItem.Tag.ToString() == "1"))
						{
							dataRow = dataTable2.NewRow();
							dataRow["MenuID"] = dropDownItem.Name;
							dataRow["ParentID"] = item.Name;
							if (dropDownItem.GetType() == typeof(ToolStripSeparator))
							{
								dataRow["MenuName"] = "-";
							}
							else
							{
								dataRow["MenuName"] = dropDownItem.Text.Replace("&&", "and").Replace("&", "");
							}
							dataRow["Visible"] = false;
							dataTable2.Rows.Add(dataRow);
							ToolStripDropDownItem toolStripDropDownItem = dropDownItem as ToolStripDropDownItem;
							if (toolStripDropDownItem != null && toolStripDropDownItem.GetType() == typeof(ToolStripMenuItem))
							{
								foreach (ToolStripItem dropDownItem2 in toolStripDropDownItem.DropDownItems)
								{
									if (dropDownItem2.Tag == null || !(dropDownItem2.Tag.ToString() == "1"))
									{
										dataRow = dataTable3.NewRow();
										dataRow["MenuID"] = dropDownItem2.Name;
										dataRow["ParentID"] = dropDownItem.Name;
										if (dropDownItem2.GetType() == typeof(ToolStripSeparator))
										{
											dataRow["MenuName"] = "-";
										}
										else
										{
											dataRow["MenuName"] = dropDownItem2.Text.Replace("&&", "and").Replace("&", "");
										}
										dataRow["Visible"] = false;
										dataTable3.Rows.Add(dataRow);
									}
								}
							}
						}
					}
				}
			}
			dataSet.Relations.Add("Rel", dataSet.Tables["Menu"].Columns["MenuID"], dataSet.Tables["SubMenu"].Columns["ParentID"]);
			dataSet.Relations.Add("Rel2", dataSet.Tables["SubMenu"].Columns["MenuID"], dataSet.Tables["SubSubMenu"].Columns["ParentID"]);
			dataGridMenu.SetupUI();
			dataGridMenu.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridMenu.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[1].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[2].Columns["MenuID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[1].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[2].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridMenu.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridMenu.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridMenu.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridMenu.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridMenu.DisplayLayout.Bands[1].Columns["ParentID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridMenu.DisplayLayout.Bands[2].Columns["ParentID"].Hidden = true;
			dataGridMenu.DisplayLayout.Bands[2].ColHeadersVisible = false;
			for (int i = 0; i < 3; i = checked(i + 1))
			{
				foreach (UltraGridRow item2 in dataGridMenu.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
				{
					DataRow[] array = data.Tables["Menu_Security"].Select("MenuID='" + item2.Cells["MenuID"].Value.ToString() + "'");
					if (array.Length != 0)
					{
						item2.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
					}
					if (checkBoxReport.Checked && array.Length != 0)
					{
						if (array.Length > 1)
						{
							item2.Cells["Visible"].Value = bool.Parse(array[1]["Visible"].ToString());
						}
						else
						{
							item2.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
						}
					}
				}
			}
			dataGridMenu.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridMenu.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridMenu.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridMenu.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridMenu.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridMenu.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridMenu.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridForms.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillSmartLists(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.SmartListSystem.GetSmartListComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridSmartList.SetupUI();
			dataGridSmartList.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridSmartList.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridSmartList.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridSmartList.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridSmartList.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridSmartList.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridSmartList.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridSmartList.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 2 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridSmartList.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
			dataGridSmartList.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridSmartList.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridSmartList.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridSmartList.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridSmartList.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridSmartList.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridSmartList.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Category"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				band.Columns["Category"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridSmartList.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillExternalReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.ExternalReportSystem.GetExternalReportComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridExternalReport.SetupUI();
			dataGridExternalReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridExternalReport.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridExternalReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridExternalReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridExternalReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridExternalReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 5 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridExternalReport.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
			dataGridExternalReport.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridExternalReport.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridExternalReport.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridExternalReport.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridExternalReport.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridExternalReport.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridExternalReport.DisplayLayout.Bands)
			{
				band.Columns["Visible"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Category"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Visible"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				band.Columns["Category"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
			}
			dataGridExternalReport.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private void FillPivotReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Category", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.PivotSystem.GetPivotComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Category"] = row["Category"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridPivotReport.SetupUI();
			dataGridPivotReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridPivotReport.DisplayLayout.Bands[0].Columns["Category"].CellActivation = Activation.NoEdit;
			dataGridPivotReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridPivotReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridPivotReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridPivotReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 3 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
			}
			dataGridPivotReport.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortMulti;
		}

		private void FillCustomReports(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CustomReport");
			dataTable.Columns.Add("MenuID", typeof(string));
			dataTable.Columns.Add("MenuName", typeof(string));
			dataTable.Columns.Add("Visible", typeof(bool));
			foreach (DataRow row in Factory.CustomReportSystem.GetCustomReportComboList().Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["MenuID"] = row["Code"].ToString();
				dataRow2["MenuName"] = row["Name"].ToString();
				dataRow2["Visible"] = false;
				dataTable.Rows.Add(dataRow2);
			}
			dataGridCustomReport.SetupUI();
			dataGridCustomReport.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridCustomReport.DisplayLayout.Bands[0].Columns["MenuID"].Hidden = true;
			dataGridCustomReport.DisplayLayout.Bands[0].Columns["MenuName"].CellActivation = Activation.NoEdit;
			dataGridCustomReport.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridCustomReport.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridCustomReport.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridCustomReport.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			foreach (UltraGridRow item in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Custom_Report_Security"].Select("ReportType = 1 AND MenuID = '" + item.Cells["MenuID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
				}
				if (checkBoxReport.Checked && array.Length != 0)
				{
					if (array.Length > 1)
					{
						item.Cells["Visible"].Value = bool.Parse(array[1]["Visible"].ToString());
					}
					else
					{
						item.Cells["Visible"].Value = bool.Parse(array[0]["Visible"].ToString());
					}
				}
			}
		}

		private void FillFormObjects(DataSet data)
		{
			DataSet securityFormList = new FormHelper().GetSecurityFormList();
			dataGridForms.DataSource = securityFormList;
			securityFormList.AcceptChanges();
			dataGridForms.SetupUI();
			dataGridForms.DisplayLayout.Bands[0].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[1].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenSubArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenSubArea"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[0].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[1].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[3].Columns["ScreenName"].CellActivation = Activation.NoEdit;
			dataGridForms.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridForms.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;
			dataGridForms.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			dataGridForms.DisplayLayout.Override.AllowColSizing = AllowColSizing.Synchronized;
			dataGridForms.DisplayLayout.Bands[1].Columns["ParentID"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[1].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[2].Columns["ScreenType"].Hidden = true;
			dataGridForms.DisplayLayout.Bands[2].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[3].ColHeadersVisible = false;
			dataGridForms.DisplayLayout.Bands[1].Columns["New"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[1].Columns["Edit"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[1].Columns["Delete"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["New"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["Edit"].CellActivation = Activation.Disabled;
			dataGridForms.DisplayLayout.Bands[2].Columns["Delete"].CellActivation = Activation.Disabled;
			foreach (UltraGridRow row in dataGridForms.Rows)
			{
				if (row.Cells["ScreenName"].Value.ToString().ToLower() == "reports")
				{
					row.Cells["New"].Activation = Activation.Disabled;
					row.Cells["Edit"].Activation = Activation.Disabled;
					row.Cells["Delete"].Activation = Activation.Disabled;
				}
			}
			for (int i = 0; i < 4; i = checked(i + 1))
			{
				foreach (UltraGridRow item in dataGridForms.DisplayLayout.Bands[i].GetRowEnumerator(GridRowType.DataRow))
				{
					DataRow[] array = data.Tables["Screen_Security"].Select("ScreenID='" + item.Cells["ScreenID"].Value.ToString() + "'");
					if (array.Length != 0)
					{
						item.Cells["View"].Value = bool.Parse(array[0]["ViewRight"].ToString());
						item.Cells["New"].Value = bool.Parse(array[0]["NewRight"].ToString());
						item.Cells["Edit"].Value = bool.Parse(array[0]["EditRight"].ToString());
						item.Cells["Delete"].Value = bool.Parse(array[0]["DeleteRight"].ToString());
					}
					if (checkBoxReport.Checked && array.Length != 0)
					{
						if (array.Length > 1)
						{
							item.Cells["View"].Value = bool.Parse(array[1]["ViewRight"].ToString());
							item.Cells["New"].Value = bool.Parse(array[1]["NewRight"].ToString());
							item.Cells["Edit"].Value = bool.Parse(array[1]["EditRight"].ToString());
							item.Cells["Delete"].Value = bool.Parse(array[1]["DeleteRight"].ToString());
						}
						else
						{
							item.Cells["View"].Value = bool.Parse(array[0]["ViewRight"].ToString());
							item.Cells["New"].Value = bool.Parse(array[0]["NewRight"].ToString());
							item.Cells["Edit"].Value = bool.Parse(array[0]["EditRight"].ToString());
							item.Cells["Delete"].Value = bool.Parse(array[0]["DeleteRight"].ToString());
						}
					}
				}
			}
			dataGridForms.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
			dataGridForms.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
			dataGridForms.DisplayLayout.Override.FilterOperandStyle = FilterOperandStyle.FilterUIProvider;
			dataGridForms.DisplayLayout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains;
			dataGridForms.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
			dataGridForms.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
			foreach (UltraGridBand band in dataGridForms.DisplayLayout.Bands)
			{
				band.Columns["Delete"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["View"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["New"].FilterOperandStyle = FilterOperandStyle.None;
				band.Columns["Edit"].FilterOperandStyle = FilterOperandStyle.None;
				AppearanceBase filterOperatorAppearance = band.Columns["Delete"].FilterOperatorAppearance;
				AppearanceBase filterOperatorAppearance2 = band.Columns["View"].FilterOperatorAppearance;
				AppearanceBase filterOperatorAppearance3 = band.Columns["New"].FilterOperatorAppearance;
				Color color = band.Columns["Edit"].FilterOperatorAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = filterOperatorAppearance3.BackColor = color;
				Color color6 = filterOperatorAppearance.BackColor = (filterOperatorAppearance2.BackColor = color3);
			}
			dataGridForms.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.Hidden;
		}

		private bool SaveData()
		{
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
				bool flag = true;
				if (checkBoxReport.Checked)
				{
					ErrorHelper.WarningMessage("Please Uncheck Generate Report");
					return false;
				}
				flag = Factory.SecuritySystem.CreateSecurity((SecurityData)currentData, comboBoxUser.SelectedID, comboBoxUserGroup.SelectedID);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
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
			if (isUserLevel && comboBoxUser.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user.");
				return false;
			}
			if (!isUserLevel && comboBoxUserGroup.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user group.");
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
		}

		private void UserGroupGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void UserGroupGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
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
			return true;
		}

		private void AccessLevelAssignForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					comboBoxUser.LoadData();
					IsNewRecord = true;
					ClearForm();
					if (IsUserLevel)
					{
						comboBoxUser.SelectedID = userID;
					}
					else
					{
						comboBoxUserGroup.SelectedID = userID;
					}
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
				Dispose();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.UserGroup);
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			CopyRightsDialog copyRightsDialog = new CopyRightsDialog();
			if (copyRightsDialog.ShowDialog(this) == DialogResult.OK)
			{
				bool isUser = copyRightsDialog.IsUser;
				if (copyRightsDialog.SelectedID == "")
				{
					return;
				}
				DataSet securityDataByID = Factory.SecuritySystem.GetSecurityDataByID(copyRightsDialog.SelectedID, !isUser);
				FillData(securityDataByID);
				formManager.IsForcedDirty = true;
			}
			base.DialogResult = DialogResult.None;
		}

		private void buttonReport_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					SecurityData securityData = new SecurityData();
					checkBoxReport.Checked = true;
					DataTable dataTable = new DataTable("UserDetail");
					dataTable.Columns.Add("UserID", typeof(string));
					dataTable.Rows.Add(comboBoxUser.SelectedID);
					for (int i = 0; i < securityData.Tables.Count; i++)
					{
						securityData.Tables[i].Columns.Add(new DataColumn("Description"));
						securityData.Tables[i].Columns.Add("SlNo.", typeof(string)).SetOrdinal(0);
					}
					securityData.Merge(dataTable);
					DataRow dataRow = null;
					object obj = null;
					object obj2 = null;
					if (IsUserLevel)
					{
						obj = comboBoxUser.SelectedID;
					}
					else
					{
						obj2 = comboBoxUserGroup.SelectedID;
					}
					string text = "";
					foreach (UltraGridRow item in dataGridGeneral.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item.Cells["ItemID"].Value.ToString() == "101" || item.Cells["ItemID"].Value.ToString() == "206" || item.Cells["ItemID"].Value.ToString() == "200" || item.Cells["ItemID"].Value.ToString() == "212")
						{
							decimal.TryParse(item.ChildBands[0].Rows[0].Cells["Days/Percentage Allowed"].Value.ToString(), out decimal result);
							text = item.Cells["Description"].Text;
							if (bool.Parse(item.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item.Index + 1, item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, result, text);
							}
						}
						else
						{
							text = item.Cells["Description"].Text;
							if (bool.Parse(item.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item.Index + 1, item.Cells["ItemID"].Value.ToString(), item.Cells["IsAllowed"].Value.ToString(), obj, obj2, 0, text);
							}
						}
					}
					foreach (UltraGridRow item2 in dataGridGeneral.DisplayLayout.Bands[3].GetRowEnumerator(GridRowType.DataRow))
					{
						if (item2.Cells["ItemID"].Value.ToString() == "702" || item2.Cells["ItemID"].Value.ToString() == "703")
						{
							text = item2.Cells["Description"].Text;
							if (bool.Parse(item2.Cells["IsAllowed"].Value.ToString()))
							{
								securityData.GeneralSecurityTable.Rows.Add(item2.Index + 1, item2.Cells["ItemID"].Value.ToString(), item2.Cells["IsAllowed"].Value.ToString(), obj, obj2, 0, text);
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						foreach (UltraGridRow item3 in dataGridMenu.DisplayLayout.Bands[j].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item3.Cells["Visible"].Value.ToString()))
							{
								dataRow = securityData.MenuSecurityTable.NewRow();
								dataRow["SlNo."] = item3.Index + 1;
								dataRow["MenuID"] = item3.Cells["MenuID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["Enable"] = true;
								dataRow["Visible"] = item3.Cells["Visible"].Value.ToString();
								dataRow["Description"] = item3.Cells["MenuName"].Text;
								dataRow.EndEdit();
								securityData.MenuSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					foreach (UltraGridRow item4 in dataGridGadgets.DisplayLayout.Bands[1].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item4.Cells["IsAllowed"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item4.Index + 1;
							dataRow["MenuID"] = item4.Cells["ItemID"].Value.ToString();
							dataRow["ReportType"] = 4;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item4.Cells["IsAllowed"].Value.ToString();
							dataRow["Description"] = item4.Cells["Description"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item5 in dataGridCustomReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item5.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item5.Index + 1;
							dataRow["MenuID"] = item5.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 1;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Enable"] = true;
							dataRow["Visible"] = item5.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item5.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item6 in dataGridSmartList.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item6.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item6.Index + 1;
							dataRow["MenuID"] = item6.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 2;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item6.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item6.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item7 in dataGridPivotReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item7.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item7.Index + 1;
							dataRow["MenuID"] = item7.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 3;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item7.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item7.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow item8 in dataGridExternalReport.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
					{
						if (bool.Parse(item8.Cells["Visible"].Value.ToString()))
						{
							dataRow = securityData.CustomReportSecurityTable.NewRow();
							dataRow["SlNo."] = item8.Index + 1;
							dataRow["MenuID"] = item8.Cells["MenuID"].Value.ToString();
							dataRow["ReportType"] = 5;
							if (isUserLevel)
							{
								dataRow["UserID"] = comboBoxUser.SelectedID;
							}
							else
							{
								dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
							}
							dataRow["Visible"] = item8.Cells["Visible"].Value.ToString();
							dataRow["Description"] = item8.Cells["MenuName"].Text;
							dataRow.EndEdit();
							securityData.CustomReportSecurityTable.Rows.Add(dataRow);
						}
					}
					for (int k = 0; k < 4; k++)
					{
						foreach (UltraGridRow item9 in dataGridForms.DisplayLayout.Bands[k].GetRowEnumerator(GridRowType.DataRow))
						{
							if (bool.Parse(item9.Cells["View"].Value.ToString()) || bool.Parse(item9.Cells["New"].Value.ToString()) || bool.Parse(item9.Cells["Delete"].Value.ToString()) || bool.Parse(item9.Cells["Edit"].Value.ToString()))
							{
								dataRow = securityData.ScreenSecurityTable.NewRow();
								dataRow["SlNo."] = item9.Index + 1;
								dataRow["ScreenID"] = item9.Cells["ScreenID"].Value.ToString();
								if (isUserLevel)
								{
									dataRow["UserID"] = comboBoxUser.SelectedID;
								}
								else
								{
									dataRow["GroupID"] = comboBoxUserGroup.SelectedID;
								}
								dataRow["ViewRight"] = item9.Cells["View"].Value.ToString();
								dataRow["NewRight"] = item9.Cells["New"].Value.ToString();
								dataRow["EditRight"] = item9.Cells["Edit"].Value.ToString();
								dataRow["DeleteRight"] = item9.Cells["Delete"].Value.ToString();
								dataRow["Description"] = item9.Cells["ScreenName"].Text;
								dataRow.EndEdit();
								securityData.ScreenSecurityTable.Rows.Add(dataRow);
							}
						}
					}
					for (int l = 0; l < securityData.Tables.Count; l++)
					{
						if (!(securityData.Tables[l].ToString() == "UserDetail"))
						{
							securityData.Relations.Add(securityData.Tables[l].ToString() + "_Rel", securityData.Tables["UserDetail"].Columns["UserID"], securityData.Tables[l].Columns["UserID"], createConstraints: false);
						}
					}
					ReportHelper reportHelper = new ReportHelper();
					XtraReport xtraReport = null;
					xtraReport = reportHelper.GetReport("UserRightReport");
					DataSet data = securityData;
					reportHelper.AddGeneralReportData(ref data, "");
					if (xtraReport == null)
					{
						checkBoxReport.Checked = false;
						ErrorHelper.ErrorMessage("Cannot find the report file. Please make sure you have access to reports path and the files are not corrupted.", "'UserRightReport.repx'");
					}
					else
					{
						xtraReport.DataSource = data;
						reportHelper.ShowReport(xtraReport);
						checkBoxReport.Checked = false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void LoadMenuSubstitutes(DataSet reportData)
		{
			List<string> list = (from x in reportData.Tables["MenuSecurity"].AsEnumerable()
				select x["MenuText"].ToString()).ToList();
			if (list.Count > 0)
			{
				foreach (ToolStripItem item in ((formMain)FormActivator.MainForm).MainMenu.Items)
				{
					foreach (ToolStripItem dropDownItem in (item as ToolStripDropDownItem).DropDownItems)
					{
						if (list.Contains(dropDownItem.Text) && dropDownItem.Text != "" && dropDownItem.Text != string.Empty)
						{
							string filterExpression = "MenuText ='" + dropDownItem.Text + "'";
							DataRow[] array = reportData.Tables[0].Select(filterExpression);
							dropDownItem.Text = array[0]["AliasName"].ToString();
						}
						ToolStripDropDownItem toolStripDropDownItem = dropDownItem as ToolStripDropDownItem;
						if (toolStripDropDownItem != null && toolStripDropDownItem.GetType() == typeof(ToolStripMenuItem))
						{
							foreach (ToolStripItem dropDownItem2 in toolStripDropDownItem.DropDownItems)
							{
								if (list.Contains(dropDownItem2.Text) && dropDownItem2.Text != "" && dropDownItem2.Text != string.Empty)
								{
									string filterExpression2 = "MenuText ='" + dropDownItem2.Text + "'";
									DataRow[] array2 = reportData.Tables[0].Select(filterExpression2);
									dropDownItem2.Text = array2[0]["AliasName"].ToString();
								}
							}
						}
					}
				}
			}
		}

		private void checkBoxReport_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxReport.Checked)
			{
				LoadData(comboBoxUser.SelectedID);
			}
			else
			{
				LoadData(comboBoxUser.SelectedID);
			}
		}

		private void FillCardsSettings(DataSet gData)
		{
			DataSet dataSet = new DataSet();
			DataTable dataTable = dataSet.Tables.Add("CardSettings");
			dataTable.Columns.Add("CardID", typeof(int));
			dataTable.Columns.Add("CardName", typeof(string));
			dataTable.Columns.Add("ConditionalQuery", typeof(string));
			dataTable.Columns.Add("FilterControl", typeof(string));
			dataTable.Columns.Add("FilterFrom", typeof(string));
			dataTable.Columns.Add("FilterTo", typeof(string));
			dataTable.Columns.Add("Condition", typeof(string));
			dataTable.Columns.Add("Clear", typeof(string));
			foreach (object value in Enum.GetValues(typeof(EntityTypesEnum)))
			{
				if ((EntityTypesEnum)value == EntityTypesEnum.Items)
				{
					dataTable.Rows.Add((int)Enum.Parse(value.GetType(), value.ToString()), value, "", "", "", "", "", "");
				}
			}
			dataGridCards.SetupUI();
			dataGridCards.DataSource = dataSet;
			dataSet.AcceptChanges();
			dataGridCards.DisplayLayout.Bands[0].Columns["CardID"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["ConditionalQuery"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterControl"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterFrom"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["FilterTo"].Hidden = true;
			dataGridCards.DisplayLayout.Bands[0].Columns["ConditionalQuery"].CellActivation = Activation.NoEdit;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].CellButtonAppearance.TextHAlign = HAlign.Center;
			dataGridCards.DisplayLayout.Bands[0].Columns["Condition"].Width = 20;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].CellButtonAppearance.TextHAlign = HAlign.Center;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Width = 20;
			dataGridCards.DisplayLayout.Bands[0].Columns["Clear"].Header.Caption = "";
			dataGridCards.DisplayLayout.Bands[0].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
			dataGridCards.DisplayLayout.Override.ExpansionIndicator = ShowExpansionIndicator.Never;
			dataGridCards.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
			foreach (UltraGridRow item in dataGridCards.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow))
			{
				DataRow[] array = gData.Tables["Card_Security"].Select("CardID = '" + item.Cells["CardID"].Value.ToString() + "'");
				if (array.Length != 0)
				{
					item.Cells["ConditionalQuery"].Value = array[0]["ConditionalQuery"].ToString();
					item.Cells["FilterControl"].Value = array[0]["FilterControl"].ToString();
					item.Cells["FilterFrom"].Value = array[0]["FilterFrom"].ToString();
					item.Cells["FilterTo"].Value = array[0]["FilterTo"].ToString();
				}
			}
		}

		private void dataGridCards_ClickCellButton(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Condition")
			{
				int index = e.Cell.Row.Index;
				if (int.Parse(dataGridCards.Rows[index].Cells["CardID"].Value.ToString()) == 25)
				{
					setupProductSelector();
				}
			}
			else if (e.Cell.Column.Key == "Clear" && ErrorHelper.QuestionMessageYesNo("Clear this condition?") == DialogResult.Yes)
			{
				int index2 = e.Cell.Row.Index;
				dataGridCards.Rows[index2].Cells["ConditionalQuery"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterControl"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterFrom"].Value = "";
				dataGridCards.Rows[index2].Cells["FilterTo"].Value = "";
			}
		}

		private void dataGridCards_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			if (!e.ReInitialize)
			{
				e.Row.Cells["Condition"].Value = "...";
				e.Row.Cells["Clear"].Value = "Clear";
			}
		}

		private void setupProductSelector()
		{
			productObj = new ProductSelector();
			productObj.BackColor = Color.Transparent;
			productObj.Location = new Point(6, 15);
			productObj.Name = "Product Selector";
			productObj.Size = new Size(430, 200);
			groupBoxCC = new GroupBox();
			groupBoxCC.Controls.Add(productObj);
			groupBoxCC.Location = new Point(2, 12);
			groupBoxCC.Name = "groupBoxCC";
			groupBoxCC.Size = new Size(453, 200);
			groupBoxCC.Text = "Product";
			Button button = new Button();
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button.Location = new Point(249, 230);
			button.Name = "buttonOK";
			button.Size = new Size(102, 24);
			button.Text = "&Ok";
			button.UseVisualStyleBackColor = true;
			button.Click += buttonOK_Click;
			Button button2 = new Button();
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button2.DialogResult = DialogResult.Cancel;
			button2.Location = new Point(353, 230);
			button2.Name = "buttonClose";
			button2.Size = new Size(102, 24);
			button2.TabIndex = 5;
			button2.Text = "&Close";
			button2.UseVisualStyleBackColor = true;
			productFormObj = new Form();
			productFormObj.Size = new Size(481, 300);
			productFormObj.Controls.Add(button);
			productFormObj.Controls.Add(button2);
			productFormObj.Controls.Add(groupBoxCC);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AccessLevelAssignForm));
			productFormObj.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			productFormObj.Text = "Select Product";
			productFormObj.ShowDialog();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			string text = "";
			string value = "";
			string value2 = "";
			RadioButton radioButton = productObj.Controls.OfType<RadioButton>().FirstOrDefault((RadioButton r) => r.Checked);
			switch (radioButton.Name)
			{
			case "radioButtonSingle":
			case "radioButtonRange":
				if (productObj.FromItem == "" || productObj.ToItem == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ProductID Between '" + productObj.FromItem + "' AND '" + productObj.ToItem + "' ";
				value = productObj.FromItem;
				value2 = productObj.ToItem;
				break;
			case "radioButtonClass":
				if (productObj.FromClass == "" || productObj.ToClass == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ClassID Between '" + productObj.FromClass + "' AND '" + productObj.ToClass + "' ";
				value = productObj.FromClass;
				value2 = productObj.ToClass;
				break;
			case "radioButtonCategory":
				if (productObj.FromCategory == "" || productObj.FromCategory == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "CategoryID Between '" + productObj.FromCategory + "' AND '" + productObj.ToCategory + "' ";
				value = productObj.FromCategory;
				value2 = productObj.ToCategory;
				break;
			case "radioButtonBrand":
				if (productObj.FromBrand == "" || productObj.FromBrand == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "BrandID Between '" + productObj.FromBrand + "' AND '" + productObj.ToBrand + "' ";
				value = productObj.FromBrand;
				value2 = productObj.ToBrand;
				break;
			case "radioButtonManufacturer":
				if (productObj.FromManufacturer == "" || productObj.FromManufacturer == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "ManufacturerID Between '" + productObj.FromManufacturer + "' AND '" + productObj.ToManufacturer + "' ";
				value = productObj.FromManufacturer;
				value2 = productObj.ToManufacturer;
				break;
			case "radioButtonOrigin":
				if (productObj.FromOrigin == "" || productObj.FromOrigin == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "Origin Between '" + productObj.FromOrigin + "' AND '" + productObj.ToOrigin + "' ";
				value = productObj.FromOrigin;
				value2 = productObj.ToOrigin;
				break;
			case "radioButtonStyle":
				if (productObj.FromStyle == "" || productObj.FromStyle == "")
				{
					ErrorHelper.InformationMessage("Please select filter.");
					return;
				}
				text = "StyleID Between '" + productObj.FromStyle + "' AND '" + productObj.ToStyle + "' ";
				value = productObj.FromStyle;
				value2 = productObj.ToStyle;
				break;
			}
			if (dataGridCards.ActiveCell != null && text != "")
			{
				dataGridCards.ActiveRow.Cells["ConditionalQuery"].Value = text;
				dataGridCards.ActiveRow.Cells["FilterControl"].Value = radioButton.Name;
				dataGridCards.ActiveRow.Cells["FilterFrom"].Value = value;
				dataGridCards.ActiveRow.Cells["FilterTo"].Value = value2;
			}
			productFormObj.Close();
		}

		private void FillCardSecurity(string filterControl, string fromFilter, string toFilter)
		{
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.AccessLevelAssignForm));
			tabPageControlOther = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label1 = new System.Windows.Forms.Label();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label3 = new System.Windows.Forms.Label();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label5 = new System.Windows.Forms.Label();
			tabPageDashboard = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label4 = new System.Windows.Forms.Label();
			tabPageCustomReports = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label6 = new System.Windows.Forms.Label();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label7 = new System.Windows.Forms.Label();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label8 = new System.Windows.Forms.Label();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label9 = new System.Windows.Forms.Label();
			panelButtons = new System.Windows.Forms.Panel();
			labelUserName = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			checkBoxReport = new System.Windows.Forms.CheckBox();
			checkBoxFull = new System.Windows.Forms.CheckBox();
			comboBoxUserGroup = new Micromind.DataControls.UserGroupComboBox();
			dataGridMenu = new Micromind.DataControls.DataEntryGrid();
			textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
			dataGridForms = new Micromind.DataControls.DataEntryGrid();
			dataGridGeneral = new Micromind.DataControls.DataEntryGrid();
			dataGridGadgets = new Micromind.DataControls.DataEntryGrid();
			dataGridCustomReport = new Micromind.DataControls.DataEntryGrid();
			dataGridSmartList = new Micromind.DataControls.DataEntryGrid();
			dataGridPivotReport = new Micromind.DataControls.DataEntryGrid();
			dataGridExternalReport = new Micromind.DataControls.DataEntryGrid();
			comboBoxUser = new Micromind.DataControls.UserComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxUserName = new Micromind.UISupport.MMTextBox();
			labelUserID = new Micromind.UISupport.MMLabel();
			buttonReport = new Micromind.UISupport.XPButton();
			buttonCopy = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label10 = new System.Windows.Forms.Label();
			dataGridCards = new Micromind.DataControls.DataEntryGrid();
			tabPageControlOther.SuspendLayout();
			tabPageGeneral.SuspendLayout();
			tabPageDetails.SuspendLayout();
			tabPageDashboard.SuspendLayout();
			tabPageCustomReports.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridMenu).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridForms).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridGeneral).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridGadgets).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridCustomReport).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridSmartList).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridPivotReport).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridExternalReport).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCards).BeginInit();
			SuspendLayout();
			tabPageControlOther.Controls.Add(label1);
			tabPageControlOther.Controls.Add(dataGridGeneral);
			tabPageControlOther.Location = new System.Drawing.Point(-10000, -10000);
			tabPageControlOther.Name = "tabPageControlOther";
			tabPageControlOther.Size = new System.Drawing.Size(696, 341);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(11, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(260, 13);
			label1.TabIndex = 24;
			label1.Text = "Select the restrictions you want to apply on screens:";
			tabPageGeneral.Controls.Add(dataGridMenu);
			tabPageGeneral.Controls.Add(label3);
			tabPageGeneral.Controls.Add(textBoxTotalSalary);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(696, 341);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(11, 14);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(254, 13);
			label3.TabIndex = 20;
			label3.Text = "Select the restrictions you want to apply on menus:";
			tabPageDetails.Controls.Add(label5);
			tabPageDetails.Controls.Add(dataGridForms);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(696, 341);
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(16, 14);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(260, 13);
			label5.TabIndex = 22;
			label5.Text = "Select the restrictions you want to apply on screens:";
			tabPageDashboard.Controls.Add(label4);
			tabPageDashboard.Controls.Add(dataGridGadgets);
			tabPageDashboard.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDashboard.Name = "tabPageDashboard";
			tabPageDashboard.Size = new System.Drawing.Size(696, 341);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(11, 14);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(260, 13);
			label4.TabIndex = 26;
			label4.Text = "Select the restrictions you want to apply on screens:";
			tabPageCustomReports.Controls.Add(label6);
			tabPageCustomReports.Controls.Add(dataGridCustomReport);
			tabPageCustomReports.Location = new System.Drawing.Point(-10000, -10000);
			tabPageCustomReports.Name = "tabPageCustomReports";
			tabPageCustomReports.Size = new System.Drawing.Size(696, 341);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(11, 14);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(260, 13);
			label6.TabIndex = 28;
			label6.Text = "Select the restrictions you want to apply on screens:";
			ultraTabPageControl1.Controls.Add(label7);
			ultraTabPageControl1.Controls.Add(dataGridSmartList);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(696, 341);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(11, 14);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(260, 13);
			label7.TabIndex = 30;
			label7.Text = "Select the restrictions you want to apply on screens:";
			ultraTabPageControl2.Controls.Add(label8);
			ultraTabPageControl2.Controls.Add(dataGridPivotReport);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 341);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(11, 14);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(260, 13);
			label8.TabIndex = 30;
			label8.Text = "Select the restrictions you want to apply on screens:";
			ultraTabPageControl3.Controls.Add(label9);
			ultraTabPageControl3.Controls.Add(dataGridExternalReport);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(696, 341);
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(13, 11);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(260, 13);
			label9.TabIndex = 32;
			label9.Text = "Select the restrictions you want to apply on screens:";
			panelButtons.Controls.Add(buttonReport);
			panelButtons.Controls.Add(buttonCopy);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 465);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(723, 40);
			panelButtons.TabIndex = 3;
			labelUserName.AutoSize = true;
			labelUserName.Location = new System.Drawing.Point(8, 34);
			labelUserName.Name = "labelUserName";
			labelUserName.Size = new System.Drawing.Size(63, 13);
			labelUserName.TabIndex = 18;
			labelUserName.Text = "User Name:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(201, 13);
			label2.TabIndex = 20;
			label2.Text = "Define the access rights and restrictions :";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageControlOther);
			ultraTabControl1.Controls.Add(tabPageDashboard);
			ultraTabControl1.Controls.Add(tabPageCustomReports);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControl1.Location = new System.Drawing.Point(11, 97);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(698, 362);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 2;
			ultraTab.TabPage = tabPageControlOther;
			ultraTab.Text = "General";
			appearance.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab2.Appearance = appearance;
			ultraTab2.TabPage = tabPageGeneral;
			ultraTab2.Text = "Menus";
			ultraTab3.TabPage = tabPageDetails;
			ultraTab3.Text = "Screens";
			ultraTab4.TabPage = tabPageDashboard;
			ultraTab4.Text = "Dashboard";
			ultraTab5.TabPage = tabPageCustomReports;
			ultraTab5.Text = "Custom Reports";
			ultraTab6.TabPage = ultraTabPageControl4;
			ultraTab6.Text = "Cards";
			ultraTab7.TabPage = ultraTabPageControl1;
			ultraTab7.Text = "Smart List";
			ultraTab8.TabPage = ultraTabPageControl2;
			ultraTab8.Text = "Pivot Reports";
			ultraTab9.TabPage = ultraTabPageControl3;
			ultraTab9.Text = "External Reports";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[9]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(696, 341);
			checkBoxReport.AutoSize = true;
			checkBoxReport.Location = new System.Drawing.Point(453, 30);
			checkBoxReport.Name = "checkBoxReport";
			checkBoxReport.Size = new System.Drawing.Size(105, 17);
			checkBoxReport.TabIndex = 21;
			checkBoxReport.Text = "Generate Report";
			checkBoxReport.UseVisualStyleBackColor = true;
			checkBoxReport.Visible = false;
			checkBoxReport.CheckedChanged += new System.EventHandler(checkBoxReport_CheckedChanged);
			checkBoxFull.AutoSize = true;
			checkBoxFull.Location = new System.Drawing.Point(565, 31);
			checkBoxFull.Name = "checkBoxFull";
			checkBoxFull.Size = new System.Drawing.Size(42, 17);
			checkBoxFull.TabIndex = 22;
			checkBoxFull.Text = "Full";
			checkBoxFull.UseVisualStyleBackColor = true;
			checkBoxFull.Visible = false;
			comboBoxUserGroup.AlwaysInEditMode = true;
			comboBoxUserGroup.Assigned = false;
			comboBoxUserGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUserGroup.CustomReportFieldName = "";
			comboBoxUserGroup.CustomReportKey = "";
			comboBoxUserGroup.CustomReportValueType = 1;
			comboBoxUserGroup.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUserGroup.DisplayLayout.Appearance = appearance2;
			comboBoxUserGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUserGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxUserGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUserGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUserGroup.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxUserGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUserGroup.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxUserGroup.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxUserGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUserGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxUserGroup.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxUserGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUserGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxUserGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUserGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUserGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUserGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUserGroup.Editable = true;
			comboBoxUserGroup.FilterString = "";
			comboBoxUserGroup.HasAllAccount = false;
			comboBoxUserGroup.HasCustom = false;
			comboBoxUserGroup.IsDataLoaded = false;
			comboBoxUserGroup.Location = new System.Drawing.Point(90, 9);
			comboBoxUserGroup.MaxDropDownItems = 12;
			comboBoxUserGroup.Name = "comboBoxUserGroup";
			comboBoxUserGroup.ShowInactiveItems = false;
			comboBoxUserGroup.ShowQuickAdd = true;
			comboBoxUserGroup.Size = new System.Drawing.Size(179, 20);
			comboBoxUserGroup.TabIndex = 0;
			comboBoxUserGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUserGroup.Visible = false;
			dataGridMenu.AllowAddNew = false;
			dataGridMenu.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridMenu.DisplayLayout.Appearance = appearance14;
			dataGridMenu.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridMenu.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMenu.DisplayLayout.GroupByBox.Appearance = appearance15;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMenu.DisplayLayout.GroupByBox.BandLabelAppearance = appearance16;
			dataGridMenu.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance17.BackColor2 = System.Drawing.SystemColors.Control;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMenu.DisplayLayout.GroupByBox.PromptAppearance = appearance17;
			dataGridMenu.DisplayLayout.MaxColScrollRegions = 1;
			dataGridMenu.DisplayLayout.MaxRowScrollRegions = 1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridMenu.DisplayLayout.Override.ActiveCellAppearance = appearance18;
			appearance19.BackColor = System.Drawing.SystemColors.Highlight;
			appearance19.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridMenu.DisplayLayout.Override.ActiveRowAppearance = appearance19;
			dataGridMenu.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridMenu.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridMenu.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			dataGridMenu.DisplayLayout.Override.CardAreaAppearance = appearance20;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			appearance21.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridMenu.DisplayLayout.Override.CellAppearance = appearance21;
			dataGridMenu.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridMenu.DisplayLayout.Override.CellPadding = 0;
			appearance22.BackColor = System.Drawing.SystemColors.Control;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMenu.DisplayLayout.Override.GroupByRowAppearance = appearance22;
			appearance23.TextHAlignAsString = "Left";
			dataGridMenu.DisplayLayout.Override.HeaderAppearance = appearance23;
			dataGridMenu.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridMenu.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			dataGridMenu.DisplayLayout.Override.RowAppearance = appearance24;
			dataGridMenu.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridMenu.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
			dataGridMenu.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridMenu.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridMenu.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridMenu.IncludeLotItems = false;
			dataGridMenu.LoadLayoutFailed = false;
			dataGridMenu.Location = new System.Drawing.Point(14, 33);
			dataGridMenu.Name = "dataGridMenu";
			dataGridMenu.ShowClearMenu = true;
			dataGridMenu.ShowDeleteMenu = true;
			dataGridMenu.ShowInsertMenu = true;
			dataGridMenu.ShowMoveRowsMenu = true;
			dataGridMenu.Size = new System.Drawing.Size(666, 299);
			dataGridMenu.TabIndex = 17;
			dataGridMenu.Text = "dataEntryGrid1";
			textBoxTotalSalary.AllowDecimal = true;
			textBoxTotalSalary.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalSalary.CustomReportFieldName = "";
			textBoxTotalSalary.CustomReportKey = "";
			textBoxTotalSalary.CustomReportValueType = 1;
			textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
			textBoxTotalSalary.IsComboTextBox = false;
			textBoxTotalSalary.IsModified = false;
			textBoxTotalSalary.Location = new System.Drawing.Point(756, 319);
			textBoxTotalSalary.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalSalary.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalSalary.Name = "textBoxTotalSalary";
			textBoxTotalSalary.NullText = "0";
			textBoxTotalSalary.ReadOnly = true;
			textBoxTotalSalary.Size = new System.Drawing.Size(114, 21);
			textBoxTotalSalary.TabIndex = 9;
			textBoxTotalSalary.TabStop = false;
			textBoxTotalSalary.Text = "0.00";
			textBoxTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalSalary.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			dataGridForms.AllowAddNew = false;
			dataGridForms.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridForms.DisplayLayout.Appearance = appearance26;
			dataGridForms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridForms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			dataGridForms.DisplayLayout.GroupByBox.Appearance = appearance27;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridForms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance28;
			dataGridForms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance29.BackColor2 = System.Drawing.SystemColors.Control;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridForms.DisplayLayout.GroupByBox.PromptAppearance = appearance29;
			dataGridForms.DisplayLayout.MaxColScrollRegions = 1;
			dataGridForms.DisplayLayout.MaxRowScrollRegions = 1;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridForms.DisplayLayout.Override.ActiveCellAppearance = appearance30;
			appearance31.BackColor = System.Drawing.SystemColors.Highlight;
			appearance31.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridForms.DisplayLayout.Override.ActiveRowAppearance = appearance31;
			dataGridForms.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridForms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridForms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			dataGridForms.DisplayLayout.Override.CardAreaAppearance = appearance32;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			appearance33.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridForms.DisplayLayout.Override.CellAppearance = appearance33;
			dataGridForms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridForms.DisplayLayout.Override.CellPadding = 0;
			appearance34.BackColor = System.Drawing.SystemColors.Control;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			dataGridForms.DisplayLayout.Override.GroupByRowAppearance = appearance34;
			appearance35.TextHAlignAsString = "Left";
			dataGridForms.DisplayLayout.Override.HeaderAppearance = appearance35;
			dataGridForms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridForms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			dataGridForms.DisplayLayout.Override.RowAppearance = appearance36;
			dataGridForms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridForms.DisplayLayout.Override.TemplateAddRowAppearance = appearance37;
			dataGridForms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridForms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridForms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridForms.IncludeLotItems = false;
			dataGridForms.LoadLayoutFailed = false;
			dataGridForms.Location = new System.Drawing.Point(14, 30);
			dataGridForms.Name = "dataGridForms";
			dataGridForms.ShowClearMenu = true;
			dataGridForms.ShowDeleteMenu = true;
			dataGridForms.ShowInsertMenu = true;
			dataGridForms.ShowMoveRowsMenu = true;
			dataGridForms.Size = new System.Drawing.Size(668, 302);
			dataGridForms.TabIndex = 18;
			dataGridForms.Text = "dataEntryGrid1";
			dataGridGeneral.AllowAddNew = false;
			dataGridGeneral.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridGeneral.DisplayLayout.Appearance = appearance38;
			dataGridGeneral.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridGeneral.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGeneral.DisplayLayout.GroupByBox.Appearance = appearance39;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGeneral.DisplayLayout.GroupByBox.BandLabelAppearance = appearance40;
			dataGridGeneral.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance41.BackColor2 = System.Drawing.SystemColors.Control;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGeneral.DisplayLayout.GroupByBox.PromptAppearance = appearance41;
			dataGridGeneral.DisplayLayout.MaxColScrollRegions = 1;
			dataGridGeneral.DisplayLayout.MaxRowScrollRegions = 1;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridGeneral.DisplayLayout.Override.ActiveCellAppearance = appearance42;
			appearance43.BackColor = System.Drawing.SystemColors.Highlight;
			appearance43.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridGeneral.DisplayLayout.Override.ActiveRowAppearance = appearance43;
			dataGridGeneral.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridGeneral.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridGeneral.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			dataGridGeneral.DisplayLayout.Override.CardAreaAppearance = appearance44;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			appearance45.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridGeneral.DisplayLayout.Override.CellAppearance = appearance45;
			dataGridGeneral.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridGeneral.DisplayLayout.Override.CellPadding = 0;
			appearance46.BackColor = System.Drawing.SystemColors.Control;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGeneral.DisplayLayout.Override.GroupByRowAppearance = appearance46;
			appearance47.TextHAlignAsString = "Left";
			dataGridGeneral.DisplayLayout.Override.HeaderAppearance = appearance47;
			dataGridGeneral.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridGeneral.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			dataGridGeneral.DisplayLayout.Override.RowAppearance = appearance48;
			dataGridGeneral.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridGeneral.DisplayLayout.Override.TemplateAddRowAppearance = appearance49;
			dataGridGeneral.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridGeneral.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridGeneral.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridGeneral.IncludeLotItems = false;
			dataGridGeneral.LoadLayoutFailed = false;
			dataGridGeneral.Location = new System.Drawing.Point(14, 33);
			dataGridGeneral.Name = "dataGridGeneral";
			dataGridGeneral.ShowClearMenu = true;
			dataGridGeneral.ShowDeleteMenu = true;
			dataGridGeneral.ShowInsertMenu = true;
			dataGridGeneral.ShowMoveRowsMenu = true;
			dataGridGeneral.Size = new System.Drawing.Size(668, 299);
			dataGridGeneral.TabIndex = 23;
			dataGridGeneral.Text = "dataEntryGrid1";
			dataGridGeneral.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(dataGridGeneral_AfterCellUpdate);
			dataGridGadgets.AllowAddNew = false;
			dataGridGadgets.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridGadgets.DisplayLayout.Appearance = appearance50;
			dataGridGadgets.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridGadgets.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGadgets.DisplayLayout.GroupByBox.Appearance = appearance51;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGadgets.DisplayLayout.GroupByBox.BandLabelAppearance = appearance52;
			dataGridGadgets.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance53.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance53.BackColor2 = System.Drawing.SystemColors.Control;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridGadgets.DisplayLayout.GroupByBox.PromptAppearance = appearance53;
			dataGridGadgets.DisplayLayout.MaxColScrollRegions = 1;
			dataGridGadgets.DisplayLayout.MaxRowScrollRegions = 1;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridGadgets.DisplayLayout.Override.ActiveCellAppearance = appearance54;
			appearance55.BackColor = System.Drawing.SystemColors.Highlight;
			appearance55.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridGadgets.DisplayLayout.Override.ActiveRowAppearance = appearance55;
			dataGridGadgets.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridGadgets.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridGadgets.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			dataGridGadgets.DisplayLayout.Override.CardAreaAppearance = appearance56;
			appearance57.BorderColor = System.Drawing.Color.Silver;
			appearance57.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridGadgets.DisplayLayout.Override.CellAppearance = appearance57;
			dataGridGadgets.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridGadgets.DisplayLayout.Override.CellPadding = 0;
			appearance58.BackColor = System.Drawing.SystemColors.Control;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			dataGridGadgets.DisplayLayout.Override.GroupByRowAppearance = appearance58;
			appearance59.TextHAlignAsString = "Left";
			dataGridGadgets.DisplayLayout.Override.HeaderAppearance = appearance59;
			dataGridGadgets.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridGadgets.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			dataGridGadgets.DisplayLayout.Override.RowAppearance = appearance60;
			dataGridGadgets.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridGadgets.DisplayLayout.Override.TemplateAddRowAppearance = appearance61;
			dataGridGadgets.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridGadgets.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridGadgets.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridGadgets.IncludeLotItems = false;
			dataGridGadgets.LoadLayoutFailed = false;
			dataGridGadgets.Location = new System.Drawing.Point(14, 33);
			dataGridGadgets.Name = "dataGridGadgets";
			dataGridGadgets.ShowClearMenu = true;
			dataGridGadgets.ShowDeleteMenu = true;
			dataGridGadgets.ShowInsertMenu = true;
			dataGridGadgets.ShowMoveRowsMenu = true;
			dataGridGadgets.Size = new System.Drawing.Size(668, 299);
			dataGridGadgets.TabIndex = 25;
			dataGridGadgets.Text = "dataEntryGrid1";
			dataGridCustomReport.AllowAddNew = false;
			dataGridCustomReport.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCustomReport.DisplayLayout.Appearance = appearance62;
			dataGridCustomReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCustomReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance63.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomReport.DisplayLayout.GroupByBox.Appearance = appearance63;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance64;
			dataGridCustomReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance65.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance65.BackColor2 = System.Drawing.SystemColors.Control;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCustomReport.DisplayLayout.GroupByBox.PromptAppearance = appearance65;
			dataGridCustomReport.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCustomReport.DisplayLayout.MaxRowScrollRegions = 1;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCustomReport.DisplayLayout.Override.ActiveCellAppearance = appearance66;
			appearance67.BackColor = System.Drawing.SystemColors.Highlight;
			appearance67.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCustomReport.DisplayLayout.Override.ActiveRowAppearance = appearance67;
			dataGridCustomReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridCustomReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCustomReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			dataGridCustomReport.DisplayLayout.Override.CardAreaAppearance = appearance68;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			appearance69.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCustomReport.DisplayLayout.Override.CellAppearance = appearance69;
			dataGridCustomReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCustomReport.DisplayLayout.Override.CellPadding = 0;
			appearance70.BackColor = System.Drawing.SystemColors.Control;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCustomReport.DisplayLayout.Override.GroupByRowAppearance = appearance70;
			appearance71.TextHAlignAsString = "Left";
			dataGridCustomReport.DisplayLayout.Override.HeaderAppearance = appearance71;
			dataGridCustomReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCustomReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			dataGridCustomReport.DisplayLayout.Override.RowAppearance = appearance72;
			dataGridCustomReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance73.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCustomReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance73;
			dataGridCustomReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCustomReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCustomReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCustomReport.IncludeLotItems = false;
			dataGridCustomReport.LoadLayoutFailed = false;
			dataGridCustomReport.Location = new System.Drawing.Point(14, 33);
			dataGridCustomReport.Name = "dataGridCustomReport";
			dataGridCustomReport.ShowClearMenu = true;
			dataGridCustomReport.ShowDeleteMenu = true;
			dataGridCustomReport.ShowInsertMenu = true;
			dataGridCustomReport.ShowMoveRowsMenu = true;
			dataGridCustomReport.Size = new System.Drawing.Size(668, 299);
			dataGridCustomReport.TabIndex = 27;
			dataGridCustomReport.Text = "dataEntryGrid1";
			dataGridSmartList.AllowAddNew = false;
			dataGridSmartList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSmartList.DisplayLayout.Appearance = appearance74;
			dataGridSmartList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSmartList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSmartList.DisplayLayout.GroupByBox.Appearance = appearance75;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSmartList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance76;
			dataGridSmartList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance77.BackColor2 = System.Drawing.SystemColors.Control;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSmartList.DisplayLayout.GroupByBox.PromptAppearance = appearance77;
			dataGridSmartList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSmartList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSmartList.DisplayLayout.Override.ActiveCellAppearance = appearance78;
			appearance79.BackColor = System.Drawing.SystemColors.Highlight;
			appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSmartList.DisplayLayout.Override.ActiveRowAppearance = appearance79;
			dataGridSmartList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSmartList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSmartList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			dataGridSmartList.DisplayLayout.Override.CardAreaAppearance = appearance80;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			appearance81.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSmartList.DisplayLayout.Override.CellAppearance = appearance81;
			dataGridSmartList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSmartList.DisplayLayout.Override.CellPadding = 0;
			appearance82.BackColor = System.Drawing.SystemColors.Control;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSmartList.DisplayLayout.Override.GroupByRowAppearance = appearance82;
			appearance83.TextHAlignAsString = "Left";
			dataGridSmartList.DisplayLayout.Override.HeaderAppearance = appearance83;
			dataGridSmartList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSmartList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			dataGridSmartList.DisplayLayout.Override.RowAppearance = appearance84;
			dataGridSmartList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSmartList.DisplayLayout.Override.TemplateAddRowAppearance = appearance85;
			dataGridSmartList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSmartList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSmartList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSmartList.IncludeLotItems = false;
			dataGridSmartList.LoadLayoutFailed = false;
			dataGridSmartList.Location = new System.Drawing.Point(14, 33);
			dataGridSmartList.Name = "dataGridSmartList";
			dataGridSmartList.ShowClearMenu = true;
			dataGridSmartList.ShowDeleteMenu = true;
			dataGridSmartList.ShowInsertMenu = true;
			dataGridSmartList.ShowMoveRowsMenu = true;
			dataGridSmartList.Size = new System.Drawing.Size(668, 299);
			dataGridSmartList.TabIndex = 29;
			dataGridSmartList.Text = "dataEntryGrid1";
			dataGridPivotReport.AllowAddNew = false;
			dataGridPivotReport.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPivotReport.DisplayLayout.Appearance = appearance86;
			dataGridPivotReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPivotReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPivotReport.DisplayLayout.GroupByBox.Appearance = appearance87;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPivotReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance88;
			dataGridPivotReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance89.BackColor2 = System.Drawing.SystemColors.Control;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPivotReport.DisplayLayout.GroupByBox.PromptAppearance = appearance89;
			dataGridPivotReport.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPivotReport.DisplayLayout.MaxRowScrollRegions = 1;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPivotReport.DisplayLayout.Override.ActiveCellAppearance = appearance90;
			appearance91.BackColor = System.Drawing.SystemColors.Highlight;
			appearance91.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPivotReport.DisplayLayout.Override.ActiveRowAppearance = appearance91;
			dataGridPivotReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPivotReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPivotReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			dataGridPivotReport.DisplayLayout.Override.CardAreaAppearance = appearance92;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			appearance93.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPivotReport.DisplayLayout.Override.CellAppearance = appearance93;
			dataGridPivotReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPivotReport.DisplayLayout.Override.CellPadding = 0;
			appearance94.BackColor = System.Drawing.SystemColors.Control;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPivotReport.DisplayLayout.Override.GroupByRowAppearance = appearance94;
			appearance95.TextHAlignAsString = "Left";
			dataGridPivotReport.DisplayLayout.Override.HeaderAppearance = appearance95;
			dataGridPivotReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPivotReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			dataGridPivotReport.DisplayLayout.Override.RowAppearance = appearance96;
			dataGridPivotReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPivotReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance97;
			dataGridPivotReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPivotReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPivotReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPivotReport.IncludeLotItems = false;
			dataGridPivotReport.LoadLayoutFailed = false;
			dataGridPivotReport.Location = new System.Drawing.Point(14, 33);
			dataGridPivotReport.Name = "dataGridPivotReport";
			dataGridPivotReport.ShowClearMenu = true;
			dataGridPivotReport.ShowDeleteMenu = true;
			dataGridPivotReport.ShowInsertMenu = true;
			dataGridPivotReport.ShowMoveRowsMenu = true;
			dataGridPivotReport.Size = new System.Drawing.Size(668, 299);
			dataGridPivotReport.TabIndex = 29;
			dataGridPivotReport.Text = "dataEntryGrid1";
			dataGridExternalReport.AllowAddNew = false;
			dataGridExternalReport.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExternalReport.DisplayLayout.Appearance = appearance98;
			dataGridExternalReport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExternalReport.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExternalReport.DisplayLayout.GroupByBox.Appearance = appearance99;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExternalReport.DisplayLayout.GroupByBox.BandLabelAppearance = appearance100;
			dataGridExternalReport.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance101.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance101.BackColor2 = System.Drawing.SystemColors.Control;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExternalReport.DisplayLayout.GroupByBox.PromptAppearance = appearance101;
			dataGridExternalReport.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExternalReport.DisplayLayout.MaxRowScrollRegions = 1;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExternalReport.DisplayLayout.Override.ActiveCellAppearance = appearance102;
			appearance103.BackColor = System.Drawing.SystemColors.Highlight;
			appearance103.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExternalReport.DisplayLayout.Override.ActiveRowAppearance = appearance103;
			dataGridExternalReport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridExternalReport.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExternalReport.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			dataGridExternalReport.DisplayLayout.Override.CardAreaAppearance = appearance104;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			appearance105.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExternalReport.DisplayLayout.Override.CellAppearance = appearance105;
			dataGridExternalReport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExternalReport.DisplayLayout.Override.CellPadding = 0;
			appearance106.BackColor = System.Drawing.SystemColors.Control;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExternalReport.DisplayLayout.Override.GroupByRowAppearance = appearance106;
			appearance107.TextHAlignAsString = "Left";
			dataGridExternalReport.DisplayLayout.Override.HeaderAppearance = appearance107;
			dataGridExternalReport.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExternalReport.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			dataGridExternalReport.DisplayLayout.Override.RowAppearance = appearance108;
			dataGridExternalReport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExternalReport.DisplayLayout.Override.TemplateAddRowAppearance = appearance109;
			dataGridExternalReport.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExternalReport.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExternalReport.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExternalReport.IncludeLotItems = false;
			dataGridExternalReport.LoadLayoutFailed = false;
			dataGridExternalReport.Location = new System.Drawing.Point(16, 30);
			dataGridExternalReport.Name = "dataGridExternalReport";
			dataGridExternalReport.ShowClearMenu = true;
			dataGridExternalReport.ShowDeleteMenu = true;
			dataGridExternalReport.ShowInsertMenu = true;
			dataGridExternalReport.ShowMoveRowsMenu = true;
			dataGridExternalReport.Size = new System.Drawing.Size(668, 299);
			dataGridExternalReport.TabIndex = 31;
			dataGridExternalReport.Text = "dataEntryGrid1";
			comboBoxUser.AlwaysInEditMode = true;
			comboBoxUser.Assigned = false;
			comboBoxUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUser.CustomReportFieldName = "";
			comboBoxUser.CustomReportKey = "";
			comboBoxUser.CustomReportValueType = 1;
			comboBoxUser.DescriptionTextBox = null;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUser.DisplayLayout.Appearance = appearance110;
			comboBoxUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance111.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.GroupByBox.Appearance = appearance111;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance112;
			comboBoxUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance113.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance113.BackColor2 = System.Drawing.SystemColors.Control;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.PromptAppearance = appearance113;
			comboBoxUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance114.BackColor = System.Drawing.SystemColors.Window;
			appearance114.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUser.DisplayLayout.Override.ActiveCellAppearance = appearance114;
			appearance115.BackColor = System.Drawing.SystemColors.Highlight;
			appearance115.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUser.DisplayLayout.Override.ActiveRowAppearance = appearance115;
			comboBoxUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.CardAreaAppearance = appearance116;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			appearance117.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUser.DisplayLayout.Override.CellAppearance = appearance117;
			comboBoxUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUser.DisplayLayout.Override.CellPadding = 0;
			appearance118.BackColor = System.Drawing.SystemColors.Control;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.GroupByRowAppearance = appearance118;
			appearance119.TextHAlignAsString = "Left";
			comboBoxUser.DisplayLayout.Override.HeaderAppearance = appearance119;
			comboBoxUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance120.BackColor = System.Drawing.SystemColors.Window;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			comboBoxUser.DisplayLayout.Override.RowAppearance = appearance120;
			comboBoxUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance121;
			comboBoxUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUser.Editable = true;
			comboBoxUser.FilterString = "";
			comboBoxUser.HasAllAccount = false;
			comboBoxUser.HasCustom = false;
			comboBoxUser.IsDataLoaded = false;
			comboBoxUser.Location = new System.Drawing.Point(90, 9);
			comboBoxUser.MaxDropDownItems = 12;
			comboBoxUser.Name = "comboBoxUser";
			comboBoxUser.ShowInactiveItems = false;
			comboBoxUser.ShowQuickAdd = true;
			comboBoxUser.Size = new System.Drawing.Size(179, 20);
			comboBoxUser.TabIndex = 0;
			comboBoxUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxUserName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUserName.CustomReportFieldName = "";
			textBoxUserName.CustomReportKey = "";
			textBoxUserName.CustomReportValueType = 1;
			textBoxUserName.ForeColor = System.Drawing.Color.Black;
			textBoxUserName.IsComboTextBox = false;
			textBoxUserName.IsModified = false;
			textBoxUserName.Location = new System.Drawing.Point(90, 31);
			textBoxUserName.MaxLength = 15;
			textBoxUserName.Name = "textBoxUserName";
			textBoxUserName.ReadOnly = true;
			textBoxUserName.Size = new System.Drawing.Size(290, 20);
			textBoxUserName.TabIndex = 1;
			labelUserID.AutoSize = true;
			labelUserID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelUserID.IsFieldHeader = false;
			labelUserID.IsRequired = true;
			labelUserID.Location = new System.Drawing.Point(8, 9);
			labelUserID.Name = "labelUserID";
			labelUserID.PenWidth = 1f;
			labelUserID.ShowBorder = false;
			labelUserID.Size = new System.Drawing.Size(54, 13);
			labelUserID.TabIndex = 0;
			labelUserID.Text = "User ID:";
			buttonReport.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonReport.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonReport.BackColor = System.Drawing.Color.DarkGray;
			buttonReport.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonReport.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonReport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonReport.Location = new System.Drawing.Point(471, 8);
			buttonReport.Name = "buttonReport";
			buttonReport.Size = new System.Drawing.Size(96, 24);
			buttonReport.TabIndex = 16;
			buttonReport.Text = "&Report";
			buttonReport.UseVisualStyleBackColor = false;
			buttonReport.Click += new System.EventHandler(buttonReport_Click);
			buttonCopy.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCopy.BackColor = System.Drawing.Color.Silver;
			buttonCopy.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCopy.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCopy.Location = new System.Drawing.Point(117, 8);
			buttonCopy.Name = "buttonCopy";
			buttonCopy.Size = new System.Drawing.Size(96, 24);
			buttonCopy.TabIndex = 15;
			buttonCopy.Text = "Copy From....";
			buttonCopy.UseVisualStyleBackColor = false;
			buttonCopy.Click += new System.EventHandler(buttonCopy_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(723, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(613, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabPageControl4.Controls.Add(label10);
			ultraTabPageControl4.Controls.Add(dataGridCards);
			ultraTabPageControl4.Location = new System.Drawing.Point(1, 20);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(696, 341);
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(11, 14);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(260, 13);
			label10.TabIndex = 30;
			label10.Text = "Select the restrictions you want to apply on screens:";
			dataGridCards.AllowAddNew = false;
			dataGridCards.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCards.DisplayLayout.Appearance = appearance122;
			dataGridCards.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCards.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance123.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance123.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCards.DisplayLayout.GroupByBox.Appearance = appearance123;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCards.DisplayLayout.GroupByBox.BandLabelAppearance = appearance124;
			dataGridCards.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance125.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance125.BackColor2 = System.Drawing.SystemColors.Control;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCards.DisplayLayout.GroupByBox.PromptAppearance = appearance125;
			dataGridCards.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCards.DisplayLayout.MaxRowScrollRegions = 1;
			appearance126.BackColor = System.Drawing.SystemColors.Window;
			appearance126.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCards.DisplayLayout.Override.ActiveCellAppearance = appearance126;
			appearance127.BackColor = System.Drawing.SystemColors.Highlight;
			appearance127.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCards.DisplayLayout.Override.ActiveRowAppearance = appearance127;
			dataGridCards.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridCards.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCards.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			dataGridCards.DisplayLayout.Override.CardAreaAppearance = appearance128;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			appearance129.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCards.DisplayLayout.Override.CellAppearance = appearance129;
			dataGridCards.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCards.DisplayLayout.Override.CellPadding = 0;
			appearance130.BackColor = System.Drawing.SystemColors.Control;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCards.DisplayLayout.Override.GroupByRowAppearance = appearance130;
			appearance131.TextHAlignAsString = "Left";
			dataGridCards.DisplayLayout.Override.HeaderAppearance = appearance131;
			dataGridCards.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCards.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance132.BackColor = System.Drawing.SystemColors.Window;
			appearance132.BorderColor = System.Drawing.Color.Silver;
			dataGridCards.DisplayLayout.Override.RowAppearance = appearance132;
			dataGridCards.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCards.DisplayLayout.Override.TemplateAddRowAppearance = appearance133;
			dataGridCards.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCards.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCards.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCards.IncludeLotItems = false;
			dataGridCards.LoadLayoutFailed = false;
			dataGridCards.Location = new System.Drawing.Point(14, 33);
			dataGridCards.Name = "dataGridCards";
			dataGridCards.ShowClearMenu = true;
			dataGridCards.ShowDeleteMenu = true;
			dataGridCards.ShowInsertMenu = true;
			dataGridCards.ShowMoveRowsMenu = true;
			dataGridCards.Size = new System.Drawing.Size(668, 299);
			dataGridCards.TabIndex = 29;
			dataGridCards.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(723, 505);
			base.Controls.Add(checkBoxFull);
			base.Controls.Add(checkBoxReport);
			base.Controls.Add(comboBoxUserGroup);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(label2);
			base.Controls.Add(labelUserName);
			base.Controls.Add(comboBoxUser);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxUserName);
			base.Controls.Add(labelUserID);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AccessLevelAssignForm";
			Text = "Access Right Setup";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AccessLevelAssignForm_Load);
			tabPageControlOther.ResumeLayout(false);
			tabPageControlOther.PerformLayout();
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			tabPageDashboard.ResumeLayout(false);
			tabPageDashboard.PerformLayout();
			tabPageCustomReports.ResumeLayout(false);
			tabPageCustomReports.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridMenu).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridForms).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridGeneral).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridGadgets).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridCustomReport).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridSmartList).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridPivotReport).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridExternalReport).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCards).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
