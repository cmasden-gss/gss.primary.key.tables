using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;
using DatabaseTransfer.Application.Schemas.Extensions;
using DatabaseTransfer.Application.TableRelations.Client;
using DatabaseTransfer.Application.TableRelations.Models;
using DatabaseTransfer.SetupWizardUi.Extensions;
using DatabaseTransfer.SetupWizardUi.Schemas.Extensions;
using DatabaseTransfer.SetupWizardUi.Schemas.Models;
using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels;
using DevExpress.Data;
using DevExpress.Utils.Behaviors.Common;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class SchemaBrowserPageUserControl : BasePageUserControl
    {
        public SchemaBrowserPageUserControl()
        {
            InitializeComponent();

            Initialize();
        }

        private ActianClient ActianClient { get; set; }

        private TransferScheduleConfiguration TransferScheduleConfiguration => UtilityState.TransferScheduleConfiguration;

        private List<SqlTable> TableRelationList { get; set; }

        private List<TableSchemaSelected> TableSchemaSelectedList { get; set; }

        private TableSchemaSelected TableSchemaSelected { get; set; }

        private bool IsValidatingTableSchema { get; set; }

        private async void Initialize()
        {
            LoadTableRelationClientTableRelations();

            while (GetSchemaPageViewModel() == null)
            {
                await Task.Delay(100);
            }

            CorrectTableNameDockPanel_Hack();
            SetDataForTransferScheduleConfiguration();
        }

        internal void SetDataForTransferScheduleConfiguration()
        {
            ActianClient = new ActianClient(UtilityState.ApplicationConfiguration.ActianConnection);

            SetTableNameDataSource();
            SetStatusOnTransferSynchronousType();

            TableSchemaSelected = TableSchemaSelectedList.FirstOrDefault();
            SetCurrentTableSchemaDataSource();

            SchemaPageViewModelCanNextView();
        }

        private void SetStatusOnTransferSynchronousType()
        {
            if (TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental)
            {
                StatusLabelControl.Text = "Select your table schemas and your date comparison field to continue.";
                UseColumnBarCheckItem.Enabled = true;
            }
            else
            {
                StatusLabelControl.Text = "Select your table schemas to continue.";
                UseColumnBarCheckItem.Enabled = false;
            }
        }

        private async void CorrectTableNameDockPanel_Hack()
        {
            while (TableNameDockPanel.Width != 350)
            {
                TableNameDockPanel.Width = 350;
                await Task.Delay(100);
            }
        }

        private void LoadTableRelationClientTableRelations()
        {
            var tableRelationClient = new TableRelationClient();

            TableRelationList = tableRelationClient.GetTableRelations();
        }

        private List<string> GetTableAndTableViewNames()
        {
            var tableNameList = new List<string>();

            tableNameList.AddRange(ActianClient.GetTableNames());
            tableNameList.AddRange(ActianClient.GetTableViewNames());

            return tableNameList;
        }

        private void SetTableNameDataSource()
        {
            TableSchemaSelectedList = new List<TableSchemaSelected>();

            foreach (var tableSchemaSelected in TransferScheduleConfiguration.TableSchemas.Select(tableSchema => tableSchema.ToTableSchemaSelected()))
            {
                tableSchemaSelected.IsSelected = true;

                SelectColumnSchemasIsSelected(tableSchemaSelected);
                SelectColumnSchemasUseMask(tableSchemaSelected);

                TableSchemaSelectedList.Add(tableSchemaSelected);
            }

            var tableNameList = GetTableAndTableViewNames();
            tableNameList.RemoveAll(c => TableSchemaSelectedList.Exists(e => e.Name.Equals(c, StringComparison.OrdinalIgnoreCase)));

            foreach (var tableName in tableNameList)
            {
                TableSchemaSelectedList.Add(new TableSchemaSelected(tableName) { IsSelected = false });
            }

            TableNameGridControl.DataSource = TableSchemaSelectedList;
            TableNameGridView.BestFitColumns();
        }

        private void GetTableRelations()
        {
            var tableRelation = TableRelationList.SingleOrDefault(c => c.Name.Equals(TableSchemaSelected.Name, StringComparison.OrdinalIgnoreCase));

            if (tableRelation == null)
            {
                return;
            }

            foreach (var columnSchema in TableSchemaSelected.ColumnSchemas)
            {
                var columnRelation = tableRelation.SqlColumns.SingleOrDefault(c => c.Name.Equals(columnSchema.Name, StringComparison.OrdinalIgnoreCase));

                if (columnRelation == null)
                {
                    continue;
                }

                if (columnRelation.SqlColumnMask != null)
                {
                    columnSchema.MaskDataType = columnRelation.SqlColumnMask.DataType;
                    columnSchema.MaskFormat = columnRelation.SqlColumnMask.Format;
                }

                columnSchema.ForeignKeyTableSchemas.Clear();

                foreach (var foreignTableSchema in columnRelation.ForeignKeyTableNames.Select(tableName => TableSchemaSelectedList.SingleOrDefault(c => c.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase)))
                    .Where(foreignTableSchema => foreignTableSchema != null))
                {
                    columnSchema.ForeignKeyTableSchemas.Add(foreignTableSchema);
                }
            }
        }

        private void TableNameGridView_Click(object sender, EventArgs e)
        {
            if (IsValidatingTableSchema)
            {
                return;
            }

            SetCurrentTableSchemaDataSource();
        }

        private async void TableNameGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsValidatingTableSchema)
            {
                return;
            }

            SetCurrentTableSchemaDataSource();
            await SaveAndValidateTableSchemaSelectedListToConfiguration();
        }

        private bool CurrentTableSchemaExists()
        {
            if (ActianClient.TableExists(TableSchemaSelected.Name))
            {
                return true;
            }

            var removeTableSchemaMessageBoxResult = XtraMessageBox.Show($"Do you wish to remove [{TableSchemaSelected.Name}]?", "Table does not exist", MessageBoxButtons.YesNo);

            if (removeTableSchemaMessageBoxResult == DialogResult.Yes)
            {
                TableSchemaSelectedList.Remove(TableSchemaSelected);

                TableNameGridControl.RefreshDataSource();
            }

            return false;
        }

        private void SetCurrentTableSchemaDataSource()
        {
            TableSchemaSelected = (TableSchemaSelected)TableNameGridView.GetRow(TableNameGridView.FocusedRowHandle);

            if (!CurrentTableSchemaExists())
            {
                TableSchemaDockPanel.Text = "[Table] Schema";
                return;
            }

            TableSchemaDockPanel.Text = $"[{TableSchemaSelected.Name}] Schema";

            var currentTableSchema = ActianClient.GetTableSchema(TableSchemaSelected.Name);
            var currentTableSchemaSelected = currentTableSchema.ToTableSchemaSelected();

            if (TableSchemaSelected.ColumnSchemas.Count > 0)
            {
                if (!TableSchemaSelected.ToActianSqlTableSchema().IsColumnSchemasEqual(currentTableSchema))
                {
                    XtraMessageBox.Show("The schema selection has been reset.", "Table schema has changed");

                    TableSchemaSelected.ColumnSchemas = currentTableSchema.ToTableSchemaSelected().ColumnSchemas;
                    SelectColumnSchemasIsIndexedColumn(TableSchemaSelected);
                }
                else
                {
                    foreach (var columnSchema in TableSchemaSelected.ColumnSchemas)
                    {
                        currentTableSchemaSelected.ColumnSchemas.RemoveAll(c => c.Name == columnSchema.Name);
                    }

                    foreach (var columnSchema in currentTableSchemaSelected.ColumnSchemas)
                    {
                        TableSchemaSelected.ColumnSchemas.Add(columnSchema);
                    }
                }
            }
            else
            {
                // this is a bug that i didn't feel like fixing /sob
                TableSchemaSelected.ColumnSchemas = currentTableSchema.ToTableSchemaSelected().ColumnSchemas;
                TableSchemaSelected.TableIndices = currentTableSchema.TableIndices;

                SelectColumnSchemasIsIndexedColumn(TableSchemaSelected);
            }

            GetTableRelations();

            TableSchemaSelected.SynchronousColumnSchema = TableSchemaSelected.ColumnSchemas.SingleOrDefault(c => c.Name.Equals(TableSchemaSelected.SynchronousColumnSchema?.Name, StringComparison.OrdinalIgnoreCase));

            TableSchemaGridControl.DataBindings.Clear();
            TableSchemaGridControl.DataBindings.Add("DataSource", TableSchemaSelected, nameof(TableSchemaSelected.ColumnSchemas), true, DataSourceUpdateMode.OnPropertyChanged);

            TableSchemaGridControl.RefreshDataSource();
            TableSchemaGridView.BestFitColumns();
        }

        private void TableSchemaGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetColumnSchemasSelectedIsSelectedIsKey();
            SaveTableSchemaSelectedListToConfiguration();
            SchemaPageViewModelCanNextView();
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

        private void SaveTableSchemaSelectedListToConfiguration()
        {
            var tableSchemaSelectedList = new List<TableSchemaSelected>();

            foreach (var tableSchema in TableSchemaSelectedList.Where(c => c.IsSelected).ToList())
            {
                var tableSchemaSelected = tableSchema.Copy();

                tableSchemaSelected.SynchronousColumnSchema = tableSchemaSelected.ColumnSchemas.Exists(c => c.Name == tableSchemaSelected.SynchronousColumnSchema?.Name && c.IsSelected) ? tableSchemaSelected.SynchronousColumnSchema : null;

                foreach (var columnSchema in tableSchemaSelected.ColumnSchemas.Where(c => !c.UseMask && c.IsSelected))
                {
                    columnSchema.MaskFormat = string.Empty;
                    columnSchema.MaskDataType = null;
                }

                tableSchemaSelected.ColumnSchemas = tableSchemaSelected.ColumnSchemas.Where(c => c.IsSelected).OrderBy(c => c.Ordinal).ToList();

                tableSchemaSelectedList.Add(tableSchemaSelected);
            }

            TransferScheduleConfiguration.TableSchemas = tableSchemaSelectedList.ToTableSchemaConfigurationList();
        }

        private async Task SaveAndValidateTableSchemaSelectedListToConfiguration()
        {
            SetUiForValidatingTableSchemaBusy();

            await Task.Run(ValidateTableSchemaSelectedList);

            SaveTableSchemaSelectedListToConfiguration();

            SetUiForValidatingTableSchemaComplete();
        }

        private void ValidateTableSchemaSelectedList()
        {
            for (var tableSchemaIndex = TableSchemaSelectedList.Count - 1; tableSchemaIndex >= 0; tableSchemaIndex--)
            {
                if (TableSchemaSelectedList[tableSchemaIndex].ColumnSchemas.Count != 0 || !TableSchemaSelectedList[tableSchemaIndex].IsSelected)
                {
                    continue;
                }

                if (!ActianClient.TableExists(TableSchemaSelectedList[tableSchemaIndex].Name))
                {
                    TableSchemaSelectedList.RemoveAt(tableSchemaIndex);
                    continue;
                }

                try
                {
                    var tableSchemaSelected = ActianClient.GetTableSchema(TableSchemaSelectedList[tableSchemaIndex].Name).ToTableSchemaSelected();
                    tableSchemaSelected.IsSelected = true;

                    SelectColumnSchemasIsIndexedColumn(tableSchemaSelected);

                    TableSchemaSelectedList[tableSchemaIndex] = tableSchemaSelected;
                }
                catch
                {
                    TableSchemaSelectedList.RemoveAt(tableSchemaIndex);
                }
            }
        }

        private void TableSchemaGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var columnSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            if (columnSelected == null)
            {
                return;
            }

            UseMaskBarCheckItem.Checked = columnSelected.UseMask;
            UseColumnBarCheckItem.Checked = columnSelected.Name.Equals(TableSchemaSelected?.SynchronousColumnSchema?.Name, StringComparison.Ordinal);

            if (e.HitInfo.HitTest == GridHitTest.RowCell)
            {
                e.Allow = false;
                TableSchemaPopupMenu.ShowPopup(TableSchemaGridControl.PointToScreen(e.Point));
            }
        }

        private void UseColumnBarCheckItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            var columnSchemaSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            if (columnSchemaSelected.DataType == typeof(DateTime) || columnSchemaSelected.MaskDataType == typeof(DateTime))
            {
                columnSchemaSelected.IsSelected = true;
                columnSchemaSelected.UseMask = columnSchemaSelected.DataType != typeof(DateTime) && columnSchemaSelected.MaskDataType == typeof(DateTime);
                TableSchemaSelected.SynchronousColumnSchema = columnSchemaSelected;

                TableSchemaGridControl.RefreshDataSource();
                SaveTableSchemaSelectedListToConfiguration();
                SchemaPageViewModelCanNextView();
            }
            else
            {
                XtraMessageBox.Show($"Column [{columnSchemaSelected.Name}] requires a DateTime Type.", "Date Comparison Field");
            }
        }

        private void UseMaskBarCheckItem_ItemClicked(object sender, ItemClickEventArgs e)
        {
            var columnSelected = TableSchemaGridView.GetRow(TableSchemaGridView.FocusedRowHandle) as ColumnSchemaSelected;

            if (columnSelected.HasMask && columnSelected.MaskDataType == typeof(DateTime))
            {
                columnSelected.IsSelected = true;
                columnSelected.UseMask = columnSelected.UseMask && columnSelected == TableSchemaSelected.SynchronousColumnSchema ? columnSelected.UseMask : UseMaskBarCheckItem.Checked;

                TableSchemaGridControl.RefreshDataSource();
                SaveTableSchemaSelectedListToConfiguration();
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

        private void TableNameGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == GridHitTest.RowCell)
            {
                e.Allow = false;
                TableNamePopupMenu.ShowPopup(TableNameGridControl.PointToScreen(e.Point));
            }
        }

        private void DetailsBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCurrentTableSchemaDataSource();

            var tableSelected = TableNameGridView.GetRow(TableNameGridView.FocusedRowHandle) as TableSchemaSelected;

            UserControlExtensions.OpenUserControlWithXtraForm(new SchemaPageUserControl(tableSelected), $"{tableSelected.Name} Details");

            SaveTableSchemaSelectedListToConfiguration();

            TableNameGridControl.RefreshDataSource();
            TableSchemaGridControl.RefreshDataSource();
        }

        private void TableSchemaGridControl_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            var masterDetailGridView = e.View as GridView;

            masterDetailGridView.Columns.ForEach(column =>
            {
                if (!column.FieldName.Equals("Name"))
                {
                    column.Visible = false;
                }

                column.OptionsColumn.AllowEdit = false;
            });

            //masterDetailGridView.Columns["IsSelected"].Visible = false;
            masterDetailGridView.OptionsDetail.EnableMasterViewMode = false;

            masterDetailGridView.SelectionChanged += MasterDetail_SelectionChanged;
        }

        private void MasterDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveTableSchemaSelectedListToConfiguration();
            SchemaPageViewModelCanNextView();

            TableNameGridControl.RefreshDataSource();
        }

        private void SetUiForValidatingTableSchemaBusy()
        {
            IsValidatingTableSchema = true;
            TableSchemaGridView.LoadingPanelVisible = true;

            var model = GetSchemaPageViewModel();

            if (model == null)
            {
                return;
            }

            model.CanPreviousView = false;
            model.CanNextView = false;
            WizardViewModel.UpdateDocumentActions();
        }

        private void SetUiForValidatingTableSchemaComplete()
        {
            IsValidatingTableSchema = false;
            TableSchemaGridView.LoadingPanelVisible = false;
            SchemaPageViewModelCanNextView();
        }

        private void SchemaPageViewModelCanNextView()
        {
            var model = GetSchemaPageViewModel();

            if (model == null)
            {
                return;
            }

            model.CanPreviousView = true;

            if (TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental)
            {
                model.CanNextView = TransferScheduleConfiguration.TableSchemas.Count > 0 && TransferScheduleConfiguration.TableSchemas.All(c => c.HasSynchronousColumnSchema);
            }
            else
            {
                model.CanNextView = TransferScheduleConfiguration.TableSchemas.Count > 0;
            }

            WizardViewModel.UpdateDocumentActions();
        }

        private SchemaBrowserPageViewModel GetSchemaPageViewModel()
        {
            if (WizardViewModel == null)
            {
                return null;
            }

            return PageViewModel as SchemaBrowserPageViewModel;
        }

        /// <summary>
        /// Sets keys & index columns IsSelected = True
        /// </summary>
        /// <param name="tableSchema"></param>
        private void SelectColumnSchemasIsIndexedColumn(TableSchemaSelected tableSchema)
        {
            tableSchema.ColumnSchemas.Where(c => c.IsKey).Select(c =>
            {
                c.IsSelected = true;
                return c;
            }).ToList();

            var indexedColumns = tableSchema.TableIndices.Select(c => c.ColumnIndices);
            tableSchema.ColumnSchemas.Where(c => indexedColumns.Any(l => l.Any(d => d.Name.Equals(c.Name)))).Select(c =>
            {
                c.IsSelected = true;
                return c;
            }).ToList();
        }

        /// <summary>
        /// Sets selected columns IsSelected = True
        /// </summary>
        /// <param name="tableSchema"></param>
        private void SelectColumnSchemasIsSelected(TableSchemaSelected tableSchema)
        {
            tableSchema.ColumnSchemas.Select(c =>
            {
                c.IsSelected = true;
                return c;
            }).ToList();
        }

        /// <summary>
        /// Sets columns with masks UseMask = True
        /// </summary>
        /// <param name="tableSchema"></param>
        private void SelectColumnSchemasUseMask(TableSchemaSelected tableSchema)
        {
            tableSchema.ColumnSchemas.Select(c =>
            {
                c.UseMask = c.MaskDataType != null;
                return c;
            }).ToList();
        }
    }
}