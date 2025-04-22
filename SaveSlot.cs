using System;
using System.Collections.Generic;
using System.Diagnostics;
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

	internal struct Region {
		internal string name;
		internal string[] customers;
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

		private ulong steamId;

		private uint slotNumber;
		/// <summary>The path to this save slot, including a trailing slash.</summary>
		private string directoryPath;

		private bool promptedForBackup;

		//
		// Construction Methods
		//

		internal SaveSlot(ulong steamId, uint slotNumber, string directoryPath) {
			this.steamId = steamId;
			this.slotNumber = slotNumber;
			this.directoryPath = directoryPath;
			this.promptedForBackup = false;
		}

		//
		// Save Slot Methods
		//

		internal string getDirectoryPath() {
			return this.directoryPath;
		}

		internal void resetPromptedForBackup() {
			this.promptedForBackup = false;
		}

		internal DialogResult promptToBackup() {
			if (this.promptedForBackup) {
				return DialogResult.No;
			} else {
				var result = MessageBox.Show("To reduce the risk of losing more progress from recovery, would you like to back up your save slot before we attempt recovery?",
					"Backup your save first?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				if (result != DialogResult.Cancel) {
					this.promptedForBackup = true;
				}
				return result;
			}
		}

		internal bool backup(out string errorMessage) {
			try {
				// TODO
				errorMessage = "TODO";
				return false;
			} catch (Exception ex) {
				errorMessage = ex.Message;
				return false;
			}
		}

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

		internal bool isSupplierUnlocked(string supplierName) {
			var customerPath = this.directoryPath + "NPCs\\" + supplierName + "\\";
			var messageConversationPath = customerPath + "MessageConversation.json";
			return this.getCustomerRelationship(supplierName) > 0 && File.Exists(messageConversationPath);
		}

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

		public override string ToString() {
			return "Steam ID " + this.steamId.ToString() + " -> Slot " + this.slotNumber.ToString();
		}
	}
}
