CREATE TABLE [dbo].[Account] (
    [AccountID]          NVARCHAR (64)  NOT NULL,
    [AccountName]        NVARCHAR (64)  NOT NULL,
    [Alias]              NVARCHAR (64)  NULL,
    [CurrencyID]         NVARCHAR (5)   NULL,
    [IsInactive]         BIT            NULL,
    [GroupID]            NVARCHAR (64)  NOT NULL,
    [InitialBalance]     MONEY          NULL,
    [SubType]            INT            NULL,
    [BankAccountType]    NVARCHAR (1)   NULL,
    [BankAccountNumber]  NVARCHAR (20)  NULL,
    [BankID]             NVARCHAR (15)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [Balance]            MONEY          NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [DateUpdated]        DATETIME       NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    [UserDefined1]       NVARCHAR (50)  NULL,
    [UserDefined2]       NVARCHAR (50)  NULL,
    [UserDefined3]       NVARCHAR (50)  NULL,
    [UserDefined4]       NVARCHAR (50)  NULL,
    [IsFavorite]         BIT            NULL,
    CONSTRAINT [PK_Company Accounts] PRIMARY KEY CLUSTERED ([AccountID] ASC),
    CONSTRAINT [FK_Account_Account_Group] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Account_Group] ([GroupID]),
    CONSTRAINT [FK_Account_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID]),
    CONSTRAINT [IX_Company Accounts_1] UNIQUE NONCLUSTERED ([AccountName] ASC, [GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Account_Analysis_Detail] (
    [AccountID]       NVARCHAR (64) NOT NULL,
    [AnalysisGroupID] NVARCHAR (15) NOT NULL,
    [Type]            TINYINT       CONSTRAINT [DF_Account_Analysis_Details_IsOptional] DEFAULT ((1)) NULL,
    [DateCreated]     DATETIME      NULL,
    [CreatedBy]       NVARCHAR (15) NULL,
    CONSTRAINT [PK_Account_Analysis_Detail] PRIMARY KEY CLUSTERED ([AccountID] ASC, [AnalysisGroupID] ASC)
);

GO
CREATE TABLE [dbo].[Account_Group] (
    [GroupID]     NVARCHAR (64)  NOT NULL,
    [GroupName]   NVARCHAR (64)  NOT NULL,
    [TypeID]      INT            NOT NULL,
    [ParentID]    NVARCHAR (15)  NULL,
    [LevelID]     INT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Account_Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Account_Type] (
    [TypeID]          SMALLINT      NOT NULL,
    [AccountTypeName] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_Account Segments] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Activity] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ActivityName]     NVARCHAR (64)   NOT NULL,
    [ActivityType]     TINYINT         NULL,
    [RelatedType]      TINYINT         NULL,
    [ReasonID]         NVARCHAR (15)   NULL,
    [RelatedID]        NVARCHAR (64)   NULL,
    [ContactID]        NVARCHAR (64)   NULL,
    [ActivityDateTime] DATETIME        NULL,
    [OwnerID]          NVARCHAR (15)   NULL,
    [Note]             NVARCHAR (4000) NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Activity Logs] (
    [ActivityLogID]   INT            IDENTITY (1, 1) NOT NULL,
    [ActivityType]    TINYINT        NULL,
    [EntityID]        NVARCHAR (64)  NULL,
    [SysDocID]        NVARCHAR (7)   NULL,
    [LogDate]         DATETIME       NULL,
    [UserID]          NVARCHAR (64)  NULL,
    [MachineID]       NVARCHAR (64)  NULL,
    [Payee]           NVARCHAR (64)  NULL,
    [Amount]          MONEY          NULL,
    [Description]     NVARCHAR (255) NULL,
    [TransactionType] TINYINT        NULL,
    [DataComboType]   TINYINT        NULL,
    [ReferenceID]     INT            NULL,
    CONSTRAINT [PK_Activity Logs] PRIMARY KEY CLUSTERED ([ActivityLogID] ASC)
);

GO
CREATE TABLE [dbo].[Adjustment_Type] (
    [TypeID]      NVARCHAR (15) NOT NULL,
    [TypeName]    NVARCHAR (64) NOT NULL,
    [AccountID]   NVARCHAR (64) NULL,
    [Inactive]    BIT           NULL,
    [IsNonSale]   BIT           NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Inventory_Adjustment_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Analysis] (
    [AnalysisID]         NVARCHAR (15)  NOT NULL,
    [AnalysisName]       NVARCHAR (64)  NOT NULL,
    [Description]        NVARCHAR (255) NULL,
    [GroupID]            NVARCHAR (15)  NULL,
    [Inactive]           BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Analysis] PRIMARY KEY CLUSTERED ([AnalysisID] ASC),
    CONSTRAINT [FK_Analysis_Analysis_Group] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Analysis_Group] ([GroupID])
);

GO
CREATE TABLE [dbo].[Analysis_Group] (
    [GroupID]     NVARCHAR (15)  NOT NULL,
    [GroupName]   NVARCHAR (30)  NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [Inactive]    BIT            NULL,
    [DateCreated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [DateUpdated] DATETIME       NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Analysis_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[AP_Payment_Advice] (
    [APJournalID]      INT             NULL,
    [VendorID]         NVARCHAR (64)   NULL,
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [APDate]           DATETIME        NULL,
    [APDueDate]        DATETIME        NULL,
    [IsDraft]          BIT             NULL,
    [PaymentSysDocID]  NVARCHAR (7)    NULL,
    [PaymentVoucherID] NVARCHAR (15)   NULL,
    [PaymentAmount]    MONEY           NULL,
    [PaymentAmountFC]  MONEY           NULL,
    [DiscountAmount]   MONEY           NULL,
    [DiscountAmountFC] MONEY           NULL,
    [RealizedGainLoss] MONEY           NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [OriginalAmount]   MONEY           NULL,
    [AmountDue]        MONEY           NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL
);

GO
CREATE TABLE [dbo].[AP_Payment_Allocation] (
    [AllocationID]     INT             IDENTITY (1, 1) NOT NULL,
    [APJournalID]      INT             NULL,
    [PaymentAPID]      INT             NULL,
    [BatchID]          INT             NULL,
    [VendorID]         NVARCHAR (64)   NULL,
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [PaymentSysDocID]  NVARCHAR (7)    NULL,
    [PaymentVoucherID] NVARCHAR (15)   NULL,
    [AllocationDate]   DATETIME        NULL,
    [PaymentAmount]    MONEY           NULL,
    [PaymentAmountFC]  MONEY           NULL,
    [DiscountAmount]   MONEY           NULL,
    [DiscountAmountFC] MONEY           NULL,
    [RealizedGainLoss] MONEY           NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    CONSTRAINT [PK_AP_Payment_Allocation] PRIMARY KEY CLUSTERED ([AllocationID] ASC)
);

GO
CREATE TABLE [dbo].[APJournal] (
    [APID]              INT             IDENTITY (1, 1) NOT NULL,
    [VendorID]          NVARCHAR (64)   NULL,
    [SysDocID]          NVARCHAR (7)    NULL,
    [VoucherID]         NVARCHAR (15)   NULL,
    [JournalID]         INT             NULL,
    [APDate]            DATETIME        NULL,
    [APDueDate]         DATETIME        NULL,
    [Debit]             MONEY           NULL,
    [Credit]            MONEY           NULL,
    [DebitFC]           MONEY           NULL,
    [CreditFC]          MONEY           NULL,
    [ConDebitFC]        MONEY           NULL,
    [ConCreditFC]       MONEY           NULL,
    [ConRate]           DECIMAL (10, 5) NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [AttributeID1]      NVARCHAR (50)   NULL,
    [AttributeID2]      NVARCHAR (50)   NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [PaymentMethodType] TINYINT         NULL,
    [ChequeNumber]      NVARCHAR (15)   NULL,
    [ChequeDate]        DATETIME        NULL,
    [BankID]            NVARCHAR (15)   NULL,
    [Description]       NVARCHAR (255)  NULL,
    [APAccountID]       NVARCHAR (64)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [Reference]         NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    [AllocationID]      INT             NULL,
    [IsPDCRow]          BIT             NULL,
    [IsNonStatement]    BIT             NULL,
    [ExcludeInPayment]  BIT             NULL,
    CONSTRAINT [PK_APJournal] PRIMARY KEY CLUSTERED ([APID] ASC),
    CONSTRAINT [FK_APJournal_Vendor] FOREIGN KEY ([VendorID]) REFERENCES [dbo].[Vendor] ([VendorID])
);

GO
CREATE TABLE [dbo].[Approval] (
    [ApprovalID]           NVARCHAR (15) NOT NULL,
    [ApprovalType]         TINYINT       NOT NULL,
    [ApprovalName]         NVARCHAR (64) NULL,
    [ObjectType]           TINYINT       NULL,
    [ObjectID]             NVARCHAR (64) NULL,
    [ObjectSysDocID]       NVARCHAR (7)  NULL,
    [Status]               TINYINT       NULL,
    [UpdateFieldName1]     NVARCHAR (30) NULL,
    [UpdateFieldValue1]    NVARCHAR (30) NULL,
    [UpdateFieldName2]     NVARCHAR (3)  NULL,
    [UpdateFieldValue2]    NVARCHAR (30) NULL,
    [ActionSetInactive]    BIT           NULL,
    [IsInactive]           BIT           NULL,
    [NotifyonPrint]        BIT           NULL,
    [AllownextTransaction] BIT           NULL,
    [AllowtoEdit]          BIT           NULL,
    [DateCreated]          DATETIME      NULL,
    [CreatedBy]            NVARCHAR (15) NULL,
    [DateUpdated]          DATETIME      NULL,
    [UpdatedBy]            NVARCHAR (15) NULL,
    CONSTRAINT [PK_Approval] PRIMARY KEY CLUSTERED ([ApprovalID] ASC, [ApprovalType] ASC)
);

GO
CREATE TABLE [dbo].[Approval_Level] (
    [ApprovalID]            NVARCHAR (15)  NOT NULL,
    [ApprovalType]          TINYINT        NOT NULL,
    [RowIndex]              INT            NOT NULL,
    [ApproverType]          TINYINT        NOT NULL,
    [ApproverID]            NVARCHAR (15)  NULL,
    [IsAssignedSalesperson] BIT            NULL,
    [PreRequisiteIndex]     INT            NULL,
    [Condition]             NVARCHAR (255) NULL
);

GO
CREATE TABLE [dbo].[Approval_Task] (
    [TaskID]            INT           IDENTITY (1, 1) NOT NULL,
    [ApprovalID]        NVARCHAR (15) NOT NULL,
    [ApprovalType]      TINYINT       NOT NULL,
    [LevelID]           INT           NULL,
    [Status]            TINYINT       NULL,
    [PreRequisiteIndex] INT           NULL,
    [AssigneeType]      TINYINT       NULL,
    [AssigneeID]        NVARCHAR (64) NULL,
    [DateCreated]       DATETIME      NULL,
    [ObjectType]        TINYINT       NULL,
    [ObjectID]          INT           NULL,
    [DocumentSysDocID]  NVARCHAR (7)  NULL,
    [DocumentCode]      NVARCHAR (64) NULL,
    [ApproverID]        NVARCHAR (15) NULL,
    [DateApproved]      DATETIME      NULL,
    [IsExpired]         BIT           NULL,
    CONSTRAINT [PK_Approval_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

GO
CREATE TABLE [dbo].[AR_Payment_Allocation] (
    [AllocationID]     INT             IDENTITY (1, 1) NOT NULL,
    [ARJournalID]      INT             NULL,
    [PaymentARID]      INT             NULL,
    [BatchID]          INT             NULL,
    [CustomerID]       NVARCHAR (64)   NULL,
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [PaymentSysDocID]  NVARCHAR (7)    NULL,
    [PaymentVoucherID] NVARCHAR (15)   NULL,
    [AllocationDate]   DATETIME        NULL,
    [PaymentAmount]    MONEY           NULL,
    [PaymentAmountFC]  MONEY           NULL,
    [DiscountAmount]   MONEY           NULL,
    [DiscountAmountFC] MONEY           NULL,
    [RealizedGainLoss] MONEY           NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    CONSTRAINT [PK_AR_Payment_Allocation] PRIMARY KEY CLUSTERED ([AllocationID] ASC)
);

GO
CREATE TABLE [dbo].[Area] (
    [AreaID]       NVARCHAR (15)  NOT NULL,
    [AreaName]     NVARCHAR (64)  NOT NULL,
    [CountryID]    NVARCHAR (15)  NULL,
    [ParentAreaID] NVARCHAR (15)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaID] ASC)
);

GO
CREATE TABLE [dbo].[ARJournal] (
    [ARID]              INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]        NVARCHAR (64)   NULL,
    [SysDocID]          NVARCHAR (7)    NULL,
    [VoucherID]         NVARCHAR (15)   NULL,
    [JournalID]         INT             NULL,
    [ARDate]            DATETIME        NULL,
    [ARDueDate]         DATETIME        NULL,
    [IsPDCRow]          BIT             NULL,
    [Debit]             MONEY           NULL,
    [Credit]            MONEY           NULL,
    [DebitFC]           MONEY           NULL,
    [CreditFC]          MONEY           NULL,
    [ConDebitFC]        MONEY           NULL,
    [ConCreditFC]       MONEY           NULL,
    [ConRate]           DECIMAL (10, 5) NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [AttributeID1]      NVARCHAR (50)   NULL,
    [AttributeID2]      NVARCHAR (50)   NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [PaymentMethodType] TINYINT         NULL,
    [ChequeNumber]      NVARCHAR (15)   NULL,
    [ChequeDate]        DATETIME        NULL,
    [BankID]            NVARCHAR (15)   NULL,
    [Description]       NVARCHAR (255)  NULL,
    [ARAccountID]       NVARCHAR (64)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [Reference]         NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    [AllocationID]      INT             NULL,
    CONSTRAINT [PK_ARJournal] PRIMARY KEY CLUSTERED ([ARID] ASC),
    CONSTRAINT [FK_ARJournal_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID])
);

GO
CREATE TABLE [dbo].[Arrival_Report] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [ContainerNumber]    NVARCHAR (20)   NULL,
    [VehicleNumber]      NVARCHAR (20)   NULL,
    [SourceSysDocID]     NVARCHAR (7)    NULL,
    [SourceVoucherID]    NVARCHAR (15)   NULL,
    [TaskID]             NVARCHAR (15)   NULL,
    [OriginID]           NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [VesselName]         NVARCHAR (20)   NULL,
    [VendorID]           NVARCHAR (64)   NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [ContainerTemp]      DECIMAL (6, 2)  NULL,
    [TotalPallets]       INT             NULL,
    [TotalQuantity]      DECIMAL (10, 2) NULL,
    [DateReceived]       DATETIME        NULL,
    [DateInspected]      DATETIME        NULL,
    [InspectorID]        NVARCHAR (15)   NULL,
    [Status]             TINYINT         NULL,
    [IsConsignment]      BIT             NULL,
    [SourceDocType]      TINYINT         NULL,
    [Note]               NTEXT           NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [TemplateID]         NVARCHAR (15)   NULL,
    [Description]        NVARCHAR (1000) NULL,
    [PackingCondition]   NVARCHAR (15)   NULL,
    [IsPalletized]       TINYINT         NULL,
    [Conclusion]         TINYINT         NULL,
    [ResultNote]         NVARCHAR (255)  NULL,
    [TotalIssue1]        DECIMAL (6, 2)  NULL,
    [TotalIssue2]        DECIMAL (6, 2)  NULL,
    [TotalIssue3]        DECIMAL (6, 2)  NULL,
    [TotalIssue4]        DECIMAL (6, 2)  NULL,
    [TotalWeightLess]    DECIMAL (6, 2)  NULL,
    [Issue1Name]         NVARCHAR (15)   NULL,
    [Issue2Name]         NVARCHAR (15)   NULL,
    [Issue3Name]         NVARCHAR (15)   NULL,
    [Issue4Name]         NVARCHAR (15)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Arrival_Report] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Arrival_Report_Detail] (
    [SysDocID]       NVARCHAR (7)   NOT NULL,
    [VoucherID]      NVARCHAR (15)  NOT NULL,
    [RowIndex]       INT            NOT NULL,
    [LotNumber]      NVARCHAR (15)  NULL,
    [ComodityID]     NVARCHAR (15)  NULL,
    [VarietyID]      NVARCHAR (20)  NULL,
    [BrandID]        NVARCHAR (15)  NULL,
    [Grower]         NVARCHAR (15)  NULL,
    [ItemSize]       NVARCHAR (15)  NULL,
    [Grade]          NVARCHAR (15)  NULL,
    [DateCode]       NVARCHAR (15)  NULL,
    [SampleCount]    REAL           NULL,
    [Temperature]    REAL           NULL,
    [StandardWeight] REAL           NULL,
    [Issue1Count]    REAL           NULL,
    [Issue2Count]    REAL           NULL,
    [Issue3Count]    REAL           NULL,
    [Issue4Count]    REAL           NULL,
    [Weight]         REAL           NULL,
    [Pressure]       REAL           NULL,
    [Brix]           REAL           NULL,
    [NumericAtr1]    REAL           NULL,
    [NumericAtr2]    REAL           NULL,
    [NumericAtr3]    REAL           NULL,
    [NumericAtr4]    REAL           NULL,
    [TextAtr1]       NVARCHAR (15)  NULL,
    [TextAtr2]       NVARCHAR (15)  NULL,
    [TextAtr3]       NVARCHAR (15)  NULL,
    [TextAtr4]       NVARCHAR (15)  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    CONSTRAINT [PK_Arrival_Report_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Arrival_Report_Template] (
    [TemplateID]        NVARCHAR (15)  NOT NULL,
    [TemplateName]      NVARCHAR (30)  NULL,
    [Issue1Name]        NVARCHAR (15)  NULL,
    [Issue2Name]        NVARCHAR (15)  NULL,
    [Issue3Name]        NVARCHAR (15)  NULL,
    [Issue4Name]        NVARCHAR (15)  NULL,
    [Issue1LossPercent] DECIMAL (5, 2) NULL,
    [Issue2LossPercent] DECIMAL (5, 2) NULL,
    [Issue3LossPercent] DECIMAL (5, 2) NULL,
    [Issue4LossPercent] DECIMAL (5, 2) NULL,
    [AtrNum1Name]       NVARCHAR (15)  NULL,
    [AtrNum2Name]       NVARCHAR (15)  NULL,
    [AtrNum3Name]       NVARCHAR (15)  NULL,
    [AtrNum4Name]       NVARCHAR (15)  NULL,
    [AtrText1Name]      NVARCHAR (15)  NULL,
    [AtrText2Name]      NVARCHAR (15)  NULL,
    [AtrText3Name]      NVARCHAR (15)  NULL,
    [AtrText4Name]      NVARCHAR (15)  NULL,
    [PrintTemplateName] NVARCHAR (64)  NULL,
    [IsBrix]            BIT            NULL,
    [IsPressure]        BIT            NULL,
    [IsGrower]          BIT            NULL,
    [IsDateCode]        BIT            NULL,
    [IsPalletID]        BIT            NULL,
    [IsTemperature]     BIT            NULL,
    [IsInactive]        BIT            NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Arrival_Report_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC)
);

GO
CREATE TABLE [dbo].[Assembly_Build] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [AssemblyProductID]  NVARCHAR (64)   NULL,
    [QuantityBuild]      DECIMAL (18, 5) NULL,
    [UnitWeight]         DECIMAL (18, 5) NULL,
    [UnitCost]           DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (255)  NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [TransactionDate]    DATETIME        NULL,
    [WorkCompDate]       DATETIME        NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Assembly_Build] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Assembly_Build_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RowIndex]     INT             NOT NULL,
    [BOMProductID] NVARCHAR (64)   NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [Cost]         DECIMAL (18, 5) NULL,
    [Description]  NVARCHAR (255)  NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [UnitFactor]   DECIMAL (18, 5) NULL,
    [FactorType]   NVARCHAR (1)    NULL,
    [SubunitCost]  DECIMAL (18, 5) NULL,
    [LocationID]   NVARCHAR (15)   NULL,
    [COGS]         MONEY           NULL,
    CONSTRAINT [PK_Assembly_Build_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Assembly_Build_Expense] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Bank] (
    [BankID]       NVARCHAR (15)  NOT NULL,
    [BankName]     NVARCHAR (64)  NOT NULL,
    [ContactName]  NVARCHAR (64)  NULL,
    [ContactTitle] NVARCHAR (30)  NULL,
    [RoutingCode]  NVARCHAR (30)  NULL,
    [TaxIDNumber]  NVARCHAR (30)  NULL,
    [Address]      NVARCHAR (64)  NULL,
    [Address2]     NVARCHAR (64)  NULL,
    [Address3]     NVARCHAR (64)  NULL,
    [City]         NVARCHAR (30)  NULL,
    [PostalCode]   NVARCHAR (15)  NULL,
    [Country]      NVARCHAR (15)  NULL,
    [State]        NVARCHAR (30)  NULL,
    [Phone]        NVARCHAR (30)  NULL,
    [Fax]          NVARCHAR (30)  NULL,
    [IsInactive]   BIT            CONSTRAINT [DF_Banks_IsInactive] DEFAULT ((0)) NULL,
    [Note]         NVARCHAR (255) NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED ([BankID] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Facility] (
    [FacilityID]            NVARCHAR (15)  NOT NULL,
    [FacilityName]          NVARCHAR (64)  NULL,
    [FacilityType]          TINYINT        NULL,
    [Alias]                 NVARCHAR (64)  NULL,
    [GroupID]               NVARCHAR (15)  NULL,
    [LimitAmount]           MONEY          NULL,
    [TenorDays]             INT            NULL,
    [Status]                TINYINT        NULL,
    [StartDate]             DATETIME       NULL,
    [EndDate]               DATETIME       NULL,
    [PayableAccountID]      NVARCHAR (64)  NULL,
    [CurrentAccountID]      NVARCHAR (64)  NULL,
    [BankChargeAccountID]   NVARCHAR (64)  NULL,
    [BankInterestAccountID] NVARCHAR (64)  NULL,
    [PrintTemplateName]     NVARCHAR (64)  NULL,
    [Note]                  NVARCHAR (255) NULL,
    [ApprovalStatus]        TINYINT        NULL,
    [VerificationStatus]    TINYINT        NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    CONSTRAINT [PK_Bank_Facility] PRIMARY KEY CLUSTERED ([FacilityID] ASC),
    CONSTRAINT [FK_BankFacility_BankFacilityGroup] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Bank_Facility_Group] ([GroupID])
);

GO
CREATE TABLE [dbo].[Bank_Facility_Group] (
    [GroupID]       NVARCHAR (15)  NOT NULL,
    [GroupName]     NVARCHAR (64)  NULL,
    [BankID]        NVARCHAR (15)  NULL,
    [TotalLimit]    MONEY          NULL,
    [Alias]         NVARCHAR (64)  NULL,
    [ContactName]   NVARCHAR (64)  NULL,
    [ContactNumber] NVARCHAR (30)  NULL,
    [StartDate]     DATETIME       NULL,
    [EndDate]       DATETIME       NULL,
    [RenewDate]     DATETIME       NULL,
    [ExpiryDate]    DATETIME       NULL,
    [Status]        TINYINT        NULL,
    [Note]          NVARCHAR (255) NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    CONSTRAINT [PK_Bank_Facility_Offer] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Facility_Group_Contacts] (
    [GroupID]   NVARCHAR (15) NOT NULL,
    [ContactID] NVARCHAR (64) NOT NULL,
    [JobTitle]  NVARCHAR (30) NULL,
    [Note]      NVARCHAR (64) NULL,
    [RowIndex]  SMALLINT      NULL,
    CONSTRAINT [PK_Bank_Facility_Group_Contacts] PRIMARY KEY CLUSTERED ([GroupID] ASC, [ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Facility_Payment] (
    [SysDocID]             NVARCHAR (7)    NOT NULL,
    [VoucherID]            NVARCHAR (15)   NOT NULL,
    [TransactionSysDocID]  NVARCHAR (7)    NULL,
    [TransactionVoucherID] NVARCHAR (15)   NULL,
    [FacilityType]         INT             NULL,
    [CostCenterID]         NVARCHAR (15)   NULL,
    [BankFacilityID]       NVARCHAR (15)   NULL,
    [Amount]               MONEY           NULL,
    [AmountFC]             MONEY           NULL,
    [TransactionDate]      DATETIME        NULL,
    [IsVoid]               BIT             NULL,
    [CurrencyID]           NVARCHAR (5)    NULL,
    [CurrencyRate]         DECIMAL (18, 5) NULL,
    [GLType]               TINYINT         NULL,
    [Reference]            NVARCHAR (15)   NULL,
    [PayFromAccountID]     NVARCHAR (64)   NULL,
    [PayToAccountID]       NVARCHAR (64)   NULL,
    [TransactionStatus]    TINYINT         CONSTRAINT [DF_Bank_Facility_Payment_TransactionStatus] DEFAULT ((1)) NULL,
    [PaidAmount]           MONEY           NULL,
    [Description]          NVARCHAR (255)  NULL,
    [ApprovalStatus]       TINYINT         NULL,
    [VerificationStatus]   TINYINT         NULL,
    [DateCreated]          DATETIME        NULL,
    [DateUpdated]          DATETIME        NULL,
    [CreatedBy]            NVARCHAR (64)   NULL,
    [UpdatedBy]            NVARCHAR (64)   NULL,
    CONSTRAINT [PK_Bank_Facility_Payment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Facility_Payment_Details] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [AccountID]       NVARCHAR (64)  NULL,
    [Description]     NVARCHAR (255) NULL,
    [Amount]          MONEY          NULL,
    [AmountFC]        MONEY          NULL,
    [RowIndex]        SMALLINT       NULL,
    [Reference]       NVARCHAR (20)  NULL,
    [PayeeID]         NVARCHAR (64)  NULL,
    [BankFacilityID]  NVARCHAR (15)  NULL,
    [DueDate]         DATETIME       NULL,
    [PayeeType]       NVARCHAR (1)   NULL,
    [AnalysisID]      NVARCHAR (15)  NULL,
    [CostCenterID]    NVARCHAR (15)  NULL,
    [IsVoid]          BIT            NULL,
    [PaymentMethodID] NVARCHAR (15)  NULL,
    [ChequebookID]    NVARCHAR (15)  NULL,
    [BankID]          NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Bank_Facility_Transaction] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [CostCenterID]       NVARCHAR (15)   NULL,
    [FacilityType]       INT             NULL,
    [BankFacilityID]     NVARCHAR (15)   NULL,
    [PayeeType]          NVARCHAR (1)    NULL,
    [PayeeID]            NVARCHAR (64)   NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TransactionDate]    DATETIME        NULL,
    [IsVoid]             BIT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [GLType]             TINYINT         NULL,
    [JournalID]          INT             NULL,
    [Reference]          NVARCHAR (15)   NULL,
    [FirstAccountID]     NVARCHAR (64)   NULL,
    [SecondAccountID]    NVARCHAR (64)   NULL,
    [TransactionStatus]  TINYINT         CONSTRAINT [DF_Bank_Facility_Transaction_TransactionStatus] DEFAULT ((1)) NULL,
    [EmployeeID]         NVARCHAR (64)   NULL,
    [PaidAmount]         MONEY           NULL,
    [Description]        NVARCHAR (255)  NULL,
    [RequestSysDocID]    NVARCHAR (7)    NULL,
    [RequestVoucherID]   NVARCHAR (15)   NULL,
    [SourceSysDocID]     NVARCHAR (7)    NULL,
    [SourceVoucherID]    NVARCHAR (15)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (64)   NULL,
    [UpdatedBy]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_Bank_Facility_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Facility_Transaction_Details] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [AccountID]       NVARCHAR (64)  NULL,
    [Description]     NVARCHAR (255) NULL,
    [Amount]          MONEY          NULL,
    [AmountFC]        MONEY          NULL,
    [RowIndex]        SMALLINT       NULL,
    [Reference]       NVARCHAR (20)  NULL,
    [PayeeID]         NVARCHAR (64)  NULL,
    [BankFacilityID]  NVARCHAR (15)  NULL,
    [DueDate]         DATETIME       NULL,
    [PayeeType]       NVARCHAR (1)   NULL,
    [AnalysisID]      NVARCHAR (15)  NULL,
    [CostCenterID]    NVARCHAR (15)  NULL,
    [IsVoid]          BIT            NULL,
    [PaymentMethodID] NVARCHAR (15)  NULL,
    [ChequebookID]    NVARCHAR (15)  NULL,
    [BankID]          NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Bank_Fee_Details] (
    [GLTransactionSysDocID]  NVARCHAR (7)    NOT NULL,
    [GLTransactionVoucherID] NVARCHAR (15)   NOT NULL,
    [RowIndex]               INT             NOT NULL,
    [BankFacilityID]         NVARCHAR (15)   NULL,
    [ChequebookID]           NVARCHAR (15)   NULL,
    [Description]            NVARCHAR (255)  NULL,
    [Reference]              NVARCHAR (30)   NULL,
    [BankAccountID]          NVARCHAR (64)   NULL,
    [ExpenseAccountID]       NVARCHAR (64)   NULL,
    [BankFeeID]              NVARCHAR (15)   NULL,
    [TaxOption]              TINYINT         NULL,
    [TaxGroupID]             NVARCHAR (15)   NULL,
    [TaxAmount]              DECIMAL (18, 5) NULL,
    [Amount]                 MONEY           NULL,
    [AmountFC]               MONEY           NULL,
    [CurrencyID]             NVARCHAR (5)    NULL,
    [CurrencyRate]           DECIMAL (18, 5) NULL,
    [IsWithTR]               BIT             NULL,
    CONSTRAINT [PK_Bank_Fee_Detail] PRIMARY KEY CLUSTERED ([GLTransactionSysDocID] ASC, [GLTransactionVoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Bank_Reconciliation] (
    [AccountID]       NVARCHAR (64)  NOT NULL,
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [Description]     NVARCHAR (255) NULL,
    [TransactionDate] DATETIME       NULL,
    [ReconcileDate]   DATETIME       NULL,
    [Debit]           MONEY          NULL,
    [Credit]          MONEY          NULL,
    [RowIndex]        TINYINT        NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Bank_Reconciliation] PRIMARY KEY CLUSTERED ([AccountID] ASC, [SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Benefit] (
    [BenefitID]      NVARCHAR (15)  NOT NULL,
    [BenefitName]    NVARCHAR (30)  NOT NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            NULL,
    [AccountID]      NVARCHAR (64)  NOT NULL,
    [IsNonFinancial] BIT            NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Benefit] PRIMARY KEY CLUSTERED ([BenefitID] ASC)
);

GO
CREATE TABLE [dbo].[Bill_Discount] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [DueDate]            DATETIME        NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [BankAccountID]      NVARCHAR (64)   NULL,
    [LiabilityAccountID] NVARCHAR (64)   NULL,
    [BankFacilityID]     NVARCHAR (15)   NULL,
    [BankChargeAmount]   MONEY           NULL,
    [PaidAmount]         MONEY           NULL,
    [FacilityType]       INT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (10, 5) NULL,
    [GLType]             TINYINT         NULL,
    [JournalID]          INT             NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [BankCommission]     MONEY           NULL,
    [BankChargePercent]  MONEY           NULL,
    [IsVoid]             BIT             NULL,
    [Note]               NVARCHAR (255)  NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Bill_Discount] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Bill_Discount_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ChequeID]         INT             NULL,
    [PayeeID]          NVARCHAR (64)   NULL,
    [CurrencyID]       NVARCHAR (5)    NULL,
    [InvoiceSysDocID]  NVARCHAR (7)    NOT NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NOT NULL,
    [Total]            DECIMAL (18, 5) NOT NULL,
    [BankChargeAmount] MONEY           NULL,
    [DiscountAmount]   MONEY           NULL,
    [Date]             DATETIME        NULL,
    [DueDate]          DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Bill_Of_Lading] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [Status]            TINYINT         CONSTRAINT [DF_BillOfLading_Status] DEFAULT ((1)) NULL,
    [IsVoid]            BIT             NULL,
    [VendorID]          NVARCHAR (64)   NULL,
    [BOLNumber]         NVARCHAR (20)   NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [VendorReferenceNo] NVARCHAR (40)   NULL,
    [PurchaseFlow]      TINYINT         NULL,
    [Port]              NVARCHAR (15)   NULL,
    [LoadingPort]       NVARCHAR (15)   NULL,
    [ETA]               DATETIME        NULL,
    [ATD]               DATETIME        NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [PONumber]          NVARCHAR (20)   NULL,
    [Shipper]           NVARCHAR (15)   NULL,
    [ClearingAgent]     NVARCHAR (30)   NULL,
    [Value]             MONEY           NULL,
    [ShipStatus]        BIT             NULL,
    [TransporterID]     NVARCHAR (50)   NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [BuyerID]           NVARCHAR (64)   NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_BillOfLading] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Bill_Of_Lading_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ContainerNumber] NVARCHAR (15)   NULL,
    [Type]            NVARCHAR (50)   NULL,
    [Weight]          DECIMAL (18, 5) NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [ContainerSizeID] NVARCHAR (30)   NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [RowIndex]        TINYINT         NULL
);

GO
CREATE TABLE [dbo].[Bin] (
    [BinID]       NVARCHAR (30)  NOT NULL,
    [BinName]     NVARCHAR (64)  NULL,
    [LocationID]  NVARCHAR (15)  NULL,
    [Inactive]    BIT            NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Bin] PRIMARY KEY CLUSTERED ([BinID] ASC)
);

GO
CREATE TABLE [dbo].[BOM] (
    [BOMID]        NVARCHAR (15)  NOT NULL,
    [BOMName]      NVARCHAR (64)  NULL,
    [Amount]       MONEY          NULL,
    [PricePercent] DECIMAL (5, 2) NULL,
    [Note]         NVARCHAR (255) NULL,
    [IsInactive]   BIT            NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_BOM] PRIMARY KEY CLUSTERED ([BOMID] ASC)
);

GO
CREATE TABLE [dbo].[BOM_Detail] (
    [BOMID]       NVARCHAR (15)   NOT NULL,
    [ProductID]   NVARCHAR (64)   NOT NULL,
    [RowIndex]    INT             NOT NULL,
    [Quantity]    DECIMAL (18, 5) NULL,
    [Cost]        DECIMAL (18, 5) NULL,
    [Description] NVARCHAR (255)  NULL,
    [UnitID]      NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Budget] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [TransactionDate] DATETIME        NULL,
    [DateFrom]        DATETIME        NULL,
    [DateTo]          DATETIME        NULL,
    [Reference]       NVARCHAR (30)   NULL,
    [Reference2]      NVARCHAR (30)   NULL,
    [BudgetType]      NCHAR (10)      NULL,
    [CurrencyID]      NVARCHAR (5)    NULL,
    [CurrencyRate]    DECIMAL (18, 5) NULL,
    [Amount]          DECIMAL (18, 5) NULL,
    [AmountFC]        DECIMAL (18, 5) NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Budget_Details] (
    [SysDocID]     NVARCHAR (7)   NULL,
    [VoucherID]    NVARCHAR (15)  NULL,
    [GroupID]      NVARCHAR (30)  NULL,
    [AccountID]    NVARCHAR (64)  NULL,
    [Description]  NVARCHAR (255) NULL,
    [Reference]    NVARCHAR (30)  NULL,
    [PayeeID]      NVARCHAR (64)  NULL,
    [PayeeType]    NVARCHAR (1)   NULL,
    [AnalysisID]   NVARCHAR (50)  NULL,
    [CostCenterID] NVARCHAR (15)  NULL,
    [JobID]        NVARCHAR (50)  NULL,
    [Credit]       MONEY          NULL,
    [CreditFC]     MONEY          NULL,
    [Debit]        MONEY          NULL,
    [DebitFC]      MONEY          NULL,
    [Rowindex]     INT            NULL
);

GO
CREATE TABLE [dbo].[Buyer] (
    [BuyerID]            NVARCHAR (64)  NOT NULL,
    [FullName]           NVARCHAR (64)  NULL,
    [EmployeeID]         NVARCHAR (64)  NULL,
    [Address]            NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (30)  NULL,
    [Website]            NVARCHAR (30)  NULL,
    [BankName]           NVARCHAR (30)  NULL,
    [BankBranch]         NVARCHAR (30)  NULL,
    [BankAccountNumber]  NVARCHAR (30)  NULL,
    [AreaID]             NVARCHAR (15)  NULL,
    [CountryID]          NVARCHAR (15)  NULL,
    [IsInactive]         BIT            CONSTRAINT [DF_Buyer_IsInactive] DEFAULT ((0)) NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateUpdated]        DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED ([BuyerID] ASC)
);

GO
CREATE TABLE [dbo].[Campaign] (
    [CampaignID]       NVARCHAR (64)   NOT NULL,
    [CampaignName]     NVARCHAR (64)   NOT NULL,
    [Type]             TINYINT         NULL,
    [Status]           TINYINT         NULL,
    [StartDate]        DATETIME        NULL,
    [EndDate]          DATETIME        NULL,
    [NumberSent]       INT             NULL,
    [ExpectedResponse] TINYINT         NULL,
    [BudgetedCost]     DECIMAL (18, 2) NULL,
    [ActualCost]       DECIMAL (18, 2) NULL,
    [ExpectedRevenue]  MONEY           NULL,
    [IsInactive]       BIT             NULL,
    [Note]             NVARCHAR (255)  NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Candidate] (
    [CandidateID]              NVARCHAR (64)  NOT NULL,
    [PassportNo]               NVARCHAR (20)  NULL,
    [GivenName]                NVARCHAR (64)  NULL,
    [SurName]                  NVARCHAR (64)  NULL,
    [GroupID]                  NVARCHAR (15)  NULL,
    [DesignationID]            NVARCHAR (30)  NULL,
    [VisaStatus]               NVARCHAR (15)  NULL,
    [NationalityID]            NVARCHAR (15)  NULL,
    [Gender]                   CHAR (1)       NULL,
    [BirthDate]                SMALLDATETIME  NULL,
    [BirthPlace]               NVARCHAR (30)  NULL,
    [PassportPlaceOfIssue]     NVARCHAR (50)  NULL,
    [PassportIssueDate]        SMALLDATETIME  NULL,
    [PassportExpiryDate]       SMALLDATETIME  NULL,
    [FatherName]               NVARCHAR (64)  NULL,
    [MotherName]               NVARCHAR (64)  NULL,
    [SpouseName]               NVARCHAR (64)  NULL,
    [PassportAddress]          NVARCHAR (255) NULL,
    [Photo]                    IMAGE          NULL,
    [ECRStatus]                TINYINT        NULL,
    [ApplType]                 TINYINT        NULL,
    [SystemDate]               DATETIME       NULL,
    [SelectionStatus]          TINYINT        NULL,
    [SelectedOn]               SMALLDATETIME  NULL,
    [SelectedAt]               NVARCHAR (30)  NULL,
    [ThroughAgent]             NVARCHAR (100) NULL,
    [VisaCopyToAgentOn]        SMALLDATETIME  NULL,
    [VisaDesignation]          NVARCHAR (30)  NULL,
    [ActualDesignation]        NVARCHAR (30)  NULL,
    [Remarks]                  NVARCHAR (255) NULL,
    [QualificationID]          NVARCHAR (30)  NULL,
    [LanguageID]               NVARCHAR (30)  NULL,
    [ExperienceLocal]          DECIMAL (18)   NULL,
    [ExperienceAbroad]         DECIMAL (18)   NULL,
    [CancellationDate]         DATETIME       NULL,
    [AOTypingDate]             DATETIME       NULL,
    [AORegNumber]              NVARCHAR (30)  NULL,
    [ApplicationTypingDateMOL] SMALLDATETIME  NULL,
    [MBNumberMOL]              NVARCHAR (30)  NULL,
    [SponsorID]                NVARCHAR (30)  NULL,
    [ApprovalStatusMOL]        TINYINT        NULL,
    [ApprovalDateMOL]          DATETIME       NULL,
    [ApprovalValidTillMOL]     SMALLDATETIME  NULL,
    [TempWPNumber]             NVARCHAR (50)  NULL,
    [ApprovalFeePaidOnMOL]     SMALLDATETIME  NULL,
    [BGPaidOnMOL]              DATETIME       NULL,
    [BGTypeMOL]                NVARCHAR (50)  NULL,
    [MOLRemarks]               NVARCHAR (500) NULL,
    [VisaAppliedThroughIMG]    NVARCHAR (64)  NULL,
    [VisaPostedOnIMG]          DATETIME       NULL,
    [VisaApprovedOnIMG]        DATETIME       NULL,
    [ApprovalStatusIMG]        TINYINT        NULL,
    [VisaIssuePlaceIMG]        NVARCHAR (64)  NULL,
    [VisaNumber]               NVARCHAR (30)  NULL,
    [VisaIssueDateIMG]         SMALLDATETIME  NULL,
    [VisaExpiryDateIMG]        SMALLDATETIME  NULL,
    [UIDNumberIMG]             NVARCHAR (30)  NULL,
    [ExpectedArrivaldate]      DATETIME       NULL,
    [IMGRemarks]               NVARCHAR (500) NULL,
    [ArrivedOn]                DATETIME       NULL,
    [ArrivalPort]              NVARCHAR (50)  NULL,
    [Category]                 NVARCHAR (50)  NULL,
    [EmployeeNo]               NVARCHAR (15)  NULL,
    [HealthCardNo]             NVARCHAR (50)  NULL,
    [MedicalTypingOn]          DATETIME       NULL,
    [MedicalAttendedOn]        DATETIME       NULL,
    [MedicalCollectedOn]       DATETIME       NULL,
    [MedicalResult]            NVARCHAR (50)  NULL,
    [MedicalNote]              NVARCHAR (255) NULL,
    [ApplicationTypedOnEID]    DATETIME       NULL,
    [AttendedForEID]           DATETIME       NULL,
    [CollectedOnEID]           DATETIME       NULL,
    [NationalID]               NVARCHAR (50)  NULL,
    [NationalIDValidity]       DATETIME       NULL,
    [AGTType]                  NVARCHAR (50)  NULL,
    [MBNumberAGT]              NVARCHAR (30)  NULL,
    [AGTTypedOn]               DATETIME       NULL,
    [AGTSubmittedOn]           DATETIME       NULL,
    [WPNumber]                 NVARCHAR (50)  NULL,
    [PersonalIDNo]             NVARCHAR (50)  NULL,
    [WPIssuePlace]             NVARCHAR (50)  NULL,
    [WPIssueDate]              DATETIME       NULL,
    [WPExpiryDate]             DATETIME       NULL,
    [RPProcessType]            NVARCHAR (30)  NULL,
    [ApplicationPostedOnRP]    DATETIME       NULL,
    [ApplicationApprovedOnRP]  DATETIME       NULL,
    [SubmittedToZajil]         DATETIME       NULL,
    [PassportCollectedOn]      DATETIME       NULL,
    [RPIssuePlace]             NVARCHAR (50)  NULL,
    [RPIssueDate]              DATETIME       NULL,
    [RPExpiryDate]             DATETIME       NULL,
    [ReligionID]               NVARCHAR (15)  NULL,
    [BloodGroup]               NVARCHAR (5)   NULL,
    [ContractType]             NVARCHAR (15)  NULL,
    [Notes]                    NVARCHAR (255) NULL,
    [MaritalStatus]            TINYINT        CONSTRAINT [DF_Candidate_MaritalStatus] DEFAULT ((1)) NULL,
    [PrimaryAddressID]         NVARCHAR (15)  NULL,
    [PDCAmount]                MONEY          NULL,
    [IsCancelled]              BIT            NULL,
    [CancellationStage]        NVARCHAR (50)  NULL,
    [VCAppReceivedDate]        DATETIME       NULL,
    [AppCancellationDate]      DATETIME       NULL,
    [IMGCancellationDate]      DATETIME       NULL,
    [MOLCancellationDate]      DATETIME       NULL,
    [SignedAOrecvdDate]        DATETIME       NULL,
    [SignedAGTrecvdDate]       DATETIME       NULL,
    [MBNumberCancel]           NVARCHAR (30)  NULL,
    [CancellationReason]       NVARCHAR (64)  NULL,
    [CancellationRemarks]      NVARCHAR (500) NULL,
    [UserDefined1]             NVARCHAR (64)  NULL,
    [UserDefined2]             NVARCHAR (64)  NULL,
    [UserDefined3]             NVARCHAR (64)  NULL,
    [UserDefined4]             NVARCHAR (64)  NULL,
    [DateCreated]              DATETIME       NULL,
    [DateUpdated]              DATETIME       NULL,
    [CreatedBy]                NVARCHAR (15)  NULL,
    [UpdatedBy]                NVARCHAR (15)  NULL,
    [DivisionID]               NVARCHAR (15)  NULL,
    [AgreementStatus]          NVARCHAR (15)  NULL,
    [SpecialCondition]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([CandidateID] ASC)
);

GO
CREATE TABLE [dbo].[Candidate_Benefit_Detail] (
    [CandidateID] NVARCHAR (15)  NOT NULL,
    [BenefitID]   NVARCHAR (15)  NOT NULL,
    [StartDate]   SMALLDATETIME  NULL,
    [EndDate]     SMALLDATETIME  NULL,
    [Amount]      MONEY          NULL,
    [LastAmount]  MONEY          NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL,
    CONSTRAINT [PK_andidate_Benefit_Detail] PRIMARY KEY CLUSTERED ([CandidateID] ASC, [BenefitID] ASC)
);

GO
CREATE TABLE [dbo].[Candidate_Salary] (
    [CandidateID]   NVARCHAR (15) NOT NULL,
    [PayrollItemID] NVARCHAR (15) NOT NULL,
    [PayType]       TINYINT       NULL,
    [Amount]        MONEY         NULL,
    [RowIndex]      TINYINT       NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (64) NULL,
    [UpdatedBy]     NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[Card_Security] (
    [CardID]           INT            NULL,
    [ConditionalQuery] NVARCHAR (MAX) NULL,
    [FilterControl]    NVARCHAR (64)  NULL,
    [FilterFrom]       NVARCHAR (64)  NULL,
    [FilterTo]         NVARCHAR (64)  NULL,
    [UserID]           NVARCHAR (15)  NULL,
    [GroupID]          NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Card_Setting] (
    [ID]        NCHAR (10) NULL,
    [ItemQuery] NTEXT      NULL
);

GO
CREATE TABLE [dbo].[Case_Client] (
    [CaseClientID]           NVARCHAR (64)  NOT NULL,
    [CaseClientName]         NVARCHAR (64)  NOT NULL,
    [ShortName]              NVARCHAR (64)  NULL,
    [ForeignName]            NVARCHAR (64)  NULL,
    [CompanyName]            NVARCHAR (64)  NULL,
    [LegalName]              NVARCHAR (64)  NULL,
    [CustomerClassID]        NVARCHAR (15)  NULL,
    [CollectionUserID]       NVARCHAR (15)  NULL,
    [PartyType]              NVARCHAR (15)  NULL,
    [PartyID]                NVARCHAR (50)  NULL,
    [IsDefendant]            BIT            NULL,
    [IsPlantiff]             BIT            NULL,
    [InsRating]              TINYINT        NULL,
    [RatingBy]               NVARCHAR (15)  NULL,
    [RatingDate]             DATETIME       NULL,
    [RatingRemarks]          NVARCHAR (255) NULL,
    [StatementEmail]         NVARCHAR (255) NULL,
    [Flag]                   TINYINT        NULL,
    [POSHidden]              BIT            NULL,
    [ContactName]            NVARCHAR (64)  NULL,
    [CurrencyID]             NVARCHAR (5)   NULL,
    [TermID]                 NVARCHAR (15)  NULL,
    [AreaID]                 NVARCHAR (15)  NULL,
    [SubAreaID]              NVARCHAR (15)  NULL,
    [Rating]                 TINYINT        NULL,
    [AcceptCheckPayment]     BIT            NULL,
    [AcceptPDC]              BIT            NULL,
    [CreditLimitType]        TINYINT        NULL,
    [CreditAmount]           MONEY          NULL,
    [ARAccountID]            NVARCHAR (64)  NULL,
    [ParentCaseClientID]     NVARCHAR (64)  NULL,
    [IsParentPosting]        BIT            NULL,
    [CustomerGroupID]        NVARCHAR (15)  NULL,
    [BillToAddressID]        NVARCHAR (15)  NULL,
    [ShipToAddressID]        NVARCHAR (15)  NULL,
    [StatementAddressID]     NVARCHAR (15)  NULL,
    [CountryID]              NVARCHAR (15)  NULL,
    [ShippingMethodID]       NVARCHAR (15)  NULL,
    [IsInactive]             BIT            NULL,
    [IsHold]                 BIT            NULL,
    [Photo]                  IMAGE          NULL,
    [BankName]               NVARCHAR (30)  NULL,
    [BankBranch]             NVARCHAR (30)  NULL,
    [BankAccountNumber]      NVARCHAR (30)  NULL,
    [VATRegistrationNumber]  NVARCHAR (30)  NULL,
    [InsStatus]              TINYINT        NULL,
    [InsApplicationDate]     DATETIME       NULL,
    [InsEffectiveDate]       DATETIME       NULL,
    [InsExpiryDate]          DATETIME       NULL,
    [InsRequestedAmount]     MONEY          NULL,
    [InsApprovedAmount]      MONEY          NULL,
    [InsPolicyNumber]        NVARCHAR (30)  NULL,
    [InsRemarks]             NVARCHAR (255) NULL,
    [InsProviderID]          NVARCHAR (15)  NULL,
    [InsuranceID]            NVARCHAR (30)  NULL,
    [PaymentTermID]          NVARCHAR (15)  NULL,
    [DivisionID]             NVARCHAR (15)  NULL,
    [CreditReviewBy]         NVARCHAR (15)  NULL,
    [CreditReviewDate]       DATETIME       NULL,
    [Note]                   NTEXT          NULL,
    [DateEstablished]        DATETIME       NULL,
    [PaymentMethodID]        NVARCHAR (15)  NULL,
    [SalesPersonID]          NVARCHAR (64)  NULL,
    [PriceLevelID]           NVARCHAR (1)   NULL,
    [PrimaryAddressID]       NVARCHAR (15)  NULL,
    [StatementSendingMethod] TINYINT        NULL,
    [AllowConsignment]       BIT            NULL,
    [CollectionRemarks]      NVARCHAR (300) NULL,
    [ConsignComPercent]      BIT            NULL,
    [PDCAmount]              MONEY          NULL,
    [LicenseExpDate]         DATETIME       NULL,
    [ContractExpDate]        DATETIME       NULL,
    [Balance]                MONEY          NULL,
    [LeadSourceID]           NVARCHAR (15)  NULL,
    [SourceLeadID]           NVARCHAR (15)  NULL,
    [IsWeightInvoice]        BIT            NULL,
    [IsCustomerSince]        DATETIME       NULL,
    [ProfileDetails]         NTEXT          NULL,
    [UserDefined1]           NVARCHAR (64)  NULL,
    [UserDefined2]           NVARCHAR (64)  NULL,
    [UserDefined3]           NVARCHAR (64)  NULL,
    [UserDefined4]           NVARCHAR (64)  NULL,
    [ApprovalStatus]         TINYINT        NULL,
    [VerificationStatus]     TINYINT        NULL,
    [DeliveryInstructions]   NVARCHAR (500) NULL,
    [AccountInstructions]    NVARCHAR (500) NULL,
    [DateCreated]            DATETIME       NULL,
    [DateUpdated]            DATETIME       NULL,
    [CreatedBy]              NVARCHAR (15)  NULL,
    [UpdatedBy]              NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Case_Client] PRIMARY KEY CLUSTERED ([CaseClientID] ASC)
);

GO
CREATE TABLE [dbo].[Case_Client_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [CaseClientID]       NVARCHAR (64)  NOT NULL,
    [AddressName]        NVARCHAR (64)  NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Latitude]           NVARCHAR (30)  NULL,
    [Longitude]          NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Twitter]            NVARCHAR (30)  NULL,
    [Facebook]           NVARCHAR (255) NULL,
    [Skype]              NVARCHAR (30)  NULL,
    [Comment]            NVARCHAR (255) NULL,
    [Inactive]           BIT            NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Case_Client_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CaseClientID] ASC)
);

GO
CREATE TABLE [dbo].[Case_Client_Contact_Detail] (
    [CaseClientID] NVARCHAR (64) NOT NULL,
    [ContactID]    NVARCHAR (64) NOT NULL,
    [JobTitle]     NVARCHAR (30) NULL,
    [Note]         NVARCHAR (64) NULL,
    [RowIndex]     SMALLINT      NULL,
    CONSTRAINT [PK_Case_Client_Contact_Detail] PRIMARY KEY CLUSTERED ([CaseClientID] ASC, [ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Case_Party] (
    [CasePartyID]   NVARCHAR (15)  NOT NULL,
    [CasePartyName] NVARCHAR (64)  NOT NULL,
    [IsInactive]    BIT            NULL,
    [Note]          NVARCHAR (255) NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    CONSTRAINT [PK_Case_Party] PRIMARY KEY CLUSTERED ([CasePartyID] ASC)
);

GO
CREATE TABLE [dbo].[Chart_Series] (
    [CustomGadgetID]   NVARCHAR (30) NOT NULL,
    [SeriesID]         NVARCHAR (15) NOT NULL,
    [DisplayName]      NVARCHAR (30) NULL,
    [ChartValueColumn] NVARCHAR (30) NULL,
    [ChartType]        TINYINT       NULL,
    [Color]            INT           NULL,
    [LabelVisible]     BIT           NULL,
    [LabelPosition]    TINYINT       NULL,
    [LabelTextPattern] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Custom_Gadget_Series] PRIMARY KEY CLUSTERED ([CustomGadgetID] ASC, [SeriesID] ASC)
);

GO
CREATE TABLE [dbo].[CheckList] (
    [CheckListID]   NVARCHAR (15) NOT NULL,
    [CheckListType] TINYINT       NOT NULL,
    [CheckListName] NVARCHAR (64) NULL,
    [ApproverType]  TINYINT       NULL,
    [ApproverID]    NVARCHAR (15) NULL,
    [Interval]      TINYINT       NULL,
    [DeadLineDays]  TINYINT       NULL,
    [StartDate]     DATETIME      NULL,
    [Status]        TINYINT       NULL,
    [IsInactive]    BIT           NULL,
    [DateCreated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (15) NULL,
    [DateUpdated]   DATETIME      NULL,
    [UpdatedBy]     NVARCHAR (15) NULL,
    CONSTRAINT [PK_CheckList] PRIMARY KEY CLUSTERED ([CheckListID] ASC)
);

GO
CREATE TABLE [dbo].[CheckList_Task] (
    [TaskID]           INT            IDENTITY (1, 1) NOT NULL,
    [CheckListID]      NVARCHAR (15)  NOT NULL,
    [CheckListType]    TINYINT        NOT NULL,
    [Status]           TINYINT        NULL,
    [AssigneeType]     TINYINT        NULL,
    [AssigneeID]       NVARCHAR (64)  NULL,
    [DueDate]          DATETIME       NULL,
    [DeadlineDate]     DATETIME       NULL,
    [DateCreated]      DATETIME       NULL,
    [CompletedBy]      NVARCHAR (15)  NULL,
    [DateCompleted]    DATETIME       NULL,
    [CompletedRemarks] NVARCHAR (255) NULL,
    CONSTRAINT [PK_CheckList_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

GO
CREATE TABLE [dbo].[Cheque_Discount] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NOT NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [BankAccountID]      NVARCHAR (64)  NULL,
    [LiabilityAccountID] NVARCHAR (64)  NULL,
    [BankFacilityID]     NVARCHAR (15)  NULL,
    [BankChargeAmount]   MONEY          NULL,
    [BankCommission]     MONEY          NULL,
    [BankChargePercent]  MONEY          NULL,
    [IsVoid]             BIT            NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Cheque_Discount] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Cheque_Discount_Detail] (
    [SysDocID]         NVARCHAR (7)  NOT NULL,
    [VoucherID]        NVARCHAR (15) NOT NULL,
    [ChequeID]         INT           NOT NULL,
    [BankChargeAmount] MONEY         NULL,
    [DiscountAmount]   MONEY         NULL
);

GO
CREATE TABLE [dbo].[Cheque_Issued] (
    [ChequeID]           INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [ChequeNumber]       NVARCHAR (15)   NOT NULL,
    [PayeeType]          VARCHAR (1)     NOT NULL,
    [PayeeID]            NVARCHAR (64)   NOT NULL,
    [PayeeAccountID]     NVARCHAR (64)   NULL,
    [BankID]             NVARCHAR (15)   NULL,
    [ChequeDate]         DATETIME        NULL,
    [IssueDate]          DATETIME        NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [ExchangeRate]       DECIMAL (10, 5) NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [Note]               NVARCHAR (255)  NULL,
    [IsVoid]             BIT             NULL,
    [Status]             TINYINT         NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [PDCAccountID]       NVARCHAR (64)   NULL,
    [ChequebookID]       NVARCHAR (15)   NULL,
    [ClearanceDate]      DATETIME        NULL,
    [BankAccountID]      NVARCHAR (64)   NULL,
    [ClearanceSysDocID]  NVARCHAR (7)    NULL,
    [ClearanceVoucherID] NVARCHAR (15)   NULL,
    [ClearanceAccountID] NVARCHAR (64)   NULL,
    [IsPrinted]          BIT             NULL,
    [PrintDate]          DATETIME        NULL,
    [PrintName]          NVARCHAR (64)   NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Cheque_Issued_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [PayeeType] ASC, [PayeeID] ASC),
    CONSTRAINT [FK_Cheque_Issued_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID])
);

GO
CREATE TABLE [dbo].[Cheque_Received] (
    [ChequeID]              INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [ChequeNumber]          NVARCHAR (15)   NOT NULL,
    [BankID]                NVARCHAR (15)   NOT NULL,
    [PayeeType]             VARCHAR (1)     NOT NULL,
    [PayeeID]               NVARCHAR (64)   NOT NULL,
    [PayeeAccountID]        NVARCHAR (64)   NULL,
    [ChequeDate]            DATETIME        NULL,
    [ReceiptDate]           DATETIME        NULL,
    [CurrencyID]            NVARCHAR (5)    NULL,
    [ExchangeRate]          DECIMAL (10, 5) NULL,
    [Amount]                MONEY           NULL,
    [AmountFC]              MONEY           NULL,
    [ConAmountFC]           MONEY           NULL,
    [ConRate]               DECIMAL (18, 5) NULL,
    [Note]                  NVARCHAR (255)  NULL,
    [IsVoid]                BIT             CONSTRAINT [DF_Cheque_Received_IsVoid] DEFAULT ((0)) NULL,
    [Status]                TINYINT         CONSTRAINT [DF_Cheque_Received_Status] DEFAULT ((1)) NULL,
    [Reference]             NVARCHAR (20)   NULL,
    [PDCAccountID]          NVARCHAR (64)   NULL,
    [DepositDate]           DATETIME        NULL,
    [DepositAccountID]      NVARCHAR (64)   NULL,
    [DepositBankID]         NVARCHAR (15)   NULL,
    [DepositSysDocID]       NVARCHAR (7)    NULL,
    [DepositVoucherID]      NVARCHAR (15)   NULL,
    [SendDate]              DATETIME        NULL,
    [SendBankAccountID]     NVARCHAR (64)   NULL,
    [SendReference]         NVARCHAR (20)   NULL,
    [DiscountDate]          DATETIME        NULL,
    [DiscountAccountID]     NVARCHAR (64)   NULL,
    [DiscountBankAccountID] NVARCHAR (64)   NULL,
    [DiscountSysDocID]      NVARCHAR (7)    NULL,
    [DiscountVoucherID]     NVARCHAR (15)   NULL,
    [DiscountAmount]        MONEY           NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Cheque_Received] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [BankID] ASC, [PayeeType] ASC, [PayeeID] ASC),
    CONSTRAINT [FK_Cheque_Received_Account] FOREIGN KEY ([PDCAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Cheque_Received_Cheque_Received] FOREIGN KEY ([SysDocID], [VoucherID], [ChequeNumber], [BankID], [PayeeType], [PayeeID]) REFERENCES [dbo].[Cheque_Received] ([SysDocID], [VoucherID], [ChequeNumber], [BankID], [PayeeType], [PayeeID])
);

GO
CREATE TABLE [dbo].[Cheque_Register] (
    [ChequebookID]     NVARCHAR (15)  NOT NULL,
    [ChequeNumber]     INT            NOT NULL,
    [ChequeID]         INT            NULL,
    [Status]           TINYINT        CONSTRAINT [DF_Cheque_Register_Status] DEFAULT ((1)) NULL,
    [ReasonID]         NVARCHAR (15)  NULL,
    [IsSecurityCheque] BIT            NULL,
    [Note]             NVARCHAR (255) NULL,
    CONSTRAINT [PK_Cheque_Register] PRIMARY KEY CLUSTERED ([ChequebookID] ASC, [ChequeNumber] ASC)
);

GO
CREATE TABLE [dbo].[Cheque_Send] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NOT NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [BankAccountID]      NVARCHAR (64)  NULL,
    [Reason]             TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Cheque_Send] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Cheque_Send_Detail] (
    [SysDocID]  NVARCHAR (7)  NOT NULL,
    [VoucherID] NVARCHAR (15) NOT NULL,
    [ChequeID]  INT           NOT NULL
);

GO
CREATE TABLE [dbo].[Chequebook] (
    [ChequebookID]       NVARCHAR (15)  NOT NULL,
    [ChequebookName]     NVARCHAR (64)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [BankID]             NVARCHAR (15)  NULL,
    [AccountID]          NVARCHAR (64)  NULL,
    [PDCIssuedAccountID] NVARCHAR (64)  NULL,
    [NextNumber]         INT            NULL,
    [Signatory]          NVARCHAR (64)  NULL,
    [TemplateName]       NVARCHAR (64)  NULL,
    [Status]             TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Chequebook] PRIMARY KEY CLUSTERED ([ChequebookID] ASC)
);

GO
CREATE TABLE [dbo].[City] (
    [CityID]      NVARCHAR (15) NOT NULL,
    [CityName]    NVARCHAR (30) NULL,
    [CountryID]   NVARCHAR (15) NULL,
    [IsInactive]  BIT           CONSTRAINT [DF_City_IsInactive] DEFAULT ((0)) NULL,
    [DateUpdated] DATETIME      NULL,
    [DateCreated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC)
);

GO
CREATE TABLE [dbo].[CL_Token] (
    [TokenID]     INT             IDENTITY (1, 1) NOT NULL,
    [SystemKey]   NVARCHAR (64)   NULL,
    [TokenNumber] NVARCHAR (10)   NULL,
    [SysDocID]    NVARCHAR (7)    NULL,
    [VoucherID]   NVARCHAR (15)   NULL,
    [Amount]      DECIMAL (18, 5) NULL,
    [IssuedBy]    NVARCHAR (15)   NULL,
    [RequestedBy] NVARCHAR (15)   NULL,
    [IssueDate]   DATETIME        NULL,
    [Status]      TINYINT         NULL,
    [RequestDate] DATETIME        NULL,
    [CustomerID]  NVARCHAR (64)   NULL,
    [DateUpdated] DATETIME        NULL,
    [DateCreated] DATETIME        NULL,
    [CreatedBy]   NVARCHAR (15)   NULL,
    [UpdatedBy]   NVARCHAR (15)   NULL,
    CONSTRAINT [PK_CL_Token] PRIMARY KEY CLUSTERED ([TokenID] ASC)
);

GO
CREATE TABLE [dbo].[CL_Voucher] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [CustomerID]         NVARCHAR (64)   NULL,
    [VoucherDate]        DATETIME        NULL,
    [ValidFrom]          DATETIME        NULL,
    [ValidTo]            DATETIME        NULL,
    [Amount]             DECIMAL (18, 5) NULL,
    [Reason]             NVARCHAR (255)  NULL,
    [ApprovedBy]         NVARCHAR (15)   NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [IsVoid]             BIT             NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_CL_Voucher] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ClientAsset] (
    [ClientAssetID]   NVARCHAR (15)  NOT NULL,
    [ClientAssetName] NVARCHAR (64)  NULL,
    [JobID]           NVARCHAR (50)  NULL,
    [BrandID]         NVARCHAR (15)  NULL,
    [ManufacturerID]  NVARCHAR (15)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [StartDate]       DATETIME       NULL,
    [SerialNo]        NVARCHAR (30)  NULL,
    [ServiceByID]     NVARCHAR (15)  NULL,
    [Inactive]        BIT            NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_ClientAsset] PRIMARY KEY CLUSTERED ([ClientAssetID] ASC)
);

GO
CREATE TABLE [dbo].[COGS_Update_Log] (
    [LogID]          INT           IDENTITY (1, 1) NOT NULL,
    [BatchReference] NVARCHAR (15) NULL,
    [BatchDate]      DATETIME      NULL,
    [SysDocID]       NVARCHAR (7)  NULL,
    [VoucherID]      NVARCHAR (15) NULL,
    [ProductID]      NVARCHAR (64) NULL,
    [RowIndex]       INT           NULL,
    [OldCost]        MONEY         NULL,
    [NewCost]        MONEY         NULL,
    [OldCOGS]        MONEY         NULL,
    [NewCOGS]        MONEY         NULL,
    [TotalDiff]      MONEY         NULL,
    [COGSAccountID]  NVARCHAR (64) NULL,
    [AssetAccountID] NVARCHAR (64) NULL,
    CONSTRAINT [PK_COGS_Update_Log] PRIMARY KEY CLUSTERED ([LogID] ASC)
);

GO
CREATE TABLE [dbo].[Collateral] (
    [CollateralID]       NVARCHAR (15)  NOT NULL,
    [CollateralName]     NVARCHAR (64)  NULL,
    [PayeeType]          CHAR (1)       NULL,
    [PayeeID]            NVARCHAR (64)  NULL,
    [TypeID]             NVARCHAR (15)  NULL,
    [ExpiryDate]         DATETIME       NULL,
    [ReceiveDate]        DATETIME       NULL,
    [Amount]             MONEY          NULL,
    [CurrencyID]         NVARCHAR (5)   NULL,
    [BankID]             NVARCHAR (15)  NULL,
    [DocNo]              NVARCHAR (15)  NULL,
    [IsReturned]         BIT            NULL,
    [ReturnDate]         DATETIME       NULL,
    [ReturnNote]         NVARCHAR (255) NULL,
    [ReceiverName]       NVARCHAR (64)  NULL,
    [Status]             TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Collateral] PRIMARY KEY CLUSTERED ([CollateralID] ASC)
);

GO
CREATE TABLE [dbo].[Company] (
    [SetupID]                 TINYINT         IDENTITY (1, 1) NOT NULL,
    [CompanyID]               TINYINT         NOT NULL,
    [CompanyName]             NVARCHAR (64)   NOT NULL,
    [CompanyUID]              INT             NULL,
    [PrimaryAddressID]        NVARCHAR (15)   NULL,
    [FiscalFirstMonth]        TINYINT         CONSTRAINT [DF_Company Information_FiscalFirstMonth] DEFAULT ((1)) NULL,
    [DBVersion]               NVARCHAR (15)   NULL,
    [DBDataVersion]           NVARCHAR (15)   NULL,
    [LastBackupDate]          DATETIME        NULL,
    [Notes]                   NVARCHAR (255)  NULL,
    [Logo]                    IMAGE           NULL,
    [FileSavingPath]          NVARCHAR (255)  NULL,
    [TemplatePathLocation]    TINYINT         NULL,
    [TemplatePathFolder]      NVARCHAR (255)  NULL,
    [TemplatePathServer]      NVARCHAR (255)  NULL,
    [BaseCurrencyID]          NVARCHAR (15)   NULL,
    [CurDecimalPoint]         TINYINT         NULL,
    [UseLogo]                 BIT             CONSTRAINT [DF_Company Information_UseLogo] DEFAULT ((0)) NULL,
    [ClosingDate]             DATETIME        NULL,
    [ClosingPassword]         NVARCHAR (64)   NULL,
    [IsTax]                   BIT             CONSTRAINT [DF_Company Information_IsTax] DEFAULT ((1)) NULL,
    [TaxPercent]              DECIMAL (15, 2) NULL,
    [IsDNInventory]           BIT             NULL,
    [ItemPrice1Name]          NVARCHAR (15)   NULL,
    [ItemPrice2Name]          NVARCHAR (15)   NULL,
    [ItemPrice3Name]          NVARCHAR (15)   NULL,
    [PDCReceivedAccountID]    NVARCHAR (64)   NULL,
    [PDCIssuedAccountID]      NVARCHAR (64)   NULL,
    [AccountUD1]              NVARCHAR (15)   NULL,
    [AccountUD2]              NVARCHAR (15)   NULL,
    [AccountUD3]              NVARCHAR (15)   NULL,
    [AccountUD4]              NVARCHAR (15)   NULL,
    [CustomerUD1]             NVARCHAR (15)   NULL,
    [CustomerUD2]             NVARCHAR (15)   NULL,
    [CustomerUD3]             NVARCHAR (15)   NULL,
    [CustomerUD4]             NVARCHAR (15)   NULL,
    [VendorUD1]               NVARCHAR (15)   NULL,
    [VendorUD2]               NVARCHAR (15)   NULL,
    [VendorUD3]               NVARCHAR (15)   NULL,
    [VendorUD4]               NVARCHAR (15)   NULL,
    [EmployeeUD1]             NVARCHAR (15)   NULL,
    [EmployeeUD2]             NVARCHAR (15)   NULL,
    [EmployeeUD3]             NVARCHAR (15)   NULL,
    [EmployeeUD4]             NVARCHAR (15)   NULL,
    [InventoryUD1]            NVARCHAR (15)   NULL,
    [InventoryUD2]            NVARCHAR (15)   NULL,
    [InventoryUD3]            NVARCHAR (15)   NULL,
    [InventoryUD4]            NVARCHAR (15)   NULL,
    [MinPriceSaleAction]      TINYINT         NULL,
    [MinPriceSalePass]        NVARCHAR (5)    NULL,
    [OverCLAction]            TINYINT         NULL,
    [OverCLPass]              NVARCHAR (5)    NULL,
    [NegativeQuantityAction]  TINYINT         NULL,
    [NegativeQuantityPass]    NVARCHAR (5)    NULL,
    [PricelessCostAction]     TINYINT         NULL,
    [PricelessCostPass]       NVARCHAR (5)    NULL,
    [IncludePDC]              BIT             NULL,
    [UseMultiCurrency]        BIT             NULL,
    [UseJobCosting]           BIT             NULL,
    [AgingByDate]             BIT             NULL,
    [DiscountWriteoffPercent] DECIMAL (15, 2) NULL,
    [RemoveAllocationAction]  TINYINT         NULL,
    [LastLotNumber]           INT             NULL,
    [LocalPurchaseFlow]       TINYINT         NULL,
    [ImportPurchaseFlow]      TINYINT         NULL,
    [MinQtyPackingAction]     TINYINT         NULL,
    [SalesFlow]               TINYINT         NULL,
    [DaysInMonth]             BIT             NULL,
    [ThirtyDays]              BIT             NULL,
    [Annual]                  BIT             NULL,
    [AutoResumptionDays]      INT             NULL,
    [HRAnalysisGroup]         NVARCHAR (15)   NULL,
    [HRAnalysisPrefix]        NVARCHAR (15)   NULL,
    [VehicleAnalysisGroup]    NVARCHAR (15)   NULL,
    [VehicleAnalysisPrefix]   NVARCHAR (15)   NULL,
    [LegalAnalysisGroup]      NVARCHAR (15)   NULL,
    [LegalAnalysisPrefix]     NVARCHAR (15)   NULL,
    [PatientAnalysisGroup]    NVARCHAR (15)   NULL,
    [PatientAnalysisPrefix]   NVARCHAR (15)   NULL,
    [SMSUserName]             NVARCHAR (30)   NULL,
    [SMSPassword]             NVARCHAR (30)   NULL,
    [SMSMobileNo]             NVARCHAR (30)   NULL,
    [LotNoIdentity]           NVARCHAR (30)   NULL,
    [Reference2]              NVARCHAR (30)   NULL,
    [StatementEmailBody]      XML             NULL,
    [IsLocationCosting]       BIT             NULL,
    [TaxEntityTypes]          NVARCHAR (15)   NULL,
    [DefaultTaxOption]        TINYINT         NULL,
    [DefaultTaxGroupID]       NVARCHAR (30)   NULL,
    [UpdatedBy]               NVARCHAR (15)   NULL,
    [CreatedBy]               NVARCHAR (15)   NULL,
    [DateCreated]             DATETIME        NULL,
    [DateUpdated]             DATETIME        NULL,
    CONSTRAINT [PK_Company Information] PRIMARY KEY CLUSTERED ([SetupID] ASC),
    CONSTRAINT [CK_Company Information] CHECK ([FiscalFirstMonth] >= (1)
                                               AND [FiscalFirstMonth] <= (12)),
    CONSTRAINT [FK_Company Information_Users] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Company Information_Users1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([UserID])
);

GO
CREATE TABLE [dbo].[Company_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Comment]            NVARCHAR (255) NULL,
    [Inactive]           BIT            NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Company_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC)
);

GO
CREATE TABLE [dbo].[Company_Division] (
    [DivisionID]   NVARCHAR (15)  NOT NULL,
    [DivisionName] NVARCHAR (64)  NULL,
    [CompanyID]    TINYINT        NOT NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            CONSTRAINT [DF_Company_Division_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Company_Division] PRIMARY KEY CLUSTERED ([DivisionID] ASC)
);

GO
CREATE TABLE [dbo].[Company_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Company_Doc_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Company_Doc_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Company_Document] (
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [SponsorID]      NVARCHAR (15)  NULL,
    [DocumentName]   NVARCHAR (64)  NULL,
    [EmployeeID]     NVARCHAR (15)  NULL,
    [RegisterNumber] NVARCHAR (20)  NULL,
    [FileNumber]     NVARCHAR (20)  NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Company_Document] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Company_Option] (
    [OptionID]    NVARCHAR (64) NULL,
    [OptionValue] NVARCHAR (64) NULL,
    [GroupID]     TINYINT       NULL,
    [SysDocID]    NVARCHAR (7)  NULL,
    [SysDocType]  INT           NULL
);

GO
CREATE TABLE [dbo].[Company_Setup] (
    [CompanyID]     INT NOT NULL,
    [IsDNInventory] BIT NULL
);

GO
CREATE TABLE [dbo].[Competitor] (
    [CompetitorID]   NVARCHAR (15)  NOT NULL,
    [CompetitorName] NVARCHAR (64)  NOT NULL,
    [Note]           NVARCHAR (255) NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Consign_In] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignLocationID]  NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [VendorAddress]      NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Consign_In_Status] DEFAULT ((1)) NULL,
    [IsClosed]           BIT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsInvoiced]         BIT             NULL,
    [TransporterID]      NVARCHAR (50)   NULL,
    [ArrivalPort]        NVARCHAR (30)   NULL,
    [ArrivalDate]        DATETIME        NULL,
    [ContainerNo]        NVARCHAR (30)   NULL,
    [BLNo]               NVARCHAR (30)   NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [CloseDate]          DATETIME        NULL,
    [CloseNote]          NVARCHAR (255)  NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Consign_In] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Consign_In_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [QuantitySettled]   DECIMAL (18, 5) NULL,
    [QuantityReturned]  DECIMAL (18, 5) NULL,
    [QuantitySold]      DECIMAL (18, 5) NULL,
    [QuantityDamage]    DECIMAL (18, 5) NULL,
    [UnitPrice]         DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [SubunitPrice]      DECIMAL (18, 5) NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [OrderVoucherID]    NVARCHAR (15)   NULL,
    [OrderSysDocID]     NVARCHAR (7)    NULL,
    [InvoiceRowIndex]   INT             NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [ConsignSysDocID]   NVARCHAR (15)   NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Consign_Out] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [IsExport]           BIT             NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignLocationID]  NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Consign_Out_Status] DEFAULT ((1)) NULL,
    [SourceDocType]      TINYINT         NULL,
    [SourceSysDocID]     NVARCHAR (7)    NULL,
    [SourceVoucherID]    NVARCHAR (15)   NULL,
    [IsClosed]           BIT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsInvoiced]         BIT             NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Consign_Out] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Consign_Out_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [QuantitySettled]   DECIMAL (18, 5) NULL,
    [QuantityReturned]  DECIMAL (18, 5) NULL,
    [UnitPrice]         DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [MarketPrice]       DECIMAL (18, 5) NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [SubunitPrice]      DECIMAL (18, 5) NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceRowIndex]    INT             NULL,
    [SourceDocType]     TINYINT         NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [ConsignSysDocID]   NVARCHAR (15)   NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL,
    [ITRowID]           INT             NULL
);

GO
CREATE TABLE [dbo].[ConsignIn_Expense] (
    [SysDocID]        NVARCHAR (7)    NULL,
    [VoucherID]       NVARCHAR (15)   NULL,
    [ExpenseID]       NVARCHAR (15)   NULL,
    [BillableAmount]  MONEY           NULL,
    [SourceRowIndex]  INT             NULL,
    [SourceSysDocID]  NVARCHAR (15)   NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [Reference]       NVARCHAR (15)   NULL,
    [CurrencyID]      NVARCHAR (15)   NULL,
    [CurrencyRate]    DECIMAL (18, 5) NULL,
    [RateType]        CHAR (1)        NULL
);

GO
CREATE TABLE [dbo].[ConsignIn_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [VendorID]          NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [ConsignSysDocID]   NVARCHAR (7)    NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [VendorAddress]     NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_ConsignIn_Return_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_ConsignIn_Return_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_ConsignIn_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ConsignIn_Return_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [COGS]              DECIMAL (18, 5) NULL,
    [ConsignSysDocID]   NVARCHAR (7)    NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL,
    [ConsignRowIndex]   INT             NULL
);

GO
CREATE TABLE [dbo].[ConsignIn_Sale] (
    [RowID]            INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]         NVARCHAR (7)    NULL,
    [VoucherID]        NVARCHAR (15)   NULL,
    [RowIndex]         INT             NULL,
    [ProductID]        NVARCHAR (64)   NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [ConsignSysDocID]  NVARCHAR (7)    NULL,
    [ConsignVoucherID] NVARCHAR (15)   NULL,
    [ConsignRowIndex]  INT             NULL,
    [SoldQty]          DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    CONSTRAINT [PK_ConsignIn_Sale] PRIMARY KEY CLUSTERED ([RowID] ASC)
);

GO
CREATE TABLE [dbo].[ConsignIn_Settled_Items] (
    [SysDocID]   NVARCHAR (7)    NULL,
    [VoucherID]  NVARCHAR (15)   NULL,
    [SalesRowID] INT             NULL,
    [ProductID]  NVARCHAR (64)   NULL,
    [UnitPrice]  DECIMAL (18, 5) NULL,
    [Quantity]   DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[ConsignIn_Settlement] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [IsCash]             BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignSysDocID]    NVARCHAR (7)    NULL,
    [ConsignVoucherID]   NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [VendorAddress]      NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_ConsignIn_Settlement_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [CommissionPercent]  MONEY           NULL,
    [CommissionAmount]   MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_ConsignIn_Settlement_IsDelivered] DEFAULT ((0)) NULL,
    [Note]               NVARCHAR (4000) NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [RequireUpdate]      BIT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_ConsignIn_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ConsignIn_Settlement_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [ExpenseAmount]    MONEY           NULL,
    [UnitExpense]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         INT             NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [COGS]             MONEY           NULL,
    [ConsignSysDocID]  NVARCHAR (7)    NULL,
    [ConsignVoucherID] NVARCHAR (15)   NULL,
    [ConsignRowIndex]  INT             NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [DNoteVoucherID]   NVARCHAR (15)   NULL,
    [DNoteSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsDNRow]          BIT             NULL,
    [IsRecost]         BIT             NULL,
    [ITRowID]          INT             NULL
);

GO
CREATE TABLE [dbo].[ConsignIn_Settlement_Price] (
    [SysDocID]  NVARCHAR (7)    NULL,
    [VoucherID] NVARCHAR (15)   NULL,
    [ProductID] NVARCHAR (64)   NULL,
    [RowIndex]  INT             NULL,
    [Quantity]  DECIMAL (18, 5) NULL,
    [Price]     DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[ConsignOut_Expense] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (255)  NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [IsDeduct]     BIT             NULL
);

GO
CREATE TABLE [dbo].[ConsignOut_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [ConsignSysDocID]   NVARCHAR (7)    NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_ConsignOut_Return_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_ConsignOut_Return_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_ConsignOut_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ConsignOut_Return_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [COGS]              MONEY           NULL,
    [ConsignSysDocID]   NVARCHAR (7)    NULL,
    [ConsignVoucherID]  NVARCHAR (15)   NULL,
    [ConsignRowIndex]   INT             NULL,
    [ITRowID]           INT             NULL
);

GO
CREATE TABLE [dbo].[ConsignOut_Settlement] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [IsCash]             BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignSysDocID]    NVARCHAR (7)    NULL,
    [ConsignVoucherID]   NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_ConsignOut_Settlement_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_ConsignOut_Settlement_IsDelivered] DEFAULT ((0)) NULL,
    [Note]               NVARCHAR (4000) NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [RequireUpdate]      BIT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_ConsignOut_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ConsignOut_Settlement_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [ExpenseAmount]    MONEY           NULL,
    [UnitExpense]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     REAL            NULL,
    [UnitFactor]       REAL            NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         TINYINT         NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [COGS]             MONEY           NULL,
    [ConsignSysDocID]  NVARCHAR (7)    NULL,
    [ConsignVoucherID] NVARCHAR (15)   NULL,
    [ConsignRowIndex]  INT             NULL,
    [QuantityShipped]  REAL            NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [DNoteVoucherID]   NVARCHAR (15)   NULL,
    [DNoteSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsDNRow]          BIT             NULL,
    [IsRecost]         BIT             NULL,
    [ITRowID]          INT             NULL
);

GO
CREATE TABLE [dbo].[Contact] (
    [ContactID]          NVARCHAR (64)  NOT NULL,
    [FirstName]          NVARCHAR (64)  NULL,
    [LastName]           NVARCHAR (64)  NULL,
    [MiddleName]         NVARCHAR (64)  NULL,
    [NickName]           NVARCHAR (64)  NULL,
    [Inactive]           BIT            NULL,
    [Photo]              IMAGE          NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (15)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [JobTitle]           NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email1]             NVARCHAR (64)  NULL,
    [Email2]             NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (64)  NULL,
    [Address]            NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [Note]               NTEXT          NULL,
    [BirthDate]          DATETIME       NULL,
    [SpouseName]         NVARCHAR (64)  NULL,
    [IMAddress]          NVARCHAR (255) NULL,
    [Anniversary]        DATETIME       NULL,
    [ManagerName]        NVARCHAR (64)  NULL,
    [AssistantName]      NVARCHAR (64)  NULL,
    [ChildrenName]       NVARCHAR (255) NULL,
    [Nationality]        NVARCHAR (30)  NULL,
    [Gender]             CHAR (1)       NULL,
    [Hobbies]            NVARCHAR (255) NULL,
    [Language]           NVARCHAR (255) NULL,
    [BankName]           NCHAR (30)     NULL,
    [BankAccountNumber]  NCHAR (30)     NULL,
    [LinkedIn]           NVARCHAR (255) NULL,
    [Twitter]            NVARCHAR (30)  NULL,
    [Facebook]           NVARCHAR (30)  NULL,
    [Skype]              NVARCHAR (30)  NULL,
    [UserDefined1]       NVARCHAR (64)  NULL,
    [UserDefined2]       NVARCHAR (64)  NULL,
    [UserDefined3]       NVARCHAR (64)  NULL,
    [UserDefined4]       NVARCHAR (64)  NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Container_Tracking] (
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [VendorID]              NVARCHAR (64)   NOT NULL,
    [TransactionDate]       DATETIME        NOT NULL,
    [PurchaseFlow]          NVARCHAR (15)   NULL,
    [ContainerNumber]       NVARCHAR (15)   NULL,
    [DestinationPort]       NVARCHAR (15)   NULL,
    [LoadingPort]           NVARCHAR (15)   NULL,
    [Weight]                DECIMAL (18, 5) NULL,
    [IsReceived]            BIT             NULL,
    [ETA]                   DATETIME        NULL,
    [ATD]                   DATETIME        NULL,
    [Status]                TINYINT         CONSTRAINT [DF_Container_Tracking_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]      NVARCHAR (15)   NULL,
    [IsVoid]                BIT             NULL,
    [Reference]             NVARCHAR (20)   NULL,
    [PONumber]              NVARCHAR (20)   NULL,
    [BOLNumber]             NVARCHAR (20)   NULL,
    [Shipper]               NVARCHAR (15)   NULL,
    [Note]                  NVARCHAR (255)  NULL,
    [Container_Status]      INT             NULL,
    [ActivityDoneBy]        NVARCHAR (20)   NULL,
    [DeliveryDate]          DATE            NULL,
    [DeliveryTime]          TIME (7)        NULL,
    [FreeTimeTODeliver]     DATETIME        NULL,
    [TruckNumber]           NVARCHAR (50)   NULL,
    [IsBL]                  BIT             NULL,
    [IsInvoice]             BIT             NULL,
    [IsPL]                  BIT             NULL,
    [IsHealthCertficate]    BIT             NULL,
    [IsCertificateOfOrigin] BIT             NULL,
    [Remarks]               NVARCHAR (50)   NULL,
    [DriverID]              NVARCHAR (50)   NULL,
    [TransportCompany]      NVARCHAR (50)   NULL,
    [TransporterID]         NVARCHAR (50)   NULL,
    [ContainerSizeID]       NVARCHAR (30)   NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[ContainerSize] (
    [ContainerSizeID]   NVARCHAR (30)  NOT NULL,
    [ContainerSizeName] NVARCHAR (64)  NOT NULL,
    [Note]              NVARCHAR (255) NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL
);

GO
CREATE TABLE [dbo].[Cost_Center] (
    [CostCenterID]          NVARCHAR (15)  NOT NULL,
    [CostCenterName]        NVARCHAR (30)  NOT NULL,
    [CashReceiptSysDocID]   NVARCHAR (7)   NULL,
    [ChequeReceiptSysDocID] NVARCHAR (7)   NULL,
    [CashPaymentSysDocID]   NVARCHAR (7)   NULL,
    [ChequePaymentSysDocID] NVARCHAR (7)   NULL,
    [Note]                  NVARCHAR (255) NULL,
    [Inactive]              BIT            CONSTRAINT [DF_Cost_Center_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Cost_Center] PRIMARY KEY CLUSTERED ([CostCenterID] ASC)
);

GO
CREATE TABLE [dbo].[Cost_Updation] (
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [SysDocID]          NVARCHAR (7)  NULL,
    [VoucherID]         NVARCHAR (15) NULL,
    [ProductID]         NVARCHAR (64) NULL,
    [InvoiceLocationID] NVARCHAR (15) NULL,
    [RowIndex]          INT           NULL,
    [Amount]            MONEY         NULL,
    [IsApplied]         BIT           NULL,
    [ApplySysDocID]     NVARCHAR (7)  NULL,
    [ApplyVoucherID]    NVARCHAR (15) NULL,
    CONSTRAINT [PK_Cost_Updation] PRIMARY KEY CLUSTERED ([ID] ASC)
);

GO
CREATE TABLE [dbo].[Country] (
    [CountryID]    NVARCHAR (15)  NOT NULL,
    [CountryName]  NVARCHAR (64)  NOT NULL,
    [PhoneCode]    NVARCHAR (15)  NULL,
    [CurrencyCode] NVARCHAR (15)  NULL,
    [Inactive]     BIT            NULL,
    [Note]         NVARCHAR (255) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

GO
CREATE TABLE [dbo].[Credit_Limit_Review] (
    [SysDocID]                NVARCHAR (7)   NOT NULL,
    [VoucherID]               NVARCHAR (15)  NOT NULL,
    [CustomerID]              NVARCHAR (64)  NOT NULL,
    [Rating]                  TINYINT        NULL,
    [RatingBy]                NVARCHAR (15)  NULL,
    [RatingDate]              DATETIME       NULL,
    [RatingRemarks]           NVARCHAR (255) NULL,
    [AcceptCheckPayment]      BIT            NULL,
    [AcceptPDC]               BIT            NULL,
    [CreditLimitType]         TINYINT        NULL,
    [CreditAmount]            MONEY          NULL,
    [ApprovalStatus]          TINYINT        NULL,
    [VerificationStatus]      TINYINT        NULL,
    [TransactionDate]         DATETIME       NOT NULL,
    [CLValidity]              DATE           NULL,
    [LimitPDCUnsecured]       BIT            NULL,
    [PDCUnsecuredLimitAmount] MONEY          NULL,
    [GraceDays]               TINYINT        NULL,
    [DateCreated]             DATETIME       NULL,
    [DateUpdated]             DATETIME       NULL,
    [CreatedBy]               NVARCHAR (15)  NULL,
    [UpdatedBy]               NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Credit_Limit_Review] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Currency] (
    [CurrencyID]      NVARCHAR (5)    NOT NULL,
    [CurrencyName]    NVARCHAR (30)   NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [ExchangeRate]    DECIMAL (18, 5) NULL,
    [RateUpdatedDate] DATETIME        NULL,
    [RateType]        CHAR (1)        NULL,
    [Inactive]        BIT             CONSTRAINT [DF_Currency_Inactive] DEFAULT ((0)) NULL,
    [IsBase]          BIT             NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([CurrencyID] ASC),
    CONSTRAINT [IX_Currencies] UNIQUE NONCLUSTERED ([CurrencyName] ASC)
);

GO
CREATE TABLE [dbo].[Currency_Exchange_Rate] (
    [CurrencyID]      NVARCHAR (5)    NULL,
    [ExchangeRate]    DECIMAL (10, 5) NULL,
    [RateUpdatedDate] DATETIME        NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (64)   NULL,
    [UpdatedBy]       NVARCHAR (64)   NULL
);

GO
CREATE TABLE [dbo].[Custom_Gadget] (
    [CustomGadgetID]                 NVARCHAR (30)  NOT NULL,
    [CustomGadgetName]               NVARCHAR (64)  NULL,
    [GadgetData]                     IMAGE          NULL,
    [GadgetStyle]                    TINYINT        NULL,
    [ChartValueColumn]               NVARCHAR (30)  NULL,
    [ChartArgColumn]                 NVARCHAR (30)  NULL,
    [ChartType]                      TINYINT        NULL,
    [ChartPallet]                    TINYINT        NULL,
    [ColorPaletteName]               NVARCHAR (20)  NULL,
    [ColorEach]                      BIT            NULL,
    [FilterOption]                   TINYINT        NULL,
    [IsInactive]                     BIT            NULL,
    [CategoryID]                     NVARCHAR (15)  NULL,
    [DrillAction]                    TINYINT        NULL,
    [DrillCardTypeID]                INT            NULL,
    [DrillCardIDField]               NVARCHAR (30)  NULL,
    [DrillTransactionSysDocIDField]  NVARCHAR (30)  NULL,
    [DrillTransactionVoucherIDField] NVARCHAR (30)  NULL,
    [AutoRefresh]                    BIT            NULL,
    [RefreshInterval]                INT            NULL,
    [ShowLegend]                     BIT            NULL,
    [IndValueColumn]                 NVARCHAR (30)  NULL,
    [IndTextValueColumn]             NVARCHAR (30)  NULL,
    [ShowName]                       BIT            NULL,
    [DisplayName]                    NVARCHAR (50)  NULL,
    [Description]                    NVARCHAR (500) NULL,
    [ShowIndicator]                  BIT            NULL,
    [TextColor]                      INT            NULL,
    [AxisYTitle]                     NVARCHAR (30)  NULL,
    [AxisXTitle]                     NVARCHAR (30)  NULL,
    [AxisYTextPattern]               NVARCHAR (30)  NULL,
    [IsRotated]                      BIT            NULL,
    [AxisYVisible]                   BIT            NULL,
    [LegendTextPattern]              NVARCHAR (50)  NULL,
    [GShowGaugeText]                 BIT            NULL,
    [GShowNeedle]                    BIT            NULL,
    [TopNOption]                     BIT            NULL,
    [TopNCount]                      TINYINT        NULL,
    [TopNOthersText]                 NVARCHAR (30)  NULL,
    [ShowTopNOther]                  BIT            NULL,
    [DrillParm1]                     NVARCHAR (30)  NULL,
    [DrillParm2]                     NVARCHAR (30)  NULL,
    [DrillParm3]                     NVARCHAR (30)  NULL,
    [DrillParm4]                     NVARCHAR (30)  NULL,
    [IsSubReport]                    BIT            NULL,
    [DrillSubReportID]               INT            NULL,
    [IsPreview]                      BIT            NULL,
    [Photo]                          IMAGE          NULL,
    [DateCreated]                    DATETIME       NULL,
    [DateUpdated]                    DATETIME       NULL,
    [CreatedBy]                      NVARCHAR (15)  NULL,
    [UpdatedBy]                      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Custom_Gadget] PRIMARY KEY CLUSTERED ([CustomGadgetID] ASC)
);

GO
CREATE TABLE [dbo].[Custom_Report] (
    [CustomReportID]   NVARCHAR (15)   NOT NULL,
    [CustomReportName] NVARCHAR (64)   NULL,
    [ParentMenuName]   NVARCHAR (64)   NULL,
    [ReportData]       IMAGE           NULL,
    [IsInactive]       BIT             NULL,
    [FormWidth]        INT             NULL,
    [FormHeight]       INT             NULL,
    [Layout]           IMAGE           NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [TemplateName]     NVARCHAR (64)   NULL,
    [Query]            NVARCHAR (500)  NULL,
    [Note]             NVARCHAR (4000) NULL,
    [DisplayNote]      NVARCHAR (4000) NULL,
    CONSTRAINT [PK_Custom_Report] PRIMARY KEY CLUSTERED ([CustomReportID] ASC)
);

GO
CREATE TABLE [dbo].[Custom_Report_Security] (
    [MenuID]     NVARCHAR (255) NULL,
    [Enable]     BIT            NULL,
    [Visible]    BIT            NULL,
    [UserID]     NVARCHAR (15)  NULL,
    [GroupID]    NVARCHAR (15)  NULL,
    [ReportType] TINYINT        NULL
);

GO
CREATE TABLE [dbo].[Customer] (
    [CustomerID]              NVARCHAR (64)   NOT NULL,
    [CustomerName]            NVARCHAR (64)   NOT NULL,
    [ShortName]               NVARCHAR (64)   NULL,
    [ForeignName]             NVARCHAR (64)   NULL,
    [CompanyName]             NVARCHAR (64)   NULL,
    [LegalName]               NVARCHAR (64)   NULL,
    [CustomerClassID]         NVARCHAR (15)   NULL,
    [CollectionUserID]        NVARCHAR (15)   NULL,
    [InsRating]               TINYINT         NULL,
    [RatingBy]                NVARCHAR (15)   NULL,
    [RatingDate]              DATETIME        NULL,
    [RatingRemarks]           NVARCHAR (255)  NULL,
    [StatementEmail]          NVARCHAR (255)  NULL,
    [Flag]                    TINYINT         NULL,
    [POSHidden]               BIT             NULL,
    [ContactName]             NVARCHAR (64)   NULL,
    [CurrencyID]              NVARCHAR (5)    NULL,
    [TermID]                  NVARCHAR (15)   NULL,
    [AreaID]                  NVARCHAR (15)   NULL,
    [SubAreaID]               NVARCHAR (15)   NULL,
    [Rating]                  TINYINT         NULL,
    [AcceptCheckPayment]      BIT             NULL,
    [AcceptPDC]               BIT             NULL,
    [CreditLimitType]         TINYINT         NULL,
    [CreditAmount]            MONEY           NULL,
    [CLValidity]              DATE            NULL,
    [LimitPDCUnsecured]       BIT             NULL,
    [GraceDays]               TINYINT         NULL,
    [PDCUnsecuredLimitAmount] MONEY           NULL,
    [BalanceConfirmationDate] DATETIME        NULL,
    [ConfirmationInterval]    TINYINT         NULL,
    [ARAccountID]             NVARCHAR (64)   NULL,
    [ParentCustomerID]        NVARCHAR (64)   NULL,
    [IsParentPosting]         BIT             NULL,
    [CustomerGroupID]         NVARCHAR (15)   NULL,
    [BillToAddressID]         NVARCHAR (15)   CONSTRAINT [DF_Customer_BillToAddressID] DEFAULT (N'PRIMARY') NULL,
    [ShipToAddressID]         NVARCHAR (15)   CONSTRAINT [DF_Customer_ShipToAddressID] DEFAULT (N'PRIMARY') NULL,
    [StatementAddressID]      NVARCHAR (15)   CONSTRAINT [DF_Customer_StatementAddressID] DEFAULT (N'PRIMARY') NULL,
    [CountryID]               NVARCHAR (15)   NULL,
    [ShippingMethodID]        NVARCHAR (15)   NULL,
    [IsInactive]              BIT             NULL,
    [IsHold]                  BIT             NULL,
    [Photo]                   IMAGE           NULL,
    [BankName]                NVARCHAR (30)   NULL,
    [BankBranch]              NVARCHAR (30)   NULL,
    [BankAccountNumber]       NVARCHAR (30)   NULL,
    [TaxOption]               TINYINT         NULL,
    [TaxGroupID]              NVARCHAR (15)   NULL,
    [TaxIDNumber]             NVARCHAR (30)   NULL,
    [InsStatus]               TINYINT         NULL,
    [InsApplicationDate]      DATETIME        NULL,
    [InsEffectiveDate]        DATETIME        NULL,
    [InsExpiryDate]           DATETIME        NULL,
    [InsRequestedAmount]      MONEY           NULL,
    [InsApprovedAmount]       MONEY           NULL,
    [InsPolicyNumber]         NVARCHAR (30)   NULL,
    [InsRemarks]              NVARCHAR (255)  NULL,
    [InsProviderID]           NVARCHAR (15)   NULL,
    [InsuranceID]             NVARCHAR (30)   NULL,
    [PaymentTermID]           NVARCHAR (15)   NULL,
    [DivisionID]              NVARCHAR (15)   NULL,
    [CreditReviewBy]          NVARCHAR (15)   NULL,
    [CreditReviewDate]        DATETIME        NULL,
    [Note]                    NTEXT           NULL,
    [DateEstablished]         DATETIME        NULL,
    [PaymentMethodID]         NVARCHAR (15)   NULL,
    [SalesPersonID]           NVARCHAR (64)   NULL,
    [PriceLevelID]            NVARCHAR (1)    NULL,
    [PrimaryAddressID]        NVARCHAR (15)   NULL,
    [StatementSendingMethod]  TINYINT         NULL,
    [AllowConsignment]        BIT             NULL,
    [CollectionRemarks]       NVARCHAR (300)  NULL,
    [ConsignComPercent]       BIT             NULL,
    [DiscountPercent]         DECIMAL (18, 2) NULL,
    [RebatePercent]           DECIMAL (18, 2) NULL,
    [PDCAmount]               MONEY           NULL,
    [LicenseNumber]           NVARCHAR (30)   NULL,
    [LicenseExpDate]          DATETIME        NULL,
    [ContractExpDate]         DATETIME        NULL,
    [Balance]                 MONEY           NULL,
    [LeadSourceID]            NVARCHAR (15)   NULL,
    [SourceLeadID]            NVARCHAR (15)   NULL,
    [IsWeightInvoice]         BIT             NULL,
    [IsCustomerSince]         DATETIME        NULL,
    [ProfileDetails]          NTEXT           NULL,
    [UserDefined1]            NVARCHAR (64)   NULL,
    [UserDefined2]            NVARCHAR (64)   NULL,
    [UserDefined3]            NVARCHAR (64)   NULL,
    [UserDefined4]            NVARCHAR (64)   NULL,
    [ApprovalStatus]          TINYINT         NULL,
    [VerificationStatus]      TINYINT         NULL,
    [DeliveryInstructions]    NVARCHAR (500)  NULL,
    [AccountInstructions]     NVARCHAR (500)  NULL,
    [CustomerSignature]       IMAGE           NULL,
    [DateCreated]             DATETIME        NULL,
    [DateUpdated]             DATETIME        NULL,
    [CreatedBy]               NVARCHAR (15)   NULL,
    [UpdatedBy]               NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_Customer_Account] FOREIGN KEY ([ARAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Customer_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[Area] ([AreaID]),
    CONSTRAINT [FK_Customer_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]),
    CONSTRAINT [FK_Customer_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([CurrencyID]),
    CONSTRAINT [FK_Customer_Customer] FOREIGN KEY ([ParentCustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]),
    CONSTRAINT [FK_Customer_Customer_Class] FOREIGN KEY ([CustomerClassID]) REFERENCES [dbo].[Customer_Class] ([ClassID]),
    CONSTRAINT [FK_Customer_Customer_Group] FOREIGN KEY ([CustomerGroupID]) REFERENCES [dbo].[Customer_Group] ([GroupID]),
    CONSTRAINT [FK_Customer_Division] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[Division] ([DivisionID]),
    CONSTRAINT [FK_Customer_Payment_Method] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[Payment_Method] ([PaymentMethodID]),
    CONSTRAINT [FK_Customer_Payment_Term] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]),
    CONSTRAINT [FK_Customer_Payment_Term1] FOREIGN KEY ([PaymentTermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]),
    CONSTRAINT [FK_Customer_Salesperson] FOREIGN KEY ([SalesPersonID]) REFERENCES [dbo].[Salesperson] ([SalespersonID]),
    CONSTRAINT [FK_Customer_Shipping_Method] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Shipping_Method] ([ShippingMethodID])
);

GO
CREATE TABLE [dbo].[Customer_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [CustomerID]         NVARCHAR (64)  NOT NULL,
    [AddressName]        NVARCHAR (64)  NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Latitude]           NVARCHAR (30)  NULL,
    [Longitude]          NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Twitter]            NVARCHAR (30)  NULL,
    [Facebook]           NVARCHAR (255) NULL,
    [Skype]              NVARCHAR (30)  NULL,
    [Comment]            NVARCHAR (255) NULL,
    [Inactive]           BIT            NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Customer_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CustomerID] ASC),
    CONSTRAINT [FK_Customer_Address_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[Customer_Category] (
    [CategoryID]   NVARCHAR (15)  NOT NULL,
    [CategoryName] NVARCHAR (15)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            CONSTRAINT [DF_Customer_Category_Inactive] DEFAULT ((0)) NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Customer_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Category_Detail] (
    [CustomerID] NVARCHAR (64) NOT NULL,
    [CategoryID] NVARCHAR (15) NOT NULL,
    [EntityType] TINYINT       NULL,
    CONSTRAINT [PK_Customer_Category_Detail] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Class] (
    [ClassID]      NVARCHAR (15)  NOT NULL,
    [ClassName]    NVARCHAR (64)  NOT NULL,
    [HasPOSAccess] BIT            NULL,
    [IsLPO]        BIT            NULL,
    [IsPRO]        BIT            NULL,
    [ARAccountID]  NVARCHAR (64)  NULL,
    [TaxOption]    TINYINT        NULL,
    [TaxGroupID]   NVARCHAR (15)  NULL,
    [IsInactive]   BIT            NULL,
    [Note]         NVARCHAR (255) NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Customer_Group] PRIMARY KEY CLUSTERED ([ClassID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Contact_Detail] (
    [CustomerID] NVARCHAR (64) NOT NULL,
    [ContactID]  NVARCHAR (64) NOT NULL,
    [JobTitle]   NVARCHAR (30) NULL,
    [Note]       NVARCHAR (64) NULL,
    [RowIndex]   SMALLINT      NULL,
    CONSTRAINT [PK_Customer_Contact_Detail] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Group] (
    [GroupID]     NVARCHAR (15)  NOT NULL,
    [GroupName]   NVARCHAR (30)  NOT NULL,
    [POSAccess]   BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            CONSTRAINT [DF_Customer_Group_Inactive] DEFAULT ((0)) NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Customer_Group_1] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Insurance] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [InsPolicyNumber]    NVARCHAR (30)  NULL,
    [ReviewDate]         DATETIME       NULL,
    [CustomerID]         NVARCHAR (64)  NULL,
    [InsuranceID]        NVARCHAR (30)  NULL,
    [InsStatus]          TINYINT        NULL,
    [InsApplicationDate] DATETIME       NULL,
    [InsRating]          TINYINT        NULL,
    [InsRequestedAmount] MONEY          NULL,
    [InsApprovedAmount]  MONEY          NULL,
    [InsProvider]        NVARCHAR (50)  NULL,
    [InsRemarks]         NVARCHAR (255) NULL,
    [InsEffectiveDate]   DATETIME       NULL,
    [InsValidTo]         DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Customer_Insurance] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Insurance_Claim] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [CustomerID]         NVARCHAR (64)  NULL,
    [InsStatus]          TINYINT        NULL,
    [InsProviderID]      NVARCHAR (15)  NULL,
    [InsApplicationDate] DATETIME       NULL,
    [InsPayableAmount]   MONEY          NULL,
    [InsEffectiveDate]   DATETIME       NULL,
    [InsRemarks]         NVARCHAR (255) NULL,
    [InsPolicyNumber]    NVARCHAR (30)  NULL,
    [InsApprovedAmount]  MONEY          NULL,
    [InsuranceID]        NVARCHAR (30)  NULL,
    [InsExpiryDate]      DATETIME       NULL,
    [ClaimAmount]        MONEY          NULL,
    [PaidAmount]         MONEY          NULL,
    [Reason]             NVARCHAR (255) NULL,
    [CustomerInsRemarks] NVARCHAR (255) NULL,
    [PaidDate]           DATETIME       NULL,
    [CustomerInsStatus]  NVARCHAR (30)  NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [DateCreated]        DATETIME       NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    [DateUpdated]        DATETIME       NULL
);

GO
CREATE TABLE [dbo].[Customer_Vendor_Link] (
    [PartyID]     NVARCHAR (15)  NOT NULL,
    [PartyName]   NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Customer_Vendor_Link] PRIMARY KEY CLUSTERED ([PartyID] ASC)
);

GO
CREATE TABLE [dbo].[Customer_Vendor_Link_Detail] (
    [PartyID]    NVARCHAR (15) NOT NULL,
    [AccountID]  NVARCHAR (15) NOT NULL,
    [EntityType] TINYINT       NOT NULL
);

GO
CREATE TABLE [dbo].[Dashboard] (
    [DashboardID] NVARCHAR (30) NOT NULL,
    [UserID]      NVARCHAR (15) NOT NULL,
    [Name]        NVARCHAR (15) NULL,
    [RowIndex]    INT           NULL,
    [Layout]      IMAGE         NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Dashboards] PRIMARY KEY CLUSTERED ([DashboardID] ASC, [UserID] ASC)
);

GO
CREATE TABLE [dbo].[Data_Patch] (
    [PatchID]          NVARCHAR (15)  NOT NULL,
    [PatchDescription] NVARCHAR (255) NULL,
    [PatchQuery]       NTEXT          NULL,
    [Status]           TINYINT        NULL,
    [DateExecuted]     DATETIME       NULL,
    [DataVersion]      NVARCHAR (15)  NULL,
    [DateCreated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Data_Patch] PRIMARY KEY CLUSTERED ([PatchID] ASC)
);

GO
CREATE TABLE [dbo].[Data_Sync] (
    [Type]         INT            NOT NULL,
    [Code]         NVARCHAR (15)  NOT NULL,
    [Name]         NVARCHAR (30)  NOT NULL,
    [DatabaseName] NVARCHAR (100) NULL,
    [ServerID]     NVARCHAR (50)  NULL,
    [UserID]       NVARCHAR (30)  NULL,
    [Password]     NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Data_Sync] PRIMARY KEY CLUSTERED ([Code] ASC)
);

GO
CREATE TABLE [dbo].[Data_Sync_Detail] (
    [Code]              NVARCHAR (15)   NOT NULL,
    [SyncType]          INT             NULL,
    [RecordType]        INT             NULL,
    [Name]              NVARCHAR (50)   NULL,
    [Description]       NVARCHAR (3000) NULL,
    [Rowindex]          INT             NULL,
    [DefaultSysDocID]   NVARCHAR (15)   NULL,
    [DefaultRegisterID] NVARCHAR (50)   NULL,
    [LastSyncTime]      DATETIME        NULL,
    [SyncInterval]      INT             NULL,
    [Status]            BIT             NULL
);

GO
CREATE TABLE [dbo].[Deduction] (
    [DeductionID]   NVARCHAR (15)  NOT NULL,
    [DeductionName] NVARCHAR (30)  NOT NULL,
    [Note]          NVARCHAR (255) NULL,
    [Inactive]      BIT            NULL,
    [AccountID]     NVARCHAR (64)  NOT NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Deduction] PRIMARY KEY CLUSTERED ([DeductionID] ASC)
);

GO
CREATE TABLE [dbo].[Degree] (
    [DegreeID]    NVARCHAR (15) NOT NULL,
    [DegreeName]  NVARCHAR (64) NOT NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    CONSTRAINT [PK_Employee_Degree] PRIMARY KEY CLUSTERED ([DegreeID] ASC)
);

GO
CREATE TABLE [dbo].[Delivery_Note] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [SalesFlow]          TINYINT         NULL,
    [CLUserID]           NVARCHAR (15)   NULL,
    [IsExport]           BIT             NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Port]               NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Delivery_Note_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [PODate]             DATETIME        NULL,
    [IsInvoiced]         BIT             NULL,
    [IsShipped]          BIT             NULL,
    [ContainerNumber]    NVARCHAR (30)   NULL,
    [ContainerSizeID]    NVARCHAR (20)   NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [DriverID]           NVARCHAR (15)   NULL,
    [VehicleID]          NVARCHAR (30)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Delivery_Note] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Delivery_Note_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [RowIndex]         INT             NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [RowSource]        TINYINT         NULL,
    [ListVoucherID]    NVARCHAR (15)   NULL,
    [ListRowIndex]     INT             NULL,
    [ListSysDocID]     NVARCHAR (7)    NULL,
    [ITRowID]          INT             NULL,
    [RefSlNo]          INT             NULL,
    [RefText1]         NVARCHAR (50)   NULL,
    [RefText2]         NVARCHAR (50)   NULL,
    [RefNum1]          DECIMAL (18, 5) NULL,
    [RefNum2]          DECIMAL (18, 5) NULL,
    [RefDate1]         DATETIME        NULL,
    [RefDate2]         DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Delivery_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [SalesFlow]         TINYINT         NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Delivery_Return_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [ReasonID]          NVARCHAR (15)   NULL,
    [Discount]          MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsInvoiced]        BIT             NULL,
    [DNoteSysDocID]     NVARCHAR (7)    NULL,
    [DNoteVoucherID]    NVARCHAR (15)   NULL,
    [DriverID]          NVARCHAR (15)   NULL,
    [VehicleID]         NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [IsCompleteReturn]  BIT             NULL,
    [IsExport]          BIT             NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Delivery_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Delivery_Return_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        TINYINT         NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL,
    [DNRowIndex]      INT             NULL,
    [ITRowID]         INT             NULL
);

GO
CREATE TABLE [dbo].[Department] (
    [DepartmentID]   NVARCHAR (15)  NOT NULL,
    [DepartmentName] NVARCHAR (64)  NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            CONSTRAINT [DF_Department_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentID] ASC)
);

GO
CREATE TABLE [dbo].[Destination] (
    [DestinationID]     NVARCHAR (15)  NOT NULL,
    [DestinationName]   NVARCHAR (30)  NULL,
    [TicketFixedAmount] MONEY          NULL,
    [AccountID]         NVARCHAR (15)  NULL,
    [Note]              NVARCHAR (255) NULL,
    [Inactive]          BIT            CONSTRAINT [DF_Destination_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Destination] PRIMARY KEY CLUSTERED ([DestinationID] ASC)
);

GO
CREATE TABLE [dbo].[Dimension] (
    [DimensionID]   NVARCHAR (15) NOT NULL,
    [DimensionName] NVARCHAR (32) NULL,
    [IsInactive]    BIT           NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (15) NULL,
    [UpdatedBy]     NVARCHAR (15) NULL,
    CONSTRAINT [PK_Dimention] PRIMARY KEY CLUSTERED ([DimensionID] ASC)
);

GO
CREATE TABLE [dbo].[Dimension_Attribute] (
    [AttributeID]   NVARCHAR (15) NOT NULL,
    [DimensionID]   NVARCHAR (15) NOT NULL,
    [AttributeName] NVARCHAR (32) NULL,
    [Code]          NVARCHAR (15) NULL,
    [IsInactive]    BIT           NULL,
    [RowIndex]      INT           NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (15) NULL,
    [UpdatedBy]     NVARCHAR (15) NULL,
    CONSTRAINT [PK_Dimention_Attribute] PRIMARY KEY CLUSTERED ([AttributeID] ASC, [DimensionID] ASC)
);

GO
CREATE TABLE [dbo].[Discipline_Action_Type] (
    [ActionTypeID]   NVARCHAR (15)  NOT NULL,
    [ActionTypeName] NVARCHAR (30)  NOT NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            CONSTRAINT [DF_Discipline_Action_Type_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Discipline_Action_Type] PRIMARY KEY CLUSTERED ([ActionTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Division] (
    [DivisionID]   NVARCHAR (15)  NOT NULL,
    [DivisionName] NVARCHAR (64)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            CONSTRAINT [DF_Division_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Division] PRIMARY KEY CLUSTERED ([DivisionID] ASC)
);

GO
CREATE TABLE [dbo].[Doc_Version] (
    [VersionID]  INT           IDENTITY (1, 1) NOT NULL,
    [ScreenType] TINYINT       NULL,
    [ScreenID]   INT           NULL,
    [SysDocID]   NVARCHAR (15) NULL,
    [DocNumber]  NVARCHAR (64) NULL,
    [DocData]    IMAGE         NULL,
    [MachineID]  NVARCHAR (64) NULL,
    [UserID]     NVARCHAR (15) NULL,
    [LogDate]    DATETIME      NULL,
    CONSTRAINT [PK_Doc_Version] PRIMARY KEY CLUSTERED ([VersionID] ASC)
);

GO
CREATE TABLE [dbo].[Draft_Document] (
    [DocumentID] INT NULL
);

GO
CREATE TABLE [dbo].[Driver] (
    [DriverID]    NVARCHAR (15)  NOT NULL,
    [DriverName]  NVARCHAR (64)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            CONSTRAINT [DF_Driver_Inactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[EA_Equipment] (
    [EquipmentID]         NVARCHAR (15) NOT NULL,
    [Description]         NVARCHAR (30) NULL,
    [RegistrationNumber]  NVARCHAR (15) NULL,
    [JobID]               NVARCHAR (50) NULL,
    [LocationID]          NVARCHAR (15) NULL,
    [EquipmentCategoryID] NVARCHAR (15) NULL,
    [EquipmentTypeID]     NVARCHAR (15) NULL,
    [ExpiryDate]          DATETIME      NULL,
    [OwnerShip]           TINYINT       NULL,
    [VendorID]            NVARCHAR (64) NULL,
    [ParentEquipmentID]   NVARCHAR (15) NULL,
    [Model]               NVARCHAR (15) NULL,
    [Color]               NVARCHAR (15) NULL,
    [Year]                SMALLINT      NULL,
    [Capacity]            NVARCHAR (15) NULL,
    [CapacityType]        TINYINT       NULL,
    [Power]               NVARCHAR (15) NULL,
    [Fuel]                NVARCHAR (15) NULL,
    [SerialNo]            NVARCHAR (15) NULL,
    [PlateNo]             NVARCHAR (15) NULL,
    [TrackingID]          NVARCHAR (30) NULL,
    [EngineNumber]        NVARCHAR (30) NULL,
    [IsMeter]             BIT           NULL,
    [IsMileage]           BIT           NULL,
    [IsHours]             BIT           NULL,
    [OwnedBy]             NVARCHAR (30) NULL,
    [MaintenanceInCharge] NVARCHAR (30) NULL,
    [NotificationEmail]   NVARCHAR (30) NULL,
    [FixedAssetGroupID]   NVARCHAR (15) NULL,
    [FixedAssetID]        NVARCHAR (15) NULL,
    [IsInactive]          BIT           NULL,
    [DateCreated]         DATETIME      NULL,
    [DateUpdated]         DATETIME      NULL,
    [CreatedBy]           NVARCHAR (15) NULL,
    [UpdatedBy]           NVARCHAR (15) NULL,
    CONSTRAINT [PK_EA_Equipment] PRIMARY KEY CLUSTERED ([EquipmentID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Equipment_Category] (
    [EquipmentCategoryID]   NVARCHAR (15)  NOT NULL,
    [EquipmentCategoryName] NVARCHAR (64)  NOT NULL,
    [IsInactive]            BIT            NULL,
    [Note]                  NVARCHAR (255) NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Equipment_Category] PRIMARY KEY CLUSTERED ([EquipmentCategoryID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Equipment_Transfer] (
    [SysDocID]            NVARCHAR (7)   NOT NULL,
    [VoucherID]           NVARCHAR (15)  NOT NULL,
    [EquipmentID]         NVARCHAR (15)  NOT NULL,
    [CurrentMeterReading] NVARCHAR (15)  NULL,
    [JobFromID]           NVARCHAR (15)  NULL,
    [JobToID]             NVARCHAR (15)  NULL,
    [LocationFromID]      NVARCHAR (15)  NULL,
    [LocationToID]        NVARCHAR (15)  NULL,
    [EmployeeFromID]      NVARCHAR (15)  NULL,
    [EmployeeToID]        NVARCHAR (15)  NULL,
    [SourceVoucherID]     NVARCHAR (15)  NULL,
    [SourceSysDocID]      NVARCHAR (7)   NULL,
    [SourceRowIndex]      INT            NULL,
    [ReqVoucherID]        NVARCHAR (15)  NULL,
    [ReqSysDocID]         NVARCHAR (7)   NULL,
    [TransactionDate]     DATETIME       NULL,
    [Reference]           NVARCHAR (15)  NULL,
    [Note]                NVARCHAR (255) NULL,
    [IsVoid]              BIT            NULL,
    [ApprovalStatus]      TINYINT        NULL,
    [VerificationStatus]  TINYINT        NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_Equipment_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Equipment_Type] (
    [EquipmentTypeID]   NVARCHAR (15)  NOT NULL,
    [EquipmentTypeName] NVARCHAR (64)  NOT NULL,
    [IsInactive]        BIT            NULL,
    [Note]              NVARCHAR (255) NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Equipment_Type] PRIMARY KEY CLUSTERED ([EquipmentTypeID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Mobilization] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [RequisitionNumber] NVARCHAR (2000) NULL,
    [PlannedDateFrom]   DATETIME        NULL,
    [PlannedDateTo]     DATETIME        NULL,
    [Status]            TINYINT         NULL,
    [IsVoid]            BIT             NULL,
    [Discount]          MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [Total]             MONEY           NULL,
    [DiscountFC]        MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [TotalFC]           MONEY           NULL,
    [Note]              NVARCHAR (2000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_EA_Mobilization] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Mobilization_Equipment__Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [RequisitionNumber] NVARCHAR (64)   NULL,
    [EquipmentID]       NVARCHAR (15)   NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [Status]            TINYINT         NULL,
    [Remarks]           NVARCHAR (2000) NULL,
    [RowIndex]          SMALLINT        NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceRowIndex]    INT             NULL,
    [IsSourcedRow]      BIT             NULL,
    [SourceDocType]     TINYINT         NULL
);

GO
CREATE TABLE [dbo].[EA_Mobilization_Manpower__Detail] (
    [SysDocID]     NVARCHAR (7)   NOT NULL,
    [VoucherID]    NVARCHAR (15)  NOT NULL,
    [PositionID]   NVARCHAR (15)  NULL,
    [RowIndex]     SMALLINT       NULL,
    [EmployeeID]   NVARCHAR (64)  NOT NULL,
    [EmployeeName] NVARCHAR (64)  NULL,
    [NoOfMembers]  INT            NULL,
    [Remarks]      NVARCHAR (100) NULL
);

GO
CREATE TABLE [dbo].[EA_Mobilization_Resources__Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [PORVoucherID]     NVARCHAR (15)   NULL,
    [PORSysDocID]      NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsPORRow]         BIT             NULL,
    [LotNumber]        INT             NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL
);

GO
CREATE TABLE [dbo].[EA_Requisition] (
    [SysDocID]            NVARCHAR (7)   NOT NULL,
    [VoucherID]           NVARCHAR (15)  NOT NULL,
    [TransactionDate]     DATETIME       NULL,
    [LocationID]          NVARCHAR (15)  NULL,
    [JobID]               NVARCHAR (50)  NULL,
    [EquipmentCategoryID] NVARCHAR (15)  NULL,
    [EquipmentID]         NVARCHAR (15)  NULL,
    [RequisitionTypeID]   NVARCHAR (15)  NULL,
    [RequiredOn]          DATETIME       NULL,
    [RequiredTill]        DATETIME       NULL,
    [Status]              TINYINT        NULL,
    [Remarks]             NVARCHAR (255) NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_Requsition] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Requisition_Type] (
    [RequisitionTypeID]   NVARCHAR (15)  NOT NULL,
    [RequisitionTypeName] NVARCHAR (64)  NOT NULL,
    [IsInactive]          BIT            NULL,
    [Note]                NVARCHAR (255) NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_Requisition] PRIMARY KEY CLUSTERED ([RequisitionTypeID] ASC)
);

GO
CREATE TABLE [dbo].[EA_Work_Order] (
    [SysDocID]            NVARCHAR (7)   NOT NULL,
    [VoucherID]           NVARCHAR (15)  NOT NULL,
    [EquipmentID]         NVARCHAR (15)  NOT NULL,
    [CurrentMeterReading] NVARCHAR (15)  NULL,
    [WorkOrderTypeID]     INT            NULL,
    [TransactionDate]     DATETIME       NULL,
    [Status]              TINYINT        NULL,
    [IsVoid]              BIT            NULL,
    [Reference]           NVARCHAR (15)  NULL,
    [Note]                NVARCHAR (255) NULL,
    [ApprovalStatus]      TINYINT        NULL,
    [VerificationStatus]  TINYINT        NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_Work_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Expense_Detail] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [PCSysDocID]   NVARCHAR (7)    NULL,
    [PCVoucherID]  NVARCHAR (15)   NULL,
    [PCRowIndex]   SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Inventory_Issue] (
    [SysDocID]         NVARCHAR (7)   NOT NULL,
    [VoucherID]        NVARCHAR (15)  NOT NULL,
    [TransactionDate]  DATETIME       NULL,
    [Reference]        NVARCHAR (20)  NULL,
    [Reference2]       NVARCHAR (20)  NULL,
    [RequestedBy]      NVARCHAR (30)  NULL,
    [RequireUpdate]    BIT            NULL,
    [SourceSysDocType] TINYINT        NULL,
    [SourceSysDocID]   NVARCHAR (7)   NULL,
    [SourceVoucherID]  NVARCHAR (15)  NULL,
    [Description]      NVARCHAR (255) NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_WorkOrder_Inventory_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Inventory_Issue_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [Factor]          DECIMAL (18, 5) NULL,
    [FactorType]      CHAR (1)        NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [RowIndex]        INT             NULL,
    [IsBillable]      BIT             NULL,
    [IsBilled]        BIT             NULL,
    [BilledAmount]    MONEY           NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (6)    NULL,
    [SourceRowIndex]  INT             NULL
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Inventory_Return] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Reference]       NVARCHAR (20)  NULL,
    [RequestedBy]     NVARCHAR (30)  NULL,
    [RequireUpdate]   BIT            NULL,
    [Description]     NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_EA_WorkOrder_Inventory_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Inventory_Return_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [Factor]          DECIMAL (18, 5) NULL,
    [FactorType]      CHAR (1)        NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [RowIndex]        INT             NULL,
    [BillType]        TINYINT         NULL,
    [IsBilled]        BIT             NULL,
    [BilledAmount]    MONEY           NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (6)    NULL,
    [SourceRowIndex]  INT             NULL
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_ManPower_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [PositionID]   NVARCHAR (15)   NULL,
    [RowIndex]     SMALLINT        NULL,
    [EmployeeID]   NVARCHAR (64)   NOT NULL,
    [EmployeeName] NVARCHAR (64)   NULL,
    [Hrs]          DECIMAL (18, 2) NULL,
    [Remarks]      NVARCHAR (100)  NULL
);

GO
CREATE TABLE [dbo].[EA_WorkOrder_Resources_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [Issued]           DECIMAL (18, 5) NULL,
    [ItemType]         TINYINT         NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [PORVoucherID]     NVARCHAR (15)   NULL,
    [PORSysDocID]      NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsPORRow]         BIT             NULL,
    [LotNumber]        INT             NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL
);

GO
CREATE TABLE [dbo].[Email_Config] (
    [CompanyID]       TINYINT        NOT NULL,
    [EmailID]         TINYINT        NOT NULL,
    [EmailAddress]    NVARCHAR (64)  NULL,
    [OutgoingServer]  NVARCHAR (64)  NULL,
    [IncommingServer] NVARCHAR (64)  NULL,
    [UserName]        NVARCHAR (64)  NULL,
    [EmailPass]       NVARCHAR (64)  NULL,
    [SenderName]      NVARCHAR (64)  NULL,
    [EmailSMTPPort]   INT            NULL,
    [EmailUseSSL]     BIT            NULL,
    [CCSalesperson]   BIT            NULL,
    [Body1]           NTEXT          NULL,
    [Body2]           NTEXT          NULL,
    [Body3]           NTEXT          NULL,
    [Body4]           NTEXT          NULL,
    [CC1]             NVARCHAR (255) NULL,
    [CC2]             NVARCHAR (255) NULL,
    [CC3]             NVARCHAR (255) NULL,
    [CC4]             NVARCHAR (255) NULL,
    [Subject1]        NVARCHAR (255) NULL,
    [Subject2]        NVARCHAR (255) NULL,
    [Subject3]        NVARCHAR (255) NULL,
    [Subject4]        NVARCHAR (255) NULL,
    [IsInactive]      BIT            NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateCreated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    CONSTRAINT [PK_Email_Config] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [EmailID] ASC)
);

GO
CREATE TABLE [dbo].[Email_Message] (
    [MessageID]        INT             IDENTITY (1, 1) NOT NULL,
    [Subject]          NVARCHAR (255)  NULL,
    [PeriodFrom]       DATETIME        NULL,
    [PeriodTo]         DATETIME        NULL,
    [ConfigType]       TINYINT         NULL,
    [MessageType]      TINYINT         NULL,
    [UserID]           NVARCHAR (15)   NULL,
    [SenderAddress]    NVARCHAR (100)  NULL,
    [SenderName]       NVARCHAR (100)  NULL,
    [RecipientAddress] NVARCHAR (500)  NULL,
    [EmailBody]        XML             NULL,
    [CCAddress]        NVARCHAR (500)  NULL,
    [BCCAddress]       NVARCHAR (500)  NULL,
    [Attachment]       VARBINARY (MAX) NULL,
    [AttachmentName]   NVARCHAR (100)  NULL,
    [PartyType]        CHAR (1)        NULL,
    [PartyID]          NVARCHAR (64)   NULL,
    [Amount]           DECIMAL (18, 5) NULL,
    [Status]           TINYINT         NULL,
    [StatusMessage]    NVARCHAR (255)  NULL,
    [EmailDate]        DATETIME        NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Email_Message] PRIMARY KEY CLUSTERED ([MessageID] ASC)
);

GO
CREATE TABLE [dbo].[Employee] (
    [EmployeeID]                 NVARCHAR (64)   NOT NULL,
    [FirstName]                  NVARCHAR (150)  NULL,
    [MiddleName]                 NVARCHAR (150)  NULL,
    [LastName]                   NVARCHAR (150)  NULL,
    [NickName]                   NVARCHAR (30)   NULL,
    [BirthDate]                  SMALLDATETIME   NULL,
    [JoiningDate]                SMALLDATETIME   NULL,
    [Photo]                      IMAGE           NULL,
    [IsTerminated]               BIT             NULL,
    [TerminationDate]            DATETIME        NULL,
    [TerminationID]              INT             NULL,
    [IsCancelled]                BIT             NULL,
    [CancellationDate]           DATETIME        NULL,
    [RehireDate]                 DATETIME        NULL,
    [IsEOSSettled]               BIT             NULL,
    [GradeID]                    NVARCHAR (15)   NULL,
    [DayOff]                     TINYINT         NULL,
    [OnVacation]                 BIT             NULL,
    [EOSRuleID]                  NVARCHAR (15)   NULL,
    [OvertimeID]                 NVARCHAR (15)   NULL,
    [BirthPlace]                 NVARCHAR (30)   NULL,
    [SponsorID]                  NVARCHAR (15)   NULL,
    [NationalityID]              NVARCHAR (15)   NULL,
    [UID]                        NVARCHAR (50)   NULL,
    [VisaNumber]                 NVARCHAR (50)   NULL,
    [Probation]                  NUMERIC (5)     NULL,
    [ConfirmationDate]           SMALLDATETIME   NULL,
    [ReligionID]                 NVARCHAR (15)   NULL,
    [BloodGroup]                 NVARCHAR (5)    NULL,
    [Qualification]              NVARCHAR (30)   NULL,
    [RecruitmentChannelID]       NVARCHAR (100)  NULL,
    [VisaDesignationID]          NVARCHAR (30)   NULL,
    [ContractType]               NVARCHAR (15)   NULL,
    [PaymentMethodID]            TINYINT         NULL,
    [BankID]                     NVARCHAR (15)   NULL,
    [AccountNumber]              NVARCHAR (30)   NULL,
    [AnnualLeaveDate]            SMALLDATETIME   NULL,
    [ResumedDate]                SMALLDATETIME   NULL,
    [Notes]                      NTEXT           NULL,
    [LocationID]                 NVARCHAR (15)   NULL,
    [DivisionID]                 NVARCHAR (15)   NULL,
    [DepartmentID]               NVARCHAR (15)   NULL,
    [PositionID]                 NVARCHAR (15)   NULL,
    [GroupID]                    NVARCHAR (15)   NULL,
    [ReportToID]                 NVARCHAR (15)   NULL,
    [PayPeriod]                  TINYINT         NULL,
    [Gender]                     CHAR (1)        NULL,
    [LastPayDate]                DATETIME        NULL,
    [MaritalStatus]              TINYINT         CONSTRAINT [DF_Employees_Married] DEFAULT ((1)) NULL,
    [SpouseName]                 NVARCHAR (64)   NULL,
    [MedicalInsuranceProviderID] NVARCHAR (64)   NULL,
    [MedicalInsuranceCategoryID] NVARCHAR (64)   NULL,
    [MedicalInsuranceNumber]     NVARCHAR (64)   NULL,
    [MedicalInsuranceAmount]     MONEY           NULL,
    [MedicalInsValidFrom]        DATETIME        NULL,
    [MedicalInsValidTo]          DATETIME        NULL,
    [NumberOfDependants]         INT             NULL,
    [NationalID]                 NVARCHAR (30)   NULL,
    [Status]                     TINYINT         NULL,
    [PrimaryAddressID]           NVARCHAR (15)   NULL,
    [DestinationID]              NVARCHAR (15)   NULL,
    [NumberOfTickets]            TINYINT         NULL,
    [TicketAmount]               MONEY           NULL,
    [TicketPeriod]               SMALLINT        NULL,
    [TicketRemarks]              NVARCHAR (255)  NULL,
    [AccountID]                  NVARCHAR (15)   NULL,
    [EmpAnalysisID]              NVARCHAR (15)   NULL,
    [LabourID]                   NVARCHAR (20)   NULL,
    [IBAN]                       NVARCHAR (50)   NULL,
    [CurrencyID]                 NVARCHAR (15)   NULL,
    [SalaryRemarks]              NVARCHAR (255)  NULL,
    [BasicSalary]                MONEY           NULL,
    [Balance]                    MONEY           NULL,
    [LastRevisedSalaryDate]      SMALLDATETIME   NULL,
    [PDCAmount]                  MONEY           NULL,
    [AppriasalPoints]            DECIMAL (15, 2) NULL,
    [CalendarID]                 NVARCHAR (50)   NULL,
    [UserDefined1]               NVARCHAR (64)   NULL,
    [UserDefined2]               NVARCHAR (64)   NULL,
    [UserDefined3]               NVARCHAR (64)   NULL,
    [UserDefined4]               NVARCHAR (64)   NULL,
    [ApprovalStatus]             TINYINT         NULL,
    [VerificationStatus]         TINYINT         NULL,
    [DateCreated]                DATETIME        NULL,
    [DateUpdated]                DATETIME        NULL,
    [CreatedBy]                  NVARCHAR (15)   NULL,
    [UpdatedBy]                  NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Absconding] (
    [ActivityID]            INT           NOT NULL,
    [AdviceReceivedOn]      DATETIME      NULL,
    [RealAbscondingDate]    DATETIME      NULL,
    [AbscondingRegDateMOL]  DATETIME      NULL,
    [MBReferenceNo]         NVARCHAR (30) NULL,
    [AbscondingRegDateIMG]  DATETIME      NULL,
    [AbscondingReferenceNo] NVARCHAR (30) NULL,
    [PassportHeldInIMG]     NVARCHAR (3)  NULL,
    [TicketAmountPaid]      NVARCHAR (3)  NULL,
    [LastWorkingDate]       DATETIME      NULL,
    [MOLCancellationDate]   DATETIME      NULL,
    [IMGCancellationDate]   DATETIME      NULL,
    CONSTRAINT [PK_Employee_Absconding] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Activity] (
    [ActivityID]         INT             IDENTITY (1, 1) NOT NULL,
    [EmployeeID]         NVARCHAR (64)   NULL,
    [Subject]            NVARCHAR (30)   NULL,
    [TransactionDate]    DATETIME        NULL,
    [ActivityType]       TINYINT         NULL,
    [Reason]             NVARCHAR (255)  NULL,
    [Reference]          NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (1000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_Activity] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Activity_Type] (
    [ActivityTypeID]   NVARCHAR (15)  NOT NULL,
    [ActivityTypeName] NVARCHAR (30)  NOT NULL,
    [ActivityNature]   INT            NULL,
    [Note]             NVARCHAR (255) NULL,
    [Inactive]         BIT            CONSTRAINT [DF_Employee_Activity_Type_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Activity_Type] PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [EmployeeID]         NVARCHAR (64)  NOT NULL,
    [Address1]           NVARCHAR (500) NULL,
    [Address2]           NVARCHAR (500) NULL,
    [Address3]           NVARCHAR (500) NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (30)  NULL,
    [Comment]            NVARCHAR (255) NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [EmployeeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Appraisal] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [EmployeeID]         NVARCHAR (15)  NULL,
    [PositionID]         NVARCHAR (15)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [DateUpdated]        DATETIME       NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Appraisal] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Appraisal_Detail] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [KPIParameter] NTEXT           NULL,
    [Scale]        NTEXT           NULL,
    [Target]       DECIMAL (15, 2) NULL,
    [Weightage]    DECIMAL (15, 2) NULL,
    [Points]       DECIMAL (15, 2) NULL,
    [Actual]       DECIMAL (15, 2) NULL,
    [ACH]          DECIMAL (15, 2) NULL,
    [Rating]       DECIMAL (15, 2) NULL,
    [Remarks]      NVARCHAR (500)  NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Employee_Benefit_Detail] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [BenefitID]   NVARCHAR (15)  NOT NULL,
    [StartDate]   SMALLDATETIME  NULL,
    [EndDate]     SMALLDATETIME  NULL,
    [Amount]      MONEY          NULL,
    [LastAmount]  MONEY          NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL,
    CONSTRAINT [PK_Employee_Benefit_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [BenefitID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Benefit_History] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [BenefitID]   NVARCHAR (15)  NOT NULL,
    [StartDate]   SMALLDATETIME  NULL,
    [EndDate]     SMALLDATETIME  NULL,
    [Amount]      MONEY          NULL,
    [LastAmount]  MONEY          NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL
);

GO
CREATE TABLE [dbo].[Employee_Cancellation] (
    [ActivityID]           INT           NOT NULL,
    [CancellationType]     TINYINT       NULL,
    [VCAppReceivedDate]    DATETIME      NULL,
    [VCAppTypedDate]       DATETIME      NULL,
    [VCAppSubmittedDate]   DATETIME      NULL,
    [MBReferenceNoCancel]  NVARCHAR (30) NULL,
    [RPCancelDateIMG]      DATETIME      NULL,
    [LastWorkingDate]      DATETIME      NULL,
    [DepartureDate]        DATETIME      NULL,
    [ExitPort]             NVARCHAR (30) NULL,
    [SignedCNDOCRecvdDate] DATETIME      NULL,
    CONSTRAINT [PK_Employee_Cancellation] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Deduction_Detail] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [DeductionID] NVARCHAR (15)  NOT NULL,
    [StartDate]   SMALLDATETIME  NULL,
    [EndDate]     SMALLDATETIME  NULL,
    [Amount]      MONEY          NULL,
    [LastAmount]  MONEY          NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL,
    CONSTRAINT [PK_Employee_Deduction_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DeductionID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Deduction_History] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [DeductionID] NVARCHAR (15)  NOT NULL,
    [StartDate]   SMALLDATETIME  NULL,
    [EndDate]     SMALLDATETIME  NULL,
    [Amount]      MONEY          NULL,
    [LastAmount]  MONEY          NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL
);

GO
CREATE TABLE [dbo].[Employee_Dependent] (
    [EmployeeID]    NVARCHAR (15)  NOT NULL,
    [DependentName] NVARCHAR (64)  NOT NULL,
    [Gender]        CHAR (1)       NULL,
    [BirthDate]     SMALLDATETIME  NULL,
    [Address]       NVARCHAR (255) NULL,
    [NationalID]    NVARCHAR (30)  NULL,
    [Relation]      NVARCHAR (15)  NULL,
    [Comment]       NVARCHAR (255) NULL,
    [Phone]         NVARCHAR (30)  NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Dependent] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DependentName] ASC)
);

GO
CREATE TABLE [dbo].[Employee_DisciplinaryAction] (
    [ActivityID]   INT           NOT NULL,
    [ActionTypeID] NVARCHAR (15) NULL,
    [RequestedBy]  NVARCHAR (30) NULL,
    CONSTRAINT [PK_Employee_DisciplinaryAction] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_DisciplinaryAction_Employee_DisciplinaryAction] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Employee_DisciplinaryAction_Employee_DisciplinaryAction1] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_DisciplinaryAction] ([ActivityID])
);

GO
CREATE TABLE [dbo].[Employee_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Employee_Docs_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Document] (
    [EmployeeID]     NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Docs] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Education] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [School]      NVARCHAR (30)  NULL,
    [Major]       NVARCHAR (30)  NOT NULL,
    [Degree]      NVARCHAR (15)  NULL,
    [GPA]         NUMERIC (3, 2) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Education] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [Major] ASC)
);

GO
CREATE TABLE [dbo].[Employee_EOS] (
    [SysDocID]                 NVARCHAR (7)    NOT NULL,
    [VoucherID]                NVARCHAR (15)   NOT NULL,
    [TransactionDate]          DATETIME        NULL,
    [EmployeeID]               NVARCHAR (64)   NULL,
    [LastWorkingDate]          DATE            NULL,
    [EmployeeBasic]            MONEY           NULL,
    [CalculatedLeaveAmount]    MONEY           NULL,
    [PaidLeaveAmount]          MONEY           NULL,
    [LeaveDescription]         NVARCHAR (255)  NULL,
    [CalculatedGratuityAmount] MONEY           NULL,
    [PaidGratuityAmount]       MONEY           NULL,
    [GratuityDescription]      NVARCHAR (255)  NULL,
    [CalculatedSalaryAmount]   MONEY           NULL,
    [PaidSalaryAmount]         MONEY           NULL,
    [SalaryDescription]        NVARCHAR (255)  NULL,
    [PaidDeductionAmount]      MONEY           NULL,
    [PaidLoanAmount]           MONEY           NULL,
    [PaidTicketAmount]         MONEY           NULL,
    [Note]                     NVARCHAR (4000) NULL,
    [NetTotal]                 MONEY           NULL,
    [IsResigned]               BIT             NULL,
    [DateCreated]              DATETIME        NULL,
    [DateUpdated]              DATETIME        NULL,
    [CreatedBy]                NVARCHAR (15)   NULL,
    [UpdatedBy]                NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_EOS] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_EOS_Deduction_Detail] (
    [SysDocID]    NVARCHAR (7)   NULL,
    [VoucherID]   NVARCHAR (15)  NULL,
    [DeductionID] NVARCHAR (15)  NULL,
    [Description] NVARCHAR (255) NULL,
    [Amount]      MONEY          NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL
);

GO
CREATE TABLE [dbo].[Employee_EOSRule] (
    [EOSRuleID]   NVARCHAR (15)  NOT NULL,
    [EOSRuleName] NVARCHAR (64)  NULL,
    [AccountID]   NVARCHAR (64)  NULL,
    [EOSSystem]   TINYINT        NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Employee_EOSRule] PRIMARY KEY CLUSTERED ([EOSRuleID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_EOSRule_Detail] (
    [EOSRuleID]    NVARCHAR (15) NOT NULL,
    [RowIndex]     INT           NOT NULL,
    [ResignedType] NVARCHAR (30) NOT NULL,
    [YearFrom]     INT           NULL,
    [YearTo]       INT           NULL,
    [EOSDay]       INT           NULL
);

GO
CREATE TABLE [dbo].[Employee_EOSSettlement] (
    [EmployeeID]       NVARCHAR (64) NOT NULL,
    [LastWorkingDate]  DATETIME      NULL,
    [EOSBenefit]       MONEY         NULL,
    [LeaveDue]         INT           NULL,
    [DueAmount]        MONEY         NULL,
    [SalaryDue]        MONEY         NULL,
    [OtherBenefits]    MONEY         NULL,
    [TotalPayable]     MONEY         NULL,
    [Loan]             MONEY         NULL,
    [TicketAmount]     MONEY         NULL,
    [OtherDeductionID] NVARCHAR (30) NULL,
    [DeductionAmount]  MONEY         NULL,
    [NetTotal]         MONEY         NULL,
    [IsVoid]           BIT           NULL,
    [DateCreated]      DATETIME      NULL,
    [DateUpdated]      DATETIME      NULL,
    [CreatedBy]        NVARCHAR (15) NULL,
    [UpdatedBy]        NVARCHAR (15) NULL,
    CONSTRAINT [PK_Employee_EOSSettlement] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_GeneralActivity] (
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [EmployeeID]            NVARCHAR (64)   NULL,
    [Subject]               NVARCHAR (30)   NULL,
    [TransactionDate]       DATETIME        NULL,
    [ActivityType]          TINYINT         NULL,
    [Reason]                NVARCHAR (255)  NULL,
    [RequestedBy]           NVARCHAR (30)   NULL,
    [GeneralActivityTypeID] NVARCHAR (15)   NULL,
    [Rating]                TINYINT         NULL,
    [Reference]             NVARCHAR (15)   NULL,
    [ApprovalStatus]        TINYINT         NULL,
    [VerificationStatus]    TINYINT         NULL,
    [Note]                  NVARCHAR (1000) NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_GeneralActivity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Grade] (
    [GradeID]     NVARCHAR (15)  NOT NULL,
    [GradeName]   NVARCHAR (64)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Emp_Grade] PRIMARY KEY CLUSTERED ([GradeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Group] (
    [GroupID]     NVARCHAR (15)  NOT NULL,
    [GroupName]   NVARCHAR (30)  NOT NULL,
    [Inactive]    BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Journal] (
    [EmpJournalID]      INT             IDENTITY (1, 1) NOT NULL,
    [EmployeeID]        NVARCHAR (64)   NULL,
    [SysDocID]          NVARCHAR (7)    NULL,
    [VoucherID]         NVARCHAR (15)   NULL,
    [JournalID]         INT             NULL,
    [JournalDate]       DATETIME        NULL,
    [Debit]             MONEY           NULL,
    [Credit]            MONEY           NULL,
    [DebitFC]           MONEY           NULL,
    [CreditFC]          MONEY           NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [PaymentMethodType] TINYINT         NULL,
    [ChequeNumber]      NVARCHAR (15)   NULL,
    [ChequeDate]        DATETIME        NULL,
    [BankID]            NVARCHAR (15)   NULL,
    [Description]       NVARCHAR (255)  NULL,
    [AccountID]         NVARCHAR (64)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [Reference]         NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    CONSTRAINT [PK_Employee_Journal] PRIMARY KEY CLUSTERED ([EmpJournalID] ASC),
    CONSTRAINT [FK_Employee_Journal_Employee] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);

GO
CREATE TABLE [dbo].[Employee_Leave_Detail] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [LeaveTypeID] NVARCHAR (50)  NOT NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [Days]        SMALLINT       NULL,
    [RowIndex]    TINYINT        NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL,
    CONSTRAINT [PK_Employee_Leave_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [LeaveTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Leave_Encashment] (
    [ActivityID]    INT           NOT NULL,
    [EncashNo]      NVARCHAR (15) NULL,
    [AsOnDate]      DATETIME      NULL,
    [LeaveEligible] INT           NULL,
    [LeaveEncash]   INT           NULL,
    [AmountEncash]  MONEY         NULL,
    [IsApproved]    BIT           NULL,
    [ApprovedBy]    NVARCHAR (15) NULL,
    [ApproveDate]   DATETIME      NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (64) NULL,
    [UpdatedBy]     NVARCHAR (64) NULL,
    CONSTRAINT [PK_Employee_Leave_Encashment] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Leave_Payment] (
    [ActivityID]         INT           NOT NULL,
    [SysDocID]           NVARCHAR (7)  NULL,
    [VoucherID]          NVARCHAR (15) NULL,
    [LeavePaymentID]     NVARCHAR (15) NULL,
    [LeaveApplicationNo] NVARCHAR (15) NULL,
    [StartDate]          DATETIME      NULL,
    [EndDate]            DATETIME      NULL,
    [EligibleDays]       INT           NULL,
    [Amount]             MONEY         NULL,
    [TicketAmount]       MONEY         NULL,
    [SalaryAmount]       MONEY         NULL,
    [DeductionID]        NVARCHAR (30) NULL,
    [DeductionAmount]    MONEY         NULL,
    [BasedOnLeaveTaken]  BIT           NULL,
    [Total]              MONEY         NULL,
    [IsApproved]         BIT           NULL,
    [ApprovedBy]         NVARCHAR (15) NULL,
    [ApproveDate]        DATETIME      NULL,
    [DateCreated]        DATETIME      NULL,
    [DateUpdated]        DATETIME      NULL,
    [CreatedBy]          NVARCHAR (64) NULL,
    [UpdatedBy]          NVARCHAR (64) NULL,
    CONSTRAINT [PK_Employee_Leave_Payment] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Leave_Process] (
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Note]            NVARCHAR (100) NULL,
    [EmployeeID]      NVARCHAR (15)  NULL,
    [EmployeeName]    NVARCHAR (64)  NULL,
    [Days]            INT            NULL,
    [FromDate]        DATETIME       NULL,
    [ToDate]          DATETIME       NULL,
    [RowIndex]        INT            NULL,
    [DateCreated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Employee_Leave_Request] (
    [ActivityID]      INT            NOT NULL,
    [DocNumber]       NVARCHAR (15)  NULL,
    [LeaveTypeID]     NVARCHAR (15)  NULL,
    [StartDate]       DATETIME       NULL,
    [EndDate]         DATETIME       NULL,
    [TravellingDate]  DATETIME       NULL,
    [ActualLeaveDays] INT            NULL,
    [LastWorkingDate] DATETIME       NULL,
    [TicketEntitle]   BIT            NULL,
    [ReplacementID]   NVARCHAR (64)  NULL,
    [IsApproved]      BIT            NULL,
    [IsClosed]        BIT            NULL,
    [IsVoid]          BIT            NULL,
    [IsPaid]          BIT            NULL,
    [ApprovedBy]      NVARCHAR (15)  NULL,
    [ApproveDate]     DATETIME       NULL,
    [ResumptionDate]  DATETIME       NULL,
    [ApprovalRemarks] NVARCHAR (500) NULL,
    CONSTRAINT [PK_Employee_Leave_Request] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Leave_Request_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[Employee_Loan] (
    [SysDocID]           NVARCHAR (15)   NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [EmployeeID]         NVARCHAR (64)   NULL,
    [LoanAccountID]      NVARCHAR (64)   NULL,
    [EmployeeAccountID]  NVARCHAR (64)   NULL,
    [TransactionDate]    DATETIME        NULL,
    [LoanType]           NVARCHAR (15)   NULL,
    [Amount]             MONEY           NULL,
    [InstallmentAmount]  MONEY           NULL,
    [DedStartDate]       DATETIME        NULL,
    [PaidAmount]         MONEY           NULL,
    [DiscountAmount]     MONEY           NULL,
    [DiscountDate]       DATETIME        NULL,
    [Reason]             NVARCHAR (255)  NULL,
    [Reference]          NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (1000) NULL,
    [IsVoid]             BIT             NULL,
    [IsClosed]           BIT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_Loan_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Loan_Detail] (
    [LoanSysDocID]     NVARCHAR (7)   NULL,
    [LoanVoucherID]    NVARCHAR (15)  NULL,
    [PaymentSysDocID]  NVARCHAR (7)   NULL,
    [PaymentVoucherID] NVARCHAR (15)  NULL,
    [Description]      NVARCHAR (255) NULL,
    [Debit]            MONEY          NULL,
    [Credit]           MONEY          NULL,
    [EmployeeID]       NVARCHAR (64)  NULL,
    [TransactionDate]  DATETIME       NULL,
    [Reference]        NVARCHAR (20)  NULL
);

GO
CREATE TABLE [dbo].[Employee_Loan_Payment] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [LoanSysDocID]    NVARCHAR (7)    NOT NULL,
    [LoanVoucherID]   NVARCHAR (15)   NOT NULL,
    [DivisionID]      NVARCHAR (15)   NULL,
    [CompanyID]       TINYINT         NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Amount]          MONEY           NOT NULL,
    [EmployeeID]      NVARCHAR (64)   NOT NULL,
    [TransactionDate] DATETIME        NOT NULL,
    [Reference]       NVARCHAR (20)   NULL,
    [Note]            NVARCHAR (1000) NULL,
    [IsVoid]          BIT             NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_Loan_Payment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Loan_Settlement] (
    [SysDocID]          NVARCHAR (15)   NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [LoanSysDocID]      NVARCHAR (7)    NULL,
    [LoanVoucherID]     NVARCHAR (15)   NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [EmployeeID]        NVARCHAR (64)   NULL,
    [LoanAccountID]     NVARCHAR (64)   NULL,
    [EmployeeAccountID] NVARCHAR (64)   NULL,
    [TransactionDate]   DATETIME        NULL,
    [LoanType]          NVARCHAR (15)   NULL,
    [Amount]            MONEY           NULL,
    [SettlementAmount]  MONEY           NULL,
    [Note]              NVARCHAR (1000) NULL,
    [IsVoid]            BIT             NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Employee_Loan_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Loan_Type] (
    [LoanTypeID]   NVARCHAR (15)  NOT NULL,
    [LoanTypeName] NVARCHAR (64)  NULL,
    [AccountID]    NVARCHAR (64)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            CONSTRAINT [DF_Employee_Loan_Type_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Employee_OverTime] (
    [OverTimeID]   NVARCHAR (15)   NOT NULL,
    [OverTimeName] NVARCHAR (64)   NULL,
    [IsFixed]      BIT             NULL,
    [FixedAmount]  MONEY           NULL,
    [FactorType]   CHAR (1)        NULL,
    [Factor]       DECIMAL (18, 5) NULL,
    [AccountID]    NVARCHAR (64)   NULL,
    [Note]         NVARCHAR (255)  NULL,
    [Inactive]     BIT             NULL,
    [CreatedBy]    NVARCHAR (15)   NULL,
    [UpdatedBy]    NVARCHAR (15)   NULL,
    [DateCreated]  DATETIME        NULL,
    [DateUpdated]  DATETIME        NULL,
    CONSTRAINT [PK_OverTime] PRIMARY KEY CLUSTERED ([OverTimeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Passport_Control] (
    [ActivityID]      INT            NOT NULL,
    [DocNumber]       NVARCHAR (15)  NULL,
    [ReasonID]        NVARCHAR (15)  NULL,
    [TransactionDate] DATETIME       NULL,
    [PPReleaseDate]   DATETIME       NULL,
    [PPReturnDate]    DATETIME       NULL,
    [IsVoid]          BIT            NULL,
    [ApprovedBy]      NVARCHAR (50)  NULL,
    [IssuedBy]        NVARCHAR (50)  NULL,
    [AcceptedBy]      NVARCHAR (50)  NULL,
    [Note]            NVARCHAR (250) NULL,
    [ReturnNote]      NVARCHAR (250) NULL,
    CONSTRAINT [PK_Employee_Passport_Control] PRIMARY KEY CLUSTERED ([ActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_PayrollItem_Detail] (
    [EmployeeID]    NVARCHAR (15) NOT NULL,
    [PayrollItemID] NVARCHAR (15) NOT NULL,
    [PayType]       TINYINT       NULL,
    [StartDate]     SMALLDATETIME NULL,
    [EndDate]       SMALLDATETIME NULL,
    [Amount]        MONEY         NULL,
    [LastAmount]    MONEY         NULL,
    [RowIndex]      TINYINT       NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (64) NULL,
    [UpdatedBy]     NVARCHAR (64) NULL,
    CONSTRAINT [PK_Employee_Allowance_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [PayrollItemID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_PayrollItem_History] (
    [EmployeeID]            NVARCHAR (15) NOT NULL,
    [PayrollItemID]         NVARCHAR (15) NOT NULL,
    [PayType]               TINYINT       NULL,
    [StartDate]             SMALLDATETIME NULL,
    [EndDate]               SMALLDATETIME NULL,
    [LastRevisedSalaryDate] DATETIME      NULL,
    [Amount]                MONEY         NULL,
    [LastAmount]            MONEY         NULL,
    [RowIndex]              TINYINT       NULL,
    [DateCreated]           DATETIME      NULL,
    [DateUpdated]           DATETIME      NULL,
    [CreatedBy]             NVARCHAR (64) NULL,
    [UpdatedBy]             NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[Employee_Performance] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [FromMonth]          DATETIME       NULL,
    [ToMonth]            DATETIME       NULL,
    [EmployeeID]         NVARCHAR (15)  NULL,
    [PositionID]         NVARCHAR (15)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [DateUpdated]        DATETIME       NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Performance] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Performance_Detail] (
    [SysDocID]             NVARCHAR (7)    NOT NULL,
    [VoucherID]            NVARCHAR (15)   NOT NULL,
    [PerformanceParameter] NTEXT           NULL,
    [Score]                DECIMAL (15, 2) NULL,
    [MinusScore]           DECIMAL (15, 2) NULL,
    [PlusScore]            DECIMAL (15, 2) NULL,
    [Remarks]              NVARCHAR (500)  NULL,
    [RowIndex]             INT             NULL
);

GO
CREATE TABLE [dbo].[Employee_Promotion] (
    [ActivityID]   INT           NOT NULL,
    [RequestedBy]  NVARCHAR (30) NULL,
    [FromGrade]    NVARCHAR (15) NULL,
    [ToGrade]      NVARCHAR (15) NULL,
    [FromPosition] NVARCHAR (15) NULL,
    [ToPosition]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Employee_Promotion] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Promotion_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[Employee_Provision] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [ProvisionTypeID] NVARCHAR (15)  NULL,
    [Reference]       NVARCHAR (64)  NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Employee_Provision] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Provision_Detail] (
    [SysDocID]      NVARCHAR (7)    NULL,
    [VoucherID]     NVARCHAR (15)   NULL,
    [EmployeeID]    NVARCHAR (64)   NULL,
    [ServicePeriod] DECIMAL (10, 4) NULL,
    [DueAmount]     MONEY           NULL,
    [Posted]        MONEY           NULL,
    [CurrentAmount] MONEY           NULL,
    [RowIndex]      INT             NULL
);

GO
CREATE TABLE [dbo].[Employee_Provision_Type] (
    [ProvisionTypeID]    NVARCHAR (15) NOT NULL,
    [ProvisionTypeName]  NVARCHAR (64) NULL,
    [ExpenseAccountID]   NVARCHAR (64) NULL,
    [ProvisionAccountID] NVARCHAR (64) NULL,
    [ProvisionFor]       INT           NULL,
    [Inactive]           BIT           NULL,
    [DateCreated]        DATETIME      NULL,
    [DateUpdated]        DATETIME      NULL,
    [CreatedBy]          NVARCHAR (15) NULL,
    [UpdatedBy]          NVARCHAR (15) NULL,
    CONSTRAINT [PK_Employee_Provision_Type] PRIMARY KEY CLUSTERED ([ProvisionTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Rehire] (
    [ActivityID]  INT           NOT NULL,
    [RequestedBy] NVARCHAR (30) NULL,
    CONSTRAINT [PK_Employee_Rehire] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Rehire_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[Employee_Resumption] (
    [ActivityID] INT NOT NULL,
    [LeaveID]    INT NULL,
    CONSTRAINT [PK_Employee_Resumption] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Resumption_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[Employee_Skill_Detail] (
    [EmployeeID]  NVARCHAR (15)  NOT NULL,
    [SkillID]     NVARCHAR (15)  NOT NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [SkillLevel]  NVARCHAR (15)  NULL,
    [RowIndex]    SMALLINT       NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (64)  NULL,
    [UpdatedBy]   NVARCHAR (64)  NULL,
    CONSTRAINT [PK_Employee_Skill_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [SkillID] ASC)
);

GO
CREATE TABLE [dbo].[Employee_Termination] (
    [ActivityID]      INT           NOT NULL,
    [TerminationType] CHAR (1)      NULL,
    [RequestedBy]     NVARCHAR (30) NULL,
    CONSTRAINT [PK_Employee_Termination] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Termination_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID])
);

GO
CREATE TABLE [dbo].[Employee_Transfer] (
    [ActivityID]           INT           NOT NULL,
    [TransferFromLocation] NVARCHAR (15) NULL,
    [TransferToLocation]   NVARCHAR (15) NULL,
    [TransferFromDep]      NVARCHAR (15) NULL,
    [TransferToDep]        NVARCHAR (15) NULL,
    [TransferFromDivision] NVARCHAR (15) NULL,
    [TransferToDivision]   NVARCHAR (15) NULL,
    [FromPosition]         NVARCHAR (15) NULL,
    [ToPosition]           NVARCHAR (15) NULL,
    [RequestedBy]          NVARCHAR (30) NULL,
    [Period]               CHAR (1)      NULL,
    CONSTRAINT [PK_Employee_Transfer] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_Employee_Transfer_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE
);

GO
CREATE TABLE [dbo].[Employee_Type] (
    [TypeID]          NVARCHAR (15)  NOT NULL,
    [TypeName]        NVARCHAR (64)  NULL,
    [IsPayroll]       BIT            NULL,
    [AccountID]       NVARCHAR (64)  NULL,
    [EOSID]           NVARCHAR (15)  NULL,
    [LeaveSelection]  CHAR (10)      NULL,
    [CalendarID]      NVARCHAR (50)  NULL,
    [DefaultOTTypeID] NVARCHAR (30)  NULL,
    [Inactive]        BIT            NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Employee_Type_Detail] (
    [TypeID]      NVARCHAR (15) NULL,
    [LeaveTypeID] NVARCHAR (15) NULL,
    [OTTypeID]    NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Entity] (
    [EntityID]    NVARCHAR (15)  NULL,
    [EntityName]  NVARCHAR (64)  NULL,
    [Description] NVARCHAR (255) NULL,
    [EntityType]  TINYINT        NULL,
    [Inactive]    BIT            NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Entity_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [CustomerID]         NVARCHAR (64)  NOT NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Comment]            NVARCHAR (255) NULL,
    CONSTRAINT [PK_Entity_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CustomerID] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Category] (
    [CategoryID]       NVARCHAR (15)  NOT NULL,
    [CategoryName]     NVARCHAR (100) NOT NULL,
    [EntityType]       TINYINT        NULL,
    [Note]             NVARCHAR (255) NULL,
    [Inactive]         BIT            CONSTRAINT [DF_Entity_Category_Inactive] DEFAULT ((0)) NULL,
    [ParentCategoryID] NVARCHAR (15)  NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    CONSTRAINT [PK_Entity_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Category_Detail] (
    [EntityID]    NVARCHAR (64) NOT NULL,
    [CategoryID]  NVARCHAR (15) NOT NULL,
    [EntityType]  TINYINT       NOT NULL,
    [DateUpdated] DATETIME      NULL,
    CONSTRAINT [PK_Entity_Category_Detail_1] PRIMARY KEY CLUSTERED ([EntityID] ASC, [CategoryID] ASC, [EntityType] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Comments] (
    [CommentID]      INT             IDENTITY (1, 1) NOT NULL,
    [EntityType]     TINYINT         NULL,
    [EntityID]       NVARCHAR (64)   NULL,
    [EntitySysDocID] NVARCHAR (7)    NULL,
    [RowIndex]       INT             NULL,
    [Note]           NVARCHAR (4000) NULL,
    [UserID]         NVARCHAR (15)   NULL,
    [DateCreated]    DATETIME        NULL,
    [DateUpdated]    DATETIME        NULL,
    [CreatedBy]      NVARCHAR (15)   NULL,
    [UpdatedBy]      NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Entity_Comments] PRIMARY KEY CLUSTERED ([CommentID] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Contacts] (
    [EntityType] TINYINT       NOT NULL,
    [EntityID]   NVARCHAR (64) NOT NULL,
    [ContactID]  NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_Entity_Contacts] PRIMARY KEY CLUSTERED ([EntityType] ASC, [EntityID] ASC, [ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Flag] (
    [FlagID]      INT            IDENTITY (1, 1) NOT NULL,
    [FlagName]    NVARCHAR (100) NOT NULL,
    [EntityType]  TINYINT        NULL,
    [Color]       INT            NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Entity_Flag] PRIMARY KEY CLUSTERED ([FlagID] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Flag_Detail] (
    [EntityID]   NVARCHAR (64) NOT NULL,
    [FlagID]     NVARCHAR (15) NOT NULL,
    [EntityType] TINYINT       NOT NULL,
    CONSTRAINT [PK_Entity_Flag_Detail] PRIMARY KEY CLUSTERED ([EntityID] ASC, [FlagID] ASC, [EntityType] ASC)
);

GO
CREATE TABLE [dbo].[Entity_Notes] (
    [CommentID]      INT             IDENTITY (1, 1) NOT NULL,
    [EntityType]     TINYINT         NULL,
    [EntityID]       NVARCHAR (64)   NULL,
    [EntitySysDocID] NVARCHAR (7)    NULL,
    [RowIndex]       INT             NULL,
    [Note]           NVARCHAR (4000) NULL,
    [UserID]         NVARCHAR (15)   NOT NULL,
    [DateCreated]    DATETIME        NULL,
    [DateUpdated]    DATETIME        NULL,
    [CreatedBy]      NVARCHAR (15)   NULL,
    [UpdatedBy]      NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Entity_Notes] PRIMARY KEY CLUSTERED ([CommentID] ASC)
);

GO
CREATE TABLE [dbo].[EntityDocs] (
    [DocID]            INT            IDENTITY (1, 1) NOT NULL,
    [EntityID]         NVARCHAR (64)  NOT NULL,
    [EntityType]       TINYINT        NOT NULL,
    [EntityDocName]    NVARCHAR (150) NOT NULL,
    [EntitySysDocID]   NVARCHAR (15)  NULL,
    [RowIndex]         INT            NULL,
    [EntityDocPath]    NVARCHAR (250) NULL,
    [EntityDocDesc]    NVARCHAR (255) NULL,
    [EntityDocKeyword] NVARCHAR (255) NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    CONSTRAINT [PK_EntityDocs] PRIMARY KEY CLUSTERED ([DocID] ASC)
);

GO
CREATE TABLE [dbo].[EOY_Product] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [FiscalYearID]      NVARCHAR (15)   NOT NULL,
    [FiscalYear]        INT             NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NULL,
    [AvgCost]           DECIMAL (18, 5) NULL,
    [LocationID]        NVARCHAR (15)   NOT NULL,
    [IsConsignLocation] BIT             NULL,
    [AssetValue]        DECIMAL (18, 5) NULL,
    [EndDate]           DATETIME        NULL,
    CONSTRAINT [PK_EOY_Product] PRIMARY KEY CLUSTERED ([FiscalYearID] ASC, [ProductID] ASC, [LocationID] ASC)
);

GO
CREATE TABLE [dbo].[Equipment] (
    [EquipmentID]   NVARCHAR (15) NOT NULL,
    [EquipmentName] NVARCHAR (64) NULL,
    [Description]   NVARCHAR (64) NULL,
    [AssetID]       NVARCHAR (15) NULL,
    [BillingRate]   MONEY         NULL,
    [BillingUnit]   TINYINT       NULL,
    [CreatedBy]     NVARCHAR (15) NULL,
    [UpdatedBy]     NVARCHAR (15) NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([EquipmentID] ASC)
);

GO
CREATE TABLE [dbo].[Event_Employee] (
    [EventID]    NVARCHAR (15) NOT NULL,
    [EmployeeID] NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Events] (
    [EventID]     NVARCHAR (15)  NOT NULL,
    [EventName]   NVARCHAR (64)  NOT NULL,
    [StartDate]   DATETIME       NULL,
    [EndDate]     DATETIME       NULL,
    [Type]        NVARCHAR (15)  NULL,
    [LeadID]      NVARCHAR (64)  NULL,
    [IsInactive]  BIT            NULL,
    [AreaID]      NVARCHAR (15)  NULL,
    [UserID]      NVARCHAR (15)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Expense_Code] (
    [ExpenseID]   NVARCHAR (15) NOT NULL,
    [ExpenseName] NVARCHAR (64) NULL,
    [Description] NVARCHAR (64) NULL,
    [Remarks]     NVARCHAR (64) NULL,
    [AccountID]   NVARCHAR (64) NULL,
    [ExpenseType] TINYINT       NULL,
    [ExpenseRate] MONEY         NULL,
    [TaxOption]   TINYINT       NULL,
    [TaxGroupID]  NVARCHAR (15) NULL,
    [Inactive]    BIT           NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Expense_Code] PRIMARY KEY CLUSTERED ([ExpenseID] ASC)
);

GO
CREATE TABLE [dbo].[Export_PackingList] (
    [SysDocID]         NVARCHAR (6)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [DivisionID]       NVARCHAR (15)   NULL,
    [CompanyID]        TINYINT         NULL,
    [CustomerID]       NVARCHAR (64)   NOT NULL,
    [TransactionDate]  DATETIME        NOT NULL,
    [SalesFlow]        TINYINT         NULL,
    [ContainerNumber]  NVARCHAR (15)   NULL,
    [Port]             NVARCHAR (15)   NULL,
    [ETA]              DATETIME        NULL,
    [Status]           TINYINT         CONSTRAINT [DF_Export_Packing_List_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID] NVARCHAR (15)   NULL,
    [IsVoid]           BIT             NULL,
    [Reference]        NVARCHAR (20)   NULL,
    [PONumber]         NVARCHAR (20)   NULL,
    [BOLNumber]        NVARCHAR (20)   NULL,
    [Shipper]          NVARCHAR (15)   NULL,
    [ClearingAgent]    NVARCHAR (30)   NULL,
    [Weight]           DECIMAL (18, 5) NULL,
    [Note]             NVARCHAR (4000) NULL,
    [Value]            MONEY           NULL,
    [PackingListTag]   NVARCHAR (64)   NULL,
    [ShipStatus]       BIT             NULL,
    [IsInvoiced]       BIT             NULL,
    [IsShipment]       BIT             NULL,
    [ContainerSizeID]  NVARCHAR (30)   NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [License]          NVARCHAR (30)   NULL,
    [Balance]          NVARCHAR (50)   NULL,
    [Terms]            NVARCHAR (50)   NULL,
    [TotalPackages]    INT             NULL,
    [CountryofOrigin]  NVARCHAR (75)   NULL,
    [Box]              NVARCHAR (30)   NULL,
    [NetWeight]        NUMERIC (15, 2) NULL,
    [GrossWeight]      NUMERIC (15, 2) NULL,
    [Length]           NUMERIC (15, 2) NULL,
    [Width]            NUMERIC (15, 2) NULL,
    [Height]           NUMERIC (15, 2) NULL,
    [CubicMeasure]     NUMERIC (15, 2) NULL,
    [DriverID]         NVARCHAR (15)   NULL,
    [VehicleID]        NVARCHAR (30)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Export_Packing_List] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Export_PackingList_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceRowIndex]   INT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [IsSourcedRow]     BIT             NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [RowIndex]         TINYINT         NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Export_PickList] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [SourceDocType]     TINYINT         NULL,
    [SalesFlow]         TINYINT         NULL,
    [CLUserID]          NVARCHAR (15)   NULL,
    [IsExport]          BIT             NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [BillingAddressID]  NVARCHAR (15)   NULL,
    [ShipToAddress]     NVARCHAR (255)  NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Port]              NVARCHAR (15)   NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Export_PickList_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [IsInvoiced]        BIT             NULL,
    [IsShipped]         BIT             NULL,
    [ContainerNumber]   NVARCHAR (30)   NULL,
    [ContainerSizeID]   NVARCHAR (20)   NULL,
    [InvoiceSysDocID]   NVARCHAR (7)    NULL,
    [InvoiceVoucherID]  NVARCHAR (15)   NULL,
    [DriverID]          NVARCHAR (15)   NULL,
    [VehicleID]         NVARCHAR (15)   NULL,
    [PriceIncludeTax]   BIT             NULL,
    [TaxOption]         TINYINT         NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Export_PickList] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Export_PickList_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [RowIndex]         TINYINT         NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [RowSource]        TINYINT         NULL
);

GO
CREATE TABLE [dbo].[External_Report_User_Link] (
    [ExternalReportName] NVARCHAR (50) NOT NULL,
    [UserID]             NVARCHAR (50) NOT NULL,
    [EntityType]         TINYINT       NOT NULL
);

GO
CREATE TABLE [dbo].[ExternalReport] (
    [ExternalReportID]               INT            IDENTITY (1, 1) NOT NULL,
    [ExternalReportName]             NVARCHAR (64)  NULL,
    [Description]                    NVARCHAR (255) NULL,
    [Query]                          NTEXT          NULL,
    [ReportData]                     IMAGE          NULL,
    [CategoryID]                     NVARCHAR (30)  NULL,
    [ParentID]                       NVARCHAR (30)  NULL,
    [DrillAction]                    TINYINT        NULL,
    [DrillCardTypeID]                INT            NULL,
    [DrillCardIDField]               NVARCHAR (30)  NULL,
    [DrillTransactionSysDocIDField]  NVARCHAR (30)  NULL,
    [DrillTransactionVoucherIDField] NVARCHAR (30)  NULL,
    [DrillParm1]                     NVARCHAR (30)  NULL,
    [DrillParm2]                     NVARCHAR (30)  NULL,
    [DrillParm3]                     NVARCHAR (30)  NULL,
    [DrillParm4]                     NVARCHAR (30)  NULL,
    [IsSubReport]                    BIT            NULL,
    [DrillSubReportID]               INT            NULL,
    [IsPreview]                      BIT            NULL,
    [CreatedBy]                      NVARCHAR (15)  NULL,
    [DateCreated]                    DATETIME       NULL,
    [UpdatedBy]                      NVARCHAR (15)  NULL,
    [DateUpdated]                    DATETIME       NULL,
    CONSTRAINT [PK_ExternalReport] PRIMARY KEY CLUSTERED ([ExternalReportID] ASC)
);

GO
CREATE TABLE [dbo].[ExternalReport_Category] (
    [CategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (30) NULL,
    [ParentID]     NVARCHAR (30) NULL,
    CONSTRAINT [PK_ExternalReport_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[FiscalYear] (
    [FiscalYearID]     NVARCHAR (15) NOT NULL,
    [FiscalYearName]   NVARCHAR (64) NULL,
    [StartDate]        DATETIME      NULL,
    [EndDate]          DATETIME      NULL,
    [PeriodsCount]     TINYINT       NULL,
    [Status]           TINYINT       NULL,
    [ClosingSysDocID]  NVARCHAR (7)  NULL,
    [ClosingVoucherID] NVARCHAR (15) NULL,
    [DateCreated]      DATETIME      NULL,
    [DateUpdated]      DATETIME      NULL,
    [CreatedBy]        NVARCHAR (15) NULL,
    [UpdatedBy]        NVARCHAR (15) NULL,
    CONSTRAINT [PK_Fiscal_Year] PRIMARY KEY CLUSTERED ([FiscalYearID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset] (
    [AssetID]            NVARCHAR (15)  NOT NULL,
    [AssetName]          NVARCHAR (64)  NULL,
    [AquesitionDate]     DATETIME       NULL,
    [AquesitionCost]     MONEY          NULL,
    [AssetClassID]       NVARCHAR (15)  NULL,
    [AssetGroupID]       NVARCHAR (15)  NULL,
    [Status]             TINYINT        NULL,
    [Life]               INT            NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [DepartmentID]       NVARCHAR (15)  NULL,
    [AssetLocationID]    NVARCHAR (15)  NULL,
    [EmployeeID]         NVARCHAR (15)  NULL,
    [BookValue]          MONEY          NULL,
    [OriginalValue]      MONEY          NULL,
    [SalvageValue]       MONEY          NULL,
    [InvoiceNumber]      NVARCHAR (30)  NULL,
    [PurchasePrice]      MONEY          NULL,
    [PurchaseDate]       DATETIME       NULL,
    [SupplierName]       NVARCHAR (64)  NULL,
    [PurchaseRemarks]    NVARCHAR (64)  NULL,
    [DepMethod]          TINYINT        NULL,
    [OpeningDepAmount]   MONEY          NULL,
    [DepPercent]         DECIMAL (5, 2) NULL,
    [DepFrequency]       TINYINT        NULL,
    [AccumDep]           MONEY          NULL,
    [LastDepAmount]      MONEY          NULL,
    [LastDepDate]        DATETIME       NULL,
    [NextDepDate]        DATETIME       NULL,
    [NextDepAmount]      MONEY          NULL,
    [DepStartDate]       DATETIME       NULL,
    [SerialNumber]       NVARCHAR (30)  NULL,
    [BarcodeNumber]      NVARCHAR (30)  NULL,
    [ModelNumber]        NVARCHAR (30)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [Inactive]           BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED ([AssetID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_BulkPurchase] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [TransactionDate] DATETIME        NULL,
    [PayeeID]         NVARCHAR (64)   NULL,
    [PayeeType]       NVARCHAR (1)    NULL,
    [VendorID]        NVARCHAR (64)   NULL,
    [CurrencyID]      NVARCHAR (15)   NULL,
    [CurrencyRate]    MONEY           NULL,
    [Reference]       NVARCHAR (15)   NULL,
    [Quantity]        DECIMAL (15, 2) NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [AssetClassID]    NVARCHAR (15)   NULL,
    [ItemAmount]      MONEY           NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [IsVoid]          BIT             NULL,
    [Note]            NVARCHAR (255)  NULL,
    [BuyerID]         NVARCHAR (15)   NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_FixedAsset_BulkPurchase] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_BulkPurchase_Detail] (
    [SysDocID]      NVARCHAR (7)  NOT NULL,
    [VoucherID]     NVARCHAR (15) NOT NULL,
    [AssetID]       NVARCHAR (15) NULL,
    [AssetName]     NVARCHAR (64) NULL,
    [SerialNumber]  NVARCHAR (30) NULL,
    [BarcodeNumber] NVARCHAR (30) NULL,
    [Description]   NVARCHAR (64) NULL,
    [RowIndex]      INT           NULL,
    [Amount]        MONEY         NULL,
    [AmountFC]      MONEY         NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Class] (
    [AssetClassID]        NVARCHAR (15)  NULL,
    [AssetClassName]      NVARCHAR (30)  NULL,
    [AssetAccountID]      NVARCHAR (64)  NULL,
    [DepAccountID]        NVARCHAR (64)  NULL,
    [ProfitLossAccountID] NVARCHAR (64)  NULL,
    [AccumDepAccountID]   NVARCHAR (64)  NULL,
    [DepFrequency]        TINYINT        NULL,
    [DepMethod]           TINYINT        NULL,
    [Note]                NVARCHAR (255) NULL,
    [Inactive]            BIT            NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Dep] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Reference]       NVARCHAR (20)  NULL,
    [Year]            INT            NULL,
    [Month]           TINYINT        NULL,
    [Description]     NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Dep_Detail] (
    [SysDocID]       NVARCHAR (7)   NOT NULL,
    [VoucherID]      NVARCHAR (15)  NOT NULL,
    [FixedAssetID]   NVARCHAR (15)  NULL,
    [YearValue]      MONEY          NULL,
    [CurrentValue]   MONEY          NULL,
    [Description]    NVARCHAR (255) NULL,
    [DepAmount]      MONEY          NULL,
    [RowIndex]       INT            NULL,
    [AssetAccountID] NVARCHAR (64)  NULL,
    [DepAccountID]   NVARCHAR (64)  NULL,
    [Month]          TINYINT        NULL,
    [Year]           INT            NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Dep_Schedule] (
    [SheduleID]       INT           IDENTITY (1, 1) NOT NULL,
    [AssetID]         NVARCHAR (15) NULL,
    [ScheduleDate]    DATETIME      NULL,
    [DepAmount]       MONEY         NULL,
    [IsRecorded]      BIT           NULL,
    [TransactionDate] DATETIME      NULL,
    [SysDocID]        NVARCHAR (7)  NULL,
    [VoucherNumber]   NVARCHAR (15) NULL,
    [Month]           TINYINT       NULL,
    [Year]            INT           NULL,
    CONSTRAINT [PK_FixedAsset_Dep_Schedule] PRIMARY KEY CLUSTERED ([SheduleID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Group] (
    [AssetGroupID]   NVARCHAR (15)  NOT NULL,
    [AssetGroupName] NVARCHAR (64)  NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Asset_Group] PRIMARY KEY CLUSTERED ([AssetGroupID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Location] (
    [AssetLocationID]   NVARCHAR (15)  NOT NULL,
    [AssetLocationName] NVARCHAR (64)  NULL,
    [DepAccountID]      NVARCHAR (64)  NULL,
    [CompanyID]         NVARCHAR (64)  NULL,
    [CountryID]         NVARCHAR (64)  NULL,
    [Note]              NVARCHAR (255) NULL,
    [Inactive]          BIT            NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Asset_Location] PRIMARY KEY CLUSTERED ([AssetLocationID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Purchase] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [TransactionDate] DATETIME        NULL,
    [PayeeID]         NVARCHAR (64)   NULL,
    [PayeeType]       NVARCHAR (1)    NULL,
    [VendorID]        NVARCHAR (64)   NULL,
    [CurrencyID]      NVARCHAR (15)   NULL,
    [CurrencyRate]    MONEY           NULL,
    [Reference]       NVARCHAR (15)   NULL,
    [Quantity]        DECIMAL (15, 2) NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [AssetClassID]    NVARCHAR (15)   NULL,
    [ItemAmount]      MONEY           NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       MONEY           NULL,
    [IsVoid]          BIT             NULL,
    [Note]            NVARCHAR (255)  NULL,
    [BuyerID]         NVARCHAR (15)   NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Asset_Purchase] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Purchase_Detail] (
    [SysDocID]      NVARCHAR (7)    NOT NULL,
    [VoucherID]     NVARCHAR (15)   NOT NULL,
    [AssetID]       NVARCHAR (15)   NULL,
    [AssetName]     NVARCHAR (64)   NULL,
    [SerialNumber]  NVARCHAR (30)   NULL,
    [BarcodeNumber] NVARCHAR (30)   NULL,
    [Description]   NVARCHAR (64)   NULL,
    [RowIndex]      INT             NULL,
    [Amount]        MONEY           NULL,
    [AmountFC]      MONEY           NULL,
    [TaxOption]     TINYINT         NULL,
    [TaxGroupID]    NVARCHAR (15)   NULL,
    [TaxAmount]     DECIMAL (18, 5) NULL,
    [TaxAmountFC]   DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Purchase_Order] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [VendorID]           NVARCHAR (64)  NOT NULL,
    [IsImport]           BIT            NULL,
    [TransactionDate]    DATETIME       NOT NULL,
    [PurchaseFlow]       TINYINT        NULL,
    [DueDate]            DATETIME       NULL,
    [ContainerSizeID]    NVARCHAR (15)  NULL,
    [BuyerID]            NVARCHAR (64)  NULL,
    [RequiredDate]       DATETIME       NULL,
    [ShippingAddressID]  NVARCHAR (15)  NULL,
    [VendorAddress]      NVARCHAR (255) NULL,
    [PriceIncludeTax]    BIT            NULL,
    [Status]             TINYINT        CONSTRAINT [DF_FixedAsset_Purchase_Order_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)   NULL,
    [TermID]             NVARCHAR (15)  NULL,
    [ShippingMethodID]   NVARCHAR (15)  NULL,
    [IsVoid]             BIT            NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)  NULL,
    [SourceDocType]      TINYINT        NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [Reference2]         NVARCHAR (20)  NULL,
    [VendorReferenceNo]  NVARCHAR (40)  NULL,
    [PortLoading]        NVARCHAR (15)  NULL,
    [PortDestination]    NVARCHAR (15)  NULL,
    [ETA]                DATETIME       NULL,
    [ETD]                DATETIME       NULL,
    [ActualReqDate]      DATETIME       NULL,
    [INCOID]             NVARCHAR (15)  NULL,
    [Discount]           MONEY          NULL,
    [TaxOption]          TINYINT        NULL,
    [TaxAmount]          MONEY          NULL,
    [TaxAmountFC]        MONEY          NULL,
    [Total]              MONEY          NULL,
    [PONumber]           NVARCHAR (15)  NULL,
    [IsShipped]          BIT            NULL,
    [BOLNo]              NVARCHAR (20)  NULL,
    [JobID]              NVARCHAR (50)  NULL,
    [CostCategoryID]     NVARCHAR (30)  NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_FixedAsset_Purchase_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Purchase_Order_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [AssetID]          NVARCHAR (64)   NOT NULL,
    [AssetName]        NVARCHAR (255)  NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsSourcedRow]     BIT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [Remarks]          NVARCHAR (300)  NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Sale] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [PayeeID]         NVARCHAR (64)  NULL,
    [PayeeType]       NVARCHAR (1)   NULL,
    [Ref]             NVARCHAR (15)  NULL,
    [CurrencyID]      NVARCHAR (15)  NULL,
    [CurrencyRate]    MONEY          NULL,
    [Amount]          MONEY          NULL,
    [AmountFC]        MONEY          NULL,
    [TaxOption]       TINYINT        NULL,
    [TaxGroupID]      NVARCHAR (15)  NULL,
    [TaxAmount]       MONEY          NULL,
    [Reference]       NVARCHAR (15)  NULL,
    [IsVoid]          BIT            NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Asset_Sale] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Sale_Detail] (
    [SysDocID]    NVARCHAR (7)    NOT NULL,
    [VoucherID]   NVARCHAR (15)   NOT NULL,
    [AssetID]     NVARCHAR (15)   NULL,
    [Description] NVARCHAR (64)   NULL,
    [Price]       MONEY           NULL,
    [RowIndex]    INT             NULL,
    [Amount]      MONEY           NULL,
    [AmountFC]    MONEY           NULL,
    [TaxOption]   TINYINT         NULL,
    [TaxGroupID]  NVARCHAR (15)   NULL,
    [TaxAmount]   DECIMAL (18, 5) NULL,
    [TaxAmountFC] DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[FixedAsset_Transfer] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [AssetID]            NVARCHAR (15)  NULL,
    [TransactionDate]    DATETIME       NULL,
    [DivisionFromID]     NVARCHAR (15)  NULL,
    [DivisionToID]       NVARCHAR (15)  NULL,
    [DepartmentFromID]   NVARCHAR (15)  NULL,
    [DepartmentToID]     NVARCHAR (15)  NULL,
    [LocationFromID]     NVARCHAR (15)  NULL,
    [LocationToID]       NVARCHAR (15)  NULL,
    [EmployeeFromID]     NVARCHAR (15)  NULL,
    [EmployeeToID]       NVARCHAR (15)  NULL,
    [Reference]          NVARCHAR (15)  NULL,
    [TransferType]       TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Asset_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[FixedAsset_Transfer_Detail] (
    [SysDocID]    NVARCHAR (7)  NULL,
    [VoucherID]   NVARCHAR (15) NULL,
    [AssetID]     NVARCHAR (15) NULL,
    [Description] NVARCHAR (64) NULL,
    [Reference]   NVARCHAR (15) NULL,
    [RowIndex]    INT           NULL
);

GO
CREATE TABLE [dbo].[FormList_Details] (
    [FormType]    TINYINT       NULL,
    [FormID]      NVARCHAR (50) NULL,
    [ControlID]   NVARCHAR (50) NULL,
    [ListValue]   NVARCHAR (50) NULL,
    [ListName]    NVARCHAR (50) NULL,
    [ValueIndex]  INT           NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[FormMenuSubstituteText] (
    [FormID]    INT            NOT NULL,
    [FormText]  NVARCHAR (250) NULL,
    [MenuText]  NVARCHAR (250) NULL,
    [AliasName] NVARCHAR (250) NOT NULL
);

GO
CREATE TABLE [dbo].[Freight_Charge] (
    [SysDocID]        NVARCHAR (6)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [DivisionID]      NVARCHAR (15)   NULL,
    [CompanyID]       TINYINT         NULL,
    [TransactionDate] DATETIME        NOT NULL,
    [Status]          TINYINT         CONSTRAINT [DF_Freight_Charge_Status] DEFAULT ((1)) NULL,
    [Inactive]        BIT             NULL,
    [IsVoid]          BIT             NULL,
    [Discount]        MONEY           NULL,
    [TaxAmount]       MONEY           NULL,
    [Total]           MONEY           NULL,
    [ValidDateFrom]   DATETIME        NULL,
    [ValidDateTo]     DATETIME        NULL,
    [Reference]       NVARCHAR (20)   NULL,
    [Note]            NVARCHAR (4000) NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Freight_Charge] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Freight_Charge_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ShippingCompanyID] NVARCHAR (64)   NULL,
    [SourcePortID]      NVARCHAR (15)   NULL,
    [DestinationPortID] NVARCHAR (15)   NULL,
    [TypeID]            NVARCHAR (15)   NULL,
    [Remarks]           NVARCHAR (255)  NULL,
    [ContainerSizeID]   NVARCHAR (30)   NULL,
    [FreeDays]          REAL            NULL,
    [TransitDays]       REAL            NULL,
    [Amount]            DECIMAL (18, 5) NULL,
    [RowIndex]          TINYINT         NULL
);

GO
CREATE TABLE [dbo].[Garment_Rental] (
    [SysDocID]             NVARCHAR (7)    NOT NULL,
    [VoucherID]            NVARCHAR (15)   NOT NULL,
    [CustomerID]           NVARCHAR (64)   NOT NULL,
    [TransactionDate]      DATETIME        NOT NULL,
    [SalespersonID]        NVARCHAR (64)   NULL,
    [ConsignLocationID]    NVARCHAR (15)   NULL,
    [ShippingAddressID]    NVARCHAR (15)   NULL,
    [CustomerAddress]      NVARCHAR (255)  NULL,
    [PackageID]            NVARCHAR (15)   NULL,
    [ExpReturnDate]        DATETIME        NULL,
    [OutDate]              DATETIME        NULL,
    [RegisterID]           NVARCHAR (15)   NULL,
    [Status]               TINYINT         CONSTRAINT [DF_Garment_Rental_Status] DEFAULT ((1)) NULL,
    [IsClosed]             BIT             NULL,
    [CurrencyID]           NVARCHAR (5)    NULL,
    [TermID]               NVARCHAR (15)   NULL,
    [ShippingMethodID]     NVARCHAR (15)   NULL,
    [TaxOption]            TINYINT         NULL,
    [PayeeTaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]            MONEY           NULL,
    [TaxAmountFC]          MONEY           NULL,
    [IsVoid]               BIT             NULL,
    [Reference]            NVARCHAR (20)   NULL,
    [Discount]             MONEY           NULL,
    [Charges]              MONEY           NULL,
    [Total]                MONEY           NULL,
    [CashAmount]           MONEY           NULL,
    [CashAccountID]        NVARCHAR (15)   NULL,
    [CardAccountID]        NVARCHAR (15)   NULL,
    [CardAmount]           MONEY           NULL,
    [AmountPaid]           MONEY           NULL,
    [Balance]              MONEY           NULL,
    [ReceiptVoucherID]     NVARCHAR (15)   NULL,
    [ReceiptVoucherAmount] MONEY           NULL,
    [Note]                 NVARCHAR (4000) NULL,
    [DateCreated]          DATETIME        NULL,
    [DateUpdated]          DATETIME        NULL,
    [CreatedBy]            NVARCHAR (15)   NULL,
    [UpdatedBy]            NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Garment_Rental] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Garment_Rental_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [QuantitySettled]   DECIMAL (18, 5) NULL,
    [QuantityReturned]  DECIMAL (18, 5) NULL,
    [UnitPrice]         DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [TaxOption]         TINYINT         NULL,
    [TaxGroupID]        NVARCHAR (15)   NULL,
    [TaxPercentage]     DECIMAL (18, 5) NULL,
    [TaxAmount]         DECIMAL (18, 5) NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [SubunitPrice]      DECIMAL (18, 5) NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [PackageID]         NVARCHAR (15)   NULL,
    [Discount]          MONEY           NULL,
    [Amount]            MONEY           NULL
);

GO
CREATE TABLE [dbo].[Garment_Rental_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Garment_Rental_Return_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [Discount]          MONEY           NULL,
    [Charges]           MONEY           NULL,
    [Total]             MONEY           NULL,
    [CashAmount]        MONEY           NULL,
    [CashAccountID]     NVARCHAR (15)   NULL,
    [CardAccountID]     NVARCHAR (15)   NULL,
    [CardAmount]        MONEY           NULL,
    [AmountPaid]        MONEY           NULL,
    [Balance]           MONEY           NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_Garment_Rental_Return_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Garment_Rental_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Garment_Rental_Return_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [ConsignLocationID] NVARCHAR (15)   NULL,
    [COGS]              MONEY           NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [SourceRowIndex]    INT             NULL
);

GO
CREATE TABLE [dbo].[Gauge_Range] (
    [RangeID]          NVARCHAR (15) NOT NULL,
    [CustomReportType] TINYINT       NOT NULL,
    [CustomReportID]   NVARCHAR (15) NOT NULL,
    [StartValue]       FLOAT (53)    NULL,
    [EndValue]         FLOAT (53)    NULL,
    [Color]            NVARCHAR (15) NULL,
    [RangeColor]       INT           NULL,
    CONSTRAINT [PK_Gauge_Range] PRIMARY KEY CLUSTERED ([RangeID] ASC, [CustomReportType] ASC, [CustomReportID] ASC)
);

GO
CREATE TABLE [dbo].[General_Payment_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]  NVARCHAR (7)    NOT NULL,
    [SourceVoucherID] NVARCHAR (15)   NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Amount]          MONEY           NOT NULL,
    [PayeeID]         NVARCHAR (64)   NOT NULL,
    [PayeeType]       NVARCHAR (15)   NULL,
    [TransactionDate] DATETIME        NOT NULL,
    [Reference]       NVARCHAR (20)   NULL,
    [Note]            NVARCHAR (1000) NULL,
    [IsVoid]          BIT             NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_General_Payment_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[General_Security] (
    [SecurityRoleID] SMALLINT        NULL,
    [IsAllowed]      BIT             NULL,
    [UserID]         NVARCHAR (15)   NULL,
    [GroupID]        NVARCHAR (15)   NULL,
    [intVal]         DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Generic_List] (
    [GenericListType]      SMALLINT       NOT NULL,
    [GenericListID]        NVARCHAR (15)  NOT NULL,
    [GenericListName]      NVARCHAR (64)  NULL,
    [GenericListShortName] NVARCHAR (20)  NULL,
    [Inactive]             BIT            NULL,
    [Note]                 NVARCHAR (255) NULL,
    [DateCreated]          DATETIME       NULL,
    [DateUpdated]          DATETIME       NULL,
    [CreatedBy]            NVARCHAR (15)  NULL,
    [UpdatedBy]            NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Combo_List] PRIMARY KEY CLUSTERED ([GenericListType] ASC, [GenericListID] ASC)
);

GO
CREATE TABLE [dbo].[GL_Setup] (
    [CompanyID]               CHAR (1)      NULL,
    [AccountCustomFieldName1] NVARCHAR (15) NULL,
    [AccountCustomFieldName2] NVARCHAR (15) NULL,
    [AccountCustomFieldName3] NVARCHAR (15) NULL,
    [AccountCustomFieldName4] NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[GL_Transaction] (
    [TransactionID]      INT             NOT NULL,
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [CostCenterID]       NVARCHAR (15)   NULL,
    [PayeeType]          NVARCHAR (1)    NULL,
    [PayeeID]            NVARCHAR (64)   NULL,
    [POSShiftID]         INT             NULL,
    [POSBatchID]         INT             NULL,
    [IsPOS]              BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SecondRegisterID]   NVARCHAR (15)   NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [ExpAmount]          MONEY           NULL,
    [ExpCode]            NVARCHAR (30)   NULL,
    [ExpPercent]         DECIMAL (18)    NULL,
    [IsDebit]            BIT             NULL,
    [TransactionDate]    DATETIME        NULL,
    [IsVoid]             BIT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [GLType]             TINYINT         NULL,
    [JournalID]          INT             NULL,
    [TransferFromType]   CHAR (1)        NULL,
    [TransferToType]     CHAR (1)        NULL,
    [ChequeID]           INT             NULL,
    [ChequebookID]       NVARCHAR (15)   NULL,
    [CheckNumber]        NVARCHAR (15)   NULL,
    [CheckDate]          DATETIME        NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [FirstAccountID]     NVARCHAR (64)   NULL,
    [SecondAccountID]    NVARCHAR (64)   NULL,
    [TransactionStatus]  TINYINT         CONSTRAINT [DF_Transaction_TransactionStatus] DEFAULT ((1)) NULL,
    [EmployeeID]         NVARCHAR (64)   NULL,
    [ReasonID]           NVARCHAR (30)   NULL,
    [Description]        NVARCHAR (255)  NULL,
    [TypeID]             INT             NULL,
    [AnalysisID]         NVARCHAR (30)   NULL,
    [RequestSysDocID]    NVARCHAR (7)    NULL,
    [RequestVoucherID]   NVARCHAR (15)   NULL,
    [IsSecondForm]       BIT             NULL,
    [CheckDeliveredDate] DATETIME        NULL,
    [TaxGroupID]         NVARCHAR (30)   NULL,
    [TaxAmount]          DECIMAL (18, 5) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (64)   NULL,
    [UpdatedBy]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC),
    CONSTRAINT [FK_GL_Transaction_Account_First] FOREIGN KEY ([FirstAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_GL_Transaction_Account_Second] FOREIGN KEY ([SecondAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_GL_Transaction_GL_Transaction] FOREIGN KEY ([SysDocID], [VoucherID]) REFERENCES [dbo].[GL_Transaction] ([SysDocID], [VoucherID])
);

GO
CREATE TABLE [dbo].[Global Preferences] (
    [ID]            INT           NOT NULL,
    [UserID]        NVARCHAR (15) NULL,
    [IsCurrentUser] BIT           CONSTRAINT [DF_Personals_IsCurrentUser] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Personals] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Personals_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

GO
CREATE TABLE [dbo].[GRN_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [VendorID]          NVARCHAR (64)   NOT NULL,
    [IsCash]            BIT             CONSTRAINT [DF_GRN_Return_IsCash] DEFAULT ((0)) NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [BuyerID]           NVARCHAR (64)   NULL,
    [PurchaseFlow]      TINYINT         NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [VendorAddress]     NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_GRN_Return_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [VendorReferenceNo] NVARCHAR (40)   NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [SourceDocType]     TINYINT         NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_GRN_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[GRN_Return_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [OrderRowIndex]   INT             NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [ITRowID]         INT             NULL
);

GO
CREATE TABLE [dbo].[Holiday_Calendar] (
    [CalendarID]         NVARCHAR (64)  NOT NULL,
    [CalendarName]       NVARCHAR (64)  NOT NULL,
    [OffDays]            NVARCHAR (50)  NULL,
    [OffDateFrom]        DATETIME       NULL,
    [OffDateTo]          DATETIME       NULL,
    [Remarks]            NVARCHAR (250) NULL,
    [IsInactive]         BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Holiday_Calendar] PRIMARY KEY CLUSTERED ([CalendarID] ASC)
);

GO
CREATE TABLE [dbo].[Holiday_Calendar_Detail] (
    [CalendarID]  NVARCHAR (64)  NULL,
    [FromDate]    DATETIME       NULL,
    [ToDate]      DATETIME       NULL,
    [Days]        INT            NULL,
    [HolidayType] NCHAR (10)     NULL,
    [Remarks]     NVARCHAR (100) NULL,
    [RowIndex]    INT            NULL
);

GO
CREATE TABLE [dbo].[Horse_Document] (
    [HorseID]        NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Horse_Docs] PRIMARY KEY CLUSTERED ([HorseID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Horse_Sex] (
    [HorseSexID]   NVARCHAR (15)  NOT NULL,
    [HorseSexName] NVARCHAR (64)  NOT NULL,
    [IsInactive]   BIT            NULL,
    [Note]         NVARCHAR (255) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Horse_Summary] (
    [HorseCode]             NVARCHAR (50) NOT NULL,
    [HorseName]             NVARCHAR (50) NULL,
    [HorseType]             NVARCHAR (50) NULL,
    [RegisterNumber]        NVARCHAR (50) NULL,
    [IsInactive]            BIT           NULL,
    [MicroChipNumber]       NVARCHAR (50) NULL,
    [Brand]                 NVARCHAR (50) NULL,
    [Breed]                 NVARCHAR (50) NULL,
    [DateOfBirth]           DATETIME      NULL,
    [Colour]                NVARCHAR (50) NULL,
    [Sex]                   NVARCHAR (50) NULL,
    [OwnershipTypeID]       NVARCHAR (50) NULL,
    [CategoryID]            NVARCHAR (50) NULL,
    [Sire]                  NVARCHAR (50) NULL,
    [Dam]                   NVARCHAR (50) NULL,
    [SireOfDam]             NVARCHAR (50) NULL,
    [Photo]                 IMAGE         NULL,
    [CountryOfOrigin]       NVARCHAR (50) NULL,
    [CurrentOwnerShip]      NVARCHAR (50) NULL,
    [PreviousOwnership]     NVARCHAR (50) NULL,
    [OwnerShipChangedDate]  DATETIME      NULL,
    [Breeder]               NVARCHAR (50) NULL,
    [LocationID]            NVARCHAR (50) NULL,
    [RiderID]               NVARCHAR (50) NULL,
    [CareTaker]             NVARCHAR (50) NULL,
    [PassportIssueDate]     DATETIME      NULL,
    [PassportExpiryDate]    DATETIME      NULL,
    [RevalidationDate]      DATETIME      NULL,
    [ImportedFrom]          NVARCHAR (50) NULL,
    [ImportedDate]          DATETIME      NULL,
    [PastPerformance]       NVARCHAR (50) NULL,
    [ExportedTo]            NVARCHAR (50) NULL,
    [ReceivedFrom]          NVARCHAR (50) NULL,
    [ReceivedDate]          DATETIME      NULL,
    [TransferredTo]         NVARCHAR (50) NULL,
    [TransferredDate]       DATETIME      NULL,
    [SoldAt]                NVARCHAR (50) NULL,
    [SoldDate]              DATETIME      NULL,
    [SexChangedFrom]        NVARCHAR (50) NULL,
    [SexChangedDate]        DATETIME      NULL,
    [OwnerShipTransferDate] DATETIME      NULL,
    [DeadDate]              DATETIME      NULL,
    [UserDefined1]          NVARCHAR (64) NULL,
    [UserDefined2]          NVARCHAR (64) NULL,
    [UserDefined3]          NVARCHAR (64) NULL,
    [UserDefined4]          NVARCHAR (64) NULL,
    [DateCreated]           DATETIME      NULL,
    [DateUpdated]           DATETIME      NULL,
    [CreatedBy]             NVARCHAR (50) NULL,
    [UpdatedBy]             NVARCHAR (50) NULL
);

GO
CREATE TABLE [dbo].[Horse_Type] (
    [HorseTypeID]   NVARCHAR (15)  NOT NULL,
    [HorseTypeName] NVARCHAR (64)  NOT NULL,
    [IsInactive]    BIT            NULL,
    [Note]          NVARCHAR (255) NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[INCO] (
    [INCOID]      NVARCHAR (15)  NOT NULL,
    [INCOName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_INCO] PRIMARY KEY CLUSTERED ([INCOID] ASC)
);

GO
CREATE TABLE [dbo].[Industry] (
    [IndustryID]   NVARCHAR (15) NOT NULL,
    [IndustryName] NVARCHAR (64) NOT NULL,
    [Inactive]     BIT           NULL,
    [CreatedBy]    NVARCHAR (15) NULL,
    [UpdatedBy]    NVARCHAR (15) NULL,
    [DateCreated]  DATETIME      NULL,
    [DateUpdated]  DATETIME      NULL,
    CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED ([IndustryID] ASC)
);

GO
CREATE TABLE [dbo].[Insurance_Provider] (
    [InsuranceProviderID]   NVARCHAR (15)  NOT NULL,
    [InsuranceProviderName] NVARCHAR (64)  NOT NULL,
    [Description]           NVARCHAR (255) NULL,
    [IsInactive]            BIT            NULL,
    [IsMedical]             BIT            NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Insurance_Provider] PRIMARY KEY CLUSTERED ([InsuranceProviderID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Adjustment] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [CompanyID]          TINYINT        NULL,
    [TransactionDate]    DATETIME       NULL,
    [LocationID]         NVARCHAR (15)  NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [AdjustmentTypeID]   NVARCHAR (15)  NULL,
    [AccountID]          NVARCHAR (64)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Inventory_Adjustment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Adjustment_Detail] (
    [SysDocID]      NVARCHAR (7)    NOT NULL,
    [VoucherID]     NVARCHAR (15)   NOT NULL,
    [ProductID]     NVARCHAR (64)   NULL,
    [Description]   NVARCHAR (255)  NULL,
    [Remarks]       NVARCHAR (255)  NULL,
    [Quantity]      DECIMAL (18, 5) NULL,
    [UnitQuantity]  DECIMAL (18, 5) NULL,
    [UnitID]        NVARCHAR (15)   NULL,
    [Factor]        DECIMAL (18, 5) NULL,
    [FactorType]    CHAR (1)        NULL,
    [Cost]          DECIMAL (18, 5) NULL,
    [RowIndex]      INT             NULL,
    [ListVoucherID] NVARCHAR (15)   NULL,
    [ListSysDocID]  NVARCHAR (7)    NULL,
    [ListRowIndex]  INT             NULL,
    [ITRowID]       INT             NULL
);

GO
CREATE TABLE [dbo].[Inventory_Damage] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [CompanyID]          TINYINT        NULL,
    [TransactionDate]    DATETIME       NULL,
    [LocationID]         NVARCHAR (15)  NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [AdjustmentTypeID]   NVARCHAR (15)  NULL,
    [AccountID]          NVARCHAR (64)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [IsVoid]             BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Inventory_Damage] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Damage_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [Description]  NVARCHAR (255)  NULL,
    [Remarks]      NVARCHAR (255)  NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [Factor]       DECIMAL (18, 5) NULL,
    [FactorType]   CHAR (1)        NULL,
    [Cost]         DECIMAL (18, 5) NULL,
    [RowIndex]     INT             NULL,
    [ITRowID]      INT             NULL
);

GO
CREATE TABLE [dbo].[Inventory_Dismantle] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [DimantledProductID] NVARCHAR (64)   NULL,
    [QuantityDismantled] DECIMAL (18, 5) NULL,
    [UnitCost]           DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (255)  NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [TransactionDate]    DATETIME        NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Inventory_Dismantle] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Dismantle_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RowIndex]     INT             NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [Cost]         DECIMAL (18, 5) NULL,
    [Cost_Percent] DECIMAL (18, 5) NULL,
    [Description]  NVARCHAR (255)  NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [UnitFactor]   DECIMAL (18, 5) NULL,
    [FactorType]   NVARCHAR (1)    NULL,
    [SubunitCost]  DECIMAL (18, 5) NULL,
    [LocationID]   NVARCHAR (15)   NULL,
    [COGS]         MONEY           NULL,
    [ITRowID]      INT             NULL,
    CONSTRAINT [PK_Inventory_Dismantle_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Dismantle_Expense] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Inventory_Repacking] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [RepackedProductID]  NVARCHAR (64)   NULL,
    [QuantityRepacked]   DECIMAL (18, 5) NULL,
    [UnitCost]           DECIMAL (18, 5) NULL,
    [Description]        NVARCHAR (255)  NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [TransactionDate]    DATETIME        NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Packed_Item] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Repacking_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RowIndex]     INT             NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [Cost]         DECIMAL (18, 5) NULL,
    [Description]  NVARCHAR (255)  NULL,
    [Remarks]      NVARCHAR (255)  NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [UnitFactor]   DECIMAL (18, 5) NULL,
    [FactorType]   NVARCHAR (1)    NULL,
    [SubunitCost]  DECIMAL (18, 5) NULL,
    [LocationID]   NVARCHAR (15)   NULL,
    [COGS]         MONEY           NULL,
    [ITRowID]      INT             NULL,
    CONSTRAINT [PK_Packet_Item_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Repacking_Expense] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Inventory_Transactions] (
    [TransactionID]    INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]         NVARCHAR (7)    NULL,
    [VoucherID]        NVARCHAR (15)   NULL,
    [SysDocType]       INT             NULL,
    [TransactionDate]  DATETIME        NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [LotNumber]        NVARCHAR (10)   NULL,
    [RowIndex]         INT             NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [EqWorkOrderID]    NVARCHAR (15)   NULL,
    [CompanyID]        TINYINT         NULL,
    [DivisionID]       NVARCHAR (15)   NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [ReturnedQuantity] DECIMAL (18, 5) NULL,
    [FOCQuantity]      DECIMAL (18, 5) NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [Factor]           DECIMAL (18, 5) NULL,
    [FactorType]       CHAR (1)        NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    [Discount]         DECIMAL (18, 5) NULL,
    [AverageCost]      DECIMAL (18, 5) NULL,
    [AssetValue]       MONEY           NULL,
    [Reference]        NVARCHAR (20)   NULL,
    [Description]      NVARCHAR (255)  NULL,
    [PayeeType]        NVARCHAR (1)    NULL,
    [PayeeID]          NVARCHAR (64)   NULL,
    [TransactionType]  TINYINT         NULL,
    [Narration]        NVARCHAR (255)  NULL,
    [Cost]             DECIMAL (18, 5) NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [IsNonCostedGRN]   BIT             NULL,
    [IsVoid]           BIT             NULL,
    [IsRecost]         BIT             NULL,
    [RefSysDocID]      NVARCHAR (7)    NULL,
    [RefVoucherID]     NVARCHAR (15)   NULL,
    [RefRowIndex]      INT             NULL,
    [RefTransactionID] INT             NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Inventory Costs] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [FK_Inventory_Transactions_Location] FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Location] ([LocationID]),
    CONSTRAINT [FK_Inventory_Transactions_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]),
    CONSTRAINT [FK_Inventory_Transactions_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID])
);

GO
CREATE TABLE [dbo].[Inventory_Transfer] (
    [SysDocID]              NVARCHAR (7)   NOT NULL,
    [VoucherID]             NVARCHAR (15)  NOT NULL,
    [DivisionID]            NVARCHAR (15)  NULL,
    [CompanyID]             TINYINT        NULL,
    [TransferTypeID]        NVARCHAR (15)  NULL,
    [TransferAccountID]     NVARCHAR (64)  NULL,
    [TransactionDate]       DATETIME       NULL,
    [LocationFromID]        NVARCHAR (15)  NULL,
    [LocationToID]          NVARCHAR (15)  NULL,
    [Reference]             NVARCHAR (20)  NULL,
    [Description]           NVARCHAR (255) NULL,
    [VehicleNumber]         NVARCHAR (15)  NULL,
    [DriverID]              NVARCHAR (15)  NULL,
    [AcceptSysDocID]        NVARCHAR (7)   NULL,
    [AcceptVoucherID]       NVARCHAR (15)  NULL,
    [AcceptReference]       NVARCHAR (20)  NULL,
    [AcceptDate]            DATETIME       NULL,
    [AcceptedBy]            NVARCHAR (15)  NULL,
    [IsAccepted]            BIT            NULL,
    [RejectSysDocID]        NVARCHAR (7)   NULL,
    [RejectReference]       NVARCHAR (20)  NULL,
    [RejectDate]            DATETIME       NULL,
    [RejectNote]            NVARCHAR (255) NULL,
    [RejectedBy]            NVARCHAR (15)  NULL,
    [IsRejected]            BIT            NULL,
    [RejectAcceptSysDocID]  NVARCHAR (7)   NULL,
    [RejectAcceptVoucherID] NVARCHAR (15)  NULL,
    [RejectAcceptNote]      NVARCHAR (255) NULL,
    [RejectAcceptDate]      DATETIME       NULL,
    [RejectAcceptReference] NVARCHAR (20)  NULL,
    [RejectAcceptedBy]      NVARCHAR (15)  NULL,
    [IsRejectAccepted]      BIT            NULL,
    [IsVoid]                BIT            NULL,
    [ApprovalStatus]        TINYINT        NULL,
    [VerificationStatus]    TINYINT        NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Inventory_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Transfer_Detail] (
    [SysDocID]             NVARCHAR (7)    NOT NULL,
    [VoucherID]            NVARCHAR (15)   NOT NULL,
    [ProductID]            NVARCHAR (64)   NOT NULL,
    [UnitID]               NVARCHAR (15)   NULL,
    [Quantity]             DECIMAL (18, 5) NULL,
    [UnitQuantity]         DECIMAL (18, 5) NULL,
    [Remarks]              NVARCHAR (255)  NULL,
    [Factor]               DECIMAL (18, 5) NULL,
    [FactorType]           CHAR (1)        NULL,
    [AcceptedQuantity]     DECIMAL (18, 5) NULL,
    [AcceptedUnitQuantity] DECIMAL (18, 5) NULL,
    [AcceptedFactor]       DECIMAL (18, 5) NULL,
    [AcceptedFactorType]   CHAR (1)        NULL,
    [RejectedQuantity]     DECIMAL (18, 5) NULL,
    [RejectedUnitQuantity] DECIMAL (18, 5) NULL,
    [RejectedFactor]       DECIMAL (18, 5) NULL,
    [RejectedFactorType]   CHAR (1)        NULL,
    [RowIndex]             INT             NULL,
    [ListVoucherID]        NVARCHAR (15)   NULL,
    [ListSysDocID]         NVARCHAR (7)    NULL,
    [ListRowIndex]         INT             NULL,
    CONSTRAINT [PK_Inventory_Transfer_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ProductID] ASC)
);

GO
CREATE TABLE [dbo].[Inventory_Transfer_Type] (
    [TypeID]      NVARCHAR (15) NOT NULL,
    [TypeName]    NVARCHAR (64) NULL,
    [AccountID]   NVARCHAR (64) NULL,
    [LocationID]  NVARCHAR (15) NULL,
    [Inactive]    BIT           NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    CONSTRAINT [PK_Inventory_Transfer_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Invoice_DNote] (
    [InvoiceSysDocID]  NVARCHAR (7)  NULL,
    [InvoiceVoucherID] NVARCHAR (15) NULL,
    [DNoteSysDocID]    NVARCHAR (7)  NULL,
    [DNoteVoucherID]   NVARCHAR (15) NULL,
    [SourceDocType]    TINYINT       NULL
);

GO
CREATE TABLE [dbo].[Invoice_GRN] (
    [InvoiceSysDocID] NVARCHAR (7)  NULL,
    [InvoiceNumber]   NVARCHAR (15) NULL,
    [GRNSysDocID]     NVARCHAR (7)  NULL,
    [GRNNumber]       NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Invoice_Payment] (
    [SysDocID]          NVARCHAR (7)  NULL,
    [VoucherID]         NVARCHAR (15) NULL,
    [TransactionDate]   DATETIME      NULL,
    [AccountID]         NVARCHAR (64) NULL,
    [Amount]            MONEY         NULL,
    [PaymentMethodID]   NVARCHAR (15) NULL,
    [PaymentMethodType] TINYINT       NULL,
    [RegisterID]        NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Item_Transaction] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [SysDocType]        INT             NULL,
    [PartyID]           NVARCHAR (64)   NULL,
    [PartyType]         NVARCHAR (64)   NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [SourceDocType]     TINYINT         NULL,
    [SalesFlow]         TINYINT         NULL,
    [CLUserID]          NVARCHAR (15)   NULL,
    [IsExport]          BIT             NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [BillingAddressID]  NVARCHAR (15)   NULL,
    [ShipToAddress]     NVARCHAR (255)  NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Port]              NVARCHAR (15)   NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Item_Transaction_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [IsInvoiced]        BIT             NULL,
    [IsShipped]         BIT             NULL,
    [ContainerNumber]   NVARCHAR (30)   NULL,
    [ContainerSizeID]   NVARCHAR (20)   NULL,
    [InvoiceSysDocID]   NVARCHAR (7)    NULL,
    [InvoiceVoucherID]  NVARCHAR (15)   NULL,
    [DriverID]          NVARCHAR (15)   NULL,
    [VehicleID]         NVARCHAR (30)   NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Item_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Item_Transaction_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [RowIndex]         TINYINT         NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [RowSource]        TINYINT         NULL
);

GO
CREATE TABLE [dbo].[Job] (
    [JobID]                    NVARCHAR (50)   NOT NULL,
    [JobName]                  NVARCHAR (64)   NOT NULL,
    [CustomerID]               NVARCHAR (64)   NULL,
    [StartDate]                DATETIME        NULL,
    [EndDate]                  DATETIME        NULL,
    [Status]                   TINYINT         NULL,
    [PONumber]                 NVARCHAR (30)   NULL,
    [SalesPersonID]            NVARCHAR (64)   NULL,
    [ProjectManagerID]         NVARCHAR (64)   NULL,
    [SiteLocationID]           NVARCHAR (15)   NULL,
    [SiteLocationAddress]      NVARCHAR (255)  NULL,
    [JobTypeID]                NVARCHAR (15)   NULL,
    [WIPAccountID]             NVARCHAR (64)   NULL,
    [IncomeAccountID]          NVARCHAR (64)   NULL,
    [TimesheetContraAccountID] NVARCHAR (64)   NULL,
    [CostAccountID]            NVARCHAR (64)   NULL,
    [RetentionAccountID]       NVARCHAR (64)   NULL,
    [RetentionItemID]          NVARCHAR (15)   NULL,
    [RetentionDescription]     NVARCHAR (255)  NULL,
    [RetentionPercent]         DECIMAL (5, 2)  NULL,
    [RetentionAmount]          MONEY           NULL,
    [RetentionPaid]            MONEY           NULL,
    [RetentionDays]            DECIMAL (18, 5) NULL,
    [RetentionDate]            DATETIME        NULL,
    [AdvanceAccountID]         NVARCHAR (64)   NULL,
    [AdvanceItemID]            NVARCHAR (15)   NULL,
    [AdvanceDescription]       NVARCHAR (255)  NULL,
    [AdvanceAmount]            MONEY           NULL,
    [AdvanceBilled]            MONEY           NULL,
    [AdvanceApplied]           MONEY           NULL,
    [CurrencyID]               NVARCHAR (15)   NULL,
    [ProjectAmount]            MONEY           NULL,
    [ProjectBudget]            MONEY           NULL,
    [CompletedPercent]         DECIMAL (5, 2)  NULL,
    [Reference]                NVARCHAR (255)  NULL,
    [Note]                     NVARCHAR (4000) NULL,
    [Inactive]                 BIT             NULL,
    [MiscellaneousAmount]      DECIMAL (18, 5) NULL,
    [MiscellaneousVariance]    DECIMAL (18, 5) NULL,
    [LaborAmount]              DECIMAL (18, 5) NULL,
    [LaborVariance]            DECIMAL (18, 5) NULL,
    [OverHeadAmount]           DECIMAL (18, 5) NULL,
    [OverHeadVariance]         DECIMAL (18, 5) NULL,
    [Profit]                   DECIMAL (18, 5) NULL,
    [ApprovalStatus]           TINYINT         NULL,
    [VerificationStatus]       TINYINT         NULL,
    [DateCreated]              DATETIME        NULL,
    [DateUpdated]              DATETIME        NULL,
    [CreatedBy]                NVARCHAR (15)   NULL,
    [UpdatedBy]                NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED ([JobID] ASC)
);

GO
CREATE TABLE [dbo].[Job_BOM] (
    [JobBOMID]           NVARCHAR (15)  NOT NULL,
    [BOMName]            NVARCHAR (64)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [IsInactive]         BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_BOM] PRIMARY KEY CLUSTERED ([JobBOMID] ASC)
);

GO
CREATE TABLE [dbo].[Job_BOM_Detail] (
    [JobBOMID]    NVARCHAR (15)   NOT NULL,
    [ProductID]   NVARCHAR (64)   NOT NULL,
    [RowIndex]    INT             NOT NULL,
    [Quantity]    DECIMAL (18, 5) NULL,
    [Cost]        DECIMAL (18, 5) NULL,
    [LabourCost]  DECIMAL (18, 5) NULL,
    [Description] NVARCHAR (255)  NULL,
    [UnitID]      NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Job_Budget] (
    [JobID]          NVARCHAR (50)   NOT NULL,
    [CostCategoryID] NVARCHAR (30)   NOT NULL,
    [RowIndex]       INT             NULL,
    [Description]    NVARCHAR (255)  NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [UnitCost]       DECIMAL (18, 5) NULL,
    [TotalCost]      MONEY           NULL,
    [CreatedBy]      NVARCHAR (15)   NULL,
    [UpdatedBy]      NVARCHAR (15)   NULL,
    [DateCreated]    DATETIME        NULL,
    [DateUpdated]    DATETIME        NULL,
    CONSTRAINT [PK_Project_Budget] PRIMARY KEY CLUSTERED ([JobID] ASC, [CostCategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Cost_Category] (
    [CostCategoryID]       NVARCHAR (30) NULL,
    [CostCategoryName]     NVARCHAR (64) NULL,
    [Description]          NVARCHAR (64) NULL,
    [CostTypeID]           TINYINT       NULL,
    [ParentCostCategoryID] NVARCHAR (30) NULL,
    [AccountID]            NVARCHAR (64) NULL,
    [Inactive]             BIT           NULL,
    [DateCreated]          DATETIME      NULL,
    [DateUpdated]          DATETIME      NULL,
    [CreatedBy]            NVARCHAR (15) NULL,
    [UpdatedBy]            NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Job_Equipment] (
    [JobID]       NVARCHAR (50) NOT NULL,
    [EquipmentID] NVARCHAR (15) NOT NULL,
    [Description] NVARCHAR (64) NULL,
    [RowIndex]    TINYINT       NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    CONSTRAINT [PK_Job_Equipment] PRIMARY KEY CLUSTERED ([JobID] ASC, [EquipmentID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Estimation] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [TransactionDate]    DATETIME        NULL,
    [MaterialOHP]        DECIMAL (15, 2) NULL,
    [LabourOHP]          DECIMAL (15, 2) NULL,
    [OtherOHP]           DECIMAL (15, 2) NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [LabelC1]            NVARCHAR (30)   NULL,
    [LabelC2]            NVARCHAR (30)   NULL,
    [LabelC3]            NVARCHAR (30)   NULL,
    [LabelC4]            NVARCHAR (30)   NULL,
    [LabelC5]            NVARCHAR (30)   NULL,
    [LabelC6]            NVARCHAR (30)   NULL,
    [LastRevisedDate]    DATETIME        NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [IsVoid]             BIT             NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Job_Estimation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Estimation_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [PhaseID]        NVARCHAR (15)   NULL,
    [BOQID]          NVARCHAR (15)   NULL,
    [BOQQuantity]    DECIMAL (18, 5) NULL,
    [RowIndex]       INT             NULL,
    [CostMarkUp]     DECIMAL (15, 2) NULL,
    [MaterialOHP]    DECIMAL (15, 2) NULL,
    [LabourOHP]      DECIMAL (15, 2) NULL,
    [OtherOHP]       DECIMAL (15, 2) NULL,
    [AttributeC1]    DECIMAL (15, 2) NULL,
    [AttributeC2]    DECIMAL (15, 2) NULL,
    [AttributeC3]    DECIMAL (15, 2) NULL,
    [AttributeC4]    DECIMAL (15, 2) NULL,
    [AttributeC5]    DECIMAL (15, 2) NULL,
    [AttributeC6]    DECIMAL (15, 2) NULL,
    [Remarks]        NVARCHAR (255)  NULL,
    [Total]          DECIMAL (18, 5) NULL,
    [RowRelation]    INT             NULL
);

GO
CREATE TABLE [dbo].[Job_Estimation_Detail_History] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [RevisionNo]     INT             NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [PhaseID]        NVARCHAR (15)   NULL,
    [BOQID]          NVARCHAR (15)   NULL,
    [BOQQuantity]    DECIMAL (18, 5) NULL,
    [RowIndex]       INT             NULL,
    [CostMarkUp]     DECIMAL (15, 2) NULL,
    [MaterialOHP]    DECIMAL (15, 2) NULL,
    [LabourOHP]      DECIMAL (15, 2) NULL,
    [OtherOHP]       DECIMAL (15, 2) NULL,
    [AttributeC1]    DECIMAL (15, 2) NULL,
    [AttributeC2]    DECIMAL (15, 2) NULL,
    [AttributeC3]    DECIMAL (15, 2) NULL,
    [AttributeC4]    DECIMAL (15, 2) NULL,
    [AttributeC5]    DECIMAL (15, 2) NULL,
    [AttributeC6]    DECIMAL (15, 2) NULL,
    [Remarks]        NVARCHAR (255)  NULL,
    [Total]          DECIMAL (18, 5) NULL,
    [RowRelation]    INT             NULL
);

GO
CREATE TABLE [dbo].[Job_Estimation_Detail_Item] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [BOQID]          NVARCHAR (15)   NULL,
    [ProductID]      NVARCHAR (64)   NULL,
    [Description]    NVARCHAR (255)  NULL,
    [UnitQuantity]   DECIMAL (18, 5) NULL,
    [UnitCost]       DECIMAL (18, 5) NULL,
    [UnitLabourCost] DECIMAL (18, 5) NULL,
    [ActualCost]     DECIMAL (18, 5) NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [Cost]           DECIMAL (18, 5) NULL,
    [LabourCost]     DECIMAL (18, 5) NULL,
    [MaterialTotal]  DECIMAL (18, 5) NULL,
    [LabourTotal]    DECIMAL (18, 5) NULL,
    [MaterialOH]     DECIMAL (18, 5) NULL,
    [LabourOH]       DECIMAL (18, 5) NULL,
    [OtherOH]        DECIMAL (18, 5) NULL,
    [NetTotal]       DECIMAL (18, 5) NULL,
    [RowIndex]       INT             NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [RowRelation]    INT             NULL,
    [Remarks]        VARCHAR (100)   NULL
);

GO
CREATE TABLE [dbo].[Job_Estimation_Detail_Item_History] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [RevisionNo]     INT             NULL,
    [BOQID]          NVARCHAR (15)   NULL,
    [ProductID]      NVARCHAR (64)   NULL,
    [Description]    NVARCHAR (255)  NULL,
    [UnitQuantity]   DECIMAL (18, 5) NULL,
    [UnitCost]       DECIMAL (18, 5) NULL,
    [UnitLabourCost] DECIMAL (18, 5) NULL,
    [ActualCost]     DECIMAL (18, 5) NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [Cost]           DECIMAL (18, 5) NULL,
    [LabourCost]     DECIMAL (18, 5) NULL,
    [MaterialTotal]  DECIMAL (18, 5) NULL,
    [LabourTotal]    DECIMAL (18, 5) NULL,
    [MaterialOH]     DECIMAL (18, 5) NULL,
    [LabourOH]       DECIMAL (18, 5) NULL,
    [OtherOH]        DECIMAL (18, 5) NULL,
    [NetTotal]       DECIMAL (18, 5) NULL,
    [RowIndex]       INT             NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [RowRelation]    INT             NULL,
    [Remarks]        VARCHAR (100)   NULL
);

GO
CREATE TABLE [dbo].[Job_Estimation_History] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [RevisionNo]      INT             NULL,
    [TransactionDate] DATETIME        NULL,
    [MaterialOHP]     DECIMAL (15, 2) NULL,
    [LabourOHP]       DECIMAL (15, 2) NULL,
    [OtherOHP]        DECIMAL (15, 2) NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Reference]       NVARCHAR (20)   NULL,
    [Note]            NVARCHAR (255)  NULL,
    [LabelC1]         NVARCHAR (30)   NULL,
    [LabelC2]         NVARCHAR (30)   NULL,
    [LabelC3]         NVARCHAR (30)   NULL,
    [LabelC4]         NVARCHAR (30)   NULL,
    [LabelC5]         NVARCHAR (30)   NULL,
    [LabelC6]         NVARCHAR (30)   NULL,
    [LastRevisedDate] DATETIME        NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Job_Expense_Issue] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [RequestedBy]        NVARCHAR (30)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Expense_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Expense_Issue_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [ExpenseID]      NVARCHAR (15)   NULL,
    [JobID]          NVARCHAR (50)   NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [EmployeeID]     NVARCHAR (64)   NULL,
    [Description]    NVARCHAR (255)  NULL,
    [Remarks]        NVARCHAR (255)  NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [UnitPrice]      DECIMAL (18, 5) NULL,
    [Amount]         MONEY           NULL,
    [RowIndex]       INT             NULL,
    [IsBillable]     BIT             NULL,
    [IsBilled]       BIT             NULL,
    [BilledAmount]   MONEY           NULL
);

GO
CREATE TABLE [dbo].[Job_Fee] (
    [FeeID]       NVARCHAR (50)  NOT NULL,
    [FeeName]     NVARCHAR (64)  NULL,
    [FeeType]     TINYINT        NOT NULL,
    [FeeAmount]   MONEY          NULL,
    [StartDate]   DATETIME       NULL,
    [EndDate]     DATETIME       NULL,
    [Inactive]    BIT            NULL,
    [TaxOption]   TINYINT        NULL,
    [TaxGroupID]  NVARCHAR (15)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Fee] PRIMARY KEY CLUSTERED ([FeeID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Fee_Detail] (
    [JobID]       NVARCHAR (50)   NOT NULL,
    [FeeID]       NVARCHAR (15)   NOT NULL,
    [Description] NVARCHAR (64)   NULL,
    [UnitID]      NVARCHAR (15)   NULL,
    [UnitPrice]   DECIMAL (18, 5) NULL,
    [Quantity]    DECIMAL (18, 5) NULL,
    [Amount]      MONEY           NULL,
    [RowIndex]    TINYINT         NULL,
    [CreatedBy]   NVARCHAR (15)   NULL,
    [UpdatedBy]   NVARCHAR (15)   NULL,
    [DateCreated] DATETIME        NULL,
    [DateUpdated] DATETIME        NULL,
    [CustomerID]  NVARCHAR (64)   NULL,
    CONSTRAINT [PK_Job_Fee_Detail] PRIMARY KEY CLUSTERED ([JobID] ASC, [FeeID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Fee_Payment_Term] (
    [JobID]               NVARCHAR (50)  NULL,
    [FeeID]               NVARCHAR (15)  NOT NULL,
    [Description]         NVARCHAR (255) NULL,
    [DueDate]             DATETIME       NULL,
    [CompletedPercentage] DECIMAL (5, 2) NULL,
    [Amount]              MONEY          NULL,
    [AmountPercent]       DECIMAL (5, 2) NULL,
    [TermType]            TINYINT        NULL,
    [RowIndex]            TINYINT        NULL
);

GO
CREATE TABLE [dbo].[Job_Fee_Schedule] (
    [ProjectID] NVARCHAR (15) NOT NULL,
    [RowIndex]  INT           NOT NULL,
    [DueDate]   DATETIME      NULL,
    [Amount]    MONEY         NULL,
    CONSTRAINT [PK_Project_Fee_Schedule] PRIMARY KEY CLUSTERED ([ProjectID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Job_Inventory_Issue] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [Reference2]         NVARCHAR (20)  NULL,
    [RequestedBy]        NVARCHAR (30)  NULL,
    [RequireUpdate]      BIT            NULL,
    [SourceSysDocType]   TINYINT        NULL,
    [SourceSysDocID]     NVARCHAR (7)   NULL,
    [SourceVoucherID]    NVARCHAR (15)  NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Inventory_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Inventory_Issue_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [Factor]          DECIMAL (18, 5) NULL,
    [FactorType]      CHAR (1)        NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [RowIndex]        INT             NULL,
    [IsBillable]      BIT             NULL,
    [IsBilled]        BIT             NULL,
    [BilledAmount]    MONEY           NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (6)    NULL,
    [SourceRowIndex]  INT             NULL
);

GO
CREATE TABLE [dbo].[Job_Inventory_Return] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [RequestedBy]        NVARCHAR (30)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Inventory_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Inventory_Return_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [ProductID]      NVARCHAR (64)   NULL,
    [JobID]          NVARCHAR (50)   NULL,
    [LocationID]     NVARCHAR (15)   NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [Description]    NVARCHAR (255)  NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [UnitQuantity]   DECIMAL (18, 5) NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [Factor]         DECIMAL (18, 5) NULL,
    [FactorType]     CHAR (1)        NULL,
    [Cost]           DECIMAL (18, 5) NULL,
    [RowIndex]       INT             NULL,
    [BillType]       TINYINT         NULL,
    [IsBilled]       BIT             NULL,
    [BilledAmount]   MONEY           NULL
);

GO
CREATE TABLE [dbo].[Job_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [DueDate]            DATETIME        NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Job_Invoice_Status] DEFAULT ((1)) NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [TaxOption]          TINYINT         NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [RetentionAmount]    MONEY           NULL,
    [RetentionAmountFC]  MONEY           NULL,
    [RetentionPercent]   DECIMAL (5, 2)  NULL,
    [AdvanceAmount]      MONEY           NULL,
    [AdvanceAmountFC]    MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [SubTotal]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (30)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_Job_Invoice_IsDelivered] DEFAULT ((0)) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [Note]               NVARCHAR (255)  NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [RequireUpdate]      BIT             NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Job_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Invoice_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ItemType]        TINYINT         NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [FeeID]           NVARCHAR (50)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [COGS]            MONEY           NULL,
    [IsRecost]        BIT             NULL,
    [RowSource]       TINYINT         NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [Cost]            MONEY           NULL,
    [CostFC]          MONEY           NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Remarks]         NVARCHAR (255)  NULL
);

GO
CREATE TABLE [dbo].[Job_Invoice_SO_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [FeeID]           NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (255)  NULL,
    [Discount]        MONEY           NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [RowIndex]        INT             NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Job_Maintenance_Schedule] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Reference]       NVARCHAR (30)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [JobID]           NVARCHAR (50)  NULL,
    [StartDate]       DATETIME       NULL,
    [EndDate]         DATETIME       NULL,
    [Reference2]      NVARCHAR (30)  NULL,
    [Description]     NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Maintenance_Schedule] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Maintenance_Schedule_Detail] (
    [SysDocID]         NVARCHAR (7)  NOT NULL,
    [VoucherID]        NVARCHAR (15) NOT NULL,
    [AssetID]          NVARCHAR (64) NULL,
    [ActivityID]       NVARCHAR (15) NULL,
    [StartDate]        DATETIME      NULL,
    [EndDate]          DATETIME      NULL,
    [ScheduledOn]      NVARCHAR (15) NULL,
    [NextScheduleDate] DATETIME      NULL,
    [RowIndex]         INT           NULL
);

GO
CREATE TABLE [dbo].[Job_Maintenance_Service] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [Reference]          NVARCHAR (30)  NULL,
    [LocationID]         NVARCHAR (15)  NULL,
    [JobID]              NVARCHAR (50)  NULL,
    [Reference2]         NVARCHAR (30)  NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Maintenance_Service] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Maintenance_Service_Detail] (
    [SysDocID]          NVARCHAR (7)   NOT NULL,
    [VoucherID]         NVARCHAR (15)  NOT NULL,
    [AssetID]           NVARCHAR (64)  NULL,
    [StartDate]         DATETIME       NULL,
    [EndDate]           DATETIME       NULL,
    [ScheduleDate]      DATETIME       NULL,
    [NextScheduleDate]  DATETIME       NULL,
    [ScheduleSysDocID]  NVARCHAR (7)   NULL,
    [ScheduleVoucherID] NVARCHAR (15)  NULL,
    [ScheduleRowIndex]  INT            NULL,
    [RowIndex]          INT            NULL,
    [Remarks]           NVARCHAR (255) NULL
);

GO
CREATE TABLE [dbo].[Job_Man_Hrs_Budgeting] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Reference]       NVARCHAR (30)  NULL,
    [CostCategoryID]  NVARCHAR (30)  NULL,
    [JobID]           NVARCHAR (50)  NULL,
    [RequestedBy]     NVARCHAR (30)  NULL,
    [RequireUpdate]   BIT            NULL,
    [Description]     NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Man_Hrs_Budgeting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Man_Hrs_Budgeting_Detail] (
    [SysDocID]      NVARCHAR (7)   NULL,
    [VoucherID]     NVARCHAR (15)  NULL,
    [EmpPositionID] NVARCHAR (15)  NULL,
    [EmployeeID]    NVARCHAR (64)  NULL,
    [RequiredHrs]   NUMERIC (5, 2) NULL,
    [FromDate]      DATETIME       NULL,
    [ToDate]        DATETIME       NULL,
    [Amount]        MONEY          NULL,
    [Variance]      NUMERIC (5, 2) NULL,
    [RowIndex]      INT            NULL
);

GO
CREATE TABLE [dbo].[Job_Material_Estimate] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [Reference]       NVARCHAR (30)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [JobID]           NVARCHAR (50)  NULL,
    [RequestedBy]     NVARCHAR (30)  NULL,
    [RequireUpdate]   BIT            NULL,
    [Description]     NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Job_Material_Estimate] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Material_Estimate_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [ProductID]      NVARCHAR (64)   NULL,
    [Description]    NVARCHAR (255)  NULL,
    [Quantity]       DECIMAL (18, 5) NULL,
    [UnitQuantity]   DECIMAL (18, 5) NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [Factor]         DECIMAL (18, 5) NULL,
    [FactorType]     CHAR (1)        NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [Cost]           DECIMAL (18, 5) NULL,
    [Amount]         MONEY           NULL,
    [UnitPrice]      DECIMAL (18, 5) NULL,
    [RequiredOn]     DATETIME        NULL,
    [RowIndex]       INT             NULL,
    [IsBillable]     BIT             NULL,
    [IsBilled]       BIT             NULL,
    [BilledAmount]   MONEY           NULL,
    [ItemType]       TINYINT         NULL,
    [Variance]       REAL            NULL
);

GO
CREATE TABLE [dbo].[Job_Material_Requisition] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [TransactionDate]    DATETIME        NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [Reference2]         NVARCHAR (30)   NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Job_Material_Requisition_Status] DEFAULT ((1)) NULL,
    [RequestedBy]        NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [ReqonDate]          DATETIME        NULL,
    [RequireUpdate]      BIT             NULL,
    [Description]        NVARCHAR (4000) NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Job_Material_Requisition] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Material_Requisition_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [OnHand]          DECIMAL (18, 5) NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL,
    [Issued]          DECIMAL (18, 5) NULL,
    [Factor]          DECIMAL (18, 5) NULL,
    [FactorType]      CHAR (1)        NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [Remarks]         NVARCHAR (255)  NULL,
    [RowIndex]        INT             NULL,
    [IsBillable]      BIT             NULL,
    [IsBilled]        BIT             NULL,
    [BilledAmount]    MONEY           NULL,
    [ItemType]        TINYINT         NULL
);

GO
CREATE TABLE [dbo].[Job_Task] (
    [TaskID]               NVARCHAR (15)  NOT NULL,
    [JobID]                NVARCHAR (50)  NULL,
    [FeeID]                NVARCHAR (15)  NOT NULL,
    [Description]          NVARCHAR (255) NOT NULL,
    [StartDate]            DATETIME       NULL,
    [EndDate]              DATETIME       NULL,
    [ActualStartDate]      DATETIME       NULL,
    [ActualEndDate]        DATETIME       NULL,
    [AssignedToID]         NVARCHAR (64)  NULL,
    [FeePercentage]        DECIMAL (5, 2) NULL,
    [CompletedPercentage]  DECIMAL (5, 2) NULL,
    [Status]               TINYINT        NULL,
    [CompletedDescription] NVARCHAR (255) NULL,
    [TotalHours]           DECIMAL (7, 2) NULL,
    [Note]                 NTEXT          NULL,
    [CreatedBy]            NVARCHAR (15)  NULL,
    [UpdatedBy]            NVARCHAR (15)  NULL,
    [DateCreated]          DATETIME       NULL,
    [DateUpdated]          DATETIME       NULL,
    CONSTRAINT [PK_Job_Task_1] PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Timesheet] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TransactionDate]    DATETIME       NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [RequestedBy]        NVARCHAR (30)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [EmployeeID]         NVARCHAR (15)  NULL,
    [Month]              TINYINT        NULL,
    [Year]               INT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Job_Timesheet_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [FeeID]           NVARCHAR (15)   NULL,
    [TaskID]          NVARCHAR (15)   NULL,
    [TaskPercent]     DECIMAL (5, 2)  NULL,
    [Description]     NVARCHAR (255)  NULL,
    [TSDate]          DATETIME        NULL,
    [PayrollItemType] NVARCHAR (15)   NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [Unit]            NVARCHAR (15)   NULL,
    [Rate]            DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [BeginTime]       DATETIME        NULL,
    [EndTime]         DATETIME        NULL,
    [RowIndex]        INT             NULL,
    [IsBillable]      BIT             NULL,
    [IsBilled]        BIT             NULL,
    [BilledAmount]    MONEY           NULL
);

GO
CREATE TABLE [dbo].[Job_Type] (
    [JobTypeID]   NVARCHAR (15) NOT NULL,
    [JobTypeName] NVARCHAR (64) NOT NULL,
    [Description] NVARCHAR (64) NULL,
    [AMCEnabled]  BIT           NULL,
    [Inactive]    BIT           NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (64) NULL,
    [UpdatedBy]   NVARCHAR (64) NULL,
    CONSTRAINT [PK_Job_Type] PRIMARY KEY CLUSTERED ([JobTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Job_Vehicle_Detail] (
    [JobID]              NVARCHAR (50)   NOT NULL,
    [VehicleID]          NVARCHAR (15)   NOT NULL,
    [Odometer]           NUMERIC (15, 2) NULL,
    [RegistrationNumber] NVARCHAR (15)   NULL,
    [Color]              NVARCHAR (15)   NULL,
    [Model]              NVARCHAR (15)   NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Job_Vehicle_Detail] PRIMARY KEY CLUSTERED ([JobID] ASC)
);

GO
CREATE TABLE [dbo].[Journal] (
    [JournalID]          INT             NOT NULL,
    [JournalDate]        DATETIME        NOT NULL,
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [SysDocType]         INT             NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [Reference2]         NVARCHAR (30)   NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [Narration]          NVARCHAR (255)  NULL,
    [Note]               NVARCHAR (255)  NULL,
    [StoreID]            INT             NULL,
    [IsVoid]             BIT             NULL,
    [STJournalID]        NVARCHAR (15)   NULL,
    [STJYear]            SMALLINT        NULL,
    [STJMonth]           TINYINT         NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Journal] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC),
    CONSTRAINT [FK_GL Transactions_Currencies] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([CurrencyID]),
    CONSTRAINT [UQ__Journal__25010387E4E8CD31] UNIQUE NONCLUSTERED ([JournalID] ASC)
);

GO
CREATE TABLE [dbo].[Journal_Details] (
    [JournalDetailID]    INT             IDENTITY (1, 1) NOT NULL,
    [JournalID]          INT             NOT NULL,
    [SysDocID]           NVARCHAR (7)    NULL,
    [VoucherID]          NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [AccountID]          NVARCHAR (64)   NOT NULL,
    [JDDate]             DATETIME        NULL,
    [DocType]            INT             NULL,
    [Description]        NVARCHAR (255)  NULL,
    [Debit]              MONEY           NULL,
    [Credit]             MONEY           NULL,
    [DebitFC]            MONEY           NULL,
    [CreditFC]           MONEY           NULL,
    [RowIndex]           SMALLINT        NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [Reference]          NVARCHAR (60)   NULL,
    [PayeeID]            NVARCHAR (64)   NULL,
    [PayeeType]          NVARCHAR (1)    NULL,
    [AnalysisID]         NVARCHAR (50)   NULL,
    [CostCenterID]       NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [BankID]             NVARCHAR (15)   NULL,
    [CheckbookID]        NVARCHAR (15)   NULL,
    [CheckID]            INT             NULL,
    [CheckDate]          DATETIME        NULL,
    [CheckNumber]        NVARCHAR (15)   NULL,
    [IsARAP]             BIT             NULL,
    [AllocationID]       INT             NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [ConsignID]          NVARCHAR (22)   NULL,
    [ConsignExpenseID]   NVARCHAR (15)   NULL,
    [IsReconciled]       BIT             NULL,
    [ReconcileDate]      DATETIME        NULL,
    [ReconciledBy]       NVARCHAR (15)   NULL,
    [IsBilled]           BIT             NULL,
    [AttributeID1]       NVARCHAR (50)   NULL,
    [AttributeID2]       NVARCHAR (50)   NULL,
    [JVEntryType]        TINYINT         NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    CONSTRAINT [PK_Journal_Details] PRIMARY KEY CLUSTERED ([JournalDetailID] ASC),
    CONSTRAINT [FK_Journal_Details_Account] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID])
);

GO
CREATE TABLE [dbo].[Lawyer] (
    [LawyerID]      NVARCHAR (15)  NOT NULL,
    [LawyerName]    NVARCHAR (64)  NOT NULL,
    [SelectionType] CHAR (10)      NULL,
    [PartyID]       NVARCHAR (15)  NULL,
    [IsInactive]    BIT            NULL,
    [Note]          NVARCHAR (255) NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    CONSTRAINT [PK_Lawyer] PRIMARY KEY CLUSTERED ([LawyerID] ASC)
);

GO
CREATE TABLE [dbo].[Lead] (
    [LeadID]             NVARCHAR (64)   NOT NULL,
    [LeadName]           NVARCHAR (64)   NULL,
    [PrimaryAddressID]   NVARCHAR (15)   NULL,
    [ShortName]          NVARCHAR (64)   NULL,
    [ForeignName]        NVARCHAR (64)   NULL,
    [CompanyName]        NVARCHAR (64)   NULL,
    [ContactName]        NVARCHAR (64)   NULL,
    [LeadSourceID]       NVARCHAR (15)   NULL,
    [LeadOwnerID]        NVARCHAR (15)   NULL,
    [IndustryID]         NVARCHAR (15)   NULL,
    [LeadStatus]         NVARCHAR (30)   NULL,
    [SourceLeadID]       NVARCHAR (15)   NULL,
    [CompanySize]        TINYINT         NULL,
    [EmailOptOut]        BIT             NULL,
    [AnnualTurnOver]     MONEY           NULL,
    [Rating]             INT             NULL,
    [EmployeeCount]      INT             NULL,
    [CountryID]          NVARCHAR (15)   NULL,
    [AreaID]             NVARCHAR (15)   NULL,
    [StageID]            NVARCHAR (15)   NULL,
    [SubAreaID]          NVARCHAR (15)   NULL,
    [DateEstablished]    DATETIME        NULL,
    [IsLeadSince]        DATETIME        NULL,
    [ReferredBy]         NVARCHAR (64)   NULL,
    [IsInactive]         BIT             NULL,
    [ExpectValue]        DECIMAL (15, 2) NULL,
    [Photo]              IMAGE           NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [ReasonID]           NVARCHAR (15)   NULL,
    [Remarks]            NVARCHAR (255)  NULL,
    [ProfileDetails]     NTEXT           NULL,
    [Note]               NTEXT           NULL,
    [SalesPersonID]      NVARCHAR (64)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED ([LeadID] ASC)
);

GO
CREATE TABLE [dbo].[Lead_Activity_Detail] (
    [LeadID]     NVARCHAR (64)  NOT NULL,
    [ActivityID] NVARCHAR (64)  NOT NULL,
    [Note]       NVARCHAR (255) NULL,
    [RowIndex]   SMALLINT       NULL
);

GO
CREATE TABLE [dbo].[Lead_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [LeadID]             NVARCHAR (64)  NOT NULL,
    [Suffix]             TINYINT        NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Gender]             TINYINT        NULL,
    [Country]            NVARCHAR (30)  NULL,
    [BirthDay]           DATETIME       NULL,
    [Children]           NVARCHAR (255) NULL,
    [Hobbies]            NVARCHAR (64)  NULL,
    [Language]           NVARCHAR (30)  NULL,
    [SpouseName]         NVARCHAR (64)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Twitter]            NVARCHAR (30)  NULL,
    [Facebook]           NVARCHAR (255) NULL,
    [Skype]              NVARCHAR (30)  NULL,
    [LinkedIn]           NVARCHAR (64)  NULL,
    [Comment]            NVARCHAR (255) NULL,
    [Inactive]           BIT            NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    [Latitude]           NVARCHAR (30)  NULL,
    [Longitude]          NVARCHAR (30)  NULL,
    CONSTRAINT [PK_Lead_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [LeadID] ASC)
);

GO
CREATE TABLE [dbo].[Lead_Contact_Detail] (
    [LeadID]    NVARCHAR (64)  NOT NULL,
    [ContactID] NVARCHAR (64)  NOT NULL,
    [JobTitle]  NVARCHAR (30)  NULL,
    [Note]      NVARCHAR (255) NULL,
    [RowIndex]  SMALLINT       NULL
);

GO
CREATE TABLE [dbo].[Lead_Followup_Details] (
    [FollowupID]           NVARCHAR (64)  NOT NULL,
    [LeadID]               NCHAR (10)     NULL,
    [CRMType]              INT            NULL,
    [SourceVoucherID]      NVARCHAR (15)  NULL,
    [SourceSysDocID]       NVARCHAR (7)   NULL,
    [ThisFollowupDate]     DATETIME       NULL,
    [NextFollowupDate]     DATETIME       NULL,
    [ThisFollowupByID]     NVARCHAR (64)  NULL,
    [NextFollowupByID]     NVARCHAR (64)  NULL,
    [ThisFollowupStatusID] NVARCHAR (15)  NULL,
    [Remark]               NVARCHAR (255) NULL,
    [CreatedBy]            NVARCHAR (15)  NULL,
    [DateCreated]          DATETIME       NULL,
    [UpdatedBy]            NVARCHAR (15)  NULL,
    [DateUpdated]          DATETIME       NULL,
    CONSTRAINT [PK_Lead_Followup_Details] PRIMARY KEY CLUSTERED ([FollowupID] ASC)
);

GO
CREATE TABLE [dbo].[Lead_Source] (
    [LeadSourceID]   NVARCHAR (15) NOT NULL,
    [LeadSourceName] NVARCHAR (30) NULL,
    [Inactive]       BIT           NULL,
    [CreatedBy]      NVARCHAR (15) NULL,
    [UpdatedBy]      NVARCHAR (15) NULL,
    [DateCreated]    DATETIME      NULL,
    [DateUpdated]    DATETIME      NULL,
    CONSTRAINT [PK_Lead_Source] PRIMARY KEY CLUSTERED ([LeadSourceID] ASC)
);

GO
CREATE TABLE [dbo].[Lead_Status] (
    [LeadStatusID] NVARCHAR (30)  NOT NULL,
    [Name]         NVARCHAR (150) NULL,
    [IsInactive]   BIT            NULL,
    [Note]         NVARCHAR (50)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Lead_Status] PRIMARY KEY CLUSTERED ([LeadStatusID] ASC)
);

GO
CREATE TABLE [dbo].[Leave_Type] (
    [LeaveTypeID]         NVARCHAR (15)   NOT NULL,
    [LeaveTypeName]       NVARCHAR (30)   NOT NULL,
    [Days]                SMALLINT        CONSTRAINT [DF_Leave_Type_Days] DEFAULT ((0)) NULL,
    [IsPayable]           BIT             NULL,
    [IsCumulative]        BIT             NULL,
    [IsAnnual]            BIT             NULL,
    [ActivateHC]          BIT             NULL,
    [DeductionProportion] FLOAT (53)      NULL,
    [AccountID]           NVARCHAR (64)   NULL,
    [MonthGreater1]       INT             NULL,
    [MonthLesser1]        INT             NULL,
    [AllowedDays1]        DECIMAL (15, 2) NULL,
    [MonthGreater2]       INT             NULL,
    [MonthLesser2]        INT             NULL,
    [AllowedDays2]        DECIMAL (15, 2) NULL,
    [MonthGreater3]       REAL            NULL,
    [MonthLesser3]        REAL            NULL,
    [AllowedDays3]        DECIMAL (15, 2) NULL,
    [IsEncash]            BIT             NULL,
    [IsLeaveSettle]       BIT             NULL,
    [Inactive]            BIT             NULL,
    [DateCreated]         DATETIME        NULL,
    [DateUpdated]         DATETIME        NULL,
    [CreatedBy]           NVARCHAR (64)   NULL,
    [UpdatedBy]           NVARCHAR (64)   NULL,
    CONSTRAINT [PK_Leave_Type] PRIMARY KEY CLUSTERED ([LeaveTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Legal_Action_Status] (
    [LegalActionStatusID]   NVARCHAR (30)  NOT NULL,
    [LegalActionStatusName] NVARCHAR (64)  NOT NULL,
    [IsInactive]            BIT            NULL,
    [IsFinalized]           BIT            NULL,
    [Note]                  NVARCHAR (255) NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Legal_Action_Status] PRIMARY KEY CLUSTERED ([LegalActionStatusID] ASC)
);

GO
CREATE TABLE [dbo].[Legal_Actions] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ActionName]      NVARCHAR (64)   NOT NULL,
    [ActionDateTime]  DATETIME        NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [ParentSysDocID]  NVARCHAR (7)    NULL,
    [ParentVoucherID] NVARCHAR (15)   NULL,
    [Amount]          DECIMAL (18, 5) NULL,
    [FileNo]          NVARCHAR (50)   NULL,
    [CaseClient1]     NVARCHAR (15)   NULL,
    [CaseClient2]     NVARCHAR (15)   NULL,
    [CasePartyID]     NVARCHAR (64)   NULL,
    [CaseTypeID]      NVARCHAR (15)   NULL,
    [ClientType]      NVARCHAR (15)   NULL,
    [LawyerID]        NVARCHAR (15)   NULL,
    [StatusID]        NVARCHAR (15)   NULL,
    [Note]            NVARCHAR (4000) NULL,
    [AnalysisID]      NVARCHAR (50)   NULL,
    [IsVoid]          BIT             NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Legal_Actions] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Legal_Actions_Client_List] (
    [SysDocID]   NVARCHAR (7)  NOT NULL,
    [VoucherID]  NVARCHAR (15) NOT NULL,
    [ClientType] NVARCHAR (15) NULL,
    [CaseClient] NVARCHAR (15) NULL,
    [RowIndex]   INT           NULL
);

GO
CREATE TABLE [dbo].[Legal_Activity] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ActivityName]     NVARCHAR (64)   NOT NULL,
    [ActivityDateTime] DATETIME        NULL,
    [ActionName]       NVARCHAR (64)   NULL,
    [ParentSysDocID]   NVARCHAR (7)    NULL,
    [ParentVoucherID]  NVARCHAR (15)   NULL,
    [FileNo]           NVARCHAR (50)   NULL,
    [CaseClient1]      NVARCHAR (15)   NULL,
    [CaseClient2]      NVARCHAR (15)   NULL,
    [CasePartyID]      NVARCHAR (64)   NULL,
    [CaseTypeID]       NVARCHAR (15)   NULL,
    [LawyerID]         NVARCHAR (15)   NULL,
    [StatusID]         NVARCHAR (15)   NULL,
    [ContactID]        NVARCHAR (15)   NULL,
    [OwnerID]          NVARCHAR (15)   NULL,
    [ActDateTime]      DATETIME        NULL,
    [Note]             NVARCHAR (4000) NULL,
    [AnalysisID]       NVARCHAR (50)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Legal_Activity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[ListHiddenFields] (
    [FieldID]          NVARCHAR (15) NOT NULL,
    [CustomReportType] TINYINT       NOT NULL,
    [CustomReportID]   NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_ListHiddenFields] PRIMARY KEY CLUSTERED ([FieldID] ASC, [CustomReportType] ASC, [CustomReportID] ASC)
);

GO
CREATE TABLE [dbo].[Loan_Entry] (
    [SysDocID]               NVARCHAR (15)   NOT NULL,
    [VoucherID]              NVARCHAR (15)   NOT NULL,
    [LoanAccountID]          NVARCHAR (64)   NULL,
    [InterestAccountID]      NVARCHAR (64)   NULL,
    [LoanRepaymentAccountID] NVARCHAR (64)   NULL,
    [LoanDate]               DATETIME        NULL,
    [LoanAmount]             MONEY           NULL,
    [InstallmentNumber]      NVARCHAR (15)   NULL,
    [DedStartDate]           DATETIME        NULL,
    [InterestRate]           MONEY           NULL,
    [Description]            NVARCHAR (1000) NULL,
    [Note]                   NVARCHAR (1000) NULL,
    [LoanTermType]           TINYINT         NULL,
    [MonthlyEMI]             MONEY           NULL,
    [IsVoid]                 BIT             NULL,
    [IsClosed]               BIT             NULL,
    [DateCreated]            DATETIME        NULL,
    [DateUpdated]            DATETIME        NULL,
    [CreatedBy]              NVARCHAR (15)   NULL,
    [UpdatedBy]              NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Loan_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Loan_Entry_Detail] (
    [LoanSysDocID]       NVARCHAR (7)  NULL,
    [LoanVoucherID]      NVARCHAR (15) NULL,
    [Installment]        NVARCHAR (15) NULL,
    [TransactionDate]    DATETIME      NULL,
    [InstallmentAmount]  MONEY         NULL,
    [Principle]          MONEY         NULL,
    [Interest]           MONEY         NULL,
    [OutStandingPayment] MONEY         NULL
);

GO
CREATE TABLE [dbo].[Location] (
    [LocationID]                      NVARCHAR (15)   NOT NULL,
    [LocationName]                    NVARCHAR (64)   NOT NULL,
    [LocationType]                    TINYINT         NULL,
    [SalesAccountID]                  NVARCHAR (64)   NULL,
    [COGSAccountID]                   NVARCHAR (64)   NULL,
    [InventoryAccountID]              NVARCHAR (64)   NULL,
    [APAccountID]                     NVARCHAR (64)   NULL,
    [ARAccountID]                     NVARCHAR (64)   NULL,
    [EmployeeAccountID]               NVARCHAR (64)   NULL,
    [SalesTaxAccountID]               NVARCHAR (64)   NULL,
    [PurchaseTaxAccountID]            NVARCHAR (64)   NULL,
    [DiscountGivenAccountID]          NVARCHAR (64)   NULL,
    [DiscountReceivedAccountID]       NVARCHAR (64)   NULL,
    [ExchangeGainLossAccountID]       NVARCHAR (64)   NULL,
    [ProjectWIPAccountID]             NVARCHAR (64)   NULL,
    [ProjectIncomeAccountID]          NVARCHAR (64)   NULL,
    [ProjectCostAccountID]            NVARCHAR (64)   NULL,
    [ProjectTimesheetContraAccountID] NVARCHAR (64)   NULL,
    [ProjectRetentionAccountID]       NVARCHAR (64)   NULL,
    [ProjectAdvanceAccountID]         NVARCHAR (64)   NULL,
    [ManuWIPAccountID]                NVARCHAR (64)   NULL,
    [ManuTimesheetContraAccountID]    NVARCHAR (64)   NULL,
    [ConsignInAccountID]              NVARCHAR (64)   NULL,
    [ConsignInCommissionAccountID]    NVARCHAR (64)   NULL,
    [ConsignInDiffAccountID]          NVARCHAR (64)   NULL,
    [ConsignOutSalesAccountID]        NVARCHAR (64)   NULL,
    [ConsignOutCOGSAccountID]         NVARCHAR (64)   NULL,
    [UnInvoicedInventoryAccountID]    NVARCHAR (64)   NULL,
    [AllocationDiscountAccountID]     NVARCHAR (64)   NULL,
    [RoundOffAccountID]               NVARCHAR (64)   NULL,
    [PurchasePrePaymentAccountID]     NVARCHAR (64)   NULL,
    [PrepaymentAPAccountID]           NVARCHAR (64)   NULL,
    [IsWarehouse]                     BIT             NULL,
    [Note]                            NVARCHAR (255)  NULL,
    [IsConsignOutLocation]            BIT             NULL,
    [IsConsignInLocation]             BIT             NULL,
    [IsSystem]                        BIT             NULL,
    [IsPOSLocation]                   BIT             CONSTRAINT [DF_Location_IsPOSLocation] DEFAULT ((0)) NULL,
    [POSCashAccountID]                NVARCHAR (64)   NULL,
    [POSCardAccountID]                NVARCHAR (64)   NULL,
    [TaxID]                           NVARCHAR (64)   NULL,
    [TaxSalesAccountID]               NVARCHAR (64)   NULL,
    [TaxPercent]                      DECIMAL (18, 2) NULL,
    [TaxPurchaseAccountID]            NVARCHAR (64)   NULL,
    [LocationCurrencyID]              NVARCHAR (5)    NULL,
    [DefaultCustomerID]               NVARCHAR (64)   NULL,
    [DefaultRegisterID]               NVARCHAR (15)   NULL,
    [LeaveExpenseAccountID]           NVARCHAR (64)   NULL,
    [EOSBenefitAccountID]             NVARCHAR (64)   NULL,
    [ProvisionAccountID]              NVARCHAR (64)   NULL,
    [TicketAccountID]                 NVARCHAR (64)   NULL,
    [Inactive]                        BIT             CONSTRAINT [DF_Location_Inactive] DEFAULT ((0)) NULL,
    [AreaID]                          NVARCHAR (15)   NULL,
    [CountryID]                       NVARCHAR (15)   NULL,
    [DateCreated]                     DATETIME        NULL,
    [DateUpdated]                     DATETIME        NULL,
    [CreatedBy]                       NVARCHAR (15)   NULL,
    [UpdatedBy]                       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

GO
CREATE TABLE [dbo].[LocationAccounts_Tax_Detail] (
    [LocationID]        NVARCHAR (15)   NOT NULL,
    [TaxID]             NVARCHAR (64)   NULL,
    [SalesAccountID]    NVARCHAR (64)   NULL,
    [PurchaseAccountID] NVARCHAR (64)   NULL,
    [TaxPercent]        DECIMAL (18, 2) NULL,
    [RowIndex]          INT             NULL
);

GO
CREATE TABLE [dbo].[LPO_Receipt] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [SysDocType]         INT             NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [Total]              MONEY           NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [IsVoid]             BIT             NULL,
    [IsReceived]         BIT             CONSTRAINT [DF_Table_1_IsDelivered] DEFAULT ((0)) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_LPO_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[LPO_Receipt_Details] (
    [SysDocID]    NVARCHAR (7)   NOT NULL,
    [VoucherID]   NVARCHAR (15)  NOT NULL,
    [PayeeID]     NVARCHAR (64)  NULL,
    [PayeeType]   NVARCHAR (1)   NULL,
    [Description] NVARCHAR (255) NULL,
    [LPONumber]   NVARCHAR (30)  NULL,
    [LPODate]     DATETIME       NULL,
    [Amount]      MONEY          NULL,
    [AmountFC]    MONEY          NULL,
    [RowIndex]    SMALLINT       NULL,
    [Reference]   NVARCHAR (20)  NULL,
    [IsVoid]      BIT            NULL
);

GO
CREATE TABLE [dbo].[Material_Reservation] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [CompanyID]          TINYINT        NULL,
    [TransactionDate]    DATETIME       NULL,
    [ValidDateFrom]      DATETIME       NULL,
    [ValidDateTo]        DATETIME       NULL,
    [IsVoid]             BIT            NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [RequireUpdate]      BIT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Material_Reservation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Material_Reservation_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [Description]  NVARCHAR (255)  NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [Factor]       DECIMAL (18, 5) NULL,
    [FactorType]   CHAR (1)        NULL,
    [Cost]         DECIMAL (18, 5) NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Matrix_Attribute_Dimension] (
    [ProductParentID] NVARCHAR (64) NULL,
    [Dimension]       TINYINT       NULL,
    [DimensionID]     NVARCHAR (15) NULL,
    [AttributeID]     NVARCHAR (15) NULL,
    [AttributeName]   NVARCHAR (32) NULL,
    [RowIndex]        INT           NULL
);

GO
CREATE TABLE [dbo].[Matrix_Template] (
    [TemplateID]   NVARCHAR (15)  NOT NULL,
    [TemplateName] NVARCHAR (64)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Matrix_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC)
);

GO
CREATE TABLE [dbo].[Matrix_Template_Detail] (
    [TemplateID]    NVARCHAR (15) NULL,
    [Dimension]     TINYINT       NULL,
    [DimensionID]   NVARCHAR (15) NULL,
    [AttributeID]   NVARCHAR (15) NULL,
    [AttributeName] NVARCHAR (15) NULL,
    [RowIndex]      INT           NULL
);

GO
CREATE TABLE [dbo].[Menu_Security] (
    [MenuID]  NVARCHAR (255) NULL,
    [Enable]  BIT            NULL,
    [Visible] BIT            NULL,
    [UserID]  NVARCHAR (15)  NULL,
    [GroupID] NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Mfg_Production] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (64)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [WorkCompDate]    DATETIME       NULL,
    [JobOrderID]      NVARCHAR (64)  NULL,
    [BOMID]           NVARCHAR (15)  NULL,
    [RouteID]         NVARCHAR (30)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [Reference]       NVARCHAR (64)  NULL,
    [Reference1]      NVARCHAR (64)  NULL,
    [Note]            NVARCHAR (255) NULL,
    [Total]           MONEY          NULL,
    [TotalFC]         MONEY          NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Mfg_Production] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Mfg_Production_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [RowIndex]       INT             NOT NULL,
    [ProductID]      NVARCHAR (64)   NULL,
    [LocationID]     NVARCHAR (15)   NULL,
    [QuantityBuild]  DECIMAL (18, 5) NULL,
    [Cost]           DECIMAL (18, 5) NULL,
    [CostFC]         DECIMAL (18, 5) NULL,
    [Description]    NVARCHAR (255)  NULL,
    [UnitID]         NVARCHAR (15)   NULL,
    [UnitQuantity]   DECIMAL (18, 5) NULL,
    [UnitFactor]     DECIMAL (18, 5) NULL,
    [FactorType]     NVARCHAR (1)    NULL,
    [CostAllocation] DECIMAL (18, 5) NULL,
    [Total]          DECIMAL (18, 5) NULL,
    [TotalFC]        DECIMAL (18, 5) NULL,
    [NextRoute]      NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Mfg_Production_Expense] (
    [SysDocID]    NVARCHAR (7)  NULL,
    [VoucherID]   NVARCHAR (15) NULL,
    [ExpenseID]   NVARCHAR (15) NULL,
    [Description] NVARCHAR (64) NULL,
    [Amount]      MONEY         NULL,
    [AmountFC]    MONEY         NULL,
    [Reference]   NVARCHAR (15) NULL,
    [RateType]    CHAR (1)      NULL,
    [RowIndex]    INT           NULL
);

GO
CREATE TABLE [dbo].[Mfg_Production_Raw_Material] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RowIndex]     INT             NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [LocationID]   NVARCHAR (15)   NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [UnitPrice]    MONEY           NULL,
    [UnitPriceFC]  MONEY           NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [UnitQuantity] DECIMAL (18, 5) NULL,
    [UnitFactor]   DECIMAL (18, 5) NULL,
    [FactorType]   NVARCHAR (1)    NULL,
    [Description]  NVARCHAR (255)  NULL,
    [Reference]    NVARCHAR (255)  NULL,
    [Total]        MONEY           NULL,
    [TotalFC]      MONEY           NULL
);

GO
CREATE TABLE [dbo].[Mfg_Production_Resource] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [EmployeeID]   NVARCHAR (15)   NULL,
    [EmployeeName] NVARCHAR (64)   NULL,
    [PositionID]   NVARCHAR (15)   NULL,
    [Hour]         DECIMAL (18, 5) NULL,
    [Remarks]      NVARCHAR (255)  NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Mfg_Work_Order] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [Status]          TINYINT        NULL,
    [Description]     NVARCHAR (30)  NULL,
    [Note]            NVARCHAR (255) NULL,
    [TransactionDate] DATETIME       NULL,
    [RequiredDate]    DATETIME       NULL,
    [Reference]       NVARCHAR (20)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Mfg_Work_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Mfg_Work_Order_Detail] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RowIndex]     INT             NOT NULL,
    [ProductID]    NVARCHAR (64)   NULL,
    [Quantity]     DECIMAL (18, 5) NULL,
    [Description]  NVARCHAR (255)  NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [BOMID]        NVARCHAR (15)   NULL,
    [RouteGroupID] NVARCHAR (30)   NULL,
    [Remarks]      NVARCHAR (255)  NULL,
    CONSTRAINT [PK_Mfg_Work_Order_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC)
);

GO
CREATE TABLE [dbo].[Mfg_Work_Order_Expense] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [ExpenseID]    NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (15)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Modify_Transactions] (
    [SysDocID]  NVARCHAR (7)  NULL,
    [VoucherID] NVARCHAR (15) NULL,
    [UserID]    NVARCHAR (64) NULL,
    [IsModify]  BIT           NULL
);

GO
CREATE TABLE [dbo].[Modules] (
    [ModuleID]  INT            IDENTITY (1, 1) NOT NULL,
    [ModuleKey] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([ModuleID] ASC)
);

GO
CREATE TABLE [dbo].[Nationality] (
    [NationalityID]   NVARCHAR (15) NOT NULL,
    [NationalityName] NVARCHAR (30) NULL,
    [DateCreated]     DATETIME      NULL,
    [DateUpdated]     DATETIME      NULL,
    [CreatedBy]       NVARCHAR (15) NULL,
    [UpdatedBy]       NVARCHAR (15) NULL,
    CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED ([NationalityID] ASC)
);

GO
CREATE TABLE [dbo].[Opening_Balance_Batch] (
    [BatchID]             NVARCHAR (15)   NOT NULL,
    [SysDocID]            NVARCHAR (7)    NOT NULL,
    [CompanyID]           TINYINT         NULL,
    [DivisionID]          NVARCHAR (15)   NULL,
    [BatchDate]           DATETIME        NULL,
    [BatchType]           TINYINT         NULL,
    [Reference]           NVARCHAR (30)   NULL,
    [Description]         NVARCHAR (255)  NULL,
    [AccountID]           NVARCHAR (64)   NULL,
    [CurrencyID]          NVARCHAR (5)    NULL,
    [CurrencyRate]        DECIMAL (18, 5) NULL,
    [TransactionSysDocID] NVARCHAR (7)    NULL,
    [LocationID]          NVARCHAR (15)   NULL,
    [DateCreated]         DATETIME        NULL,
    [DateUpdated]         DATETIME        NULL,
    [CreatedBy]           NVARCHAR (15)   NULL,
    [UpdatedBy]           NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Opening_Balance_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC, [SysDocID] ASC)
);

GO
CREATE TABLE [dbo].[Opening_Balance_Batch_Detail] (
    [BatchID]             NVARCHAR (15)   NOT NULL,
    [SysDocID]            NVARCHAR (7)    NULL,
    [TransactionSysDocID] NVARCHAR (7)    NULL,
    [BatchType]           TINYINT         NULL,
    [AccountID]           NVARCHAR (64)   NOT NULL,
    [ProductID]           NVARCHAR (64)   NULL,
    [LocationID]          NVARCHAR (15)   NULL,
    [Quantity]            DECIMAL (18, 5) NULL,
    [Cost]                DECIMAL (18, 5) NULL,
    [PurchaseDate]        DATE            NULL,
    [TransactionDate]     DATETIME        NOT NULL,
    [DueDate]             DATETIME        NULL,
    [InvoiceAmount]       MONEY           NULL,
    [BalanceAmount]       MONEY           NULL,
    [VoucherID]           NVARCHAR (15)   NULL,
    [Description]         NVARCHAR (255)  NULL,
    [Reference]           NVARCHAR (30)   NULL,
    [CurrencyID]          NVARCHAR (5)    NULL,
    [CurrencyRate]        DECIMAL (18, 5) NULL,
    [RowIndex]            INT             NULL
);

GO
CREATE TABLE [dbo].[Opening_Balance_Leave] (
    [BatchID]             NVARCHAR (15)  NOT NULL,
    [SysDocID]            NVARCHAR (7)   NOT NULL,
    [BatchDate]           DATETIME       NULL,
    [BatchType]           TINYINT        NULL,
    [Reference]           NVARCHAR (30)  NULL,
    [Description]         NVARCHAR (255) NULL,
    [TransactionSysDocID] NVARCHAR (7)   NULL,
    [LocationID]          NVARCHAR (15)  NULL,
    [DateCreated]         DATETIME       NULL,
    [DateUpdated]         DATETIME       NULL,
    [CreatedBy]           NVARCHAR (15)  NULL,
    [UpdatedBy]           NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Opening_Balance_Leave] PRIMARY KEY CLUSTERED ([BatchID] ASC, [SysDocID] ASC)
);

GO
CREATE TABLE [dbo].[Opening_Balance_Leave_Detail] (
    [BatchID]             NVARCHAR (15)  NOT NULL,
    [SysDocID]            NVARCHAR (7)   NULL,
    [TransactionSysDocID] NVARCHAR (7)   NULL,
    [BatchType]           TINYINT        NULL,
    [EmployeeID]          NVARCHAR (64)  NULL,
    [LocationID]          NVARCHAR (15)  NULL,
    [LeaveTypeID]         NVARCHAR (15)  NULL,
    [LeaveStartDate]      DATETIME       NULL,
    [LeaveEndDate]        DATETIME       NULL,
    [LeaveTaken]          NUMERIC (15)   NULL,
    [PaidDays]            INT            NULL,
    [VoucherID]           NVARCHAR (15)  NULL,
    [Description]         NVARCHAR (255) NULL,
    [Reference]           NVARCHAR (30)  NULL,
    [RowIndex]            INT            NULL
);

GO
CREATE TABLE [dbo].[Opening_Cheque_Issued] (
    [ChequeID]           INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [ChequeNumber]       NVARCHAR (15)   NOT NULL,
    [PayeeType]          VARCHAR (1)     NOT NULL,
    [PayeeID]            NVARCHAR (64)   NOT NULL,
    [PayeeAccountID]     NVARCHAR (64)   NULL,
    [BankID]             NVARCHAR (15)   NULL,
    [ChequeDate]         DATETIME        NULL,
    [IssueDate]          DATETIME        NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [ExchangeRate]       DECIMAL (10, 5) NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [Note]               NVARCHAR (255)  NULL,
    [IsVoid]             BIT             NULL,
    [Status]             TINYINT         NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [PDCAccountID]       NVARCHAR (64)   NULL,
    [ChequebookID]       NVARCHAR (15)   NULL,
    [ClearanceDate]      DATETIME        NULL,
    [BankAccountID]      NVARCHAR (64)   NULL,
    [ClearanceSysDocID]  NVARCHAR (7)    NULL,
    [ClearanceVoucherID] NVARCHAR (15)   NULL,
    [ClearanceAccountID] NVARCHAR (64)   NULL,
    [IsPrinted]          BIT             NULL,
    [PrintDate]          DATETIME        NULL,
    [PrintName]          NVARCHAR (64)   NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Opening_Cheque_Issued] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [PayeeType] ASC, [PayeeID] ASC)
);

GO
CREATE TABLE [dbo].[Opening_Cheque_Received] (
    [ChequeID]              INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [ChequeNumber]          NVARCHAR (15)   NOT NULL,
    [BankID]                NVARCHAR (15)   NOT NULL,
    [PayeeType]             VARCHAR (1)     NOT NULL,
    [PayeeID]               NVARCHAR (64)   NOT NULL,
    [PayeeAccountID]        NVARCHAR (64)   NULL,
    [ChequeDate]            DATETIME        NULL,
    [ReceiptDate]           DATETIME        NULL,
    [CurrencyID]            NVARCHAR (5)    NULL,
    [ExchangeRate]          DECIMAL (10, 5) NULL,
    [Amount]                MONEY           NULL,
    [AmountFC]              MONEY           NULL,
    [ConAmountFC]           MONEY           NULL,
    [ConRate]               DECIMAL (18, 5) NULL,
    [Note]                  NVARCHAR (255)  NULL,
    [IsVoid]                BIT             CONSTRAINT [DF_Opening_Cheque_Received_Entry_IsVoid] DEFAULT ((0)) NULL,
    [Status]                TINYINT         CONSTRAINT [DF_Opening_Cheque_Received_Entry_Status] DEFAULT ((1)) NULL,
    [Reference]             NVARCHAR (20)   NULL,
    [PDCAccountID]          NVARCHAR (64)   NULL,
    [DepositDate]           DATETIME        NULL,
    [DepositAccountID]      NVARCHAR (64)   NULL,
    [DepositBankID]         NVARCHAR (15)   NULL,
    [DepositSysDocID]       NVARCHAR (7)    NULL,
    [DepositVoucherID]      NVARCHAR (15)   NULL,
    [SendDate]              DATETIME        NULL,
    [SendBankAccountID]     NVARCHAR (64)   NULL,
    [SendReference]         NVARCHAR (20)   NULL,
    [DiscountDate]          DATETIME        NULL,
    [DiscountAccountID]     NVARCHAR (64)   NULL,
    [DiscountBankAccountID] NVARCHAR (64)   NULL,
    [DiscountSysDocID]      NVARCHAR (7)    NULL,
    [DiscountVoucherID]     NVARCHAR (15)   NULL,
    [DiscountAmount]        MONEY           NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Opening_Cheque_Received_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [BankID] ASC, [PayeeType] ASC, [PayeeID] ASC)
);

GO
CREATE TABLE [dbo].[Opportunity] (
    [OpportunityID]   NVARCHAR (15)   NOT NULL,
    [OpportunityName] NVARCHAR (64)   NOT NULL,
    [Status]          TINYINT         NULL,
    [ClosingDate]     DATETIME        NULL,
    [Probability]     TINYINT         NULL,
    [Amount]          DECIMAL (18, 2) NULL,
    [RelatedID]       NVARCHAR (64)   NULL,
    [RelatedType]     TINYINT         NULL,
    [OwnerID]         NVARCHAR (15)   NULL,
    [DueDate]         DATETIME        NULL,
    [Note]            NVARCHAR (255)  NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[OverTimeEntry] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [Month]              TINYINT        NULL,
    [Year]               INT            NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovedBy]         NVARCHAR (15)  NULL,
    [ApprovalDate]       DATETIME       NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [DateCreated]        DATETIME       NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    [DateUpdated]        DATETIME       NULL
);

GO
CREATE TABLE [dbo].[OverTimeEntry_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [JobID]          NVARCHAR (50)   NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [PayrollPeriod]  DATETIME        NULL,
    [WorkDate]       DATETIME        NULL,
    [EmployeeID]     NVARCHAR (15)   NOT NULL,
    [EmployeeName]   NVARCHAR (64)   NULL,
    [Remarks]        NVARCHAR (3000) NULL,
    [LocationID]     NVARCHAR (15)   NULL,
    [FromTime]       DATETIME        NULL,
    [ToTime]         DATETIME        NULL,
    [Hours]          DECIMAL (18, 5) NULL,
    [OTHours]        DECIMAL (18, 5) NULL,
    [OTType]         NVARCHAR (50)   NULL,
    [OTRate]         DECIMAL (18, 5) NULL,
    [Amount]         MONEY           NULL,
    [RowIndex]       INT             NULL,
    [LeaveDays]      INT             NULL
);

GO
CREATE TABLE [dbo].[Patient] (
    [CustomerID]        NVARCHAR (64)  NOT NULL,
    [FileOpenDate]      DATETIME       NULL,
    [FileNo]            NVARCHAR (64)  NULL,
    [FirstName]         NVARCHAR (150) NULL,
    [MiddleName]        NVARCHAR (150) NULL,
    [LastName]          NVARCHAR (150) NULL,
    [NickName]          NVARCHAR (30)  NULL,
    [BloodGroup]        NCHAR (5)      NULL,
    [AnalysisID]        NVARCHAR (15)  NULL,
    [BirthDate]         SMALLDATETIME  NULL,
    [BirthPlace]        NVARCHAR (50)  NULL,
    [Photo]             IMAGE          NULL,
    [NationalityID]     NVARCHAR (15)  NULL,
    [UID]               NVARCHAR (50)  NULL,
    [ReligionID]        NVARCHAR (15)  NULL,
    [LocationID]        NVARCHAR (15)  NULL,
    [Gender]            CHAR (1)       NULL,
    [MaritalStatus]     TINYINT        CONSTRAINT [DF_Patient_MaritalStatus] DEFAULT ((1)) NULL,
    [NationalID]        NVARCHAR (30)  NULL,
    [ChronicsControlID] NCHAR (10)     NULL,
    [AllergyControlID]  NCHAR (10)     NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);

GO
CREATE TABLE [dbo].[Patient_Detail_Table] (
    [CustomerID]   NVARCHAR (64)  NULL,
    [RelativeName] NVARCHAR (50)  NULL,
    [Age]          INT            NULL,
    [Relation]     NVARCHAR (50)  NULL,
    [Remarks]      NVARCHAR (500) NULL,
    [RowIndex]     INT            NULL
);

GO
CREATE TABLE [dbo].[Patient_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Patient_Docs_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Patient_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Patient_Document] (
    [CustomerID]     NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Patient_Docs] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Payment_Allocation_Batch] (
    [BatchID]   INT           IDENTITY (1, 1) NOT NULL,
    [BatchDate] DATETIME      NULL,
    [PartyType] CHAR (1)      NULL,
    [PartyID]   NVARCHAR (64) NULL,
    CONSTRAINT [PK_Payment_Allocation_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC)
);

GO
CREATE TABLE [dbo].[Payment_Method] (
    [PaymentMethodID]  NVARCHAR (15)  NOT NULL,
    [MethodName]       NVARCHAR (64)  NOT NULL,
    [MethodType]       TINYINT        CONSTRAINT [DF_Payment_Method_MethodType] DEFAULT ((1)) NOT NULL,
    [IsInactive]       BIT            NULL,
    [Note]             NVARCHAR (255) NULL,
    [DefaultAccountID] INT            NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Payment Methods] PRIMARY KEY CLUSTERED ([PaymentMethodID] ASC)
);

GO
CREATE TABLE [dbo].[Payment_Request] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [TypeID]             TINYINT        NULL,
    [TransactionDate]    DATETIME       NULL,
    [PayFromID]          NVARCHAR (64)  NULL,
    [PayeeType]          CHAR (1)       NULL,
    [PayeeID]            NVARCHAR (64)  NULL,
    [Amount]             MONEY          NULL,
    [AmountFC]           MONEY          NULL,
    [AvailableBal]       MONEY          NULL,
    [CurrentBal]         MONEY          NULL,
    [CurrencyID]         NVARCHAR (7)   NULL,
    [POSysDocID]         NVARCHAR (7)   NULL,
    [POVoucherID]        NVARCHAR (15)  NULL,
    [PLSysDocID]         NVARCHAR (7)   NULL,
    [PLVoucherID]        NVARCHAR (15)  NULL,
    [PaymentRequested]   MONEY          NULL,
    [PaymentRequestedFC] MONEY          NULL,
    [InvoiceNos]         NVARCHAR (100) NULL,
    [Authorizedby]       NVARCHAR (30)  NULL,
    [NoofInvoices]       INT            NULL,
    [NoofPL]             INT            NULL,
    [NoofBOL]            INT            NULL,
    [NoofGoods]          NVARCHAR (30)  NULL,
    [IsVoid]             BIT            NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [Reason]             TINYINT        NULL,
    [Status]             TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    CONSTRAINT [PK_Payment_Request] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Payment_Term] (
    [PaymentTermID]  NVARCHAR (15)  NOT NULL,
    [TermName]       NVARCHAR (64)  NOT NULL,
    [NetDays]        INT            NOT NULL,
    [DiscountDays]   TINYINT        NOT NULL,
    [DiscountAmount] MONEY          NULL,
    [DiscountType]   TINYINT        NOT NULL,
    [TermType]       TINYINT        NOT NULL,
    [IsInstallments] BIT            NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            NULL,
    [DateUpdated]    DATETIME       NULL,
    [DateCreated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Terms] PRIMARY KEY CLUSTERED ([PaymentTermID] ASC)
);

GO
CREATE TABLE [dbo].[Payment_Term_Installment] (
    [PaymentTermID] NVARCHAR (15) NULL,
    [RowIndex]      TINYINT       NULL,
    [Percentage]    INT           NULL,
    [Days]          INT           NULL,
    [TermType]      TINYINT       NULL
);

GO
CREATE TABLE [dbo].[Payroll_Transaction] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [BankAccountID]      NVARCHAR (64)   NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [ChequeTotal]        MONEY           NULL,
    [OtherCharges]       MONEY           NULL,
    [OtherAccountID]     NVARCHAR (64)   NULL,
    [Name]               NVARCHAR (64)   NULL,
    [Month]              TINYINT         NULL,
    [StartDate]          DATETIME        NULL,
    [EndDate]            DATETIME        NULL,
    [IsDebit]            BIT             NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [TransactionDate]    DATETIME        NULL,
    [IsVoid]             BIT             NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [GLType]             TINYINT         NULL,
    [JournalID]          INT             NULL,
    [ChequeID]           INT             NULL,
    [ChequebookID]       NVARCHAR (15)   NULL,
    [CheckNumber]        NVARCHAR (15)   NULL,
    [CheckDate]          DATETIME        NULL,
    [Reference]          NVARCHAR (15)   NULL,
    [TransactionStatus]  TINYINT         CONSTRAINT [DF_Payroll_Transaction_TransactionStatus] DEFAULT ((1)) NULL,
    [Description]        NVARCHAR (255)  NULL,
    [TypeID]             INT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [TaxGroupID]         NVARCHAR (30)   NULL,
    [TaxAmount]          MONEY           NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Payroll_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Payroll_Transaction_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [EmployeeID]        NVARCHAR (64)   NULL,
    [SheetID]           NVARCHAR (15)   NULL,
    [SheetDocID]        NVARCHAR (15)   NULL,
    [PayrollItemType]   TINYINT         NULL,
    [PayrollItemID]     NVARCHAR (15)   NULL,
    [PayType]           TINYINT         NULL,
    [LoanSysDocID]      NVARCHAR (7)    NULL,
    [LoanVoucherID]     NVARCHAR (15)   NULL,
    [AccountID]         NVARCHAR (64)   NULL,
    [Days]              DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (255)  NULL,
    [PaymentMethodType] TINYINT         NULL,
    [Amount]            MONEY           NULL,
    [AmountFC]          MONEY           NULL,
    [RowIndex]          SMALLINT        NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [AnalysisID]        NVARCHAR (15)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [SheetSysDocID]     NVARCHAR (15)   NULL,
    [SheetVoucherID]    NVARCHAR (15)   NULL,
    [SheetRowIndex]     SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[PayrollItem] (
    [PayrollItemID]     NVARCHAR (15)  NOT NULL,
    [PayrollItemName]   NVARCHAR (30)  NOT NULL,
    [PayrollItemType]   TINYINT        NULL,
    [PayCodeType]       TINYINT        NULL,
    [Note]              NVARCHAR (255) NULL,
    [Inactive]          BIT            CONSTRAINT [DF_Allowance_Inactive] DEFAULT ((1)) NULL,
    [InLeaveSalary]     BIT            CONSTRAINT [DF_Allowance_InLeaveSalary] DEFAULT ((1)) NULL,
    [InDeduction]       BIT            NULL,
    [InOvertime]        BIT            NULL,
    [InServiceBenefit]  BIT            NULL,
    [InFixed]           BIT            NULL,
    [InSalaryDeduction] BIT            NULL,
    [AccountID]         NVARCHAR (64)  NOT NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Allowance] PRIMARY KEY CLUSTERED ([PayrollItemID] ASC)
);

GO
CREATE TABLE [dbo].[Performance_Details] (
    [PositionID]           NVARCHAR (15)   NOT NULL,
    [PerformanceParameter] NTEXT           NULL,
    [Score]                DECIMAL (15, 2) NULL,
    [RowIndex]             NCHAR (10)      NULL
);

GO
CREATE TABLE [dbo].[Period_Lock] (
    [PeriodID]  INT            IDENTITY (1, 1) NOT NULL,
    [CloseDate] DATETIME       NULL,
    [IsLocked]  BIT            NULL,
    [Remarks]   NVARCHAR (255) NULL,
    CONSTRAINT [PK_Periods] PRIMARY KEY CLUSTERED ([PeriodID] ASC)
);

GO
CREATE TABLE [dbo].[Physical_Stock_Entry] (
    [DocNumber]    NVARCHAR (50) NOT NULL,
    [PurchaseDate] DATETIME      NULL,
    [Reference]    NVARCHAR (50) NULL,
    [LocationID]   NVARCHAR (50) NULL,
    [Note]         NVARCHAR (50) NULL,
    [CreatedBy]    NVARCHAR (50) NULL,
    [DateCreated]  DATETIME      NULL,
    [UpdatedBy]    NVARCHAR (50) NULL,
    [DateUpdated]  DATETIME      NULL,
    CONSTRAINT [PK_PhysicalStockEntry] PRIMARY KEY CLUSTERED ([DocNumber] ASC)
);

GO
CREATE TABLE [dbo].[Physical_Stock_Entry_Detail] (
    [DocNumber]       NVARCHAR (50)   NOT NULL,
    [ItemID]          NVARCHAR (50)   NULL,
    [ItemDescription] NVARCHAR (50)   NULL,
    [Unit]            NVARCHAR (50)   NULL,
    [Qty]             DECIMAL (18, 2) NULL,
    [PurchaseDate]    DATETIME        NULL,
    [Reference]       NVARCHAR (50)   NULL,
    [LocationID]      NVARCHAR (50)   NULL,
    [Note]            NVARCHAR (50)   NULL,
    [RowIndex]        INT             NULL
);

GO
CREATE TABLE [dbo].[Pivot_Group] (
    [GroupID]     INT           IDENTITY (1, 1) NOT NULL,
    [GroupName]   NVARCHAR (30) NULL,
    [DateCreated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [DateUpdated] DATETIME      NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Chart_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Pivot_Report] (
    [PivotID]     NVARCHAR (15)  NOT NULL,
    [PivotName]   NVARCHAR (64)  NULL,
    [Description] NVARCHAR (255) NULL,
    [GroupID]     INT            NULL,
    [DataQuery]   NTEXT          NULL,
    [ChartLayout] XML            NULL,
    [HideTotal]   BIT            NULL,
    [Inactive]    BIT            NULL,
    [DateCreated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [DateUpdated] DATETIME       NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Chart] PRIMARY KEY CLUSTERED ([PivotID] ASC)
);

GO
CREATE TABLE [dbo].[Pivot_Report_Field] (
    [PivotID]       NVARCHAR (15) NOT NULL,
    [FieldName]     NVARCHAR (30) NOT NULL,
    [DisplayName]   NVARCHAR (30) NULL,
    [DataType]      NVARCHAR (30) NULL,
    [Area]          TINYINT       NULL,
    [GroupInterval] INT           NULL,
    [AreaIndex]     TINYINT       NULL,
    CONSTRAINT [PK_Pivot_Report_Field] PRIMARY KEY CLUSTERED ([PivotID] ASC, [FieldName] ASC)
);

GO
CREATE TABLE [dbo].[PO_Shipment] (
    [SysDocID]           NVARCHAR (6)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [ContainerNumber]    NVARCHAR (15)   NULL,
    [Port]               NVARCHAR (15)   NULL,
    [LoadingPort]        NVARCHAR (15)   NULL,
    [ETA]                DATETIME        NULL,
    [ATD]                DATETIME        NULL,
    [Status]             TINYINT         CONSTRAINT [DF_PO_Shipment_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [VendorReferenceNo]  NVARCHAR (40)   NULL,
    [PONumber]           NVARCHAR (20)   NULL,
    [BOLNumber]          NVARCHAR (20)   NULL,
    [Shipper]            NVARCHAR (15)   NULL,
    [ClearingAgent]      NVARCHAR (30)   NULL,
    [Weight]             DECIMAL (18, 5) NULL,
    [IsReceived]         BIT             NULL,
    [Note]               NVARCHAR (4000) NULL,
    [Value]              MONEY           NULL,
    [ShipStatus]         BIT             NULL,
    [TransporterID]      NVARCHAR (50)   NULL,
    [ContainerSizeID]    NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    CONSTRAINT [PK_PO_Shipment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[PO_Shipment_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceRowIndex]   INT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [IsSourcedRow]     BIT             NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [RowIndex]         TINYINT         NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Port] (
    [PortID]      NVARCHAR (15)  NOT NULL,
    [PortName]    NVARCHAR (64)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [CountryID]   NVARCHAR (15)  NULL,
    [Inactive]    BIT            CONSTRAINT [DF_Port_Inactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Port] PRIMARY KEY CLUSTERED ([PortID] ASC)
);

GO
CREATE TABLE [dbo].[POS_Batch] (
    [BatchID]     INT           IDENTITY (1, 1) NOT NULL,
    [LocationID]  NVARCHAR (15) NULL,
    [OpenDate]    DATETIME      NULL,
    [CloseDate]   DATETIME      NULL,
    [Status]      CHAR (1)      NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_POS_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC)
);

GO
CREATE TABLE [dbo].[POS_Cashier] (
    [CashierID]             NVARCHAR (15) NOT NULL,
    [CashierName]           NVARCHAR (60) NULL,
    [LocationID]            NVARCHAR (15) NULL,
    [ComputerName]          NVARCHAR (30) NULL,
    [ReceiptDocID]          NVARCHAR (15) NULL,
    [ReturnDocID]           NVARCHAR (15) NULL,
    [CashAccountID]         NVARCHAR (64) NULL,
    [CardReceivedAccountID] NVARCHAR (64) NULL,
    [CheckAccountID]        NVARCHAR (64) NULL,
    CONSTRAINT [PK_POS_Cashier] PRIMARY KEY CLUSTERED ([CashierID] ASC)
);

GO
CREATE TABLE [dbo].[POS_CashRegister] (
    [CashRegisterID]     NVARCHAR (15)  NOT NULL,
    [CashRegisterName]   NVARCHAR (60)  NULL,
    [LocationID]         NVARCHAR (15)  NULL,
    [ComputerName]       NVARCHAR (30)  NULL,
    [ReceiptDocID]       NVARCHAR (15)  NULL,
    [ReturnDocID]        NVARCHAR (15)  NULL,
    [DefaultCustomerID]  NVARCHAR (64)  NULL,
    [DiscountAccountID]  NVARCHAR (64)  NULL,
    [ExpenseDocID]       NVARCHAR (7)   NULL,
    [PettyCashAccountID] NVARCHAR (64)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateUpdated]        DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_POS_CashRegister] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC),
    CONSTRAINT [FK_POS_CashRegister_Customer] FOREIGN KEY ([DefaultCustomerID]) REFERENCES [dbo].[Customer] ([CustomerID])
);

GO
CREATE TABLE [dbo].[POS_CashRegister_Expense] (
    [CashRegisterID] NVARCHAR (15) NOT NULL,
    [DisplayName]    NVARCHAR (64) NOT NULL,
    [AccountID]      NVARCHAR (64) NULL,
    [RowIndex]       TINYINT       NULL,
    CONSTRAINT [PK_POS_CashRegister_ExpenseAccounts] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC, [DisplayName] ASC)
);

GO
CREATE TABLE [dbo].[POS_CashRegister_PaymentMethod] (
    [CashRegisterID]  NVARCHAR (15) NOT NULL,
    [PaymentMethodID] NVARCHAR (15) NOT NULL,
    [DisplayName]     NVARCHAR (64) NULL,
    [AccountID]       NVARCHAR (64) NULL,
    [Inactive]        BIT           NULL,
    [RowIndex]        TINYINT       NULL,
    CONSTRAINT [PK_POS_CashRegister_PaymentMethod] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC, [PaymentMethodID] ASC)
);

GO
CREATE TABLE [dbo].[POS_HOLD] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ShiftID]           INT             NULL,
    [BatchID]           INT             NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [DueDate]           DATETIME        NULL,
    [IsCash]            BIT             NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxOption]         TINYINT         NULL,
    [PriceIncludeTax]   BIT             NULL,
    [Status]            TINYINT         CONSTRAINT [DF_POS_HOLD_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [SearchValue]       NVARCHAR (50)   NULL,
    [Discount]          MONEY           NULL,
    [DiscountFC]        MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Total]             MONEY           NULL,
    [TotalCOGS]         MONEY           NULL,
    [TotalFC]           MONEY           NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_POS_HOLD_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (255)  NULL,
    [PaymentMethodType] TINYINT         NULL,
    [RequireUpdate]     BIT             NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_POS_HOLD] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[POS_Hold_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Discount]        DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [COGS]            MONEY           NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [OrderVoucherID]  NVARCHAR (15)   NULL,
    [OrderSysDocID]   NVARCHAR (6)    NULL,
    [DNoteVoucherID]  NVARCHAR (15)   NULL,
    [DNoteSysDocID]   NVARCHAR (7)    NULL,
    [OrderRowIndex]   INT             NULL,
    [IsDNRow]         BIT             NULL
);

GO
CREATE TABLE [dbo].[POS_Shift] (
    [ShiftID]        INT           NOT NULL,
    [BatchID]        INT           NOT NULL,
    [LocationID]     NVARCHAR (15) NULL,
    [UserID]         NVARCHAR (15) NULL,
    [CashRegisterID] NVARCHAR (15) NULL,
    [OpenDate]       DATETIME      NULL,
    [CloseDate]      DATETIME      NULL,
    [OpeningCash]    MONEY         NULL,
    [ClosingCash]    MONEY         NULL,
    [Status]         CHAR (1)      NULL,
    [DateCreated]    DATETIME      NULL,
    [CreatedBy]      NVARCHAR (15) NULL,
    CONSTRAINT [PK_POS_Shift] PRIMARY KEY CLUSTERED ([ShiftID] ASC, [BatchID] ASC)
);

GO
CREATE TABLE [dbo].[Position] (
    [PositionID]        NVARCHAR (15)  NOT NULL,
    [PositionName]      NVARCHAR (64)  NULL,
    [Note]              NVARCHAR (255) NULL,
    [JobDescription]    NTEXT          NULL,
    [AppraisalInterval] INT            NULL,
    [AppraisalFromDate] DATETIME       NULL,
    [AppraisalToDate]   DATETIME       NULL,
    [ReportTo]          NVARCHAR (15)  NULL,
    [Inactive]          BIT            CONSTRAINT [DF_Position_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED ([PositionID] ASC)
);

GO
CREATE TABLE [dbo].[Position_Details] (
    [PositionID]   NVARCHAR (15)   NOT NULL,
    [KPIParameter] NTEXT           NULL,
    [Weightage]    DECIMAL (15, 2) NULL,
    [Scale]        NTEXT           NULL,
    [Target]       DECIMAL (15, 2) NULL,
    [RowIndex]     NCHAR (10)      NULL
);

GO
CREATE TABLE [dbo].[Price_Level] (
    [PriceLevelID]   NVARCHAR (15)  NOT NULL,
    [PriceLevelName] NVARCHAR (64)  NOT NULL,
    [Note]           NVARCHAR (255) NULL,
    [IsInactive]     BIT            CONSTRAINT [DF_Price Levels_IsInactive] DEFAULT ((0)) NULL,
    [DateUpdated]    DATETIME       NULL,
    [DateCreated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Price Levels] PRIMARY KEY CLUSTERED ([PriceLevelID] ASC),
    CONSTRAINT [FK_Price Levels_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Price Levels_Users1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([UserID])
);

GO
CREATE TABLE [dbo].[Price_List] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [ValidDateFrom]     DATETIME        NULL,
    [ValidDateTo]       DATETIME        NULL,
    [ApplicableToChild] BIT             NULL,
    [Inactive]          BIT             NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Price_List_Status] DEFAULT ((1)) NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [Total]             MONEY           NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Price_List_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [CustomerProductID] NVARCHAR (64)   NULL,
    [UnitPrice]         DECIMAL (18, 5) NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [Remarks]           NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [SubunitPrice]      DECIMAL (18, 5) NULL,
    [RowIndex]          INT             NULL
);

GO
CREATE TABLE [dbo].[Print_Template_Map] (
    [MapID]        NVARCHAR (15)  NOT NULL,
    [ScreenType]   TINYINT        NULL,
    [ScreenID]     NVARCHAR (255) NULL,
    [ScreenArea]   NVARCHAR (64)  NULL,
    [TemplateName] NVARCHAR (64)  NULL,
    [FileName]     NVARCHAR (64)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Print_Template_Map_1] PRIMARY KEY CLUSTERED ([MapID] ASC)
);

GO
CREATE TABLE [dbo].[Product] (
    [ProductID]            NVARCHAR (64)   NOT NULL,
    [Description]          NVARCHAR (255)  NULL,
    [Description2]         NVARCHAR (255)  NULL,
    [Description3]         NVARCHAR (255)  NULL,
    [ParentProductID]      NVARCHAR (64)   NULL,
    [MatrixParentID]       NVARCHAR (64)   NULL,
    [ClassID]              NVARCHAR (15)   NULL,
    [UPC]                  NVARCHAR (30)   NULL,
    [IsPriceEmbedded]      BIT             NULL,
    [ExcludeFromCatalogue] BIT             NULL,
    [VendorRef]            NVARCHAR (30)   NULL,
    [Attribute1]           NVARCHAR (30)   NULL,
    [Attribute2]           NVARCHAR (30)   NULL,
    [Attribute3]           NVARCHAR (30)   NULL,
    [ItemType]             TINYINT         CONSTRAINT [DF_Product_ItemType] DEFAULT ((1)) NULL,
    [UnitPrice1]           DECIMAL (18, 5) NULL,
    [UnitPrice2]           DECIMAL (18, 5) NULL,
    [UnitPrice3]           DECIMAL (18, 5) NULL,
    [MinPrice]             DECIMAL (18, 5) NULL,
    [StandardCost]         DECIMAL (18, 5) NULL,
    [AverageCost]          DECIMAL (18, 5) NOT NULL,
    [LastCost]             DECIMAL (18, 5) NULL,
    [CostMethod]           TINYINT         NULL,
    [Flag]                 TINYINT         NULL,
    [IsTrackLot]           BIT             NULL,
    [IsTrackSerial]        BIT             NULL,
    [BOMID]                NVARCHAR (15)   NULL,
    [CategoryID]           NVARCHAR (15)   NULL,
    [QuantityPerUnit]      REAL            CONSTRAINT [DF_Products_QuantityPerUnit] DEFAULT ((0)) NULL,
    [IsInactive]           BIT             CONSTRAINT [DF_Products_IsDiscontinued] DEFAULT ((0)) NULL,
    [IsHoldSale]           BIT             NULL,
    [HideInPOS]            BIT             NULL,
    [Photo]                IMAGE           NULL,
    [Weight]               NUMERIC (5, 2)  NULL,
    [UnitID]               NVARCHAR (15)   NULL,
    [ReorderLevel]         DECIMAL (18, 5) NULL,
    [DefaultLocationID]    NVARCHAR (15)   NULL,
    [COGSAccount]          NVARCHAR (64)   NULL,
    [IncomeAccount]        NVARCHAR (64)   NULL,
    [ExpenseCode]          NVARCHAR (50)   NULL,
    [AssetAccount]         NVARCHAR (64)   NULL,
    [PreferredVendor]      NVARCHAR (64)   NULL,
    [StyleID]              NVARCHAR (15)   NULL,
    [Attribute]            NVARCHAR (30)   NULL,
    [Size]                 NVARCHAR (30)   NULL,
    [BrandID]              NVARCHAR (15)   NULL,
    [ManufacturerID]       NVARCHAR (15)   NULL,
    [MaterialID]           NVARCHAR (15)   NULL,
    [FinishingID]          NVARCHAR (15)   NULL,
    [ColorID]              NVARCHAR (15)   NULL,
    [GradeID]              NVARCHAR (15)   NULL,
    [StandardID]           NVARCHAR (15)   NULL,
    [PType1]               NVARCHAR (50)   NULL,
    [PType2]               NVARCHAR (50)   NULL,
    [PType3]               NVARCHAR (50)   NULL,
    [PType4]               NVARCHAR (50)   NULL,
    [PType5]               NVARCHAR (50)   NULL,
    [PType6]               NVARCHAR (50)   NULL,
    [PType7]               NVARCHAR (50)   NULL,
    [PType8]               NVARCHAR (50)   NULL,
    [Origin]               NVARCHAR (30)   NULL,
    [WarrantyPeriod]       NUMERIC (5)     NULL,
    [Quantity]             DECIMAL (18, 5) NULL,
    [ReservedQuantity]     DECIMAL (18, 5) NULL,
    [OrderedQuantity]      DECIMAL (18, 5) NULL,
    [IgnoreCostDiffAmount] DECIMAL (5, 2)  NULL,
    [Note]                 NTEXT           NULL,
    [IsTaxable]            BIT             NULL,
    [RackBin]              NVARCHAR (30)   NULL,
    [TaxOption]            TINYINT         NULL,
    [TaxGroupID]           NVARCHAR (15)   NULL,
    [TaxIDNumber]          NVARCHAR (30)   NULL,
    [UserDefined1]         NVARCHAR (64)   NULL,
    [UserDefined2]         NVARCHAR (64)   NULL,
    [UserDefined3]         NVARCHAR (64)   NULL,
    [UserDefined4]         NVARCHAR (64)   NULL,
    [ApprovalStatus]       TINYINT         NULL,
    [VerificationStatus]   TINYINT         NULL,
    [W3PLRentPrice]        DECIMAL (18, 5) NULL,
    [DateCreated]          DATETIME        NULL,
    [DateUpdated]          DATETIME        NULL,
    [CreatedBy]            NVARCHAR (15)   NULL,
    [UpdatedBy]            NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID])
);

GO
CREATE TABLE [dbo].[Product_AppliedModels_Detail] (
    [ProductID]     NVARCHAR (64)  NOT NULL,
    [VehicleMakeID] NVARCHAR (15)  NULL,
    [VehicleTypeID] NVARCHAR (15)  NULL,
    [Remarks]       NVARCHAR (255) NULL
);

GO
CREATE TABLE [dbo].[Product_Attribute] (
    [AttributeID]   NVARCHAR (15) NOT NULL,
    [AttributeName] NVARCHAR (30) NOT NULL,
    [IsInactive]    BIT           CONSTRAINT [DF_Product_Attribute_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated]   DATETIME      NULL,
    [DateUpdated]   DATETIME      NULL,
    [CreatedBy]     NVARCHAR (15) NULL,
    [UpdatedBy]     NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Product_Brand] (
    [BrandID]         NVARCHAR (15)  NOT NULL,
    [BrandName]       NVARCHAR (64)  NOT NULL,
    [IsInactive]      BIT            CONSTRAINT [DF_Product_Brand_IsInactive] DEFAULT ((0)) NULL,
    [Note]            NVARCHAR (255) NULL,
    [PreferredVendor] NVARCHAR (64)  NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product_Brand] PRIMARY KEY CLUSTERED ([BrandID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Category] (
    [CategoryID]            NVARCHAR (15)   NOT NULL,
    [CategoryName]          NVARCHAR (125)  NOT NULL,
    [ParentCategoryID]      NVARCHAR (15)   NULL,
    [IsInactive]            BIT             CONSTRAINT [DF_Product Categories_IsInactive] DEFAULT ((0)) NULL,
    [AgeGroup1]             SMALLINT        NULL,
    [AgeGroup2]             SMALLINT        NULL,
    [AgeGroup3]             SMALLINT        NULL,
    [Note]                  NVARCHAR (255)  NULL,
    [StandardPricePercent]  DECIMAL (18, 2) NULL,
    [WholesalePricePercent] DECIMAL (18, 2) NULL,
    [SpecialPricePercent]   DECIMAL (18, 2) NULL,
    [MinimumPricePercent]   DECIMAL (18, 2) NULL,
    [CommissionPercent]     DECIMAL (4, 2)  NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Product Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Category_Detail] (
    [ProductID]         NVARCHAR (64) NOT NULL,
    [ProductCategoryID] NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Product_Category_Detail] PRIMARY KEY CLUSTERED ([ProductID] ASC, [ProductCategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Class] (
    [ClassID]       NVARCHAR (15)   NOT NULL,
    [ClassName]     NVARCHAR (64)   NOT NULL,
    [ItemType]      TINYINT         NULL,
    [CostMethod]    TINYINT         NULL,
    [CategoryID]    NVARCHAR (15)   NULL,
    [IsInactive]    BIT             CONSTRAINT [DF_Product_Class_IsInactive_1] DEFAULT ((0)) NULL,
    [UnitID]        NVARCHAR (15)   NULL,
    [COGSAccount]   NVARCHAR (64)   NULL,
    [IncomeAccount] NVARCHAR (64)   NULL,
    [AssetAccount]  NVARCHAR (64)   NULL,
    [Note]          NTEXT           NULL,
    [IsTaxable]     BIT             NULL,
    [TaxOption]     TINYINT         NULL,
    [TaxGroupID]    NVARCHAR (15)   NULL,
    [W3PLRentPrice] DECIMAL (18, 5) NULL,
    [DateCreated]   DATETIME        NULL,
    [DateUpdated]   DATETIME        NULL,
    [CreatedBy]     NVARCHAR (15)   NULL,
    [UpdatedBy]     NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Product_Class] PRIMARY KEY CLUSTERED ([ClassID] ASC)
);

GO
CREATE TABLE [dbo].[Product_COGS_Change] (
    [ItemCode]      NVARCHAR (64)   NULL,
    [InvoiceNumber] NVARCHAR (15)   NULL,
    [DocID]         NVARCHAR (6)    NULL,
    [RowIndex]      INT             NULL,
    [LotNo]         INT             NULL,
    [Quantity]      DECIMAL (18, 5) NULL,
    [Cost]          DECIMAL (18, 5) NULL,
    [COGS]          MONEY           NULL
);

GO
CREATE TABLE [dbo].[Product_Group] (
    [GroupID]     INT            IDENTITY (1, 1) NOT NULL,
    [GroupName]   NVARCHAR (64)  NOT NULL,
    [IsInactive]  BIT            CONSTRAINT [DF_Product_Group_IsInactive] DEFAULT ((0)) NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Location] (
    [LocationID] NVARCHAR (15) NULL,
    [ProductID]  NVARCHAR (64) NULL,
    [Quantity]   REAL          NULL
);

GO
CREATE TABLE [dbo].[Product_Lot] (
    [LotNumber]       INT             NOT NULL,
    [ItemCode]        NVARCHAR (64)   NULL,
    [Reference]       NVARCHAR (100)  NULL,
    [SourceLotNumber] INT             NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [AvgCost]         DECIMAL (18, 5) NULL,
    [LotQty]          DECIMAL (18, 5) NULL,
    [SoldQty]         DECIMAL (18, 5) NULL,
    [ReturnedQty]     DECIMAL (18, 5) NULL,
    [DocID]           NVARCHAR (8)    NOT NULL,
    [ReceiptNumber]   NVARCHAR (15)   NOT NULL,
    [RowIndex]        INT             NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [ReceiptDate]     DATETIME        NULL,
    [ProductionDate]  DATETIME        NULL,
    [ExpiryDate]      DATETIME        NULL,
    [SupplierCode]    NVARCHAR (64)   NULL,
    [IsDeleted]       BIT             NULL,
    [BinID]           NVARCHAR (30)   NULL,
    [RackID]          NVARCHAR (30)   NULL,
    [Reference2]      NVARCHAR (30)   NULL,
    [SourceRecordID]  INT             NULL,
    CONSTRAINT [PK_Product_Lot] PRIMARY KEY CLUSTERED ([LotNumber] ASC)
);

GO
CREATE TABLE [dbo].[Product_Lot_Issue_Detail] (
    [LotNumber]       NVARCHAR (15)   NULL,
    [Reference]       NVARCHAR (40)   NULL,
    [TransactionDate] DATETIME        NULL,
    [SourceLotNumber] NVARCHAR (15)   NULL,
    [SysDocID]        NVARCHAR (7)    NULL,
    [VoucherID]       NVARCHAR (15)   NULL,
    [RowIndex]        INT             NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [BinID]           NVARCHAR (30)   NULL,
    [RackID]          NVARCHAR (30)   NULL,
    [SoldQty]         DECIMAL (18, 5) NULL,
    [UnitPrice]       DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [Reference2]      NVARCHAR (30)   NULL
);

GO
CREATE TABLE [dbo].[Product_Lot_Receiving_Detail] (
    [LotNumber]       NVARCHAR (40)   NULL,
    [SourceLotNumber] INT             NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [BinID]           NVARCHAR (30)   NULL,
    [RackID]          NVARCHAR (30)   NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [SysDocID]        NVARCHAR (7)    NULL,
    [VoucherID]       NVARCHAR (15)   NULL,
    [RowIndex]        INT             NULL,
    [LotQty]          DECIMAL (18, 5) NULL,
    [SoldQty]         DECIMAL (18, 5) NULL,
    [ReceiptDate]     DATETIME        NULL,
    [ProductionDate]  DATETIME        NULL,
    [ExpiryDate]      DATETIME        NULL,
    [Reference2]      NVARCHAR (30)   NULL
);

GO
CREATE TABLE [dbo].[Product_Lot_Sales] (
    [RecordID]        INT             IDENTITY (1, 1) NOT NULL,
    [DocID]           NVARCHAR (7)    NULL,
    [InvoiceNumber]   NVARCHAR (15)   NULL,
    [TransactionDate] DATETIME        NULL,
    [RowIndex]        INT             NULL,
    [ItemCode]        NVARCHAR (64)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [ReturnQty]       DECIMAL (18, 5) NULL,
    [LotNo]           INT             NULL,
    [SoldQty]         DECIMAL (18, 5) NULL,
    [FOCQuantity]     DECIMAL (18, 5) NULL,
    [Discount]        DECIMAL (18, 5) NULL,
    [UnitPrice]       DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [IsSettled]       BIT             NULL,
    [BinID]           NVARCHAR (30)   NULL,
    [RackID]          NVARCHAR (30)   NULL,
    [Reference2]      NVARCHAR (30)   NULL,
    CONSTRAINT [PK_Product_Lot_Sales] PRIMARY KEY CLUSTERED ([RecordID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Lot_Sales_PickList] (
    [RecordID]      INT             IDENTITY (1, 1) NOT NULL,
    [DocID]         NVARCHAR (7)    NULL,
    [InvoiceNumber] NVARCHAR (15)   NULL,
    [RowIndex]      INT             NULL,
    [ItemCode]      NVARCHAR (64)   NULL,
    [LocationID]    NVARCHAR (15)   NULL,
    [ReturnQty]     DECIMAL (18, 5) NULL,
    [LotNo]         INT             NULL,
    [SoldQty]       DECIMAL (18, 5) NULL,
    [FOCQuantity]   DECIMAL (18, 5) NULL,
    [Discount]      DECIMAL (18, 5) NULL,
    [UnitPrice]     DECIMAL (18, 5) NULL,
    [Cost]          DECIMAL (18, 5) NULL,
    [IsSettled]     BIT             NULL,
    [BinID]         NVARCHAR (15)   NULL,
    [Reference2]    NVARCHAR (30)   NULL,
    CONSTRAINT [PK_Product_Lot_Sales_PickList] PRIMARY KEY CLUSTERED ([RecordID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Make] (
    [MakeID]      NVARCHAR (15)  NOT NULL,
    [MakeName]    NVARCHAR (64)  NOT NULL,
    [IsInactive]  BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product_Make] PRIMARY KEY CLUSTERED ([MakeID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Manufacturer] (
    [ManufacturerID]   NVARCHAR (15)  NOT NULL,
    [ManufacturerName] NVARCHAR (64)  NOT NULL,
    [IsInactive]       BIT            CONSTRAINT [DF_Product_Manufacturer_IsInactive] DEFAULT ((0)) NULL,
    [Note]             NVARCHAR (255) NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product Manufacturers] PRIMARY KEY CLUSTERED ([ManufacturerID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Model] (
    [ModelID]     NVARCHAR (15)  NOT NULL,
    [ModelName]   NVARCHAR (64)  NOT NULL,
    [TypeID]      NVARCHAR (15)  NULL,
    [IsInactive]  BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product_Model] PRIMARY KEY CLUSTERED ([ModelID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Parent] (
    [ProductParentID]      NVARCHAR (64)   NOT NULL,
    [Description]          NVARCHAR (255)  NULL,
    [Dimensions]           TINYINT         NULL,
    [DIM1]                 NVARCHAR (15)   NULL,
    [DIM2]                 NVARCHAR (15)   NULL,
    [DIM3]                 NVARCHAR (15)   NULL,
    [UnitPrice1]           DECIMAL (18, 5) NULL,
    [UnitPrice2]           DECIMAL (18, 5) NULL,
    [UnitPrice3]           DECIMAL (18, 5) NULL,
    [MinPrice]             DECIMAL (18, 5) NULL,
    [ClassID]              NVARCHAR (15)   NULL,
    [ExcludeFromCatalogue] BIT             NULL,
    [ParentType]           TINYINT         NULL,
    [LookupCode]           NVARCHAR (30)   NULL,
    [Photo]                IMAGE           NULL,
    [ItemType]             TINYINT         NULL,
    [CostMethod]           TINYINT         NULL,
    [CategoryID]           NVARCHAR (15)   NULL,
    [QuantityPerUnit]      REAL            NULL,
    [UnitID]               NVARCHAR (15)   NULL,
    [BrandID]              NVARCHAR (15)   NULL,
    [ManufacturerID]       NVARCHAR (15)   NULL,
    [PreferredVendor]      NVARCHAR (15)   NULL,
    [Origin]               NVARCHAR (30)   NULL,
    [IsInactive]           BIT             NULL,
    [Note]                 NTEXT           NULL,
    [DateCreated]          DATETIME        NULL,
    [DateUpdated]          DATETIME        NULL,
    [CreatedBy]            NVARCHAR (15)   NULL,
    [UpdatedBy]            NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Product_Parent] PRIMARY KEY CLUSTERED ([ProductParentID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Parent_Components] (
    [ProductParentID] NVARCHAR (64) NULL,
    [ProductID]       NVARCHAR (64) NULL,
    [Attribute1]      NVARCHAR (15) NULL,
    [Attribute2]      NVARCHAR (15) NULL,
    [Attribute3]      NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Product_Parts_Detail] (
    [ProductID]       NVARCHAR (64)  NOT NULL,
    [Specification]   NTEXT          NULL,
    [VehicleMakeID]   NVARCHAR (15)  NULL,
    [VehicleTypeID]   NVARCHAR (15)  NULL,
    [VehicleModelID]  NVARCHAR (15)  NULL,
    [PartsMakeTypeID] NVARCHAR (15)  NULL,
    [PartsTypeID]     NVARCHAR (15)  NULL,
    [PartsFamilyID]   NVARCHAR (15)  NULL,
    [PartsChasisNo]   NVARCHAR (100) NULL,
    [PartsModel]      NVARCHAR (30)  NULL,
    [PartsEngineNo]   NVARCHAR (100) NULL,
    [OEMCode]         NVARCHAR (100) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Product_Price_Bulk_Update] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [TransactionDate] DATETIME        NOT NULL,
    [Status]          TINYINT         CONSTRAINT [DF_Product_Price_Bulk_Update_Status] DEFAULT ((1)) NULL,
    [IsVoid]          BIT             NULL,
    [Note]            NVARCHAR (4000) NULL,
    [ApprovalStatus]  TINYINT         NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Product_Price_Bulk_Update] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Price_Bulk_Update_Detail] (
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [ProductID]             NVARCHAR (64)   NOT NULL,
    [Description]           NVARCHAR (255)  NULL,
    [CategoryID]            NVARCHAR (15)   NULL,
    [LastPurchasePrice]     DECIMAL (18, 2) NULL,
    [StandardPricePercent]  DECIMAL (18, 2) NULL,
    [StandardPrice]         DECIMAL (18, 2) NULL,
    [WholesalePricePercent] DECIMAL (18, 2) NULL,
    [WholesalePrice]        DECIMAL (18, 2) NULL,
    [SpecialPricePercent]   DECIMAL (18, 2) NULL,
    [SpecialPrice]          DECIMAL (18, 2) NULL,
    [MinimumPricePercent]   DECIMAL (18, 2) NULL,
    [MinimumPrice]          DECIMAL (18, 2) NULL,
    [StandardCost]          DECIMAL (18, 2) NULL,
    [RowIndex]              SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[Product_PriceList_Detail] (
    [ProductID]  NVARCHAR (64)   NOT NULL,
    [UnitPrice1] DECIMAL (18, 5) NULL,
    [UnitPrice2] DECIMAL (18, 5) NULL,
    [UnitPrice3] DECIMAL (18, 5) NULL,
    [MinPrice]   DECIMAL (18, 5) NULL,
    [UnitID]     NVARCHAR (15)   NULL,
    [LocationID] NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Product_Size] (
    [SizeID]      NVARCHAR (15) NOT NULL,
    [SizeName]    NVARCHAR (30) NOT NULL,
    [IsInactive]  BIT           CONSTRAINT [DF_Product_Size_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Product_Size] PRIMARY KEY CLUSTERED ([SizeID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Specification] (
    [SpecificationID]   NVARCHAR (15) NOT NULL,
    [SpecificationName] NVARCHAR (30) NOT NULL,
    [IsInactive]        BIT           CONSTRAINT [DF_Product_Specification_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated]       DATETIME      NULL,
    [DateUpdated]       DATETIME      NULL,
    [CreatedBy]         NVARCHAR (15) NULL,
    [UpdatedBy]         NVARCHAR (15) NULL,
    CONSTRAINT [PK_Product_Specification] PRIMARY KEY CLUSTERED ([SpecificationID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Style] (
    [StyleID]     NVARCHAR (15) NOT NULL,
    [StyleName]   NVARCHAR (30) NOT NULL,
    [IsInactive]  BIT           CONSTRAINT [DF_Product_Style_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_Product_Style] PRIMARY KEY CLUSTERED ([StyleID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Substitute_Detail] (
    [ProductID]             NVARCHAR (64)   NOT NULL,
    [SubstituteProductID]   NVARCHAR (64)   NULL,
    [SubProductDescription] NVARCHAR (255)  NULL,
    [UnitPrice]             DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Product_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [MakeID]      NVARCHAR (15)  NULL,
    [IsInactive]  BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Product_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Product_Unit] (
    [ProductID]  NVARCHAR (64) NOT NULL,
    [UnitID]     NVARCHAR (15) NOT NULL,
    [FactorType] CHAR (1)      NULL,
    [Factor]     REAL          NULL,
    [IsMainUnit] BIT           NULL,
    CONSTRAINT [PK_Product_Unit] PRIMARY KEY CLUSTERED ([ProductID] ASC, [UnitID] ASC)
);

GO
CREATE TABLE [dbo].[Project_Expense_Allocation] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [Name]               NVARCHAR (64)  NULL,
    [Month]              TINYINT        NULL,
    [Year]               SMALLINT       NULL,
    [TransactionDate]    DATETIME       NULL,
    [IsVoid]             BIT            NULL,
    [Reference]          NVARCHAR (15)  NULL,
    [TransactionStatus]  TINYINT        CONSTRAINT [DF_Project_Expense_Allocation_TransactionStatus] DEFAULT ((1)) NULL,
    [Description]        NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Project_Expense_Allocation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Project_Expense_Allocation_Detail] (
    [SysDocID]       NVARCHAR (7)    NOT NULL,
    [VoucherID]      NVARCHAR (15)   NOT NULL,
    [EmployeeID]     NVARCHAR (64)   NULL,
    [OTAmount]       MONEY           NULL,
    [GrossSalary]    MONEY           NULL,
    [ProjectID]      NVARCHAR (30)   NULL,
    [CostCategoryID] NVARCHAR (30)   NULL,
    [Hours]          NUMERIC (15, 2) NULL,
    [RowIndex]       INT             NULL,
    [Amount]         MONEY           NULL,
    [IsVoid]         BIT             NULL,
    [SheetSysDocID]  NVARCHAR (15)   NULL,
    [SheetVoucherID] NVARCHAR (15)   NULL,
    [SheetRowIndex]  SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[Project_SubContract_PI] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [IsCash]             BIT             NULL,
    [IsImport]           BIT             NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [DueDate]            DATETIME        NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [BOLNo]              NVARCHAR (20)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Project_SubContract_PI_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [SourceDocType]      TINYINT         NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [ContainerNumber]    NVARCHAR (20)   NULL,
    [Port]               NVARCHAR (15)   NULL,
    [BOLNumber]          NVARCHAR (20)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [Shipper]            NVARCHAR (15)   NULL,
    [ClearingAgent]      NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [Note]               NVARCHAR (255)  NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Project_SubContract_PI] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Project_SubContract_PI_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [OrderValue]       DECIMAL (18, 5) NULL,
    [Invoiced]         DECIMAL (18, 5) NULL,
    [PercentCompleted] DECIMAL (5, 2)  NULL,
    [CurrentValue]     DECIMAL (18, 5) NULL,
    [CurrentPercent]   DECIMAL (5, 2)  NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [PORVoucherID]     NVARCHAR (15)   NULL,
    [PORSysDocID]      NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsPORRow]         BIT             NULL,
    [LotNumber]        INT             NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [Remarks]          NVARCHAR (300)  NULL
);

GO
CREATE TABLE [dbo].[Project_Subcontract_PO] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [VendorID]           NVARCHAR (64)  NOT NULL,
    [IsImport]           BIT            NULL,
    [TransactionDate]    DATETIME       NOT NULL,
    [PurchaseFlow]       TINYINT        NULL,
    [DueDate]            DATETIME       NULL,
    [ContainerSizeID]    NVARCHAR (15)  NULL,
    [BuyerID]            NVARCHAR (64)  NULL,
    [StartDate]          DATETIME       NULL,
    [EndDate]            DATETIME       NULL,
    [ShippingAddressID]  NVARCHAR (15)  NULL,
    [VendorAddress]      NVARCHAR (255) NULL,
    [Status]             TINYINT        CONSTRAINT [DF_Project_Subcontract_PO_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)   NULL,
    [TermID]             NVARCHAR (15)  NULL,
    [ShippingMethodID]   NVARCHAR (15)  NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)  NULL,
    [TaxOption]          TINYINT        NULL,
    [TaxGroupID]         NVARCHAR (15)  NULL,
    [IsVoid]             BIT            NULL,
    [SourceDocType]      TINYINT        NULL,
    [Reference]          NVARCHAR (20)  NULL,
    [Reference2]         NVARCHAR (20)  NULL,
    [PortLoading]        NVARCHAR (15)  NULL,
    [PortDestination]    NVARCHAR (15)  NULL,
    [ETA]                DATETIME       NULL,
    [ETD]                DATETIME       NULL,
    [ActualReqDate]      DATETIME       NULL,
    [INCOID]             NVARCHAR (15)  NULL,
    [Discount]           MONEY          NULL,
    [TaxAmount]          MONEY          NULL,
    [TaxAmountFC]        MONEY          NULL,
    [Total]              MONEY          NULL,
    [PONumber]           NVARCHAR (15)  NULL,
    [IsShipped]          BIT            NULL,
    [BOLNo]              NVARCHAR (20)  NULL,
    [JobID]              NVARCHAR (50)  NULL,
    [CostCategoryID]     NVARCHAR (30)  NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Project_Subcontract_PO] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Project_Subcontract_PO_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Quantity]         DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsSourcedRow]     BIT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [Remarks]          NVARCHAR (300)  NULL
);

GO
CREATE TABLE [dbo].[Property] (
    [PropertyID]                 NVARCHAR (15)   NOT NULL,
    [PropertyName]               NVARCHAR (64)   NOT NULL,
    [ShortName]                  NVARCHAR (30)   NULL,
    [OfferTypeID]                NVARCHAR (15)   NULL,
    [FixedAssetID]               NVARCHAR (15)   NULL,
    [PropertyClassID]            NVARCHAR (15)   NULL,
    [CountryID]                  NVARCHAR (15)   NULL,
    [CityID]                     NVARCHAR (15)   NULL,
    [AreaID]                     NVARCHAR (15)   NULL,
    [Address1]                   NVARCHAR (255)  NULL,
    [Address2]                   NVARCHAR (255)  NULL,
    [YearBuilt]                  SMALLINT        NULL,
    [Builtby]                    NVARCHAR (64)   NULL,
    [LandArea]                   DECIMAL (18, 2) NULL,
    [BuildArea]                  DECIMAL (18, 2) NULL,
    [OwnerName]                  NVARCHAR (64)   NULL,
    [RegisterNumber]             NVARCHAR (30)   NULL,
    [RentIncomeAccountID]        NVARCHAR (15)   NULL,
    [PrepaidRentIncomeAccountID] NVARCHAR (15)   NULL,
    [Note]                       NTEXT           NULL,
    [Status]                     BIT             NULL,
    [IsPeriodicInvoice]          BIT             NULL,
    [TaxOption]                  TINYINT         NULL,
    [TaxGroupID]                 NVARCHAR (15)   NULL,
    [TaxIDNumber]                NVARCHAR (30)   NULL,
    [LocationID]                 NVARCHAR (15)   NULL,
    [Photo]                      IMAGE           NULL,
    [ElectricityRegnNumber]      NVARCHAR (50)   NULL,
    [TelecomRegnNumber]          NVARCHAR (50)   NULL,
    [MunicipalityRegnNumber]     NVARCHAR (50)   NULL,
    [ElectricityPremiseNumber]   NVARCHAR (50)   NULL,
    [ElectricityContractNumber]  NVARCHAR (50)   NULL,
    [DateCreated]                DATETIME        NULL,
    [DateUpdated]                DATETIME        NULL,
    [CreatedBy]                  NVARCHAR (15)   NULL,
    [UpdatedBy]                  NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([PropertyID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Agent] (
    [PropertyAgentID]   NVARCHAR (15)  NOT NULL,
    [PropertyAgentName] NVARCHAR (64)  NOT NULL,
    [Note]              NVARCHAR (255) NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    CONSTRAINT [PK_Property_Agent] PRIMARY KEY CLUSTERED ([PropertyAgentID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Cancel] (
    [SysDocID]          NVARCHAR (7)   NOT NULL,
    [VoucherID]         NVARCHAR (15)  NOT NULL,
    [TransactionDate]   DATETIME       NULL,
    [PropertyID]        NVARCHAR (15)  NULL,
    [UnitID]            NVARCHAR (15)  NULL,
    [CustomerID]        NVARCHAR (15)  NULL,
    [ContractStartDate] DATETIME       NULL,
    [ContractEndDate]   DATETIME       NULL,
    [TotalDays]         NUMERIC (10)   NULL,
    [LastStayDate]      DATETIME       NULL,
    [SourceSysDocID]    NVARCHAR (7)   NULL,
    [SourceVoucherID]   NVARCHAR (15)  NULL,
    [ParentSysDocID]    NVARCHAR (7)   NULL,
    [ParentVoucherID]   NVARCHAR (15)  NULL,
    [AgreementType]     NVARCHAR (15)  NULL,
    [AgreementStatus]   NVARCHAR (15)  NULL,
    [Status]            TINYINT        NULL,
    [Note]              NVARCHAR (255) NULL,
    [TaxOption]         TINYINT        NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)  NULL,
    [TaxAmount]         MONEY          NULL,
    [PropertyAgentID]   NVARCHAR (15)  NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Cancel] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Cancel_Detail] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [IncomeID]     NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (50)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [TaxOption]    TINYINT         NULL,
    [TaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]    DECIMAL (18, 5) NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Property_Category] (
    [CategoryID]   NVARCHAR (15)  NOT NULL,
    [CategoryName] NVARCHAR (15)  NULL,
    [Note]         NVARCHAR (255) NULL,
    [Inactive]     BIT            CONSTRAINT [DF_Property_Category_Inactive] DEFAULT ((0)) NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    CONSTRAINT [PK_Property_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Category_Detail] (
    [PropertyID] NVARCHAR (64) NOT NULL,
    [CategoryID] NVARCHAR (15) NOT NULL,
    [EntityType] TINYINT       NOT NULL,
    CONSTRAINT [PK_Property_Category_Detail] PRIMARY KEY CLUSTERED ([PropertyID] ASC, [CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Class] (
    [PropertyClassID]   NVARCHAR (15)  NOT NULL,
    [PropertyClassName] NVARCHAR (64)  NOT NULL,
    [Note]              NVARCHAR (255) NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    CONSTRAINT [PK_Property_Class] PRIMARY KEY CLUSTERED ([PropertyClassID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Property_Docs_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Document] (
    [PropertyID]     NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Docs] PRIMARY KEY CLUSTERED ([PropertyID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Property_Facility] (
    [PropertyID]  NVARCHAR (64) NOT NULL,
    [FacilityID]  NVARCHAR (15) NOT NULL,
    [Type]        TINYINT       CONSTRAINT [DF_Property_Facility_Type] DEFAULT ((1)) NULL,
    [DateCreated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Property_Owner_Detail] (
    [PropertyID]       NVARCHAR (15)   NOT NULL,
    [PropertyOwnerID]  NVARCHAR (15)   NOT NULL,
    [Description]      NVARCHAR (64)   NULL,
    [OwnerShipPercent] DECIMAL (15, 3) NULL,
    [RowIndex]         INT             NULL
);

GO
CREATE TABLE [dbo].[Property_Refund_Detail] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [IncomeID]     NVARCHAR (50)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [PaidAmount]   MONEY           NULL,
    [Charges]      MONEY           NULL,
    [RefundAmount] MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (50)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [TaxOption]    TINYINT         NULL,
    [TaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]    DECIMAL (18, 5) NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Property_Rent] (
    [SysDocID]          NVARCHAR (7)   NOT NULL,
    [VoucherID]         NVARCHAR (15)  NOT NULL,
    [TransactionDate]   DATETIME       NULL,
    [PropertyID]        NVARCHAR (15)  NULL,
    [UnitID]            NVARCHAR (15)  NULL,
    [CustomerID]        NVARCHAR (15)  NULL,
    [ContractStartDate] DATETIME       NULL,
    [ContractEndDate]   DATETIME       NULL,
    [TotalDays]         NUMERIC (10)   NULL,
    [NoofInstallments]  INT            NULL,
    [SourceSysDocID]    NVARCHAR (7)   NULL,
    [SourceVoucherID]   NVARCHAR (15)  NULL,
    [ParentSysDocID]    NVARCHAR (7)   NULL,
    [ParentVoucherID]   NVARCHAR (15)  NULL,
    [Total]             MONEY          NULL,
    [AgreementType]     NVARCHAR (15)  NULL,
    [AgreementStatus]   NVARCHAR (15)  NULL,
    [Status]            TINYINT        NULL,
    [Note]              NVARCHAR (255) NULL,
    [TaxOption]         TINYINT        NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)  NULL,
    [TaxAmount]         MONEY          NULL,
    [PropertyAgentID]   NVARCHAR (15)  NULL,
    [InvoiceStartDate]  DATETIME       NULL,
    [IsPeriodicInvoice] BIT            NULL,
    [Frequency]         CHAR (1)       NULL,
    [FrequencyCount]    TINYINT        NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Rent] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Rent_Detail] (
    [SysDocID]     NVARCHAR (7)    NULL,
    [VoucherID]    NVARCHAR (15)   NULL,
    [IncomeID]     NVARCHAR (15)   NULL,
    [Description]  NVARCHAR (64)   NULL,
    [UnitPrice]    DECIMAL (18, 5) NULL,
    [Amount]       MONEY           NULL,
    [AmountFC]     MONEY           NULL,
    [Reference]    NVARCHAR (50)   NULL,
    [CurrencyID]   NVARCHAR (15)   NULL,
    [CurrencyRate] DECIMAL (18, 5) NULL,
    [RateType]     CHAR (1)        NULL,
    [Discount]     MONEY           NULL,
    [TaxOption]    TINYINT         NULL,
    [TaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]    DECIMAL (18, 5) NULL,
    [RowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Property_Service_Assign] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]    NVARCHAR (7)    NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [ServiceProviderID] NVARCHAR (15)   NULL,
    [PlannedDate]       DATETIME        NULL,
    [StatusDate]        DATETIME        NULL,
    [Status]            TINYINT         NULL,
    [Amount]            DECIMAL (18, 4) NULL,
    [Remarks]           NVARCHAR (255)  NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Property_Service_Assign] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Service_Request] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [ReportingDate]      DATETIME       NULL,
    [PropertyID]         NVARCHAR (15)  NULL,
    [UnitID]             NVARCHAR (15)  NULL,
    [TenantID]           NVARCHAR (15)  NULL,
    [PriorityStatus]     NCHAR (10)     NULL,
    [RequiredDatetime]   DATETIME       NULL,
    [ConvenientDatetime] DATETIME       NULL,
    [RequestNotes]       NVARCHAR (255) NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Service_Request] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Property_ServiceFacility_Detail] (
    [SysDocID]   NVARCHAR (7)  NOT NULL,
    [VoucherID]  NVARCHAR (15) NOT NULL,
    [RowIndex]   INT           NULL,
    [FacilityID] NVARCHAR (30) NULL
);

GO
CREATE TABLE [dbo].[Property_ServiceType_Detail] (
    [SysDocID]      NVARCHAR (7)  NOT NULL,
    [VoucherID]     NVARCHAR (15) NOT NULL,
    [RowIndex]      INT           NULL,
    [ServiceTypeID] NVARCHAR (30) NULL
);

GO
CREATE TABLE [dbo].[Property_Tenant_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Property_Tenant_Docs_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Tenant_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Tenant_Document] (
    [CustomerID]     NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_Tenant_Docs] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Property_Transaction] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [BankAccountID]     NVARCHAR (64)   NULL,
    [Amount]            MONEY           NULL,
    [AmountFC]          MONEY           NULL,
    [Name]              NVARCHAR (64)   NULL,
    [Month]             TINYINT         NULL,
    [StartDate]         DATETIME        NULL,
    [EndDate]           DATETIME        NULL,
    [IsDebit]           BIT             NULL,
    [PaymentMethodType] TINYINT         NULL,
    [TransactionDate]   DATETIME        NULL,
    [IsVoid]            BIT             NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [GLType]            TINYINT         NULL,
    [JournalID]         INT             NULL,
    [ChequeID]          INT             NULL,
    [ChequebookID]      NVARCHAR (15)   NULL,
    [CheckNumber]       NVARCHAR (15)   NULL,
    [CheckDate]         DATETIME        NULL,
    [Reference]         NVARCHAR (15)   NULL,
    [TransactionStatus] TINYINT         CONSTRAINT [DF_Property_Transaction_TransactionStatus] DEFAULT ((1)) NULL,
    [Description]       NVARCHAR (255)  NULL,
    [TypeID]            INT             NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Property_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Property_Transaction_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [TenantID]          NVARCHAR (64)   NULL,
    [SourceSysDocID]    NVARCHAR (15)   NULL,
    [SourceVoucherID]   NVARCHAR (15)   NULL,
    [IncomeType]        TINYINT         NULL,
    [IncomeID]          NVARCHAR (15)   NULL,
    [PayType]           TINYINT         NULL,
    [AccountID]         NVARCHAR (64)   NULL,
    [Days]              DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (255)  NULL,
    [PaymentMethodType] TINYINT         NULL,
    [Amount]            MONEY           NULL,
    [AmountFC]          MONEY           NULL,
    [RowIndex]          SMALLINT        NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [AnalysisID]        NVARCHAR (15)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL
);

GO
CREATE TABLE [dbo].[Property_Unit] (
    [PropertyUnitID]           NVARCHAR (15)   NOT NULL,
    [PropertyUnitName]         NVARCHAR (64)   NOT NULL,
    [ShortName]                NVARCHAR (30)   NULL,
    [UnitStatus]               NVARCHAR (15)   NULL,
    [AvailableFrom]            DATETIME        NULL,
    [AvailableTo]              DATETIME        NULL,
    [PropertyID]               NVARCHAR (15)   NULL,
    [ParentUnitID]             NVARCHAR (15)   NULL,
    [UnitTypeID]               NVARCHAR (15)   NULL,
    [NoBedRooms]               INT             NULL,
    [NoBathRooms]              INT             NULL,
    [TotalRooms]               INT             NULL,
    [Area]                     DECIMAL (18, 2) NULL,
    [NoofParking]              INT             NULL,
    [ViewTypeID]               NVARCHAR (15)   NULL,
    [KitchenTypeID]            NVARCHAR (15)   NULL,
    [Note]                     NTEXT           NULL,
    [Status]                   BIT             NULL,
    [TaxOption]                TINYINT         NULL,
    [TaxGroupID]               NVARCHAR (15)   NULL,
    [Photo]                    IMAGE           NULL,
    [PropertyType]             TINYINT         NULL,
    [ElectricityPremiseNumber] NVARCHAR (50)   NULL,
    [MunicipalityFileNumber]   NVARCHAR (50)   NULL,
    [ElectricityFileNumber]    NVARCHAR (50)   NULL,
    [MunicipalityPermitNumber] NVARCHAR (50)   NULL,
    [ElectricityPermitNumber]  NVARCHAR (50)   NULL,
    [RentalIncome]             MONEY           NULL,
    [IsVirtual]                BIT             NULL,
    [DateCreated]              DATETIME        NULL,
    [DateUpdated]              DATETIME        NULL,
    [CreatedBy]                NVARCHAR (15)   NULL,
    [UpdatedBy]                NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Property_Unit] PRIMARY KEY CLUSTERED ([PropertyUnitID] ASC)
);

GO
CREATE TABLE [dbo].[Property_VirtualUnit] (
    [PropertyVirtualUnitID]   NVARCHAR (30)  NOT NULL,
    [PropertyVirtualUnitName] NVARCHAR (150) NULL,
    [IsInactive]              BIT            NULL,
    [Note]                    NVARCHAR (50)  NULL,
    [DateCreated]             DATETIME       NULL,
    [DateUpdated]             DATETIME       NULL,
    [CreatedBy]               NVARCHAR (15)  NULL,
    [UpdatedBy]               NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Property_VirtualUnit] PRIMARY KEY CLUSTERED ([PropertyVirtualUnitID] ASC)
);

GO
CREATE TABLE [dbo].[Property_VirtualUnit_Detail] (
    [PropertyVirtualUnitID] NVARCHAR (64)   NOT NULL,
    [PropertyUnitID]        NVARCHAR (64)   NULL,
    [Description]           NVARCHAR (255)  NULL,
    [SharingPercent]        DECIMAL (15, 3) NULL,
    [RowIndex]              SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[PropertyIncome_Code] (
    [IncomeID]    NVARCHAR (15) NOT NULL,
    [IncomeName]  NVARCHAR (64) NULL,
    [Description] NVARCHAR (64) NULL,
    [AccountID]   NVARCHAR (64) NULL,
    [IncomeType]  TINYINT       NULL,
    [IncomeRate]  MONEY         NULL,
    [Inactive]    BIT           NULL,
    [TaxOption]   TINYINT       NULL,
    [TaxGroupID]  NVARCHAR (15) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    CONSTRAINT [PK_PropertyIncome_Code] PRIMARY KEY CLUSTERED ([IncomeID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Claim] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [DivisionID]      NVARCHAR (15)  NULL,
    [CompanyID]       TINYINT        NULL,
    [SourceSysDocID]  NVARCHAR (50)  NULL,
    [SourceVoucherID] NVARCHAR (50)  NULL,
    [TransactionDate] DATETIME       NULL,
    [ClaimAmount]     MONEY          NULL,
    [ClaimDetails]    NVARCHAR (255) NULL,
    [CurrencyID]      NVARCHAR (7)   NULL,
    [CreditNoteNo]    NVARCHAR (30)  NULL,
    [CRNoteAmount]    MONEY          NULL,
    [ClaimStatus]     TINYINT        NULL,
    [IsVoid]          BIT            NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Purchase_Claim] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Cost_Entry] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [DivisionID]       NVARCHAR (15)   NULL,
    [CompanyID]        TINYINT         NULL,
    [TransactionDate]  DATETIME        NOT NULL,
    [PurchaseFlow]     TINYINT         NULL,
    [ContainerNumber]  NVARCHAR (15)   NULL,
    [Port]             NVARCHAR (15)   NULL,
    [LoadingPort]      NVARCHAR (15)   NULL,
    [ETA]              DATETIME        NULL,
    [ATD]              DATETIME        NULL,
    [Status]           TINYINT         CONSTRAINT [DF_Purchase_Cost_Entry_Status] DEFAULT ((1)) NULL,
    [ShippingMethodID] NVARCHAR (15)   NULL,
    [IsVoid]           BIT             NULL,
    [Reference]        NVARCHAR (20)   NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [VendorID]         NVARCHAR (64)   NULL,
    [PONumber]         NVARCHAR (20)   NULL,
    [BOLNumber]        NVARCHAR (20)   NULL,
    [Shipper]          NVARCHAR (15)   NULL,
    [ClearingAgent]    NVARCHAR (30)   NULL,
    [Weight]           DECIMAL (18, 5) NULL,
    [IsReceived]       BIT             NULL,
    [Note]             NVARCHAR (4000) NULL,
    [Value]            MONEY           NULL,
    [ShipStatus]       BIT             NULL,
    [TransporterID]    NVARCHAR (50)   NULL,
    [ContainerSizeID]  NVARCHAR (30)   NULL,
    [ApprovalStatus]   TINYINT         NULL,
    [CurrencyID]       NVARCHAR (5)    NULL,
    [BuyerID]          NVARCHAR (64)   NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [Discount]         MONEY           NULL,
    [DiscountFC]       MONEY           NULL,
    [TaxAmount]        MONEY           NULL,
    [TaxAmountFC]      MONEY           NULL,
    [Total]            MONEY           NULL,
    [TotalFC]          MONEY           NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Cost_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Cost_Entry_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [BOLNumber]       NVARCHAR (20)   NULL,
    [ExpenseID]       NVARCHAR (15)   NULL,
    [Description]     NVARCHAR (64)   NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [SupplierID]      NVARCHAR (15)   NULL,
    [Amount]          MONEY           NULL,
    [Cost]            DECIMAL (10, 5) NULL,
    [DueDate]         DATETIME        NULL,
    [AmountFC]        MONEY           NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [CurrencyID]      NCHAR (10)      NULL,
    [CurrencyRate]    DECIMAL (18, 5) NULL,
    [RateType]        CHAR (1)        NULL,
    [RowIndex]        SMALLINT        NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [AllocatedCost]   DECIMAL (10, 5) NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Purchase_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [IsCash]             BIT             NULL,
    [IsImport]           BIT             NULL,
    [IsTaxIncluded]      BIT             NULL,
    [IsHoldForPayment]   BIT             NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [DueDate]            DATETIME        NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    [PriceIncludeTax]    BIT             NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Purchase_Invoice_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [SourceDocType]      TINYINT         NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (30)   NULL,
    [VendorReferenceNo]  NVARCHAR (40)   NULL,
    [ContainerNumber]    NVARCHAR (20)   NULL,
    [Port]               NVARCHAR (15)   NULL,
    [BOLNumber]          NVARCHAR (20)   NULL,
    [Shipper]            NVARCHAR (15)   NULL,
    [ClearingAgent]      NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Invoice_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Discount]         MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [PORVoucherID]     NVARCHAR (15)   NULL,
    [PORSysDocID]      NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsPORRow]         BIT             NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsSourcedRow]     BIT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [LotNumber]        INT             NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [ITRowID]          INT             NULL,
    [RefSlNo]          INT             NULL,
    [RefText1]         NVARCHAR (50)   NULL,
    [RefText2]         NVARCHAR (50)   NULL,
    [RefNum1]          DECIMAL (18, 5) NULL,
    [RefNum2]          DECIMAL (18, 5) NULL,
    [RefDate1]         DATETIME        NULL,
    [RefDate2]         DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Invoice_Expense] (
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [ExpenseID]        NVARCHAR (15)   NULL,
    [Description]      NVARCHAR (64)   NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [Reference]        NVARCHAR (15)   NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        MONEY           NULL,
    [RateType]         CHAR (1)        NULL,
    [PCSysDocID]       NVARCHAR (7)    NULL,
    [PCVoucherID]      NVARCHAR (15)   NULL,
    [PCRowIndex]       SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Invoice_NonInv] (
    [SysDocID]              NVARCHAR (7)    NOT NULL,
    [VoucherID]             NVARCHAR (15)   NOT NULL,
    [DivisionID]            NVARCHAR (15)   NULL,
    [CompanyID]             TINYINT         NULL,
    [VendorID]              NVARCHAR (64)   NOT NULL,
    [IsCash]                BIT             NULL,
    [IsImport]              BIT             NULL,
    [PurchaseFlow]          TINYINT         NULL,
    [RegisterID]            NVARCHAR (15)   NULL,
    [TransactionDate]       DATETIME        NOT NULL,
    [DueDate]               DATETIME        NULL,
    [BuyerID]               NVARCHAR (64)   NULL,
    [ShippingMethodID]      NVARCHAR (15)   NULL,
    [PONumber]              NVARCHAR (50)   NULL,
    [PriceIncludeTax]       BIT             NULL,
    [BOLNo]                 NVARCHAR (20)   NULL,
    [Status]                TINYINT         CONSTRAINT [DF_Purchase_Invoice_NonInv_Status] DEFAULT ((1)) NULL,
    [CurrencyID]            NVARCHAR (5)    NULL,
    [CurrencyRate]          DECIMAL (18, 5) NULL,
    [PayeeTaxGroupID]       NVARCHAR (15)   NULL,
    [TermID]                NVARCHAR (15)   NULL,
    [IsVoid]                BIT             NULL,
    [SourceDocType]         TINYINT         NULL,
    [Reference]             NVARCHAR (20)   NULL,
    [Reference2]            NVARCHAR (20)   NULL,
    [ContainerNumber]       NVARCHAR (20)   NULL,
    [Port]                  NVARCHAR (15)   NULL,
    [BOLNumber]             NVARCHAR (20)   NULL,
    [SupplierInvoiceNumber] NVARCHAR (40)   NULL,
    [JobID]                 NVARCHAR (50)   NULL,
    [CostCategoryID]        NVARCHAR (30)   NULL,
    [Shipper]               NVARCHAR (15)   NULL,
    [ClearingAgent]         NVARCHAR (20)   NULL,
    [Discount]              MONEY           NULL,
    [DiscountFC]            MONEY           NULL,
    [TaxOption]             TINYINT         NULL,
    [TaxAmount]             MONEY           NULL,
    [TaxAmountFC]           MONEY           NULL,
    [Total]                 MONEY           NULL,
    [TotalFC]               MONEY           NULL,
    [ApprovalStatus]        TINYINT         NULL,
    [VerificationStatus]    TINYINT         NULL,
    [Note]                  NVARCHAR (4000) NULL,
    [DateCreated]           DATETIME        NULL,
    [DateUpdated]           DATETIME        NULL,
    [CreatedBy]             NVARCHAR (15)   NULL,
    [UpdatedBy]             NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Invoice_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Invoice_NonInv_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [AnalysisID]       NVARCHAR (15)   NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [PORVoucherID]     NVARCHAR (15)   NULL,
    [PORSysDocID]      NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsPORRow]         BIT             NULL,
    [LotNumber]        INT             NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [AttributeID1]     NVARCHAR (50)   NULL,
    [AttributeID2]     NVARCHAR (50)   NULL
);

GO
CREATE TABLE [dbo].[Purchase_Invoice_NonInv_Expense] (
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [ExpenseID]        NVARCHAR (15)   NULL,
    [Description]      NVARCHAR (64)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [Reference]        NVARCHAR (15)   NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [RateType]         CHAR (1)        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Order] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [IsImport]           BIT             NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [ContainerSizeID]    NVARCHAR (15)   NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [PriceIncludeTax]    BIT             NULL,
    [DeliveryAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [DeliveryAddress]    NVARCHAR (255)  NULL,
    [VendorAddress]      NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Purchase_Order_Status] DEFAULT ((1)) NOT NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (50)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [LocationID]         NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [SourceDocType]      TINYINT         NULL,
    [Reference]          NVARCHAR (100)  NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [VendorReferenceNo]  NVARCHAR (40)   NULL,
    [PortLoading]        NVARCHAR (15)   NULL,
    [TaxOption]          TINYINT         NULL,
    [TaxGroupID]         NVARCHAR (15)   NULL,
    [TaxPercentage]      DECIMAL (18, 5) NULL,
    [PortDestination]    NVARCHAR (15)   NULL,
    [ETA]                DATETIME        NULL,
    [ETD]                DATETIME        NULL,
    [ActualReqDate]      DATETIME        NULL,
    [INCOID]             NVARCHAR (15)   NULL,
    [Discount]           MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [IsShipped]          BIT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [Remarks1]           NVARCHAR (2000) NULL,
    [Remarks2]           NVARCHAR (2000) NULL,
    [Note]               NVARCHAR (4000) NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Order_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [MinGuarantee]     DECIMAL (18, 5) NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [Length]           DECIMAL (18, 5) NULL,
    [Width]            DECIMAL (18, 5) NULL,
    [Height]           DECIMAL (18, 5) NULL,
    [Number]           DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsSourcedRow]     BIT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [RefSlNo]          INT             NULL,
    [RefText1]         NVARCHAR (50)   NULL,
    [RefText2]         NVARCHAR (50)   NULL,
    [RefNum1]          DECIMAL (18, 5) NULL,
    [RefNum2]          DECIMAL (18, 5) NULL,
    [RefDate1]         DATETIME        NULL,
    [RefDate2]         DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Order_NonInv] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [IsImport]           BIT             NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [ContainerSizeID]    NVARCHAR (15)   NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [VendorAddress]      NVARCHAR (255)  NULL,
    [PriceIncludeTax]    BIT             NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Purchase_Order_NonInv_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [VendorReferenceNo]  NVARCHAR (40)   NULL,
    [PortLoading]        NVARCHAR (15)   NULL,
    [PortDestination]    NVARCHAR (15)   NULL,
    [ETA]                DATETIME        NULL,
    [ETD]                DATETIME        NULL,
    [ActualReqDate]      DATETIME        NULL,
    [INCOID]             NVARCHAR (15)   NULL,
    [Discount]           MONEY           NULL,
    [TaxOption]          TINYINT         NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsShipped]          BIT             NULL,
    [BOLNo]              NVARCHAR (20)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [Note]               NVARCHAR (4000) NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Order_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Order_NonInv_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [IsSourcedRow]     BIT             NULL,
    [SourceDocType]    TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [Remarks]          NVARCHAR (3000) NULL
);

GO
CREATE TABLE [dbo].[Purchase_Payment_Invoice] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]  NVARCHAR (50)   NULL,
    [SourceVoucherID] NVARCHAR (50)   NULL,
    [TransactionDate] DATETIME        NULL,
    [POAmount]        MONEY           NULL,
    [Paid]            MONEY           NULL,
    [Balance]         MONEY           NULL,
    [CurrencyID]      NVARCHAR (7)    NULL,
    [TermID]          NVARCHAR (15)   NULL,
    [SuggestedDue]    MONEY           NULL,
    [Amount]          DECIMAL (18, 2) NULL,
    [IsVoid]          BIT             NULL,
    [DateCreated]     DATETIME        NULL,
    [DateUpdated]     DATETIME        NULL,
    [CreatedBy]       NVARCHAR (15)   NULL,
    [UpdatedBy]       NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Payment_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Prepayment_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [VendorID]           NVARCHAR (64)   NULL,
    [SourceSysDocID]     NVARCHAR (50)   NULL,
    [SourceVoucherID]    NVARCHAR (50)   NULL,
    [TransactionDate]    DATETIME        NULL,
    [POAmount]           MONEY           NULL,
    [Paid]               MONEY           NULL,
    [Balance]            MONEY           NULL,
    [CurrencyID]         NVARCHAR (7)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [DueDate]            DATETIME        NULL,
    [PrepaymentTermID]   NVARCHAR (15)   NULL,
    [SuggestedDue]       MONEY           NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [IsVoid]             BIT             NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [Status]             TINYINT         NULL,
    [Remarks]            NVARCHAR (500)  NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Prepayment_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Quote] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [VendorID]          NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [IsImport]          BIT             NULL,
    [DueDate]           DATETIME        NULL,
    [PurchaseFlow]      TINYINT         NULL,
    [BuyerID]           NVARCHAR (64)   NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [VendorAddress]     NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Purchase_Quote_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [PriceIncludeTax]   BIT             NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [VendorReferenceNo] NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [TaxOption]         TINYINT         NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [Note]              NVARCHAR (4000) NULL,
    [ApprovalStatus]    TINYINT         NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Quote] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Quote_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [QuantityOrdered] DECIMAL (18, 5) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceRowIndex]  SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Receipt] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [VendorID]           NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [BuyerID]            NVARCHAR (64)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [PurchaseFlow]       TINYINT         NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_PO_Receipt_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [IsImport]           BIT             NULL,
    [IsInvoiced]         BIT             NULL,
    [SourceDocType]      TINYINT         NULL,
    [SourceSysDocID]     NVARCHAR (7)    NULL,
    [SourceVoucherID]    NVARCHAR (15)   NULL,
    [Reference]          NVARCHAR (30)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [VendorReferenceNo]  NVARCHAR (40)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [POSysDocID]         NVARCHAR (7)    NULL,
    [POVoucherID]        NVARCHAR (15)   NULL,
    [TransporterID]      NVARCHAR (30)   NULL,
    [VehicleID]          NVARCHAR (15)   NULL,
    [DriverID]           NVARCHAR (15)   NULL,
    [ContainerNumber]    NVARCHAR (30)   NULL,
    [ContainerSizeID]    NVARCHAR (20)   NULL,
    [ClaimStatus]        TINYINT         NULL,
    [GroupName]          NVARCHAR (64)   NULL,
    [ClaimAmount]        MONEY           NULL,
    [ClaimAmountFC]      MONEY           NULL,
    [ClaimCurrencyID]    NVARCHAR (5)    NULL,
    [ClaimCurrencyRate]  DECIMAL (10, 5) NULL,
    [ClaimRef1]          NVARCHAR (30)   NULL,
    [ClaimRef2]          NVARCHAR (30)   NULL,
    [ClaimRemarks]       NVARCHAR (255)  NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [Note]               NVARCHAR (4000) NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_PO_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Receipt_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [RowIndex]         SMALLINT        NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [IsPORRow]         BIT             NULL,
    [RowSource]        SMALLINT        NULL,
    [PKSysDocID]       NVARCHAR (7)    NULL,
    [PKVoucherID]      NVARCHAR (15)   NULL,
    [PKRowIndex]       INT             NULL,
    [ListVoucherID]    NVARCHAR (15)   NULL,
    [ListSysDocID]     NVARCHAR (7)    NULL,
    [ListRowIndex]     INT             NULL,
    [ITRowID]          INT             NULL,
    [RefSlNo]          INT             NULL,
    [RefText1]         NVARCHAR (50)   NULL,
    [RefText2]         NVARCHAR (50)   NULL,
    [RefNum1]          DECIMAL (18, 5) NULL,
    [RefNum2]          DECIMAL (18, 5) NULL,
    [RefDate1]         DATETIME        NULL,
    [RefDate2]         DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Purchase_Return] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [VendorID]          NVARCHAR (64)   NOT NULL,
    [IsCash]            BIT             CONSTRAINT [DF_Purchase_Return_IsCash] DEFAULT ((0)) NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [BuyerID]           NVARCHAR (64)   NULL,
    [PurchaseFlow]      TINYINT         NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [VendorAddress]     NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Purchase_Return_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      SMALLMONEY      NULL,
    [PriceIncludeTax]   BIT             NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxOption]         TINYINT         NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [VendorReferenceNo] NVARCHAR (40)   NULL,
    [Discount]          MONEY           NULL,
    [DiscountFC]        MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Total]             MONEY           NULL,
    [TotalFC]           MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_Purchase_Return_IsDelivered] DEFAULT ((0)) NULL,
    [RequireUpdate]     BIT             NULL,
    [SourceDocType]     TINYINT         NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Purchase_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Purchase_Return_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [DNoteVoucherID]   NVARCHAR (15)   NULL,
    [DNoteSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceRowIndex]   INT             NULL,
    [AmountFC]         MONEY           NULL,
    [Amount]           MONEY           NULL,
    [ITRowID]          INT             NULL
);

GO
CREATE TABLE [dbo].[Quality_Claim] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [VendorID]         NVARCHAR (64)   NULL,
    [ClaimAmount]      MONEY           NULL,
    [ReceivedAmount]   MONEY           NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [Reference]        NVARCHAR (20)   NULL,
    [Reference2]       NVARCHAR (20)   NULL,
    [CurrencyID]       NVARCHAR (5)    NULL,
    [TotalPallets]     INT             NULL,
    [TotalQuantity]    DECIMAL (10, 2) NULL,
    [Note]             NTEXT           NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [Description]      NVARCHAR (255)  NULL,
    [SurveyType]       TINYINT         NULL,
    [DateReceived]     DATETIME        NULL,
    [DateInspected]    DATETIME        NULL,
    [SurveyerID]       NVARCHAR (15)   NULL,
    [Surveyer2ID]      NVARCHAR (15)   NULL,
    [SurveyDate]       DATETIME        NULL,
    [ClaimDate]        DATETIME        NULL,
    [VesselName]       NVARCHAR (20)   NULL,
    [ContainerNumber]  NVARCHAR (20)   NULL,
    [OriginID]         NVARCHAR (15)   NULL,
    [ClaimStatus]      TINYINT         NULL,
    [Status]           TINYINT         NULL,
    [CloseDescription] NVARCHAR (255)  NULL,
    [CRSysDocID]       NVARCHAR (7)    NULL,
    [CRVoucherID]      NVARCHAR (15)   NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [DateCreated]      DATETIME        NULL,
    [DateUpdated]      DATETIME        NULL,
    CONSTRAINT [PK_Quality_Claim] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Quality_Claim_Detail] (
    [SysDocID]         NVARCHAR (7)    NULL,
    [VoucherID]        NVARCHAR (15)   NULL,
    [RowType]          TINYINT         NULL,
    [CommodityID]      NVARCHAR (15)   NULL,
    [VarietyID]        NVARCHAR (15)   NULL,
    [BrandID]          NVARCHAR (15)   NULL,
    [Grade]            NVARCHAR (15)   NULL,
    [IssueName]        NVARCHAR (20)   NULL,
    [IssuePercent]     DECIMAL (8, 2)  NULL,
    [UnitCost]         MONEY           NULL,
    [ReceivedQuantity] DECIMAL (10, 2) NULL,
    [Quantity]         DECIMAL (10, 2) NULL,
    [LossRatio]        DECIMAL (8, 2)  NULL,
    [ClaimRate]        MONEY           NULL,
    [ClaimAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL
);

GO
CREATE TABLE [dbo].[Quality_Task] (
    [TaskID]          NVARCHAR (15)  NOT NULL,
    [ContainerNumber] NVARCHAR (64)  NOT NULL,
    [VendorID]        NVARCHAR (15)  NULL,
    [LocationID]      NVARCHAR (15)  NULL,
    [ReceiveDate]     DATETIME       NULL,
    [GRNSysDocID]     NVARCHAR (30)  NULL,
    [GRNVoucherID]    NVARCHAR (30)  NULL,
    [Description]     NVARCHAR (255) NULL,
    [AssignedTo]      NVARCHAR (64)  NULL,
    [CommodityID]     NVARCHAR (30)  NULL,
    [Status]          BIT            NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    CONSTRAINT [PK_Quality_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

GO
CREATE TABLE [dbo].[Rack] (
    [RackID]      NVARCHAR (30)  NOT NULL,
    [BinID]       NVARCHAR (30)  NOT NULL,
    [RackName]    NVARCHAR (64)  NULL,
    [Inactive]    BIT            NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Rack] PRIMARY KEY CLUSTERED ([RackID] ASC)
);

GO
CREATE TABLE [dbo].[Recurring_Transaction] (
    [TransactionID]   VARCHAR (50)  NOT NULL,
    [SysDocID]        NVARCHAR (7)  NULL,
    [VoucherID]       NVARCHAR (15) NULL,
    [SysDocType]      INT           NULL,
    [StartDate]       DATE          NULL,
    [EndDate]         DATE          NULL,
    [RepeateEvery]    CHAR (1)      NULL,
    [Interval]        TINYINT       NULL,
    [LastRunDate]     DATE          NULL,
    [LastSysDocID]    NVARCHAR (7)  NULL,
    [LastVoucherID]   NVARCHAR (15) NULL,
    [SourceSysDocID]  NVARCHAR (7)  NULL,
    [SourceVoucherID] NVARCHAR (15) NULL,
    [ProcessedBy]     VARCHAR (50)  NULL,
    [Status]          BIT           NULL,
    [DateCreated]     DATETIME      NULL,
    [DateUpdated]     DATETIME      NULL,
    [CreatedBy]       NVARCHAR (15) NULL,
    [UpdatedBy]       NVARCHAR (15) NULL,
    CONSTRAINT [PK_Recurring_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC)
);

GO
CREATE TABLE [dbo].[Recurring_Transaction_Detail] (
    [TransactionID]    VARCHAR (50)  NOT NULL,
    [TransactionDate]  DATETIME      NULL,
    [SysDocID]         NVARCHAR (7)  NULL,
    [VoucherID]        NVARCHAR (15) NULL,
    [CreatedSysDocID]  NVARCHAR (7)  NULL,
    [CreatedVoucherID] NVARCHAR (15) NULL,
    [DateCreated]      DATETIME      NULL,
    [DateUpdated]      DATETIME      NULL,
    [CreatedBy]        NVARCHAR (15) NULL,
    [UpdatedBy]        NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Register] (
    [RegisterID]            NVARCHAR (15)  NOT NULL,
    [RegisterName]          NVARCHAR (64)  NULL,
    [Note]                  NVARCHAR (255) NULL,
    [CashAccountID]         NVARCHAR (64)  NULL,
    [CardReceivedAccountID] NVARCHAR (64)  NULL,
    [PDCReceivedAccountID]  NVARCHAR (64)  NULL,
    [PDCIssuedAccountID]    NVARCHAR (64)  NULL,
    [Inactive]              BIT            CONSTRAINT [DF_Register_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]           DATETIME       NULL,
    [DateUpdated]           DATETIME       NULL,
    [CreatedBy]             NVARCHAR (15)  NULL,
    [UpdatedBy]             NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED ([RegisterID] ASC),
    CONSTRAINT [FK_Register_Account_Card] FOREIGN KEY ([CardReceivedAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Register_Account_Cash] FOREIGN KEY ([CashAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Register_Account_PDC] FOREIGN KEY ([PDCIssuedAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Register_Account_PDCR] FOREIGN KEY ([PDCReceivedAccountID]) REFERENCES [dbo].[Account] ([AccountID])
);

GO
CREATE TABLE [dbo].[Release_Type] (
    [ReleaseTypeID]   NVARCHAR (30)  NOT NULL,
    [ReleaseTypeName] NVARCHAR (64)  NOT NULL,
    [IsInactive]      BIT            NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Release_Type] PRIMARY KEY CLUSTERED ([ReleaseTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Religion] (
    [ReligionID]   NVARCHAR (15) NOT NULL,
    [ReligionName] NVARCHAR (30) NULL,
    [DateCreated]  DATETIME      NULL,
    [DateUpdated]  DATETIME      NULL,
    [CreatedBy]    NVARCHAR (15) NULL,
    [UpdatedBy]    NVARCHAR (15) NULL,
    CONSTRAINT [PK_Religion] PRIMARY KEY CLUSTERED ([ReligionID] ASC)
);

GO
CREATE TABLE [dbo].[Reminder_Setting] (
    [ReminderID] TINYINT       NULL,
    [UserID]     NVARCHAR (15) NULL,
    [Days]       TINYINT       NULL,
    [IsSelected] BIT           NULL
);

GO
CREATE TABLE [dbo].[Rental_Posting] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [SheetName]       NVARCHAR (30)  NULL,
    [TransactionDate] DATETIME       NULL,
    [Month]           TINYINT        NULL,
    [Year]            SMALLINT       NULL,
    [AsofDate]        DATETIME       NULL,
    [Note]            NVARCHAR (255) NULL,
    [Reference]       NVARCHAR (15)  NULL,
    [IsPosted]        BIT            NULL,
    [IsClosed]        BIT            NULL,
    [IsVoid]          BIT            NULL,
    [ApprovalStatus]  TINYINT        NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_PropertyRental_Posting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Rental_Posting_Detail] (
    [SysDocID]        NVARCHAR (15)   NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [RowIndex]        NCHAR (10)      NULL,
    [TenantID]        NVARCHAR (64)   NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (6)    NULL,
    [StartDate]       DATETIME        NULL,
    [EndDate]         DATETIME        NULL,
    [RentedDays]      DECIMAL (18, 5) NULL,
    [NetAmount]       MONEY           NULL,
    [PaidAmount]      MONEY           NULL
);

GO
CREATE TABLE [dbo].[Rental_Posting_Detail_Item] (
    [SysDocID]        NVARCHAR (15)   NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [TenantID]        NVARCHAR (64)   NULL,
    [PayrollItemID]   NVARCHAR (15)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [RowIndex]        INT             NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (6)    NULL,
    [PayType]         TINYINT         NULL,
    [Amount]          MONEY           NULL,
    [PayableAmount]   MONEY           NULL,
    [DaysRented]      DECIMAL (18, 5) NULL,
    [Paid]            MONEY           NULL,
    [PayCodeType]     TINYINT         NULL,
    [IsFixed]         BIT             NULL
);

GO
CREATE TABLE [dbo].[Reservation_Detail] (
    [ReserveID]       INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SysDocType]      INT             NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [LotNumber]       NVARCHAR (50)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CustomerID]      NVARCHAR (64)   NULL,
    [OrderRowIndex]   INT             NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [ValidDateUpTo]   DATETIME        NULL,
    [SourceReserveID] INT             NULL
);

GO
CREATE TABLE [dbo].[Returned_Cheque_Reason] (
    [ReasonID]    NVARCHAR (15) NOT NULL,
    [ReasonName]  NVARCHAR (64) NULL,
    [Inactive]    BIT           CONSTRAINT [DF_Returned_Cheque_Reason_Inactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME      NULL,
    [DateUpdated] DATETIME      NULL,
    [CreatedBy]   NVARCHAR (64) NULL,
    [UpdatedBy]   NVARCHAR (64) NULL,
    CONSTRAINT [PK_Returned_Cheque_Reason] PRIMARY KEY CLUSTERED ([ReasonID] ASC)
);

GO
CREATE TABLE [dbo].[Rider_Summary] (
    [RiderID]           NVARCHAR (50) NOT NULL,
    [RiderName]         NVARCHAR (50) NULL,
    [IsInactive]        BIT           NULL,
    [Type]              NVARCHAR (50) NULL,
    [LicenseNumber]     NVARCHAR (50) NULL,
    [FEIRegisterNumber] NVARCHAR (50) NULL,
    [FathersName]       NVARCHAR (50) NULL,
    [FamilyName]        NVARCHAR (50) NULL,
    [Nationality]       NVARCHAR (50) NULL,
    [DateofBirth]       DATETIME      NULL,
    [Gender]            NVARCHAR (50) NULL,
    [ContactNumber]     NVARCHAR (50) NULL,
    [EMail]             NVARCHAR (50) NULL,
    [DateCreated]       DATETIME      NULL,
    [DateUpdated]       DATETIME      NULL,
    [CreatedBy]         NVARCHAR (50) NULL,
    [UpdatedBy]         NVARCHAR (50) NULL
);

GO
CREATE TABLE [dbo].[Route] (
    [RouteID]     NVARCHAR (30)  NOT NULL,
    [RouteName]   NVARCHAR (64)  NULL,
    [LocationID]  NVARCHAR (15)  NULL,
    [BOMID]       NVARCHAR (15)  NULL,
    [Inactive]    BIT            NULL,
    [Remarks]     NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED ([RouteID] ASC)
);

GO
CREATE TABLE [dbo].[Route_Group] (
    [RouteGroupID]   NVARCHAR (30)  NOT NULL,
    [RouteGroupName] NVARCHAR (64)  NULL,
    [Note]           NVARCHAR (255) NULL,
    [Inactive]       BIT            NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Route_Group] PRIMARY KEY CLUSTERED ([RouteGroupID] ASC)
);

GO
CREATE TABLE [dbo].[Route_Group_Detail] (
    [RouteGroupID] NVARCHAR (30)  NOT NULL,
    [Step]         INT            NOT NULL,
    [RouteID]      NVARCHAR (30)  NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [PreviousStep] INT            NULL,
    [Remarks]      NVARCHAR (255) NULL,
    [DateCreated]  DATETIME       NULL,
    [DateUpdated]  DATETIME       NULL,
    [CreatedBy]    NVARCHAR (15)  NULL,
    [UpdatedBy]    NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Salary_Addition] (
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [ApprovedBy]      NVARCHAR (15)  NULL,
    [ApprovalDate]    DATETIME       NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Salary_Addition] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Salary_Addition_Detail] (
    [VoucherID]     NVARCHAR (15)   NOT NULL,
    [PayrollPeriod] DATETIME        NULL,
    [EmployeeID]    NVARCHAR (15)   NOT NULL,
    [EmployeeName]  NVARCHAR (64)   NULL,
    [AdditionCode]  NVARCHAR (15)   NULL,
    [Quantity]      DECIMAL (18, 5) NULL,
    [Rate]          MONEY           NULL,
    [Amount]        MONEY           NULL,
    [AmountType]    NVARCHAR (5)    NULL,
    [Remarks]       NVARCHAR (500)  NULL,
    [RowIndex]      INT             NULL
);

GO
CREATE TABLE [dbo].[Salary_Deduction] (
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [ApprovedBy]      NVARCHAR (15)  NULL,
    [ApprovalDate]    DATETIME       NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Salary_Deduction] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Salary_Deduction_Detail] (
    [VoucherID]     NVARCHAR (15)   NOT NULL,
    [PayrollPeriod] DATETIME        NULL,
    [EmployeeID]    NVARCHAR (15)   NOT NULL,
    [EmployeeName]  NVARCHAR (64)   NULL,
    [DeductionCode] NVARCHAR (15)   NULL,
    [Quantity]      DECIMAL (18, 5) NULL,
    [Rate]          MONEY           NULL,
    [Amount]        MONEY           NULL,
    [Remarks]       NVARCHAR (500)  NULL,
    [RowIndex]      INT             NULL
);

GO
CREATE TABLE [dbo].[SalarySheet] (
    [SysDocID]           NVARCHAR (7)   NOT NULL,
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [CompanyID]          TINYINT        NULL,
    [SheetName]          NVARCHAR (100) NULL,
    [TransactionDate]    DATETIME       NULL,
    [Month]              TINYINT        NULL,
    [Year]               SMALLINT       NULL,
    [StartDate]          DATETIME       NULL,
    [EndDate]            DATETIME       NULL,
    [Note]               NVARCHAR (255) NULL,
    [Reference]          NVARCHAR (15)  NULL,
    [IsPosted]           BIT            NULL,
    [IsClosed]           BIT            NULL,
    [IsVoid]             BIT            NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Salary_Sheet_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[SalarySheet_Detail] (
    [SysDocID]           NVARCHAR (15)   NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [RowIndex]           NCHAR (10)      NULL,
    [EmployeeID]         NVARCHAR (64)   NULL,
    [WorkDays]           DECIMAL (18, 5) NULL,
    [Absent]             DECIMAL (18, 5) NULL,
    [AnnualLeaves]       DECIMAL (18, 5) NULL,
    [UnpaidAnnualLeaves] DECIMAL (18, 5) NULL,
    [NormalLeaves]       DECIMAL (18, 5) NULL,
    [SickLeaves]         DECIMAL (18, 5) NULL,
    [Basic]              MONEY           NULL,
    [Allowance]          MONEY           NULL,
    [Deduction]          MONEY           NULL,
    [LoanDeduction]      MONEY           NULL,
    [OTHours]            DECIMAL (18, 5) NULL,
    [OTRate]             MONEY           NULL,
    [OTAmount]           MONEY           NULL,
    [OTBase]             MONEY           NULL,
    [OTFactor]           DECIMAL (18, 5) NULL,
    [NetSalary]          MONEY           NULL,
    [OTFixedAmount]      MONEY           NULL,
    [OTIsFixed]          BIT             NULL,
    [PaidAmount]         MONEY           NULL,
    [Remarks]            NVARCHAR (3000) NULL
);

GO
CREATE TABLE [dbo].[SalarySheet_Detail_Item] (
    [SysDocID]      NVARCHAR (15)   NOT NULL,
    [VoucherID]     NVARCHAR (15)   NOT NULL,
    [EmployeeID]    NVARCHAR (64)   NULL,
    [PayrollItemID] NVARCHAR (15)   NULL,
    [Description]   NVARCHAR (255)  NULL,
    [RowIndex]      INT             NULL,
    [LoanSysDocID]  NVARCHAR (7)    NULL,
    [PayType]       TINYINT         NULL,
    [Amount]        MONEY           NULL,
    [PayableAmount] MONEY           NULL,
    [DaysWorked]    DECIMAL (18, 5) NULL,
    [Paid]          MONEY           NULL,
    [InDeduction]   BIT             NULL,
    [PayCodeType]   TINYINT         NULL,
    [IsFixed]       BIT             NULL
);

GO
CREATE TABLE [dbo].[Sales_Enquiry] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [IsExport]           BIT             NULL,
    [DueDate]            DATETIME        NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Sales_Enquiry_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Enquiry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Enquiry_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL
);

GO
CREATE TABLE [dbo].[Sales_Forecasting] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [LocationFrom]       NVARCHAR (15)   NULL,
    [Period]             INT             NULL,
    [Loc1]               NVARCHAR (15)   NULL,
    [Loc2]               NVARCHAR (15)   NULL,
    [Loc3]               NVARCHAR (15)   NULL,
    [Loc4]               NVARCHAR (15)   NULL,
    [Loc5]               NVARCHAR (15)   NULL,
    [Status]             TINYINT         NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [CalculationMethod]  TINYINT         NULL,
    [FromDate]           DATETIME        NULL,
    [ToDate]             DATETIME        NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Forecasting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Forecasting_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Loc1Qty]         DECIMAL (18, 5) NULL,
    [Loc2Qty]         DECIMAL (18, 5) NULL,
    [Loc3Qty]         DECIMAL (18, 5) NULL,
    [Loc4Qty]         DECIMAL (18, 5) NULL,
    [Loc5Qty]         DECIMAL (18, 5) NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceRowIndex]  INT             NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [RowSource]       TINYINT         NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL
);

GO
CREATE TABLE [dbo].[Sales_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [CLUserID]           NVARCHAR (15)   NULL,
    [IsCash]             BIT             NULL,
    [IsExport]           BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ReportTo]           NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Sales_Invoice_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [PriceIncludeTax]    BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [RoundOff]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_Sales_Invoice_IsDelivered] DEFAULT ((0)) NULL,
    [IsWeightInvoice]    BIT             NULL,
    [Note]               NVARCHAR (4000) NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [ExpAmount]          MONEY           NULL,
    [ExpCode]            NVARCHAR (30)   NULL,
    [ExpPercent]         DECIMAL (18)    NULL,
    [RequireUpdate]      BIT             NULL,
    [DriverID]           NVARCHAR (15)   NULL,
    [VehicleID]          NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Invoice_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [FOCQuantity]      DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [Discount]         MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [Cost]             DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [COGS]             MONEY           NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [DNoteVoucherID]   NVARCHAR (15)   NULL,
    [DNoteSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [IsDNRow]          BIT             NULL,
    [IsRecost]         BIT             NULL,
    [ITRowID]          INT             NULL,
    [RowSource]        TINYINT         NULL,
    [WeightQuantity]   DECIMAL (18, 5) NULL,
    [WeightPrice]      DECIMAL (18, 5) NULL,
    [ConsignmentNo]    NVARCHAR (50)   NULL,
    [ListVoucherID]    NVARCHAR (15)   NULL,
    [ListSysDocID]     NVARCHAR (7)    NULL,
    [ListRowIndex]     INT             NULL,
    [RefSlNo]          INT             NULL,
    [RefText1]         NVARCHAR (50)   NULL,
    [RefText2]         NVARCHAR (50)   NULL,
    [RefNum1]          DECIMAL (18, 5) NULL,
    [RefNum2]          DECIMAL (18, 5) NULL,
    [RefDate1]         DATETIME        NULL,
    [RefDate2]         DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Sales_Invoice_Expense] (
    [InvoiceSysDocID]  NVARCHAR (7)    NULL,
    [InvoiceVoucherID] NVARCHAR (15)   NULL,
    [ExpenseID]        NVARCHAR (15)   NULL,
    [Description]      NVARCHAR (64)   NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [Reference]        NVARCHAR (15)   NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [RateType]         CHAR (1)        NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        MONEY           NULL,
    [IsDeduct]         BIT             NULL
);

GO
CREATE TABLE [dbo].[Sales_Invoice_NonInv] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [CLUserID]           NVARCHAR (15)   NULL,
    [IsCash]             BIT             NULL,
    [IsExport]           BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ReportTo]           NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Sales_Invoice_NonInv_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [PriceIncludeTax]    BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [RoundOff]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_Sales_Invoice_NonInv_IsDelivered] DEFAULT ((0)) NULL,
    [IsWeightInvoice]    BIT             NULL,
    [Note]               NVARCHAR (4000) NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [ExpAmount]          MONEY           NULL,
    [ExpCode]            NVARCHAR (30)   NULL,
    [ExpPercent]         DECIMAL (18)    NULL,
    [RequireUpdate]      BIT             NULL,
    [DriverID]           NVARCHAR (15)   NULL,
    [VehicleID]          NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [PropertyID]         NVARCHAR (15)   NULL,
    [PropertyUnitID]     NVARCHAR (15)   NULL,
    [AgentID]            NVARCHAR (15)   NULL,
    [InvoiceType]        INT             NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Invoice_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Invoice_NonInv_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [FOCQuantity]      DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [TaxPercentage]    DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [Discount]         MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [Remarks]          NVARCHAR (3000) NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [Cost]             DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [COGS]             MONEY           NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [OrderVoucherID]   NVARCHAR (15)   NULL,
    [OrderSysDocID]    NVARCHAR (7)    NULL,
    [DNoteVoucherID]   NVARCHAR (15)   NULL,
    [DNoteSysDocID]    NVARCHAR (7)    NULL,
    [OrderRowIndex]    INT             NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CostCategoryID]   NVARCHAR (30)   NULL,
    [SpecificationID]  NVARCHAR (15)   NULL,
    [StyleID]          NVARCHAR (15)   NULL,
    [IsDNRow]          BIT             NULL,
    [IsRecost]         BIT             NULL,
    [ITRowID]          INT             NULL,
    [RowSource]        TINYINT         NULL,
    [WeightQuantity]   DECIMAL (18, 5) NULL,
    [WeightPrice]      DECIMAL (18, 5) NULL,
    [ConsignmentNo]    NVARCHAR (50)   NULL,
    [ListVoucherID]    NVARCHAR (15)   NULL,
    [ListSysDocID]     NVARCHAR (7)    NULL,
    [ListRowIndex]     INT             NULL
);

GO
CREATE TABLE [dbo].[Sales_Man_Target] (
    [SysDocID]    NVARCHAR (7)  NOT NULL,
    [VoucherID]   NVARCHAR (15) NOT NULL,
    [FromDate]    DATETIME      NULL,
    [ToDate]      DATETIME      NULL,
    [Month]       INT           NULL,
    [TargetType]  NVARCHAR (15) NULL,
    [CreatedBy]   NVARCHAR (15) NULL,
    [DateCreated] DATETIME      NULL,
    [UpdatedBy]   NVARCHAR (15) NULL,
    [DateUpdated] DATETIME      NULL
);

GO
CREATE TABLE [dbo].[Sales_Man_Target_Detail] (
    [SysDocID]          NVARCHAR (7)  NOT NULL,
    [VoucherID]         NVARCHAR (15) NOT NULL,
    [SalesManGroupID]   NVARCHAR (15) NULL,
    [SalesManID]        NVARCHAR (15) NULL,
    [ProductClassID]    NVARCHAR (15) NULL,
    [ProductCategoryID] NVARCHAR (50) NULL,
    [ProductID]         NVARCHAR (50) NULL,
    [Amount]            MONEY         NULL,
    [CommissionPercent] MONEY         NULL,
    [RowIndex]          INT           NULL
);

GO
CREATE TABLE [dbo].[Sales_Order] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [IsExport]           BIT             NULL,
    [DueDate]            DATETIME        NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [PriceIncludeTax]    BIT             NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Sales_Order_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [RoundOff]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Order_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [Discount]        MONEY           NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [RowIndex]        INT             NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [RefSlNo]         INT             NULL,
    [RefText1]        NVARCHAR (50)   NULL,
    [RefText2]        NVARCHAR (50)   NULL,
    [RefNum1]         DECIMAL (18, 5) NULL,
    [RefNum2]         DECIMAL (18, 5) NULL,
    [RefDate1]        DATETIME        NULL,
    [RefDate2]        DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Sales_Order_ProductLot_Detail] (
    [LotNumber]       NVARCHAR (50)   NULL,
    [Reference]       NVARCHAR (50)   NULL,
    [SourceLotNumber] NVARCHAR (15)   NULL,
    [SysDocID]        NVARCHAR (7)    NULL,
    [VoucherID]       NVARCHAR (15)   NULL,
    [RowIndex]        INT             NULL,
    [ProductID]       NVARCHAR (64)   NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [BinID]           NVARCHAR (15)   NULL,
    [RackID]          NVARCHAR (30)   NULL,
    [SoldQty]         DECIMAL (18, 5) NULL,
    [UnitPrice]       DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [Reference2]      NVARCHAR (30)   NULL
);

GO
CREATE TABLE [dbo].[Sales_POS] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [ShiftID]           INT             NULL,
    [BatchID]           INT             NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [DueDate]           DATETIME        NULL,
    [IsCash]            BIT             NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxOption]         TINYINT         NULL,
    [PriceIncludeTax]   BIT             NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Sales_POS_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Discount]          DECIMAL (18, 2) NULL,
    [DiscountFC]        MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Total]             DECIMAL (18, 2) NULL,
    [Change]            DECIMAL (18, 2) NULL,
    [TotalCOGS]         MONEY           NULL,
    [TotalFC]           MONEY           NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_Sales_POS_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (255)  NULL,
    [PaymentMethodType] TINYINT         NULL,
    [RequireUpdate]     BIT             NULL,
    [SearchValue]       NVARCHAR (64)   NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_POS] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_POS_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [Barcode]         NVARCHAR (64)   NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Discount]        DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [COGS]            MONEY           NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [OrderVoucherID]  NVARCHAR (15)   NULL,
    [OrderSysDocID]   NVARCHAR (6)    NULL,
    [DNoteVoucherID]  NVARCHAR (15)   NULL,
    [DNoteSysDocID]   NVARCHAR (7)    NULL,
    [OrderRowIndex]   INT             NULL,
    [IsDNRow]         BIT             NULL,
    [IsRecost]        BIT             NULL,
    [ITRowID]         INT             NULL
);

GO
CREATE TABLE [dbo].[Sales_Quote] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [EntityType]        NVARCHAR (2)    NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [SalesFlow]         TINYINT         NULL,
    [RequiredDate]      DATETIME        NULL,
    [ExpiryDate]        DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [BillingAddressID]  NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [ShipToAddress]     NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Sales_Quote_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [PriceIncludeTax]   BIT             NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [TaxOption]         TINYINT         NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]         MONEY           NULL,
    [RoundOff]          MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [Note]              NVARCHAR (4000) NULL,
    [Remarks1]          NVARCHAR (2000) NULL,
    [Remarks2]          NVARCHAR (2000) NULL,
    [ApprovalStatus]    TINYINT         NULL,
    [LastRevisedDate]   DATETIME        NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Quote] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Quote_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    REAL            NULL,
    [UnitFactor]      REAL            NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [Cost]            DECIMAL (18, 5) NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [RowIndex]        TINYINT         NULL,
    [RefSlNo]         INT             NULL,
    [RefText1]        NVARCHAR (50)   NULL,
    [RefText2]        NVARCHAR (50)   NULL,
    [RefNum1]         DECIMAL (18, 5) NULL,
    [RefNum2]         DECIMAL (18, 5) NULL,
    [RefDate1]        DATETIME        NULL,
    [RefDate2]        DATETIME        NULL
);

GO
CREATE TABLE [dbo].[Sales_Quote_Detail_History] (
    [SysDocID]     NVARCHAR (7)    NOT NULL,
    [VoucherID]    NVARCHAR (15)   NOT NULL,
    [RevisionNo]   INT             NULL,
    [ProductID]    NVARCHAR (64)   NOT NULL,
    [Quantity]     DECIMAL (18, 5) NOT NULL,
    [UnitPrice]    DECIMAL (18, 5) NOT NULL,
    [Description]  NVARCHAR (255)  NULL,
    [UnitID]       NVARCHAR (15)   NULL,
    [UnitQuantity] REAL            NULL,
    [UnitFactor]   REAL            NULL,
    [FactorType]   NVARCHAR (1)    NULL,
    [SubunitPrice] DECIMAL (18, 5) NULL,
    [TaxOption]    TINYINT         NULL,
    [TaxGroupID]   NVARCHAR (15)   NULL,
    [TaxAmount]    DECIMAL (18, 5) NULL,
    [RowIndex]     TINYINT         NULL
);

GO
CREATE TABLE [dbo].[Sales_Quote_History] (
    [SysDocID]          NVARCHAR (7)   NOT NULL,
    [VoucherID]         NVARCHAR (15)  NOT NULL,
    [RevisionNo]        INT            NULL,
    [CustomerID]        NVARCHAR (64)  NOT NULL,
    [TransactionDate]   DATETIME       NOT NULL,
    [EntityType]        NVARCHAR (2)   NULL,
    [SalespersonID]     NVARCHAR (64)  NULL,
    [SalesFlow]         TINYINT        NULL,
    [RequiredDate]      DATETIME       NULL,
    [ExpiryDate]        DATETIME       NULL,
    [ShippingAddressID] NVARCHAR (15)  NULL,
    [BillingAddressID]  NVARCHAR (15)  NULL,
    [CustomerAddress]   NVARCHAR (255) NULL,
    [ShipToAddress]     NVARCHAR (255) NULL,
    [Status]            TINYINT        CONSTRAINT [DF_Sales_Quote_History_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)   NULL,
    [TermID]            NVARCHAR (15)  NULL,
    [ShippingMethodID]  NVARCHAR (15)  NULL,
    [JobID]             NVARCHAR (50)  NULL,
    [CostCategoryID]    NVARCHAR (30)  NULL,
    [IsVoid]            BIT            NULL,
    [Reference]         NVARCHAR (20)  NULL,
    [Discount]          MONEY          NULL,
    [TaxOption]         TINYINT        NULL,
    [PayeeTaxGroupID]   NVARCHAR (15)  NULL,
    [TaxAmount]         MONEY          NULL,
    [Total]             MONEY          NULL,
    [PONumber]          NVARCHAR (50)  NULL,
    [Note]              NVARCHAR (500) NULL,
    [ApprovalStatus]    TINYINT        NULL,
    [LastRevisedDate]   DATETIME       NULL,
    [DateCreated]       DATETIME       NULL,
    [DateUpdated]       DATETIME       NULL,
    [CreatedBy]         NVARCHAR (15)  NULL,
    [UpdatedBy]         NVARCHAR (15)  NULL
);

GO
CREATE TABLE [dbo].[Sales_Receipt] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalesFlow]         TINYINT         NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Sales_Receipt_Status] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [RegisterID]        NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [DiscountFC]        MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [TaxAmountFC]       MONEY           NULL,
    [Total]             MONEY           NULL,
    [TotalFC]           MONEY           NULL,
    [PONumber]          NVARCHAR (50)   NULL,
    [IsDelivered]       BIT             CONSTRAINT [DF_Sales_Receipt_IsDelivered] DEFAULT ((0)) NULL,
    [Note]              NVARCHAR (500)  NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Receipt_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [DNoteVoucherID]  NVARCHAR (15)   NULL,
    [DNoteSysDocID]   NVARCHAR (7)    NULL,
    [OrderRowIndex]   INT             NULL
);

GO
CREATE TABLE [dbo].[Sales_Return] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [IsCash]             BIT             CONSTRAINT [DF_Sales_Return_IsCash] DEFAULT ((0)) NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ReportTo]           NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [SourceDocType]      TINYINT         NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_Sales_Return_Status_1] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [PriceIncludeTax]    BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Reference2]         NVARCHAR (20)   NULL,
    [ReasonID]           NVARCHAR (15)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [RoundOff]           MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_Sales_Return_IsDelivered_1] DEFAULT ((0)) NULL,
    [RequireUpdate]      BIT             NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Sales_Return_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Sales_Return_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [TaxAmount]       MONEY           NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        SMALLINT        NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [DNoteVoucherID]  NVARCHAR (15)   NULL,
    [DNoteSysDocID]   NVARCHAR (7)    NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL,
    [OrderRowIndex]   INT             NULL,
    [SourceDocType]   TINYINT         NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [ITRowID]         INT             NULL
);

GO
CREATE TABLE [dbo].[Salesperson] (
    [SalespersonID]      NVARCHAR (64)  NOT NULL,
    [FullName]           NVARCHAR (64)  NULL,
    [Alias]              NVARCHAR (64)  NULL,
    [EmployeeID]         NVARCHAR (64)  NULL,
    [ReportTo]           NVARCHAR (64)  NULL,
    [Address]            NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [DivisionID]         NVARCHAR (15)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [GroupID]            NVARCHAR (15)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (30)  NULL,
    [Website]            NVARCHAR (30)  NULL,
    [BankName]           NVARCHAR (30)  NULL,
    [BankBranch]         NVARCHAR (30)  NULL,
    [BankAccountNumber]  NVARCHAR (30)  NULL,
    [CommissionPercent]  DECIMAL (4, 2) NULL,
    [CommissionType]     TINYINT        NULL,
    [AreaID]             NVARCHAR (15)  NULL,
    [CountryID]          NVARCHAR (15)  NULL,
    [EmailStatement]     BIT            NULL,
    [IsInactive]         BIT            CONSTRAINT [DF_Salesperson_Inactive] DEFAULT ((0)) NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateUpdated]        DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Salesperson] PRIMARY KEY CLUSTERED ([SalespersonID] ASC)
);

GO
CREATE TABLE [dbo].[Salesperson_Group] (
    [GroupID]     NVARCHAR (15)  NOT NULL,
    [GroupName]   NVARCHAR (30)  NOT NULL,
    [POSAccess]   BIT            NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Salesperson_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[SalesProforma_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [IsExport]           BIT             NULL,
    [DueDate]            DATETIME        NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [BillingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [ShipToAddress]      NVARCHAR (255)  NULL,
    [PriceIncludeTax]    BIT             NULL,
    [TaxOption]          TINYINT         NULL,
    [PayeeTaxGroupID]    NVARCHAR (15)   NULL,
    [Status]             TINYINT         CONSTRAINT [DF_SalesProforma_Invoice1_Detail_Status] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [JobID]              NVARCHAR (50)   NULL,
    [CostCategoryID]     NVARCHAR (30)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [Total]              MONEY           NULL,
    [PONumber]           NVARCHAR (50)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [ShippingReference]  NVARCHAR (15)   NULL,
    [SourcePortID]       NVARCHAR (15)   NULL,
    [DestinationPortID]  NVARCHAR (15)   NULL,
    [ETD]                DATETIME        NULL,
    [ETA]                DATETIME        NULL,
    [Weight]             DECIMAL (18, 5) NULL,
    [TransporterID]      NVARCHAR (50)   NULL,
    [ClearingAgent]      NVARCHAR (30)   NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_SalesProforma_Invoice1_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[SalesProforma_Invoice_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [TaxOption]       TINYINT         NULL,
    [TaxGroupID]      NVARCHAR (15)   NULL,
    [TaxAmount]       DECIMAL (18, 5) NULL,
    [TaxPercentage]   DECIMAL (18, 5) NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [JobID]           NVARCHAR (50)   NULL,
    [CostCategoryID]  NVARCHAR (30)   NULL,
    [RowIndex]        SMALLINT        NULL,
    [QuantityShipped] DECIMAL (18, 5) NULL,
    [Remarks]         NVARCHAR (3000) NULL,
    [SpecificationID] NVARCHAR (15)   NULL,
    [StyleID]         NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Screen_Security] (
    [ScreenID]    NVARCHAR (60) NULL,
    [ViewRight]   BIT           NULL,
    [DeleteRight] BIT           NULL,
    [EditRight]   BIT           NULL,
    [NewRight]    BIT           NULL,
    [UserID]      NVARCHAR (15) NULL,
    [GroupID]     NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Security_Cheque] (
    [VoucherID]          NVARCHAR (15)  NOT NULL,
    [SysDocID]           NVARCHAR (15)  NOT NULL,
    [ChequebookID]       NVARCHAR (15)  NULL,
    [ChequeNumber]       NVARCHAR (15)  NULL,
    [IssueDate]          DATETIME       NULL,
    [Amount]             MONEY          NULL,
    [ChequeDate]         DATETIME       NULL,
    [PayeeType]          CHAR (1)       NULL,
    [PayeeID]            NVARCHAR (64)  NULL,
    [IsVoid]             BIT            NULL,
    [VoidDate]           DATETIME       NULL,
    [Note]               NVARCHAR (255) NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    CONSTRAINT [PK_Security_Cheque_1] PRIMARY KEY CLUSTERED ([VoucherID] ASC, [SysDocID] ASC)
);

GO
CREATE TABLE [dbo].[Service_Activity] (
    [ServiceActivityID]   NVARCHAR (15) NOT NULL,
    [ServiceActivityName] NVARCHAR (64) NOT NULL,
    [Description]         NVARCHAR (64) NULL,
    [Inactive]            BIT           NULL,
    [DateCreated]         DATETIME      NULL,
    [DateUpdated]         DATETIME      NULL,
    [CreatedBy]           NVARCHAR (64) NULL,
    [UpdatedBy]           NVARCHAR (64) NULL,
    CONSTRAINT [PK_Service_Activity] PRIMARY KEY CLUSTERED ([ServiceActivityID] ASC)
);

GO
CREATE TABLE [dbo].[Service_CallTrack] (
    [SysDocID]            NVARCHAR (7)    NOT NULL,
    [VoucherID]           NVARCHAR (15)   NOT NULL,
    [TransactionDate]     DATETIME        NOT NULL,
    [JobID]               NVARCHAR (50)   NULL,
    [CustomerID]          NVARCHAR (64)   NOT NULL,
    [ShippingAddressID]   NVARCHAR (15)   NULL,
    [CustomerAddress]     NVARCHAR (255)  NULL,
    [ContactName]         NVARCHAR (30)   NULL,
    [ContactNo]           NVARCHAR (30)   NULL,
    [Location]            NVARCHAR (30)   NULL,
    [ReqReceivedDate]     DATETIME        NULL,
    [ReqReceivedTime]     TIME (7)        NULL,
    [ServiceEmployeeID]   NVARCHAR (15)   NULL,
    [ServiceAssignDate]   DATETIME        NULL,
    [ServiceAssignTime]   TIME (7)        NULL,
    [ServiceUnder]        NCHAR (4)       NULL,
    [ServiceStartDate]    DATETIME        NULL,
    [ServiceStartTime]    TIME (7)        NULL,
    [ServiceFinishedDate] DATETIME        NULL,
    [ServiceFinishedTime] TIME (7)        NULL,
    [TravelHours]         INT             NULL,
    [TravelMins]          INT             NULL,
    [LabourHours]         INT             NULL,
    [LabourMins]          INT             NULL,
    [ServiceHours]        NUMERIC (15, 2) NULL,
    [ServiceTotal]        NUMERIC (15, 2) NULL,
    [PartsTotal]          NUMERIC (15, 2) NULL,
    [LabourTotal]         NUMERIC (15, 2) NULL,
    [TravelTotal]         NUMERIC (15, 2) NULL,
    [TotalCharges]        NUMERIC (15, 2) NULL,
    [RepairDetails]       NTEXT           NULL,
    [Status]              TINYINT         CONSTRAINT [DF_ServiceCallTrack_Status] DEFAULT ((1)) NULL,
    [ApprovalStatus]      TINYINT         NULL,
    [VerificationStatus]  TINYINT         NULL,
    [IsVoid]              BIT             NULL,
    [DateCreated]         DATETIME        NULL,
    [DateUpdated]         DATETIME        NULL,
    [CreatedBy]           NVARCHAR (15)   NULL,
    [UpdatedBy]           NVARCHAR (15)   NULL,
    CONSTRAINT [PK_ServiceCallTrack] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Service_ClientAsset_Detail] (
    [SysDocID]           NVARCHAR (7)   NULL,
    [VoucherID]          NVARCHAR (15)  NULL,
    [ClientAssetID]      NVARCHAR (15)  NULL,
    [SerialNo]           NVARCHAR (50)  NULL,
    [ProblemDescription] NVARCHAR (255) NULL,
    [RowIndex]           TINYINT        NULL
);

GO
CREATE TABLE [dbo].[Service_Item] (
    [ServiceItemID]   NVARCHAR (15)  NOT NULL,
    [Description]     NVARCHAR (64)  NULL,
    [ServiceType]     NVARCHAR (50)  NULL,
    [APAccountID]     NVARCHAR (64)  NULL,
    [RepeatCountDays] NVARCHAR (50)  NULL,
    [RepeatCountKM]   NCHAR (50)     NULL,
    [ReminderDays]    NVARCHAR (50)  NULL,
    [ReminderKM]      NCHAR (50)     NULL,
    [VehicleType]     NVARCHAR (50)  NULL,
    [TaxOption]       TINYINT        NULL,
    [TaxGroupID]      NVARCHAR (15)  NULL,
    [Note]            NVARCHAR (255) NULL,
    [DateCreated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [DateUpdated]     DATETIME       NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    [Inactive]        BIT            NULL,
    CONSTRAINT [PK_Service_Item] PRIMARY KEY CLUSTERED ([ServiceItemID] ASC)
);

GO
CREATE TABLE [dbo].[Service_PartsReplaced_Detail] (
    [SysDocID]         NVARCHAR (7)   NULL,
    [VoucherID]        NVARCHAR (15)  NULL,
    [ProductID]        NVARCHAR (50)  NULL,
    [Quantity]         INT            NULL,
    [Description]      NVARCHAR (255) NULL,
    [ChargeableStatus] BIT            NULL,
    [RowIndex]         TINYINT        NULL
);

GO
CREATE TABLE [dbo].[Settings] (
    [ID]       NVARCHAR (64) NULL,
    [SName]    NVARCHAR (64) NULL,
    [SKey]     NVARCHAR (64) NULL,
    [SValue]   NVARCHAR (69) NULL,
    [SData]    NTEXT         NULL,
    [SBinData] IMAGE         NULL
);

GO
CREATE TABLE [dbo].[Setup_Inventory] (
    [CompanyID]  TINYINT       NULL,
    [Price1Name] NVARCHAR (15) NULL,
    [Price2Name] NVARCHAR (15) NULL,
    [Price3Name] NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[Shipping_Method] (
    [ShippingMethodID]   NVARCHAR (15)  NOT NULL,
    [ShippingMethodName] NVARCHAR (64)  NOT NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [Phone]              NVARCHAR (30)  NULL,
    [Inactive]           BIT            CONSTRAINT [DF_Shippers_IsInactive] DEFAULT ((0)) NULL,
    [TrackShipment]      BIT            NULL,
    [Note]               NVARCHAR (255) NULL,
    [DateUpdated]        DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Shipers] PRIMARY KEY CLUSTERED ([ShippingMethodID] ASC)
);

GO
CREATE TABLE [dbo].[Shortcut] (
    [ShortcutType] TINYINT       NOT NULL,
    [UserID]       NVARCHAR (15) NOT NULL,
    [ShortcutKey]  NVARCHAR (30) NOT NULL,
    [ShortcutText] NVARCHAR (30) NULL,
    CONSTRAINT [PK_Shortcut] PRIMARY KEY CLUSTERED ([ShortcutType] ASC, [UserID] ASC, [ShortcutKey] ASC)
);

GO
CREATE TABLE [dbo].[Skill] (
    [SkillID]     NVARCHAR (15)  NOT NULL,
    [SkillName]   NVARCHAR (255) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED ([SkillID] ASC)
);

GO
CREATE TABLE [dbo].[Smartlist] (
    [SmartListID]                    INT             IDENTITY (1, 1) NOT NULL,
    [SmartListName]                  NVARCHAR (64)   NULL,
    [Description]                    NVARCHAR (255)  NULL,
    [Query]                          NTEXT           NULL,
    [ReportData]                     IMAGE           NULL,
    [CategoryID]                     NVARCHAR (30)   NULL,
    [ParentID]                       NVARCHAR (30)   NULL,
    [DrillAction]                    TINYINT         NULL,
    [DrillCardTypeID]                INT             NULL,
    [DrillCardIDField]               NVARCHAR (30)   NULL,
    [DrillTransactionSysDocIDField]  NVARCHAR (30)   NULL,
    [DrillTransactionVoucherIDField] NVARCHAR (30)   NULL,
    [DrillParm1]                     NVARCHAR (30)   NULL,
    [DrillParm2]                     NVARCHAR (30)   NULL,
    [DrillParm3]                     NVARCHAR (30)   NULL,
    [DrillParm4]                     NVARCHAR (30)   NULL,
    [IsSubReport]                    BIT             NULL,
    [DrillSubReportID]               INT             NULL,
    [IsPreview]                      BIT             NULL,
    [Note]                           NVARCHAR (4000) NULL,
    [DisplayNote]                    NVARCHAR (4000) NULL,
    [IsHideDateFilter]               BIT             NULL,
    [IsSetDateEqualTo]               BIT             NULL,
    [CreatedBy]                      NVARCHAR (15)   NULL,
    [DateCreated]                    DATETIME        NULL,
    [UpdatedBy]                      NVARCHAR (15)   NULL,
    [DateUpdated]                    DATETIME        NULL,
    CONSTRAINT [PK_Smartlist] PRIMARY KEY CLUSTERED ([SmartListID] ASC)
);

GO
CREATE TABLE [dbo].[Smartlist_Category] (
    [CategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (30) NULL,
    [ParentID]     NVARCHAR (30) NULL,
    [RowIndex]     INT           NULL,
    CONSTRAINT [PK_Smartlist_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

GO
CREATE TABLE [dbo].[Sponsor] (
    [SponsorID]   NVARCHAR (15)  NOT NULL,
    [SponsorName] NVARCHAR (64)  NULL,
    [MOLId]       NVARCHAR (64)  NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            CONSTRAINT [DF_Sponsor_IsInactive] DEFAULT ((0)) NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED ([SponsorID] ASC)
);

GO
CREATE TABLE [dbo].[Standing_Journal] (
    [StandingJournalID]   NVARCHAR (15)   NOT NULL,
    [Reference]           NVARCHAR (30)   NULL,
    [CurrencyID]          NVARCHAR (5)    NULL,
    [CurrencyRate]        DECIMAL (18, 5) NULL,
    [TransactionSysDocID] NVARCHAR (7)    NULL,
    [StartYear]           SMALLINT        NULL,
    [StartMonth]          TINYINT         NULL,
    [EndYear]             SMALLINT        NULL,
    [EndMonth]            TINYINT         NULL,
    [Status]              TINYINT         NULL,
    [Narration]           NVARCHAR (255)  NULL,
    [Note]                NVARCHAR (255)  NULL,
    [IsVoid]              BIT             NULL,
    [DateCreated]         DATETIME        NULL,
    [DateUpdated]         DATETIME        NULL,
    [CreatedBy]           NVARCHAR (15)   NULL,
    [UpdatedBy]           NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Standing_Journal_1] PRIMARY KEY CLUSTERED ([StandingJournalID] ASC)
);

GO
CREATE TABLE [dbo].[Standing_Journal_Detail] (
    [StandingJournalID] NVARCHAR (15)   NOT NULL,
    [AccountID]         NVARCHAR (64)   NOT NULL,
    [Description]       NVARCHAR (255)  NULL,
    [Debit]             MONEY           NULL,
    [Credit]            MONEY           NULL,
    [RowIndex]          SMALLINT        NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [Reference]         NVARCHAR (30)   NULL,
    [PayeeID]           NVARCHAR (64)   NULL,
    [PayeeType]         NVARCHAR (1)    NULL,
    [AnalysisID]        NVARCHAR (15)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Surveyor] (
    [SurveyorID]   NVARCHAR (15) NOT NULL,
    [SurveyorName] NVARCHAR (64) NOT NULL,
    [Tel]          NVARCHAR (40) NULL,
    [Mobile]       NVARCHAR (40) NULL,
    [Email]        NVARCHAR (50) NULL,
    [Website]      NVARCHAR (50) NULL,
    [ContactName]  NVARCHAR (50) NULL,
    [DateCreated]  DATETIME      NULL,
    [DateUpdated]  DATETIME      NULL,
    [CreatedBy]    NVARCHAR (15) NULL,
    [UpdatedBy]    NVARCHAR (15) NULL,
    CONSTRAINT [PK_Surveyor] PRIMARY KEY CLUSTERED ([SurveyorID] ASC)
);

GO
CREATE TABLE [dbo].[Sync_Activity] (
    [SyncActvityID]    INT            IDENTITY (1, 1) NOT NULL,
    [SyncActivityType] TINYINT        NULL,
    [SyncTokenID]      INT            NULL,
    [EntityID]         NVARCHAR (64)  NULL,
    [SysDocID]         NVARCHAR (7)   NULL,
    [SyncLogDate]      DATETIME       NULL,
    [UserID]           NVARCHAR (64)  NULL,
    [MachineID]        NVARCHAR (64)  NULL,
    [Description]      NVARCHAR (500) NULL,
    [ActivityDataView] IMAGE          NULL
);

GO
CREATE TABLE [dbo].[System_Doc_Detail] (
    [SysDocID]            NVARCHAR (7)   NOT NULL,
    [SysDocType]          INT            NULL,
    [RowIndex]            INT            NULL,
    [PrintTemplate]       NVARCHAR (100) NULL,
    [TemplateDescription] NVARCHAR (30)  NULL,
    [TemplateKeyword]     NVARCHAR (30)  NULL
);

GO
CREATE TABLE [dbo].[System_Doc_Entity_Link] (
    [SysDocID]   NVARCHAR (7)  NOT NULL,
    [EntityID]   NVARCHAR (64) NOT NULL,
    [EntityType] TINYINT       NOT NULL
);

GO
CREATE TABLE [dbo].[System_Document] (
    [SysDocID]                     NVARCHAR (7)   NOT NULL,
    [SysDocType]                   INT            NULL,
    [DocName]                      NVARCHAR (64)  NULL,
    [CompanyID]                    TINYINT        NULL,
    [DivisionID]                   NVARCHAR (15)  NULL,
    [PrintTitle]                   NVARCHAR (30)  NULL,
    [LocationID]                   NVARCHAR (15)  NULL,
    [PrintTemplateName]            NVARCHAR (64)  NULL,
    [ConsignOutLocationID]         NVARCHAR (15)  NULL,
    [ConsignInSalesAccountID]      NVARCHAR (64)  NULL,
    [ConsignInCOGSAccountID]       NVARCHAR (64)  NULL,
    [ConsignInPayableAccountID]    NVARCHAR (64)  NULL,
    [SalesAccountID]               NVARCHAR (64)  NULL,
    [COGSAccountID]                NVARCHAR (64)  NULL,
    [SalesTaxAccountID]            NVARCHAR (64)  NULL,
    [DiscountGivenAccountID]       NVARCHAR (64)  NULL,
    [NextNumber]                   BIGINT         NULL,
    [PriceIncludeTax]              BIT            NULL,
    [NumberPrefix]                 NVARCHAR (10)  NULL,
    [LastNumber]                   NVARCHAR (15)  NULL,
    [NextTempNumber]               BIGINT         NULL,
    [AllowFOC]                     BIT            NULL,
    [IsBOLMandatory]               BIT            NULL,
    [IsSupplierInvoiceNoMandatory] BIT            NULL,
    [ItemFilterBasedonCustomer]    BIT            NULL,
    [AllowMultiTemplate]           BIT            NULL,
    [Inactive]                     BIT            NULL,
    [PrintAfterSave]               BIT            NULL,
    [DoPrint]                      BIT            NULL,
    [OpenListQuery]                NVARCHAR (MAX) NULL,
    [IsSystem]                     BIT            NULL,
    [DateCreated]                  DATETIME       NULL,
    [CreatedBy]                    NVARCHAR (15)  NULL,
    [DateUpdated]                  DATETIME       NULL,
    [UpdatedBy]                    NVARCHAR (15)  NULL,
    CONSTRAINT [PK_System_Document] PRIMARY KEY CLUSTERED ([SysDocID] ASC)
);

GO
CREATE TABLE [dbo].[Tabs_Security] (
    [TabID]   NVARCHAR (255) NOT NULL,
    [Visible] BIT            NULL,
    [UserID]  NVARCHAR (15)  NULL,
    [GroupID] NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Tab_Security] PRIMARY KEY CLUSTERED ([TabID] ASC)
);

GO
CREATE TABLE [dbo].[Task_Steps] (
    [TaskStepID]  NVARCHAR (30)  NOT NULL,
    [Name]        NVARCHAR (150) NULL,
    [TaskTypeID]  NVARCHAR (30)  NOT NULL,
    [IsInactive]  BIT            NULL,
    [Note]        NVARCHAR (50)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Task_Steps] PRIMARY KEY CLUSTERED ([TaskStepID] ASC)
);

GO
CREATE TABLE [dbo].[Task_Transaction] (
    [SysDocID]         NVARCHAR (7)   NOT NULL,
    [VoucherID]        NVARCHAR (15)  NOT NULL,
    [TransactionDate]  DATETIME       NOT NULL,
    [TaskName]         NVARCHAR (50)  NULL,
    [TaskTypeID]       NVARCHAR (15)  NULL,
    [Description]      NVARCHAR (255) NULL,
    [IsVoid]           BIT            NULL,
    [StartDate]        DATETIME       NULL,
    [DocType]          INT            NULL,
    [AssignedSysDocID] NVARCHAR (7)   NULL,
    [AssignedVouherID] NVARCHAR (15)  NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [DateCreated]      DATETIME       NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    [DateUpdated]      DATETIME       NULL,
    CONSTRAINT [PK_Task_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Task_Transaction_Detail] (
    [SysDocID]          NVARCHAR (7)   NOT NULL,
    [VoucherID]         NVARCHAR (15)  NOT NULL,
    [TaskStepID]        NVARCHAR (64)  NULL,
    [Description]       NVARCHAR (255) NULL,
    [DefaultAssigneeID] NVARCHAR (15)  NULL,
    [StartDate]         DATETIME       NULL,
    [DeadLine]          DATETIME       NULL,
    [Status]            NVARCHAR (50)  NULL,
    [DocType]           INT            NULL,
    [PreRequest]        NVARCHAR (50)  NULL,
    [DaysAllowed]       TINYINT        NULL,
    [RowIndex]          INT            NULL
);

GO
CREATE TABLE [dbo].[Task_Transaction_Status] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TransactionDate] DATETIME       NULL,
    [TaskName]        NVARCHAR (15)  NOT NULL,
    [TaskStepID]      NVARCHAR (30)  NOT NULL,
    [SourceSysDocID]  NVARCHAR (7)   NULL,
    [SourceVoucherID] NVARCHAR (15)  NULL,
    [SourceRowIndex]  INT            NULL,
    [TRSysDocID]      NVARCHAR (7)   NULL,
    [TRVoucherID]     NVARCHAR (15)  NULL,
    [Status]          NVARCHAR (7)   NULL,
    [Remarks]         NVARCHAR (255) NULL,
    [Message]         NVARCHAR (255) NULL,
    [IsVoid]          BIT            NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Task_Transaction_Status] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Task_Type] (
    [TaskTypeID]  NVARCHAR (30)  NOT NULL,
    [Name]        NVARCHAR (150) NULL,
    [IsInactive]  BIT            NULL,
    [Note]        NVARCHAR (50)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Task_Type] PRIMARY KEY CLUSTERED ([TaskTypeID] ASC)
);

GO
CREATE TABLE [dbo].[Task_Type_Detail] (
    [TaskTypeID]        NVARCHAR (64)  NOT NULL,
    [TaskStepID]        NVARCHAR (64)  NULL,
    [DefaultAssigneeID] NVARCHAR (15)  NULL,
    [Description]       NVARCHAR (255) NULL,
    [DocTypeID]         TINYINT        NULL,
    [DocTypeName]       VARCHAR (50)   NULL,
    [DaysAllowed]       TINYINT        NULL,
    [PreRequest]        NVARCHAR (50)  NULL,
    [RowIndex]          SMALLINT       NULL
);

GO
CREATE TABLE [dbo].[Tax] (
    [TaxCode]                   NVARCHAR (15)   NOT NULL,
    [TaxName]                   NVARCHAR (64)   NULL,
    [Description]               NVARCHAR (64)   NULL,
    [Remarks]                   NVARCHAR (2000) NULL,
    [SalesTaxAccountID]         NVARCHAR (64)   NULL,
    [PurchaseTaxAccountID]      NVARCHAR (64)   NULL,
    [TaxReverseChargeAccountID] NVARCHAR (64)   NULL,
    [TaxID]                     NVARCHAR (15)   NULL,
    [TaxType]                   NVARCHAR (15)   NULL,
    [CalculationMethod]         TINYINT         NULL,
    [TaxRate]                   MONEY           NULL,
    [IsFixed]                   BIT             NULL,
    [IsPercent]                 BIT             NULL,
    [Inactive]                  BIT             NULL,
    [DateCreated]               DATETIME        NULL,
    [DateUpdated]               DATETIME        NULL,
    [CreatedBy]                 NVARCHAR (15)   NULL,
    [UpdatedBy]                 NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED ([TaxCode] ASC)
);

GO
CREATE TABLE [dbo].[Tax_Detail] (
    [SysDocID]          NVARCHAR (15)   NOT NULL,
    [VoucherID]         NVARCHAR (30)   NOT NULL,
    [TaxLevel]          TINYINT         NULL,
    [TaxGroupID]        NVARCHAR (15)   NOT NULL,
    [TaxItemID]         NVARCHAR (15)   NOT NULL,
    [CalculationMethod] TINYINT         NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [CurrencyRate]      DECIMAL (18, 5) NULL,
    [TaxRate]           DECIMAL (18, 5) NULL,
    [TaxAmount]         DECIMAL (18, 5) NULL,
    [OrderIndex]        INT             NULL,
    [RowIndex]          INT             NULL,
    [AccountID]         NVARCHAR (30)   NULL
);

GO
CREATE TABLE [dbo].[Tax_Group] (
    [TaxGroupID]   NVARCHAR (15) NOT NULL,
    [TaxGroupName] NVARCHAR (64) NULL,
    [Note]         NVARCHAR (64) NULL,
    [Inactive]     BIT           NULL,
    [DateCreated]  DATETIME      NULL,
    [DateUpdated]  DATETIME      NULL,
    [CreatedBy]    NVARCHAR (15) NULL,
    [UpdatedBy]    NVARCHAR (15) NULL,
    CONSTRAINT [PK_Tax_Group] PRIMARY KEY CLUSTERED ([TaxGroupID] ASC)
);

GO
CREATE TABLE [dbo].[Tax_Group_Detail] (
    [TaxGroupID]  NVARCHAR (15) NOT NULL,
    [TaxCode]     NVARCHAR (15) NOT NULL,
    [Description] NVARCHAR (64) NOT NULL,
    [RowIndex]    INT           NULL
);

GO
CREATE TABLE [dbo].[Tax_ProductClass_Detail] (
    [ClassID]    NVARCHAR (15)   NOT NULL,
    [TaxID]      NVARCHAR (15)   NOT NULL,
    [TaxPercent] DECIMAL (18, 2) NULL,
    [RowIndex]   INT             NULL
);

GO
CREATE TABLE [dbo].[Temporary_Transaction] (
    [AutoKeyID]       INT            IDENTITY (1, 1) NOT NULL,
    [ID]              NVARCHAR (64)  NULL,
    [SName]           NVARCHAR (64)  NULL,
    [SKey]            NVARCHAR (275) NULL,
    [SValue]          NVARCHAR (69)  NULL,
    [SData]           NTEXT          NULL,
    [SBinData]        IMAGE          NULL,
    [SysDocID]        NVARCHAR (64)  NULL,
    [VoucherID]       NVARCHAR (64)  NULL,
    [CustomerID]      NVARCHAR (30)  NULL,
    [TransactionDate] NVARCHAR (30)  NULL
);

GO
CREATE TABLE [dbo].[Tenancy_Contract] (
    [ContractID]     NVARCHAR (30) NOT NULL,
    [Description]    NVARCHAR (64) NULL,
    [Landlord]       NVARCHAR (64) NULL,
    [Tenant]         NVARCHAR (64) NULL,
    [ContactID]      NVARCHAR (64) NULL,
    [Location]       NVARCHAR (30) NULL,
    [Status]         TINYINT       NULL,
    [ContractAmount] MONEY         NULL,
    [Installments]   TINYINT       NULL,
    [IssueDate]      SMALLDATETIME NULL,
    [ExpiryDate]     SMALLDATETIME NULL,
    [Note]           NTEXT         NULL,
    [DateCreated]    DATETIME      NULL,
    [DateUpdated]    DATETIME      NULL,
    [CreatedBy]      NVARCHAR (15) NULL,
    [UpdatedBy]      NVARCHAR (15) NULL,
    CONSTRAINT [PK_Tenancy_Contract] PRIMARY KEY CLUSTERED ([ContractID] ASC)
);

GO
CREATE TABLE [dbo].[Total_Mile_Report] (
    [VehicleID]      NVARCHAR (15)   NULL,
    [StartDate]      DATETIME        NULL,
    [TotalOdometer]  NUMERIC (15, 2) NULL,
    [TotalDriveTime] NUMERIC (15, 2) NULL,
    [TotalStopTime]  NUMERIC (15, 2) NULL,
    [TotalIdleTime]  NUMERIC (15, 2) NULL,
    [VehiclePlate]   NVARCHAR (250)  NULL,
    [FleetName]      NVARCHAR (250)  NULL
);

GO
CREATE TABLE [dbo].[TR_Application] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [CostCenterID]       NVARCHAR (15)   NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TransactionDate]    DATETIME        NULL,
    [PayeeType]          NVARCHAR (1)    NULL,
    [PayeeID]            NVARCHAR (64)   NULL,
    [Description]        NVARCHAR (255)  NULL,
    [BankFacilityID]     NVARCHAR (15)   NULL,
    [Reference]          NVARCHAR (15)   NULL,
    [DueDate]            DATETIME        NULL,
    [Amount]             MONEY           NULL,
    [AmountFC]           MONEY           NULL,
    [RequestSysDocID]    NVARCHAR (7)    NULL,
    [RequestVoucherID]   NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (255)  NULL,
    [IsVoid]             BIT             NULL,
    [POSysDocID]         NVARCHAR (7)    NULL,
    [POVoucherID]        NVARCHAR (15)   NULL,
    [InvoiceNos]         NVARCHAR (100)  NULL,
    [Authorizedby]       NVARCHAR (30)   NULL,
    [NoofInvoices]       INT             NULL,
    [NoofPL]             INT             NULL,
    [NoofBOL]            INT             NULL,
    [NoofGoods]          NVARCHAR (30)   NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (64)   NULL,
    [UpdatedBy]          NVARCHAR (64)   NULL,
    CONSTRAINT [PK_TR_Application] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Trade_License] (
    [TradeLicenseID] NVARCHAR (20)  NOT NULL,
    [Description]    NVARCHAR (64)  NULL,
    [Sponsors]       NVARCHAR (255) NULL,
    [ContactID]      NVARCHAR (64)  NULL,
    [Partners]       NVARCHAR (255) NULL,
    [RegisterNumber] NVARCHAR (20)  NULL,
    [LegalType]      NVARCHAR (30)  NULL,
    [IssuePlace]     NVARCHAR (30)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [RenewDate]      SMALLDATETIME  NULL,
    [Status]         TINYINT        NULL,
    [Note]           NTEXT          NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Trade_License] PRIMARY KEY CLUSTERED ([TradeLicenseID] ASC)
);

GO
CREATE TABLE [dbo].[Transaction_Details] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [AccountID]         NVARCHAR (64)   NULL,
    [Description]       NVARCHAR (255)  NULL,
    [PaymentMethodType] TINYINT         NULL,
    [Amount]            MONEY           NULL,
    [AmountFC]          MONEY           NULL,
    [ExpAmount]         MONEY           NULL,
    [ExpCode]           NVARCHAR (30)   NULL,
    [ExpPercent]        DECIMAL (18)    NULL,
    [RowIndex]          INT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [PayeeID]           NVARCHAR (64)   NULL,
    [BankFacilityID]    NVARCHAR (15)   NULL,
    [DueDate]           DATETIME        NULL,
    [PayeeType]         NVARCHAR (1)    NULL,
    [AnalysisID]        NVARCHAR (15)   NULL,
    [CostCenterID]      NVARCHAR (15)   NULL,
    [TaxOption]         TINYINT         NULL,
    [TaxGroupID]        NVARCHAR (15)   NULL,
    [TaxAmount]         DECIMAL (18, 5) NULL,
    [IsVoid]            BIT             NULL,
    [PaymentMethodID]   NVARCHAR (15)   NULL,
    [ChequebookID]      NVARCHAR (15)   NULL,
    [BankID]            NVARCHAR (15)   NULL,
    [CheckNumber]       NVARCHAR (20)   NULL,
    [CheckDate]         DATETIME        NULL,
    [ChequeID]          INT             NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [ConsignID]         NVARCHAR (22)   NULL,
    [ConsignExpenseID]  NVARCHAR (15)   NULL,
    [IsBilled]          BIT             NULL,
    [AttributeID1]      NVARCHAR (50)   NULL,
    [AttributeID2]      NVARCHAR (50)   NULL,
    [RefDate]           DATETIME        NULL,
    CONSTRAINT [FK_Transaction_Details_Account] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Transaction_Details_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID]),
    CONSTRAINT [FK_Transaction_Details_Cost_Center] FOREIGN KEY ([CostCenterID]) REFERENCES [dbo].[Cost_Center] ([CostCenterID])
);

GO
CREATE TABLE [dbo].[Transporter] (
    [TransporterID]   NVARCHAR (30)  NOT NULL,
    [TransporterName] NVARCHAR (64)  NOT NULL,
    [Note]            NVARCHAR (255) NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL
);

GO
CREATE TABLE [dbo].[UDF_Account] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_Candidate] (
    [EntityID] NVARCHAR (64) NULL,
    [TDEE]     NVARCHAR (30) NULL
);

GO
CREATE TABLE [dbo].[UDF_Contact] (
    [EntityID]   NVARCHAR (64) NOT NULL,
    [FATHERNAME] NVARCHAR (30) NULL,
    [ISMARRIED]  BIT           NULL,
    CONSTRAINT [PK_UDF_Contact] PRIMARY KEY CLUSTERED ([EntityID] ASC)
);

GO
CREATE TABLE [dbo].[UDF_Customer] (
    [EntityID] NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_UDF_Customer] PRIMARY KEY CLUSTERED ([EntityID] ASC)
);

GO
CREATE TABLE [dbo].[UDF_Employee] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_FixedAsset] (
    [EntityID] NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[UDF_Horse] (
    [EntityID] NVARCHAR (64) NULL,
    [TEST]     NVARCHAR (30) NULL,
    [YU]       NVARCHAR (30) NULL
);

GO
CREATE TABLE [dbo].[UDF_Job] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_Lead] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_Product] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_Property] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_PropertyUnit] (
    [EntityID] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[UDF_Setup] (
    [UDFTypeID]   NVARCHAR (64) NOT NULL,
    [FieldName]   NVARCHAR (15) NOT NULL,
    [DisplayName] NVARCHAR (30) NULL,
    [TableName]   NVARCHAR (30) NULL,
    CONSTRAINT [PK_UDF_Setup] PRIMARY KEY CLUSTERED ([UDFTypeID] ASC, [FieldName] ASC)
);

GO
CREATE TABLE [dbo].[UDF_Vendor] (
    [EntityID] NVARCHAR (64) NOT NULL,
    [LABID]    NVARCHAR (30) NULL,
    CONSTRAINT [PK_UDF_Vendor] PRIMARY KEY CLUSTERED ([EntityID] ASC)
);

GO
CREATE TABLE [dbo].[Unallocated_Lot_Items] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [TransactionDate] DATETIME        NULL,
    [RowIndex]        INT             NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [Quantity]        DECIMAL (18, 5) NULL,
    [Price]           DECIMAL (18, 5) NULL,
    CONSTRAINT [PK_Unallocated_Lot_Items] PRIMARY KEY CLUSTERED ([ID] ASC)
);

GO
CREATE TABLE [dbo].[Unit] (
    [UnitID]      NVARCHAR (15)  NOT NULL,
    [UnitName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED ([UnitID] ASC)
);

GO
CREATE TABLE [dbo].[User_Favorite] (
    [UserID]       NVARCHAR (15) NULL,
    [FavoriteType] TINYINT       NULL,
    [DocumentID]   NVARCHAR (15) NULL,
    [FavoriteName] NVARCHAR (64) NULL
);

GO
CREATE TABLE [dbo].[User_Group] (
    [GroupID]              NVARCHAR (15)  NOT NULL,
    [GroupName]            NVARCHAR (30)  NULL,
    [CanCreateCard]        BIT            NULL,
    [CanEditCard]          BIT            CONSTRAINT [DF_User_Group_CanEditCard] DEFAULT ((0)) NULL,
    [CanDeleteCard]        BIT            CONSTRAINT [DF_User_Group_CanDeleteCard] DEFAULT ((0)) NULL,
    [CanCreateTransaction] BIT            NULL,
    [CanEditTransaction]   BIT            NULL,
    [CanDeleteTransaction] BIT            NULL,
    [Note]                 NVARCHAR (255) NULL,
    [Inactive]             BIT            CONSTRAINT [DF_User Groups_IsInactive] DEFAULT ((0)) NULL,
    [CreatedBy]            NVARCHAR (15)  NULL,
    [UpdatedBy]            NVARCHAR (15)  NULL,
    [DateCreated]          DATETIME       NULL,
    [DateUpdated]          DATETIME       NULL,
    CONSTRAINT [PK_User Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[User_Group_Detail] (
    [GroupID] NVARCHAR (15) NOT NULL,
    [UserID]  NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_User Group Assignments] PRIMARY KEY CLUSTERED ([GroupID] ASC, [UserID] ASC),
    CONSTRAINT [FK_User Group Assignments_User Groups] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[User_Group] ([GroupID]),
    CONSTRAINT [FK_User Group Assignments_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO
CREATE TABLE [dbo].[User_Location_Detail] (
    [LocationID] NVARCHAR (15) NOT NULL,
    [UserID]     NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_User_Location_Detail] PRIMARY KEY CLUSTERED ([LocationID] ASC, [UserID] ASC)
);

GO
CREATE TABLE [dbo].[Users] (
    [UserID]                       NVARCHAR (15)  NOT NULL,
    [UserName]                     NVARCHAR (64)  NOT NULL,
    [EmployeeID]                   NVARCHAR (64)  NULL,
    [LocationID]                   NVARCHAR (15)  NULL,
    [DefaultSalespersonID]         NVARCHAR (15)  NULL,
    [DefaultInventoryLocationID]   NVARCHAR (15)  NULL,
    [DefaultTransactionLocationID] NVARCHAR (15)  NULL,
    [DefaultTransactionRegisterID] NVARCHAR (15)  NULL,
    [CanCreateCard]                BIT            NULL,
    [CanEditCard]                  BIT            NULL,
    [CanDeleteCard]                BIT            NULL,
    [CanCreateTransaction]         BIT            NULL,
    [CanEditTransaction]           BIT            NULL,
    [CanDeleteTransaction]         BIT            NULL,
    [Note]                         NVARCHAR (255) NULL,
    [Email]                        NVARCHAR (100) NULL,
    [Phone]                        NVARCHAR (30)  NULL,
    [CLUserPass]                   NVARCHAR (64)  NULL,
    [ISCLUser]                     BIT            NULL,
    [Inactive]                     BIT            NULL,
    [IsAdmin]                      BIT            NULL,
    [CreatedBy]                    NVARCHAR (15)  NULL,
    [UpdatedBy]                    NVARCHAR (15)  NULL,
    [DateCreated]                  DATETIME       NULL,
    [DateUpdated]                  DATETIME       NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle] (
    [VehicleID]              NVARCHAR (15)   NOT NULL,
    [VehicleName]            NVARCHAR (30)   NULL,
    [RegistrationNumber]     NVARCHAR (15)   NULL,
    [RegistrationCityID]     NVARCHAR (15)   NULL,
    [CountryID]              NVARCHAR (15)   NULL,
    [WorkingAreaID]          NVARCHAR (15)   NULL,
    [ChasisNumber]           NVARCHAR (20)   NULL,
    [VehicleTypeID]          NVARCHAR (15)   NULL,
    [Color]                  NVARCHAR (15)   NULL,
    [Year]                   SMALLINT        NULL,
    [Fuel]                   NVARCHAR (15)   NULL,
    [Model]                  NVARCHAR (15)   NULL,
    [FixedAssetID]           NVARCHAR (15)   NULL,
    [InsuranceVendorID]      NVARCHAR (64)   NULL,
    [InsurancePolicyNumber]  NVARCHAR (20)   NULL,
    [InsuranceExpiryDate]    DATETIME        NULL,
    [RegistrationExpiryDate] DATETIME        NULL,
    [CustomExpiryDate1]      DATETIME        NULL,
    [CustomExpiryDate2]      DATETIME        NULL,
    [CustomExpiryDate3]      DATETIME        NULL,
    [TrackingNumber]         NVARCHAR (30)   NULL,
    [LocationID]             NVARCHAR (15)   NULL,
    [DivisionID]             NVARCHAR (15)   NULL,
    [DriverID]               NVARCHAR (15)   NULL,
    [OwnedBy]                NVARCHAR (64)   NULL,
    [AnalysisID]             NVARCHAR (15)   NULL,
    [IsInactive]             BIT             NULL,
    [WeightCapacity]         DECIMAL (10, 2) NULL,
    [VehicleWeight]          DECIMAL (10, 2) NULL,
    [TrafficFileNo]          NVARCHAR (64)   NULL,
    [PlateNo]                NVARCHAR (64)   NULL,
    [Origin]                 NVARCHAR (15)   NULL,
    [Photo]                  IMAGE           NULL,
    [Note]                   NVARCHAR (500)  NULL,
    [DateCreated]            DATETIME        NULL,
    [DateUpdated]            DATETIME        NULL,
    [CreatedBy]              NVARCHAR (15)   NULL,
    [UpdatedBy]              NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([VehicleID] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle_Doc_Type] (
    [TypeID]      NVARCHAR (15)  NOT NULL,
    [TypeName]    NVARCHAR (64)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Remind]      BIT            CONSTRAINT [DF_Vehicle_Doc_Type_Remind] DEFAULT ((0)) NULL,
    [RemindDays]  NUMERIC (3)    NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Vehicle_Doc_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle_Document] (
    [VehicleID]      NVARCHAR (15)  NOT NULL,
    [DocumentNumber] NVARCHAR (30)  NOT NULL,
    [DocumentTypeID] NVARCHAR (15)  NOT NULL,
    [IssuePlace]     NVARCHAR (15)  NULL,
    [IssueDate]      SMALLDATETIME  NULL,
    [ExpiryDate]     SMALLDATETIME  NULL,
    [Remarks]        NVARCHAR (255) NULL,
    [RowIndex]       SMALLINT       NULL,
    [DateCreated]    DATETIME       NULL,
    [DateUpdated]    DATETIME       NULL,
    [CreatedBy]      NVARCHAR (15)  NULL,
    [UpdatedBy]      NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Vehicle_Document] PRIMARY KEY CLUSTERED ([VehicleID] ASC, [DocumentNumber] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle_Maintenance_Entry] (
    [SysDocID]                  NVARCHAR (7)    NOT NULL,
    [VoucherID]                 NVARCHAR (15)   NOT NULL,
    [VehicleNumber]             NVARCHAR (50)   NULL,
    [Odometer]                  NVARCHAR (50)   NULL,
    [ServiceType]               NVARCHAR (50)   NULL,
    [VendorID]                  NVARCHAR (64)   NOT NULL,
    [PurchaseFlow]              TINYINT         NULL,
    [DueDate]                   DATETIME        NULL,
    [BuyerID]                   NVARCHAR (64)   NULL,
    [CurrencyID]                NVARCHAR (5)    NULL,
    [CurrencyRate]              DECIMAL (18, 5) NULL,
    [TermID]                    NVARCHAR (15)   NULL,
    [SourceDocType]             TINYINT         NULL,
    [ServiceProvider]           NVARCHAR (50)   NULL,
    [Amount]                    NVARCHAR (50)   NULL,
    [IsCash]                    BIT             NULL,
    [IsImport]                  BIT             NULL,
    [TimeRequired]              NVARCHAR (50)   NULL,
    [TransactionDate]           DATETIME        NOT NULL,
    [IsVoid]                    BIT             NULL,
    [Reference]                 NVARCHAR (20)   NULL,
    [TaxOption]                 TINYINT         NULL,
    [PayeeTaxGroupID]           NVARCHAR (15)   NULL,
    [Discount]                  MONEY           NULL,
    [DiscountFC]                MONEY           NULL,
    [TaxAmount]                 MONEY           NULL,
    [TaxAmountFC]               MONEY           NULL,
    [Total]                     MONEY           NULL,
    [TotalFC]                   MONEY           NULL,
    [Note]                      NVARCHAR (4000) NULL,
    [Status]                    TINYINT         CONSTRAINT [DF_Vehicle_Maintenance_Entry_Status1] DEFAULT ((1)) NULL,
    [SourceSysDocID]            NVARCHAR (7)    NULL,
    [SourceVoucherID]           NVARCHAR (15)   NULL,
    [NextServiceScheduleStatus] BIT             NULL,
    [CreatedBy]                 NVARCHAR (15)   NULL,
    [DateCreated]               DATETIME        NULL,
    [UpdatedBy]                 NVARCHAR (15)   NULL,
    [DateUpdated]               DATETIME        NULL,
    CONSTRAINT [PK_Maintenance_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle_Maintenance_Entry_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NULL,
    [UnitPriceFC]      DECIMAL (18, 5) NULL,
    [LCost]            DECIMAL (18, 5) NULL,
    [LCostAmount]      MONEY           NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [TaxOption]        TINYINT         NULL,
    [TaxGroupID]       NVARCHAR (15)   NULL,
    [TaxAmount]        DECIMAL (18, 5) NULL,
    [RowIndex]         SMALLINT        NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [QuantityReceived] DECIMAL (18, 5) NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [Amount]           MONEY           NULL,
    [AmountFC]         MONEY           NULL,
    [RowSource]        TINYINT         NULL,
    [JobID]            NVARCHAR (50)   NULL,
    [CurrencyID]       NVARCHAR (15)   NULL,
    [CurrencyRate]     DECIMAL (18, 5) NULL,
    [RateType]         CHAR (1)        NULL
);

GO
CREATE TABLE [dbo].[Vehicle_Maintenance_Scheduler] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [VehicleNumber]    NVARCHAR (50)   NULL,
    [Odometer]         NVARCHAR (50)   NULL,
    [ServiceType]      NVARCHAR (50)   NULL,
    [ServiceProvider]  NVARCHAR (50)   NULL,
    [Amount]           NVARCHAR (50)   NULL,
    [TimeRequired]     NVARCHAR (50)   NULL,
    [MaintenanceDate]  DATETIME        NULL,
    [IsVoid]           BIT             NULL,
    [TrackMaintenance] NVARCHAR (50)   NULL,
    [Status]           BIT             NULL,
    [Note]             NVARCHAR (4000) NULL,
    [CreatedBy]        NVARCHAR (15)   NULL,
    [DateCreated]      DATETIME        NULL,
    [UpdatedBy]        NVARCHAR (15)   NULL,
    [DateUpdated]      DATETIME        NULL,
    CONSTRAINT [PK_Maintenance_Scheduler] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[Vehicle_Mileage_Track] (
    [SysDocID]        NVARCHAR (7)  NOT NULL,
    [VoucherID]       NVARCHAR (15) NOT NULL,
    [TransactionDate] DATETIME      NULL,
    [IsDateWise]      BIT           NULL,
    [Note]            NTEXT         NULL,
    [CreatedBy]       NVARCHAR (15) NULL,
    [DateCreated]     DATETIME      NULL,
    [UpdatedBy]       NVARCHAR (15) NULL,
    [DateUpdated]     DATETIME      NULL
);

GO
CREATE TABLE [dbo].[Vehicle_Mileage_Track_Detail] (
    [SysDocID]        NVARCHAR (7)   NOT NULL,
    [VoucherID]       NVARCHAR (15)  NOT NULL,
    [TripDate]        DATETIME       NULL,
    [TripTime]        DATETIME       NULL,
    [VehicleID]       NVARCHAR (15)  NULL,
    [Name]            NVARCHAR (50)  NULL,
    [Purpose]         NVARCHAR (50)  NULL,
    [DriverID]        NVARCHAR (15)  NULL,
    [PreviousReading] INT            NULL,
    [CurrentReading]  INT            NULL,
    [Mileage]         INT            NULL,
    [Remarks]         NVARCHAR (255) NULL,
    [RowIndex]        INT            NULL
);

GO
CREATE TABLE [dbo].[Vendor] (
    [VendorID]           NVARCHAR (64)  NOT NULL,
    [VendorName]         NVARCHAR (64)  NOT NULL,
    [ShortName]          NVARCHAR (64)  NULL,
    [ForeignName]        NVARCHAR (64)  NULL,
    [CompanyName]        NVARCHAR (64)  NULL,
    [LegalName]          NVARCHAR (64)  NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [TermID]             NVARCHAR (15)  NULL,
    [VendorClassID]      NVARCHAR (15)  NULL,
    [AcceptCheckPayment] BIT            NULL,
    [AcceptPDC]          BIT            NULL,
    [CreditLimitType]    TINYINT        NULL,
    [CreditAmount]       MONEY          NULL,
    [APAccountID]        NVARCHAR (64)  NULL,
    [CurrencyID]         NVARCHAR (15)  NULL,
    [AreaID]             NVARCHAR (15)  NULL,
    [VendorGroupID]      NVARCHAR (15)  NULL,
    [CountryID]          NVARCHAR (15)  NULL,
    [ShippingMethodID]   NVARCHAR (15)  NULL,
    [IsInactive]         BIT            NULL,
    [IsHold]             BIT            NULL,
    [IsHoldForPayment]   BIT            NULL,
    [IsServiceProvider]  BIT            NULL,
    [AllowOAP]           BIT            NULL,
    [Photo]              IMAGE          NULL,
    [Flag]               TINYINT        NULL,
    [BankName]           NVARCHAR (50)  NULL,
    [BankBranch]         NVARCHAR (100) NULL,
    [BankAccountNumber]  NVARCHAR (30)  NULL,
    [SwiftCode]          NVARCHAR (50)  NULL,
    [TaxOption]          TINYINT        NULL,
    [TaxGroupID]         NVARCHAR (15)  NULL,
    [TaxIDNumber]        NVARCHAR (30)  NULL,
    [PaymentTermID]      NVARCHAR (15)  NULL,
    [Note]               NVARCHAR (255) NULL,
    [PaymentMethodID]    NVARCHAR (15)  NULL,
    [ParentVendorID]     NVARCHAR (64)  NULL,
    [BuyerID]            NVARCHAR (64)  NULL,
    [PrimaryAddressID]   NVARCHAR (15)  NULL,
    [LicenseExpDate]     DATETIME       NULL,
    [ContractExpDate]    DATETIME       NULL,
    [Balance]            MONEY          NULL,
    [PDCAmount]          MONEY          NULL,
    [AllowConsignment]   BIT            NULL,
    [ConsignComPercent]  DECIMAL (5, 2) NULL,
    [UserDefined1]       NVARCHAR (64)  NULL,
    [UserDefined2]       NVARCHAR (64)  NULL,
    [UserDefined3]       NVARCHAR (64)  NULL,
    [UserDefined4]       NVARCHAR (64)  NULL,
    [ApprovalStatus]     TINYINT        NULL,
    [VerificationStatus] TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED ([VendorID] ASC),
    CONSTRAINT [FK_Vendor_Account] FOREIGN KEY ([APAccountID]) REFERENCES [dbo].[Account] ([AccountID]),
    CONSTRAINT [FK_Vendor_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[Area] ([AreaID]),
    CONSTRAINT [FK_Vendor_Buyer] FOREIGN KEY ([BuyerID]) REFERENCES [dbo].[Buyer] ([BuyerID]),
    CONSTRAINT [FK_Vendor_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]),
    CONSTRAINT [FK_Vendor_Payment_Method] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[Payment_Method] ([PaymentMethodID]),
    CONSTRAINT [FK_Vendor_Payment_Term] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]),
    CONSTRAINT [FK_Vendor_Shipping_Method] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Shipping_Method] ([ShippingMethodID]),
    CONSTRAINT [FK_Vendor_Vendor_Class] FOREIGN KEY ([VendorClassID]) REFERENCES [dbo].[Vendor_Class] ([ClassID]),
    CONSTRAINT [FK_Vendor_Vendor_Group] FOREIGN KEY ([VendorGroupID]) REFERENCES [dbo].[Vendor_Group] ([GroupID])
);

GO
CREATE TABLE [dbo].[Vendor_Address] (
    [AddressID]          NVARCHAR (15)  NOT NULL,
    [VendorID]           NVARCHAR (64)  NOT NULL,
    [ContactName]        NVARCHAR (64)  NULL,
    [ContactTitle]       NVARCHAR (30)  NULL,
    [Address1]           NVARCHAR (64)  NULL,
    [Address2]           NVARCHAR (64)  NULL,
    [Address3]           NVARCHAR (64)  NULL,
    [AddressPrintFormat] NVARCHAR (255) NULL,
    [City]               NVARCHAR (30)  NULL,
    [State]              NVARCHAR (30)  NULL,
    [PostalCode]         NVARCHAR (30)  NULL,
    [Country]            NVARCHAR (30)  NULL,
    [Department]         NVARCHAR (30)  NULL,
    [Phone1]             NVARCHAR (30)  NULL,
    [Phone2]             NVARCHAR (30)  NULL,
    [Fax]                NVARCHAR (30)  NULL,
    [Mobile]             NVARCHAR (30)  NULL,
    [Email]              NVARCHAR (64)  NULL,
    [Website]            NVARCHAR (255) NULL,
    [Comment]            NVARCHAR (255) NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Vendor_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [VendorID] ASC)
);

GO
CREATE TABLE [dbo].[Vendor_Class] (
    [ClassID]       NVARCHAR (15)  NOT NULL,
    [ClassName]     NVARCHAR (64)  NOT NULL,
    [APAccountID]   NVARCHAR (64)  NULL,
    [IsInactive]    BIT            NULL,
    [Note]          NVARCHAR (255) NULL,
    [TaxOption]     TINYINT        NULL,
    [TaxGroupID]    NVARCHAR (15)  NULL,
    [CreatedBy]     NVARCHAR (15)  NULL,
    [UpdatedBy]     NVARCHAR (15)  NULL,
    [DateCreated]   DATETIME       NULL,
    [DateUpdated]   DATETIME       NULL,
    [VendorGroupID] NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Vendor_Class] PRIMARY KEY CLUSTERED ([ClassID] ASC)
);

GO
CREATE TABLE [dbo].[Vendor_Contact_Detail] (
    [VendorID]  NVARCHAR (64) NOT NULL,
    [ContactID] NVARCHAR (64) NOT NULL,
    [JobTitle]  NVARCHAR (30) NULL,
    [Note]      NVARCHAR (64) NULL,
    [RowIndex]  SMALLINT      NULL,
    CONSTRAINT [PK_Vendor_Contact_Detail] PRIMARY KEY CLUSTERED ([VendorID] ASC, [ContactID] ASC)
);

GO
CREATE TABLE [dbo].[Vendor_Group] (
    [GroupID]     NVARCHAR (15)  NOT NULL,
    [GroupName]   NVARCHAR (30)  NOT NULL,
    [Note]        NVARCHAR (255) NULL,
    [Inactive]    BIT            CONSTRAINT [DF_Vendor_Group_Inactive] DEFAULT ((0)) NULL,
    [CreatedBy]   NVARCHAR (15)  NULL,
    [UpdatedBy]   NVARCHAR (15)  NULL,
    [DateCreated] DATETIME       NULL,
    [DateUpdated] DATETIME       NULL,
    CONSTRAINT [PK_Vendor_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)
);

GO
CREATE TABLE [dbo].[Vendor_Price_List] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [VendorID]          NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [BuyerID]           NVARCHAR (64)   NULL,
    [ValidDateFrom]     DATETIME        NULL,
    [ValidDateTo]       DATETIME        NULL,
    [ApplicableToChild] BIT             NULL,
    [Inactive]          BIT             NULL,
    [Status]            TINYINT         CONSTRAINT [DF_Vendor_Price_List_Status] DEFAULT ((1)) NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [TaxAmount]         MONEY           NULL,
    [Total]             MONEY           NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL
);

GO
CREATE TABLE [dbo].[Vendor_Price_List_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [VendorProductID] NVARCHAR (64)   NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Remarks]         NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        INT             NULL
);

GO
CREATE TABLE [dbo].[VER1002] (
    [Version] INT NULL
);

GO
CREATE TABLE [dbo].[Visa] (
    [VisaID]             NVARCHAR (30)  NOT NULL,
    [Description]        NVARCHAR (64)  NULL,
    [SponsorID]          NVARCHAR (15)  NULL,
    [VisaType]           TINYINT        NULL,
    [ApplicantName]      NVARCHAR (64)  NULL,
    [Days]               NUMERIC (3)    NULL,
    [Nationality]        NVARCHAR (15)  NULL,
    [Gender]             CHAR (1)       NULL,
    [BirthDate]          SMALLDATETIME  NULL,
    [ContactID]          NVARCHAR (64)  NULL,
    [PassportNumber]     NVARCHAR (15)  NULL,
    [PassportIssuePlace] NVARCHAR (30)  NULL,
    [PassportExpiryDate] DATETIME       NULL,
    [IssueDate]          DATETIME       NULL,
    [ValidityDate]       DATETIME       NULL,
    [IssuePlace]         NVARCHAR (30)  NULL,
    [ArrivalDate]        DATETIME       NULL,
    [ExpiryDate]         DATETIME       NULL,
    [DepartureDate]      DATETIME       NULL,
    [Note]               NVARCHAR (255) NULL,
    [Status]             TINYINT        NULL,
    [DateCreated]        DATETIME       NULL,
    [DateUpdated]        DATETIME       NULL,
    [CreatedBy]          NVARCHAR (15)  NULL,
    [UpdatedBy]          NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Company_Visa] PRIMARY KEY CLUSTERED ([VisaID] ASC)
);

GO
CREATE TABLE [dbo].[W3PL_Delivery] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [DivisionID]        NVARCHAR (15)   NULL,
    [CompanyID]         TINYINT         NULL,
    [CustomerID]        NVARCHAR (64)   NOT NULL,
    [TransactionDate]   DATETIME        NOT NULL,
    [SalespersonID]     NVARCHAR (64)   NULL,
    [SourceDocType]     TINYINT         NULL,
    [SalesFlow]         TINYINT         NULL,
    [IsExport]          BIT             NULL,
    [RequiredDate]      DATETIME        NULL,
    [ShippingAddressID] NVARCHAR (15)   NULL,
    [CustomerAddress]   NVARCHAR (255)  NULL,
    [Status]            TINYINT         CONSTRAINT [DF_W3PL_Delivery_Status_1] DEFAULT ((1)) NULL,
    [CurrencyID]        NVARCHAR (5)    NULL,
    [TermID]            NVARCHAR (15)   NULL,
    [ShippingMethodID]  NVARCHAR (15)   NULL,
    [JobID]             NVARCHAR (50)   NULL,
    [CostCategoryID]    NVARCHAR (30)   NULL,
    [IsVoid]            BIT             NULL,
    [Reference]         NVARCHAR (20)   NULL,
    [Reference2]        NVARCHAR (20)   NULL,
    [Discount]          MONEY           NULL,
    [Total]             MONEY           NULL,
    [PONumber]          NVARCHAR (15)   NULL,
    [IsInvoiced]        BIT             NULL,
    [IsShipped]         BIT             NULL,
    [ContainerNumber]   NVARCHAR (30)   NULL,
    [ContainerSizeID]   NVARCHAR (20)   NULL,
    [InvoiceSysDocID]   NVARCHAR (7)    NULL,
    [InvoiceVoucherID]  NVARCHAR (15)   NULL,
    [DriverID]          NVARCHAR (15)   NULL,
    [VehicleID]         NVARCHAR (15)   NULL,
    [Note]              NVARCHAR (4000) NULL,
    [DateCreated]       DATETIME        NULL,
    [DateUpdated]       DATETIME        NULL,
    [CreatedBy]         NVARCHAR (15)   NULL,
    [UpdatedBy]         NVARCHAR (15)   NULL,
    CONSTRAINT [PK_W3PL_Delivery] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[W3PL_Delivery_Detail] (
    [SysDocID]         NVARCHAR (7)    NOT NULL,
    [VoucherID]        NVARCHAR (15)   NOT NULL,
    [ProductID]        NVARCHAR (64)   NOT NULL,
    [Quantity]         DECIMAL (18, 5) NOT NULL,
    [QuantityReturned] DECIMAL (18, 5) NULL,
    [QuantityShipped]  DECIMAL (18, 5) NULL,
    [UnitPrice]        DECIMAL (18, 5) NOT NULL,
    [Description]      NVARCHAR (255)  NULL,
    [UnitID]           NVARCHAR (15)   NULL,
    [UnitQuantity]     DECIMAL (18, 5) NULL,
    [UnitFactor]       DECIMAL (18, 5) NULL,
    [FactorType]       NVARCHAR (1)    NULL,
    [SubunitPrice]     DECIMAL (18, 5) NULL,
    [RowIndex]         TINYINT         NULL,
    [LocationID]       NVARCHAR (15)   NULL,
    [SourceVoucherID]  NVARCHAR (15)   NULL,
    [SourceSysDocID]   NVARCHAR (7)    NULL,
    [SourceRowIndex]   INT             NULL,
    [RowSource]        TINYINT         NULL
);

GO
CREATE TABLE [dbo].[W3PL_Expense] (
    [SysDocID]        NVARCHAR (7)    NULL,
    [VoucherID]       NVARCHAR (15)   NULL,
    [ExpenseID]       NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL,
    [SourceSysDocID]  NVARCHAR (15)   NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [Description]     NVARCHAR (255)  NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [Reference]       NVARCHAR (15)   NULL,
    [CurrencyID]      NVARCHAR (15)   NULL,
    [CurrencyRate]    DECIMAL (18, 5) NULL,
    [RateType]        CHAR (1)        NULL
);

GO
CREATE TABLE [dbo].[W3PL_GRN] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [DivisionID]         NVARCHAR (15)   NULL,
    [CompanyID]          TINYINT         NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignLocationID]  NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_W3PL_GRN_Status_1] DEFAULT ((1)) NULL,
    [IsClosed]           BIT             NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsInvoiced]         BIT             NULL,
    [TransporterID]      NVARCHAR (50)   NULL,
    [ArrivalPort]        NVARCHAR (30)   NULL,
    [ArrivalDate]        DATETIME        NULL,
    [ContainerNo]        NVARCHAR (30)   NULL,
    [BLNo]               NVARCHAR (30)   NULL,
    [InvoiceSysDocID]    NVARCHAR (7)    NULL,
    [InvoiceVoucherID]   NVARCHAR (15)   NULL,
    [Note]               NVARCHAR (4000) NULL,
    [CloseDate]          DATETIME        NULL,
    [CloseNote]          NVARCHAR (255)  NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_Warehouse3PL_GRN] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[W3PL_GRN_Detail] (
    [SysDocID]          NVARCHAR (7)    NOT NULL,
    [VoucherID]         NVARCHAR (15)   NOT NULL,
    [ProductID]         NVARCHAR (64)   NOT NULL,
    [Quantity]          DECIMAL (18, 5) NOT NULL,
    [QuantityInvoiced]  DECIMAL (18, 5) NULL,
    [QuantityDelivered] DECIMAL (18, 5) NULL,
    [UnitPrice]         DECIMAL (18, 5) NULL,
    [Description]       NVARCHAR (255)  NULL,
    [UnitID]            NVARCHAR (15)   NULL,
    [UnitQuantity]      DECIMAL (18, 5) NULL,
    [UnitFactor]        DECIMAL (18, 5) NULL,
    [FactorType]        NVARCHAR (1)    NULL,
    [SubunitPrice]      DECIMAL (18, 5) NULL,
    [RowIndex]          TINYINT         NULL,
    [LocationID]        NVARCHAR (15)   NULL,
    [OrderVoucherID]    NVARCHAR (15)   NULL,
    [OrderSysDocID]     NVARCHAR (7)    NULL,
    [InvoiceRowIndex]   INT             NULL,
    [LastRentDate]      DATETIME        NULL
);

GO
CREATE TABLE [dbo].[W3PL_Invoice] (
    [SysDocID]           NVARCHAR (7)    NOT NULL,
    [VoucherID]          NVARCHAR (15)   NOT NULL,
    [CustomerID]         NVARCHAR (64)   NOT NULL,
    [TransactionDate]    DATETIME        NOT NULL,
    [SalesFlow]          TINYINT         NULL,
    [DueDate]            DATETIME        NULL,
    [IsCash]             BIT             NULL,
    [RegisterID]         NVARCHAR (15)   NULL,
    [SalespersonID]      NVARCHAR (64)   NULL,
    [ConsignSysDocID]    NVARCHAR (7)    NULL,
    [ConsignVoucherID]   NVARCHAR (15)   NULL,
    [RequiredDate]       DATETIME        NULL,
    [ShippingAddressID]  NVARCHAR (15)   NULL,
    [CustomerAddress]    NVARCHAR (255)  NULL,
    [Status]             TINYINT         CONSTRAINT [DF_W3PL_Invoice_Status_1] DEFAULT ((1)) NULL,
    [CurrencyID]         NVARCHAR (5)    NULL,
    [CurrencyRate]       DECIMAL (18, 5) NULL,
    [TermID]             NVARCHAR (15)   NULL,
    [ShippingMethodID]   NVARCHAR (15)   NULL,
    [IsVoid]             BIT             NULL,
    [Reference]          NVARCHAR (20)   NULL,
    [Discount]           MONEY           NULL,
    [DiscountFC]         MONEY           NULL,
    [TaxAmount]          MONEY           NULL,
    [TaxAmountFC]        MONEY           NULL,
    [CommissionPercent]  MONEY           NULL,
    [CommissionAmount]   MONEY           NULL,
    [Total]              MONEY           NULL,
    [TotalCOGS]          MONEY           NULL,
    [TotalFC]            MONEY           NULL,
    [PONumber]           NVARCHAR (15)   NULL,
    [IsDelivered]        BIT             CONSTRAINT [DF_W3PL_Invoice_IsDelivered_1] DEFAULT ((0)) NULL,
    [Note]               NVARCHAR (4000) NULL,
    [PaymentMethodType]  TINYINT         NULL,
    [RequireUpdate]      BIT             NULL,
    [ApprovalStatus]     TINYINT         NULL,
    [VerificationStatus] TINYINT         NULL,
    [DateCreated]        DATETIME        NULL,
    [DateUpdated]        DATETIME        NULL,
    [CreatedBy]          NVARCHAR (15)   NULL,
    [UpdatedBy]          NVARCHAR (15)   NULL,
    CONSTRAINT [PK_W3PL_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC)
);

GO
CREATE TABLE [dbo].[W3PL_Invoice_Detail] (
    [SysDocID]        NVARCHAR (7)    NOT NULL,
    [VoucherID]       NVARCHAR (15)   NOT NULL,
    [ProductID]       NVARCHAR (64)   NOT NULL,
    [Quantity]        DECIMAL (18, 5) NOT NULL,
    [DLQuantity]      DECIMAL (18, 5) NULL,
    [StoreQuantity]   DECIMAL (18, 5) NULL,
    [StartDate]       DATETIME        NULL,
    [EndDate]         DATETIME        NULL,
    [UnitWeight]      DECIMAL (18, 5) NULL,
    [TotalWeight]     DECIMAL (18, 5) NULL,
    [UnitPrice]       DECIMAL (18, 5) NOT NULL,
    [UnitPriceFC]     DECIMAL (18, 5) NULL,
    [Amount]          MONEY           NULL,
    [AmountFC]        MONEY           NULL,
    [ExpenseAmount]   MONEY           NULL,
    [UnitExpense]     MONEY           NULL,
    [Description]     NVARCHAR (255)  NULL,
    [UnitID]          NVARCHAR (15)   NULL,
    [UnitQuantity]    DECIMAL (18, 5) NULL,
    [UnitFactor]      DECIMAL (18, 5) NULL,
    [FactorType]      NVARCHAR (1)    NULL,
    [SubunitPrice]    DECIMAL (18, 5) NULL,
    [RowIndex]        TINYINT         NULL,
    [LocationID]      NVARCHAR (15)   NULL,
    [SourceSysDocID]  NVARCHAR (7)    NULL,
    [SourceVoucherID] NVARCHAR (15)   NULL,
    [SourceRowIndex]  INT             NULL
);

GO
CREATE TABLE [dbo].[WebDashboard] (
    [WebDashboardID]  NVARCHAR (30)  NOT NULL,
    [UserID]          NVARCHAR (15)  NOT NULL,
    [Name]            NVARCHAR (15)  NULL,
    [RowIndex]        INT            NULL,
    [ZoneIndex]       INT            NULL,
    [ZoneLayout]      NTEXT          NULL,
    [Layout]          NTEXT          NULL,
    [SelectedGadgets] NVARCHAR (MAX) NULL,
    [DateCreated]     DATETIME       NULL,
    [DateUpdated]     DATETIME       NULL,
    [CreatedBy]       NVARCHAR (15)  NULL,
    [UpdatedBy]       NVARCHAR (15)  NULL,
    CONSTRAINT [PK_WebDashboards] PRIMARY KEY CLUSTERED ([WebDashboardID] ASC, [UserID] ASC)
);

GO
CREATE TABLE [dbo].[Work_Location] (
    [WorkLocationID]   NVARCHAR (15)  NOT NULL,
    [WorkLocationName] NVARCHAR (64)  NOT NULL,
    [WorkLocationType] TINYINT        NULL,
    [JobID]            NVARCHAR (50)  NULL,
    [Note]             NVARCHAR (255) NULL,
    [Inactive]         BIT            CONSTRAINT [DF_Work_Location_Inactive] DEFAULT ((0)) NULL,
    [DateCreated]      DATETIME       NULL,
    [DateUpdated]      DATETIME       NULL,
    [CreatedBy]        NVARCHAR (15)  NULL,
    [UpdatedBy]        NVARCHAR (15)  NULL,
    CONSTRAINT [PK_Work_Location] PRIMARY KEY CLUSTERED ([WorkLocationID] ASC)
);

GO
CREATE VIEW [dbo].[Ax_Expense_Journals]
AS
SELECT JD.AccountID,
       J.JournalDate AS Date,
       J.VoucherID,
       AG.GroupName,
       AC.AccountName,
       AC.Alias,
       ISNULL(JD.Debit, 0) - ISNULL(JD.Credit, 0) AS Amount,
       AN.AnalysisName AS Analysis
FROM   dbo.Journal_Details AS JD
       INNER JOIN
       dbo.Journal AS J
       ON J.SysDocID = JD.SysDocID
          AND J.VoucherID = JD.VoucherID
       INNER JOIN
       dbo.Account AS AC
       ON AC.AccountID = JD.AccountID
       INNER JOIN
       dbo.Account_Group AS AG
       ON AC.GroupID = AG.GroupID
       LEFT OUTER JOIN
       dbo.Analysis AS AN
       ON AN.AnalysisID = JD.AnalysisID
WHERE  (AG.TypeID = 4)
       AND (ISNULL(J.IsVoid, 'False') = 'False');

GO
CREATE VIEW [dbo].[Axo_Consign_Item_Lot]
AS
WITH     MyCTE
AS       (SELECT ItemCode,
                 LotNumber,
                 SourceLotNumber,
                 ReceiptNumber,
                 LocationID,
                 LotQty - SoldQty AS Quantity
          FROM   dbo.Product_Lot AS LOT1
          WHERE  (SourceLotNumber IS NULL)
          UNION ALL
          SELECT LOT2.ItemCode,
                 LOT2.LotNumber,
                 LOT2.SourceLotNumber,
                 LOT2.ReceiptNumber,
                 LOT2.LocationID,
                 LOT2.LotQty - LOT2.SoldQty AS Quantity
          FROM   dbo.Product_Lot AS LOT2
                 INNER JOIN
                 MyCTE AS MyCTE_2
                 ON LOT2.SourceLotNumber = MyCTE_2.LotNumber
          WHERE  (LOT2.SourceLotNumber IS NOT NULL))
SELECT   TOP (100) PERCENT MyCTE_1.ItemCode,
                           P.Description,
                           ISNULL(MyCTE_1.SourceLotNumber, MyCTE_1.LotNumber) AS LotNumber,
                           PL.Reference,
                           PL.ReceiptNumber,
                           LOC.LocationName,
                           PL.SupplierCode + ' | ' + Ven.VendorName AS Suplier,
                           MyCTE_1.Quantity
FROM     MyCTE AS MyCTE_1
         INNER JOIN
         dbo.Product_Lot AS PL
         ON PL.LotNumber = ISNULL(MyCTE_1.SourceLotNumber, MyCTE_1.LotNumber)
         INNER JOIN
         dbo.Product AS P
         ON P.ProductID = MyCTE_1.ItemCode
         INNER JOIN
         dbo.Location AS LOC
         ON LOC.LocationID = MyCTE_1.LocationID
         LEFT OUTER JOIN
         dbo.Vendor AS Ven
         ON Ven.VendorID = PL.SupplierCode
WHERE    (P.ItemType = 5)
ORDER BY MyCTE_1.ItemCode, P.Description;

GO
CREATE VIEW [dbo].[Axo_Customer_Balance_Summary]
AS
SELECT DISTINCT TOP (100) PERCENT ARJ.CustomerID,
                                  CUS.CustomerName,
                                  ISNULL((SELECT SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0)) + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss, 0)) AS Expr1
                                                                                                     FROM   dbo.AR_Payment_Allocation AS ARP
                                                                                                     WHERE  (CustomerID = ARJ.CustomerID)), 0) AS Expr1
                                          FROM   dbo.ARJournal AS ARJ2
                                          WHERE  (ARJ.CustomerID = CustomerID)
                                                 AND (ISNULL(IsPDCRow, 'False') = 'False')
                                                 AND (ISNULL(IsVoid, 'False') = 'False')), 0) AS Balance,
                                  (SELECT ISNULL(SUM(Amount), 0) AS Expr1
                                   FROM   dbo.Cheque_Received AS CR
                                   WHERE  (Status IN (1, 3, 4))
                                          AND (PayeeType = 'C')
                                          AND (ARJ.CustomerID = PayeeID)
                                          AND (ISNULL(IsVoid, 'False') = 'False')) AS PDC
FROM   dbo.ARJournal AS ARJ
       INNER JOIN
       dbo.Customer AS CUS
       ON ARJ.CustomerID = CUS.CustomerID
WHERE  (ISNULL(ARJ.IsVoid, 'False') = 'False')
       AND (ISNULL(ARJ.IsPDCRow, 'False') = 'False')
       AND (ISNULL((SELECT SUM(ISNULL(Debit, 0) - ISNULL(Credit, 0)) + ISNULL((SELECT SUM(ISNULL(RealizedGainLoss, 0)) AS Expr1
                                                                               FROM   dbo.AR_Payment_Allocation AS ARP
                                                                               WHERE  (CustomerID = ARJ.CustomerID)), 0) AS Expr1
                    FROM   dbo.ARJournal AS ARJ2
                    WHERE  (ARJ.CustomerID = CustomerID)
                           AND (ISNULL(IsPDCRow, 'False') = 'False')
                           AND (ISNULL(IsVoid, 'False') = 'False')), 0) <> 0);

GO
CREATE VIEW [dbo].[AXO_Journal]
AS

SELECT J.JournalID,JD.JournalDetailID,JD.DivisionID, CONVERT(date, J.JournalDate, 106) AS JournalDate, J.SysDocID, J.VoucherID, J.SysDocType, JD.AccountID, ACC.AccountName,AG.GroupID,
CASE WHEN AG.TypeID=1 THEN 'Asset' WHEN AG.TypeID=2 THEN 'Liability' WHEN AG.TypeID=3 THEN 'Income' WHEN AG.TypeID=4 THEN 'Expense' WHEN AG.TypeID=5 THEN 'Capital' END AS [Type],
AG.GroupName, 
ISNULL(JD.Debit, 0) AS Debit, ISNULL(JD.Credit, 0) AS Credit,ISNULL(JD.DebitFC, 0) AS DebitFC, ISNULL(JD.CreditFC, 0) AS CreditFC,  J.Reference, J.Reference2,  J.Narration, J.Note, 
JD.Description AS JD_Description, 
JD.CurrencyID AS JD_CurrencyID, JD.CurrencyRate AS JD_CurrencyRate, JD.Reference AS JD_Reference, JD.PayeeID, 
JD.PayeeType, JD.AnalysisID, JD.CostCenterID, JD.BankID, 
JD.CheckbookID, JD.CheckID, JD.CheckDate, JD.CheckNumber, JD.IsARAP, JD.JobID, 
JD.CostCategoryID,  JD.JDDate, JD.AttributeID1, JD.AttributeID2
FROM Journal J INNER JOIN
Journal_Details JD WITH(NOLOCK) ON J.JournalID = JD.JournalID INNER JOIN
Account AS ACC WITH(NOLOCK) ON ACC.AccountID = JD.AccountID
INNER JOIN Account_Group AG WITH(NOLOCK) ON ACC.GroupID = AG.GroupID
WHERE (ISNULL(J.IsVoid, 0) = 0);

GO
CREATE VIEW [dbo].[AXO_Lot_Lotledger]
AS
SELECT ReceiptDate,
       LocationID,
       LotNumber,
       ISNULL(SourceLotNumber, LotNumber) AS 'SrcLotNo',
       ItemCode,
       LotQty,
       RowIndex,
       DocID,
       ReceiptNumber,
       ProductionDate,
       ExpiryDate,
       Reference
FROM   Product_Lot
UNION
SELECT PLS.TransactionDate,
       PLS.LocationID,
       PLS.LotNo,
       PL.LotNumber AS 'SrcLotNo',
       PLS.ItemCode,
       PLS.SoldQty * -1,
       PLS.RowIndex,
       PLS.DocID,
       PLS.InvoiceNumber,
       ProductionDate,
       ExpiryDate,
       Reference
FROM   Product_Lot_Sales AS PLS
       INNER JOIN
       Product_Lot AS PL
       ON ISNULL(PL.SourceLotNumber, PL.LotNumber) = PLS.LotNo
          AND PL.ItemCode = PLS.ItemCode
          AND PLS.DocID = PL.DocID
          AND PLS.InvoiceNumber = PL.ReceiptNumber;

GO
CREATE VIEW [dbo].[Axo_Product_Aging]
AS
SELECT   TOP (100) PERCENT PL.ProductID,
                           DATEDIFF(Day, MIN(ISNULL(PL.ReceiptDate, GETDATE())), GETDATE()) AS Age
FROM     dbo.Axo_Product_Lot_Quantity AS PL
         INNER JOIN
         dbo.Location AS LOC
         ON LOC.LocationID = PL.LocationID
WHERE    (ISNULL(LOC.IsConsignOutLocation, 'False') = 'False')
GROUP BY PL.ProductID
ORDER BY PL.ProductID;

GO
CREATE VIEW [dbo].[Axo_Product_Lot_Quantity]
AS
SELECT   TOP (100) PERCENT PL.LotNumber,
                           PL.ItemCode AS ProductID,
                           PL.LocationID,
                           PL.AvgCost,
                           PL.Cost,
                           SUM(PL2.LotQty) AS ReceivedQuantity,
                           SUM(PL.LotQty) AS LotQty,
                           SUM(PL.SoldQty) AS SoldQty,
                           SUM(PL.LotQty - PL.SoldQty) AS Quantity,
                           PL2.DocID,
                           PL2.ReceiptNumber,
                           PL2.ReceiptDate,
                           PL2.SupplierCode,
                           PL2.Reference,
                           PL2.ProductionDate,
                           PL2.ExpiryDate,
                           LOC.LocationName,
                           PL2.Reference2
FROM     (SELECT ISNULL(SourceLotNumber, LotNumber) AS LotNumber,
                 ItemCode,
                 Cost,
                 AvgCost,
                 LotQty - ISNULL(ReturnedQty, 0) AS LotQty,
                 SoldQty,
                 LocationID
          FROM   dbo.Product_Lot AS Lot
          WHERE  (LotQty - ISNULL(SoldQty, 0) - ISNULL(ReturnedQty, 0) <> 0)) AS PL
         INNER JOIN
         dbo.Product_Lot AS PL2
         ON PL.LotNumber = PL2.LotNumber
         INNER JOIN
         dbo.Location AS LOC
         ON LOC.LocationID = PL.LocationID
GROUP BY PL.LotNumber, PL.ItemCode, PL.LocationID, PL.AvgCost, PL.Cost, PL2.DocID, PL2.ReceiptNumber, PL2.ReceiptDate, PL2.SupplierCode, PL2.Reference, PL2.Reference2, PL2.ProductionDate, PL2.ExpiryDate, LOC.LocationName
ORDER BY ProductID, PL.LotNumber;

GO
CREATE VIEW [dbo].[Axo_Purchase_Detail]
AS
SELECT Type,
       [Doc ID],
       [Doc Number],
       ProductID,
       Description,
       ISNULL(COGS1, COGS2) AS COGS,
       ROUND(ISNULL(COGS1, COGS2) / Quantity, 5) AS AverageCost,
       VendorID,
       Date,
       BuyerID,
       Amount - ISNULL(COGS1, COGS2) AS Profit,
       Reference,
       CurrencyID,
       CurrencyRate,
       Quantity,
       UnitPrice,
       Amount,
       AmountFC
FROM   (SELECT Type,
               [Doc ID],
               [Doc Number],
               ProductID,
               Description,
               RowIndex,
               SourceSysDocID,
               SourceVoucherID,
               (SELECT TOP (1) -(1 * AssetValue) AS Expr1
                FROM   dbo.Inventory_Transactions AS XIT
                WHERE  (SysDocID = X.[Doc ID])
                       AND (VoucherID = X.[Doc Number])
                       AND (ProductID = X.ProductID)
                       AND (RowIndex = X.RowIndex)
                       AND (ABS(Quantity) = ABS(X.Quantity))) AS COGS1,
               (SELECT -(1 * AssetValue) AS Expr1
                FROM   dbo.Inventory_Transactions AS XIT
                WHERE  (SysDocID = X.SourceSysDocID)
                       AND (VoucherID = X.SourceVoucherID)
                       AND (ProductID = X.ProductID)
                       AND (RowIndex = X.SourceRowIndex)
                       AND (ABS(Quantity) = ABS(X.Quantity))) AS COGS2,
               SourceRowIndex,
               VendorID,
               Date,
               BuyerID,
               Reference,
               CurrencyID,
               CurrencyRate,
               Quantity,
               UnitPrice,
               Amount,
               AmountFC
        FROM   (SELECT 33 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       SID.OrderSysDocID AS SourceSysDocID,
                       SID.OrderVoucherID AS SourceVoucherID,
                       SID.OrderRowIndex AS SourceRowIndex,
                       SI.VendorID,
                       SI.TransactionDate AS Date,
                       SI.BuyerID,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       SID.Quantity,
                       SID.UnitPrice,
                       SID.Amount,
                       SID.AmountFC
                FROM   dbo.Purchase_Invoice AS SI
                       INNER JOIN
                       dbo.Purchase_Invoice_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)
                UNION
                SELECT 35 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       NULL AS SourceSysDocID,
                       NULL AS SourceVoucherID,
                       NULL AS SourceRowIndex,
                       SI.VendorID,
                       SI.TransactionDate AS Date,
                       SI.BuyerID,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       -(1 * SID.Quantity) AS Quantity,
                       SID.UnitPrice,
                       -(1 * SID.Quantity * SID.UnitPrice) AS Amount,
                       -(1 * SID.Quantity * SID.UnitPriceFC) AS AmountFC
                FROM   dbo.Purchase_Return AS SI
                       INNER JOIN
                       dbo.Purchase_Return_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)
                UNION
                SELECT 56 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       SI.ConsignSysDocID AS SourceSysDocID,
                       SI.ConsignVoucherID AS SourceVoucherID,
                       SID.ConsignRowIndex AS SourceRowIndex,
                       SI.VendorID,
                       SI.TransactionDate AS Date,
                       SI.SalespersonID,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       SID.Quantity,
                       SID.UnitPrice,
                       SID.Amount,
                       SID.AmountFC
                FROM   dbo.ConsignIn_Settlement AS SI
                       INNER JOIN
                       dbo.ConsignIn_Settlement_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)) AS X) AS Purchase;

GO
CREATE VIEW [dbo].[Axo_Sales_Detail]
AS
SELECT Sale.Type,
       Sale.[Doc ID],
       Sale.[Doc Number],
       Sale.ProductID,
       Sale.Description,
       CASE WHEN pd.ItemType = 5 THEN Amount ELSE COGS END AS COGS,
       ROUND(CASE WHEN pd.ItemType = 5 THEN Amount ELSE COGS END / Sale.Quantity, 5) AS AverageCost,
       Sale.CustomerID,
       Sale.Date,
       Sale.SalespersonID,
       Sale.ReportTo,
       Sale.Amount - CASE WHEN pd.ItemType = 5 THEN Amount ELSE COGS END AS Profit,
       Sale.IsExport,
       Sale.Reference,
       Sale.CurrencyID,
       Sale.CurrencyRate,
       Sale.Quantity,
       Sale.UnitPrice,
       Sale.Amount,
       Sale.AmountFC
FROM   (SELECT Type,
               [Doc ID],
               [Doc Number],
               ProductID,
               Description,
               RowIndex,
               SourceSysDocID,
               SourceVoucherID,
               (SELECT SUM(-(1 * AssetValue)) AS COGS
                FROM   dbo.Inventory_Transactions AS XIT
                WHERE  (X.ITRowID = TransactionID)) + ISNULL((SELECT SUM(-(1 * AssetValue)) AS COGS
                                                              FROM   dbo.Inventory_Transactions AS XIT
                                                              WHERE  (SysDocType = 29)
                                                                     AND (X.ITRowID = RefTransactionID)), 0) AS COGS,
               CustomerID,
               Date,
               SalespersonID,
               ReportTo,
               IsExport,
               Reference,
               CurrencyID,
               CurrencyRate,
               Quantity,
               UnitPrice,
               Amount,
               AmountFC
        FROM   (SELECT 25 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       SID.OrderSysDocID AS SourceSysDocID,
                       SID.OrderVoucherID AS SourceVoucherID,
                       SID.OrderRowIndex AS SourceRowIndex,
                       SI.CustomerID,
                       SI.TransactionDate AS Date,
                       SI.SalespersonID,
                       SI.ReportTo,
                       SI.IsExport,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       SID.Quantity,
                       CASE WHEN sid.FactorType = 'M' THEN sid.UnitPrice * sid.UnitFactor WHEN sid.FactorType = 'D' THEN sid.UnitPrice / sid.UnitFactor WHEN sid.FactorType IS NULL THEN sid.UnitPrice END AS [UnitPrice],
                       SID.Amount,
                       SID.AmountFC,
                       SID.ITRowID
                FROM   dbo.Sales_Invoice AS SI
                       INNER JOIN
                       dbo.Sales_Invoice_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)
                UNION
                SELECT 46 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       NULL AS SourceSysDocID,
                       NULL AS SourceVoucherID,
                       NULL AS SourceRowIndex,
                       SI.CustomerID,
                       SI.TransactionDate AS Date,
                       SI.SalespersonID,
                       '' AS ReportTo,
                       'False' AS IsExport,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       SID.Quantity,
                       CASE WHEN sid.FactorType = 'M' THEN sid.UnitPrice * sid.UnitFactor WHEN sid.FactorType = 'D' THEN sid.UnitPrice / sid.UnitFactor WHEN sid.FactorType IS NULL THEN sid.UnitPrice END AS [UnitPrice],
                       SID.Amount,
                       SID.AmountFC,
                       SID.ITRowID
                FROM   dbo.Sales_POS AS SI
                       INNER JOIN
                       dbo.Sales_POS_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)
                UNION
                SELECT 27 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       NULL AS SourceSysDocID,
                       NULL AS SourceVoucherID,
                       NULL AS SourceRowIndex,
                       SI.CustomerID,
                       SI.TransactionDate AS Date,
                       SI.SalespersonID,
                       SI.ReportTo,
                       'False' AS IsExport,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       -(1 * SID.Quantity) AS Quantity,
                       CASE WHEN sid.FactorType = 'M' THEN sid.UnitPrice * sid.UnitFactor WHEN sid.FactorType = 'D' THEN sid.UnitPrice / sid.UnitFactor WHEN sid.FactorType IS NULL THEN sid.UnitPrice END AS [UnitPrice],
                       -(1 * SID.Amount) AS Amount,
                       -(1 * SID.AmountFC) AS AmountFC,
                       SID.ITRowID
                FROM   dbo.Sales_Return AS SI
                       INNER JOIN
                       dbo.Sales_Return_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)
                UNION
                SELECT 48 AS Type,
                       SID.SysDocID AS [Doc ID],
                       SID.VoucherID AS [Doc Number],
                       SID.ProductID,
                       SID.Description,
                       SID.RowIndex,
                       SI.ConsignSysDocID AS SourceSysDocID,
                       SI.ConsignVoucherID AS SourceVoucherID,
                       SID.ConsignRowIndex AS SourceRowIndex,
                       SI.CustomerID,
                       SI.TransactionDate AS Date,
                       SI.SalespersonID,
                       '' AS ReportTo,
                       'False' AS IsExport,
                       SI.Reference,
                       SI.CurrencyID,
                       SI.CurrencyRate,
                       SID.Quantity,
                       CASE WHEN sid.FactorType = 'M' THEN sid.UnitPrice * sid.UnitFactor WHEN sid.FactorType = 'D' THEN sid.UnitPrice / sid.UnitFactor WHEN sid.FactorType IS NULL THEN sid.UnitPrice END AS [UnitPrice],
                       SID.Amount,
                       SID.AmountFC,
                       SID.ITRowID
                FROM   dbo.ConsignOut_Settlement AS SI
                       INNER JOIN
                       dbo.ConsignOut_Settlement_Detail AS SID
                       ON SI.SysDocID = SID.SysDocID
                          AND SI.VoucherID = SID.VoucherID
                WHERE  (ISNULL(SI.IsVoid, 'False') = 'False')
                       AND (SID.Quantity <> 0)) AS X) AS Sale
       INNER JOIN
       dbo.Product AS PD
       ON Sale.ProductID = PD.ProductID;

GO
CREATE VIEW [dbo].[Axo_Sales_Summary]
AS
SELECT 25 AS [Type],
       SysDocID AS [Doc ID],
       VoucherID AS [Doc Number],
       SI.CustomerID,
       Cus.CustomerName,
       TransactionDate AS [Date],
       S.SalespersonID,
       IsExport,
       S.FullName AS [Salesperson Name],
       Reference,
       SI.CurrencyID,
       CurrencyRate,
       Total,
       TotalFC,
       Discount,
       DiscountFC,
       TaxAmount,
       TaxAmountFC
FROM   Sales_Invoice AS SI
       INNER JOIN
       Customer AS CUS
       ON Cus.customerID = SI.CustomerID
       LEFT OUTER JOIN
       Salesperson AS S
       ON S.SalespersonID = SI.SalespersonID
WHERE  ISNULL(IsVoid, 'False') = 'False'
UNION
SELECT 46 AS [Type],
       SysDocID AS [Doc ID],
       VoucherID AS [Doc Number],
       SI.CustomerID,
       Cus.CustomerName,
       TransactionDate AS [Date],
       S.SalespersonID,
       'False' AS IsExport,
       S.FullName AS [Salesperson Name],
       Reference,
       SI.CurrencyID,
       CurrencyRate,
       Total,
       TotalFC,
       Discount,
       DiscountFC,
       TaxAmount,
       TaxAmountFC
FROM   Sales_POS AS SI
       INNER JOIN
       Customer AS CUS
       ON Cus.customerID = SI.CustomerID
       LEFT OUTER JOIN
       Salesperson AS S
       ON S.SalespersonID = SI.SalespersonID
WHERE  ISNULL(IsVoid, 'False') = 'False'
UNION
SELECT 27 AS [Type],
       SysDocID AS [Doc ID],
       VoucherID AS [Doc Number],
       SI.CustomerID,
       Cus.CustomerName,
       TransactionDate AS [Date],
       S.SalespersonID,
       'False' AS IsExport,
       S.FullName AS [Salesperson Name],
       Reference,
       SI.CurrencyID,
       CurrencyRate,
       -1 * Total AS Total,
       -1 * TotalFC AS TotalFC,
       -1 * Discount AS Discount,
       -1 * DiscountFC AS DiscountFC,
       -1 * TaxAmount AS TaxAmount,
       -1 * TaxAmountFC AS TaxAmountFC
FROM   Sales_Return AS SI
       INNER JOIN
       Customer AS CUS
       ON Cus.customerID = SI.CustomerID
       LEFT OUTER JOIN
       Salesperson AS S
       ON S.SalespersonID = SI.SalespersonID
WHERE  ISNULL(IsVoid, 'False') = 'False'
UNION
SELECT 48 AS [Type],
       SysDocID AS [Doc ID],
       VoucherID AS [Doc Number],
       SI.CustomerID,
       Cus.CustomerName,
       TransactionDate AS [Date],
       S.SalespersonID,
       'False' AS IsExport,
       S.FullName AS [Salesperson Name],
       Reference,
       SI.CurrencyID,
       CurrencyRate,
       Total,
       TotalFC,
       Discount,
       DiscountFC,
       TaxAmount,
       TaxAmountFC
FROM   ConsignOut_Settlement AS SI
       INNER JOIN
       Customer AS CUS
       ON Cus.customerID = SI.CustomerID
       LEFT OUTER JOIN
       Salesperson AS S
       ON S.SalespersonID = SI.SalespersonID
WHERE  ISNULL(IsVoid, 'False') = 'False';

GO
CREATE VIEW [dbo].[axo_stemp]
AS
SELECT CONVERT (DATE, SI.TransactionDate) AS TransactionDate,
       SID.ProductID,
       SID.Description,
       SID.Quantity,
       SID.LocationID
FROM   Sales_Invoice_Detail AS SID
       INNER JOIN
       Sales_Invoice AS SI
       ON SID.SysDocID = SI.SysDocID
          AND SID.VoucherID = SI.VoucherID;

GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [FK_Account_Account_Group] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Account_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [FK_Account_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID]);

GO
ALTER TABLE [dbo].[Analysis]
    ADD CONSTRAINT [FK_Analysis_Analysis_Group] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Analysis_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[APJournal]
    ADD CONSTRAINT [FK_APJournal_Vendor] FOREIGN KEY ([VendorID]) REFERENCES [dbo].[Vendor] ([VendorID]);

GO
ALTER TABLE [dbo].[ARJournal]
    ADD CONSTRAINT [FK_ARJournal_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]);

GO
ALTER TABLE [dbo].[Bank_Facility]
    ADD CONSTRAINT [FK_BankFacility_BankFacilityGroup] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Bank_Facility_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[Cheque_Issued]
    ADD CONSTRAINT [FK_Cheque_Issued_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID]);

GO
ALTER TABLE [dbo].[Cheque_Received]
    ADD CONSTRAINT [FK_Cheque_Received_Account] FOREIGN KEY ([PDCAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Cheque_Received]
    ADD CONSTRAINT [FK_Cheque_Received_Cheque_Received] FOREIGN KEY ([SysDocID], [VoucherID], [ChequeNumber], [BankID], [PayeeType], [PayeeID]) REFERENCES [dbo].[Cheque_Received] ([SysDocID], [VoucherID], [ChequeNumber], [BankID], [PayeeType], [PayeeID]);

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [FK_Company Information_Users] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [FK_Company Information_Users1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Account] FOREIGN KEY ([ARAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Customer_Address]
    ADD CONSTRAINT [FK_Customer_Address_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[Area] ([AreaID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([CurrencyID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Customer] FOREIGN KEY ([ParentCustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Customer_Class] FOREIGN KEY ([CustomerClassID]) REFERENCES [dbo].[Customer_Class] ([ClassID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Customer_Group] FOREIGN KEY ([CustomerGroupID]) REFERENCES [dbo].[Customer_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Division] FOREIGN KEY ([DivisionID]) REFERENCES [dbo].[Division] ([DivisionID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Payment_Method] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[Payment_Method] ([PaymentMethodID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Payment_Term] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Payment_Term1] FOREIGN KEY ([PaymentTermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Salesperson] FOREIGN KEY ([SalesPersonID]) REFERENCES [dbo].[Salesperson] ([SalespersonID]);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [FK_Customer_Shipping_Method] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Shipping_Method] ([ShippingMethodID]);

GO
ALTER TABLE [dbo].[Employee_DisciplinaryAction]
    ADD CONSTRAINT [FK_Employee_DisciplinaryAction_Employee_DisciplinaryAction] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Employee_DisciplinaryAction]
    ADD CONSTRAINT [FK_Employee_DisciplinaryAction_Employee_DisciplinaryAction1] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_DisciplinaryAction] ([ActivityID]);

GO
ALTER TABLE [dbo].[Employee_Journal]
    ADD CONSTRAINT [FK_Employee_Journal_Employee] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID]);

GO
ALTER TABLE [dbo].[Employee_Leave_Request]
    ADD CONSTRAINT [FK_Employee_Leave_Request_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Employee_Promotion]
    ADD CONSTRAINT [FK_Employee_Promotion_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Employee_Rehire]
    ADD CONSTRAINT [FK_Employee_Rehire_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Employee_Resumption]
    ADD CONSTRAINT [FK_Employee_Resumption_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Employee_Termination]
    ADD CONSTRAINT [FK_Employee_Termination_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]);

GO
ALTER TABLE [dbo].[Employee_Transfer]
    ADD CONSTRAINT [FK_Employee_Transfer_Employee_Activity] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Employee_Activity] ([ActivityID]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[Journal]
    ADD CONSTRAINT [FK_GL Transactions_Currencies] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([CurrencyID]);

GO
ALTER TABLE [dbo].[GL_Transaction]
    ADD CONSTRAINT [FK_GL_Transaction_Account_First] FOREIGN KEY ([FirstAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[GL_Transaction]
    ADD CONSTRAINT [FK_GL_Transaction_Account_Second] FOREIGN KEY ([SecondAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[GL_Transaction]
    ADD CONSTRAINT [FK_GL_Transaction_GL_Transaction] FOREIGN KEY ([SysDocID], [VoucherID]) REFERENCES [dbo].[GL_Transaction] ([SysDocID], [VoucherID]);

GO
ALTER TABLE [dbo].[Inventory_Transactions]
    ADD CONSTRAINT [FK_Inventory_Transactions_Location] FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Location] ([LocationID]);

GO
ALTER TABLE [dbo].[Inventory_Transactions]
    ADD CONSTRAINT [FK_Inventory_Transactions_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]);

GO
ALTER TABLE [dbo].[Inventory_Transactions]
    ADD CONSTRAINT [FK_Inventory_Transactions_Unit] FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Unit] ([UnitID]);

GO
ALTER TABLE [dbo].[Journal_Details]
    ADD CONSTRAINT [FK_Journal_Details_Account] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Global Preferences]
    ADD CONSTRAINT [FK_Personals_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[POS_CashRegister]
    ADD CONSTRAINT [FK_POS_CashRegister_Customer] FOREIGN KEY ([DefaultCustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]);

GO
ALTER TABLE [dbo].[Price_Level]
    ADD CONSTRAINT [FK_Price Levels_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[Price_Level]
    ADD CONSTRAINT [FK_Price Levels_Users1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[Users] ([UserID]);

GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [FK_Product_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Product] ([ProductID]);

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [FK_Register_Account_Card] FOREIGN KEY ([CardReceivedAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [FK_Register_Account_Cash] FOREIGN KEY ([CashAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [FK_Register_Account_PDC] FOREIGN KEY ([PDCIssuedAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [FK_Register_Account_PDCR] FOREIGN KEY ([PDCReceivedAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Transaction_Details]
    ADD CONSTRAINT [FK_Transaction_Details_Account] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Transaction_Details]
    ADD CONSTRAINT [FK_Transaction_Details_Bank] FOREIGN KEY ([BankID]) REFERENCES [dbo].[Bank] ([BankID]);

GO
ALTER TABLE [dbo].[Transaction_Details]
    ADD CONSTRAINT [FK_Transaction_Details_Cost_Center] FOREIGN KEY ([CostCenterID]) REFERENCES [dbo].[Cost_Center] ([CostCenterID]);

GO
ALTER TABLE [dbo].[User_Group_Detail]
    ADD CONSTRAINT [FK_User Group Assignments_User Groups] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[User_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[User_Group_Detail]
    ADD CONSTRAINT [FK_User Group Assignments_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID]) ON DELETE CASCADE ON UPDATE CASCADE;

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Account] FOREIGN KEY ([APAccountID]) REFERENCES [dbo].[Account] ([AccountID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[Area] ([AreaID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Buyer] FOREIGN KEY ([BuyerID]) REFERENCES [dbo].[Buyer] ([BuyerID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Payment_Method] FOREIGN KEY ([PaymentMethodID]) REFERENCES [dbo].[Payment_Method] ([PaymentMethodID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Payment_Term] FOREIGN KEY ([TermID]) REFERENCES [dbo].[Payment_Term] ([PaymentTermID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Shipping_Method] FOREIGN KEY ([ShippingMethodID]) REFERENCES [dbo].[Shipping_Method] ([ShippingMethodID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Vendor_Class] FOREIGN KEY ([VendorClassID]) REFERENCES [dbo].[Vendor_Class] ([ClassID]);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [FK_Vendor_Vendor_Group] FOREIGN KEY ([VendorGroupID]) REFERENCES [dbo].[Vendor_Group] ([GroupID]);

GO
ALTER TABLE [dbo].[Account_Analysis_Detail]
    ADD CONSTRAINT [DF_Account_Analysis_Details_IsOptional] DEFAULT ((1)) FOR [Type];

GO
ALTER TABLE [dbo].[PayrollItem]
    ADD CONSTRAINT [DF_Allowance_Inactive] DEFAULT ((1)) FOR [Inactive];

GO
ALTER TABLE [dbo].[PayrollItem]
    ADD CONSTRAINT [DF_Allowance_InLeaveSalary] DEFAULT ((1)) FOR [InLeaveSalary];

GO
ALTER TABLE [dbo].[Bank_Facility_Payment]
    ADD CONSTRAINT [DF_Bank_Facility_Payment_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[Bank_Facility_Transaction]
    ADD CONSTRAINT [DF_Bank_Facility_Transaction_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[Bank]
    ADD CONSTRAINT [DF_Banks_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Bill_Of_Lading]
    ADD CONSTRAINT [DF_BillOfLading_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Buyer]
    ADD CONSTRAINT [DF_Buyer_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Candidate]
    ADD CONSTRAINT [DF_Candidate_MaritalStatus] DEFAULT ((1)) FOR [MaritalStatus];

GO
ALTER TABLE [dbo].[Cheque_Received]
    ADD CONSTRAINT [DF_Cheque_Received_IsVoid] DEFAULT ((0)) FOR [IsVoid];

GO
ALTER TABLE [dbo].[Cheque_Received]
    ADD CONSTRAINT [DF_Cheque_Received_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Cheque_Register]
    ADD CONSTRAINT [DF_Cheque_Register_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[City]
    ADD CONSTRAINT [DF_City_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [DF_Company Information_FiscalFirstMonth] DEFAULT ((1)) FOR [FiscalFirstMonth];

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [DF_Company Information_IsTax] DEFAULT ((1)) FOR [IsTax];

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [DF_Company Information_UseLogo] DEFAULT ((0)) FOR [UseLogo];

GO
ALTER TABLE [dbo].[Company_Division]
    ADD CONSTRAINT [DF_Company_Division_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Company_Doc_Type]
    ADD CONSTRAINT [DF_Company_Doc_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Consign_In]
    ADD CONSTRAINT [DF_Consign_In_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Consign_Out]
    ADD CONSTRAINT [DF_Consign_Out_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[ConsignIn_Return]
    ADD CONSTRAINT [DF_ConsignIn_Return_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[ConsignIn_Return]
    ADD CONSTRAINT [DF_ConsignIn_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[ConsignIn_Settlement]
    ADD CONSTRAINT [DF_ConsignIn_Settlement_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[ConsignIn_Settlement]
    ADD CONSTRAINT [DF_ConsignIn_Settlement_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[ConsignOut_Return]
    ADD CONSTRAINT [DF_ConsignOut_Return_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[ConsignOut_Return]
    ADD CONSTRAINT [DF_ConsignOut_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[ConsignOut_Settlement]
    ADD CONSTRAINT [DF_ConsignOut_Settlement_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[ConsignOut_Settlement]
    ADD CONSTRAINT [DF_ConsignOut_Settlement_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Container_Tracking]
    ADD CONSTRAINT [DF_Container_Tracking_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Cost_Center]
    ADD CONSTRAINT [DF_Cost_Center_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Currency]
    ADD CONSTRAINT [DF_Currency_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [DF_Customer_BillToAddressID] DEFAULT (N'PRIMARY') FOR [BillToAddressID];

GO
ALTER TABLE [dbo].[Customer_Category]
    ADD CONSTRAINT [DF_Customer_Category_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Customer_Group]
    ADD CONSTRAINT [DF_Customer_Group_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [DF_Customer_ShipToAddressID] DEFAULT (N'PRIMARY') FOR [ShipToAddressID];

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [DF_Customer_StatementAddressID] DEFAULT (N'PRIMARY') FOR [StatementAddressID];

GO
ALTER TABLE [dbo].[Delivery_Note]
    ADD CONSTRAINT [DF_Delivery_Note_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Delivery_Return]
    ADD CONSTRAINT [DF_Delivery_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Department]
    ADD CONSTRAINT [DF_Department_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Destination]
    ADD CONSTRAINT [DF_Destination_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Discipline_Action_Type]
    ADD CONSTRAINT [DF_Discipline_Action_Type_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Division]
    ADD CONSTRAINT [DF_Division_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Driver]
    ADD CONSTRAINT [DF_Driver_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Employee_Activity_Type]
    ADD CONSTRAINT [DF_Employee_Activity_Type_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Employee_Doc_Type]
    ADD CONSTRAINT [DF_Employee_Docs_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Employee_Loan_Type]
    ADD CONSTRAINT [DF_Employee_Loan_Type_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Employee]
    ADD CONSTRAINT [DF_Employees_Married] DEFAULT ((1)) FOR [MaritalStatus];

GO
ALTER TABLE [dbo].[Entity_Category]
    ADD CONSTRAINT [DF_Entity_Category_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Export_PackingList]
    ADD CONSTRAINT [DF_Export_Packing_List_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Export_PickList]
    ADD CONSTRAINT [DF_Export_PickList_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[FixedAsset_Purchase_Order]
    ADD CONSTRAINT [DF_FixedAsset_Purchase_Order_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Freight_Charge]
    ADD CONSTRAINT [DF_Freight_Charge_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Garment_Rental_Return]
    ADD CONSTRAINT [DF_Garment_Rental_Return_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Garment_Rental_Return]
    ADD CONSTRAINT [DF_Garment_Rental_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Garment_Rental]
    ADD CONSTRAINT [DF_Garment_Rental_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[GRN_Return]
    ADD CONSTRAINT [DF_GRN_Return_IsCash] DEFAULT ((0)) FOR [IsCash];

GO
ALTER TABLE [dbo].[GRN_Return]
    ADD CONSTRAINT [DF_GRN_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Item_Transaction]
    ADD CONSTRAINT [DF_Item_Transaction_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Job_Invoice]
    ADD CONSTRAINT [DF_Job_Invoice_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Job_Invoice]
    ADD CONSTRAINT [DF_Job_Invoice_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Job_Material_Requisition]
    ADD CONSTRAINT [DF_Job_Material_Requisition_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Leave_Type]
    ADD CONSTRAINT [DF_Leave_Type_Days] DEFAULT ((0)) FOR [Days];

GO
ALTER TABLE [dbo].[Location]
    ADD CONSTRAINT [DF_Location_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Location]
    ADD CONSTRAINT [DF_Location_IsPOSLocation] DEFAULT ((0)) FOR [IsPOSLocation];

GO
ALTER TABLE [dbo].[Opening_Cheque_Received]
    ADD CONSTRAINT [DF_Opening_Cheque_Received_Entry_IsVoid] DEFAULT ((0)) FOR [IsVoid];

GO
ALTER TABLE [dbo].[Opening_Cheque_Received]
    ADD CONSTRAINT [DF_Opening_Cheque_Received_Entry_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Patient_Doc_Type]
    ADD CONSTRAINT [DF_Patient_Docs_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Patient]
    ADD CONSTRAINT [DF_Patient_MaritalStatus] DEFAULT ((1)) FOR [MaritalStatus];

GO
ALTER TABLE [dbo].[Payment_Method]
    ADD CONSTRAINT [DF_Payment_Method_MethodType] DEFAULT ((1)) FOR [MethodType];

GO
ALTER TABLE [dbo].[Payroll_Transaction]
    ADD CONSTRAINT [DF_Payroll_Transaction_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[Global Preferences]
    ADD CONSTRAINT [DF_Personals_IsCurrentUser] DEFAULT ((0)) FOR [IsCurrentUser];

GO
ALTER TABLE [dbo].[Purchase_Receipt]
    ADD CONSTRAINT [DF_PO_Receipt_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[PO_Shipment]
    ADD CONSTRAINT [DF_PO_Shipment_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Port]
    ADD CONSTRAINT [DF_Port_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[POS_HOLD]
    ADD CONSTRAINT [DF_POS_HOLD_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[POS_HOLD]
    ADD CONSTRAINT [DF_POS_HOLD_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Position]
    ADD CONSTRAINT [DF_Position_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Price_Level]
    ADD CONSTRAINT [DF_Price Levels_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Price_List]
    ADD CONSTRAINT [DF_Price_List_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Product_Category]
    ADD CONSTRAINT [DF_Product Categories_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Attribute]
    ADD CONSTRAINT [DF_Product_Attribute_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Brand]
    ADD CONSTRAINT [DF_Product_Brand_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Class]
    ADD CONSTRAINT [DF_Product_Class_IsInactive_1] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Group]
    ADD CONSTRAINT [DF_Product_Group_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [DF_Product_ItemType] DEFAULT ((1)) FOR [ItemType];

GO
ALTER TABLE [dbo].[Product_Manufacturer]
    ADD CONSTRAINT [DF_Product_Manufacturer_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Price_Bulk_Update]
    ADD CONSTRAINT [DF_Product_Price_Bulk_Update_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Product_Size]
    ADD CONSTRAINT [DF_Product_Size_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Specification]
    ADD CONSTRAINT [DF_Product_Specification_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product_Style]
    ADD CONSTRAINT [DF_Product_Style_IsInactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [DF_Products_IsDiscontinued] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [DF_Products_QuantityPerUnit] DEFAULT ((0)) FOR [QuantityPerUnit];

GO
ALTER TABLE [dbo].[Project_Expense_Allocation]
    ADD CONSTRAINT [DF_Project_Expense_Allocation_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[Project_SubContract_PI]
    ADD CONSTRAINT [DF_Project_SubContract_PI_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Project_Subcontract_PO]
    ADD CONSTRAINT [DF_Project_Subcontract_PO_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Property_Category]
    ADD CONSTRAINT [DF_Property_Category_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Property_Doc_Type]
    ADD CONSTRAINT [DF_Property_Docs_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Property_Facility]
    ADD CONSTRAINT [DF_Property_Facility_Type] DEFAULT ((1)) FOR [Type];

GO
ALTER TABLE [dbo].[Property_Tenant_Doc_Type]
    ADD CONSTRAINT [DF_Property_Tenant_Docs_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Property_Transaction]
    ADD CONSTRAINT [DF_Property_Transaction_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[Purchase_Cost_Entry]
    ADD CONSTRAINT [DF_Purchase_Cost_Entry_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Invoice_NonInv]
    ADD CONSTRAINT [DF_Purchase_Invoice_NonInv_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Invoice]
    ADD CONSTRAINT [DF_Purchase_Invoice_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Order_NonInv]
    ADD CONSTRAINT [DF_Purchase_Order_NonInv_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Order]
    ADD CONSTRAINT [DF_Purchase_Order_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Quote]
    ADD CONSTRAINT [DF_Purchase_Quote_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Purchase_Return]
    ADD CONSTRAINT [DF_Purchase_Return_IsCash] DEFAULT ((0)) FOR [IsCash];

GO
ALTER TABLE [dbo].[Purchase_Return]
    ADD CONSTRAINT [DF_Purchase_Return_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Purchase_Return]
    ADD CONSTRAINT [DF_Purchase_Return_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [DF_Register_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Returned_Cheque_Reason]
    ADD CONSTRAINT [DF_Returned_Cheque_Reason_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Sales_Enquiry]
    ADD CONSTRAINT [DF_Sales_Enquiry_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Invoice]
    ADD CONSTRAINT [DF_Sales_Invoice_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Sales_Invoice_NonInv]
    ADD CONSTRAINT [DF_Sales_Invoice_NonInv_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Sales_Invoice_NonInv]
    ADD CONSTRAINT [DF_Sales_Invoice_NonInv_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Invoice]
    ADD CONSTRAINT [DF_Sales_Invoice_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Order]
    ADD CONSTRAINT [DF_Sales_Order_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_POS]
    ADD CONSTRAINT [DF_Sales_POS_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Sales_POS]
    ADD CONSTRAINT [DF_Sales_POS_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Quote_History]
    ADD CONSTRAINT [DF_Sales_Quote_History_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Quote]
    ADD CONSTRAINT [DF_Sales_Quote_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Receipt]
    ADD CONSTRAINT [DF_Sales_Receipt_IsDelivered] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Sales_Receipt]
    ADD CONSTRAINT [DF_Sales_Receipt_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Sales_Return]
    ADD CONSTRAINT [DF_Sales_Return_IsCash] DEFAULT ((0)) FOR [IsCash];

GO
ALTER TABLE [dbo].[Sales_Return]
    ADD CONSTRAINT [DF_Sales_Return_IsDelivered_1] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[Sales_Return]
    ADD CONSTRAINT [DF_Sales_Return_Status_1] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Salesperson]
    ADD CONSTRAINT [DF_Salesperson_Inactive] DEFAULT ((0)) FOR [IsInactive];

GO
ALTER TABLE [dbo].[SalesProforma_Invoice]
    ADD CONSTRAINT [DF_SalesProforma_Invoice1_Detail_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Service_CallTrack]
    ADD CONSTRAINT [DF_ServiceCallTrack_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Shipping_Method]
    ADD CONSTRAINT [DF_Shippers_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Sponsor]
    ADD CONSTRAINT [DF_Sponsor_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[LPO_Receipt]
    ADD CONSTRAINT [DF_Table_1_IsDelivered] DEFAULT ((0)) FOR [IsReceived];

GO
ALTER TABLE [dbo].[GL_Transaction]
    ADD CONSTRAINT [DF_Transaction_TransactionStatus] DEFAULT ((1)) FOR [TransactionStatus];

GO
ALTER TABLE [dbo].[User_Group]
    ADD CONSTRAINT [DF_User Groups_IsInactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[User_Group]
    ADD CONSTRAINT [DF_User_Group_CanDeleteCard] DEFAULT ((0)) FOR [CanDeleteCard];

GO
ALTER TABLE [dbo].[User_Group]
    ADD CONSTRAINT [DF_User_Group_CanEditCard] DEFAULT ((0)) FOR [CanEditCard];

GO
ALTER TABLE [dbo].[Vehicle_Doc_Type]
    ADD CONSTRAINT [DF_Vehicle_Doc_Type_Remind] DEFAULT ((0)) FOR [Remind];

GO
ALTER TABLE [dbo].[Vehicle_Maintenance_Entry]
    ADD CONSTRAINT [DF_Vehicle_Maintenance_Entry_Status1] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Vendor_Group]
    ADD CONSTRAINT [DF_Vendor_Group_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Vendor_Price_List]
    ADD CONSTRAINT [DF_Vendor_Price_List_Status] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[W3PL_Delivery]
    ADD CONSTRAINT [DF_W3PL_Delivery_Status_1] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[W3PL_GRN]
    ADD CONSTRAINT [DF_W3PL_GRN_Status_1] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[W3PL_Invoice]
    ADD CONSTRAINT [DF_W3PL_Invoice_IsDelivered_1] DEFAULT ((0)) FOR [IsDelivered];

GO
ALTER TABLE [dbo].[W3PL_Invoice]
    ADD CONSTRAINT [DF_W3PL_Invoice_Status_1] DEFAULT ((1)) FOR [Status];

GO
ALTER TABLE [dbo].[Work_Location]
    ADD CONSTRAINT [DF_Work_Location_Inactive] DEFAULT ((0)) FOR [Inactive];

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [CK_Company Information] CHECK ([FiscalFirstMonth] >= (1)
                                                   AND [FiscalFirstMonth] <= (12));

GO
ALTER TABLE [dbo].[Account_Type]
    ADD CONSTRAINT [PK_Account Segments] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Account_Analysis_Detail]
    ADD CONSTRAINT [PK_Account_Analysis_Detail] PRIMARY KEY CLUSTERED ([AccountID] ASC, [AnalysisGroupID] ASC);

GO
ALTER TABLE [dbo].[Account_Group]
    ADD CONSTRAINT [PK_Account_Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Activity]
    ADD CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Activity Logs]
    ADD CONSTRAINT [PK_Activity Logs] PRIMARY KEY CLUSTERED ([ActivityLogID] ASC);

GO
ALTER TABLE [dbo].[PayrollItem]
    ADD CONSTRAINT [PK_Allowance] PRIMARY KEY CLUSTERED ([PayrollItemID] ASC);

GO
ALTER TABLE [dbo].[Analysis]
    ADD CONSTRAINT [PK_Analysis] PRIMARY KEY CLUSTERED ([AnalysisID] ASC);

GO
ALTER TABLE [dbo].[Analysis_Group]
    ADD CONSTRAINT [PK_Analysis_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Candidate_Benefit_Detail]
    ADD CONSTRAINT [PK_andidate_Benefit_Detail] PRIMARY KEY CLUSTERED ([CandidateID] ASC, [BenefitID] ASC);

GO
ALTER TABLE [dbo].[AP_Payment_Allocation]
    ADD CONSTRAINT [PK_AP_Payment_Allocation] PRIMARY KEY CLUSTERED ([AllocationID] ASC);

GO
ALTER TABLE [dbo].[APJournal]
    ADD CONSTRAINT [PK_APJournal] PRIMARY KEY CLUSTERED ([APID] ASC);

GO
ALTER TABLE [dbo].[Approval]
    ADD CONSTRAINT [PK_Approval] PRIMARY KEY CLUSTERED ([ApprovalID] ASC, [ApprovalType] ASC);

GO
ALTER TABLE [dbo].[Approval_Task]
    ADD CONSTRAINT [PK_Approval_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC);

GO
ALTER TABLE [dbo].[AR_Payment_Allocation]
    ADD CONSTRAINT [PK_AR_Payment_Allocation] PRIMARY KEY CLUSTERED ([AllocationID] ASC);

GO
ALTER TABLE [dbo].[Area]
    ADD CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaID] ASC);

GO
ALTER TABLE [dbo].[ARJournal]
    ADD CONSTRAINT [PK_ARJournal] PRIMARY KEY CLUSTERED ([ARID] ASC);

GO
ALTER TABLE [dbo].[Arrival_Report]
    ADD CONSTRAINT [PK_Arrival_Report] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Arrival_Report_Detail]
    ADD CONSTRAINT [PK_Arrival_Report_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Arrival_Report_Template]
    ADD CONSTRAINT [PK_Arrival_Report_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC);

GO
ALTER TABLE [dbo].[Assembly_Build]
    ADD CONSTRAINT [PK_Assembly_Build] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Assembly_Build_Detail]
    ADD CONSTRAINT [PK_Assembly_Build_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[FixedAsset]
    ADD CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED ([AssetID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Group]
    ADD CONSTRAINT [PK_Asset_Group] PRIMARY KEY CLUSTERED ([AssetGroupID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Location]
    ADD CONSTRAINT [PK_Asset_Location] PRIMARY KEY CLUSTERED ([AssetLocationID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Purchase]
    ADD CONSTRAINT [PK_Asset_Purchase] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Sale]
    ADD CONSTRAINT [PK_Asset_Sale] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Transfer]
    ADD CONSTRAINT [PK_Asset_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bank_Facility]
    ADD CONSTRAINT [PK_Bank_Facility] PRIMARY KEY CLUSTERED ([FacilityID] ASC);

GO
ALTER TABLE [dbo].[Bank_Facility_Group_Contacts]
    ADD CONSTRAINT [PK_Bank_Facility_Group_Contacts] PRIMARY KEY CLUSTERED ([GroupID] ASC, [ContactID] ASC);

GO
ALTER TABLE [dbo].[Bank_Facility_Group]
    ADD CONSTRAINT [PK_Bank_Facility_Offer] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Bank_Facility_Payment]
    ADD CONSTRAINT [PK_Bank_Facility_Payment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bank_Facility_Transaction]
    ADD CONSTRAINT [PK_Bank_Facility_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bank_Fee_Details]
    ADD CONSTRAINT [PK_Bank_Fee_Detail] PRIMARY KEY CLUSTERED ([GLTransactionSysDocID] ASC, [GLTransactionVoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Bank_Reconciliation]
    ADD CONSTRAINT [PK_Bank_Reconciliation] PRIMARY KEY CLUSTERED ([AccountID] ASC, [SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bank]
    ADD CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED ([BankID] ASC);

GO
ALTER TABLE [dbo].[Benefit]
    ADD CONSTRAINT [PK_Benefit] PRIMARY KEY CLUSTERED ([BenefitID] ASC);

GO
ALTER TABLE [dbo].[Bill_Discount]
    ADD CONSTRAINT [PK_Bill_Discount] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bill_Of_Lading]
    ADD CONSTRAINT [PK_BillOfLading] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Bin]
    ADD CONSTRAINT [PK_Bin] PRIMARY KEY CLUSTERED ([BinID] ASC);

GO
ALTER TABLE [dbo].[BOM]
    ADD CONSTRAINT [PK_BOM] PRIMARY KEY CLUSTERED ([BOMID] ASC);

GO
ALTER TABLE [dbo].[Budget]
    ADD CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Buyer]
    ADD CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED ([BuyerID] ASC);

GO
ALTER TABLE [dbo].[Candidate]
    ADD CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([CandidateID] ASC);

GO
ALTER TABLE [dbo].[Case_Client]
    ADD CONSTRAINT [PK_Case_Client] PRIMARY KEY CLUSTERED ([CaseClientID] ASC);

GO
ALTER TABLE [dbo].[Case_Client_Address]
    ADD CONSTRAINT [PK_Case_Client_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CaseClientID] ASC);

GO
ALTER TABLE [dbo].[Case_Client_Contact_Detail]
    ADD CONSTRAINT [PK_Case_Client_Contact_Detail] PRIMARY KEY CLUSTERED ([CaseClientID] ASC, [ContactID] ASC);

GO
ALTER TABLE [dbo].[Case_Party]
    ADD CONSTRAINT [PK_Case_Party] PRIMARY KEY CLUSTERED ([CasePartyID] ASC);

GO
ALTER TABLE [dbo].[Pivot_Report]
    ADD CONSTRAINT [PK_Chart] PRIMARY KEY CLUSTERED ([PivotID] ASC);

GO
ALTER TABLE [dbo].[Pivot_Group]
    ADD CONSTRAINT [PK_Chart_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[CheckList]
    ADD CONSTRAINT [PK_CheckList] PRIMARY KEY CLUSTERED ([CheckListID] ASC);

GO
ALTER TABLE [dbo].[CheckList_Task]
    ADD CONSTRAINT [PK_CheckList_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC);

GO
ALTER TABLE [dbo].[Cheque_Discount]
    ADD CONSTRAINT [PK_Cheque_Discount] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Cheque_Issued]
    ADD CONSTRAINT [PK_Cheque_Issued_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [PayeeType] ASC, [PayeeID] ASC);

GO
ALTER TABLE [dbo].[Cheque_Received]
    ADD CONSTRAINT [PK_Cheque_Received] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [BankID] ASC, [PayeeType] ASC, [PayeeID] ASC);

GO
ALTER TABLE [dbo].[Cheque_Register]
    ADD CONSTRAINT [PK_Cheque_Register] PRIMARY KEY CLUSTERED ([ChequebookID] ASC, [ChequeNumber] ASC);

GO
ALTER TABLE [dbo].[Cheque_Send]
    ADD CONSTRAINT [PK_Cheque_Send] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Chequebook]
    ADD CONSTRAINT [PK_Chequebook] PRIMARY KEY CLUSTERED ([ChequebookID] ASC);

GO
ALTER TABLE [dbo].[City]
    ADD CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC);

GO
ALTER TABLE [dbo].[CL_Token]
    ADD CONSTRAINT [PK_CL_Token] PRIMARY KEY CLUSTERED ([TokenID] ASC);

GO
ALTER TABLE [dbo].[CL_Voucher]
    ADD CONSTRAINT [PK_CL_Voucher] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ClientAsset]
    ADD CONSTRAINT [PK_ClientAsset] PRIMARY KEY CLUSTERED ([ClientAssetID] ASC);

GO
ALTER TABLE [dbo].[COGS_Update_Log]
    ADD CONSTRAINT [PK_COGS_Update_Log] PRIMARY KEY CLUSTERED ([LogID] ASC);

GO
ALTER TABLE [dbo].[Collateral]
    ADD CONSTRAINT [PK_Collateral] PRIMARY KEY CLUSTERED ([CollateralID] ASC);

GO
ALTER TABLE [dbo].[Generic_List]
    ADD CONSTRAINT [PK_Combo_List] PRIMARY KEY CLUSTERED ([GenericListType] ASC, [GenericListID] ASC);

GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [PK_Company Accounts] PRIMARY KEY CLUSTERED ([AccountID] ASC);

GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [PK_Company Information] PRIMARY KEY CLUSTERED ([SetupID] ASC);

GO
ALTER TABLE [dbo].[Company_Address]
    ADD CONSTRAINT [PK_Company_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC);

GO
ALTER TABLE [dbo].[Company_Division]
    ADD CONSTRAINT [PK_Company_Division] PRIMARY KEY CLUSTERED ([DivisionID] ASC);

GO
ALTER TABLE [dbo].[Company_Doc_Type]
    ADD CONSTRAINT [PK_Company_Doc_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Company_Document]
    ADD CONSTRAINT [PK_Company_Document] PRIMARY KEY CLUSTERED ([DocumentTypeID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Visa]
    ADD CONSTRAINT [PK_Company_Visa] PRIMARY KEY CLUSTERED ([VisaID] ASC);

GO
ALTER TABLE [dbo].[Consign_In]
    ADD CONSTRAINT [PK_Consign_In] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Consign_Out]
    ADD CONSTRAINT [PK_Consign_Out] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ConsignIn_Return]
    ADD CONSTRAINT [PK_ConsignIn_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ConsignIn_Sale]
    ADD CONSTRAINT [PK_ConsignIn_Sale] PRIMARY KEY CLUSTERED ([RowID] ASC);

GO
ALTER TABLE [dbo].[ConsignIn_Settlement]
    ADD CONSTRAINT [PK_ConsignIn_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ConsignOut_Return]
    ADD CONSTRAINT [PK_ConsignOut_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ConsignOut_Settlement]
    ADD CONSTRAINT [PK_ConsignOut_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Contact]
    ADD CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ContactID] ASC);

GO
ALTER TABLE [dbo].[Cost_Center]
    ADD CONSTRAINT [PK_Cost_Center] PRIMARY KEY CLUSTERED ([CostCenterID] ASC);

GO
ALTER TABLE [dbo].[Cost_Updation]
    ADD CONSTRAINT [PK_Cost_Updation] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
ALTER TABLE [dbo].[Country]
    ADD CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID] ASC);

GO
ALTER TABLE [dbo].[Credit_Limit_Review]
    ADD CONSTRAINT [PK_Credit_Limit_Review] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Currency]
    ADD CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([CurrencyID] ASC);

GO
ALTER TABLE [dbo].[Custom_Gadget]
    ADD CONSTRAINT [PK_Custom_Gadget] PRIMARY KEY CLUSTERED ([CustomGadgetID] ASC);

GO
ALTER TABLE [dbo].[Chart_Series]
    ADD CONSTRAINT [PK_Custom_Gadget_Series] PRIMARY KEY CLUSTERED ([CustomGadgetID] ASC, [SeriesID] ASC);

GO
ALTER TABLE [dbo].[Custom_Report]
    ADD CONSTRAINT [PK_Custom_Report] PRIMARY KEY CLUSTERED ([CustomReportID] ASC);

GO
ALTER TABLE [dbo].[Customer_Address]
    ADD CONSTRAINT [PK_Customer_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CustomerID] ASC);

GO
ALTER TABLE [dbo].[Customer_Category]
    ADD CONSTRAINT [PK_Customer_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Customer_Category_Detail]
    ADD CONSTRAINT [PK_Customer_Category_Detail] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [CategoryID] ASC);

GO
ALTER TABLE [dbo].[Customer_Contact_Detail]
    ADD CONSTRAINT [PK_Customer_Contact_Detail] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [ContactID] ASC);

GO
ALTER TABLE [dbo].[Customer_Class]
    ADD CONSTRAINT [PK_Customer_Group] PRIMARY KEY CLUSTERED ([ClassID] ASC);

GO
ALTER TABLE [dbo].[Customer_Group]
    ADD CONSTRAINT [PK_Customer_Group_1] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Customer_Insurance]
    ADD CONSTRAINT [PK_Customer_Insurance] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Customer_Vendor_Link]
    ADD CONSTRAINT [PK_Customer_Vendor_Link] PRIMARY KEY CLUSTERED ([PartyID] ASC);

GO
ALTER TABLE [dbo].[Customer]
    ADD CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC);

GO
ALTER TABLE [dbo].[Dashboard]
    ADD CONSTRAINT [PK_Dashboards] PRIMARY KEY CLUSTERED ([DashboardID] ASC, [UserID] ASC);

GO
ALTER TABLE [dbo].[Data_Patch]
    ADD CONSTRAINT [PK_Data_Patch] PRIMARY KEY CLUSTERED ([PatchID] ASC);

GO
ALTER TABLE [dbo].[Data_Sync]
    ADD CONSTRAINT [PK_Data_Sync] PRIMARY KEY CLUSTERED ([Code] ASC);

GO
ALTER TABLE [dbo].[Deduction]
    ADD CONSTRAINT [PK_Deduction] PRIMARY KEY CLUSTERED ([DeductionID] ASC);

GO
ALTER TABLE [dbo].[Delivery_Note]
    ADD CONSTRAINT [PK_Delivery_Note] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Delivery_Return]
    ADD CONSTRAINT [PK_Delivery_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Department]
    ADD CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentID] ASC);

GO
ALTER TABLE [dbo].[Destination]
    ADD CONSTRAINT [PK_Destination] PRIMARY KEY CLUSTERED ([DestinationID] ASC);

GO
ALTER TABLE [dbo].[Dimension]
    ADD CONSTRAINT [PK_Dimention] PRIMARY KEY CLUSTERED ([DimensionID] ASC);

GO
ALTER TABLE [dbo].[Dimension_Attribute]
    ADD CONSTRAINT [PK_Dimention_Attribute] PRIMARY KEY CLUSTERED ([AttributeID] ASC, [DimensionID] ASC);

GO
ALTER TABLE [dbo].[Discipline_Action_Type]
    ADD CONSTRAINT [PK_Discipline_Action_Type] PRIMARY KEY CLUSTERED ([ActionTypeID] ASC);

GO
ALTER TABLE [dbo].[Division]
    ADD CONSTRAINT [PK_Division] PRIMARY KEY CLUSTERED ([DivisionID] ASC);

GO
ALTER TABLE [dbo].[Doc_Version]
    ADD CONSTRAINT [PK_Doc_Version] PRIMARY KEY CLUSTERED ([VersionID] ASC);

GO
ALTER TABLE [dbo].[EA_Equipment]
    ADD CONSTRAINT [PK_EA_Equipment] PRIMARY KEY CLUSTERED ([EquipmentID] ASC);

GO
ALTER TABLE [dbo].[EA_Equipment_Transfer]
    ADD CONSTRAINT [PK_EA_Equipment_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[EA_Mobilization]
    ADD CONSTRAINT [PK_EA_Mobilization] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[EA_Requisition_Type]
    ADD CONSTRAINT [PK_EA_Requisition] PRIMARY KEY CLUSTERED ([RequisitionTypeID] ASC);

GO
ALTER TABLE [dbo].[EA_Requisition]
    ADD CONSTRAINT [PK_EA_Requsition] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[EA_Work_Order]
    ADD CONSTRAINT [PK_EA_Work_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[EA_WorkOrder_Inventory_Issue]
    ADD CONSTRAINT [PK_EA_WorkOrder_Inventory_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[EA_WorkOrder_Inventory_Return]
    ADD CONSTRAINT [PK_EA_WorkOrder_Inventory_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Email_Config]
    ADD CONSTRAINT [PK_Email_Config] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [EmailID] ASC);

GO
ALTER TABLE [dbo].[Email_Message]
    ADD CONSTRAINT [PK_Email_Message] PRIMARY KEY CLUSTERED ([MessageID] ASC);

GO
ALTER TABLE [dbo].[Employee_Grade]
    ADD CONSTRAINT [PK_Emp_Grade] PRIMARY KEY CLUSTERED ([GradeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Absconding]
    ADD CONSTRAINT [PK_Employee_Absconding] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Activity]
    ADD CONSTRAINT [PK_Employee_Activity] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Activity_Type]
    ADD CONSTRAINT [PK_Employee_Activity_Type] PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Address]
    ADD CONSTRAINT [PK_Employee_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [EmployeeID] ASC);

GO
ALTER TABLE [dbo].[Employee_PayrollItem_Detail]
    ADD CONSTRAINT [PK_Employee_Allowance_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [PayrollItemID] ASC);

GO
ALTER TABLE [dbo].[Employee_Appraisal]
    ADD CONSTRAINT [PK_Employee_Appraisal] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Benefit_Detail]
    ADD CONSTRAINT [PK_Employee_Benefit_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [BenefitID] ASC);

GO
ALTER TABLE [dbo].[Employee_Cancellation]
    ADD CONSTRAINT [PK_Employee_Cancellation] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Deduction_Detail]
    ADD CONSTRAINT [PK_Employee_Deduction_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DeductionID] ASC);

GO
ALTER TABLE [dbo].[Degree]
    ADD CONSTRAINT [PK_Employee_Degree] PRIMARY KEY CLUSTERED ([DegreeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Dependent]
    ADD CONSTRAINT [PK_Employee_Dependent] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DependentName] ASC);

GO
ALTER TABLE [dbo].[Employee_DisciplinaryAction]
    ADD CONSTRAINT [PK_Employee_DisciplinaryAction] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Document]
    ADD CONSTRAINT [PK_Employee_Docs] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Employee_Doc_Type]
    ADD CONSTRAINT [PK_Employee_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Education]
    ADD CONSTRAINT [PK_Employee_Education] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [Major] ASC);

GO
ALTER TABLE [dbo].[Employee_EOS]
    ADD CONSTRAINT [PK_Employee_EOS] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_EOSRule]
    ADD CONSTRAINT [PK_Employee_EOSRule] PRIMARY KEY CLUSTERED ([EOSRuleID] ASC);

GO
ALTER TABLE [dbo].[Employee_EOSSettlement]
    ADD CONSTRAINT [PK_Employee_EOSSettlement] PRIMARY KEY CLUSTERED ([EmployeeID] ASC);

GO
ALTER TABLE [dbo].[Employee_GeneralActivity]
    ADD CONSTRAINT [PK_Employee_GeneralActivity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Group]
    ADD CONSTRAINT [PK_Employee_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Employee_Journal]
    ADD CONSTRAINT [PK_Employee_Journal] PRIMARY KEY CLUSTERED ([EmpJournalID] ASC);

GO
ALTER TABLE [dbo].[Employee_Leave_Detail]
    ADD CONSTRAINT [PK_Employee_Leave_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [LeaveTypeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Leave_Encashment]
    ADD CONSTRAINT [PK_Employee_Leave_Encashment] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Leave_Payment]
    ADD CONSTRAINT [PK_Employee_Leave_Payment] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Leave_Request]
    ADD CONSTRAINT [PK_Employee_Leave_Request] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Loan]
    ADD CONSTRAINT [PK_Employee_Loan_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Loan_Payment]
    ADD CONSTRAINT [PK_Employee_Loan_Payment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Loan_Settlement]
    ADD CONSTRAINT [PK_Employee_Loan_Settlement] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Passport_Control]
    ADD CONSTRAINT [PK_Employee_Passport_Control] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Performance]
    ADD CONSTRAINT [PK_Employee_Performance] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Promotion]
    ADD CONSTRAINT [PK_Employee_Promotion] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Provision]
    ADD CONSTRAINT [PK_Employee_Provision] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Employee_Provision_Type]
    ADD CONSTRAINT [PK_Employee_Provision_Type] PRIMARY KEY CLUSTERED ([ProvisionTypeID] ASC);

GO
ALTER TABLE [dbo].[Employee_Rehire]
    ADD CONSTRAINT [PK_Employee_Rehire] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Resumption]
    ADD CONSTRAINT [PK_Employee_Resumption] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Skill_Detail]
    ADD CONSTRAINT [PK_Employee_Skill_Detail] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [SkillID] ASC);

GO
ALTER TABLE [dbo].[Employee_Termination]
    ADD CONSTRAINT [PK_Employee_Termination] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee_Transfer]
    ADD CONSTRAINT [PK_Employee_Transfer] PRIMARY KEY CLUSTERED ([ActivityID] ASC);

GO
ALTER TABLE [dbo].[Employee]
    ADD CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC);

GO
ALTER TABLE [dbo].[Entity_Address]
    ADD CONSTRAINT [PK_Entity_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [CustomerID] ASC);

GO
ALTER TABLE [dbo].[Entity_Category]
    ADD CONSTRAINT [PK_Entity_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Entity_Category_Detail]
    ADD CONSTRAINT [PK_Entity_Category_Detail_1] PRIMARY KEY CLUSTERED ([EntityID] ASC, [CategoryID] ASC, [EntityType] ASC);

GO
ALTER TABLE [dbo].[Entity_Comments]
    ADD CONSTRAINT [PK_Entity_Comments] PRIMARY KEY CLUSTERED ([CommentID] ASC);

GO
ALTER TABLE [dbo].[Entity_Contacts]
    ADD CONSTRAINT [PK_Entity_Contacts] PRIMARY KEY CLUSTERED ([EntityType] ASC, [EntityID] ASC, [ContactID] ASC);

GO
ALTER TABLE [dbo].[Entity_Flag]
    ADD CONSTRAINT [PK_Entity_Flag] PRIMARY KEY CLUSTERED ([FlagID] ASC);

GO
ALTER TABLE [dbo].[Entity_Flag_Detail]
    ADD CONSTRAINT [PK_Entity_Flag_Detail] PRIMARY KEY CLUSTERED ([EntityID] ASC, [FlagID] ASC, [EntityType] ASC);

GO
ALTER TABLE [dbo].[Entity_Notes]
    ADD CONSTRAINT [PK_Entity_Notes] PRIMARY KEY CLUSTERED ([CommentID] ASC);

GO
ALTER TABLE [dbo].[EntityDocs]
    ADD CONSTRAINT [PK_EntityDocs] PRIMARY KEY CLUSTERED ([DocID] ASC);

GO
ALTER TABLE [dbo].[EOY_Product]
    ADD CONSTRAINT [PK_EOY_Product] PRIMARY KEY CLUSTERED ([FiscalYearID] ASC, [ProductID] ASC, [LocationID] ASC);

GO
ALTER TABLE [dbo].[Equipment]
    ADD CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([EquipmentID] ASC);

GO
ALTER TABLE [dbo].[EA_Equipment_Category]
    ADD CONSTRAINT [PK_Equipment_Category] PRIMARY KEY CLUSTERED ([EquipmentCategoryID] ASC);

GO
ALTER TABLE [dbo].[EA_Equipment_Type]
    ADD CONSTRAINT [PK_Equipment_Type] PRIMARY KEY CLUSTERED ([EquipmentTypeID] ASC);

GO
ALTER TABLE [dbo].[Expense_Code]
    ADD CONSTRAINT [PK_Expense_Code] PRIMARY KEY CLUSTERED ([ExpenseID] ASC);

GO
ALTER TABLE [dbo].[Export_PackingList]
    ADD CONSTRAINT [PK_Export_Packing_List] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Export_PickList]
    ADD CONSTRAINT [PK_Export_PickList] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ExternalReport]
    ADD CONSTRAINT [PK_ExternalReport] PRIMARY KEY CLUSTERED ([ExternalReportID] ASC);

GO
ALTER TABLE [dbo].[ExternalReport_Category]
    ADD CONSTRAINT [PK_ExternalReport_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[FiscalYear]
    ADD CONSTRAINT [PK_Fiscal_Year] PRIMARY KEY CLUSTERED ([FiscalYearID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_BulkPurchase]
    ADD CONSTRAINT [PK_FixedAsset_BulkPurchase] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Dep_Schedule]
    ADD CONSTRAINT [PK_FixedAsset_Dep_Schedule] PRIMARY KEY CLUSTERED ([SheduleID] ASC);

GO
ALTER TABLE [dbo].[FixedAsset_Purchase_Order]
    ADD CONSTRAINT [PK_FixedAsset_Purchase_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Freight_Charge]
    ADD CONSTRAINT [PK_Freight_Charge] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Garment_Rental]
    ADD CONSTRAINT [PK_Garment_Rental] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Garment_Rental_Return]
    ADD CONSTRAINT [PK_Garment_Rental_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Gauge_Range]
    ADD CONSTRAINT [PK_Gauge_Range] PRIMARY KEY CLUSTERED ([RangeID] ASC, [CustomReportType] ASC, [CustomReportID] ASC);

GO
ALTER TABLE [dbo].[General_Payment_Detail]
    ADD CONSTRAINT [PK_General_Payment_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[GRN_Return]
    ADD CONSTRAINT [PK_GRN_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Holiday_Calendar]
    ADD CONSTRAINT [PK_Holiday_Calendar] PRIMARY KEY CLUSTERED ([CalendarID] ASC);

GO
ALTER TABLE [dbo].[Horse_Document]
    ADD CONSTRAINT [PK_Horse_Docs] PRIMARY KEY CLUSTERED ([HorseID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[INCO]
    ADD CONSTRAINT [PK_INCO] PRIMARY KEY CLUSTERED ([INCOID] ASC);

GO
ALTER TABLE [dbo].[Industry]
    ADD CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED ([IndustryID] ASC);

GO
ALTER TABLE [dbo].[Insurance_Provider]
    ADD CONSTRAINT [PK_Insurance_Provider] PRIMARY KEY CLUSTERED ([InsuranceProviderID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Transactions]
    ADD CONSTRAINT [PK_Inventory Costs] PRIMARY KEY CLUSTERED ([TransactionID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Adjustment]
    ADD CONSTRAINT [PK_Inventory_Adjustment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Adjustment_Type]
    ADD CONSTRAINT [PK_Inventory_Adjustment_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Damage]
    ADD CONSTRAINT [PK_Inventory_Damage] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Dismantle]
    ADD CONSTRAINT [PK_Inventory_Dismantle] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Dismantle_Detail]
    ADD CONSTRAINT [PK_Inventory_Dismantle_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Inventory_Transfer]
    ADD CONSTRAINT [PK_Inventory_Transfer] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Transfer_Detail]
    ADD CONSTRAINT [PK_Inventory_Transfer_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ProductID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Transfer_Type]
    ADD CONSTRAINT [PK_Inventory_Transfer_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Item_Transaction]
    ADD CONSTRAINT [PK_Item_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job]
    ADD CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED ([JobID] ASC);

GO
ALTER TABLE [dbo].[Job_BOM]
    ADD CONSTRAINT [PK_Job_BOM] PRIMARY KEY CLUSTERED ([JobBOMID] ASC);

GO
ALTER TABLE [dbo].[Job_Equipment]
    ADD CONSTRAINT [PK_Job_Equipment] PRIMARY KEY CLUSTERED ([JobID] ASC, [EquipmentID] ASC);

GO
ALTER TABLE [dbo].[Job_Estimation]
    ADD CONSTRAINT [PK_Job_Estimation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Expense_Issue]
    ADD CONSTRAINT [PK_Job_Expense_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Fee]
    ADD CONSTRAINT [PK_Job_Fee] PRIMARY KEY CLUSTERED ([FeeID] ASC);

GO
ALTER TABLE [dbo].[Job_Fee_Detail]
    ADD CONSTRAINT [PK_Job_Fee_Detail] PRIMARY KEY CLUSTERED ([JobID] ASC, [FeeID] ASC);

GO
ALTER TABLE [dbo].[Job_Inventory_Issue]
    ADD CONSTRAINT [PK_Job_Inventory_Issue] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Inventory_Return]
    ADD CONSTRAINT [PK_Job_Inventory_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Invoice]
    ADD CONSTRAINT [PK_Job_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Maintenance_Schedule]
    ADD CONSTRAINT [PK_Job_Maintenance_Schedule] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Maintenance_Service]
    ADD CONSTRAINT [PK_Job_Maintenance_Service] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Man_Hrs_Budgeting]
    ADD CONSTRAINT [PK_Job_Man_Hrs_Budgeting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Material_Estimate]
    ADD CONSTRAINT [PK_Job_Material_Estimate] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Material_Requisition]
    ADD CONSTRAINT [PK_Job_Material_Requisition] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Task]
    ADD CONSTRAINT [PK_Job_Task_1] PRIMARY KEY CLUSTERED ([TaskID] ASC);

GO
ALTER TABLE [dbo].[Job_Type]
    ADD CONSTRAINT [PK_Job_Type] PRIMARY KEY CLUSTERED ([JobTypeID] ASC);

GO
ALTER TABLE [dbo].[Job_Vehicle_Detail]
    ADD CONSTRAINT [PK_Job_Vehicle_Detail] PRIMARY KEY CLUSTERED ([JobID] ASC);

GO
ALTER TABLE [dbo].[Journal]
    ADD CONSTRAINT [PK_Journal] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Journal_Details]
    ADD CONSTRAINT [PK_Journal_Details] PRIMARY KEY CLUSTERED ([JournalDetailID] ASC);

GO
ALTER TABLE [dbo].[Lawyer]
    ADD CONSTRAINT [PK_Lawyer] PRIMARY KEY CLUSTERED ([LawyerID] ASC);

GO
ALTER TABLE [dbo].[Lead]
    ADD CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED ([LeadID] ASC);

GO
ALTER TABLE [dbo].[Lead_Address]
    ADD CONSTRAINT [PK_Lead_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [LeadID] ASC);

GO
ALTER TABLE [dbo].[Lead_Followup_Details]
    ADD CONSTRAINT [PK_Lead_Followup_Details] PRIMARY KEY CLUSTERED ([FollowupID] ASC);

GO
ALTER TABLE [dbo].[Lead_Source]
    ADD CONSTRAINT [PK_Lead_Source] PRIMARY KEY CLUSTERED ([LeadSourceID] ASC);

GO
ALTER TABLE [dbo].[Lead_Status]
    ADD CONSTRAINT [PK_Lead_Status] PRIMARY KEY CLUSTERED ([LeadStatusID] ASC);

GO
ALTER TABLE [dbo].[Leave_Type]
    ADD CONSTRAINT [PK_Leave_Type] PRIMARY KEY CLUSTERED ([LeaveTypeID] ASC);

GO
ALTER TABLE [dbo].[Legal_Action_Status]
    ADD CONSTRAINT [PK_Legal_Action_Status] PRIMARY KEY CLUSTERED ([LegalActionStatusID] ASC);

GO
ALTER TABLE [dbo].[Legal_Actions]
    ADD CONSTRAINT [PK_Legal_Actions] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Legal_Activity]
    ADD CONSTRAINT [PK_Legal_Activity] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[ListHiddenFields]
    ADD CONSTRAINT [PK_ListHiddenFields] PRIMARY KEY CLUSTERED ([FieldID] ASC, [CustomReportType] ASC, [CustomReportID] ASC);

GO
ALTER TABLE [dbo].[Loan_Entry]
    ADD CONSTRAINT [PK_Loan_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Location]
    ADD CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC);

GO
ALTER TABLE [dbo].[LPO_Receipt]
    ADD CONSTRAINT [PK_LPO_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Vehicle_Maintenance_Entry]
    ADD CONSTRAINT [PK_Maintenance_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Vehicle_Maintenance_Scheduler]
    ADD CONSTRAINT [PK_Maintenance_Scheduler] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Material_Reservation]
    ADD CONSTRAINT [PK_Material_Reservation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Matrix_Template]
    ADD CONSTRAINT [PK_Matrix_Template] PRIMARY KEY CLUSTERED ([TemplateID] ASC);

GO
ALTER TABLE [dbo].[Mfg_Production]
    ADD CONSTRAINT [PK_Mfg_Production] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Mfg_Work_Order]
    ADD CONSTRAINT [PK_Mfg_Work_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Mfg_Work_Order_Detail]
    ADD CONSTRAINT [PK_Mfg_Work_Order_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Modules]
    ADD CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED ([ModuleID] ASC);

GO
ALTER TABLE [dbo].[Nationality]
    ADD CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED ([NationalityID] ASC);

GO
ALTER TABLE [dbo].[Opening_Balance_Batch]
    ADD CONSTRAINT [PK_Opening_Balance_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC, [SysDocID] ASC);

GO
ALTER TABLE [dbo].[Opening_Balance_Leave]
    ADD CONSTRAINT [PK_Opening_Balance_Leave] PRIMARY KEY CLUSTERED ([BatchID] ASC, [SysDocID] ASC);

GO
ALTER TABLE [dbo].[Opening_Cheque_Issued]
    ADD CONSTRAINT [PK_Opening_Cheque_Issued] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [PayeeType] ASC, [PayeeID] ASC);

GO
ALTER TABLE [dbo].[Opening_Cheque_Received]
    ADD CONSTRAINT [PK_Opening_Cheque_Received_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [ChequeNumber] ASC, [BankID] ASC, [PayeeType] ASC, [PayeeID] ASC);

GO
ALTER TABLE [dbo].[Employee_OverTime]
    ADD CONSTRAINT [PK_OverTime] PRIMARY KEY CLUSTERED ([OverTimeID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Repacking]
    ADD CONSTRAINT [PK_Packed_Item] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Inventory_Repacking_Detail]
    ADD CONSTRAINT [PK_Packet_Item_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Patient]
    ADD CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([CustomerID] ASC);

GO
ALTER TABLE [dbo].[Patient_Document]
    ADD CONSTRAINT [PK_Patient_Docs] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Patient_Doc_Type]
    ADD CONSTRAINT [PK_Patient_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Payment_Method]
    ADD CONSTRAINT [PK_Payment Methods] PRIMARY KEY CLUSTERED ([PaymentMethodID] ASC);

GO
ALTER TABLE [dbo].[Payment_Allocation_Batch]
    ADD CONSTRAINT [PK_Payment_Allocation_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC);

GO
ALTER TABLE [dbo].[Payment_Request]
    ADD CONSTRAINT [PK_Payment_Request] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Payroll_Transaction]
    ADD CONSTRAINT [PK_Payroll_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Period_Lock]
    ADD CONSTRAINT [PK_Periods] PRIMARY KEY CLUSTERED ([PeriodID] ASC);

GO
ALTER TABLE [dbo].[Global Preferences]
    ADD CONSTRAINT [PK_Personals] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
ALTER TABLE [dbo].[Physical_Stock_Entry]
    ADD CONSTRAINT [PK_PhysicalStockEntry] PRIMARY KEY CLUSTERED ([DocNumber] ASC);

GO
ALTER TABLE [dbo].[Pivot_Report_Field]
    ADD CONSTRAINT [PK_Pivot_Report_Field] PRIMARY KEY CLUSTERED ([PivotID] ASC, [FieldName] ASC);

GO
ALTER TABLE [dbo].[Purchase_Receipt]
    ADD CONSTRAINT [PK_PO_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[PO_Shipment]
    ADD CONSTRAINT [PK_PO_Shipment] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Port]
    ADD CONSTRAINT [PK_Port] PRIMARY KEY CLUSTERED ([PortID] ASC);

GO
ALTER TABLE [dbo].[POS_Batch]
    ADD CONSTRAINT [PK_POS_Batch] PRIMARY KEY CLUSTERED ([BatchID] ASC);

GO
ALTER TABLE [dbo].[POS_Cashier]
    ADD CONSTRAINT [PK_POS_Cashier] PRIMARY KEY CLUSTERED ([CashierID] ASC);

GO
ALTER TABLE [dbo].[POS_CashRegister]
    ADD CONSTRAINT [PK_POS_CashRegister] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC);

GO
ALTER TABLE [dbo].[POS_CashRegister_Expense]
    ADD CONSTRAINT [PK_POS_CashRegister_ExpenseAccounts] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC, [DisplayName] ASC);

GO
ALTER TABLE [dbo].[POS_CashRegister_PaymentMethod]
    ADD CONSTRAINT [PK_POS_CashRegister_PaymentMethod] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC, [PaymentMethodID] ASC);

GO
ALTER TABLE [dbo].[POS_HOLD]
    ADD CONSTRAINT [PK_POS_HOLD] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[POS_Shift]
    ADD CONSTRAINT [PK_POS_Shift] PRIMARY KEY CLUSTERED ([ShiftID] ASC, [BatchID] ASC);

GO
ALTER TABLE [dbo].[Position]
    ADD CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED ([PositionID] ASC);

GO
ALTER TABLE [dbo].[Price_Level]
    ADD CONSTRAINT [PK_Price Levels] PRIMARY KEY CLUSTERED ([PriceLevelID] ASC);

GO
ALTER TABLE [dbo].[Print_Template_Map]
    ADD CONSTRAINT [PK_Print_Template_Map_1] PRIMARY KEY CLUSTERED ([MapID] ASC);

GO
ALTER TABLE [dbo].[Product_Category]
    ADD CONSTRAINT [PK_Product Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Product_Group]
    ADD CONSTRAINT [PK_Product Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[Product_Manufacturer]
    ADD CONSTRAINT [PK_Product Manufacturers] PRIMARY KEY CLUSTERED ([ManufacturerID] ASC);

GO
ALTER TABLE [dbo].[Product_Brand]
    ADD CONSTRAINT [PK_Product_Brand] PRIMARY KEY CLUSTERED ([BrandID] ASC);

GO
ALTER TABLE [dbo].[Product_Category_Detail]
    ADD CONSTRAINT [PK_Product_Category_Detail] PRIMARY KEY CLUSTERED ([ProductID] ASC, [ProductCategoryID] ASC);

GO
ALTER TABLE [dbo].[Product_Class]
    ADD CONSTRAINT [PK_Product_Class] PRIMARY KEY CLUSTERED ([ClassID] ASC);

GO
ALTER TABLE [dbo].[Product_Lot]
    ADD CONSTRAINT [PK_Product_Lot] PRIMARY KEY CLUSTERED ([LotNumber] ASC);

GO
ALTER TABLE [dbo].[Product_Lot_Sales]
    ADD CONSTRAINT [PK_Product_Lot_Sales] PRIMARY KEY CLUSTERED ([RecordID] ASC);

GO
ALTER TABLE [dbo].[Product_Lot_Sales_PickList]
    ADD CONSTRAINT [PK_Product_Lot_Sales_PickList] PRIMARY KEY CLUSTERED ([RecordID] ASC);

GO
ALTER TABLE [dbo].[Product_Make]
    ADD CONSTRAINT [PK_Product_Make] PRIMARY KEY CLUSTERED ([MakeID] ASC);

GO
ALTER TABLE [dbo].[Product_Model]
    ADD CONSTRAINT [PK_Product_Model] PRIMARY KEY CLUSTERED ([ModelID] ASC);

GO
ALTER TABLE [dbo].[Product_Parent]
    ADD CONSTRAINT [PK_Product_Parent] PRIMARY KEY CLUSTERED ([ProductParentID] ASC);

GO
ALTER TABLE [dbo].[Product_Price_Bulk_Update]
    ADD CONSTRAINT [PK_Product_Price_Bulk_Update] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Product_Size]
    ADD CONSTRAINT [PK_Product_Size] PRIMARY KEY CLUSTERED ([SizeID] ASC);

GO
ALTER TABLE [dbo].[Product_Specification]
    ADD CONSTRAINT [PK_Product_Specification] PRIMARY KEY CLUSTERED ([SpecificationID] ASC);

GO
ALTER TABLE [dbo].[Product_Style]
    ADD CONSTRAINT [PK_Product_Style] PRIMARY KEY CLUSTERED ([StyleID] ASC);

GO
ALTER TABLE [dbo].[Product_Type]
    ADD CONSTRAINT [PK_Product_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Product_Unit]
    ADD CONSTRAINT [PK_Product_Unit] PRIMARY KEY CLUSTERED ([ProductID] ASC, [UnitID] ASC);

GO
ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC);

GO
ALTER TABLE [dbo].[Job_Budget]
    ADD CONSTRAINT [PK_Project_Budget] PRIMARY KEY CLUSTERED ([JobID] ASC, [CostCategoryID] ASC);

GO
ALTER TABLE [dbo].[Project_Expense_Allocation]
    ADD CONSTRAINT [PK_Project_Expense_Allocation] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Job_Fee_Schedule]
    ADD CONSTRAINT [PK_Project_Fee_Schedule] PRIMARY KEY CLUSTERED ([ProjectID] ASC, [RowIndex] ASC);

GO
ALTER TABLE [dbo].[Project_SubContract_PI]
    ADD CONSTRAINT [PK_Project_SubContract_PI] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Project_Subcontract_PO]
    ADD CONSTRAINT [PK_Project_Subcontract_PO] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property]
    ADD CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([PropertyID] ASC);

GO
ALTER TABLE [dbo].[Property_Agent]
    ADD CONSTRAINT [PK_Property_Agent] PRIMARY KEY CLUSTERED ([PropertyAgentID] ASC);

GO
ALTER TABLE [dbo].[Property_Cancel]
    ADD CONSTRAINT [PK_Property_Cancel] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property_Category]
    ADD CONSTRAINT [PK_Property_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Property_Category_Detail]
    ADD CONSTRAINT [PK_Property_Category_Detail] PRIMARY KEY CLUSTERED ([PropertyID] ASC, [CategoryID] ASC);

GO
ALTER TABLE [dbo].[Property_Class]
    ADD CONSTRAINT [PK_Property_Class] PRIMARY KEY CLUSTERED ([PropertyClassID] ASC);

GO
ALTER TABLE [dbo].[Property_Document]
    ADD CONSTRAINT [PK_Property_Docs] PRIMARY KEY CLUSTERED ([PropertyID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Property_Doc_Type]
    ADD CONSTRAINT [PK_Property_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Property_Rent]
    ADD CONSTRAINT [PK_Property_Rent] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property_Service_Assign]
    ADD CONSTRAINT [PK_Property_Service_Assign] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property_Service_Request]
    ADD CONSTRAINT [PK_Property_Service_Request] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property_Tenant_Document]
    ADD CONSTRAINT [PK_Property_Tenant_Docs] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Property_Tenant_Doc_Type]
    ADD CONSTRAINT [PK_Property_Tenant_Docs_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Property_Transaction]
    ADD CONSTRAINT [PK_Property_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Property_Unit]
    ADD CONSTRAINT [PK_Property_Unit] PRIMARY KEY CLUSTERED ([PropertyUnitID] ASC);

GO
ALTER TABLE [dbo].[Property_VirtualUnit]
    ADD CONSTRAINT [PK_Property_VirtualUnit] PRIMARY KEY CLUSTERED ([PropertyVirtualUnitID] ASC);

GO
ALTER TABLE [dbo].[PropertyIncome_Code]
    ADD CONSTRAINT [PK_PropertyIncome_Code] PRIMARY KEY CLUSTERED ([IncomeID] ASC);

GO
ALTER TABLE [dbo].[Rental_Posting]
    ADD CONSTRAINT [PK_PropertyRental_Posting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Claim]
    ADD CONSTRAINT [PK_Purchase_Claim] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Cost_Entry]
    ADD CONSTRAINT [PK_Purchase_Cost_Entry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Invoice]
    ADD CONSTRAINT [PK_Purchase_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Invoice_NonInv]
    ADD CONSTRAINT [PK_Purchase_Invoice_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Order]
    ADD CONSTRAINT [PK_Purchase_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Order_NonInv]
    ADD CONSTRAINT [PK_Purchase_Order_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Payment_Invoice]
    ADD CONSTRAINT [PK_Purchase_Payment_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Prepayment_Invoice]
    ADD CONSTRAINT [PK_Purchase_Prepayment_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Quote]
    ADD CONSTRAINT [PK_Purchase_Quote] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Purchase_Return]
    ADD CONSTRAINT [PK_Purchase_Return] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Quality_Claim]
    ADD CONSTRAINT [PK_Quality_Claim] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Quality_Task]
    ADD CONSTRAINT [PK_Quality_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC);

GO
ALTER TABLE [dbo].[Rack]
    ADD CONSTRAINT [PK_Rack] PRIMARY KEY CLUSTERED ([RackID] ASC);

GO
ALTER TABLE [dbo].[Recurring_Transaction]
    ADD CONSTRAINT [PK_Recurring_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC);

GO
ALTER TABLE [dbo].[Register]
    ADD CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED ([RegisterID] ASC);

GO
ALTER TABLE [dbo].[Release_Type]
    ADD CONSTRAINT [PK_Release_Type] PRIMARY KEY CLUSTERED ([ReleaseTypeID] ASC);

GO
ALTER TABLE [dbo].[Religion]
    ADD CONSTRAINT [PK_Religion] PRIMARY KEY CLUSTERED ([ReligionID] ASC);

GO
ALTER TABLE [dbo].[Returned_Cheque_Reason]
    ADD CONSTRAINT [PK_Returned_Cheque_Reason] PRIMARY KEY CLUSTERED ([ReasonID] ASC);

GO
ALTER TABLE [dbo].[Route]
    ADD CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED ([RouteID] ASC);

GO
ALTER TABLE [dbo].[Route_Group]
    ADD CONSTRAINT [PK_Route_Group] PRIMARY KEY CLUSTERED ([RouteGroupID] ASC);

GO
ALTER TABLE [dbo].[Salary_Addition]
    ADD CONSTRAINT [PK_Salary_Addition] PRIMARY KEY CLUSTERED ([VoucherID] ASC);

GO
ALTER TABLE [dbo].[Salary_Deduction]
    ADD CONSTRAINT [PK_Salary_Deduction] PRIMARY KEY CLUSTERED ([VoucherID] ASC);

GO
ALTER TABLE [dbo].[SalarySheet]
    ADD CONSTRAINT [PK_Salary_Sheet_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Enquiry]
    ADD CONSTRAINT [PK_Sales_Enquiry] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Forecasting]
    ADD CONSTRAINT [PK_Sales_Forecasting] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Invoice]
    ADD CONSTRAINT [PK_Sales_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Invoice_NonInv]
    ADD CONSTRAINT [PK_Sales_Invoice_NonInv] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Order]
    ADD CONSTRAINT [PK_Sales_Order] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_POS]
    ADD CONSTRAINT [PK_Sales_POS] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Quote]
    ADD CONSTRAINT [PK_Sales_Quote] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Receipt]
    ADD CONSTRAINT [PK_Sales_Receipt] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Sales_Return]
    ADD CONSTRAINT [PK_Sales_Return_1] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Salesperson]
    ADD CONSTRAINT [PK_Salesperson] PRIMARY KEY CLUSTERED ([SalespersonID] ASC);

GO
ALTER TABLE [dbo].[Salesperson_Group]
    ADD CONSTRAINT [PK_Salesperson_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[SalesProforma_Invoice]
    ADD CONSTRAINT [PK_SalesProforma_Invoice1_Detail] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Security_Cheque]
    ADD CONSTRAINT [PK_Security_Cheque_1] PRIMARY KEY CLUSTERED ([VoucherID] ASC, [SysDocID] ASC);

GO
ALTER TABLE [dbo].[Service_Activity]
    ADD CONSTRAINT [PK_Service_Activity] PRIMARY KEY CLUSTERED ([ServiceActivityID] ASC);

GO
ALTER TABLE [dbo].[Service_Item]
    ADD CONSTRAINT [PK_Service_Item] PRIMARY KEY CLUSTERED ([ServiceItemID] ASC);

GO
ALTER TABLE [dbo].[Service_CallTrack]
    ADD CONSTRAINT [PK_ServiceCallTrack] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Shipping_Method]
    ADD CONSTRAINT [PK_Shipers] PRIMARY KEY CLUSTERED ([ShippingMethodID] ASC);

GO
ALTER TABLE [dbo].[Shortcut]
    ADD CONSTRAINT [PK_Shortcut] PRIMARY KEY CLUSTERED ([ShortcutType] ASC, [UserID] ASC, [ShortcutKey] ASC);

GO
ALTER TABLE [dbo].[Skill]
    ADD CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED ([SkillID] ASC);

GO
ALTER TABLE [dbo].[Smartlist]
    ADD CONSTRAINT [PK_Smartlist] PRIMARY KEY CLUSTERED ([SmartListID] ASC);

GO
ALTER TABLE [dbo].[Smartlist_Category]
    ADD CONSTRAINT [PK_Smartlist_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Sponsor]
    ADD CONSTRAINT [PK_Sponsor] PRIMARY KEY CLUSTERED ([SponsorID] ASC);

GO
ALTER TABLE [dbo].[Standing_Journal]
    ADD CONSTRAINT [PK_Standing_Journal_1] PRIMARY KEY CLUSTERED ([StandingJournalID] ASC);

GO
ALTER TABLE [dbo].[Surveyor]
    ADD CONSTRAINT [PK_Surveyor] PRIMARY KEY CLUSTERED ([SurveyorID] ASC);

GO
ALTER TABLE [dbo].[System_Document]
    ADD CONSTRAINT [PK_System_Document] PRIMARY KEY CLUSTERED ([SysDocID] ASC);

GO
ALTER TABLE [dbo].[Tabs_Security]
    ADD CONSTRAINT [PK_Tab_Security] PRIMARY KEY CLUSTERED ([TabID] ASC);

GO
ALTER TABLE [dbo].[Task_Steps]
    ADD CONSTRAINT [PK_Task_Steps] PRIMARY KEY CLUSTERED ([TaskStepID] ASC);

GO
ALTER TABLE [dbo].[Task_Transaction]
    ADD CONSTRAINT [PK_Task_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Task_Transaction_Status]
    ADD CONSTRAINT [PK_Task_Transaction_Status] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Task_Type]
    ADD CONSTRAINT [PK_Task_Type] PRIMARY KEY CLUSTERED ([TaskTypeID] ASC);

GO
ALTER TABLE [dbo].[Tax]
    ADD CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED ([TaxCode] ASC);

GO
ALTER TABLE [dbo].[Tax_Group]
    ADD CONSTRAINT [PK_Tax_Group] PRIMARY KEY CLUSTERED ([TaxGroupID] ASC);

GO
ALTER TABLE [dbo].[Tenancy_Contract]
    ADD CONSTRAINT [PK_Tenancy_Contract] PRIMARY KEY CLUSTERED ([ContractID] ASC);

GO
ALTER TABLE [dbo].[Payment_Term]
    ADD CONSTRAINT [PK_Terms] PRIMARY KEY CLUSTERED ([PaymentTermID] ASC);

GO
ALTER TABLE [dbo].[TR_Application]
    ADD CONSTRAINT [PK_TR_Application] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[Trade_License]
    ADD CONSTRAINT [PK_Trade_License] PRIMARY KEY CLUSTERED ([TradeLicenseID] ASC);

GO
ALTER TABLE [dbo].[GL_Transaction]
    ADD CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[UDF_Contact]
    ADD CONSTRAINT [PK_UDF_Contact] PRIMARY KEY CLUSTERED ([EntityID] ASC);

GO
ALTER TABLE [dbo].[UDF_Customer]
    ADD CONSTRAINT [PK_UDF_Customer] PRIMARY KEY CLUSTERED ([EntityID] ASC);

GO
ALTER TABLE [dbo].[UDF_Setup]
    ADD CONSTRAINT [PK_UDF_Setup] PRIMARY KEY CLUSTERED ([UDFTypeID] ASC, [FieldName] ASC);

GO
ALTER TABLE [dbo].[UDF_Vendor]
    ADD CONSTRAINT [PK_UDF_Vendor] PRIMARY KEY CLUSTERED ([EntityID] ASC);

GO
ALTER TABLE [dbo].[Unallocated_Lot_Items]
    ADD CONSTRAINT [PK_Unallocated_Lot_Items] PRIMARY KEY CLUSTERED ([ID] ASC);

GO
ALTER TABLE [dbo].[Unit]
    ADD CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED ([UnitID] ASC);

GO
ALTER TABLE [dbo].[User_Group_Detail]
    ADD CONSTRAINT [PK_User Group Assignments] PRIMARY KEY CLUSTERED ([GroupID] ASC, [UserID] ASC);

GO
ALTER TABLE [dbo].[User_Group]
    ADD CONSTRAINT [PK_User Groups] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[User_Location_Detail]
    ADD CONSTRAINT [PK_User_Location_Detail] PRIMARY KEY CLUSTERED ([LocationID] ASC, [UserID] ASC);

GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC);

GO
ALTER TABLE [dbo].[Vehicle]
    ADD CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([VehicleID] ASC);

GO
ALTER TABLE [dbo].[Vehicle_Doc_Type]
    ADD CONSTRAINT [PK_Vehicle_Doc_Type] PRIMARY KEY CLUSTERED ([TypeID] ASC);

GO
ALTER TABLE [dbo].[Vehicle_Document]
    ADD CONSTRAINT [PK_Vehicle_Document] PRIMARY KEY CLUSTERED ([VehicleID] ASC, [DocumentNumber] ASC);

GO
ALTER TABLE [dbo].[Vendor]
    ADD CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED ([VendorID] ASC);

GO
ALTER TABLE [dbo].[Vendor_Address]
    ADD CONSTRAINT [PK_Vendor_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC, [VendorID] ASC);

GO
ALTER TABLE [dbo].[Vendor_Class]
    ADD CONSTRAINT [PK_Vendor_Class] PRIMARY KEY CLUSTERED ([ClassID] ASC);

GO
ALTER TABLE [dbo].[Vendor_Contact_Detail]
    ADD CONSTRAINT [PK_Vendor_Contact_Detail] PRIMARY KEY CLUSTERED ([VendorID] ASC, [ContactID] ASC);

GO
ALTER TABLE [dbo].[Vendor_Group]
    ADD CONSTRAINT [PK_Vendor_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC);

GO
ALTER TABLE [dbo].[W3PL_Delivery]
    ADD CONSTRAINT [PK_W3PL_Delivery] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[W3PL_Invoice]
    ADD CONSTRAINT [PK_W3PL_Invoice] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[W3PL_GRN]
    ADD CONSTRAINT [PK_Warehouse3PL_GRN] PRIMARY KEY CLUSTERED ([SysDocID] ASC, [VoucherID] ASC);

GO
ALTER TABLE [dbo].[WebDashboard]
    ADD CONSTRAINT [PK_WebDashboards] PRIMARY KEY CLUSTERED ([WebDashboardID] ASC, [UserID] ASC);

GO
ALTER TABLE [dbo].[Work_Location]
    ADD CONSTRAINT [PK_Work_Location] PRIMARY KEY CLUSTERED ([WorkLocationID] ASC);

GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [IX_Company Accounts_1] UNIQUE NONCLUSTERED ([AccountName] ASC, [GroupID] ASC);

GO
ALTER TABLE [dbo].[Currency]
    ADD CONSTRAINT [IX_Currencies] UNIQUE NONCLUSTERED ([CurrencyName] ASC);

GO
ALTER TABLE [dbo].[Journal]
    ADD CONSTRAINT [UQ__Journal__25010387E4E8CD31] UNIQUE NONCLUSTERED ([JournalID] ASC);

GO
CREATE DEFAULT [dbo].[zero]
    AS 0;

GO
CREATE NONCLUSTERED INDEX [IX_Account_AccountID]
    ON [dbo].[Account]([AccountID] ASC);

GO
CREATE NONCLUSTERED INDEX [NCI_Account_Group_TypeID]
    ON [dbo].[Account_Group]([TypeID] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20161116-113329]
    ON [dbo].[Account_Group]([GroupID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_APDoc]
    ON [dbo].[AP_Payment_Allocation]([InvoiceSysDocID] ASC, [InvoiceVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_PaymentDoc]
    ON [dbo].[AP_Payment_Allocation]([PaymentSysDocID] ASC, [PaymentVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_JID_001]
    ON [dbo].[AP_Payment_Allocation]([APJournalID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_DocID_VoucherID]
    ON [dbo].[APJournal]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_ARDoc]
    ON [dbo].[AR_Payment_Allocation]([InvoiceSysDocID] ASC, [InvoiceVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_PaymentDoc]
    ON [dbo].[AR_Payment_Allocation]([PaymentSysDocID] ASC, [PaymentVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_AR_Payment_Allocation_CustomerID]
    ON [dbo].[AR_Payment_Allocation]([CustomerID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_CUST_VOUCHID]
    ON [dbo].[AR_Payment_Allocation]([CustomerID] ASC, [InvoiceVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_DocID_VoucherID]
    ON [dbo].[ARJournal]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_ARJournal_CustomerID]
    ON [dbo].[ARJournal]([CustomerID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_CUSTID]
    ON [dbo].[ARJournal]([CustomerID] ASC)
    INCLUDE([IsPDCRow], [Debit], [Credit], [IsVoid]);

GO
CREATE NONCLUSTERED INDEX [IX_DOCID]
    ON [dbo].[ARJournal]([SysDocID] ASC)
    INCLUDE([CustomerID], [ARDate]);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Banks]
    ON [dbo].[Bank]([BankName] ASC);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Countries]
    ON [dbo].[Country]([CountryName] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160111-151236]
    ON [dbo].[Customer]([CustomerClassID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_CustID]
    ON [dbo].[Delivery_Note]([CustomerID] ASC)
    INCLUDE([SysDocID], [VoucherID], [IsVoid], [IsInvoiced]);

GO
CREATE NONCLUSTERED INDEX [IX_DN_CustID]
    ON [dbo].[Delivery_Note]([CustomerID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DN_SysdocID]
    ON [dbo].[Delivery_Note]([SysDocID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DN_TrDt]
    ON [dbo].[Delivery_Note]([TransactionDate] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DN_VoucherID]
    ON [dbo].[Delivery_Note]([VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Delivery_Note_Detail_ProductID]
    ON [dbo].[Delivery_Note_Detail]([ProductID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DNote]
    ON [dbo].[Delivery_Note_Detail]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DNTSysDoc_VoucherID]
    ON [dbo].[Delivery_Return]([DNoteSysDocID] ASC, [DNoteVoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_DocID_VoucherID]
    ON [dbo].[Employee_Journal]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_SysDoc_VoucherID]
    ON [dbo].[Inventory_Transactions]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Inventory_Transactions_ProductID]
    ON [dbo].[Inventory_Transactions]([ProductID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RefTransactionID]
    ON [dbo].[Inventory_Transactions]([SysDocType] ASC, [RefTransactionID] ASC, [TransactionID] ASC)
    INCLUDE([AssetValue]);

GO
CREATE NONCLUSTERED INDEX [IX_Journal_1805111]
    ON [dbo].[Journal]([JournalDate] ASC)
    INCLUDE([JournalID], [IsVoid]);

GO
CREATE NONCLUSTERED INDEX [IX_Journal_1805112]
    ON [dbo].[Journal]([JournalID] ASC, [JournalDate] ASC)
    INCLUDE([IsVoid]);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-DOC_ID_VOUCHERID]
    ON [dbo].[Journal]([SysDocID] ASC, [VoucherID] ASC);


GO
ALTER INDEX [NonClusteredIndex-DOC_ID_VOUCHERID]
    ON [dbo].[Journal] DISABLE;

GO
CREATE NONCLUSTERED INDEX [IND_23434]
    ON [dbo].[Journal_Details]([SysDocID] ASC, [VoucherID] ASC, [IsARAP] ASC)
    INCLUDE([CheckDate]);

GO
CREATE NONCLUSTERED INDEX [IX_JD_1805111]
    ON [dbo].[Journal_Details]([AccountID] ASC)
    INCLUDE([Debit], [Credit]);

GO
CREATE NONCLUSTERED INDEX [IX_JD_1805112]
    ON [dbo].[Journal_Details]([AccountID] ASC)
    INCLUDE([JournalID], [Debit], [Credit]);

GO
CREATE NONCLUSTERED INDEX [IX_jd_JDdate]
    ON [dbo].[Journal_Details]([JDDate] ASC)
    INCLUDE([Debit], [Credit]);

GO
CREATE NONCLUSTERED INDEX [IX_JD_JournalID]
    ON [dbo].[Journal_Details]([JournalID] ASC)
    INCLUDE([Reference]);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Terms]
    ON [dbo].[Payment_Term]([TermName] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Product_productID]
    ON [dbo].[Product]([ProductID] ASC);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Product Categories]
    ON [dbo].[Product_Category]([CategoryName] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Product_Lot_ProductID]
    ON [dbo].[Product_Lot]([ItemCode] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20151225-011922]
    ON [dbo].[Product_Lot]([DocID] ASC, [ReceiptNumber] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_DocID_VoucherID_IND]
    ON [dbo].[Product_Lot_Issue_Detail]([SysDocID] ASC, [VoucherID] ASC, [RowIndex] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_ProductID]
    ON [dbo].[Product_Lot_Issue_Detail]([ProductID] ASC)
    INCLUDE([LotNumber]);

GO
CREATE NONCLUSTERED INDEX [IX_SourceLot]
    ON [dbo].[Product_Lot_Issue_Detail]([SourceLotNumber] ASC)
    INCLUDE([SysDocID]);

GO
CREATE NONCLUSTERED INDEX [IND_LotNo]
    ON [dbo].[Product_Lot_Sales]([LotNo] ASC)
    INCLUDE([RecordID]);

GO
CREATE NONCLUSTERED INDEX [IX_Product_Lot_Sale_DocID]
    ON [dbo].[Product_Lot_Sales]([DocID] ASC, [InvoiceNumber] ASC, [ItemCode] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Product_Lot_Sales_ProductID]
    ON [dbo].[Product_Lot_Sales]([ItemCode] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_SysDoc_VID_001]
    ON [dbo].[Purchase_Invoice_Detail]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IND_SysDoc_Voucher]
    ON [dbo].[Purchase_Order_Detail]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160111-150928]
    ON [dbo].[Sales_Invoice]([TransactionDate] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160111-151032]
    ON [dbo].[Sales_Invoice]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_OrderVID_DocID_Index]
    ON [dbo].[Sales_Invoice_Detail]([OrderVoucherID] ASC, [OrderSysDocID] ASC, [OrderRowIndex] ASC)
    INCLUDE([SysDocID], [VoucherID], [RowIndex]);

GO
CREATE NONCLUSTERED INDEX [IX_OrderVID_OrderSysID_Index]
    ON [dbo].[Sales_Invoice_Detail]([OrderVoucherID] ASC, [OrderSysDocID] ASC, [OrderRowIndex] ASC)
    INCLUDE([SysDocID], [VoucherID], [RowIndex]);

GO
CREATE NONCLUSTERED INDEX [IX_Sales_Invoice_Detail_ProductID]
    ON [dbo].[Sales_Invoice_Detail]([ProductID] ASC);

GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160111-151051]
    ON [dbo].[Sales_Invoice_Detail]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [SourceSys_Voucher_Ind]
    ON [dbo].[Sales_Return_Detail]([SourceSysDocID] ASC, [SourceVoucherID] ASC, [SourceRowIndex] ASC)
    INCLUDE([Quantity]);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Shippers]
    ON [dbo].[Shipping_Method]([ShippingMethodName] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_TR_Application]
    ON [dbo].[TR_Application]([VoucherID] ASC);

GO
CREATE NONCLUSTERED INDEX [SYSDOCID_VOUCHERID]
    ON [dbo].[Transaction_Details]([SysDocID] ASC, [VoucherID] ASC);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[Users]([UserName] ASC);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Does it use taxing system', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'IsTax';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determines if the PO is already fully shipped by PO Shipment.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FixedAsset_Purchase_Order', @level2type = N'COLUMN', @level2name = N'IsShipped';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determines if this row is from a GRN which is not yet costed', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inventory_Transactions', @level2type = N'COLUMN', @level2name = N'IsNonCostedGRN';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default account ID that the transaction is debit/credited', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Payment_Method', @level2type = N'COLUMN', @level2name = N'DefaultAccountID';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'check,tranfer,cash,creditcard?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Payment_Method', @level2type = N'COLUMN', @level2name = N'MethodType';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determines if the PO is already fully shipped by PO Shipment.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Purchase_Order', @level2type = N'COLUMN', @level2name = N'IsShipped';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determines if the PO is already fully shipped by PO Shipment.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Purchase_Order_NonInv', @level2type = N'COLUMN', @level2name = N'IsShipped';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'if the prepayment invoice is closed means cant pay anymore and it can be allocated to invoices.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Purchase_Prepayment_Invoice', @level2type = N'COLUMN', @level2name = N'Status';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Settings', @level2type = N'COLUMN', @level2name = N'SKey';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Settings', @level2type = N'COLUMN', @level2name = N'SName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Settings', @level2type = N'COLUMN', @level2name = N'SValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Temporary_Transaction', @level2type = N'COLUMN', @level2name = N'SKey';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Temporary_Transaction', @level2type = N'COLUMN', @level2name = N'SName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Temporary_Transaction', @level2type = N'COLUMN', @level2name = N'SValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "JD"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "J"
            Begin Extent = 
               Top = 6
               Left = 280
               Bottom = 135
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AC"
            Begin Extent = 
               Top = 6
               Left = 488
               Bottom = 135
               Right = 692
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AG"
            Begin Extent = 
               Top = 6
               Left = 730
               Bottom = 135
               Right = 900
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AN"
            Begin Extent = 
               Top = 6
               Left = 938
               Bottom = 135
               Right = 1108
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ax_Expense_Journals';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ax_Expense_Journals';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MyCTE_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PL"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 135
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 486
               Bottom = 135
               Right = 697
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LOC"
            Begin Extent = 
               Top = 6
               Left = 735
               Bottom = 135
               Right = 1008
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Ven"
            Begin Extent = 
               Top = 6
               Left = 1046
               Bottom = 135
               Right = 1252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Consign_Item_Lot';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Consign_Item_Lot';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Consign_Item_Lot';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ARJ"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CUS"
            Begin Extent = 
               Top = 6
               Left = 280
               Bottom = 135
               Right = 508
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Customer_Balance_Summary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Customer_Balance_Summary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PL"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LOC"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 135
               Right = 553
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Product_Aging';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Product_Aging';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PL2"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 135
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LOC"
            Begin Extent = 
               Top = 6
               Left = 486
               Bottom = 135
               Right = 759
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PL"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Product_Lot_Quantity';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Product_Lot_Quantity';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PD"
            Begin Extent = 
               Top = 6
               Left = 256
               Bottom = 136
               Right = 467
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Sale"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Sales_Detail';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Sales_Detail';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Sales_Summary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Axo_Sales_Summary';

GO
