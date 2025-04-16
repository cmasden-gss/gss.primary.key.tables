# Schema Manager: End User Guide

Schema Manager is a web-based tool designed to help you view, manage, and audit the key metadata for your database tables and fields. It presents everything in a user-friendly grid interface so you can easily update key designations, review changes, and maintain accurate database records.

---

## TL;DR / Getting Started

1. **Access the Application:**  
   Open your browser and navigate to [http://10.10.10.5:8888/](http://10.10.10.5:8888/).

2. **Log In:**  
   Use your Windows username and password. The system automatically detects your login to determine your access level:
   - **Admins (e.g., tmaynard, jdavis):** Can update all fields.
   - **Non-admin users:** Can change key-related fields only.

3. **How to Use the Grid:**
   - **Search & Filter:** Easily find any table or field with the search box.
   - **View Key Status:** Each row shows a table’s field along with checkboxes to mark it as:
     - None
     - Master Key
     - Primary Key
     - Foreign Key  
       (If you mark a field as “Foreign Key,” additional fields appear to enter the related table and field names.)
   - **Audit History:** Click the down arrow on a row to reveal the history of changes (who, what, and when).

4. **Saving Changes:**
   - **Submit Button:** After making changes, click **Submit** (a confirmation modal will appear) to record your updates.
   - **Revert Button:** If you want to cancel your changes before saving, click **Revert**.

5. **Additional Tools:**
   - **New:** Add a new table/field record (available for admins).
   - **Column Chooser:** Customize the columns visible in the grid.
   - **Removed Toggle:** Show or hide records marked as removed.

---

## Detailed Usage Guide

### 1. Viewing and Managing the Schema

- **Grid Overview:**  
  The main screen displays all your database tables and fields in one searchable grid. Each row represents a database field and shows:
  - **Table Name & Column Name:** Identifies the source of the data.
  - **Key Checkboxes:** Quickly identify and change the key type (None, Master Key, Primary Key, Foreign Key).

- **Foreign Key Fields:**  
  When you mark a field as a Foreign Key, two new input fields appear:
  - **Foreign Key Table**
  - **Foreign Key Field**  
  This helps in maintaining relationships between database tables.

### 2. Tracking Changes and Auditing

- **Change Tracking:**  
  Any changes you make (whether adding a new row or modifying an existing one) are recorded. This includes:
  - Who made the change.
  - What fields were altered.
  - When the change was made.

- **Audit History:**  
  For each field, you can expand the row to view a detailed history. This history includes timestamps, usernames, and descriptions of what was modified.

### 3. Saving or Reverting Changes

- **Submit Changes:**  
  Once you’ve made all the desired changes:
  - Click the **Submit** button in the toolbar.  
  - A confirmation modal will appear asking you to confirm your save.
  - Once confirmed, your changes and the corresponding audit entries are saved to the database.

- **Revert Changes:**  
  If you need to cancel any unsaved modifications:
  - Click **Revert**.
  - A confirmation modal will appear to let you confirm the reset.  
  - Unsaved changes are discarded, and the grid reloads the original data.

### 4. Access Control and User Roles

- **Role-Based Editing:**  
  - **Administrators:**  
    - Can edit all fields including table and column names.
    - Have access to extra functionalities like adding new tables or modifying existing entries.
  - **Non-admin Users:**  
    - Can view all schema data.
    - Are limited to modifying key assignments (checkboxes and foreign key references) only.

- **How Your Access is Determined:**  
  The system uses your Windows username to check your permissions against a pre-configured list.

### 5. Extra Features for Enhanced Usability

- **Toolbar Functions:**  
  - **New:** Starts a new row entry for adding a record.
  - **Column Chooser:** Lets you customize which columns you see.
  - **Removed Toggle:** Allows you to view or hide records marked as removed (soft deletion).

- **Responsive and Intuitive Interface:**  
  The grid supports filtering, sorting, and inline editing. It’s designed to handle large sets of data smoothly, ensuring you can quickly find and update records.

---

## How It Works Behind the Scenes (Briefly)

While you don’t need to worry about the technical details, know that:
- **Grid and Edit Modes:** The application uses a dynamic grid that allows inline editing.
- **Auto-Audit:** All changes are captured and stored with audit details to ensure complete traceability.
- **Performance:** The grid supports pagination and search, keeping it responsive even with many records.
- **Security:** Your Windows credentials determine whether you can make full updates or only key-related changes.
