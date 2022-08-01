using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.DataControls.OtherControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class ConsignOutForm : Form, IForm
	{
		private bool allowEdit = true;

		private ConsignOutData currentData;

		private const string TABLENAME_CONST = "Consign_Out";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private bool isDNInventory;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private SysDocTypes sourceDocType = SysDocTypes.None;

		private bool allowMultiTemplate;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool isExport;

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ProductComboBox comboBoxGridItem;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxCustomerName;

		private MMTextBox textBoxBilltoAddress;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraButton buttonPrevBillto;

		private UltraButton buttonNextBillTo;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private CustomerAddressComboBox comboBoxShippingAddressID;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private Label labelSourceDocNumber;

		private TextBox textBoxSourceDocNumber;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem createFromPackingListToolStripMenuItem;

		private ToolStripMenuItem createFromDeliveryNoteToolStripMenuItem;

		private ToolStripButton toolStripButtonDistribution;

		private DocStatusLabel docStatusLabel;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private UltraFormattedLinkLabel ultraFormattedLinkMethod;

		private ToolStripButton toolStripButtonMultiPreview;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2007;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					enabled = (sysDocComboBox.Enabled = flag3);
					customersFlatComboBox.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					customersFlatComboBox customersFlatComboBox2 = comboBoxCustomer;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = false;
					bool enabled = sysDocComboBox2.Enabled = flag3;
					customersFlatComboBox2.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonMultiPreview.Visible = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
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

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				buttonSave.Enabled = !value;
				textBoxNote.Enabled = !value;
				labelVoided.Visible = value;
				if (value)
				{
					buttonVoid.Enabled = false;
					return;
				}
				buttonVoid.Text = UIMessages.Void;
				if (!IsNewRecord)
				{
					buttonVoid.Enabled = true;
				}
				else
				{
					buttonVoid.Enabled = false;
				}
			}
		}

		public ConsignOutForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.BeforeRowsDeleted += dataGridItems_BeforeRowsDeleted;
			comboBoxCustomer.ShowConsignmentOnly = true;
			if (CompanyPreferences.ExportSalesFlow == SalesFlows.SOThenDNThenInvoice)
			{
				isDNInventory = true;
				duplicateToolStripMenuItem.Enabled = false;
			}
		}

		private void dataGridItems_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
		{
			if (e.Rows[0].Cells["IsSourcedRow"].Value.ToString() != "")
			{
				ErrorHelper.WarningMessage("This row is sourced from another document and cannot be deleted.");
				e.Cancel = true;
			}
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			docStatusLabel.LinkClicked += docStatusLabel_LinkClicked;
		}

		private void docStatusLabel_LinkClicked(object sender, EventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			Control control = sender as Control;
			if (control != null)
			{
				formHelper.EditTransaction(TransactionListType.ConsignOutSettlement, control.Tag.ToString(), control.Text);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result2);
				int result3 = 1;
				int.TryParse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString(), out result3);
				ItemTypes itemTypes = (ItemTypes)checked((byte)result3);
				if (result2 < 0m && itemTypes != ItemTypes.Discount)
				{
					ErrorHelper.InformationMessage("Negative amount is not allowed. Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null || activeRow.Cells["Item Code"].Value == null || activeRow.Cells["Item Code"].Value.ToString() == "" || !isDNInventory || activeRow == null || !activeRow.DataChanged || CompanyPreferences.NegativeQuantityAction == 1)
			{
				return;
			}
			string sysDocID = "";
			string voucherID = "";
			if (!IsNewRecord)
			{
				sysDocID = comboBoxSysDoc.SelectedID;
				voucherID = textBoxVoucherNumber.Text;
			}
			if (Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Consign_Out_Detail", sysDocID, voucherID, decimal.Parse(activeRow.Cells["Quantity"].Value.ToString())))
			{
				return;
			}
			if (CompanyPreferences.NegativeQuantityAction == 2)
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
				{
					e.Row.Cells["Quantity"].Value = 0;
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
			}
			else if (CompanyPreferences.NegativeQuantityAction == 3)
			{
				ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
				e.Row.Cells["Quantity"].Value = 0;
				if (!e.Row.IsAddRow)
				{
					e.Cancel = true;
				}
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
			FormActivator.ImageViewerFormObj.Image = productPhotoViewer.Image;
			FormActivator.BringFormToFront(FormActivator.ImageViewerFormObj);
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			LoadCustomerBillingAddress();
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
				allowMultiTemplate = false;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowMultiTemplate", "SysDocID", comboBoxSysDoc.SelectedID);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					allowMultiTemplate = bool.Parse(fieldValue.ToString());
				}
				if (allowMultiTemplate)
				{
					toolStripButtonMultiPreview.Visible = true;
				}
				else
				{
					toolStripButtonMultiPreview.Visible = false;
				}
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null)
					{
						if (!(e.Cell.Column.Key == "Item Code"))
						{
							if (e.Cell.Column.Key == "Location" && e.Cell.Row.Cells["Quantity"].Tag != null)
							{
								e.Cell.Row.Cells["Quantity"].Tag = null;
								e.Cell.Row.Cells["Quantity"].Value = 0;
								e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
							}
							goto IL_03e5;
						}
						if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
						{
							comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
							goto IL_0096;
						}
						if (comboBoxGridItem.SelectedRow != null)
						{
							goto IL_0096;
						}
					}
					goto end_IL_0000;
					IL_0096:
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
					dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
					dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
					dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
					if (comboBoxGridItem.SelectedRow != null)
					{
						dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackLot"].Value.ToString();
					}
					else
					{
						dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = null;
					}
					if (comboBoxGridItem.SelectedRow != null)
					{
						dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackSerial"].Value.ToString();
					}
					else
					{
						dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = null;
					}
					dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
					dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
					if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
					{
						dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Location"].Value;
					}
					goto IL_03e5;
					IL_03e5:
					if (e.Cell.Column.Key == "Location")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["Location"].Value = e.Cell.Value.ToString();
							}
						}
					}
					end_IL_0000:;
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.IsLoadingData = true;
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = "";
				}
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Price")
				{
					decimal result = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
					result = Math.Round(result, 5);
					dataGridItems.ActiveCell.Value = result;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
				{
					decimal result2 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result2);
					result2 = Math.Round(result2, 4);
					dataGridItems.ActiveCell.Value = result2;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
				{
					decimal result3 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result3);
					result3 = Math.Round(result3, Global.CurDecimalPoints);
					dataGridItems.ActiveCell.Value = result3;
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["Item Code"].Activate();
				}
				else if (activeRow.Cells["Item Code"].Value.ToString() != "" && activeRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location.");
					e.Cancel = true;
					activeRow.Cells["Location"].Activate();
				}
				else if (activeRow.Cells["Quantity"].Value.ToString() == "")
				{
					activeRow.Cells["Quantity"].Value = 0;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (e.Cell.Column.Key == "Item Code" && dataGridItems.ExistCellValue("Item Code", e.NewValue.ToString()) >= 0)
			{
				ErrorHelper.WarningMessage("This item is already added.");
				e.Cancel = true;
				return;
			}
			if (activeRow != null && activeRow.DataChanged && activeRow.IsAddRow && e.Cell.Column.Key == "Quantity" && CompanyPreferences.NegativeQuantityAction != 1)
			{
				string sysDocID = "";
				string voucherID = "";
				if (!IsNewRecord)
				{
					sysDocID = comboBoxSysDoc.SelectedID;
					voucherID = textBoxVoucherNumber.Text;
				}
				if (!Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Consign_Out_Detail", sysDocID, voucherID, decimal.Parse(e.NewValue.ToString())))
				{
					if (CompanyPreferences.NegativeQuantityAction == 2)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
						{
							e.Cancel = true;
							return;
						}
					}
					else if (CompanyPreferences.NegativeQuantityAction == 3)
					{
						ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
						e.Cancel = true;
						return;
					}
				}
			}
			if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (dataGridItems.ActiveRow.Cells["IsTrackLot"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackLot"].Value.ToString(), out result);
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
			}
			if (result)
			{
				if (dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				issueLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
				issueLotSelectionForm.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
				issueLotSelectionForm.RowQuantity = float.Parse(dataGridItems.ActiveCell.Text);
				if (!isNewRecord)
				{
					issueLotSelectionForm.SysDocID = comboBoxSysDoc.SelectedID;
					issueLotSelectionForm.VoucherID = textBoxVoucherNumber.Text;
				}
				if (cell.Tag != null)
				{
					issueLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
				}
				if (issueLotSelectionForm.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				cell.Tag = issueLotSelectionForm.ProductLotTable;
				cell.Appearance.FontData.Underline = DefaultableBoolean.True;
			}
			return true;
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Item Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Cost")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "New Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Unit")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid UOM for the item.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid location for the item.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ConsignOutData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ConsignOutTable.Rows[0] : currentData.ConsignOutTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["PONumber"] = textBoxSourceDocNumber.Text;
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalespersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalespersonID"] = DBNull.Value;
				}
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["IsExport"] = isExport;
				if (sourceVoucherID != "")
				{
					dataRow["SourceSysDocID"] = sourceSysDocID;
					dataRow["SourceVoucherID"] = sourceVoucherID;
					dataRow["SourceDocType"] = sourceDocType;
				}
				else
				{
					dataRow["SourceSysDocID"] = DBNull.Value;
					dataRow["SourceVoucherID"] = DBNull.Value;
					dataRow["SourceDocType"] = DBNull.Value;
				}
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ConsignOutTable.Rows.Add(dataRow);
				}
				currentData.ConsignOutDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.ConsignOutDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					if (row.Cells["MarketPrice"].Value != null && row.Cells["MarketPrice"].Value.ToString() != "")
					{
						dataRow2["MarketPrice"] = row.Cells["MarketPrice"].Value.ToString();
					}
					else
					{
						dataRow2["MarketPrice"] = 0;
					}
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["SourceDocType"].Value != null && row.Cells["SourceDocType"].Value.ToString() != "")
					{
						dataRow2["SourceDocType"] = row.Cells["SourceDocType"].Value.ToString();
						dataRow2["SourceRowIndex"] = row.Cells["SourceRowIndex"].Value.ToString();
						dataRow2["SourceSysDocID"] = row.Cells["SourceSysDocID"].Value.ToString();
						dataRow2["SourceVoucherID"] = row.Cells["SourceVoucherID"].Value.ToString();
					}
					dataRow2.EndEdit();
					currentData.ConsignOutDetailTable.Rows.Add(dataRow2);
					string b = row.Cells["Location"].Value.ToString();
					string text = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Quantity"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
						{
							if (row2["LocationID"].ToString() != b || row2["ProductID"].ToString() != text)
							{
								ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text + "'.\nPlease reallocate the lots for this item.");
								return false;
							}
							DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
							dataRow4["ProductID"] = row2["ProductID"];
							dataRow4["LocationID"] = row2["LocationID"];
							dataRow4["Reference"] = row2["Reference"];
							dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
							dataRow4["LotNumber"] = row2["LotNumber"];
							dataRow4["SoldQty"] = row2["SoldQty"];
							dataRow4["Cost"] = row2["Cost"];
							dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
							dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow4["UnitPrice"] = 0;
							dataRow4["RowIndex"] = row.Index;
							currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("SourceRowIndex", typeof(int));
				dataTable.Columns.Add("SourceDocType", typeof(byte));
				dataTable.Columns.Add("IsSourcedRow", typeof(bool));
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("MarketPrice");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceDocType"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["IsSourcedRow"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["SourceDocType"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridItems.DisplayLayout.Bands[0].Columns["IsSourcedRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser4 = ultraGridColumn8.ExcludeFromColumnChooser = excludeFromColumnChooser2;
				ExcludeFromColumnChooser excludeFromColumnChooser6 = ultraGridColumn7.ExcludeFromColumnChooser = excludeFromColumnChooser4;
				ExcludeFromColumnChooser excludeFromColumnChooser9 = ultraGridColumn5.ExcludeFromColumnChooser = (ultraGridColumn6.ExcludeFromColumnChooser = excludeFromColumnChooser6);
				AdjustGridColumnSettings();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SISysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SIVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SIRowIndex"].Hidden = true;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["MarketPrice"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["MarketPrice"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["MarketPrice"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["MarketPrice"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["MarketPrice"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Shipped"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Invoiced", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Shipped", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].DisplayFormat = "{0:n}";
			}
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
			bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
			dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
			if (isDNInventory)
			{
				dataGridItems.DisplayLayout.Appearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Appearance.ForeColorDisabled = Color.Black;
				dataGridItems.ShowDeleteMenu = false;
				DataEntryGrid dataEntryGrid = dataGridItems;
				hidden = (dataGridItems.ShowInsertMenu = false);
				dataEntryGrid.ShowMoveRowsMenu = hidden;
				dataGridItems.AllowAddNew = false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxCustomer.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.ConsignOutSystem.GetConsignOutByID(SystemDocID, voucherID);
					if (currentData != null)
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Consign_Out"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxSourceDocNumber.Text = dataRow["PONumber"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					if (dataRow["IsExport"] != DBNull.Value)
					{
						isExport = bool.Parse(dataRow["IsExport"].ToString());
					}
					else
					{
						isExport = false;
					}
					ConsignOutStatusEnum consignOutStatusEnum = ConsignOutStatusEnum.Issued;
					if (dataRow["Status"] != DBNull.Value)
					{
						consignOutStatusEnum = (ConsignOutStatusEnum)byte.Parse(dataRow["Status"].ToString());
					}
					if (consignOutStatusEnum == ConsignOutStatusEnum.Settled)
					{
						docStatusLabel.Visible = true;
						docStatusLabel.ShowDocNumber = false;
						docStatusLabel.DisplayStatus = "SETTLED";
					}
					else
					{
						docStatusLabel.Visible = false;
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Consign_Out_Detail") && currentData.ConsignOutDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Consign_Out_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							dataRow3["Description"] = row["Description"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["Location"] = row["LocationID"];
							if (row["MarketPrice"] != DBNull.Value)
							{
								dataRow3["MarketPrice"] = Math.Round(decimal.Parse(row["MarketPrice"].ToString()), Global.CurDecimalPoints);
							}
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3["SourceDocType"] = row["SourceDocType"];
							dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
							dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
							dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
								if (array.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
							if (row2.Cells["SourceDocType"].Value != null && row2.Cells["SourceDocType"].Value.ToString() != "")
							{
								row2.Cells["Item Code"].Activation = Activation.Disabled;
								row2.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Description"].Activation = Activation.Disabled;
								row2.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Location"].Activation = Activation.Disabled;
								row2.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Unit"].Activation = Activation.Disabled;
								row2.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Quantity"].Activation = Activation.Disabled;
								row2.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
							}
						}
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						CalculateTotal();
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isDataLoading = false;
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
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = Factory.ConsignOutSystem.CreateConsignOut(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool result2 = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("DoPrint").ToString(), out result);
					if (result)
					{
						bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result2);
						if (result2)
						{
							Print(isPrint: true, showPrintDialog: true, saveChanges: false);
						}
						else
						{
							Print(isPrint: false, showPrintDialog: true, saveChanges: false);
						}
					}
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						if (IsNewRecord)
						{
							IsNewRecord = false;
						}
						formManager.ResetDirty();
					}
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == textBoxVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						textBoxVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					return SaveData();
				}
				ErrorHelper.ProcessError(ex);
				return false;
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Consign_Out", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimePickerDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
				{
					flag = true;
				}
				else if (Global.isUserAdmin)
				{
					flag = true;
				}
				else if (num == 0)
				{
					flag = true;
				}
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (!isNewRecord && !Factory.ConsignOutSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Document is in use by another transaction. Unable to modify.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
						continue;
					}
					if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
					if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select a location for all the items.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (comboBoxCustomer.IsCustomerOnHold)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Consign_Out", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				return true;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Quantity"].Value.ToString());
				}
			}
			return result;
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
			try
			{
				comboBoxCustomer.Enabled = true;
				comboBoxCustomer.ReadOnly = false;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				comboBoxShippingAddressID.Clear();
				textBoxSourceDocNumber.Clear();
				comboBoxSalesperson.Clear();
				comboBoxShippingMethod.Clear();
				textBoxBilltoAddress.Clear();
				isDiscountPercent = false;
				isDiscountPercent = false;
				DataTable obj = dataGridItems.DataSource as DataTable;
				sourceSysDocID = "";
				sourceVoucherID = "";
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
				obj.Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxCustomer.Focus();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JournalLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JournalLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
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
				if (!Factory.ConsignOutSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Document is in use by another transaction. Unable to modify.");
					return false;
				}
				return Factory.ConsignOutSystem.DeleteConsignOut(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Consign_Out", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Consign_Out", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Consign_Out", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Consign_Out", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Consign_Out", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
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

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.ConsignOut);
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				comboBoxGridItem.IsLoadingData = false;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No && Void(isVoid: true))
			{
				IsVoid = true;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				if (!isNewRecord && !Factory.ConsignOutSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Document is in use by another transaction. Unable to modify.");
					return false;
				}
				return Factory.ConsignOutSystem.VoidConsignOut(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ConsignOut);
		}

		private void LoadCustomerBillingAddress()
		{
			try
			{
				if (!isDataLoading)
				{
					DataSet customerDocumentAddress = Factory.CustomerSystem.GetCustomerDocumentAddress(comboBoxCustomer.SelectedID, "BillToAddressID");
					DataRow dataRow = null;
					if (customerDocumentAddress != null && customerDocumentAddress.Tables.Count > 0 && customerDocumentAddress.Tables[0].Rows.Count > 0)
					{
						dataRow = customerDocumentAddress.Tables[0].Rows[0];
					}
					if (dataRow != null)
					{
						currentCustomerAddressID = dataRow["BillToAddressID"].ToString();
						textBoxBilltoAddress.Text = dataRow["AddressPrintFormat"].ToString();
					}
					else
					{
						textBoxBilltoAddress.Clear();
						currentCustomerAddressID = "";
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string nextID = DatabaseHelper.GetNextID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (nextID != "")
			{
				currentCustomerAddressID = nextID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", nextID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void buttonPrevBillto_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentCustomerAddressID = previousID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", previousID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void CalculateTotal()
		{
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridItems.Left + 20, dataGridItems.Top + 20);
				}
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxShippingAddressID.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentCustomerAddressID == ""))
			{
				new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, currentCustomerAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					docStatusLabel.Visible = false;
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					docStatusLabel.Visible = false;
				}
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
		}

		private void selectInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataSet dataSource = null;
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.DataSource = dataSource;
			selectDocumentDialog.Text = "Select Sales Invoice for Delivery";
			selectDocumentDialog.ValidateSelection += form_ValidateSelection;
			checked
			{
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					ClearForm();
					bool flag = false;
					int num = 0;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						string text = selectedRow.Cells["Doc ID"].Value.ToString();
						string text2 = selectedRow.Cells["Number"].Value.ToString();
						stringBuilder.Append(text2);
						if (num < selectDocumentDialog.SelectedRows.Count - 1)
						{
							stringBuilder.Append(",");
						}
						num++;
						SalesInvoiceData salesInvoiceByID = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(text, text2);
						if (!flag)
						{
							DataRow dataRow = salesInvoiceByID.SalesInvoiceTable.Rows[0];
							textBoxNote.Text = dataRow["Note"].ToString();
							if (comboBoxCustomer.SelectedID == "")
							{
								comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
							}
							textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
							comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
							comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
							flag = true;
						}
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						if (!dataTable.Columns.Contains("Invoiced"))
						{
							dataTable.Columns.Remove("Quantity");
							dataTable.Columns.Add("Invoiced", typeof(decimal));
							dataTable.Columns.Add("Shipped", typeof(decimal));
							dataTable.Columns.Add("Quantity", typeof(decimal));
							if (!dataTable.Columns.Contains("SISysDocID"))
							{
								dataTable.Columns.Add("SISysDocID");
								dataTable.Columns.Add("SIVoucherID");
								dataTable.Columns.Add("SIRowIndex", typeof(int));
							}
						}
						if (!salesInvoiceByID.Tables.Contains("Sales_Invoice_Detail") || salesInvoiceByID.SalesInvoiceDetailTable.Rows.Count == 0)
						{
							return;
						}
						foreach (DataRow row in salesInvoiceByID.Tables["Sales_Invoice_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3["SISysDocID"] = text;
							dataRow3["SIVoucherID"] = text2;
							dataRow3["SIRowIndex"] = row["RowIndex"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							dataRow3["Description"] = row["Description"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
							decimal.TryParse(row["QuantityShipped"].ToString(), out result2);
							dataRow3["Invoiced"] = result;
							dataRow3["Shipped"] = result2;
							result -= result2;
							dataRow3["Quantity"] = result;
							if (result < 0m)
							{
								result = default(decimal);
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
					}
					textBoxNote.Text = "Invoice Numbers:\r\n";
					textBoxNote.AppendText(stringBuilder.ToString());
					AdjustGridColumnSettings();
					CalculateTotal();
				}
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			if (selectDocumentDialog != null)
			{
				List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
				if (selectedRows != null)
				{
					string a = "";
					foreach (UltraGridRow item in selectedRows)
					{
						if (a == "")
						{
							a = item.Cells["Customer"].Value.ToString();
						}
						else if (a != item.Cells["Customer"].Value.ToString())
						{
							ErrorHelper.InformationMessage("Only documents that are from same customer can be selected together.");
							selectDocumentDialog.CanClose = false;
							break;
						}
					}
				}
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet consignOutToPrint = Factory.ConsignOutSystem.GetConsignOutToPrint(selectedID, text);
					if (consignOutToPrint == null || consignOutToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(consignOutToPrint, selectedID, "Consign-Out Detail", SysDocTypes.ConsignOut, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void createFromPackingListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (isNewRecord)
				{
					DataSet packingListsForInvoice = Factory.ExportPackingListSystem.GetPackingListsForInvoice(comboBoxCustomer.SelectedID);
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.ValidateSelection += form_ValidateSelection;
					selectDocumentDialog.IsMultiSelect = false;
					selectDocumentDialog.DataSource = packingListsForInvoice;
					selectDocumentDialog.Text = "Select Packing List";
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						ClearForm();
						string text = "";
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							string text2 = selectedRow.Cells["Doc ID"].Value.ToString();
							string text3 = selectedRow.Cells["Number"].Value.ToString();
							sourceSysDocID = text2;
							sourceVoucherID = text3;
							sourceDocType = SysDocTypes.PackingList;
							textBoxSourceDocNumber.Text = sourceVoucherID;
							DataSet packingListItemsToInvoice = Factory.DeliveryNoteSystem.GetPackingListItemsToInvoice(text2, text3);
							DataTable dataTable = dataGridItems.DataSource as DataTable;
							ArrayList arrayList = new ArrayList();
							foreach (DataRow row in packingListItemsToInvoice.Tables["Delivery_Note_Detail"].Rows)
							{
								decimal result = default(decimal);
								decimal num = default(decimal);
								text = row["CustomerID"].ToString();
								comboBoxCustomer.SelectedID = text;
								if (!arrayList.Contains(text))
								{
									arrayList.Add(text);
								}
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2["Item Code"] = row["ProductID"];
								dataRow2["Description"] = row["Description"];
								dataRow2["Location"] = row["LocationID"];
								dataRow2["SourceSysDocID"] = text2;
								dataRow2["SourceVoucherID"] = text3;
								dataRow2["SourceRowIndex"] = row["RowIndex"];
								dataRow2["SourceDocType"] = ItemSourceTypes.DeliveryNote;
								dataRow2["IsSourcedRow"] = true;
								decimal d = (row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString());
								decimal.TryParse(row["QuantityShipped"].ToString(), out result);
								num = d - result;
								dataRow2["Quantity"] = num;
								dataRow2["Unit"] = row["UnitID"];
								if (!(num <= 0m))
								{
									dataRow2.EndEdit();
									dataTable.Rows.Add(dataRow2);
								}
							}
							foreach (UltraGridRow row2 in dataGridItems.Rows)
							{
								row2.Cells["Item Code"].Activation = Activation.NoEdit;
								row2.Cells["Item Code"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Item Code"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Description"].Activation = Activation.NoEdit;
								row2.Cells["Description"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Description"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Location"].Activation = Activation.NoEdit;
								row2.Cells["Location"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Location"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Unit"].Activation = Activation.NoEdit;
								row2.Cells["Unit"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Unit"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Quantity"].Activation = Activation.NoEdit;
								row2.Cells["Quantity"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Quantity"].SelectedAppearance.BackColor = Color.WhiteSmoke;
							}
							comboBoxCustomer.ReadOnly = true;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void createFromDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (isNewRecord)
				{
					DataSet uninvoicedDeliveryNotes = Factory.DeliveryNoteSystem.GetUninvoicedDeliveryNotes(comboBoxCustomer.SelectedID, isExport: true);
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.ValidateSelection += form_ValidateSelection;
					selectDocumentDialog.IsMultiSelect = true;
					selectDocumentDialog.DataSource = uninvoicedDeliveryNotes;
					selectDocumentDialog.Text = "Select Delivery Notes";
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						ClearForm();
						textBoxSourceDocNumber.Clear();
						string text = "";
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							string text2 = selectedRow.Cells["Doc ID"].Value.ToString();
							string text3 = selectedRow.Cells["Number"].Value.ToString();
							sourceSysDocID = text2;
							sourceVoucherID = text3;
							sourceDocType = SysDocTypes.DeliveryNote;
							isExport = true;
							textBoxSourceDocNumber.Text += sourceVoucherID;
							DataSet dOItemsToShip = Factory.DeliveryNoteSystem.GetDOItemsToShip(text2, text3);
							DataTable dataTable = dataGridItems.DataSource as DataTable;
							ArrayList arrayList = new ArrayList();
							foreach (DataRow row in dOItemsToShip.Tables["Delivery_Note_Detail"].Rows)
							{
								decimal result = default(decimal);
								decimal num = default(decimal);
								text = row["CustomerID"].ToString();
								comboBoxCustomer.SelectedID = text;
								if (!arrayList.Contains(text))
								{
									arrayList.Add(text);
								}
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2["Item Code"] = row["ProductID"];
								dataRow2["Description"] = row["Description"];
								dataRow2["Location"] = row["LocationID"];
								dataRow2["SourceSysDocID"] = text2;
								dataRow2["SourceVoucherID"] = text3;
								dataRow2["SourceRowIndex"] = row["RowIndex"];
								dataRow2["SourceDocType"] = ItemSourceTypes.DeliveryNote;
								dataRow2["IsSourcedRow"] = true;
								decimal d = (row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString());
								decimal.TryParse(row["QuantityShipped"].ToString(), out result);
								num = d - result;
								dataRow2["Quantity"] = num;
								dataRow2["Unit"] = row["UnitID"];
								if (!(num <= 0m))
								{
									dataRow2.EndEdit();
									dataTable.Rows.Add(dataRow2);
								}
							}
							foreach (UltraGridRow row2 in dataGridItems.Rows)
							{
								row2.Cells["Item Code"].Activation = Activation.NoEdit;
								row2.Cells["Item Code"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Item Code"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Description"].Activation = Activation.NoEdit;
								row2.Cells["Description"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Description"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Location"].Activation = Activation.NoEdit;
								row2.Cells["Location"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Location"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Unit"].Activation = Activation.NoEdit;
								row2.Cells["Unit"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Unit"].SelectedAppearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Quantity"].Activation = Activation.NoEdit;
								row2.Cells["Quantity"].Appearance.BackColor = Color.WhiteSmoke;
								row2.Cells["Quantity"].SelectedAppearance.BackColor = Color.WhiteSmoke;
							}
							comboBoxCustomer.ReadOnly = true;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ConsignmentOutListFormObj);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkMethod_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.ConsignOut;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsignOutForm));
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromDeliveryNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromPackingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonVoid = new Micromind.UISupport.XPButton();
            this.buttonDelete = new Micromind.UISupport.XPButton();
            this.buttonNew = new Micromind.UISupport.XPButton();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxVoucherNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRef1 = new System.Windows.Forms.TextBox();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.ultraFormattedLinkMethod = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
            this.ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.labelSourceDocNumber = new System.Windows.Forms.Label();
            this.textBoxSourceDocNumber = new System.Windows.Forms.TextBox();
            this.comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
            this.ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
            this.buttonPrevBillto = new Infragistics.Win.Misc.UltraButton();
            this.buttonNextBillTo = new Infragistics.Win.Misc.UltraButton();
            this.ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
            this.ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
            this.ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.textBoxCustomerName = new System.Windows.Forms.TextBox();
            this.labelVoided = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
            this.productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
            this.formManager = new Micromind.DataControls.FormManager();
            this.dataGridItems = new Micromind.DataControls.DataEntryGrid();
            this.comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
            this.docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingAddressID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSysDoc)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridItem)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFirst,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripButtonLast,
            this.toolStripSeparator1,
            this.toolStripButtonOpenList,
            this.toolStripSeparator4,
            this.toolStripTextBoxFind,
            this.toolStripButtonFind,
            this.toolStripSeparator2,
            this.toolStripButtonAttach,
            this.toolStripSeparator7,
            this.toolStripButtonPrint,
            this.toolStripButtonPreview,
            this.toolStripButtonMultiPreview,
            this.toolStripSeparator5,
            this.toolStripButtonDistribution,
            this.toolStripButtonInformation,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonFirst
            // 
            this.toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirst.Image = global::Micromind.ClientUI.Properties.Resources.first;
            this.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirst.Name = "toolStripButtonFirst";
            this.toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonFirst.Text = "First Record";
            this.toolStripButtonFirst.Click += new System.EventHandler(this.toolStripButtonFirst_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Image = global::Micromind.ClientUI.Properties.Resources.prev;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPrevious.Text = "Previous Record";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Image = global::Micromind.ClientUI.Properties.Resources.next;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonNext.Text = "Next Record";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonLast
            // 
            this.toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLast.Image = global::Micromind.ClientUI.Properties.Resources.last;
            this.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLast.Name = "toolStripButtonLast";
            this.toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonLast.Text = "Last Record";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
            // toolStripButtonAttach
            // 
            this.toolStripButtonAttach.Image = global::Micromind.ClientUI.Properties.Resources.attach_24x24;
            this.toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAttach.Name = "toolStripButtonAttach";
            this.toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
            this.toolStripButtonAttach.Text = "Attach File";
            this.toolStripButtonAttach.Click += new System.EventHandler(this.toolStripButtonAttach_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::Micromind.ClientUI.Properties.Resources.printer;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPrint.Text = "&Print";
            this.toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonPreview
            // 
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = global::Micromind.ClientUI.Properties.Resources.preview;
            this.toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonPreview.Text = "Preview";
            this.toolStripButtonPreview.ToolTipText = "Preview";
            this.toolStripButtonPreview.Click += new System.EventHandler(this.toolStripButtonPreview_Click);
            // 
            // toolStripButtonMultiPreview
            // 
            this.toolStripButtonMultiPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMultiPreview.Image = global::Micromind.ClientUI.Properties.Resources.multi_doc;
            this.toolStripButtonMultiPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMultiPreview.Name = "toolStripButtonMultiPreview";
            this.toolStripButtonMultiPreview.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonMultiPreview.Text = "MultiPreview";
            this.toolStripButtonMultiPreview.Click += new System.EventHandler(this.toolStripButtonMultiPreview_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonDistribution
            // 
            this.toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDistribution.Image = global::Micromind.ClientUI.Properties.Resources.jvdistribution;
            this.toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDistribution.Name = "toolStripButtonDistribution";
            this.toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonDistribution.Text = "Distribution Summary";
            this.toolStripButtonDistribution.Click += new System.EventHandler(this.toolStripButtonDistribution_Click);
            // 
            // toolStripButtonInformation
            // 
            this.toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInformation.Image = global::Micromind.ClientUI.Properties.Resources.docinfo_24x24;
            this.toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInformation.Name = "toolStripButtonInformation";
            this.toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonInformation.Text = "Document Information";
            this.toolStripButtonInformation.Click += new System.EventHandler(this.toolStripButtonInformation_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem,
            this.createFromDeliveryNoteToolStripMenuItem,
            this.createFromPackingListToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
            this.toolStripDropDownButton1.Text = "Actions";
            this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpening);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.duplicateToolStripMenuItem.Text = "Copy";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // createFromDeliveryNoteToolStripMenuItem
            // 
            this.createFromDeliveryNoteToolStripMenuItem.Name = "createFromDeliveryNoteToolStripMenuItem";
            this.createFromDeliveryNoteToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createFromDeliveryNoteToolStripMenuItem.Text = "Create from Delivery Note...";
            this.createFromDeliveryNoteToolStripMenuItem.Click += new System.EventHandler(this.createFromDeliveryNoteToolStripMenuItem_Click);
            // 
            // createFromPackingListToolStripMenuItem
            // 
            this.createFromPackingListToolStripMenuItem.Name = "createFromPackingListToolStripMenuItem";
            this.createFromPackingListToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createFromPackingListToolStripMenuItem.Text = "Create from Packing List...";
            this.createFromPackingListToolStripMenuItem.Visible = false;
            this.createFromPackingListToolStripMenuItem.Click += new System.EventHandler(this.createFromPackingListToolStripMenuItem_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonVoid);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.buttonNew);
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.xpButton1);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 527);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(778, 40);
            this.panelButtons.TabIndex = 4;
            // 
            // buttonVoid
            // 
            this.buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonVoid.BackColor = System.Drawing.Color.DarkGray;
            this.buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonVoid.Location = new System.Drawing.Point(216, 8);
            this.buttonVoid.Name = "buttonVoid";
            this.buttonVoid.Size = new System.Drawing.Size(96, 24);
            this.buttonVoid.TabIndex = 2;
            this.buttonVoid.Text = "&Void";
            this.buttonVoid.UseVisualStyleBackColor = false;
            this.buttonVoid.Click += new System.EventHandler(this.buttonVoid_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonDelete.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDelete.Location = new System.Drawing.Point(318, 8);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(96, 24);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "De&lete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
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
            // linePanelDown
            // 
            this.linePanelDown.BackColor = System.Drawing.Color.White;
            this.linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.linePanelDown.DrawWidth = 1;
            this.linePanelDown.IsVertical = false;
            this.linePanelDown.LineBackColor = System.Drawing.Color.Silver;
            this.linePanelDown.Location = new System.Drawing.Point(0, 0);
            this.linePanelDown.Name = "linePanelDown";
            this.linePanelDown.Size = new System.Drawing.Size(778, 1);
            this.linePanelDown.TabIndex = 14;
            this.linePanelDown.TabStop = false;
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
            this.xpButton1.Location = new System.Drawing.Point(668, 8);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(96, 24);
            this.xpButton1.TabIndex = 4;
            this.xpButton1.Text = "&Close";
            this.xpButton1.UseVisualStyleBackColor = false;
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
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
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDate.Location = new System.Drawing.Point(551, 1);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
            this.dateTimePickerDate.TabIndex = 7;
            // 
            // textBoxVoucherNumber
            // 
            this.textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
            this.textBoxVoucherNumber.MaxLength = 15;
            this.textBoxVoucherNumber.Name = "textBoxVoucherNumber";
            this.textBoxVoucherNumber.Size = new System.Drawing.Size(109, 20);
            this.textBoxVoucherNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(469, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Reference:";
            // 
            // textBoxRef1
            // 
            this.textBoxRef1.Location = new System.Drawing.Point(551, 23);
            this.textBoxRef1.MaxLength = 20;
            this.textBoxRef1.Name = "textBoxRef1";
            this.textBoxRef1.Size = new System.Drawing.Size(107, 20);
            this.textBoxRef1.TabIndex = 8;
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNote.Location = new System.Drawing.Point(49, 419);
            this.textBoxNote.MaxLength = 4000;
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNote.Size = new System.Drawing.Size(381, 95);
            this.textBoxNote.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Note:";
            // 
            // ultraFormattedLinkLabel2
            // 
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel2.Appearance = appearance1;
            this.ultraFormattedLinkLabel2.AutoSize = true;
            this.ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
            this.ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
            this.ultraFormattedLinkLabel2.Size = new System.Drawing.Size(102, 15);
            this.ultraFormattedLinkLabel2.TabIndex = 0;
            this.ultraFormattedLinkLabel2.TabStop = true;
            this.ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel2.Value = "Voucher Number:";
            appearance2.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
            this.ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel2_LinkClicked);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.ultraFormattedLinkMethod);
            this.panelDetails.Controls.Add(this.comboBoxSalesperson);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel6);
            this.panelDetails.Controls.Add(this.labelSourceDocNumber);
            this.panelDetails.Controls.Add(this.textBoxSourceDocNumber);
            this.panelDetails.Controls.Add(this.comboBoxShippingMethod);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel3);
            this.panelDetails.Controls.Add(this.comboBoxShippingAddressID);
            this.panelDetails.Controls.Add(this.buttonPrevBillto);
            this.panelDetails.Controls.Add(this.buttonNextBillTo);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel1);
            this.panelDetails.Controls.Add(this.textBoxBilltoAddress);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel4);
            this.panelDetails.Controls.Add(this.comboBoxCustomer);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel5);
            this.panelDetails.Controls.Add(this.comboBoxSysDoc);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel2);
            this.panelDetails.Controls.Add(this.mmLabel1);
            this.panelDetails.Controls.Add(this.label1);
            this.panelDetails.Controls.Add(this.textBoxCustomerName);
            this.panelDetails.Controls.Add(this.textBoxRef1);
            this.panelDetails.Controls.Add(this.textBoxVoucherNumber);
            this.panelDetails.Controls.Add(this.dateTimePickerDate);
            this.panelDetails.Location = new System.Drawing.Point(0, 33);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(749, 145);
            this.panelDetails.TabIndex = 0;
            // 
            // ultraFormattedLinkMethod
            // 
            appearance3.FontData.BoldAsString = "False";
            appearance3.FontData.Name = "Tahoma";
            this.ultraFormattedLinkMethod.Appearance = appearance3;
            this.ultraFormattedLinkMethod.AutoSize = true;
            this.ultraFormattedLinkMethod.Location = new System.Drawing.Point(271, 121);
            this.ultraFormattedLinkMethod.Name = "ultraFormattedLinkMethod";
            this.ultraFormattedLinkMethod.Size = new System.Drawing.Size(46, 15);
            this.ultraFormattedLinkMethod.TabIndex = 145;
            this.ultraFormattedLinkMethod.TabStop = true;
            this.ultraFormattedLinkMethod.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkMethod.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkMethod.Value = "Method:";
            appearance4.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkMethod.VisitedLinkAppearance = appearance4;
            this.ultraFormattedLinkMethod.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkMethod_LinkClicked);
            // 
            // comboBoxSalesperson
            // 
            this.comboBoxSalesperson.Assigned = false;
            this.comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSalesperson.CustomReportFieldName = "";
            this.comboBoxSalesperson.CustomReportKey = "";
            this.comboBoxSalesperson.CustomReportValueType = ((byte)(1));
            this.comboBoxSalesperson.DescriptionTextBox = null;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSalesperson.DisplayLayout.Appearance = appearance5;
            this.comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance6.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance6;
            appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance8.BackColor2 = System.Drawing.SystemColors.Control;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
            this.comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance9;
            appearance10.BackColor = System.Drawing.SystemColors.Highlight;
            appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance10;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance11;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance12;
            this.comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
            appearance13.BackColor = System.Drawing.SystemColors.Control;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance13.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance13;
            appearance14.TextHAlignAsString = "Left";
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance14;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            appearance15.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance15;
            this.comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
            this.comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSalesperson.Editable = true;
            this.comboBoxSalesperson.FilterString = "";
            this.comboBoxSalesperson.HasAllAccount = false;
            this.comboBoxSalesperson.HasCustom = false;
            this.comboBoxSalesperson.IsDataLoaded = false;
            this.comboBoxSalesperson.Location = new System.Drawing.Point(551, 67);
            this.comboBoxSalesperson.MaxDropDownItems = 12;
            this.comboBoxSalesperson.Name = "comboBoxSalesperson";
            this.comboBoxSalesperson.ShowInactiveItems = false;
            this.comboBoxSalesperson.ShowQuickAdd = true;
            this.comboBoxSalesperson.Size = new System.Drawing.Size(153, 20);
            this.comboBoxSalesperson.TabIndex = 10;
            this.comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel6
            // 
            appearance17.FontData.BoldAsString = "False";
            appearance17.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel6.Appearance = appearance17;
            this.ultraFormattedLinkLabel6.AutoSize = true;
            this.ultraFormattedLinkLabel6.Location = new System.Drawing.Point(472, 69);
            this.ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
            this.ultraFormattedLinkLabel6.Size = new System.Drawing.Size(68, 15);
            this.ultraFormattedLinkLabel6.TabIndex = 144;
            this.ultraFormattedLinkLabel6.TabStop = true;
            this.ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel6.Value = "Salesperson:";
            appearance18.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance18;
            // 
            // labelSourceDocNumber
            // 
            this.labelSourceDocNumber.AutoSize = true;
            this.labelSourceDocNumber.Location = new System.Drawing.Point(469, 48);
            this.labelSourceDocNumber.Name = "labelSourceDocNumber";
            this.labelSourceDocNumber.Size = new System.Drawing.Size(68, 13);
            this.labelSourceDocNumber.TabIndex = 143;
            this.labelSourceDocNumber.Text = "Packing List:";
            // 
            // textBoxSourceDocNumber
            // 
            this.textBoxSourceDocNumber.Location = new System.Drawing.Point(551, 45);
            this.textBoxSourceDocNumber.MaxLength = 15;
            this.textBoxSourceDocNumber.Name = "textBoxSourceDocNumber";
            this.textBoxSourceDocNumber.ReadOnly = true;
            this.textBoxSourceDocNumber.Size = new System.Drawing.Size(153, 20);
            this.textBoxSourceDocNumber.TabIndex = 9;
            this.textBoxSourceDocNumber.TabStop = false;
            // 
            // comboBoxShippingMethod
            // 
            this.comboBoxShippingMethod.Assigned = false;
            this.comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShippingMethod.CustomReportFieldName = "";
            this.comboBoxShippingMethod.CustomReportKey = "";
            this.comboBoxShippingMethod.CustomReportValueType = ((byte)(1));
            this.comboBoxShippingMethod.DescriptionTextBox = null;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShippingMethod.DisplayLayout.Appearance = appearance19;
            this.comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance20.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance20;
            appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance22.BackColor2 = System.Drawing.SystemColors.Control;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
            this.comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance23;
            appearance24.BackColor = System.Drawing.SystemColors.Highlight;
            appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance24;
            this.comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance25;
            appearance26.BorderColor = System.Drawing.Color.Silver;
            appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance26;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
            appearance27.BackColor = System.Drawing.SystemColors.Control;
            appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance27.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance27;
            appearance28.TextHAlignAsString = "Left";
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance28;
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance29;
            this.comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
            this.comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShippingMethod.Editable = true;
            this.comboBoxShippingMethod.FilterString = "";
            this.comboBoxShippingMethod.HasAllAccount = false;
            this.comboBoxShippingMethod.HasCustom = false;
            this.comboBoxShippingMethod.IsDataLoaded = false;
            this.comboBoxShippingMethod.Location = new System.Drawing.Point(318, 119);
            this.comboBoxShippingMethod.MaxDropDownItems = 12;
            this.comboBoxShippingMethod.Name = "comboBoxShippingMethod";
            this.comboBoxShippingMethod.ShowInactiveItems = false;
            this.comboBoxShippingMethod.ShowQuickAdd = true;
            this.comboBoxShippingMethod.Size = new System.Drawing.Size(117, 20);
            this.comboBoxShippingMethod.TabIndex = 6;
            this.comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel3
            // 
            appearance31.FontData.BoldAsString = "False";
            appearance31.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel3.Appearance = appearance31;
            this.ultraFormattedLinkLabel3.AutoSize = true;
            this.ultraFormattedLinkLabel3.Location = new System.Drawing.Point(13, 121);
            this.ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
            this.ultraFormattedLinkLabel3.Size = new System.Drawing.Size(85, 15);
            this.ultraFormattedLinkLabel3.TabIndex = 137;
            this.ultraFormattedLinkLabel3.TabStop = true;
            this.ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel3.Value = "Ship to Address:";
            appearance32.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance32;
            this.ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel3_LinkClicked);
            // 
            // comboBoxShippingAddressID
            // 
            this.comboBoxShippingAddressID.Assigned = false;
            this.comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShippingAddressID.CustomReportFieldName = "";
            this.comboBoxShippingAddressID.CustomReportKey = "";
            this.comboBoxShippingAddressID.CustomReportValueType = ((byte)(1));
            this.comboBoxShippingAddressID.DescriptionTextBox = null;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShippingAddressID.DisplayLayout.Appearance = appearance33;
            this.comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance34.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance34;
            appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance36.BackColor2 = System.Drawing.SystemColors.Control;
            appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
            this.comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance37;
            appearance38.BackColor = System.Drawing.SystemColors.Highlight;
            appearance38.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance38;
            this.comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance39.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance39;
            appearance40.BorderColor = System.Drawing.Color.Silver;
            appearance40.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance40;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
            appearance41.BackColor = System.Drawing.SystemColors.Control;
            appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance41.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance41.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance41;
            appearance42.TextHAlignAsString = "Left";
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance42;
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            appearance43.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance43;
            this.comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance44.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance44;
            this.comboBoxShippingAddressID.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShippingAddressID.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShippingAddressID.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShippingAddressID.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShippingAddressID.Editable = true;
            this.comboBoxShippingAddressID.FilterString = "";
            this.comboBoxShippingAddressID.HasAllAccount = false;
            this.comboBoxShippingAddressID.HasCustom = false;
            this.comboBoxShippingAddressID.IsDataLoaded = false;
            this.comboBoxShippingAddressID.Location = new System.Drawing.Point(99, 119);
            this.comboBoxShippingAddressID.MaxDropDownItems = 12;
            this.comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
            this.comboBoxShippingAddressID.ShowInactiveItems = false;
            this.comboBoxShippingAddressID.ShowQuickAdd = true;
            this.comboBoxShippingAddressID.Size = new System.Drawing.Size(166, 20);
            this.comboBoxShippingAddressID.TabIndex = 5;
            this.comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // buttonPrevBillto
            // 
            appearance45.ImageBackground = global::Micromind.ClientUI.Properties.Resources.prev;
            appearance45.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.buttonPrevBillto.Appearance = appearance45;
            this.buttonPrevBillto.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
            this.buttonPrevBillto.Location = new System.Drawing.Point(285, 46);
            this.buttonPrevBillto.Name = "buttonPrevBillto";
            this.buttonPrevBillto.ShowFocusRect = false;
            this.buttonPrevBillto.ShowOutline = false;
            this.buttonPrevBillto.Size = new System.Drawing.Size(14, 14);
            this.buttonPrevBillto.TabIndex = 135;
            this.buttonPrevBillto.TabStop = false;
            this.buttonPrevBillto.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.buttonPrevBillto.Click += new System.EventHandler(this.buttonPrevBillto_Click);
            // 
            // buttonNextBillTo
            // 
            appearance46.ImageBackground = global::Micromind.ClientUI.Properties.Resources.next;
            appearance46.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.buttonNextBillTo.Appearance = appearance46;
            this.buttonNextBillTo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
            this.buttonNextBillTo.Location = new System.Drawing.Point(299, 46);
            this.buttonNextBillTo.Name = "buttonNextBillTo";
            this.buttonNextBillTo.ShowFocusRect = false;
            this.buttonNextBillTo.ShowOutline = false;
            this.buttonNextBillTo.Size = new System.Drawing.Size(14, 14);
            this.buttonNextBillTo.TabIndex = 135;
            this.buttonNextBillTo.TabStop = false;
            this.buttonNextBillTo.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.buttonNextBillTo.Click += new System.EventHandler(this.buttonNextBillTo_Click);
            // 
            // ultraFormattedLinkLabel1
            // 
            appearance47.FontData.BoldAsString = "False";
            appearance47.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel1.Appearance = appearance47;
            this.ultraFormattedLinkLabel1.AutoSize = true;
            this.ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 44);
            this.ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
            this.ultraFormattedLinkLabel1.Size = new System.Drawing.Size(77, 15);
            this.ultraFormattedLinkLabel1.TabIndex = 134;
            this.ultraFormattedLinkLabel1.TabStop = true;
            this.ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel1.Value = "Bill to Address:";
            appearance48.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance48;
            this.ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel1_LinkClicked);
            // 
            // textBoxBilltoAddress
            // 
            this.textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
            this.textBoxBilltoAddress.CustomReportFieldName = "";
            this.textBoxBilltoAddress.CustomReportKey = "";
            this.textBoxBilltoAddress.CustomReportValueType = ((byte)(1));
            this.textBoxBilltoAddress.IsComboTextBox = false;
            this.textBoxBilltoAddress.IsModified = false;
            this.textBoxBilltoAddress.Location = new System.Drawing.Point(99, 45);
            this.textBoxBilltoAddress.MaxLength = 255;
            this.textBoxBilltoAddress.Multiline = true;
            this.textBoxBilltoAddress.Name = "textBoxBilltoAddress";
            this.textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
            this.textBoxBilltoAddress.TabIndex = 4;
            // 
            // ultraFormattedLinkLabel4
            // 
            appearance49.FontData.BoldAsString = "True";
            appearance49.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel4.Appearance = appearance49;
            this.ultraFormattedLinkLabel4.AutoSize = true;
            this.ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
            this.ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
            this.ultraFormattedLinkLabel4.Size = new System.Drawing.Size(80, 15);
            this.ultraFormattedLinkLabel4.TabIndex = 129;
            this.ultraFormattedLinkLabel4.TabStop = true;
            this.ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel4.Value = "Customer ID:";
            appearance50.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance50;
            this.ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel4_LinkClicked);
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.Assigned = false;
            this.comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCustomer.CustomReportFieldName = "";
            this.comboBoxCustomer.CustomReportKey = "";
            this.comboBoxCustomer.CustomReportValueType = ((byte)(1));
            this.comboBoxCustomer.DescriptionTextBox = null;
            appearance51.BackColor = System.Drawing.SystemColors.Window;
            appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCustomer.DisplayLayout.Appearance = appearance51;
            this.comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance52.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance52;
            appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance54.BackColor2 = System.Drawing.SystemColors.Control;
            appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
            this.comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance55;
            appearance56.BackColor = System.Drawing.SystemColors.Highlight;
            appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance56;
            this.comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance57.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance57;
            appearance58.BorderColor = System.Drawing.Color.Silver;
            appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance58;
            this.comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
            appearance59.BackColor = System.Drawing.SystemColors.Control;
            appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance59.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance59;
            appearance60.TextHAlignAsString = "Left";
            this.comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance60;
            this.comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            appearance61.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance61;
            this.comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
            this.comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCustomer.Editable = true;
            this.comboBoxCustomer.FilterString = "";
            this.comboBoxCustomer.FilterSysDocID = "";
            this.comboBoxCustomer.HasAll = false;
            this.comboBoxCustomer.HasCustom = false;
            this.comboBoxCustomer.IsDataLoaded = false;
            this.comboBoxCustomer.Location = new System.Drawing.Point(99, 23);
            this.comboBoxCustomer.MaxDropDownItems = 12;
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.ShowConsignmentOnly = false;
            this.comboBoxCustomer.ShowInactive = false;
            this.comboBoxCustomer.ShowLPOCustomersOnly = false;
            this.comboBoxCustomer.ShowPROCustomersOnly = false;
            this.comboBoxCustomer.ShowQuickAdd = true;
            this.comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
            this.comboBoxCustomer.TabIndex = 2;
            this.comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel5
            // 
            appearance63.FontData.BoldAsString = "True";
            appearance63.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel5.Appearance = appearance63;
            this.ultraFormattedLinkLabel5.AutoSize = true;
            this.ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
            this.ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
            this.ultraFormattedLinkLabel5.Size = new System.Drawing.Size(46, 15);
            this.ultraFormattedLinkLabel5.TabIndex = 2;
            this.ultraFormattedLinkLabel5.TabStop = true;
            this.ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel5.Value = "Doc ID:";
            appearance64.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance64;
            this.ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel5_LinkClicked);
            // 
            // comboBoxSysDoc
            // 
            this.comboBoxSysDoc.Assigned = false;
            this.comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSysDoc.CustomReportFieldName = "";
            this.comboBoxSysDoc.CustomReportKey = "";
            this.comboBoxSysDoc.CustomReportValueType = ((byte)(1));
            this.comboBoxSysDoc.DescriptionTextBox = null;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSysDoc.DisplayLayout.Appearance = appearance65;
            this.comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance66.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance66;
            appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance68.BackColor2 = System.Drawing.SystemColors.Control;
            appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
            this.comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
            appearance69.BackColor = System.Drawing.SystemColors.Window;
            appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance69;
            appearance70.BackColor = System.Drawing.SystemColors.Highlight;
            appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance70;
            this.comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance71.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance71;
            appearance72.BorderColor = System.Drawing.Color.Silver;
            appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance72;
            this.comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
            appearance73.BackColor = System.Drawing.SystemColors.Control;
            appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance73.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance73;
            appearance74.TextHAlignAsString = "Left";
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance74;
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance75.BackColor = System.Drawing.SystemColors.Window;
            appearance75.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance75;
            this.comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
            this.comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSysDoc.DivisionID = "";
            this.comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxSysDoc.Editable = true;
            this.comboBoxSysDoc.ExcludeFromSecurity = false;
            this.comboBoxSysDoc.FilterString = "";
            this.comboBoxSysDoc.HasAllAccount = false;
            this.comboBoxSysDoc.HasCustom = false;
            this.comboBoxSysDoc.IsDataLoaded = false;
            this.comboBoxSysDoc.Location = new System.Drawing.Point(99, 1);
            this.comboBoxSysDoc.MaxDropDownItems = 12;
            this.comboBoxSysDoc.Name = "comboBoxSysDoc";
            this.comboBoxSysDoc.ShowAll = false;
            this.comboBoxSysDoc.ShowInactiveItems = false;
            this.comboBoxSysDoc.ShowQuickAdd = true;
            this.comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
            this.comboBoxSysDoc.TabIndex = 0;
            this.comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = true;
            this.mmLabel1.Location = new System.Drawing.Point(469, 3);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(38, 13);
            this.mmLabel1.TabIndex = 2;
            this.mmLabel1.Text = "Date:";
            // 
            // textBoxCustomerName
            // 
            this.textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxCustomerName.Location = new System.Drawing.Point(210, 23);
            this.textBoxCustomerName.MaxLength = 64;
            this.textBoxCustomerName.Name = "textBoxCustomerName";
            this.textBoxCustomerName.ReadOnly = true;
            this.textBoxCustomerName.Size = new System.Drawing.Size(225, 20);
            this.textBoxCustomerName.TabIndex = 3;
            this.textBoxCustomerName.TabStop = false;
            // 
            // labelVoided
            // 
            this.labelVoided.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVoided.BackColor = System.Drawing.Color.White;
            this.labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoided.ForeColor = System.Drawing.Color.DarkRed;
            this.labelVoided.Location = new System.Drawing.Point(14, 364);
            this.labelVoided.Name = "labelVoided";
            this.labelVoided.Size = new System.Drawing.Size(750, 49);
            this.labelVoided.TabIndex = 117;
            this.labelVoided.Text = "VOIDED";
            this.labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVoided.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.availableQuantityToolStripMenuItem,
            this.salesStatisticsToolStripMenuItem,
            this.itemPicToolStripMenuItem,
            this.itemDetailsToolStripMenuItem,
            this.toolStripSeparator3,
            this.removeRowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 120);
            // 
            // availableQuantityToolStripMenuItem
            // 
            this.availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
            this.availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.availableQuantityToolStripMenuItem.Text = "Available Quantity...";
            this.availableQuantityToolStripMenuItem.Click += new System.EventHandler(this.availableQuantityToolStripMenuItem_Click);
            // 
            // salesStatisticsToolStripMenuItem
            // 
            this.salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
            this.salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
            // 
            // itemPicToolStripMenuItem
            // 
            this.itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
            this.itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.itemPicToolStripMenuItem.Text = "Item Photo...";
            this.itemPicToolStripMenuItem.Click += new System.EventHandler(this.itemPicToolStripMenuItem_Click);
            // 
            // itemDetailsToolStripMenuItem
            // 
            this.itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
            this.itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.itemDetailsToolStripMenuItem.Text = "Item Details...";
            this.itemDetailsToolStripMenuItem.Click += new System.EventHandler(this.itemDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // removeRowToolStripMenuItem
            // 
            this.removeRowToolStripMenuItem.Image = global::Micromind.ClientUI.Properties.Resources.Delete;
            this.removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
            this.removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeRowToolStripMenuItem.Text = "Remove Row";
            this.removeRowToolStripMenuItem.Click += new System.EventHandler(this.removeRowToolStripMenuItem_Click);
            // 
            // comboBoxGridLocation
            // 
            this.comboBoxGridLocation.Assigned = false;
            this.comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridLocation.CustomReportFieldName = "";
            this.comboBoxGridLocation.CustomReportKey = "";
            this.comboBoxGridLocation.CustomReportValueType = ((byte)(1));
            this.comboBoxGridLocation.DescriptionTextBox = null;
            appearance77.BackColor = System.Drawing.SystemColors.Window;
            appearance77.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridLocation.DisplayLayout.Appearance = appearance77;
            this.comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance78.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance78.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance78;
            appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance79;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance80.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance80.BackColor2 = System.Drawing.SystemColors.Control;
            appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance80.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance80;
            this.comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance81.BackColor = System.Drawing.SystemColors.Window;
            appearance81.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance81;
            appearance82.BackColor = System.Drawing.SystemColors.Highlight;
            appearance82.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance82;
            this.comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance83.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance83;
            appearance84.BorderColor = System.Drawing.Color.Silver;
            appearance84.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance84;
            this.comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
            appearance85.BackColor = System.Drawing.SystemColors.Control;
            appearance85.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance85.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance85.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance85;
            appearance86.TextHAlignAsString = "Left";
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance86;
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance87.BackColor = System.Drawing.SystemColors.Window;
            appearance87.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance87;
            this.comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance88.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance88;
            this.comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGridLocation.Editable = true;
            this.comboBoxGridLocation.FilterString = "";
            this.comboBoxGridLocation.HasAllAccount = false;
            this.comboBoxGridLocation.HasCustom = false;
            this.comboBoxGridLocation.IsDataLoaded = false;
            this.comboBoxGridLocation.Location = new System.Drawing.Point(654, 194);
            this.comboBoxGridLocation.MaxDropDownItems = 12;
            this.comboBoxGridLocation.Name = "comboBoxGridLocation";
            this.comboBoxGridLocation.ShowAll = false;
            this.comboBoxGridLocation.ShowConsignIn = false;
            this.comboBoxGridLocation.ShowConsignOut = false;
            this.comboBoxGridLocation.ShowDefaultLocationOnly = false;
            this.comboBoxGridLocation.ShowInactiveItems = false;
            this.comboBoxGridLocation.ShowNormalLocations = true;
            this.comboBoxGridLocation.ShowPOSOnly = false;
            this.comboBoxGridLocation.ShowQuickAdd = true;
            this.comboBoxGridLocation.ShowWarehouseOnly = false;
            this.comboBoxGridLocation.Size = new System.Drawing.Size(114, 20);
            this.comboBoxGridLocation.TabIndex = 121;
            this.comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxGridLocation.Visible = false;
            // 
            // productPhotoViewer
            // 
            this.productPhotoViewer.BackColor = System.Drawing.Color.White;
            this.productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productPhotoViewer.Location = new System.Drawing.Point(48, 251);
            this.productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.Name = "productPhotoViewer";
            this.productPhotoViewer.Size = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.TabIndex = 120;
            this.productPhotoViewer.Visible = false;
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
            // dataGridItems
            // 
            this.dataGridItems.AllowAddNew = false;
            this.dataGridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance89.BackColor = System.Drawing.SystemColors.Window;
            appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridItems.DisplayLayout.Appearance = appearance89;
            this.dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance90.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance90;
            appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
            this.dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance92.BackColor2 = System.Drawing.SystemColors.Control;
            appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
            this.dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
            appearance93.BackColor = System.Drawing.SystemColors.Window;
            appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance93;
            appearance94.BackColor = System.Drawing.SystemColors.Highlight;
            appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance94;
            this.dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance95.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance95;
            appearance96.BorderColor = System.Drawing.Color.Silver;
            appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridItems.DisplayLayout.Override.CellAppearance = appearance96;
            this.dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridItems.DisplayLayout.Override.CellPadding = 0;
            appearance97.BackColor = System.Drawing.SystemColors.Control;
            appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance97.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance97;
            appearance98.TextHAlignAsString = "Left";
            this.dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance98;
            this.dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance99.BackColor = System.Drawing.SystemColors.Window;
            appearance99.BorderColor = System.Drawing.Color.Silver;
            this.dataGridItems.DisplayLayout.Override.RowAppearance = appearance99;
            this.dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
            this.dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
            this.dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridItems.ExitEditModeOnLeave = false;
            this.dataGridItems.IncludeLotItems = false;
            this.dataGridItems.LoadLayoutFailed = false;
            this.dataGridItems.Location = new System.Drawing.Point(12, 179);
            this.dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
            this.dataGridItems.Name = "dataGridItems";
            this.dataGridItems.ShowClearMenu = true;
            this.dataGridItems.ShowDeleteMenu = true;
            this.dataGridItems.ShowInsertMenu = true;
            this.dataGridItems.ShowMoveRowsMenu = true;
            this.dataGridItems.Size = new System.Drawing.Size(752, 234);
            this.dataGridItems.TabIndex = 1;
            this.dataGridItems.Text = "dataEntryGrid1";
            // 
            // comboBoxGridItem
            // 
            this.comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
            this.comboBoxGridItem.Assigned = false;
            this.comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridItem.CustomReportFieldName = "";
            this.comboBoxGridItem.CustomReportKey = "";
            this.comboBoxGridItem.CustomReportValueType = ((byte)(1));
            this.comboBoxGridItem.DescriptionTextBox = null;
            appearance101.BackColor = System.Drawing.SystemColors.Window;
            appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridItem.DisplayLayout.Appearance = appearance101;
            this.comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance102.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance102;
            appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance104.BackColor2 = System.Drawing.SystemColors.Control;
            appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
            this.comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
            appearance105.BackColor = System.Drawing.SystemColors.Window;
            appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance105;
            appearance106.BackColor = System.Drawing.SystemColors.Highlight;
            appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance106;
            this.comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance107.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance107;
            appearance108.BorderColor = System.Drawing.Color.Silver;
            appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance108;
            this.comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
            appearance109.BackColor = System.Drawing.SystemColors.Control;
            appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance109.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance109;
            appearance110.TextHAlignAsString = "Left";
            this.comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance110;
            this.comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance111.BackColor = System.Drawing.SystemColors.Window;
            appearance111.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance111;
            this.comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
            this.comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGridItem.Editable = true;
            this.comboBoxGridItem.FilterCustomerID = "";
            this.comboBoxGridItem.FilterString = "";
            this.comboBoxGridItem.FilterSysDocID = "";
            this.comboBoxGridItem.HasAllAccount = false;
            this.comboBoxGridItem.HasCustom = false;
            this.comboBoxGridItem.IsDataLoaded = false;
            this.comboBoxGridItem.Location = new System.Drawing.Point(675, 167);
            this.comboBoxGridItem.MaxDropDownItems = 12;
            this.comboBoxGridItem.Name = "comboBoxGridItem";
            this.comboBoxGridItem.Show3PLItems = true;
            this.comboBoxGridItem.ShowInactiveItems = false;
            this.comboBoxGridItem.ShowOnlyLotItems = false;
            this.comboBoxGridItem.ShowQuickAdd = true;
            this.comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
            this.comboBoxGridItem.TabIndex = 118;
            this.comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxGridItem.Visible = false;
            // 
            // docStatusLabel
            // 
            this.docStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.docStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.docStatusLabel.DocumentNumber = "";
            this.docStatusLabel.LinkEnabled = true;
            this.docStatusLabel.Location = new System.Drawing.Point(608, 458);
            this.docStatusLabel.Name = "docStatusLabel";
            this.docStatusLabel.ShowDocNumber = true;
            this.docStatusLabel.Size = new System.Drawing.Size(131, 56);
            this.docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
            this.docStatusLabel.TabIndex = 129;
            this.docStatusLabel.Visible = false;
            // 
            // ConsignOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(778, 567);
            this.Controls.Add(this.docStatusLabel);
            this.Controls.Add(this.comboBoxGridLocation);
            this.Controls.Add(this.productPhotoViewer);
            this.Controls.Add(this.labelVoided);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridItems);
            this.Controls.Add(this.comboBoxGridItem);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(649, 396);
            this.Name = "ConsignOutForm";
            this.Text = "Consignment Out";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingAddressID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSysDoc)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
