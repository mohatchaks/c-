using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Recruitment
{
	public class AppointmentDetailsForm : Form, IForm
	{
		private CandidateData currentData;

		private EmployeeData employeeData;

		private const string TABLENAME_CONST = "Candidate";

		private const string IDFIELD_CONST = "CandidateID";

		private const string PASSPORTFIELD_CONST = "PassportNo";

		private bool isNewRecord = true;

		private List<MMSDateTimePicker> dateTimePickersToValidate = new List<MMSDateTimePicker>();

		private List<MMSDateTimePicker> dateTimePickers = new List<MMSDateTimePicker>();

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

		private FormManager formManager;

		private UltraTabControl ultraTabControl;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Panel panel1;

		private UltraTabPageControl tabPageUserDefined;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem dependentsToolStripMenuItem;

		private ToolStripMenuItem documentsToolStripMenuItem;

		private ToolStripMenuItem skillsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelCustomerNameHeader;

		private UDFEntryControl udfEntryGrid;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private UltraTabPageControl ultraTabPageControl1;

		private OpenFileDialog openFileDialog1;

		private ToolStripButton toolStripButtonEmployee;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraTabPageControl ultraTabPageControl4;

		private UltraTabPageControl ultraTabPageControl5;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private MMLabel mmLabel20;

		private MMTextBox textBoxPostalCode;

		private MMLabel mmLabel18;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel17;

		private MMTextBox textBoxMobile;

		private MMLabel mmLabel16;

		private MMTextBox textBoxFax;

		private MMLabel mmLabel15;

		private MMTextBox textBoxPhone2;

		private MMLabel mmLabel14;

		private MMTextBox textBoxPhone1;

		private MMLabel mmLabel12;

		private MMTextBox textBoxCountry;

		private MMLabel mmLabel11;

		private MMTextBox textBoxState;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel10;

		private MMTextBox textBoxAddress1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxAddressID;

		private Panel panelGeneral;

		private MMLabel mmLabel69;

		private PictureBox pictureBoxNoImage;

		private UltraFormattedLinkLabel linkLoadImage;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel54;

		private MMLabel mmLabel53;

		private MMTextBox textBoxPassportNo;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private MMTextBox textBoxPPAddress;

		private MMLabel mmLabel21;

		private MMTextBox textBoxBloodGroup;

		private ReligionComboBox comboBoxReligion;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MaritalStatusComboBox comboBoxMaritalStatus;

		private MMLabel mmLabel47;

		private MMLabel mmLabel19;

		private MMTextBox textBoxSpouseName;

		private MMLabel mmLabel9;

		private MMTextBox textBoxMotherName;

		private MMLabel mmLabel6;

		private MMTextBox textBoxFatherName;

		private MMLabel mmLabel4;

		private MMSDateTimePicker dateTimePPExpiryDate;

		private MMLabel mmLabel3;

		private MMSDateTimePicker dateTimePPIssueDate;

		private MMTextBox textBoxPPIssuePlace;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMTextBox textBoxBirthPlace;

		private MMLabel mmLabel33;

		private MMLabel mmLabel51;

		private MMSDateTimePicker dateTimeBirthDate;

		private MMTextBox textBoxAge;

		private MMLabel mmLabel31;

		private GenderComboBox comboBoxGender;

		private NationalityComboBox comboBoxNationality;

		private MMLabel mmLabel5;

		private MMLabel labelCandidateNumber;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxSurName;

		private MMTextBox textBoxGivenName;

		private MMLabel labelGivenName;

		private Panel panelRecruitment;

		private MMLabel mmLabel81;

		private MMLabel mmLabel76;

		private TextBox textBoxLanguageName;

		private TextBox textBoxQualificationName;

		private NumericUpDown numericExperienceAbroad;

		private NumericUpDown numericExperienceLocal;

		private LanguageComboBox comboBoxLanguage;

		private QualificationComboBox comboBoxQualification;

		private MMLabel mmLabel75;

		private MMLabel mmLabel74;

		private MMLabel mmLabel73;

		private MMLabel mmLabel71;

		private TextBox textBoxActualDesignationName;

		private TextBox textBoxThroughAgentName;

		private PositionComboBox comboBoxPositionActual;

		private MMLabel mmLabel68;

		private AgentComboBox comboBoxAgentThrough;

		private MMLabel mmLabel55;

		private MMTextBox textBoxRemarks;

		private SelectionStatusComboBox comboBoxSelectionStatus;

		private MMLabel mmLabel39;

		private MMLabel mmLabel38;

		private MMTextBox textBoxSelectedAt;

		private MMSDateTimePicker dateTimeSelectedOn;

		private MMLabel mmLabel37;

		private MMLabel mmLabel36;

		private Panel panelVisaProcess;

		private UltraGroupBox panelVisaIMG;

		private MMLabel mmLabel77;

		private MMSDateTimePicker dateTimeVisaCopyToAgentOn;

		private ComboBox comboBoxVisaAppliedThroughIMG;

		private MMSDateTimePicker dateTimeVisaExpiryDate;

		private MMSDateTimePicker dateTimeVisaIssueDate;

		private MMLabel mmLabel87;

		private MMTextBox textBoxVisaIssuePlaceIMG;

		private MMLabel mmLabel86;

		private MMTextBox textBoxVisaNumber;

		private MMLabel mmLabel85;

		private MMLabel mmLabel84;

		private MMTextBox textBoxUIDNumberIMG;

		private MMLabel mmLabel83;

		private MMSDateTimePicker dateTimeVisaPostedOn;

		private MMLabel mmLabel26;

		private MMSDateTimePicker dateTimeApprovedOn;

		private MMLabel mmLabel28;

		private MMLabel mmLabel32;

		private UltraGroupBox panelVisaMOL;

		private TextBox textBoxVisaDesignationName;

		private PositionComboBox comboBoxPositionVisa;

		private MMLabel mmLabel67;

		private TextBox textBoxSponsorName;

		private SponsorComboBox comboBoxSponsor;

		private MMLabel mmLabel70;

		private ComboBox comboBoxBGTypeMOL;

		private MMLabel mmLabel82;

		private MMSDateTimePicker dateTimeApprovalFeePaidOnMOL;

		private MMTextBox textBoxTempWPNo;

		private MMLabel mmLabel80;

		private MMSDateTimePicker dateTimeApprovalValidTillMOL;

		private MMLabel mmLabel79;

		private MMSDateTimePicker dateTimeBGPaidOnMOL;

		private MMLabel mmLabel25;

		private MMLabel mmLabel24;

		private MMSDateTimePicker dateTimeApprovalDateMOL;

		private MMLabel mmLabel22;

		private MMTextBox textBoxMOLMBNo;

		private MMLabel mmLabel7;

		private MMSDateTimePicker dateTimeApplTypingDateMOL;

		private MMLabel mmLabel30;

		private Panel panelArrival;

		private XPButton buttonMakeEmployee;

		private MMTextBox textBoxEmployeeNo;

		private MMLabel mmLabel34;

		private MMSDateTimePicker dateTimeArrivedOn;

		private MMLabel mmLabel27;

		private Panel panelMedicalEmirates;

		private UltraGroupBox panelEmirates;

		private MMSDateTimePicker dateTimeValidityEID;

		private MMSDateTimePicker dateTimeCollectedOnEID;

		private MMLabel mmLabel42;

		private MMLabel mmLabel43;

		private MMSDateTimePicker dateTimeAttendedDateEID;

		private MMLabel mmLabel44;

		private MMTextBox textBoxNationalID;

		private MMLabel mmLabel45;

		private MMSDateTimePicker dateTimeApplTypingDateEID;

		private MMLabel mmLabel46;

		private UltraGroupBox panelMedicalDetail;

		private MMTextBox textBoxHealthCardNo;

		private MMLabel mmLabel72;

		private ComboBox comboBoxMedicalResult;

		private MMTextBox textBoxMedicalNote;

		private MMLabel mmLabel78;

		private MMLabel mmLabel89;

		private MMSDateTimePicker dateTimeMedicalCollectedOn;

		private MMLabel mmLabel41;

		private MMSDateTimePicker dateTimeMedicalAttendedOn;

		private MMLabel mmLabel40;

		private MMSDateTimePicker dateTimeMedicalTypingOn;

		private MMLabel mmLabel35;

		private Panel panelWPRP;

		private UltraGroupBox panelMedicalReport;

		private ComboBox comboBoxProcessType;

		private MMSDateTimePicker dateTimeRPExpiryDate;

		private MMLabel mmLabel56;

		private MMLabel mmLabel65;

		private MMSDateTimePicker dateTimeRPIssueDate;

		private MMLabel mmLabel64;

		private MMTextBox textBoxRPIssuePlace;

		private MMLabel mmLabel63;

		private MMSDateTimePicker dateTimePassportCollectedOnRP;

		private MMLabel mmLabel52;

		private MMSDateTimePicker dateTimeSubmittedZajilOnRP;

		private MMLabel mmLabel50;

		private MMSDateTimePicker dateTimeApplApprovedOnRP;

		private MMLabel mmLabel49;

		private MMSDateTimePicker dateTimeApplPostedOnRP;

		private MMLabel mmLabel48;

		private UltraGroupBox panelAGT;

		private MMTextBox textBoxPersonIDNo;

		private MMLabel mmLabel66;

		private MMTextBox textBoxWPIssuePlace;

		private MMSDateTimePicker dateTimeWPExpiryDate;

		private MMSDateTimePicker dateTimeWPIssueDate;

		private MMLabel mmLabel62;

		private MMLabel mmLabel61;

		private MMLabel mmLabel60;

		private MMTextBox textBoxWPNo;

		private MMSDateTimePicker dateTimeAGTSubmittedOn;

		private MMLabel mmLabel57;

		private MMLabel mmLabel58;

		private MMSDateTimePicker dateTimeAGTTypedOn;

		private MMLabel mmLabel59;

		private Label labelCategory;

		private MMTextBox textBoxAGTMBNo;

		private MMLabel mmLabel91;

		private MMLabel mmLabel90;

		private ComboBox comboBoxAGTType;

		private Label labelCancelled;

		private ToolStripButton toolStripButtonInformation;

		private TextBox textBoxDesignation;

		private PositionComboBox comboBoxDesignation;

		private VisaTypeComboBox comboBoxvisaType;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl ultraTabPageControl6;

		private DataEntryGrid dataGridPayrollItem;

		private PayrollItemComboBox comboBoxPayrollItem;

		private AmountTextBox textBoxTotalSalary;

		private Label label7;

		private TextBox textBox1;

		private SponsorComboBox sponsorComboBox;

		private MMLabel mmLabel92;

		private PortComboBox comboBoxArrivalPort;

		private EmployeeTypeComboBox comboBoxCategory;

		private MMLabel mmLabel29;

		private MMLabel mmLabel88;

		private EmployeeGroupComboBox comboBoxGroup;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMLabel mmLabel93;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem morePrintToolStripMenuItem;

		private IContainer components;

		private WorkflowType _workFlowType = WorkflowType.None;

		private ScreenAccessRight screenRight;

		private bool isExist;

		private bool isCancelled;

		private string prefix = string.Empty;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5011;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public WorkflowType WorkflowStep
		{
			get
			{
				return _workFlowType;
			}
			set
			{
				_workFlowType = value;
			}
		}

		public bool IsExist
		{
			get
			{
				return isExist;
			}
			set
			{
				buttonMakeEmployee.Enabled = value;
				isExist = value;
			}
		}

		private bool IsCancelled
		{
			get
			{
				return isCancelled;
			}
			set
			{
				labelCancelled.Visible = value;
				ultraTabControl.Enabled = !value;
				buttonNew.Enabled = !value;
				buttonSave.Enabled = !value;
				isCancelled = value;
			}
		}

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
					textBoxAddressID.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxAddressID.Enabled = false;
				}
				toolStripButtonAttach.Enabled = !value;
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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				bool enabled = toolStripButtonPreview.Enabled = !isNewRecord;
				toolStripButton.Enabled = enabled;
			}
		}

		public AppointmentDetailsForm()
		{
			InitializeComponent();
			AddDateTimePickersToValidate();
		}

		private void AddDateTimePickersToValidate()
		{
			dateTimePickersToValidate.Add(dateTimeSelectedOn);
			dateTimePickersToValidate.Add(dateTimeApplTypingDateMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalDateMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalValidTillMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalFeePaidOnMOL);
			dateTimePickersToValidate.Add(dateTimeBGPaidOnMOL);
			dateTimePickersToValidate.Add(dateTimeVisaPostedOn);
			dateTimePickersToValidate.Add(dateTimeApprovedOn);
			dateTimePickersToValidate.Add(dateTimeVisaIssueDate);
			dateTimePickersToValidate.Add(dateTimeVisaExpiryDate);
			dateTimePickersToValidate.Add(dateTimeArrivedOn);
			dateTimePickersToValidate.Add(dateTimeMedicalTypingOn);
			dateTimePickersToValidate.Add(dateTimeMedicalAttendedOn);
			dateTimePickersToValidate.Add(dateTimeMedicalCollectedOn);
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Recruitment.AppointmentDetailsForm));
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridPayrollItem = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
			textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
			label7 = new System.Windows.Forms.Label();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelGeneral = new System.Windows.Forms.Panel();
			mmLabel93 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxGroup = new Micromind.DataControls.EmployeeGroupComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxvisaType = new Micromind.DataControls.VisaTypeComboBox();
			textBoxDesignation = new System.Windows.Forms.TextBox();
			comboBoxDesignation = new Micromind.DataControls.PositionComboBox();
			mmLabel69 = new Micromind.UISupport.MMLabel();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textBoxPassportNo = new Micromind.UISupport.MMTextBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			textBoxPPAddress = new Micromind.UISupport.MMTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxBloodGroup = new Micromind.UISupport.MMTextBox();
			comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxSpouseName = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxMotherName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxFatherName = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimePPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel3 = new Micromind.UISupport.MMLabel();
			dateTimePPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxPPIssuePlace = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			dateTimeBirthDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxAge = new Micromind.UISupport.MMTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			comboBoxGender = new Micromind.DataControls.GenderComboBox();
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCandidateNumber = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxSurName = new Micromind.UISupport.MMTextBox();
			textBoxGivenName = new Micromind.UISupport.MMTextBox();
			labelGivenName = new Micromind.UISupport.MMLabel();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelArrival = new System.Windows.Forms.Panel();
			textBox1 = new System.Windows.Forms.TextBox();
			sponsorComboBox = new Micromind.DataControls.SponsorComboBox();
			mmLabel92 = new Micromind.UISupport.MMLabel();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			buttonMakeEmployee = new Micromind.UISupport.XPButton();
			textBoxEmployeeNo = new Micromind.UISupport.MMTextBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			dateTimeArrivedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel27 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelRecruitment = new System.Windows.Forms.Panel();
			comboBoxArrivalPort = new Micromind.DataControls.PortComboBox();
			comboBoxCategory = new Micromind.DataControls.EmployeeTypeComboBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			mmLabel88 = new Micromind.UISupport.MMLabel();
			mmLabel81 = new Micromind.UISupport.MMLabel();
			mmLabel76 = new Micromind.UISupport.MMLabel();
			textBoxLanguageName = new System.Windows.Forms.TextBox();
			textBoxQualificationName = new System.Windows.Forms.TextBox();
			numericExperienceAbroad = new System.Windows.Forms.NumericUpDown();
			numericExperienceLocal = new System.Windows.Forms.NumericUpDown();
			comboBoxLanguage = new Micromind.DataControls.LanguageComboBox();
			comboBoxQualification = new Micromind.DataControls.QualificationComboBox();
			mmLabel75 = new Micromind.UISupport.MMLabel();
			mmLabel74 = new Micromind.UISupport.MMLabel();
			mmLabel73 = new Micromind.UISupport.MMLabel();
			mmLabel71 = new Micromind.UISupport.MMLabel();
			textBoxActualDesignationName = new System.Windows.Forms.TextBox();
			textBoxThroughAgentName = new System.Windows.Forms.TextBox();
			comboBoxPositionActual = new Micromind.DataControls.PositionComboBox();
			mmLabel68 = new Micromind.UISupport.MMLabel();
			comboBoxAgentThrough = new Micromind.DataControls.AgentComboBox();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			comboBoxSelectionStatus = new Micromind.DataControls.SelectionStatusComboBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxSelectedAt = new Micromind.UISupport.MMTextBox();
			dateTimeSelectedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelVisaProcess = new System.Windows.Forms.Panel();
			panelVisaIMG = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel77 = new Micromind.UISupport.MMLabel();
			dateTimeVisaCopyToAgentOn = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxVisaAppliedThroughIMG = new System.Windows.Forms.ComboBox();
			dateTimeVisaExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVisaIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel87 = new Micromind.UISupport.MMLabel();
			textBoxVisaIssuePlaceIMG = new Micromind.UISupport.MMTextBox();
			mmLabel86 = new Micromind.UISupport.MMLabel();
			textBoxVisaNumber = new Micromind.UISupport.MMTextBox();
			mmLabel85 = new Micromind.UISupport.MMLabel();
			mmLabel84 = new Micromind.UISupport.MMLabel();
			textBoxUIDNumberIMG = new Micromind.UISupport.MMTextBox();
			mmLabel83 = new Micromind.UISupport.MMLabel();
			dateTimeVisaPostedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel26 = new Micromind.UISupport.MMLabel();
			dateTimeApprovedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel28 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			panelVisaMOL = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxVisaDesignationName = new System.Windows.Forms.TextBox();
			comboBoxPositionVisa = new Micromind.DataControls.PositionComboBox();
			mmLabel67 = new Micromind.UISupport.MMLabel();
			textBoxSponsorName = new System.Windows.Forms.TextBox();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			mmLabel70 = new Micromind.UISupport.MMLabel();
			comboBoxBGTypeMOL = new System.Windows.Forms.ComboBox();
			mmLabel82 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalFeePaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxTempWPNo = new Micromind.UISupport.MMTextBox();
			mmLabel80 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalValidTillMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel79 = new Micromind.UISupport.MMLabel();
			dateTimeBGPaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalDateMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxMOLMBNo = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimeApplTypingDateMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel30 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelMedicalEmirates = new System.Windows.Forms.Panel();
			panelEmirates = new Infragistics.Win.Misc.UltraGroupBox();
			dateTimeValidityEID = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeCollectedOnEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel42 = new Micromind.UISupport.MMLabel();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			dateTimeAttendedDateEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel44 = new Micromind.UISupport.MMLabel();
			textBoxNationalID = new Micromind.UISupport.MMTextBox();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			dateTimeApplTypingDateEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel46 = new Micromind.UISupport.MMLabel();
			panelMedicalDetail = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxHealthCardNo = new Micromind.UISupport.MMTextBox();
			mmLabel72 = new Micromind.UISupport.MMLabel();
			comboBoxMedicalResult = new System.Windows.Forms.ComboBox();
			mmLabel89 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalCollectedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel41 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalAttendedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel40 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalTypingOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel35 = new Micromind.UISupport.MMLabel();
			textBoxMedicalNote = new Micromind.UISupport.MMTextBox();
			mmLabel78 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelWPRP = new System.Windows.Forms.Panel();
			panelMedicalReport = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxProcessType = new System.Windows.Forms.ComboBox();
			dateTimeRPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel56 = new Micromind.UISupport.MMLabel();
			mmLabel65 = new Micromind.UISupport.MMLabel();
			dateTimeRPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel64 = new Micromind.UISupport.MMLabel();
			textBoxRPIssuePlace = new Micromind.UISupport.MMTextBox();
			mmLabel63 = new Micromind.UISupport.MMLabel();
			dateTimePassportCollectedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel52 = new Micromind.UISupport.MMLabel();
			dateTimeSubmittedZajilOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel50 = new Micromind.UISupport.MMLabel();
			dateTimeApplApprovedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel49 = new Micromind.UISupport.MMLabel();
			dateTimeApplPostedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel48 = new Micromind.UISupport.MMLabel();
			panelAGT = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxAGTMBNo = new Micromind.UISupport.MMTextBox();
			mmLabel91 = new Micromind.UISupport.MMLabel();
			mmLabel90 = new Micromind.UISupport.MMLabel();
			comboBoxAGTType = new System.Windows.Forms.ComboBox();
			textBoxPersonIDNo = new Micromind.UISupport.MMTextBox();
			mmLabel66 = new Micromind.UISupport.MMLabel();
			textBoxWPIssuePlace = new Micromind.UISupport.MMTextBox();
			dateTimeWPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeWPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel62 = new Micromind.UISupport.MMLabel();
			mmLabel61 = new Micromind.UISupport.MMLabel();
			mmLabel60 = new Micromind.UISupport.MMLabel();
			textBoxWPNo = new Micromind.UISupport.MMTextBox();
			dateTimeAGTSubmittedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel57 = new Micromind.UISupport.MMLabel();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			dateTimeAGTTypedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel59 = new Micromind.UISupport.MMLabel();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonEmployee = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			labelCancelled = new System.Windows.Forms.Label();
			labelCategory = new System.Windows.Forms.Label();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			dependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			morePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraTabPageControl6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).BeginInit();
			tabPageGeneral.SuspendLayout();
			panelGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDesignation).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			panelArrival.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)sponsorComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			tabPageDetails.SuspendLayout();
			panelRecruitment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceAbroad).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceLocal).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLanguage).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionActual).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAgentThrough).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			panelVisaProcess.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaIMG).BeginInit();
			panelVisaIMG.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaMOL).BeginInit();
			panelVisaMOL.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionVisa).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			panelMedicalEmirates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelEmirates).BeginInit();
			panelEmirates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalDetail).BeginInit();
			panelMedicalDetail.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			panelWPRP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalReport).BeginInit();
			panelMedicalReport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelAGT).BeginInit();
			panelAGT.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			ultraTabPageControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl).BeginInit();
			ultraTabControl.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl6.Controls.Add(dataGridPayrollItem);
			ultraTabPageControl6.Controls.Add(comboBoxPayrollItem);
			ultraTabPageControl6.Controls.Add(textBoxTotalSalary);
			ultraTabPageControl6.Controls.Add(label7);
			ultraTabPageControl6.Location = new System.Drawing.Point(1, 20);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(788, 366);
			dataGridPayrollItem.AllowAddNew = false;
			dataGridPayrollItem.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayrollItem.DisplayLayout.Appearance = appearance;
			dataGridPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridPayrollItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayrollItem.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridPayrollItem.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayrollItem.LoadLayoutFailed = false;
			dataGridPayrollItem.Location = new System.Drawing.Point(12, 3);
			dataGridPayrollItem.Name = "dataGridPayrollItem";
			dataGridPayrollItem.ShowClearMenu = true;
			dataGridPayrollItem.ShowDeleteMenu = true;
			dataGridPayrollItem.ShowInsertMenu = true;
			dataGridPayrollItem.ShowMoveRowsMenu = true;
			dataGridPayrollItem.Size = new System.Drawing.Size(765, 339);
			dataGridPayrollItem.TabIndex = 0;
			dataGridPayrollItem.Text = "dataEntryGrid1";
			comboBoxPayrollItem.Assigned = false;
			comboBoxPayrollItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItem.CustomReportFieldName = "";
			comboBoxPayrollItem.CustomReportKey = "";
			comboBoxPayrollItem.CustomReportValueType = 1;
			comboBoxPayrollItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItem.DisplayLayout.Appearance = appearance13;
			comboBoxPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItem.Editable = true;
			comboBoxPayrollItem.FilterString = "";
			comboBoxPayrollItem.HasAllAccount = false;
			comboBoxPayrollItem.HasCustom = false;
			comboBoxPayrollItem.IsDataLoaded = false;
			comboBoxPayrollItem.IsDeduction = false;
			comboBoxPayrollItem.Location = new System.Drawing.Point(560, 3);
			comboBoxPayrollItem.MaxDropDownItems = 12;
			comboBoxPayrollItem.Name = "comboBoxPayrollItem";
			comboBoxPayrollItem.ShowInactiveItems = false;
			comboBoxPayrollItem.ShowQuickAdd = true;
			comboBoxPayrollItem.Size = new System.Drawing.Size(81, 21);
			comboBoxPayrollItem.TabIndex = 19;
			comboBoxPayrollItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItem.Visible = false;
			textBoxTotalSalary.AllowDecimal = true;
			textBoxTotalSalary.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalSalary.CustomReportFieldName = "";
			textBoxTotalSalary.CustomReportKey = "";
			textBoxTotalSalary.CustomReportValueType = 1;
			textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
			textBoxTotalSalary.IsComboTextBox = false;
			textBoxTotalSalary.IsModified = false;
			textBoxTotalSalary.Location = new System.Drawing.Point(663, 342);
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
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(585, 345);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(68, 13);
			label7.TabIndex = 27;
			label7.Text = "Total Salary:";
			tabPageGeneral.Controls.Add(panelGeneral);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(810, 519);
			panelGeneral.Controls.Add(mmLabel93);
			panelGeneral.Controls.Add(ultraFormattedLinkLabel2);
			panelGeneral.Controls.Add(comboBoxGroup);
			panelGeneral.Controls.Add(ultraFormattedLinkLabel3);
			panelGeneral.Controls.Add(comboBoxvisaType);
			panelGeneral.Controls.Add(textBoxDesignation);
			panelGeneral.Controls.Add(comboBoxDesignation);
			panelGeneral.Controls.Add(mmLabel69);
			panelGeneral.Controls.Add(pictureBoxNoImage);
			panelGeneral.Controls.Add(linkLoadImage);
			panelGeneral.Controls.Add(textBoxNote);
			panelGeneral.Controls.Add(mmLabel54);
			panelGeneral.Controls.Add(mmLabel53);
			panelGeneral.Controls.Add(textBoxPassportNo);
			panelGeneral.Controls.Add(linkRemovePicture);
			panelGeneral.Controls.Add(linkAddPicture);
			panelGeneral.Controls.Add(pictureBoxPhoto);
			panelGeneral.Controls.Add(textBoxPPAddress);
			panelGeneral.Controls.Add(mmLabel21);
			panelGeneral.Controls.Add(textBoxBloodGroup);
			panelGeneral.Controls.Add(comboBoxReligion);
			panelGeneral.Controls.Add(ultraFormattedLinkLabel1);
			panelGeneral.Controls.Add(comboBoxMaritalStatus);
			panelGeneral.Controls.Add(mmLabel47);
			panelGeneral.Controls.Add(mmLabel19);
			panelGeneral.Controls.Add(textBoxSpouseName);
			panelGeneral.Controls.Add(mmLabel9);
			panelGeneral.Controls.Add(textBoxMotherName);
			panelGeneral.Controls.Add(mmLabel6);
			panelGeneral.Controls.Add(textBoxFatherName);
			panelGeneral.Controls.Add(mmLabel4);
			panelGeneral.Controls.Add(dateTimePPExpiryDate);
			panelGeneral.Controls.Add(mmLabel3);
			panelGeneral.Controls.Add(dateTimePPIssueDate);
			panelGeneral.Controls.Add(textBoxPPIssuePlace);
			panelGeneral.Controls.Add(mmLabel1);
			panelGeneral.Controls.Add(mmLabel2);
			panelGeneral.Controls.Add(textBoxBirthPlace);
			panelGeneral.Controls.Add(mmLabel33);
			panelGeneral.Controls.Add(mmLabel51);
			panelGeneral.Controls.Add(dateTimeBirthDate);
			panelGeneral.Controls.Add(textBoxAge);
			panelGeneral.Controls.Add(mmLabel31);
			panelGeneral.Controls.Add(comboBoxGender);
			panelGeneral.Controls.Add(comboBoxNationality);
			panelGeneral.Controls.Add(mmLabel5);
			panelGeneral.Controls.Add(labelCandidateNumber);
			panelGeneral.Controls.Add(lblDescriptions);
			panelGeneral.Controls.Add(textBoxCode);
			panelGeneral.Controls.Add(textBoxSurName);
			panelGeneral.Controls.Add(textBoxGivenName);
			panelGeneral.Controls.Add(labelGivenName);
			panelGeneral.Location = new System.Drawing.Point(-2, 0);
			panelGeneral.Name = "panelGeneral";
			panelGeneral.Size = new System.Drawing.Size(814, 529);
			panelGeneral.TabIndex = 0;
			mmLabel93.AutoSize = true;
			mmLabel93.BackColor = System.Drawing.Color.Transparent;
			mmLabel93.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel93.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel93.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel93.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel93.IsFieldHeader = false;
			mmLabel93.IsRequired = false;
			mmLabel93.Location = new System.Drawing.Point(13, 126);
			mmLabel93.Name = "mmLabel93";
			mmLabel93.PenWidth = 1f;
			mmLabel93.ShowBorder = false;
			mmLabel93.Size = new System.Drawing.Size(50, 13);
			mmLabel93.TabIndex = 157;
			mmLabel93.Text = "Status :";
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance25;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(13, 104);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel2.TabIndex = 155;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Designation:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxGroup.Assigned = false;
			comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGroup.CustomReportFieldName = "";
			comboBoxGroup.CustomReportKey = "";
			comboBoxGroup.CustomReportValueType = 1;
			comboBoxGroup.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGroup.DisplayLayout.Appearance = appearance27;
			comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGroup.Editable = true;
			comboBoxGroup.FilterString = "";
			comboBoxGroup.HasAllAccount = false;
			comboBoxGroup.HasCustom = false;
			comboBoxGroup.IsDataLoaded = false;
			comboBoxGroup.Location = new System.Drawing.Point(347, 124);
			comboBoxGroup.MaxDropDownItems = 12;
			comboBoxGroup.Name = "comboBoxGroup";
			comboBoxGroup.ShowInactiveItems = false;
			comboBoxGroup.ShowQuickAdd = true;
			comboBoxGroup.Size = new System.Drawing.Size(136, 20);
			comboBoxGroup.TabIndex = 6;
			comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(303, 126);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel3.TabIndex = 154;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Group:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxvisaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxvisaType.FormattingEnabled = true;
			comboBoxvisaType.Location = new System.Drawing.Point(169, 123);
			comboBoxvisaType.Name = "comboBoxvisaType";
			comboBoxvisaType.SelectedID = 0;
			comboBoxvisaType.Size = new System.Drawing.Size(124, 21);
			comboBoxvisaType.TabIndex = 5;
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.Location = new System.Drawing.Point(295, 101);
			textBoxDesignation.MaxLength = 64;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(188, 20);
			textBoxDesignation.TabIndex = 4;
			textBoxDesignation.TabStop = false;
			comboBoxDesignation.Assigned = false;
			comboBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDesignation.CustomReportFieldName = "";
			comboBoxDesignation.CustomReportKey = "";
			comboBoxDesignation.CustomReportValueType = 1;
			comboBoxDesignation.DescriptionTextBox = textBoxDesignation;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDesignation.DisplayLayout.Appearance = appearance40;
			comboBoxDesignation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDesignation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDesignation.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDesignation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxDesignation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDesignation.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxDesignation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDesignation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDesignation.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDesignation.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxDesignation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDesignation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDesignation.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDesignation.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxDesignation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDesignation.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDesignation.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxDesignation.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxDesignation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDesignation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxDesignation.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxDesignation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDesignation.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
			comboBoxDesignation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDesignation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDesignation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDesignation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDesignation.Editable = true;
			comboBoxDesignation.FilterString = "";
			comboBoxDesignation.HasAllAccount = false;
			comboBoxDesignation.HasCustom = false;
			comboBoxDesignation.IsDataLoaded = false;
			comboBoxDesignation.Location = new System.Drawing.Point(169, 101);
			comboBoxDesignation.MaxDropDownItems = 12;
			comboBoxDesignation.Name = "comboBoxDesignation";
			comboBoxDesignation.ShowInactiveItems = false;
			comboBoxDesignation.ShowQuickAdd = true;
			comboBoxDesignation.Size = new System.Drawing.Size(124, 20);
			comboBoxDesignation.TabIndex = 3;
			comboBoxDesignation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel69.AutoSize = true;
			mmLabel69.BackColor = System.Drawing.Color.Transparent;
			mmLabel69.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel69.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel69.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel69.IsFieldHeader = false;
			mmLabel69.IsRequired = false;
			mmLabel69.Location = new System.Drawing.Point(13, 82);
			mmLabel69.Name = "mmLabel69";
			mmLabel69.PenWidth = 1f;
			mmLabel69.ShowBorder = false;
			mmLabel69.Size = new System.Drawing.Size(85, 13);
			mmLabel69.TabIndex = 147;
			mmLabel69.Text = "Middle Name :";
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(751, 345);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 146;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(702, 99);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 126;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance52;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(169, 431);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(631, 89);
			textBoxNote.TabIndex = 21;
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(15, 455);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(37, 13);
			mmLabel54.TabIndex = 145;
			mmLabel54.Text = "Note :";
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(13, 34);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(80, 13);
			mmLabel53.TabIndex = 144;
			mmLabel53.Text = "Passport No :";
			textBoxPassportNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPassportNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPassportNo.BackColor = System.Drawing.Color.White;
			textBoxPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPassportNo.CustomReportFieldName = "";
			textBoxPassportNo.CustomReportKey = "";
			textBoxPassportNo.CustomReportValueType = 1;
			textBoxPassportNo.IsComboTextBox = false;
			textBoxPassportNo.IsModified = false;
			textBoxPassportNo.Location = new System.Drawing.Point(169, 34);
			textBoxPassportNo.MaxLength = 20;
			textBoxPassportNo.Name = "textBoxPassportNo";
			textBoxPassportNo.Size = new System.Drawing.Size(146, 20);
			textBoxPassportNo.TabIndex = 0;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(733, 176);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 129;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance53;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(694, 176);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 127;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance54;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(672, 42);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 143;
			pictureBoxPhoto.TabStop = false;
			textBoxPPAddress.BackColor = System.Drawing.Color.White;
			textBoxPPAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPPAddress.CustomReportFieldName = "";
			textBoxPPAddress.CustomReportKey = "";
			textBoxPPAddress.CustomReportValueType = 1;
			textBoxPPAddress.IsComboTextBox = false;
			textBoxPPAddress.IsModified = false;
			textBoxPPAddress.Location = new System.Drawing.Point(169, 370);
			textBoxPPAddress.MaxLength = 255;
			textBoxPPAddress.Multiline = true;
			textBoxPPAddress.Name = "textBoxPPAddress";
			textBoxPPAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxPPAddress.Size = new System.Drawing.Size(314, 55);
			textBoxPPAddress.TabIndex = 20;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(526, 292);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(69, 13);
			mmLabel21.TabIndex = 142;
			mmLabel21.Text = "Blood Group:";
			mmLabel21.Visible = false;
			textBoxBloodGroup.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxBloodGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBloodGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxBloodGroup.BackColor = System.Drawing.Color.White;
			textBoxBloodGroup.CustomReportFieldName = "";
			textBoxBloodGroup.CustomReportKey = "";
			textBoxBloodGroup.CustomReportValueType = 1;
			textBoxBloodGroup.IsComboTextBox = false;
			textBoxBloodGroup.IsModified = false;
			textBoxBloodGroup.Location = new System.Drawing.Point(604, 287);
			textBoxBloodGroup.MaxLength = 3;
			textBoxBloodGroup.Name = "textBoxBloodGroup";
			textBoxBloodGroup.Size = new System.Drawing.Size(72, 20);
			textBoxBloodGroup.TabIndex = 15;
			textBoxBloodGroup.Visible = false;
			comboBoxReligion.Assigned = false;
			comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReligion.CustomReportFieldName = "";
			comboBoxReligion.CustomReportKey = "";
			comboBoxReligion.CustomReportValueType = 1;
			comboBoxReligion.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReligion.DisplayLayout.Appearance = appearance55;
			comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReligion.Editable = true;
			comboBoxReligion.FilterString = "";
			comboBoxReligion.HasAllAccount = false;
			comboBoxReligion.HasCustom = false;
			comboBoxReligion.IsDataLoaded = false;
			comboBoxReligion.Location = new System.Drawing.Point(604, 311);
			comboBoxReligion.MaxDropDownItems = 12;
			comboBoxReligion.Name = "comboBoxReligion";
			comboBoxReligion.ShowInactiveItems = false;
			comboBoxReligion.ShowQuickAdd = true;
			comboBoxReligion.Size = new System.Drawing.Size(196, 20);
			comboBoxReligion.TabIndex = 17;
			comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxReligion.Visible = false;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(526, 313);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel1.TabIndex = 141;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Religion:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance67.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance67;
			comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMaritalStatus.FormattingEnabled = true;
			comboBoxMaritalStatus.Location = new System.Drawing.Point(604, 263);
			comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
			comboBoxMaritalStatus.SelectedID = 0;
			comboBoxMaritalStatus.Size = new System.Drawing.Size(117, 21);
			comboBoxMaritalStatus.TabIndex = 13;
			comboBoxMaritalStatus.Visible = false;
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(526, 268);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(77, 13);
			mmLabel47.TabIndex = 140;
			mmLabel47.Text = "Marital Status:";
			mmLabel47.Visible = false;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(13, 373);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(68, 13);
			mmLabel19.TabIndex = 139;
			mmLabel19.Text = "PP Address :";
			textBoxSpouseName.BackColor = System.Drawing.Color.White;
			textBoxSpouseName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSpouseName.CustomReportFieldName = "";
			textBoxSpouseName.CustomReportKey = "";
			textBoxSpouseName.CustomReportValueType = 1;
			textBoxSpouseName.IsComboTextBox = false;
			textBoxSpouseName.IsModified = false;
			textBoxSpouseName.IsRequired = true;
			textBoxSpouseName.Location = new System.Drawing.Point(169, 348);
			textBoxSpouseName.MaxLength = 64;
			textBoxSpouseName.Name = "textBoxSpouseName";
			textBoxSpouseName.Size = new System.Drawing.Size(314, 20);
			textBoxSpouseName.TabIndex = 19;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(13, 351);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(79, 13);
			mmLabel9.TabIndex = 138;
			mmLabel9.Text = "Spouse Name :";
			textBoxMotherName.BackColor = System.Drawing.Color.White;
			textBoxMotherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMotherName.CustomReportFieldName = "";
			textBoxMotherName.CustomReportKey = "";
			textBoxMotherName.CustomReportValueType = 1;
			textBoxMotherName.IsComboTextBox = false;
			textBoxMotherName.IsModified = false;
			textBoxMotherName.IsRequired = true;
			textBoxMotherName.Location = new System.Drawing.Point(169, 326);
			textBoxMotherName.MaxLength = 64;
			textBoxMotherName.Name = "textBoxMotherName";
			textBoxMotherName.Size = new System.Drawing.Size(314, 20);
			textBoxMotherName.TabIndex = 18;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(13, 329);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(78, 13);
			mmLabel6.TabIndex = 137;
			mmLabel6.Text = "Mother Name :";
			textBoxFatherName.BackColor = System.Drawing.Color.White;
			textBoxFatherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFatherName.CustomReportFieldName = "";
			textBoxFatherName.CustomReportKey = "";
			textBoxFatherName.CustomReportValueType = 1;
			textBoxFatherName.IsComboTextBox = false;
			textBoxFatherName.IsModified = false;
			textBoxFatherName.IsRequired = true;
			textBoxFatherName.Location = new System.Drawing.Point(169, 304);
			textBoxFatherName.MaxLength = 64;
			textBoxFatherName.Name = "textBoxFatherName";
			textBoxFatherName.Size = new System.Drawing.Size(314, 20);
			textBoxFatherName.TabIndex = 16;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(13, 307);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(76, 13);
			mmLabel4.TabIndex = 136;
			mmLabel4.Text = "Father Name :";
			dateTimePPExpiryDate.Checked = false;
			dateTimePPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimePPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePPExpiryDate.Location = new System.Drawing.Point(169, 282);
			dateTimePPExpiryDate.Name = "dateTimePPExpiryDate";
			dateTimePPExpiryDate.ShowCheckBox = true;
			dateTimePPExpiryDate.Size = new System.Drawing.Size(147, 20);
			dateTimePPExpiryDate.TabIndex = 14;
			dateTimePPExpiryDate.Value = new System.DateTime(0L);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(13, 286);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(85, 13);
			mmLabel3.TabIndex = 135;
			mmLabel3.Text = "PP Expiry Date :";
			dateTimePPIssueDate.Checked = false;
			dateTimePPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimePPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePPIssueDate.Location = new System.Drawing.Point(169, 259);
			dateTimePPIssueDate.Name = "dateTimePPIssueDate";
			dateTimePPIssueDate.ShowCheckBox = true;
			dateTimePPIssueDate.Size = new System.Drawing.Size(147, 20);
			dateTimePPIssueDate.TabIndex = 12;
			dateTimePPIssueDate.Value = new System.DateTime(0L);
			textBoxPPIssuePlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPPIssuePlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxPPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPPIssuePlace.CustomReportFieldName = "";
			textBoxPPIssuePlace.CustomReportKey = "";
			textBoxPPIssuePlace.CustomReportValueType = 1;
			textBoxPPIssuePlace.IsComboTextBox = false;
			textBoxPPIssuePlace.IsModified = false;
			textBoxPPIssuePlace.Location = new System.Drawing.Point(169, 237);
			textBoxPPIssuePlace.MaxLength = 30;
			textBoxPPIssuePlace.Name = "textBoxPPIssuePlace";
			textBoxPPIssuePlace.Size = new System.Drawing.Size(314, 20);
			textBoxPPIssuePlace.TabIndex = 11;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(13, 241);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(96, 13);
			mmLabel1.TabIndex = 133;
			mmLabel1.Text = "PP Place of Issue :";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(13, 264);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(81, 13);
			mmLabel2.TabIndex = 134;
			mmLabel2.Text = "PP Issue Date :";
			textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBirthPlace.BackColor = System.Drawing.Color.White;
			textBoxBirthPlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxBirthPlace.CustomReportFieldName = "";
			textBoxBirthPlace.CustomReportKey = "";
			textBoxBirthPlace.CustomReportValueType = 1;
			textBoxBirthPlace.IsComboTextBox = false;
			textBoxBirthPlace.IsModified = false;
			textBoxBirthPlace.Location = new System.Drawing.Point(169, 215);
			textBoxBirthPlace.MaxLength = 30;
			textBoxBirthPlace.Name = "textBoxBirthPlace";
			textBoxBirthPlace.Size = new System.Drawing.Size(314, 20);
			textBoxBirthPlace.TabIndex = 10;
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(13, 218);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(64, 13);
			mmLabel33.TabIndex = 132;
			mmLabel33.Text = "Birth Place :";
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(288, 196);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(30, 13);
			mmLabel51.TabIndex = 131;
			mmLabel51.Text = "Age:";
			dateTimeBirthDate.Checked = false;
			dateTimeBirthDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeBirthDate.Location = new System.Drawing.Point(169, 193);
			dateTimeBirthDate.Name = "dateTimeBirthDate";
			dateTimeBirthDate.ShowCheckBox = true;
			dateTimeBirthDate.Size = new System.Drawing.Size(117, 20);
			dateTimeBirthDate.TabIndex = 9;
			dateTimeBirthDate.Value = new System.DateTime(0L);
			textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAge.CustomReportFieldName = "";
			textBoxAge.CustomReportKey = "";
			textBoxAge.CustomReportValueType = 1;
			textBoxAge.IsComboTextBox = false;
			textBoxAge.IsModified = false;
			textBoxAge.Location = new System.Drawing.Point(319, 193);
			textBoxAge.MaxLength = 30;
			textBoxAge.Name = "textBoxAge";
			textBoxAge.ReadOnly = true;
			textBoxAge.Size = new System.Drawing.Size(50, 20);
			textBoxAge.TabIndex = 112;
			textBoxAge.TabStop = false;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(13, 196);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(62, 13);
			mmLabel31.TabIndex = 130;
			mmLabel31.Text = "Birth Date :";
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(169, 169);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.SelectedID = 0;
			comboBoxGender.Size = new System.Drawing.Size(95, 21);
			comboBoxGender.TabIndex = 8;
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance68;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance69;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance70;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance71.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance71.BackColor2 = System.Drawing.SystemColors.Control;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance71;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			appearance72.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance72;
			appearance73.BackColor = System.Drawing.SystemColors.Highlight;
			appearance73.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance73;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance74;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			appearance75.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance75;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance76.BackColor = System.Drawing.SystemColors.Control;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance76;
			appearance77.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance77;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance78;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance79.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance79;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(169, 147);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(196, 20);
			comboBoxNationality.TabIndex = 7;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(13, 173);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(32, 13);
			mmLabel5.TabIndex = 114;
			mmLabel5.Text = "Sex :";
			labelCandidateNumber.AutoSize = true;
			labelCandidateNumber.BackColor = System.Drawing.Color.Transparent;
			labelCandidateNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCandidateNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCandidateNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCandidateNumber.IsFieldHeader = false;
			labelCandidateNumber.IsRequired = true;
			labelCandidateNumber.Location = new System.Drawing.Point(13, 12);
			labelCandidateNumber.Name = "labelCandidateNumber";
			labelCandidateNumber.PenWidth = 1f;
			labelCandidateNumber.ShowBorder = false;
			labelCandidateNumber.Size = new System.Drawing.Size(124, 13);
			labelCandidateNumber.TabIndex = 103;
			labelCandidateNumber.Text = "VS Application Code :";
			labelCandidateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(13, 152);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(65, 13);
			lblDescriptions.TabIndex = 111;
			lblDescriptions.Text = "Nationality :";
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(169, 11);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(197, 20);
			textBoxCode.TabIndex = 124;
			textBoxCode.TabStop = false;
			textBoxSurName.BackColor = System.Drawing.Color.White;
			textBoxSurName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSurName.CustomReportFieldName = "";
			textBoxSurName.CustomReportKey = "";
			textBoxSurName.CustomReportValueType = 1;
			textBoxSurName.IsComboTextBox = false;
			textBoxSurName.IsModified = false;
			textBoxSurName.IsRequired = true;
			textBoxSurName.Location = new System.Drawing.Point(169, 79);
			textBoxSurName.MaxLength = 64;
			textBoxSurName.Name = "textBoxSurName";
			textBoxSurName.Size = new System.Drawing.Size(314, 20);
			textBoxSurName.TabIndex = 2;
			textBoxGivenName.BackColor = System.Drawing.Color.White;
			textBoxGivenName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGivenName.CustomReportFieldName = "";
			textBoxGivenName.CustomReportKey = "";
			textBoxGivenName.CustomReportValueType = 1;
			textBoxGivenName.IsComboTextBox = false;
			textBoxGivenName.IsModified = false;
			textBoxGivenName.IsRequired = true;
			textBoxGivenName.Location = new System.Drawing.Point(169, 57);
			textBoxGivenName.MaxLength = 64;
			textBoxGivenName.Name = "textBoxGivenName";
			textBoxGivenName.Size = new System.Drawing.Size(314, 20);
			textBoxGivenName.TabIndex = 1;
			labelGivenName.AutoSize = true;
			labelGivenName.BackColor = System.Drawing.Color.Transparent;
			labelGivenName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelGivenName.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelGivenName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelGivenName.IsFieldHeader = false;
			labelGivenName.IsRequired = false;
			labelGivenName.Location = new System.Drawing.Point(13, 59);
			labelGivenName.Name = "labelGivenName";
			labelGivenName.PenWidth = 1f;
			labelGivenName.ShowBorder = false;
			labelGivenName.Size = new System.Drawing.Size(73, 13);
			labelGivenName.TabIndex = 106;
			labelGivenName.Text = "First Name :";
			labelGivenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl1.Controls.Add(panelArrival);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(810, 525);
			panelArrival.Controls.Add(textBox1);
			panelArrival.Controls.Add(sponsorComboBox);
			panelArrival.Controls.Add(mmLabel92);
			panelArrival.Controls.Add(ultraTabControl1);
			panelArrival.Controls.Add(buttonMakeEmployee);
			panelArrival.Controls.Add(textBoxEmployeeNo);
			panelArrival.Controls.Add(mmLabel34);
			panelArrival.Controls.Add(dateTimeArrivedOn);
			panelArrival.Controls.Add(mmLabel27);
			panelArrival.Location = new System.Drawing.Point(0, 0);
			panelArrival.Name = "panelArrival";
			panelArrival.Size = new System.Drawing.Size(810, 524);
			panelArrival.TabIndex = 0;
			textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox1.Location = new System.Drawing.Point(271, 48);
			textBox1.MaxLength = 64;
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new System.Drawing.Size(232, 20);
			textBox1.TabIndex = 148;
			textBox1.TabStop = false;
			sponsorComboBox.Assigned = false;
			sponsorComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			sponsorComboBox.CustomReportFieldName = "";
			sponsorComboBox.CustomReportKey = "";
			sponsorComboBox.CustomReportValueType = 1;
			sponsorComboBox.DescriptionTextBox = textBox1;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			sponsorComboBox.DisplayLayout.Appearance = appearance80;
			sponsorComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			sponsorComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance81.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			sponsorComboBox.DisplayLayout.GroupByBox.Appearance = appearance81;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			sponsorComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance82;
			sponsorComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance83.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance83.BackColor2 = System.Drawing.SystemColors.Control;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			sponsorComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance83;
			sponsorComboBox.DisplayLayout.MaxColScrollRegions = 1;
			sponsorComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.ForeColor = System.Drawing.SystemColors.ControlText;
			sponsorComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance84;
			appearance85.BackColor = System.Drawing.SystemColors.Highlight;
			appearance85.ForeColor = System.Drawing.SystemColors.HighlightText;
			sponsorComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance85;
			sponsorComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			sponsorComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			sponsorComboBox.DisplayLayout.Override.CardAreaAppearance = appearance86;
			appearance87.BorderColor = System.Drawing.Color.Silver;
			appearance87.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			sponsorComboBox.DisplayLayout.Override.CellAppearance = appearance87;
			sponsorComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			sponsorComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance88.BackColor = System.Drawing.SystemColors.Control;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			sponsorComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance88;
			appearance89.TextHAlignAsString = "Left";
			sponsorComboBox.DisplayLayout.Override.HeaderAppearance = appearance89;
			sponsorComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			sponsorComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			sponsorComboBox.DisplayLayout.Override.RowAppearance = appearance90;
			sponsorComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance91.BackColor = System.Drawing.SystemColors.ControlLight;
			sponsorComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance91;
			sponsorComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			sponsorComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			sponsorComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			sponsorComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			sponsorComboBox.Editable = true;
			sponsorComboBox.FilterString = "";
			sponsorComboBox.HasAllAccount = false;
			sponsorComboBox.HasCustom = false;
			sponsorComboBox.IsDataLoaded = false;
			sponsorComboBox.Location = new System.Drawing.Point(136, 48);
			sponsorComboBox.MaxDropDownItems = 12;
			sponsorComboBox.Name = "sponsorComboBox";
			sponsorComboBox.ShowInactiveItems = false;
			sponsorComboBox.ShowQuickAdd = true;
			sponsorComboBox.Size = new System.Drawing.Size(129, 20);
			sponsorComboBox.TabIndex = 147;
			sponsorComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			sponsorComboBox.SelectedIndexChanged += new System.EventHandler(sponsorComboBox_SelectedIndexChanged);
			mmLabel92.AutoSize = true;
			mmLabel92.BackColor = System.Drawing.Color.Transparent;
			mmLabel92.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel92.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel92.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel92.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel92.IsFieldHeader = false;
			mmLabel92.IsRequired = false;
			mmLabel92.Location = new System.Drawing.Point(27, 52);
			mmLabel92.Name = "mmLabel92";
			mmLabel92.PenWidth = 1f;
			mmLabel92.ShowBorder = false;
			mmLabel92.Size = new System.Drawing.Size(83, 13);
			mmLabel92.TabIndex = 149;
			mmLabel92.Text = "Sponsor Name :";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl1.Controls.Add(ultraTabPageControl6);
			ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControl1.Location = new System.Drawing.Point(10, 134);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl1.Size = new System.Drawing.Size(790, 387);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 146;
			appearance92.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance92;
			ultraTab.TabPage = ultraTabPageControl6;
			ultraTab.Text = "&PayrollItems";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[1]
			{
				ultraTab
			});
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(788, 366);
			buttonMakeEmployee.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMakeEmployee.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonMakeEmployee.BackColor = System.Drawing.Color.DarkGray;
			buttonMakeEmployee.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMakeEmployee.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMakeEmployee.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonMakeEmployee.Enabled = false;
			buttonMakeEmployee.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMakeEmployee.Location = new System.Drawing.Point(271, 69);
			buttonMakeEmployee.Name = "buttonMakeEmployee";
			buttonMakeEmployee.Size = new System.Drawing.Size(102, 24);
			buttonMakeEmployee.TabIndex = 2;
			buttonMakeEmployee.Text = "Make Employee";
			buttonMakeEmployee.UseVisualStyleBackColor = false;
			buttonMakeEmployee.Click += new System.EventHandler(toolStripBtnMakeEmployee_Click);
			textBoxEmployeeNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeNo.CustomReportFieldName = "";
			textBoxEmployeeNo.CustomReportKey = "";
			textBoxEmployeeNo.CustomReportValueType = 1;
			textBoxEmployeeNo.IsComboTextBox = false;
			textBoxEmployeeNo.IsModified = false;
			textBoxEmployeeNo.Location = new System.Drawing.Point(136, 71);
			textBoxEmployeeNo.MaxLength = 15;
			textBoxEmployeeNo.Name = "textBoxEmployeeNo";
			textBoxEmployeeNo.ReadOnly = true;
			textBoxEmployeeNo.Size = new System.Drawing.Size(129, 20);
			textBoxEmployeeNo.TabIndex = 3;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(27, 75);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(73, 13);
			mmLabel34.TabIndex = 143;
			mmLabel34.Text = "Employee No:";
			dateTimeArrivedOn.Checked = false;
			dateTimeArrivedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeArrivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeArrivedOn.Location = new System.Drawing.Point(136, 25);
			dateTimeArrivedOn.Name = "dateTimeArrivedOn";
			dateTimeArrivedOn.ShowCheckBox = true;
			dateTimeArrivedOn.Size = new System.Drawing.Size(129, 20);
			dateTimeArrivedOn.TabIndex = 0;
			dateTimeArrivedOn.Value = new System.DateTime(0L);
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(27, 27);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(63, 13);
			mmLabel27.TabIndex = 142;
			mmLabel27.Text = "Arrived On:";
			tabPageDetails.Controls.Add(panelRecruitment);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(810, 525);
			panelRecruitment.Controls.Add(comboBoxArrivalPort);
			panelRecruitment.Controls.Add(comboBoxCategory);
			panelRecruitment.Controls.Add(mmLabel29);
			panelRecruitment.Controls.Add(mmLabel88);
			panelRecruitment.Controls.Add(mmLabel81);
			panelRecruitment.Controls.Add(mmLabel76);
			panelRecruitment.Controls.Add(textBoxLanguageName);
			panelRecruitment.Controls.Add(textBoxQualificationName);
			panelRecruitment.Controls.Add(numericExperienceAbroad);
			panelRecruitment.Controls.Add(numericExperienceLocal);
			panelRecruitment.Controls.Add(comboBoxLanguage);
			panelRecruitment.Controls.Add(comboBoxQualification);
			panelRecruitment.Controls.Add(mmLabel75);
			panelRecruitment.Controls.Add(mmLabel74);
			panelRecruitment.Controls.Add(mmLabel73);
			panelRecruitment.Controls.Add(mmLabel71);
			panelRecruitment.Controls.Add(textBoxActualDesignationName);
			panelRecruitment.Controls.Add(textBoxThroughAgentName);
			panelRecruitment.Controls.Add(comboBoxPositionActual);
			panelRecruitment.Controls.Add(mmLabel68);
			panelRecruitment.Controls.Add(comboBoxAgentThrough);
			panelRecruitment.Controls.Add(mmLabel55);
			panelRecruitment.Controls.Add(textBoxRemarks);
			panelRecruitment.Controls.Add(comboBoxSelectionStatus);
			panelRecruitment.Controls.Add(mmLabel39);
			panelRecruitment.Controls.Add(mmLabel38);
			panelRecruitment.Controls.Add(textBoxSelectedAt);
			panelRecruitment.Controls.Add(dateTimeSelectedOn);
			panelRecruitment.Controls.Add(mmLabel37);
			panelRecruitment.Controls.Add(mmLabel36);
			panelRecruitment.Location = new System.Drawing.Point(0, 0);
			panelRecruitment.Name = "panelRecruitment";
			panelRecruitment.Size = new System.Drawing.Size(810, 529);
			panelRecruitment.TabIndex = 0;
			comboBoxArrivalPort.Assigned = false;
			comboBoxArrivalPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxArrivalPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArrivalPort.CustomReportFieldName = "";
			comboBoxArrivalPort.CustomReportKey = "";
			comboBoxArrivalPort.CustomReportValueType = 1;
			comboBoxArrivalPort.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArrivalPort.DisplayLayout.Appearance = appearance93;
			comboBoxArrivalPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArrivalPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxArrivalPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArrivalPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArrivalPort.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxArrivalPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArrivalPort.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxArrivalPort.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxArrivalPort.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxArrivalPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArrivalPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			comboBoxArrivalPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArrivalPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArrivalPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArrivalPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArrivalPort.Editable = true;
			comboBoxArrivalPort.FilterString = "";
			comboBoxArrivalPort.HasAllAccount = false;
			comboBoxArrivalPort.HasCustom = false;
			comboBoxArrivalPort.IsDataLoaded = false;
			comboBoxArrivalPort.Location = new System.Drawing.Point(166, 244);
			comboBoxArrivalPort.MaxDropDownItems = 12;
			comboBoxArrivalPort.Name = "comboBoxArrivalPort";
			comboBoxArrivalPort.ShowInactiveItems = false;
			comboBoxArrivalPort.ShowQuickAdd = true;
			comboBoxArrivalPort.Size = new System.Drawing.Size(129, 20);
			comboBoxArrivalPort.TabIndex = 146;
			comboBoxArrivalPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxArrivalPort.Visible = false;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance105;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(166, 266);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.ShowQuickAdd = true;
			comboBoxCategory.Size = new System.Drawing.Size(129, 20);
			comboBoxCategory.TabIndex = 147;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Visible = false;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(15, 251);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(31, 13);
			mmLabel29.TabIndex = 149;
			mmLabel29.Text = "Port:";
			mmLabel29.Visible = false;
			mmLabel88.AutoSize = true;
			mmLabel88.BackColor = System.Drawing.Color.Transparent;
			mmLabel88.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel88.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel88.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel88.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel88.IsFieldHeader = false;
			mmLabel88.IsRequired = false;
			mmLabel88.Location = new System.Drawing.Point(15, 274);
			mmLabel88.Name = "mmLabel88";
			mmLabel88.PenWidth = 1f;
			mmLabel88.ShowBorder = false;
			mmLabel88.Size = new System.Drawing.Size(56, 13);
			mmLabel88.TabIndex = 148;
			mmLabel88.Text = "Category:";
			mmLabel88.Visible = false;
			mmLabel81.AutoSize = true;
			mmLabel81.BackColor = System.Drawing.Color.Transparent;
			mmLabel81.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel81.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel81.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel81.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel81.IsFieldHeader = false;
			mmLabel81.IsRequired = false;
			mmLabel81.Location = new System.Drawing.Point(228, 199);
			mmLabel81.Name = "mmLabel81";
			mmLabel81.PenWidth = 1f;
			mmLabel81.ShowBorder = false;
			mmLabel81.Size = new System.Drawing.Size(34, 13);
			mmLabel81.TabIndex = 135;
			mmLabel81.Text = "Years";
			mmLabel76.AutoSize = true;
			mmLabel76.BackColor = System.Drawing.Color.Transparent;
			mmLabel76.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel76.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel76.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel76.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel76.IsFieldHeader = false;
			mmLabel76.IsRequired = false;
			mmLabel76.Location = new System.Drawing.Point(227, 176);
			mmLabel76.Name = "mmLabel76";
			mmLabel76.PenWidth = 1f;
			mmLabel76.ShowBorder = false;
			mmLabel76.Size = new System.Drawing.Size(34, 13);
			mmLabel76.TabIndex = 134;
			mmLabel76.Text = "Years";
			textBoxLanguageName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLanguageName.Location = new System.Drawing.Point(291, 151);
			textBoxLanguageName.MaxLength = 64;
			textBoxLanguageName.Name = "textBoxLanguageName";
			textBoxLanguageName.ReadOnly = true;
			textBoxLanguageName.Size = new System.Drawing.Size(287, 20);
			textBoxLanguageName.TabIndex = 10;
			textBoxLanguageName.TabStop = false;
			textBoxQualificationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxQualificationName.Location = new System.Drawing.Point(291, 129);
			textBoxQualificationName.MaxLength = 64;
			textBoxQualificationName.Name = "textBoxQualificationName";
			textBoxQualificationName.ReadOnly = true;
			textBoxQualificationName.Size = new System.Drawing.Size(287, 20);
			textBoxQualificationName.TabIndex = 8;
			textBoxQualificationName.TabStop = false;
			numericExperienceAbroad.DecimalPlaces = 1;
			numericExperienceAbroad.Increment = new decimal(new int[4]
			{
				50,
				0,
				0,
				131072
			});
			numericExperienceAbroad.Location = new System.Drawing.Point(166, 195);
			numericExperienceAbroad.Name = "numericExperienceAbroad";
			numericExperienceAbroad.Size = new System.Drawing.Size(56, 20);
			numericExperienceAbroad.TabIndex = 12;
			numericExperienceLocal.DecimalPlaces = 1;
			numericExperienceLocal.Increment = new decimal(new int[4]
			{
				50,
				0,
				0,
				131072
			});
			numericExperienceLocal.Location = new System.Drawing.Point(166, 173);
			numericExperienceLocal.Name = "numericExperienceLocal";
			numericExperienceLocal.Size = new System.Drawing.Size(56, 20);
			numericExperienceLocal.TabIndex = 11;
			comboBoxLanguage.Assigned = false;
			comboBoxLanguage.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLanguage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLanguage.CustomReportFieldName = "";
			comboBoxLanguage.CustomReportKey = "";
			comboBoxLanguage.CustomReportValueType = 1;
			comboBoxLanguage.DescriptionTextBox = textBoxLanguageName;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLanguage.DisplayLayout.Appearance = appearance117;
			comboBoxLanguage.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLanguage.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLanguage.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxLanguage.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLanguage.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxLanguage.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLanguage.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLanguage.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLanguage.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxLanguage.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLanguage.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLanguage.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxLanguage.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLanguage.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxLanguage.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxLanguage.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLanguage.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxLanguage.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxLanguage.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLanguage.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			comboBoxLanguage.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLanguage.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLanguage.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLanguage.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLanguage.Editable = true;
			comboBoxLanguage.FilterString = "";
			comboBoxLanguage.HasAllAccount = false;
			comboBoxLanguage.HasCustom = false;
			comboBoxLanguage.IsDataLoaded = false;
			comboBoxLanguage.Location = new System.Drawing.Point(166, 151);
			comboBoxLanguage.MaxDropDownItems = 12;
			comboBoxLanguage.Name = "comboBoxLanguage";
			comboBoxLanguage.ShowInactiveItems = false;
			comboBoxLanguage.ShowQuickAdd = true;
			comboBoxLanguage.Size = new System.Drawing.Size(124, 20);
			comboBoxLanguage.TabIndex = 9;
			comboBoxLanguage.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxQualification.Assigned = false;
			comboBoxQualification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxQualification.CustomReportFieldName = "";
			comboBoxQualification.CustomReportKey = "";
			comboBoxQualification.CustomReportValueType = 1;
			comboBoxQualification.DescriptionTextBox = textBoxQualificationName;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxQualification.DisplayLayout.Appearance = appearance129;
			comboBoxQualification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxQualification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			comboBoxQualification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			comboBoxQualification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxQualification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxQualification.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxQualification.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			comboBoxQualification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxQualification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxQualification.DisplayLayout.Override.CellAppearance = appearance136;
			comboBoxQualification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxQualification.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			comboBoxQualification.DisplayLayout.Override.HeaderAppearance = appearance138;
			comboBoxQualification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxQualification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			comboBoxQualification.DisplayLayout.Override.RowAppearance = appearance139;
			comboBoxQualification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxQualification.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			comboBoxQualification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxQualification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxQualification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxQualification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxQualification.Editable = true;
			comboBoxQualification.FilterString = "";
			comboBoxQualification.HasAllAccount = false;
			comboBoxQualification.HasCustom = false;
			comboBoxQualification.IsDataLoaded = false;
			comboBoxQualification.Location = new System.Drawing.Point(166, 129);
			comboBoxQualification.MaxDropDownItems = 12;
			comboBoxQualification.Name = "comboBoxQualification";
			comboBoxQualification.ShowInactiveItems = false;
			comboBoxQualification.ShowQuickAdd = true;
			comboBoxQualification.Size = new System.Drawing.Size(124, 20);
			comboBoxQualification.TabIndex = 7;
			comboBoxQualification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel75.AutoSize = true;
			mmLabel75.BackColor = System.Drawing.Color.Transparent;
			mmLabel75.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel75.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel75.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel75.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel75.IsFieldHeader = false;
			mmLabel75.IsRequired = false;
			mmLabel75.Location = new System.Drawing.Point(15, 199);
			mmLabel75.Name = "mmLabel75";
			mmLabel75.PenWidth = 1f;
			mmLabel75.ShowBorder = false;
			mmLabel75.Size = new System.Drawing.Size(112, 13);
			mmLabel75.TabIndex = 131;
			mmLabel75.Text = "Experience - Abroad :";
			mmLabel74.AutoSize = true;
			mmLabel74.BackColor = System.Drawing.Color.Transparent;
			mmLabel74.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel74.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel74.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel74.IsFieldHeader = false;
			mmLabel74.IsRequired = false;
			mmLabel74.Location = new System.Drawing.Point(15, 155);
			mmLabel74.Name = "mmLabel74";
			mmLabel74.PenWidth = 1f;
			mmLabel74.ShowBorder = false;
			mmLabel74.Size = new System.Drawing.Size(61, 13);
			mmLabel74.TabIndex = 130;
			mmLabel74.Text = "Language :";
			mmLabel73.AutoSize = true;
			mmLabel73.BackColor = System.Drawing.Color.Transparent;
			mmLabel73.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel73.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel73.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel73.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel73.IsFieldHeader = false;
			mmLabel73.IsRequired = false;
			mmLabel73.Location = new System.Drawing.Point(15, 177);
			mmLabel73.Name = "mmLabel73";
			mmLabel73.PenWidth = 1f;
			mmLabel73.ShowBorder = false;
			mmLabel73.Size = new System.Drawing.Size(101, 13);
			mmLabel73.TabIndex = 129;
			mmLabel73.Text = "Experience - Local :";
			mmLabel71.AutoSize = true;
			mmLabel71.BackColor = System.Drawing.Color.Transparent;
			mmLabel71.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel71.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel71.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel71.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel71.IsFieldHeader = false;
			mmLabel71.IsRequired = false;
			mmLabel71.Location = new System.Drawing.Point(15, 132);
			mmLabel71.Name = "mmLabel71";
			mmLabel71.PenWidth = 1f;
			mmLabel71.ShowBorder = false;
			mmLabel71.Size = new System.Drawing.Size(73, 13);
			mmLabel71.TabIndex = 128;
			mmLabel71.Text = "Qualification :";
			textBoxActualDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxActualDesignationName.Location = new System.Drawing.Point(291, 107);
			textBoxActualDesignationName.MaxLength = 64;
			textBoxActualDesignationName.Name = "textBoxActualDesignationName";
			textBoxActualDesignationName.ReadOnly = true;
			textBoxActualDesignationName.Size = new System.Drawing.Size(287, 20);
			textBoxActualDesignationName.TabIndex = 6;
			textBoxActualDesignationName.TabStop = false;
			textBoxThroughAgentName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxThroughAgentName.Location = new System.Drawing.Point(291, 85);
			textBoxThroughAgentName.MaxLength = 64;
			textBoxThroughAgentName.Name = "textBoxThroughAgentName";
			textBoxThroughAgentName.ReadOnly = true;
			textBoxThroughAgentName.Size = new System.Drawing.Size(287, 20);
			textBoxThroughAgentName.TabIndex = 4;
			textBoxThroughAgentName.TabStop = false;
			comboBoxPositionActual.Assigned = false;
			comboBoxPositionActual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPositionActual.CustomReportFieldName = "";
			comboBoxPositionActual.CustomReportKey = "";
			comboBoxPositionActual.CustomReportValueType = 1;
			comboBoxPositionActual.DescriptionTextBox = textBoxActualDesignationName;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPositionActual.DisplayLayout.Appearance = appearance141;
			comboBoxPositionActual.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPositionActual.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance142.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.GroupByBox.Appearance = appearance142;
			appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionActual.DisplayLayout.GroupByBox.BandLabelAppearance = appearance143;
			comboBoxPositionActual.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance144.BackColor2 = System.Drawing.SystemColors.Control;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionActual.DisplayLayout.GroupByBox.PromptAppearance = appearance144;
			comboBoxPositionActual.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPositionActual.DisplayLayout.MaxRowScrollRegions = 1;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPositionActual.DisplayLayout.Override.ActiveCellAppearance = appearance145;
			appearance146.BackColor = System.Drawing.SystemColors.Highlight;
			appearance146.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPositionActual.DisplayLayout.Override.ActiveRowAppearance = appearance146;
			comboBoxPositionActual.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPositionActual.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.Override.CardAreaAppearance = appearance147;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			appearance148.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPositionActual.DisplayLayout.Override.CellAppearance = appearance148;
			comboBoxPositionActual.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPositionActual.DisplayLayout.Override.CellPadding = 0;
			appearance149.BackColor = System.Drawing.SystemColors.Control;
			appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance149.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance149.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.Override.GroupByRowAppearance = appearance149;
			appearance150.TextHAlignAsString = "Left";
			comboBoxPositionActual.DisplayLayout.Override.HeaderAppearance = appearance150;
			comboBoxPositionActual.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPositionActual.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.Color.Silver;
			comboBoxPositionActual.DisplayLayout.Override.RowAppearance = appearance151;
			comboBoxPositionActual.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPositionActual.DisplayLayout.Override.TemplateAddRowAppearance = appearance152;
			comboBoxPositionActual.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPositionActual.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPositionActual.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPositionActual.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPositionActual.Editable = true;
			comboBoxPositionActual.FilterString = "";
			comboBoxPositionActual.HasAllAccount = false;
			comboBoxPositionActual.HasCustom = false;
			comboBoxPositionActual.IsDataLoaded = false;
			comboBoxPositionActual.Location = new System.Drawing.Point(166, 107);
			comboBoxPositionActual.MaxDropDownItems = 12;
			comboBoxPositionActual.Name = "comboBoxPositionActual";
			comboBoxPositionActual.ShowInactiveItems = false;
			comboBoxPositionActual.ShowQuickAdd = true;
			comboBoxPositionActual.Size = new System.Drawing.Size(124, 20);
			comboBoxPositionActual.TabIndex = 5;
			comboBoxPositionActual.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel68.AutoSize = true;
			mmLabel68.BackColor = System.Drawing.Color.Transparent;
			mmLabel68.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel68.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel68.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel68.IsFieldHeader = false;
			mmLabel68.IsRequired = false;
			mmLabel68.Location = new System.Drawing.Point(15, 109);
			mmLabel68.Name = "mmLabel68";
			mmLabel68.PenWidth = 1f;
			mmLabel68.ShowBorder = false;
			mmLabel68.Size = new System.Drawing.Size(103, 13);
			mmLabel68.TabIndex = 127;
			mmLabel68.Text = "Actual Designation :";
			comboBoxAgentThrough.Assigned = false;
			comboBoxAgentThrough.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAgentThrough.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAgentThrough.CustomReportFieldName = "";
			comboBoxAgentThrough.CustomReportKey = "";
			comboBoxAgentThrough.CustomReportValueType = 1;
			comboBoxAgentThrough.DescriptionTextBox = textBoxThroughAgentName;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAgentThrough.DisplayLayout.Appearance = appearance153;
			comboBoxAgentThrough.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAgentThrough.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance154.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.Appearance = appearance154;
			appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.BandLabelAppearance = appearance155;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance156.BackColor2 = System.Drawing.SystemColors.Control;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.PromptAppearance = appearance156;
			comboBoxAgentThrough.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAgentThrough.DisplayLayout.MaxRowScrollRegions = 1;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAgentThrough.DisplayLayout.Override.ActiveCellAppearance = appearance157;
			appearance158.BackColor = System.Drawing.SystemColors.Highlight;
			appearance158.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAgentThrough.DisplayLayout.Override.ActiveRowAppearance = appearance158;
			comboBoxAgentThrough.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAgentThrough.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.Override.CardAreaAppearance = appearance159;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			appearance160.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAgentThrough.DisplayLayout.Override.CellAppearance = appearance160;
			comboBoxAgentThrough.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAgentThrough.DisplayLayout.Override.CellPadding = 0;
			appearance161.BackColor = System.Drawing.SystemColors.Control;
			appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance161.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance161.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.Override.GroupByRowAppearance = appearance161;
			appearance162.TextHAlignAsString = "Left";
			comboBoxAgentThrough.DisplayLayout.Override.HeaderAppearance = appearance162;
			comboBoxAgentThrough.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAgentThrough.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.BorderColor = System.Drawing.Color.Silver;
			comboBoxAgentThrough.DisplayLayout.Override.RowAppearance = appearance163;
			comboBoxAgentThrough.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance164.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAgentThrough.DisplayLayout.Override.TemplateAddRowAppearance = appearance164;
			comboBoxAgentThrough.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAgentThrough.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAgentThrough.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAgentThrough.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAgentThrough.Editable = true;
			comboBoxAgentThrough.FilterString = "";
			comboBoxAgentThrough.HasAllAccount = false;
			comboBoxAgentThrough.HasCustom = false;
			comboBoxAgentThrough.IsDataLoaded = false;
			comboBoxAgentThrough.Location = new System.Drawing.Point(166, 85);
			comboBoxAgentThrough.MaxDropDownItems = 12;
			comboBoxAgentThrough.MaxLength = 100;
			comboBoxAgentThrough.Name = "comboBoxAgentThrough";
			comboBoxAgentThrough.ShowInactiveItems = false;
			comboBoxAgentThrough.ShowQuickAdd = true;
			comboBoxAgentThrough.Size = new System.Drawing.Size(124, 20);
			comboBoxAgentThrough.TabIndex = 3;
			comboBoxAgentThrough.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(15, 221);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(91, 13);
			mmLabel55.TabIndex = 126;
			mmLabel55.Text = "Special Remarks :";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(166, 309);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(631, 112);
			textBoxRemarks.TabIndex = 13;
			comboBoxSelectionStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSelectionStatus.FormattingEnabled = true;
			comboBoxSelectionStatus.Location = new System.Drawing.Point(166, 18);
			comboBoxSelectionStatus.Name = "comboBoxSelectionStatus";
			comboBoxSelectionStatus.SelectedID = 0;
			comboBoxSelectionStatus.Size = new System.Drawing.Size(124, 21);
			comboBoxSelectionStatus.TabIndex = 0;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(15, 89);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(86, 13);
			mmLabel39.TabIndex = 125;
			mmLabel39.Text = "Through Agent :";
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(15, 67);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(69, 13);
			mmLabel38.TabIndex = 124;
			mmLabel38.Text = "Selected At :";
			textBoxSelectedAt.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxSelectedAt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSelectedAt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxSelectedAt.BackColor = System.Drawing.Color.White;
			textBoxSelectedAt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSelectedAt.CustomReportFieldName = "";
			textBoxSelectedAt.CustomReportKey = "";
			textBoxSelectedAt.CustomReportValueType = 1;
			textBoxSelectedAt.IsComboTextBox = false;
			textBoxSelectedAt.IsModified = false;
			textBoxSelectedAt.Location = new System.Drawing.Point(166, 63);
			textBoxSelectedAt.MaxLength = 30;
			textBoxSelectedAt.Name = "textBoxSelectedAt";
			textBoxSelectedAt.Size = new System.Drawing.Size(209, 20);
			textBoxSelectedAt.TabIndex = 2;
			dateTimeSelectedOn.Checked = false;
			dateTimeSelectedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeSelectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSelectedOn.Location = new System.Drawing.Point(166, 41);
			dateTimeSelectedOn.Name = "dateTimeSelectedOn";
			dateTimeSelectedOn.ShowCheckBox = true;
			dateTimeSelectedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeSelectedOn.TabIndex = 1;
			dateTimeSelectedOn.Value = new System.DateTime(0L);
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(15, 44);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(72, 13);
			mmLabel37.TabIndex = 123;
			mmLabel37.Text = "Selected On :";
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(15, 21);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(91, 13);
			mmLabel36.TabIndex = 122;
			mmLabel36.Text = "Selection Status :";
			ultraTabPageControl4.Controls.Add(panelVisaProcess);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(810, 525);
			panelVisaProcess.Controls.Add(panelVisaIMG);
			panelVisaProcess.Controls.Add(panelVisaMOL);
			panelVisaProcess.Enabled = false;
			panelVisaProcess.Location = new System.Drawing.Point(-2, 0);
			panelVisaProcess.Name = "panelVisaProcess";
			panelVisaProcess.Size = new System.Drawing.Size(814, 527);
			panelVisaProcess.TabIndex = 0;
			panelVisaIMG.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelVisaIMG.Controls.Add(mmLabel77);
			panelVisaIMG.Controls.Add(dateTimeVisaCopyToAgentOn);
			panelVisaIMG.Controls.Add(comboBoxVisaAppliedThroughIMG);
			panelVisaIMG.Controls.Add(dateTimeVisaExpiryDate);
			panelVisaIMG.Controls.Add(dateTimeVisaIssueDate);
			panelVisaIMG.Controls.Add(mmLabel87);
			panelVisaIMG.Controls.Add(textBoxVisaIssuePlaceIMG);
			panelVisaIMG.Controls.Add(mmLabel86);
			panelVisaIMG.Controls.Add(textBoxVisaNumber);
			panelVisaIMG.Controls.Add(mmLabel85);
			panelVisaIMG.Controls.Add(mmLabel84);
			panelVisaIMG.Controls.Add(textBoxUIDNumberIMG);
			panelVisaIMG.Controls.Add(mmLabel83);
			panelVisaIMG.Controls.Add(dateTimeVisaPostedOn);
			panelVisaIMG.Controls.Add(mmLabel26);
			panelVisaIMG.Controls.Add(dateTimeApprovedOn);
			panelVisaIMG.Controls.Add(mmLabel28);
			panelVisaIMG.Controls.Add(mmLabel32);
			panelVisaIMG.Enabled = false;
			panelVisaIMG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelVisaIMG.Location = new System.Drawing.Point(11, 275);
			panelVisaIMG.Name = "panelVisaIMG";
			panelVisaIMG.Size = new System.Drawing.Size(788, 239);
			panelVisaIMG.TabIndex = 102;
			panelVisaIMG.Text = "Visa Process (IMG)";
			mmLabel77.AutoSize = true;
			mmLabel77.BackColor = System.Drawing.Color.Transparent;
			mmLabel77.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel77.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel77.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel77.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel77.IsFieldHeader = false;
			mmLabel77.IsRequired = false;
			mmLabel77.Location = new System.Drawing.Point(8, 210);
			mmLabel77.Name = "mmLabel77";
			mmLabel77.PenWidth = 1f;
			mmLabel77.ShowBorder = false;
			mmLabel77.Size = new System.Drawing.Size(148, 13);
			mmLabel77.TabIndex = 102;
			mmLabel77.Text = "Visa Copy Sent to Agent On :";
			dateTimeVisaCopyToAgentOn.Checked = false;
			dateTimeVisaCopyToAgentOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaCopyToAgentOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaCopyToAgentOn.Location = new System.Drawing.Point(158, 209);
			dateTimeVisaCopyToAgentOn.Name = "dateTimeVisaCopyToAgentOn";
			dateTimeVisaCopyToAgentOn.ShowCheckBox = true;
			dateTimeVisaCopyToAgentOn.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaCopyToAgentOn.TabIndex = 20;
			dateTimeVisaCopyToAgentOn.Value = new System.DateTime(0L);
			comboBoxVisaAppliedThroughIMG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxVisaAppliedThroughIMG.FormattingEnabled = true;
			comboBoxVisaAppliedThroughIMG.Items.AddRange(new object[2]
			{
				"Online",
				"Manual"
			});
			comboBoxVisaAppliedThroughIMG.Location = new System.Drawing.Point(158, 32);
			comboBoxVisaAppliedThroughIMG.Name = "comboBoxVisaAppliedThroughIMG";
			comboBoxVisaAppliedThroughIMG.Size = new System.Drawing.Size(124, 21);
			comboBoxVisaAppliedThroughIMG.TabIndex = 12;
			dateTimeVisaExpiryDate.Checked = false;
			dateTimeVisaExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaExpiryDate.Location = new System.Drawing.Point(158, 165);
			dateTimeVisaExpiryDate.Name = "dateTimeVisaExpiryDate";
			dateTimeVisaExpiryDate.ShowCheckBox = true;
			dateTimeVisaExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaExpiryDate.TabIndex = 18;
			dateTimeVisaExpiryDate.Value = new System.DateTime(0L);
			dateTimeVisaIssueDate.Checked = false;
			dateTimeVisaIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaIssueDate.Location = new System.Drawing.Point(158, 143);
			dateTimeVisaIssueDate.Name = "dateTimeVisaIssueDate";
			dateTimeVisaIssueDate.ShowCheckBox = true;
			dateTimeVisaIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaIssueDate.TabIndex = 17;
			dateTimeVisaIssueDate.Value = new System.DateTime(0L);
			mmLabel87.AutoSize = true;
			mmLabel87.BackColor = System.Drawing.Color.Transparent;
			mmLabel87.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel87.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel87.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel87.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel87.IsFieldHeader = false;
			mmLabel87.IsRequired = false;
			mmLabel87.Location = new System.Drawing.Point(7, 150);
			mmLabel87.Name = "mmLabel87";
			mmLabel87.PenWidth = 1f;
			mmLabel87.ShowBorder = false;
			mmLabel87.Size = new System.Drawing.Size(88, 13);
			mmLabel87.TabIndex = 107;
			mmLabel87.Text = "Visa Issue Date :";
			textBoxVisaIssuePlaceIMG.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxVisaIssuePlaceIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxVisaIssuePlaceIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxVisaIssuePlaceIMG.BackColor = System.Drawing.Color.White;
			textBoxVisaIssuePlaceIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVisaIssuePlaceIMG.CustomReportFieldName = "";
			textBoxVisaIssuePlaceIMG.CustomReportKey = "";
			textBoxVisaIssuePlaceIMG.CustomReportValueType = 1;
			textBoxVisaIssuePlaceIMG.IsComboTextBox = false;
			textBoxVisaIssuePlaceIMG.IsModified = false;
			textBoxVisaIssuePlaceIMG.Location = new System.Drawing.Point(158, 99);
			textBoxVisaIssuePlaceIMG.MaxLength = 30;
			textBoxVisaIssuePlaceIMG.Name = "textBoxVisaIssuePlaceIMG";
			textBoxVisaIssuePlaceIMG.Size = new System.Drawing.Size(414, 20);
			textBoxVisaIssuePlaceIMG.TabIndex = 15;
			mmLabel86.AutoSize = true;
			mmLabel86.BackColor = System.Drawing.Color.Transparent;
			mmLabel86.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel86.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel86.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel86.IsFieldHeader = false;
			mmLabel86.IsRequired = false;
			mmLabel86.Location = new System.Drawing.Point(8, 190);
			mmLabel86.Name = "mmLabel86";
			mmLabel86.PenWidth = 1f;
			mmLabel86.ShowBorder = false;
			mmLabel86.Size = new System.Drawing.Size(48, 13);
			mmLabel86.TabIndex = 106;
			mmLabel86.Text = "UID No :";
			textBoxVisaNumber.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxVisaNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxVisaNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxVisaNumber.BackColor = System.Drawing.Color.White;
			textBoxVisaNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVisaNumber.CustomReportFieldName = "";
			textBoxVisaNumber.CustomReportKey = "";
			textBoxVisaNumber.CustomReportValueType = 1;
			textBoxVisaNumber.IsComboTextBox = false;
			textBoxVisaNumber.IsModified = false;
			textBoxVisaNumber.Location = new System.Drawing.Point(158, 121);
			textBoxVisaNumber.MaxLength = 30;
			textBoxVisaNumber.Name = "textBoxVisaNumber";
			textBoxVisaNumber.Size = new System.Drawing.Size(205, 20);
			textBoxVisaNumber.TabIndex = 16;
			mmLabel85.AutoSize = true;
			mmLabel85.BackColor = System.Drawing.Color.Transparent;
			mmLabel85.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel85.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel85.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel85.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel85.IsFieldHeader = false;
			mmLabel85.IsRequired = false;
			mmLabel85.Location = new System.Drawing.Point(8, 128);
			mmLabel85.Name = "mmLabel85";
			mmLabel85.PenWidth = 1f;
			mmLabel85.ShowBorder = false;
			mmLabel85.Size = new System.Drawing.Size(49, 13);
			mmLabel85.TabIndex = 104;
			mmLabel85.Text = "Visa No :";
			mmLabel84.AutoSize = true;
			mmLabel84.BackColor = System.Drawing.Color.Transparent;
			mmLabel84.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel84.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel84.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel84.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel84.IsFieldHeader = false;
			mmLabel84.IsRequired = false;
			mmLabel84.Location = new System.Drawing.Point(8, 170);
			mmLabel84.Name = "mmLabel84";
			mmLabel84.PenWidth = 1f;
			mmLabel84.ShowBorder = false;
			mmLabel84.Size = new System.Drawing.Size(92, 13);
			mmLabel84.TabIndex = 103;
			mmLabel84.Text = "Visa Expiry Date :";
			textBoxUIDNumberIMG.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxUIDNumberIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxUIDNumberIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxUIDNumberIMG.BackColor = System.Drawing.Color.White;
			textBoxUIDNumberIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxUIDNumberIMG.CustomReportFieldName = "";
			textBoxUIDNumberIMG.CustomReportKey = "";
			textBoxUIDNumberIMG.CustomReportValueType = 1;
			textBoxUIDNumberIMG.IsComboTextBox = false;
			textBoxUIDNumberIMG.IsModified = false;
			textBoxUIDNumberIMG.Location = new System.Drawing.Point(158, 187);
			textBoxUIDNumberIMG.MaxLength = 30;
			textBoxUIDNumberIMG.Name = "textBoxUIDNumberIMG";
			textBoxUIDNumberIMG.Size = new System.Drawing.Size(205, 20);
			textBoxUIDNumberIMG.TabIndex = 19;
			mmLabel83.AutoSize = true;
			mmLabel83.BackColor = System.Drawing.Color.Transparent;
			mmLabel83.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel83.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel83.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel83.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel83.IsFieldHeader = false;
			mmLabel83.IsRequired = false;
			mmLabel83.Location = new System.Drawing.Point(7, 106);
			mmLabel83.Name = "mmLabel83";
			mmLabel83.PenWidth = 1f;
			mmLabel83.ShowBorder = false;
			mmLabel83.Size = new System.Drawing.Size(90, 13);
			mmLabel83.TabIndex = 102;
			mmLabel83.Text = "Visa Issue Place :";
			dateTimeVisaPostedOn.Checked = false;
			dateTimeVisaPostedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaPostedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaPostedOn.Location = new System.Drawing.Point(158, 55);
			dateTimeVisaPostedOn.Name = "dateTimeVisaPostedOn";
			dateTimeVisaPostedOn.ShowCheckBox = true;
			dateTimeVisaPostedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaPostedOn.TabIndex = 13;
			dateTimeVisaPostedOn.Value = new System.DateTime(0L);
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(7, 58);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(86, 13);
			mmLabel26.TabIndex = 75;
			mmLabel26.Text = "Visa Posted On :";
			dateTimeApprovedOn.Checked = false;
			dateTimeApprovedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovedOn.Location = new System.Drawing.Point(158, 77);
			dateTimeApprovedOn.Name = "dateTimeApprovedOn";
			dateTimeApprovedOn.ShowCheckBox = true;
			dateTimeApprovedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovedOn.TabIndex = 14;
			dateTimeApprovedOn.Value = new System.DateTime(0L);
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(7, 83);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(100, 13);
			mmLabel28.TabIndex = 71;
			mmLabel28.Text = "Visa Approved On :";
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(7, 35);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(114, 13);
			mmLabel32.TabIndex = 10;
			mmLabel32.Text = "Visa Applied Through :";
			panelVisaMOL.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelVisaMOL.Controls.Add(textBoxVisaDesignationName);
			panelVisaMOL.Controls.Add(comboBoxPositionVisa);
			panelVisaMOL.Controls.Add(mmLabel67);
			panelVisaMOL.Controls.Add(textBoxSponsorName);
			panelVisaMOL.Controls.Add(comboBoxSponsor);
			panelVisaMOL.Controls.Add(mmLabel70);
			panelVisaMOL.Controls.Add(comboBoxBGTypeMOL);
			panelVisaMOL.Controls.Add(mmLabel82);
			panelVisaMOL.Controls.Add(dateTimeApprovalFeePaidOnMOL);
			panelVisaMOL.Controls.Add(textBoxTempWPNo);
			panelVisaMOL.Controls.Add(mmLabel80);
			panelVisaMOL.Controls.Add(dateTimeApprovalValidTillMOL);
			panelVisaMOL.Controls.Add(mmLabel79);
			panelVisaMOL.Controls.Add(dateTimeBGPaidOnMOL);
			panelVisaMOL.Controls.Add(mmLabel25);
			panelVisaMOL.Controls.Add(mmLabel24);
			panelVisaMOL.Controls.Add(dateTimeApprovalDateMOL);
			panelVisaMOL.Controls.Add(mmLabel22);
			panelVisaMOL.Controls.Add(textBoxMOLMBNo);
			panelVisaMOL.Controls.Add(mmLabel7);
			panelVisaMOL.Controls.Add(dateTimeApplTypingDateMOL);
			panelVisaMOL.Controls.Add(mmLabel30);
			panelVisaMOL.Enabled = false;
			panelVisaMOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelVisaMOL.Location = new System.Drawing.Point(11, 13);
			panelVisaMOL.Name = "panelVisaMOL";
			panelVisaMOL.Size = new System.Drawing.Size(788, 250);
			panelVisaMOL.TabIndex = 101;
			panelVisaMOL.Text = "Visa Process (MOL)";
			textBoxVisaDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVisaDesignationName.Location = new System.Drawing.Point(283, 95);
			textBoxVisaDesignationName.MaxLength = 64;
			textBoxVisaDesignationName.Name = "textBoxVisaDesignationName";
			textBoxVisaDesignationName.ReadOnly = true;
			textBoxVisaDesignationName.Size = new System.Drawing.Size(287, 20);
			textBoxVisaDesignationName.TabIndex = 5;
			textBoxVisaDesignationName.TabStop = false;
			comboBoxPositionVisa.Assigned = false;
			comboBoxPositionVisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPositionVisa.CustomReportFieldName = "";
			comboBoxPositionVisa.CustomReportKey = "";
			comboBoxPositionVisa.CustomReportValueType = 1;
			comboBoxPositionVisa.DescriptionTextBox = textBoxVisaDesignationName;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			appearance165.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPositionVisa.DisplayLayout.Appearance = appearance165;
			comboBoxPositionVisa.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPositionVisa.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance166.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance166.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance166.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.Appearance = appearance166;
			appearance167.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.BandLabelAppearance = appearance167;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance168.BackColor2 = System.Drawing.SystemColors.Control;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.PromptAppearance = appearance168;
			comboBoxPositionVisa.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPositionVisa.DisplayLayout.MaxRowScrollRegions = 1;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPositionVisa.DisplayLayout.Override.ActiveCellAppearance = appearance169;
			appearance170.BackColor = System.Drawing.SystemColors.Highlight;
			appearance170.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPositionVisa.DisplayLayout.Override.ActiveRowAppearance = appearance170;
			comboBoxPositionVisa.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPositionVisa.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.Override.CardAreaAppearance = appearance171;
			appearance172.BorderColor = System.Drawing.Color.Silver;
			appearance172.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPositionVisa.DisplayLayout.Override.CellAppearance = appearance172;
			comboBoxPositionVisa.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPositionVisa.DisplayLayout.Override.CellPadding = 0;
			appearance173.BackColor = System.Drawing.SystemColors.Control;
			appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance173.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance173.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.Override.GroupByRowAppearance = appearance173;
			appearance174.TextHAlignAsString = "Left";
			comboBoxPositionVisa.DisplayLayout.Override.HeaderAppearance = appearance174;
			comboBoxPositionVisa.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPositionVisa.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.BorderColor = System.Drawing.Color.Silver;
			comboBoxPositionVisa.DisplayLayout.Override.RowAppearance = appearance175;
			comboBoxPositionVisa.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance176.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPositionVisa.DisplayLayout.Override.TemplateAddRowAppearance = appearance176;
			comboBoxPositionVisa.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPositionVisa.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPositionVisa.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPositionVisa.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPositionVisa.Editable = true;
			comboBoxPositionVisa.FilterString = "";
			comboBoxPositionVisa.HasAllAccount = false;
			comboBoxPositionVisa.HasCustom = false;
			comboBoxPositionVisa.IsDataLoaded = false;
			comboBoxPositionVisa.Location = new System.Drawing.Point(158, 95);
			comboBoxPositionVisa.MaxDropDownItems = 12;
			comboBoxPositionVisa.Name = "comboBoxPositionVisa";
			comboBoxPositionVisa.ShowInactiveItems = false;
			comboBoxPositionVisa.ShowQuickAdd = true;
			comboBoxPositionVisa.Size = new System.Drawing.Size(124, 20);
			comboBoxPositionVisa.TabIndex = 4;
			comboBoxPositionVisa.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel67.AutoSize = true;
			mmLabel67.BackColor = System.Drawing.Color.Transparent;
			mmLabel67.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel67.IsFieldHeader = false;
			mmLabel67.IsRequired = false;
			mmLabel67.Location = new System.Drawing.Point(7, 100);
			mmLabel67.Name = "mmLabel67";
			mmLabel67.PenWidth = 1f;
			mmLabel67.ShowBorder = false;
			mmLabel67.Size = new System.Drawing.Size(92, 13);
			mmLabel67.TabIndex = 107;
			mmLabel67.Text = "Visa Designation :";
			textBoxSponsorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSponsorName.Location = new System.Drawing.Point(283, 73);
			textBoxSponsorName.MaxLength = 64;
			textBoxSponsorName.Name = "textBoxSponsorName";
			textBoxSponsorName.ReadOnly = true;
			textBoxSponsorName.Size = new System.Drawing.Size(287, 20);
			textBoxSponsorName.TabIndex = 3;
			textBoxSponsorName.TabStop = false;
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = textBoxSponsorName;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			appearance177.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance177;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance178.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance178.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance178.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance178;
			appearance179.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance179;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance180.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance180.BackColor2 = System.Drawing.SystemColors.Control;
			appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance180.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance180;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance181;
			appearance182.BackColor = System.Drawing.SystemColors.Highlight;
			appearance182.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance182;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance183;
			appearance184.BorderColor = System.Drawing.Color.Silver;
			appearance184.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance184;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance185.BackColor = System.Drawing.SystemColors.Control;
			appearance185.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance185.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance185.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance185.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance185;
			appearance186.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance186;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			appearance187.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance187;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance188.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance188;
			comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSponsor.Editable = true;
			comboBoxSponsor.FilterString = "";
			comboBoxSponsor.HasAllAccount = false;
			comboBoxSponsor.HasCustom = false;
			comboBoxSponsor.IsDataLoaded = false;
			comboBoxSponsor.Location = new System.Drawing.Point(158, 73);
			comboBoxSponsor.MaxDropDownItems = 12;
			comboBoxSponsor.Name = "comboBoxSponsor";
			comboBoxSponsor.ShowInactiveItems = false;
			comboBoxSponsor.ShowQuickAdd = true;
			comboBoxSponsor.Size = new System.Drawing.Size(124, 20);
			comboBoxSponsor.TabIndex = 2;
			comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel70.AutoSize = true;
			mmLabel70.BackColor = System.Drawing.Color.Transparent;
			mmLabel70.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel70.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel70.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel70.IsFieldHeader = false;
			mmLabel70.IsRequired = false;
			mmLabel70.Location = new System.Drawing.Point(8, 78);
			mmLabel70.Name = "mmLabel70";
			mmLabel70.PenWidth = 1f;
			mmLabel70.ShowBorder = false;
			mmLabel70.Size = new System.Drawing.Size(83, 13);
			mmLabel70.TabIndex = 86;
			mmLabel70.Text = "Sponsor Name :";
			comboBoxBGTypeMOL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxBGTypeMOL.FormattingEnabled = true;
			comboBoxBGTypeMOL.Items.AddRange(new object[2]
			{
				"Fixed",
				"Individual"
			});
			comboBoxBGTypeMOL.Location = new System.Drawing.Point(158, 228);
			comboBoxBGTypeMOL.Name = "comboBoxBGTypeMOL";
			comboBoxBGTypeMOL.Size = new System.Drawing.Size(124, 21);
			comboBoxBGTypeMOL.TabIndex = 11;
			mmLabel82.AutoSize = true;
			mmLabel82.BackColor = System.Drawing.Color.Transparent;
			mmLabel82.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel82.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel82.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel82.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel82.IsFieldHeader = false;
			mmLabel82.IsRequired = false;
			mmLabel82.Location = new System.Drawing.Point(8, 231);
			mmLabel82.Name = "mmLabel82";
			mmLabel82.PenWidth = 1f;
			mmLabel82.ShowBorder = false;
			mmLabel82.Size = new System.Drawing.Size(58, 13);
			mmLabel82.TabIndex = 84;
			mmLabel82.Text = "B/G Type :";
			dateTimeApprovalFeePaidOnMOL.Checked = false;
			dateTimeApprovalFeePaidOnMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovalFeePaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalFeePaidOnMOL.Location = new System.Drawing.Point(158, 183);
			dateTimeApprovalFeePaidOnMOL.Name = "dateTimeApprovalFeePaidOnMOL";
			dateTimeApprovalFeePaidOnMOL.ShowCheckBox = true;
			dateTimeApprovalFeePaidOnMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalFeePaidOnMOL.TabIndex = 9;
			dateTimeApprovalFeePaidOnMOL.Value = new System.DateTime(0L);
			textBoxTempWPNo.BackColor = System.Drawing.Color.White;
			textBoxTempWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTempWPNo.CustomReportFieldName = "";
			textBoxTempWPNo.CustomReportKey = "";
			textBoxTempWPNo.CustomReportValueType = 1;
			textBoxTempWPNo.IsComboTextBox = false;
			textBoxTempWPNo.IsModified = false;
			textBoxTempWPNo.Location = new System.Drawing.Point(158, 161);
			textBoxTempWPNo.MaxLength = 30;
			textBoxTempWPNo.Name = "textBoxTempWPNo";
			textBoxTempWPNo.Size = new System.Drawing.Size(124, 20);
			textBoxTempWPNo.TabIndex = 8;
			mmLabel80.AutoSize = true;
			mmLabel80.BackColor = System.Drawing.Color.Transparent;
			mmLabel80.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel80.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel80.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel80.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel80.IsFieldHeader = false;
			mmLabel80.IsRequired = false;
			mmLabel80.Location = new System.Drawing.Point(8, 168);
			mmLabel80.Name = "mmLabel80";
			mmLabel80.PenWidth = 1f;
			mmLabel80.ShowBorder = false;
			mmLabel80.Size = new System.Drawing.Size(75, 13);
			mmLabel80.TabIndex = 80;
			mmLabel80.Text = "Temp WP No :";
			dateTimeApprovalValidTillMOL.Checked = false;
			dateTimeApprovalValidTillMOL.CustomFormat = "dd-MMM-yyyy";
			dateTimeApprovalValidTillMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalValidTillMOL.Location = new System.Drawing.Point(158, 139);
			dateTimeApprovalValidTillMOL.Name = "dateTimeApprovalValidTillMOL";
			dateTimeApprovalValidTillMOL.ShowCheckBox = true;
			dateTimeApprovalValidTillMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalValidTillMOL.TabIndex = 7;
			dateTimeApprovalValidTillMOL.Value = new System.DateTime(0L);
			mmLabel79.AutoSize = true;
			mmLabel79.BackColor = System.Drawing.Color.Transparent;
			mmLabel79.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel79.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel79.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel79.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel79.IsFieldHeader = false;
			mmLabel79.IsRequired = false;
			mmLabel79.Location = new System.Drawing.Point(8, 145);
			mmLabel79.Name = "mmLabel79";
			mmLabel79.PenWidth = 1f;
			mmLabel79.ShowBorder = false;
			mmLabel79.Size = new System.Drawing.Size(97, 13);
			mmLabel79.TabIndex = 78;
			mmLabel79.Text = "Approval Valid Till :";
			dateTimeBGPaidOnMOL.Checked = false;
			dateTimeBGPaidOnMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeBGPaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeBGPaidOnMOL.Location = new System.Drawing.Point(158, 205);
			dateTimeBGPaidOnMOL.Name = "dateTimeBGPaidOnMOL";
			dateTimeBGPaidOnMOL.ShowCheckBox = true;
			dateTimeBGPaidOnMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeBGPaidOnMOL.TabIndex = 10;
			dateTimeBGPaidOnMOL.Value = new System.DateTime(0L);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(8, 211);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(71, 13);
			mmLabel25.TabIndex = 75;
			mmLabel25.Text = "B/G Paid On :";
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(8, 189);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(118, 13);
			mmLabel24.TabIndex = 73;
			mmLabel24.Text = "Approval Fee Paid On :";
			dateTimeApprovalDateMOL.Checked = false;
			dateTimeApprovalDateMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovalDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalDateMOL.Location = new System.Drawing.Point(158, 117);
			dateTimeApprovalDateMOL.Name = "dateTimeApprovalDateMOL";
			dateTimeApprovalDateMOL.ShowCheckBox = true;
			dateTimeApprovalDateMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalDateMOL.TabIndex = 6;
			dateTimeApprovalDateMOL.Value = new System.DateTime(0L);
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(8, 122);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(107, 13);
			mmLabel22.TabIndex = 71;
			mmLabel22.Text = "Approval Date MOL :";
			textBoxMOLMBNo.BackColor = System.Drawing.Color.White;
			textBoxMOLMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMOLMBNo.CustomReportFieldName = "";
			textBoxMOLMBNo.CustomReportKey = "";
			textBoxMOLMBNo.CustomReportValueType = 1;
			textBoxMOLMBNo.IsComboTextBox = false;
			textBoxMOLMBNo.IsModified = false;
			textBoxMOLMBNo.Location = new System.Drawing.Point(158, 50);
			textBoxMOLMBNo.MaxLength = 30;
			textBoxMOLMBNo.Name = "textBoxMOLMBNo";
			textBoxMOLMBNo.Size = new System.Drawing.Size(205, 20);
			textBoxMOLMBNo.TabIndex = 1;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(8, 57);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(93, 13);
			mmLabel7.TabIndex = 69;
			mmLabel7.Text = "MB Ref No - Visa :";
			dateTimeApplTypingDateMOL.Checked = false;
			dateTimeApplTypingDateMOL.CustomFormat = "dd-MMM-yyyy";
			dateTimeApplTypingDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplTypingDateMOL.Location = new System.Drawing.Point(158, 29);
			dateTimeApplTypingDateMOL.Name = "dateTimeApplTypingDateMOL";
			dateTimeApplTypingDateMOL.ShowCheckBox = true;
			dateTimeApplTypingDateMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApplTypingDateMOL.TabIndex = 0;
			dateTimeApplTypingDateMOL.Value = new System.DateTime(0L);
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(8, 33);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(100, 13);
			mmLabel30.TabIndex = 10;
			mmLabel30.Text = "Appl. Typing Date :";
			ultraTabPageControl2.Controls.Add(panelMedicalEmirates);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(810, 525);
			panelMedicalEmirates.Controls.Add(panelEmirates);
			panelMedicalEmirates.Controls.Add(panelMedicalDetail);
			panelMedicalEmirates.Controls.Add(textBoxMedicalNote);
			panelMedicalEmirates.Controls.Add(mmLabel78);
			panelMedicalEmirates.Enabled = false;
			panelMedicalEmirates.Location = new System.Drawing.Point(-2, 0);
			panelMedicalEmirates.Name = "panelMedicalEmirates";
			panelMedicalEmirates.Size = new System.Drawing.Size(814, 529);
			panelMedicalEmirates.TabIndex = 0;
			panelEmirates.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelEmirates.Controls.Add(dateTimeValidityEID);
			panelEmirates.Controls.Add(dateTimeCollectedOnEID);
			panelEmirates.Controls.Add(mmLabel42);
			panelEmirates.Controls.Add(mmLabel43);
			panelEmirates.Controls.Add(dateTimeAttendedDateEID);
			panelEmirates.Controls.Add(mmLabel44);
			panelEmirates.Controls.Add(textBoxNationalID);
			panelEmirates.Controls.Add(mmLabel45);
			panelEmirates.Controls.Add(dateTimeApplTypingDateEID);
			panelEmirates.Controls.Add(mmLabel46);
			panelEmirates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelEmirates.Location = new System.Drawing.Point(12, 179);
			panelEmirates.Name = "panelEmirates";
			panelEmirates.Size = new System.Drawing.Size(788, 155);
			panelEmirates.TabIndex = 68;
			panelEmirates.Text = "Emirates ID Details";
			dateTimeValidityEID.Checked = false;
			dateTimeValidityEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeValidityEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeValidityEID.Location = new System.Drawing.Point(137, 122);
			dateTimeValidityEID.Name = "dateTimeValidityEID";
			dateTimeValidityEID.ShowCheckBox = true;
			dateTimeValidityEID.Size = new System.Drawing.Size(124, 20);
			dateTimeValidityEID.TabIndex = 9;
			dateTimeValidityEID.Value = new System.DateTime(0L);
			dateTimeCollectedOnEID.Checked = false;
			dateTimeCollectedOnEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeCollectedOnEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeCollectedOnEID.Location = new System.Drawing.Point(137, 78);
			dateTimeCollectedOnEID.Name = "dateTimeCollectedOnEID";
			dateTimeCollectedOnEID.ShowCheckBox = true;
			dateTimeCollectedOnEID.Size = new System.Drawing.Size(124, 20);
			dateTimeCollectedOnEID.TabIndex = 7;
			dateTimeCollectedOnEID.Value = new System.DateTime(0L);
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(20, 84);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(89, 13);
			mmLabel42.TabIndex = 75;
			mmLabel42.Text = "ID Collected On :";
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(20, 127);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(62, 13);
			mmLabel43.TabIndex = 73;
			mmLabel43.Text = "ID Validity :";
			dateTimeAttendedDateEID.Checked = false;
			dateTimeAttendedDateEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeAttendedDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAttendedDateEID.Location = new System.Drawing.Point(137, 56);
			dateTimeAttendedDateEID.Name = "dateTimeAttendedDateEID";
			dateTimeAttendedDateEID.ShowCheckBox = true;
			dateTimeAttendedDateEID.Size = new System.Drawing.Size(124, 20);
			dateTimeAttendedDateEID.TabIndex = 6;
			dateTimeAttendedDateEID.Value = new System.DateTime(0L);
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(21, 62);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(90, 13);
			mmLabel44.TabIndex = 71;
			mmLabel44.Text = "Attended for ID :";
			textBoxNationalID.BackColor = System.Drawing.Color.White;
			textBoxNationalID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxNationalID.CustomReportFieldName = "";
			textBoxNationalID.CustomReportKey = "";
			textBoxNationalID.CustomReportValueType = 1;
			textBoxNationalID.IsComboTextBox = false;
			textBoxNationalID.IsModified = false;
			textBoxNationalID.Location = new System.Drawing.Point(137, 100);
			textBoxNationalID.MaxLength = 50;
			textBoxNationalID.Name = "textBoxNationalID";
			textBoxNationalID.Size = new System.Drawing.Size(199, 20);
			textBoxNationalID.TabIndex = 8;
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(20, 105);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(65, 13);
			mmLabel45.TabIndex = 69;
			mmLabel45.Text = "ID Number :";
			dateTimeApplTypingDateEID.Checked = false;
			dateTimeApplTypingDateEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplTypingDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplTypingDateEID.Location = new System.Drawing.Point(137, 33);
			dateTimeApplTypingDateEID.Name = "dateTimeApplTypingDateEID";
			dateTimeApplTypingDateEID.ShowCheckBox = true;
			dateTimeApplTypingDateEID.Size = new System.Drawing.Size(124, 20);
			dateTimeApplTypingDateEID.TabIndex = 5;
			dateTimeApplTypingDateEID.Value = new System.DateTime(0L);
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(21, 39);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(89, 13);
			mmLabel46.TabIndex = 10;
			mmLabel46.Text = "Appl. Typed On :";
			panelMedicalDetail.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelMedicalDetail.Controls.Add(textBoxHealthCardNo);
			panelMedicalDetail.Controls.Add(mmLabel72);
			panelMedicalDetail.Controls.Add(comboBoxMedicalResult);
			panelMedicalDetail.Controls.Add(mmLabel89);
			panelMedicalDetail.Controls.Add(dateTimeMedicalCollectedOn);
			panelMedicalDetail.Controls.Add(mmLabel41);
			panelMedicalDetail.Controls.Add(dateTimeMedicalAttendedOn);
			panelMedicalDetail.Controls.Add(mmLabel40);
			panelMedicalDetail.Controls.Add(dateTimeMedicalTypingOn);
			panelMedicalDetail.Controls.Add(mmLabel35);
			panelMedicalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelMedicalDetail.Location = new System.Drawing.Point(12, 21);
			panelMedicalDetail.Name = "panelMedicalDetail";
			panelMedicalDetail.Size = new System.Drawing.Size(788, 146);
			panelMedicalDetail.TabIndex = 67;
			panelMedicalDetail.Text = "Medical Details";
			textBoxHealthCardNo.BackColor = System.Drawing.Color.White;
			textBoxHealthCardNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxHealthCardNo.CustomReportFieldName = "";
			textBoxHealthCardNo.CustomReportKey = "";
			textBoxHealthCardNo.CustomReportValueType = 1;
			textBoxHealthCardNo.IsComboTextBox = false;
			textBoxHealthCardNo.IsModified = false;
			textBoxHealthCardNo.Location = new System.Drawing.Point(137, 32);
			textBoxHealthCardNo.MaxLength = 30;
			textBoxHealthCardNo.Name = "textBoxHealthCardNo";
			textBoxHealthCardNo.Size = new System.Drawing.Size(205, 20);
			textBoxHealthCardNo.TabIndex = 0;
			mmLabel72.AutoSize = true;
			mmLabel72.BackColor = System.Drawing.Color.Transparent;
			mmLabel72.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel72.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel72.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel72.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel72.IsFieldHeader = false;
			mmLabel72.IsRequired = false;
			mmLabel72.Location = new System.Drawing.Point(21, 35);
			mmLabel72.Name = "mmLabel72";
			mmLabel72.PenWidth = 1f;
			mmLabel72.ShowBorder = false;
			mmLabel72.Size = new System.Drawing.Size(87, 13);
			mmLabel72.TabIndex = 138;
			mmLabel72.Text = "Health Card No :";
			comboBoxMedicalResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMedicalResult.FormattingEnabled = true;
			comboBoxMedicalResult.Items.AddRange(new object[2]
			{
				"Fit",
				"Unfit"
			});
			comboBoxMedicalResult.Location = new System.Drawing.Point(137, 119);
			comboBoxMedicalResult.Name = "comboBoxMedicalResult";
			comboBoxMedicalResult.Size = new System.Drawing.Size(124, 21);
			comboBoxMedicalResult.TabIndex = 4;
			mmLabel89.AutoSize = true;
			mmLabel89.BackColor = System.Drawing.Color.Transparent;
			mmLabel89.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel89.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel89.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel89.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel89.IsFieldHeader = false;
			mmLabel89.IsRequired = false;
			mmLabel89.Location = new System.Drawing.Point(21, 125);
			mmLabel89.Name = "mmLabel89";
			mmLabel89.PenWidth = 1f;
			mmLabel89.ShowBorder = false;
			mmLabel89.Size = new System.Drawing.Size(82, 13);
			mmLabel89.TabIndex = 73;
			mmLabel89.Text = "Medical Result :";
			dateTimeMedicalCollectedOn.Checked = false;
			dateTimeMedicalCollectedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalCollectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalCollectedOn.Location = new System.Drawing.Point(137, 97);
			dateTimeMedicalCollectedOn.Name = "dateTimeMedicalCollectedOn";
			dateTimeMedicalCollectedOn.ShowCheckBox = true;
			dateTimeMedicalCollectedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalCollectedOn.TabIndex = 3;
			dateTimeMedicalCollectedOn.Value = new System.DateTime(0L);
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(21, 103);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(113, 13);
			mmLabel41.TabIndex = 72;
			mmLabel41.Text = "Medical Collected On :";
			dateTimeMedicalAttendedOn.Checked = false;
			dateTimeMedicalAttendedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalAttendedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalAttendedOn.Location = new System.Drawing.Point(137, 75);
			dateTimeMedicalAttendedOn.Name = "dateTimeMedicalAttendedOn";
			dateTimeMedicalAttendedOn.ShowCheckBox = true;
			dateTimeMedicalAttendedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalAttendedOn.TabIndex = 2;
			dateTimeMedicalAttendedOn.Value = new System.DateTime(0L);
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(20, 81);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(114, 13);
			mmLabel40.TabIndex = 70;
			mmLabel40.Text = "Medical Attended On :";
			dateTimeMedicalTypingOn.Checked = false;
			dateTimeMedicalTypingOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalTypingOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalTypingOn.Location = new System.Drawing.Point(137, 53);
			dateTimeMedicalTypingOn.Name = "dateTimeMedicalTypingOn";
			dateTimeMedicalTypingOn.ShowCheckBox = true;
			dateTimeMedicalTypingOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalTypingOn.TabIndex = 1;
			dateTimeMedicalTypingOn.Value = new System.DateTime(0L);
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(21, 58);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(101, 13);
			mmLabel35.TabIndex = 11;
			mmLabel35.Text = "Medical Typing On :";
			textBoxMedicalNote.BackColor = System.Drawing.Color.White;
			textBoxMedicalNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMedicalNote.CustomReportFieldName = "";
			textBoxMedicalNote.CustomReportKey = "";
			textBoxMedicalNote.CustomReportValueType = 1;
			textBoxMedicalNote.IsComboTextBox = false;
			textBoxMedicalNote.IsModified = false;
			textBoxMedicalNote.Location = new System.Drawing.Point(149, 349);
			textBoxMedicalNote.MaxLength = 255;
			textBoxMedicalNote.Multiline = true;
			textBoxMedicalNote.Name = "textBoxMedicalNote";
			textBoxMedicalNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxMedicalNote.Size = new System.Drawing.Size(651, 97);
			textBoxMedicalNote.TabIndex = 10;
			mmLabel78.AutoSize = true;
			mmLabel78.BackColor = System.Drawing.Color.Transparent;
			mmLabel78.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel78.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel78.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel78.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel78.IsFieldHeader = false;
			mmLabel78.IsRequired = false;
			mmLabel78.Location = new System.Drawing.Point(32, 352);
			mmLabel78.Name = "mmLabel78";
			mmLabel78.PenWidth = 1f;
			mmLabel78.ShowBorder = false;
			mmLabel78.Size = new System.Drawing.Size(120, 13);
			mmLabel78.TabIndex = 136;
			mmLabel78.Text = "Medical/Emirates Note :";
			ultraTabPageControl3.Controls.Add(panelWPRP);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(810, 525);
			panelWPRP.Controls.Add(panelMedicalReport);
			panelWPRP.Controls.Add(panelAGT);
			panelWPRP.Enabled = false;
			panelWPRP.Location = new System.Drawing.Point(-2, 0);
			panelWPRP.Name = "panelWPRP";
			panelWPRP.Size = new System.Drawing.Size(814, 529);
			panelWPRP.TabIndex = 0;
			panelMedicalReport.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelMedicalReport.Controls.Add(comboBoxProcessType);
			panelMedicalReport.Controls.Add(dateTimeRPExpiryDate);
			panelMedicalReport.Controls.Add(mmLabel56);
			panelMedicalReport.Controls.Add(mmLabel65);
			panelMedicalReport.Controls.Add(dateTimeRPIssueDate);
			panelMedicalReport.Controls.Add(mmLabel64);
			panelMedicalReport.Controls.Add(textBoxRPIssuePlace);
			panelMedicalReport.Controls.Add(mmLabel63);
			panelMedicalReport.Controls.Add(dateTimePassportCollectedOnRP);
			panelMedicalReport.Controls.Add(mmLabel52);
			panelMedicalReport.Controls.Add(dateTimeSubmittedZajilOnRP);
			panelMedicalReport.Controls.Add(mmLabel50);
			panelMedicalReport.Controls.Add(dateTimeApplApprovedOnRP);
			panelMedicalReport.Controls.Add(mmLabel49);
			panelMedicalReport.Controls.Add(dateTimeApplPostedOnRP);
			panelMedicalReport.Controls.Add(mmLabel48);
			panelMedicalReport.Enabled = false;
			panelMedicalReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelMedicalReport.Location = new System.Drawing.Point(12, 265);
			panelMedicalReport.Name = "panelMedicalReport";
			panelMedicalReport.Size = new System.Drawing.Size(788, 222);
			panelMedicalReport.TabIndex = 140;
			panelMedicalReport.Text = "RP Submission Details";
			comboBoxProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxProcessType.FormattingEnabled = true;
			comboBoxProcessType.Items.AddRange(new object[2]
			{
				"Online",
				"Manual"
			});
			comboBoxProcessType.Location = new System.Drawing.Point(157, 32);
			comboBoxProcessType.Name = "comboBoxProcessType";
			comboBoxProcessType.Size = new System.Drawing.Size(124, 21);
			comboBoxProcessType.TabIndex = 7;
			dateTimeRPExpiryDate.Checked = false;
			dateTimeRPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRPExpiryDate.Location = new System.Drawing.Point(157, 188);
			dateTimeRPExpiryDate.Name = "dateTimeRPExpiryDate";
			dateTimeRPExpiryDate.ShowCheckBox = true;
			dateTimeRPExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeRPExpiryDate.TabIndex = 14;
			dateTimeRPExpiryDate.Value = new System.DateTime(0L);
			mmLabel56.AutoSize = true;
			mmLabel56.BackColor = System.Drawing.Color.Transparent;
			mmLabel56.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel56.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel56.IsFieldHeader = false;
			mmLabel56.IsRequired = false;
			mmLabel56.Location = new System.Drawing.Point(17, 35);
			mmLabel56.Name = "mmLabel56";
			mmLabel56.PenWidth = 1f;
			mmLabel56.ShowBorder = false;
			mmLabel56.Size = new System.Drawing.Size(75, 13);
			mmLabel56.TabIndex = 137;
			mmLabel56.Text = "Process Type:";
			mmLabel65.AutoSize = true;
			mmLabel65.BackColor = System.Drawing.Color.Transparent;
			mmLabel65.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel65.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel65.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel65.IsFieldHeader = false;
			mmLabel65.IsRequired = false;
			mmLabel65.Location = new System.Drawing.Point(17, 194);
			mmLabel65.Name = "mmLabel65";
			mmLabel65.PenWidth = 1f;
			mmLabel65.ShowBorder = false;
			mmLabel65.Size = new System.Drawing.Size(83, 13);
			mmLabel65.TabIndex = 84;
			mmLabel65.Text = "RP Expiry Date:";
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRPIssueDate.Location = new System.Drawing.Point(157, 165);
			dateTimeRPIssueDate.Name = "dateTimeRPIssueDate";
			dateTimeRPIssueDate.ShowCheckBox = true;
			dateTimeRPIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeRPIssueDate.TabIndex = 13;
			dateTimeRPIssueDate.Value = new System.DateTime(0L);
			mmLabel64.AutoSize = true;
			mmLabel64.BackColor = System.Drawing.Color.Transparent;
			mmLabel64.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel64.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel64.IsFieldHeader = false;
			mmLabel64.IsRequired = false;
			mmLabel64.Location = new System.Drawing.Point(17, 171);
			mmLabel64.Name = "mmLabel64";
			mmLabel64.PenWidth = 1f;
			mmLabel64.ShowBorder = false;
			mmLabel64.Size = new System.Drawing.Size(79, 13);
			mmLabel64.TabIndex = 82;
			mmLabel64.Text = "RP Issue Date:";
			textBoxRPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxRPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRPIssuePlace.CustomReportFieldName = "";
			textBoxRPIssuePlace.CustomReportKey = "";
			textBoxRPIssuePlace.CustomReportValueType = 1;
			textBoxRPIssuePlace.IsComboTextBox = false;
			textBoxRPIssuePlace.IsModified = false;
			textBoxRPIssuePlace.Location = new System.Drawing.Point(157, 143);
			textBoxRPIssuePlace.MaxLength = 30;
			textBoxRPIssuePlace.Name = "textBoxRPIssuePlace";
			textBoxRPIssuePlace.Size = new System.Drawing.Size(417, 20);
			textBoxRPIssuePlace.TabIndex = 12;
			mmLabel63.AutoSize = true;
			mmLabel63.BackColor = System.Drawing.Color.Transparent;
			mmLabel63.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel63.IsFieldHeader = false;
			mmLabel63.IsRequired = false;
			mmLabel63.Location = new System.Drawing.Point(17, 148);
			mmLabel63.Name = "mmLabel63";
			mmLabel63.PenWidth = 1f;
			mmLabel63.ShowBorder = false;
			mmLabel63.Size = new System.Drawing.Size(81, 13);
			mmLabel63.TabIndex = 80;
			mmLabel63.Text = "RP Issue Place:";
			dateTimePassportCollectedOnRP.Checked = false;
			dateTimePassportCollectedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimePassportCollectedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePassportCollectedOnRP.Location = new System.Drawing.Point(157, 121);
			dateTimePassportCollectedOnRP.Name = "dateTimePassportCollectedOnRP";
			dateTimePassportCollectedOnRP.ShowCheckBox = true;
			dateTimePassportCollectedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimePassportCollectedOnRP.TabIndex = 11;
			dateTimePassportCollectedOnRP.Value = new System.DateTime(0L);
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(17, 127);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(117, 13);
			mmLabel52.TabIndex = 76;
			mmLabel52.Text = "Passport Collected On:";
			dateTimeSubmittedZajilOnRP.Checked = false;
			dateTimeSubmittedZajilOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeSubmittedZajilOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSubmittedZajilOnRP.Location = new System.Drawing.Point(157, 99);
			dateTimeSubmittedZajilOnRP.Name = "dateTimeSubmittedZajilOnRP";
			dateTimeSubmittedZajilOnRP.ShowCheckBox = true;
			dateTimeSubmittedZajilOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeSubmittedZajilOnRP.TabIndex = 10;
			dateTimeSubmittedZajilOnRP.Value = new System.DateTime(0L);
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(17, 105);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(111, 13);
			mmLabel50.TabIndex = 74;
			mmLabel50.Text = "Submitted to Zajil On:";
			dateTimeApplApprovedOnRP.Checked = false;
			dateTimeApplApprovedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplApprovedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplApprovedOnRP.Location = new System.Drawing.Point(157, 76);
			dateTimeApplApprovedOnRP.Name = "dateTimeApplApprovedOnRP";
			dateTimeApplApprovedOnRP.ShowCheckBox = true;
			dateTimeApplApprovedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeApplApprovedOnRP.TabIndex = 9;
			dateTimeApplApprovedOnRP.Value = new System.DateTime(0L);
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(17, 82);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(103, 13);
			mmLabel49.TabIndex = 72;
			mmLabel49.Text = "Appl. Approved On:";
			dateTimeApplPostedOnRP.Checked = false;
			dateTimeApplPostedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplPostedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplPostedOnRP.Location = new System.Drawing.Point(157, 54);
			dateTimeApplPostedOnRP.Name = "dateTimeApplPostedOnRP";
			dateTimeApplPostedOnRP.ShowCheckBox = true;
			dateTimeApplPostedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeApplPostedOnRP.TabIndex = 8;
			dateTimeApplPostedOnRP.Value = new System.DateTime(0L);
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(17, 59);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(89, 13);
			mmLabel48.TabIndex = 70;
			mmLabel48.Text = "Appl. Posted On:";
			panelAGT.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelAGT.Controls.Add(textBoxAGTMBNo);
			panelAGT.Controls.Add(mmLabel91);
			panelAGT.Controls.Add(mmLabel90);
			panelAGT.Controls.Add(comboBoxAGTType);
			panelAGT.Controls.Add(textBoxPersonIDNo);
			panelAGT.Controls.Add(mmLabel66);
			panelAGT.Controls.Add(textBoxWPIssuePlace);
			panelAGT.Controls.Add(dateTimeWPExpiryDate);
			panelAGT.Controls.Add(dateTimeWPIssueDate);
			panelAGT.Controls.Add(mmLabel62);
			panelAGT.Controls.Add(mmLabel61);
			panelAGT.Controls.Add(mmLabel60);
			panelAGT.Controls.Add(textBoxWPNo);
			panelAGT.Controls.Add(dateTimeAGTSubmittedOn);
			panelAGT.Controls.Add(mmLabel57);
			panelAGT.Controls.Add(mmLabel58);
			panelAGT.Controls.Add(dateTimeAGTTypedOn);
			panelAGT.Controls.Add(mmLabel59);
			panelAGT.Enabled = false;
			panelAGT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelAGT.Location = new System.Drawing.Point(12, 24);
			panelAGT.Name = "panelAGT";
			panelAGT.Size = new System.Drawing.Size(788, 228);
			panelAGT.TabIndex = 139;
			panelAGT.Text = "AGT/WP Submission Details";
			textBoxAGTMBNo.BackColor = System.Drawing.Color.White;
			textBoxAGTMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAGTMBNo.CustomReportFieldName = "";
			textBoxAGTMBNo.CustomReportKey = "";
			textBoxAGTMBNo.CustomReportValueType = 1;
			textBoxAGTMBNo.IsComboTextBox = false;
			textBoxAGTMBNo.IsModified = false;
			textBoxAGTMBNo.Location = new System.Drawing.Point(157, 46);
			textBoxAGTMBNo.MaxLength = 30;
			textBoxAGTMBNo.Name = "textBoxAGTMBNo";
			textBoxAGTMBNo.Size = new System.Drawing.Size(234, 20);
			textBoxAGTMBNo.TabIndex = 1;
			mmLabel91.AutoSize = true;
			mmLabel91.BackColor = System.Drawing.Color.Transparent;
			mmLabel91.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel91.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel91.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel91.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel91.IsFieldHeader = false;
			mmLabel91.IsRequired = false;
			mmLabel91.Location = new System.Drawing.Point(17, 49);
			mmLabel91.Name = "mmLabel91";
			mmLabel91.PenWidth = 1f;
			mmLabel91.ShowBorder = false;
			mmLabel91.Size = new System.Drawing.Size(114, 13);
			mmLabel91.TabIndex = 86;
			mmLabel91.Text = "MB Ref No - AGT/WP :";
			mmLabel90.AutoSize = true;
			mmLabel90.BackColor = System.Drawing.Color.Transparent;
			mmLabel90.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel90.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel90.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel90.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel90.IsFieldHeader = false;
			mmLabel90.IsRequired = false;
			mmLabel90.Location = new System.Drawing.Point(17, 27);
			mmLabel90.Name = "mmLabel90";
			mmLabel90.PenWidth = 1f;
			mmLabel90.ShowBorder = false;
			mmLabel90.Size = new System.Drawing.Size(58, 13);
			mmLabel90.TabIndex = 84;
			mmLabel90.Text = "AGT Type:";
			comboBoxAGTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxAGTType.FormattingEnabled = true;
			comboBoxAGTType.Items.AddRange(new object[2]
			{
				"Limited",
				"Unlimited"
			});
			comboBoxAGTType.Location = new System.Drawing.Point(157, 24);
			comboBoxAGTType.Name = "comboBoxAGTType";
			comboBoxAGTType.Size = new System.Drawing.Size(124, 21);
			comboBoxAGTType.TabIndex = 0;
			textBoxPersonIDNo.BackColor = System.Drawing.Color.White;
			textBoxPersonIDNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPersonIDNo.CustomReportFieldName = "";
			textBoxPersonIDNo.CustomReportKey = "";
			textBoxPersonIDNo.CustomReportValueType = 1;
			textBoxPersonIDNo.IsComboTextBox = false;
			textBoxPersonIDNo.IsModified = false;
			textBoxPersonIDNo.Location = new System.Drawing.Point(157, 134);
			textBoxPersonIDNo.MaxLength = 30;
			textBoxPersonIDNo.Name = "textBoxPersonIDNo";
			textBoxPersonIDNo.Size = new System.Drawing.Size(234, 20);
			textBoxPersonIDNo.TabIndex = 5;
			mmLabel66.AutoSize = true;
			mmLabel66.BackColor = System.Drawing.Color.Transparent;
			mmLabel66.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel66.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel66.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel66.IsFieldHeader = false;
			mmLabel66.IsRequired = false;
			mmLabel66.Location = new System.Drawing.Point(17, 137);
			mmLabel66.Name = "mmLabel66";
			mmLabel66.PenWidth = 1f;
			mmLabel66.ShowBorder = false;
			mmLabel66.Size = new System.Drawing.Size(82, 13);
			mmLabel66.TabIndex = 82;
			mmLabel66.Text = "Personal ID No:";
			textBoxWPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxWPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxWPIssuePlace.CustomReportFieldName = "";
			textBoxWPIssuePlace.CustomReportKey = "";
			textBoxWPIssuePlace.CustomReportValueType = 1;
			textBoxWPIssuePlace.IsComboTextBox = false;
			textBoxWPIssuePlace.IsModified = false;
			textBoxWPIssuePlace.Location = new System.Drawing.Point(157, 156);
			textBoxWPIssuePlace.MaxLength = 30;
			textBoxWPIssuePlace.Name = "textBoxWPIssuePlace";
			textBoxWPIssuePlace.Size = new System.Drawing.Size(417, 20);
			textBoxWPIssuePlace.TabIndex = 6;
			dateTimeWPExpiryDate.Checked = false;
			dateTimeWPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeWPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeWPExpiryDate.Location = new System.Drawing.Point(157, 200);
			dateTimeWPExpiryDate.Name = "dateTimeWPExpiryDate";
			dateTimeWPExpiryDate.ShowCheckBox = true;
			dateTimeWPExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeWPExpiryDate.TabIndex = 8;
			dateTimeWPExpiryDate.Value = new System.DateTime(0L);
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeWPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeWPIssueDate.Location = new System.Drawing.Point(157, 178);
			dateTimeWPIssueDate.Name = "dateTimeWPIssueDate";
			dateTimeWPIssueDate.ShowCheckBox = true;
			dateTimeWPIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeWPIssueDate.TabIndex = 7;
			dateTimeWPIssueDate.Value = new System.DateTime(0L);
			mmLabel62.AutoSize = true;
			mmLabel62.BackColor = System.Drawing.Color.Transparent;
			mmLabel62.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel62.IsFieldHeader = false;
			mmLabel62.IsRequired = false;
			mmLabel62.Location = new System.Drawing.Point(17, 205);
			mmLabel62.Name = "mmLabel62";
			mmLabel62.PenWidth = 1f;
			mmLabel62.ShowBorder = false;
			mmLabel62.Size = new System.Drawing.Size(86, 13);
			mmLabel62.TabIndex = 81;
			mmLabel62.Text = "WP Expiry Date:";
			mmLabel61.AutoSize = true;
			mmLabel61.BackColor = System.Drawing.Color.Transparent;
			mmLabel61.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel61.IsFieldHeader = false;
			mmLabel61.IsRequired = false;
			mmLabel61.Location = new System.Drawing.Point(17, 183);
			mmLabel61.Name = "mmLabel61";
			mmLabel61.PenWidth = 1f;
			mmLabel61.ShowBorder = false;
			mmLabel61.Size = new System.Drawing.Size(82, 13);
			mmLabel61.TabIndex = 80;
			mmLabel61.Text = "WP Issue Date:";
			mmLabel60.AutoSize = true;
			mmLabel60.BackColor = System.Drawing.Color.Transparent;
			mmLabel60.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel60.IsFieldHeader = false;
			mmLabel60.IsRequired = false;
			mmLabel60.Location = new System.Drawing.Point(17, 160);
			mmLabel60.Name = "mmLabel60";
			mmLabel60.PenWidth = 1f;
			mmLabel60.ShowBorder = false;
			mmLabel60.Size = new System.Drawing.Size(84, 13);
			mmLabel60.TabIndex = 79;
			mmLabel60.Text = "WP Issue Place:";
			textBoxWPNo.BackColor = System.Drawing.Color.White;
			textBoxWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxWPNo.CustomReportFieldName = "";
			textBoxWPNo.CustomReportKey = "";
			textBoxWPNo.CustomReportValueType = 1;
			textBoxWPNo.IsComboTextBox = false;
			textBoxWPNo.IsModified = false;
			textBoxWPNo.Location = new System.Drawing.Point(157, 112);
			textBoxWPNo.MaxLength = 30;
			textBoxWPNo.Name = "textBoxWPNo";
			textBoxWPNo.Size = new System.Drawing.Size(234, 20);
			textBoxWPNo.TabIndex = 4;
			dateTimeAGTSubmittedOn.Checked = false;
			dateTimeAGTSubmittedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeAGTSubmittedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAGTSubmittedOn.Location = new System.Drawing.Point(157, 90);
			dateTimeAGTSubmittedOn.Name = "dateTimeAGTSubmittedOn";
			dateTimeAGTSubmittedOn.ShowCheckBox = true;
			dateTimeAGTSubmittedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeAGTSubmittedOn.TabIndex = 3;
			dateTimeAGTSubmittedOn.Value = new System.DateTime(0L);
			mmLabel57.AutoSize = true;
			mmLabel57.BackColor = System.Drawing.Color.Transparent;
			mmLabel57.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel57.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel57.IsFieldHeader = false;
			mmLabel57.IsRequired = false;
			mmLabel57.Location = new System.Drawing.Point(17, 94);
			mmLabel57.Name = "mmLabel57";
			mmLabel57.PenWidth = 1f;
			mmLabel57.ShowBorder = false;
			mmLabel57.Size = new System.Drawing.Size(117, 13);
			mmLabel57.TabIndex = 74;
			mmLabel57.Text = "AGT/WP Submitted on:";
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(17, 116);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(43, 13);
			mmLabel58.TabIndex = 72;
			mmLabel58.Text = "WP No:";
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTTypedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeAGTTypedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAGTTypedOn.Location = new System.Drawing.Point(157, 68);
			dateTimeAGTTypedOn.Name = "dateTimeAGTTypedOn";
			dateTimeAGTTypedOn.ShowCheckBox = true;
			dateTimeAGTTypedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeAGTTypedOn.TabIndex = 2;
			dateTimeAGTTypedOn.Value = new System.DateTime(0L);
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(17, 72);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(128, 13);
			mmLabel59.TabIndex = 70;
			mmLabel59.Text = "AGT/WP Form Typed On:";
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(810, 525);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Enabled = false;
			udfEntryGrid.Location = new System.Drawing.Point(8, 18);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(790, 456);
			udfEntryGrid.TabIndex = 1;
			ultraTabPageControl5.Controls.Add(ultraGroupBox1);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(810, 525);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(textBoxComment);
			ultraGroupBox1.Controls.Add(mmLabel20);
			ultraGroupBox1.Controls.Add(textBoxPostalCode);
			ultraGroupBox1.Controls.Add(mmLabel18);
			ultraGroupBox1.Controls.Add(textBoxEmail);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(textBoxMobile);
			ultraGroupBox1.Controls.Add(mmLabel16);
			ultraGroupBox1.Controls.Add(textBoxFax);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(textBoxPhone2);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxPhone1);
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(textBoxCountry);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxState);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxCity);
			ultraGroupBox1.Controls.Add(textBoxAddress3);
			ultraGroupBox1.Controls.Add(textBoxAddress2);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxAddress1);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxAddressID);
			ultraGroupBox1.Location = new System.Drawing.Point(10, 20);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(718, 205);
			ultraGroupBox1.TabIndex = 22;
			ultraGroupBox1.Text = "Address";
			ultraGroupBox1.Visible = false;
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(362, 132);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(56, 13);
			mmLabel23.TabIndex = 32;
			mmLabel23.Text = "Comment:";
			textBoxComment.BackColor = System.Drawing.Color.White;
			textBoxComment.CustomReportFieldName = "";
			textBoxComment.CustomReportKey = "";
			textBoxComment.CustomReportValueType = 1;
			textBoxComment.IsComboTextBox = false;
			textBoxComment.IsModified = false;
			textBoxComment.Location = new System.Drawing.Point(460, 129);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(229, 20);
			textBoxComment.TabIndex = 13;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(9, 176);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(68, 13);
			mmLabel20.TabIndex = 14;
			mmLabel20.Text = "Postal Code:";
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.IsModified = false;
			textBoxPostalCode.Location = new System.Drawing.Point(123, 173);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 7;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(362, 110);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(35, 13);
			mmLabel18.TabIndex = 28;
			mmLabel18.Text = "Email:";
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.IsModified = false;
			textBoxEmail.Location = new System.Drawing.Point(460, 107);
			textBoxEmail.MaxLength = 64;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(229, 20);
			textBoxEmail.TabIndex = 12;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(362, 88);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(41, 13);
			mmLabel17.TabIndex = 26;
			mmLabel17.Text = "Mobile:";
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.IsModified = false;
			textBoxMobile.Location = new System.Drawing.Point(460, 85);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(229, 20);
			textBoxMobile.TabIndex = 11;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(362, 65);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(29, 13);
			mmLabel16.TabIndex = 24;
			mmLabel16.Text = "Fax:";
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.IsModified = false;
			textBoxFax.Location = new System.Drawing.Point(460, 63);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(229, 20);
			textBoxFax.TabIndex = 10;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(362, 44);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(50, 13);
			mmLabel15.TabIndex = 22;
			mmLabel15.Text = "Phone 2:";
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.IsModified = false;
			textBoxPhone2.Location = new System.Drawing.Point(460, 41);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(229, 20);
			textBoxPhone2.TabIndex = 9;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(362, 22);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(50, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Phone 1:";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.IsModified = false;
			textBoxPhone1.Location = new System.Drawing.Point(460, 19);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(229, 20);
			textBoxPhone1.TabIndex = 8;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(9, 154);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(50, 13);
			mmLabel12.TabIndex = 12;
			mmLabel12.Text = "Country:";
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.IsModified = false;
			textBoxCountry.Location = new System.Drawing.Point(123, 151);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 6;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 132);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(37, 13);
			mmLabel11.TabIndex = 10;
			mmLabel11.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(123, 129);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 5;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 109);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(30, 13);
			mmLabel13.TabIndex = 8;
			mmLabel13.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(123, 107);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(229, 20);
			textBoxCity.TabIndex = 4;
			textBoxAddress3.BackColor = System.Drawing.Color.White;
			textBoxAddress3.CustomReportFieldName = "";
			textBoxAddress3.CustomReportKey = "";
			textBoxAddress3.CustomReportValueType = 1;
			textBoxAddress3.IsComboTextBox = false;
			textBoxAddress3.IsModified = false;
			textBoxAddress3.Location = new System.Drawing.Point(123, 85);
			textBoxAddress3.MaxLength = 64;
			textBoxAddress3.Name = "textBoxAddress3";
			textBoxAddress3.Size = new System.Drawing.Size(229, 20);
			textBoxAddress3.TabIndex = 3;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(123, 63);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(229, 20);
			textBoxAddress2.TabIndex = 2;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(9, 43);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(50, 13);
			mmLabel10.TabIndex = 4;
			mmLabel10.Text = "Address:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(123, 41);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(229, 20);
			textBoxAddress1.TabIndex = 1;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 22);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(64, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "Address ID:";
			textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAddressID.CustomReportFieldName = "";
			textBoxAddressID.CustomReportKey = "";
			textBoxAddressID.CustomReportValueType = 1;
			textBoxAddressID.Enabled = false;
			textBoxAddressID.ForeColor = System.Drawing.Color.Black;
			textBoxAddressID.IsComboTextBox = false;
			textBoxAddressID.IsModified = false;
			textBoxAddressID.Location = new System.Drawing.Point(123, 19);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(229, 20);
			textBoxAddressID.TabIndex = 0;
			textBoxAddressID.Text = "PRIMARY";
			ultraTabControl.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl.Controls.Add(tabPageGeneral);
			ultraTabControl.Controls.Add(tabPageDetails);
			ultraTabControl.Controls.Add(tabPageUserDefined);
			ultraTabControl.Controls.Add(ultraTabPageControl1);
			ultraTabControl.Controls.Add(ultraTabPageControl2);
			ultraTabControl.Controls.Add(ultraTabPageControl3);
			ultraTabControl.Controls.Add(ultraTabPageControl4);
			ultraTabControl.Controls.Add(ultraTabPageControl5);
			ultraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl.Location = new System.Drawing.Point(0, 60);
			ultraTabControl.MinTabWidth = 80;
			ultraTabControl.Name = "ultraTabControl";
			ultraTabControl.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl.Size = new System.Drawing.Size(814, 542);
			ultraTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl.TabIndex = 6;
			appearance189.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab2.Appearance = appearance189;
			ultraTab2.TabPage = tabPageGeneral;
			ultraTab2.Text = "&General";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "&Renumeration";
			ultraTab4.TabPage = tabPageDetails;
			ultraTab4.Text = "&Recruitment";
			ultraTab4.Visible = false;
			ultraTab5.TabPage = ultraTabPageControl4;
			ultraTab5.Text = "Visa Process";
			ultraTab5.Visible = false;
			ultraTab6.TabPage = ultraTabPageControl2;
			ultraTab6.Text = "&Medical/Emirates";
			ultraTab6.Visible = false;
			ultraTab7.TabPage = ultraTabPageControl3;
			ultraTab7.Text = "&WP/RP";
			ultraTab7.Visible = false;
			ultraTab8.TabPage = tabPageUserDefined;
			ultraTab8.Text = "&User Defined";
			ultraTab8.Visible = false;
			ultraTab9.TabPage = ultraTabPageControl5;
			ultraTab9.Text = "Container";
			ultraTab9.Visible = false;
			ultraTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[8]
			{
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(810, 519);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 602);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(814, 40);
			panelButtons.TabIndex = 99;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(814, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(704, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
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
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButtonEmployee,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(814, 31);
			toolStrip1.TabIndex = 306;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonEmployee.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonEmployee.Image");
			toolStripButtonEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonEmployee.Name = "toolStripButtonEmployee";
			toolStripButtonEmployee.Size = new System.Drawing.Size(119, 28);
			toolStripButtonEmployee.Text = "Make Employee";
			toolStripButtonEmployee.Visible = false;
			toolStripButtonEmployee.Click += new System.EventHandler(toolStripBtnMakeEmployee_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Controls.Add(labelCancelled);
			panel1.Controls.Add(labelCategory);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(814, 29);
			panel1.TabIndex = 314;
			labelCancelled.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelCancelled.BackColor = System.Drawing.Color.White;
			labelCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelCancelled.ForeColor = System.Drawing.Color.DarkRed;
			labelCancelled.Location = new System.Drawing.Point(683, 3);
			labelCancelled.Name = "labelCancelled";
			labelCancelled.Size = new System.Drawing.Size(126, 23);
			labelCancelled.TabIndex = 148;
			labelCancelled.Text = "CANCELLED";
			labelCancelled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelCancelled.Visible = false;
			labelCategory.AutoSize = true;
			labelCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelCategory.Location = new System.Drawing.Point(550, 8);
			labelCategory.Name = "labelCategory";
			labelCategory.Size = new System.Drawing.Size(0, 13);
			labelCategory.TabIndex = 3;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(29, 7);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 2;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				dependentsToolStripMenuItem,
				documentsToolStripMenuItem,
				skillsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(138, 70);
			dependentsToolStripMenuItem.Name = "dependentsToolStripMenuItem";
			dependentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			dependentsToolStripMenuItem.Text = "Dependents";
			dependentsToolStripMenuItem.Click += new System.EventHandler(dependentsToolStripMenuItem_Click);
			documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
			documentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			documentsToolStripMenuItem.Text = "Documents";
			documentsToolStripMenuItem.Click += new System.EventHandler(documentsToolStripMenuItem_Click);
			skillsToolStripMenuItem.Name = "skillsToolStripMenuItem";
			skillsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			skillsToolStripMenuItem.Text = "Skills";
			skillsToolStripMenuItem.Click += new System.EventHandler(skillsToolStripMenuItem_Click);
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				morePrintToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			morePrintToolStripMenuItem.Name = "morePrintToolStripMenuItem";
			morePrintToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			morePrintToolStripMenuItem.Text = "More Print";
			morePrintToolStripMenuItem.Click += new System.EventHandler(morePrintToolStripMenuItem_Click);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(814, 642);
			base.Controls.Add(ultraTabControl);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(830, 670);
			base.Name = "AppointmentDetailsForm";
			Text = "Appointment Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CandidateClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CandidateDetailsForm_Load);
			ultraTabPageControl6.ResumeLayout(false);
			ultraTabPageControl6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).EndInit();
			tabPageGeneral.ResumeLayout(false);
			panelGeneral.ResumeLayout(false);
			panelGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDesignation).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			panelArrival.ResumeLayout(false);
			panelArrival.PerformLayout();
			((System.ComponentModel.ISupportInitialize)sponsorComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			tabPageDetails.ResumeLayout(false);
			panelRecruitment.ResumeLayout(false);
			panelRecruitment.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceAbroad).EndInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceLocal).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLanguage).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionActual).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAgentThrough).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			panelVisaProcess.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelVisaIMG).EndInit();
			panelVisaIMG.ResumeLayout(false);
			panelVisaIMG.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaMOL).EndInit();
			panelVisaMOL.ResumeLayout(false);
			panelVisaMOL.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionVisa).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			panelMedicalEmirates.ResumeLayout(false);
			panelMedicalEmirates.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelEmirates).EndInit();
			panelEmirates.ResumeLayout(false);
			panelEmirates.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalDetail).EndInit();
			panelMedicalDetail.ResumeLayout(false);
			panelMedicalDetail.PerformLayout();
			ultraTabPageControl3.ResumeLayout(false);
			panelWPRP.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelMedicalReport).EndInit();
			panelMedicalReport.ResumeLayout(false);
			panelMedicalReport.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelAGT).EndInit();
			panelAGT.ResumeLayout(false);
			panelAGT.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			ultraTabPageControl5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl).EndInit();
			ultraTabControl.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
		}

		private void AddEvents()
		{
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			textBoxGivenName.TextChanged += textBoxCode_TextChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			textBoxEmployeeNo.TextChanged += textBoxEmployeeNo_TextChanged;
			textBoxNationalID.TextChanged += textBoxNationalID_TextChanged;
			comboBoxSelectionStatus.SelectedIndexChanged += comboBoxSelectionStatus_SelectedIndexChanged;
			dateTimeBirthDate.ValueChanged += dateTimeBirthDate_ValueChanged;
			textBoxPassportNo.Leave += textBoxPassportNo_Leave;
			textBoxPassportNo.KeyPress += textBoxPassportNo_KeyPress;
			dateTimeApplTypingDateEID.ValueChanged += dateTimeApplTypingDateEID_ValueChanged;
			dateTimeArrivedOn.ValueChanged += dateTimeArrivedOn_ValueChanged;
			dateTimeApprovalDateMOL.ValueChanged += dateTimeApprovalDateMOL_ValueChanged;
			dateTimeVisaIssueDate.ValueChanged += dateTimeVisaIssueDate_ValueChanged;
			dateTimeWPIssueDate.ValueChanged += dateTimeWPIssueDate_ValueChanged;
			comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
			dataGridPayrollItem.CellDataError += dataGrid_CellDataError;
			dataGridPayrollItem.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridPayrollItem.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridPayrollItem.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxPayrollItem.SelectedIndexChanged += comboBoxPayrollItem_SelectedIndexChanged;
			dataGridPayrollItem.AfterCellUpdate += dataGridPayrollItem_AfterCellUpdate;
			dataGridPayrollItem.HeaderClicked += dataGridPayrollItem_HeaderClicked;
		}

		private void dataGridPayrollItem_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "PayrollItem")
			{
				string id = "";
				if (dataGridPayrollItem.ActiveRow != null)
				{
					id = dataGridPayrollItem.ActiveRow.Cells["PayrollItem"].Text;
				}
				new FormHelper().EditPayrollItem(id);
			}
		}

		private void dataGridPayrollItem_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				ShowTotalSalary();
			}
			else if (e.Cell.Column.Key == "PayrollItem")
			{
				dataGridPayrollItem.ActiveRow.Cells["Description"].Value = comboBoxPayrollItem.SelectedName;
			}
		}

		private void ShowTotalSalary()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridPayrollItem.Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Amount"].Text, out result);
				num += result;
			}
			textBoxTotalSalary.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void comboBoxPayrollItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = dataGridPayrollItem.ActiveRow;
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "PayrollItem")
			{
				e.RaiseErrorEvent = false;
				comboBoxPayrollItem.Text = dataGridPayrollItem.ActiveCell.Text;
				comboBoxPayrollItem.QuickAddItem();
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridPayrollItem.ActiveRow;
			if (activeRow != null && activeRow.Cells["PayrollItem"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an payrollItem.");
				e.Cancel = true;
				activeRow.Cells["PayrollItem"].Activate();
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key == "PayrollItem")
			{
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (row.Index != dataGridPayrollItem.ActiveRow.Index && !(row.Cells["PayrollItem"].Value.ToString() == "") && row.Cells["PayrollItem"].Value.ToString() == dataGridPayrollItem.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This payrollItem is already selected for this employee.");
						e.Cancel = true;
						break;
					}
				}
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridPayrollItem.ActiveCell.Text == "")
				{
					dataGridPayrollItem.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridPayrollItem.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridPayrollItem.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			labelCategory.Text = comboBoxCategory.SelectedName;
		}

		private void dateTimeWPIssueDate_ValueChanged(object sender, EventArgs e)
		{
			dateTimeWPExpiryDate.Checked = dateTimeWPIssueDate.Checked;
			if (dateTimeWPIssueDate.Checked)
			{
				dateTimeWPExpiryDate.Value = dateTimeWPIssueDate.Value.AddYears(2);
			}
		}

		private void dateTimeVisaIssueDate_ValueChanged(object sender, EventArgs e)
		{
			dateTimeVisaExpiryDate.Checked = dateTimeVisaIssueDate.Checked;
			if (dateTimeVisaIssueDate.Checked)
			{
				dateTimeVisaExpiryDate.Value = dateTimeVisaIssueDate.Value.AddMonths(2);
			}
		}

		private void dateTimeApprovalDateMOL_ValueChanged(object sender, EventArgs e)
		{
			dateTimeApprovalValidTillMOL.Checked = dateTimeApprovalDateMOL.Checked;
			if (dateTimeApprovalDateMOL.Checked)
			{
				dateTimeApprovalValidTillMOL.Value = dateTimeApprovalDateMOL.Value.AddMonths(2);
			}
		}

		public void EnableDisableWorkFlowScreen(WorkflowType workFlowType)
		{
			switch (workFlowType)
			{
			case WorkflowType.General:
				panelGeneral.Enabled = true;
				break;
			case WorkflowType.Recruitment:
				panelRecruitment.Enabled = true;
				break;
			case WorkflowType.VisaProcess:
				panelVisaProcess.Enabled = true;
				break;
			case WorkflowType.Arrival:
				panelArrival.Enabled = true;
				break;
			case WorkflowType.Medical_Emirates:
				panelMedicalEmirates.Enabled = true;
				break;
			case WorkflowType.WP_RP:
				panelWPRP.Enabled = true;
				udfEntryGrid.Enabled = true;
				break;
			}
			if ((int)workFlowType < ultraTabControl.Tabs.Count)
			{
				ultraTabControl.Tabs[(int)workFlowType].Selected = true;
			}
		}

		private void sponsorComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dateTimeArrivedOn.Checked)
			{
				if (sponsorComboBox.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Select Sponsor");
				}
				else if (!isExist)
				{
					textBoxEmployeeNo.Text = GetNextEmployeeNumber();
				}
			}
			else if (!isExist)
			{
				textBoxEmployeeNo.Clear();
			}
			Console.WriteLine("");
		}

		private void dateTimeArrivedOn_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimeArrivedOn.Checked)
			{
				if (sponsorComboBox.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Select Sponsor");
				}
				else if (!isExist)
				{
					textBoxEmployeeNo.Text = GetNextEmployeeNumber();
				}
			}
			else if (!isExist)
			{
				textBoxEmployeeNo.Clear();
			}
			Console.WriteLine("");
		}

		private void dateTimeApplTypingDateEID_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimeApplTypingDateEID.Checked)
			{
				UltraGroupBox ultraGroupBox = panelMedicalReport;
				bool enabled = panelAGT.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelMedicalReport;
				bool enabled = panelAGT.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void textBoxPassportNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
			{
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void textBoxPassportNo_Leave(object sender, EventArgs e)
		{
			if (!(textBoxPassportNo.Text.Trim() == string.Empty) && isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "PassportNo", textBoxPassportNo.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Passport Number already exist.");
				tabPageGeneral.Tab.Selected = true;
			}
		}

		private void textBoxNationalID_TextChanged(object sender, EventArgs e)
		{
			if (textBoxNationalID.Text.Trim().Length > 0)
			{
				UltraGroupBox ultraGroupBox = panelMedicalDetail;
				bool enabled = panelMedicalReport.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelMedicalDetail;
				bool enabled = panelMedicalReport.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
		{
			if (textBoxEmployeeNo.Text.Trim().Length > 0)
			{
				panelEmirates.Enabled = true;
			}
			else
			{
				panelEmirates.Enabled = false;
			}
		}

		private void dateTimeBirthDate_ValueChanged(object sender, EventArgs e)
		{
			if (!dateTimeBirthDate.Checked)
			{
				textBoxAge.Clear();
				return;
			}
			DateTime value = dateTimeBirthDate.Value;
			TimeSpan timeSpan = DateTime.Today - value;
			int num = 0;
			if (timeSpan.Days > 0)
			{
				num = timeSpan.Days / 365;
			}
			if (num > 0)
			{
				textBoxAge.Text = num.ToString() + " Years";
			}
			else
			{
				textBoxAge.Clear();
			}
		}

		private void comboBoxSelectionStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSelectionStatus.SelectedID == 3 || comboBoxSelectionStatus.SelectedID == 4)
			{
				UltraGroupBox ultraGroupBox = panelVisaMOL;
				bool enabled = panelVisaIMG.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelVisaMOL;
				bool enabled = panelVisaIMG.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void textBoxLastName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxGivenName.Text + " " + textBoxSurName.Text + " " + textBoxFatherName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxGivenName.Text.Trim() == "" && textBoxSurName.Text == "" && textBoxFatherName.Text == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void EventHelper_CandidateAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				_ = dataSet.Tables[0].Rows[0];
			}
		}

		private void CandidateDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxGender.LoadData();
				comboBoxMaritalStatus.LoadData();
				comboBoxSelectionStatus.LoadData();
				comboBoxAgentThrough.ShowQuickAdd = true;
				comboBoxAgentThrough.LoadData();
				comboBoxvisaType.LoadData();
				SetSecurity();
				dataGridPayrollItem.SetupUI();
				SetupPayrollItemGrid();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm();
					textBoxCode.Focus();
				}
			}
			catch (Exception e2)
			{
				dataGridPayrollItem.LoadLayoutFailed = true;
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

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.CandidateTable.Rows[0];
				textBoxCode.Text = "VS" + dataRow["CandidateID"].ToString();
				textBoxPassportNo.Text = dataRow["PassportNo"].ToString();
				textBoxGivenName.Text = dataRow["GivenName"].ToString();
				textBoxSurName.Text = dataRow["SurName"].ToString();
				comboBoxNationality.SelectedID = dataRow["NationalityID"].ToString();
				comboBoxGender.SelectedGender = char.Parse(dataRow["Gender"].ToString());
				if (dataRow["BirthDate"] != DBNull.Value)
				{
					dateTimeBirthDate.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
					dateTimeBirthDate.Checked = true;
				}
				else
				{
					dateTimeBirthDate.Checked = false;
				}
				textBoxBirthPlace.Text = dataRow["BirthPlace"].ToString();
				textBoxPPIssuePlace.Text = dataRow["PassportPlaceOfIssue"].ToString();
				if (dataRow["PassportIssueDate"] != DBNull.Value)
				{
					dateTimePPIssueDate.Value = DateTime.Parse(dataRow["PassportIssueDate"].ToString());
					dateTimePPIssueDate.Checked = true;
				}
				else
				{
					dateTimePPIssueDate.Checked = false;
				}
				if (dataRow["PassportExpiryDate"] != DBNull.Value)
				{
					dateTimePPExpiryDate.Value = DateTime.Parse(dataRow["PassportExpiryDate"].ToString());
					dateTimePPExpiryDate.Checked = true;
				}
				else
				{
					dateTimePPExpiryDate.Checked = false;
				}
				textBoxFatherName.Text = dataRow["FatherName"].ToString();
				textBoxMotherName.Text = dataRow["MotherName"].ToString();
				textBoxSpouseName.Text = dataRow["SpouseName"].ToString();
				textBoxPPAddress.Text = dataRow["PassportAddress"].ToString();
				textBoxNote.Text = dataRow["Notes"].ToString();
				if (dataRow["SelectionStatus"] != DBNull.Value)
				{
					comboBoxSelectionStatus.SelectedID = int.Parse(dataRow["SelectionStatus"].ToString());
				}
				else
				{
					comboBoxSelectionStatus.SelectedID = -1;
				}
				if (dataRow["SelectedOn"] != DBNull.Value)
				{
					dateTimeSelectedOn.Value = DateTime.Parse(dataRow["SelectedOn"].ToString());
					dateTimeSelectedOn.Checked = true;
				}
				else
				{
					dateTimeSelectedOn.Checked = false;
				}
				textBoxSelectedAt.Text = dataRow["SelectedAt"].ToString();
				if (dataRow["ThroughAgent"] != DBNull.Value)
				{
					comboBoxAgentThrough.SelectedID = dataRow["ThroughAgent"].ToString();
				}
				else
				{
					comboBoxAgentThrough.Clear();
				}
				comboBoxPositionActual.SelectedID = dataRow["ActualDesignation"].ToString();
				comboBoxDesignation.SelectedID = dataRow["DesignationID"].ToString();
				comboBoxvisaType.SelectedID = byte.Parse(dataRow["VisaSelectStatus"].ToString());
				textBoxRemarks.Text = dataRow["Remarks"].ToString();
				comboBoxQualification.SelectedID = dataRow["QualificationID"].ToString();
				comboBoxLanguage.SelectedID = dataRow["LanguageID"].ToString();
				numericExperienceLocal.Text = dataRow["ExperienceLocal"].ToString();
				numericExperienceAbroad.Text = dataRow["ExperienceAbroad"].ToString();
				if (dataRow["ApplicationTypingDateMOL"] != DBNull.Value)
				{
					dateTimeApplTypingDateMOL.Value = DateTime.Parse(dataRow["ApplicationTypingDateMOL"].ToString());
					dateTimeApplTypingDateMOL.Checked = true;
				}
				else
				{
					dateTimeApplTypingDateMOL.Checked = false;
				}
				textBoxMOLMBNo.Text = dataRow["MBNumberMOL"].ToString();
				comboBoxSponsor.SelectedID = dataRow["SponsorID"].ToString();
				comboBoxGroup.SelectedID = dataRow["GroupID"].ToString();
				comboBoxPositionVisa.SelectedID = dataRow["VisaDesignation"].ToString();
				if (dataRow["ApprovalDateMOL"] != DBNull.Value)
				{
					dateTimeApprovalDateMOL.Value = DateTime.Parse(dataRow["ApprovalDateMOL"].ToString());
					dateTimeApprovalDateMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalDateMOL.Checked = false;
				}
				if (dataRow["ApprovalValidTillMOL"] != DBNull.Value)
				{
					dateTimeApprovalValidTillMOL.Value = DateTime.Parse(dataRow["ApprovalValidTillMOL"].ToString());
					dateTimeApprovalValidTillMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalValidTillMOL.Checked = false;
				}
				textBoxTempWPNo.Text = dataRow["TempWPNumber"].ToString();
				if (dataRow["ApprovalFeePaidOnMOL"] != DBNull.Value)
				{
					dateTimeApprovalFeePaidOnMOL.Value = DateTime.Parse(dataRow["ApprovalFeePaidOnMOL"].ToString());
					dateTimeApprovalFeePaidOnMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalFeePaidOnMOL.Checked = false;
				}
				if (dataRow["BGPaidOnMOL"] != DBNull.Value)
				{
					dateTimeBGPaidOnMOL.Value = DateTime.Parse(dataRow["BGPaidOnMOL"].ToString());
					dateTimeBGPaidOnMOL.Checked = true;
				}
				else
				{
					dateTimeBGPaidOnMOL.Checked = false;
				}
				if (dataRow["BGTypeMOL"] != DBNull.Value)
				{
					comboBoxBGTypeMOL.SelectedItem = dataRow["BGTypeMOL"].ToString();
				}
				else
				{
					comboBoxBGTypeMOL.SelectedIndex = -1;
				}
				if (dataRow["VisaAppliedThroughIMG"] != DBNull.Value)
				{
					comboBoxVisaAppliedThroughIMG.SelectedItem = dataRow["VisaAppliedThroughIMG"].ToString();
				}
				else
				{
					comboBoxVisaAppliedThroughIMG.SelectedIndex = -1;
				}
				if (dataRow["VisaPostedOnIMG"] != DBNull.Value)
				{
					dateTimeVisaPostedOn.Value = DateTime.Parse(dataRow["VisaPostedOnIMG"].ToString());
					dateTimeVisaPostedOn.Checked = true;
				}
				else
				{
					dateTimeVisaPostedOn.Checked = false;
				}
				if (dataRow["VisaApprovedOnIMG"] != DBNull.Value)
				{
					dateTimeApprovedOn.Value = DateTime.Parse(dataRow["VisaApprovedOnIMG"].ToString());
					dateTimeApprovedOn.Checked = true;
				}
				else
				{
					dateTimeApprovedOn.Checked = false;
				}
				textBoxVisaIssuePlaceIMG.Text = dataRow["VisaIssuePlaceIMG"].ToString();
				textBoxVisaNumber.Text = dataRow["VisaNumber"].ToString();
				if (dataRow["VisaIssueDateIMG"] != DBNull.Value)
				{
					dateTimeVisaIssueDate.Value = DateTime.Parse(dataRow["VisaIssueDateIMG"].ToString());
					dateTimeVisaIssueDate.Checked = true;
				}
				else
				{
					dateTimeVisaIssueDate.Checked = false;
				}
				if (dataRow["VisaExpiryDateIMG"] != DBNull.Value)
				{
					dateTimeVisaExpiryDate.Value = DateTime.Parse(dataRow["VisaExpiryDateIMG"].ToString());
					dateTimeVisaExpiryDate.Checked = true;
				}
				else
				{
					dateTimeVisaExpiryDate.Checked = false;
				}
				textBoxUIDNumberIMG.Text = dataRow["UIDNumberIMG"].ToString();
				if (dataRow["VisaCopyToAgentOn"] != DBNull.Value)
				{
					dateTimeVisaCopyToAgentOn.Value = DateTime.Parse(dataRow["VisaCopyToAgentOn"].ToString());
					dateTimeVisaCopyToAgentOn.Checked = true;
				}
				else
				{
					dateTimeVisaCopyToAgentOn.Checked = false;
				}
				textBoxEmployeeNo.Text = dataRow["EmployeeNo"].ToString();
				IsExist = bool.Parse(dataRow["IsExist"].ToString());
				sponsorComboBox.SelectedID = dataRow["SponsorID"].ToString();
				if (dataRow["ArrivedOn"] != DBNull.Value)
				{
					dateTimeArrivedOn.Value = DateTime.Parse(dataRow["ArrivedOn"].ToString());
					dateTimeArrivedOn.Checked = true;
				}
				else
				{
					dateTimeArrivedOn.Checked = false;
				}
				if (dataRow["ArrivalPort"] != DBNull.Value)
				{
					comboBoxArrivalPort.SelectedID = dataRow["ArrivalPort"].ToString();
				}
				else
				{
					comboBoxArrivalPort.Clear();
				}
				if (dataRow["Category"] != DBNull.Value)
				{
					comboBoxCategory.SelectedID = dataRow["Category"].ToString();
				}
				else
				{
					comboBoxCategory.Clear();
				}
				textBoxHealthCardNo.Text = dataRow["HealthCardNo"].ToString();
				if (dataRow["MedicalTypingOn"] != DBNull.Value)
				{
					dateTimeMedicalTypingOn.Value = DateTime.Parse(dataRow["MedicalTypingOn"].ToString());
					dateTimeMedicalTypingOn.Checked = true;
				}
				else
				{
					dateTimeMedicalTypingOn.Checked = false;
				}
				if (dataRow["MedicalAttendedOn"] != DBNull.Value)
				{
					dateTimeMedicalAttendedOn.Value = DateTime.Parse(dataRow["MedicalAttendedOn"].ToString());
					dateTimeMedicalAttendedOn.Checked = true;
				}
				else
				{
					dateTimeMedicalAttendedOn.Checked = false;
				}
				if (dataRow["MedicalCollectedOn"] != DBNull.Value)
				{
					dateTimeMedicalCollectedOn.Value = DateTime.Parse(dataRow["MedicalCollectedOn"].ToString());
					dateTimeMedicalCollectedOn.Checked = true;
				}
				else
				{
					dateTimeMedicalCollectedOn.Checked = false;
				}
				if (dataRow["MedicalResult"] != DBNull.Value)
				{
					comboBoxMedicalResult.SelectedItem = dataRow["MedicalResult"].ToString();
				}
				else
				{
					comboBoxMedicalResult.SelectedIndex = -1;
				}
				textBoxMedicalNote.Text = dataRow["MedicalNote"].ToString();
				if (dataRow["ApplicationTypedOnEID"] != DBNull.Value)
				{
					dateTimeApplTypingDateEID.Value = DateTime.Parse(dataRow["ApplicationTypedOnEID"].ToString());
					dateTimeApplTypingDateEID.Checked = true;
				}
				else
				{
					dateTimeApplTypingDateEID.Checked = false;
				}
				if (dataRow["AttendedForEID"] != DBNull.Value)
				{
					dateTimeAttendedDateEID.Value = DateTime.Parse(dataRow["AttendedForEID"].ToString());
					dateTimeAttendedDateEID.Checked = true;
				}
				else
				{
					dateTimeAttendedDateEID.Checked = false;
				}
				if (dataRow["CollectedOnEID"] != DBNull.Value)
				{
					dateTimeCollectedOnEID.Value = DateTime.Parse(dataRow["CollectedOnEID"].ToString());
					dateTimeCollectedOnEID.Checked = true;
				}
				else
				{
					dateTimeCollectedOnEID.Checked = false;
				}
				textBoxNationalID.Text = dataRow["NationalID"].ToString();
				if (dataRow["NationalIDValidity"] != DBNull.Value)
				{
					dateTimeValidityEID.Value = DateTime.Parse(dataRow["NationalIDValidity"].ToString());
					dateTimeValidityEID.Checked = true;
				}
				else
				{
					dateTimeValidityEID.Checked = false;
				}
				if (dataRow["AGTType"] != DBNull.Value)
				{
					comboBoxAGTType.SelectedItem = dataRow["AGTType"].ToString();
				}
				else
				{
					comboBoxAGTType.SelectedIndex = -1;
				}
				textBoxAGTMBNo.Text = dataRow["MBNumberAGT"].ToString();
				if (dataRow["AGTTypedOn"] != DBNull.Value)
				{
					dateTimeAGTTypedOn.Value = DateTime.Parse(dataRow["AGTTypedOn"].ToString());
					dateTimeAGTTypedOn.Checked = true;
				}
				else
				{
					dateTimeAGTTypedOn.Checked = false;
				}
				if (dataRow["AGTSubmittedOn"] != DBNull.Value)
				{
					dateTimeAGTSubmittedOn.Value = DateTime.Parse(dataRow["AGTSubmittedOn"].ToString());
					dateTimeAGTSubmittedOn.Checked = true;
				}
				else
				{
					dateTimeAGTSubmittedOn.Checked = false;
				}
				textBoxWPNo.Text = dataRow["WPNumber"].ToString();
				textBoxPersonIDNo.Text = dataRow["PersonalIDNo"].ToString();
				textBoxWPIssuePlace.Text = dataRow["WPIssuePlace"].ToString();
				if (dataRow["WPIssueDate"] != DBNull.Value)
				{
					dateTimeWPIssueDate.Value = DateTime.Parse(dataRow["WPIssueDate"].ToString());
					dateTimeWPIssueDate.Checked = true;
				}
				else
				{
					dateTimeWPIssueDate.Checked = false;
				}
				if (dataRow["WPExpiryDate"] != DBNull.Value)
				{
					dateTimeWPExpiryDate.Value = DateTime.Parse(dataRow["WPExpiryDate"].ToString());
					dateTimeWPExpiryDate.Checked = true;
				}
				else
				{
					dateTimeWPExpiryDate.Checked = false;
				}
				if (dataRow["RPProcessType"] != DBNull.Value)
				{
					comboBoxProcessType.SelectedItem = dataRow["RPProcessType"].ToString();
				}
				else
				{
					comboBoxProcessType.SelectedIndex = -1;
				}
				if (dataRow["ApplicationPostedOnRP"] != DBNull.Value)
				{
					dateTimeApplPostedOnRP.Value = DateTime.Parse(dataRow["ApplicationPostedOnRP"].ToString());
					dateTimeApplPostedOnRP.Checked = true;
				}
				else
				{
					dateTimeApplPostedOnRP.Checked = false;
				}
				if (dataRow["ApplicationApprovedOnRP"] != DBNull.Value)
				{
					dateTimeApplApprovedOnRP.Value = DateTime.Parse(dataRow["ApplicationApprovedOnRP"].ToString());
					dateTimeApplApprovedOnRP.Checked = true;
				}
				else
				{
					dateTimeApplApprovedOnRP.Checked = false;
				}
				if (dataRow["SubmittedToZajil"] != DBNull.Value)
				{
					dateTimeSubmittedZajilOnRP.Value = DateTime.Parse(dataRow["SubmittedToZajil"].ToString());
					dateTimeSubmittedZajilOnRP.Checked = true;
				}
				else
				{
					dateTimeSubmittedZajilOnRP.Checked = false;
				}
				if (dataRow["PassportCollectedOn"] != DBNull.Value)
				{
					dateTimePassportCollectedOnRP.Value = DateTime.Parse(dataRow["PassportCollectedOn"].ToString());
					dateTimePassportCollectedOnRP.Checked = true;
				}
				else
				{
					dateTimePassportCollectedOnRP.Checked = false;
				}
				textBoxRPIssuePlace.Text = dataRow["RPIssuePlace"].ToString();
				if (dataRow["RPIssueDate"] != DBNull.Value)
				{
					dateTimeRPIssueDate.Value = DateTime.Parse(dataRow["RPIssueDate"].ToString());
					dateTimeRPIssueDate.Checked = true;
				}
				else
				{
					dateTimeRPIssueDate.Checked = false;
				}
				if (dataRow["RPExpiryDate"] != DBNull.Value)
				{
					dateTimeRPExpiryDate.Value = DateTime.Parse(dataRow["RPExpiryDate"].ToString());
					dateTimeRPExpiryDate.Checked = true;
				}
				else
				{
					dateTimeRPExpiryDate.Checked = false;
				}
				if (dataRow["Photo"] != DBNull.Value)
				{
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					bool visible = linkRemovePicture.Enabled = true;
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkLoadImage;
					bool visible = linkRemovePicture.Enabled = false;
					ultraFormattedLinkLabel2.Visible = visible;
				}
				pictureBoxPhoto.Image = null;
				SetHeaderName();
				if (currentData.Tables["UDF"].Rows.Count > 0)
				{
					_ = currentData.Tables["UDF"].Rows[0];
					foreach (DataColumn column in currentData.Tables["UDF"].Columns)
					{
						_ = (column.ColumnName == "EntityID");
					}
				}
				else
				{
					udfEntryGrid.ClearData();
				}
				if (dataRow["IsCancelled"] != DBNull.Value)
				{
					IsCancelled = bool.Parse(dataRow["IsCancelled"].ToString());
				}
				else
				{
					IsCancelled = false;
				}
				DataTable dataTable = dataGridPayrollItem.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Candidate_Salary"].Rows)
				{
					byte result = 0;
					byte.TryParse(row["PayType"].ToString(), out result);
					if (result == 1)
					{
						DataRow dataRow3 = dataTable.NewRow();
						foreach (DataColumn column2 in dataRow3.Table.Columns)
						{
							_ = column2;
							dataRow3["PayrollItem"] = row["PayrollItemID"];
							dataRow3["Description"] = row["PayrollItemName"];
							if (row["Amount"] != DBNull.Value)
							{
								object obj3 = dataRow3["Amount"] = (dataRow3["New Amount"] = decimal.Round(decimal.Parse(row["Amount"].ToString()), 2));
							}
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
				}
				dataTable.AcceptChanges();
				ShowTotalSalary();
			}
		}

		private void FillAddressData(DataRow row)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CandidateData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CandidateTable.Rows[0] : currentData.CandidateTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PassportNo"] = textBoxPassportNo.Text.Trim();
				dataRow["GivenName"] = textBoxGivenName.Text.Trim();
				dataRow["SurName"] = textBoxSurName.Text.Trim();
				if (comboBoxNationality.SelectedID != "")
				{
					dataRow["NationalityID"] = comboBoxNationality.SelectedID;
				}
				else
				{
					dataRow["NationalityID"] = DBNull.Value;
				}
				if (comboBoxGender.SelectedID.ToString() != "")
				{
					dataRow["Gender"] = comboBoxGender.SelectedGender;
				}
				else
				{
					dataRow["Gender"] = 'M';
				}
				if (dateTimeBirthDate.Checked)
				{
					dataRow["BirthDate"] = dateTimeBirthDate.Value;
				}
				else
				{
					dataRow["BirthDate"] = DBNull.Value;
				}
				dataRow["GroupID"] = comboBoxGroup.SelectedID;
				dataRow["BirthPlace"] = textBoxBirthPlace.Text.Trim();
				dataRow["PassportPlaceOfIssue"] = textBoxPPIssuePlace.Text.Trim();
				if (dateTimePPIssueDate.Checked)
				{
					dataRow["PassportIssueDate"] = dateTimePPIssueDate.Value;
				}
				else
				{
					dataRow["PassportIssueDate"] = DBNull.Value;
				}
				if (dateTimePPExpiryDate.Checked)
				{
					dataRow["PassportExpiryDate"] = dateTimePPExpiryDate.Value;
				}
				else
				{
					dataRow["PassportExpiryDate"] = DBNull.Value;
				}
				dataRow["VisaStatus"] = comboBoxvisaType.SelectedID;
				dataRow["FatherName"] = textBoxFatherName.Text.Trim();
				dataRow["MotherName"] = textBoxMotherName.Text.Trim();
				dataRow["SpouseName"] = textBoxSpouseName.Text.Trim();
				dataRow["PassportAddress"] = textBoxPPAddress.Text.Trim();
				dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
				dataRow["Notes"] = textBoxNote.Text.Trim();
				if (comboBoxDesignation.SelectedID != "")
				{
					dataRow["DesignationID"] = comboBoxDesignation.SelectedID;
				}
				else
				{
					dataRow["DesignationID"] = DBNull.Value;
				}
				dataRow["IsExist"] = isExist;
				if (comboBoxSelectionStatus.SelectedID > 0)
				{
					dataRow["SelectionStatus"] = comboBoxSelectionStatus.SelectedID;
				}
				else
				{
					dataRow["SelectionStatus"] = DBNull.Value;
				}
				if (dateTimeSelectedOn.Checked)
				{
					dataRow["SelectedOn"] = dateTimeSelectedOn.Value;
				}
				else
				{
					dataRow["SelectedOn"] = DBNull.Value;
				}
				dataRow["SelectedAt"] = textBoxSelectedAt.Text.Trim();
				dataRow["ThroughAgent"] = comboBoxAgentThrough.SelectedID;
				if (comboBoxPositionActual.SelectedID != "")
				{
					dataRow["ActualDesignation"] = comboBoxPositionActual.SelectedID;
				}
				else
				{
					dataRow["ActualDesignation"] = DBNull.Value;
				}
				dataRow["Remarks"] = textBoxRemarks.Text.Trim();
				dataRow["QualificationID"] = comboBoxQualification.SelectedID;
				dataRow["LanguageID"] = comboBoxLanguage.SelectedID;
				dataRow["ExperienceLocal"] = ((numericExperienceLocal.Text != string.Empty) ? Convert.ToDecimal(numericExperienceLocal.Text) : 0m);
				dataRow["ExperienceAbroad"] = ((numericExperienceAbroad.Text != string.Empty) ? Convert.ToDecimal(numericExperienceAbroad.Text) : 0m);
				if (dateTimeApplTypingDateMOL.Checked)
				{
					dataRow["ApplicationTypingDateMOL"] = dateTimeApplTypingDateMOL.Value;
				}
				else
				{
					dataRow["ApplicationTypingDateMOL"] = DBNull.Value;
				}
				dataRow["MBNumberMOL"] = textBoxMOLMBNo.Text.Trim();
				dataRow["SponsorID"] = comboBoxSponsor.SelectedID;
				dataRow["SponsorID"] = sponsorComboBox.SelectedID;
				if (comboBoxPositionVisa.SelectedID != "")
				{
					dataRow["VisaDesignation"] = comboBoxPositionVisa.SelectedID;
				}
				else
				{
					dataRow["VisaDesignation"] = DBNull.Value;
				}
				if (dateTimeApprovalDateMOL.Checked)
				{
					dataRow["ApprovalDateMOL"] = dateTimeApprovalDateMOL.Value;
				}
				else
				{
					dataRow["ApprovalDateMOL"] = DBNull.Value;
				}
				if (dateTimeApprovalValidTillMOL.Checked)
				{
					dataRow["ApprovalValidTillMOL"] = dateTimeApprovalValidTillMOL.Value;
				}
				else
				{
					dataRow["ApprovalValidTillMOL"] = DBNull.Value;
				}
				dataRow["TempWPNumber"] = textBoxTempWPNo.Text.Trim();
				if (dateTimeApprovalFeePaidOnMOL.Checked)
				{
					dataRow["ApprovalFeePaidOnMOL"] = dateTimeApprovalFeePaidOnMOL.Value;
				}
				else
				{
					dataRow["ApprovalFeePaidOnMOL"] = DBNull.Value;
				}
				if (dateTimeBGPaidOnMOL.Checked)
				{
					dataRow["BGPaidOnMOL"] = dateTimeBGPaidOnMOL.Value;
				}
				else
				{
					dataRow["BGPaidOnMOL"] = DBNull.Value;
				}
				dataRow["BGTypeMOL"] = comboBoxBGTypeMOL.SelectedItem;
				dataRow["VisaAppliedThroughIMG"] = comboBoxVisaAppliedThroughIMG.SelectedItem;
				if (dateTimeVisaPostedOn.Checked)
				{
					dataRow["VisaPostedOnIMG"] = dateTimeVisaPostedOn.Value;
				}
				else
				{
					dataRow["VisaPostedOnIMG"] = DBNull.Value;
				}
				if (dateTimeApprovedOn.Checked)
				{
					dataRow["VisaApprovedOnIMG"] = dateTimeApprovedOn.Value;
				}
				else
				{
					dataRow["VisaApprovedOnIMG"] = DBNull.Value;
				}
				dataRow["VisaIssuePlaceIMG"] = textBoxVisaIssuePlaceIMG.Text.Trim();
				dataRow["VisaNumber"] = textBoxVisaNumber.Text.Trim();
				if (dateTimeVisaIssueDate.Checked)
				{
					dataRow["VisaIssueDateIMG"] = dateTimeVisaIssueDate.Value;
				}
				else
				{
					dataRow["VisaIssueDateIMG"] = DBNull.Value;
				}
				if (dateTimeVisaExpiryDate.Checked)
				{
					dataRow["VisaExpiryDateIMG"] = dateTimeVisaExpiryDate.Value;
				}
				else
				{
					dataRow["VisaExpiryDateIMG"] = DBNull.Value;
				}
				dataRow["UIDNumberIMG"] = textBoxUIDNumberIMG.Text.Trim();
				if (dateTimeVisaCopyToAgentOn.Checked)
				{
					dataRow["VisaCopyToAgentOn"] = dateTimeVisaCopyToAgentOn.Value;
				}
				else
				{
					dataRow["VisaCopyToAgentOn"] = DBNull.Value;
				}
				if (dateTimeArrivedOn.Checked)
				{
					dataRow["ArrivedOn"] = dateTimeArrivedOn.Value;
				}
				else
				{
					dataRow["ArrivedOn"] = DBNull.Value;
				}
				dataRow["ArrivalPort"] = comboBoxArrivalPort.SelectedID;
				dataRow["Category"] = comboBoxCategory.SelectedID;
				dataRow["EmployeeNo"] = textBoxEmployeeNo.Text.Trim();
				dataRow["HealthCardNo"] = textBoxHealthCardNo.Text;
				if (dateTimeMedicalTypingOn.Checked)
				{
					dataRow["MedicalTypingOn"] = dateTimeMedicalTypingOn.Value;
				}
				else
				{
					dataRow["MedicalTypingOn"] = DBNull.Value;
				}
				if (dateTimeMedicalAttendedOn.Checked)
				{
					dataRow["MedicalAttendedOn"] = dateTimeMedicalAttendedOn.Value;
				}
				else
				{
					dataRow["MedicalAttendedOn"] = DBNull.Value;
				}
				if (dateTimeMedicalCollectedOn.Checked)
				{
					dataRow["MedicalCollectedOn"] = dateTimeMedicalCollectedOn.Value;
				}
				else
				{
					dataRow["MedicalCollectedOn"] = DBNull.Value;
				}
				dataRow["MedicalResult"] = comboBoxMedicalResult.SelectedItem;
				dataRow["MedicalNote"] = textBoxMedicalNote.Text.Trim();
				if (dateTimeApplTypingDateEID.Checked)
				{
					dataRow["ApplicationTypedOnEID"] = dateTimeApplTypingDateEID.Value;
				}
				else
				{
					dataRow["ApplicationTypedOnEID"] = DBNull.Value;
				}
				if (dateTimeAttendedDateEID.Checked)
				{
					dataRow["AttendedForEID"] = dateTimeAttendedDateEID.Value;
				}
				else
				{
					dataRow["AttendedForEID"] = DBNull.Value;
				}
				if (dateTimeCollectedOnEID.Checked)
				{
					dataRow["CollectedOnEID"] = dateTimeCollectedOnEID.Value;
				}
				else
				{
					dataRow["CollectedOnEID"] = DBNull.Value;
				}
				dataRow["NationalID"] = textBoxNationalID.Text.Trim();
				if (dateTimeValidityEID.Checked)
				{
					dataRow["NationalIDValidity"] = dateTimeValidityEID.Value;
				}
				else
				{
					dataRow["NationalIDValidity"] = DBNull.Value;
				}
				dataRow["AGTType"] = comboBoxAGTType.SelectedItem;
				dataRow["MBNumberAGT"] = textBoxAGTMBNo.Text.Trim();
				if (dateTimeAGTTypedOn.Checked)
				{
					dataRow["AGTTypedOn"] = dateTimeAGTTypedOn.Value;
				}
				else
				{
					dataRow["AGTTypedOn"] = DBNull.Value;
				}
				if (dateTimeAGTSubmittedOn.Checked)
				{
					dataRow["AGTSubmittedOn"] = dateTimeAGTSubmittedOn.Value;
				}
				else
				{
					dataRow["AGTSubmittedOn"] = DBNull.Value;
				}
				dataRow["WPNumber"] = textBoxWPNo.Text.Trim();
				dataRow["PersonalIDNo"] = textBoxPersonIDNo.Text.Trim();
				dataRow["WPIssuePlace"] = textBoxWPIssuePlace.Text.Trim();
				if (dateTimeWPIssueDate.Checked)
				{
					dataRow["WPIssueDate"] = dateTimeWPIssueDate.Value;
				}
				else
				{
					dataRow["WPIssueDate"] = DBNull.Value;
				}
				if (dateTimeWPExpiryDate.Checked)
				{
					dataRow["WPExpiryDate"] = dateTimeWPExpiryDate.Value;
				}
				else
				{
					dataRow["WPExpiryDate"] = DBNull.Value;
				}
				dataRow["RPProcessType"] = comboBoxProcessType.SelectedItem;
				if (dateTimeApplPostedOnRP.Checked)
				{
					dataRow["ApplicationPostedOnRP"] = dateTimeApplPostedOnRP.Value;
				}
				else
				{
					dataRow["ApplicationPostedOnRP"] = DBNull.Value;
				}
				if (dateTimeApplApprovedOnRP.Checked)
				{
					dataRow["ApplicationApprovedOnRP"] = dateTimeApplApprovedOnRP.Value;
				}
				else
				{
					dataRow["ApplicationApprovedOnRP"] = DBNull.Value;
				}
				if (dateTimeSubmittedZajilOnRP.Checked)
				{
					dataRow["SubmittedToZajil"] = dateTimeSubmittedZajilOnRP.Value;
				}
				else
				{
					dataRow["SubmittedToZajil"] = DBNull.Value;
				}
				if (dateTimePassportCollectedOnRP.Checked)
				{
					dataRow["PassportCollectedOn"] = dateTimePassportCollectedOnRP.Value;
				}
				else
				{
					dataRow["PassportCollectedOn"] = DBNull.Value;
				}
				dataRow["RPIssuePlace"] = textBoxRPIssuePlace.Text.Trim();
				if (dateTimeRPIssueDate.Checked)
				{
					dataRow["RPIssueDate"] = dateTimeRPIssueDate.Value;
				}
				else
				{
					dataRow["RPIssueDate"] = DBNull.Value;
				}
				if (dateTimeRPExpiryDate.Checked)
				{
					dataRow["RPExpiryDate"] = dateTimeRPExpiryDate.Value;
				}
				else
				{
					dataRow["RPExpiryDate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CandidateTable.Rows.Add(dataRow);
				}
				currentData.CandidateSalaryTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (!(row.Cells["PayrollItem"].Value.ToString() == ""))
					{
						if (!currentData.Tables.Contains("Candidate_Salary"))
						{
							CandidateData candidateData = new CandidateData();
							currentData.Tables.Add(candidateData.Tables[0].Clone());
						}
						dataRow = currentData.Tables["Candidate_Salary"].NewRow();
						dataRow.BeginEdit();
						dataRow["PayType"] = 1;
						dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
						dataRow["PayrollItemID"] = row.Cells["PayrollItem"].Value.ToString();
						dataRow["Amount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.Tables["Candidate_Salary"].Rows.Add(dataRow);
					}
				}
				DataRow dataRow2 = null;
				DataTable dataTable = null;
				if (!isNewRecord)
				{
					dataTable = currentData.Tables["UDF"];
				}
				else
				{
					dataTable = currentData.Tables.Add("UDF");
					foreach (UltraGridCell field in udfEntryGrid.Fields)
					{
						dataTable.Columns.Add(field.Column.Key, field.Column.DataType);
					}
				}
				dataRow2 = currentData.Tables["UDF"].NewRow();
				foreach (UltraGridCell field2 in udfEntryGrid.Fields)
				{
					dataRow2[field2.Column.Key] = udfEntryGrid.Fields[field2.Column.Key].Value;
				}
				dataRow2["EntityID"] = textBoxCode.Text;
				dataRow2.EndEdit();
				currentData.Tables["UDF"].Rows.Add(dataRow2);
				if (dateTimeArrivedOn.Checked && !string.IsNullOrEmpty(textBoxEmployeeNo.Text) && !IsExist)
				{
					dataRow = currentData.EmployeeTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
					dataRow["FirstName"] = textBoxGivenName.Text;
					dataRow["MiddleName"] = textBoxSurName.Text;
					dataRow["LastName"] = textBoxFatherName.Text;
					dataRow["NationalID"] = textBoxNationalID.Text;
					dataRow["Status"] = 1;
					if (comboBoxGender.SelectedID.ToString() != "")
					{
						dataRow["Gender"] = comboBoxGender.SelectedGender;
					}
					else
					{
						dataRow["Gender"] = 'M';
					}
					if (dateTimeBirthDate.Checked)
					{
						dataRow["BirthDate"] = dateTimeBirthDate.Value;
					}
					else
					{
						dataRow["BirthDate"] = DBNull.Value;
					}
					dataRow["BirthPlace"] = textBoxBirthPlace.Text;
					if (dateTimeArrivedOn.Checked)
					{
						dataRow["JoiningDate"] = dateTimeArrivedOn.Value;
					}
					else
					{
						dataRow["JoiningDate"] = DBNull.Value;
					}
					if (comboBoxGroup.SelectedID != "")
					{
						dataRow["GroupID"] = comboBoxGroup.SelectedID;
					}
					else
					{
						dataRow["GroupID"] = DBNull.Value;
					}
					if (comboBoxNationality.SelectedID != "")
					{
						dataRow["NationalityID"] = comboBoxNationality.SelectedID;
					}
					else
					{
						dataRow["NationalityID"] = DBNull.Value;
					}
					dataRow["SpouseName"] = textBoxSpouseName.Text;
					if (comboBoxMaritalStatus.SelectedID > 0)
					{
						dataRow["MaritalStatus"] = comboBoxMaritalStatus.SelectedID;
					}
					else
					{
						dataRow["MaritalStatus"] = DBNull.Value;
					}
					if (comboBoxReligion.SelectedID != "")
					{
						dataRow["ReligionID"] = comboBoxReligion.SelectedID;
					}
					else
					{
						dataRow["ReligionID"] = DBNull.Value;
					}
					dataRow["BloodGroup"] = textBoxBloodGroup.Text;
					dataRow["Notes"] = textBoxNote.Text;
					dataRow["ContractType"] = comboBoxCategory.SelectedID;
					dataRow["PositionID"] = comboBoxPositionActual.SelectedID;
					dataRow["PrimaryAddressID"] = "PRIMARY";
					if (comboBoxQualification.SelectedID != "")
					{
						dataRow["Qualification"] = comboBoxQualification.SelectedID;
					}
					else
					{
						dataRow["Qualification"] = DBNull.Value;
					}
					dataRow["SponsorID"] = sponsorComboBox.SelectedID;
					if (!isExist)
					{
						dataRow["DateCreated"] = DateTime.Now;
						dataRow["CreatedBy"] = Global.CurrentUser;
					}
					dataRow.EndEdit();
					currentData.EmployeeTable.Rows.Add(dataRow);
					dataRow = currentData.EmployeeAddressTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
					dataRow["AddressID"] = "PRIMARY";
					dataRow["Address1"] = textBoxPPAddress.Text;
					dataRow.EndEdit();
					currentData.EmployeeAddressTable.Rows.Add(dataRow);
					currentData.EmployeePayrollItemDetail.Rows.Clear();
					foreach (UltraGridRow row2 in dataGridPayrollItem.Rows)
					{
						if (!(row2.Cells["PayrollItem"].Value.ToString() == ""))
						{
							if (!currentData.Tables.Contains("Employee_PayrollItem_Detail"))
							{
								EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
								currentData.Tables.Add(employeePayrollItemDetailData.Tables[0].Clone());
							}
							dataRow = currentData.Tables["Employee_PayrollItem_Detail"].NewRow();
							dataRow.BeginEdit();
							dataRow["PayType"] = 1;
							dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
							dataRow["PayrollItemID"] = row2.Cells["PayrollItem"].Value.ToString();
							dataRow["Amount"] = decimal.Parse(row2.Cells["Amount"].Value.ToString());
							dataRow["RowIndex"] = row2.Index;
							dataRow.EndEdit();
							currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
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

		private string GetNextSequenceNumber()
		{
			try
			{
				return Factory.CandidateSystem.GetNextSequenceNumber("Candidate", "CandidateID");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetNextEmployeeNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextSponserEmployeeNumber(sponsorComboBox.SelectedID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
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
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (textBoxCode.Text.Trim() == string.Empty || textBoxGivenName.Text.Trim() == string.Empty || textBoxSurName.Text.Trim() == string.Empty || textBoxPassportNo.Text.Trim() == string.Empty || comboBoxDesignation.SelectedID == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (dateTimePPIssueDate.Value > dateTimePPExpiryDate.Value)
				{
					ErrorHelper.InformationMessage("Passport - Expiry Date should not be less than Issue Date");
					dateTimePPIssueDate.Focus();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "CandidateID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "PassportNo", textBoxPassportNo.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Passport Number already exist.");
					tabPageGeneral.Tab.Selected = true;
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
					flag = Factory.CandidateSystem.CreateCandidate(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Candidate, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CandidateSystem.UpdateCandidate(currentData);
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
				if (!base.IsDisposed && !(id == ""))
				{
					currentData = Factory.CandidateSystem.GetCandidateByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						IsNewRecord = false;
						FillData();
						formManager.ResetDirty();
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
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

		private void SetupPayrollItemGrid()
		{
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("PayrollItem");
			dataTable.Columns.Add("PayrollItem", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridPayrollItem.DataSource = dataTable;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].CharacterCasing = CharacterCasing.Upper;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].ValueList = comboBoxPayrollItem;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Width = checked(25 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Format = "n";
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.Cursor = Cursors.Hand;
		}

		public void RefreshData()
		{
			Refresh();
			if (FormActivator.ProgramLoaded)
			{
				_ = Global.ConStatus;
				_ = 2;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string currentID = textBoxCode.Text.Substring(2);
			LoadData(DatabaseHelper.GetPreviousID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string currentID = textBoxCode.Text.Substring(2);
			LoadData(DatabaseHelper.GetNextID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Candidate", "CandidateID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.CandidateSystem.DeleteCandidate(textBoxCode.Text);
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = "VS" + GetNextSequenceNumber();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			textBoxCity.Clear();
			textBoxComment.Clear();
			textBoxCountry.Clear();
			textBoxEmail.Clear();
			textBoxFax.Clear();
			textBoxMobile.Clear();
			textBoxPhone1.Clear();
			textBoxPhone2.Clear();
			textBoxPostalCode.Clear();
			textBoxState.Clear();
			textBoxTotalSalary.Clear();
			udfEntryGrid.ClearData();
			pictureBoxPhoto.Image = null;
			comboBoxBGTypeMOL.SelectedIndex = -1;
			comboBoxVisaAppliedThroughIMG.SelectedIndex = -1;
			comboBoxAgentThrough.SelectedIndex = -1;
			textBoxGivenName.Clear();
			textBoxSurName.Clear();
			textBoxFatherName.Clear();
			textBoxMotherName.Clear();
			textBoxSpouseName.Clear();
			textBoxBirthPlace.Clear();
			textBoxAge.Clear();
			dateTimeBirthDate.Checked = false;
			comboBoxGroup.Clear();
			comboBoxNationality.Clear();
			comboBoxReligion.Clear();
			comboBoxGender.SelectedGender = 'M';
			comboBoxMaritalStatus.Clear();
			textBoxBloodGroup.Clear();
			textBoxPassportNo.Clear();
			textBoxPPIssuePlace.Clear();
			textBoxPPAddress.Clear();
			dateTimePPIssueDate.Checked = false;
			dateTimePPExpiryDate.Checked = false;
			comboBoxSelectionStatus.SelectedID = 1;
			textBoxSelectedAt.Clear();
			dateTimeSelectedOn.Checked = false;
			dateTimeApplTypingDateMOL.Checked = false;
			dateTimeBGPaidOnMOL.Checked = false;
			textBoxMOLMBNo.Clear();
			dateTimeApprovalDateMOL.Checked = false;
			dateTimeVisaCopyToAgentOn.Checked = false;
			textBoxUIDNumberIMG.Clear();
			dateTimeVisaPostedOn.Checked = false;
			dateTimeApprovedOn.Checked = false;
			textBoxRemarks.Clear();
			comboBoxDesignation.Clear();
			comboBoxvisaType.SelectedID = 1;
			comboBoxSelectionStatus.SelectedIndex = -1;
			comboBoxArrivalPort.Clear();
			comboBoxCategory.Clear();
			dateTimeApprovalValidTillMOL.Checked = false;
			dateTimeVisaIssueDate.Checked = false;
			dateTimeVisaExpiryDate.Checked = false;
			textBoxVisaNumber.Clear();
			textBoxTempWPNo.Clear();
			comboBoxMedicalResult.SelectedIndex = -1;
			textBoxMedicalNote.Clear();
			dateTimeArrivedOn.Checked = false;
			comboBoxProcessType.SelectedIndex = -1;
			dateTimeApplTypingDateEID.Checked = false;
			dateTimeCollectedOnEID.Checked = false;
			textBoxNationalID.Clear();
			dateTimeAttendedDateEID.Checked = false;
			dateTimeValidityEID.Checked = false;
			dateTimeMedicalTypingOn.Checked = false;
			dateTimeMedicalCollectedOn.Checked = false;
			dateTimeMedicalAttendedOn.Checked = false;
			dateTimeApplPostedOnRP.Checked = false;
			dateTimeApplApprovedOnRP.Checked = false;
			dateTimeSubmittedZajilOnRP.Checked = false;
			dateTimePassportCollectedOnRP.Checked = false;
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPExpiryDate.Checked = false;
			textBoxRPIssuePlace.Clear();
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTSubmittedOn.Checked = false;
			textBoxWPNo.Clear();
			textBoxPersonIDNo.Clear();
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPExpiryDate.Checked = false;
			textBoxWPIssuePlace.Clear();
			labelCustomerNameHeader.Text = string.Empty;
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPExpiryDate.Checked = false;
			textBoxRPIssuePlace.Clear();
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTSubmittedOn.Checked = false;
			textBoxWPNo.Clear();
			comboBoxPositionVisa.Clear();
			comboBoxPositionActual.Clear();
			comboBoxAgentThrough.Clear();
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPExpiryDate.Checked = false;
			textBoxWPIssuePlace.Clear();
			comboBoxAGTType.SelectedIndex = -1;
			textBoxAGTMBNo.Clear();
			comboBoxQualification.Clear();
			comboBoxLanguage.Clear();
			numericExperienceLocal.Text = 0m.ToString();
			numericExperienceAbroad.Text = 0m.ToString();
			textBoxEmployeeNo.Clear();
			textBoxPassportNo.Focus();
			comboBoxSponsor.Clear();
			sponsorComboBox.Clear();
			textBoxHealthCardNo.Clear();
			(dataGridPayrollItem.DataSource as DataTable).Rows.Clear();
			isExist = false;
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

		private void CandidateClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void dateTimePickerBirthDate_ValueChanged(object sender, EventArgs e)
		{
			if (!dateTimeBirthDate.Checked)
			{
				textBoxAge.Clear();
				return;
			}
			DateTime value = dateTimeBirthDate.Value;
			TimeSpan timeSpan = DateTime.Today - value;
			int num = 0;
			if (timeSpan.Days > 0)
			{
				num = timeSpan.Days / 365;
			}
			if (num > 0)
			{
				textBoxAge.Text = num.ToString() + " Years";
			}
			else
			{
				textBoxAge.Clear();
			}
		}

		private void dateTimePickerJoiningDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPosition(comboBoxDesignation.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployeeGroup(comboBoxGroup.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonMore_Click(object sender, EventArgs e)
		{
		}

		private void dependentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void skillsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
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
					DataSet candidateAppointmentDetails = Factory.CandidateSystem.GetCandidateAppointmentDetails(textBoxCode.Text.Substring(2), textBoxCode.Text.Substring(2), "", "", "", "", showInactive: true);
					if (candidateAppointmentDetails == null || candidateAppointmentDetails.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(candidateAppointmentDetails, "", "Offer Letter", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.Appointment);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxGivenName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Candidates;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddCandidatePhoto(textBoxCode.Text.Substring(2), image))
					{
						pictureBoxPhoto.Image = image;
						linkLoadImage.Visible = false;
						linkRemovePicture.Enabled = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add image.");
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetCandidateThumbnailImage(textBoxCode.Text.Substring(2));
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.CandidateSystem.RemoveCandidatePhoto(textBoxCode.Text.Substring(2)))
					{
						pictureBoxPhoto.Image = null;
						linkLoadImage.Visible = false;
						linkRemovePicture.Enabled = false;
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

		private void toolStripBtnMakeEmployee_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDetailsFormObj);
			FormActivator.EmployeeDetailsFormObj.LoadData(textBoxEmployeeNo.Text);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void morePrintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet candidateAppointmentDetails = Factory.CandidateSystem.GetCandidateAppointmentDetails(textBoxCode.Text.Substring(2), textBoxCode.Text.Substring(2), "", "", "", "", showInactive: true);
					if (candidateAppointmentDetails == null || candidateAppointmentDetails.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(candidateAppointmentDetails, "", "Appointment Details", SysDocTypes.None, isPrint: true, showPrintDialog: true);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
