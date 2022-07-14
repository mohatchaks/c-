using DevExpress.Utils;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class SalesInvoiceForm : Form, IForm
	{
		private bool allowNegativeQty = true;

		private DataTable deliveryNoteTable;

		private bool mergeMatrixPrint;

		private string clUserID = "";

		private bool allowEdit = true;

		private bool allowFOC;

		private bool allowMultiTemplate;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private SalesInvoiceData currentData;

		private const string TABLENAME_CONST = "Sales_Invoice";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentBillingAddressID = "";

		private ItemSourceTypes sourceDocType;

		private DataSet priceListData;

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private bool useInlineDiscount = CompanyPreferences.UseInlineDiscount;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private decimal TaxPercent = CompanyPreferences.TaxPercent;

		private bool isWeightInvoice;

		private bool useDNotePrice;

		private bool showLotDetail = CompanyPreferences.ShowLotdetailinPrintout;

		private bool showItemdetail = CompanyPreferences.ShowItemdetail;

		private bool setlastSalesprice = CompanyPreferences.SetlastSalesprice;

		private bool userViewStaus;

		private string refDocID = "";

		private string refVoucherID = "";

		private decimal discountPercAllowed;

		private DateTime refDateTime = DateTime.MinValue;

		private DateTime lastUpdateDateTime = DateTime.MinValue;

		private DataSet timeStampStatus;

		private bool allowZeroprice = CompanyPreferences.Allowzeropriceinsale;

		private bool loadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool activatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool canAccessCost;

		private bool IsTaxbasedonProfit;

		private decimal priceDiscountPercAllowed;

		private decimal TotalDiscountPercAllowed;

		private List<DateTime> dNDateList = new List<DateTime>();

		private bool allowPriceChange = true;

		private int slNo = 1;

		private bool disableDiscount;

		private string baseFilter = "";

		private string currentCustomerID;

		private bool supressInventoryMessage;

		private bool isDataLoading;

		private bool restrictTransaction;

		private bool showedNotDiscount;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool totalChanged;

		private IContainer components;

		private ToolStrip toolStrip1;

		private System.Windows.Forms.Panel panelButtons;

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

		private System.Windows.Forms.TextBox textBoxVoucherNumber;

		private System.Windows.Forms.Label label1;

		private MMLabel mmLabel1;

		private System.Windows.Forms.TextBox textBoxRef1;

		private System.Windows.Forms.TextBox textBoxNote;

		private System.Windows.Forms.Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private System.Windows.Forms.Panel panelDetails;

		private System.Windows.Forms.Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ProductComboBox comboBoxGridItem;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private System.Windows.Forms.TextBox textBoxCustomerName;

		private MMTextBox textBoxBilltoAddress;

		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.TextBox textBoxPONumber;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

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

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private PaymentTermComboBox comboBoxTerm;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem createFromDeliveryNoteToolStripMenuItem;

		private ToolStripMenuItem createFromSalesOrderToolStripMenuItem;

		private CurrencySelector comboBoxCurrency;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDueDate;

		private XPButton buttonSelectDocument;

		private System.Windows.Forms.Label labelSelectedDocs;

		private System.Windows.Forms.ListBox checkedListBoxDN;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private JobComboBox comboBoxJob1;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonDistribution;

		private System.Windows.Forms.Label label4;

		private System.Windows.Forms.TextBox textBoxRef2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private UltraTabPageControl tabPageExpense;

		private DataEntryGrid dataGridExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private CurrencyComboBox comboBoxGridCurrency;

		private ToolStripButton toolStripButtonBalance;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonInformation;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ToolTipController toolTipController1;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		private JobComboBox ComboBoxitemJob;

		private MMTextBox textBoxShipto;

		private CustomerAddressComboBox comboBoxBillingAddress;

		private Timer timer1;

		private ToolStripButton toolStripButtonMultiPreview;

		private UltraFormattedLinkLabel ultraFormattedLinkCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkCostCategory;

		private UltraFormattedLinkLabel ultraFormattedLinkProject;

		private UltraFormattedLinkLabel ultraFormattedLinkShipping;

		private UltraFormattedLinkLabel ultraFormattedLinkPaymentTerm;

		private UltraToolTipManager ultraToolTipManager1;

		private ToolStripButton toolStripButtonInfo;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ProductSpecificationComboBox comboBoxSpecification;

		private ProductStyleComboBox comboBoxStyle;

		private GroupBox groupBoxTax;

		private TableLayoutPanel tableLayoutPanel1;

		private ToolStripMenuItem createFromProformaInvoiceToolStripMenuItem;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private ToolStripMenuItem grantEditPermissionToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator8;

		private System.Windows.Forms.CheckBox checkBoxPriceIncludeTax;

		private System.Windows.Forms.Panel panel1;

		private System.Windows.Forms.Label label7;

		private PercentTextBox textBoxDiscountPercent;

		private AmountTextBox textBoxDiscountAmount;

		private System.Windows.Forms.Label label11;

		private System.Windows.Forms.Label label5;

		private AmountTextBox textBoxSubtotal;

		private System.Windows.Forms.Panel panelNonTax;

		private System.Windows.Forms.Panel panelTotal;

		private System.Windows.Forms.Label label9;

		private AmountTextBox textBoxRoundOff;

		private AmountTextBox textBoxTotal;

		private System.Windows.Forms.Label label8;

		private System.Windows.Forms.Label label14;

		private AmountTextBox textBoxTotalExpense;

		private UltraFormattedLinkLabel linkLabelTax;

		private AmountTextBox textBoxTaxAmount;

		private System.Windows.Forms.Label labelReportTo;

		private ToolStripButton toolStripButtonPaymentAllocation;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripMenuItem toolStripMenuRelationshipMap;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2010;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string CurrentCustomerID
		{
			get
			{
				return currentCustomerID;
			}
			set
			{
				currentCustomerID = value;
			}
		}

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
				bool flag3;
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					flag3 = (textBoxVoucherNumber.Enabled = true);
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
					if (sourceDocType != 0)
					{
						comboBoxCustomer.Enabled = false;
					}
					else
					{
						comboBoxCustomer.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				ToolStripButton toolStripButton2 = toolStripButtonPreview;
				flag3 = (toolStripButtonMultiPreview.Enabled = !isNewRecord);
				enabled = (toolStripButton2.Enabled = flag3);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
					grantEditPermissionToolStripMenuItem.Visible = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
				SetApprovalStatus();
			}
		}

		private bool RestrictTransaction
		{
			get
			{
				return restrictTransaction;
			}
			set
			{
				restrictTransaction = value;
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
				dataGridExpense.Enabled = !value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				panel1.Enabled = !value;
				textBoxNote.Enabled = !value;
				buttonSave.Enabled = !value;
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

		public SalesInvoiceForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridExpense.AllowCustomizeHeaders = true;
			allowNegativeQty = CompanyPreferences.AllowSalesInvoiceNegativeQty;
			comboBoxGridExpenseCode.ExpenseCodeType = ExpenseCodeTypes.InvoiceCharge;
			useDNotePrice = CompanyPreferences.CheckCreditLimitInDeliveryNote;
			ultraFormattedLinkProject.Visible = (ultraFormattedLinkCostCategory.Visible = (comboBoxJob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
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
			textBoxDiscountAmount.Validating += textBoxDiscountAmount_Validating;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			textBoxDiscountPercent.Validated += textBoxDiscountPercent_Validated;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxTerm.SelectedIndexChanged += comboBoxTerm_SelectedIndexChanged;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
			comboBoxBillingAddress.SelectedIndexChanged += comboBoxBillingAddress_SelectedIndexChanged;
			comboBoxShippingAddressID.SelectedIndexChanged += comboBoxShippingAddressID_SelectedIndexChanged;
			dataGridExpense.BeforeCellUpdate += dataGridExpense_BeforeCellUpdate;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridExpense.BeforeRowDeactivate += dataGridExpense_BeforeRowDeactivate;
			dataGridExpense.BeforeCellDeactivate += dataGridExpense_BeforeCellDeactivate;
			dataGridExpense.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
			dataGridExpense.HeaderClicked += dataGridExpense_HeaderClicked;
		}

		private void comboBoxShippingAddressID_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxShippingAddressID.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxShipto.Clear();
				}
				else
				{
					textBoxShipto.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxBillingAddress_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxBillingAddress.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxBilltoAddress.Clear();
				}
				else
				{
					textBoxBilltoAddress.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridExpense_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGridExpense_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridExpense_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridExpense.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridExpense.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridExpense.ActiveCell.Value = result;
			}
		}

		private void dataGridExpense_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridExpense.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Amount"].Value.ToString() == "" && activeRow.Cells["Expense Code"].Value.ToString() != "")
				{
					activeRow.Cells["Amount"].Value = 0;
				}
				if (activeRow.Cells["Expense Code"].Value == null || (activeRow.Cells["Expense Code"].Value.ToString() == string.Empty && activeRow.Cells["Amount"].Value.ToString() != ""))
				{
					ErrorHelper.InformationMessage("Please select an expense code for the row");
					e.Cancel = true;
					activeRow.Cells["Expense Code"].Activate();
				}
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void ApplyCustomerSettings()
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				isWeightInvoice = false;
			}
			else
			{
				object selectedCellValue = comboBoxCustomer.GetSelectedCellValue("IsWeightInvoice");
				if (selectedCellValue != null && selectedCellValue != DBNull.Value && selectedCellValue != "")
				{
					isWeightInvoice = Convert.ToBoolean(selectedCellValue.ToString());
				}
				else
				{
					isWeightInvoice = false;
				}
			}
			if (isWeightInvoice)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
			}
		}

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridExpense.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Expense Code")
					{
						dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
						ItemTaxOptions taxOption = comboBoxGridExpenseCode.TaxOption;
						dataGridExpense.ActiveRow.Cells["TaxOption"].Value = taxOption;
						switch (taxOption)
						{
						case ItemTaxOptions.BasedOnCustomer:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
							break;
						case ItemTaxOptions.Taxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridExpenseCode.TaxGroupID;
							break;
						case ItemTaxOptions.NonTaxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
							break;
						}
						dataGridExpense.ActiveRow.Cells["Amount"].Value = comboBoxGridExpenseCode.ExpenseRate;
						dataGridExpense.ActiveRow.Cells["Deductable"].Value = DefaultableBoolean.True;
						goto IL_0250;
					}
					if (!(e.Cell.Column.Key == "TaxGroupID"))
					{
						goto IL_0250;
					}
					if (Convert.ToBoolean(e.Cell.Row.Cells["Deductable"].Value.ToString()))
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.NonTaxable;
						if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridExpenseCode.TaxGroupID);
						e.Cell.Row.Cells["Tax"].Tag = tag;
						goto IL_0250;
					}
				}
				goto end_IL_0000;
				IL_0250:
				if (!(e.Cell.Column.Key == "Amount") || !(dataGridExpense.ActiveRow.Cells["Expense Code"].Value.ToString() != ""))
				{
					goto IL_03d2;
				}
				if (Convert.ToBoolean(e.Cell.Row.Cells["Deductable"].Value.ToString()))
				{
					decimal result = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result);
					decimal subtotal = decimal.Parse(textBoxTotalExpense.Text);
					decimal tradeDiscount = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result3);
					decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result2);
					result2 = result3 * result2;
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result2, result, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
					goto IL_03d2;
				}
				e.Cell.Row.Cells["Tax"].Value = 0;
				goto end_IL_0000;
				IL_03d2:
				if (e.Cell.Column.Key == "Deductable")
				{
					CalculateAllRowsTaxes();
					CalculateTotal();
				}
				end_IL_0000:;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			string text = "";
			if (dataGridItems.Rows.Count > 0 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item code"].Value != null)
			{
				text = activeRow.Cells["Item code"].Value.ToString();
			}
			if (text != "" && showItemdetail && userViewStaus)
			{
				DisplayItemDetails(text);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (allowNegativeQty)
			{
				return;
			}
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

		private void comboBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				SetDueDate();
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				SetDueDate();
			}
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Modified)
			{
				CalculateTotal();
				textBoxDiscountPercent.Modified = false;
				CalculateAllRowsTaxes();
			}
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Modified)
			{
				textBoxDiscountAmount.Modified = false;
				CalculateAllRowsTaxes();
			}
			CalculateTotal();
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ((!Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint) || Global.isUserAdmin) && e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (e.KeyCode == Keys.G && e.Alt)
			{
				dataGridItems.Focus();
				comboBoxGridItem.Focus();
			}
			if (loadItemFeatures && e.KeyCode == Keys.F3 && !activatePartsDetails)
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				_ = dataGridItems.ActiveRow;
				DataSet dataSet = new DataSet();
				dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
				if (dataGridItems.ActiveCell == null)
				{
					comboSearchDialogNew.SelectedItem = "";
				}
				else
				{
					comboBoxGridItem.SelectedID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					comboSearchDialogNew.SelectedItem = comboBoxGridItem.SelectedID;
				}
				if (comboBoxCustomer.SelectedID != "" && comboBoxCustomer.SelectedID != null)
				{
					comboSearchDialogNew.SelectedProvider = comboBoxCustomer.SelectedID;
				}
				comboSearchDialogNew.ShowDialog();
				_ = 1;
			}
			if (e.KeyCode == Keys.F6 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				ProductQuantityForm productQuantityForm = new ProductQuantityForm();
				productQuantityForm.LoadData(productID);
				productQuantityForm.ShowDialog();
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			checked
			{
				try
				{
					UltraGridRow activeRow = dataGridItems.ActiveRow;
					bool flag;
					decimal result2;
					decimal num2;
					if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == ""))
					{
						if (activeRow != null && activeRow.DataChanged && Factory.ProductSystem.IsHoldSaleonProduct(activeRow.Cells["Item Code"].Value.ToString()))
						{
							ErrorHelper.WarningMessage("Sale Hold for product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
							activeRow.CancelUpdate();
							e.Cancel = true;
						}
						else
						{
							flag = false;
							decimal result = default(decimal);
							result2 = default(decimal);
							if (activeRow == null || !activeRow.DataChanged)
							{
								goto IL_04da;
							}
							decimal.TryParse(activeRow.Cells["Price"].Value.ToString(), out result2);
							decimal num = default(decimal);
							num2 = default(decimal);
							string text = "MinPrice";
							text = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) ? "MinPrice" : "UnitPrice3");
							decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString()).ToString(), out result);
							object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", comboBoxSysDoc.LocationID);
							if (fieldValue == null)
							{
								fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", "");
							}
							if (fieldValue != null)
							{
								decimal.TryParse(fieldValue.ToString(), out result);
							}
							string text2 = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
							string text3 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
							dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
							if (text3 != "" && text2 != "")
							{
								DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text3, text2, comboBoxSysDoc.LocationID);
								if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
								{
									DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
									string a = dataRow["FactorType"].ToString();
									decimal num3 = decimal.Parse(dataRow["Factor"].ToString());
									if (a == "M")
									{
										result /= num3;
									}
									else
									{
										result *= num3;
									}
								}
							}
							num = result * (priceDiscountPercAllowed / 100m);
							num2 = result - num;
							if (!(result2 < num2))
							{
								goto IL_04a7;
							}
							flag = true;
							if (!(priceDiscountPercAllowed > 0m))
							{
								goto IL_04a7;
							}
							ErrorHelper.WarningMessage("More discount not allowed for the  product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
							e.Row.Cells["Price"].Value = 0;
							e.Row.Cells["Amount"].Value = 0;
							e.Row.Cells["TaxTotal"].Value = 0;
							if (!e.Row.IsAddRow)
							{
								e.Cancel = true;
							}
						}
					}
					goto end_IL_0000;
					IL_04da:
					if (CompanyPreferences.LocalSalesFlow != SalesFlows.SOThenDNThenInvoice && activeRow != null && activeRow.DataChanged && activeRow.Cells["ItemType"].Value.ToString() != "")
					{
						_ = (byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString());
					}
					if (activeRow == null || !activeRow.DataChanged || activeRow.Cells["Price"].Value == null || !(activeRow.Cells["Price"].Value.ToString() != ""))
					{
						goto IL_090a;
					}
					if (CompanyPreferences.MinPriceSaleAction == 1)
					{
						goto IL_07b5;
					}
					bool flag2 = Factory.SalesInvoiceSystem.IsBelowMinPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
					{
						flag2 = Factory.SalesInvoiceSystem.IsBelowMinAllowedPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()));
					}
					if (!flag2)
					{
						goto IL_07b5;
					}
					if (CompanyPreferences.MinPriceSaleAction == 2)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. Do you want to continue?") == DialogResult.Yes)
						{
							goto IL_07b5;
						}
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					else if (CompanyPreferences.MinPriceSaleAction == 3 && flag)
					{
						ErrorHelper.WarningMessage("The price you have entered is below the minimum price. You are not allowed to sell at this price.");
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
						if (flag)
						{
							activeRow.Cells["price"].Value = 0;
							activeRow.Cells["Amount"].Value = 0;
						}
					}
					else
					{
						if (CompanyPreferences.MinPriceSaleAction != 4)
						{
							goto IL_07b5;
						}
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
						{
							if (new ApprovalPasswordForm
							{
								CorrectPassword = CompanyPreferences.MinPriceSalePassword
							}.ShowDialog() == DialogResult.OK)
							{
								goto IL_07b5;
							}
							if (!e.Row.IsAddRow)
							{
								e.Cancel = true;
							}
						}
						else if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					goto end_IL_0000;
					IL_04a7:
					if (Global.IsUserAdmin || priceDiscountPercAllowed == 0m)
					{
						flag = false;
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount) && result2 < num2)
					{
						flag = true;
					}
					goto IL_04da;
					IL_07b5:
					if (CompanyPreferences.PricelessCostAction == 1 || !Factory.SalesInvoiceSystem.IsBelowAverageCost(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString())))
					{
						goto IL_090a;
					}
					if (CompanyPreferences.PricelessCostAction == 2)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. Do you want to continue?") == DialogResult.Yes)
						{
							goto IL_090a;
						}
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					else if (CompanyPreferences.PricelessCostAction == 3)
					{
						ErrorHelper.WarningMessage("The price you have entered is below the cost. You are not allowed to sell at this price.");
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					else
					{
						if (CompanyPreferences.PricelessCostAction != 4)
						{
							goto IL_090a;
						}
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
						{
							if (new ApprovalPasswordForm
							{
								CorrectPassword = CompanyPreferences.PricelessCostPassword
							}.ShowDialog() == DialogResult.OK)
							{
								goto IL_090a;
							}
							if (!e.Row.IsAddRow)
							{
								e.Cancel = true;
							}
						}
						else if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					goto end_IL_0000;
					IL_090a:
					activeRow.Cells["Price"].Appearance.BackColor = Color.Transparent;
					end_IL_0000:;
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotal();
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void textBoxDiscountAmount_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num3 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			if (!(num == 0m))
			{
				if (num > num2 + num3)
				{
					ErrorHelper.InformationMessage("Discount amount should be less or equal to the subtotal.");
					e.Cancel = true;
				}
				else if (num < 0m)
				{
					ErrorHelper.InformationMessage("Discount amount should be greater than or equal to zero.");
					e.Cancel = true;
				}
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
			if (dataGridItems.ActiveRow == null || ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
			else if (ultraGridColumn.Key == "Price")
			{
				ProductPriceListForm productPriceListForm = new ProductPriceListForm();
				productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
				{
					string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
					string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					if (text2 != "" && text != "")
					{
						DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
						if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
						{
							DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
							string a = dataRow["FactorType"].ToString();
							decimal num = decimal.Parse(dataRow["Factor"].ToString());
							decimal num2 = default(decimal);
							num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
							if (a == "M")
							{
								num2 /= num;
							}
							else
							{
								num2 *= num;
							}
							dataGridItems.ActiveRow.Cells["Price"].Value = num2;
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						}
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else if (ultraGridColumn.Key == "Tax" && dataGridItems.ActiveRow != null)
			{
				TaxTransactionData taxData = new TaxTransactionData();
				if (dataGridItems.ActiveRow.Cells["Tax"].Tag != null)
				{
					taxData = (dataGridItems.ActiveRow.Cells["Tax"].Tag as TaxTransactionData);
				}
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog();
			}
		}

		private void dataGridExpense_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridExpense.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Tax" && dataGridExpense.ActiveRow != null)
			{
				TaxTransactionData taxData = new TaxTransactionData();
				if (dataGridExpense.ActiveRow.Cells["Tax"].Tag != null)
				{
					taxData = (dataGridExpense.ActiveRow.Cells["Tax"].Tag as TaxTransactionData);
				}
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog();
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				bool result = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result);
				if (result && IsNewRecord)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
				}
				else
				{
					textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
					CurrentCustomerID = comboBoxCustomer.SelectedID;
					comboBoxBillingAddress.Clear();
					comboBoxShippingAddressID.Clear();
					LoadCustomerPriceList();
					if (!isDataLoading && comboBoxCustomer.SelectedID != "")
					{
						comboBoxCurrency.SelectedID = comboBoxCustomer.DefaultCurrencyID;
						comboBoxCurrency.GetLastRate();
						comboBoxTerm.SelectedID = comboBoxCustomer.DefaultPaymentTerm;
						comboBoxSalesperson.SelectedID = comboBoxCustomer.DefaultSalesPersonID;
						if (comboBoxSalesperson.SelectedID == "")
						{
							comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
						}
						comboBoxShippingMethod.SelectedID = comboBoxCustomer.DefaultShippingMethod;
						string defaultShipToAddress = comboBoxCustomer.DefaultShipToAddress;
						comboBoxShippingAddressID.IsDataLoaded = true;
						comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
						if (useJobCosting)
						{
							comboBoxJob.Clear();
							comboBoxJob.Filter(comboBoxCustomer.SelectedID.Trim());
						}
						if (defaultShipToAddress == "")
						{
							comboBoxShippingAddressID.SelectedID = "PRIMARY";
						}
						else
						{
							comboBoxShippingAddressID.SelectedID = defaultShipToAddress;
						}
						defaultShipToAddress = comboBoxCustomer.DefaultBillToAddress;
						comboBoxBillingAddress.IsDataLoaded = true;
						comboBoxBillingAddress.Filter(comboBoxCustomer.SelectedID);
						if (defaultShipToAddress == "")
						{
							comboBoxBillingAddress.SelectedID = "PRIMARY";
						}
						else
						{
							comboBoxBillingAddress.SelectedID = defaultShipToAddress;
						}
						if (CompanyPreferences.IsTax)
						{
							comboBoxPayeeTaxGroup.Clear();
							if (comboBoxCustomer.TaxOption == PayeeTaxOptions.Taxable)
							{
								comboBoxPayeeTaxGroup.SelectedID = comboBoxCustomer.DefaultTaxGroupID;
							}
						}
						else
						{
							comboBoxPayeeTaxGroup.Clear();
						}
						SetDueDate();
					}
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
					UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
					ultraToolTipInfo.ToolTipText = "";
					if (fieldValue != null)
					{
						ultraToolTipInfo.ToolTipText = fieldValue.ToString();
					}
					if (!fieldValue.IsNullOrEmpty())
					{
						toolStripButtonInfo.Visible = true;
						ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
					}
					else
					{
						ultraToolTipInfo.ToolTipText = "";
						ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
						toolStripButtonInfo.Visible = false;
					}
					ApplyCustomerSettings();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadCustomerPriceList()
		{
			try
			{
				if (!isDataLoading)
				{
					priceListData = Factory.PriceListSystem.GetActivePriceListByCustomerID(comboBoxCustomer.SelectedID);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetDueDate()
		{
			dateTimePickerDueDate.Value = Global.CalculateDueDate(dateTimePickerDate.Value, comboBoxTerm.SelectedID);
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!isDataLoading)
				{
					if (isNewRecord)
					{
						textBoxVoucherNumber.Text = GetNextVoucherNumber();
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowFOC", "SysDocID", comboBoxSysDoc.SelectedID);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						allowFOC = bool.Parse(fieldValue.ToString());
					}
					if (allowFOC)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = false;
						dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = false;
						dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = false;
					}
					else
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = true;
						dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = true;
						dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = true;
					}
					comboBoxCustomer.FilterSysDocID = comboBoxSysDoc.SelectedID;
					comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
					allowMultiTemplate = false;
					object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowMultiTemplate", "SysDocID", comboBoxSysDoc.SelectedID);
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						allowMultiTemplate = bool.Parse(fieldValue2.ToString());
					}
					if (allowMultiTemplate)
					{
						toolStripButtonMultiPreview.Visible = true;
					}
					else
					{
						toolStripButtonMultiPreview.Visible = false;
					}
					System.Windows.Forms.CheckBox checkBox = checkBoxPriceIncludeTax;
					bool visible = checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
					checkBox.Visible = visible;
					allowPriceChange = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowToChangeSalesInvoicePrice, 25, comboBoxSysDoc.SelectedID, defaultValue: true);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow == null)
				{
					return;
				}
				if (e.Cell.Column.Key == "Item Code")
				{
					if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
					{
						comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
					}
					else if (comboBoxGridItem.SelectedRow == null)
					{
						return;
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Matrix)
					{
						supressInventoryMessage = true;
						MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
						if (!allowNegativeQty)
						{
							matrixSelectionForm.AllowNegativeQuantity = false;
						}
						string text = "";
						if (comboBoxCustomer.SelectedID != "")
						{
							text = comboBoxCustomer.GetSelectedCellValue("PriceLevelID").ToString();
						}
						if (text != "")
						{
							matrixSelectionForm.PriceLevel = text;
						}
						matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
						string text2 = "";
						if (dataGridItems.ActiveRow.Index > 0)
						{
							text2 = dataGridItems.Rows[0].Cells["Location"].Value.ToString();
						}
						dataGridItems.ActiveRow.Delete(displayPrompt: false);
						if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
						{
							foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
							{
								UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
								ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
								ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
								if (text2 == "")
								{
									text2 = Security.DefaultInventoryLocationID;
								}
								ultraGridRow.Cells["Location"].Value = text2;
								ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
								ultraGridRow.Cells["Price"].Value = row["UnitPrice"].ToString();
								decimal result = default(decimal);
								decimal result2 = default(decimal);
								decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result);
								decimal.TryParse(ultraGridRow.Cells["Price"].Value.ToString(), out result2);
								decimal num = result * result2;
								ultraGridRow.Cells["Amount"].Value = num;
								ultraGridRow.Update();
							}
						}
						CalculateTotal();
						supressInventoryMessage = false;
						return;
					}
					checked
					{
						if (comboBoxGridItem.SelectedID != "")
						{
							object obj2 = dataGridItems.ActiveRow.Cells["Description"].Value = (dataGridItems.ActiveRow.Cells["DefaultDescription"].Value = comboBoxGridItem.SelectedName);
							dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
							dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
							dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
							dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
							dataGridItems.ActiveRow.Cells["HiddenAverageCost"].Value = comboBoxGridItem.SelectedRow.Cells["AverageCost"].Value;
							dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
							dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
							ItemTaxOptions taxOption = comboBoxGridItem.TaxOption;
							dataGridItems.ActiveRow.Cells["TaxOption"].Value = taxOption;
							switch (taxOption)
							{
							case ItemTaxOptions.BasedOnCustomer:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridItem.TaxGroupID;
								break;
							case ItemTaxOptions.NonTaxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							if (priceListData != null)
							{
								SetPriceAndDescription();
							}
							else
							{
								decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
								dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
							}
							if (setlastSalesprice)
							{
								decimal lastSaleTransationByCustomerID = Factory.ProductSystem.GetLastSaleTransationByCustomerID(comboBoxGridItem.SelectedID, comboBoxCustomer.SelectedID);
								dataGridItems.ActiveRow.Cells["Price"].Value = lastSaleTransationByCustomerID;
							}
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
							if (comboBoxGridItem.SelectedRow != null)
							{
								dataGridItems.ActiveRow.Cells["Cost"].Value = comboBoxGridItem.SelectedRow.Cells["StandardCost"].Value.ToString();
							}
							else
							{
								dataGridItems.ActiveRow.Cells["Cost"].Value = null;
							}
							if (comboBoxGridItem.SelectedRow != null)
							{
								dataGridItems.ActiveRow.Cells["ItemType"].Value = comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString();
							}
							else
							{
								dataGridItems.ActiveRow.Cells["ItemType"].Value = null;
							}
							if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Location"].Value;
							}
							if ((dataGridItems.ActiveRow.Cells["SpecificationID"].Value == null || dataGridItems.ActiveRow.Cells["SpecificationID"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["SpecificationID"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["SpecificationID"].Value;
							}
							if ((dataGridItems.ActiveRow.Cells["Style"].Value == null || dataGridItems.ActiveRow.Cells["Style"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Style"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Style"].Value;
							}
							if (comboBoxGridItem.SelectedItemType == ItemTypes.Discount)
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Value = -1;
								dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.Disabled;
							}
							else
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.AllowEdit;
							}
						}
					}
				}
				else if (e.Cell.Column.Key == "TaxGroupID")
				{
					ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
					if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
					}
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridItem.TaxGroupID);
					e.Cell.Row.Cells["Tax"].Tag = tag;
				}
				else if (e.Cell.Column.Key == "Location" && e.Cell.Row.Cells["Quantity"].Tag != null)
				{
					e.Cell.Row.Cells["Quantity"].Tag = null;
					e.Cell.Row.Cells["Quantity"].Value = 0;
					e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
				}
				if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null)
				{
					if (priceListData == null || (dataGridItems.ActiveRow.Cells["Unit"].Value == null && e.Cell.Value.ToString() == ""))
					{
						return;
					}
					SetPriceAndDescription();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			try
			{
				string key;
				decimal result4;
				if (dataGridItems.ActiveRow != null)
				{
					key = e.Cell.Column.Key;
					decimal result3 = default(decimal);
					result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal result8 = default(decimal);
					decimal result9 = default(decimal);
					decimal result10 = default(decimal);
					decimal result11 = default(decimal);
					decimal result12 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].OriginalValue.ToString(), out result11);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].OriginalValue.ToString(), out result12);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result3);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result4);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Discount"].Value.ToString(), out result6);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result5);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["FOCQuantity"].Value.ToString(), out result7);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["WeightQuantity"].Value.ToString(), out result8);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["WeightPrice"].Value.ToString(), out result9);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Tax"].Value.ToString(), out result10);
					dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Unit"].OriginalValue.ToString();
					if ((key == "Quantity" || key == "FOCQuantity") && dataGridItems.ActiveCell != null && (dataGridItems.ActiveCell.Column.Key == "Quantity" || dataGridItems.ActiveCell.Column.Key == "FOCQuantity"))
					{
						if (!isWeightInvoice)
						{
							result5 = (useInlineDiscount ? Math.Round((result3 - result7) * (result4 - result6), Global.CurDecimalPoints) : Math.Round((result3 - result7) * result4, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
							dataGridItems.ActiveRow.Cells["InvQuantity"].Value = result3 - result7;
						}
						goto IL_1997;
					}
					if (key == "WeightQuantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "WeightQuantity")
					{
						if (isWeightInvoice)
						{
							result5 = Math.Round(result8 * result9, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(result5 / result3, 5);
						}
						goto IL_1997;
					}
					if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
					{
						if (!isWeightInvoice)
						{
							result5 = (useInlineDiscount ? Math.Round((result3 - result7) * (result4 - result6), Global.CurDecimalPoints) : Math.Round((result3 - result7) * result4, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						}
						goto IL_1997;
					}
					if (key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
					{
						result5 = (useInlineDiscount ? Math.Round(result3 * result4 - result6, Global.CurDecimalPoints) : Math.Round(result3 * result4, Global.CurDecimalPoints));
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						goto IL_1997;
					}
					if (key == "Location" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Location")
					{
						result5 = (useInlineDiscount ? Math.Round(result3 * result4 - result6, Global.CurDecimalPoints) : Math.Round(result3 * result4, Global.CurDecimalPoints));
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						goto IL_1997;
					}
					if (key == "WeightPrice" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "WeightPrice")
					{
						if (isWeightInvoice)
						{
							result5 = Math.Round(result8 * result9, 2);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(result5 / result3, 5);
						}
						goto IL_1997;
					}
					if (key == "Discount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Discount")
					{
						if (isWeightInvoice)
						{
							ErrorHelper.WarningMessage("Inline discount is not allowed when invoicing on weight.");
						}
						else
						{
							result5 = (useInlineDiscount ? Math.Round((result3 - result7) * (result4 - result6), Global.CurDecimalPoints) : Math.Round((result3 - result7) * result4, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						}
						goto IL_1997;
					}
					if (!(key == "Amount") || dataGridItems.ActiveCell == null || !(dataGridItems.ActiveCell.Column.Key == "Amount"))
					{
						if (key == "Item Code")
						{
							if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
							}
							result5 = (useInlineDiscount ? Math.Round((result3 - result7) * (result4 - result6), Global.CurDecimalPoints) : Math.Round((result3 - result7) * result4, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						}
						goto IL_1997;
					}
					if (result3 == 0m)
					{
						result3 = 1m;
						dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
					}
					if (isWeightInvoice && result8 == 0m)
					{
						result8 = 1m;
						dataGridItems.ActiveRow.Cells["WeightQuantity"].Value = result8;
					}
					ItemTypes itemTypes2 = ItemTypes.Inventory;
					if (dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString() != "")
					{
						itemTypes2 = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
					}
					if ((itemTypes2 == ItemTypes.Discount && result5 > 0m) || (result3 < 0m && result5 > 0m))
					{
						result5 = -1m * Math.Abs(result5);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
					}
					result4 = Math.Round((result5 + result6) / result3, 4);
					if (isWeightInvoice)
					{
						result9 = Math.Round(result5 / result8, 5);
					}
					if (!(result5 < 0m))
					{
						goto IL_1872;
					}
					if (!isWeightInvoice)
					{
						result3 = -1m * Math.Abs(result3);
						dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
						goto IL_1872;
					}
					ErrorHelper.WarningMessage("Negative amount is not allowed when invoicing on weight.");
					e.Cell.Value = 0;
				}
				goto end_IL_0e4d;
				IL_1997:
				if (key == "Amount")
				{
					decimal result13 = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result13);
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
					decimal result14 = default(decimal);
					decimal result15 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result15);
					decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result14);
					result14 = result15 * result14;
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result14, result13, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
				}
				if (key == "Cost")
				{
					decimal result16 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Amount"].Value.ToString(), out result16);
					decimal subtotal2 = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount2 = decimal.Parse(textBoxDiscountAmount.Text);
					decimal result17 = default(decimal);
					decimal result18 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result18);
					decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result17);
					result17 = result18 * result17;
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result17, result16, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
				}
				checked
				{
					if (e.Cell.Column.Key == "SpecificationID")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["SpecificationID"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["SpecificationID"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (e.Cell.Column.Key == "Style")
					{
						for (int j = e.Cell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
						{
							if (dataGridItems.Rows[j].Cells["Style"].Value.ToString() == "")
							{
								dataGridItems.Rows[j].Cells["Style"].Value = e.Cell.Value.ToString();
							}
						}
					}
					goto end_IL_0e4d;
				}
				IL_1872:
				dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result4);
				goto IL_1997;
				end_IL_0e4d:;
			}
			catch (Exception e3)
			{
				dataGridItems.ActiveRow.Cells["Price"].Value = 0;
				dataGridItems.ActiveRow.Cells["Amount"].Value = 0;
				ErrorHelper.ProcessError(e3);
			}
		}

		private void SetPriceAndDescription()
		{
			string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
			string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
			string text3 = "ProductID='" + text2 + "' ";
			if (text != "")
			{
				text3 = text3 + " AND UnitID='" + text + "'";
			}
			DataRow[] array = priceListData.Tables[0].Select(text3);
			if (array.Length != 0)
			{
				if (loadItemDescFromPriceList)
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = array[0]["Description"].ToString();
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = decimal.Parse(array[0]["UnitPrice"].ToString());
			}
			else
			{
				decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
			}
			if (!(text2 != "") || !(text != ""))
			{
				return;
			}
			DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
			if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
			{
				DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
				string a = dataRow["FactorType"].ToString();
				decimal num = decimal.Parse(dataRow["Factor"].ToString());
				decimal num2 = decimal.Parse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString());
				if (a == "M")
				{
					num2 /= num;
				}
				else
				{
					num2 *= num;
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = num2;
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && (dataGridItems.ActiveRow.Cells["RefSlNo"].Value == null || dataGridItems.ActiveRow.Cells["RefSlNo"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
				{
					int.TryParse(dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["RefSlNo"].Text.ToString(), out slNo);
					dataGridItems.ActiveRow.Cells["RefSlNo"].Value = slNo + 1;
				}
			}
		}

		private void dataGridItems_KeyDown(object sender, KeyPressEventArgs e)
		{
			if (dataGridItems.ActiveCell == null)
			{
				return;
			}
			UltraGridColumn column = dataGridItems.ActiveCell.Column;
			if (dataGridItems.ActiveRow == null || column == null || !(column.Key == "Price") || !char.IsLetter(e.KeyChar))
			{
				return;
			}
			ProductPriceListForm productPriceListForm = new ProductPriceListForm();
			productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
			if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
			{
				e.Handled = true;
				string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
				string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				if (text2 != "" && text != "")
				{
					DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
					if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
					{
						DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
						string a = dataRow["FactorType"].ToString();
						decimal num = decimal.Parse(dataRow["Factor"].ToString());
						decimal num2 = default(decimal);
						num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						if (a == "M")
						{
							num2 /= num;
						}
						else
						{
							num2 *= num;
						}
						dataGridItems.ActiveRow.Cells["Price"].Value = num2;
					}
					else
					{
						dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else
			{
				e.Handled = true;
			}
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
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
			checked
			{
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
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
				{
					for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["Location"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Job")
				{
					for (int j = dataGridItems.ActiveCell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
					{
						if (dataGridItems.Rows[j].Cells["Job"].Value.ToString() == "")
						{
							dataGridItems.Rows[j].Cells["Job"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else
				{
					if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "CostCategory"))
					{
						return;
					}
					for (int k = dataGridItems.ActiveCell.Row.Index + 1; k < dataGridItems.Rows.Count; k++)
					{
						if (dataGridItems.Rows[k].Cells["CostCategory"].Value.ToString() == "")
						{
							dataGridItems.Rows[k].Cells["CostCategory"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Item Code"].Activate();
				return;
			}
			if (activeRow.Cells["Quantity"].Value.ToString() == "")
			{
				activeRow.Cells["Quantity"].Value = 0;
			}
			if (decimal.Parse(activeRow.Cells["Quantity"].Value.ToString()) == 0m)
			{
				activeRow.Cells["Amount"].Value = 0;
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && e.Cell.DataChanged && e.Cell.Column.Key == "Quantity")
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (activeRow.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString()));
				}
				if ((CompanyPreferences.NegativeQuantityAction != 1 || itemTypes == ItemTypes.ConsignmentItem) && !supressInventoryMessage)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					if (!Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Sales_Invoice_Detail", sysDocID, voucherID, decimal.Parse(e.NewValue.ToString())))
					{
						if (itemTypes == ItemTypes.ConsignmentItem)
						{
							ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
							e.Cancel = true;
							return;
						}
						if (CompanyPreferences.NegativeQuantityAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
							{
								e.Cancel = true;
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
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (e.Cell.Row.Cells["RowSourceType"].Value.ToString() != "")
				{
					itemSourceTypes = (ItemSourceTypes)int.Parse(e.Cell.Row.Cells["RowSourceType"].Value.ToString());
				}
				if (e.Cell.Column.Key == "Quantity" && sourceDocType == ItemSourceTypes.SalesOrder && !CompanyPreferences.AllowLocalSellQtyMoreThanSO && itemSourceTypes == ItemSourceTypes.SalesOrder)
				{
					decimal d = decimal.Parse(e.NewValue.ToString());
					decimal result = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Shipped"].Value.ToString(), out result);
					decimal result2 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Ordered"].Value.ToString(), out result2);
					if (d > result2 - result)
					{
						ErrorHelper.InformationMessage("Invoice quantity cannot be greater than ordered quantity.");
						e.Cancel = true;
						return;
					}
				}
			}
			if (dataGridItems.ActiveCell != null)
			{
				if (e.Cell.DataChanged && e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
				{
					e.Cancel = true;
				}
				if (activeRow != null && e.Cell.Column.Key == "Discount" && dataGridItems.ActiveCell.Column.Key == "Discount")
				{
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result3);
					decimal.TryParse(e.NewValue.ToString(), out result4);
					if (result4 > result3)
					{
						ErrorHelper.InformationMessage("Discount can not be greater than Price!");
						e.Cancel = true;
						return;
					}
				}
			}
			if (activeRow != null && e.Cell.Column.Key == "FOCQuantity")
			{
				decimal result5 = default(decimal);
				decimal result6 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["quantity"].Value.ToString(), out result5);
				decimal.TryParse(e.NewValue.ToString(), out result6);
				if (result6 > result5)
				{
					ErrorHelper.InformationMessage("FOC quantity cannot be greater than invoiced quantity.");
					e.Cancel = true;
				}
			}
		}

		private void SetQuantity(UltraGridRow row)
		{
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (row.Cells["ItemType"].Value.ToString() != "")
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(row.Cells["ItemType"].Value.ToString()));
			}
			if ((CompanyPreferences.NegativeQuantityAction == 1 && itemTypes != ItemTypes.ConsignmentItem) || supressInventoryMessage)
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
			if (Factory.ProductSystem.IsSufficientQuantityOnhand(row.Cells["Item Code"].Value.ToString(), row.Cells["Unit"].Value.ToString(), row.Cells["Location"].Value.ToString(), "Sales_Invoice_Detail", sysDocID, voucherID, decimal.Parse(row.Cells["Quantity"].Value.ToString())))
			{
				return;
			}
			if (itemTypes == ItemTypes.ConsignmentItem)
			{
				ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
				row.Cells["Quantity"].Value = 0;
			}
			else if (CompanyPreferences.NegativeQuantityAction == 2)
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
				{
					row.Cells["Quantity"].Value = 0;
				}
			}
			else if (CompanyPreferences.NegativeQuantityAction == 3)
			{
				ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
				row.Cells["Quantity"].Value = 0;
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (dataGridItems.ActiveCell == null)
			{
				return true;
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackLot"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackLot"].Value.ToString(), out result);
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
			}
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (dataGridItems.ActiveRow.Cells["ItemType"].Value != null && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()))
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
			}
			if (result || itemTypes == ItemTypes.ConsignmentItem)
			{
				if (dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = cell.Row.Cells["Item Code"].Value.ToString();
				issueLotSelectionForm.ProductDescription = cell.Row.Cells["Description"].Value.ToString();
				issueLotSelectionForm.LocationID = cell.Row.Cells["Location"].Value.ToString();
				issueLotSelectionForm.RowQuantity = float.Parse(cell.Text.ToString());
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity" || dataGridItems.ActiveCell.Column.Key.ToString() == "FOCQuantity")
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Discount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid discount! Please enter a valid value.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SalesInvoiceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesInvoiceTable.Rows[0] : currentData.SalesInvoiceTable.NewRow();
				if (clUserID != "")
				{
					dataRow["CLUserID"] = clUserID;
				}
				else
				{
					dataRow["CLUserID"] = DBNull.Value;
				}
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["DueDate"] = dateTimePickerDueDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["SalesFlow"] = CompanyPreferences.LocalSalesFlow;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ShipToAddress"] = textBoxShipto.Text;
				dataRow["IsCash"] = false;
				dataRow["IsWeightInvoice"] = isWeightInvoice;
				dataRow["SourceDocType"] = sourceDocType;
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
				}
				if (comboBoxBillingAddress.SelectedID != "")
				{
					dataRow["BillingAddressID"] = comboBoxBillingAddress.SelectedID;
				}
				else
				{
					dataRow["BillingAddressID"] = DBNull.Value;
				}
				if (comboBoxTerm.SelectedID != "")
				{
					dataRow["TermID"] = comboBoxTerm.SelectedID;
				}
				else
				{
					dataRow["TermID"] = DBNull.Value;
				}
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
				}
				if (CompanyPreferences.IsTax)
				{
					dataRow["TaxOption"] = (byte)comboBoxCustomer.TaxOption;
					dataRow["PriceIncludeTax"] = checkBoxPriceIncludeTax.Checked;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
					dataRow["PriceIncludeTax"] = false;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalespersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalespersonID"] = DBNull.Value;
				}
				dataRow["ReportTo"] = labelReportTo.Text;
				dataRow["CurrentUser"] = Global.CurrentUser.ToString();
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow["CurrencyRate"] = comboBoxCurrency.Rate;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = DBNull.Value;
				}
				if (comboBoxJob.SelectedID != "")
				{
					dataRow["JobID"] = comboBoxJob.SelectedID;
				}
				else
				{
					dataRow["JobID"] = DBNull.Value;
				}
				if (comboBoxCostCategory.SelectedID != "")
				{
					dataRow["CostCategoryID"] = comboBoxCostCategory.SelectedID;
				}
				else
				{
					dataRow["CostCategoryID"] = DBNull.Value;
				}
				dataRow["PONumber"] = textBoxPONumber.Text;
				dataRow["Discount"] = textBoxDiscountAmount.Text;
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				decimal num = default(decimal);
				num = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
				dataRow["RoundOff"] = num;
				dataRow["Total"] = textBoxTotal.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.SalesInvoiceTable.Rows.Add(dataRow);
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.SalesInvoiceDetailTable.Columns.Contains(column.Key))
					{
						currentData.SalesInvoiceDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.SalesInvoiceDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.SalesInvoiceDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					if (!dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden && row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
					{
						dataRow2["FOCQuantity"] = ((!string.IsNullOrEmpty(row.Cells["FOCQuantity"].Value.ToString())) ? float.Parse(row.Cells["FOCQuantity"].Value.ToString()) : 0f);
					}
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
					{
						if (row.Cells["SOSysDocID"].Value.ToString() != "")
						{
							dataRow2["OrderSysDocID"] = row.Cells["SOSysDocID"].Value.ToString();
						}
						if (row.Cells["SOVoucherID"].Value.ToString() != "")
						{
							dataRow2["OrderVoucherID"] = row.Cells["SOVoucherID"].Value.ToString();
						}
						if (row.Cells["SORowIndex"].Value.ToString() != "")
						{
							dataRow2["OrderRowIndex"] = row.Cells["SORowIndex"].Value.ToString();
						}
						if (row.Cells.Exists("IsDNRow") && row.Cells["IsDNRow"].Value.ToString() != "")
						{
							dataRow2["IsDNRow"] = true;
						}
					}
					if (isWeightInvoice)
					{
						dataRow2["WeightQuantity"] = row.Cells["WeightQuantity"].Value.ToString();
						dataRow2["WeightPrice"] = row.Cells["WeightPrice"].Value.ToString();
					}
					dataRow2["SpecificationID"] = row.Cells["SpecificationID"].Value.ToString();
					dataRow2["StyleID"] = row.Cells["Style"].Value.ToString();
					dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
					dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
					dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
					dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["RefSlNo"].Value.ToString()))
					{
						dataRow2["RefSlNo"] = row.Cells["RefSlNo"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefText1"].Value.ToString()))
					{
						dataRow2["RefText1"] = row.Cells["RefText1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefText2"].Value.ToString()))
					{
						dataRow2["RefText2"] = row.Cells["RefText2"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefNum1"].Value.ToString()))
					{
						dataRow2["RefNum1"] = row.Cells["RefNum1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefNum2"].Value.ToString()))
					{
						dataRow2["RefNum2"] = row.Cells["RefNum2"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefDate1"].Value.ToString()))
					{
						dataRow2["RefDate1"] = row.Cells["RefDate1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefDate2"].Value.ToString()))
					{
						dataRow2["RefDate2"] = row.Cells["RefDate2"].Value.ToString();
					}
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					if (row.Cells["ItemType"].Value != null && row.Cells["ItemType"].Value.ToString() != "")
					{
						dataRow2["ItemType"] = row.Cells["ItemType"].Value.ToString();
					}
					else
					{
						dataRow2["ItemType"] = DBNull.Value;
					}
					dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
					if (row.Cells["Cost"].Value != null && row.Cells["Cost"].Value.ToString() != "")
					{
						dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
					}
					else
					{
						dataRow2["Cost"] = DBNull.Value;
					}
					if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
					{
						dataRow2["Discount"] = row.Cells["Discount"].Value.ToString();
					}
					else
					{
						dataRow2["Discount"] = DBNull.Value;
					}
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow2["TaxOption"] = (byte)2;
					}
					if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
					{
						dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow2["TaxAmount"] = DBNull.Value;
					}
					if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow2["TaxGroupID"] = DBNull.Value;
					}
					dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					if (row.Cells["RowSourceType"].Value != null && row.Cells["RowSourceType"].Value.ToString() != "")
					{
						dataRow2["RowSource"] = row.Cells["RowSourceType"].Value.ToString();
					}
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
					{
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					if (row.Cells["CostCategory"].Value != null && row.Cells["CostCategory"].Value.ToString() != "")
					{
						dataRow2["CostCategoryID"] = row.Cells["CostCategory"].Value.ToString();
					}
					else
					{
						dataRow2["CostCategoryID"] = DBNull.Value;
					}
					dataRow2.EndEdit();
					currentData.SalesInvoiceDetailTable.Rows.Add(dataRow2);
					string b = row.Cells["Location"].Value.ToString();
					string text = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Quantity"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
						{
							if (row2["LocationID"].ToString() != b || row2["ProductID"].ToString() != text)
							{
								ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text + "'", "Please reallocate the lots for this item.");
								return false;
							}
							DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
							dataRow4["ProductID"] = row2["ProductID"];
							dataRow4["LocationID"] = row2["LocationID"];
							dataRow4["LotNumber"] = row2["LotNumber"];
							dataRow4["Reference"] = row2["Reference"];
							dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
							dataRow4["BinID"] = row2["BinID"];
							dataRow4["Reference2"] = row2["Reference2"];
							dataRow4["SoldQty"] = row2["SoldQty"];
							dataRow4["Cost"] = row2["Cost"];
							dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
							dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow4["UnitPrice"] = row.Cells["Price"].Value.ToString();
							dataRow4["RowIndex"] = row.Index;
							currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
						}
					}
				}
				currentData.InvoiceExpenseTable.Rows.Clear();
				foreach (UltraGridRow row3 in dataGridExpense.Rows)
				{
					DataRow dataRow5 = currentData.InvoiceExpenseTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["InvoiceSysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow5["InvoiceVoucherID"] = textBoxVoucherNumber.Text;
					dataRow5["ExpenseID"] = row3.Cells["Expense Code"].Value.ToString();
					dataRow5["Description"] = row3.Cells["Description"].Value.ToString();
					dataRow5["Reference"] = row3.Cells["Ref"].Value.ToString();
					dataRow5["RowIndex"] = row3.Index;
					if (row3.Cells["Amount"].Value != null && row3.Cells["Amount"].Value.ToString() != "")
					{
						dataRow5["Amount"] = decimal.Parse(row3.Cells["Amount"].Value.ToString());
					}
					else
					{
						dataRow5["Amount"] = 0;
					}
					dataRow5["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow5["CurrencyRate"] = comboBoxCurrency.Rate;
					dataRow5["RateType"] = comboBoxCurrency.RateType;
					if (!row3.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow5["TaxOption"] = row3.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow5["TaxOption"] = (byte)2;
					}
					if (row3.Cells["Tax"].Value != null && row3.Cells["Tax"].Value.ToString() != "")
					{
						dataRow5["TaxAmount"] = row3.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow5["TaxAmount"] = DBNull.Value;
					}
					if (row3.Cells["TaxGroupID"].Value != null && row3.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow5["TaxGroupID"] = row3.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow5["TaxGroupID"] = DBNull.Value;
					}
					dataRow5["IsDeduct"] = bool.Parse(row3.Cells["Deductable"].Value.ToString());
					dataRow5.EndEdit();
					currentData.InvoiceExpenseTable.Rows.Add(dataRow5);
				}
				if (sourceDocType == ItemSourceTypes.DeliveryNote)
				{
					currentData.InvoiceDNoteTable.Rows.Clear();
					foreach (object item in checkedListBoxDN.Items)
					{
						NameValue nameValue = item as NameValue;
						DataRow dataRow6 = currentData.InvoiceDNoteTable.NewRow();
						dataRow6["DNoteSysDocID"] = nameValue.ID;
						dataRow6["DNoteVoucherID"] = nameValue.Name;
						dataRow6.EndEdit();
						currentData.InvoiceDNoteTable.Rows.Add(dataRow6);
					}
				}
				if (deliveryNoteTable != null)
				{
					currentData.Tables.Add(deliveryNoteTable.Copy());
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text2 = textBoxVoucherNumber.Text;
				int num2 = 0;
				checked
				{
					foreach (UltraGridRow row4 in dataGridItems.Rows)
					{
						if (row4.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row4.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text2, num2, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num2++;
					}
					num2 = 0;
					foreach (UltraGridRow row5 in dataGridExpense.Rows)
					{
						if (row5.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row5.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Expenses, selectedID, text2, num2, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num2++;
					}
					if (textBoxTaxAmount.Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text2, -1, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
					}
					return true;
				}
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
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("SOSysDocID");
				dataTable.Columns.Add("SOVoucherID");
				dataTable.Columns.Add("SORowIndex", typeof(int));
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("Brand");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("SpecificationID");
				dataTable.Columns.Add("Style");
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("RowSourceType", typeof(int));
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("IsDNRow");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("FOCQuantity", typeof(decimal));
				dataTable.Columns.Add("InvQuantity", typeof(decimal));
				dataTable.Columns.Add("ConsignmentNo");
				dataTable.Columns.Add("WeightQuantity", typeof(decimal));
				dataTable.Columns.Add("WeightPrice", typeof(decimal));
				dataTable.Columns.Add("HiddenAverageCost", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Discount", typeof(decimal));
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataTable.Columns.Add("RefSlNo", typeof(int));
				dataTable.Columns.Add("RefText1");
				dataTable.Columns.Add("RefText2");
				dataTable.Columns.Add("RefNum1", typeof(decimal));
				dataTable.Columns.Add("RefNum2", typeof(decimal));
				dataTable.Columns.Add("RefDate1", typeof(DateTime));
				dataTable.Columns.Add("RefDate2", typeof(DateTime));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Job"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"];
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Hidden = true;
				bool flag4 = ultraGridColumn11.Hidden = flag2;
				bool flag6 = ultraGridColumn10.Hidden = flag4;
				bool flag8 = ultraGridColumn9.Hidden = flag6;
				bool flag10 = ultraGridColumn8.Hidden = flag8;
				bool flag12 = ultraGridColumn7.Hidden = flag10;
				bool flag14 = ultraGridColumn6.Hidden = flag12;
				bool flag16 = ultraGridColumn5.Hidden = flag14;
				bool flag18 = ultraGridColumn4.Hidden = flag16;
				bool flag20 = ultraGridColumn3.Hidden = flag18;
				bool hidden = ultraGridColumn2.Hidden = flag20;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"];
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"];
				UltraGridColumn ultraGridColumn14 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"];
				UltraGridColumn ultraGridColumn15 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"];
				UltraGridColumn ultraGridColumn16 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"];
				UltraGridColumn ultraGridColumn17 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"];
				flag12 = (dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true);
				flag14 = (ultraGridColumn17.Hidden = flag12);
				flag16 = (ultraGridColumn16.Hidden = flag14);
				flag18 = (ultraGridColumn15.Hidden = flag16);
				flag20 = (ultraGridColumn14.Hidden = flag18);
				hidden = (ultraGridColumn13.Hidden = flag20);
				ultraGridColumn12.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = false;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				UltraGridColumn ultraGridColumn18 = dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"];
				UltraGridColumn ultraGridColumn19 = dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"];
				UltraGridColumn ultraGridColumn20 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"];
				UltraGridColumn ultraGridColumn21 = dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"];
				UltraGridColumn ultraGridColumn22 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				flag14 = (dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true);
				flag16 = (ultraGridColumn22.Hidden = flag14);
				flag18 = (ultraGridColumn21.Hidden = flag16);
				flag20 = (ultraGridColumn20.Hidden = flag18);
				hidden = (ultraGridColumn19.Hidden = flag20);
				ultraGridColumn18.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = !canAccessCost;
				UltraGridColumn ultraGridColumn23 = dataGridItems.DisplayLayout.Bands[0].Columns["HiddenAverageCost"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn23.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Hidden = true;
				UltraGridColumn ultraGridColumn24 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn25 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn26 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn27 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn27.CellActivation = activation2;
				Activation activation6 = ultraGridColumn26.CellActivation = activation4;
				Activation activation9 = ultraGridColumn24.CellActivation = (ultraGridColumn25.CellActivation = activation6);
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance.BackColor = Color.WhiteSmoke;
				UltraGridColumn ultraGridColumn28 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn29 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn30 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn31 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				flag16 = (dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].TabStop = false);
				flag18 = (ultraGridColumn31.TabStop = flag16);
				flag20 = (ultraGridColumn30.TabStop = flag18);
				hidden = (ultraGridColumn29.TabStop = flag20);
				ultraGridColumn28.TabStop = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["HiddenAverageCost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeAdditionalColumns();
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color3 = cellAppearance6.BackColorDisabled = color;
				Color color5 = cellAppearance5.BackColorDisabled = color3;
				Color color7 = cellAppearance4.BackColorDisabled = color5;
				Color color9 = cellAppearance3.BackColorDisabled = color7;
				Color color12 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color9);
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance;
				AppearanceBase cellAppearance8 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				AppearanceBase cellAppearance10 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance;
				color5 = (dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance.ForeColor = Color.Black);
				color7 = (cellAppearance10.ForeColor = color5);
				color9 = (cellAppearance9.ForeColor = color7);
				color12 = (cellAppearance7.ForeColor = (cellAppearance8.ForeColor = color9));
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Hidden = !useInlineDiscount;
				if (useInlineDiscount)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].ValueList = comboBoxStyle;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].ValueList = comboBoxSpecification;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Header.Caption = "Default Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Header.Caption = "Weight";
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Caption = "Tax";
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Header.Caption = "Price";
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Header.Caption = CompanyPreferences.SpecificationID;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Header.Caption = CompanyPreferences.RefSlNo;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Header.Caption = CompanyPreferences.RefText1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Header.Caption = CompanyPreferences.RefText2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Header.Caption = CompanyPreferences.RefNum1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Header.Caption = CompanyPreferences.RefNum2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Header.Caption = CompanyPreferences.RefDate1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Header.Caption = CompanyPreferences.RefDate2;
				if (false)
				{
					dataGridItems.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
					dataGridItems.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
					dataGridItems.DisplayLayout.Override.RowFilterAction = RowFilterAction.AppearancesOnly;
					dataGridItems.DisplayLayout.Override.FilteredInRowAppearance.BackColor = Color.Blue;
					dataGridItems.DisplayLayout.Override.FilteredInCellAppearance.ForeColor = Color.Yellow;
					dataGridItems.DisplayLayout.Override.FilteredOutRowAppearance.BackColor = SystemColors.ControlLight;
					dataGridItems.DisplayLayout.Override.FilteredOutCellAppearance.ForeColor = SystemColors.ControlDarkDark;
				}
				else
				{
					dataGridItems.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				}
				if (allowFOC)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = false;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = true;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
				}
				UltraGridColumn ultraGridColumn32 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = true);
				ultraGridColumn32.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Header.Caption = "FOC";
				dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Header.Caption = "Inv Qty";
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Header.Caption = "Consign #";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
				UltraGridColumn ultraGridColumn33 = dataGridItems.DisplayLayout.Bands[0].Columns["Price"];
				UltraGridColumn ultraGridColumn34 = dataGridItems.DisplayLayout.Bands[0].Columns["Discount"];
				UltraGridColumn ultraGridColumn35 = dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"];
				UltraGridColumn ultraGridColumn36 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn37 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn38 = dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"];
				UltraGridColumn ultraGridColumn39 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				int num2 = dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MinWidth = 50;
				int num4 = ultraGridColumn39.MinWidth = num2;
				int num6 = ultraGridColumn38.MinWidth = num4;
				int num8 = ultraGridColumn37.MinWidth = num6;
				int num10 = ultraGridColumn36.MinWidth = num8;
				int num12 = ultraGridColumn35.MinWidth = num10;
				int num15 = ultraGridColumn33.MinWidth = (ultraGridColumn34.MinWidth = num12);
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].MinWidth = 60;
				num15 = (dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MinWidth = (dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MinWidth = 40));
				UltraGridColumn ultraGridColumn40 = dataGridItems.DisplayLayout.Bands[0].Columns["Price"];
				UltraGridColumn ultraGridColumn41 = dataGridItems.DisplayLayout.Bands[0].Columns["Discount"];
				UltraGridColumn ultraGridColumn42 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn43 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn44 = dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"];
				UltraGridColumn ultraGridColumn45 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				UltraGridColumn ultraGridColumn46 = dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"];
				UltraGridColumn ultraGridColumn47 = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"];
				UltraGridColumn ultraGridColumn48 = dataGridItems.DisplayLayout.Bands[0].Columns["Unit"];
				int num19 = dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxWidth = 100;
				int num21 = ultraGridColumn48.MaxWidth = num19;
				num2 = (ultraGridColumn47.MaxWidth = num21);
				num4 = (ultraGridColumn46.MaxWidth = num2);
				num6 = (ultraGridColumn45.MaxWidth = num4);
				num8 = (ultraGridColumn44.MaxWidth = num6);
				num10 = (ultraGridColumn43.MaxWidth = num8);
				num12 = (ultraGridColumn42.MaxWidth = num10);
				num15 = (ultraGridColumn40.MaxWidth = (ultraGridColumn41.MaxWidth = num12));
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxWidth = 200;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void ExcludeAdditionalColumns()
		{
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
		}

		private void SetupExpenseGrid()
		{
			try
			{
				dataGridExpense.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Expense Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Deductable", typeof(bool));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridExpense.DataSource = dataTable;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Hidden = !CompanyPreferences.IsTax;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridExpense.LoadLayout("ExpenseList");
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].ValueList = comboBoxGridExpenseCode;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 15;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 1;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.White;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
				dataGridExpense.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				Activation activation3 = dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = (dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].CellActivation = Activation.NoEdit);
				Color color2 = dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance.BackColorDisabled = (dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].DefaultCellValue = DefaultableBoolean.True;
				if (dataGridExpense.DisplayLayout.Bands[0].Columns.Exists("Deductable"))
				{
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellActivation = Activation.AllowEdit;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellClickAction = CellClickAction.Edit;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				}
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 70;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = 250;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Width = checked(2 * dataGridExpense.Width) / 100;
			}
			catch (Exception e)
			{
				dataGridExpense.LoadLayoutFailed = true;
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
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.ForeColorDisabled = Color.Black;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Description"].Activation = Activation.Disabled;
					row.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].TabStop = false;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("FOC", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("InvQuantity", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("RowCount", SummaryType.Count, dataGridItems.DisplayLayout.Bands[0].Columns["Description"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].Appearance.TextHAlign = HAlign.Left;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].DisplayFormat = "{0:#,##0} Rows";
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered") && !dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Ordered", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Shipped", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].DisplayFormat = "Count:{0:n}";
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangePrice))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = ComboBoxitemJob;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = ComboBoxitemcostCategory;
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"];
			bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
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
					currentData = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
						if (!isDataLoading)
						{
							comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
							comboBoxBillingAddress.Filter(comboBoxCustomer.SelectedID);
						}
						if (useJobCosting)
						{
							comboBoxJob.Filter(comboBoxCustomer.SelectedID);
						}
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
					bool flag = false;
					DataRow dataRow = currentData.Tables["Sales_Invoice"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID)
					{
						flag = true;
					}
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
					textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					labelReportTo.Text = dataRow["ReportTo"].ToString();
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						sourceDocType = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
					}
					else
					{
						sourceDocType = ItemSourceTypes.None;
					}
					if (sourceDocType != 0)
					{
						comboBoxCustomer.Enabled = false;
					}
					else
					{
						comboBoxCustomer.Enabled = true;
					}
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					if (dataRow["DiscountFC"] != DBNull.Value)
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["DiscountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (textBoxDiscountAmount.Text == 0.ToString(Format.TotalAmountFormat))
					{
						textBoxDiscountPercent.Text = "0";
					}
					if (dataRow["TaxAmountFC"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["RoundOff"] != DBNull.Value)
					{
						textBoxRoundOff.Text = decimal.Parse(dataRow["RoundOff"].ToString()).ToString(Format.TotalAmountFormat);
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_Invoice_Detail") && currentData.SalesInvoiceDetailTable.Rows.Count != 0)
					{
						CustomerData customerByID = Factory.CustomerSystem.GetCustomerByID(comboBoxCustomer.SelectedID);
						if (customerByID != null && customerByID.Tables.Count != 0 && customerByID.Tables[0].Rows.Count != 0)
						{
							ultraToolTipManager1.Enabled = true;
							UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
							ultraToolTipInfo.ToolTipText = "";
							string text = customerByID.CustomerTable.Rows[0]["DeliveryInstructions"].ToString();
							if (text != null && text != "")
							{
								ultraToolTipInfo.ToolTipText = text;
								ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
							}
							else
							{
								ultraToolTipManager1.Enabled = false;
							}
							foreach (DataRow row in currentData.Tables["Sales_Invoice_Detail"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								bool result = false;
								if (row["IsDNRow"] != DBNull.Value && bool.TryParse(row["IsDNRow"].ToString(), out result))
								{
									dataRow3["IsDNRow"] = row["IsDNRow"];
								}
								dataRow3["RowSourceType"] = row["RowSource"];
								dataRow3["Item Code"] = row["ProductID"];
								if (row["UnitQuantity"] != DBNull.Value)
								{
									dataRow3["Quantity"] = row["UnitQuantity"];
								}
								else
								{
									dataRow3["Quantity"] = row["Quantity"];
								}
								if (row["Quantity"] != DBNull.Value)
								{
									dataRow3["FOCQuantity"] = row["FOCQuantity"];
								}
								dataRow3["Description"] = row["Description"];
								dataRow3["Remarks"] = row["Remarks"];
								dataRow3["Attribute1"] = row["Attribute1"];
								dataRow3["Attribute2"] = row["Attribute2"];
								dataRow3["Attribute3"] = row["Attribute3"];
								dataRow3["Brand"] = row["Brand"];
								dataRow3["MatrixParentID"] = row["MatrixParentID"];
								dataRow3["IsTrackLot"] = row["IsTrackLot"];
								dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
								dataRow3["RefSlNo"] = row["RefSlNo"];
								dataRow3["RefText1"] = row["RefText1"];
								dataRow3["RefText2"] = row["RefText2"];
								dataRow3["RefNum1"] = row["RefNum1"];
								dataRow3["RefNum2"] = row["RefNum2"];
								dataRow3["RefDate1"] = row["RefDate1"];
								dataRow3["RefDate2"] = row["RefDate2"];
								dataRow3["Location"] = row["LocationID"];
								dataRow3["Unit"] = row["UnitID"];
								dataRow3["ItemType"] = row["ItemType"];
								dataRow3["TaxOption"] = row["TaxOption"];
								dataRow3["ConsignmentNo"] = row["ConsignNumber"];
								dataRow3["SOSysDocID"] = row["OrderSysDocID"];
								dataRow3["SOVoucherID"] = row["OrderVoucherID"];
								dataRow3["SORowIndex"] = row["OrderRowIndex"];
								dataRow3["Job"] = row["JobID"];
								dataRow3["CostCategory"] = row["CostCategoryID"];
								dataRow3["WeightQuantity"] = row["WeightQuantity"];
								dataRow3["WeightPrice"] = row["WeightPrice"];
								if (flag)
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.UnitPriceFormat);
								}
								else
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
								}
								if (row["Discount"] != DBNull.Value)
								{
									dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.UnitPriceFormat);
								}
								if (row["Cost"] != DBNull.Value)
								{
									dataRow3["Cost"] = decimal.Parse(row["Cost"].ToString()).ToString(Format.UnitPriceFormat);
								}
								if (row["TaxOption"] != DBNull.Value)
								{
									dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
								}
								else
								{
									dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
								}
								if (row["TaxAmount"] != DBNull.Value)
								{
									dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
								}
								if (!row["TaxGroupID"].IsDBNullOrEmpty())
								{
									dataRow3["TaxGroupID"] = row["TaxGroupID"];
								}
								decimal result2 = default(decimal);
								decimal result3 = default(decimal);
								decimal result4 = default(decimal);
								decimal result5 = default(decimal);
								decimal result6 = default(decimal);
								decimal result7 = default(decimal);
								decimal.TryParse(dataRow3["Quantity"].ToString(), out result2);
								decimal.TryParse(dataRow3["FOCQuantity"].ToString(), out result5);
								decimal.TryParse(dataRow3["Price"].ToString(), out result3);
								decimal.TryParse(dataRow3["Discount"].ToString(), out result4);
								decimal.TryParse(dataRow3["Tax"].ToString(), out result6);
								dataRow3["InvQuantity"] = result2 - result5;
								if (flag)
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
								}
								decimal.TryParse(dataRow3["InvQuantity"].ToString(), out result7);
								dataRow3["SpecificationID"] = row["SpecificationID"];
								dataRow3["Style"] = row["StyleID"];
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
							dataTable.AcceptChanges();
							dataTable = (dataGridExpense.DataSource as DataTable);
							dataTable.Clear();
							textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
							foreach (DataRow row2 in currentData.Tables["Sales_Invoice_Expense"].Rows)
							{
								DataRow dataRow5 = dataTable.NewRow();
								bool flag2 = false;
								dataRow5["Expense Code"] = row2["ExpenseID"];
								dataRow5["Description"] = row2["Description"];
								dataRow5["Ref"] = row2["Reference"];
								if (row2["TaxOption"] != DBNull.Value)
								{
									dataRow5["TaxOption"] = byte.Parse(row2["TaxOption"].ToString());
								}
								else
								{
									dataRow5["TaxOption"] = ItemTaxOptions.NonTaxable;
								}
								if (row2["TaxAmount"] != DBNull.Value)
								{
									dataRow5["Tax"] = decimal.Parse(row2["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
								}
								if (!row2["TaxGroupID"].IsDBNullOrEmpty())
								{
									dataRow5["TaxGroupID"] = row2["TaxGroupID"];
								}
								if (comboBoxCurrency.SelectedID != Global.BaseCurrencyID)
								{
									flag2 = true;
								}
								if (flag2)
								{
									if (row2["AmountFC"] != DBNull.Value)
									{
										dataRow5["Amount"] = decimal.Parse(row2["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
									}
								}
								else
								{
									dataRow5["Amount"] = decimal.Parse(row2["Amount"].ToString()).ToString(Format.TotalAmountFormat);
								}
								bool result8 = true;
								bool.TryParse(row2["Deductable"].ToString(), out result8);
								dataRow5["Deductable"] = result8;
								dataRow5.EndEdit();
								dataTable.Rows.Add(dataRow5);
							}
							dataTable.AcceptChanges();
							if (dataRow["IsVoid"] != DBNull.Value)
							{
								IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
							}
							else
							{
								IsVoid = false;
							}
							foreach (UltraGridRow row3 in dataGridItems.Rows)
							{
								ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
								if (row3.Cells["RowSourceType"].Value != null && row3.Cells["RowSourceType"].Value.ToString() != "")
								{
									itemSourceTypes = (ItemSourceTypes)int.Parse(row3.Cells["RowSourceType"].Value.ToString());
								}
								bool flag3 = false;
								if (row3.Cells["IsDNRow"].Value != null && row3.Cells["IsDNRow"].Value.ToString() != "")
								{
									flag3 = bool.Parse(row3.Cells["IsDNRow"].Value.ToString());
								}
								if (itemSourceTypes == ItemSourceTypes.SalesOrder)
								{
									row3.Cells["Item Code"].Activation = Activation.Disabled;
									row3.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Description"].Activation = Activation.Disabled;
									row3.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Location"].Activation = Activation.Disabled;
									row3.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Unit"].Activation = Activation.Disabled;
									row3.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
								}
								else if ((itemSourceTypes == ItemSourceTypes.DeliveryNote) | flag3)
								{
									row3.Cells["Item Code"].Activation = Activation.Disabled;
									row3.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Description"].Activation = Activation.Disabled;
									row3.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Location"].Activation = Activation.Disabled;
									row3.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Unit"].Activation = Activation.Disabled;
									row3.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Quantity"].Activation = Activation.Disabled;
									row3.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["Job"].Activation = Activation.Disabled;
									row3.Cells["Job"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["Job"].Appearance.ForeColorDisabled = Color.Black;
									row3.Cells["CostCategory"].Activation = Activation.Disabled;
									row3.Cells["CostCategory"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row3.Cells["CostCategory"].Appearance.ForeColorDisabled = Color.Black;
									if (!allowPriceChange)
									{
										row3.Cells["Price"].Activation = Activation.Disabled;
									}
								}
								ItemTypes itemTypes = (ItemTypes)checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString()));
								bool flag4 = false;
								if (row3.Cells["IsTrackLot"].Value != DBNull.Value)
								{
									flag4 = bool.Parse(row3.Cells["IsTrackLot"].Value.ToString());
								}
								if (flag4 || itemTypes == ItemTypes.ConsignmentItem)
								{
									DataRow[] array = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row3.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row3.Index);
									if (array.Length != 0)
									{
										DataSet dataSet = new DataSet();
										dataSet.Merge(array);
										DataTable tag = dataSet.Tables[0];
										row3.Cells["Quantity"].Tag = tag;
										row3.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
									}
								}
								if (itemTypes == ItemTypes.Discount)
								{
									row3.Cells["Quantity"].Activation = Activation.Disabled;
								}
								string productID = row3.Cells["Item Code"].Value.ToString();
								row3.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
								DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = " + row3.Index + " AND TaxLevel = " + (byte)2);
								if (array2.Length != 0)
								{
									TaxTransactionData taxTransactionData = new TaxTransactionData();
									taxTransactionData.Merge(array2);
									row3.Cells["Tax"].Tag = taxTransactionData;
								}
							}
							foreach (UltraGridRow row4 in dataGridExpense.Rows)
							{
								DataRow[] array3 = currentData.TaxDetailsTable.Select("RowIndex = " + row4.Index + " AND TaxLevel = " + (byte)3);
								if (array3.Length != 0)
								{
									TaxTransactionData taxTransactionData2 = new TaxTransactionData();
									taxTransactionData2.Merge(array3);
									row4.Cells["Tax"].Tag = taxTransactionData2;
								}
							}
							checkedListBoxDN.Items.Clear();
							if (currentData.Tables.Contains("Invoice_DNote"))
							{
								foreach (DataRow row5 in currentData.Tables["Invoice_DNote"].Rows)
								{
									NameValue item = new NameValue(row5["DNoteVoucherID"].ToString(), row5["DNoteSysDocID"].ToString());
									checkedListBoxDN.Items.Add(item);
								}
							}
							if (currentData.Tables.Contains("SalesProforma_Invoice"))
							{
								foreach (DataRow row6 in currentData.Tables["SalesProforma_Invoice"].Rows)
								{
									NameValue item2 = new NameValue(row6["VoucherID"].ToString(), row6["SysDocID"].ToString());
									checkedListBoxDN.Items.Add(item2);
								}
							}
							DataRow[] array4 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
							if (array4.Length != 0)
							{
								TaxTransactionData taxTransactionData3 = new TaxTransactionData();
								taxTransactionData3.Merge(array4);
								textBoxTaxAmount.Tag = taxTransactionData3;
							}
							isDiscountPercent = false;
							CalculateTotal();
							EnableButton();
							if (CompanyPreferences.LocalSalesFlow == SalesFlows.DirectInvoice)
							{
								priceListData = Factory.PriceListSystem.GetActivePriceListByCustomerID(comboBoxCustomer.SelectedID);
							}
						}
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

		private void EnableButton()
		{
			bool flag = Factory.SalesInvoiceSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, Global.CurrentUser.ToUpper(), isModify: false, "");
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.Edit && flag)
			{
				buttonSave.Enabled = true;
				screenRight.Edit = true;
			}
			else if (!screenRight.Edit)
			{
				buttonSave.Enabled = false;
				screenRight.Edit = false;
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
			dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			bool flag = false;
			try
			{
				bool flag2 = Factory.SalesInvoiceSystem.CreateSalesInvoice(currentData, !isNewRecord, TempSave: false);
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
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
						formManager.ResetDirty();
					}
				}
				return flag2;
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
				if (ex.Number == 1051)
				{
					ErrorHelper.WarningMessage(ex.Message);
					return false;
				}
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				if (flag)
				{
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						formManager.ResetDirty();
					}
				}
			}
		}

		public void SetApprovalStatus()
		{
			if (IsNewRecord)
			{
				ToolStripButton toolStripButton = toolStripButtonApproval;
				ToolStripLabel toolStripLabel = toolStripLabelApproval;
				bool flag2 = toolStripSeparatorApproval.Visible = false;
				bool visible = toolStripLabel.Visible = flag2;
				toolStripButton.Visible = visible;
			}
			else
			{
				if (currentData == null || currentData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				DataRow dataRow = currentData.Tables[0].Rows[0];
				bool flag2;
				bool visible;
				if (!dataRow.Table.Columns.Contains("ApprovalStatus") || dataRow["ApprovalStatus"].IsDBNullOrEmpty())
				{
					ToolStripButton toolStripButton2 = toolStripButtonApproval;
					ToolStripLabel toolStripLabel2 = toolStripLabelApproval;
					flag2 = (toolStripSeparatorApproval.Visible = false);
					visible = (toolStripLabel2.Visible = flag2);
					toolStripButton2.Visible = visible;
					return;
				}
				switch (int.Parse(dataRow["ApprovalStatus"].ToString()))
				{
				case 3:
					toolStripButtonApproval.Text = "Rejected";
					toolStripButtonApproval.ForeColor = Color.Red;
					break;
				case 10:
					toolStripButtonApproval.Text = "Approved";
					toolStripButtonApproval.ForeColor = Color.ForestGreen;
					break;
				default:
					toolStripButtonApproval.Text = "Pending";
					toolStripButtonApproval.ForeColor = Color.Orange;
					break;
				}
				ToolStripButton toolStripButton3 = toolStripButtonApproval;
				ToolStripLabel toolStripLabel3 = toolStripLabelApproval;
				flag2 = (toolStripSeparatorApproval.Visible = true);
				visible = (toolStripLabel3.Visible = flag2);
				toolStripButton3.Visible = visible;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Sales_Invoice", "VoucherID");
		}

		private void ValidateAllowedDiscount()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			decimal d2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num3 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			_ = d2 + num2 - num + num3;
			if (decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any) < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				return;
			}
			_ = (num < 0m);
			if (TotalDiscountPercAllowed < d && !showedNotDiscount && TotalDiscountPercAllowed > 0m && !Global.IsUserAdmin)
			{
				showedNotDiscount = true;
				ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
			}
		}

		private bool ValidateData()
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
			CalculateTotal();
			if (!ValidationwithColor())
			{
				ErrorHelper.WarningMessage("Please validate price.");
				return false;
			}
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
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (sourceDocType == ItemSourceTypes.SalesOrder)
			{
				timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
			}
			else if (sourceDocType == ItemSourceTypes.DeliveryNote)
			{
				timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Delivery_Note", refDocID, refVoucherID);
			}
			else if (sourceDocType == ItemSourceTypes.SalesProformaInvoice)
			{
				timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("SalesProforma_Invoice", refDocID, refVoucherID);
			}
			if (timeStampStatus != null && timeStampStatus.Tables.Count > 0 && timeStampStatus.Tables[0].Rows.Count > 0 && IsNewRecord)
			{
				lastUpdateDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["updatetime"].ToString());
			}
			_ = (lastUpdateDateTime > refDateTime);
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.SalesInvoice, Global.CurrentUser, includeApproveUser: false);
				if (entityApprovalStatus.Tables[0].Rows.Count > 0 && !bool.Parse(entityApprovalStatus.Tables[0].Rows[0]["ModifyTransaction"].ToString()))
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
					return false;
				}
				decimal result = -1m;
				if (!decimal.TryParse(textBoxTotal.Text, out result) || result < 0m)
				{
					ErrorHelper.InformationMessage("Cannot save an invoice with negative total.");
					return false;
				}
				bool result2 = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result2);
				if (result2)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				showedNotDiscount = false;
				ValidateAllowedDiscount();
				if (showedNotDiscount)
				{
					ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
					return false;
				}
				int num2 = 0;
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else
					{
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
						if (dataGridItems.Rows[i].Cells["Price"].Value == null || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please enter price for all the items.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (allowZeroprice && (dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0.00" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0.00"))
						{
							ErrorHelper.InformationMessage("Do not allowed to save with zero price.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
					}
					if (isWeightInvoice)
					{
						if (dataGridItems.Rows[i].Cells["WeightQuantity"].Value == null || dataGridItems.Rows[i].Cells["WeightQuantity"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please enter weight quantity for all rows.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (dataGridItems.Rows[i].Cells["WeightPrice"].Value == null || dataGridItems.Rows[i].Cells["WeightPrice"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please enter price for all rows.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
					}
					decimal d = decimal.Parse(ultraGridRow.Cells["Quantity"].Value.ToString());
					decimal d2 = decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString());
					decimal num3 = decimal.Parse(ultraGridRow.Cells["Amount"].Value.ToString());
					decimal d3 = default(decimal);
					decimal result3 = default(decimal);
					if (ultraGridRow.Cells["Discount"].Value != DBNull.Value)
					{
						decimal.TryParse(ultraGridRow.Cells["Discount"].Value.ToString(), out result3);
					}
					decimal d4 = default(decimal);
					if (ultraGridRow.Cells["FOCQuantity"].Value != null && ultraGridRow.Cells["FOCQuantity"].Value.ToString() != "")
					{
						d4 = decimal.Parse(ultraGridRow.Cells["FOCQuantity"].Value.ToString());
					}
					if (Math.Abs(Math.Round((d - d4) * (d2 - result3 + d3), Global.CurDecimalPoints) - num3) > 0.1m)
					{
						ErrorHelper.WarningMessage("Amount does not match the quantity and price for row no:" + ultraGridRow.Index + 1 + "\nItem Code:" + ultraGridRow.Cells["Item Code"].Value.ToString());
						return false;
					}
					if (num3 == 0m)
					{
						num2++;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (num2 != 0)
				{
					switch (ErrorHelper.QuestionMessageYesNo(num2 + " No. of items with Zero price, Do you wants to continue?"))
					{
					case DialogResult.Yes:
						return true;
					case DialogResult.No:
						return false;
					}
				}
				foreach (UltraGridRow row in dataGridExpense.Rows)
				{
					if (row.Cells["Expense Code"].Value == null || !(row.Cells["Expense Code"].Value.ToString() != string.Empty))
					{
						ErrorHelper.InformationMessage("Please select an expense code for a row");
						return false;
					}
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				if (!IsNewRecord && !Factory.SalesInvoiceSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already returned. You are not able to modify.");
					return false;
				}
				if (CompanyPreferences.OverCLAction != 1)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					if (Factory.CustomerSystem.IsOverCreditLimit(comboBoxCustomer.SelectedID, sysDocID, voucherID, decimal.Parse(textBoxTotal.Text), checkOpenDN: false, dateTimePickerDate.Value))
					{
						if (CompanyPreferences.OverCLAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "This transaction exceeds the customer's credit limit.", "Do you want to continue?") == DialogResult.No)
							{
								return false;
							}
						}
						else
						{
							if (CompanyPreferences.OverCLAction == 3)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "You are not allowed to sell over the customer's credit limit.");
								return false;
							}
							if (CompanyPreferences.OverCLAction == 4)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "The transaction should be approved to proceed.");
								CLPasswordForm cLPasswordForm = new CLPasswordForm();
								cLPasswordForm.CustomerID = comboBoxCustomer.SelectedID;
								cLPasswordForm.CustomerName = comboBoxCustomer.SelectedName;
								cLPasswordForm.InvoiceAmount = decimal.Parse(textBoxTotal.Text);
								if (cLPasswordForm.ShowDialog() == DialogResult.Cancel)
								{
									return false;
								}
								clUserID = cLPasswordForm.ApproverID;
							}
						}
					}
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
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				clUserID = "";
				dateTimePickerDate.Value = DateTime.Now;
				textBoxShipto.Clear();
				comboBoxBillingAddress.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPONumber.Clear();
				comboBoxSalesperson.Clear();
				comboBoxShippingAddressID.Clear();
				comboBoxShippingMethod.Clear();
				comboBoxJob.Clear();
				comboBoxJob.Filter("");
				comboBoxCostCategory.Clear();
				comboBoxTerm.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxBilltoAddress.Clear();
				comboBoxPayeeTaxGroup.Clear();
				comboBoxCustomer.Enabled = true;
				priceListData = null;
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				textBoxRoundOff.Text = 0.ToString(Format.TotalAmountFormat);
				labelReportTo.Text = "";
				checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
				dNDateList.Clear();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Ordered");
					dataTable.Columns.Remove("Shipped");
				}
				dataTable.Rows.Clear();
				IsNewRecord = true;
				dataTable = (dataGridExpense.DataSource as DataTable);
				dataTable.Rows.Clear();
				ApplyCustomerSettings();
				checkedListBoxDN.Items.Clear();
				sourceDocType = ItemSourceTypes.None;
				if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
				{
					if (!CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.AllowAddNew = false;
					}
					else
					{
						dataGridItems.AllowAddNew = true;
					}
				}
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
				deliveryNoteTable = null;
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxCustomer.Enabled = true;
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to delete.");
					return false;
				}
				if (!IsNewRecord && !Factory.SalesInvoiceSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already returned. You are not able to modify.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.SalesInvoiceSystem.DeleteSalesInvoice(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Sales_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Sales_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Sales_Invoice", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Sales_Invoice", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Sales_Invoice", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				return;
			}
			dataGridItems.SaveLayout();
			dataGridExpense.SaveLayout("ExpenseList");
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

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridExpense.SetupUI();
				SetupExpenseGrid();
				SetupGrid();
				if (!CompanyPreferences.IsTax)
				{
					panelNonTax.Visible = false;
					panelTotal.Location = new Point(761, 545);
				}
				else
				{
					panelNonTax.Visible = true;
					panelTotal.Location = new Point(761, 570);
				}
				groupBoxTax.Visible = false;
				labelTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
				checkBoxPriceIncludeTax.Visible = false;
				UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLabelTax;
				bool visible = textBoxTaxAmount.Visible = CompanyPreferences.IsTax;
				ultraFormattedLinkLabel.Visible = visible;
				UltraFormattedLinkLabel ultraFormattedLinkLabel2 = ultraFormattedLinkCurrency;
				visible = (comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency);
				ultraFormattedLinkLabel2.Visible = visible;
				mergeMatrixPrint = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.MergeMatrixItems.ToString(), false).ToString());
				comboBoxSysDoc.FilterByType(SysDocTypes.SalesInvoice);
				object obj = null;
				obj = Factory.DatabaseSystem.GetFieldValue("Tax", "CalculationMethod", "CalculationMethod", 3);
				if (obj != null)
				{
					if (int.Parse(obj.ToString()) == 3)
					{
						IsTaxbasedonProfit = true;
					}
					else
					{
						IsTaxbasedonProfit = false;
					}
				}
				SetSecurity();
				IsNewRecord = true;
				if (!base.IsDisposed)
				{
					ClearForm();
					SetupForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				dataGridExpense.LoadLayoutFailed = true;
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDueDate))
			{
				comboBoxTerm.Enabled = false;
				dateTimePickerDueDate.Enabled = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.GiveDiscount))
			{
				textBoxDiscountAmount.ReadOnly = true;
				textBoxDiscountPercent.ReadOnly = true;
				textBoxTotal.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			userViewStaus = Security.IsAllowedSecurityRole(GeneralSecurityRoles.ViewItemdetails);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowChangeBillAddress))
			{
				comboBoxBillingAddress.ReadOnly = true;
				textBoxBilltoAddress.ReadOnly = true;
			}
			else
			{
				comboBoxBillingAddress.ReadOnly = false;
				textBoxBilltoAddress.ReadOnly = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost) && IsTaxbasedonProfit)
			{
				canAccessCost = true;
				dataGridItems.DataSource = null;
				SetupGrid();
			}
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockDiscountModification) && !Global.isUserAdmin)
			{
				AmountTextBox amountTextBox = textBoxDiscountAmount;
				bool readOnly = textBoxDiscountPercent.ReadOnly = true;
				amountTextBox.ReadOnly = readOnly;
			}
			else
			{
				AmountTextBox amountTextBox2 = textBoxDiscountAmount;
				bool readOnly = textBoxDiscountPercent.ReadOnly = false;
				amountTextBox2.ReadOnly = readOnly;
			}
			TotalDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.GiveDiscount);
			priceDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.AllowPriceDiscount);
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			checkBoxPriceIncludeTax.Enabled = Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void SetupForm()
		{
			checked
			{
				switch (CompanyPreferences.LocalSalesFlow)
				{
				case SalesFlows.SOThenDNThenInvoice:
					dataGridItems.AllowAddNew = false;
					createFromSalesOrderToolStripMenuItem.Visible = false;
					toolStripButtonExcelImport.Visible = false;
					duplicateToolStripMenuItem.Enabled = false;
					buttonSelectDocument.Visible = true;
					textBoxCustomerName.Width = textBoxCustomerName.Width - buttonSelectDocument.Width - 3;
					textBoxNote.Width = 306;
					checkedListBoxDN.Visible = true;
					labelSelectedDocs.Visible = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.Disabled;
					break;
				case SalesFlows.SOThenInvoiceThenDN:
					if (CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.AllowAddNew = true;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
						dataGridItems.ShowDeleteMenu = false;
					}
					createFromDeliveryNoteToolStripMenuItem.Visible = false;
					buttonSelectDocument.Visible = true;
					duplicateToolStripMenuItem.Enabled = false;
					textBoxCustomerName.Width = textBoxCustomerName.Width - buttonSelectDocument.Width - 3;
					checkedListBoxDN.Visible = false;
					labelSelectedDocs.Visible = false;
					if (!CompanyPreferences.AllowLSaleAddNew && !CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					}
					break;
				case SalesFlows.DirectInvoice:
					buttonSelectDocument.Visible = false;
					checkedListBoxDN.Visible = false;
					labelSelectedDocs.Visible = false;
					break;
				}
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

		private void CalculateTotalExpense()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridExpense.Rows)
			{
				decimal result = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
					if (Convert.ToBoolean(row.Cells["Deductable"].Value.ToString()))
					{
						num += result;
					}
				}
			}
			textBoxTotalExpense.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No)
			{
				if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
				}
				else if (Void(!isVoid))
				{
					IsVoid = true;
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to void the transaction.");
				}
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				return Factory.SalesInvoiceSystem.VoidSalesInvoice(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalesInvoice);
		}

		private void LoadCustomerDocAddress()
		{
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string nextID = DatabaseHelper.GetNextID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (nextID != "")
			{
				currentBillingAddressID = nextID;
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
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentBillingAddressID = previousID;
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
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result4);
					num += result4;
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						result3 += decimal.Parse(row.Cells["Tax"].Value.ToString());
					}
					decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result5);
					row.Cells["TaxTotal"].Value = result4 + result5;
				}
			}
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscountPercent.Text, out result2);
			decimal.TryParse(textBoxDiscountAmount.Text, out result);
			if (isDiscountPercent && result2 != 0m)
			{
				result = Math.Round(num * result2 / 100m, Global.CurDecimalPoints);
				textBoxDiscountAmount.Text = result.ToString(Format.TotalAmountFormat);
			}
			else if (num > 0m)
			{
				result2 = Math.Round(result / num * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = result2.ToString();
				isDiscountPercent = false;
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num - result;
			CalculateTotalExpense();
			foreach (UltraGridRow row2 in dataGridExpense.Rows)
			{
				if (!row2.Cells["Tax"].Value.IsNullOrEmpty())
				{
					result3 += decimal.Parse(row2.Cells["Tax"].Value.ToString());
				}
			}
			textBoxTaxAmount.Text = result3.ToString(Format.TotalAmountFormat);
			decimal result6 = default(decimal);
			decimal.TryParse(textBoxTotalExpense.Text, out result6);
			decimal.TryParse(textBoxTaxAmount.Text, out result3);
			num3 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num2 + result6 + num3;
			if (!checkBoxPriceIncludeTax.Checked)
			{
				num2 += result3;
			}
			textBoxTotal.Text = num2.ToString(Format.TotalAmountFormat);
			CalculateTotalTaxes();
		}

		private void CalculateAllRowsTaxes()
		{
			try
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.BasedOnCustomer;
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOptions = (ItemTaxOptions)byte.Parse(row.Cells["TaxOption"].Value.ToString());
					}
					if (itemTaxOptions == ItemTaxOptions.BasedOnCustomer)
					{
						row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
					}
					decimal amount = decimal.Parse(row.Cells["Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result2);
					decimal.TryParse(row.Cells["Cost"].Value.ToString(), out result);
					result = result2 * result;
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = tag;
					UIGlobal.CalculateRowTax(row, "Tax", result, amount, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
				}
				foreach (UltraGridRow row2 in dataGridExpense.Rows)
				{
					if (!Convert.ToBoolean(row2.Cells["Deductable"].Value.ToString()))
					{
						row2.Cells["Tax"].Value = 0;
					}
					else
					{
						ItemTaxOptions itemTaxOptions2 = ItemTaxOptions.NonTaxable;
						if (!row2.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOptions2 = (ItemTaxOptions)byte.Parse(row2.Cells["TaxOption"].Value.ToString());
						}
						if (itemTaxOptions2 == ItemTaxOptions.BasedOnCustomer)
						{
							row2.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
						}
						decimal amount2 = decimal.Parse(row2.Cells["Amount"].Value.ToString());
						decimal subtotal2 = decimal.Parse(textBoxTotalExpense.Text);
						decimal tradeDiscount2 = default(decimal);
						TaxTransactionData tag2 = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions2, row2.Cells["TaxGroupID"].Value.ToString());
						row2.Cells["Tax"].Tag = tag2;
						UIGlobal.CalculateRowTax(row2, "Tax", amount2, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalTaxes()
		{
			TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID);
			DataTable taxDetailTable = taxTransactionData.TaxDetailTable;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Tax"].Tag != null)
				{
					foreach (DataRow row2 in (row.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text = row2["TaxItemID"].ToString();
						decimal result = default(decimal);
						decimal.TryParse(row2["TaxAmount"].ToString(), out result);
						DataRow[] array = taxDetailTable.Select("TaxItemID  = '" + text + "'");
						if (array.Count() > 0)
						{
							decimal result2 = default(decimal);
							decimal.TryParse(array[0]["TaxAmount"].ToString(), out result2);
							result2 += result;
							array[0]["TaxAmount"] = result2;
						}
						else
						{
							DataRow dataRow = taxDetailTable.NewRow();
							dataRow["TaxItemID"] = text;
							dataRow["TaxAmount"] = result;
							taxDetailTable.Rows.Add(dataRow);
						}
					}
				}
			}
			foreach (UltraGridRow row3 in dataGridExpense.Rows)
			{
				if (row3.Cells["Tax"].Tag != null && Convert.ToBoolean(row3.Cells["Deductable"].Value.ToString()))
				{
					foreach (DataRow row4 in (row3.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text2 = row4["TaxItemID"].ToString();
						decimal result3 = default(decimal);
						decimal.TryParse(row4["TaxAmount"].ToString(), out result3);
						DataRow[] array2 = taxDetailTable.Select("TaxItemID  = '" + text2 + "'");
						if (array2.Count() > 0)
						{
							decimal result4 = default(decimal);
							decimal.TryParse(array2[0]["TaxAmount"].ToString(), out result4);
							result4 += result3;
							array2[0]["TaxAmount"] = result4;
						}
						else
						{
							DataRow dataRow2 = taxDetailTable.NewRow();
							dataRow2["TaxItemID"] = text2;
							dataRow2["TaxAmount"] = result3;
							taxDetailTable.Rows.Add(dataRow2);
						}
					}
				}
			}
			textBoxTaxAmount.Tag = taxTransactionData;
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Focused)
			{
				isDiscountPercent = true;
			}
			decimal result = default(decimal);
			decimal.TryParse(textBoxDiscountPercent.Text, out result);
			if (result > 0m && !isDiscountPercent)
			{
				isDiscountPercent = true;
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
			}
		}

		private void textBoxTotal_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTotal.Focused)
			{
				totalChanged = true;
			}
		}

		private void textBoxTotal_Validating(object sender, CancelEventArgs e)
		{
			if (!totalChanged)
			{
				return;
			}
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num6 = decimal.Parse(textBoxTotalExpense.Text, NumberStyles.Any);
			num7 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num + num5 + num6 - num4 + num7;
			if (num2 < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				e.Cancel = true;
				return;
			}
			_ = (num4 < 0m);
			textBoxDiscountAmount.Text = num2.ToString(Format.TotalAmountFormat);
			if (num > 0m)
			{
				num3 = Math.Round(num2 / (num + num5) * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = num3.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
			}
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
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxBillingAddress.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridCell cell in row.Cells)
					{
						if (cell.Activation == Activation.Disabled)
						{
							cell.Activation = Activation.AllowEdit;
						}
					}
					row.Cells["IsDNRow"].Value = DBNull.Value;
					string text2 = row.Cells["SOSysDocID"].Value.ToString();
					string text3 = row.Cells["SOVoucherID"].Value.ToString();
					refDocID = text2;
					refVoucherID = text3;
					timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
					if (timeStampStatus != null && timeStampStatus.Tables.Count > 0 && timeStampStatus.Tables[0].Rows.Count > 0)
					{
						refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
					}
				}
				deliveryNoteTable = null;
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
		}

		private void createFromSalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesOrderSystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Order";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			refDocID = text;
			refVoucherID = text2;
			timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
			refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesOrderData salesOrderByID = Factory.SalesOrderSystem.GetSalesOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesOrderByID, SysDocTypes.SalesOrder, Global.CurrentUser, includeApproveUser: true);
			if (entityApprovalStatus.Tables[0].Rows.Count > 0)
			{
				bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
				int num = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
				if (restrictTransaction && num != 10)
				{
					restrictTransaction = true;
				}
				else
				{
					restrictTransaction = false;
				}
				if (restrictTransaction)
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				}
			}
			DataRow dataRow = salesOrderByID.SalesOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.SalesOrder;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Shipped", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("SOSysDocID"))
				{
					dataTable.Columns.Add("SOSysDocID");
					dataTable.Columns.Add("SOVoucherID");
					dataTable.Columns.Add("SORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!salesOrderByID.Tables.Contains("Sales_Order_Detail") || salesOrderByID.SalesOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in salesOrderByID.Tables["Sales_Order_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["SOSysDocID"] = text;
				dataRow3["SOVoucherID"] = text2;
				dataRow3["SORowIndex"] = row["RowIndex"];
				if (row["JobID"] != DBNull.Value)
				{
					dataRow3["Job"] = row["JobID"];
				}
				else
				{
					dataRow3["Job"] = row["JobID"];
				}
				if (row["CostCategoryID"] != DBNull.Value)
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
				else
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["ItemType"] = row["ItemType"];
				dataRow3["RowSourceType"] = ItemSourceTypes.SalesOrder;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				if (!string.IsNullOrEmpty(row["RefSlNo"].ToString()))
				{
					dataRow3["RefSlNo"] = row["RefSlNo"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefText1"].ToString()))
				{
					dataRow3["RefText1"] = row["RefText1"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefText2"].ToString()))
				{
					dataRow3["RefText2"] = row["RefText2"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefNum1"].ToString()))
				{
					dataRow3["RefNum1"] = row["RefNum1"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefNum2"].ToString()))
				{
					dataRow3["RefNum2"] = row["RefNum2"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefDate1"].ToString()))
				{
					dataRow3["RefDate1"] = row["RefDate1"].ToString();
				}
				if (!string.IsNullOrEmpty(row["RefDate2"].ToString()))
				{
					dataRow3["RefDate2"] = row["RefDate2"].ToString();
				}
				dataRow3["Description"] = row["Description"];
				dataRow3["Remarks"] = row["Remarks"];
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				dataRow3["Location"] = Security.DefaultInventoryLocationID;
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				if (row["Cost"] != DBNull.Value)
				{
					dataRow3["Cost"] = row["Cost"];
				}
				else
				{
					dataRow3["Cost"] = DBNull.Value;
				}
				decimal num2 = default(decimal);
				decimal d = default(decimal);
				decimal num3 = default(decimal);
				if (dataRow3["Quantity"].ToString() != "")
				{
					num2 = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
				}
				if (dataRow3["Price"].ToString() != "")
				{
					d = decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
				}
				if (row["QuantityShipped"].ToString() != "")
				{
					num3 = decimal.Parse(row["QuantityShipped"].ToString(), NumberStyles.Any);
				}
				if (row["TaxOption"] != DBNull.Value)
				{
					dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
				}
				else
				{
					dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
				}
				if (row["TaxAmount"] != DBNull.Value)
				{
					dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
				}
				if (!row["TaxGroupID"].IsDBNullOrEmpty())
				{
					dataRow3["TaxGroupID"] = row["TaxGroupID"];
				}
				dataRow3["Ordered"] = num2;
				dataRow3["Shipped"] = num3;
				num2 -= num3;
				dataRow3["Quantity"] = num2;
				dataRow3["Amount"] = Math.Round(num2 * d, Global.CurDecimalPoints);
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Discount)
				{
					row2.Cells["Quantity"].Activation = Activation.Disabled;
				}
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				if (!CompanyPreferences.AllowLSaleAddNew)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void createFromDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsNewRecord)
				{
					ErrorHelper.InformationMessage("Please start a new transaction first.");
				}
				else if (CompanyPreferences.LocalSalesFlow != SalesFlows.SOThenDNThenInvoice)
				{
					ErrorHelper.InformationMessage("This feature is not available in current Sales Flow.");
				}
				else
				{
					DataSet dataSet = new DataSet();
					decimal result = default(decimal);
					List<string> list = new List<string>();
					if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.DefaultLocationSales) && !Global.isUserAdmin)
					{
						string defaultLocationID = Global.DefaultLocationID;
						dataSet = Factory.DeliveryNoteSystem.GetUninvoicedDeliveryNotesOnLocation(comboBoxSysDoc.SelectedID, comboBoxCustomer.SelectedID, isExport: false, defaultLocationID);
					}
					else
					{
						dataSet = Factory.DeliveryNoteSystem.GetUninvoicedDeliveryNotes(comboBoxSysDoc.SelectedID, comboBoxCustomer.SelectedID, isExport: false);
					}
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["RowSourceType"].Value != null && row.Cells["RowSourceType"].Value.ToString() != "")
						{
							string item = row.Cells["SOSysDocID"].Value.ToString() + row.Cells["SOVoucherID"].Value.ToString();
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.DataSource = dataSet;
					selectDocumentDialog.IsMultiSelect = true;
					selectDocumentDialog.SelectedDocuments = list;
					selectDocumentDialog.Text = "Select Delivery Notes";
					selectDocumentDialog.ValidateSelection += form_ValidateSelection;
					selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						list = selectDocumentDialog.SelectedDocuments;
						checked
						{
							for (int i = 0; i < checkedListBoxDN.Items.Count; i++)
							{
								NameValue nameValue = checkedListBoxDN.Items[i] as NameValue;
								string text = nameValue.ID + nameValue.Name;
								if (!list.Contains(text))
								{
									for (int j = 0; j < dataGridItems.Rows.Count; j++)
									{
										UltraGridRow ultraGridRow = dataGridItems.Rows[j];
										if (ultraGridRow.Cells["SOSysDocID"].Value != null && ultraGridRow.Cells["SOSysDocID"].Value.ToString() + ultraGridRow.Cells["SOVoucherID"].Value.ToString() == text)
										{
											ultraGridRow.Delete(displayPrompt: false);
											j--;
										}
									}
									checkedListBoxDN.Items.RemoveAt(i);
									i--;
								}
							}
							textBoxNote.Text = "Delivery Notes:\r\n";
							deliveryNoteTable = new DataTable("DeliveryNotes");
							deliveryNoteTable.Columns.Add("SysDocID");
							deliveryNoteTable.Columns.Add("VoucherID");
							sourceDocType = ItemSourceTypes.DeliveryNote;
						}
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							string text2 = selectedRow.Cells["Doc ID"].Value.ToString();
							string text3 = selectedRow.Cells["Number"].Value.ToString();
							bool flag = false;
							foreach (NameValue item3 in checkedListBoxDN.Items)
							{
								if (item3.ID + item3.Name == text2 + text3)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								NameValue item2 = new NameValue(text3, text2);
								checkedListBoxDN.Items.Add(item2);
								deliveryNoteTable.Rows.Add(text2, text3);
								refDocID = text2;
								refVoucherID = text3;
								timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Delivery_Note", refDocID, refVoucherID);
								refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
								DeliveryNoteData deliveryNoteByID = Factory.DeliveryNoteSystem.GetDeliveryNoteByID(text2, text3);
								DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(deliveryNoteByID, SysDocTypes.DeliveryNote, Global.CurrentUser, includeApproveUser: true);
								if (entityApprovalStatus.Tables[0].Rows.Count > 0)
								{
									bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
									int num = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
									if (restrictTransaction && num != 10)
									{
										restrictTransaction = true;
									}
									else
									{
										restrictTransaction = false;
									}
									if (restrictTransaction)
									{
										ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
									}
								}
								DataRow dataRow = deliveryNoteByID.DeliveryNoteTable.Rows[0];
								decimal.TryParse(dataRow["DiscountPercent"].ToString(), out result);
								SalesFlows salesFlows = SalesFlows.DirectInvoice;
								if (dataRow["SalesFlow"] != DBNull.Value && dataRow["SalesFlow"].ToString() != "")
								{
									salesFlows = (SalesFlows)int.Parse(dataRow["SalesFlow"].ToString());
								}
								if (salesFlows != SalesFlows.SOThenDNThenInvoice)
								{
									ErrorHelper.InformationMessage("One or more selected delivery notes are made in a different sales flow and cannot be used in this transaction.\nPlease select another delivery note.");
									ClearForm();
									return;
								}
								textBoxRef1.Text = dataRow["VoucherID"].ToString();
								textBoxRef2.Text = dataRow["Reference2"].ToString();
								textBoxPONumber.Text = dataRow["PONumber"].ToString();
								textBoxNote.Text = dataRow["Note"].ToString();
								if (comboBoxCustomer.SelectedID == "")
								{
									comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
								}
								comboBoxCustomer.Enabled = false;
								comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
								textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
								if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
								{
									comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
								{
									comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
								{
									comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
								{
									comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["PaymentTermID"].ToString()))
								{
									comboBoxTerm.SelectedID = dataRow["PaymentTermID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
								{
									comboBoxJob.SelectedID = dataRow["JobID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
								{
									comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
								}
								textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
								DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
								dNDateList.Add(DateTime.Parse(dateTime.ToShortDateString()));
								if (deliveryNoteByID.Tables.Contains("Sales_Order") && deliveryNoteByID.Tables["Sales_Order"].Rows.Count > 0)
								{
									DataRow dataRow2 = deliveryNoteByID.Tables["Sales_Order"].Rows[0];
									if (!string.IsNullOrEmpty(dataRow2["TermID"].ToString()))
									{
										comboBoxTerm.SelectedID = dataRow2["TermID"].ToString();
									}
									textBoxPONumber.Text = dataRow2["PONumber"].ToString();
								}
								textBoxPONumber.Text = dataRow["PONumber"].ToString();
								DataTable dataTable = dataGridItems.DataSource as DataTable;
								if (!dataTable.Columns.Contains("ItemType"))
								{
									dataTable.Columns.Add("ItemType", typeof(byte));
								}
								if (!dataTable.Columns.Contains("SOSysDocID"))
								{
									dataTable.Columns.Add("SOSysDocID");
									dataTable.Columns.Add("SOVoucherID");
									dataTable.Columns.Add("SORowIndex", typeof(int));
								}
								if (!deliveryNoteByID.Tables.Contains("Delivery_Note_Detail") || deliveryNoteByID.DeliveryNoteDetailTable.Rows.Count == 0)
								{
									return;
								}
								foreach (DataRow row2 in deliveryNoteByID.Tables["Delivery_Note_Detail"].Rows)
								{
									decimal result2 = default(decimal);
									decimal result3 = default(decimal);
									decimal result4 = default(decimal);
									decimal result5 = default(decimal);
									DataRow dataRow4 = dataTable.NewRow();
									dataRow4["Item Code"] = row2["ProductID"];
									dataRow4["SOSysDocID"] = text2;
									dataRow4["SOVoucherID"] = text3;
									dataRow4["SORowIndex"] = row2["RowIndex"];
									dataRow4["IsDNRow"] = true;
									dataRow4["RowSourceType"] = ItemSourceTypes.DeliveryNote;
									if (row2["UnitQuantity"] != DBNull.Value)
									{
										decimal.TryParse(row2["UnitQuantity"].ToString(), out result2);
									}
									else
									{
										decimal.TryParse(row2["Quantity"].ToString(), out result2);
									}
									decimal.TryParse(row2["QuantityReturned"].ToString(), out result3);
									dataRow4["ConsignmentNo"] = row2["Consign#"].ToString();
									if (!string.IsNullOrEmpty(row2["FactorType"].ToString()))
									{
										string a = row2["FactorType"].ToString();
										decimal num2 = decimal.Parse(row2["UnitFactor"].ToString());
										if (a == "D")
										{
											result3 /= num2;
										}
										else
										{
											result3 *= num2;
										}
									}
									dataRow4["Quantity"] = result2 - result3;
									dataRow4["Location"] = row2["LocationID"];
									dataRow4["Unit"] = row2["UnitID"];
									dataRow4["Brand"] = row2["Brand"];
									dataRow4["TaxGroupID"] = row2["TaxGroupID"];
									if (row2["TaxOption"] != DBNull.Value)
									{
										dataRow4["TaxOption"] = byte.Parse(row2["TaxOption"].ToString());
									}
									else
									{
										dataRow4["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
									}
									if (!string.IsNullOrEmpty(row2["JobID"].ToString()))
									{
										dataRow4["Job"] = row2["JobID"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["CostCategoryID"].ToString()))
									{
										dataRow4["CostCategory"] = row2["CostCategoryID"].ToString();
									}
									string text4 = row2["UnitID"].ToString();
									string text5 = row2["ProductID"].ToString();
									decimal result6 = default(decimal);
									decimal.TryParse(row2["UnitPrice"].ToString(), out result6);
									string text6 = "ProductID='" + text5 + "' ";
									if (text4 != "")
									{
										text6 = text6 + " AND UnitID='" + text4 + "'";
									}
									DataRow[] array = priceListData.Tables[0].Select(text6);
									if (array.Length != 0)
									{
										if (!loadItemDescFromPriceList)
										{
											object obj2 = dataRow4["Description"] = (dataRow4["DefaultDescription"] = row2["Description"].ToString());
										}
										else
										{
											dataRow4["Description"] = array[0]["Description"].ToString();
										}
										dataRow4["Price"] = decimal.Parse(array[0]["UnitPrice"].ToString());
									}
									else
									{
										if (setlastSalesprice)
										{
											decimal lastSaleTransationByCustomerID = Factory.ProductSystem.GetLastSaleTransationByCustomerID(text5, comboBoxCustomer.SelectedID);
											dataRow4["Price"] = lastSaleTransationByCustomerID;
										}
										else if (useDNotePrice)
										{
											dataRow4["Price"] = row2["UnitPrice"].ToString();
										}
										if (result6.ToString() != "0.00000" && setlastSalesprice)
										{
											dataRow4["Price"] = row2["UnitPrice"].ToString();
										}
										else if (!setlastSalesprice && !useDNotePrice)
										{
											decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(text5, comboBoxCustomer.SelectedID);
											dataRow4["Price"] = productSalesPrice;
										}
										object obj2 = dataRow4["Description"] = (dataRow4["DefaultDescription"] = row2["Description"].ToString());
									}
									dataRow4["SpecificationID"] = row2["SpecificationID"].ToString();
									dataRow4["Style"] = row2["StyleID"].ToString();
									dataRow4["Remarks"] = row2["Remarks"].ToString();
									if (!string.IsNullOrEmpty(row2["RefSlNo"].ToString()))
									{
										dataRow4["RefSlNo"] = row2["RefSlNo"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefText1"].ToString()))
									{
										dataRow4["RefText1"] = row2["RefText1"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefText2"].ToString()))
									{
										dataRow4["RefText2"] = row2["RefText2"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefNum1"].ToString()))
									{
										dataRow4["RefNum1"] = row2["RefNum1"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefNum2"].ToString()))
									{
										dataRow4["RefNum2"] = row2["RefNum2"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefDate1"].ToString()))
									{
										dataRow4["RefDate1"] = row2["RefDate1"].ToString();
									}
									if (!string.IsNullOrEmpty(row2["RefDate2"].ToString()))
									{
										dataRow4["RefDate2"] = row2["RefDate2"].ToString();
									}
									decimal.TryParse(dataRow4["Quantity"].ToString(), out result4);
									decimal.TryParse(dataRow4["Price"].ToString(), out result5);
									if (result5 == 0m)
									{
										dataRow4["Price"] = row2["UnitPrice"];
									}
									decimal.TryParse(dataRow4["Price"].ToString(), out result5);
									if (!(result4 == 0m))
									{
										if (result4 < 0m)
										{
											result4 = default(decimal);
										}
										dataRow4["Amount"] = Math.Round(result4 * result5, Global.CurDecimalPoints);
										dataRow4.EndEdit();
										dataTable.Rows.Add(dataRow4);
									}
								}
								if (deliveryNoteByID.Tables.Contains("Sales_Order") && deliveryNoteByID.Tables["Sales_Order"].Rows.Count > 0)
								{
									textBoxDiscountAmount.Text = decimal.Parse(deliveryNoteByID.Tables["Sales_Order"].Rows[0]["Discount"].ToString()).ToString(Format.TotalAmountFormat);
								}
								AdjustGridColumnSettings();
								CalculateTotal();
								if (salesFlows == SalesFlows.SOThenDNThenInvoice)
								{
									foreach (UltraGridRow row3 in dataGridItems.Rows)
									{
										if (!allowPriceChange)
										{
											row3.Cells["Price"].Activation = Activation.Disabled;
										}
									}
								}
								foreach (UltraGridRow row4 in dataGridItems.Rows)
								{
									row4.Cells["Item Code"].Activation = Activation.Disabled;
									row4.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row4.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
									row4.Cells["Description"].Activation = Activation.Disabled;
									row4.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row4.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
									row4.Cells["Location"].Activation = Activation.Disabled;
									row4.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row4.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
									row4.Cells["Unit"].Activation = Activation.Disabled;
									row4.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row4.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
									row4.Cells["Quantity"].Activation = Activation.Disabled;
									row4.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row4.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
								}
							}
						}
						DateTime t = dNDateList.Max((DateTime p) => p);
						DateTime t2 = DateTime.Parse(dateTimePickerDate.Value.ToShortDateString());
						if (t > t2)
						{
							ErrorHelper.WarningMessage("One of the DN date is greater than sales invoice date.");
						}
						textBoxDiscountPercent.Text = result.ToString();
						CalculateAllRowsTaxes();
						CalculateTotal();
						ValidationwithColor();
					}
				}
			}
			catch (Exception e2)
			{
				ClearForm();
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			string text = "";
			foreach (UltraGridRow row in selectDocumentDialog.Grid.Rows)
			{
				if (row.Cells["C"].Value != null && row.Cells["C"].Value.ToString() != "" && bool.Parse(row.Cells["C"].Value.ToString()))
				{
					string text2 = row.Cells["Customer"].Value.ToString();
					if (text != "" && text2 != text)
					{
						ErrorHelper.WarningMessage("Cannot select documents from different customers.");
						selectDocumentDialog.CanClose = false;
						break;
					}
					text = text2;
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
					DataSet salesInvoiceToPrint = Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(selectedID, text, mergeMatrixPrint, showLotDetail);
					if (salesInvoiceToPrint == null || salesInvoiceToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(salesInvoiceToPrint, selectedID, printTemplateName, SysDocTypes.SalesInvoice, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(salesInvoiceToPrint, selectedID, "Sales Invoice", SysDocTypes.SalesInvoice, isPrint, showPrintDialog);
						}
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

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			switch (CompanyPreferences.LocalSalesFlow)
			{
			case SalesFlows.SOThenDNThenInvoice:
				createFromDeliveryNoteToolStripMenuItem_Click(sender, e);
				break;
			case SalesFlows.SOThenInvoiceThenDN:
				createFromSalesOrderToolStripMenuItem_Click(sender, e);
				break;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.SalesInvoiceListFormObj);
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.SalesInvoice);
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 25.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.SalesInvoice);
					currentData = (dataSet as SalesInvoiceData);
					FillData();
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void textBoxTotalExpense_TextChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonBalance_Click(object sender, EventArgs e)
		{
			if (!(comboBoxCustomer.SelectedID == ""))
			{
				CustomerBalanceSnapDialog customerBalanceSnapDialog = new CustomerBalanceSnapDialog();
				customerBalanceSnapDialog.LoadData(comboBoxCustomer.SelectedID);
				customerBalanceSnapDialog.ShowDialog(this);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void checkedListBoxDN_DoubleClick(object sender, EventArgs e)
		{
			NameValue nameValue = checkedListBoxDN.SelectedItem as NameValue;
			if (nameValue != null)
			{
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void DisplayItemDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail(comboBoxCustomer.SelectedID, SelectedID);
			DataTable dataTable = new DataSet().Tables.Add("Table");
			dataTable.Columns.Add("Date", typeof(string));
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Date"] = row["Date"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
			}
			toolTipController1.HideHint();
			string text = "";
			text = ToCSV(dataTable);
			toolTipController1.ShowHint("Last Sale Details", text, ToolTipLocation.BottomRight);
			toolTipController1.InitialDelay = 15000;
		}

		protected string ExportDatatableToHtml(DataTable dt)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
			stringValue = "LightBlue";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<html >");
			stringBuilder.Append("<head>");
			stringBuilder.Append("</head>");
			stringBuilder.Append("<body>");
			stringBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='" + stringValue + "' style='font-family:Garamond; font-size:smaller'>");
			stringBuilder.Append("<tr >");
			foreach (DataColumn column in dt.Columns)
			{
				stringBuilder.Append("<td >");
				stringBuilder.Append(column.ColumnName);
				stringBuilder.Append("</td>");
			}
			stringBuilder.Append("</tr>");
			foreach (DataRow row in dt.Rows)
			{
				stringBuilder.Append("<tr >");
				foreach (DataColumn column2 in dt.Columns)
				{
					stringBuilder.Append("<td >");
					stringBuilder.Append(row[column2.ColumnName].ToString());
					stringBuilder.Append("</td>");
				}
				stringBuilder.Append("</tr>");
			}
			stringBuilder.Append("</table>");
			stringBuilder.Append("</body>");
			stringBuilder.Append("</html>");
			return stringBuilder.ToString();
		}

		public string ToCSV(DataTable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			checked
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					stringBuilder.Append("\t" + table.Columns[i].ColumnName);
					stringBuilder.Append((i == table.Columns.Count - 1) ? "\n" : "\t\t\t");
				}
				foreach (DataRow row in table.Rows)
				{
					for (int j = 0; j < table.Columns.Count; j++)
					{
						stringBuilder.Append(row[j].ToString());
						stringBuilder.Append((j == table.Columns.Count - 1) ? "" : "\t\t");
					}
					stringBuilder.Append("\n");
				}
				return stringBuilder.ToString();
			}
		}

		public static Table DataTableToHTMLTable(DataTable dt, bool includeHeaders)
		{
			Table table = new Table();
			TableRow tableRow = null;
			TableCell tableCell = null;
			int count = dt.Rows.Count;
			int count2 = dt.Columns.Count;
			checked
			{
				if (includeHeaders)
				{
					TableHeaderRow tableHeaderRow = new TableHeaderRow();
					TableHeaderCell tableHeaderCell = null;
					for (int i = 0; i < count2; i++)
					{
						tableHeaderCell = new TableHeaderCell();
						tableHeaderCell.Text = dt.Columns[i].ColumnName.ToString();
						tableHeaderRow.Cells.Add(tableHeaderCell);
					}
					table.Rows.Add(tableHeaderRow);
				}
				for (int j = 0; j < count; j++)
				{
					tableRow = new TableRow();
					for (int k = 0; k < count2; k++)
					{
						tableCell = new TableCell();
						tableCell.Text = dt.Rows[j][k].ToString();
						tableRow.Cells.Add(tableCell);
					}
					table.Rows.Add(tableRow);
				}
				return table;
			}
		}

		private void toolStripSeparator7_Click(object sender, EventArgs e)
		{
		}

		private void buttonPrevShipto_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentBillingAddressID = previousID;
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

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.SalesInvoice;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void ultraFormattedLinkShipping_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void ultraFormattedLinkProject_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkCostCategory_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
		}

		private void ultraFormattedLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void pictureBoxDeliveryInr_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInfo_Click(object sender, EventArgs e)
		{
		}

		private void toolStrip1_MouseEnter(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInfo_MouseEnter(object sender, EventArgs e)
		{
			toolTip.OwnerDraw = true;
			toolTip.Draw += tooltip_Draw;
			toolTip.Popup += tooltip_Popup;
			toolTip.InitialDelay = 500;
			toolTip.IsBalloon = true;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
			if (fieldValue.ToString() != "" || fieldValue.ToString() != string.Empty)
			{
				toolStrip1.PointToClient(Cursor.Position);
				toolStripButtonInfo.Visible = true;
				toolTip.Show(fieldValue.ToString(), toolStrip1);
				toolTip.ShowAlways = true;
			}
			else
			{
				toolTip.Show("", toolStrip1.Parent);
				toolStripButtonInfo.Visible = false;
			}
		}

		private void toolStripButtonInfo_MouseLeave(object sender, EventArgs e)
		{
			toolTip.Show("", toolStrip1);
		}

		private void ultraFormattedLinkPaymentTerm_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxTerm.SelectedID);
		}

		private void tooltip_Draw(object sender, DrawToolTipEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			bounds.Offset(200, 400);
			toolTip.BackColor = Color.Yellow;
			DrawToolTipEventArgs drawToolTipEventArgs = new DrawToolTipEventArgs(e.Graphics, e.AssociatedWindow, e.AssociatedControl, bounds, e.ToolTipText, Color.Yellow, Color.Yellow, new Font("Courier New", 10f, FontStyle.Bold));
			drawToolTipEventArgs.DrawBackground();
			drawToolTipEventArgs.DrawBorder();
			drawToolTipEventArgs.DrawText(TextFormatFlags.TextBoxControl);
		}

		private void createFromProformaInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesProformaSystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Proforma Invoice";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			refDocID = text;
			refVoucherID = text2;
			NameValue item = new NameValue(text2, text);
			checkedListBoxDN.Items.Add(item);
			timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("SalesProforma_Invoice", refDocID, refVoucherID);
			refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesProformaInvoiceData salesOrderByID = Factory.SalesProformaSystem.GetSalesOrderByID(text, text2);
			DataRow dataRow = salesOrderByID.SalesProformaTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.SalesProformaInvoice;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Shipped", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("SOSysDocID"))
				{
					dataTable.Columns.Add("SOSysDocID");
					dataTable.Columns.Add("SOVoucherID");
					dataTable.Columns.Add("SORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!salesOrderByID.Tables.Contains("SalesProforma_Invoice_Detail") || salesOrderByID.SalesProformaDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in salesOrderByID.Tables["SalesProforma_Invoice_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["SOSysDocID"] = text;
				dataRow3["SOVoucherID"] = text2;
				dataRow3["SORowIndex"] = row["RowIndex"];
				if (row["JobID"] != DBNull.Value)
				{
					dataRow3["Job"] = row["JobID"];
				}
				else
				{
					dataRow3["Job"] = row["JobID"];
				}
				if (row["CostCategoryID"] != DBNull.Value)
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
				else
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["ItemType"] = row["ItemType"];
				dataRow3["RowSourceType"] = ItemSourceTypes.SalesProformaInvoice;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Location"] = Security.DefaultInventoryLocationID;
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				decimal num = default(decimal);
				decimal d = default(decimal);
				decimal num2 = default(decimal);
				if (dataRow3["Quantity"].ToString() != "")
				{
					num = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
				}
				if (dataRow3["Price"].ToString() != "")
				{
					d = decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
				}
				if (row["QuantityShipped"].ToString() != "")
				{
					num2 = decimal.Parse(row["QuantityShipped"].ToString(), NumberStyles.Any);
				}
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				dataRow3["Ordered"] = num;
				dataRow3["Shipped"] = num2;
				num -= num2;
				dataRow3["Quantity"] = num;
				dataRow3["Amount"] = Math.Round(num * d, Global.CurDecimalPoints);
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Discount)
				{
					row2.Cells["Quantity"].Activation = Activation.Disabled;
				}
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				if (!CompanyPreferences.AllowLSaleAddNew)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			CalculateTotal();
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
		}

		private void textBoxRoundOff_Leave(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Leave(object sender, EventArgs e)
		{
			showedNotDiscount = false;
			ValidateAllowedDiscount();
		}

		private void textBoxDiscountPercent_Leave(object sender, EventArgs e)
		{
			showedNotDiscount = false;
			ValidateAllowedDiscount();
		}

		private void grantEditPermissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedCodes = new List<string>();
			dataSet = Factory.UserSystem.GetUserComboList();
			bool flag = false;
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedCodes = selectedCodes;
			selectDocumentDialog.Text = "Select User";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedCodes = selectDocumentDialog.SelectedCodes;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Code"].Value.ToString();
					string name = selectedRow.Cells["Name"].Value.ToString();
					if (0 == 0)
					{
						new NameValue(name, text);
						flag = Factory.SalesInvoiceSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, text, isModify: true, "");
					}
				}
				if (flag)
				{
					ErrorHelper.WarningMessage("User can able to modify");
				}
			}
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
			}
		}

		private void linkLabelTax_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				TaxTransactionData taxData = new TaxTransactionData();
				if (textBoxTaxAmount.Tag != null)
				{
					taxData = (textBoxTaxAmount.Tag as TaxTransactionData);
				}
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxSalesperson_SelectedIndexChanged(object sender, EventArgs e)
		{
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Salesperson", "ReportTo", "SalespersonID", comboBoxSalesperson.SelectedID);
			if (fieldValue != null)
			{
				labelReportTo.Text = fieldValue.ToString();
			}
		}

		private void toolStripButtonPaymentAllocation_Click(object sender, EventArgs e)
		{
			DataSet paymentAllocationDetails = Factory.SalesInvoiceSystem.GetPaymentAllocationDetails(SystemDocID, textBoxVoucherNumber.Text);
			decimal num = default(decimal);
			decimal result = default(decimal);
			if (paymentAllocationDetails != null && paymentAllocationDetails.Tables.Count > 0 && paymentAllocationDetails.Tables[0].Rows.Count > 0)
			{
				PaymentAllocationDialog paymentAllocationDialog = new PaymentAllocationDialog();
				paymentAllocationDialog.EntityType = EntityTypesEnum.Customers;
				paymentAllocationDialog.DataSource = paymentAllocationDetails;
				foreach (DataRow row in paymentAllocationDetails.Tables[0].Rows)
				{
					num += decimal.Parse(row["PaymentAmount"].ToString());
					decimal.TryParse(row["Total"].ToString(), out result);
				}
				decimal balance = result - num;
				paymentAllocationDialog.InvoiceAmount = result;
				paymentAllocationDialog.Paid = num;
				paymentAllocationDialog.Balance = balance;
				if (paymentAllocationDialog.ShowDialog(this) == DialogResult.OK)
				{
					paymentAllocationDialog.Close();
				}
			}
			else
			{
				ErrorHelper.InformationMessage("No allocation for this invoice");
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			bool result = false;
			bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString(), out result);
			if (result)
			{
				DataSet dataSet = dataGridItems.ImportFromExcel(autoFill: true);
				string text = "";
				string text2 = "";
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Item Code"].Value.ToString() != "")
					{
						text = row.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text))
						{
							string value = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text).ToString() ?? null;
							row.Cells["Description"].Value = value;
							string value2 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text).ToString() ?? null;
							row.Cells["Unit"].Value = value2;
							text2 = row.Cells["LotNumber"].Value.ToString();
							num = decimal.Parse(row.Cells["Quantity"].Value.ToString());
							decimal d = decimal.Parse(row.Cells["Price"].Value.ToString());
							row.Cells["Amount"].Value = (num * d).ToString(Format.TotalAmountFormat);
							if (row.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
							ItemTaxOptions itemTaxOptions = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text).ToString() ?? null);
							row.Cells["TaxOption"].Value = itemTaxOptions;
							switch (itemTaxOptions)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
						}
					}
					DataRow[] array = dataSet.Tables[0].Select("[Item Code]='" + text + "'    AND LotNumber='" + text2 + "'");
					DataTable dataTable = new DataTable();
					dataTable = dataSet.Tables[0].Clone();
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Item Code"] = array[i]["Item Code"];
						dataRow["LotNumber"] = array[i]["LotNumber"];
						dataRow["BinID"] = array[i]["BinID"];
						if (!dataTable.Columns.Contains("ProductID"))
						{
							dataTable.Columns.Add("ProductID");
						}
						dataRow["ProductID"] = array[i]["Item Code"];
						if (!dataTable.Columns.Contains("LocationID"))
						{
							dataTable.Columns.Add("LocationID");
							dataRow["LocationID"] = array[i]["Location"];
						}
						if (!dataTable.Columns.Contains("SourceLotNumber"))
						{
							dataTable.Columns.Add("SourceLotNumber");
						}
						if (!dataTable.Columns.Contains("SoldQty"))
						{
							dataTable.Columns.Add("SoldQty");
							dataRow["SoldQty"] = num;
						}
						dataRow["Cost"] = array[i]["Cost"];
						dataTable.Rows.Add(dataRow);
					}
					row.Cells["Quantity"].Tag = dataTable;
					row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SoldQty"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Hidden = true;
			}
			else
			{
				dataGridItems.ImportFromExcel(autoFill: true);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (row2.Cells["Item Code"].Value.ToString() != "")
					{
						string text3 = row2.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text3))
						{
							string value3 = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text3).ToString() ?? null;
							row2.Cells["Description"].Value = value3;
							string value4 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text3).ToString() ?? null;
							row2.Cells["Unit"].Value = value4;
							decimal d2 = decimal.Parse(row2.Cells["Quantity"].Value.ToString());
							decimal d3 = decimal.Parse(row2.Cells["Price"].Value.ToString());
							ItemTaxOptions itemTaxOptions2 = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text3).ToString() ?? null);
							row2.Cells["TaxOption"].Value = itemTaxOptions2;
							switch (itemTaxOptions2)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row2.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row2.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text3).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row2.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							row2.Cells["Amount"].Value = (d2 * d3).ToString(Format.TotalAmountFormat);
							if (row2.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
						}
					}
				}
			}
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void toolStripButtonApproval_Click(object sender, EventArgs e)
		{
			new ViewApprovalDetailDialog().ShowApprovalDetail(1, 25, textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID);
		}

		private void toolStripMenuRelationshipMap_Click(object sender, EventArgs e)
		{
			RelationshipMapForm relationshipMapForm = new RelationshipMapForm();
			relationshipMapForm.sysDocID = comboBoxSysDoc.SelectedID;
			relationshipMapForm.voucherID = textBoxVoucherNumber.Text;
			relationshipMapForm.Show();
		}

		private void toolStripButtonInfo_MouseHover(object sender, EventArgs e)
		{
			toolTip.OwnerDraw = true;
			toolTip.Draw += tooltip_Draw;
			toolTip.Popup += tooltip_Popup;
			toolTip.InitialDelay = 500;
			toolTip.IsBalloon = true;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
			if (fieldValue.ToString() != "" || fieldValue.ToString() != string.Empty)
			{
				toolStrip1.Parent.PointToClient(Cursor.Position);
				toolStripButtonInfo.Visible = true;
				toolTip.Show(fieldValue.ToString(), toolStrip1);
			}
			else
			{
				toolTip.Show("", toolStrip1.Parent);
				toolStripButtonInfo.Visible = false;
			}
		}

		private void tooltip_Popup(object sender, PopupEventArgs e)
		{
		}

		public bool ValidationwithColor()
		{
			bool flag = true;
			try
			{
				object[] all = dataGridItems.Rows.All;
				for (int i = 0; i < all.Length; i++)
				{
					UltraGridRow ultraGridRow = (UltraGridRow)all[i];
					ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Transparent;
					bool flag2 = false;
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					if (ultraGridRow != null && ultraGridRow.DataChanged)
					{
						decimal.TryParse(ultraGridRow.Cells["Price"].Value.ToString(), out result2);
						decimal num = default(decimal);
						decimal num2 = default(decimal);
						string text = "MinPrice";
						text = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) ? "MinPrice" : "UnitPrice3");
						decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString()).ToString(), out result);
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString(), "UnitID", ultraGridRow.Cells["Unit"].Value.ToString(), "LocationID", comboBoxSysDoc.LocationID);
						if (fieldValue == null)
						{
							fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString(), "UnitID", ultraGridRow.Cells["Unit"].Value.ToString(), "LocationID", "");
						}
						if (fieldValue != null)
						{
							decimal.TryParse(fieldValue.ToString(), out result);
						}
						string text2 = ultraGridRow.Cells["Unit"].Value.ToString();
						string text3 = ultraGridRow.Cells["Item Code"].Value.ToString();
						ultraGridRow.Cells["Location"].Value.ToString();
						if (text3 != "" && text2 != "")
						{
							DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text3, text2, comboBoxSysDoc.LocationID);
							if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
							{
								DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
								string a = dataRow["FactorType"].ToString();
								decimal num3 = decimal.Parse(dataRow["Factor"].ToString());
								if (a == "M")
								{
									result /= num3;
								}
								else
								{
									result *= num3;
								}
							}
						}
						num = result * (priceDiscountPercAllowed / 100m);
						num2 = result - num;
						if (result2 < num2)
						{
							flag2 = true;
							if (priceDiscountPercAllowed > 0m)
							{
								ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
								flag = false;
							}
						}
						if (Global.IsUserAdmin || priceDiscountPercAllowed == 0m)
						{
							flag2 = false;
						}
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount) && result2 < num2)
						{
							flag2 = true;
						}
					}
					checked
					{
						if (CompanyPreferences.LocalSalesFlow != SalesFlows.SOThenDNThenInvoice && ultraGridRow != null && ultraGridRow.DataChanged && ultraGridRow.Cells["ItemType"].Value.ToString() != "")
						{
							_ = (byte)int.Parse(ultraGridRow.Cells["ItemType"].Value.ToString());
						}
						if (ultraGridRow != null && ultraGridRow.DataChanged && ultraGridRow.Cells["Price"].Value != null && ultraGridRow.Cells["Price"].Value.ToString() != "")
						{
							if (CompanyPreferences.MinPriceSaleAction != 1)
							{
								bool flag3 = Factory.SalesInvoiceSystem.IsBelowMinPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
								if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
								{
									flag3 = Factory.SalesInvoiceSystem.IsBelowMinAllowedPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()));
								}
								if (flag3)
								{
									if (CompanyPreferences.MinPriceSaleAction == 2)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
									else if (CompanyPreferences.MinPriceSaleAction == 3 && flag2)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
									else if (CompanyPreferences.MinPriceSaleAction == 4)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
								}
							}
							if (CompanyPreferences.PricelessCostAction != 1 && Factory.SalesInvoiceSystem.IsBelowAverageCost(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString())))
							{
								if (CompanyPreferences.PricelessCostAction == 2)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
								else if (CompanyPreferences.PricelessCostAction == 3)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
								else if (CompanyPreferences.PricelessCostAction == 4)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
							}
						}
						flag = (flag && flag);
					}
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return flag;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesInvoiceForm));
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
            Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
            this.tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dataGridItems = new Micromind.DataControls.DataEntryGrid();
            this.groupBoxTax = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
            this.ComboBoxitemJob = new Micromind.DataControls.JobComboBox();
            this.comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
            this.comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
            this.comboBoxJob1 = new Micromind.DataControls.JobComboBox();
            this.productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
            this.comboBoxSpecification = new Micromind.DataControls.ProductSpecificationComboBox();
            this.comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
            this.labelVoided = new System.Windows.Forms.Label();
            this.tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dataGridExpense = new Micromind.DataControls.DataEntryGrid();
            this.comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
            this.comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBalance = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPaymentAllocation = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuRelationshipMap = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromProformaInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromDeliveryNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.grantEditPermissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
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
            this.labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
            this.ultraFormattedLinkPaymentTerm = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.ultraFormattedLinkProject = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.ultraFormattedLinkShipping = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.ultraFormattedLinkCostCategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.ultraFormattedLinkCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxShipto = new Micromind.UISupport.MMTextBox();
            this.comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
            this.comboBoxJob = new Micromind.DataControls.JobComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRef2 = new System.Windows.Forms.TextBox();
            this.buttonSelectDocument = new Micromind.UISupport.XPButton();
            this.comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
            this.comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
            this.comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
            this.ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxBillingAddress = new Micromind.DataControls.CustomerAddressComboBox();
            this.comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
            this.ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPONumber = new System.Windows.Forms.TextBox();
            this.textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
            this.ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
            this.ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
            this.mmLabel2 = new Micromind.UISupport.MMLabel();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.textBoxCustomerName = new System.Windows.Forms.TextBox();
            this.dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
            this.comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
            this.labelReportTo = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSelectedDocs = new System.Windows.Forms.Label();
            this.checkedListBoxDN = new System.Windows.Forms.ListBox();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxTotalExpense = new Micromind.UISupport.AmountTextBox();
            this.textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
            this.textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
            this.panelNonTax = new System.Windows.Forms.Panel();
            this.linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
            this.textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
            this.panelTotal = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRoundOff = new Micromind.UISupport.AmountTextBox();
            this.textBoxTotal = new Micromind.UISupport.AmountTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.formManager = new Micromind.DataControls.FormManager();
            this.tabPageItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).BeginInit();
            this.groupBoxTax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxitemcostCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxitemJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxJob1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSpecification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxStyle)).BeginInit();
            this.tabPageExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridExpenseCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridCurrency)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayeeTaxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCostCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxBillingAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingAddressID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSysDoc)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelNonTax.SuspendLayout();
            this.panelTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.dataGridItems);
            this.tabPageItems.Controls.Add(this.groupBoxTax);
            this.tabPageItems.Controls.Add(this.ComboBoxitemcostCategory);
            this.tabPageItems.Controls.Add(this.ComboBoxitemJob);
            this.tabPageItems.Controls.Add(this.comboBoxGridItem);
            this.tabPageItems.Controls.Add(this.comboBoxGridLocation);
            this.tabPageItems.Controls.Add(this.comboBoxJob1);
            this.tabPageItems.Controls.Add(this.productPhotoViewer);
            this.tabPageItems.Controls.Add(this.comboBoxSpecification);
            this.tabPageItems.Controls.Add(this.comboBoxStyle);
            this.tabPageItems.Controls.Add(this.labelVoided);
            this.tabPageItems.Location = new System.Drawing.Point(23, 1);
            this.tabPageItems.Name = "tabPageItems";
            this.tabPageItems.Size = new System.Drawing.Size(955, 273);
            // 
            // dataGridItems
            // 
            this.dataGridItems.AllowAddNew = false;
            this.dataGridItems.AllowCustomizeHeaders = true;
            this.dataGridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridItems.DisplayLayout.Appearance = appearance1;
            this.dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
            this.dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridItems.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
            this.dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
            this.dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridItems.ExitEditModeOnLeave = false;
            this.dataGridItems.IncludeLotItems = false;
            this.dataGridItems.LoadLayoutFailed = false;
            this.dataGridItems.Location = new System.Drawing.Point(5, 3);
            this.dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
            this.dataGridItems.Name = "dataGridItems";
            this.dataGridItems.ShowClearMenu = true;
            this.dataGridItems.ShowDeleteMenu = true;
            this.dataGridItems.ShowInsertMenu = true;
            this.dataGridItems.ShowMoveRowsMenu = true;
            this.dataGridItems.Size = new System.Drawing.Size(944, 266);
            this.dataGridItems.TabIndex = 1;
            this.dataGridItems.Text = "dataEntryGrid1";
            // 
            // groupBoxTax
            // 
            this.groupBoxTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTax.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxTax.Location = new System.Drawing.Point(630, 120);
            this.groupBoxTax.Name = "groupBoxTax";
            this.groupBoxTax.Size = new System.Drawing.Size(201, 92);
            this.groupBoxTax.TabIndex = 128;
            this.groupBoxTax.TabStop = false;
            this.groupBoxTax.Text = "Tax :";
            this.groupBoxTax.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(193, 76);
            this.tableLayoutPanel1.TabIndex = 132;
            // 
            // ComboBoxitemcostCategory
            // 
            this.ComboBoxitemcostCategory.Assigned = false;
            this.ComboBoxitemcostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.ComboBoxitemcostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ComboBoxitemcostCategory.CustomReportFieldName = "";
            this.ComboBoxitemcostCategory.CustomReportKey = "";
            this.ComboBoxitemcostCategory.CustomReportValueType = ((byte)(1));
            this.ComboBoxitemcostCategory.DescriptionTextBox = null;
            this.ComboBoxitemcostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.ComboBoxitemcostCategory.Editable = true;
            this.ComboBoxitemcostCategory.FilterString = "";
            this.ComboBoxitemcostCategory.HasAllAccount = false;
            this.ComboBoxitemcostCategory.HasCustom = false;
            this.ComboBoxitemcostCategory.IsDataLoaded = false;
            this.ComboBoxitemcostCategory.Location = new System.Drawing.Point(404, 99);
            this.ComboBoxitemcostCategory.MaxDropDownItems = 12;
            this.ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
            this.ComboBoxitemcostCategory.ShowInactiveItems = false;
            this.ComboBoxitemcostCategory.ShowQuickAdd = true;
            this.ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
            this.ComboBoxitemcostCategory.TabIndex = 158;
            this.ComboBoxitemcostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ComboBoxitemcostCategory.Visible = false;
            // 
            // ComboBoxitemJob
            // 
            this.ComboBoxitemJob.Assigned = false;
            this.ComboBoxitemJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.ComboBoxitemJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ComboBoxitemJob.CustomReportFieldName = "";
            this.ComboBoxitemJob.CustomReportKey = "";
            this.ComboBoxitemJob.CustomReportValueType = ((byte)(1));
            this.ComboBoxitemJob.DescriptionTextBox = null;
            this.ComboBoxitemJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.ComboBoxitemJob.Editable = true;
            this.ComboBoxitemJob.FilterString = "";
            this.ComboBoxitemJob.HasAllAccount = false;
            this.ComboBoxitemJob.HasCustom = false;
            this.ComboBoxitemJob.IsDataLoaded = false;
            this.ComboBoxitemJob.Location = new System.Drawing.Point(231, 99);
            this.ComboBoxitemJob.MaxDropDownItems = 12;
            this.ComboBoxitemJob.Name = "ComboBoxitemJob";
            this.ComboBoxitemJob.ShowInactiveItems = false;
            this.ComboBoxitemJob.ShowQuickAdd = true;
            this.ComboBoxitemJob.Size = new System.Drawing.Size(100, 20);
            this.ComboBoxitemJob.TabIndex = 157;
            this.ComboBoxitemJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ComboBoxitemJob.Visible = false;
            // 
            // comboBoxGridItem
            // 
            this.comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
            this.comboBoxGridItem.AlwaysInEditMode = true;
            this.comboBoxGridItem.Assigned = false;
            this.comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridItem.CustomReportFieldName = "";
            this.comboBoxGridItem.CustomReportKey = "";
            this.comboBoxGridItem.CustomReportValueType = ((byte)(1));
            this.comboBoxGridItem.DescriptionTextBox = null;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridItem.DisplayLayout.Appearance = appearance13;
            this.comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance20;
            this.comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance23;
            this.comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
            this.comboBoxGridItem.Location = new System.Drawing.Point(497, 19);
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
            // comboBoxGridLocation
            // 
            this.comboBoxGridLocation.AlwaysInEditMode = true;
            this.comboBoxGridLocation.Assigned = false;
            this.comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridLocation.CustomReportFieldName = "";
            this.comboBoxGridLocation.CustomReportKey = "";
            this.comboBoxGridLocation.CustomReportValueType = ((byte)(1));
            this.comboBoxGridLocation.DescriptionTextBox = null;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridLocation.DisplayLayout.Appearance = appearance25;
            this.comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance26.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance26;
            appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance28.BackColor2 = System.Drawing.SystemColors.Control;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
            this.comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance29;
            appearance30.BackColor = System.Drawing.SystemColors.Highlight;
            appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance30;
            this.comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance31;
            appearance32.BorderColor = System.Drawing.Color.Silver;
            appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance32;
            this.comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
            appearance33.BackColor = System.Drawing.SystemColors.Control;
            appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance33.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance33;
            appearance34.TextHAlignAsString = "Left";
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance34;
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            appearance35.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance35;
            this.comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
            this.comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGridLocation.Editable = true;
            this.comboBoxGridLocation.FilterString = "";
            this.comboBoxGridLocation.HasAllAccount = false;
            this.comboBoxGridLocation.HasCustom = false;
            this.comboBoxGridLocation.IsDataLoaded = false;
            this.comboBoxGridLocation.Location = new System.Drawing.Point(471, 34);
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
            this.comboBoxGridLocation.TabIndex = 122;
            this.comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxGridLocation.Visible = false;
            // 
            // comboBoxJob1
            // 
            this.comboBoxJob1.Assigned = false;
            this.comboBoxJob1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxJob1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxJob1.CustomReportFieldName = "";
            this.comboBoxJob1.CustomReportKey = "";
            this.comboBoxJob1.CustomReportValueType = ((byte)(1));
            this.comboBoxJob1.DescriptionTextBox = null;
            this.comboBoxJob1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxJob1.Editable = true;
            this.comboBoxJob1.FilterString = "";
            this.comboBoxJob1.HasAllAccount = false;
            this.comboBoxJob1.HasCustom = false;
            this.comboBoxJob1.IsDataLoaded = false;
            this.comboBoxJob1.Location = new System.Drawing.Point(528, 52);
            this.comboBoxJob1.MaxDropDownItems = 12;
            this.comboBoxJob1.Name = "comboBoxJob1";
            this.comboBoxJob1.ShowInactiveItems = false;
            this.comboBoxJob1.ShowQuickAdd = true;
            this.comboBoxJob1.Size = new System.Drawing.Size(100, 20);
            this.comboBoxJob1.TabIndex = 128;
            this.comboBoxJob1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxJob1.Visible = false;
            // 
            // productPhotoViewer
            // 
            this.productPhotoViewer.BackColor = System.Drawing.Color.White;
            this.productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productPhotoViewer.Location = new System.Drawing.Point(8, 37);
            this.productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.Name = "productPhotoViewer";
            this.productPhotoViewer.Size = new System.Drawing.Size(186, 162);
            this.productPhotoViewer.TabIndex = 120;
            this.productPhotoViewer.Visible = false;
            // 
            // comboBoxSpecification
            // 
            this.comboBoxSpecification.Assigned = false;
            this.comboBoxSpecification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSpecification.CustomReportFieldName = "";
            this.comboBoxSpecification.CustomReportKey = "";
            this.comboBoxSpecification.CustomReportValueType = ((byte)(1));
            this.comboBoxSpecification.DescriptionTextBox = null;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSpecification.DisplayLayout.Appearance = appearance37;
            this.comboBoxSpecification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSpecification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance38.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSpecification.DisplayLayout.GroupByBox.Appearance = appearance38;
            appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSpecification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
            this.comboBoxSpecification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance40.BackColor2 = System.Drawing.SystemColors.Control;
            appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSpecification.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
            this.comboBoxSpecification.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSpecification.DisplayLayout.MaxRowScrollRegions = 1;
            appearance41.BackColor = System.Drawing.SystemColors.Window;
            appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSpecification.DisplayLayout.Override.ActiveCellAppearance = appearance41;
            appearance42.BackColor = System.Drawing.SystemColors.Highlight;
            appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSpecification.DisplayLayout.Override.ActiveRowAppearance = appearance42;
            this.comboBoxSpecification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSpecification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSpecification.DisplayLayout.Override.CardAreaAppearance = appearance43;
            appearance44.BorderColor = System.Drawing.Color.Silver;
            appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSpecification.DisplayLayout.Override.CellAppearance = appearance44;
            this.comboBoxSpecification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSpecification.DisplayLayout.Override.CellPadding = 0;
            appearance45.BackColor = System.Drawing.SystemColors.Control;
            appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance45.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSpecification.DisplayLayout.Override.GroupByRowAppearance = appearance45;
            appearance46.TextHAlignAsString = "Left";
            this.comboBoxSpecification.DisplayLayout.Override.HeaderAppearance = appearance46;
            this.comboBoxSpecification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSpecification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance47.BackColor = System.Drawing.SystemColors.Window;
            appearance47.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSpecification.DisplayLayout.Override.RowAppearance = appearance47;
            this.comboBoxSpecification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSpecification.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
            this.comboBoxSpecification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSpecification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSpecification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSpecification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSpecification.Editable = true;
            this.comboBoxSpecification.FilterString = "";
            this.comboBoxSpecification.HasAllAccount = false;
            this.comboBoxSpecification.HasCustom = false;
            this.comboBoxSpecification.IsDataLoaded = false;
            this.comboBoxSpecification.Location = new System.Drawing.Point(478, 130);
            this.comboBoxSpecification.MaxDropDownItems = 12;
            this.comboBoxSpecification.Name = "comboBoxSpecification";
            this.comboBoxSpecification.ShowInactiveItems = false;
            this.comboBoxSpecification.Size = new System.Drawing.Size(100, 20);
            this.comboBoxSpecification.TabIndex = 160;
            this.comboBoxSpecification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxSpecification.Visible = false;
            // 
            // comboBoxStyle
            // 
            this.comboBoxStyle.Assigned = false;
            this.comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxStyle.CustomReportFieldName = "";
            this.comboBoxStyle.CustomReportKey = "";
            this.comboBoxStyle.CustomReportValueType = ((byte)(1));
            this.comboBoxStyle.DescriptionTextBox = null;
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxStyle.DisplayLayout.Appearance = appearance49;
            this.comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance50.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance50;
            appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
            this.comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance52.BackColor2 = System.Drawing.SystemColors.Control;
            appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
            this.comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
            appearance53.BackColor = System.Drawing.SystemColors.Window;
            appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance53;
            appearance54.BackColor = System.Drawing.SystemColors.Highlight;
            appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance54;
            this.comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance55;
            appearance56.BorderColor = System.Drawing.Color.Silver;
            appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance56;
            this.comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
            appearance57.BackColor = System.Drawing.SystemColors.Control;
            appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance57.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance57;
            appearance58.TextHAlignAsString = "Left";
            this.comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance58;
            this.comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            appearance59.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance59;
            this.comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
            this.comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxStyle.Editable = true;
            this.comboBoxStyle.FilterString = "";
            this.comboBoxStyle.HasAllAccount = false;
            this.comboBoxStyle.HasCustom = false;
            this.comboBoxStyle.IsDataLoaded = false;
            this.comboBoxStyle.Location = new System.Drawing.Point(284, 103);
            this.comboBoxStyle.MaxDropDownItems = 12;
            this.comboBoxStyle.Name = "comboBoxStyle";
            this.comboBoxStyle.ShowInactiveItems = false;
            this.comboBoxStyle.Size = new System.Drawing.Size(154, 20);
            this.comboBoxStyle.TabIndex = 159;
            this.comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxStyle.Visible = false;
            // 
            // labelVoided
            // 
            this.labelVoided.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVoided.BackColor = System.Drawing.Color.White;
            this.labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVoided.ForeColor = System.Drawing.Color.DarkRed;
            this.labelVoided.Location = new System.Drawing.Point(-93, 212);
            this.labelVoided.Name = "labelVoided";
            this.labelVoided.Size = new System.Drawing.Size(1114, 59);
            this.labelVoided.TabIndex = 0;
            this.labelVoided.Text = "VOIDED";
            this.labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVoided.Visible = false;
            // 
            // tabPageExpense
            // 
            this.tabPageExpense.Controls.Add(this.dataGridExpense);
            this.tabPageExpense.Controls.Add(this.comboBoxGridExpenseCode);
            this.tabPageExpense.Controls.Add(this.comboBoxGridCurrency);
            this.tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
            this.tabPageExpense.Name = "tabPageExpense";
            this.tabPageExpense.Size = new System.Drawing.Size(955, 273);
            // 
            // dataGridExpense
            // 
            this.dataGridExpense.AllowAddNew = false;
            this.dataGridExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridExpense.DisplayLayout.Appearance = appearance61;
            this.dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance62.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance62;
            appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
            this.dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance64.BackColor2 = System.Drawing.SystemColors.Control;
            appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
            this.dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance65;
            appearance66.BackColor = System.Drawing.SystemColors.Highlight;
            appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance66;
            this.dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance67.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance67;
            appearance68.BorderColor = System.Drawing.Color.Silver;
            appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridExpense.DisplayLayout.Override.CellAppearance = appearance68;
            this.dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridExpense.DisplayLayout.Override.CellPadding = 0;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance69;
            appearance70.TextHAlignAsString = "Left";
            this.dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance70;
            this.dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance71.BackColor = System.Drawing.SystemColors.Window;
            appearance71.BorderColor = System.Drawing.Color.Silver;
            this.dataGridExpense.DisplayLayout.Override.RowAppearance = appearance71;
            this.dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
            this.dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridExpense.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
            this.dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridExpense.ExitEditModeOnLeave = false;
            this.dataGridExpense.IncludeLotItems = false;
            this.dataGridExpense.LoadLayoutFailed = false;
            this.dataGridExpense.Location = new System.Drawing.Point(4, 2);
            this.dataGridExpense.MinimumSize = new System.Drawing.Size(450, 50);
            this.dataGridExpense.Name = "dataGridExpense";
            this.dataGridExpense.ShowClearMenu = true;
            this.dataGridExpense.ShowDeleteMenu = true;
            this.dataGridExpense.ShowInsertMenu = true;
            this.dataGridExpense.ShowMoveRowsMenu = true;
            this.dataGridExpense.Size = new System.Drawing.Size(948, 268);
            this.dataGridExpense.TabIndex = 2;
            this.dataGridExpense.Text = "dataEntryGrid1";
            // 
            // comboBoxGridExpenseCode
            // 
            this.comboBoxGridExpenseCode.AlwaysInEditMode = true;
            this.comboBoxGridExpenseCode.Assigned = false;
            this.comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridExpenseCode.CustomReportFieldName = "";
            this.comboBoxGridExpenseCode.CustomReportKey = "";
            this.comboBoxGridExpenseCode.CustomReportValueType = ((byte)(1));
            this.comboBoxGridExpenseCode.DescriptionTextBox = null;
            appearance73.BackColor = System.Drawing.SystemColors.Window;
            appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance73;
            this.comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance74.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance74;
            appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
            this.comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance76.BackColor2 = System.Drawing.SystemColors.Control;
            appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
            this.comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
            appearance77.BackColor = System.Drawing.SystemColors.Window;
            appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance77;
            appearance78.BackColor = System.Drawing.SystemColors.Highlight;
            appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance78;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance79.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance79;
            appearance80.BorderColor = System.Drawing.Color.Silver;
            appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance80;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
            appearance81.BackColor = System.Drawing.SystemColors.Control;
            appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance81.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance81;
            appearance82.TextHAlignAsString = "Left";
            this.comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance82;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance83.BackColor = System.Drawing.SystemColors.Window;
            appearance83.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance83;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
            this.comboBoxGridExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGridExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGridExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGridExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGridExpenseCode.Editable = true;
            this.comboBoxGridExpenseCode.FilterString = "";
            this.comboBoxGridExpenseCode.HasAllAccount = false;
            this.comboBoxGridExpenseCode.HasCustom = false;
            this.comboBoxGridExpenseCode.IsDataLoaded = false;
            this.comboBoxGridExpenseCode.Location = new System.Drawing.Point(557, 40);
            this.comboBoxGridExpenseCode.MaxDropDownItems = 12;
            this.comboBoxGridExpenseCode.Name = "comboBoxGridExpenseCode";
            this.comboBoxGridExpenseCode.ShowInactiveItems = false;
            this.comboBoxGridExpenseCode.ShowQuickAdd = true;
            this.comboBoxGridExpenseCode.Size = new System.Drawing.Size(124, 20);
            this.comboBoxGridExpenseCode.TabIndex = 121;
            this.comboBoxGridExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxGridExpenseCode.Visible = false;
            // 
            // comboBoxGridCurrency
            // 
            this.comboBoxGridCurrency.AlwaysInEditMode = true;
            this.comboBoxGridCurrency.Assigned = false;
            this.comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGridCurrency.CustomReportFieldName = "";
            this.comboBoxGridCurrency.CustomReportKey = "";
            this.comboBoxGridCurrency.CustomReportValueType = ((byte)(1));
            this.comboBoxGridCurrency.DescriptionTextBox = null;
            appearance85.BackColor = System.Drawing.SystemColors.Window;
            appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGridCurrency.DisplayLayout.Appearance = appearance85;
            this.comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance86.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance86;
            appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
            this.comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance88.BackColor2 = System.Drawing.SystemColors.Control;
            appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
            this.comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
            appearance89.BackColor = System.Drawing.SystemColors.Window;
            appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance89;
            appearance90.BackColor = System.Drawing.SystemColors.Highlight;
            appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance90;
            this.comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance91.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance91;
            appearance92.BorderColor = System.Drawing.Color.Silver;
            appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance92;
            this.comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
            appearance93.BackColor = System.Drawing.SystemColors.Control;
            appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance93.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance93;
            appearance94.TextHAlignAsString = "Left";
            this.comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance94;
            this.comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance95.BackColor = System.Drawing.SystemColors.Window;
            appearance95.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance95;
            this.comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
            this.comboBoxGridCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGridCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGridCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGridCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGridCurrency.Editable = true;
            this.comboBoxGridCurrency.FilterString = "";
            this.comboBoxGridCurrency.HasAllAccount = false;
            this.comboBoxGridCurrency.HasCustom = false;
            this.comboBoxGridCurrency.IsDataLoaded = false;
            this.comboBoxGridCurrency.Location = new System.Drawing.Point(350, 104);
            this.comboBoxGridCurrency.MaxDropDownItems = 12;
            this.comboBoxGridCurrency.Name = "comboBoxGridCurrency";
            this.comboBoxGridCurrency.ShowInactiveItems = false;
            this.comboBoxGridCurrency.ShowQuickAdd = true;
            this.comboBoxGridCurrency.Size = new System.Drawing.Size(95, 20);
            this.comboBoxGridCurrency.TabIndex = 122;
            this.comboBoxGridCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxGridCurrency.Visible = false;
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
            this.toolStripSeparator6,
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
            this.toolStripSeparator9,
            this.toolStripButtonExcelImport,
            this.toolStripButtonInformation,
            this.toolStripButtonBalance,
            this.toolStripButtonPaymentAllocation,
            this.toolStripDropDownButton1,
            this.toolStripButtonInfo,
            this.toolStripButtonApproval,
            this.toolStripLabelApproval,
            this.toolStripSeparatorApproval});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(993, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.MouseEnter += new System.EventHandler(this.toolStrip1_MouseEnter);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
            this.toolStripSeparator7.Click += new System.EventHandler(this.toolStripSeparator7_Click);
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
            this.toolStripButtonDistribution.Text = "Journal Distribution Summary";
            this.toolStripButtonDistribution.Click += new System.EventHandler(this.toolStripButtonDistribution_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonExcelImport
            // 
            this.toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExcelImport.Image = global::Micromind.ClientUI.Properties.Resources.excelimport;
            this.toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
            this.toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonExcelImport.Text = "Import from Excel";
            this.toolStripButtonExcelImport.Click += new System.EventHandler(this.toolStripButtonExcelImport_Click);
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
            // toolStripButtonBalance
            // 
            this.toolStripButtonBalance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBalance.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBalance.Image")));
            this.toolStripButtonBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBalance.Name = "toolStripButtonBalance";
            this.toolStripButtonBalance.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonBalance.Text = "Check Customer Balance";
            this.toolStripButtonBalance.Click += new System.EventHandler(this.toolStripButtonBalance_Click);
            // 
            // toolStripButtonPaymentAllocation
            // 
            this.toolStripButtonPaymentAllocation.Image = global::Micromind.ClientUI.Properties.Resources.allocate;
            this.toolStripButtonPaymentAllocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPaymentAllocation.Name = "toolStripButtonPaymentAllocation";
            this.toolStripButtonPaymentAllocation.Size = new System.Drawing.Size(82, 28);
            this.toolStripButtonPaymentAllocation.Text = "Payment";
            this.toolStripButtonPaymentAllocation.Click += new System.EventHandler(this.toolStripButtonPaymentAllocation_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem,
            this.saveAsDraftToolStripMenuItem,
            this.loadDraftToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripMenuRelationshipMap,
            this.createFromProformaInvoiceToolStripMenuItem,
            this.createFromDeliveryNoteToolStripMenuItem,
            this.createFromSalesOrderToolStripMenuItem,
            this.toolStripSeparator8,
            this.grantEditPermissionToolStripMenuItem});
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
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.duplicateToolStripMenuItem.Text = "Copy";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // saveAsDraftToolStripMenuItem
            // 
            this.saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
            this.saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.saveAsDraftToolStripMenuItem.Text = "Save as Draft";
            this.saveAsDraftToolStripMenuItem.Click += new System.EventHandler(this.saveAsDraftToolStripMenuItem_Click);
            // 
            // loadDraftToolStripMenuItem
            // 
            this.loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
            this.loadDraftToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.loadDraftToolStripMenuItem.Text = "Load Draft...";
            this.loadDraftToolStripMenuItem.Click += new System.EventHandler(this.loadDraftToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(228, 6);
            // 
            // toolStripMenuRelationshipMap
            // 
            this.toolStripMenuRelationshipMap.Name = "toolStripMenuRelationshipMap";
            this.toolStripMenuRelationshipMap.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuRelationshipMap.Text = "RelationshipMap";
            this.toolStripMenuRelationshipMap.Click += new System.EventHandler(this.toolStripMenuRelationshipMap_Click);
            // 
            // createFromProformaInvoiceToolStripMenuItem
            // 
            this.createFromProformaInvoiceToolStripMenuItem.Name = "createFromProformaInvoiceToolStripMenuItem";
            this.createFromProformaInvoiceToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.createFromProformaInvoiceToolStripMenuItem.Text = "Create from Proforma Invoice";
            this.createFromProformaInvoiceToolStripMenuItem.Click += new System.EventHandler(this.createFromProformaInvoiceToolStripMenuItem_Click);
            // 
            // createFromDeliveryNoteToolStripMenuItem
            // 
            this.createFromDeliveryNoteToolStripMenuItem.Name = "createFromDeliveryNoteToolStripMenuItem";
            this.createFromDeliveryNoteToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.createFromDeliveryNoteToolStripMenuItem.Text = "Create from Delivery Note...";
            this.createFromDeliveryNoteToolStripMenuItem.Click += new System.EventHandler(this.createFromDeliveryNoteToolStripMenuItem_Click);
            // 
            // createFromSalesOrderToolStripMenuItem
            // 
            this.createFromSalesOrderToolStripMenuItem.Name = "createFromSalesOrderToolStripMenuItem";
            this.createFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.createFromSalesOrderToolStripMenuItem.Text = "Create from Sales Order...";
            this.createFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(this.createFromSalesOrderToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(228, 6);
            // 
            // grantEditPermissionToolStripMenuItem
            // 
            this.grantEditPermissionToolStripMenuItem.Name = "grantEditPermissionToolStripMenuItem";
            this.grantEditPermissionToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.grantEditPermissionToolStripMenuItem.Text = "Grant Edit Permission";
            this.grantEditPermissionToolStripMenuItem.Click += new System.EventHandler(this.grantEditPermissionToolStripMenuItem_Click);
            // 
            // toolStripButtonInfo
            // 
            this.toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInfo.Image = global::Micromind.ClientUI.Properties.Resources.Alert;
            this.toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInfo.Name = "toolStripButtonInfo";
            this.toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonInfo.Visible = false;
            this.toolStripButtonInfo.Click += new System.EventHandler(this.toolStripButtonInfo_Click);
            this.toolStripButtonInfo.MouseLeave += new System.EventHandler(this.toolStripButtonInfo_MouseLeave);
            this.toolStripButtonInfo.MouseHover += new System.EventHandler(this.toolStripButtonInfo_MouseHover);
            // 
            // toolStripButtonApproval
            // 
            this.toolStripButtonApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonApproval.AutoSize = false;
            this.toolStripButtonApproval.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButtonApproval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButtonApproval.ForeColor = System.Drawing.Color.Green;
            this.toolStripButtonApproval.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonApproval.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonApproval.Name = "toolStripButtonApproval";
            this.toolStripButtonApproval.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripButtonApproval.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonApproval.Text = "Pending";
            this.toolStripButtonApproval.Click += new System.EventHandler(this.toolStripButtonApproval_Click);
            // 
            // toolStripLabelApproval
            // 
            this.toolStripLabelApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelApproval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabelApproval.Name = "toolStripLabelApproval";
            this.toolStripLabelApproval.Size = new System.Drawing.Size(45, 28);
            this.toolStripLabelApproval.Text = "Status:";
            // 
            // toolStripSeparatorApproval
            // 
            this.toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
            this.toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
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
            this.panelButtons.Location = new System.Drawing.Point(0, 619);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(993, 40);
            this.panelButtons.TabIndex = 7;
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
            this.linePanelDown.Size = new System.Drawing.Size(993, 1);
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
            this.xpButton1.Location = new System.Drawing.Point(883, 8);
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
            this.dateTimePickerDate.Location = new System.Drawing.Point(730, 3);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(111, 20);
            this.dateTimePickerDate.TabIndex = 12;
            // 
            // textBoxVoucherNumber
            // 
            this.textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
            this.textBoxVoucherNumber.MaxLength = 15;
            this.textBoxVoucherNumber.Name = "textBoxVoucherNumber";
            this.textBoxVoucherNumber.Size = new System.Drawing.Size(181, 20);
            this.textBoxVoucherNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(650, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Reference 1:";
            // 
            // textBoxRef1
            // 
            this.textBoxRef1.Location = new System.Drawing.Point(730, 25);
            this.textBoxRef1.MaxLength = 20;
            this.textBoxRef1.Name = "textBoxRef1";
            this.textBoxRef1.Size = new System.Drawing.Size(142, 20);
            this.textBoxRef1.TabIndex = 14;
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxNote.Location = new System.Drawing.Point(52, 484);
            this.textBoxNote.MaxLength = 4000;
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNote.Size = new System.Drawing.Size(306, 106);
            this.textBoxNote.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 494);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Note:";
            // 
            // ultraFormattedLinkLabel2
            // 
            appearance97.FontData.BoldAsString = "True";
            appearance97.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel2.Appearance = appearance97;
            this.ultraFormattedLinkLabel2.AutoSize = true;
            this.ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
            this.ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
            this.ultraFormattedLinkLabel2.Size = new System.Drawing.Size(102, 15);
            this.ultraFormattedLinkLabel2.TabIndex = 1;
            this.ultraFormattedLinkLabel2.TabStop = true;
            this.ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel2.Value = "Voucher Number:";
            appearance98.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance98;
            this.ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel2_LinkClicked);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.labelTaxGroup);
            this.panelDetails.Controls.Add(this.comboBoxPayeeTaxGroup);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkPaymentTerm);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkProject);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkShipping);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkCostCategory);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkCurrency);
            this.panelDetails.Controls.Add(this.textBoxShipto);
            this.panelDetails.Controls.Add(this.comboBoxCostCategory);
            this.panelDetails.Controls.Add(this.comboBoxJob);
            this.panelDetails.Controls.Add(this.label4);
            this.panelDetails.Controls.Add(this.textBoxRef2);
            this.panelDetails.Controls.Add(this.buttonSelectDocument);
            this.panelDetails.Controls.Add(this.comboBoxShippingMethod);
            this.panelDetails.Controls.Add(this.comboBoxTerm);
            this.panelDetails.Controls.Add(this.comboBoxSalesperson);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel6);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel3);
            this.panelDetails.Controls.Add(this.comboBoxBillingAddress);
            this.panelDetails.Controls.Add(this.comboBoxShippingAddressID);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel1);
            this.panelDetails.Controls.Add(this.label2);
            this.panelDetails.Controls.Add(this.textBoxPONumber);
            this.panelDetails.Controls.Add(this.textBoxBilltoAddress);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel4);
            this.panelDetails.Controls.Add(this.comboBoxCustomer);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel5);
            this.panelDetails.Controls.Add(this.comboBoxSysDoc);
            this.panelDetails.Controls.Add(this.ultraFormattedLinkLabel2);
            this.panelDetails.Controls.Add(this.mmLabel2);
            this.panelDetails.Controls.Add(this.mmLabel1);
            this.panelDetails.Controls.Add(this.label1);
            this.panelDetails.Controls.Add(this.textBoxCustomerName);
            this.panelDetails.Controls.Add(this.textBoxRef1);
            this.panelDetails.Controls.Add(this.textBoxVoucherNumber);
            this.panelDetails.Controls.Add(this.dateTimePickerDueDate);
            this.panelDetails.Controls.Add(this.dateTimePickerDate);
            this.panelDetails.Controls.Add(this.comboBoxCurrency);
            this.panelDetails.Controls.Add(this.labelReportTo);
            this.panelDetails.Location = new System.Drawing.Point(0, 33);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(883, 165);
            this.panelDetails.TabIndex = 0;
            // 
            // labelTaxGroup
            // 
            appearance99.FontData.BoldAsString = "False";
            appearance99.FontData.Name = "Tahoma";
            this.labelTaxGroup.Appearance = appearance99;
            this.labelTaxGroup.AutoSize = true;
            this.labelTaxGroup.Location = new System.Drawing.Point(452, 94);
            this.labelTaxGroup.Name = "labelTaxGroup";
            this.labelTaxGroup.Size = new System.Drawing.Size(60, 15);
            this.labelTaxGroup.TabIndex = 148;
            this.labelTaxGroup.TabStop = true;
            this.labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.labelTaxGroup.Value = "Tax Group:";
            appearance100.ForeColor = System.Drawing.Color.Blue;
            this.labelTaxGroup.VisitedLinkAppearance = appearance100;
            this.labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel7_LinkClicked);
            // 
            // comboBoxPayeeTaxGroup
            // 
            this.comboBoxPayeeTaxGroup.Assigned = false;
            this.comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPayeeTaxGroup.CustomReportFieldName = "";
            this.comboBoxPayeeTaxGroup.CustomReportKey = "";
            this.comboBoxPayeeTaxGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxPayeeTaxGroup.DescriptionTextBox = null;
            appearance101.BackColor = System.Drawing.SystemColors.Window;
            appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance101;
            this.comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance102.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance102;
            appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance104.BackColor2 = System.Drawing.SystemColors.Control;
            appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
            this.comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance105.BackColor = System.Drawing.SystemColors.Window;
            appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance105;
            appearance106.BackColor = System.Drawing.SystemColors.Highlight;
            appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance106;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance107.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance107;
            appearance108.BorderColor = System.Drawing.Color.Silver;
            appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance108;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
            appearance109.BackColor = System.Drawing.SystemColors.Control;
            appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance109.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance109;
            appearance110.TextHAlignAsString = "Left";
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance110;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance111.BackColor = System.Drawing.SystemColors.Window;
            appearance111.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance111;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPayeeTaxGroup.Editable = true;
            this.comboBoxPayeeTaxGroup.FilterString = "";
            this.comboBoxPayeeTaxGroup.HasAllAccount = false;
            this.comboBoxPayeeTaxGroup.HasCustom = false;
            this.comboBoxPayeeTaxGroup.IsDataLoaded = false;
            this.comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(534, 91);
            this.comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
            this.comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
            this.comboBoxPayeeTaxGroup.ReadOnly = true;
            this.comboBoxPayeeTaxGroup.ShowInactiveItems = false;
            this.comboBoxPayeeTaxGroup.ShowQuickAdd = true;
            this.comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(107, 20);
            this.comboBoxPayeeTaxGroup.TabIndex = 147;
            this.comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkPaymentTerm
            // 
            appearance113.FontData.BoldAsString = "False";
            appearance113.FontData.Name = "Tahoma";
            this.ultraFormattedLinkPaymentTerm.Appearance = appearance113;
            this.ultraFormattedLinkPaymentTerm.AutoSize = true;
            this.ultraFormattedLinkPaymentTerm.Location = new System.Drawing.Point(454, 50);
            this.ultraFormattedLinkPaymentTerm.Name = "ultraFormattedLinkPaymentTerm";
            this.ultraFormattedLinkPaymentTerm.Size = new System.Drawing.Size(79, 15);
            this.ultraFormattedLinkPaymentTerm.TabIndex = 146;
            this.ultraFormattedLinkPaymentTerm.TabStop = true;
            this.ultraFormattedLinkPaymentTerm.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkPaymentTerm.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkPaymentTerm.Value = "Payment Term:";
            appearance114.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkPaymentTerm.VisitedLinkAppearance = appearance114;
            this.ultraFormattedLinkPaymentTerm.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkPaymentTerm_LinkClicked);
            // 
            // ultraFormattedLinkProject
            // 
            appearance115.FontData.BoldAsString = "False";
            appearance115.FontData.Name = "Tahoma";
            this.ultraFormattedLinkProject.Appearance = appearance115;
            this.ultraFormattedLinkProject.AutoSize = true;
            this.ultraFormattedLinkProject.Location = new System.Drawing.Point(230, 142);
            this.ultraFormattedLinkProject.Name = "ultraFormattedLinkProject";
            this.ultraFormattedLinkProject.Size = new System.Drawing.Size(44, 15);
            this.ultraFormattedLinkProject.TabIndex = 145;
            this.ultraFormattedLinkProject.TabStop = true;
            this.ultraFormattedLinkProject.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkProject.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkProject.Value = "Project:";
            appearance116.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkProject.VisitedLinkAppearance = appearance116;
            this.ultraFormattedLinkProject.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkProject_LinkClicked);
            // 
            // ultraFormattedLinkShipping
            // 
            appearance117.FontData.BoldAsString = "False";
            appearance117.FontData.Name = "Tahoma";
            this.ultraFormattedLinkShipping.Appearance = appearance117;
            this.ultraFormattedLinkShipping.AutoSize = true;
            this.ultraFormattedLinkShipping.Location = new System.Drawing.Point(12, 144);
            this.ultraFormattedLinkShipping.Name = "ultraFormattedLinkShipping";
            this.ultraFormattedLinkShipping.Size = new System.Drawing.Size(89, 15);
            this.ultraFormattedLinkShipping.TabIndex = 144;
            this.ultraFormattedLinkShipping.TabStop = true;
            this.ultraFormattedLinkShipping.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkShipping.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkShipping.Value = "Shipping Method:";
            appearance118.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkShipping.VisitedLinkAppearance = appearance118;
            this.ultraFormattedLinkShipping.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkShipping_LinkClicked);
            // 
            // ultraFormattedLinkCostCategory
            // 
            appearance119.FontData.BoldAsString = "False";
            appearance119.FontData.Name = "Tahoma";
            this.ultraFormattedLinkCostCategory.Appearance = appearance119;
            this.ultraFormattedLinkCostCategory.AutoSize = true;
            this.ultraFormattedLinkCostCategory.Location = new System.Drawing.Point(409, 142);
            this.ultraFormattedLinkCostCategory.Name = "ultraFormattedLinkCostCategory";
            this.ultraFormattedLinkCostCategory.Size = new System.Drawing.Size(78, 15);
            this.ultraFormattedLinkCostCategory.TabIndex = 143;
            this.ultraFormattedLinkCostCategory.TabStop = true;
            this.ultraFormattedLinkCostCategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkCostCategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkCostCategory.Value = "Cost Category:";
            appearance120.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkCostCategory.VisitedLinkAppearance = appearance120;
            this.ultraFormattedLinkCostCategory.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkCostCategory_LinkClicked);
            // 
            // ultraFormattedLinkCurrency
            // 
            appearance121.FontData.BoldAsString = "False";
            appearance121.FontData.Name = "Tahoma";
            this.ultraFormattedLinkCurrency.Appearance = appearance121;
            this.ultraFormattedLinkCurrency.AutoSize = true;
            this.ultraFormattedLinkCurrency.Location = new System.Drawing.Point(650, 116);
            this.ultraFormattedLinkCurrency.Name = "ultraFormattedLinkCurrency";
            this.ultraFormattedLinkCurrency.Size = new System.Drawing.Size(54, 15);
            this.ultraFormattedLinkCurrency.TabIndex = 142;
            this.ultraFormattedLinkCurrency.TabStop = true;
            this.ultraFormattedLinkCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkCurrency.Value = "Currency:";
            appearance122.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkCurrency.VisitedLinkAppearance = appearance122;
            this.ultraFormattedLinkCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkCurrency_LinkClicked);
            // 
            // textBoxShipto
            // 
            this.textBoxShipto.BackColor = System.Drawing.Color.White;
            this.textBoxShipto.CustomReportFieldName = "";
            this.textBoxShipto.CustomReportKey = "";
            this.textBoxShipto.CustomReportValueType = ((byte)(1));
            this.textBoxShipto.IsComboTextBox = false;
            this.textBoxShipto.IsModified = false;
            this.textBoxShipto.Location = new System.Drawing.Point(230, 65);
            this.textBoxShipto.MaxLength = 255;
            this.textBoxShipto.Multiline = true;
            this.textBoxShipto.Name = "textBoxShipto";
            this.textBoxShipto.Size = new System.Drawing.Size(215, 72);
            this.textBoxShipto.TabIndex = 26;
            // 
            // comboBoxCostCategory
            // 
            this.comboBoxCostCategory.Assigned = false;
            this.comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCostCategory.CustomReportFieldName = "";
            this.comboBoxCostCategory.CustomReportKey = "";
            this.comboBoxCostCategory.CustomReportValueType = ((byte)(1));
            this.comboBoxCostCategory.DescriptionTextBox = null;
            this.comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCostCategory.Editable = true;
            this.comboBoxCostCategory.FilterString = "";
            this.comboBoxCostCategory.HasAllAccount = false;
            this.comboBoxCostCategory.HasCustom = false;
            this.comboBoxCostCategory.IsDataLoaded = false;
            this.comboBoxCostCategory.Location = new System.Drawing.Point(490, 140);
            this.comboBoxCostCategory.MaxDropDownItems = 12;
            this.comboBoxCostCategory.Name = "comboBoxCostCategory";
            this.comboBoxCostCategory.ShowInactiveItems = false;
            this.comboBoxCostCategory.ShowQuickAdd = true;
            this.comboBoxCostCategory.Size = new System.Drawing.Size(107, 20);
            this.comboBoxCostCategory.TabIndex = 32;
            this.comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxJob
            // 
            this.comboBoxJob.Assigned = false;
            this.comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxJob.CustomReportFieldName = "";
            this.comboBoxJob.CustomReportKey = "";
            this.comboBoxJob.CustomReportValueType = ((byte)(1));
            this.comboBoxJob.DescriptionTextBox = null;
            this.comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxJob.Editable = true;
            this.comboBoxJob.FilterString = "";
            this.comboBoxJob.HasAllAccount = false;
            this.comboBoxJob.HasCustom = false;
            this.comboBoxJob.IsDataLoaded = false;
            this.comboBoxJob.Location = new System.Drawing.Point(278, 140);
            this.comboBoxJob.MaxDropDownItems = 12;
            this.comboBoxJob.Name = "comboBoxJob";
            this.comboBoxJob.ShowInactiveItems = false;
            this.comboBoxJob.ShowQuickAdd = true;
            this.comboBoxJob.Size = new System.Drawing.Size(125, 20);
            this.comboBoxJob.TabIndex = 30;
            this.comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(650, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Reference 2:";
            // 
            // textBoxRef2
            // 
            this.textBoxRef2.Location = new System.Drawing.Point(730, 47);
            this.textBoxRef2.MaxLength = 20;
            this.textBoxRef2.Name = "textBoxRef2";
            this.textBoxRef2.Size = new System.Drawing.Size(142, 20);
            this.textBoxRef2.TabIndex = 16;
            // 
            // buttonSelectDocument
            // 
            this.buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSelectDocument.Location = new System.Drawing.Point(522, 22);
            this.buttonSelectDocument.Name = "buttonSelectDocument";
            this.buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
            this.buttonSelectDocument.TabIndex = 141;
            this.buttonSelectDocument.Text = "...";
            this.buttonSelectDocument.UseVisualStyleBackColor = false;
            this.buttonSelectDocument.Click += new System.EventHandler(this.buttonSelectDocument_Click);
            // 
            // comboBoxShippingMethod
            // 
            this.comboBoxShippingMethod.Assigned = false;
            this.comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShippingMethod.CustomReportFieldName = "";
            this.comboBoxShippingMethod.CustomReportKey = "";
            this.comboBoxShippingMethod.CustomReportValueType = ((byte)(1));
            this.comboBoxShippingMethod.DescriptionTextBox = null;
            appearance123.BackColor = System.Drawing.SystemColors.Window;
            appearance123.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShippingMethod.DisplayLayout.Appearance = appearance123;
            this.comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance124.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance124.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance124;
            appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance125;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance126.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance126.BackColor2 = System.Drawing.SystemColors.Control;
            appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance126;
            this.comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
            appearance127.BackColor = System.Drawing.SystemColors.Window;
            appearance127.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance127;
            appearance128.BackColor = System.Drawing.SystemColors.Highlight;
            appearance128.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance128;
            this.comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance129.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance129;
            appearance130.BorderColor = System.Drawing.Color.Silver;
            appearance130.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance130;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
            appearance131.BackColor = System.Drawing.SystemColors.Control;
            appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance131.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance131.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance131;
            appearance132.TextHAlignAsString = "Left";
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance132;
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance133.BackColor = System.Drawing.SystemColors.Window;
            appearance133.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance133;
            this.comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance134.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance134;
            this.comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShippingMethod.Editable = true;
            this.comboBoxShippingMethod.FilterString = "";
            this.comboBoxShippingMethod.HasAllAccount = false;
            this.comboBoxShippingMethod.HasCustom = false;
            this.comboBoxShippingMethod.IsDataLoaded = false;
            this.comboBoxShippingMethod.Location = new System.Drawing.Point(106, 140);
            this.comboBoxShippingMethod.MaxDropDownItems = 12;
            this.comboBoxShippingMethod.Name = "comboBoxShippingMethod";
            this.comboBoxShippingMethod.ShowInactiveItems = false;
            this.comboBoxShippingMethod.ShowQuickAdd = true;
            this.comboBoxShippingMethod.Size = new System.Drawing.Size(117, 20);
            this.comboBoxShippingMethod.TabIndex = 28;
            this.comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxTerm
            // 
            this.comboBoxTerm.Assigned = false;
            this.comboBoxTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxTerm.CustomReportFieldName = "";
            this.comboBoxTerm.CustomReportKey = "";
            this.comboBoxTerm.CustomReportValueType = ((byte)(1));
            this.comboBoxTerm.DescriptionTextBox = null;
            appearance135.BackColor = System.Drawing.SystemColors.Window;
            appearance135.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxTerm.DisplayLayout.Appearance = appearance135;
            this.comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance136.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance136.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance136.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance136;
            appearance137.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance137;
            this.comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance138.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance138.BackColor2 = System.Drawing.SystemColors.Control;
            appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance138;
            this.comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
            appearance139.BackColor = System.Drawing.SystemColors.Window;
            appearance139.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance139;
            appearance140.BackColor = System.Drawing.SystemColors.Highlight;
            appearance140.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance140;
            this.comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance141.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance141;
            appearance142.BorderColor = System.Drawing.Color.Silver;
            appearance142.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance142;
            this.comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
            appearance143.BackColor = System.Drawing.SystemColors.Control;
            appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance143.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance143.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance143;
            appearance144.TextHAlignAsString = "Left";
            this.comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance144;
            this.comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance145.BackColor = System.Drawing.SystemColors.Window;
            appearance145.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance145;
            this.comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance146.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance146;
            this.comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxTerm.Editable = true;
            this.comboBoxTerm.FilterString = "";
            this.comboBoxTerm.HasAllAccount = false;
            this.comboBoxTerm.HasCustom = false;
            this.comboBoxTerm.IsDataLoaded = false;
            this.comboBoxTerm.Location = new System.Drawing.Point(534, 47);
            this.comboBoxTerm.MaxDropDownItems = 12;
            this.comboBoxTerm.Name = "comboBoxTerm";
            this.comboBoxTerm.ShowInactiveItems = false;
            this.comboBoxTerm.ShowQuickAdd = true;
            this.comboBoxTerm.Size = new System.Drawing.Size(107, 20);
            this.comboBoxTerm.TabIndex = 8;
            this.comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxSalesperson
            // 
            this.comboBoxSalesperson.Assigned = false;
            this.comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSalesperson.CustomReportFieldName = "";
            this.comboBoxSalesperson.CustomReportKey = "";
            this.comboBoxSalesperson.CustomReportValueType = ((byte)(1));
            this.comboBoxSalesperson.DescriptionTextBox = null;
            appearance147.BackColor = System.Drawing.SystemColors.Window;
            appearance147.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSalesperson.DisplayLayout.Appearance = appearance147;
            this.comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance148.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance148.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance148.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance148;
            appearance149.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance149;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance150.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance150.BackColor2 = System.Drawing.SystemColors.Control;
            appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance150.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance150;
            this.comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
            appearance151.BackColor = System.Drawing.SystemColors.Window;
            appearance151.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance151;
            appearance152.BackColor = System.Drawing.SystemColors.Highlight;
            appearance152.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance152;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance153.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance153;
            appearance154.BorderColor = System.Drawing.Color.Silver;
            appearance154.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance154;
            this.comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
            appearance155.BackColor = System.Drawing.SystemColors.Control;
            appearance155.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance155.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance155.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance155.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance155;
            appearance156.TextHAlignAsString = "Left";
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance156;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance157.BackColor = System.Drawing.SystemColors.Window;
            appearance157.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance157;
            this.comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance158.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance158;
            this.comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSalesperson.Editable = true;
            this.comboBoxSalesperson.FilterString = "";
            this.comboBoxSalesperson.HasAllAccount = false;
            this.comboBoxSalesperson.HasCustom = false;
            this.comboBoxSalesperson.IsDataLoaded = false;
            this.comboBoxSalesperson.Location = new System.Drawing.Point(730, 91);
            this.comboBoxSalesperson.MaxDropDownItems = 12;
            this.comboBoxSalesperson.Name = "comboBoxSalesperson";
            this.comboBoxSalesperson.ShowInactiveItems = false;
            this.comboBoxSalesperson.ShowQuickAdd = true;
            this.comboBoxSalesperson.Size = new System.Drawing.Size(142, 20);
            this.comboBoxSalesperson.TabIndex = 20;
            this.comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxSalesperson.SelectedIndexChanged += new System.EventHandler(this.comboBoxSalesperson_SelectedIndexChanged);
            // 
            // ultraFormattedLinkLabel6
            // 
            appearance159.FontData.BoldAsString = "False";
            appearance159.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel6.Appearance = appearance159;
            this.ultraFormattedLinkLabel6.AutoSize = true;
            this.ultraFormattedLinkLabel6.Location = new System.Drawing.Point(650, 94);
            this.ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
            this.ultraFormattedLinkLabel6.Size = new System.Drawing.Size(68, 15);
            this.ultraFormattedLinkLabel6.TabIndex = 19;
            this.ultraFormattedLinkLabel6.TabStop = true;
            this.ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel6.Value = "Salesperson:";
            appearance160.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance160;
            this.ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel6_LinkClicked);
            // 
            // ultraFormattedLinkLabel3
            // 
            appearance161.FontData.BoldAsString = "False";
            appearance161.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel3.Appearance = appearance161;
            this.ultraFormattedLinkLabel3.AutoSize = true;
            this.ultraFormattedLinkLabel3.Location = new System.Drawing.Point(232, 47);
            this.ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
            this.ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
            this.ultraFormattedLinkLabel3.TabIndex = 14;
            this.ultraFormattedLinkLabel3.TabStop = true;
            this.ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel3.Value = "Ship To:";
            appearance162.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance162;
            this.ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel3_LinkClicked);
            // 
            // comboBoxBillingAddress
            // 
            this.comboBoxBillingAddress.Assigned = false;
            this.comboBoxBillingAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxBillingAddress.CustomReportFieldName = "";
            this.comboBoxBillingAddress.CustomReportKey = "";
            this.comboBoxBillingAddress.CustomReportValueType = ((byte)(1));
            this.comboBoxBillingAddress.DescriptionTextBox = null;
            appearance163.BackColor = System.Drawing.SystemColors.Window;
            appearance163.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxBillingAddress.DisplayLayout.Appearance = appearance163;
            this.comboBoxBillingAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxBillingAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance164.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance164.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance164.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance164.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxBillingAddress.DisplayLayout.GroupByBox.Appearance = appearance164;
            appearance165.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxBillingAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance165;
            this.comboBoxBillingAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance166.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance166.BackColor2 = System.Drawing.SystemColors.Control;
            appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance166.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxBillingAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance166;
            this.comboBoxBillingAddress.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxBillingAddress.DisplayLayout.MaxRowScrollRegions = 1;
            appearance167.BackColor = System.Drawing.SystemColors.Window;
            appearance167.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxBillingAddress.DisplayLayout.Override.ActiveCellAppearance = appearance167;
            appearance168.BackColor = System.Drawing.SystemColors.Highlight;
            appearance168.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxBillingAddress.DisplayLayout.Override.ActiveRowAppearance = appearance168;
            this.comboBoxBillingAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxBillingAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance169.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxBillingAddress.DisplayLayout.Override.CardAreaAppearance = appearance169;
            appearance170.BorderColor = System.Drawing.Color.Silver;
            appearance170.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxBillingAddress.DisplayLayout.Override.CellAppearance = appearance170;
            this.comboBoxBillingAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxBillingAddress.DisplayLayout.Override.CellPadding = 0;
            appearance171.BackColor = System.Drawing.SystemColors.Control;
            appearance171.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance171.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance171.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance171.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxBillingAddress.DisplayLayout.Override.GroupByRowAppearance = appearance171;
            appearance172.TextHAlignAsString = "Left";
            this.comboBoxBillingAddress.DisplayLayout.Override.HeaderAppearance = appearance172;
            this.comboBoxBillingAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxBillingAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance173.BackColor = System.Drawing.SystemColors.Window;
            appearance173.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxBillingAddress.DisplayLayout.Override.RowAppearance = appearance173;
            this.comboBoxBillingAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance174.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxBillingAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance174;
            this.comboBoxBillingAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxBillingAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxBillingAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxBillingAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxBillingAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxBillingAddress.Editable = true;
            this.comboBoxBillingAddress.FilterString = "";
            this.comboBoxBillingAddress.HasAllAccount = false;
            this.comboBoxBillingAddress.HasCustom = false;
            this.comboBoxBillingAddress.IsDataLoaded = false;
            this.comboBoxBillingAddress.Location = new System.Drawing.Point(99, 44);
            this.comboBoxBillingAddress.MaxDropDownItems = 12;
            this.comboBoxBillingAddress.Name = "comboBoxBillingAddress";
            this.comboBoxBillingAddress.ShowInactiveItems = false;
            this.comboBoxBillingAddress.ShowQuickAdd = true;
            this.comboBoxBillingAddress.Size = new System.Drawing.Size(128, 20);
            this.comboBoxBillingAddress.TabIndex = 23;
            this.comboBoxBillingAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxShippingAddressID
            // 
            this.comboBoxShippingAddressID.Assigned = false;
            this.comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShippingAddressID.CustomReportFieldName = "";
            this.comboBoxShippingAddressID.CustomReportKey = "";
            this.comboBoxShippingAddressID.CustomReportValueType = ((byte)(1));
            this.comboBoxShippingAddressID.DescriptionTextBox = null;
            appearance175.BackColor = System.Drawing.SystemColors.Window;
            appearance175.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShippingAddressID.DisplayLayout.Appearance = appearance175;
            this.comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance176.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance176.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance176.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance176.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance176;
            appearance177.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance177;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance178.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance178.BackColor2 = System.Drawing.SystemColors.Control;
            appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance178.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance178;
            this.comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
            appearance179.BackColor = System.Drawing.SystemColors.Window;
            appearance179.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance179;
            appearance180.BackColor = System.Drawing.SystemColors.Highlight;
            appearance180.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance180;
            this.comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance181.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance181;
            appearance182.BorderColor = System.Drawing.Color.Silver;
            appearance182.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance182;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
            appearance183.BackColor = System.Drawing.SystemColors.Control;
            appearance183.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance183.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance183.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance183.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance183;
            appearance184.TextHAlignAsString = "Left";
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance184;
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance185.BackColor = System.Drawing.SystemColors.Window;
            appearance185.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance185;
            this.comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance186.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance186;
            this.comboBoxShippingAddressID.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShippingAddressID.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShippingAddressID.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShippingAddressID.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShippingAddressID.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxShippingAddressID.Editable = true;
            this.comboBoxShippingAddressID.FilterString = "";
            this.comboBoxShippingAddressID.HasAllAccount = false;
            this.comboBoxShippingAddressID.HasCustom = false;
            this.comboBoxShippingAddressID.IsDataLoaded = false;
            this.comboBoxShippingAddressID.Location = new System.Drawing.Point(305, 45);
            this.comboBoxShippingAddressID.MaxDropDownItems = 12;
            this.comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
            this.comboBoxShippingAddressID.ShowInactiveItems = false;
            this.comboBoxShippingAddressID.ShowQuickAdd = true;
            this.comboBoxShippingAddressID.Size = new System.Drawing.Size(140, 20);
            this.comboBoxShippingAddressID.TabIndex = 24;
            this.comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel1
            // 
            appearance187.FontData.BoldAsString = "False";
            appearance187.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel1.Appearance = appearance187;
            this.ultraFormattedLinkLabel1.AutoSize = true;
            this.ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 45);
            this.ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
            this.ultraFormattedLinkLabel1.Size = new System.Drawing.Size(37, 15);
            this.ultraFormattedLinkLabel1.TabIndex = 134;
            this.ultraFormattedLinkLabel1.TabStop = true;
            this.ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel1.Value = "Bill To:";
            appearance188.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance188;
            this.ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(650, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Customer PO#:";
            // 
            // textBoxPONumber
            // 
            this.textBoxPONumber.Location = new System.Drawing.Point(730, 69);
            this.textBoxPONumber.MaxLength = 30;
            this.textBoxPONumber.Name = "textBoxPONumber";
            this.textBoxPONumber.Size = new System.Drawing.Size(142, 20);
            this.textBoxPONumber.TabIndex = 18;
            // 
            // textBoxBilltoAddress
            // 
            this.textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
            this.textBoxBilltoAddress.CustomReportFieldName = "";
            this.textBoxBilltoAddress.CustomReportKey = "";
            this.textBoxBilltoAddress.CustomReportValueType = ((byte)(1));
            this.textBoxBilltoAddress.IsComboTextBox = false;
            this.textBoxBilltoAddress.IsModified = false;
            this.textBoxBilltoAddress.Location = new System.Drawing.Point(12, 64);
            this.textBoxBilltoAddress.MaxLength = 255;
            this.textBoxBilltoAddress.Multiline = true;
            this.textBoxBilltoAddress.Name = "textBoxBilltoAddress";
            this.textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
            this.textBoxBilltoAddress.TabIndex = 25;
            // 
            // ultraFormattedLinkLabel4
            // 
            appearance189.FontData.BoldAsString = "True";
            appearance189.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel4.Appearance = appearance189;
            this.ultraFormattedLinkLabel4.AutoSize = true;
            this.ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
            this.ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
            this.ultraFormattedLinkLabel4.Size = new System.Drawing.Size(80, 15);
            this.ultraFormattedLinkLabel4.TabIndex = 3;
            this.ultraFormattedLinkLabel4.TabStop = true;
            this.ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel4.Value = "Customer ID:";
            appearance190.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance190;
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
            appearance191.BackColor = System.Drawing.SystemColors.Window;
            appearance191.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCustomer.DisplayLayout.Appearance = appearance191;
            this.comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance192.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance192.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance192;
            appearance193.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance193;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance194.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance194.BackColor2 = System.Drawing.SystemColors.Control;
            appearance194.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance194.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance194;
            this.comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
            appearance195.BackColor = System.Drawing.SystemColors.Window;
            appearance195.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance195;
            appearance196.BackColor = System.Drawing.SystemColors.Highlight;
            appearance196.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance196;
            this.comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance197.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance197;
            appearance198.BorderColor = System.Drawing.Color.Silver;
            appearance198.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance198;
            this.comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
            appearance199.BackColor = System.Drawing.SystemColors.Control;
            appearance199.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance199.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance199.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance199;
            appearance200.TextHAlignAsString = "Left";
            this.comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance200;
            this.comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance201.BackColor = System.Drawing.SystemColors.Window;
            appearance201.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance201;
            this.comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance202.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance202;
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
            this.comboBoxCustomer.TabIndex = 4;
            this.comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel5
            // 
            appearance203.FontData.BoldAsString = "True";
            appearance203.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel5.Appearance = appearance203;
            this.ultraFormattedLinkLabel5.AutoSize = true;
            this.ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
            this.ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
            this.ultraFormattedLinkLabel5.Size = new System.Drawing.Size(46, 15);
            this.ultraFormattedLinkLabel5.TabIndex = 0;
            this.ultraFormattedLinkLabel5.TabStop = true;
            this.ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel5.Value = "Doc ID:";
            appearance204.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance204;
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
            appearance205.BackColor = System.Drawing.SystemColors.Window;
            appearance205.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSysDoc.DisplayLayout.Appearance = appearance205;
            this.comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance206.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance206.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance206.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance206.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance206;
            appearance207.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance207;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance208.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance208.BackColor2 = System.Drawing.SystemColors.Control;
            appearance208.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance208.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance208;
            this.comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
            appearance209.BackColor = System.Drawing.SystemColors.Window;
            appearance209.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance209;
            appearance210.BackColor = System.Drawing.SystemColors.Highlight;
            appearance210.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance210;
            this.comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance211.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance211;
            appearance212.BorderColor = System.Drawing.Color.Silver;
            appearance212.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance212;
            this.comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
            appearance213.BackColor = System.Drawing.SystemColors.Control;
            appearance213.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance213.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance213.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance213.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance213;
            appearance214.TextHAlignAsString = "Left";
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance214;
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance215.BackColor = System.Drawing.SystemColors.Window;
            appearance215.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance215;
            this.comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance216.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance216;
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
            this.comboBoxSysDoc.TabIndex = 1;
            this.comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel2
            // 
            this.mmLabel2.AutoSize = true;
            this.mmLabel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel2.IsFieldHeader = false;
            this.mmLabel2.IsRequired = true;
            this.mmLabel2.Location = new System.Drawing.Point(451, 73);
            this.mmLabel2.Name = "mmLabel2";
            this.mmLabel2.PenWidth = 1F;
            this.mmLabel2.ShowBorder = false;
            this.mmLabel2.Size = new System.Drawing.Size(65, 13);
            this.mmLabel2.TabIndex = 7;
            this.mmLabel2.Text = "Due Date:";
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = true;
            this.mmLabel1.Location = new System.Drawing.Point(650, 7);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(38, 13);
            this.mmLabel1.TabIndex = 11;
            this.mmLabel1.Text = "Date:";
            // 
            // textBoxCustomerName
            // 
            this.textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxCustomerName.Location = new System.Drawing.Point(210, 23);
            this.textBoxCustomerName.MaxLength = 64;
            this.textBoxCustomerName.Name = "textBoxCustomerName";
            this.textBoxCustomerName.ReadOnly = true;
            this.textBoxCustomerName.Size = new System.Drawing.Size(342, 20);
            this.textBoxCustomerName.TabIndex = 5;
            this.textBoxCustomerName.TabStop = false;
            // 
            // dateTimePickerDueDate
            // 
            this.dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDueDate.Location = new System.Drawing.Point(534, 69);
            this.dateTimePickerDueDate.Name = "dateTimePickerDueDate";
            this.dateTimePickerDueDate.Size = new System.Drawing.Size(107, 20);
            this.dateTimePickerDueDate.TabIndex = 9;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCurrency.Location = new System.Drawing.Point(730, 113);
            this.comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
            this.comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Rate = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.comboBoxCurrency.SelectedID = "";
            this.comboBoxCurrency.Size = new System.Drawing.Size(142, 20);
            this.comboBoxCurrency.TabIndex = 22;
            // 
            // labelReportTo
            // 
            this.labelReportTo.AutoSize = true;
            this.labelReportTo.Location = new System.Drawing.Point(765, 94);
            this.labelReportTo.Name = "labelReportTo";
            this.labelReportTo.Size = new System.Drawing.Size(52, 13);
            this.labelReportTo.TabIndex = 149;
            this.labelReportTo.Text = "ReportTo";
            this.labelReportTo.Visible = false;
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
            // labelSelectedDocs
            // 
            this.labelSelectedDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedDocs.AutoSize = true;
            this.labelSelectedDocs.Location = new System.Drawing.Point(362, 486);
            this.labelSelectedDocs.Name = "labelSelectedDocs";
            this.labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
            this.labelSelectedDocs.TabIndex = 126;
            this.labelSelectedDocs.Text = "Selected Documents:";
            this.labelSelectedDocs.Visible = false;
            // 
            // checkedListBoxDN
            // 
            this.checkedListBoxDN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBoxDN.FormattingEnabled = true;
            this.checkedListBoxDN.Location = new System.Drawing.Point(365, 507);
            this.checkedListBoxDN.Name = "checkedListBoxDN";
            this.checkedListBoxDN.Size = new System.Drawing.Size(152, 82);
            this.checkedListBoxDN.TabIndex = 3;
            this.checkedListBoxDN.DoubleClick += new System.EventHandler(this.checkedListBoxDN_DoubleClick);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.tabPageItems);
            this.ultraTabControl1.Controls.Add(this.tabPageExpense);
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 200);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(981, 277);
            this.ultraTabControl1.TabIndex = 1;
            this.ultraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
            ultraTab.TabPage = this.tabPageItems;
            ultraTab.Text = "Items";
            ultraTab2.TabPage = this.tabPageExpense;
            ultraTab2.Text = "Charges";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab,
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(955, 273);
            // 
            // ultraToolTipManager1
            // 
            this.ultraToolTipManager1.ContainingControl = this;
            // 
            // checkBoxPriceIncludeTax
            // 
            this.checkBoxPriceIncludeTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPriceIncludeTax.AutoSize = true;
            this.checkBoxPriceIncludeTax.Location = new System.Drawing.Point(603, 483);
            this.checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
            this.checkBoxPriceIncludeTax.Size = new System.Drawing.Size(123, 17);
            this.checkBoxPriceIncludeTax.TabIndex = 4;
            this.checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
            this.checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
            this.checkBoxPriceIncludeTax.Visible = false;
            this.checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(this.checkBoxPriceIncludeTax_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 133;
            this.label5.Text = "Subtotal:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 148;
            this.label11.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 164;
            this.label7.Text = "Discount:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxTotalExpense);
            this.panel1.Controls.Add(this.textBoxDiscountPercent);
            this.panel1.Controls.Add(this.textBoxDiscountAmount);
            this.panel1.Controls.Add(this.panelNonTax);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxSubtotal);
            this.panel1.Location = new System.Drawing.Point(761, 483);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 134);
            this.panel1.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(4, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 15);
            this.label14.TabIndex = 162;
            this.label14.Text = "Charges:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxTotalExpense
            // 
            this.textBoxTotalExpense.AllowDecimal = true;
            this.textBoxTotalExpense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTotalExpense.CustomReportFieldName = "";
            this.textBoxTotalExpense.CustomReportKey = "";
            this.textBoxTotalExpense.CustomReportValueType = ((byte)(1));
            this.textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
            this.textBoxTotalExpense.IsComboTextBox = false;
            this.textBoxTotalExpense.IsModified = false;
            this.textBoxTotalExpense.Location = new System.Drawing.Point(81, 42);
            this.textBoxTotalExpense.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxTotalExpense.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxTotalExpense.Name = "textBoxTotalExpense";
            this.textBoxTotalExpense.NullText = "0";
            this.textBoxTotalExpense.ReadOnly = true;
            this.textBoxTotalExpense.Size = new System.Drawing.Size(137, 20);
            this.textBoxTotalExpense.TabIndex = 3;
            this.textBoxTotalExpense.TabStop = false;
            this.textBoxTotalExpense.Text = "0.00";
            this.textBoxTotalExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTotalExpense.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // textBoxDiscountPercent
            // 
            this.textBoxDiscountPercent.CustomReportFieldName = "";
            this.textBoxDiscountPercent.CustomReportKey = "";
            this.textBoxDiscountPercent.CustomReportValueType = ((byte)(1));
            this.textBoxDiscountPercent.IsComboTextBox = false;
            this.textBoxDiscountPercent.IsModified = false;
            this.textBoxDiscountPercent.Location = new System.Drawing.Point(81, 21);
            this.textBoxDiscountPercent.MaxLength = 4;
            this.textBoxDiscountPercent.Name = "textBoxDiscountPercent";
            this.textBoxDiscountPercent.Size = new System.Drawing.Size(34, 20);
            this.textBoxDiscountPercent.TabIndex = 1;
            this.textBoxDiscountPercent.Text = "0";
            this.textBoxDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDiscountPercent.TextChanged += new System.EventHandler(this.textBoxDiscountPercent_TextChanged);
            this.textBoxDiscountPercent.Leave += new System.EventHandler(this.textBoxDiscountPercent_Leave);
            // 
            // textBoxDiscountAmount
            // 
            this.textBoxDiscountAmount.AllowDecimal = true;
            this.textBoxDiscountAmount.CustomReportFieldName = "";
            this.textBoxDiscountAmount.CustomReportKey = "";
            this.textBoxDiscountAmount.CustomReportValueType = ((byte)(1));
            this.textBoxDiscountAmount.IsComboTextBox = false;
            this.textBoxDiscountAmount.IsModified = false;
            this.textBoxDiscountAmount.Location = new System.Drawing.Point(131, 21);
            this.textBoxDiscountAmount.MaxLength = 15;
            this.textBoxDiscountAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxDiscountAmount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxDiscountAmount.Name = "textBoxDiscountAmount";
            this.textBoxDiscountAmount.NullText = "0";
            this.textBoxDiscountAmount.Size = new System.Drawing.Size(87, 20);
            this.textBoxDiscountAmount.TabIndex = 2;
            this.textBoxDiscountAmount.Text = "0.00";
            this.textBoxDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDiscountAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.textBoxDiscountAmount.TextChanged += new System.EventHandler(this.textBoxDiscountAmount_TextChanged);
            this.textBoxDiscountAmount.Leave += new System.EventHandler(this.textBoxDiscountAmount_Leave);
            // 
            // panelNonTax
            // 
            this.panelNonTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNonTax.Controls.Add(this.linkLabelTax);
            this.panelNonTax.Controls.Add(this.textBoxTaxAmount);
            this.panelNonTax.Location = new System.Drawing.Point(0, 63);
            this.panelNonTax.Name = "panelNonTax";
            this.panelNonTax.Size = new System.Drawing.Size(218, 25);
            this.panelNonTax.TabIndex = 4;
            // 
            // linkLabelTax
            // 
            appearance217.FontData.BoldAsString = "False";
            appearance217.FontData.Name = "Tahoma";
            this.linkLabelTax.Appearance = appearance217;
            this.linkLabelTax.AutoSize = true;
            this.linkLabelTax.Location = new System.Drawing.Point(4, 5);
            this.linkLabelTax.Name = "linkLabelTax";
            this.linkLabelTax.Size = new System.Drawing.Size(28, 15);
            this.linkLabelTax.TabIndex = 164;
            this.linkLabelTax.TabStop = true;
            this.linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkLabelTax.Value = "Tax:";
            appearance218.ForeColor = System.Drawing.Color.Blue;
            this.linkLabelTax.VisitedLinkAppearance = appearance218;
            // 
            // textBoxTaxAmount
            // 
            this.textBoxTaxAmount.AllowDecimal = true;
            this.textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTaxAmount.CustomReportFieldName = "";
            this.textBoxTaxAmount.CustomReportKey = "";
            this.textBoxTaxAmount.CustomReportValueType = ((byte)(1));
            this.textBoxTaxAmount.IsComboTextBox = false;
            this.textBoxTaxAmount.IsModified = false;
            this.textBoxTaxAmount.Location = new System.Drawing.Point(81, 2);
            this.textBoxTaxAmount.MaxLength = 15;
            this.textBoxTaxAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxTaxAmount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxTaxAmount.Name = "textBoxTaxAmount";
            this.textBoxTaxAmount.NullText = "0";
            this.textBoxTaxAmount.ReadOnly = true;
            this.textBoxTaxAmount.Size = new System.Drawing.Size(137, 20);
            this.textBoxTaxAmount.TabIndex = 0;
            this.textBoxTaxAmount.Text = "0.00";
            this.textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTaxAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // textBoxSubtotal
            // 
            this.textBoxSubtotal.AllowDecimal = true;
            this.textBoxSubtotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxSubtotal.CustomReportFieldName = "";
            this.textBoxSubtotal.CustomReportKey = "";
            this.textBoxSubtotal.CustomReportValueType = ((byte)(1));
            this.textBoxSubtotal.ForeColor = System.Drawing.Color.Black;
            this.textBoxSubtotal.IsComboTextBox = false;
            this.textBoxSubtotal.IsModified = false;
            this.textBoxSubtotal.Location = new System.Drawing.Point(81, -1);
            this.textBoxSubtotal.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxSubtotal.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxSubtotal.Name = "textBoxSubtotal";
            this.textBoxSubtotal.NullText = "0";
            this.textBoxSubtotal.ReadOnly = true;
            this.textBoxSubtotal.Size = new System.Drawing.Size(137, 20);
            this.textBoxSubtotal.TabIndex = 0;
            this.textBoxSubtotal.TabStop = false;
            this.textBoxSubtotal.Text = "0.00";
            this.textBoxSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSubtotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // panelTotal
            // 
            this.panelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotal.Controls.Add(this.label9);
            this.panelTotal.Controls.Add(this.textBoxRoundOff);
            this.panelTotal.Controls.Add(this.textBoxTotal);
            this.panelTotal.Controls.Add(this.label8);
            this.panelTotal.Location = new System.Drawing.Point(761, 570);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(220, 47);
            this.panelTotal.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(4, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 174;
            this.label9.Text = "Round Off:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxRoundOff
            // 
            this.textBoxRoundOff.AllowDecimal = true;
            this.textBoxRoundOff.CustomReportFieldName = "";
            this.textBoxRoundOff.CustomReportKey = "";
            this.textBoxRoundOff.CustomReportValueType = ((byte)(1));
            this.textBoxRoundOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRoundOff.IsComboTextBox = false;
            this.textBoxRoundOff.IsModified = false;
            this.textBoxRoundOff.Location = new System.Drawing.Point(81, 2);
            this.textBoxRoundOff.MaxLength = 15;
            this.textBoxRoundOff.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxRoundOff.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxRoundOff.Name = "textBoxRoundOff";
            this.textBoxRoundOff.NullText = "0";
            this.textBoxRoundOff.Size = new System.Drawing.Size(137, 20);
            this.textBoxRoundOff.TabIndex = 0;
            this.textBoxRoundOff.Text = "0.00";
            this.textBoxRoundOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxRoundOff.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.textBoxRoundOff.Leave += new System.EventHandler(this.textBoxRoundOff_Leave);
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.AllowDecimal = true;
            this.textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTotal.CustomReportFieldName = "";
            this.textBoxTotal.CustomReportKey = "";
            this.textBoxTotal.CustomReportValueType = ((byte)(1));
            this.textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotal.IsComboTextBox = false;
            this.textBoxTotal.IsModified = false;
            this.textBoxTotal.Location = new System.Drawing.Point(81, 23);
            this.textBoxTotal.MaxLength = 15;
            this.textBoxTotal.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxTotal.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.NullText = "0";
            this.textBoxTotal.ReadOnly = true;
            this.textBoxTotal.Size = new System.Drawing.Size(137, 20);
            this.textBoxTotal.TabIndex = 1;
            this.textBoxTotal.Text = "0.00";
            this.textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTotal.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 172;
            this.label8.Text = "Total:";
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
            // SalesInvoiceForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(993, 659);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.checkBoxPriceIncludeTax);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSelectedDocs);
            this.Controls.Add(this.checkedListBoxDN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(902, 521);
            this.Name = "SalesInvoiceForm";
            this.Text = "Sales Invoice";
            this.toolTipController1.SetToolTipIconType(this, DevExpress.Utils.ToolTipIconType.Information);
            this.tabPageItems.ResumeLayout(false);
            this.tabPageItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridItems)).EndInit();
            this.groupBoxTax.ResumeLayout(false);
            this.groupBoxTax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxitemcostCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxitemJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxJob1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSpecification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxStyle)).EndInit();
            this.tabPageExpense.ResumeLayout(false);
            this.tabPageExpense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridExpenseCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGridCurrency)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayeeTaxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCostCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxBillingAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingAddressID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSysDoc)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelNonTax.ResumeLayout(false);
            this.panelNonTax.PerformLayout();
            this.panelTotal.ResumeLayout(false);
            this.panelTotal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
