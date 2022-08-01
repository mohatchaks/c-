using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraRichEdit;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerDetailsForm : Form, IForm
	{
		private CustomerData currentData;

		private const string TABLENAME_CONST = "Customer";

		private const string IDFIELD_CONST = "CustomerID";

		private bool isNewRecord = true;

		private string ARAccountID = "";

		private bool disableCustomerCreditLimit = CompanyPreferences.DisableCustomerCreditLimit;

		private MMTextBox textBoxName;

		private CheckBox checkBoxIsInactive;

		private MMTextBox textBoxCode;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton buttonClose;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private MMTextBox textBoxFormalName;

		private FormManager formManager;

		private MMTextBox textBoxForeignName;

		private CheckBox checkBoxHold;

		private Panel panel1;

		private MMTextBox textBoxAddressID;

		private MMTextBox textBoxPostalCode;

		private MMTextBox textBoxEmail;

		private MMTextBox textBoxMobile;

		private MMTextBox textBoxFax;

		private MMTextBox textBoxPhone2;

		private MMTextBox textBoxPhone1;

		private MMTextBox textBoxCountry;

		private MMTextBox textBoxState;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMTextBox textBoxAddress1;

		private MMTextBox textBoxContactName;

		private XPButton buttonMoreAddress;

		private MMTextBox textBoxWebsite;

		private MMTextBox textBoxDepartment;

		private MMTextBox textBoxComment;

		private MMTextBox textBoxBankAccountNumber;

		private MMTextBox textBoxBankBranch;

		private MMTextBox textBoxBankName;

		private DataEntryGrid dataGridContacts;

		private customersFlatComboBox comboBoxParentCustomer;

		private CustomerClassComboBox comboBoxCustomerClass;

		private CountryComboBox comboBoxCountry;

		private AreaComboBox comboBoxArea;

		private PriceLevelComboBox comboBoxPriceLevel;

		private ShippingMethodsComboBox comboBoxShippingMethods;

		private CustomerAddressComboBox comboBoxShiptoAddress;

		private CustomerAddressComboBox comboBoxBilltoAddress;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel labelCustomerNameHeader;

		private MMSDateTimePicker dateTimePickerEstablished;

		private MMSDateTimePicker dateTimePickerCustomerSince;

		private XPButton buttonCategories;

		private ContextMenuStrip contextMenuStripContact;

		private ToolStripMenuItem openContactToolStripMenuItem;

		private ContactsComboBox gridComboBoxContact;

		private UDFEntryControl udfEntryGrid;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private MMTextBox textBoxNote;

		private ToolStripMenuItem newContactToolStripMenuItem;

		private ToolStripMenuItem deleteContactToolStripMenuItem;

		private ToolStripMenuItem deleteRowToolStripMenuItem;

		private PercentTextBox textBoxConsignCommission;

		private CheckBox checkBoxAllowConsignment;

		private CheckBox checkBoxWeightInvoice;

		private RadioButton radioButtonSublimit;

		private CheckBox checkBoxAcceptPDC;

		private AmountTextBox textBoxCreditLimit;

		private RadioButton radioButtonCreditLimitNoCredit;

		private RadioButton radioButtonCreditLimitUnlimited;

		private RadioButton radioButtonCreditLimitAmount;

		private UserComboBox comboBoxCollectionUser;

		private MMSDateTimePicker dateTimePickerReviewDate;

		private MMTextBox textBoxPaymentTermName;

		private MMTextBox textBoxPaymentMethodName;

		private UltraComboEditor comboBoxRating;

		private PaymentTermComboBox comboBoxPaymentTerms;

		private paymentMethodsComboBox comboBoxPaymentMethods;

		private MMTextBox textBoxInsuranceRemarks;

		private MMTextBox textBoxInsuranceNumber;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private AmountTextBox textBoxInsuranceReqAmount;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private ComboBox comboBoxInsuranceStatus;

		private CheckBox checkBoxAcceptCheque;

		private GenericListComboBox comboBoxLeadSource;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonInformation;

		private MMSDateTimePicker dateTimePickerLicenseExpDate;

		private MMSDateTimePicker dateTimePickerContractExpDate;

		private Button buttonAddActivity;

		private GadgetDateRangeComboBox comboBoxActivityPeriod;

		private DataGridList dataGridActivities;

		private UserComboBox comboBoxCreditReviewBy;

		private UserComboBox comboBoxRatingBy;

		private MMSDateTimePicker dateTimePickerRatingDate;

		private UltraComboEditor comboBoxInsuranceRating;

		private ComboBox comboBoxStatementMethod;

		private MMTextBox textBoxStatementEmail;

		private RichEditControl textBoxProfileDetails;

		private MMTextBox textBoxTempLimit;

		private ToolStripButton toolStripButtonComments;

		private MMTextBox textBoxRatingRemarks;

		private MMTextBox textBoxDeliveryInstructions;

		private MMTextBox textBoxAccountInstructions;

		private MMTextBox textBoxInsuranceID;

		private InsuranceProviderComboBox comboBoxInsuranceProvider;

		private MMTextBox textBoxProvider;

		private MMSDateTimePicker dateTimePickerValidTo;

		private MMSDateTimePicker datetimePickerEffectiveDate;

		private CheckBox checkBoxparentACforposting;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraPictureBox ultraPictureBox1;

		private MMTextBox textBoxLongitude;

		private MMTextBox textBoxLatitude;

		private CustomerGroupComboBox comboBoxCustomerGroup;

		private ComboBox comboBoxTaxOption;

		private MMTextBox textBoxTaxIDNumber;

		private TaxGroupComboBox comboBoxTaxGroup;

		private AmountTextBox textBoxUnsecuredLimit;

		private CheckBox checkBoxUnsecuredLimit;

		private MMSDateTimePicker dateTimePickerCLValidity;

		private XPButton buttonAccounts;

		private MMSDateTimePicker dateTimeBalanceConfirmationDate;

		private NumberTextBox textBoxConfirmationLevel;

		private PercentTextBox textBoxDiscountPercent;

		private PercentTextBox textBoxRebatePercent;

		private ImageList imageListComments;

		private XPButton buttonCustomerInsuranceClaim;

		private ToolStripButton toolStripButtonHistory;

		private NumberTextBox textBoxGraceDays;

		private MMTextBox textBoxLicenseNumber;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private OpenFileDialog openFileDialog1;

		private UltraFormattedLinkLabel linkLoadImage;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem PlantiffToolStripMenuItem;

		private ToolStripMenuItem defendantToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private LayoutControl layoutControl1;

		private LayoutControlGroup Root;

		private TabbedControlGroup tabbedControlGroup1;

		private LayoutControlGroup tabPageGeneral;

		private LayoutControlItem layoutControlItem1;

		private LayoutControlItem layoutControlItem2;

		private LayoutControlItem layoutControlItem3;

		private LayoutControlItem layoutControlItem4;

		private LayoutControlItem layoutControlItem5;

		private LayoutControlItem layoutControlItem6;

		private LayoutControlItem layoutControlItem7;

		private LayoutControlItem layoutControlItem8;

		private LayoutControlItem layoutControlItem9;

		private LayoutControlItem layoutControlItem10;

		private LayoutControlItem layoutControlItem11;

		private LayoutControlItem layoutControlItem12;

		private LayoutControlItem layoutControlItem13;

		private LayoutControlItem layoutControlItem14;

		private EmptySpaceItem emptySpaceItem2;

		private EmptySpaceItem emptySpaceItem3;

		private LayoutControlItem layoutControlItem15;

		private LayoutControlGroup layoutControlGroup4;

		private LayoutControlItem layoutControlItem16;

		private LayoutControlItem layoutControlItem17;

		private LayoutControlItem layoutControlItem18;

		private LayoutControlItem layoutControlItem26;

		private LayoutControlItem layoutControlItem27;

		private LayoutControlItem layoutControlItem19;

		private LayoutControlItem layoutControlItem20;

		private LayoutControlItem layoutControlItem21;

		private LayoutControlItem layoutControlItem22;

		private LayoutControlItem layoutControlItem23;

		private LayoutControlItem layoutControlItem24;

		private LayoutControlItem layoutControlItem28;

		private LayoutControlItem layoutControlItem29;

		private EmptySpaceItem emptySpaceItem5;

		private LayoutControlGroup tabPageDetails;

		private LayoutControlGroup layoutControlGroup3;

		private LayoutControlItem layoutControlItem30;

		private LayoutControlItem layoutControlItem31;

		private LayoutControlItem layoutControlItem32;

		private LayoutControlItem layoutControlItem34;

		private LayoutControlItem layoutControlItem35;

		private LayoutControlItem layoutControlItem36;

		private LayoutControlItem layoutControlItem33;

		private LayoutControlItem layoutControlItem37;

		private LayoutControlItem layoutControlItem38;

		private LayoutControlItem layoutControlItem39;

		private LayoutControlItem layoutControlItem40;

		private LayoutControlItem layoutControlItem41;

		private LayoutControlItem layoutControlItem42;

		private LayoutControlItem layoutControlItem43;

		private LayoutControlItem layoutControlItem44;

		private LayoutControlItem layoutControlItem45;

		private LayoutControlItem layoutControlItem46;

		private EmptySpaceItem emptySpaceItem9;

		private EmptySpaceItem emptySpaceItem10;

		private LayoutControlItem layoutControlItem47;

		private LayoutControlItem layoutControlItem50;

		private LayoutControlItem layoutControlItem51;

		private LayoutControlItem layoutControlItem52;

		private EmptySpaceItem emptySpaceItem11;

		private LayoutControlItem layoutControlItem53;

		private LayoutControlItem layoutControlItem54;

		private EmptySpaceItem emptySpaceItem12;

		private LayoutControlGroup layoutControlGroup5;

		private LayoutControlItem layoutControlItem55;

		private LayoutControlItem layoutControlItem56;

		private LayoutControlItem layoutControlItem57;

		private LayoutControlGroup layoutControlGroup6;

		private LayoutControlItem layoutControlItem58;

		private LayoutControlItem layoutControlItem59;

		private LayoutControlItem layoutControlItem60;

		private LayoutControlItem layoutItemConsignmentCom;

		private LayoutControlItem layoutControlItem48;

		private EmptySpaceItem emptySpaceItem15;

		private LayoutControlGroup layoutControlGroup7;

		private EmptySpaceItem emptySpaceItem14;

		private LayoutControlItem layoutControlItem61;

		private LayoutControlItem layoutControlItem62;

		private LayoutControlItem layoutControlItem63;

		private LayoutControlItem layoutControlItem64;

		private LayoutControlItem layoutControlItem65;

		private LayoutControlItem layoutControlItem66;

		private LayoutControlItem layoutControlItem67;

		private LayoutControlItem layoutControlItem68;

		private LayoutControlItem layoutControlItem69;

		private LayoutControlItem layoutControlItem70;

		private LayoutControlItem layoutControlItem72;

		private LayoutControlItem layoutControlItem73;

		private LayoutControlItem layoutControlItem71;

		private LayoutControlItem layoutControlItem74;

		private LayoutControlItem layoutControlItem75;

		private LayoutControlGroup layoutGroupCreditLimit;

		private LayoutControlItem layoutControlItem77;

		private LayoutControlItem layoutControlItem78;

		private LayoutControlItem layoutControlItem81;

		private LayoutControlItem layoutControlItem79;

		private LayoutControlItem layoutControlItem80;

		private LayoutControlItem layoutControlItem82;

		private LayoutControlItem layoutControlItem83;

		private LayoutControlItem layoutControlItem84;

		private LayoutControlItem layoutControlItem85;

		private LayoutControlItem layoutControlItem86;

		private LayoutControlItem layoutControlItem87;

		private LayoutControlItem layoutControlItem88;

		private LayoutControlItem layoutControlItem89;

		private LayoutControlItem layoutControlItem90;

		private LayoutControlItem layoutControlItem91;

		private LayoutControlItem layoutControlItem92;

		private EmptySpaceItem emptySpaceItem8;

		private LayoutControlItem layoutControlItem76;

		private LayoutControlGroup layoutControlGroup9;

		private LayoutControlItem layoutControlItem95;

		private LayoutControlItem layoutControlItem93;

		private LayoutControlGroup panelInsuranceDetails;

		private EmptySpaceItem emptySpaceItem16;

		private LayoutControlItem layoutControlItem94;

		private LayoutControlItem layoutControlItem97;

		private LayoutControlItem layoutControlItem98;

		private LayoutControlItem layoutControlItem99;

		private LayoutControlItem layoutControlItem100;

		private LayoutControlItem layoutControlItem101;

		private LayoutControlItem layoutControlItem102;

		private LayoutControlItem layoutControlItem103;

		private LayoutControlItem layoutControlItem104;

		private LayoutControlItem layoutControlItem105;

		private LayoutControlItem layoutControlItem96;

		private LayoutControlGroup layoutControlGroup10;

		private LayoutControlGroup tabPageActivity;

		private LayoutControlGroup layoutControlGroup12;

		private LayoutControlGroup layoutControlGroup13;

		private LayoutControlGroup layoutControlGroup14;

		private EmptySpaceItem emptySpaceItem18;

		private LayoutControlItem layoutControlItem106;

		private LayoutControlItem layoutControlItem107;

		private EmptySpaceItem emptySpaceItem19;

		private LayoutControlItem layoutControlItem108;

		private LayoutControlItem layoutControlItem109;

		private EmptySpaceItem emptySpaceItem20;

		private LayoutControlItem layoutControlItem110;

		private LayoutControlItem layoutControlItem111;

		private LayoutControlItem layoutControlItem112;

		private LayoutControlItem layoutControlItem113;

		private EmptySpaceItem emptySpaceItem1;

		private MMTextBox textBoxAddressPrintFormat;

		private LayoutControlItem layoutControlItem25;

		private EmptySpaceItem emptySpaceItem4;

		private EmptySpaceItem emptySpaceItem6;

		private EmptySpaceItem emptySpaceItem7;

		private ToolStripDropDownButton toolStripDropDownButtonEnquiry;

		private ToolStripMenuItem menuItemCustomerLedger;

		private ToolStripMenuItem menuItemSalesStatistics;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripDropDownButton toolStripButtonDesign;

		private ToolStripMenuItem menuItemLayoutDesign;

		private ToolStripMenuItem menuItemCustomFields;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private bool isLoading;

		private string sourceLeadID = "";

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2003;

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
					textBoxAddressID.Enabled = false;
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
					comboBoxBilltoAddress.Filter("");
					comboBoxShiptoAddress.Filter("");
					toolStripButtonHistory.Visible = false;
					toolStripDropDownButtonEnquiry.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
					linkAddPicture.Enabled = true;
					comboBoxBilltoAddress.Filter(textBoxCode.Text);
					comboBoxShiptoAddress.Filter(textBoxCode.Text);
					toolStripButtonHistory.Visible = true;
					toolStripDropDownButtonEnquiry.Enabled = true;
				}
				buttonCategories.Enabled = !value;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonComments.Enabled = !value;
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

		public string SourceLeadID
		{
			get
			{
				return sourceLeadID;
			}
			set
			{
				sourceLeadID = value;
			}
		}

		public CustomerDetailsForm()
		{
			InitializeComponent();
			dataGridContacts.DropDownMenu.Items.Add(new ToolStripSeparator());
			checked
			{
				int num;
				for (num = 0; num < contextMenuStripContact.Items.Count; num++)
				{
					dataGridContacts.DropDownMenu.Items.Add(contextMenuStripContact.Items[num]);
					num--;
				}
				if (!GlobalRules.IsModuleAvailable(AxolonModules.CRM))
				{
					tabPageActivity.Visibility = LayoutVisibility.Never;
				}
				openContactToolStripMenuItem.Click += openContactToolStripMenuItem_Click;
				newContactToolStripMenuItem.Click += newContactToolStripMenuItem_Click;
				deleteContactToolStripMenuItem.Click += deleteContactToolStripMenuItem_Click;
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
            Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem15 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem16 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem17 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem18 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem19 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem20 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem21 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem22 = new Infragistics.Win.ValueListItem();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDetailsForm));
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
            Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
            this.textBoxConfirmationLevel = new Micromind.UISupport.NumberTextBox();
            this.dateTimeBalanceConfirmationDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxRatingRemarks = new Micromind.UISupport.MMTextBox();
            this.comboBoxCreditReviewBy = new Micromind.DataControls.UserComboBox();
            this.comboBoxRatingBy = new Micromind.DataControls.UserComboBox();
            this.dateTimePickerRatingDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.buttonCustomerInsuranceClaim = new Micromind.UISupport.XPButton();
            this.comboBoxInsuranceProvider = new Micromind.DataControls.InsuranceProviderComboBox();
            this.textBoxProvider = new Micromind.UISupport.MMTextBox();
            this.dateTimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.datetimePickerEffectiveDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxInsuranceID = new Micromind.UISupport.MMTextBox();
            this.comboBoxInsuranceRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
            this.textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
            this.textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
            this.textBoxInsuranceReqAmount = new Micromind.UISupport.AmountTextBox();
            this.dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.comboBoxInsuranceStatus = new System.Windows.Forms.ComboBox();
            this.textBoxPaymentTermName = new Micromind.UISupport.MMTextBox();
            this.textBoxPaymentMethodName = new Micromind.UISupport.MMTextBox();
            this.comboBoxRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.comboBoxPaymentTerms = new Micromind.DataControls.PaymentTermComboBox();
            this.linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxGraceDays = new Micromind.UISupport.NumberTextBox();
            this.dateTimePickerCLValidity = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxUnsecuredLimit = new Micromind.UISupport.AmountTextBox();
            this.checkBoxUnsecuredLimit = new System.Windows.Forms.CheckBox();
            this.textBoxTempLimit = new Micromind.UISupport.MMTextBox();
            this.checkBoxAcceptCheque = new System.Windows.Forms.CheckBox();
            this.radioButtonSublimit = new System.Windows.Forms.RadioButton();
            this.checkBoxAcceptPDC = new System.Windows.Forms.CheckBox();
            this.textBoxCreditLimit = new Micromind.UISupport.AmountTextBox();
            this.radioButtonCreditLimitNoCredit = new System.Windows.Forms.RadioButton();
            this.radioButtonCreditLimitUnlimited = new System.Windows.Forms.RadioButton();
            this.radioButtonCreditLimitAmount = new System.Windows.Forms.RadioButton();
            this.comboBoxCollectionUser = new Micromind.DataControls.UserComboBox();
            this.dateTimePickerReviewDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.dataGridContacts = new Micromind.DataControls.DataEntryGrid();
            this.gridComboBoxContact = new Micromind.DataControls.ContactsComboBox();
            this.comboBoxActivityPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textBoxAddressPrintFormat = new Micromind.UISupport.MMTextBox();
            this.udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
            this.textBoxNote = new Micromind.UISupport.MMTextBox();
            this.textBoxProfileDetails = new DevExpress.XtraRichEdit.RichEditControl();
            this.dataGridActivities = new Micromind.UISupport.DataGridList(this.components);
            this.buttonAddActivity = new System.Windows.Forms.Button();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.textBoxConsignCommission = new Micromind.UISupport.PercentTextBox();
            this.textBoxDeliveryInstructions = new Micromind.UISupport.MMTextBox();
            this.textBoxAccountInstructions = new Micromind.UISupport.MMTextBox();
            this.comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
            this.buttonAccounts = new Micromind.UISupport.XPButton();
            this.textBoxTaxIDNumber = new Micromind.UISupport.MMTextBox();
            this.textBoxLongitude = new Micromind.UISupport.MMTextBox();
            this.textBoxBankBranch = new Micromind.UISupport.MMTextBox();
            this.comboBoxTaxOption = new System.Windows.Forms.ComboBox();
            this.textBoxBankName = new Micromind.UISupport.MMTextBox();
            this.textBoxLicenseNumber = new Micromind.UISupport.MMTextBox();
            this.textBoxBankAccountNumber = new Micromind.UISupport.MMTextBox();
            this.textBoxRebatePercent = new Micromind.UISupport.PercentTextBox();
            this.comboBoxCustomerGroup = new Micromind.DataControls.CustomerGroupComboBox();
            this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
            this.buttonCategories = new Micromind.UISupport.XPButton();
            this.comboBoxPaymentMethods = new Micromind.DataControls.paymentMethodsComboBox();
            this.textBoxLatitude = new Micromind.UISupport.MMTextBox();
            this.buttonMoreAddress = new Micromind.UISupport.XPButton();
            this.comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
            this.checkBoxparentACforposting = new System.Windows.Forms.CheckBox();
            this.dateTimePickerContractExpDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.dateTimePickerLicenseExpDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
            this.textBoxStatementEmail = new Micromind.UISupport.MMTextBox();
            this.comboBoxStatementMethod = new System.Windows.Forms.ComboBox();
            this.textBoxComment = new Micromind.UISupport.MMTextBox();
            this.textBoxCode = new Micromind.UISupport.MMTextBox();
            this.checkBoxWeightInvoice = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowConsignment = new System.Windows.Forms.CheckBox();
            this.textBoxDepartment = new Micromind.UISupport.MMTextBox();
            this.textBoxWebsite = new Micromind.UISupport.MMTextBox();
            this.textBoxName = new Micromind.UISupport.MMTextBox();
            this.comboBoxLeadSource = new Micromind.DataControls.GenericListComboBox();
            this.comboBoxPriceLevel = new Micromind.DataControls.PriceLevelComboBox();
            this.textBoxFormalName = new Micromind.UISupport.MMTextBox();
            this.comboBoxArea = new Micromind.DataControls.AreaComboBox();
            this.textBoxForeignName = new Micromind.UISupport.MMTextBox();
            this.dateTimePickerEstablished = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxEmail = new Micromind.UISupport.MMTextBox();
            this.dateTimePickerCustomerSince = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.comboBoxCountry = new Micromind.DataControls.CountryComboBox();
            this.textBoxMobile = new Micromind.UISupport.MMTextBox();
            this.comboBoxBilltoAddress = new Micromind.DataControls.CustomerAddressComboBox();
            this.comboBoxParentCustomer = new Micromind.DataControls.customersFlatComboBox();
            this.comboBoxShippingMethods = new Micromind.DataControls.ShippingMethodsComboBox();
            this.textBoxPostalCode = new Micromind.UISupport.MMTextBox();
            this.textBoxFax = new Micromind.UISupport.MMTextBox();
            this.checkBoxIsInactive = new System.Windows.Forms.CheckBox();
            this.comboBoxShiptoAddress = new Micromind.DataControls.CustomerAddressComboBox();
            this.checkBoxHold = new System.Windows.Forms.CheckBox();
            this.textBoxPhone2 = new Micromind.UISupport.MMTextBox();
            this.comboBoxCustomerClass = new Micromind.DataControls.CustomerClassComboBox();
            this.textBoxAddressID = new Micromind.UISupport.MMTextBox();
            this.textBoxPhone1 = new Micromind.UISupport.MMTextBox();
            this.textBoxContactName = new Micromind.UISupport.MMTextBox();
            this.textBoxAddress1 = new Micromind.UISupport.MMTextBox();
            this.textBoxAddress2 = new Micromind.UISupport.MMTextBox();
            this.textBoxAddress3 = new Micromind.UISupport.MMTextBox();
            this.textBoxCity = new Micromind.UISupport.MMTextBox();
            this.textBoxState = new Micromind.UISupport.MMTextBox();
            this.textBoxCountry = new Micromind.UISupport.MMTextBox();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem64 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem65 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem66 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem67 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem68 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem69 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem70 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem72 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem73 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem71 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem74 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem75 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutGroupCreditLimit = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem77 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem78 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem81 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem79 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem80 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem82 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem83 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem84 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem85 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem86 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem87 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem88 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem89 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem90 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem91 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem92 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem76 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem95 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem93 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelInsuranceDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem97 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem98 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem99 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem100 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem101 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem102 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem103 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem104 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem105 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem94 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem96 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem18 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tabPageGeneral = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.tabPageDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem53 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem55 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem57 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem60 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemConsignmentCom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem62 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem106 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem107 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabPageActivity = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem19 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem108 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem109 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem20 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem110 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem111 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem112 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem113 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.buttonDelete = new Micromind.UISupport.XPButton();
            this.buttonClose = new Micromind.UISupport.XPButton();
            this.buttonNew = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDesign = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemLayoutDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCustomFields = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonEnquiry = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemCustomerLedger = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSalesStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.PlantiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defendantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
            this.contextMenuStripContact = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListComments = new System.Windows.Forms.ImageList(this.components);
            this.formManager = new Micromind.DataControls.FormManager();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCreditReviewBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRatingBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInsuranceProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInsuranceRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPaymentTerms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCollectionUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContacts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComboBoxContact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxActivityPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridActivities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTaxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPaymentMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLeadSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPriceLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxBilltoAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxParentCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShiptoAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomerClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem65)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem66)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem67)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem68)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem69)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem70)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem73)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem71)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem74)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem75)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupCreditLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem77)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem78)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem81)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem79)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem80)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem82)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem83)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem84)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem85)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem86)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem87)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem88)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem89)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem90)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem91)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem92)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem76)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem93)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelInsuranceDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem97)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem98)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem99)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem101)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem102)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem103)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem104)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem105)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem94)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem96)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem53)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem60)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemConsignmentCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem61)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem62)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem63)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem106)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem107)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem108)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem109)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem110)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem111)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem112)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem113)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStripContact.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxConfirmationLevel
            // 
            this.textBoxConfirmationLevel.AllowDecimal = false;
            this.textBoxConfirmationLevel.CustomReportFieldName = "";
            this.textBoxConfirmationLevel.CustomReportKey = "";
            this.textBoxConfirmationLevel.CustomReportValueType = ((byte)(1));
            this.textBoxConfirmationLevel.IsComboTextBox = false;
            this.textBoxConfirmationLevel.IsModified = false;
            this.textBoxConfirmationLevel.Location = new System.Drawing.Point(630, 165);
            this.textBoxConfirmationLevel.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxConfirmationLevel.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxConfirmationLevel.Name = "textBoxConfirmationLevel";
            this.textBoxConfirmationLevel.NullText = "0";
            this.textBoxConfirmationLevel.Size = new System.Drawing.Size(286, 20);
            this.textBoxConfirmationLevel.TabIndex = 11;
            this.textBoxConfirmationLevel.Text = "0";
            // 
            // dateTimeBalanceConfirmationDate
            // 
            this.dateTimeBalanceConfirmationDate.Checked = false;
            this.dateTimeBalanceConfirmationDate.CustomFormat = " ";
            this.dateTimeBalanceConfirmationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBalanceConfirmationDate.Location = new System.Drawing.Point(191, 165);
            this.dateTimeBalanceConfirmationDate.Name = "dateTimeBalanceConfirmationDate";
            this.dateTimeBalanceConfirmationDate.ShowCheckBox = true;
            this.dateTimeBalanceConfirmationDate.Size = new System.Drawing.Size(268, 20);
            this.dateTimeBalanceConfirmationDate.TabIndex = 10;
            this.dateTimeBalanceConfirmationDate.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxRatingRemarks
            // 
            this.textBoxRatingRemarks.BackColor = System.Drawing.Color.White;
            this.textBoxRatingRemarks.CustomReportFieldName = "";
            this.textBoxRatingRemarks.CustomReportKey = "";
            this.textBoxRatingRemarks.CustomReportValueType = ((byte)(1));
            this.textBoxRatingRemarks.IsComboTextBox = false;
            this.textBoxRatingRemarks.IsModified = false;
            this.textBoxRatingRemarks.Location = new System.Drawing.Point(191, 189);
            this.textBoxRatingRemarks.MaxLength = 255;
            this.textBoxRatingRemarks.Multiline = true;
            this.textBoxRatingRemarks.Name = "textBoxRatingRemarks";
            this.textBoxRatingRemarks.Size = new System.Drawing.Size(725, 20);
            this.textBoxRatingRemarks.TabIndex = 12;
            // 
            // comboBoxCreditReviewBy
            // 
            this.comboBoxCreditReviewBy.Assigned = false;
            this.comboBoxCreditReviewBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxCreditReviewBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCreditReviewBy.CustomReportFieldName = "";
            this.comboBoxCreditReviewBy.CustomReportKey = "";
            this.comboBoxCreditReviewBy.CustomReportValueType = ((byte)(1));
            this.comboBoxCreditReviewBy.DescriptionTextBox = null;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCreditReviewBy.DisplayLayout.Appearance = appearance1;
            this.comboBoxCreditReviewBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCreditReviewBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCreditReviewBy.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCreditReviewBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.comboBoxCreditReviewBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCreditReviewBy.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.comboBoxCreditReviewBy.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCreditReviewBy.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.CellAppearance = appearance8;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.comboBoxCreditReviewBy.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.RowAppearance = appearance11;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCreditReviewBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.comboBoxCreditReviewBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCreditReviewBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCreditReviewBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCreditReviewBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCreditReviewBy.Editable = true;
            this.comboBoxCreditReviewBy.FilterString = "";
            this.comboBoxCreditReviewBy.HasAllAccount = false;
            this.comboBoxCreditReviewBy.HasCustom = false;
            this.comboBoxCreditReviewBy.IsDataLoaded = false;
            this.comboBoxCreditReviewBy.Location = new System.Drawing.Point(630, 93);
            this.comboBoxCreditReviewBy.MaxDropDownItems = 12;
            this.comboBoxCreditReviewBy.Name = "comboBoxCreditReviewBy";
            this.comboBoxCreditReviewBy.ShowInactiveItems = false;
            this.comboBoxCreditReviewBy.ShowQuickAdd = true;
            this.comboBoxCreditReviewBy.Size = new System.Drawing.Size(286, 20);
            this.comboBoxCreditReviewBy.TabIndex = 5;
            this.comboBoxCreditReviewBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxRatingBy
            // 
            this.comboBoxRatingBy.Assigned = false;
            this.comboBoxRatingBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxRatingBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxRatingBy.CustomReportFieldName = "";
            this.comboBoxRatingBy.CustomReportKey = "";
            this.comboBoxRatingBy.CustomReportValueType = ((byte)(1));
            this.comboBoxRatingBy.DescriptionTextBox = null;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxRatingBy.DisplayLayout.Appearance = appearance13;
            this.comboBoxRatingBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxRatingBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxRatingBy.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxRatingBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.comboBoxRatingBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxRatingBy.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.comboBoxRatingBy.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxRatingBy.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxRatingBy.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxRatingBy.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.comboBoxRatingBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxRatingBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxRatingBy.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxRatingBy.DisplayLayout.Override.CellAppearance = appearance20;
            this.comboBoxRatingBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxRatingBy.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxRatingBy.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.comboBoxRatingBy.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.comboBoxRatingBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxRatingBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxRatingBy.DisplayLayout.Override.RowAppearance = appearance23;
            this.comboBoxRatingBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxRatingBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.comboBoxRatingBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxRatingBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxRatingBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxRatingBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxRatingBy.Editable = true;
            this.comboBoxRatingBy.FilterString = "";
            this.comboBoxRatingBy.HasAllAccount = false;
            this.comboBoxRatingBy.HasCustom = false;
            this.comboBoxRatingBy.IsDataLoaded = false;
            this.comboBoxRatingBy.Location = new System.Drawing.Point(752, 141);
            this.comboBoxRatingBy.MaxDropDownItems = 12;
            this.comboBoxRatingBy.Name = "comboBoxRatingBy";
            this.comboBoxRatingBy.ShowInactiveItems = false;
            this.comboBoxRatingBy.ShowQuickAdd = true;
            this.comboBoxRatingBy.Size = new System.Drawing.Size(164, 20);
            this.comboBoxRatingBy.TabIndex = 9;
            this.comboBoxRatingBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // dateTimePickerRatingDate
            // 
            this.dateTimePickerRatingDate.Checked = false;
            this.dateTimePickerRatingDate.CustomFormat = " ";
            this.dateTimePickerRatingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerRatingDate.Location = new System.Drawing.Point(191, 141);
            this.dateTimePickerRatingDate.Name = "dateTimePickerRatingDate";
            this.dateTimePickerRatingDate.ShowCheckBox = true;
            this.dateTimePickerRatingDate.Size = new System.Drawing.Size(74, 20);
            this.dateTimePickerRatingDate.TabIndex = 8;
            this.dateTimePickerRatingDate.Value = new System.DateTime(((long)(0)));
            // 
            // buttonCustomerInsuranceClaim
            // 
            this.buttonCustomerInsuranceClaim.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCustomerInsuranceClaim.BackColor = System.Drawing.Color.DarkGray;
            this.buttonCustomerInsuranceClaim.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCustomerInsuranceClaim.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCustomerInsuranceClaim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCustomerInsuranceClaim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCustomerInsuranceClaim.Location = new System.Drawing.Point(453, 432);
            this.buttonCustomerInsuranceClaim.Name = "buttonCustomerInsuranceClaim";
            this.buttonCustomerInsuranceClaim.Size = new System.Drawing.Size(208, 21);
            this.buttonCustomerInsuranceClaim.TabIndex = 46;
            this.buttonCustomerInsuranceClaim.Text = "Customer Insurance Claim";
            this.buttonCustomerInsuranceClaim.UseVisualStyleBackColor = false;
            this.buttonCustomerInsuranceClaim.Visible = false;
            // 
            // comboBoxInsuranceProvider
            // 
            this.comboBoxInsuranceProvider.Assigned = false;
            this.comboBoxInsuranceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxInsuranceProvider.CustomReportFieldName = "";
            this.comboBoxInsuranceProvider.CustomReportKey = "";
            this.comboBoxInsuranceProvider.CustomReportValueType = ((byte)(1));
            this.comboBoxInsuranceProvider.DescriptionTextBox = this.textBoxProvider;
            this.comboBoxInsuranceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxInsuranceProvider.Editable = true;
            this.comboBoxInsuranceProvider.FilterString = "";
            this.comboBoxInsuranceProvider.HasAllAccount = false;
            this.comboBoxInsuranceProvider.HasCustom = false;
            this.comboBoxInsuranceProvider.IsDataLoaded = false;
            this.comboBoxInsuranceProvider.Location = new System.Drawing.Point(195, 457);
            this.comboBoxInsuranceProvider.MaxDropDownItems = 12;
            this.comboBoxInsuranceProvider.Name = "comboBoxInsuranceProvider";
            this.comboBoxInsuranceProvider.ShowInactiveItems = false;
            this.comboBoxInsuranceProvider.ShowQuickAdd = true;
            this.comboBoxInsuranceProvider.Size = new System.Drawing.Size(254, 20);
            this.comboBoxInsuranceProvider.TabIndex = 1;
            this.comboBoxInsuranceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxProvider.CustomReportFieldName = "";
            this.textBoxProvider.CustomReportKey = "";
            this.textBoxProvider.CustomReportValueType = ((byte)(1));
            this.textBoxProvider.Enabled = false;
            this.textBoxProvider.IsComboTextBox = false;
            this.textBoxProvider.IsModified = false;
            this.textBoxProvider.Location = new System.Drawing.Point(453, 457);
            this.textBoxProvider.MaxLength = 64;
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.ReadOnly = true;
            this.textBoxProvider.Size = new System.Drawing.Size(459, 20);
            this.textBoxProvider.TabIndex = 2;
            this.textBoxProvider.TabStop = false;
            // 
            // dateTimePickerValidTo
            // 
            this.dateTimePickerValidTo.Checked = false;
            this.dateTimePickerValidTo.CustomFormat = " ";
            this.dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerValidTo.Location = new System.Drawing.Point(560, 553);
            this.dateTimePickerValidTo.Name = "dateTimePickerValidTo";
            this.dateTimePickerValidTo.ShowCheckBox = true;
            this.dateTimePickerValidTo.Size = new System.Drawing.Size(352, 20);
            this.dateTimePickerValidTo.TabIndex = 7;
            this.dateTimePickerValidTo.Value = new System.DateTime(((long)(0)));
            // 
            // datetimePickerEffectiveDate
            // 
            this.datetimePickerEffectiveDate.Checked = false;
            this.datetimePickerEffectiveDate.CustomFormat = " ";
            this.datetimePickerEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimePickerEffectiveDate.Location = new System.Drawing.Point(195, 553);
            this.datetimePickerEffectiveDate.Name = "datetimePickerEffectiveDate";
            this.datetimePickerEffectiveDate.ShowCheckBox = true;
            this.datetimePickerEffectiveDate.Size = new System.Drawing.Size(194, 20);
            this.datetimePickerEffectiveDate.TabIndex = 6;
            this.datetimePickerEffectiveDate.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxInsuranceID
            // 
            this.textBoxInsuranceID.BackColor = System.Drawing.Color.White;
            this.textBoxInsuranceID.CustomReportFieldName = "";
            this.textBoxInsuranceID.CustomReportKey = "";
            this.textBoxInsuranceID.CustomReportValueType = ((byte)(1));
            this.textBoxInsuranceID.IsComboTextBox = false;
            this.textBoxInsuranceID.IsModified = false;
            this.textBoxInsuranceID.Location = new System.Drawing.Point(560, 529);
            this.textBoxInsuranceID.MaxLength = 30;
            this.textBoxInsuranceID.Name = "textBoxInsuranceID";
            this.textBoxInsuranceID.Size = new System.Drawing.Size(352, 20);
            this.textBoxInsuranceID.TabIndex = 5;
            // 
            // comboBoxInsuranceRating
            // 
            this.comboBoxInsuranceRating.AutoSize = false;
            this.comboBoxInsuranceRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            valueListItem.DataValue = ((byte)(0));
            valueListItem.DisplayText = "N/A";
            valueListItem2.DataValue = ((byte)(1));
            valueListItem2.DisplayText = "1";
            valueListItem3.DataValue = "2";
            valueListItem3.DisplayText = "2";
            valueListItem4.DataValue = ((byte)(3));
            valueListItem4.DisplayText = "3";
            valueListItem5.DataValue = "4";
            valueListItem5.DisplayText = "4";
            valueListItem6.DataValue = ((byte)(5));
            valueListItem6.DisplayText = "5";
            valueListItem7.DataValue = ((byte)(6));
            valueListItem7.DisplayText = "6";
            valueListItem8.DataValue = "7";
            valueListItem8.DisplayText = "7";
            valueListItem9.DataValue = "8";
            valueListItem9.DisplayText = "8";
            valueListItem10.DataValue = "9";
            valueListItem10.DisplayText = "9";
            valueListItem11.DataValue = "10";
            valueListItem11.DisplayText = "10";
            this.comboBoxInsuranceRating.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem,
            valueListItem2,
            valueListItem3,
            valueListItem4,
            valueListItem5,
            valueListItem6,
            valueListItem7,
            valueListItem8,
            valueListItem9,
            valueListItem10,
            valueListItem11});
            this.comboBoxInsuranceRating.Location = new System.Drawing.Point(195, 529);
            this.comboBoxInsuranceRating.Name = "comboBoxInsuranceRating";
            this.comboBoxInsuranceRating.Size = new System.Drawing.Size(194, 20);
            this.comboBoxInsuranceRating.TabIndex = 4;
            // 
            // textBoxInsuranceRemarks
            // 
            this.textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
            this.textBoxInsuranceRemarks.CustomReportFieldName = "";
            this.textBoxInsuranceRemarks.CustomReportKey = "";
            this.textBoxInsuranceRemarks.CustomReportValueType = ((byte)(1));
            this.textBoxInsuranceRemarks.IsComboTextBox = false;
            this.textBoxInsuranceRemarks.IsModified = false;
            this.textBoxInsuranceRemarks.Location = new System.Drawing.Point(195, 577);
            this.textBoxInsuranceRemarks.MaxLength = 255;
            this.textBoxInsuranceRemarks.Multiline = true;
            this.textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
            this.textBoxInsuranceRemarks.Size = new System.Drawing.Size(717, 42);
            this.textBoxInsuranceRemarks.TabIndex = 8;
            // 
            // textBoxInsuranceNumber
            // 
            this.textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
            this.textBoxInsuranceNumber.CustomReportFieldName = "";
            this.textBoxInsuranceNumber.CustomReportKey = "";
            this.textBoxInsuranceNumber.CustomReportValueType = ((byte)(1));
            this.textBoxInsuranceNumber.IsComboTextBox = false;
            this.textBoxInsuranceNumber.IsModified = false;
            this.textBoxInsuranceNumber.Location = new System.Drawing.Point(560, 481);
            this.textBoxInsuranceNumber.MaxLength = 30;
            this.textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
            this.textBoxInsuranceNumber.Size = new System.Drawing.Size(171, 20);
            this.textBoxInsuranceNumber.TabIndex = 1;
            // 
            // textBoxInsuranceApprovedAmount
            // 
            this.textBoxInsuranceApprovedAmount.AllowDecimal = true;
            this.textBoxInsuranceApprovedAmount.BackColor = System.Drawing.Color.White;
            this.textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
            this.textBoxInsuranceApprovedAmount.CustomReportKey = "";
            this.textBoxInsuranceApprovedAmount.CustomReportValueType = ((byte)(1));
            this.textBoxInsuranceApprovedAmount.IsComboTextBox = false;
            this.textBoxInsuranceApprovedAmount.IsModified = false;
            this.textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(560, 505);
            this.textBoxInsuranceApprovedAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxInsuranceApprovedAmount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxInsuranceApprovedAmount.Name = "textBoxInsuranceApprovedAmount";
            this.textBoxInsuranceApprovedAmount.NullText = "0";
            this.textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(171, 20);
            this.textBoxInsuranceApprovedAmount.TabIndex = 3;
            this.textBoxInsuranceApprovedAmount.Text = "0.00";
            this.textBoxInsuranceApprovedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxInsuranceApprovedAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // textBoxInsuranceReqAmount
            // 
            this.textBoxInsuranceReqAmount.AllowDecimal = true;
            this.textBoxInsuranceReqAmount.BackColor = System.Drawing.Color.White;
            this.textBoxInsuranceReqAmount.CustomReportFieldName = "";
            this.textBoxInsuranceReqAmount.CustomReportKey = "";
            this.textBoxInsuranceReqAmount.CustomReportValueType = ((byte)(1));
            this.textBoxInsuranceReqAmount.IsComboTextBox = false;
            this.textBoxInsuranceReqAmount.IsModified = false;
            this.textBoxInsuranceReqAmount.Location = new System.Drawing.Point(195, 505);
            this.textBoxInsuranceReqAmount.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxInsuranceReqAmount.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxInsuranceReqAmount.Name = "textBoxInsuranceReqAmount";
            this.textBoxInsuranceReqAmount.NullText = "0";
            this.textBoxInsuranceReqAmount.Size = new System.Drawing.Size(194, 20);
            this.textBoxInsuranceReqAmount.TabIndex = 2;
            this.textBoxInsuranceReqAmount.Text = "0.00";
            this.textBoxInsuranceReqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxInsuranceReqAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // dateTimePickerInsuranceDate
            // 
            this.dateTimePickerInsuranceDate.Checked = false;
            this.dateTimePickerInsuranceDate.CustomFormat = " ";
            this.dateTimePickerInsuranceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerInsuranceDate.Location = new System.Drawing.Point(195, 481);
            this.dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
            this.dateTimePickerInsuranceDate.ShowCheckBox = true;
            this.dateTimePickerInsuranceDate.Size = new System.Drawing.Size(194, 20);
            this.dateTimePickerInsuranceDate.TabIndex = 0;
            this.dateTimePickerInsuranceDate.Value = new System.DateTime(((long)(0)));
            // 
            // comboBoxInsuranceStatus
            // 
            this.comboBoxInsuranceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInsuranceStatus.FormattingEnabled = true;
            this.comboBoxInsuranceStatus.Items.AddRange(new object[] {
            "Not Insured",
            "Under Process",
            "Insured",
            "Insured-Sublimit of Parent",
            "Rejected",
            "On Hold",
            "Cancelled"});
            this.comboBoxInsuranceStatus.Location = new System.Drawing.Point(195, 432);
            this.comboBoxInsuranceStatus.Name = "comboBoxInsuranceStatus";
            this.comboBoxInsuranceStatus.Size = new System.Drawing.Size(254, 21);
            this.comboBoxInsuranceStatus.TabIndex = 0;
            // 
            // textBoxPaymentTermName
            // 
            this.textBoxPaymentTermName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxPaymentTermName.CustomReportFieldName = "";
            this.textBoxPaymentTermName.CustomReportKey = "";
            this.textBoxPaymentTermName.CustomReportValueType = ((byte)(1));
            this.textBoxPaymentTermName.IsComboTextBox = false;
            this.textBoxPaymentTermName.IsModified = false;
            this.textBoxPaymentTermName.Location = new System.Drawing.Point(463, 69);
            this.textBoxPaymentTermName.MaxLength = 30;
            this.textBoxPaymentTermName.Name = "textBoxPaymentTermName";
            this.textBoxPaymentTermName.ReadOnly = true;
            this.textBoxPaymentTermName.Size = new System.Drawing.Size(453, 20);
            this.textBoxPaymentTermName.TabIndex = 3;
            this.textBoxPaymentTermName.TabStop = false;
            // 
            // textBoxPaymentMethodName
            // 
            this.textBoxPaymentMethodName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxPaymentMethodName.CustomReportFieldName = "";
            this.textBoxPaymentMethodName.CustomReportKey = "";
            this.textBoxPaymentMethodName.CustomReportValueType = ((byte)(1));
            this.textBoxPaymentMethodName.IsComboTextBox = false;
            this.textBoxPaymentMethodName.IsModified = false;
            this.textBoxPaymentMethodName.Location = new System.Drawing.Point(463, 45);
            this.textBoxPaymentMethodName.MaxLength = 30;
            this.textBoxPaymentMethodName.Name = "textBoxPaymentMethodName";
            this.textBoxPaymentMethodName.ReadOnly = true;
            this.textBoxPaymentMethodName.Size = new System.Drawing.Size(453, 20);
            this.textBoxPaymentMethodName.TabIndex = 1;
            this.textBoxPaymentMethodName.TabStop = false;
            // 
            // comboBoxRating
            // 
            this.comboBoxRating.AutoSize = false;
            this.comboBoxRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            valueListItem12.DataValue = ((byte)(0));
            valueListItem12.DisplayText = "N/A";
            valueListItem13.DataValue = ((byte)(1));
            valueListItem13.DisplayText = "1";
            valueListItem14.DataValue = "2";
            valueListItem14.DisplayText = "2";
            valueListItem15.DataValue = ((byte)(3));
            valueListItem15.DisplayText = "3";
            valueListItem16.DataValue = "4";
            valueListItem16.DisplayText = "4";
            valueListItem17.DataValue = ((byte)(5));
            valueListItem17.DisplayText = "5";
            valueListItem18.DataValue = ((byte)(6));
            valueListItem18.DisplayText = "6";
            valueListItem19.DataValue = "7";
            valueListItem19.DisplayText = "7";
            valueListItem20.DataValue = "8";
            valueListItem20.DisplayText = "8";
            valueListItem21.DataValue = "9";
            valueListItem21.DisplayText = "9";
            valueListItem22.DataValue = "10";
            valueListItem22.DisplayText = "10";
            this.comboBoxRating.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem12,
            valueListItem13,
            valueListItem14,
            valueListItem15,
            valueListItem16,
            valueListItem17,
            valueListItem18,
            valueListItem19,
            valueListItem20,
            valueListItem21,
            valueListItem22});
            this.comboBoxRating.Location = new System.Drawing.Point(436, 141);
            this.comboBoxRating.Name = "comboBoxRating";
            this.comboBoxRating.Size = new System.Drawing.Size(145, 20);
            this.comboBoxRating.TabIndex = 7;
            this.comboBoxRating.ValueChanged += new System.EventHandler(this.comboBoxRating_ValueChanged);
            // 
            // comboBoxPaymentTerms
            // 
            this.comboBoxPaymentTerms.Assigned = false;
            this.comboBoxPaymentTerms.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPaymentTerms.CustomReportFieldName = "";
            this.comboBoxPaymentTerms.CustomReportKey = "";
            this.comboBoxPaymentTerms.CustomReportValueType = ((byte)(1));
            this.comboBoxPaymentTerms.DescriptionTextBox = this.textBoxPaymentTermName;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPaymentTerms.DisplayLayout.Appearance = appearance25;
            this.comboBoxPaymentTerms.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPaymentTerms.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance26.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentTerms.DisplayLayout.GroupByBox.Appearance = appearance26;
            appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPaymentTerms.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
            this.comboBoxPaymentTerms.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance28.BackColor2 = System.Drawing.SystemColors.Control;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPaymentTerms.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
            this.comboBoxPaymentTerms.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPaymentTerms.DisplayLayout.MaxRowScrollRegions = 1;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPaymentTerms.DisplayLayout.Override.ActiveCellAppearance = appearance29;
            appearance30.BackColor = System.Drawing.SystemColors.Highlight;
            appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPaymentTerms.DisplayLayout.Override.ActiveRowAppearance = appearance30;
            this.comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPaymentTerms.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentTerms.DisplayLayout.Override.CardAreaAppearance = appearance31;
            appearance32.BorderColor = System.Drawing.Color.Silver;
            appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPaymentTerms.DisplayLayout.Override.CellAppearance = appearance32;
            this.comboBoxPaymentTerms.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPaymentTerms.DisplayLayout.Override.CellPadding = 0;
            appearance33.BackColor = System.Drawing.SystemColors.Control;
            appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance33.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentTerms.DisplayLayout.Override.GroupByRowAppearance = appearance33;
            appearance34.TextHAlignAsString = "Left";
            this.comboBoxPaymentTerms.DisplayLayout.Override.HeaderAppearance = appearance34;
            this.comboBoxPaymentTerms.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPaymentTerms.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            appearance35.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPaymentTerms.DisplayLayout.Override.RowAppearance = appearance35;
            this.comboBoxPaymentTerms.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPaymentTerms.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
            this.comboBoxPaymentTerms.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPaymentTerms.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPaymentTerms.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPaymentTerms.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPaymentTerms.Editable = true;
            this.comboBoxPaymentTerms.FilterString = "";
            this.comboBoxPaymentTerms.HasAllAccount = false;
            this.comboBoxPaymentTerms.HasCustom = false;
            this.comboBoxPaymentTerms.IsDataLoaded = false;
            this.comboBoxPaymentTerms.Location = new System.Drawing.Point(191, 69);
            this.comboBoxPaymentTerms.MaxDropDownItems = 12;
            this.comboBoxPaymentTerms.MaxLength = 15;
            this.comboBoxPaymentTerms.Name = "comboBoxPaymentTerms";
            this.comboBoxPaymentTerms.ShowInactiveItems = false;
            this.comboBoxPaymentTerms.ShowQuickAdd = true;
            this.comboBoxPaymentTerms.Size = new System.Drawing.Size(268, 20);
            this.comboBoxPaymentTerms.TabIndex = 2;
            this.comboBoxPaymentTerms.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // linkLoadImage
            // 
            this.linkLoadImage.Location = new System.Drawing.Point(299, 364);
            this.linkLoadImage.Name = "linkLoadImage";
            this.linkLoadImage.Size = new System.Drawing.Size(613, 20);
            this.linkLoadImage.TabIndex = 89;
            this.linkLoadImage.TabStop = true;
            this.linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkLoadImage.Value = "Load Sign";
            appearance37.ForeColor = System.Drawing.Color.Blue;
            this.linkLoadImage.VisitedLinkAppearance = appearance37;
            this.linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkLoadImage_LinkClicked);
            // 
            // linkRemovePicture
            // 
            this.linkRemovePicture.Location = new System.Drawing.Point(299, 340);
            this.linkRemovePicture.Name = "linkRemovePicture";
            this.linkRemovePicture.Size = new System.Drawing.Size(613, 20);
            this.linkRemovePicture.TabIndex = 86;
            this.linkRemovePicture.TabStop = true;
            this.linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkRemovePicture.Value = "Remove";
            appearance38.ForeColor = System.Drawing.Color.Blue;
            this.linkRemovePicture.VisitedLinkAppearance = appearance38;
            this.linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkRemovePicture_LinkClicked);
            // 
            // linkAddPicture
            // 
            this.linkAddPicture.Location = new System.Drawing.Point(299, 316);
            this.linkAddPicture.Name = "linkAddPicture";
            this.linkAddPicture.Size = new System.Drawing.Size(613, 20);
            this.linkAddPicture.TabIndex = 85;
            this.linkAddPicture.TabStop = true;
            this.linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkAddPicture.Value = "Add";
            appearance39.ForeColor = System.Drawing.Color.Blue;
            this.linkAddPicture.VisitedLinkAppearance = appearance39;
            this.linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkAddPicture_LinkClicked);
            // 
            // textBoxGraceDays
            // 
            this.textBoxGraceDays.AllowDecimal = false;
            this.textBoxGraceDays.CustomReportFieldName = "";
            this.textBoxGraceDays.CustomReportKey = "";
            this.textBoxGraceDays.CustomReportValueType = ((byte)(1));
            this.textBoxGraceDays.IsComboTextBox = false;
            this.textBoxGraceDays.IsModified = false;
            this.textBoxGraceDays.Location = new System.Drawing.Point(692, 292);
            this.textBoxGraceDays.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxGraceDays.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxGraceDays.Name = "textBoxGraceDays";
            this.textBoxGraceDays.NullText = "0";
            this.textBoxGraceDays.Size = new System.Drawing.Size(220, 20);
            this.textBoxGraceDays.TabIndex = 81;
            this.textBoxGraceDays.Text = "0";
            // 
            // dateTimePickerCLValidity
            // 
            this.dateTimePickerCLValidity.Checked = false;
            this.dateTimePickerCLValidity.CustomFormat = " ";
            this.dateTimePickerCLValidity.Enabled = false;
            this.dateTimePickerCLValidity.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerCLValidity.Location = new System.Drawing.Point(195, 268);
            this.dateTimePickerCLValidity.Name = "dateTimePickerCLValidity";
            this.dateTimePickerCLValidity.ShowCheckBox = true;
            this.dateTimePickerCLValidity.Size = new System.Drawing.Size(148, 20);
            this.dateTimePickerCLValidity.TabIndex = 5;
            this.dateTimePickerCLValidity.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxUnsecuredLimit
            // 
            this.textBoxUnsecuredLimit.AllowDecimal = true;
            this.textBoxUnsecuredLimit.BackColor = System.Drawing.Color.White;
            this.textBoxUnsecuredLimit.CustomReportFieldName = "";
            this.textBoxUnsecuredLimit.CustomReportKey = "";
            this.textBoxUnsecuredLimit.CustomReportValueType = ((byte)(1));
            this.textBoxUnsecuredLimit.Enabled = false;
            this.textBoxUnsecuredLimit.IsComboTextBox = false;
            this.textBoxUnsecuredLimit.IsModified = false;
            this.textBoxUnsecuredLimit.Location = new System.Drawing.Point(525, 268);
            this.textBoxUnsecuredLimit.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxUnsecuredLimit.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxUnsecuredLimit.Name = "textBoxUnsecuredLimit";
            this.textBoxUnsecuredLimit.NullText = "0";
            this.textBoxUnsecuredLimit.Size = new System.Drawing.Size(122, 20);
            this.textBoxUnsecuredLimit.TabIndex = 7;
            this.textBoxUnsecuredLimit.Text = "0.00";
            this.textBoxUnsecuredLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxUnsecuredLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // checkBoxUnsecuredLimit
            // 
            this.checkBoxUnsecuredLimit.Enabled = false;
            this.checkBoxUnsecuredLimit.Location = new System.Drawing.Point(347, 268);
            this.checkBoxUnsecuredLimit.Name = "checkBoxUnsecuredLimit";
            this.checkBoxUnsecuredLimit.Size = new System.Drawing.Size(174, 20);
            this.checkBoxUnsecuredLimit.TabIndex = 6;
            this.checkBoxUnsecuredLimit.Text = "Limit PDC Unsecured to:";
            this.checkBoxUnsecuredLimit.UseVisualStyleBackColor = true;
            this.checkBoxUnsecuredLimit.CheckedChanged += new System.EventHandler(this.checkBoxUnsecuredLimit_CheckedChanged);
            // 
            // textBoxTempLimit
            // 
            this.textBoxTempLimit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTempLimit.CustomReportFieldName = "";
            this.textBoxTempLimit.CustomReportKey = "";
            this.textBoxTempLimit.CustomReportValueType = ((byte)(1));
            this.textBoxTempLimit.IsComboTextBox = false;
            this.textBoxTempLimit.IsModified = false;
            this.textBoxTempLimit.Location = new System.Drawing.Point(818, 268);
            this.textBoxTempLimit.MaxLength = 30;
            this.textBoxTempLimit.Name = "textBoxTempLimit";
            this.textBoxTempLimit.ReadOnly = true;
            this.textBoxTempLimit.Size = new System.Drawing.Size(94, 20);
            this.textBoxTempLimit.TabIndex = 8;
            this.textBoxTempLimit.TabStop = false;
            this.textBoxTempLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxAcceptCheque
            // 
            this.checkBoxAcceptCheque.Checked = true;
            this.checkBoxAcceptCheque.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAcceptCheque.Location = new System.Drawing.Point(28, 292);
            this.checkBoxAcceptCheque.Name = "checkBoxAcceptCheque";
            this.checkBoxAcceptCheque.Size = new System.Drawing.Size(194, 20);
            this.checkBoxAcceptCheque.TabIndex = 9;
            this.checkBoxAcceptCheque.Text = "Accept cheque payment";
            this.checkBoxAcceptCheque.UseVisualStyleBackColor = true;
            // 
            // radioButtonSublimit
            // 
            this.radioButtonSublimit.Location = new System.Drawing.Point(345, 239);
            this.radioButtonSublimit.Name = "radioButtonSublimit";
            this.radioButtonSublimit.Size = new System.Drawing.Size(175, 25);
            this.radioButtonSublimit.TabIndex = 2;
            this.radioButtonSublimit.Text = "Sublimit of Parent";
            this.radioButtonSublimit.UseVisualStyleBackColor = true;
            // 
            // checkBoxAcceptPDC
            // 
            this.checkBoxAcceptPDC.Checked = true;
            this.checkBoxAcceptPDC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAcceptPDC.Location = new System.Drawing.Point(226, 292);
            this.checkBoxAcceptPDC.Name = "checkBoxAcceptPDC";
            this.checkBoxAcceptPDC.Size = new System.Drawing.Size(295, 20);
            this.checkBoxAcceptPDC.TabIndex = 10;
            this.checkBoxAcceptPDC.Text = "Accept post-dated cheque payment";
            this.checkBoxAcceptPDC.UseVisualStyleBackColor = true;
            // 
            // textBoxCreditLimit
            // 
            this.textBoxCreditLimit.AllowDecimal = true;
            this.textBoxCreditLimit.BackColor = System.Drawing.Color.White;
            this.textBoxCreditLimit.CustomReportFieldName = "";
            this.textBoxCreditLimit.CustomReportKey = "";
            this.textBoxCreditLimit.CustomReportValueType = ((byte)(1));
            this.textBoxCreditLimit.IsComboTextBox = false;
            this.textBoxCreditLimit.IsModified = false;
            this.textBoxCreditLimit.Location = new System.Drawing.Point(669, 239);
            this.textBoxCreditLimit.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxCreditLimit.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxCreditLimit.Name = "textBoxCreditLimit";
            this.textBoxCreditLimit.NullText = "0";
            this.textBoxCreditLimit.Size = new System.Drawing.Size(243, 25);
            this.textBoxCreditLimit.TabIndex = 4;
            this.textBoxCreditLimit.Text = "0.00";
            this.textBoxCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxCreditLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // radioButtonCreditLimitNoCredit
            // 
            this.radioButtonCreditLimitNoCredit.Checked = true;
            this.radioButtonCreditLimitNoCredit.Location = new System.Drawing.Point(188, 239);
            this.radioButtonCreditLimitNoCredit.Name = "radioButtonCreditLimitNoCredit";
            this.radioButtonCreditLimitNoCredit.Size = new System.Drawing.Size(153, 25);
            this.radioButtonCreditLimitNoCredit.TabIndex = 1;
            this.radioButtonCreditLimitNoCredit.TabStop = true;
            this.radioButtonCreditLimitNoCredit.Text = "No Credit";
            this.radioButtonCreditLimitNoCredit.UseVisualStyleBackColor = true;
            // 
            // radioButtonCreditLimitUnlimited
            // 
            this.radioButtonCreditLimitUnlimited.Location = new System.Drawing.Point(28, 239);
            this.radioButtonCreditLimitUnlimited.Name = "radioButtonCreditLimitUnlimited";
            this.radioButtonCreditLimitUnlimited.Size = new System.Drawing.Size(156, 25);
            this.radioButtonCreditLimitUnlimited.TabIndex = 0;
            this.radioButtonCreditLimitUnlimited.Text = "Unlimited";
            this.radioButtonCreditLimitUnlimited.UseVisualStyleBackColor = true;
            // 
            // radioButtonCreditLimitAmount
            // 
            this.radioButtonCreditLimitAmount.Location = new System.Drawing.Point(524, 239);
            this.radioButtonCreditLimitAmount.Name = "radioButtonCreditLimitAmount";
            this.radioButtonCreditLimitAmount.Size = new System.Drawing.Size(141, 25);
            this.radioButtonCreditLimitAmount.TabIndex = 3;
            this.radioButtonCreditLimitAmount.Text = "Amount of:";
            this.radioButtonCreditLimitAmount.UseVisualStyleBackColor = true;
            this.radioButtonCreditLimitAmount.CheckedChanged += new System.EventHandler(this.radioButtonCreditLimitAmount_CheckedChanged);
            // 
            // comboBoxCollectionUser
            // 
            this.comboBoxCollectionUser.Assigned = false;
            this.comboBoxCollectionUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxCollectionUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCollectionUser.CustomReportFieldName = "";
            this.comboBoxCollectionUser.CustomReportKey = "";
            this.comboBoxCollectionUser.CustomReportValueType = ((byte)(1));
            this.comboBoxCollectionUser.DescriptionTextBox = null;
            appearance40.BackColor = System.Drawing.SystemColors.Window;
            appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCollectionUser.DisplayLayout.Appearance = appearance40;
            this.comboBoxCollectionUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCollectionUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance41.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCollectionUser.DisplayLayout.GroupByBox.Appearance = appearance41;
            appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCollectionUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
            this.comboBoxCollectionUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance43.BackColor2 = System.Drawing.SystemColors.Control;
            appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCollectionUser.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
            this.comboBoxCollectionUser.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCollectionUser.DisplayLayout.MaxRowScrollRegions = 1;
            appearance44.BackColor = System.Drawing.SystemColors.Window;
            appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCollectionUser.DisplayLayout.Override.ActiveCellAppearance = appearance44;
            appearance45.BackColor = System.Drawing.SystemColors.Highlight;
            appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCollectionUser.DisplayLayout.Override.ActiveRowAppearance = appearance45;
            this.comboBoxCollectionUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCollectionUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance46.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCollectionUser.DisplayLayout.Override.CardAreaAppearance = appearance46;
            appearance47.BorderColor = System.Drawing.Color.Silver;
            appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCollectionUser.DisplayLayout.Override.CellAppearance = appearance47;
            this.comboBoxCollectionUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCollectionUser.DisplayLayout.Override.CellPadding = 0;
            appearance48.BackColor = System.Drawing.SystemColors.Control;
            appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance48.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCollectionUser.DisplayLayout.Override.GroupByRowAppearance = appearance48;
            appearance49.TextHAlignAsString = "Left";
            this.comboBoxCollectionUser.DisplayLayout.Override.HeaderAppearance = appearance49;
            this.comboBoxCollectionUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCollectionUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance50.BackColor = System.Drawing.SystemColors.Window;
            appearance50.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCollectionUser.DisplayLayout.Override.RowAppearance = appearance50;
            this.comboBoxCollectionUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCollectionUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
            this.comboBoxCollectionUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCollectionUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCollectionUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCollectionUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCollectionUser.Editable = true;
            this.comboBoxCollectionUser.FilterString = "";
            this.comboBoxCollectionUser.HasAllAccount = false;
            this.comboBoxCollectionUser.HasCustom = false;
            this.comboBoxCollectionUser.IsDataLoaded = false;
            this.comboBoxCollectionUser.Location = new System.Drawing.Point(191, 117);
            this.comboBoxCollectionUser.MaxDropDownItems = 12;
            this.comboBoxCollectionUser.Name = "comboBoxCollectionUser";
            this.comboBoxCollectionUser.ShowInactiveItems = false;
            this.comboBoxCollectionUser.ShowQuickAdd = true;
            this.comboBoxCollectionUser.Size = new System.Drawing.Size(725, 20);
            this.comboBoxCollectionUser.TabIndex = 6;
            this.comboBoxCollectionUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // dateTimePickerReviewDate
            // 
            this.dateTimePickerReviewDate.Checked = false;
            this.dateTimePickerReviewDate.CustomFormat = " ";
            this.dateTimePickerReviewDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerReviewDate.Location = new System.Drawing.Point(191, 93);
            this.dateTimePickerReviewDate.Name = "dateTimePickerReviewDate";
            this.dateTimePickerReviewDate.ShowCheckBox = true;
            this.dateTimePickerReviewDate.Size = new System.Drawing.Size(268, 20);
            this.dateTimePickerReviewDate.TabIndex = 4;
            this.dateTimePickerReviewDate.Value = new System.DateTime(((long)(0)));
            // 
            // dataGridContacts
            // 
            this.dataGridContacts.AllowAddNew = false;
            this.dataGridContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance52.BackColor = System.Drawing.SystemColors.Window;
            appearance52.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridContacts.DisplayLayout.Appearance = appearance52;
            this.dataGridContacts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridContacts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance53.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance53.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridContacts.DisplayLayout.GroupByBox.Appearance = appearance53;
            appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridContacts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance54;
            this.dataGridContacts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance55.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance55.BackColor2 = System.Drawing.SystemColors.Control;
            appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridContacts.DisplayLayout.GroupByBox.PromptAppearance = appearance55;
            this.dataGridContacts.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridContacts.DisplayLayout.MaxRowScrollRegions = 1;
            appearance56.BackColor = System.Drawing.SystemColors.Window;
            appearance56.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridContacts.DisplayLayout.Override.ActiveCellAppearance = appearance56;
            appearance57.BackColor = System.Drawing.SystemColors.Highlight;
            appearance57.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridContacts.DisplayLayout.Override.ActiveRowAppearance = appearance57;
            this.dataGridContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridContacts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridContacts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance58.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridContacts.DisplayLayout.Override.CardAreaAppearance = appearance58;
            appearance59.BorderColor = System.Drawing.Color.Silver;
            appearance59.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridContacts.DisplayLayout.Override.CellAppearance = appearance59;
            this.dataGridContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridContacts.DisplayLayout.Override.CellPadding = 0;
            appearance60.BackColor = System.Drawing.SystemColors.Control;
            appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance60.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance60.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridContacts.DisplayLayout.Override.GroupByRowAppearance = appearance60;
            appearance61.TextHAlignAsString = "Left";
            this.dataGridContacts.DisplayLayout.Override.HeaderAppearance = appearance61;
            this.dataGridContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridContacts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance62.BackColor = System.Drawing.SystemColors.Window;
            appearance62.BorderColor = System.Drawing.Color.Silver;
            this.dataGridContacts.DisplayLayout.Override.RowAppearance = appearance62;
            this.dataGridContacts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance63.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridContacts.DisplayLayout.Override.TemplateAddRowAppearance = appearance63;
            this.dataGridContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridContacts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridContacts.IncludeLotItems = false;
            this.dataGridContacts.LoadLayoutFailed = false;
            this.dataGridContacts.Location = new System.Drawing.Point(24, 61);
            this.dataGridContacts.Name = "dataGridContacts";
            this.dataGridContacts.ShowClearMenu = true;
            this.dataGridContacts.ShowDeleteMenu = true;
            this.dataGridContacts.ShowInsertMenu = true;
            this.dataGridContacts.ShowMoveRowsMenu = true;
            this.dataGridContacts.Size = new System.Drawing.Size(892, 281);
            this.dataGridContacts.TabIndex = 0;
            this.dataGridContacts.Text = "dataEntryGrid1";
            // 
            // gridComboBoxContact
            // 
            this.gridComboBoxContact.Assigned = false;
            this.gridComboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.gridComboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.gridComboBoxContact.CustomReportFieldName = "";
            this.gridComboBoxContact.CustomReportKey = "";
            this.gridComboBoxContact.CustomReportValueType = ((byte)(1));
            this.gridComboBoxContact.DescriptionTextBox = null;
            appearance64.BackColor = System.Drawing.SystemColors.Window;
            appearance64.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridComboBoxContact.DisplayLayout.Appearance = appearance64;
            this.gridComboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridComboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance65.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance65.BorderColor = System.Drawing.SystemColors.Window;
            this.gridComboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance65;
            appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridComboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance66;
            this.gridComboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance67.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance67.BackColor2 = System.Drawing.SystemColors.Control;
            appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridComboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance67;
            this.gridComboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
            this.gridComboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
            appearance68.BackColor = System.Drawing.SystemColors.Window;
            appearance68.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridComboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance68;
            appearance69.BackColor = System.Drawing.SystemColors.Highlight;
            appearance69.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridComboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance69;
            this.gridComboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridComboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance70.BackColor = System.Drawing.SystemColors.Window;
            this.gridComboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance70;
            appearance71.BorderColor = System.Drawing.Color.Silver;
            appearance71.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridComboBoxContact.DisplayLayout.Override.CellAppearance = appearance71;
            this.gridComboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridComboBoxContact.DisplayLayout.Override.CellPadding = 0;
            appearance72.BackColor = System.Drawing.SystemColors.Control;
            appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance72.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance72.BorderColor = System.Drawing.SystemColors.Window;
            this.gridComboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance72;
            appearance73.TextHAlignAsString = "Left";
            this.gridComboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance73;
            this.gridComboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridComboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance74.BackColor = System.Drawing.SystemColors.Window;
            appearance74.BorderColor = System.Drawing.Color.Silver;
            this.gridComboBoxContact.DisplayLayout.Override.RowAppearance = appearance74;
            this.gridComboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance75.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridComboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance75;
            this.gridComboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridComboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridComboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.gridComboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.gridComboBoxContact.Editable = true;
            this.gridComboBoxContact.FilterString = "";
            this.gridComboBoxContact.HasAllAccount = false;
            this.gridComboBoxContact.HasCustom = false;
            this.gridComboBoxContact.IsDataLoaded = false;
            this.gridComboBoxContact.Location = new System.Drawing.Point(24, 346);
            this.gridComboBoxContact.MaxDropDownItems = 12;
            this.gridComboBoxContact.Name = "gridComboBoxContact";
            this.gridComboBoxContact.ShowInactiveItems = false;
            this.gridComboBoxContact.ShowQuickAdd = true;
            this.gridComboBoxContact.Size = new System.Drawing.Size(892, 20);
            this.gridComboBoxContact.TabIndex = 356;
            this.gridComboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.gridComboBoxContact.Visible = false;
            // 
            // comboBoxActivityPeriod
            // 
            this.comboBoxActivityPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxActivityPeriod.Location = new System.Drawing.Point(788, 45);
            this.comboBoxActivityPeriod.Name = "comboBoxActivityPeriod";
            this.comboBoxActivityPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxActivityPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxActivityPeriod.Size = new System.Drawing.Size(128, 20);
            this.comboBoxActivityPeriod.StyleController = this.layoutControl1;
            this.comboBoxActivityPeriod.TabIndex = 361;
            this.comboBoxActivityPeriod.SelectedIndexChanged += new System.EventHandler(this.comboBoxActivityPeriod_SelectedIndexChanged);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textBoxAddressPrintFormat);
            this.layoutControl1.Controls.Add(this.udfEntryGrid);
            this.layoutControl1.Controls.Add(this.textBoxNote);
            this.layoutControl1.Controls.Add(this.textBoxProfileDetails);
            this.layoutControl1.Controls.Add(this.dataGridActivities);
            this.layoutControl1.Controls.Add(this.comboBoxActivityPeriod);
            this.layoutControl1.Controls.Add(this.buttonAddActivity);
            this.layoutControl1.Controls.Add(this.gridComboBoxContact);
            this.layoutControl1.Controls.Add(this.dataGridContacts);
            this.layoutControl1.Controls.Add(this.dateTimePickerValidTo);
            this.layoutControl1.Controls.Add(this.textBoxProvider);
            this.layoutControl1.Controls.Add(this.comboBoxInsuranceProvider);
            this.layoutControl1.Controls.Add(this.datetimePickerEffectiveDate);
            this.layoutControl1.Controls.Add(this.textBoxInsuranceRemarks);
            this.layoutControl1.Controls.Add(this.buttonCustomerInsuranceClaim);
            this.layoutControl1.Controls.Add(this.linkLoadImage);
            this.layoutControl1.Controls.Add(this.textBoxInsuranceID);
            this.layoutControl1.Controls.Add(this.textBoxConfirmationLevel);
            this.layoutControl1.Controls.Add(this.comboBoxInsuranceRating);
            this.layoutControl1.Controls.Add(this.comboBoxInsuranceStatus);
            this.layoutControl1.Controls.Add(this.linkRemovePicture);
            this.layoutControl1.Controls.Add(this.linkAddPicture);
            this.layoutControl1.Controls.Add(this.textBoxRatingRemarks);
            this.layoutControl1.Controls.Add(this.textBoxInsuranceApprovedAmount);
            this.layoutControl1.Controls.Add(this.textBoxInsuranceNumber);
            this.layoutControl1.Controls.Add(this.pictureBoxPhoto);
            this.layoutControl1.Controls.Add(this.textBoxInsuranceReqAmount);
            this.layoutControl1.Controls.Add(this.dateTimeBalanceConfirmationDate);
            this.layoutControl1.Controls.Add(this.textBoxConsignCommission);
            this.layoutControl1.Controls.Add(this.dateTimePickerInsuranceDate);
            this.layoutControl1.Controls.Add(this.textBoxDeliveryInstructions);
            this.layoutControl1.Controls.Add(this.textBoxGraceDays);
            this.layoutControl1.Controls.Add(this.textBoxAccountInstructions);
            this.layoutControl1.Controls.Add(this.dateTimePickerCLValidity);
            this.layoutControl1.Controls.Add(this.textBoxUnsecuredLimit);
            this.layoutControl1.Controls.Add(this.checkBoxAcceptPDC);
            this.layoutControl1.Controls.Add(this.checkBoxAcceptCheque);
            this.layoutControl1.Controls.Add(this.textBoxTempLimit);
            this.layoutControl1.Controls.Add(this.comboBoxRatingBy);
            this.layoutControl1.Controls.Add(this.checkBoxUnsecuredLimit);
            this.layoutControl1.Controls.Add(this.comboBoxCreditReviewBy);
            this.layoutControl1.Controls.Add(this.comboBoxTaxGroup);
            this.layoutControl1.Controls.Add(this.dateTimePickerRatingDate);
            this.layoutControl1.Controls.Add(this.buttonAccounts);
            this.layoutControl1.Controls.Add(this.textBoxTaxIDNumber);
            this.layoutControl1.Controls.Add(this.textBoxLongitude);
            this.layoutControl1.Controls.Add(this.radioButtonSublimit);
            this.layoutControl1.Controls.Add(this.textBoxBankBranch);
            this.layoutControl1.Controls.Add(this.comboBoxTaxOption);
            this.layoutControl1.Controls.Add(this.textBoxCreditLimit);
            this.layoutControl1.Controls.Add(this.comboBoxRating);
            this.layoutControl1.Controls.Add(this.radioButtonCreditLimitAmount);
            this.layoutControl1.Controls.Add(this.radioButtonCreditLimitNoCredit);
            this.layoutControl1.Controls.Add(this.textBoxBankName);
            this.layoutControl1.Controls.Add(this.radioButtonCreditLimitUnlimited);
            this.layoutControl1.Controls.Add(this.textBoxPaymentTermName);
            this.layoutControl1.Controls.Add(this.textBoxLicenseNumber);
            this.layoutControl1.Controls.Add(this.textBoxPaymentMethodName);
            this.layoutControl1.Controls.Add(this.comboBoxCollectionUser);
            this.layoutControl1.Controls.Add(this.textBoxBankAccountNumber);
            this.layoutControl1.Controls.Add(this.textBoxRebatePercent);
            this.layoutControl1.Controls.Add(this.comboBoxCustomerGroup);
            this.layoutControl1.Controls.Add(this.comboBoxPaymentTerms);
            this.layoutControl1.Controls.Add(this.ultraPictureBox1);
            this.layoutControl1.Controls.Add(this.dateTimePickerReviewDate);
            this.layoutControl1.Controls.Add(this.textBoxDiscountPercent);
            this.layoutControl1.Controls.Add(this.buttonCategories);
            this.layoutControl1.Controls.Add(this.comboBoxPaymentMethods);
            this.layoutControl1.Controls.Add(this.textBoxLatitude);
            this.layoutControl1.Controls.Add(this.buttonMoreAddress);
            this.layoutControl1.Controls.Add(this.comboBoxCurrency);
            this.layoutControl1.Controls.Add(this.checkBoxparentACforposting);
            this.layoutControl1.Controls.Add(this.dateTimePickerContractExpDate);
            this.layoutControl1.Controls.Add(this.dateTimePickerLicenseExpDate);
            this.layoutControl1.Controls.Add(this.comboBoxSalesperson);
            this.layoutControl1.Controls.Add(this.textBoxStatementEmail);
            this.layoutControl1.Controls.Add(this.comboBoxStatementMethod);
            this.layoutControl1.Controls.Add(this.textBoxComment);
            this.layoutControl1.Controls.Add(this.textBoxCode);
            this.layoutControl1.Controls.Add(this.checkBoxWeightInvoice);
            this.layoutControl1.Controls.Add(this.checkBoxAllowConsignment);
            this.layoutControl1.Controls.Add(this.textBoxDepartment);
            this.layoutControl1.Controls.Add(this.textBoxWebsite);
            this.layoutControl1.Controls.Add(this.textBoxName);
            this.layoutControl1.Controls.Add(this.comboBoxLeadSource);
            this.layoutControl1.Controls.Add(this.comboBoxPriceLevel);
            this.layoutControl1.Controls.Add(this.textBoxFormalName);
            this.layoutControl1.Controls.Add(this.comboBoxArea);
            this.layoutControl1.Controls.Add(this.textBoxForeignName);
            this.layoutControl1.Controls.Add(this.dateTimePickerEstablished);
            this.layoutControl1.Controls.Add(this.textBoxEmail);
            this.layoutControl1.Controls.Add(this.dateTimePickerCustomerSince);
            this.layoutControl1.Controls.Add(this.comboBoxCountry);
            this.layoutControl1.Controls.Add(this.textBoxMobile);
            this.layoutControl1.Controls.Add(this.comboBoxBilltoAddress);
            this.layoutControl1.Controls.Add(this.comboBoxParentCustomer);
            this.layoutControl1.Controls.Add(this.comboBoxShippingMethods);
            this.layoutControl1.Controls.Add(this.textBoxPostalCode);
            this.layoutControl1.Controls.Add(this.textBoxFax);
            this.layoutControl1.Controls.Add(this.checkBoxIsInactive);
            this.layoutControl1.Controls.Add(this.comboBoxShiptoAddress);
            this.layoutControl1.Controls.Add(this.checkBoxHold);
            this.layoutControl1.Controls.Add(this.textBoxPhone2);
            this.layoutControl1.Controls.Add(this.comboBoxCustomerClass);
            this.layoutControl1.Controls.Add(this.textBoxAddressID);
            this.layoutControl1.Controls.Add(this.textBoxPhone1);
            this.layoutControl1.Controls.Add(this.textBoxContactName);
            this.layoutControl1.Controls.Add(this.textBoxAddress1);
            this.layoutControl1.Controls.Add(this.textBoxAddress2);
            this.layoutControl1.Controls.Add(this.textBoxAddress3);
            this.layoutControl1.Controls.Add(this.textBoxCity);
            this.layoutControl1.Controls.Add(this.textBoxState);
            this.layoutControl1.Controls.Add(this.textBoxCountry);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 61);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(486, 0, 880, 788);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(938, 648);
            this.layoutControl1.TabIndex = 308;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textBoxAddressPrintFormat
            // 
            this.textBoxAddressPrintFormat.BackColor = System.Drawing.Color.White;
            this.textBoxAddressPrintFormat.CustomReportFieldName = "";
            this.textBoxAddressPrintFormat.CustomReportKey = "";
            this.textBoxAddressPrintFormat.CustomReportValueType = ((byte)(1));
            this.textBoxAddressPrintFormat.IsComboTextBox = false;
            this.textBoxAddressPrintFormat.IsModified = false;
            this.textBoxAddressPrintFormat.Location = new System.Drawing.Point(204, 527);
            this.textBoxAddressPrintFormat.MaxLength = 255;
            this.textBoxAddressPrintFormat.Multiline = true;
            this.textBoxAddressPrintFormat.Name = "textBoxAddressPrintFormat";
            this.textBoxAddressPrintFormat.Size = new System.Drawing.Size(252, 94);
            this.textBoxAddressPrintFormat.TabIndex = 364;
            // 
            // udfEntryGrid
            // 
            this.udfEntryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udfEntryGrid.Location = new System.Drawing.Point(24, 45);
            this.udfEntryGrid.Margin = new System.Windows.Forms.Padding(4);
            this.udfEntryGrid.Name = "udfEntryGrid";
            this.udfEntryGrid.Size = new System.Drawing.Size(892, 599);
            this.udfEntryGrid.TabIndex = 0;
            this.udfEntryGrid.TableName = "";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.BackColor = System.Drawing.Color.White;
            this.textBoxNote.CustomReportFieldName = "";
            this.textBoxNote.CustomReportKey = "";
            this.textBoxNote.CustomReportValueType = ((byte)(1));
            this.textBoxNote.IsComboTextBox = false;
            this.textBoxNote.IsModified = false;
            this.textBoxNote.Location = new System.Drawing.Point(24, 45);
            this.textBoxNote.MaxLength = 5000;
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(892, 599);
            this.textBoxNote.TabIndex = 43;
            // 
            // textBoxProfileDetails
            // 
            this.textBoxProfileDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProfileDetails.Location = new System.Drawing.Point(24, 61);
            this.textBoxProfileDetails.Name = "textBoxProfileDetails";
            this.textBoxProfileDetails.Size = new System.Drawing.Size(892, 583);
            this.textBoxProfileDetails.TabIndex = 19;
            // 
            // dataGridActivities
            // 
            this.dataGridActivities.AllowUnfittedView = false;
            this.dataGridActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance76.BackColor = System.Drawing.SystemColors.Window;
            appearance76.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridActivities.DisplayLayout.Appearance = appearance76;
            this.dataGridActivities.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridActivities.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance77.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance77.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridActivities.DisplayLayout.GroupByBox.Appearance = appearance77;
            appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridActivities.DisplayLayout.GroupByBox.BandLabelAppearance = appearance78;
            this.dataGridActivities.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance79.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance79.BackColor2 = System.Drawing.SystemColors.Control;
            appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridActivities.DisplayLayout.GroupByBox.PromptAppearance = appearance79;
            this.dataGridActivities.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridActivities.DisplayLayout.MaxRowScrollRegions = 1;
            appearance80.BackColor = System.Drawing.SystemColors.Window;
            appearance80.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridActivities.DisplayLayout.Override.ActiveCellAppearance = appearance80;
            appearance81.BackColor = System.Drawing.SystemColors.Highlight;
            appearance81.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridActivities.DisplayLayout.Override.ActiveRowAppearance = appearance81;
            this.dataGridActivities.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridActivities.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance82.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridActivities.DisplayLayout.Override.CardAreaAppearance = appearance82;
            appearance83.BorderColor = System.Drawing.Color.Silver;
            appearance83.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridActivities.DisplayLayout.Override.CellAppearance = appearance83;
            this.dataGridActivities.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridActivities.DisplayLayout.Override.CellPadding = 0;
            appearance84.BackColor = System.Drawing.SystemColors.Control;
            appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance84.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance84.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridActivities.DisplayLayout.Override.GroupByRowAppearance = appearance84;
            appearance85.TextHAlignAsString = "Left";
            this.dataGridActivities.DisplayLayout.Override.HeaderAppearance = appearance85;
            this.dataGridActivities.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridActivities.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance86.BackColor = System.Drawing.SystemColors.Window;
            appearance86.BorderColor = System.Drawing.Color.Silver;
            this.dataGridActivities.DisplayLayout.Override.RowAppearance = appearance86;
            this.dataGridActivities.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance87.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridActivities.DisplayLayout.Override.TemplateAddRowAppearance = appearance87;
            this.dataGridActivities.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridActivities.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridActivities.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridActivities.LoadLayoutFailed = false;
            this.dataGridActivities.Location = new System.Drawing.Point(24, 69);
            this.dataGridActivities.Name = "dataGridActivities";
            this.dataGridActivities.ShowDeleteMenu = false;
            this.dataGridActivities.ShowMinusInRed = true;
            this.dataGridActivities.ShowNewMenu = false;
            this.dataGridActivities.Size = new System.Drawing.Size(892, 376);
            this.dataGridActivities.TabIndex = 360;
            this.dataGridActivities.Text = "dataGridList1";
            // 
            // buttonAddActivity
            // 
            this.buttonAddActivity.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.buttonAddActivity.Location = new System.Drawing.Point(24, 45);
            this.buttonAddActivity.Name = "buttonAddActivity";
            this.buttonAddActivity.Size = new System.Drawing.Size(295, 20);
            this.buttonAddActivity.TabIndex = 363;
            this.buttonAddActivity.UseVisualStyleBackColor = true;
            this.buttonAddActivity.Click += new System.EventHandler(this.buttonAddActivity_Click);
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPhoto.InitialImage = global::Micromind.ClientUI.Properties.Resources.noimage;
            this.pictureBoxPhoto.Location = new System.Drawing.Point(195, 316);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(100, 68);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPhoto.TabIndex = 84;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // textBoxConsignCommission
            // 
            this.textBoxConsignCommission.BackColor = System.Drawing.Color.White;
            this.textBoxConsignCommission.CustomReportFieldName = "";
            this.textBoxConsignCommission.CustomReportKey = "";
            this.textBoxConsignCommission.CustomReportValueType = ((byte)(1));
            this.textBoxConsignCommission.IsComboTextBox = false;
            this.textBoxConsignCommission.IsModified = false;
            this.textBoxConsignCommission.Location = new System.Drawing.Point(574, 180);
            this.textBoxConsignCommission.Name = "textBoxConsignCommission";
            this.textBoxConsignCommission.Size = new System.Drawing.Size(118, 20);
            this.textBoxConsignCommission.TabIndex = 0;
            this.textBoxConsignCommission.Text = "0.00";
            this.textBoxConsignCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxDeliveryInstructions
            // 
            this.textBoxDeliveryInstructions.BackColor = System.Drawing.Color.White;
            this.textBoxDeliveryInstructions.CustomReportFieldName = "";
            this.textBoxDeliveryInstructions.CustomReportKey = "";
            this.textBoxDeliveryInstructions.CustomReportValueType = ((byte)(1));
            this.textBoxDeliveryInstructions.IsComboTextBox = false;
            this.textBoxDeliveryInstructions.IsModified = false;
            this.textBoxDeliveryInstructions.Location = new System.Drawing.Point(195, 428);
            this.textBoxDeliveryInstructions.MaxLength = 500;
            this.textBoxDeliveryInstructions.Multiline = true;
            this.textBoxDeliveryInstructions.Name = "textBoxDeliveryInstructions";
            this.textBoxDeliveryInstructions.Size = new System.Drawing.Size(708, 64);
            this.textBoxDeliveryInstructions.TabIndex = 0;
            // 
            // textBoxAccountInstructions
            // 
            this.textBoxAccountInstructions.BackColor = System.Drawing.Color.White;
            this.textBoxAccountInstructions.CustomReportFieldName = "";
            this.textBoxAccountInstructions.CustomReportKey = "";
            this.textBoxAccountInstructions.CustomReportValueType = ((byte)(1));
            this.textBoxAccountInstructions.IsComboTextBox = false;
            this.textBoxAccountInstructions.IsModified = false;
            this.textBoxAccountInstructions.Location = new System.Drawing.Point(195, 496);
            this.textBoxAccountInstructions.MaxLength = 500;
            this.textBoxAccountInstructions.Multiline = true;
            this.textBoxAccountInstructions.Name = "textBoxAccountInstructions";
            this.textBoxAccountInstructions.Size = new System.Drawing.Size(708, 63);
            this.textBoxAccountInstructions.TabIndex = 1;
            // 
            // comboBoxTaxGroup
            // 
            this.comboBoxTaxGroup.Assigned = false;
            this.comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxTaxGroup.CustomReportFieldName = "";
            this.comboBoxTaxGroup.CustomReportKey = "";
            this.comboBoxTaxGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxTaxGroup.DescriptionTextBox = null;
            appearance88.BackColor = System.Drawing.SystemColors.Window;
            appearance88.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxTaxGroup.DisplayLayout.Appearance = appearance88;
            this.comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance89.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance89.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance89;
            appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance90;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance91.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance91.BackColor2 = System.Drawing.SystemColors.Control;
            appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance91;
            this.comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance92.BackColor = System.Drawing.SystemColors.Window;
            appearance92.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance92;
            appearance93.BackColor = System.Drawing.SystemColors.Highlight;
            appearance93.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance93;
            this.comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance94.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance94;
            appearance95.BorderColor = System.Drawing.Color.Silver;
            appearance95.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance95;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
            appearance96.BackColor = System.Drawing.SystemColors.Control;
            appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance96.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance96.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance96;
            appearance97.TextHAlignAsString = "Left";
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance97;
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance98.BackColor = System.Drawing.SystemColors.Window;
            appearance98.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance98;
            this.comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance99.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance99;
            this.comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxTaxGroup.Editable = true;
            this.comboBoxTaxGroup.FilterString = "";
            this.comboBoxTaxGroup.HasAllAccount = false;
            this.comboBoxTaxGroup.HasCustom = false;
            this.comboBoxTaxGroup.IsDataLoaded = false;
            this.comboBoxTaxGroup.Location = new System.Drawing.Point(656, 334);
            this.comboBoxTaxGroup.MaxDropDownItems = 12;
            this.comboBoxTaxGroup.Name = "comboBoxTaxGroup";
            this.comboBoxTaxGroup.ReadOnly = true;
            this.comboBoxTaxGroup.ShowInactiveItems = false;
            this.comboBoxTaxGroup.ShowQuickAdd = true;
            this.comboBoxTaxGroup.Size = new System.Drawing.Size(247, 20);
            this.comboBoxTaxGroup.TabIndex = 1;
            this.comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // buttonAccounts
            // 
            this.buttonAccounts.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonAccounts.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAccounts.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonAccounts.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonAccounts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAccounts.Location = new System.Drawing.Point(703, 563);
            this.buttonAccounts.Name = "buttonAccounts";
            this.buttonAccounts.Size = new System.Drawing.Size(200, 39);
            this.buttonAccounts.TabIndex = 17;
            this.buttonAccounts.Text = "&Accounts...";
            this.buttonAccounts.UseVisualStyleBackColor = false;
            this.buttonAccounts.Click += new System.EventHandler(this.buttonAccounts_Click);
            // 
            // textBoxTaxIDNumber
            // 
            this.textBoxTaxIDNumber.BackColor = System.Drawing.Color.White;
            this.textBoxTaxIDNumber.CustomReportFieldName = "";
            this.textBoxTaxIDNumber.CustomReportKey = "";
            this.textBoxTaxIDNumber.CustomReportValueType = ((byte)(1));
            this.textBoxTaxIDNumber.IsComboTextBox = false;
            this.textBoxTaxIDNumber.IsModified = false;
            this.textBoxTaxIDNumber.Location = new System.Drawing.Point(656, 358);
            this.textBoxTaxIDNumber.MaxLength = 75;
            this.textBoxTaxIDNumber.Name = "textBoxTaxIDNumber";
            this.textBoxTaxIDNumber.Size = new System.Drawing.Size(247, 20);
            this.textBoxTaxIDNumber.TabIndex = 2;
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.BackColor = System.Drawing.Color.White;
            this.textBoxLongitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxLongitude.CustomReportFieldName = "";
            this.textBoxLongitude.CustomReportKey = "";
            this.textBoxLongitude.CustomReportValueType = ((byte)(1));
            this.textBoxLongitude.IsComboTextBox = false;
            this.textBoxLongitude.IsModified = false;
            this.textBoxLongitude.Location = new System.Drawing.Point(627, 479);
            this.textBoxLongitude.MaxLength = 64;
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(156, 20);
            this.textBoxLongitude.TabIndex = 42;
            this.textBoxLongitude.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLongitude_MouseClick);
            this.textBoxLongitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLongitude_KeyDown);
            this.textBoxLongitude.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxLongitude_KeyUp);
            this.textBoxLongitude.MouseLeave += new System.EventHandler(this.textBoxLongitude_MouseLeave);
            // 
            // textBoxBankBranch
            // 
            this.textBoxBankBranch.BackColor = System.Drawing.Color.White;
            this.textBoxBankBranch.CustomReportFieldName = "";
            this.textBoxBankBranch.CustomReportKey = "";
            this.textBoxBankBranch.CustomReportValueType = ((byte)(1));
            this.textBoxBankBranch.IsComboTextBox = false;
            this.textBoxBankBranch.IsModified = false;
            this.textBoxBankBranch.Location = new System.Drawing.Point(195, 357);
            this.textBoxBankBranch.MaxLength = 30;
            this.textBoxBankBranch.Name = "textBoxBankBranch";
            this.textBoxBankBranch.Size = new System.Drawing.Size(264, 21);
            this.textBoxBankBranch.TabIndex = 2;
            // 
            // comboBoxTaxOption
            // 
            this.comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBoxTaxOption.FormattingEnabled = true;
            this.comboBoxTaxOption.Items.AddRange(new object[] {
            "Based on Class",
            "Taxable",
            "NonTaxable"});
            this.comboBoxTaxOption.Location = new System.Drawing.Point(656, 309);
            this.comboBoxTaxOption.Name = "comboBoxTaxOption";
            this.comboBoxTaxOption.Size = new System.Drawing.Size(247, 21);
            this.comboBoxTaxOption.TabIndex = 0;
            this.comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaxOption_SelectedIndexChanged);
            // 
            // textBoxBankName
            // 
            this.textBoxBankName.BackColor = System.Drawing.Color.White;
            this.textBoxBankName.CustomReportFieldName = "";
            this.textBoxBankName.CustomReportKey = "";
            this.textBoxBankName.CustomReportValueType = ((byte)(1));
            this.textBoxBankName.IsComboTextBox = false;
            this.textBoxBankName.IsModified = false;
            this.textBoxBankName.Location = new System.Drawing.Point(195, 309);
            this.textBoxBankName.MaxLength = 30;
            this.textBoxBankName.Name = "textBoxBankName";
            this.textBoxBankName.Size = new System.Drawing.Size(264, 20);
            this.textBoxBankName.TabIndex = 0;
            // 
            // textBoxLicenseNumber
            // 
            this.textBoxLicenseNumber.BackColor = System.Drawing.Color.White;
            this.textBoxLicenseNumber.CustomReportFieldName = "";
            this.textBoxLicenseNumber.CustomReportKey = "";
            this.textBoxLicenseNumber.CustomReportValueType = ((byte)(1));
            this.textBoxLicenseNumber.IsComboTextBox = false;
            this.textBoxLicenseNumber.IsModified = false;
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(191, 204);
            this.textBoxLicenseNumber.MaxLength = 30;
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(212, 20);
            this.textBoxLicenseNumber.TabIndex = 11;
            // 
            // textBoxBankAccountNumber
            // 
            this.textBoxBankAccountNumber.BackColor = System.Drawing.Color.White;
            this.textBoxBankAccountNumber.CustomReportFieldName = "";
            this.textBoxBankAccountNumber.CustomReportKey = "";
            this.textBoxBankAccountNumber.CustomReportValueType = ((byte)(1));
            this.textBoxBankAccountNumber.IsComboTextBox = false;
            this.textBoxBankAccountNumber.IsModified = false;
            this.textBoxBankAccountNumber.Location = new System.Drawing.Point(195, 333);
            this.textBoxBankAccountNumber.MaxLength = 30;
            this.textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
            this.textBoxBankAccountNumber.Size = new System.Drawing.Size(264, 20);
            this.textBoxBankAccountNumber.TabIndex = 1;
            // 
            // textBoxRebatePercent
            // 
            this.textBoxRebatePercent.BackColor = System.Drawing.Color.White;
            this.textBoxRebatePercent.CustomReportFieldName = "";
            this.textBoxRebatePercent.CustomReportKey = "";
            this.textBoxRebatePercent.CustomReportValueType = ((byte)(1));
            this.textBoxRebatePercent.IsComboTextBox = false;
            this.textBoxRebatePercent.IsModified = false;
            this.textBoxRebatePercent.Location = new System.Drawing.Point(356, 252);
            this.textBoxRebatePercent.Name = "textBoxRebatePercent";
            this.textBoxRebatePercent.Size = new System.Drawing.Size(120, 20);
            this.textBoxRebatePercent.TabIndex = 15;
            this.textBoxRebatePercent.Text = "0.00";
            this.textBoxRebatePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBoxCustomerGroup
            // 
            this.comboBoxCustomerGroup.Assigned = false;
            this.comboBoxCustomerGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxCustomerGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCustomerGroup.CustomReportFieldName = "";
            this.comboBoxCustomerGroup.CustomReportKey = "";
            this.comboBoxCustomerGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxCustomerGroup.DescriptionTextBox = null;
            appearance100.BackColor = System.Drawing.SystemColors.Window;
            appearance100.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCustomerGroup.DisplayLayout.Appearance = appearance100;
            this.comboBoxCustomerGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCustomerGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance101.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance101.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerGroup.DisplayLayout.GroupByBox.Appearance = appearance101;
            appearance102.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomerGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance102;
            this.comboBoxCustomerGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance103.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance103.BackColor2 = System.Drawing.SystemColors.Control;
            appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomerGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance103;
            this.comboBoxCustomerGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCustomerGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance104.BackColor = System.Drawing.SystemColors.Window;
            appearance104.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCustomerGroup.DisplayLayout.Override.ActiveCellAppearance = appearance104;
            appearance105.BackColor = System.Drawing.SystemColors.Highlight;
            appearance105.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCustomerGroup.DisplayLayout.Override.ActiveRowAppearance = appearance105;
            this.comboBoxCustomerGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCustomerGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance106.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerGroup.DisplayLayout.Override.CardAreaAppearance = appearance106;
            appearance107.BorderColor = System.Drawing.Color.Silver;
            appearance107.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCustomerGroup.DisplayLayout.Override.CellAppearance = appearance107;
            this.comboBoxCustomerGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCustomerGroup.DisplayLayout.Override.CellPadding = 0;
            appearance108.BackColor = System.Drawing.SystemColors.Control;
            appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance108.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance108.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerGroup.DisplayLayout.Override.GroupByRowAppearance = appearance108;
            appearance109.TextHAlignAsString = "Left";
            this.comboBoxCustomerGroup.DisplayLayout.Override.HeaderAppearance = appearance109;
            this.comboBoxCustomerGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCustomerGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance110.BackColor = System.Drawing.SystemColors.Window;
            appearance110.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCustomerGroup.DisplayLayout.Override.RowAppearance = appearance110;
            this.comboBoxCustomerGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance111.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCustomerGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance111;
            this.comboBoxCustomerGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCustomerGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCustomerGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCustomerGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCustomerGroup.Editable = true;
            this.comboBoxCustomerGroup.FilterString = "";
            this.comboBoxCustomerGroup.HasAllAccount = false;
            this.comboBoxCustomerGroup.HasCustom = false;
            this.comboBoxCustomerGroup.IsDataLoaded = false;
            this.comboBoxCustomerGroup.Location = new System.Drawing.Point(630, 93);
            this.comboBoxCustomerGroup.MaxDropDownItems = 12;
            this.comboBoxCustomerGroup.Name = "comboBoxCustomerGroup";
            this.comboBoxCustomerGroup.ShowInactiveItems = false;
            this.comboBoxCustomerGroup.ShowQuickAdd = true;
            this.comboBoxCustomerGroup.Size = new System.Drawing.Size(284, 20);
            this.comboBoxCustomerGroup.TabIndex = 10;
            this.comboBoxCustomerGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraPictureBox1
            // 
            this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
            this.ultraPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraPictureBox1.Image = ((object)(resources.GetObject("ultraPictureBox1.Image")));
            this.ultraPictureBox1.Location = new System.Drawing.Point(787, 455);
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            this.ultraPictureBox1.Size = new System.Drawing.Size(116, 44);
            this.ultraPictureBox1.TabIndex = 35;
            this.ultraPictureBox1.Click += new System.EventHandler(this.ultraPictureBox1_Click);
            // 
            // textBoxDiscountPercent
            // 
            this.textBoxDiscountPercent.BackColor = System.Drawing.Color.White;
            this.textBoxDiscountPercent.CustomReportFieldName = "";
            this.textBoxDiscountPercent.CustomReportKey = "";
            this.textBoxDiscountPercent.CustomReportValueType = ((byte)(1));
            this.textBoxDiscountPercent.IsComboTextBox = false;
            this.textBoxDiscountPercent.IsModified = false;
            this.textBoxDiscountPercent.Location = new System.Drawing.Point(191, 252);
            this.textBoxDiscountPercent.Name = "textBoxDiscountPercent";
            this.textBoxDiscountPercent.Size = new System.Drawing.Size(103, 20);
            this.textBoxDiscountPercent.TabIndex = 14;
            this.textBoxDiscountPercent.Text = "0.00";
            this.textBoxDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonCategories
            // 
            this.buttonCategories.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCategories.BackColor = System.Drawing.Color.DarkGray;
            this.buttonCategories.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCategories.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCategories.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCategories.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCategories.Location = new System.Drawing.Point(743, 213);
            this.buttonCategories.Name = "buttonCategories";
            this.buttonCategories.Size = new System.Drawing.Size(171, 27);
            this.buttonCategories.TabIndex = 15;
            this.buttonCategories.Text = "Categories...";
            this.buttonCategories.UseVisualStyleBackColor = false;
            this.buttonCategories.Click += new System.EventHandler(this.buttonCategories_Click);
            // 
            // comboBoxPaymentMethods
            // 
            this.comboBoxPaymentMethods.Assigned = false;
            this.comboBoxPaymentMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPaymentMethods.CustomReportFieldName = "";
            this.comboBoxPaymentMethods.CustomReportKey = "";
            this.comboBoxPaymentMethods.CustomReportValueType = ((byte)(1));
            this.comboBoxPaymentMethods.DescriptionTextBox = this.textBoxPaymentMethodName;
            appearance112.BackColor = System.Drawing.SystemColors.Window;
            appearance112.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPaymentMethods.DisplayLayout.Appearance = appearance112;
            this.comboBoxPaymentMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPaymentMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance113.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance113.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentMethods.DisplayLayout.GroupByBox.Appearance = appearance113;
            appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPaymentMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance114;
            this.comboBoxPaymentMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance115.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance115.BackColor2 = System.Drawing.SystemColors.Control;
            appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance115.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPaymentMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance115;
            this.comboBoxPaymentMethods.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPaymentMethods.DisplayLayout.MaxRowScrollRegions = 1;
            appearance116.BackColor = System.Drawing.SystemColors.Window;
            appearance116.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPaymentMethods.DisplayLayout.Override.ActiveCellAppearance = appearance116;
            appearance117.BackColor = System.Drawing.SystemColors.Highlight;
            appearance117.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPaymentMethods.DisplayLayout.Override.ActiveRowAppearance = appearance117;
            this.comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPaymentMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance118.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentMethods.DisplayLayout.Override.CardAreaAppearance = appearance118;
            appearance119.BorderColor = System.Drawing.Color.Silver;
            appearance119.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPaymentMethods.DisplayLayout.Override.CellAppearance = appearance119;
            this.comboBoxPaymentMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPaymentMethods.DisplayLayout.Override.CellPadding = 0;
            appearance120.BackColor = System.Drawing.SystemColors.Control;
            appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance120.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance120.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPaymentMethods.DisplayLayout.Override.GroupByRowAppearance = appearance120;
            appearance121.TextHAlignAsString = "Left";
            this.comboBoxPaymentMethods.DisplayLayout.Override.HeaderAppearance = appearance121;
            this.comboBoxPaymentMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPaymentMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance122.BackColor = System.Drawing.SystemColors.Window;
            appearance122.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPaymentMethods.DisplayLayout.Override.RowAppearance = appearance122;
            this.comboBoxPaymentMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance123.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPaymentMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance123;
            this.comboBoxPaymentMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPaymentMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPaymentMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPaymentMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPaymentMethods.Editable = true;
            this.comboBoxPaymentMethods.FilterString = "";
            this.comboBoxPaymentMethods.IsDataLoaded = false;
            this.comboBoxPaymentMethods.Location = new System.Drawing.Point(191, 45);
            this.comboBoxPaymentMethods.MaxDropDownItems = 12;
            this.comboBoxPaymentMethods.MaxLength = 15;
            this.comboBoxPaymentMethods.Name = "comboBoxPaymentMethods";
            this.comboBoxPaymentMethods.Size = new System.Drawing.Size(268, 20);
            this.comboBoxPaymentMethods.TabIndex = 0;
            this.comboBoxPaymentMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.BackColor = System.Drawing.Color.White;
            this.textBoxLatitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxLatitude.CustomReportFieldName = "";
            this.textBoxLatitude.CustomReportKey = "";
            this.textBoxLatitude.CustomReportValueType = ((byte)(1));
            this.textBoxLatitude.IsComboTextBox = false;
            this.textBoxLatitude.IsModified = false;
            this.textBoxLatitude.Location = new System.Drawing.Point(627, 455);
            this.textBoxLatitude.MaxLength = 64;
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(156, 20);
            this.textBoxLatitude.TabIndex = 41;
            this.textBoxLatitude.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLatitude_MouseClick);
            this.textBoxLatitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLatitude_KeyDown);
            this.textBoxLatitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLatitude_KeyDown);
            this.textBoxLatitude.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxLatitude_KeyUp);
            this.textBoxLatitude.MouseLeave += new System.EventHandler(this.textBoxLatitude_MouseLeave);
            // 
            // buttonMoreAddress
            // 
            this.buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
            this.buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonMoreAddress.Location = new System.Drawing.Point(745, 503);
            this.buttonMoreAddress.Name = "buttonMoreAddress";
            this.buttonMoreAddress.Size = new System.Drawing.Size(157, 38);
            this.buttonMoreAddress.TabIndex = 34;
            this.buttonMoreAddress.Text = "More Addresses...";
            this.buttonMoreAddress.UseVisualStyleBackColor = false;
            this.buttonMoreAddress.Click += new System.EventHandler(this.buttonMoreAddress_Click);
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.Assigned = false;
            this.comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCurrency.CustomReportFieldName = "";
            this.comboBoxCurrency.CustomReportKey = "";
            this.comboBoxCurrency.CustomReportValueType = ((byte)(1));
            this.comboBoxCurrency.DescriptionTextBox = null;
            appearance124.BackColor = System.Drawing.SystemColors.Window;
            appearance124.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCurrency.DisplayLayout.Appearance = appearance124;
            this.comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance125.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance125.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance125;
            appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance126;
            this.comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance127.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance127.BackColor2 = System.Drawing.SystemColors.Control;
            appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance127.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance127;
            this.comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
            appearance128.BackColor = System.Drawing.SystemColors.Window;
            appearance128.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance128;
            appearance129.BackColor = System.Drawing.SystemColors.Highlight;
            appearance129.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance129;
            this.comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance130.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance130;
            appearance131.BorderColor = System.Drawing.Color.Silver;
            appearance131.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance131;
            this.comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
            appearance132.BackColor = System.Drawing.SystemColors.Control;
            appearance132.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance132.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance132.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance132;
            appearance133.TextHAlignAsString = "Left";
            this.comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance133;
            this.comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance134.BackColor = System.Drawing.SystemColors.Window;
            appearance134.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance134;
            this.comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance135.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance135;
            this.comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCurrency.Editable = true;
            this.comboBoxCurrency.FilterString = "";
            this.comboBoxCurrency.HasAllAccount = false;
            this.comboBoxCurrency.HasCustom = false;
            this.comboBoxCurrency.IsDataLoaded = false;
            this.comboBoxCurrency.Location = new System.Drawing.Point(630, 189);
            this.comboBoxCurrency.MaxDropDownItems = 12;
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.ShowInactiveItems = false;
            this.comboBoxCurrency.ShowQuickAdd = true;
            this.comboBoxCurrency.Size = new System.Drawing.Size(284, 20);
            this.comboBoxCurrency.TabIndex = 14;
            this.comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // checkBoxparentACforposting
            // 
            this.checkBoxparentACforposting.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxparentACforposting.Enabled = false;
            this.checkBoxparentACforposting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxparentACforposting.Location = new System.Drawing.Point(24, 165);
            this.checkBoxparentACforposting.Name = "checkBoxparentACforposting";
            this.checkBoxparentACforposting.Size = new System.Drawing.Size(435, 20);
            this.checkBoxparentACforposting.TabIndex = 6;
            this.checkBoxparentACforposting.Text = "Use parent Account for Finance posting";
            this.checkBoxparentACforposting.UseVisualStyleBackColor = false;
            // 
            // dateTimePickerContractExpDate
            // 
            this.dateTimePickerContractExpDate.Checked = false;
            this.dateTimePickerContractExpDate.CustomFormat = " ";
            this.dateTimePickerContractExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerContractExpDate.Location = new System.Drawing.Point(191, 228);
            this.dateTimePickerContractExpDate.Name = "dateTimePickerContractExpDate";
            this.dateTimePickerContractExpDate.ShowCheckBox = true;
            this.dateTimePickerContractExpDate.Size = new System.Drawing.Size(212, 20);
            this.dateTimePickerContractExpDate.TabIndex = 13;
            this.dateTimePickerContractExpDate.Value = new System.DateTime(((long)(0)));
            // 
            // dateTimePickerLicenseExpDate
            // 
            this.dateTimePickerLicenseExpDate.Checked = false;
            this.dateTimePickerLicenseExpDate.CustomFormat = " ";
            this.dateTimePickerLicenseExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLicenseExpDate.Location = new System.Drawing.Point(574, 204);
            this.dateTimePickerLicenseExpDate.Name = "dateTimePickerLicenseExpDate";
            this.dateTimePickerLicenseExpDate.ShowCheckBox = true;
            this.dateTimePickerLicenseExpDate.Size = new System.Drawing.Size(342, 20);
            this.dateTimePickerLicenseExpDate.TabIndex = 12;
            this.dateTimePickerLicenseExpDate.Value = new System.DateTime(((long)(0)));
            // 
            // comboBoxSalesperson
            // 
            this.comboBoxSalesperson.Assigned = false;
            this.comboBoxSalesperson.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSalesperson.CustomReportFieldName = "";
            this.comboBoxSalesperson.CustomReportKey = "";
            this.comboBoxSalesperson.CustomReportValueType = ((byte)(1));
            this.comboBoxSalesperson.DescriptionTextBox = null;
            appearance136.BackColor = System.Drawing.SystemColors.Window;
            appearance136.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSalesperson.DisplayLayout.Appearance = appearance136;
            this.comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance137.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance137.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance137;
            appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance138;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance139.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance139.BackColor2 = System.Drawing.SystemColors.Control;
            appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance139.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance139;
            this.comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
            appearance140.BackColor = System.Drawing.SystemColors.Window;
            appearance140.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance140;
            appearance141.BackColor = System.Drawing.SystemColors.Highlight;
            appearance141.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance141;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance142.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance142;
            appearance143.BorderColor = System.Drawing.Color.Silver;
            appearance143.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance143;
            this.comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
            appearance144.BackColor = System.Drawing.SystemColors.Control;
            appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance144.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance144.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance144;
            appearance145.TextHAlignAsString = "Left";
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance145;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance146.BackColor = System.Drawing.SystemColors.Window;
            appearance146.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance146;
            this.comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance147.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance147;
            this.comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSalesperson.Editable = true;
            this.comboBoxSalesperson.FilterString = "";
            this.comboBoxSalesperson.HasAllAccount = false;
            this.comboBoxSalesperson.HasCustom = false;
            this.comboBoxSalesperson.IsDataLoaded = false;
            this.comboBoxSalesperson.Location = new System.Drawing.Point(191, 45);
            this.comboBoxSalesperson.MaxDropDownItems = 12;
            this.comboBoxSalesperson.Name = "comboBoxSalesperson";
            this.comboBoxSalesperson.ShowInactiveItems = false;
            this.comboBoxSalesperson.ShowQuickAdd = true;
            this.comboBoxSalesperson.Size = new System.Drawing.Size(227, 20);
            this.comboBoxSalesperson.TabIndex = 0;
            this.comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxStatementEmail
            // 
            this.textBoxStatementEmail.BackColor = System.Drawing.Color.White;
            this.textBoxStatementEmail.CustomReportFieldName = "";
            this.textBoxStatementEmail.CustomReportKey = "";
            this.textBoxStatementEmail.CustomReportValueType = ((byte)(1));
            this.textBoxStatementEmail.IsComboTextBox = false;
            this.textBoxStatementEmail.IsModified = false;
            this.textBoxStatementEmail.Location = new System.Drawing.Point(191, 142);
            this.textBoxStatementEmail.MaxLength = 255;
            this.textBoxStatementEmail.Name = "textBoxStatementEmail";
            this.textBoxStatementEmail.Size = new System.Drawing.Size(227, 20);
            this.textBoxStatementEmail.TabIndex = 8;
            // 
            // comboBoxStatementMethod
            // 
            this.comboBoxStatementMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatementMethod.FormattingEnabled = true;
            this.comboBoxStatementMethod.Items.AddRange(new object[] {
            "None",
            "Email",
            "Fax",
            "Post",
            "Delivery",
            "Other"});
            this.comboBoxStatementMethod.Location = new System.Drawing.Point(589, 117);
            this.comboBoxStatementMethod.Name = "comboBoxStatementMethod";
            this.comboBoxStatementMethod.Size = new System.Drawing.Size(327, 21);
            this.comboBoxStatementMethod.TabIndex = 7;
            // 
            // textBoxComment
            // 
            this.textBoxComment.BackColor = System.Drawing.Color.White;
            this.textBoxComment.CustomReportFieldName = "";
            this.textBoxComment.CustomReportKey = "";
            this.textBoxComment.CustomReportValueType = ((byte)(1));
            this.textBoxComment.IsComboTextBox = false;
            this.textBoxComment.IsModified = false;
            this.textBoxComment.Location = new System.Drawing.Point(204, 503);
            this.textBoxComment.MaxLength = 255;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(252, 20);
            this.textBoxComment.TabIndex = 33;
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
            this.textBoxCode.Location = new System.Drawing.Point(191, 45);
            this.textBoxCode.MaxLength = 64;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(268, 20);
            this.textBoxCode.TabIndex = 0;
            // 
            // checkBoxWeightInvoice
            // 
            this.checkBoxWeightInvoice.Location = new System.Drawing.Point(24, 180);
            this.checkBoxWeightInvoice.Name = "checkBoxWeightInvoice";
            this.checkBoxWeightInvoice.Size = new System.Drawing.Size(165, 20);
            this.checkBoxWeightInvoice.TabIndex = 9;
            this.checkBoxWeightInvoice.Text = "Invoice by item weight";
            this.checkBoxWeightInvoice.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowConsignment
            // 
            this.checkBoxAllowConsignment.Location = new System.Drawing.Point(193, 180);
            this.checkBoxAllowConsignment.Name = "checkBoxAllowConsignment";
            this.checkBoxAllowConsignment.Size = new System.Drawing.Size(210, 20);
            this.checkBoxAllowConsignment.TabIndex = 10;
            this.checkBoxAllowConsignment.Text = "Allow consignment sales";
            this.checkBoxAllowConsignment.UseVisualStyleBackColor = true;
            // 
            // textBoxDepartment
            // 
            this.textBoxDepartment.BackColor = System.Drawing.Color.White;
            this.textBoxDepartment.CustomReportFieldName = "";
            this.textBoxDepartment.CustomReportKey = "";
            this.textBoxDepartment.CustomReportValueType = ((byte)(1));
            this.textBoxDepartment.IsComboTextBox = false;
            this.textBoxDepartment.IsModified = false;
            this.textBoxDepartment.Location = new System.Drawing.Point(627, 287);
            this.textBoxDepartment.MaxLength = 30;
            this.textBoxDepartment.Name = "textBoxDepartment";
            this.textBoxDepartment.Size = new System.Drawing.Size(276, 20);
            this.textBoxDepartment.TabIndex = 19;
            // 
            // textBoxWebsite
            // 
            this.textBoxWebsite.BackColor = System.Drawing.Color.White;
            this.textBoxWebsite.CustomReportFieldName = "";
            this.textBoxWebsite.CustomReportKey = "";
            this.textBoxWebsite.CustomReportValueType = ((byte)(1));
            this.textBoxWebsite.IsComboTextBox = false;
            this.textBoxWebsite.IsModified = false;
            this.textBoxWebsite.Location = new System.Drawing.Point(627, 431);
            this.textBoxWebsite.MaxLength = 255;
            this.textBoxWebsite.Name = "textBoxWebsite";
            this.textBoxWebsite.Size = new System.Drawing.Size(276, 20);
            this.textBoxWebsite.TabIndex = 31;
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.White;
            this.textBoxName.CustomReportFieldName = "";
            this.textBoxName.CustomReportKey = "";
            this.textBoxName.CustomReportValueType = ((byte)(1));
            this.textBoxName.IsComboTextBox = false;
            this.textBoxName.IsModified = false;
            this.textBoxName.IsRequired = true;
            this.textBoxName.Location = new System.Drawing.Point(191, 69);
            this.textBoxName.MaxLength = 64;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(268, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // comboBoxLeadSource
            // 
            this.comboBoxLeadSource.Assigned = false;
            this.comboBoxLeadSource.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxLeadSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxLeadSource.CustomReportFieldName = "";
            this.comboBoxLeadSource.CustomReportKey = "";
            this.comboBoxLeadSource.CustomReportValueType = ((byte)(1));
            this.comboBoxLeadSource.DescriptionTextBox = null;
            appearance148.BackColor = System.Drawing.SystemColors.Window;
            appearance148.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxLeadSource.DisplayLayout.Appearance = appearance148;
            this.comboBoxLeadSource.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxLeadSource.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance149.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance149.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLeadSource.DisplayLayout.GroupByBox.Appearance = appearance149;
            appearance150.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLeadSource.DisplayLayout.GroupByBox.BandLabelAppearance = appearance150;
            this.comboBoxLeadSource.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance151.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance151.BackColor2 = System.Drawing.SystemColors.Control;
            appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance151.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLeadSource.DisplayLayout.GroupByBox.PromptAppearance = appearance151;
            this.comboBoxLeadSource.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxLeadSource.DisplayLayout.MaxRowScrollRegions = 1;
            appearance152.BackColor = System.Drawing.SystemColors.Window;
            appearance152.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxLeadSource.DisplayLayout.Override.ActiveCellAppearance = appearance152;
            appearance153.BackColor = System.Drawing.SystemColors.Highlight;
            appearance153.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxLeadSource.DisplayLayout.Override.ActiveRowAppearance = appearance153;
            this.comboBoxLeadSource.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxLeadSource.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance154.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxLeadSource.DisplayLayout.Override.CardAreaAppearance = appearance154;
            appearance155.BorderColor = System.Drawing.Color.Silver;
            appearance155.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxLeadSource.DisplayLayout.Override.CellAppearance = appearance155;
            this.comboBoxLeadSource.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxLeadSource.DisplayLayout.Override.CellPadding = 0;
            appearance156.BackColor = System.Drawing.SystemColors.Control;
            appearance156.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance156.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance156.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLeadSource.DisplayLayout.Override.GroupByRowAppearance = appearance156;
            appearance157.TextHAlignAsString = "Left";
            this.comboBoxLeadSource.DisplayLayout.Override.HeaderAppearance = appearance157;
            this.comboBoxLeadSource.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxLeadSource.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance158.BackColor = System.Drawing.SystemColors.Window;
            appearance158.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxLeadSource.DisplayLayout.Override.RowAppearance = appearance158;
            this.comboBoxLeadSource.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance159.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxLeadSource.DisplayLayout.Override.TemplateAddRowAppearance = appearance159;
            this.comboBoxLeadSource.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxLeadSource.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxLeadSource.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxLeadSource.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxLeadSource.Editable = true;
            this.comboBoxLeadSource.FilterString = "";
            this.comboBoxLeadSource.GenericListType = Micromind.Common.Data.GenericListTypes.LeadSource;
            this.comboBoxLeadSource.HasAllAccount = false;
            this.comboBoxLeadSource.HasCustom = false;
            this.comboBoxLeadSource.IsDataLoaded = false;
            this.comboBoxLeadSource.IsSingleColumn = false;
            this.comboBoxLeadSource.Location = new System.Drawing.Point(191, 117);
            this.comboBoxLeadSource.MaxDropDownItems = 12;
            this.comboBoxLeadSource.Name = "comboBoxLeadSource";
            this.comboBoxLeadSource.ShowInactiveItems = false;
            this.comboBoxLeadSource.ShowQuickAdd = true;
            this.comboBoxLeadSource.Size = new System.Drawing.Size(227, 20);
            this.comboBoxLeadSource.TabIndex = 6;
            this.comboBoxLeadSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxPriceLevel
            // 
            this.comboBoxPriceLevel.Assigned = false;
            this.comboBoxPriceLevel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPriceLevel.CustomReportFieldName = "";
            this.comboBoxPriceLevel.CustomReportKey = "";
            this.comboBoxPriceLevel.CustomReportValueType = ((byte)(1));
            this.comboBoxPriceLevel.DescriptionTextBox = null;
            appearance160.BackColor = System.Drawing.SystemColors.Window;
            appearance160.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPriceLevel.DisplayLayout.Appearance = appearance160;
            this.comboBoxPriceLevel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPriceLevel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance161.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance161.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPriceLevel.DisplayLayout.GroupByBox.Appearance = appearance161;
            appearance162.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPriceLevel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance162;
            this.comboBoxPriceLevel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance163.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance163.BackColor2 = System.Drawing.SystemColors.Control;
            appearance163.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance163.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPriceLevel.DisplayLayout.GroupByBox.PromptAppearance = appearance163;
            this.comboBoxPriceLevel.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPriceLevel.DisplayLayout.MaxRowScrollRegions = 1;
            appearance164.BackColor = System.Drawing.SystemColors.Window;
            appearance164.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPriceLevel.DisplayLayout.Override.ActiveCellAppearance = appearance164;
            appearance165.BackColor = System.Drawing.SystemColors.Highlight;
            appearance165.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPriceLevel.DisplayLayout.Override.ActiveRowAppearance = appearance165;
            this.comboBoxPriceLevel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPriceLevel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance166.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPriceLevel.DisplayLayout.Override.CardAreaAppearance = appearance166;
            appearance167.BorderColor = System.Drawing.Color.Silver;
            appearance167.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPriceLevel.DisplayLayout.Override.CellAppearance = appearance167;
            this.comboBoxPriceLevel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPriceLevel.DisplayLayout.Override.CellPadding = 0;
            appearance168.BackColor = System.Drawing.SystemColors.Control;
            appearance168.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance168.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance168.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPriceLevel.DisplayLayout.Override.GroupByRowAppearance = appearance168;
            appearance169.TextHAlignAsString = "Left";
            this.comboBoxPriceLevel.DisplayLayout.Override.HeaderAppearance = appearance169;
            this.comboBoxPriceLevel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPriceLevel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance170.BackColor = System.Drawing.SystemColors.Window;
            appearance170.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPriceLevel.DisplayLayout.Override.RowAppearance = appearance170;
            this.comboBoxPriceLevel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance171.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPriceLevel.DisplayLayout.Override.TemplateAddRowAppearance = appearance171;
            this.comboBoxPriceLevel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPriceLevel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPriceLevel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPriceLevel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPriceLevel.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxPriceLevel.Editable = true;
            this.comboBoxPriceLevel.FilterString = "";
            this.comboBoxPriceLevel.HasAllAccount = false;
            this.comboBoxPriceLevel.HasCustom = false;
            this.comboBoxPriceLevel.IsDataLoaded = false;
            this.comboBoxPriceLevel.Location = new System.Drawing.Point(630, 165);
            this.comboBoxPriceLevel.MaxDropDownItems = 12;
            this.comboBoxPriceLevel.MaxLength = 15;
            this.comboBoxPriceLevel.Name = "comboBoxPriceLevel";
            this.comboBoxPriceLevel.ShowInactiveItems = false;
            this.comboBoxPriceLevel.ShowQuickAdd = true;
            this.comboBoxPriceLevel.Size = new System.Drawing.Size(284, 20);
            this.comboBoxPriceLevel.TabIndex = 13;
            this.comboBoxPriceLevel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxFormalName
            // 
            this.textBoxFormalName.BackColor = System.Drawing.Color.White;
            this.textBoxFormalName.CustomReportFieldName = "";
            this.textBoxFormalName.CustomReportKey = "";
            this.textBoxFormalName.CustomReportValueType = ((byte)(1));
            this.textBoxFormalName.IsComboTextBox = false;
            this.textBoxFormalName.IsModified = false;
            this.textBoxFormalName.Location = new System.Drawing.Point(191, 93);
            this.textBoxFormalName.MaxLength = 64;
            this.textBoxFormalName.Name = "textBoxFormalName";
            this.textBoxFormalName.Size = new System.Drawing.Size(268, 20);
            this.textBoxFormalName.TabIndex = 2;
            // 
            // comboBoxArea
            // 
            this.comboBoxArea.Assigned = false;
            this.comboBoxArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxArea.CustomReportFieldName = "";
            this.comboBoxArea.CustomReportKey = "";
            this.comboBoxArea.CustomReportValueType = ((byte)(1));
            this.comboBoxArea.DescriptionTextBox = null;
            appearance172.BackColor = System.Drawing.SystemColors.Window;
            appearance172.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxArea.DisplayLayout.Appearance = appearance172;
            this.comboBoxArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance173.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance173.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxArea.DisplayLayout.GroupByBox.Appearance = appearance173;
            appearance174.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance174;
            this.comboBoxArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance175.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance175.BackColor2 = System.Drawing.SystemColors.Control;
            appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance175.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxArea.DisplayLayout.GroupByBox.PromptAppearance = appearance175;
            this.comboBoxArea.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxArea.DisplayLayout.MaxRowScrollRegions = 1;
            appearance176.BackColor = System.Drawing.SystemColors.Window;
            appearance176.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxArea.DisplayLayout.Override.ActiveCellAppearance = appearance176;
            appearance177.BackColor = System.Drawing.SystemColors.Highlight;
            appearance177.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxArea.DisplayLayout.Override.ActiveRowAppearance = appearance177;
            this.comboBoxArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance178.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxArea.DisplayLayout.Override.CardAreaAppearance = appearance178;
            appearance179.BorderColor = System.Drawing.Color.Silver;
            appearance179.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxArea.DisplayLayout.Override.CellAppearance = appearance179;
            this.comboBoxArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxArea.DisplayLayout.Override.CellPadding = 0;
            appearance180.BackColor = System.Drawing.SystemColors.Control;
            appearance180.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance180.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance180.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxArea.DisplayLayout.Override.GroupByRowAppearance = appearance180;
            appearance181.TextHAlignAsString = "Left";
            this.comboBoxArea.DisplayLayout.Override.HeaderAppearance = appearance181;
            this.comboBoxArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance182.BackColor = System.Drawing.SystemColors.Window;
            appearance182.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxArea.DisplayLayout.Override.RowAppearance = appearance182;
            this.comboBoxArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance183.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance183;
            this.comboBoxArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxArea.Editable = true;
            this.comboBoxArea.FilterString = "";
            this.comboBoxArea.HasAllAccount = false;
            this.comboBoxArea.HasCustom = false;
            this.comboBoxArea.IsDataLoaded = false;
            this.comboBoxArea.Location = new System.Drawing.Point(630, 141);
            this.comboBoxArea.MaxDropDownItems = 12;
            this.comboBoxArea.MaxLength = 15;
            this.comboBoxArea.Name = "comboBoxArea";
            this.comboBoxArea.ShowInactiveItems = false;
            this.comboBoxArea.ShowQuickAdd = true;
            this.comboBoxArea.Size = new System.Drawing.Size(284, 20);
            this.comboBoxArea.TabIndex = 12;
            this.comboBoxArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxForeignName
            // 
            this.textBoxForeignName.BackColor = System.Drawing.Color.White;
            this.textBoxForeignName.CustomReportFieldName = "";
            this.textBoxForeignName.CustomReportKey = "";
            this.textBoxForeignName.CustomReportValueType = ((byte)(1));
            this.textBoxForeignName.IsComboTextBox = false;
            this.textBoxForeignName.IsModified = false;
            this.textBoxForeignName.IsRequired = true;
            this.textBoxForeignName.Location = new System.Drawing.Point(191, 117);
            this.textBoxForeignName.MaxLength = 64;
            this.textBoxForeignName.Name = "textBoxForeignName";
            this.textBoxForeignName.Size = new System.Drawing.Size(268, 20);
            this.textBoxForeignName.TabIndex = 3;
            // 
            // dateTimePickerEstablished
            // 
            this.dateTimePickerEstablished.Checked = false;
            this.dateTimePickerEstablished.CustomFormat = " ";
            this.dateTimePickerEstablished.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEstablished.Location = new System.Drawing.Point(589, 93);
            this.dateTimePickerEstablished.Name = "dateTimePickerEstablished";
            this.dateTimePickerEstablished.ShowCheckBox = true;
            this.dateTimePickerEstablished.Size = new System.Drawing.Size(327, 20);
            this.dateTimePickerEstablished.TabIndex = 5;
            this.dateTimePickerEstablished.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.Color.White;
            this.textBoxEmail.CustomReportFieldName = "";
            this.textBoxEmail.CustomReportKey = "";
            this.textBoxEmail.CustomReportValueType = ((byte)(1));
            this.textBoxEmail.IsComboTextBox = false;
            this.textBoxEmail.IsModified = false;
            this.textBoxEmail.Location = new System.Drawing.Point(627, 407);
            this.textBoxEmail.MaxLength = 64;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(276, 20);
            this.textBoxEmail.TabIndex = 29;
            // 
            // dateTimePickerCustomerSince
            // 
            this.dateTimePickerCustomerSince.Checked = false;
            this.dateTimePickerCustomerSince.CustomFormat = " ";
            this.dateTimePickerCustomerSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerCustomerSince.Location = new System.Drawing.Point(191, 93);
            this.dateTimePickerCustomerSince.Name = "dateTimePickerCustomerSince";
            this.dateTimePickerCustomerSince.ShowCheckBox = true;
            this.dateTimePickerCustomerSince.Size = new System.Drawing.Size(227, 20);
            this.dateTimePickerCustomerSince.TabIndex = 4;
            this.dateTimePickerCustomerSince.Value = new System.DateTime(((long)(0)));
            // 
            // comboBoxCountry
            // 
            this.comboBoxCountry.Assigned = false;
            this.comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCountry.CustomReportFieldName = "";
            this.comboBoxCountry.CustomReportKey = "";
            this.comboBoxCountry.CustomReportValueType = ((byte)(1));
            this.comboBoxCountry.DescriptionTextBox = null;
            appearance184.BackColor = System.Drawing.SystemColors.Window;
            appearance184.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCountry.DisplayLayout.Appearance = appearance184;
            this.comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance185.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance185.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance185.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance185.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance185;
            appearance186.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance186;
            this.comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance187.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance187.BackColor2 = System.Drawing.SystemColors.Control;
            appearance187.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance187.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance187;
            this.comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
            appearance188.BackColor = System.Drawing.SystemColors.Window;
            appearance188.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance188;
            appearance189.BackColor = System.Drawing.SystemColors.Highlight;
            appearance189.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance189;
            this.comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance190.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance190;
            appearance191.BorderColor = System.Drawing.Color.Silver;
            appearance191.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance191;
            this.comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
            appearance192.BackColor = System.Drawing.SystemColors.Control;
            appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance192.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance192.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance192;
            appearance193.TextHAlignAsString = "Left";
            this.comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance193;
            this.comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance194.BackColor = System.Drawing.SystemColors.Window;
            appearance194.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance194;
            this.comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance195.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance195;
            this.comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCountry.Editable = true;
            this.comboBoxCountry.FilterString = "";
            this.comboBoxCountry.HasAllAccount = false;
            this.comboBoxCountry.HasCustom = false;
            this.comboBoxCountry.IsDataLoaded = false;
            this.comboBoxCountry.Location = new System.Drawing.Point(630, 117);
            this.comboBoxCountry.MaxDropDownItems = 12;
            this.comboBoxCountry.MaxLength = 15;
            this.comboBoxCountry.Name = "comboBoxCountry";
            this.comboBoxCountry.ShowInactiveItems = false;
            this.comboBoxCountry.ShowQuickAdd = true;
            this.comboBoxCountry.Size = new System.Drawing.Size(284, 20);
            this.comboBoxCountry.TabIndex = 11;
            this.comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxMobile
            // 
            this.textBoxMobile.BackColor = System.Drawing.Color.White;
            this.textBoxMobile.CustomReportFieldName = "";
            this.textBoxMobile.CustomReportKey = "";
            this.textBoxMobile.CustomReportValueType = ((byte)(1));
            this.textBoxMobile.IsComboTextBox = false;
            this.textBoxMobile.IsModified = false;
            this.textBoxMobile.Location = new System.Drawing.Point(627, 383);
            this.textBoxMobile.MaxLength = 30;
            this.textBoxMobile.Name = "textBoxMobile";
            this.textBoxMobile.Size = new System.Drawing.Size(276, 20);
            this.textBoxMobile.TabIndex = 27;
            // 
            // comboBoxBilltoAddress
            // 
            this.comboBoxBilltoAddress.Assigned = false;
            this.comboBoxBilltoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxBilltoAddress.CustomReportFieldName = "";
            this.comboBoxBilltoAddress.CustomReportKey = "";
            this.comboBoxBilltoAddress.CustomReportValueType = ((byte)(1));
            this.comboBoxBilltoAddress.DescriptionTextBox = null;
            appearance196.BackColor = System.Drawing.SystemColors.Window;
            appearance196.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxBilltoAddress.DisplayLayout.Appearance = appearance196;
            this.comboBoxBilltoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxBilltoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance197.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance197.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance197.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance197.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxBilltoAddress.DisplayLayout.GroupByBox.Appearance = appearance197;
            appearance198.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxBilltoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance198;
            this.comboBoxBilltoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance199.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance199.BackColor2 = System.Drawing.SystemColors.Control;
            appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance199.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxBilltoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance199;
            this.comboBoxBilltoAddress.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxBilltoAddress.DisplayLayout.MaxRowScrollRegions = 1;
            appearance200.BackColor = System.Drawing.SystemColors.Window;
            appearance200.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxBilltoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance200;
            appearance201.BackColor = System.Drawing.SystemColors.Highlight;
            appearance201.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxBilltoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance201;
            this.comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxBilltoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance202.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxBilltoAddress.DisplayLayout.Override.CardAreaAppearance = appearance202;
            appearance203.BorderColor = System.Drawing.Color.Silver;
            appearance203.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxBilltoAddress.DisplayLayout.Override.CellAppearance = appearance203;
            this.comboBoxBilltoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxBilltoAddress.DisplayLayout.Override.CellPadding = 0;
            appearance204.BackColor = System.Drawing.SystemColors.Control;
            appearance204.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance204.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance204.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance204.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxBilltoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance204;
            appearance205.TextHAlignAsString = "Left";
            this.comboBoxBilltoAddress.DisplayLayout.Override.HeaderAppearance = appearance205;
            this.comboBoxBilltoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxBilltoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance206.BackColor = System.Drawing.SystemColors.Window;
            appearance206.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxBilltoAddress.DisplayLayout.Override.RowAppearance = appearance206;
            this.comboBoxBilltoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance207.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxBilltoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance207;
            this.comboBoxBilltoAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxBilltoAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxBilltoAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxBilltoAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxBilltoAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxBilltoAddress.Editable = true;
            this.comboBoxBilltoAddress.FilterString = "";
            this.comboBoxBilltoAddress.HasAllAccount = false;
            this.comboBoxBilltoAddress.HasCustom = false;
            this.comboBoxBilltoAddress.IsDataLoaded = false;
            this.comboBoxBilltoAddress.Location = new System.Drawing.Point(589, 45);
            this.comboBoxBilltoAddress.MaxDropDownItems = 12;
            this.comboBoxBilltoAddress.Name = "comboBoxBilltoAddress";
            this.comboBoxBilltoAddress.ShowInactiveItems = false;
            this.comboBoxBilltoAddress.ShowQuickAdd = true;
            this.comboBoxBilltoAddress.Size = new System.Drawing.Size(327, 20);
            this.comboBoxBilltoAddress.TabIndex = 1;
            this.comboBoxBilltoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxParentCustomer
            // 
            this.comboBoxParentCustomer.Assigned = false;
            this.comboBoxParentCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxParentCustomer.CustomReportFieldName = "";
            this.comboBoxParentCustomer.CustomReportKey = "";
            this.comboBoxParentCustomer.CustomReportValueType = ((byte)(1));
            this.comboBoxParentCustomer.DescriptionTextBox = null;
            appearance208.BackColor = System.Drawing.SystemColors.Window;
            appearance208.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxParentCustomer.DisplayLayout.Appearance = appearance208;
            this.comboBoxParentCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxParentCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance209.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance209.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance209.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance209.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCustomer.DisplayLayout.GroupByBox.Appearance = appearance209;
            appearance210.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxParentCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance210;
            this.comboBoxParentCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance211.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance211.BackColor2 = System.Drawing.SystemColors.Control;
            appearance211.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance211.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxParentCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance211;
            this.comboBoxParentCustomer.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxParentCustomer.DisplayLayout.MaxRowScrollRegions = 1;
            appearance212.BackColor = System.Drawing.SystemColors.Window;
            appearance212.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxParentCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance212;
            appearance213.BackColor = System.Drawing.SystemColors.Highlight;
            appearance213.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxParentCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance213;
            this.comboBoxParentCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxParentCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance214.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCustomer.DisplayLayout.Override.CardAreaAppearance = appearance214;
            appearance215.BorderColor = System.Drawing.Color.Silver;
            appearance215.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxParentCustomer.DisplayLayout.Override.CellAppearance = appearance215;
            this.comboBoxParentCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxParentCustomer.DisplayLayout.Override.CellPadding = 0;
            appearance216.BackColor = System.Drawing.SystemColors.Control;
            appearance216.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance216.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance216.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance216.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxParentCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance216;
            appearance217.TextHAlignAsString = "Left";
            this.comboBoxParentCustomer.DisplayLayout.Override.HeaderAppearance = appearance217;
            this.comboBoxParentCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxParentCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance218.BackColor = System.Drawing.SystemColors.Window;
            appearance218.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxParentCustomer.DisplayLayout.Override.RowAppearance = appearance218;
            this.comboBoxParentCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance219.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxParentCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance219;
            this.comboBoxParentCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxParentCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxParentCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxParentCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxParentCustomer.Editable = true;
            this.comboBoxParentCustomer.FilterString = "";
            this.comboBoxParentCustomer.FilterSysDocID = "";
            this.comboBoxParentCustomer.HasAll = false;
            this.comboBoxParentCustomer.HasCustom = false;
            this.comboBoxParentCustomer.IsDataLoaded = false;
            this.comboBoxParentCustomer.Location = new System.Drawing.Point(191, 141);
            this.comboBoxParentCustomer.MaxDropDownItems = 12;
            this.comboBoxParentCustomer.MaxLength = 64;
            this.comboBoxParentCustomer.Name = "comboBoxParentCustomer";
            this.comboBoxParentCustomer.ShowConsignmentOnly = false;
            this.comboBoxParentCustomer.ShowInactive = false;
            this.comboBoxParentCustomer.ShowLPOCustomersOnly = false;
            this.comboBoxParentCustomer.ShowPROCustomersOnly = false;
            this.comboBoxParentCustomer.ShowQuickAdd = true;
            this.comboBoxParentCustomer.Size = new System.Drawing.Size(268, 20);
            this.comboBoxParentCustomer.TabIndex = 5;
            this.comboBoxParentCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxParentCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxParentCustomer_SelectedIndexChanged);
            // 
            // comboBoxShippingMethods
            // 
            this.comboBoxShippingMethods.Assigned = false;
            this.comboBoxShippingMethods.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShippingMethods.CustomReportFieldName = "";
            this.comboBoxShippingMethods.CustomReportKey = "";
            this.comboBoxShippingMethods.CustomReportValueType = ((byte)(1));
            this.comboBoxShippingMethods.DescriptionTextBox = null;
            appearance220.BackColor = System.Drawing.SystemColors.Window;
            appearance220.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShippingMethods.DisplayLayout.Appearance = appearance220;
            this.comboBoxShippingMethods.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShippingMethods.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance221.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance221.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance221.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance221.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethods.DisplayLayout.GroupByBox.Appearance = appearance221;
            appearance222.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethods.DisplayLayout.GroupByBox.BandLabelAppearance = appearance222;
            this.comboBoxShippingMethods.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance223.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance223.BackColor2 = System.Drawing.SystemColors.Control;
            appearance223.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance223.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShippingMethods.DisplayLayout.GroupByBox.PromptAppearance = appearance223;
            this.comboBoxShippingMethods.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShippingMethods.DisplayLayout.MaxRowScrollRegions = 1;
            appearance224.BackColor = System.Drawing.SystemColors.Window;
            appearance224.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShippingMethods.DisplayLayout.Override.ActiveCellAppearance = appearance224;
            appearance225.BackColor = System.Drawing.SystemColors.Highlight;
            appearance225.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShippingMethods.DisplayLayout.Override.ActiveRowAppearance = appearance225;
            this.comboBoxShippingMethods.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShippingMethods.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance226.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethods.DisplayLayout.Override.CardAreaAppearance = appearance226;
            appearance227.BorderColor = System.Drawing.Color.Silver;
            appearance227.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShippingMethods.DisplayLayout.Override.CellAppearance = appearance227;
            this.comboBoxShippingMethods.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShippingMethods.DisplayLayout.Override.CellPadding = 0;
            appearance228.BackColor = System.Drawing.SystemColors.Control;
            appearance228.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance228.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance228.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance228.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShippingMethods.DisplayLayout.Override.GroupByRowAppearance = appearance228;
            appearance229.TextHAlignAsString = "Left";
            this.comboBoxShippingMethods.DisplayLayout.Override.HeaderAppearance = appearance229;
            this.comboBoxShippingMethods.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShippingMethods.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance230.BackColor = System.Drawing.SystemColors.Window;
            appearance230.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShippingMethods.DisplayLayout.Override.RowAppearance = appearance230;
            this.comboBoxShippingMethods.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance231.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShippingMethods.DisplayLayout.Override.TemplateAddRowAppearance = appearance231;
            this.comboBoxShippingMethods.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShippingMethods.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShippingMethods.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShippingMethods.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShippingMethods.Editable = true;
            this.comboBoxShippingMethods.FilterString = "";
            this.comboBoxShippingMethods.HasAllAccount = false;
            this.comboBoxShippingMethods.HasCustom = false;
            this.comboBoxShippingMethods.IsDataLoaded = false;
            this.comboBoxShippingMethods.Location = new System.Drawing.Point(589, 69);
            this.comboBoxShippingMethods.MaxDropDownItems = 12;
            this.comboBoxShippingMethods.MaxLength = 15;
            this.comboBoxShippingMethods.Name = "comboBoxShippingMethods";
            this.comboBoxShippingMethods.ShowInactiveItems = false;
            this.comboBoxShippingMethods.ShowQuickAdd = true;
            this.comboBoxShippingMethods.Size = new System.Drawing.Size(327, 20);
            this.comboBoxShippingMethods.TabIndex = 3;
            this.comboBoxShippingMethods.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // textBoxPostalCode
            // 
            this.textBoxPostalCode.BackColor = System.Drawing.Color.White;
            this.textBoxPostalCode.CustomReportFieldName = "";
            this.textBoxPostalCode.CustomReportKey = "";
            this.textBoxPostalCode.CustomReportValueType = ((byte)(1));
            this.textBoxPostalCode.IsComboTextBox = false;
            this.textBoxPostalCode.IsModified = false;
            this.textBoxPostalCode.Location = new System.Drawing.Point(204, 479);
            this.textBoxPostalCode.MaxLength = 30;
            this.textBoxPostalCode.Name = "textBoxPostalCode";
            this.textBoxPostalCode.Size = new System.Drawing.Size(252, 20);
            this.textBoxPostalCode.TabIndex = 15;
            // 
            // textBoxFax
            // 
            this.textBoxFax.BackColor = System.Drawing.Color.White;
            this.textBoxFax.CustomReportFieldName = "";
            this.textBoxFax.CustomReportKey = "";
            this.textBoxFax.CustomReportValueType = ((byte)(1));
            this.textBoxFax.IsComboTextBox = false;
            this.textBoxFax.IsModified = false;
            this.textBoxFax.Location = new System.Drawing.Point(627, 359);
            this.textBoxFax.MaxLength = 30;
            this.textBoxFax.Name = "textBoxFax";
            this.textBoxFax.Size = new System.Drawing.Size(276, 20);
            this.textBoxFax.TabIndex = 25;
            // 
            // checkBoxIsInactive
            // 
            this.checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxIsInactive.Location = new System.Drawing.Point(463, 45);
            this.checkBoxIsInactive.Name = "checkBoxIsInactive";
            this.checkBoxIsInactive.Size = new System.Drawing.Size(94, 20);
            this.checkBoxIsInactive.TabIndex = 7;
            this.checkBoxIsInactive.Text = "Inactive";
            this.checkBoxIsInactive.UseVisualStyleBackColor = false;
            // 
            // comboBoxShiptoAddress
            // 
            this.comboBoxShiptoAddress.Assigned = false;
            this.comboBoxShiptoAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxShiptoAddress.CustomReportFieldName = "";
            this.comboBoxShiptoAddress.CustomReportKey = "";
            this.comboBoxShiptoAddress.CustomReportValueType = ((byte)(1));
            this.comboBoxShiptoAddress.DescriptionTextBox = null;
            appearance232.BackColor = System.Drawing.SystemColors.Window;
            appearance232.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxShiptoAddress.DisplayLayout.Appearance = appearance232;
            this.comboBoxShiptoAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxShiptoAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance233.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance233.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance233.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance233.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShiptoAddress.DisplayLayout.GroupByBox.Appearance = appearance233;
            appearance234.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShiptoAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance234;
            this.comboBoxShiptoAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance235.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance235.BackColor2 = System.Drawing.SystemColors.Control;
            appearance235.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance235.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxShiptoAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance235;
            this.comboBoxShiptoAddress.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxShiptoAddress.DisplayLayout.MaxRowScrollRegions = 1;
            appearance236.BackColor = System.Drawing.SystemColors.Window;
            appearance236.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxShiptoAddress.DisplayLayout.Override.ActiveCellAppearance = appearance236;
            appearance237.BackColor = System.Drawing.SystemColors.Highlight;
            appearance237.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxShiptoAddress.DisplayLayout.Override.ActiveRowAppearance = appearance237;
            this.comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxShiptoAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance238.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxShiptoAddress.DisplayLayout.Override.CardAreaAppearance = appearance238;
            appearance239.BorderColor = System.Drawing.Color.Silver;
            appearance239.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxShiptoAddress.DisplayLayout.Override.CellAppearance = appearance239;
            this.comboBoxShiptoAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxShiptoAddress.DisplayLayout.Override.CellPadding = 0;
            appearance240.BackColor = System.Drawing.SystemColors.Control;
            appearance240.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance240.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance240.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance240.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxShiptoAddress.DisplayLayout.Override.GroupByRowAppearance = appearance240;
            appearance241.TextHAlignAsString = "Left";
            this.comboBoxShiptoAddress.DisplayLayout.Override.HeaderAppearance = appearance241;
            this.comboBoxShiptoAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxShiptoAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance242.BackColor = System.Drawing.SystemColors.Window;
            appearance242.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxShiptoAddress.DisplayLayout.Override.RowAppearance = appearance242;
            this.comboBoxShiptoAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance243.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxShiptoAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance243;
            this.comboBoxShiptoAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxShiptoAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxShiptoAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxShiptoAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxShiptoAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            this.comboBoxShiptoAddress.Editable = true;
            this.comboBoxShiptoAddress.FilterString = "";
            this.comboBoxShiptoAddress.HasAllAccount = false;
            this.comboBoxShiptoAddress.HasCustom = false;
            this.comboBoxShiptoAddress.IsDataLoaded = false;
            this.comboBoxShiptoAddress.Location = new System.Drawing.Point(191, 69);
            this.comboBoxShiptoAddress.MaxDropDownItems = 12;
            this.comboBoxShiptoAddress.Name = "comboBoxShiptoAddress";
            this.comboBoxShiptoAddress.ShowInactiveItems = false;
            this.comboBoxShiptoAddress.ShowQuickAdd = true;
            this.comboBoxShiptoAddress.Size = new System.Drawing.Size(227, 20);
            this.comboBoxShiptoAddress.TabIndex = 2;
            this.comboBoxShiptoAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // checkBoxHold
            // 
            this.checkBoxHold.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxHold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxHold.Location = new System.Drawing.Point(561, 45);
            this.checkBoxHold.Name = "checkBoxHold";
            this.checkBoxHold.Size = new System.Drawing.Size(338, 20);
            this.checkBoxHold.TabIndex = 8;
            this.checkBoxHold.Text = "Hold";
            this.checkBoxHold.UseVisualStyleBackColor = true;
            // 
            // textBoxPhone2
            // 
            this.textBoxPhone2.BackColor = System.Drawing.Color.White;
            this.textBoxPhone2.CustomReportFieldName = "";
            this.textBoxPhone2.CustomReportKey = "";
            this.textBoxPhone2.CustomReportValueType = ((byte)(1));
            this.textBoxPhone2.IsComboTextBox = false;
            this.textBoxPhone2.IsModified = false;
            this.textBoxPhone2.Location = new System.Drawing.Point(627, 335);
            this.textBoxPhone2.MaxLength = 30;
            this.textBoxPhone2.Name = "textBoxPhone2";
            this.textBoxPhone2.Size = new System.Drawing.Size(276, 20);
            this.textBoxPhone2.TabIndex = 23;
            // 
            // comboBoxCustomerClass
            // 
            this.comboBoxCustomerClass.Assigned = false;
            this.comboBoxCustomerClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCustomerClass.CustomReportFieldName = "";
            this.comboBoxCustomerClass.CustomReportKey = "";
            this.comboBoxCustomerClass.CustomReportValueType = ((byte)(1));
            this.comboBoxCustomerClass.DescriptionTextBox = null;
            appearance244.BackColor = System.Drawing.SystemColors.Window;
            appearance244.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCustomerClass.DisplayLayout.Appearance = appearance244;
            this.comboBoxCustomerClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCustomerClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance245.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance245.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance245.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance245.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerClass.DisplayLayout.GroupByBox.Appearance = appearance245;
            appearance246.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomerClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance246;
            this.comboBoxCustomerClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance247.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance247.BackColor2 = System.Drawing.SystemColors.Control;
            appearance247.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance247.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCustomerClass.DisplayLayout.GroupByBox.PromptAppearance = appearance247;
            this.comboBoxCustomerClass.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCustomerClass.DisplayLayout.MaxRowScrollRegions = 1;
            appearance248.BackColor = System.Drawing.SystemColors.Window;
            appearance248.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCustomerClass.DisplayLayout.Override.ActiveCellAppearance = appearance248;
            appearance249.BackColor = System.Drawing.SystemColors.Highlight;
            appearance249.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCustomerClass.DisplayLayout.Override.ActiveRowAppearance = appearance249;
            this.comboBoxCustomerClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCustomerClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance250.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerClass.DisplayLayout.Override.CardAreaAppearance = appearance250;
            appearance251.BorderColor = System.Drawing.Color.Silver;
            appearance251.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCustomerClass.DisplayLayout.Override.CellAppearance = appearance251;
            this.comboBoxCustomerClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCustomerClass.DisplayLayout.Override.CellPadding = 0;
            appearance252.BackColor = System.Drawing.SystemColors.Control;
            appearance252.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance252.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance252.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance252.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCustomerClass.DisplayLayout.Override.GroupByRowAppearance = appearance252;
            appearance253.TextHAlignAsString = "Left";
            this.comboBoxCustomerClass.DisplayLayout.Override.HeaderAppearance = appearance253;
            this.comboBoxCustomerClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCustomerClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance254.BackColor = System.Drawing.SystemColors.Window;
            appearance254.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCustomerClass.DisplayLayout.Override.RowAppearance = appearance254;
            this.comboBoxCustomerClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance255.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCustomerClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance255;
            this.comboBoxCustomerClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCustomerClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCustomerClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCustomerClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCustomerClass.Editable = true;
            this.comboBoxCustomerClass.FilterString = "";
            this.comboBoxCustomerClass.HasAllAccount = false;
            this.comboBoxCustomerClass.HasCustom = false;
            this.comboBoxCustomerClass.IsDataLoaded = false;
            this.comboBoxCustomerClass.Location = new System.Drawing.Point(630, 69);
            this.comboBoxCustomerClass.MaxDropDownItems = 12;
            this.comboBoxCustomerClass.MaxLength = 15;
            this.comboBoxCustomerClass.Name = "comboBoxCustomerClass";
            this.comboBoxCustomerClass.ShowInactiveItems = false;
            this.comboBoxCustomerClass.ShowQuickAdd = true;
            this.comboBoxCustomerClass.Size = new System.Drawing.Size(284, 20);
            this.comboBoxCustomerClass.TabIndex = 9;
            this.comboBoxCustomerClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxCustomerClass.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomerClass_SelectedIndexChanged);
            // 
            // textBoxAddressID
            // 
            this.textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxAddressID.CustomReportFieldName = "";
            this.textBoxAddressID.CustomReportKey = "";
            this.textBoxAddressID.CustomReportValueType = ((byte)(1));
            this.textBoxAddressID.Enabled = false;
            this.textBoxAddressID.ForeColor = System.Drawing.Color.Black;
            this.textBoxAddressID.IsComboTextBox = false;
            this.textBoxAddressID.IsModified = false;
            this.textBoxAddressID.Location = new System.Drawing.Point(204, 287);
            this.textBoxAddressID.MaxLength = 15;
            this.textBoxAddressID.Name = "textBoxAddressID";
            this.textBoxAddressID.Size = new System.Drawing.Size(252, 20);
            this.textBoxAddressID.TabIndex = 1;
            this.textBoxAddressID.Text = "PRIMARY";
            // 
            // textBoxPhone1
            // 
            this.textBoxPhone1.BackColor = System.Drawing.Color.White;
            this.textBoxPhone1.CustomReportFieldName = "";
            this.textBoxPhone1.CustomReportKey = "";
            this.textBoxPhone1.CustomReportValueType = ((byte)(1));
            this.textBoxPhone1.IsComboTextBox = false;
            this.textBoxPhone1.IsModified = false;
            this.textBoxPhone1.Location = new System.Drawing.Point(627, 311);
            this.textBoxPhone1.MaxLength = 30;
            this.textBoxPhone1.Name = "textBoxPhone1";
            this.textBoxPhone1.Size = new System.Drawing.Size(276, 20);
            this.textBoxPhone1.TabIndex = 21;
            // 
            // textBoxContactName
            // 
            this.textBoxContactName.BackColor = System.Drawing.Color.White;
            this.textBoxContactName.CustomReportFieldName = "";
            this.textBoxContactName.CustomReportKey = "";
            this.textBoxContactName.CustomReportValueType = ((byte)(1));
            this.textBoxContactName.IsComboTextBox = false;
            this.textBoxContactName.IsModified = false;
            this.textBoxContactName.Location = new System.Drawing.Point(204, 311);
            this.textBoxContactName.MaxLength = 64;
            this.textBoxContactName.Name = "textBoxContactName";
            this.textBoxContactName.Size = new System.Drawing.Size(252, 20);
            this.textBoxContactName.TabIndex = 3;
            // 
            // textBoxAddress1
            // 
            this.textBoxAddress1.BackColor = System.Drawing.Color.White;
            this.textBoxAddress1.CustomReportFieldName = "";
            this.textBoxAddress1.CustomReportKey = "";
            this.textBoxAddress1.CustomReportValueType = ((byte)(1));
            this.textBoxAddress1.IsComboTextBox = false;
            this.textBoxAddress1.IsModified = false;
            this.textBoxAddress1.Location = new System.Drawing.Point(204, 335);
            this.textBoxAddress1.MaxLength = 64;
            this.textBoxAddress1.Name = "textBoxAddress1";
            this.textBoxAddress1.Size = new System.Drawing.Size(252, 20);
            this.textBoxAddress1.TabIndex = 5;
            // 
            // textBoxAddress2
            // 
            this.textBoxAddress2.BackColor = System.Drawing.Color.White;
            this.textBoxAddress2.CustomReportFieldName = "";
            this.textBoxAddress2.CustomReportKey = "";
            this.textBoxAddress2.CustomReportValueType = ((byte)(1));
            this.textBoxAddress2.IsComboTextBox = false;
            this.textBoxAddress2.IsModified = false;
            this.textBoxAddress2.Location = new System.Drawing.Point(204, 359);
            this.textBoxAddress2.MaxLength = 64;
            this.textBoxAddress2.Name = "textBoxAddress2";
            this.textBoxAddress2.Size = new System.Drawing.Size(252, 20);
            this.textBoxAddress2.TabIndex = 6;
            // 
            // textBoxAddress3
            // 
            this.textBoxAddress3.BackColor = System.Drawing.Color.White;
            this.textBoxAddress3.CustomReportFieldName = "";
            this.textBoxAddress3.CustomReportKey = "";
            this.textBoxAddress3.CustomReportValueType = ((byte)(1));
            this.textBoxAddress3.IsComboTextBox = false;
            this.textBoxAddress3.IsModified = false;
            this.textBoxAddress3.Location = new System.Drawing.Point(204, 383);
            this.textBoxAddress3.MaxLength = 64;
            this.textBoxAddress3.Name = "textBoxAddress3";
            this.textBoxAddress3.Size = new System.Drawing.Size(252, 20);
            this.textBoxAddress3.TabIndex = 7;
            // 
            // textBoxCity
            // 
            this.textBoxCity.BackColor = System.Drawing.Color.White;
            this.textBoxCity.CustomReportFieldName = "";
            this.textBoxCity.CustomReportKey = "";
            this.textBoxCity.CustomReportValueType = ((byte)(1));
            this.textBoxCity.IsComboTextBox = false;
            this.textBoxCity.IsModified = false;
            this.textBoxCity.Location = new System.Drawing.Point(204, 407);
            this.textBoxCity.MaxLength = 30;
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(252, 20);
            this.textBoxCity.TabIndex = 9;
            // 
            // textBoxState
            // 
            this.textBoxState.BackColor = System.Drawing.Color.White;
            this.textBoxState.CustomReportFieldName = "";
            this.textBoxState.CustomReportKey = "";
            this.textBoxState.CustomReportValueType = ((byte)(1));
            this.textBoxState.IsComboTextBox = false;
            this.textBoxState.IsModified = false;
            this.textBoxState.Location = new System.Drawing.Point(204, 431);
            this.textBoxState.MaxLength = 30;
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(252, 20);
            this.textBoxState.TabIndex = 11;
            // 
            // textBoxCountry
            // 
            this.textBoxCountry.BackColor = System.Drawing.Color.White;
            this.textBoxCountry.CustomReportFieldName = "";
            this.textBoxCountry.CustomReportKey = "";
            this.textBoxCountry.CustomReportValueType = ((byte)(1));
            this.textBoxCountry.IsComboTextBox = false;
            this.textBoxCountry.IsModified = false;
            this.textBoxCountry.Location = new System.Drawing.Point(204, 455);
            this.textBoxCountry.MaxLength = 30;
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.Size = new System.Drawing.Size(252, 20);
            this.textBoxCountry.TabIndex = 13;
            // 
            // Root
            // 
            this.Root.AllowHtmlStringInCaption = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(940, 668);
            this.Root.TextVisible = false;
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.tabPageGeneral;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(920, 648);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabPageGeneral,
            this.tabPageDetails,
            this.layoutControlGroup3,
            this.layoutControlGroup10,
            this.tabPageActivity,
            this.layoutControlGroup12,
            this.layoutControlGroup13,
            this.layoutControlGroup14});
            this.tabbedControlGroup1.Text = "Contacts";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Credit Control Tab";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem64,
            this.layoutControlItem65,
            this.layoutControlItem66,
            this.layoutControlItem67,
            this.layoutControlItem68,
            this.layoutControlItem69,
            this.layoutControlItem70,
            this.layoutControlItem72,
            this.layoutControlItem73,
            this.layoutControlItem71,
            this.layoutControlItem74,
            this.layoutControlItem75,
            this.layoutGroupCreditLimit,
            this.layoutControlItem76,
            this.layoutControlGroup9});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(896, 603);
            this.layoutControlGroup3.Text = "Credit Control";
            // 
            // layoutControlItem64
            // 
            this.layoutControlItem64.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem64.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem64.Control = this.comboBoxPaymentMethods;
            this.layoutControlItem64.CustomizationFormText = "Payment Method";
            this.layoutControlItem64.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem64.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem64.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem64.Name = "layoutControlItem64";
            this.layoutControlItem64.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem64.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem64.Text = "Payment Method:";
            this.layoutControlItem64.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem64.Click += new System.EventHandler(this.layoutControlItem64_Click);
            // 
            // layoutControlItem65
            // 
            this.layoutControlItem65.Control = this.textBoxPaymentMethodName;
            this.layoutControlItem65.CustomizationFormText = "Payment Method Name";
            this.layoutControlItem65.Location = new System.Drawing.Point(439, 0);
            this.layoutControlItem65.MaxSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem65.MinSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem65.Name = "layoutControlItem65";
            this.layoutControlItem65.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem65.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem65.Text = " ";
            this.layoutControlItem65.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem65.TextVisible = false;
            // 
            // layoutControlItem66
            // 
            this.layoutControlItem66.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem66.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem66.Control = this.comboBoxPaymentTerms;
            this.layoutControlItem66.CustomizationFormText = "Payment Term";
            this.layoutControlItem66.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem66.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem66.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem66.Name = "layoutControlItem66";
            this.layoutControlItem66.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem66.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem66.Text = "Payment Term:";
            this.layoutControlItem66.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem66.Click += new System.EventHandler(this.layoutControlItem66_Click);
            // 
            // layoutControlItem67
            // 
            this.layoutControlItem67.Control = this.textBoxPaymentTermName;
            this.layoutControlItem67.CustomizationFormText = "Payment Term Name";
            this.layoutControlItem67.Location = new System.Drawing.Point(439, 24);
            this.layoutControlItem67.MaxSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem67.MinSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem67.Name = "layoutControlItem67";
            this.layoutControlItem67.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem67.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem67.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem67.TextVisible = false;
            // 
            // layoutControlItem68
            // 
            this.layoutControlItem68.Control = this.dateTimePickerReviewDate;
            this.layoutControlItem68.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem68.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem68.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem68.Name = "layoutControlItem68";
            this.layoutControlItem68.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem68.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem68.Text = "Credit Review Date:";
            this.layoutControlItem68.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem69
            // 
            this.layoutControlItem69.Control = this.comboBoxCreditReviewBy;
            this.layoutControlItem69.Location = new System.Drawing.Point(439, 48);
            this.layoutControlItem69.MaxSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem69.MinSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem69.Name = "layoutControlItem69";
            this.layoutControlItem69.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem69.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem69.Text = "Reviewed By:";
            this.layoutControlItem69.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem70
            // 
            this.layoutControlItem70.Control = this.comboBoxCollectionUser;
            this.layoutControlItem70.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem70.MaxSize = new System.Drawing.Size(879, 24);
            this.layoutControlItem70.MinSize = new System.Drawing.Size(879, 24);
            this.layoutControlItem70.Name = "layoutControlItem70";
            this.layoutControlItem70.Size = new System.Drawing.Size(896, 24);
            this.layoutControlItem70.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem70.Text = "Collection User:";
            this.layoutControlItem70.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem72
            // 
            this.layoutControlItem72.Control = this.dateTimePickerRatingDate;
            this.layoutControlItem72.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem72.MaxSize = new System.Drawing.Size(245, 24);
            this.layoutControlItem72.MinSize = new System.Drawing.Size(245, 24);
            this.layoutControlItem72.Name = "layoutControlItem72";
            this.layoutControlItem72.Size = new System.Drawing.Size(245, 24);
            this.layoutControlItem72.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem72.Text = "Credit Rating:";
            this.layoutControlItem72.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem73
            // 
            this.layoutControlItem73.Control = this.comboBoxRatingBy;
            this.layoutControlItem73.Location = new System.Drawing.Point(561, 96);
            this.layoutControlItem73.MaxSize = new System.Drawing.Size(318, 24);
            this.layoutControlItem73.MinSize = new System.Drawing.Size(318, 24);
            this.layoutControlItem73.Name = "layoutControlItem73";
            this.layoutControlItem73.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItem73.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem73.Text = "Rating By:";
            this.layoutControlItem73.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem71
            // 
            this.layoutControlItem71.Control = this.comboBoxRating;
            this.layoutControlItem71.CustomizationFormText = "Rating Date:";
            this.layoutControlItem71.Location = new System.Drawing.Point(245, 96);
            this.layoutControlItem71.MaxSize = new System.Drawing.Size(316, 24);
            this.layoutControlItem71.MinSize = new System.Drawing.Size(316, 24);
            this.layoutControlItem71.Name = "layoutControlItem71";
            this.layoutControlItem71.Size = new System.Drawing.Size(316, 24);
            this.layoutControlItem71.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem71.Text = "Rating Date:";
            this.layoutControlItem71.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem74
            // 
            this.layoutControlItem74.Control = this.dateTimeBalanceConfirmationDate;
            this.layoutControlItem74.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem74.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem74.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem74.Name = "layoutControlItem74";
            this.layoutControlItem74.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem74.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem74.Text = "Bal Confirm Date:";
            this.layoutControlItem74.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem75
            // 
            this.layoutControlItem75.Control = this.textBoxConfirmationLevel;
            this.layoutControlItem75.Location = new System.Drawing.Point(439, 120);
            this.layoutControlItem75.MaxSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem75.MinSize = new System.Drawing.Size(440, 24);
            this.layoutControlItem75.Name = "layoutControlItem75";
            this.layoutControlItem75.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem75.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem75.Text = "Bal Confirm Interval:";
            this.layoutControlItem75.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutGroupCreditLimit
            // 
            this.layoutGroupCreditLimit.ExpandButtonVisible = true;
            this.layoutGroupCreditLimit.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem77,
            this.layoutControlItem78,
            this.layoutControlItem81,
            this.layoutControlItem79,
            this.layoutControlItem80,
            this.layoutControlItem82,
            this.layoutControlItem83,
            this.layoutControlItem84,
            this.layoutControlItem85,
            this.layoutControlItem86,
            this.layoutControlItem87,
            this.layoutControlItem88,
            this.layoutControlItem89,
            this.layoutControlItem90,
            this.layoutControlItem91,
            this.layoutControlItem92,
            this.emptySpaceItem8});
            this.layoutGroupCreditLimit.Location = new System.Drawing.Point(0, 168);
            this.layoutGroupCreditLimit.Name = "layoutGroupCreditLimit";
            this.layoutGroupCreditLimit.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutGroupCreditLimit.Size = new System.Drawing.Size(896, 191);
            this.layoutGroupCreditLimit.Text = "Credit Limit";
            // 
            // layoutControlItem77
            // 
            this.layoutControlItem77.Control = this.radioButtonCreditLimitUnlimited;
            this.layoutControlItem77.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem77.MaxSize = new System.Drawing.Size(160, 29);
            this.layoutControlItem77.MinSize = new System.Drawing.Size(160, 29);
            this.layoutControlItem77.Name = "layoutControlItem77";
            this.layoutControlItem77.Size = new System.Drawing.Size(160, 29);
            this.layoutControlItem77.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem77.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem77.TextVisible = false;
            // 
            // layoutControlItem78
            // 
            this.layoutControlItem78.Control = this.radioButtonCreditLimitNoCredit;
            this.layoutControlItem78.Location = new System.Drawing.Point(160, 0);
            this.layoutControlItem78.MaxSize = new System.Drawing.Size(157, 29);
            this.layoutControlItem78.MinSize = new System.Drawing.Size(157, 29);
            this.layoutControlItem78.Name = "layoutControlItem78";
            this.layoutControlItem78.Size = new System.Drawing.Size(157, 29);
            this.layoutControlItem78.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem78.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem78.TextVisible = false;
            // 
            // layoutControlItem81
            // 
            this.layoutControlItem81.Control = this.radioButtonSublimit;
            this.layoutControlItem81.Location = new System.Drawing.Point(317, 0);
            this.layoutControlItem81.MaxSize = new System.Drawing.Size(179, 29);
            this.layoutControlItem81.MinSize = new System.Drawing.Size(179, 29);
            this.layoutControlItem81.Name = "layoutControlItem81";
            this.layoutControlItem81.Size = new System.Drawing.Size(179, 29);
            this.layoutControlItem81.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem81.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem81.TextVisible = false;
            // 
            // layoutControlItem79
            // 
            this.layoutControlItem79.Control = this.radioButtonCreditLimitAmount;
            this.layoutControlItem79.Location = new System.Drawing.Point(496, 0);
            this.layoutControlItem79.MaxSize = new System.Drawing.Size(145, 29);
            this.layoutControlItem79.MinSize = new System.Drawing.Size(145, 29);
            this.layoutControlItem79.Name = "layoutControlItem79";
            this.layoutControlItem79.Size = new System.Drawing.Size(145, 29);
            this.layoutControlItem79.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem79.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem79.TextVisible = false;
            // 
            // layoutControlItem80
            // 
            this.layoutControlItem80.Control = this.textBoxCreditLimit;
            this.layoutControlItem80.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItem80.CustomizationFormText = "Credit Limit Amount";
            this.layoutControlItem80.Location = new System.Drawing.Point(641, 0);
            this.layoutControlItem80.MaxSize = new System.Drawing.Size(232, 29);
            this.layoutControlItem80.MinSize = new System.Drawing.Size(232, 29);
            this.layoutControlItem80.Name = "layoutControlItem80";
            this.layoutControlItem80.Size = new System.Drawing.Size(247, 29);
            this.layoutControlItem80.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem80.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem80.TextVisible = false;
            // 
            // layoutControlItem82
            // 
            this.layoutControlItem82.Control = this.dateTimePickerCLValidity;
            this.layoutControlItem82.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem82.MaxSize = new System.Drawing.Size(319, 24);
            this.layoutControlItem82.MinSize = new System.Drawing.Size(319, 24);
            this.layoutControlItem82.Name = "layoutControlItem82";
            this.layoutControlItem82.Size = new System.Drawing.Size(319, 24);
            this.layoutControlItem82.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem82.Text = "Limit Validity:";
            this.layoutControlItem82.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem83
            // 
            this.layoutControlItem83.Control = this.checkBoxUnsecuredLimit;
            this.layoutControlItem83.CustomizationFormText = "PDC Unsecured Limit";
            this.layoutControlItem83.Location = new System.Drawing.Point(319, 29);
            this.layoutControlItem83.MaxSize = new System.Drawing.Size(178, 24);
            this.layoutControlItem83.MinSize = new System.Drawing.Size(178, 24);
            this.layoutControlItem83.Name = "layoutControlItem83";
            this.layoutControlItem83.Size = new System.Drawing.Size(178, 24);
            this.layoutControlItem83.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem83.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem83.TextVisible = false;
            // 
            // layoutControlItem84
            // 
            this.layoutControlItem84.Control = this.textBoxUnsecuredLimit;
            this.layoutControlItem84.CustomizationFormText = "PDC Unsecured Limit";
            this.layoutControlItem84.Location = new System.Drawing.Point(497, 29);
            this.layoutControlItem84.MaxSize = new System.Drawing.Size(126, 24);
            this.layoutControlItem84.MinSize = new System.Drawing.Size(126, 24);
            this.layoutControlItem84.Name = "layoutControlItem84";
            this.layoutControlItem84.Size = new System.Drawing.Size(126, 24);
            this.layoutControlItem84.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem84.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem84.TextVisible = false;
            // 
            // layoutControlItem85
            // 
            this.layoutControlItem85.Control = this.textBoxTempLimit;
            this.layoutControlItem85.Location = new System.Drawing.Point(623, 29);
            this.layoutControlItem85.MaxSize = new System.Drawing.Size(250, 24);
            this.layoutControlItem85.MinSize = new System.Drawing.Size(250, 24);
            this.layoutControlItem85.Name = "layoutControlItem85";
            this.layoutControlItem85.Size = new System.Drawing.Size(265, 24);
            this.layoutControlItem85.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem85.Text = "Temp Limit:";
            this.layoutControlItem85.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem86
            // 
            this.layoutControlItem86.Control = this.checkBoxAcceptCheque;
            this.layoutControlItem86.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem86.MaxSize = new System.Drawing.Size(198, 24);
            this.layoutControlItem86.MinSize = new System.Drawing.Size(198, 24);
            this.layoutControlItem86.Name = "layoutControlItem86";
            this.layoutControlItem86.Size = new System.Drawing.Size(198, 24);
            this.layoutControlItem86.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem86.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem86.TextVisible = false;
            // 
            // layoutControlItem87
            // 
            this.layoutControlItem87.Control = this.checkBoxAcceptPDC;
            this.layoutControlItem87.Location = new System.Drawing.Point(198, 53);
            this.layoutControlItem87.MaxSize = new System.Drawing.Size(299, 24);
            this.layoutControlItem87.MinSize = new System.Drawing.Size(299, 24);
            this.layoutControlItem87.Name = "layoutControlItem87";
            this.layoutControlItem87.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem87.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem87.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem87.TextVisible = false;
            // 
            // layoutControlItem88
            // 
            this.layoutControlItem88.Control = this.textBoxGraceDays;
            this.layoutControlItem88.Location = new System.Drawing.Point(497, 53);
            this.layoutControlItem88.MaxSize = new System.Drawing.Size(376, 24);
            this.layoutControlItem88.MinSize = new System.Drawing.Size(376, 24);
            this.layoutControlItem88.Name = "layoutControlItem88";
            this.layoutControlItem88.Size = new System.Drawing.Size(391, 24);
            this.layoutControlItem88.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem88.Text = "Grace Days on Overdue:";
            this.layoutControlItem88.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem89
            // 
            this.layoutControlItem89.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem89.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.layoutControlItem89.Control = this.pictureBoxPhoto;
            this.layoutControlItem89.Location = new System.Drawing.Point(0, 77);
            this.layoutControlItem89.MaxSize = new System.Drawing.Size(271, 72);
            this.layoutControlItem89.MinSize = new System.Drawing.Size(271, 72);
            this.layoutControlItem89.Name = "layoutControlItem89";
            this.layoutControlItem89.Size = new System.Drawing.Size(271, 72);
            this.layoutControlItem89.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem89.Text = "Signature:";
            this.layoutControlItem89.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem90
            // 
            this.layoutControlItem90.Control = this.linkAddPicture;
            this.layoutControlItem90.Location = new System.Drawing.Point(271, 77);
            this.layoutControlItem90.MaxSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem90.MinSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem90.Name = "layoutControlItem90";
            this.layoutControlItem90.Size = new System.Drawing.Size(617, 24);
            this.layoutControlItem90.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem90.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem90.TextVisible = false;
            // 
            // layoutControlItem91
            // 
            this.layoutControlItem91.Control = this.linkRemovePicture;
            this.layoutControlItem91.Location = new System.Drawing.Point(271, 101);
            this.layoutControlItem91.MaxSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem91.MinSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem91.Name = "layoutControlItem91";
            this.layoutControlItem91.Size = new System.Drawing.Size(617, 24);
            this.layoutControlItem91.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem91.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem91.TextVisible = false;
            // 
            // layoutControlItem92
            // 
            this.layoutControlItem92.Control = this.linkLoadImage;
            this.layoutControlItem92.Location = new System.Drawing.Point(271, 125);
            this.layoutControlItem92.MaxSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem92.MinSize = new System.Drawing.Size(617, 24);
            this.layoutControlItem92.Name = "layoutControlItem92";
            this.layoutControlItem92.Size = new System.Drawing.Size(617, 24);
            this.layoutControlItem92.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem92.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem92.TextVisible = false;
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.Location = new System.Drawing.Point(0, 149);
            this.emptySpaceItem8.MaxSize = new System.Drawing.Size(873, 12);
            this.emptySpaceItem8.MinSize = new System.Drawing.Size(873, 12);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(888, 12);
            this.emptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem76
            // 
            this.layoutControlItem76.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem76.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.layoutControlItem76.Control = this.textBoxRatingRemarks;
            this.layoutControlItem76.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem76.MaxSize = new System.Drawing.Size(879, 24);
            this.layoutControlItem76.MinSize = new System.Drawing.Size(879, 24);
            this.layoutControlItem76.Name = "layoutControlItem76";
            this.layoutControlItem76.Size = new System.Drawing.Size(896, 24);
            this.layoutControlItem76.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem76.Text = "Remarks:";
            this.layoutControlItem76.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.ExpandButtonVisible = true;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem95,
            this.layoutControlItem93,
            this.panelInsuranceDetails,
            this.layoutControlItem94,
            this.layoutControlItem96,
            this.emptySpaceItem18});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 359);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 4, 9);
            this.layoutControlGroup9.Size = new System.Drawing.Size(896, 244);
            this.layoutControlGroup9.Text = "Credit Insurance Info";
            // 
            // layoutControlItem95
            // 
            this.layoutControlItem95.Control = this.comboBoxInsuranceStatus;
            this.layoutControlItem95.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem95.MaxSize = new System.Drawing.Size(425, 25);
            this.layoutControlItem95.MinSize = new System.Drawing.Size(425, 25);
            this.layoutControlItem95.Name = "layoutControlItem95";
            this.layoutControlItem95.Size = new System.Drawing.Size(425, 25);
            this.layoutControlItem95.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem95.Text = "Insurance Status:";
            this.layoutControlItem95.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem93
            // 
            this.layoutControlItem93.Control = this.buttonCustomerInsuranceClaim;
            this.layoutControlItem93.Location = new System.Drawing.Point(425, 0);
            this.layoutControlItem93.MaxSize = new System.Drawing.Size(212, 25);
            this.layoutControlItem93.MinSize = new System.Drawing.Size(212, 25);
            this.layoutControlItem93.Name = "layoutControlItem93";
            this.layoutControlItem93.Size = new System.Drawing.Size(212, 25);
            this.layoutControlItem93.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem93.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem93.TextVisible = false;
            // 
            // panelInsuranceDetails
            // 
            this.panelInsuranceDetails.AppearanceGroup.BorderColor = System.Drawing.Color.Transparent;
            this.panelInsuranceDetails.AppearanceGroup.Options.UseBorderColor = true;
            this.panelInsuranceDetails.GroupBordersVisible = false;
            this.panelInsuranceDetails.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.panelInsuranceDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem16,
            this.layoutControlItem97,
            this.layoutControlItem98,
            this.layoutControlItem99,
            this.layoutControlItem100,
            this.layoutControlItem101,
            this.layoutControlItem102,
            this.layoutControlItem103,
            this.layoutControlItem104,
            this.layoutControlItem105});
            this.panelInsuranceDetails.Location = new System.Drawing.Point(0, 49);
            this.panelInsuranceDetails.Name = "panelInsuranceDetails";
            this.panelInsuranceDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 9, 9);
            this.panelInsuranceDetails.Size = new System.Drawing.Size(888, 154);
            this.panelInsuranceDetails.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
            this.panelInsuranceDetails.Text = "InsuranceInfo";
            this.panelInsuranceDetails.TextVisible = false;
            // 
            // emptySpaceItem16
            // 
            this.emptySpaceItem16.AllowHotTrack = false;
            this.emptySpaceItem16.Location = new System.Drawing.Point(707, 0);
            this.emptySpaceItem16.Name = "emptySpaceItem16";
            this.emptySpaceItem16.Size = new System.Drawing.Size(181, 48);
            this.emptySpaceItem16.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem97
            // 
            this.layoutControlItem97.Control = this.dateTimePickerInsuranceDate;
            this.layoutControlItem97.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem97.MaxSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem97.MinSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem97.Name = "layoutControlItem97";
            this.layoutControlItem97.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem97.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem97.Text = "Application Date:";
            this.layoutControlItem97.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem98
            // 
            this.layoutControlItem98.Control = this.textBoxInsuranceNumber;
            this.layoutControlItem98.Location = new System.Drawing.Point(365, 0);
            this.layoutControlItem98.MaxSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem98.MinSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem98.Name = "layoutControlItem98";
            this.layoutControlItem98.Size = new System.Drawing.Size(342, 24);
            this.layoutControlItem98.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem98.Text = "Application Number:";
            this.layoutControlItem98.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem99
            // 
            this.layoutControlItem99.Control = this.textBoxInsuranceReqAmount;
            this.layoutControlItem99.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem99.MaxSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem99.MinSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem99.Name = "layoutControlItem99";
            this.layoutControlItem99.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem99.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem99.Text = "Requested Amount:";
            this.layoutControlItem99.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem100
            // 
            this.layoutControlItem100.Control = this.textBoxInsuranceApprovedAmount;
            this.layoutControlItem100.Location = new System.Drawing.Point(365, 24);
            this.layoutControlItem100.MaxSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem100.MinSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem100.Name = "layoutControlItem100";
            this.layoutControlItem100.Size = new System.Drawing.Size(342, 24);
            this.layoutControlItem100.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem100.Text = "Approved Amount:";
            this.layoutControlItem100.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem101
            // 
            this.layoutControlItem101.Control = this.comboBoxInsuranceRating;
            this.layoutControlItem101.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem101.MaxSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem101.MinSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem101.Name = "layoutControlItem101";
            this.layoutControlItem101.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem101.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem101.Text = "Rating:";
            this.layoutControlItem101.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem102
            // 
            this.layoutControlItem102.Control = this.textBoxInsuranceID;
            this.layoutControlItem102.Location = new System.Drawing.Point(365, 48);
            this.layoutControlItem102.MaxSize = new System.Drawing.Size(504, 24);
            this.layoutControlItem102.MinSize = new System.Drawing.Size(504, 24);
            this.layoutControlItem102.Name = "layoutControlItem102";
            this.layoutControlItem102.Size = new System.Drawing.Size(523, 24);
            this.layoutControlItem102.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem102.Text = "Insurance ID:";
            this.layoutControlItem102.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem103
            // 
            this.layoutControlItem103.Control = this.datetimePickerEffectiveDate;
            this.layoutControlItem103.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem103.MaxSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem103.MinSize = new System.Drawing.Size(365, 24);
            this.layoutControlItem103.Name = "layoutControlItem103";
            this.layoutControlItem103.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem103.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem103.Text = "Effective Date:";
            this.layoutControlItem103.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem104
            // 
            this.layoutControlItem104.Control = this.dateTimePickerValidTo;
            this.layoutControlItem104.Location = new System.Drawing.Point(365, 72);
            this.layoutControlItem104.MaxSize = new System.Drawing.Size(504, 24);
            this.layoutControlItem104.MinSize = new System.Drawing.Size(504, 24);
            this.layoutControlItem104.Name = "layoutControlItem104";
            this.layoutControlItem104.Size = new System.Drawing.Size(523, 24);
            this.layoutControlItem104.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem104.Text = "Valid To:";
            this.layoutControlItem104.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem105
            // 
            this.layoutControlItem105.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem105.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.layoutControlItem105.Control = this.textBoxInsuranceRemarks;
            this.layoutControlItem105.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem105.MaxSize = new System.Drawing.Size(869, 46);
            this.layoutControlItem105.MinSize = new System.Drawing.Size(869, 46);
            this.layoutControlItem105.Name = "layoutControlItem105";
            this.layoutControlItem105.Size = new System.Drawing.Size(888, 58);
            this.layoutControlItem105.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem105.Text = "Remarks:";
            this.layoutControlItem105.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem94
            // 
            this.layoutControlItem94.Control = this.comboBoxInsuranceProvider;
            this.layoutControlItem94.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem94.MaxSize = new System.Drawing.Size(425, 24);
            this.layoutControlItem94.MinSize = new System.Drawing.Size(425, 24);
            this.layoutControlItem94.Name = "layoutControlItem94";
            this.layoutControlItem94.Size = new System.Drawing.Size(425, 24);
            this.layoutControlItem94.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem94.Text = "Provider:";
            this.layoutControlItem94.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem96
            // 
            this.layoutControlItem96.Control = this.textBoxProvider;
            this.layoutControlItem96.Location = new System.Drawing.Point(425, 25);
            this.layoutControlItem96.Name = "layoutControlItem96";
            this.layoutControlItem96.Size = new System.Drawing.Size(463, 24);
            this.layoutControlItem96.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem96.TextVisible = false;
            // 
            // emptySpaceItem18
            // 
            this.emptySpaceItem18.AllowHotTrack = false;
            this.emptySpaceItem18.Location = new System.Drawing.Point(637, 0);
            this.emptySpaceItem18.MaxSize = new System.Drawing.Size(216, 25);
            this.emptySpaceItem18.MinSize = new System.Drawing.Size(216, 25);
            this.emptySpaceItem18.Name = "emptySpaceItem18";
            this.emptySpaceItem18.Size = new System.Drawing.Size(251, 25);
            this.emptySpaceItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem18.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.CustomizationFormText = "General";
            this.tabPageGeneral.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem15,
            this.layoutControlGroup4,
            this.emptySpaceItem5});
            this.tabPageGeneral.Location = new System.Drawing.Point(0, 0);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(896, 603);
            this.tabPageGeneral.Text = "&General";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.textBoxCode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Customer Code:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.textBoxName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Customer Name:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textBoxFormalName;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Short Name:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textBoxForeignName;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Foreign Name:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.comboBoxParentCustomer;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Parent Customer:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.checkBoxparentACforposting;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(439, 24);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(439, 24);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.checkBoxIsInactive;
            this.layoutControlItem7.Location = new System.Drawing.Point(439, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(98, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(98, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(98, 24);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.checkBoxHold;
            this.layoutControlItem8.Location = new System.Drawing.Point(537, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(342, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(359, 24);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AllowHtmlStringInCaption = true;
            this.layoutControlItem9.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem9.Control = this.comboBoxCustomerClass;
            this.layoutControlItem9.Location = new System.Drawing.Point(439, 24);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Customer Class:";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem9.Click += new System.EventHandler(this.layoutControlItem9_Click);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem10.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem10.Control = this.comboBoxCustomerGroup;
            this.layoutControlItem10.Location = new System.Drawing.Point(439, 48);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "Customer Group:";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem10.Click += new System.EventHandler(this.layoutControlItem10_Click);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem11.Control = this.comboBoxCountry;
            this.layoutControlItem11.Location = new System.Drawing.Point(439, 72);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "Country:";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem11.Click += new System.EventHandler(this.layoutControlItem11_Click);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem12.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem12.Control = this.comboBoxArea;
            this.layoutControlItem12.Location = new System.Drawing.Point(439, 96);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "Area:";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem12.Click += new System.EventHandler(this.layoutControlItem12_Click);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.comboBoxPriceLevel;
            this.layoutControlItem13.Location = new System.Drawing.Point(439, 120);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.Text = "Price Level:";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem14.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem14.Control = this.comboBoxCurrency;
            this.layoutControlItem14.Location = new System.Drawing.Point(439, 144);
            this.layoutControlItem14.MaxSize = new System.Drawing.Size(455, 0);
            this.layoutControlItem14.MinSize = new System.Drawing.Size(455, 24);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem14.Text = "Currency:";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem14.Click += new System.EventHandler(this.layoutControlItem14_Click);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 144);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(439, 24);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(439, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(439, 24);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 168);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(719, 31);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(719, 31);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(719, 31);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.buttonCategories;
            this.layoutControlItem15.Location = new System.Drawing.Point(719, 168);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(175, 31);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(175, 31);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(177, 31);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.AppearanceGroup.BackColor = System.Drawing.Color.White;
            this.layoutControlGroup4.AppearanceGroup.BackColor2 = System.Drawing.Color.White;
            this.layoutControlGroup4.AppearanceGroup.Options.UseBackColor = true;
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.layoutControlGroup4.ExpandButtonVisible = true;
            this.layoutControlGroup4.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem18,
            this.layoutControlItem26,
            this.layoutControlItem27,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem28,
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.layoutControlItem34,
            this.layoutControlItem35,
            this.layoutControlItem36,
            this.layoutControlItem33,
            this.layoutControlItem37,
            this.layoutControlItem25,
            this.emptySpaceItem4,
            this.emptySpaceItem6,
            this.emptySpaceItem7});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 209);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(896, 394);
            this.layoutControlGroup4.Text = "Primary Address";
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.textBoxAddressID;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem16.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.Text = "Address ID:";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.textBoxContactName;
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem17.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem17.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem17.Text = "Contact Name:";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.textBoxAddress1;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem18.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.Text = "Adress:";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.textBoxDepartment;
            this.layoutControlItem26.Location = new System.Drawing.Point(423, 0);
            this.layoutControlItem26.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem26.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem26.Text = "Department:";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.textBoxPhone1;
            this.layoutControlItem27.Location = new System.Drawing.Point(423, 24);
            this.layoutControlItem27.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem27.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem27.Text = "Phone 1:";
            this.layoutControlItem27.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.textBoxAddress2;
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem19.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.Text = " ";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.textBoxAddress3;
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem20.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem20.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem20.Text = " ";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.textBoxCity;
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem21.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem21.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem21.Text = "City:";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.textBoxState;
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem22.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem22.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem22.Text = "State:";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.textBoxCountry;
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.Text = "Country:";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.textBoxPostalCode;
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 192);
            this.layoutControlItem24.MaxSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem24.MinSize = new System.Drawing.Size(423, 24);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem24.Text = "Postal Code:";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.textBoxPhone2;
            this.layoutControlItem28.Location = new System.Drawing.Point(423, 48);
            this.layoutControlItem28.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem28.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem28.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem28.Text = "Phone 2:";
            this.layoutControlItem28.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.textBoxFax;
            this.layoutControlItem29.Location = new System.Drawing.Point(423, 72);
            this.layoutControlItem29.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem29.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem29.Text = "Fax:";
            this.layoutControlItem29.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.textBoxMobile;
            this.layoutControlItem30.Location = new System.Drawing.Point(423, 96);
            this.layoutControlItem30.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem30.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem30.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem30.Text = "Mobile:";
            this.layoutControlItem30.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.textBoxEmail;
            this.layoutControlItem31.Location = new System.Drawing.Point(423, 120);
            this.layoutControlItem31.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem31.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem31.Text = "Email:";
            this.layoutControlItem31.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.textBoxWebsite;
            this.layoutControlItem32.Location = new System.Drawing.Point(423, 144);
            this.layoutControlItem32.MaxSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem32.MinSize = new System.Drawing.Size(447, 24);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(447, 24);
            this.layoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem32.Text = "Website:";
            this.layoutControlItem32.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.textBoxLatitude;
            this.layoutControlItem34.Location = new System.Drawing.Point(423, 168);
            this.layoutControlItem34.MaxSize = new System.Drawing.Size(327, 24);
            this.layoutControlItem34.MinSize = new System.Drawing.Size(327, 24);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(327, 24);
            this.layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem34.Text = "Latitude:";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.textBoxLongitude;
            this.layoutControlItem35.Location = new System.Drawing.Point(423, 192);
            this.layoutControlItem35.MaxSize = new System.Drawing.Size(327, 24);
            this.layoutControlItem35.MinSize = new System.Drawing.Size(327, 24);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(327, 24);
            this.layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem35.Text = "Longitude:";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.ultraPictureBox1;
            this.layoutControlItem36.Location = new System.Drawing.Point(750, 168);
            this.layoutControlItem36.MaxSize = new System.Drawing.Size(120, 48);
            this.layoutControlItem36.MinSize = new System.Drawing.Size(120, 48);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(120, 48);
            this.layoutControlItem36.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem36.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.textBoxComment;
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 216);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(423, 24);
            this.layoutControlItem33.Text = "Comment:";
            this.layoutControlItem33.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.buttonMoreAddress;
            this.layoutControlItem37.Location = new System.Drawing.Point(708, 216);
            this.layoutControlItem37.MaxSize = new System.Drawing.Size(161, 42);
            this.layoutControlItem37.MinSize = new System.Drawing.Size(161, 42);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(162, 42);
            this.layoutControlItem37.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem37.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem37.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem25.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.layoutControlItem25.Control = this.textBoxAddressPrintFormat;
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 240);
            this.layoutControlItem25.MaxSize = new System.Drawing.Size(423, 98);
            this.layoutControlItem25.MinSize = new System.Drawing.Size(423, 98);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(423, 98);
            this.layoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem25.Text = "Print Format:";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(164, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(708, 258);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(151, 80);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(151, 80);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(162, 80);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.Location = new System.Drawing.Point(423, 216);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(285, 122);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(285, 122);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(285, 122);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.Location = new System.Drawing.Point(0, 338);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(870, 10);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 199);
            this.emptySpaceItem5.MaxSize = new System.Drawing.Size(879, 10);
            this.emptySpaceItem5.MinSize = new System.Drawing.Size(879, 10);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(896, 10);
            this.emptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.CustomizationFormText = "Details Tab";
            this.tabPageDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem38,
            this.layoutControlItem39,
            this.layoutControlItem40,
            this.layoutControlItem41,
            this.layoutControlItem42,
            this.layoutControlItem43,
            this.layoutControlItem44,
            this.layoutControlItem45,
            this.layoutControlItem46,
            this.emptySpaceItem9,
            this.emptySpaceItem10,
            this.layoutControlItem47,
            this.layoutControlItem50,
            this.layoutControlItem51,
            this.layoutControlItem52,
            this.emptySpaceItem11,
            this.layoutControlItem53,
            this.emptySpaceItem12,
            this.layoutControlGroup5,
            this.layoutControlGroup6,
            this.layoutItemConsignmentCom,
            this.layoutControlItem48,
            this.emptySpaceItem15,
            this.layoutControlGroup7,
            this.layoutControlItem54});
            this.tabPageDetails.Location = new System.Drawing.Point(0, 0);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Size = new System.Drawing.Size(896, 603);
            this.tabPageDetails.Text = "&Details";
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem38.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem38.Control = this.comboBoxSalesperson;
            this.layoutControlItem38.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem38.MaxSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem38.MinSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(398, 24);
            this.layoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem38.Text = "Salesperson:";
            this.layoutControlItem38.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem38.Click += new System.EventHandler(this.layoutControlItem38_Click);
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem39.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem39.Control = this.comboBoxShiptoAddress;
            this.layoutControlItem39.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem39.MaxSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem39.MinSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(398, 24);
            this.layoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem39.Text = "Ship to Address:";
            this.layoutControlItem39.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem39.Click += new System.EventHandler(this.layoutControlItem39_Click);
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.dateTimePickerCustomerSince;
            this.layoutControlItem40.CustomizationFormText = "Customer Since";
            this.layoutControlItem40.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem40.MaxSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem40.MinSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(398, 24);
            this.layoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem40.Text = "Customer Since:";
            this.layoutControlItem40.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.comboBoxLeadSource;
            this.layoutControlItem41.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem41.MaxSize = new System.Drawing.Size(398, 25);
            this.layoutControlItem41.MinSize = new System.Drawing.Size(398, 25);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(398, 25);
            this.layoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem41.Text = "Lead Source:";
            this.layoutControlItem41.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem41.Click += new System.EventHandler(this.layoutControlItem41_Click);
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem42.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem42.Control = this.comboBoxBilltoAddress;
            this.layoutControlItem42.Location = new System.Drawing.Point(398, 0);
            this.layoutControlItem42.MaxSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem42.MinSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem42.Text = "Bill to Address:";
            this.layoutControlItem42.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem42.Click += new System.EventHandler(this.layoutControlItem42_Click);
            // 
            // layoutControlItem43
            // 
            this.layoutControlItem43.AppearanceItemCaption.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.layoutControlItem43.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem43.Control = this.comboBoxShippingMethods;
            this.layoutControlItem43.Location = new System.Drawing.Point(398, 24);
            this.layoutControlItem43.MaxSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem43.MinSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem43.Name = "layoutControlItem43";
            this.layoutControlItem43.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem43.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem43.Text = "Shipping Method:";
            this.layoutControlItem43.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem43.Click += new System.EventHandler(this.layoutControlItem43_Click);
            // 
            // layoutControlItem44
            // 
            this.layoutControlItem44.Control = this.dateTimePickerEstablished;
            this.layoutControlItem44.Location = new System.Drawing.Point(398, 48);
            this.layoutControlItem44.MaxSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem44.MinSize = new System.Drawing.Size(467, 24);
            this.layoutControlItem44.Name = "layoutControlItem44";
            this.layoutControlItem44.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem44.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem44.Text = "Date Established:";
            this.layoutControlItem44.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem45
            // 
            this.layoutControlItem45.Control = this.comboBoxStatementMethod;
            this.layoutControlItem45.Location = new System.Drawing.Point(398, 72);
            this.layoutControlItem45.MaxSize = new System.Drawing.Size(467, 25);
            this.layoutControlItem45.MinSize = new System.Drawing.Size(467, 25);
            this.layoutControlItem45.Name = "layoutControlItem45";
            this.layoutControlItem45.Size = new System.Drawing.Size(498, 25);
            this.layoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem45.Text = "Statement Method:";
            this.layoutControlItem45.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem46
            // 
            this.layoutControlItem46.Control = this.textBoxStatementEmail;
            this.layoutControlItem46.Location = new System.Drawing.Point(0, 97);
            this.layoutControlItem46.MaxSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem46.MinSize = new System.Drawing.Size(398, 24);
            this.layoutControlItem46.Name = "layoutControlItem46";
            this.layoutControlItem46.Size = new System.Drawing.Size(398, 24);
            this.layoutControlItem46.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem46.Text = "Statement Email:";
            this.layoutControlItem46.TextSize = new System.Drawing.Size(164, 13);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.Location = new System.Drawing.Point(398, 97);
            this.emptySpaceItem9.MaxSize = new System.Drawing.Size(468, 24);
            this.emptySpaceItem9.MinSize = new System.Drawing.Size(468, 24);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(498, 24);
            this.emptySpaceItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 121);
            this.emptySpaceItem10.MaxSize = new System.Drawing.Size(863, 14);
            this.emptySpaceItem10.MinSize = new System.Drawing.Size(863, 14);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(896, 14);
            this.emptySpaceItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem47
            // 
            this.layoutControlItem47.Control = this.checkBoxWeightInvoice;
            this.layoutControlItem47.Location = new System.Drawing.Point(0, 135);
            this.layoutControlItem47.MaxSize = new System.Drawing.Size(169, 24);
            this.layoutControlItem47.MinSize = new System.Drawing.Size(169, 24);
            this.layoutControlItem47.Name = "layoutControlItem47";
            this.layoutControlItem47.Size = new System.Drawing.Size(169, 24);
            this.layoutControlItem47.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem47.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem47.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.textBoxLicenseNumber;
            this.layoutControlItem50.Location = new System.Drawing.Point(0, 159);
            this.layoutControlItem50.MaxSize = new System.Drawing.Size(383, 24);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(383, 24);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(383, 24);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Text = "Trade Licence No:";
            this.layoutControlItem50.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem51
            // 
            this.layoutControlItem51.Control = this.dateTimePickerLicenseExpDate;
            this.layoutControlItem51.Location = new System.Drawing.Point(383, 159);
            this.layoutControlItem51.MaxSize = new System.Drawing.Size(483, 24);
            this.layoutControlItem51.MinSize = new System.Drawing.Size(483, 24);
            this.layoutControlItem51.Name = "layoutControlItem51";
            this.layoutControlItem51.Size = new System.Drawing.Size(513, 24);
            this.layoutControlItem51.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem51.Text = "Expiry Date:";
            this.layoutControlItem51.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem52
            // 
            this.layoutControlItem52.Control = this.dateTimePickerContractExpDate;
            this.layoutControlItem52.Location = new System.Drawing.Point(0, 183);
            this.layoutControlItem52.MaxSize = new System.Drawing.Size(383, 24);
            this.layoutControlItem52.MinSize = new System.Drawing.Size(383, 24);
            this.layoutControlItem52.Name = "layoutControlItem52";
            this.layoutControlItem52.Size = new System.Drawing.Size(383, 24);
            this.layoutControlItem52.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem52.Text = "Contract Expiry Date:";
            this.layoutControlItem52.TextSize = new System.Drawing.Size(164, 13);
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            this.emptySpaceItem11.Location = new System.Drawing.Point(383, 183);
            this.emptySpaceItem11.MaxSize = new System.Drawing.Size(483, 24);
            this.emptySpaceItem11.MinSize = new System.Drawing.Size(483, 24);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(513, 24);
            this.emptySpaceItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem53
            // 
            this.layoutControlItem53.Control = this.textBoxDiscountPercent;
            this.layoutControlItem53.Location = new System.Drawing.Point(0, 207);
            this.layoutControlItem53.MaxSize = new System.Drawing.Size(274, 24);
            this.layoutControlItem53.MinSize = new System.Drawing.Size(274, 24);
            this.layoutControlItem53.Name = "layoutControlItem53";
            this.layoutControlItem53.Size = new System.Drawing.Size(274, 24);
            this.layoutControlItem53.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem53.Text = "Discount %:";
            this.layoutControlItem53.TextSize = new System.Drawing.Size(164, 13);
            // 
            // emptySpaceItem12
            // 
            this.emptySpaceItem12.AllowHotTrack = false;
            this.emptySpaceItem12.Location = new System.Drawing.Point(456, 207);
            this.emptySpaceItem12.MaxSize = new System.Drawing.Size(411, 24);
            this.emptySpaceItem12.MinSize = new System.Drawing.Size(411, 24);
            this.emptySpaceItem12.Name = "emptySpaceItem12";
            this.emptySpaceItem12.Size = new System.Drawing.Size(440, 24);
            this.emptySpaceItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.ExpandButtonVisible = true;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem55,
            this.layoutControlItem56,
            this.layoutControlItem57});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 231);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 9, 9, 9);
            this.layoutControlGroup5.Size = new System.Drawing.Size(452, 119);
            this.layoutControlGroup5.Text = "Bank Info";
            // 
            // layoutControlItem55
            // 
            this.layoutControlItem55.Control = this.textBoxBankName;
            this.layoutControlItem55.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem55.MaxSize = new System.Drawing.Size(435, 24);
            this.layoutControlItem55.MinSize = new System.Drawing.Size(435, 24);
            this.layoutControlItem55.Name = "layoutControlItem55";
            this.layoutControlItem55.Size = new System.Drawing.Size(435, 24);
            this.layoutControlItem55.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem55.Text = "Bank Name:";
            this.layoutControlItem55.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem56
            // 
            this.layoutControlItem56.Control = this.textBoxBankAccountNumber;
            this.layoutControlItem56.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem56.MaxSize = new System.Drawing.Size(435, 24);
            this.layoutControlItem56.MinSize = new System.Drawing.Size(435, 24);
            this.layoutControlItem56.Name = "layoutControlItem56";
            this.layoutControlItem56.Size = new System.Drawing.Size(435, 24);
            this.layoutControlItem56.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem56.Text = "Bank Account No:";
            this.layoutControlItem56.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem57
            // 
            this.layoutControlItem57.Control = this.textBoxBankBranch;
            this.layoutControlItem57.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem57.MaxSize = new System.Drawing.Size(435, 25);
            this.layoutControlItem57.MinSize = new System.Drawing.Size(435, 25);
            this.layoutControlItem57.Name = "layoutControlItem57";
            this.layoutControlItem57.Size = new System.Drawing.Size(435, 25);
            this.layoutControlItem57.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem57.Text = "Branch:";
            this.layoutControlItem57.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.ExpandButtonVisible = true;
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem58,
            this.layoutControlItem59,
            this.layoutControlItem60});
            this.layoutControlGroup6.Location = new System.Drawing.Point(452, 231);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(444, 119);
            this.layoutControlGroup6.Text = "Tax Details";
            // 
            // layoutControlItem58
            // 
            this.layoutControlItem58.Control = this.comboBoxTaxOption;
            this.layoutControlItem58.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem58.MaxSize = new System.Drawing.Size(405, 25);
            this.layoutControlItem58.MinSize = new System.Drawing.Size(405, 25);
            this.layoutControlItem58.Name = "layoutControlItem58";
            this.layoutControlItem58.Size = new System.Drawing.Size(418, 25);
            this.layoutControlItem58.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem58.Text = "Tax Option:";
            this.layoutControlItem58.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem59
            // 
            this.layoutControlItem59.Control = this.comboBoxTaxGroup;
            this.layoutControlItem59.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem59.MaxSize = new System.Drawing.Size(405, 24);
            this.layoutControlItem59.MinSize = new System.Drawing.Size(405, 24);
            this.layoutControlItem59.Name = "layoutControlItem59";
            this.layoutControlItem59.Size = new System.Drawing.Size(418, 24);
            this.layoutControlItem59.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem59.Text = "Tax Group:";
            this.layoutControlItem59.TextSize = new System.Drawing.Size(164, 13);
            this.layoutControlItem59.Click += new System.EventHandler(this.layoutControlItem59_Click);
            // 
            // layoutControlItem60
            // 
            this.layoutControlItem60.Control = this.textBoxTaxIDNumber;
            this.layoutControlItem60.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem60.MaxSize = new System.Drawing.Size(405, 24);
            this.layoutControlItem60.MinSize = new System.Drawing.Size(405, 24);
            this.layoutControlItem60.Name = "layoutControlItem60";
            this.layoutControlItem60.Size = new System.Drawing.Size(418, 24);
            this.layoutControlItem60.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem60.Text = "Tax ID:";
            this.layoutControlItem60.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutItemConsignmentCom
            // 
            this.layoutItemConsignmentCom.Control = this.textBoxConsignCommission;
            this.layoutItemConsignmentCom.Location = new System.Drawing.Point(383, 135);
            this.layoutItemConsignmentCom.MaxSize = new System.Drawing.Size(289, 24);
            this.layoutItemConsignmentCom.MinSize = new System.Drawing.Size(289, 24);
            this.layoutItemConsignmentCom.Name = "layoutItemConsignmentCom";
            this.layoutItemConsignmentCom.Size = new System.Drawing.Size(289, 24);
            this.layoutItemConsignmentCom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutItemConsignmentCom.Text = "Commission Percent:";
            this.layoutItemConsignmentCom.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem48
            // 
            this.layoutControlItem48.Control = this.checkBoxAllowConsignment;
            this.layoutControlItem48.Location = new System.Drawing.Point(169, 135);
            this.layoutControlItem48.MaxSize = new System.Drawing.Size(214, 24);
            this.layoutControlItem48.MinSize = new System.Drawing.Size(214, 24);
            this.layoutControlItem48.Name = "layoutControlItem48";
            this.layoutControlItem48.Size = new System.Drawing.Size(214, 24);
            this.layoutControlItem48.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem48.TextVisible = false;
            // 
            // emptySpaceItem15
            // 
            this.emptySpaceItem15.AllowHotTrack = false;
            this.emptySpaceItem15.Location = new System.Drawing.Point(672, 135);
            this.emptySpaceItem15.MaxSize = new System.Drawing.Size(194, 24);
            this.emptySpaceItem15.MinSize = new System.Drawing.Size(194, 24);
            this.emptySpaceItem15.Name = "emptySpaceItem15";
            this.emptySpaceItem15.Size = new System.Drawing.Size(224, 24);
            this.emptySpaceItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.ExpandButtonVisible = true;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem14,
            this.layoutControlItem61,
            this.layoutControlItem62,
            this.layoutControlItem63,
            this.emptySpaceItem1});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 350);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 9, 9, 9);
            this.layoutControlGroup7.Size = new System.Drawing.Size(896, 253);
            this.layoutControlGroup7.Text = "Comments";
            // 
            // emptySpaceItem14
            // 
            this.emptySpaceItem14.AllowHotTrack = false;
            this.emptySpaceItem14.Location = new System.Drawing.Point(0, 135);
            this.emptySpaceItem14.MaxSize = new System.Drawing.Size(675, 43);
            this.emptySpaceItem14.MinSize = new System.Drawing.Size(675, 43);
            this.emptySpaceItem14.Name = "emptySpaceItem14";
            this.emptySpaceItem14.Size = new System.Drawing.Size(675, 43);
            this.emptySpaceItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem61
            // 
            this.layoutControlItem61.Control = this.textBoxDeliveryInstructions;
            this.layoutControlItem61.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem61.MaxSize = new System.Drawing.Size(864, 68);
            this.layoutControlItem61.MinSize = new System.Drawing.Size(864, 68);
            this.layoutControlItem61.Name = "layoutControlItem61";
            this.layoutControlItem61.Size = new System.Drawing.Size(879, 68);
            this.layoutControlItem61.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem61.Text = "Delivery Instructions";
            this.layoutControlItem61.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem62
            // 
            this.layoutControlItem62.Control = this.textBoxAccountInstructions;
            this.layoutControlItem62.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem62.MaxSize = new System.Drawing.Size(864, 67);
            this.layoutControlItem62.MinSize = new System.Drawing.Size(864, 67);
            this.layoutControlItem62.Name = "layoutControlItem62";
            this.layoutControlItem62.Size = new System.Drawing.Size(879, 67);
            this.layoutControlItem62.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem62.Text = "Account Instructions:";
            this.layoutControlItem62.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem63
            // 
            this.layoutControlItem63.Control = this.buttonAccounts;
            this.layoutControlItem63.Location = new System.Drawing.Point(675, 135);
            this.layoutControlItem63.MaxSize = new System.Drawing.Size(189, 43);
            this.layoutControlItem63.MinSize = new System.Drawing.Size(189, 43);
            this.layoutControlItem63.Name = "layoutControlItem63";
            this.layoutControlItem63.Size = new System.Drawing.Size(204, 43);
            this.layoutControlItem63.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem63.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem63.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 178);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(879, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem54
            // 
            this.layoutControlItem54.Control = this.textBoxRebatePercent;
            this.layoutControlItem54.Location = new System.Drawing.Point(274, 207);
            this.layoutControlItem54.MaxSize = new System.Drawing.Size(182, 24);
            this.layoutControlItem54.MinSize = new System.Drawing.Size(182, 24);
            this.layoutControlItem54.Name = "layoutControlItem54";
            this.layoutControlItem54.Size = new System.Drawing.Size(182, 24);
            this.layoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem54.Text = "Rebate %:";
            this.layoutControlItem54.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem54.TextSize = new System.Drawing.Size(53, 13);
            this.layoutControlItem54.TextToControlDistance = 5;
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem106,
            this.layoutControlItem107});
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Size = new System.Drawing.Size(896, 603);
            this.layoutControlGroup10.Text = "Contacts";
            // 
            // layoutControlItem106
            // 
            this.layoutControlItem106.Control = this.dataGridContacts;
            this.layoutControlItem106.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem106.Name = "layoutControlItem106";
            this.layoutControlItem106.Size = new System.Drawing.Size(896, 301);
            this.layoutControlItem106.Text = "Contacts related to this customer:";
            this.layoutControlItem106.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem106.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlItem107
            // 
            this.layoutControlItem107.Control = this.gridComboBoxContact;
            this.layoutControlItem107.Location = new System.Drawing.Point(0, 301);
            this.layoutControlItem107.Name = "layoutControlItem107";
            this.layoutControlItem107.Size = new System.Drawing.Size(896, 302);
            this.layoutControlItem107.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem107.TextVisible = false;
            this.layoutControlItem107.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // tabPageActivity
            // 
            this.tabPageActivity.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem19,
            this.layoutControlItem108,
            this.layoutControlItem109,
            this.emptySpaceItem20,
            this.layoutControlItem110});
            this.tabPageActivity.Location = new System.Drawing.Point(0, 0);
            this.tabPageActivity.Name = "tabPageActivity";
            this.tabPageActivity.Size = new System.Drawing.Size(896, 603);
            this.tabPageActivity.Text = "Activities";
            // 
            // emptySpaceItem19
            // 
            this.emptySpaceItem19.AllowHotTrack = false;
            this.emptySpaceItem19.Location = new System.Drawing.Point(0, 404);
            this.emptySpaceItem19.Name = "emptySpaceItem19";
            this.emptySpaceItem19.Size = new System.Drawing.Size(896, 199);
            this.emptySpaceItem19.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem108
            // 
            this.layoutControlItem108.Control = this.buttonAddActivity;
            this.layoutControlItem108.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem108.Name = "layoutControlItem108";
            this.layoutControlItem108.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem108.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem108.TextVisible = false;
            // 
            // layoutControlItem109
            // 
            this.layoutControlItem109.Control = this.comboBoxActivityPeriod;
            this.layoutControlItem109.Location = new System.Drawing.Point(597, 0);
            this.layoutControlItem109.Name = "layoutControlItem109";
            this.layoutControlItem109.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem109.Text = "Period:";
            this.layoutControlItem109.TextSize = new System.Drawing.Size(164, 13);
            // 
            // emptySpaceItem20
            // 
            this.emptySpaceItem20.AllowHotTrack = false;
            this.emptySpaceItem20.Location = new System.Drawing.Point(299, 0);
            this.emptySpaceItem20.Name = "emptySpaceItem20";
            this.emptySpaceItem20.Size = new System.Drawing.Size(298, 24);
            this.emptySpaceItem20.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem110
            // 
            this.layoutControlItem110.Control = this.dataGridActivities;
            this.layoutControlItem110.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem110.Name = "layoutControlItem110";
            this.layoutControlItem110.Size = new System.Drawing.Size(896, 380);
            this.layoutControlItem110.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem110.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem111});
            this.layoutControlGroup12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.Size = new System.Drawing.Size(896, 603);
            this.layoutControlGroup12.Text = "Profile";
            // 
            // layoutControlItem111
            // 
            this.layoutControlItem111.Control = this.textBoxProfileDetails;
            this.layoutControlItem111.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem111.Name = "layoutControlItem111";
            this.layoutControlItem111.Size = new System.Drawing.Size(896, 603);
            this.layoutControlItem111.Text = "Customer Profile:";
            this.layoutControlItem111.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem111.TextSize = new System.Drawing.Size(164, 13);
            // 
            // layoutControlGroup13
            // 
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem112});
            this.layoutControlGroup13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup13.Name = "layoutControlGroup13";
            this.layoutControlGroup13.Size = new System.Drawing.Size(896, 603);
            this.layoutControlGroup13.Text = "&Note";
            // 
            // layoutControlItem112
            // 
            this.layoutControlItem112.Control = this.textBoxNote;
            this.layoutControlItem112.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem112.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem112.Name = "layoutControlItem112";
            this.layoutControlItem112.Size = new System.Drawing.Size(896, 603);
            this.layoutControlItem112.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem112.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem112.TextVisible = false;
            // 
            // layoutControlGroup14
            // 
            this.layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem113});
            this.layoutControlGroup14.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup14.Name = "layoutControlGroup14";
            this.layoutControlGroup14.Size = new System.Drawing.Size(896, 603);
            this.layoutControlGroup14.Text = "&User Defined";
            // 
            // layoutControlItem113
            // 
            this.layoutControlItem113.Control = this.udfEntryGrid;
            this.layoutControlItem113.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem113.Name = "layoutControlItem113";
            this.layoutControlItem113.Size = new System.Drawing.Size(896, 603);
            this.layoutControlItem113.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem113.TextVisible = false;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.buttonClose);
            this.panelButtons.Controls.Add(this.buttonNew);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 709);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(938, 40);
            this.panelButtons.TabIndex = 1;
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
            this.linePanelDown.Size = new System.Drawing.Size(938, 1);
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
            // buttonClose
            // 
            this.buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.DarkGray;
            this.buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClose.Location = new System.Drawing.Point(828, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(96, 24);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.xpButton1_Click);
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
            this.toolStripSeparator3,
            this.toolStripTextBoxFind,
            this.toolStripButtonFind,
            this.toolStripSeparator4,
            this.toolStripButtonAttach,
            this.toolStripButtonComments,
            this.toolStripSeparator2,
            this.toolStripButtonPrint,
            this.toolStripButtonPreview,
            this.toolStripSeparator5,
            this.toolStripButtonInformation,
            this.toolStripButtonHistory,
            this.toolStripButtonDesign,
            this.toolStripDropDownButtonEnquiry,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(938, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
            // toolStripButtonComments
            // 
            this.toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonComments.Image = global::Micromind.ClientUI.Properties.Resources.comment;
            this.toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonComments.Name = "toolStripButtonComments";
            this.toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonComments.Text = "Comments...";
            this.toolStripButtonComments.Click += new System.EventHandler(this.toolStripButtonComments_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
            // toolStripButtonHistory
            // 
            this.toolStripButtonHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHistory.Image = global::Micromind.ClientUI.Properties.Resources.historyIcon24x24;
            this.toolStripButtonHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHistory.Name = "toolStripButtonHistory";
            this.toolStripButtonHistory.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonHistory.Text = "toolStripButton1";
            this.toolStripButtonHistory.Visible = false;
            this.toolStripButtonHistory.Click += new System.EventHandler(this.toolStripButtonHistory_Click);
            // 
            // toolStripButtonDesign
            // 
            this.toolStripButtonDesign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLayoutDesign,
            this.menuItemCustomFields});
            this.toolStripButtonDesign.Image = global::Micromind.ClientUI.Properties.Resources.layout;
            this.toolStripButtonDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDesign.Name = "toolStripButtonDesign";
            this.toolStripButtonDesign.Size = new System.Drawing.Size(111, 28);
            this.toolStripButtonDesign.Text = "Design Form";
            // 
            // menuItemLayoutDesign
            // 
            this.menuItemLayoutDesign.Name = "menuItemLayoutDesign";
            this.menuItemLayoutDesign.Size = new System.Drawing.Size(158, 22);
            this.menuItemLayoutDesign.Text = "Layout...";
            this.menuItemLayoutDesign.Click += new System.EventHandler(this.menuItemLayoutDesign_Click);
            // 
            // menuItemCustomFields
            // 
            this.menuItemCustomFields.Name = "menuItemCustomFields";
            this.menuItemCustomFields.Size = new System.Drawing.Size(158, 22);
            this.menuItemCustomFields.Text = "Custom Fields...";
            this.menuItemCustomFields.Click += new System.EventHandler(this.menuItemCustomFields_Click);
            // 
            // toolStripDropDownButtonEnquiry
            // 
            this.toolStripDropDownButtonEnquiry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonEnquiry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCustomerLedger,
            this.menuItemSalesStatistics,
            this.toolStripSeparator7});
            this.toolStripDropDownButtonEnquiry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonEnquiry.Name = "toolStripDropDownButtonEnquiry";
            this.toolStripDropDownButtonEnquiry.Size = new System.Drawing.Size(60, 28);
            this.toolStripDropDownButtonEnquiry.Text = "Enquiry";
            // 
            // menuItemCustomerLedger
            // 
            this.menuItemCustomerLedger.Name = "menuItemCustomerLedger";
            this.menuItemCustomerLedger.Size = new System.Drawing.Size(165, 22);
            this.menuItemCustomerLedger.Text = "Customer Ledger";
            this.menuItemCustomerLedger.Click += new System.EventHandler(this.menuItemCustomerLedger_Click);
            // 
            // menuItemSalesStatistics
            // 
            this.menuItemSalesStatistics.Name = "menuItemSalesStatistics";
            this.menuItemSalesStatistics.Size = new System.Drawing.Size(165, 22);
            this.menuItemSalesStatistics.Text = "Sales Statistics";
            this.menuItemSalesStatistics.Click += new System.EventHandler(this.menuItemSalesStatistics_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(162, 6);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlantiffToolStripMenuItem,
            this.defendantToolStripMenuItem,
            this.toolStripSeparator6});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
            this.toolStripDropDownButton1.Text = "Actions";
            // 
            // PlantiffToolStripMenuItem
            // 
            this.PlantiffToolStripMenuItem.Name = "PlantiffToolStripMenuItem";
            this.PlantiffToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.PlantiffToolStripMenuItem.Text = "Convert To Plantiff";
            this.PlantiffToolStripMenuItem.Click += new System.EventHandler(this.PlantiffToolStripMenuItem_Click);
            // 
            // defendantToolStripMenuItem
            // 
            this.defendantToolStripMenuItem.Name = "defendantToolStripMenuItem";
            this.defendantToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.defendantToolStripMenuItem.Text = "Convert To Defendant";
            this.defendantToolStripMenuItem.Click += new System.EventHandler(this.defendantToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(186, 6);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelCustomerNameHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 30);
            this.panel1.TabIndex = 1;
            // 
            // labelCustomerNameHeader
            // 
            this.labelCustomerNameHeader.AutoSize = true;
            this.labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCustomerNameHeader.IsFieldHeader = false;
            this.labelCustomerNameHeader.IsRequired = true;
            this.labelCustomerNameHeader.Location = new System.Drawing.Point(24, 7);
            this.labelCustomerNameHeader.Name = "labelCustomerNameHeader";
            this.labelCustomerNameHeader.PenWidth = 1F;
            this.labelCustomerNameHeader.ShowBorder = false;
            this.labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
            this.labelCustomerNameHeader.TabIndex = 1;
            this.labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStripContact
            // 
            this.contextMenuStripContact.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripContact.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContactToolStripMenuItem,
            this.newContactToolStripMenuItem,
            this.deleteContactToolStripMenuItem});
            this.contextMenuStripContact.Name = "contextMenuStripContact";
            this.contextMenuStripContact.Size = new System.Drawing.Size(153, 70);
            // 
            // openContactToolStripMenuItem
            // 
            this.openContactToolStripMenuItem.Name = "openContactToolStripMenuItem";
            this.openContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openContactToolStripMenuItem.Text = "Open Contact";
            this.openContactToolStripMenuItem.Click += new System.EventHandler(this.openContactToolStripMenuItem_Click_1);
            // 
            // newContactToolStripMenuItem
            // 
            this.newContactToolStripMenuItem.Name = "newContactToolStripMenuItem";
            this.newContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newContactToolStripMenuItem.Text = "New Contact";
            // 
            // deleteContactToolStripMenuItem
            // 
            this.deleteContactToolStripMenuItem.Name = "deleteContactToolStripMenuItem";
            this.deleteContactToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteContactToolStripMenuItem.Text = "Delete Contact";
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            // 
            // imageListComments
            // 
            this.imageListComments.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListComments.ImageStream")));
            this.imageListComments.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListComments.Images.SetKeyName(0, "comment.png");
            this.imageListComments.Images.SetKeyName(1, "comment-yw.png");
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 25);
            this.formManager.MaximumSize = new System.Drawing.Size(20, 20);
            this.formManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(20, 20);
            this.formManager.TabIndex = 307;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "JPG";
            this.openFileDialog1.Filter = "Picture Files|*.jpg";
            // 
            // CustomerDetailsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(938, 749);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelButtons);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CustomerDetailsForm";
            this.Text = "Customer Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomerClassDetailsForm_FormClosing);
            this.Load += new System.EventHandler(this.CustomerDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCreditReviewBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRatingBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInsuranceProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxInsuranceRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPaymentTerms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCollectionUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridContacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComboBoxContact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxActivityPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.layoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridActivities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxTaxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPaymentMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSalesperson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLeadSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPriceLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxBilltoAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxParentCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShippingMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxShiptoAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCustomerClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem65)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem66)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem67)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem68)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem69)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem70)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem73)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem71)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem74)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem75)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupCreditLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem77)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem78)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem81)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem79)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem80)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem82)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem83)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem84)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem85)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem86)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem87)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem88)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem89)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem90)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem91)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem92)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem76)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem93)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelInsuranceDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem97)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem98)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem99)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem101)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem102)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem103)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem104)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem105)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem94)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem96)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem53)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem60)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemConsignmentCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem61)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem62)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem63)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem106)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem107)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem108)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem109)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem110)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem111)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem112)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem113)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStripContact.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void Init()
		{
			AddEvents();
			comboBoxActivityPeriod.LoadData();
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void AddEvents()
		{
			FormActivator.CustomerAddressDetailsFormObj.CustomerAddressChanged += EventHelper_CustomerAddressChanged;
			dataGridContacts.AfterCellUpdate += dataGridContacts_AfterCellUpdate;
			dataGridContacts.BeforeCellUpdate += dataGridContacts_BeforeCellUpdate;
			dataGridContacts.ClickCellButton += dataGridContacts_ClickCellButton;
			dataGridContacts.CellDataError += dataGridContacts_CellDataError;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			checkBoxAllowConsignment.CheckedChanged += checkBoxAllowConsignment_CheckedChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			dataGridActivities.DoubleClick += dataGridActivities_DoubleClick;
			textBoxLatitude.KeyDown += textBoxLatitude_KeyDown;
			textBoxLongitude.KeyDown += textBoxLongitude_KeyDown;
			textBoxLatitude.KeyUp += textBoxLatitude_KeyUp;
			textBoxLongitude.KeyUp += textBoxLongitude_KeyUp;
			textBoxGraceDays.KeyPress += txtDays_KeyPress;
			comboBoxInsuranceStatus.SelectedIndexChanged += ComboBoxInsuranceStatus_SelectedIndexChanged;
		}

		private void ComboBoxInsuranceStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxInsuranceStatus.SelectedIndex == 0)
			{
				panelInsuranceDetails.Visibility = LayoutVisibility.OnlyInCustomization;
			}
			else
			{
				panelInsuranceDetails.Visibility = LayoutVisibility.Always;
			}
		}

		private void dataGridActivities_DoubleClick(object sender, EventArgs e)
		{
			string voucherID = dataGridActivities.ActiveRow.Cells["VoucherID"].Value.ToString();
			string sysDocID = dataGridActivities.ActiveRow.Cells["SysDocID"].Value.ToString();
			new FormHelper().EditTransaction(sysDocID, voucherID);
		}

		private void checkBoxAllowConsignment_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAllowConsignment.Checked)
			{
				layoutItemConsignmentCom.Visibility = LayoutVisibility.Always;
			}
			else
			{
				layoutItemConsignmentCom.Visibility = LayoutVisibility.OnlyInCustomization;
			}
		}

		private void dataGridContacts_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridContacts.ActiveCell.Column.Key.ToString() == "ContactID")
			{
				e.RaiseErrorEvent = false;
				gridComboBoxContact.Text = dataGridContacts.ActiveCell.Text;
				gridComboBoxContact.QuickAddItem();
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void dataGridContacts_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID")
			{
				DataRow dataRow = Factory.ContactSystem.GetContactByID(e.Cell.Value.ToString()).Tables[0].Rows[0];
				dataGridContacts.ActiveRow.Cells["FirstName"].Value = dataRow["FirstName"].ToString();
				dataGridContacts.ActiveRow.Cells["LastName"].Value = dataRow["LastName"].ToString();
				dataGridContacts.ActiveRow.Cells["JobTitle"].Value = dataRow["JobTitle"].ToString();
				dataGridContacts.ActiveRow.Cells["Note"].Value = dataRow["Note"].ToString();
			}
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void comboBoxARAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridContacts_ClickCellButton(object sender, CellEventArgs e)
		{
		}

		private void dataGridContacts_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "ContactID" && dataGridContacts.ExistCellValue("ContactID", e.NewValue.ToString()) >= 0)
			{
				ErrorHelper.InformationMessage("This contact is already added to list. Please select another contact.");
				e.Cancel = true;
			}
		}

		private void EventHelper_CustomerAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				if (dataRow["CustomerID"].ToString() == textBoxCode.Text && dataRow["AddressID"].ToString() == textBoxAddressID.Text && !isNewRecord)
				{
					FillAddressData(dataRow);
				}
			}
		}

		private void CustomerDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					dataGridContacts.SetupUI();
					SetupContactsGrid();
					dataGridActivities.ApplyUIDesign();
					SetupActivityGrid();
					if (textBoxLatitude.Text != "")
					{
						textBoxLatitude.ForeColor = Color.Gray;
						textBoxLongitude.ForeColor = Color.Gray;
						textBoxLatitude.Text = "25.2824891";
						textBoxLongitude.Text = "55.3583311";
					}
					ClearForm();
					textBoxCode.Focus();
					if (CompanyPreferences.TaxEntityTypes.Contains("C"))
					{
						comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
						comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
					}
					layoutGroupCreditLimit.Enabled = !disableCustomerCreditLimit;
					Init();
					FormHelper formHelper = new FormHelper();
					formHelper.AddCustomFieldsToForm(base.Name, "Customer", layoutControl1);
					formHelper.InitLayoutControl(layoutControl1);
					formHelper.LoadFormLayout(layoutControl1, base.Name, "Default");
					toolStripButtonDesign.Visible = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetupContactsGrid()
		{
			dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ContactID").Unique = true;
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("JobTitle");
			dataTable.Columns.Add("Phone1");
			dataTable.Columns.Add("Mobile");
			dataTable.Columns.Add("Email1");
			dataTable.Columns.Add("Note");
			dataGridContacts.DataSource = dataTable;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].MaxLength = 30;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].MaxLength = 64;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].MaxLength = 255;
			dataGridContacts.DisplayLayout.Bands[0].Columns["JobTitle"].Header.Caption = "Job Title";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Header.Caption = "Contact Code";
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].Header.Caption = "First Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].Header.Caption = "Last Name";
			dataGridContacts.DisplayLayout.Bands[0].Columns["Phone1"].Header.Caption = "Land Line";
			dataGridContacts.DisplayLayout.Bands[0].Columns["Email1"].Header.Caption = "E-Mail";
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridContacts.DisplayLayout.Bands[0].Columns["ContactID"].ValueList = gridComboBoxContact;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["FirstName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].CellActivation = Activation.NoEdit;
			dataGridContacts.DisplayLayout.Bands[0].Columns["LastName"].TabStop = false;
			dataGridContacts.DisplayLayout.Bands[0].Columns["Note"].CellActivation = Activation.AllowEdit;
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.CustomerTable.Rows[0];
					textBoxCode.Text = dataRow["CustomerID"].ToString();
					textBoxName.Text = dataRow["CustomerName"].ToString();
					textBoxForeignName.Text = dataRow["ForeignName"].ToString();
					textBoxFormalName.Text = dataRow["ShortName"].ToString();
					comboBoxParentCustomer.SelectedID = dataRow["ParentCustomerID"].ToString();
					comboBoxCustomerGroup.SelectedID = dataRow["CustomerGroupID"].ToString();
					if (comboBoxParentCustomer.SelectedID != "")
					{
						checkBoxparentACforposting.Enabled = true;
					}
					else
					{
						checkBoxparentACforposting.Checked = false;
						checkBoxparentACforposting.Enabled = false;
					}
					comboBoxCustomerClass.SelectedID = dataRow["CustomerClassID"].ToString();
					comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
					comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
					comboBoxLeadSource.SelectedID = dataRow["LeadSourceID"].ToString();
					comboBoxPriceLevel.SelectedID = dataRow["PriceLevelID"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					checkBoxHold.Checked = bool.Parse(dataRow["IsHold"].ToString());
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxCollectionUser.SelectedID = dataRow["CollectionUserID"].ToString();
					textBoxTempLimit.Text = decimal.Parse(dataRow["TempCredit"].ToString()).ToString(Format.TotalAmountFormat);
					comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
					comboBoxPaymentTerms.SelectedID = dataRow["PaymentTermID"].ToString();
					comboBoxPaymentMethods.SelectedID = dataRow["PaymentMethodID"].ToString();
					comboBoxShippingMethods.SelectedID = dataRow["ShippingMethodID"].ToString();
					textBoxStatementEmail.Text = dataRow["StatementEmail"].ToString();
					textBoxDeliveryInstructions.Text = dataRow["DeliveryInstructions"].ToString();
					textBoxAccountInstructions.Text = dataRow["AccountInstructions"].ToString();
					comboBoxBilltoAddress.SelectedID = dataRow["BillToAddressID"].ToString();
					comboBoxShiptoAddress.SelectedID = dataRow["ShipToAddressID"].ToString();
					if (!dataRow["StatementSendingMethod"].IsDBNullOrEmpty())
					{
						comboBoxStatementMethod.SelectedIndex = int.Parse(dataRow["StatementSendingMethod"].ToString());
					}
					else
					{
						comboBoxStatementMethod.SelectedIndex = 0;
					}
					SourceLeadID = dataRow["SourceLeadID"].ToString();
					if (dataRow["AcceptCheckPayment"] != DBNull.Value)
					{
						checkBoxAcceptCheque.Checked = bool.Parse(dataRow["AcceptCheckPayment"].ToString());
					}
					else
					{
						checkBoxAcceptCheque.Checked = true;
					}
					if (dataRow["IsWeightInvoice"] != DBNull.Value)
					{
						checkBoxWeightInvoice.Checked = bool.Parse(dataRow["IsWeightInvoice"].ToString());
					}
					else
					{
						checkBoxWeightInvoice.Checked = false;
					}
					if (dataRow["IsParentPosting"] != DBNull.Value)
					{
						checkBoxparentACforposting.Checked = bool.Parse(dataRow["IsParentPosting"].ToString());
					}
					else
					{
						checkBoxparentACforposting.Checked = false;
					}
					if (dataRow["LimitPDCUnsecured"] != DBNull.Value)
					{
						checkBoxUnsecuredLimit.Checked = bool.Parse(dataRow["LimitPDCUnsecured"].ToString());
						if (dataRow["PDCUnsecuredLimitAmount"] != DBNull.Value)
						{
							textBoxUnsecuredLimit.Text = decimal.Parse(dataRow["PDCUnsecuredLimitAmount"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxUnsecuredLimit.Text = 0.ToString(Format.TotalAmountFormat);
						}
					}
					else
					{
						checkBoxUnsecuredLimit.Checked = false;
						textBoxUnsecuredLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["CLValidity"] != DBNull.Value)
					{
						dateTimePickerCLValidity.Checked = true;
						dateTimePickerCLValidity.Value = DateTime.Parse(dataRow["CLValidity"].ToString());
					}
					else
					{
						dateTimePickerCLValidity.Clear();
						dateTimePickerCLValidity.Checked = false;
					}
					if (dataRow["AcceptPDC"] != DBNull.Value)
					{
						checkBoxAcceptPDC.Checked = bool.Parse(dataRow["AcceptPDC"].ToString());
					}
					else
					{
						checkBoxAcceptPDC.Checked = true;
					}
					if (dataRow["Rating"] != DBNull.Value)
					{
						comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
					}
					else
					{
						comboBoxRating.SelectedIndex = 0;
					}
					if (dataRow["InsRating"] != DBNull.Value)
					{
						comboBoxInsuranceRating.SelectedIndex = int.Parse(dataRow["InsRating"].ToString());
					}
					else
					{
						comboBoxInsuranceRating.SelectedIndex = 0;
					}
					if (dataRow["AllowConsignment"] != DBNull.Value)
					{
						checkBoxAllowConsignment.Checked = bool.Parse(dataRow["AllowConsignment"].ToString());
					}
					else
					{
						checkBoxAllowConsignment.Checked = false;
					}
					if (dataRow["ConsignComPercent"] != DBNull.Value)
					{
						textBoxConsignCommission.Text = dataRow["ConsignComPercent"].ToString();
					}
					else
					{
						textBoxConsignCommission.Text = "0.00";
					}
					if (dataRow["DiscountPercent"] != DBNull.Value)
					{
						textBoxDiscountPercent.Text = dataRow["DiscountPercent"].ToString();
					}
					else
					{
						textBoxDiscountPercent.Text = "0.00";
					}
					if (dataRow["RebatePercent"] != DBNull.Value)
					{
						textBoxRebatePercent.Text = dataRow["RebatePercent"].ToString();
					}
					else
					{
						textBoxRebatePercent.Text = "0.00";
					}
					if (dataRow["IsCustomerSince"] != DBNull.Value)
					{
						dateTimePickerCustomerSince.Value = DateTime.Parse(dataRow["IsCustomerSince"].ToString());
						dateTimePickerCustomerSince.Checked = true;
					}
					else
					{
						dateTimePickerCustomerSince.IsNull = true;
						dateTimePickerCustomerSince.Checked = false;
					}
					if (dataRow["DateEstablished"] != DBNull.Value)
					{
						dateTimePickerEstablished.Value = DateTime.Parse(dataRow["DateEstablished"].ToString());
						dateTimePickerEstablished.Checked = true;
					}
					else
					{
						dateTimePickerEstablished.IsNull = true;
						dateTimePickerEstablished.Checked = false;
					}
					if (dataRow["CreditReviewDate"] != DBNull.Value)
					{
						dateTimePickerReviewDate.Value = DateTime.Parse(dataRow["CreditReviewDate"].ToString());
						dateTimePickerReviewDate.Checked = true;
					}
					else
					{
						dateTimePickerReviewDate.IsNull = true;
						dateTimePickerReviewDate.Checked = false;
					}
					DateTime result = new DateTime(1, 1, 1);
					DateTime.TryParse(dataRow["RatingDate"].ToString(), out result);
					if (dataRow["GraceDays"] != DBNull.Value)
					{
						textBoxGraceDays.Text = dataRow["GraceDays"].ToString();
					}
					else
					{
						textBoxGraceDays.Text = "0";
					}
					if (dataRow["RatingDate"] != DBNull.Value)
					{
						dateTimePickerRatingDate.Value = DateTime.Parse(dataRow["RatingDate"].ToString());
						dateTimePickerRatingDate.Checked = true;
					}
					else
					{
						dateTimePickerRatingDate.IsNull = true;
						dateTimePickerRatingDate.Checked = false;
					}
					if ((SqlBoolean)(dataRow["RatingDate"] != DBNull.Value) && result > SqlDateTime.MinValue)
					{
						dateTimePickerRatingDate.Value = DateTime.Parse(dataRow["RatingDate"].ToString());
						dateTimePickerRatingDate.Checked = true;
					}
					else
					{
						dateTimePickerRatingDate.IsNull = true;
						dateTimePickerRatingDate.Checked = false;
					}
					textBoxLicenseNumber.Text = dataRow["LicenseNumber"].ToString();
					if (dataRow["LicenseExpDate"] != DBNull.Value)
					{
						dateTimePickerLicenseExpDate.Value = DateTime.Parse(dataRow["LicenseExpDate"].ToString());
						dateTimePickerLicenseExpDate.Checked = true;
					}
					else
					{
						dateTimePickerLicenseExpDate.IsNull = true;
						dateTimePickerLicenseExpDate.Checked = false;
					}
					if (dataRow["ContractExpDate"] != DBNull.Value)
					{
						dateTimePickerContractExpDate.Value = DateTime.Parse(dataRow["ContractExpDate"].ToString());
						dateTimePickerContractExpDate.Checked = true;
					}
					else
					{
						dateTimePickerContractExpDate.IsNull = true;
						dateTimePickerContractExpDate.Checked = false;
					}
					if (dataRow["CreditReviewBy"] != DBNull.Value)
					{
						comboBoxCreditReviewBy.SelectedID = dataRow["CreditReviewBy"].ToString();
					}
					else
					{
						comboBoxCreditReviewBy.Clear();
					}
					if (dataRow["RatingBy"] != DBNull.Value)
					{
						comboBoxRatingBy.SelectedID = dataRow["RatingBy"].ToString();
					}
					else
					{
						comboBoxRatingBy.Clear();
					}
					CreditLimitTypes creditLimitTypes = CreditLimitTypes.Unlimited;
					if (dataRow["CreditLimitType"] != DBNull.Value)
					{
						creditLimitTypes = (CreditLimitTypes)byte.Parse(dataRow["CreditLimitType"].ToString());
					}
					switch (creditLimitTypes)
					{
					case CreditLimitTypes.CreditAmount:
						radioButtonCreditLimitAmount.Checked = true;
						break;
					case CreditLimitTypes.NoCredit:
						radioButtonCreditLimitNoCredit.Checked = true;
						break;
					case CreditLimitTypes.ParentSublimit:
						radioButtonSublimit.Checked = true;
						break;
					default:
						radioButtonCreditLimitUnlimited.Checked = true;
						break;
					}
					if (dataRow["CreditAmount"] != DBNull.Value)
					{
						textBoxCreditLimit.Text = decimal.Parse(dataRow["CreditAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxCreditLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					textBoxRatingRemarks.Text = dataRow["RatingRemarks"].ToString();
					if (dataRow["InsApprovedAmount"] != DBNull.Value)
					{
						textBoxInsuranceApprovedAmount.Text = decimal.Parse(dataRow["InsApprovedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxInsuranceApprovedAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["InsRequestedAmount"] != DBNull.Value)
					{
						textBoxInsuranceReqAmount.Text = decimal.Parse(dataRow["InsRequestedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxInsuranceReqAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["InsApplicationDate"] != DBNull.Value)
					{
						dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
						dateTimePickerInsuranceDate.Checked = true;
					}
					else
					{
						dateTimePickerInsuranceDate.IsNull = true;
						dateTimePickerInsuranceDate.Checked = false;
					}
					textBoxInsuranceNumber.Text = dataRow["InsPolicyNumber"].ToString();
					textBoxInsuranceRemarks.Text = dataRow["InsRemarks"].ToString();
					textBoxInsuranceID.Text = dataRow["InsuranceID"].ToString();
					checked
					{
						if (dataRow["InsStatus"] != DBNull.Value)
						{
							comboBoxInsuranceStatus.SelectedIndex = unchecked((int)byte.Parse(dataRow["InsStatus"].ToString())) - 1;
						}
						else
						{
							comboBoxInsuranceStatus.SelectedIndex = 0;
						}
						if (dataRow["InsProviderID"] != DBNull.Value)
						{
							comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
						}
						DateTime result2 = new DateTime(1753, 1, 1);
						DateTime result3 = new DateTime(1753, 1, 1);
						if (dataRow["InsEffectiveDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result2);
							if ((SqlBoolean)true && result2 > SqlDateTime.MinValue)
							{
								datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
								datetimePickerEffectiveDate.Checked = true;
							}
							else
							{
								datetimePickerEffectiveDate.IsNull = true;
								datetimePickerEffectiveDate.Checked = false;
							}
						}
						else
						{
							datetimePickerEffectiveDate.IsNull = true;
							datetimePickerEffectiveDate.Checked = false;
						}
						if (dataRow["InsExpiryDate"] != DBNull.Value)
						{
							DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result3);
							if ((SqlBoolean)true && result3 > SqlDateTime.MinValue)
							{
								dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsExpiryDate"].ToString());
								dateTimePickerValidTo.Checked = true;
							}
							else
							{
								dateTimePickerValidTo.IsNull = true;
								dateTimePickerValidTo.Checked = false;
							}
						}
						else
						{
							dateTimePickerValidTo.IsNull = true;
							dateTimePickerValidTo.Checked = false;
						}
						textBoxBankName.Text = dataRow["BankName"].ToString();
						textBoxBankBranch.Text = dataRow["BankBranch"].ToString();
						textBoxBankAccountNumber.Text = dataRow["BankAccountNumber"].ToString();
						if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
						{
							comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
						}
						else
						{
							comboBoxTaxOption.SelectedIndex = 0;
						}
						if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
						{
							comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
						}
						else
						{
							comboBoxTaxGroup.Clear();
						}
						textBoxTaxIDNumber.Text = dataRow["TaxIDNumber"].ToString();
						textBoxProfileDetails.WordMLText = dataRow["ProfileDetails"].ToString();
						textBoxProfileDetails.EndUpdate();
						if (dataRow["HasPhoto"] != DBNull.Value)
						{
							bool flag = Convert.ToBoolean(byte.Parse(dataRow["HasPhoto"].ToString()));
							linkLoadImage.Visible = flag;
							linkRemovePicture.Enabled = flag;
						}
						else
						{
							linkLoadImage.Visible = false;
							linkRemovePicture.Enabled = false;
						}
						SetHeaderName();
						if (currentData.Tables.Contains("Customer_Address") && currentData.CustomerAddressTable.Rows.Count != 0)
						{
							dataRow = currentData.CustomerAddressTable.Rows[0];
							FillAddressData(dataRow);
							if (currentData.Tables.Contains("Customer_Contact_Detail") && currentData.CustomerAddressTable.Rows.Count != 0)
							{
								DataTable dataTable = dataGridContacts.DataSource as DataTable;
								dataTable.Rows.Clear();
								foreach (DataRow row in currentData.Tables["Customer_Contact_Detail"].Rows)
								{
									DataRow dataRow3 = dataTable.NewRow();
									foreach (DataColumn column in dataTable.Columns)
									{
										if (dataRow3.Table.Columns.Contains(column.ColumnName))
										{
											dataRow3[column.ColumnName] = row[column.ColumnName];
										}
										else
										{
											ErrorHelper.ErrorMessage(column.ColumnName + " Does not exist.");
										}
									}
									dataRow3.EndEdit();
									dataTable.Rows.Add(dataRow3);
								}
								dataTable.AcceptChanges();
								new FormHelper().FillEntityUDFData("Customer", "CustomerID", "", currentData, layoutControl1);
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void comboBoxInsuranceProvider_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxProvider.Text = comboBoxInsuranceProvider.SelectedName;
		}

		private void FillAddressData(DataRow row)
		{
			textBoxAddressID.Text = row["AddressID"].ToString();
			textBoxContactName.Text = row["ContactName"].ToString();
			textBoxAddress1.Text = row["Address1"].ToString();
			textBoxAddress2.Text = row["Address2"].ToString();
			textBoxAddress3.Text = row["Address3"].ToString();
			textBoxAddressPrintFormat.Text = row["AddressPrintFormat"].ToString();
			textBoxCity.Text = row["City"].ToString();
			textBoxState.Text = row["State"].ToString();
			textBoxCountry.Text = row["Country"].ToString();
			textBoxPostalCode.Text = row["PostalCode"].ToString();
			if (!string.IsNullOrEmpty(row["Latitude"].ToString()))
			{
				textBoxLatitude.Text = row["Latitude"].ToString();
				textBoxLatitude.ForeColor = Color.Black;
			}
			if (!string.IsNullOrEmpty(row["Longitude"].ToString()))
			{
				textBoxLongitude.Text = row["Longitude"].ToString();
				textBoxLongitude.ForeColor = Color.Black;
			}
			textBoxDepartment.Text = row["Department"].ToString();
			textBoxPhone1.Text = row["Phone1"].ToString();
			textBoxPhone2.Text = row["Phone2"].ToString();
			textBoxFax.Text = row["Fax"].ToString();
			textBoxMobile.Text = row["Mobile"].ToString();
			textBoxEmail.Text = row["Email"].ToString();
			textBoxWebsite.Text = row["Website"].ToString();
			textBoxComment.Text = row["Comment"].ToString();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomerData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerTable.Rows[0] : currentData.CustomerTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["CustomerName"] = textBoxName.Text;
				dataRow["ForeignName"] = textBoxForeignName.Text;
				dataRow["ShortName"] = textBoxFormalName.Text;
				if (comboBoxParentCustomer.SelectedID != "")
				{
					dataRow["ParentCustomerID"] = comboBoxParentCustomer.SelectedID;
				}
				else
				{
					dataRow["ParentCustomerID"] = DBNull.Value;
				}
				dataRow["CollectionUserID"] = (string.IsNullOrEmpty(comboBoxCollectionUser.SelectedID) ? ((IConvertible)DBNull.Value) : ((IConvertible)comboBoxCollectionUser.SelectedID));
				if (comboBoxParentCustomer.SelectedID != "")
				{
					dataRow["ParentCustomerID"] = comboBoxParentCustomer.SelectedID;
				}
				else
				{
					dataRow["ParentCustomerID"] = DBNull.Value;
				}
				if (comboBoxCustomerGroup.SelectedID != "")
				{
					dataRow["CustomerGroupID"] = comboBoxCustomerGroup.SelectedID;
				}
				else
				{
					dataRow["CustomerGroupID"] = DBNull.Value;
				}
				if (comboBoxStatementMethod.SelectedIndex != -1)
				{
					dataRow["StatementSendingMethod"] = comboBoxStatementMethod.SelectedIndex;
				}
				else
				{
					dataRow["StatementSendingMethod"] = 0;
				}
				dataRow["StatementEmail"] = textBoxStatementEmail.Text;
				dataRow["DeliveryInstructions"] = textBoxDeliveryInstructions.Text;
				dataRow["AccountInstructions"] = textBoxAccountInstructions.Text;
				if (comboBoxCustomerClass.SelectedID != "")
				{
					dataRow["CustomerClassID"] = comboBoxCustomerClass.SelectedID;
				}
				else
				{
					dataRow["CustomerClassID"] = DBNull.Value;
				}
				if (comboBoxCountry.SelectedID != "")
				{
					dataRow["CountryID"] = comboBoxCountry.SelectedID;
				}
				else
				{
					dataRow["CountryID"] = DBNull.Value;
				}
				if (comboBoxArea.SelectedID != "")
				{
					dataRow["AreaID"] = comboBoxArea.SelectedID;
				}
				else
				{
					dataRow["AreaID"] = DBNull.Value;
				}
				if (comboBoxLeadSource.SelectedID != "")
				{
					dataRow["LeadSourceID"] = comboBoxLeadSource.SelectedID;
				}
				else
				{
					dataRow["LeadSourceID"] = DBNull.Value;
				}
				if (comboBoxPriceLevel.SelectedID != "")
				{
					dataRow["PriceLevelID"] = comboBoxPriceLevel.SelectedID;
				}
				else
				{
					dataRow["PriceLevelID"] = DBNull.Value;
				}
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
				}
				dataRow["IsWeightInvoice"] = checkBoxWeightInvoice.Checked;
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["IsHold"] = checkBoxHold.Checked;
				dataRow["IsParentPosting"] = checkBoxparentACforposting.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["RatingRemarks"] = textBoxRatingRemarks.Text;
				dataRow["ProfileDetails"] = textBoxProfileDetails.WordMLText;
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalesPersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalesPersonID"] = DBNull.Value;
				}
				if (comboBoxPaymentTerms.SelectedID != "")
				{
					dataRow["PaymentTermID"] = comboBoxPaymentTerms.SelectedID;
				}
				else
				{
					dataRow["PaymentTermID"] = DBNull.Value;
				}
				if (comboBoxPaymentMethods.SelectedID != "")
				{
					dataRow["PaymentMethodID"] = comboBoxPaymentMethods.SelectedID;
				}
				else
				{
					dataRow["PaymentMethodID"] = DBNull.Value;
				}
				if (comboBoxShippingMethods.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethods.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxBilltoAddress.SelectedID != "")
				{
					dataRow["BillToAddressID"] = comboBoxBilltoAddress.SelectedID;
				}
				else
				{
					dataRow["BillToAddressID"] = DBNull.Value;
				}
				if (comboBoxShiptoAddress.SelectedID != "")
				{
					dataRow["ShipToAddressID"] = comboBoxShiptoAddress.SelectedID;
				}
				else
				{
					dataRow["ShipToAddressID"] = DBNull.Value;
				}
				if (comboBoxRating.SelectedIndex != -1)
				{
					dataRow["Rating"] = comboBoxRating.SelectedIndex;
				}
				else
				{
					dataRow["Rating"] = DBNull.Value;
				}
				if (comboBoxInsuranceRating.SelectedIndex != -1)
				{
					dataRow["InsRating"] = comboBoxInsuranceRating.SelectedIndex;
				}
				else
				{
					dataRow["InsRating"] = DBNull.Value;
				}
				if (comboBoxRatingBy.SelectedIndex != -1)
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				else
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				if (dateTimePickerEstablished.Checked)
				{
					dataRow["DateEstablished"] = dateTimePickerEstablished.Value;
				}
				else
				{
					dataRow["DateEstablished"] = DBNull.Value;
				}
				if (dateTimePickerReviewDate.Checked)
				{
					dataRow["CreditReviewDate"] = dateTimePickerReviewDate.Value;
				}
				else
				{
					dataRow["CreditReviewDate"] = DBNull.Value;
				}
				if (dateTimePickerCustomerSince.Checked)
				{
					dataRow["IsCustomerSince"] = dateTimePickerCustomerSince.Value;
				}
				else
				{
					dataRow["IsCustomerSince"] = DBNull.Value;
				}
				if (comboBoxCreditReviewBy.SelectedID == "")
				{
					dataRow["CreditReviewBy"] = DBNull.Value;
				}
				else
				{
					dataRow["CreditReviewBy"] = comboBoxCreditReviewBy.SelectedID;
				}
				if (comboBoxRatingBy.SelectedID == "")
				{
					dataRow["RatingBy"] = DBNull.Value;
				}
				else
				{
					dataRow["RatingBy"] = comboBoxRatingBy.SelectedID;
				}
				if (dateTimePickerRatingDate.Checked)
				{
					dataRow["RatingDate"] = dateTimePickerRatingDate.Value;
				}
				else
				{
					dataRow["RatingDate"] = DBNull.Value;
				}
				dataRow["AcceptCheckPayment"] = checkBoxAcceptCheque.Checked;
				dataRow["AcceptPDC"] = checkBoxAcceptPDC.Checked;
				if (dateTimeBalanceConfirmationDate.Checked)
				{
					dataRow["BalanceConfirmationDate"] = dateTimeBalanceConfirmationDate.Value;
				}
				if (!string.IsNullOrEmpty(textBoxConfirmationLevel.Text))
				{
					dataRow["ConfirmationInterval"] = textBoxConfirmationLevel.Text;
				}
				dataRow["LimitPDCUnsecured"] = checkBoxUnsecuredLimit.Checked;
				if (checkBoxUnsecuredLimit.Checked)
				{
					dataRow["PDCUnsecuredLimitAmount"] = textBoxUnsecuredLimit.Text;
				}
				else
				{
					dataRow["PDCUnsecuredLimitAmount"] = DBNull.Value;
				}
				dataRow["AllowConsignment"] = checkBoxAllowConsignment.Checked;
				if (checkBoxAllowConsignment.Checked)
				{
					dataRow["ConsignComPercent"] = textBoxConsignCommission.Text;
				}
				else
				{
					dataRow["ConsignComPercent"] = DBNull.Value;
				}
				dataRow["DiscountPercent"] = textBoxDiscountPercent.Text;
				dataRow["RebatePercent"] = textBoxRebatePercent.Text;
				if (radioButtonCreditLimitAmount.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.CreditAmount;
				}
				else if (radioButtonCreditLimitNoCredit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.NoCredit;
				}
				else if (radioButtonSublimit.Checked)
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.ParentSublimit;
				}
				else
				{
					dataRow["CreditLimitType"] = CreditLimitTypes.Unlimited;
				}
				if (radioButtonCreditLimitAmount.Checked)
				{
					if (dateTimePickerCLValidity.Checked)
					{
						dataRow["CLValidity"] = dateTimePickerCLValidity.Value;
					}
					else
					{
						dataRow["CLValidity"] = DBNull.Value;
					}
				}
				else
				{
					dataRow["CLValidity"] = DBNull.Value;
				}
				if (textBoxCreditLimit.Text != "")
				{
					dataRow["CreditAmount"] = textBoxCreditLimit.Text;
				}
				else
				{
					dataRow["CreditAmount"] = 0;
				}
				if (!string.IsNullOrEmpty(textBoxGraceDays.Text))
				{
					dataRow["GraceDays"] = textBoxGraceDays.Text;
				}
				else
				{
					dataRow["GraceDays"] = 0;
				}
				if (dateTimePickerInsuranceDate.Checked)
				{
					dataRow["InsApplicationDate"] = dateTimePickerInsuranceDate.Value;
				}
				else
				{
					dataRow["InsApplicationDate"] = DBNull.Value;
				}
				dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				dataRow["InsPolicyNumber"] = textBoxInsuranceNumber.Text;
				dataRow["InsRemarks"] = textBoxInsuranceRemarks.Text.Trim();
				dataRow["InsRequestedAmount"] = textBoxInsuranceReqAmount.Text;
				dataRow["InsStatus"] = checked(comboBoxInsuranceStatus.SelectedIndex + 1);
				dataRow["InsuranceID"] = textBoxInsuranceID.Text;
				dataRow["BankName"] = textBoxBankName.Text;
				dataRow["BankBranch"] = textBoxBankBranch.Text;
				dataRow["BankAccountNumber"] = textBoxBankAccountNumber.Text;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow["TaxIDNumber"] = textBoxTaxIDNumber.Text;
				if (ARAccountID != "")
				{
					dataRow["ARAccountID"] = ARAccountID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
				dataRow["LicenseNumber"] = textBoxLicenseNumber.Text;
				if (dateTimePickerLicenseExpDate.Checked)
				{
					dataRow["LicenseExpDate"] = dateTimePickerLicenseExpDate.Value;
				}
				else
				{
					dataRow["LicenseExpDate"] = DBNull.Value;
				}
				if (dateTimePickerContractExpDate.Checked)
				{
					dataRow["ContractExpDate"] = dateTimePickerContractExpDate.Value;
				}
				else
				{
					dataRow["ContractExpDate"] = DBNull.Value;
				}
				dataRow["SourceLeadID"] = SourceLeadID;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.CustomerAddressTable.Rows[0] : currentData.CustomerAddressTable.NewRow());
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["AddressID"] = textBoxAddressID.Text.Trim();
				dataRow["ContactName"] = textBoxContactName.Text;
				dataRow["Address1"] = textBoxAddress1.Text;
				dataRow["Address2"] = textBoxAddress2.Text;
				dataRow["Address3"] = textBoxAddress3.Text;
				dataRow["AddressPrintFormat"] = textBoxAddressPrintFormat.Text;
				dataRow["City"] = textBoxCity.Text;
				dataRow["State"] = textBoxState.Text;
				dataRow["Country"] = textBoxCountry.Text;
				dataRow["PostalCode"] = textBoxPostalCode.Text;
				if (textBoxLatitude.Text != "25.278306" && textBoxLongitude.Text != "55.322663")
				{
					dataRow["Latitude"] = textBoxLatitude.Text;
					dataRow["Longitude"] = textBoxLongitude.Text;
				}
				else
				{
					dataRow["Longitude"] = DBNull.Value;
					dataRow["Latitude"] = DBNull.Value;
				}
				dataRow["Department"] = textBoxDepartment.Text;
				dataRow["Phone1"] = textBoxPhone1.Text;
				dataRow["Phone2"] = textBoxPhone2.Text;
				dataRow["Fax"] = textBoxFax.Text;
				dataRow["Mobile"] = textBoxMobile.Text;
				dataRow["Email"] = textBoxEmail.Text;
				dataRow["Website"] = textBoxWebsite.Text;
				dataRow["Comment"] = textBoxComment.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerAddressTable.Rows.Add(dataRow);
				}
				currentData.CustomerContactTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridContacts.Rows)
				{
					dataRow = currentData.CustomerContactTable.NewRow();
					dataRow.BeginEdit();
					if (!(row.Cells["ContactID"].Value.ToString() == ""))
					{
						dataRow["CustomerID"] = textBoxCode.Text;
						dataRow["ContactID"] = row.Cells["ContactID"].Value.ToString();
						dataRow["Note"] = row.Cells["Note"].Value.ToString();
						dataRow["RowIndex"] = row.Index;
						dataRow["JobTitle"] = row.Cells["JobTitle"].Value.ToString();
						dataRow.EndEdit();
						currentData.CustomerContactTable.Rows.Add(dataRow);
					}
				}
				FormHelper formHelper = new FormHelper();
				currentData = (CustomerData)formHelper.GetEntityUDFData("Customer", "CustomerID", "", currentData, layoutControl1);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			try
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
				if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Customer", "CustomerID", textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("You dont have permission to edit.");
					return false;
				}
				textBoxCode.Text = textBoxCode.Text.Trim();
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
				if (textBoxName.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					textBoxName.Focus();
					textBoxName.SelectAll();
					return false;
				}
				if (textBoxCode.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					tabPageGeneral.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (textBoxCode.Text == comboBoxParentCustomer.SelectedID)
				{
					ErrorHelper.WarningMessage("A customer cannot be parent of itself.");
					tabPageGeneral.Selected = true;
					comboBoxParentCustomer.Focus();
					return false;
				}
				if (!isNewRecord && checkBoxIsInactive.Checked && Factory.CustomerSystem.HasBalance(textBoxCode.Text))
				{
					ErrorHelper.WarningMessage("A customer that has balance cannot be inactive.");
					return false;
				}
				if (radioButtonSublimit.Checked && comboBoxParentCustomer.SelectedID == "")
				{
					ErrorHelper.WarningMessage("Parent customer must be selected when the credit limit is set to Sublimit or select a different credit limit type.");
					comboBoxParentCustomer.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return true;
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.CustomerSystem.CreateCustomer(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Customer, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CustomerSystem.UpdateCustomer(currentData);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else if (clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					formManager.ResetDirty();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed)
				{
					isLoading = true;
					if (!(id == "") && CanClose())
					{
						PublicFunctions.StartWaiting(this);
						currentData = Factory.CustomerSystem.GetCustomerByID(id);
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
							DataSet transactiondetails = Factory.CustomerSystem.GetTransactiondetails(id);
							if (transactiondetails == null || transactiondetails.Tables.Count == 0 || transactiondetails.Tables[0].Rows.Count == 0)
							{
								comboBoxCurrency.Enabled = true;
							}
							else
							{
								comboBoxCurrency.Enabled = false;
							}
							IsNewRecord = false;
							DataSet entityCommentList = Factory.EntityCommentSystem.GetEntityCommentList(EntityTypesEnum.Customers, textBoxCode.Text);
							if (entityCommentList != null && entityCommentList.Tables.Count > 0 && entityCommentList.Tables[0].Rows.Count > 0)
							{
								toolStripButtonComments.Image = imageListComments.Images[1];
							}
							else
							{
								toolStripButtonComments.Image = imageListComments.Images[0];
							}
							LoadActivities();
							formManager.ResetDirty();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
			finally
			{
				isLoading = false;
				PublicFunctions.EndWaiting(this);
			}
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Sales;
		}

		public static int GetScreenID()
		{
			return 7003;
		}

		public void RefreshData()
		{
			Refresh();
			_ = Global.ConStatus;
			_ = 2;
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Customer", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Customer", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Customer", "CustomerID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Customer", "CustomerID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Customer", "CustomerID", toolStripTextBoxFind.Text.Trim()))
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

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
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
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.No)
				{
					return false;
				}
				return Factory.CustomerSystem.DeleteCustomer(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			dateTimePickerReviewDate.Clear();
			dateTimePickerEstablished.Clear();
			dateTimePickerCustomerSince.Clear();
			dateTimePickerLicenseExpDate.Clear();
			dateTimePickerContractExpDate.Clear();
			comboBoxCreditReviewBy.Clear();
			comboBoxRating.SelectedIndex = 0;
			textBoxTempLimit.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxInsuranceProvider.Clear();
			comboBoxCurrency.Enabled = true;
			comboBoxStatementMethod.SelectedIndex = 0;
			textBoxStatementEmail.Clear();
			textBoxProfileDetails.Text = "";
			textBoxRatingRemarks.Clear();
			comboBoxRatingBy.Clear();
			dateTimePickerRatingDate.Checked = false;
			dateTimePickerRatingDate.Value = DateTime.Now;
			dateTimePickerCLValidity.Clear();
			dateTimePickerCLValidity.Checked = false;
			comboBoxInsuranceRating.SelectedIndex = 0;
			datetimePickerEffectiveDate.Clear();
			datetimePickerEffectiveDate.Checked = false;
			dateTimePickerValidTo.Clear();
			dateTimePickerValidTo.Checked = false;
			checkBoxWeightInvoice.Checked = false;
			checkBoxparentACforposting.Checked = false;
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			comboBoxCollectionUser.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			comboBoxBilltoAddress.Text = "PRIMARY";
			comboBoxShiptoAddress.Text = "PRIMARY";
			textBoxAddressPrintFormat.Clear();
			textBoxBankAccountNumber.Clear();
			textBoxBankBranch.Clear();
			comboBoxCurrency.Clear();
			textBoxBankName.Clear();
			textBoxCity.Clear();
			textBoxComment.Clear();
			textBoxContactName.Clear();
			textBoxCountry.Clear();
			textBoxLatitude.Text = "25.2824891";
			textBoxLatitude.ForeColor = Color.Gray;
			textBoxLongitude.Text = "55.3583311";
			textBoxLongitude.ForeColor = Color.Gray;
			textBoxCreditLimit.Clear();
			textBoxDepartment.Clear();
			textBoxEmail.Clear();
			textBoxFax.Clear();
			textBoxForeignName.Clear();
			textBoxFormalName.Clear();
			textBoxMobile.Clear();
			textBoxPhone1.Clear();
			textBoxPhone2.Clear();
			textBoxPostalCode.Clear();
			textBoxState.Clear();
			textBoxWebsite.Clear();
			checkBoxIsInactive.Checked = false;
			checkBoxAllowConsignment.Checked = false;
			textBoxConsignCommission.Text = "0.00";
			textBoxDiscountPercent.Text = "0.00";
			textBoxRebatePercent.Text = "0.00";
			textBoxDeliveryInstructions.Clear();
			textBoxAccountInstructions.Clear();
			comboBoxCustomerGroup.Clear();
			comboBoxInsuranceStatus.SelectedIndex = 0;
			dateTimePickerInsuranceDate.Checked = false;
			textBoxInsuranceApprovedAmount.SetZero();
			textBoxInsuranceNumber.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsuranceReqAmount.SetZero();
			textBoxInsuranceID.Clear();
			textBoxLicenseNumber.Clear();
			textBoxUnsecuredLimit.Clear();
			checkBoxUnsecuredLimit.Checked = false;
			udfEntryGrid.ClearData();
			comboBoxArea.Clear();
			comboBoxLeadSource.Clear();
			comboBoxCountry.Clear();
			comboBoxCustomerClass.Clear();
			comboBoxParentCustomer.Clear();
			comboBoxPaymentMethods.Clear();
			comboBoxPaymentTerms.Clear();
			comboBoxPriceLevel.Clear();
			comboBoxSalesperson.Clear();
			comboBoxShippingMethods.Clear();
			checkBoxAcceptCheque.Checked = true;
			checkBoxAcceptPDC.Checked = true;
			checkBoxHold.Checked = false;
			radioButtonCreditLimitNoCredit.Checked = true;
			textBoxGraceDays.Text = "0";
			ARAccountID = "";
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			dateTimeBalanceConfirmationDate.Checked = false;
			textBoxConfirmationLevel.Clear();
			textBoxTaxIDNumber.Clear();
			comboBoxTaxGroup.Clear();
			comboBoxTaxOption.SelectedIndex = 0;
			if (CompanyPreferences.TaxEntityTypes.Contains("C"))
			{
				comboBoxTaxOption.SelectedIndex = checked(CompanyPreferences.DefaultTaxOption + 1);
				comboBoxTaxGroup.SelectedID = CompanyPreferences.DefaultTaxGroup;
			}
			IsNewRecord = true;
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Customer", "CustomerID");
			(dataGridContacts.DataSource as DataTable).Rows.Clear();
			(dataGridActivities.DataSource as DataTable).Rows.Clear();
			new FormHelper().ClearUDFData("Customer", "CustomerID", "", layoutControl1);
			formManager.ResetDirty();
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

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void CustomerClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		public bool CanClose()
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
						return true;
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxPaymentTerms.Text);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethods.Text);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelPriceLevel_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPriceLevel(comboBoxPriceLevel.Text);
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void radioButtonCreditLimitAmount_CheckedChanged(object sender, EventArgs e)
		{
			AmountTextBox amountTextBox = textBoxCreditLimit;
			MMSDateTimePicker mMSDateTimePicker = dateTimePickerCLValidity;
			bool flag = checkBoxUnsecuredLimit.Enabled = radioButtonCreditLimitAmount.Checked;
			bool enabled = mMSDateTimePicker.Enabled = flag;
			amountTextBox.Enabled = enabled;
		}

		private void comboBoxCustomerClass_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet customerProfileReport = Factory.CustomerSystem.GetCustomerProfileReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", "", "", "", "", showInactive: true, "");
					if (customerProfileReport == null || customerProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(customerProfileReport, "", "Customer Profile", SysDocTypes.None, isPrint, showPrintDialog);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerListFormObj);
		}

		private void buttonCategories_Click(object sender, EventArgs e)
		{
			EntityCategoryAssignDialog entityCategoryAssignDialog = new EntityCategoryAssignDialog();
			entityCategoryAssignDialog.EntityID = textBoxCode.Text;
			entityCategoryAssignDialog.EntityName = textBoxName.Text;
			entityCategoryAssignDialog.EntityType = EntityTypesEnum.Customers;
			entityCategoryAssignDialog.IsTreeView = true;
			if (!screenRight.Edit)
			{
				entityCategoryAssignDialog.AllowEdit = false;
			}
			entityCategoryAssignDialog.ShowDialog(this);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Customers;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void openContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string id = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				new FormHelper().EditContact(id);
			}
		}

		private void newContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			new FormHelper().EditContact(empty);
		}

		private void deleteContactToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridContacts.ActiveRow != null && dataGridContacts.ActiveRow.Cells["ContactID"].Value != null && !(dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString() == ""))
			{
				string iD = dataGridContacts.ActiveRow.Cells["ContactID"].Value.ToString();
				Factory.ContactSystem.DeleteContact(iD);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxRating_ValueChanged(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				textBoxInsuranceRemarks.Text += "  ";
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", 1, this);
			}
		}

		private void buttonAddActivity_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormActivator.BringFormToFront(FormActivator.ActivityDetailsFormObj);
				FormActivator.ActivityDetailsFormObj.AddNewActivity(CRMRelatedTypes.Customer, textBoxCode.Text);
			}
		}

		private void comboBoxActivityPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadActivities();
		}

		private void SetupActivityGrid()
		{
			try
			{
				dataGridContacts.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("Type");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Contact");
				dataTable.Columns.Add("Performed By");
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("Time", typeof(DateTime));
				dataGridActivities.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridActivities.DisplayLayout.Bands[0].Columns["SysDocID"];
				bool hidden = dataGridActivities.DisplayLayout.Bands[0].Columns["VoucherID"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				dataGridActivities.DisplayLayout.Bands[0].Columns["Time"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Time;
				dataGridActivities.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadActivities()
		{
			try
			{
				if (!isNewRecord && GlobalRules.IsModuleAvailable(AxolonModules.CRM))
				{
					DataSet activityListByLeadID = Factory.ActivitySystem.GetActivityListByLeadID(CRMRelatedTypes.Customer, textBoxCode.Text, comboBoxActivityPeriod.FromDate, comboBoxActivityPeriod.ToDate);
					DataTable dataTable = dataGridActivities.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in activityListByLeadID.Tables["Activity"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["SysDocID"] = row["Doc ID"];
						dataRow2["VoucherID"] = row["Number"];
						dataRow2["Name"] = row["Activity Name"];
						dataRow2["Type"] = row["Activity Type"];
						dataRow2["Contact"] = row["Contact"];
						dataRow2["Performed By"] = row["Performed By"];
						dataRow2["Date"] = row["Date"];
						dataRow2["Time"] = row["Date"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadLeadData()
		{
			if (SourceLeadID != "")
			{
				LeadData leadByID = Factory.LeadSystem.GetLeadByID(SourceLeadID);
				FillLeadData(leadByID);
				textBoxCode.ReadOnly = false;
			}
		}

		private void FillLeadData(DataSet currentLeadData)
		{
			try
			{
				if (currentLeadData != null && currentLeadData.Tables.Count != 0 && currentLeadData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentLeadData.Tables[0].Rows[0];
					textBoxCode.Text = "";
					textBoxName.Text = dataRow["LeadName"].ToString();
					textBoxForeignName.Text = dataRow["ForeignName"].ToString();
					textBoxFormalName.Text = dataRow["ShortName"].ToString();
					comboBoxCountry.SelectedID = dataRow["CountryID"].ToString();
					comboBoxArea.SelectedID = dataRow["AreaID"].ToString();
					comboBoxLeadSource.SelectedID = dataRow["LeadSourceID"].ToString();
					checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxProfileDetails.WordMLText = dataRow["ProfileDetails"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
					if (dataRow["Rating"] != DBNull.Value)
					{
						comboBoxRating.SelectedIndex = int.Parse(dataRow["Rating"].ToString());
					}
					else
					{
						comboBoxRating.SelectedIndex = 0;
					}
					if (dataRow["DateEstablished"] != DBNull.Value)
					{
						dateTimePickerEstablished.Value = DateTime.Parse(dataRow["DateEstablished"].ToString());
						dateTimePickerEstablished.Checked = true;
					}
					else
					{
						dateTimePickerEstablished.IsNull = true;
						dateTimePickerEstablished.Checked = false;
					}
					if (dataRow["CreditReviewDate"] != DBNull.Value)
					{
						dateTimePickerReviewDate.Value = DateTime.Parse(dataRow["CreditReviewDate"].ToString());
						dateTimePickerReviewDate.Checked = true;
					}
					else
					{
						dateTimePickerReviewDate.IsNull = true;
						dateTimePickerReviewDate.Checked = false;
					}
					if (dataRow["CreditReviewBy"] != DBNull.Value)
					{
						comboBoxCreditReviewBy.SelectedID = dataRow["CreditReviewBy"].ToString();
					}
					else
					{
						comboBoxCreditReviewBy.Clear();
					}
					switch (1)
					{
					case 3:
						radioButtonCreditLimitAmount.Checked = true;
						break;
					case 2:
						radioButtonCreditLimitNoCredit.Checked = true;
						break;
					case 4:
						radioButtonSublimit.Checked = true;
						break;
					default:
						radioButtonCreditLimitUnlimited.Checked = true;
						break;
					}
					SetHeaderName();
					if (currentLeadData.Tables.Contains("Lead_Address") && currentLeadData.Tables[1].Rows.Count != 0)
					{
						dataRow = currentLeadData.Tables[1].Rows[0];
						FillAddressData(dataRow);
						if (currentLeadData.Tables.Contains("Lead_Contact_Detail") && currentLeadData.Tables[2].Rows.Count != 0)
						{
							DataTable dataTable = dataGridContacts.DataSource as DataTable;
							dataTable.Rows.Clear();
							foreach (DataRow row in currentLeadData.Tables["Lead_Contact_Detail"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								foreach (DataColumn column in dataTable.Columns)
								{
									if (dataRow3.Table.Columns.Contains(column.ColumnName))
									{
										dataRow3[column.ColumnName] = row[column.ColumnName];
									}
									else
									{
										ErrorHelper.ErrorMessage(column.ColumnName + " Does not exist.");
									}
								}
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
							dataTable.AcceptChanges();
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void textBoxProfileDetails_ContentChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void toolStripButtonComments_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
					entityCommentsForm.EntityID = textBoxCode.Text;
					entityCommentsForm.EntityName = textBoxName.Text;
					entityCommentsForm.EntityType = EntityTypesEnum.Customers;
					entityCommentsForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void tabControlTab_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonLimitModify_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				CreditlimitReviewForm creditlimitReviewForm = new CreditlimitReviewForm();
				creditlimitReviewForm.StartPosition = FormStartPosition.CenterScreen;
				creditlimitReviewForm.StartPosition = FormStartPosition.Manual;
				creditlimitReviewForm.Location = checked(new Point(base.Location.X + (base.Width - creditlimitReviewForm.Width) * 2, base.Location.Y + (base.Height - creditlimitReviewForm.Height) * 2));
				creditlimitReviewForm.CustomerID = textBoxCode.Text;
				Factory.CreditLimitReviewSystem.GetCustomerIndividualsByID(textBoxCode.Text);
				creditlimitReviewForm.ShowDialog();
				LoadData(textBoxCode.Text);
			}
		}

		private void ultraPictureBox1_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			text = textBoxLatitude.Text;
			text2 = textBoxLongitude.Text;
			if (text != "" && text2 != "")
			{
				Process.Start("https://www.google.com/maps/preview/?q=" + text + "," + text2);
			}
			else
			{
				Process.Start("https://www.google.com/maps/preview/?q=25.2048° N, 55.2708° E");
			}
		}

		private void textBoxLatitude_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLongitude_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLatitude_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLongitude_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void textBoxLatitude_KeyDown(object sender, KeyPressEventArgs e)
		{
		}

		private void textBoxLatitude_MouseClick(object sender, MouseEventArgs e)
		{
			if (textBoxLatitude.Text.Equals("25.2824891"))
			{
				textBoxLatitude.Text = "";
				textBoxLatitude.ForeColor = Color.Black;
			}
			else if (textBoxLatitude.Text != "")
			{
				textBoxLatitude.ForeColor = Color.Black;
			}
		}

		private void textBoxLongitude_MouseClick(object sender, MouseEventArgs e)
		{
			if (textBoxLongitude.Text.Equals("55.3583311"))
			{
				textBoxLongitude.Text = "";
				textBoxLongitude.ForeColor = Color.Black;
			}
			else if (textBoxLongitude.Text != "")
			{
				textBoxLongitude.ForeColor = Color.Black;
			}
		}

		private void textBoxLatitude_MouseLeave(object sender, EventArgs e)
		{
			if (textBoxLatitude.Text.Equals(null) || textBoxLatitude.Text.Equals(""))
			{
				textBoxLatitude.Text = "25.2824891";
				textBoxLatitude.ForeColor = Color.Gray;
			}
		}

		private void textBoxLongitude_MouseLeave(object sender, EventArgs e)
		{
			if (textBoxLongitude.Text.Equals(null) || textBoxLongitude.Text.Equals(""))
			{
				textBoxLongitude.Text = "55.3583311";
				textBoxLongitude.ForeColor = Color.Gray;
			}
		}

		private void comboBoxParentCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxParentCustomer.SelectedID == "")
			{
				checkBoxparentACforposting.Checked = false;
				checkBoxparentACforposting.Enabled = false;
			}
			else
			{
				checkBoxparentACforposting.Enabled = true;
			}
		}

		private void ultraFormattedLinkLabel9_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private void checkBoxUnsecuredLimit_CheckedChanged(object sender, EventArgs e)
		{
			textBoxUnsecuredLimit.Enabled = checkBoxUnsecuredLimit.Checked;
		}

		private void buttonAccounts_Click(object sender, EventArgs e)
		{
			PayeeAccountsForm payeeAccountsForm = new PayeeAccountsForm();
			payeeAccountsForm.ARAccount = ARAccountID;
			payeeAccountsForm.EntityType = EntityTypesEnum.Customers;
			if (payeeAccountsForm.ShowDialog() == DialogResult.OK)
			{
				ARAccountID = payeeAccountsForm.ARAccount;
				if (!formManager.IsForcedDirty)
				{
					formManager.IsForcedDirty = payeeAccountsForm.IsDirty;
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

		private void Deleted_menuItem_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				return;
			}
			ToolStripItem toolStripItem = (ToolStripItem)sender;
			if (toolStripItem.Name == "Customer Ledger")
			{
				CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
				customerLedgerForm.SelectedID = textBoxCode.Text;
				customerLedgerForm.Show();
				customerLedgerForm.BringToFront();
			}
			else if (toolStripItem.Name == "Sale Statistics")
			{
				InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
				inventorySalesStatisticForm.ShowCustomer = true;
				inventorySalesStatisticForm.SelectedCode = textBoxCode.Text;
				inventorySalesStatisticForm.Show();
				inventorySalesStatisticForm.BringToFront();
			}
			else if (toolStripItem.Name == "Transaction Details")
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				DataSet dataSet = new DataSet();
				dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
				comboSearchDialogNew.SelectedItem = "";
				if (textBoxCode.Text != "" && textBoxCode.Text != null)
				{
					comboSearchDialogNew.SelectedProvider = textBoxCode.Text;
				}
				comboSearchDialogNew.ShowDialog();
			}
		}

		private void buttonCustomerInsuranceClaim_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerInsuranceClaimFormObj);
		}

		private void openContactToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
		}

		private void saleStatistcsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonHistory_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				DocumentVersionList documentVersionList = new DocumentVersionList();
				documentVersionList.LoadData(currentData, ScreenTypes.Card, 1, "", textBoxCode.Text);
				documentVersionList.ShowDialog();
			}
		}

		private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddCustomerSignature(textBoxCode.Text, image))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.CustomerSystem.RemoveCustomerSignature(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = null;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetCustomerSignatureThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void PlantiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CaseClientDetailsFormObj);
			FormActivator.CaseClientDetailsFormObj.SourceID = textBoxCode.Text;
			FormActivator.CaseClientDetailsFormObj.ClientType = "C";
			FormActivator.CaseClientDetailsFormObj.IsPlantiff = true;
			FormActivator.CaseClientDetailsFormObj.LoadCustomerData();
		}

		private void defendantToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CaseClientDetailsFormObj);
			FormActivator.CaseClientDetailsFormObj.SourceID = textBoxCode.Text;
			FormActivator.CaseClientDetailsFormObj.ClientType = "C";
			FormActivator.CaseClientDetailsFormObj.IsDefendant = true;
			FormActivator.CaseClientDetailsFormObj.LoadCustomerData();
		}

		private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void layoutControlItem9_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerClass(comboBoxCustomerClass.Text);
		}

		private void layoutControlItem10_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerGroup(comboBoxCustomerGroup.SelectedID);
		}

		private void layoutControlItem11_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.Text);
		}

		private void layoutControlItem12_Click(object sender, EventArgs e)
		{
			new FormHelper().EditArea(comboBoxArea.Text);
		}

		private void layoutControlItem14_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.Text);
		}

		private void layoutControlItem38_Click(object sender, EventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.Text);
		}

		private void layoutControlItem39_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxShiptoAddress.SelectedID);
		}

		private void layoutControlItem41_Click(object sender, EventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.LeadSource, comboBoxLeadSource.SelectedID);
		}

		private void layoutControlItem42_Click(object sender, EventArgs e)
		{
			new FormHelper().EditCustomerAddress(textBoxCode.Text, comboBoxBilltoAddress.SelectedID);
		}

		private void layoutControlItem43_Click(object sender, EventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethods.Text);
		}

		private void layoutControlItem59_Click(object sender, EventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
		}

		private void layoutControlItem64_Click(object sender, EventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethods.Text);
		}

		private void layoutControlItem66_Click(object sender, EventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxPaymentTerms.Text);
		}

		private void toolStripButtonDesignLayout_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCustomerLedger_Click(object sender, EventArgs e)
		{
			CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
			customerLedgerForm.SelectedID = textBoxCode.Text;
			customerLedgerForm.Show();
			customerLedgerForm.BringToFront();
		}

		private void menuItemSalesStatistics_Click(object sender, EventArgs e)
		{
			InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
			inventorySalesStatisticForm.ShowCustomer = true;
			inventorySalesStatisticForm.SelectedCode = textBoxCode.Text;
			inventorySalesStatisticForm.Show();
			inventorySalesStatisticForm.BringToFront();
		}

		private void transactionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
			comboSearchDialogNew.IsMultiSelect = false;
			DataSet dataSet = new DataSet();
			dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
			comboSearchDialogNew.SelectedItem = "";
			if (textBoxCode.Text != "" && textBoxCode.Text != null)
			{
				comboSearchDialogNew.SelectedProvider = textBoxCode.Text;
			}
			comboSearchDialogNew.ShowDialog();
		}

		private void menuItemLayoutDesign_Click(object sender, EventArgs e)
		{
			new FormHelper().CustomizeLayout(layoutControl1);
		}

		private void menuItemCustomFields_Click(object sender, EventArgs e)
		{
			new UDFSetupForm(base.Name, "Customer").ShowDialog();
		}
	}
}
