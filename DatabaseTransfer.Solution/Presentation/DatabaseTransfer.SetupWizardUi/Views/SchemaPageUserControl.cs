using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;
using DatabaseTransfer.SetupWizardUi.Schemas.Extensions;
using DatabaseTransfer.SetupWizardUi.Schemas.Models;
using DatabaseTransfer.SetupWizardUi.States;
using DevExpress.Data;
using DevExpress.Utils.Behaviors.Common;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class SchemaPageUserControl : BasePageUserControl
    {
        public SchemaPageUserControl(TableSchemaSelected tableSchemaSelected)
        {
            InitializeComponent();

            TableSchemaSelected = tableSchemaSelected;

            SetUserControlDataBindings();
            SetDataSources();
        }

        private ActianClient ActianClient => new ActianClient(UtilityState.ApplicationConfiguration.ActianConnection);

        private TransferScheduleConfiguration TransferScheduleConfiguration => UtilityState.TransferScheduleConfiguration;

        private TableSchemaSelected TableSchemaSelected { get; }

        private void SetUserControlDataBindings()
        {
            // TableNameGridLookUpEdit.DataBindings.Add("EditValue", TableSchemaSelected, nameof(TableSchemaSelected.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            SynchronousColumnGridLookUpEdit.DataBindings.Add("EditValue", TableSchemaSelected, nameof(TableSchemaSelected.SynchronousColumnSchema), true, DataSourceUpdateMode.OnPropertyChanged);
            SqlWhereClauseMemoEdit.DataBindings.Add("EditValue", TableSchemaSelected, nameof(TableSchemaSelected.SqlWhereClause), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetDataSources()
        {
            TableNameGridLookUpEdit.Enabled = false;
            TableNameGridLookUpEdit.Properties.DataSource = TableSchemaSelected;
            TableNameGridLookUpEdit.Properties.NullText = TableSchemaSelected.Name;
            TableNameGridLookUpEdit.Properties.DisplayMember = nameof(Schemas.Models.TableSchemaSelected.Name);
            TableNameGridLookUpEdit.Properties.PopupView.OptionsBehavior.AutoPopulateColumns = false;

            SynchronousColumnGridLookUpEdit.Enabled = TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental;
            SynchronousColumnGridLookUpEdit.Properties.NullText = TableSchemaSelected.SynchronousColumnSchema?.Name;
            SynchronousColumnGridLookUpEdit.Properties.DataSource = TableSchemaSelected.ColumnSchemas;
            SynchronousColumnGridLookUpEdit.Properties.DisplayMember = nameof(ColumnSchemaSelected.Name);
            SynchronousColumnGridLookUpEdit.Properties.PopupView.OptionsBehavior.AutoPopulateColumns = false;
            AddGridLookupEditColumn(SynchronousColumnGridLookUpEdit, nameof(ColumnSchemaSelected.Name), "Name", 0);
            AddGridLookupEditColumn(SynchronousColumnGridLookUpEdit, nameof(ColumnSchemaSelected.IsKey), "Key", 1);
            AddGridLookupEditColumn(SynchronousColumnGridLookUpEdit, nameof(ColumnSchemaSelected.DataType), "Data Type", 2);
            AddGridLookupEditColumn(SynchronousColumnGridLookUpEdit, nameof(ColumnSchemaSelected.MaskDataType), "Mask Data Type", 3);

            SynchronousColumnGridLookUpEdit.ShowPopup();
            SynchronousColumnGridLookUpEdit.ClosePopup();

            TableSchemaGridControl.DataSource = TableSchemaSelected.ColumnSchemas;

            SetTablePreviewDataSource();
        }

        private void AddGridLookupEditColumn(GridLookUpEdit gridLookUpEdit, string fieldName, string displayName, int index)
        {
            var gridColumn = gridLookUpEdit.Properties.PopupView.Columns.AddField(fieldName);
            gridColumn.VisibleIndex = index;
            gridColumn.Caption = displayName;
        }

        private void SynchronousColumnGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            var columnSchemaSelected = SynchronousColumnGridLookUpEdit.EditValue as ColumnSchemaSelected;
            SetSynchronousColumnSchema(columnSchemaSelected);
        }

        private void TableSchemaGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTablePreviewDataSource();
            SetColumnSchemasSelectedIsSelectedIsKey();
        }

        private void SetColumnSchemasSelectedIsSelectedIsKey()
        {
            foreach (var columnSchema in TableSchemaSelected.ColumnSchemas.Where(c => c.IsKey).ToList())
            {
                columnSchema.IsSelected = columnSchema.IsKey;
            }
        }

        private void TableSchemaGridViewDisabledCellEvents_ProcessingCell(object sender, ProcessCellEventArgs e)
        {
            var currentColumnSchema = (ColumnSchemaSelected)TableSchemaGridView.GetRow(e.RecordId);

            if (currentColumnSchema != null && currentColumnSchema.IsKey)
            {
                e.Disabled = true;
            }
        }

        private void TableSchemaGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var columnSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            UseMaskBarCheckItem.Checked = columnSelected.UseMask;
            UseColumnBarCheckItem.Checked = columnSelected.Name.Equals(TableSchemaSelected?.SynchronousColumnSchema?.Name, StringComparison.Ordinal);

            if (e.HitInfo.HitTest == GridHitTest.RowCell)
            {
                e.Allow = false;
                TableSchemaPopupMenu.ShowPopup(TableSchemaGridControl.PointToScreen(e.Point));
            }
        }

        private void UseColumnBarCheckItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var columnSchemaSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            SetSynchronousColumnSchema(columnSchemaSelected);
        }

        private void SetSynchronousColumnSchema(ColumnSchemaSelected columnSchemaSelected)
        {
            if (columnSchemaSelected == null)
            {
                return;
            }

            if (columnSchemaSelected.DataType == typeof(DateTime) || columnSchemaSelected.MaskDataType == typeof(DateTime))
            {
                columnSchemaSelected.IsSelected = true;
                columnSchemaSelected.UseMask = columnSchemaSelected.DataType != typeof(DateTime) && columnSchemaSelected.MaskDataType == typeof(DateTime);

                SynchronousColumnGridLookUpEdit.EditValue = columnSchemaSelected;
                TableSchemaGridControl.RefreshDataSource();
            }
            else
            {
                SynchronousColumnGridLookUpEdit.EditValue = null;
                XtraMessageBox.Show($"Column [{columnSchemaSelected.Name}] requires a DateTime Type.", "Date Comparison Field");
            }
        }

        private void UseMaskBarCheckItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var columnSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            if (columnSelected.HasMask && columnSelected.MaskDataType == typeof(DateTime))
            {
                columnSelected.IsSelected = true;
                columnSelected.UseMask = columnSelected.UseMask && columnSelected == TableSchemaSelected.SynchronousColumnSchema ? columnSelected.UseMask : UseMaskBarCheckItem.Checked;
                TableSchemaGridControl.RefreshDataSource();
            }
            else
            {
                XtraMessageBox.Show($"No available mask conversion for [{columnSelected.Name}].", "Column Mask");
            }
        }

        private void TableSchemaGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = (ColumnView)sender;

            if (!view.IsValidRowHandle(e.RowHandle))
            {
                return;
            }

            var columnSelected = TableSchemaGridView.GetRow(e.RowHandle) as ColumnSchemaSelected;

            if (columnSelected.UseMask)
            {
                e.Appearance.BackColor = Color.FromArgb(123, 184, 214);
                e.HighPriority = true;
            }

            if (columnSelected == TableSchemaSelected?.SynchronousColumnSchema)
            {
                e.Appearance.BackColor = Color.FromArgb(242, 165, 118);
                e.HighPriority = true;
            }
        }

        private void TablePreviewGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == GridHitTest.RowCell || e.HitInfo.HitTest == GridHitTest.EmptyRow)
            {
                e.Allow = false;
                TablePreviewPopupMenu.ShowPopup(TablePreviewGridControl.PointToScreen(e.Point));
            }
        }

        private void RefreshBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetTablePreviewDataSource();
        }

        private void SetTablePreviewDataSource()
        {
            var tableSchemaSelected = TableSchemaSelected.Copy();
            tableSchemaSelected.ColumnSchemas = TableSchemaSelected.ColumnSchemas.Where(c => c.IsSelected).OrderBy(c => c.Ordinal).ToList();

            TablePreviewGridView.Columns.Clear();
            TablePreviewGridControl.DataSource = null;

            if (tableSchemaSelected.ColumnSchemas.Count == 0)
            {
                return;
            }

            try
            {
                TablePreviewGridControl.DataSource = ActianClient.GetPreviewTableDataFromTableSchema(tableSchemaSelected.ToTableSchemaConfiguration());
            }
            catch
            {
                XtraMessageBox.Show("An error occurred while executing the SQL Statement.  Please check your where clause and try again.", "SQL Statement");
            }

            TablePreviewGridView.RefreshData();
            TablePreviewGridView.BestFitColumns();
        }
    }
}