# Schema Manager

Schema Manager is a Blazor application built to let you view, manage, and audit key metadata about your database tables and fields. Its grid-based user interface allows administrators and users to efficiently mark key types, track changes in real time, and view a detailed history of modifications.

---

## TL;DR / Getting Started

1. **Prerequisites:**  
   - [.NET SDK](https://dotnet.microsoft.com/download) installed (compatible with your target framework)  
   - SQL Server or equivalent database with the necessary tables  
   - A configured `appsettings.json` with a valid connection string and admin users (e.g., `tmaynard`, `jdavis`)

2. **Clone & Configure:**  
   ```bash
   git clone https://your-repository-url.git
   cd your-repository-folder
   ```
   - Update your `appsettings.json` with:
     - Database connection strings.
     - Administrators list in `IdentityConfiguration:Administrators`.

3. **Build & Run:**  
   - Open the solution in Visual Studio (or your preferred IDE).  
   - Restore NuGet packages and build the project.  
   - Run the application and navigate to [http://10.10.10.5:8888/](http://10.10.10.5:8888/).  
   - Log in using your Windows credentials.

4. **Usage Highlights:**  
   - **Grid Display:** View all tables and their fields in a searchable grid.
   - **Key Management:** Use checkboxes to mark fields as “None”, “Master Key”, “Primary Key”, or “Foreign Key.” (Foreign keys reveal additional fields for “Foreign Key Table” and “Foreign Key Field”.)
   - **Change Tracking:** Audit history is automatically captured for every change.
   - **Modals for Save/Revert:** Confirm before final save or rollback.
   - **Role-Based Permissions:** Admins (hardcoded from configuration) can edit table/column names; non-admins can only adjust key-related fields.

---

## Detailed Explanation

### 1. Confirmation of Features as Implemented in Code

The source code confirms that each major requirement is implemented:

- **Grid Display & Key Management:**  
  - The code uses a **DevExpress DxGrid** to display all database tables and fields. Each row represents a field and includes columns such as *TableName*, *ColumnName*, and multiple boolean fields (`IsNone`, `IsMasterKey`, `IsPrimaryKey`, `IsForeignKey`).
  - Two additional columns, *ForeignKeyTable* and *ForeignKeyField*, are available when the `IsForeignKey` option is selected.
  - The grid supports search and filter functionality as configured by `ShowFilterRow` and `ShowSearchBox` properties.

- **Inline Editing and Change Tracking:**  
  - The grid is set to an inline editing mode (`EditMode="GridEditMode.EditCell"`).  
  - The event handler `Grid_EditModelSaving` captures both new rows and modifications. For modifications, it stores a deep copy of the original row (via `OriginalState = originalRow.Clone()`) in a dictionary (`UnsavedChanges`) along with a set of changed fields.
  - Audit history is implemented within the code. For every property change (from TableName to key flags and foreign key references), an audit entry is appended to the `AuditHistory` list. These changes are then rendered in a detail row (`<DetailRowTemplate>`) that uses another DxGrid.

- **Save Behavior & Modal Confirmation:**  
  - The code implements a bulk save mechanism: Users make multiple changes and then click the **Submit** button.  
  - Modal dialogs are provided for save confirmation (`ShowSaveModalFlag`) and for reverting unsaved changes (`ShowRevertModalFlag`). These modals prevent accidental submissions and enable users to cancel changes.
  - The method `ConfirmSave()` iterates through the unsaved changes, compares old and new values for defined properties, and logs audit entries accordingly before saving to the database.

- **Security & Access Control:**  
  - Authentication is enforced via the injected `IHttpContextAccessor`.  
  - The current Windows user is determined by splitting the Windows login name (`currentUser = ...Split('\\').Last().ToLower()`).
  - Admin privileges are checked by reading the configuration section `IdentityConfiguration:Administrators`, ensuring only those users can edit sensitive fields (the grid fields *TableName* and *ColumnName* are read-only for non-admins).

### 2. How the Code Confirms the Acceptance Criteria

- **Grid Display & Key Management:**  
  The grid displays:
  - All database fields in a searchable, filterable layout.
  - Checkboxes for key selections ("None", "Master Key", "Primary Key", "Foreign Key").
  - Conditional editing for foreign key additional fields.
  
- **Change Tracking & Audit History:**  
  - Changes are tracked in the `UnsavedChanges` dictionary and audit history is maintained as a list of audit entries for each field.
  - The detail row template allows users to expand a row to see full change history (date, who made the change, and descriptions of changes).

- **Save Behavior & Performance:**  
  - Batch save functionality is implemented with confirmation via modals.
  - The grid supports pagination (with set page sizes of 100, 250, or 500), and uses efficient Entity Framework Core queries with filtering (based on the `IsRemoved` flag) and sorting.

- **Security, Access Control & Admin Permissions:**  
  - Admin and non-admin roles are differentiated; editing rights are enforced through read-only settings and the injected configuration.
  
- **Administrative Functions:**  
  - Although delete functionality is commented out, the code supports marking rows as removed (`row.IsRemoved = true`) and reloading the grid.
  - Admin-specific functions such as adding a new row (`New_Click()`) and column chooser access (`ColumnChooserButton_Click()`) confirm the application supports further admin functions like editing and adding new data entries.

### 3. Alignment with the Provided Code

Every piece of the acceptance criteria is confirmed by the code:
- **Grid functionality:** Confirmed via `<DxGrid>` and its extensive configuration including toolbars, columns, and detail templates.
- **Change tracking:** Clearly managed through the `DataChange` record and the use of deep copies.
- **Modals and save/revert logic:** Implemented using conditional Razor markup (`@if (ShowSaveModalFlag)`) and bound methods.
- **Role-based permissions:** Established during initialization by comparing the current user against a hardcoded list from configuration.
  
The provided code is not “bullshit”—it directly implements each required behavior. All grid actions, change audits, and admin-specific features are coded explicitly.