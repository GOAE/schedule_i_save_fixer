namespace Schedule_I_Save_Fixer {
	partial class FormMain {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.lblSavePath = new Label();
			this.tlpTop = new TableLayoutPanel();
			this.lblSaveSlot = new Label();
			this.tbSavePath = new TextBox();
			this.btnSavePathBrowse = new Button();
			this.cbSaveSlot = new ComboBox();
			this.btnSaveSlotOpen = new Button();
			this.cbEnableFileDeletion = new CheckBox();
			this.folderBrowserDialog = new FolderBrowserDialog();
			this.dgvFixActions = new DataGridView();
			this.colFixActionsEnable = new DataGridViewCheckBoxColumn();
			this.colFixActionsFilePath = new DataGridViewTextBoxColumn();
			this.colFixActionsFixAction = new DataGridViewTextBoxColumn();
			this.colFixActionsFixResult = new DataGridViewTextBoxColumn();
			this.panelFixActionsBottom = new Panel();
			this.btnFix = new Button();
			this.tabControlMain = new TabControl();
			this.tabPageFixActions = new TabPage();
			this.tabPageCustomerRelationships = new TabPage();
			this.gbCustomers = new GroupBox();
			this.dgvCustomerRelationships = new DataGridView();
			this.colCustomerRelationshipsNpc = new DataGridViewTextBoxColumn();
			this.colCustomerRelationshipsRelationship = new DataGridViewComboBoxColumn();
			this.colCustomerRelationshipsApply = new DataGridViewButtonColumn();
			this.gbCustomerRegions = new GroupBox();
			this.btnCustomerRegionApply = new Button();
			this.cbCustomerRegionRelationship = new ComboBox();
			this.cbCustomerRegion = new ComboBox();
			this.label2 = new Label();
			this.tabPageSupplierUnlock = new TabPage();
			this.dgvSupplierUnlock = new DataGridView();
			this.colSupplierUnlockDealer = new DataGridViewTextBoxColumn();
			this.colSupplierUnlockStatus = new DataGridViewTextBoxColumn();
			this.colSupplierUnlockLock = new DataGridViewButtonColumn();
			this.colSupplierUnlockUnlock = new DataGridViewButtonColumn();
			this.tabPagePropertyOwnership = new TabPage();
			this.dgvPropertyOwnership = new DataGridView();
			this.colPropertyOwnershipProperty = new DataGridViewTextBoxColumn();
			this.colPropertyOwnershipStatus = new DataGridViewTextBoxColumn();
			this.colPropertyOwnershipOwn = new DataGridViewButtonColumn();
			this.colPropertyOwnershipDisown = new DataGridViewButtonColumn();
			this.tabPageBusinessOwnership = new TabPage();
			this.dgvBusinessOwnership = new DataGridView();
			this.colBusinessOwnershipBusiness = new DataGridViewTextBoxColumn();
			this.colBusinessOwnershipStatus = new DataGridViewTextBoxColumn();
			this.colBusinessOwnershipOwn = new DataGridViewButtonColumn();
			this.colBusinessOwnershipDisown = new DataGridViewButtonColumn();
			this.tabPageVehicleOwnership = new TabPage();
			this.toolTip = new ToolTip(this.components);
			this.tlpTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvFixActions).BeginInit();
			this.panelFixActionsBottom.SuspendLayout();
			this.tabControlMain.SuspendLayout();
			this.tabPageFixActions.SuspendLayout();
			this.tabPageCustomerRelationships.SuspendLayout();
			this.gbCustomers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvCustomerRelationships).BeginInit();
			this.gbCustomerRegions.SuspendLayout();
			this.tabPageSupplierUnlock.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvSupplierUnlock).BeginInit();
			this.tabPagePropertyOwnership.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvPropertyOwnership).BeginInit();
			this.tabPageBusinessOwnership.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvBusinessOwnership).BeginInit();
			this.SuspendLayout();
			// 
			// lblSavePath
			// 
			this.lblSavePath.AutoSize = true;
			this.lblSavePath.Dock = DockStyle.Right;
			this.lblSavePath.Location = new Point(33, 0);
			this.lblSavePath.Name = "lblSavePath";
			this.lblSavePath.Size = new Size(92, 36);
			this.lblSavePath.TabIndex = 0;
			this.lblSavePath.Text = "Save Path:";
			this.lblSavePath.TextAlign = ContentAlignment.MiddleRight;
			// 
			// tlpTop
			// 
			this.tlpTop.ColumnCount = 3;
			this.tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
			this.tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
			this.tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
			this.tlpTop.Controls.Add(this.lblSavePath, 0, 0);
			this.tlpTop.Controls.Add(this.lblSaveSlot, 0, 1);
			this.tlpTop.Controls.Add(this.tbSavePath, 1, 0);
			this.tlpTop.Controls.Add(this.btnSavePathBrowse, 2, 0);
			this.tlpTop.Controls.Add(this.cbSaveSlot, 1, 1);
			this.tlpTop.Controls.Add(this.btnSaveSlotOpen, 2, 1);
			this.tlpTop.Location = new Point(12, 12);
			this.tlpTop.Name = "tlpTop";
			this.tlpTop.RowCount = 2;
			this.tlpTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.tlpTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.tlpTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			this.tlpTop.Size = new Size(754, 72);
			this.tlpTop.TabIndex = 0;
			// 
			// lblSaveSlot
			// 
			this.lblSaveSlot.AutoSize = true;
			this.lblSaveSlot.Dock = DockStyle.Right;
			this.lblSaveSlot.Location = new Point(36, 36);
			this.lblSaveSlot.Name = "lblSaveSlot";
			this.lblSaveSlot.Size = new Size(89, 36);
			this.lblSaveSlot.TabIndex = 3;
			this.lblSaveSlot.Text = "Save Slot:";
			this.lblSaveSlot.TextAlign = ContentAlignment.MiddleRight;
			// 
			// tbSavePath
			// 
			this.tbSavePath.Dock = DockStyle.Fill;
			this.tbSavePath.Location = new Point(131, 3);
			this.tbSavePath.Name = "tbSavePath";
			this.tbSavePath.Size = new Size(474, 31);
			this.tbSavePath.TabIndex = 1;
			this.tbSavePath.Text = "%USERPROFILE%\\AppData\\LocalLow\\TVGS\\Schedule I\\Saves\\";
			this.toolTip.SetToolTip(this.tbSavePath, "The path to the \"Saves\" directory for Schedule I.\r\nThis should usually be: %USERPROFILE%\\AppData\\LocalLow\\TVGS\\Schedule I\\Saves\\");
			this.tbSavePath.TextChanged += this.tbSavePath_TextChanged;
			// 
			// btnSavePathBrowse
			// 
			this.btnSavePathBrowse.Dock = DockStyle.Fill;
			this.btnSavePathBrowse.Location = new Point(611, 3);
			this.btnSavePathBrowse.Name = "btnSavePathBrowse";
			this.btnSavePathBrowse.Size = new Size(140, 30);
			this.btnSavePathBrowse.TabIndex = 2;
			this.btnSavePathBrowse.Text = "Choose";
			this.toolTip.SetToolTip(this.btnSavePathBrowse, "Browse for the Schedule I \"Saves\" directory.");
			this.btnSavePathBrowse.UseVisualStyleBackColor = true;
			this.btnSavePathBrowse.Click += this.btnSavePathBrowse_Click;
			// 
			// cbSaveSlot
			// 
			this.cbSaveSlot.Dock = DockStyle.Fill;
			this.cbSaveSlot.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbSaveSlot.FormattingEnabled = true;
			this.cbSaveSlot.Location = new Point(131, 39);
			this.cbSaveSlot.Name = "cbSaveSlot";
			this.cbSaveSlot.Size = new Size(474, 33);
			this.cbSaveSlot.TabIndex = 4;
			this.toolTip.SetToolTip(this.cbSaveSlot, "The save slot to try to recover.\r\nIf this is empty, the save path is probably not correct.");
			this.cbSaveSlot.SelectedIndexChanged += this.cbSaveSlot_SelectedIndexChanged;
			// 
			// btnSaveSlotOpen
			// 
			this.btnSaveSlotOpen.Dock = DockStyle.Fill;
			this.btnSaveSlotOpen.Enabled = false;
			this.btnSaveSlotOpen.Location = new Point(611, 39);
			this.btnSaveSlotOpen.Name = "btnSaveSlotOpen";
			this.btnSaveSlotOpen.Size = new Size(140, 30);
			this.btnSaveSlotOpen.TabIndex = 6;
			this.btnSaveSlotOpen.Text = "Open";
			this.toolTip.SetToolTip(this.btnSaveSlotOpen, "Open the directory that contains the save slot in explorer.");
			this.btnSaveSlotOpen.UseVisualStyleBackColor = true;
			this.btnSaveSlotOpen.Click += this.btnSaveSlotOpen_Click;
			// 
			// cbEnableFileDeletion
			// 
			this.cbEnableFileDeletion.AutoSize = true;
			this.cbEnableFileDeletion.Dock = DockStyle.Left;
			this.cbEnableFileDeletion.Location = new Point(0, 0);
			this.cbEnableFileDeletion.Name = "cbEnableFileDeletion";
			this.cbEnableFileDeletion.Size = new Size(294, 48);
			this.cbEnableFileDeletion.TabIndex = 0;
			this.cbEnableFileDeletion.Text = "Enable File Deletion When Fixing";
			this.toolTip.SetToolTip(this.cbEnableFileDeletion, resources.GetString("cbEnableFileDeletion.ToolTip"));
			this.cbEnableFileDeletion.UseVisualStyleBackColor = true;
			this.cbEnableFileDeletion.CheckedChanged += this.cbEnableFileDeletion_CheckedChanged;
			// 
			// dgvFixActions
			// 
			this.dgvFixActions.AllowUserToAddRows = false;
			this.dgvFixActions.AllowUserToDeleteRows = false;
			this.dgvFixActions.AllowUserToResizeColumns = false;
			this.dgvFixActions.AllowUserToResizeRows = false;
			this.dgvFixActions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvFixActions.Columns.AddRange(new DataGridViewColumn[] { this.colFixActionsEnable, this.colFixActionsFilePath, this.colFixActionsFixAction, this.colFixActionsFixResult });
			this.dgvFixActions.Dock = DockStyle.Fill;
			this.dgvFixActions.Location = new Point(3, 3);
			this.dgvFixActions.Name = "dgvFixActions";
			this.dgvFixActions.ReadOnly = true;
			this.dgvFixActions.RowHeadersVisible = false;
			this.dgvFixActions.RowHeadersWidth = 62;
			this.dgvFixActions.Size = new Size(740, 520);
			this.dgvFixActions.TabIndex = 0;
			// 
			// colFixActionsEnable
			// 
			this.colFixActionsEnable.HeaderText = "En";
			this.colFixActionsEnable.MinimumWidth = 8;
			this.colFixActionsEnable.Name = "colFixActionsEnable";
			this.colFixActionsEnable.ReadOnly = true;
			this.colFixActionsEnable.Width = 40;
			// 
			// colFixActionsFilePath
			// 
			this.colFixActionsFilePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colFixActionsFilePath.HeaderText = "File Path";
			this.colFixActionsFilePath.MinimumWidth = 8;
			this.colFixActionsFilePath.Name = "colFixActionsFilePath";
			this.colFixActionsFilePath.ReadOnly = true;
			// 
			// colFixActionsFixAction
			// 
			this.colFixActionsFixAction.HeaderText = "Fix Action";
			this.colFixActionsFixAction.MinimumWidth = 8;
			this.colFixActionsFixAction.Name = "colFixActionsFixAction";
			this.colFixActionsFixAction.ReadOnly = true;
			this.colFixActionsFixAction.Width = 192;
			// 
			// colFixActionsFixResult
			// 
			this.colFixActionsFixResult.HeaderText = "Fix Result";
			this.colFixActionsFixResult.MinimumWidth = 8;
			this.colFixActionsFixResult.Name = "colFixActionsFixResult";
			this.colFixActionsFixResult.ReadOnly = true;
			this.colFixActionsFixResult.Width = 192;
			// 
			// panelFixActionsBottom
			// 
			this.panelFixActionsBottom.Controls.Add(this.btnFix);
			this.panelFixActionsBottom.Controls.Add(this.cbEnableFileDeletion);
			this.panelFixActionsBottom.Dock = DockStyle.Bottom;
			this.panelFixActionsBottom.Location = new Point(3, 523);
			this.panelFixActionsBottom.Name = "panelFixActionsBottom";
			this.panelFixActionsBottom.Size = new Size(740, 48);
			this.panelFixActionsBottom.TabIndex = 1;
			// 
			// btnFix
			// 
			this.btnFix.Dock = DockStyle.Right;
			this.btnFix.Enabled = false;
			this.btnFix.Location = new Point(541, 0);
			this.btnFix.Name = "btnFix";
			this.btnFix.Size = new Size(199, 48);
			this.btnFix.TabIndex = 1;
			this.btnFix.Text = "Fix Save";
			this.toolTip.SetToolTip(this.btnFix, "Try to automatically fix the save slot.");
			this.btnFix.UseVisualStyleBackColor = true;
			this.btnFix.Click += this.btnFix_Click;
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tabPageFixActions);
			this.tabControlMain.Controls.Add(this.tabPageCustomerRelationships);
			this.tabControlMain.Controls.Add(this.tabPageSupplierUnlock);
			this.tabControlMain.Controls.Add(this.tabPagePropertyOwnership);
			this.tabControlMain.Controls.Add(this.tabPageBusinessOwnership);
			this.tabControlMain.Controls.Add(this.tabPageVehicleOwnership);
			this.tabControlMain.Enabled = false;
			this.tabControlMain.Location = new Point(12, 90);
			this.tabControlMain.Multiline = true;
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new Size(754, 642);
			this.tabControlMain.TabIndex = 1;
			// 
			// tabPageFixActions
			// 
			this.tabPageFixActions.Controls.Add(this.dgvFixActions);
			this.tabPageFixActions.Controls.Add(this.panelFixActionsBottom);
			this.tabPageFixActions.Location = new Point(4, 64);
			this.tabPageFixActions.Name = "tabPageFixActions";
			this.tabPageFixActions.Padding = new Padding(3);
			this.tabPageFixActions.Size = new Size(746, 574);
			this.tabPageFixActions.TabIndex = 0;
			this.tabPageFixActions.Text = "Auto Fix";
			this.tabPageFixActions.UseVisualStyleBackColor = true;
			// 
			// tabPageCustomerRelationships
			// 
			this.tabPageCustomerRelationships.Controls.Add(this.gbCustomers);
			this.tabPageCustomerRelationships.Controls.Add(this.gbCustomerRegions);
			this.tabPageCustomerRelationships.Location = new Point(4, 64);
			this.tabPageCustomerRelationships.Name = "tabPageCustomerRelationships";
			this.tabPageCustomerRelationships.Size = new Size(746, 574);
			this.tabPageCustomerRelationships.TabIndex = 1;
			this.tabPageCustomerRelationships.Text = "Customers Relationships";
			this.tabPageCustomerRelationships.UseVisualStyleBackColor = true;
			// 
			// gbCustomers
			// 
			this.gbCustomers.Controls.Add(this.dgvCustomerRelationships);
			this.gbCustomers.Location = new Point(3, 78);
			this.gbCustomers.Name = "gbCustomers";
			this.gbCustomers.Size = new Size(737, 487);
			this.gbCustomers.TabIndex = 1;
			this.gbCustomers.TabStop = false;
			this.gbCustomers.Text = "Individual Customers:";
			// 
			// dgvCustomerRelationships
			// 
			this.dgvCustomerRelationships.AllowUserToAddRows = false;
			this.dgvCustomerRelationships.AllowUserToDeleteRows = false;
			this.dgvCustomerRelationships.AllowUserToResizeColumns = false;
			this.dgvCustomerRelationships.AllowUserToResizeRows = false;
			this.dgvCustomerRelationships.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCustomerRelationships.Columns.AddRange(new DataGridViewColumn[] { this.colCustomerRelationshipsNpc, this.colCustomerRelationshipsRelationship, this.colCustomerRelationshipsApply });
			this.dgvCustomerRelationships.Dock = DockStyle.Fill;
			this.dgvCustomerRelationships.Location = new Point(3, 27);
			this.dgvCustomerRelationships.Name = "dgvCustomerRelationships";
			this.dgvCustomerRelationships.RowHeadersVisible = false;
			this.dgvCustomerRelationships.RowHeadersWidth = 62;
			this.dgvCustomerRelationships.Size = new Size(731, 457);
			this.dgvCustomerRelationships.TabIndex = 0;
			this.dgvCustomerRelationships.CellClick += this.dgvCustomerRelationships_CellClick;
			// 
			// colCustomerRelationshipsNpc
			// 
			this.colCustomerRelationshipsNpc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colCustomerRelationshipsNpc.HeaderText = "NPC";
			this.colCustomerRelationshipsNpc.MinimumWidth = 8;
			this.colCustomerRelationshipsNpc.Name = "colCustomerRelationshipsNpc";
			this.colCustomerRelationshipsNpc.ReadOnly = true;
			// 
			// colCustomerRelationshipsRelationship
			// 
			this.colCustomerRelationshipsRelationship.HeaderText = "Relationship";
			this.colCustomerRelationshipsRelationship.MinimumWidth = 8;
			this.colCustomerRelationshipsRelationship.Name = "colCustomerRelationshipsRelationship";
			this.colCustomerRelationshipsRelationship.Resizable = DataGridViewTriState.True;
			this.colCustomerRelationshipsRelationship.SortMode = DataGridViewColumnSortMode.Automatic;
			this.colCustomerRelationshipsRelationship.Width = 128;
			// 
			// colCustomerRelationshipsApply
			// 
			this.colCustomerRelationshipsApply.HeaderText = "Apply";
			this.colCustomerRelationshipsApply.MinimumWidth = 8;
			this.colCustomerRelationshipsApply.Name = "colCustomerRelationshipsApply";
			this.colCustomerRelationshipsApply.Width = 96;
			// 
			// gbCustomerRegions
			// 
			this.gbCustomerRegions.Controls.Add(this.btnCustomerRegionApply);
			this.gbCustomerRegions.Controls.Add(this.cbCustomerRegionRelationship);
			this.gbCustomerRegions.Controls.Add(this.cbCustomerRegion);
			this.gbCustomerRegions.Controls.Add(this.label2);
			this.gbCustomerRegions.Location = new Point(3, 3);
			this.gbCustomerRegions.Name = "gbCustomerRegions";
			this.gbCustomerRegions.Size = new Size(737, 69);
			this.gbCustomerRegions.TabIndex = 0;
			this.gbCustomerRegions.TabStop = false;
			this.gbCustomerRegions.Text = "Regions:";
			// 
			// btnCustomerRegionApply
			// 
			this.btnCustomerRegionApply.Location = new Point(639, 30);
			this.btnCustomerRegionApply.Name = "btnCustomerRegionApply";
			this.btnCustomerRegionApply.Size = new Size(92, 33);
			this.btnCustomerRegionApply.TabIndex = 3;
			this.btnCustomerRegionApply.Text = "Apply";
			this.toolTip.SetToolTip(this.btnCustomerRegionApply, "Apply the chosen relationship level to the region.");
			this.btnCustomerRegionApply.UseVisualStyleBackColor = true;
			this.btnCustomerRegionApply.Click += this.btnCustomerRegionApply_Click;
			// 
			// cbCustomerRegionRelationship
			// 
			this.cbCustomerRegionRelationship.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbCustomerRegionRelationship.FormattingEnabled = true;
			this.cbCustomerRegionRelationship.Items.AddRange(new object[] { "Locked", "1/5", "2/5", "3/5", "4/5", "5/5 (Max)" });
			this.cbCustomerRegionRelationship.Location = new Point(505, 30);
			this.cbCustomerRegionRelationship.Name = "cbCustomerRegionRelationship";
			this.cbCustomerRegionRelationship.Size = new Size(128, 33);
			this.cbCustomerRegionRelationship.TabIndex = 2;
			this.toolTip.SetToolTip(this.cbCustomerRegionRelationship, "The new relationship level for the region.");
			// 
			// cbCustomerRegion
			// 
			this.cbCustomerRegion.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbCustomerRegion.FormattingEnabled = true;
			this.cbCustomerRegion.Location = new Point(83, 30);
			this.cbCustomerRegion.Name = "cbCustomerRegion";
			this.cbCustomerRegion.Size = new Size(416, 33);
			this.cbCustomerRegion.TabIndex = 1;
			this.toolTip.SetToolTip(this.cbCustomerRegion, "The region to adjust the customer relations of.");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 33);
			this.label2.Name = "label2";
			this.label2.Size = new Size(71, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "Region:";
			// 
			// tabPageSupplierUnlock
			// 
			this.tabPageSupplierUnlock.Controls.Add(this.dgvSupplierUnlock);
			this.tabPageSupplierUnlock.Location = new Point(4, 64);
			this.tabPageSupplierUnlock.Name = "tabPageSupplierUnlock";
			this.tabPageSupplierUnlock.Size = new Size(746, 574);
			this.tabPageSupplierUnlock.TabIndex = 2;
			this.tabPageSupplierUnlock.Text = "Supplier Unlock";
			this.tabPageSupplierUnlock.UseVisualStyleBackColor = true;
			// 
			// dgvSupplierUnlock
			// 
			this.dgvSupplierUnlock.AllowUserToAddRows = false;
			this.dgvSupplierUnlock.AllowUserToDeleteRows = false;
			this.dgvSupplierUnlock.AllowUserToResizeColumns = false;
			this.dgvSupplierUnlock.AllowUserToResizeRows = false;
			this.dgvSupplierUnlock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSupplierUnlock.Columns.AddRange(new DataGridViewColumn[] { this.colSupplierUnlockDealer, this.colSupplierUnlockStatus, this.colSupplierUnlockLock, this.colSupplierUnlockUnlock });
			this.dgvSupplierUnlock.Dock = DockStyle.Fill;
			this.dgvSupplierUnlock.Location = new Point(0, 0);
			this.dgvSupplierUnlock.Name = "dgvSupplierUnlock";
			this.dgvSupplierUnlock.RowHeadersVisible = false;
			this.dgvSupplierUnlock.RowHeadersWidth = 62;
			this.dgvSupplierUnlock.Size = new Size(746, 574);
			this.dgvSupplierUnlock.TabIndex = 1;
			this.dgvSupplierUnlock.CellClick += this.dgvSupplierUnlock_CellClick;
			// 
			// colSupplierUnlockDealer
			// 
			this.colSupplierUnlockDealer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colSupplierUnlockDealer.HeaderText = "Supplier";
			this.colSupplierUnlockDealer.MinimumWidth = 8;
			this.colSupplierUnlockDealer.Name = "colSupplierUnlockDealer";
			this.colSupplierUnlockDealer.ReadOnly = true;
			// 
			// colSupplierUnlockStatus
			// 
			this.colSupplierUnlockStatus.HeaderText = "Status";
			this.colSupplierUnlockStatus.MinimumWidth = 8;
			this.colSupplierUnlockStatus.Name = "colSupplierUnlockStatus";
			this.colSupplierUnlockStatus.ReadOnly = true;
			this.colSupplierUnlockStatus.Resizable = DataGridViewTriState.True;
			this.colSupplierUnlockStatus.Width = 128;
			// 
			// colSupplierUnlockLock
			// 
			this.colSupplierUnlockLock.HeaderText = "Lock";
			this.colSupplierUnlockLock.MinimumWidth = 8;
			this.colSupplierUnlockLock.Name = "colSupplierUnlockLock";
			this.colSupplierUnlockLock.Width = 96;
			// 
			// colSupplierUnlockUnlock
			// 
			this.colSupplierUnlockUnlock.HeaderText = "Unlock";
			this.colSupplierUnlockUnlock.MinimumWidth = 8;
			this.colSupplierUnlockUnlock.Name = "colSupplierUnlockUnlock";
			this.colSupplierUnlockUnlock.Width = 96;
			// 
			// tabPagePropertyOwnership
			// 
			this.tabPagePropertyOwnership.Controls.Add(this.dgvPropertyOwnership);
			this.tabPagePropertyOwnership.Location = new Point(4, 64);
			this.tabPagePropertyOwnership.Name = "tabPagePropertyOwnership";
			this.tabPagePropertyOwnership.Size = new Size(746, 574);
			this.tabPagePropertyOwnership.TabIndex = 4;
			this.tabPagePropertyOwnership.Text = "Property Ownership";
			this.tabPagePropertyOwnership.UseVisualStyleBackColor = true;
			// 
			// dgvPropertyOwnership
			// 
			this.dgvPropertyOwnership.AllowUserToAddRows = false;
			this.dgvPropertyOwnership.AllowUserToDeleteRows = false;
			this.dgvPropertyOwnership.AllowUserToResizeColumns = false;
			this.dgvPropertyOwnership.AllowUserToResizeRows = false;
			this.dgvPropertyOwnership.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPropertyOwnership.Columns.AddRange(new DataGridViewColumn[] { this.colPropertyOwnershipProperty, this.colPropertyOwnershipStatus, this.colPropertyOwnershipOwn, this.colPropertyOwnershipDisown });
			this.dgvPropertyOwnership.Dock = DockStyle.Fill;
			this.dgvPropertyOwnership.Location = new Point(0, 0);
			this.dgvPropertyOwnership.Name = "dgvPropertyOwnership";
			this.dgvPropertyOwnership.RowHeadersVisible = false;
			this.dgvPropertyOwnership.RowHeadersWidth = 62;
			this.dgvPropertyOwnership.Size = new Size(746, 574);
			this.dgvPropertyOwnership.TabIndex = 3;
			this.dgvPropertyOwnership.CellClick += this.dgvPropertyOwnership_CellClick;
			// 
			// colPropertyOwnershipProperty
			// 
			this.colPropertyOwnershipProperty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colPropertyOwnershipProperty.HeaderText = "Property";
			this.colPropertyOwnershipProperty.MinimumWidth = 8;
			this.colPropertyOwnershipProperty.Name = "colPropertyOwnershipProperty";
			this.colPropertyOwnershipProperty.ReadOnly = true;
			// 
			// colPropertyOwnershipStatus
			// 
			this.colPropertyOwnershipStatus.HeaderText = "Status";
			this.colPropertyOwnershipStatus.MinimumWidth = 8;
			this.colPropertyOwnershipStatus.Name = "colPropertyOwnershipStatus";
			this.colPropertyOwnershipStatus.ReadOnly = true;
			this.colPropertyOwnershipStatus.Resizable = DataGridViewTriState.True;
			this.colPropertyOwnershipStatus.Width = 128;
			// 
			// colPropertyOwnershipOwn
			// 
			this.colPropertyOwnershipOwn.HeaderText = "Own";
			this.colPropertyOwnershipOwn.MinimumWidth = 8;
			this.colPropertyOwnershipOwn.Name = "colPropertyOwnershipOwn";
			this.colPropertyOwnershipOwn.Width = 96;
			// 
			// colPropertyOwnershipDisown
			// 
			this.colPropertyOwnershipDisown.HeaderText = "Disown";
			this.colPropertyOwnershipDisown.MinimumWidth = 8;
			this.colPropertyOwnershipDisown.Name = "colPropertyOwnershipDisown";
			this.colPropertyOwnershipDisown.Width = 96;
			// 
			// tabPageBusinessOwnership
			// 
			this.tabPageBusinessOwnership.Controls.Add(this.dgvBusinessOwnership);
			this.tabPageBusinessOwnership.Location = new Point(4, 64);
			this.tabPageBusinessOwnership.Name = "tabPageBusinessOwnership";
			this.tabPageBusinessOwnership.Size = new Size(746, 574);
			this.tabPageBusinessOwnership.TabIndex = 3;
			this.tabPageBusinessOwnership.Text = "Business Ownership";
			this.tabPageBusinessOwnership.UseVisualStyleBackColor = true;
			// 
			// dgvBusinessOwnership
			// 
			this.dgvBusinessOwnership.AllowUserToAddRows = false;
			this.dgvBusinessOwnership.AllowUserToDeleteRows = false;
			this.dgvBusinessOwnership.AllowUserToResizeColumns = false;
			this.dgvBusinessOwnership.AllowUserToResizeRows = false;
			this.dgvBusinessOwnership.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvBusinessOwnership.Columns.AddRange(new DataGridViewColumn[] { this.colBusinessOwnershipBusiness, this.colBusinessOwnershipStatus, this.colBusinessOwnershipOwn, this.colBusinessOwnershipDisown });
			this.dgvBusinessOwnership.Dock = DockStyle.Fill;
			this.dgvBusinessOwnership.Location = new Point(0, 0);
			this.dgvBusinessOwnership.Name = "dgvBusinessOwnership";
			this.dgvBusinessOwnership.RowHeadersVisible = false;
			this.dgvBusinessOwnership.RowHeadersWidth = 62;
			this.dgvBusinessOwnership.Size = new Size(746, 574);
			this.dgvBusinessOwnership.TabIndex = 2;
			this.dgvBusinessOwnership.CellClick += this.dgvBusinessOwnership_CellClick;
			// 
			// colBusinessOwnershipBusiness
			// 
			this.colBusinessOwnershipBusiness.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colBusinessOwnershipBusiness.HeaderText = "Business";
			this.colBusinessOwnershipBusiness.MinimumWidth = 8;
			this.colBusinessOwnershipBusiness.Name = "colBusinessOwnershipBusiness";
			this.colBusinessOwnershipBusiness.ReadOnly = true;
			// 
			// colBusinessOwnershipStatus
			// 
			this.colBusinessOwnershipStatus.HeaderText = "Status";
			this.colBusinessOwnershipStatus.MinimumWidth = 8;
			this.colBusinessOwnershipStatus.Name = "colBusinessOwnershipStatus";
			this.colBusinessOwnershipStatus.ReadOnly = true;
			this.colBusinessOwnershipStatus.Resizable = DataGridViewTriState.True;
			this.colBusinessOwnershipStatus.Width = 128;
			// 
			// colBusinessOwnershipOwn
			// 
			this.colBusinessOwnershipOwn.HeaderText = "Own";
			this.colBusinessOwnershipOwn.MinimumWidth = 8;
			this.colBusinessOwnershipOwn.Name = "colBusinessOwnershipOwn";
			this.colBusinessOwnershipOwn.Width = 96;
			// 
			// colBusinessOwnershipDisown
			// 
			this.colBusinessOwnershipDisown.HeaderText = "Disown";
			this.colBusinessOwnershipDisown.MinimumWidth = 8;
			this.colBusinessOwnershipDisown.Name = "colBusinessOwnershipDisown";
			this.colBusinessOwnershipDisown.Width = 96;
			// 
			// tabPageVehicleOwnership
			// 
			this.tabPageVehicleOwnership.Location = new Point(4, 64);
			this.tabPageVehicleOwnership.Name = "tabPageVehicleOwnership";
			this.tabPageVehicleOwnership.Size = new Size(746, 574);
			this.tabPageVehicleOwnership.TabIndex = 5;
			this.tabPageVehicleOwnership.Text = "Vehicle Ownership";
			this.tabPageVehicleOwnership.UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new SizeF(10F, 25F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(778, 744);
			this.Controls.Add(this.tabControlMain);
			this.Controls.Add(this.tlpTop);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Schedule I Save Fixer";
			this.Load += this.FormMain_Load;
			this.tlpTop.ResumeLayout(false);
			this.tlpTop.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.dgvFixActions).EndInit();
			this.panelFixActionsBottom.ResumeLayout(false);
			this.panelFixActionsBottom.PerformLayout();
			this.tabControlMain.ResumeLayout(false);
			this.tabPageFixActions.ResumeLayout(false);
			this.tabPageCustomerRelationships.ResumeLayout(false);
			this.gbCustomers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.dgvCustomerRelationships).EndInit();
			this.gbCustomerRegions.ResumeLayout(false);
			this.gbCustomerRegions.PerformLayout();
			this.tabPageSupplierUnlock.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.dgvSupplierUnlock).EndInit();
			this.tabPagePropertyOwnership.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.dgvPropertyOwnership).EndInit();
			this.tabPageBusinessOwnership.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.dgvBusinessOwnership).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private Label lblSavePath;
		private TableLayoutPanel tlpTop;
		private Label lblSaveSlot;
		private TextBox tbSavePath;
		private Button btnSavePathBrowse;
		private ComboBox cbSaveSlot;
		private FolderBrowserDialog folderBrowserDialog;
		private Panel panelFixActionsBottom;
		private Button btnFix;
		private DataGridView dgvFixActions;
		private TabControl tabControlMain;
		private TabPage tabPageFixActions;
		private TabPage tabPageCustomerRelationships;
		private TabPage tabPageSupplierUnlock;
		private TabPage tabPageBusinessOwnership;
		private CheckBox cbEnableFileDeletion;
		private Button btnSaveSlotOpen;
		private ToolTip toolTip;
		private DataGridViewCheckBoxColumn colFixActionsEnable;
		private DataGridViewTextBoxColumn colFixActionsFilePath;
		private DataGridViewTextBoxColumn colFixActionsFixAction;
		private DataGridViewTextBoxColumn colFixActionsFixResult;
		private GroupBox gbCustomers;
		private DataGridView dgvCustomerRelationships;
		private GroupBox gbCustomerRegions;
		private Label label2;
		private ComboBox cbCustomerRegion;
		private Button btnCustomerRegionApply;
		private ComboBox cbCustomerRegionRelationship;
		private DataGridView dgvSupplierUnlock;
		private DataGridViewTextBoxColumn colCustomerRelationshipsNpc;
		private DataGridViewComboBoxColumn colCustomerRelationshipsRelationship;
		private DataGridViewButtonColumn colCustomerRelationshipsApply;
		private DataGridViewTextBoxColumn colSupplierUnlockDealer;
		private DataGridViewTextBoxColumn colSupplierUnlockStatus;
		private DataGridViewButtonColumn colSupplierUnlockLock;
		private DataGridViewButtonColumn colSupplierUnlockUnlock;
		private DataGridView dgvBusinessOwnership;
		private DataGridViewTextBoxColumn colBusinessOwnershipBusiness;
		private DataGridViewTextBoxColumn colBusinessOwnershipStatus;
		private DataGridViewButtonColumn colBusinessOwnershipOwn;
		private DataGridViewButtonColumn colBusinessOwnershipDisown;
		private TabPage tabPagePropertyOwnership;
		private TabPage tabPageVehicleOwnership;
		private DataGridView dgvPropertyOwnership;
		private DataGridViewTextBoxColumn colPropertyOwnershipProperty;
		private DataGridViewTextBoxColumn colPropertyOwnershipStatus;
		private DataGridViewButtonColumn colPropertyOwnershipOwn;
		private DataGridViewButtonColumn colPropertyOwnershipDisown;
	}
}
