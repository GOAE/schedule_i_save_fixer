using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Schedule_I_Save_Fixer {
	//
	// Classes
	//
	/// <summary>The main form.</summary>
	public partial class FormMain : Form {
		//
		// Properties
		//
		/// <summary>The save slots that were detected under the specified saves game path.</summary>
		private BindingList<SaveSlot> saveSlots = new();
		/// <summary>The fix action definitions that are used by the auto fix tab.</summary>
		private FixActionDefinition[] fixActionDefinitions;
		/// <summary>The proposed fix actions that can be applied to auto fix the chosen save slot.</summary>
		private List<FixAction>? fixActions = null;
		/// <summary>Customers grouped by region. This does not include suppliers.</summary>
		private Region[] customersByRegion = new Region[] {
			new Region("Westville", new string[] {
				"Charles Rowland",
				"Dean Webster",
				"Doris Lubbin",
				"George Greene",
				"Jerry Montero",
				"Joyce Ball",
				"Keith Wagner",
				"Kim Delaney",
				"Meg Cooley",
				"Trent Sherman",
			}),
			new Region("Downtown", new string[] {
				"Elizabeth Homley",
				"Eugene Buckley",
				"Greg Figgle",
				"Jeff Gilmore",
				"Jennifer Rivera",
				"Kevin Oakley",
				"Louis Fourier",
				"Lucy Pennington",
				"Philip Wentworth",
				"Randy Caulfield",
			}),
			new Region("Docks", new string[] {
				"Anna Chesterfield",
				"Billy Kramer",
				"Cranky Frank",
				"Genghis Barn",
				"Javier Perez",
				"Lisa Gardener",
				"Mac Cooper",
				"Marco Barone",
				"Melissa Wood",
			}),
			new Region("Northtown", new string[] {
				"Austin Steiner",
				"Beth Penn",
				"Chloe Bowers",
				"Donna Martin",
				"Geraldine Poon",
				"Jessi Waters",
				"Kathy Henderson",
				"Kyle Cooley",
				"Ludwig Meyer",
				"Mick Lubbin",
				"Mrs. Ming",
				"Peggy Myers",
				"Peter File",
				"Sam Thompson",
			}),
			new Region("Uptown", new string[] {
				"Fiona Hancock",
				"Herbert Bleuball",
				"Jen Heard",
				"Lily Turner",
				"Michael Boog",
				"Pearl Moore",
				"Ray Hoffman",
				"Tobas Wentworth",
				"Walter Cussler",
			}),
			new Region("Suburbia", new string[] {
				"Alison Knight",
				"Carl Bundy",
				"Chris Sullivan",
				"Dennis Kennedy",
				"Hank Stevenson",
				"Harold Colt",
				"Jack Knight",
				"Jackie Stevenson",
				"Jeremy Wilkinson",
				"Karen Kennedy",
			}),
		};
		/// <summary>All customers names.</summary>
		private List<string> customers;
		/// <summary>All customer regions, each value in this array correspond to the element in this.customers at the same index.</summary>
		private List<string> customerRegions;
		/// <summary>All supplier names.</summary>
		private string[] supplierNames = new string[] {
			"Albert Hoover",
			"Shirley Watts",
			"Salvador Moreno",
		};
		/// <summary>All property names.</summary>
		private string[] propertyNames = new string[] {
			"Motel Room",
			"Sweatshop",
			"Bungalow",
			"Barn",
			"Docks Warehouse", // TODO: Is this the right name?
		};
		/// <summary>All business names.</summary>
		private string[] businessNames = new string[] {
			"Car Wash",
			"Laundromat",
			"Post Office",
			"Taco Ticklers",
		};
		// TODO: vehicleNames

		//
		// Construction Methods
		//
		/// <summary>Constructs this class.</summary>
		public FormMain() {
			InitializeComponent();

			var tempCustomers = new List<Tuple<string, string>>();
			foreach (var region in this.customersByRegion) {
				this.cbCustomerRegion.Items.Add(region.name);
				foreach (var customer in region.customers) {
					tempCustomers.Add(new Tuple<string, string>(customer, region.name));
				}
			}
			var sortedCustomers = from entry in tempCustomers orderby entry.Item1 ascending select entry;
			this.customers = (from entry in sortedCustomers select entry.Item1).ToList();
			this.customerRegions = (from entry in sortedCustomers select entry.Item2).ToList();
			this.fixActionDefinitions = new FixActionDefinition[] {
				new FixActionDefinition.MissingCritical(),
				new FixActionDefinition.MissingGame(),
				new FixActionDefinition.MissingLaw(),
				new FixActionDefinition.MissingTime(),
				new FixActionDefinition.MissingPlayer(),
				new FixActionDefinition.MissingBusinessLaunderingStation(),
				// new FixActionDefinition.MissingNpcCustomerData(),
				new FixActionDefinition.MissingNpcNpc(),
				// new FixActionDefinition.MissingNpcRelationship(),
				// new FixActionDefinition.MissingNpcSupplierMessages(this.supplierNames),
				// IMPORTANT: InvalidJson should be last so it's lowest precedence.
				new FixActionDefinition.InvalidJson(),
			};

			this.Text += " v" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
			this.cbSaveSlot.DataSource = this.saveSlots;
			this.cbCustomerRegion.SelectedIndex = 0;
			this.cbCustomerRegionRelationship.SelectedIndex = 0;
			for (int i = 0; i < this.customers.Count; ++i) {
				var row = new DataGridViewRow();
				var region = new DataGridViewTextBoxCell();
				region.Value = this.customerRegions[i];
				row.Cells.Add(region);
				var name = new DataGridViewTextBoxCell();
				name.Value = this.customers[i];
				row.Cells.Add(name);
				var relationship = new DataGridViewComboBoxCell();
				foreach (var item in this.cbCustomerRegionRelationship.Items) {
					relationship.Items.Add(item);
				}
				row.Cells.Add(relationship);
				var apply = new DataGridViewButtonCell();
				apply.Value = "Apply";
				row.Cells.Add(apply);
				this.dgvCustomerRelationships.Rows.Add(row);
			}
			foreach (var supplier in this.supplierNames) {
				var row = new DataGridViewRow();
				var name = new DataGridViewTextBoxCell();
				name.Value = supplier;
				row.Cells.Add(name);
				var status = new DataGridViewTextBoxCell();
				status.Value = "Locked";
				row.Cells.Add(status);
				var lockCell = new DataGridViewButtonCell();
				lockCell.Value = "Lock";
				row.Cells.Add(lockCell);
				var unlockCell = new DataGridViewButtonCell();
				unlockCell.Value = "Unlock";
				row.Cells.Add(unlockCell);
				this.dgvSupplierUnlock.Rows.Add(row);
			}
			foreach (var propertyName in this.propertyNames) {
				var row = new DataGridViewRow();
				var name = new DataGridViewTextBoxCell();
				name.Value = propertyName;
				row.Cells.Add(name);
				var status = new DataGridViewTextBoxCell();
				status.Value = "Not Owned";
				row.Cells.Add(status);
				var own = new DataGridViewButtonCell();
				own.Value = "Own";
				row.Cells.Add(own);
				var disown = new DataGridViewButtonCell();
				disown.Value = "Disown";
				row.Cells.Add(disown);
				this.dgvPropertyOwnership.Rows.Add(row);
			}
			foreach (var business in this.businessNames) {
				var row = new DataGridViewRow();
				var name = new DataGridViewTextBoxCell();
				name.Value = business;
				row.Cells.Add(name);
				var status = new DataGridViewTextBoxCell();
				status.Value = "Not Owned";
				row.Cells.Add(status);
				var own = new DataGridViewButtonCell();
				own.Value = "Own";
				row.Cells.Add(own);
				var disown = new DataGridViewButtonCell();
				disown.Value = "Disown";
				row.Cells.Add(disown);
				this.dgvBusinessOwnership.Rows.Add(row);
			}
		}

		//
		// Form Methods
		//
		/// <summary>The callback that is invoked when the form loads</summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void FormMain_Load(object sender, EventArgs e) {
			try {
				if (File.Exists("Settings.json")) {
					var json = JsonDocument.Parse(File.ReadAllText("Settings.json"));
					if (json != null) {
						if (json.RootElement.TryGetProperty("saves_path", out var savesPath) && savesPath.ValueKind == JsonValueKind.String) {
							this.tbSavePath.Text = savesPath.GetString();
						}
					}
				}
			} catch { }
			this.fetchSaveSlots();
		}
		/// <summary>Saves the settings of this application.</summary>
		private void saveSettings() {
			try {
				var json = new JsonObject {
					{ "saves_path", this.tbSavePath.Text },
				};
				File.WriteAllText("Settings.json", json.ToJsonString());
			} catch { }
		}

		//
		// Child Control Methods
		//

		private void tbSavePath_TextChanged(object sender, EventArgs e) {
			this.fetchSaveSlots();
			this.saveSettings();
		}

		private void btnSavePathBrowse_Click(object sender, EventArgs e) {
			if (this.folderBrowserDialog.SelectedPath.Length == 0) {
				this.folderBrowserDialog.SelectedPath = Environment.ExpandEnvironmentVariables(this.tbSavePath.Text);
			}
			if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK) {
				var path = this.folderBrowserDialog.SelectedPath.Replace('/', '\\');
				if (path.EndsWith("\\TVGS")) {
					path = path + "\\Schedule I\\Saves";
				} else if (path.EndsWith("\\Schedule I")) {
					path = path + "\\Saves";
				} else {
					var lastComponent = path.Substring(path.LastIndexOf('\\') + 1);
					if (lastComponent.StartsWith("SaveGame_")) {
						path = path.TrimEnd('\\');
						path = path.Substring(0, path.LastIndexOf('\\'));
						path = path.Substring(0, path.LastIndexOf('\\'));
					} else if (UInt64.TryParse(lastComponent, out var steamId)) {
						path = path.TrimEnd('\\');
						path = path.Substring(0, path.LastIndexOf('\\'));
					}
				}
				this.tbSavePath.Text = path;
			}
		}

		private void cbSaveSlot_SelectedIndexChanged(object sender, EventArgs e) {
			this.dgvFixActions.Rows.Clear();
			if (this.cbSaveSlot.SelectedItem != null) {
				this.populateFixActions();
				this.updateCustomerRelationships();
				this.updateSupplierUnlock();
				this.updatePropertyOwnership();
				this.updateBusinessOwnership();
			}
			var enabled = this.cbSaveSlot.SelectedItem != null;
			this.btnSaveSlotOpen.Enabled = enabled;
			this.btnFix.Enabled = enabled;
			this.tabControlMain.Enabled = enabled;
		}

		private void btnSaveSlotOpen_Click(object sender, EventArgs e) {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			try {
				Process.Start("explorer.exe", ((SaveSlot)this.cbSaveSlot.SelectedItem).getDirectoryPath());
			} catch { }
		}

		private void cbEnableFileDeletion_CheckedChanged(object sender, EventArgs e) {
			if (this.cbEnableFileDeletion.Checked) {
				// TODO: Inform the user of the risk? If they reject it, this.cbEnableFileDeletion.Checked = false;
			}
		}

		private void btnAutoFixBackups_Click(object sender, EventArgs e) {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			try {
				var backupsPath = ((SaveSlot)this.cbSaveSlot.SelectedItem).getBackupsDirectory();
				if (Directory.Exists(backupsPath)) {
					Process.Start("explorer.exe", backupsPath);
				} else {
					MessageBox.Show("The backups directory does not exist. Are you sure a backup has been made for the chosen save slot?",
						"No backups.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			} catch { }
		}

		private void btnFix_Click(object sender, EventArgs e) {
			Debug.Assert(this.fixActions != null);
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var hasDeleteFile = false;
			var hasCriticalFile = false;
			for (int i = 0; i < this.fixActions.Count; ++i) {
				var fixAction = this.fixActions[i];
				if (!fixAction.attemptedFix && ((DataGridViewCheckBoxCell)this.dgvFixActions.Rows[i].Cells[0]).Value == ((DataGridViewCheckBoxCell)this.dgvFixActions.Rows[i].Cells[0]).TrueValue) {
					if (fixAction.fixCriteria == FixActionCriteria.DeleteFile) {
						hasDeleteFile = true;
					} else if (fixAction.fixCriteria == FixActionCriteria.AlwaysCritical) {
						hasCriticalFile = true;
					}
				}
			}
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			switch (saveSlot.promptToBackup()) {
				case DialogResult.Yes:
					if (!saveSlot.backup(out var errorMessage)) {
						MessageBox.Show("Failed to back up your save slot, so to be safe recovery has been aborted.\r\n\r\n" + errorMessage,
							"Failed to backup.", MessageBoxButtons.OK, MessageBoxIcon.Error);
						saveSlot.resetPromptedForBackup();
						return;
					}
					break;

				case DialogResult.Cancel:
					return;
			}
			if (hasDeleteFile && !this.cbEnableFileDeletion.Checked) {
				MessageBox.Show(
					"One or more files in your save slot was determined to be corrupt but have no specific recovery action available.\r\n"
						+ "It is possible your save game will not load correctly or at all without manually fixing the file yourself or without permitting this application to "
						+ "delete the file.\r\n"
						+ "Should you so choose, you can check the checkbox to delete files and we'll try getting rid of the files to see if that fixes your save.",
					"Deleting files is not enabled.",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation
				);
			}
			if (hasCriticalFile) {
				if (MessageBox.Show(
						"One or more files in your save slot are very important to game progress but cannot be safely recovered without setting your progress back.\r\n\r\n"
							+ "Do you still want to proceed?",
						"A critical file is corrupted or missing.",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Exclamation
						) != DialogResult.Yes) {
					return;
				}
			}
			for (int i = 0; i < this.fixActions.Count; ++i) {
				var fixAction = this.fixActions[i];
				if (!fixAction.attemptedFix && ((DataGridViewCheckBoxCell)this.dgvFixActions.Rows[i].Cells[0]).Value == ((DataGridViewCheckBoxCell)this.dgvFixActions.Rows[i].Cells[0]).TrueValue
						&& (fixAction.fixCriteria != FixActionCriteria.DeleteFile || this.cbEnableFileDeletion.Checked)) {
					fixAction.fix();
					fixAction.updateDgvRowResult(this.dgvFixActions.Rows[i], this.colFixActionsFixResult.Index);
				}
			}
		}

		private void btnCustomerRegionApply_Click(object sender, EventArgs e) {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			Debug.Assert(this.cbCustomerRegion.SelectedIndex >= 0);
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			var region = this.customersByRegion[this.cbCustomerRegion.SelectedIndex];
			foreach (var customer in region.customers) {
				var customerIndex = this.customers.IndexOf(customer);
				var cell = (DataGridViewComboBoxCell)this.dgvCustomerRelationships.Rows[customerIndex].Cells[this.colCustomerRelationshipsRelationship.Index];
				cell.Value = cell.Items[this.cbCustomerRegionRelationship.SelectedIndex];
				saveSlot.setCustomerRelationship(customer, this.cbCustomerRegionRelationship.SelectedIndex);
			}
		}

		private void dgvCustomerRelationships_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex >= 0 && e.ColumnIndex == this.colCustomerRelationshipsApply.Index) {
				Debug.Assert(this.cbSaveSlot.SelectedItem != null);
				var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
				var customer = this.customers[e.RowIndex];
				var cell = (DataGridViewComboBoxCell)this.dgvCustomerRelationships.Rows[e.RowIndex].Cells[this.colCustomerRelationshipsRelationship.Index];
				var relationshipValue = cell.Items.IndexOf(cell.Value);
				saveSlot.setCustomerRelationship(customer, relationshipValue);
			}
		}

		private void dgvSupplierUnlock_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex >= 0) {
				Debug.Assert(this.cbSaveSlot.SelectedItem != null);
				var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
				var supplier = this.supplierNames[e.RowIndex];
				var statusCell = (DataGridViewTextBoxCell)this.dgvSupplierUnlock.Rows[e.RowIndex].Cells[this.colSupplierUnlockStatus.Index];
				if (e.ColumnIndex == this.colSupplierUnlockLock.Index) {
					saveSlot.setSupplierUnlocked(supplier, true);
					statusCell.Value = "Unlocked";
				} else if (e.ColumnIndex == this.colSupplierUnlockUnlock.Index) {
					saveSlot.setSupplierUnlocked(supplier, false);
					statusCell.Value = "Locked";
				}
			}
		}

		private void dgvPropertyOwnership_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex >= 0) {
				Debug.Assert(this.cbSaveSlot.SelectedItem != null);
				var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
				var propertyName = this.propertyNames[e.RowIndex];
				var statusCell = (DataGridViewTextBoxCell)this.dgvPropertyOwnership.Rows[e.RowIndex].Cells[this.colPropertyOwnershipStatus.Index];
				if (e.ColumnIndex == this.colSupplierUnlockLock.Index) {
					saveSlot.setPropertyOwned(propertyName, true);
					statusCell.Value = "Owned";
				} else if (e.ColumnIndex == this.colSupplierUnlockUnlock.Index) {
					saveSlot.setPropertyOwned(propertyName, false);
					statusCell.Value = "Not Owned";
				}
			}
		}

		private void dgvBusinessOwnership_CellClick(object sender, DataGridViewCellEventArgs e) {
			if (e.RowIndex >= 0) {
				Debug.Assert(this.cbSaveSlot.SelectedItem != null);
				var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
				var business = this.businessNames[e.RowIndex];
				var statusCell = (DataGridViewTextBoxCell)this.dgvBusinessOwnership.Rows[e.RowIndex].Cells[this.colBusinessOwnershipStatus.Index];
				if (e.ColumnIndex == this.colSupplierUnlockLock.Index) {
					saveSlot.setBusinessOwned(business, true);
					statusCell.Value = "Owned";
				} else if (e.ColumnIndex == this.colSupplierUnlockUnlock.Index) {
					saveSlot.setBusinessOwned(business, false);
					statusCell.Value = "Not Owned";
				}
			}
		}

		//
		// Save Slot Methods
		//
		/// <summary>Fetch save slots detected within the specified game saves path, populating this.cbSaveSlot.</summary>
		private void fetchSaveSlots() {
			this.saveSlots.Clear();
			try {
				var savePath = Environment.ExpandEnvironmentVariables(this.tbSavePath.Text.Replace('/', '\\'));
				while (savePath.EndsWith(Path.DirectorySeparatorChar)) {
					savePath = savePath.Substring(0, savePath.Length - 1);
				}
				foreach (var steamIdSubdirectoryPath in Directory.EnumerateDirectories(savePath)) {
					if (ulong.TryParse(steamIdSubdirectoryPath.Substring(steamIdSubdirectoryPath.LastIndexOf('\\') + 1), out var steamId)) {
						for (uint i = 1; i <= 20; ++i) {
							var path = savePath + "\\" + steamId.ToString() + "\\SaveGame_" + i.ToString() + "\\";
							if (Directory.Exists(path)) {
								this.saveSlots.Add(new SaveSlot(steamId, i, path));
							}
						}
					}
				}
				this.cbSaveSlot_SelectedIndexChanged(this, EventArgs.Empty);
			} catch { }
		}
		/// <summary>Populate this.dgvFixActions.</summary>
		private void populateFixActions() {
			Debug.Assert(this.dgvFixActions.Rows.Count == 0);
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var saveSlotPath = ((SaveSlot)this.cbSaveSlot.SelectedItem).getDirectoryPath();
			Debug.Assert(saveSlotPath != null);
			this.fixActions = new List<FixAction>();
			var fixActionFilePaths = new Dictionary<string, bool>();
			foreach (var fixActionDefinition in this.fixActionDefinitions) {
				var potentialFilePaths = fixActionDefinition.getPotentialFilePaths(saveSlotPath);
				foreach (var potentialFilePath in potentialFilePaths) {
					if (fixActionDefinition.shouldFix(potentialFilePath, out var fixAction, out var fixCriteria) && fixCriteria != FixActionCriteria.Never
							&& !fixActionFilePaths.ContainsKey(potentialFilePath)) {
						this.fixActions.Add(new FixAction(fixActionDefinition, saveSlotPath, null, potentialFilePath, fixAction, fixCriteria));
						fixActionFilePaths.Add(potentialFilePath, true);
					}
				}
			}
			var rows = new DataGridViewRow[this.fixActions.Count];
			for (int i = 0; i < this.fixActions.Count; ++i) {
				rows[i] = this.fixActions[i].createDgvRow();
			}
			this.dgvFixActions.Rows.AddRange(rows);
		}
		/// <summary>Update the Relationship column of this.dgvCustomerRelationships.</summary>
		private void updateCustomerRelationships() {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			for (int i = 0; i < this.customers.Count; ++i) {
				var relationship = saveSlot.getCustomerRelationship(this.customers[i]);
				var cell = (DataGridViewComboBoxCell)this.dgvCustomerRelationships.Rows[i].Cells[this.colCustomerRelationshipsRelationship.Index];
				cell.Value = cell.Items[relationship];
			}
		}
		/// <summary>Update the Status column of this.dgvSupplierUnlock.</summary>
		private void updateSupplierUnlock() {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			for (int i = 0; i < this.supplierNames.Length; ++i) {
				var unlocked = saveSlot.isSupplierUnlocked(this.supplierNames[i]);
				var cell = (DataGridViewTextBoxCell)this.dgvSupplierUnlock.Rows[i].Cells[this.colSupplierUnlockStatus.Index];
				cell.Value = unlocked ? "Unlocked" : "Locked";
			}
		}
		/// <summary>Update the Status column of this.dgvPropertyOwnership.</summary>
		private void updatePropertyOwnership() {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			for (int i = 0; i < this.propertyNames.Length; ++i) {
				var owned = saveSlot.isPropertyOwned(this.propertyNames[i]);
				var cell = (DataGridViewTextBoxCell)this.dgvPropertyOwnership.Rows[i].Cells[this.colPropertyOwnershipStatus.Index];
				cell.Value = owned ? "Owned" : "Not Owned";
			}
		}
		/// <summary>Update the Status column of this.dgvBusinessOwnership.</summary>
		private void updateBusinessOwnership() {
			Debug.Assert(this.cbSaveSlot.SelectedItem != null);
			var saveSlot = (SaveSlot)this.cbSaveSlot.SelectedItem;
			for (int i = 0; i < this.businessNames.Length; ++i) {
				var owned = saveSlot.isBusinessOwned(this.businessNames[i]);
				var cell = (DataGridViewTextBoxCell)this.dgvBusinessOwnership.Rows[i].Cells[this.colBusinessOwnershipStatus.Index];
				cell.Value = owned ? "Owned" : "Not Owned";
			}
		}
	}
}
