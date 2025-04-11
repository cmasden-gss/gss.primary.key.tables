using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseTransfer.Application.Configurations;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;
using DatabaseTransfer.SetupWizardUi.Extensions;
using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class ScheduleBrowserPageUserControl : BasePageUserControl
    {
        public ScheduleBrowserPageUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private ApplicationConfiguration ApplicationConfiguration => UtilityState.ApplicationConfiguration;

        private async void Initialize()
        {
            while (GetScheduleBrowserPageViewModel() == null)
            {
                await Task.Delay(100);
            }

            SetUserControlDataBinds();
        }

        private void SetUserControlDataBinds()
        {
            ScheduleGridControl.DataSource = ApplicationConfiguration.TransferScheduleConfigurations;
            ScheduleGridView.BestFitColumns();
        }

        private ScheduleBrowserPageViewModel GetScheduleBrowserPageViewModel()
        {
            if (WizardViewModel == null)
            {
                return null;
            }

            return PageViewModel as ScheduleBrowserPageViewModel;
        }

        private void ScheduleGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.HitTest == GridHitTest.EmptyRow || e.HitInfo.HitTest == GridHitTest.RowCell)
            {
                e.Allow = false;

                if (ScheduleGridView.FocusedRowHandle < -1)
                {
                    EditScheduleBarButtonItem.Enabled = false;
                    DeleteScheduleBarButtonItem.Enabled = false;
                    SchemaBarButtonItem.Enabled = false;
                    ScheduleSingleTestSchemaBarButtonItem.Enabled = false;
                }
                else
                {
                    EditScheduleBarButtonItem.Enabled = true;
                    DeleteScheduleBarButtonItem.Enabled = true;
                    SchemaBarButtonItem.Enabled = true;
                    ScheduleSingleTestSchemaBarButtonItem.Enabled = true;
                }

                SchedulePopupMenu.ShowPopup(ScheduleGridControl.PointToScreen(e.Point));
            }
        }

        private void AddScheduleBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserControlExtensions.OpenUserControlWithXtraForm(new SchedulePageUserControl(), "New Schedule");

            ScheduleGridControl.RefreshDataSource();
            WizardViewModel.UpdateDocumentActions();
        }

        private void EditScheduleBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferScheduleConfiguration = ScheduleGridView.GetRow(ScheduleGridView.FocusedRowHandle) as TransferScheduleConfiguration;

            UserControlExtensions.OpenUserControlWithXtraForm(new SchedulePageUserControl(transferScheduleConfiguration), $"{transferScheduleConfiguration.Name} Routine");

            ScheduleGridControl.RefreshDataSource();
        }

        private void DeleteScheduleBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferScheduleConfiguration = ScheduleGridView.GetRow(ScheduleGridView.FocusedRowHandle) as TransferScheduleConfiguration;

            var deleteMessageBoxResult = XtraMessageBox.Show($"Remove {transferScheduleConfiguration.Name}?", "Remove Schedule", MessageBoxButtons.YesNo);

            if (deleteMessageBoxResult == DialogResult.Yes)
            {
                ApplicationConfiguration.TransferScheduleConfigurations.Remove(transferScheduleConfiguration);
                UtilityState.TransferScheduleConfiguration = ApplicationConfiguration.TransferScheduleConfigurations.FirstOrDefault();

                ScheduleGridControl.RefreshDataSource();
                WizardViewModel.UpdateDocumentActions();
            }
        }

        private void SchemaBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferScheduleConfiguration = ScheduleGridView.GetRow(ScheduleGridView.FocusedRowHandle) as TransferScheduleConfiguration;

            UtilityState.TransferScheduleConfiguration = transferScheduleConfiguration;

            WizardViewModel.CompleteView();
            StartupState.SchemaBrowserPageUserControl.SetDataForTransferScheduleConfiguration();
        }

        private void ScheduleSingleTestSchemaBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var transferScheduleConfiguration = ScheduleGridView.GetRow(ScheduleGridView.FocusedRowHandle) as TransferScheduleConfiguration;
            var copyTransferScheduleConfiguration = transferScheduleConfiguration.Copy();

            copyTransferScheduleConfiguration.TransferScheduleType = TransferScheduleTypes.Single;
            copyTransferScheduleConfiguration.TransferScheduleStartTime = DateTime.MinValue;

            copyTransferScheduleConfiguration.Name = $"Test Schema: {copyTransferScheduleConfiguration.Name}";

            ApplicationConfiguration.TransferScheduleConfigurations.Insert(ScheduleGridView.FocusedRowHandle + 1, copyTransferScheduleConfiguration);
            ScheduleGridControl.RefreshDataSource();
        }

        private void ScheduleGridView_DoubleClick(object sender, EventArgs e)
        {
            var transferScheduleConfiguration = ScheduleGridView.GetRow(ScheduleGridView.FocusedRowHandle) as TransferScheduleConfiguration;

            UtilityState.TransferScheduleConfiguration = transferScheduleConfiguration;

            WizardViewModel.CompleteView();
            StartupState.SchemaBrowserPageUserControl.SetDataForTransferScheduleConfiguration();
        }
    }
}