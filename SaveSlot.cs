using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Schedule_I_Save_Fixer {
	//
	// Structures
	//
	/// <summary>A region within the game.</summary>
	internal struct Region {
		//
		// Properties
		//
		/// <summary>The name of the region.</summary>
		internal string name;
		/// <summary>The names of the customers in this region.</summary>
		internal string[] customers;

		//
		// Construction Methods
		//
		/// <summary>Constructs this structure.</summary>
		/// <param name="name">The name of the region.</param>
		/// <param name="customers">The names of the customers in this region.</param>
		public Region(string name, string[] customers) {
			this.name = name;
			this.customers = customers;
		}
	}

	//
	// Classes
	//
	/// <summary>A Schedule I save slot.</summary>
	internal class SaveSlot {
		//
		// Properties
		//
		/// <summary>The Steam ID of the save that contains this save slot.</summary>
		private ulong steamId;
		/// <summary>The slot number of this save slot.</summary>
		private uint slotNumber;
		/// <summary>The path to this save slot, including a trailing slash.</summary>
		private string directoryPath;
		/// <summary>Whether the user has been prompted to have their save slot backed up.</summary>
		private bool promptedForBackup;

		//
		// Construction Methods
		//
		/// <summary>Constructs this class.</summary>
		/// <param name="steamId">The Steam ID of the save that contains this save slot.</param>
		/// <param name="slotNumber">The slot number of this save slot.</param>
		/// <param name="directoryPath">The path to this save slot, including a trailing slash.</param>
		internal SaveSlot(ulong steamId, uint slotNumber, string directoryPath) {
			this.steamId = steamId;
			this.slotNumber = slotNumber;
			this.directoryPath = directoryPath;
			this.promptedForBackup = false;
		}

		//
		// Save Slot Methods
		//
		/// <summary>Gets the directory path of this save slot.</summary>
		/// <returns>The directory path of this save slot, including a trailing slash.</returns>
		internal string getDirectoryPath() {
			return this.directoryPath;
		}
		/// <summary>Gets the directory path to the parent steam ID directory containing its save slots.</summary>
		/// <returns>The directory path to the parent steam ID directory containing its save slots, including a trailing slash.</returns>
		internal string getSteamIdPath() {
			var steamIdPath = this.directoryPath.TrimEnd('\\');
			steamIdPath = steamIdPath.Substring(0, steamIdPath.LastIndexOf('\\'));
			return steamIdPath + "\\";
		}
		/// <summary>Gets the directory path to the backups directory of the parent steam ID.</summary>
		/// <returns>The directory path to the backups directory of the parent steam ID, including a trailing slash.</returns>
		internal string getBackupsDirectory() {
			return this.getSteamIdPath() + "Backups\\";
		}
		/// <summary>Reset whether this save slot has prompted to backup.</summary>
		internal void resetPromptedForBackup() {
			this.promptedForBackup = false;
		}
		/// <summary>Prompt the user to backup this save slot.</summary>
		/// <returns>The DialogResult. If yes, this.backup() should be called from outside this method. If no, fixing the save slot can proceed without backing up. If cancel, then cancel.</returns>
		internal DialogResult promptToBackup() {
			if (this.promptedForBackup) {
				return DialogResult.No;
			} else {
				var result = MessageBox.Show("To reduce the risk of losing more progress from recovery, would you like to back up your save slot before we attempt recovery?\r\n\r\n"
					+ "Backups will be stored as zip files in the parent directory of the save slot.",
					"Backup your save first?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				if (result != DialogResult.Cancel) {
					this.promptedForBackup = true;
				}
				return result;
			}
		}
		/// <summary>Recurses into a directory during a backup.</summary>
		/// <param name="archive">The archive to backup files into.</param>
		/// <param name="savePath">The save path to recurse into. Does not need to end in a trailing slash.</param>
		/// <param name="errorMessage"></param>
		/// <returns>Whether recursion and backing up every file succeeded.</returns>
		private bool recurseBackup(ZipArchive archive, string savePath, ref string errorMessage) {
			try {
				foreach (var filePath in Directory.EnumerateFiles(savePath)) {
					archive.CreateEntryFromFile(filePath, filePath.Substring(this.directoryPath.Length));
				}
				foreach (var subdirectoryPath in Directory.EnumerateDirectories(savePath)) {
					if (!this.recurseBackup(archive, subdirectoryPath, ref errorMessage)) {
						return false;
					}
				}
				return true;
			} catch (Exception ex) {
				errorMessage = ex.Message;
				return false;
			}
		}
		/// <summary>Backs up this save slot.</summary>
		/// <param name="errorMessage">The variable that will receive an error message if backing up failed.</param>
		/// <returns>Whether backup was successful.</returns>
		internal bool backup(out string errorMessage) {
			try {
				var now = DateTime.Now;
				var backupsPath = this.getBackupsDirectory();
				if (!Directory.Exists(backupsPath)) {
					Directory.CreateDirectory(backupsPath);
				}
				using (var archive = new ZipArchive(
						new FileStream(backupsPath + "SaveGame_" + this.slotNumber.ToString()
							+ " " + now.Year.ToString() + "-" + now.Month.ToString() + "-" + now.Day.ToString()
							+ " " + now.Hour.ToString() + "-" + now.Minute.ToString() + "-" + now.Second.ToString()
							+ ".zip", FileMode.CreateNew, FileAccess.Write),
						ZipArchiveMode.Create,
						false
						)) {
					errorMessage = "";
					return this.recurseBackup(archive, this.directoryPath, ref errorMessage);
				}
			} catch (Exception ex) {
				errorMessage = ex.Message;
				return false;
			}
		}
		/// <summary>Gets the customer relationship level of the specified customer.</summary>
		/// <param name="customerName">The customer name.</param>
		/// <returns>The customer relationship level of the specified customer, ranging from 0 (locked) to 5 (max relationship).</returns>
		internal int getCustomerRelationship(string customerName) {
			try {
				var customerPath = this.directoryPath + "NPCs\\" + customerName + "\\";
				var relationshipPath = customerPath + "Relationship.json";
				// TODO: Consider checking "HasBeenRecommended" in CustomerData.json?
				if (File.Exists(relationshipPath)) {
					var json = JsonDocument.Parse(File.ReadAllText(relationshipPath));
					JsonElement? root = json != null ? json.RootElement : null;
					if (root != null && root.Value.TryGetProperty("Unlocked", out var unlocked) && unlocked.GetBoolean()
							&& root.Value.TryGetProperty("RelationDelta", out var relationDelta)) {
						var result = (int)relationDelta.GetDouble();
						if (result >= 1 && result <= 5) {
							return result;
						}
					}
				}
			} catch {}
			return 0;
		}
		/// <summary>Sets the customer relationship level of the specified customer.</summary>
		/// <param name="customerName">The customer name.</param>
		/// <param name="relationshipValue">The relationship level, ranging from 0 (locked) to 5 (max relationship).</param>
		internal void setCustomerRelationship(string customerName, int relationshipValue) {
			Debug.Assert(relationshipValue >= 0 && relationshipValue <= 5);
			try {
				var customerPath = this.directoryPath + "NPCs\\" + customerName + "\\";
				var relationshipPath = customerPath + "Relationship.json";
				var relationshipTemplatePath = FixActionDefinition.JsonRecoveryFilesPath + "\\NPCs_Templates\\Relationship.json";
				var relationshipFileExists = File.Exists(relationshipPath);
				// TODO: Consider setting "HasBeenRecommended" in CustomerData.json?
				JsonNode? json = null;
				try {
					json = JsonNode.Parse(File.ReadAllText(relationshipFileExists ? relationshipPath : relationshipTemplatePath));
				} catch {
					if (relationshipFileExists) {
						json = JsonNode.Parse(File.ReadAllText(relationshipTemplatePath));
					}
				}
				if (json != null) {
					JsonNode root = json.Root;
					root["Unlocked"] = relationshipValue > 0; // TODO: 'Unlocked' should be true even if relationshipValue is zero when the prior customer node in the relationship tree is at 5/5?
					root["RelationDelta"] = (double)relationshipValue;
					File.WriteAllText(relationshipPath, json.ToJsonString());
				}
			} catch {}
		}
		/// <summary>Checks whether the specified supplier is unlocked.</summary>
		/// <param name="supplierName">The supplier name.</param>
		/// <returns>Whether the specified supplier is unlocked.</returns>
		internal bool isSupplierUnlocked(string supplierName) {
			var customerPath = this.directoryPath + "NPCs\\" + supplierName + "\\";
			var messageConversationPath = customerPath + "MessageConversation.json";
			return this.getCustomerRelationship(supplierName) > 0 && File.Exists(messageConversationPath);
		}
		/// <summary>Sets whether the specified supplier is unlocked.</summary>
		/// <param name="supplierName">The supplier name.</param>
		/// <param name="unlock">Whether to unlock the specified supplier. If false, the supplier will become locked.</param>
		internal void setSupplierUnlocked(string supplierName, bool unlock) {
			try {
				var currentRelationship = this.getCustomerRelationship(supplierName);
				var customerPath = this.directoryPath + "NPCs\\" + supplierName + "\\";
				var messageConversationPath = customerPath + "MessageConversation.json";
				var messageConversationTemplatePath = FixActionDefinition.JsonRecoveryFilesPath + "\\NPCs_Templates\\MessageConversation.json";
				try {
					if (unlock) {
						if (currentRelationship == 0) {
							this.setCustomerRelationship(supplierName, 1);
						}
						if (!File.Exists(messageConversationPath)) {
							File.Copy(messageConversationTemplatePath, messageConversationPath);
						}
					} else {
						if (currentRelationship > 0) {
							this.setCustomerRelationship(supplierName, 0);
						}
						if (File.Exists(messageConversationPath)) {
							File.Delete(messageConversationPath);
						}
					}
				} catch {}
			} catch {}
		}
		/// <summary>Checks whether the specified property is owned.</summary>
		/// <param name="propertyName">The property name.</param>
		/// <returns>Whether the specified property is owned.</returns>
		internal bool isPropertyOwned(string propertyName) {
			try {
				var propertyPath = this.directoryPath + "Properties\\" + propertyName + "\\";
				var propertyJsonPath = propertyPath + "Property.json";
				if (File.Exists(propertyJsonPath)) {
					var json = JsonDocument.Parse(File.ReadAllText(propertyJsonPath));
					JsonElement? root = json != null ? json.RootElement : null;
					if (root != null && root.Value.TryGetProperty("IsOwned", out var owned)) {
						return owned.GetBoolean();
					}
				}
			} catch {}
			return false;
		}
		/// <summary>Sets whether the specified property is owned.</summary>
		/// <param name="propertyName">The property name.</param>
		/// <param name="owned">Whether to own the specified property. If false, the property will become disowned.</param>
		internal void setPropertyOwned(string propertyName, bool owned) {
			try {
				var propertyPath = this.directoryPath + "Properties\\" + propertyName + "\\";
				var propertyJsonPath = propertyName + "Property.json";
				var propertyJsonTemplatePath = FixActionDefinition.JsonRecoveryFilesPath + "\\Properties\\" + propertyName + "\\Property.json";
				var propertyJsonFileExists = File.Exists(propertyJsonPath);
				JsonNode? json = null;
				try {
					json = JsonNode.Parse(File.ReadAllText(propertyJsonFileExists ? propertyJsonPath : propertyJsonTemplatePath));
				} catch {
					if (propertyJsonFileExists) {
						json = JsonNode.Parse(File.ReadAllText(propertyJsonTemplatePath));
					}
				}
				if (json != null) {
					JsonNode root = json.Root;
					root["IsOwned"] = owned;
					File.WriteAllText(propertyJsonPath, json.ToJsonString());
				}
			} catch {}
		}
		/// <summary>Checks whether the specified business is owned.</summary>
		/// <param name="businessName">The business name.</param>
		/// <returns>Whether the specified business is owned.</returns>
		internal bool isBusinessOwned(string businessName) {
			try {
				var businessPath = this.directoryPath + "Businesses\\" + businessName + "\\";
				var businessJsonPath = businessPath + "Business.json";
				if (File.Exists(businessJsonPath)) {
					var json = JsonDocument.Parse(File.ReadAllText(businessJsonPath));
					JsonElement? root = json != null ? json.RootElement : null;
					if (root != null && root.Value.TryGetProperty("IsOwned", out var owned)) {
						return owned.GetBoolean();
					}
				}
			} catch {}
			return false;
		}
		/// <summary>Sets whether the specified business is owned.</summary>
		/// <param name="businessName">The business name.</param>
		/// <param name="owned">Whether to own the specified business. If false, the business will become disowned.</param>
		internal void setBusinessOwned(string businessName, bool owned) {
			try {
				var businessPath = this.directoryPath + "Businesses\\" + businessName + "\\";
				var businessJsonPath = businessPath + "Business.json";
				var businessJsonTemplatePath = FixActionDefinition.JsonRecoveryFilesPath + "\\NPCs_Templates\\Businesses\\" + businessName + "\\Business.json";
				var businessJsonFileExists = File.Exists(businessJsonPath);
				JsonNode? json = null;
				try {
					json = JsonNode.Parse(File.ReadAllText(businessJsonFileExists ? businessJsonPath : businessJsonTemplatePath));
				} catch {
					if (businessJsonFileExists) {
						json = JsonNode.Parse(File.ReadAllText(businessJsonTemplatePath));
					}
				}
				if (json != null) {
					JsonNode root = json.Root;
					root["IsOwned"] = owned;
					File.WriteAllText(businessJsonPath, json.ToJsonString());
				}
			} catch {}
		}
		/// <summary>Converts this save slot to a name.</summary>
		/// <returns>The name of this save slot.</returns>
		public override string ToString() {
			return "Steam ID " + this.steamId.ToString() + " -> Slot " + this.slotNumber.ToString();
		}
	}
}
