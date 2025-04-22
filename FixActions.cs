using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule_I_Save_Fixer {
	//
	// Enumerations
	//
	/// <summary>Fix action criteria under which a fix action can be performed.</summary>
	enum FixActionCriteria {
		/// <summary>No fix action will be performed.</summary>
		Never,
		/// <summary>The fix action has no criteria so it will be performed.</summary>
		Always,
		/// <summary>The fix action has no criteria so it will be performed. But the action involves a critical file that may significantly set back progress.</summary>
		AlwaysCritical,
		/// <summary>The fix action will only be performed if the user requested deleting files as a viable solution to fixing the save.</summary>
		DeleteFile,
	}

	//
	// Classes
	//
	/// <summary>A single fix action that can be performed in an attempt to repair a save slot.</summary>
	internal class FixAction {
		//
		// Properties
		//
		/// <summary>The fix definition this fix action was suggested from.</summary>
		internal FixActionDefinition fixDefinition;
		/// <summary>The path to the save slot this fix action will fix something within.</summary>
		internal string saveSlotPath;
		/// <summary>The path to the directory to be fixed. Null if a specific file is to be fixed instead.</summary>
		internal string? directoryPath;
		/// <summary>The path to the file to be fixed. Null if a directory is to be fixed instead.</summary>
		internal string? filePath;
		/// <summary>The name of this fix action, as presented to the user.</summary>
		internal string fixAction;
		/// <summary>The fix action criteria under which this fix action can be performed.</summary>
		internal FixActionCriteria fixCriteria;
		/// <summary>The result of this fix action. Empty string if not yet attempted.</summary>
		internal string fixResult;
		/// <summary>Whether this fix action has been attempted.</summary>
		internal bool attemptedFix;

		//
		// Construction Methods
		//
		/// <summary>Constructs this class.</summary>
		/// <param name="fixDefinition">The fix definition this fix action was suggested from.</param>
		/// <param name="saveSlotPath">The path to the save slot this fix action will fix something within.</param>
		/// <param name="directoryPath">The path to the directory to be fixed. Null if a specific file is to be fixed instead.</param>
		/// <param name="filePath">The path to the file to be fixed. Null if a directory is to be fixed instead.</param>
		/// <param name="fixAction">The name of this fix action, as presented to the user.</param>
		/// <param name="fixCriteria">The fix action criteria under which this fix action can be performed.</param>
		internal FixAction(FixActionDefinition fixDefinition, string saveSlotPath, string? directoryPath, string? filePath, string fixAction, FixActionCriteria fixCriteria) {
			Debug.Assert(directoryPath != null || filePath != null);
			Debug.Assert((directoryPath != null) != (filePath != null));
			this.fixDefinition = fixDefinition;
			this.saveSlotPath = saveSlotPath;
			this.directoryPath = directoryPath;
			this.filePath = filePath;
			this.fixAction = fixAction;
			this.fixCriteria = fixCriteria;
			this.fixResult = "";
			this.attemptedFix = false;
		}

		//
		// Fix Action Methods
		//
		/// <summary>Attempt to apply this fix action.</summary>
		internal void fix() {
			if (this.attemptedFix) {
				return;
			}
			Debug.Assert(this.filePath != null); // XXX: For now, this.directoryPath is not supported.
			try {
				var directoryPath = Path.GetDirectoryName(this.filePath);
				if (directoryPath != null && !Directory.Exists(directoryPath)) {
					Directory.CreateDirectory(directoryPath);
				}
			} catch {}
			var result = this.fixDefinition.fix(this.saveSlotPath, this.filePath, out var errorMessage);
			if (result) {
				this.fixResult = "Complete";
			} else {
				this.fixResult = errorMessage;
			}
			this.attemptedFix = true;
		}
		/// <summary>Create a DataGridView that represents this fix action.</summary>
		/// <returns>The new DataGridView.</returns>
		internal DataGridViewRow createDgvRow() {
			Debug.Assert(this.filePath != null);
			var result = new DataGridViewRow();
			var checkboxCell = new DataGridViewCheckBoxCell();
			checkboxCell.Value = checkboxCell.TrueValue;
			result.Cells.Add(checkboxCell);
			var filePathCell = new DataGridViewTextBoxCell();
			filePathCell.Value = this.filePath.Substring(this.saveSlotPath.Length);
			result.Cells.Add(filePathCell);
			filePathCell.ReadOnly = true;
			var fixActionCell = new DataGridViewTextBoxCell();
			fixActionCell.Value = this.fixAction;
			result.Cells.Add(fixActionCell);
			fixActionCell.ReadOnly = true;
			var fixResultCell = new DataGridViewTextBoxCell();
			fixResultCell.Value = "";
			result.Cells.Add(fixResultCell);
			fixResultCell.ReadOnly = true;
			return result;
		}
		/// <summary>Update the associated DataGridView's result cell to reflect the result of attempting this fix action.</summary>
		/// <param name="row">The associated DataGridView row.</param>
		/// <param name="resultCellIndex">The index of the "Result" row.</param>
		internal void updateDgvRowResult(DataGridViewRow row, int resultCellIndex) {
			var fixResultCell = (DataGridViewTextBoxCell)row.Cells[resultCellIndex];
			fixResultCell.Value = this.fixResult;
		}
	}
	/// <summary>The base class of an implemented fix action.</summary>
	internal abstract class FixActionDefinition {
		//
		// Properties
		//
		/// <summary>The relative path to the directory that contains JSON recovery files.</summary>
		internal const string JsonRecoveryFilesPath = "Json Recovery Files";
		/// <summary>The name of this fix action definition.</summary>
		protected string name;
		/// <summary>
		/// The array of relative file path pattern to use when finding eligible files. Each element in this array is a fully unique pattern that is processed separately.
		/// Each component can have a pipe (|) to specify multiple eligible component string in that component position.
		/// Each component can have an asterisk (*) to specify a wildcard. In such a case, if no dot is present directories matching the pattern are matched, else files are.
		/// Note that for now, patterns cannot contain both a pipe and an asterisk.
		/// </summary>
		protected string[] relativeFilePathPatterns;

		//
		// Construction Methods
		//
		/// <summary>Constructs this class.</summary>
		/// <param name="name">The name of this fix action definition.</param>
		/// <param name="relativeFilePathPatterns">
		/// The array of relative file path pattern to use when finding eligible files. Each element in this array is a fully unique pattern that is processed separately.
		/// See this.relativeFilePathPatterns for more details.
		/// </param>
		internal FixActionDefinition(string name, string[] relativeFilePathPatterns) {
			this.name = name;
			this.relativeFilePathPatterns = relativeFilePathPatterns;
		}

		//
		// Fix Action Methods
		//
		/// <summary>Checks whether the specified JSON file exists and is valid.</summary>
		/// <param name="filePath">The path to the JSON file.</param>
		/// <returns>Whether the specified JSON file exists and is valid.</returns>
		protected static bool IsValidJson(string filePath) {
			try {
				return File.Exists(filePath) && (new System.IO.FileInfo(filePath)).Length > 0;
			} catch {
				return false;
			}
		}
		/// <summary>Checks whether the specified JSON file should be fixed by replacing it with a template file.</summary>
		/// <param name="filePath">The path to the JSON file.</param>
		/// <param name="fixAction">The variable that will receive the suggested fix action.</param>
		/// <returns>Whether the specified JSON file should be fixed by replacing it with a template file.</returns>
		protected static bool ShouldFixFromTemplate(string filePath, out string fixAction) {
			var parentDirectoryPath = Path.GetDirectoryName(filePath);
			if (Directory.Exists(parentDirectoryPath) && !FixActionDefinition.IsValidJson(filePath)) {
				fixAction = "Recover From Template";
				return true;
			}
			fixAction = "";
			return false;
		}
		/// <summary>Attempts to recover a JSON file from its corresponding template file.</summary>
		/// <param name="saveSlotPath">The path to the save slot directory, including a trailing slash.</param>
		/// <param name="filePath">The path to the file to recover.</param>
		/// <param name="errorMessage">The variable that will receive an error message if recovery failed.</param>
		/// <returns>Whether recovery succeeded.</returns>
		protected static bool RecoverFromTemplate(string saveSlotPath, string filePath, out string errorMessage) {
			var relativeFilePath = filePath.Substring(saveSlotPath.Length);
			var recoverFromFilePath = FixActionDefinition.JsonRecoveryFilesPath + "\\" + relativeFilePath;
			if (!File.Exists(recoverFromFilePath)) {
				errorMessage = "Missing Recovery File";
				return false;
			}
			try {
				File.Copy(recoverFromFilePath, filePath, true);
				errorMessage = "";
				return true;
			} catch (Exception ex) {
				errorMessage = ex.Message;
				return false;
			}
		}
		/// <summary>Recursively finds potential file paths that may need fixing.</summary>
		/// <param name="result">The list of file paths that may need fixing. This is appended to.</param>
		/// <param name="pathComponentsStack">The stack of path components at this recursion level.</param>
		/// <param name="patternComponentIndex">The index within patternComponents that corresponds to this recursion level. This increments with each recursion.</param>
		/// <param name="patternComponents">The components in this.relativeFilePathPatterns, created by splitting this.relativeFilePathPatterns by backslash.</param>
		private void recursePotentialFilePaths(List<string> result, List<String> pathComponentsStack, int patternComponentIndex, string[] patternComponents) {
			var nextPatternComponent = patternComponents[patternComponentIndex];
			if (nextPatternComponent.Contains('*')) {
				var pathThusFar = string.Join('\\', pathComponentsStack);
				try {
					if (nextPatternComponent.Contains('.')) {
						foreach (var subdirectoryPath in Directory.EnumerateFiles(pathThusFar, nextPatternComponent)) {
							var fileName = Path.GetFileName(subdirectoryPath);
							if (fileName != null && fileName.EndsWith(".json")) {
								pathComponentsStack.Add(fileName);
								result.Add(string.Join('\\', pathComponentsStack));
								pathComponentsStack.RemoveAt(pathComponentsStack.Count - 1);
							}
						}
					} else {
						// TODO: Also enumerate from FixActionBase::JsonRecoveryFilesPath + "\\" + ...
						foreach (var subdirectoryPath in Directory.EnumerateDirectories(pathThusFar, nextPatternComponent)) {
							var subdirectoryName = subdirectoryPath.Substring(subdirectoryPath.LastIndexOf('\\') + 1);
							if (subdirectoryName != null) {
								subdirectoryName = subdirectoryName.Substring(subdirectoryName.TrimEnd('\\').LastIndexOf('\\') + 1);
								pathComponentsStack.Add(subdirectoryName);
								this.recursePotentialFilePaths(result, pathComponentsStack, patternComponentIndex + 1, patternComponents);
								pathComponentsStack.RemoveAt(pathComponentsStack.Count - 1);
							}
						}
					}
				} catch {}
			} else if (nextPatternComponent.Contains('|')) {
				var subcomponents = nextPatternComponent.Split('|');
				foreach (var subcomponent in subcomponents) {
					pathComponentsStack.Add(subcomponent);
					if (subcomponent.EndsWith(".json")) {
						result.Add(string.Join('\\', pathComponentsStack));
					} else if (patternComponents.Length > patternComponentIndex + 1) {
						this.recursePotentialFilePaths(result, pathComponentsStack, patternComponentIndex + 1, patternComponents);
					}
					pathComponentsStack.RemoveAt(pathComponentsStack.Count - 1);
				}
			} else {
				pathComponentsStack.Add(nextPatternComponent);
				if (nextPatternComponent.EndsWith(".json")) {
					result.Add(string.Join('\\', pathComponentsStack));
				} else if (patternComponents.Length > patternComponentIndex + 1) {
					this.recursePotentialFilePaths(result, pathComponentsStack, patternComponentIndex + 1, patternComponents);
				}
				pathComponentsStack.RemoveAt(pathComponentsStack.Count - 1);
			}
		}
		/// <summary>Gets all potential file paths that may need fixing given this.relativeFilePathPatterns.</summary>
		/// <param name="saveSlotPath">The path to the save slot directory, including a trailing slash.</param>
		/// <returns>All potential file paths that may need fixing.</returns>
		internal virtual string[] getPotentialFilePaths(string saveSlotPath) {
			var result = new List<string>();
			var pathComponentsStack = new List<String>();
			pathComponentsStack.Add(saveSlotPath.TrimEnd('\\'));
			foreach (var filePathPattern in this.relativeFilePathPatterns) {
				var patternComponents = filePathPattern.Split('\\');
				this.recursePotentialFilePaths(result, pathComponentsStack, 0, patternComponents);
			}
			return result.ToArray();
		}
		/// <summary>Checks whether a fix may be needed.</summary>
		/// <param name="filePath">The path to the file to test.</param>
		/// <param name="fixAction">The variable that will receive the name of the suggested fix action.</param>
		/// <param name="criteria">The variable that will receive the fix action criteria.</param>
		/// <returns>Whether a fix may be needed.</returns>
		internal abstract bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria);
		/// <summary>Attempt to perform a fix action. This should not be called if the associated fix action was already attempted.</summary>
		/// <param name="saveSlotPath">The path to the save slot directory, including a trailing slash.</param>
		/// <param name="filePath">The path to the file to fix.</param>
		/// <param name="errorMessage">The variable that will receive an error message if fixing failed.</param>
		/// <returns>Whether fixing succeeded.</returns>
		internal abstract bool fix(string saveSlotPath, string filePath, out string errorMessage);

		//
		// Classes
		//

		internal class InvalidJson : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal InvalidJson() : base("Delete Invalid Json File", new string[] { "*.json", "*\\*.json", "*\\*\\*.json", "*\\*\\*\\*.json", "*\\*\\*\\*\\*.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				try {
					// TODO: Consider instead doing a JSON parsable check, but that is more costly...
					if (!FixActionDefinition.IsValidJson(filePath)) {
						fixAction = "Delete File";
						criteria = FixActionCriteria.DeleteFile;
						return true;
					}
				} catch {}
				fixAction = "";
				criteria = FixActionCriteria.Never;
				return false;
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				try {
					File.Delete(filePath);
					errorMessage = "";
					return true;
				} catch (Exception ex) {
					errorMessage = ex.Message;
					return false;
				}
			}
		}

		internal class MissingCritical : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingCritical() : base("Recover Critical", new string[] { "Money.json|Rank.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.AlwaysCritical;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingGame : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingGame() : base("Recover Game", new string[] { "Game.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingLaw : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingLaw() : base("Recover Law", new string[] { "Law.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingTime : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingTime() : base("Recover Time", new string[] { "Time.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingPlayer : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingPlayer() : base("Recover Player 1", new string[] { "Players\\Player_0\\Player.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingBusinessLaunderingStation : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingBusinessLaunderingStation() : base("Recover Business Laundering Station", new string[] { "Businesses\\*\\launderingstation_*\\Data.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingNpcCustomerData : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingNpcCustomerData() : base("Recover Customer Data", new string[] { "NPCs\\*\\CustomerData.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingNpcNpc : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingNpcNpc() : base("Recover Customer NPC", new string[] { "NPCs\\*\\NPC.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingNpcRelationship : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingNpcRelationship() : base("Recover Customer Relationship", new string[] { "NPCs\\*\\Relationship.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}

		internal class MissingNpcSupplierMessages : FixActionDefinition {
			//
			// Construction Methods
			//
		
			internal MissingNpcSupplierMessages(string[] supplierNames) : base("Recover Supplier Messages", new string[] { "NPCs\\" + string.Join('|', supplierNames) + "\\MessageConversation.json" }) {}

			//
			// Fix Action methods
			//
			/// <inheritdoc />
			internal override bool shouldFix(string filePath, out string fixAction, out FixActionCriteria criteria) {
				criteria = FixActionCriteria.Always;
				// TODO: Check the corresponding Variables\\?.json file to determine if the supplier should be unlocked
				return FixActionDefinition.ShouldFixFromTemplate(filePath, out fixAction);
			}
			/// <inheritdoc />
			internal override bool fix(string saveSlotPath, string filePath, out string errorMessage) {
				return FixActionDefinition.RecoverFromTemplate(saveSlotPath, filePath, out errorMessage);
			}
		}
	}
}
