using System;
using System.ComponentModel;
using System.Windows.Forms;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.SetupWizardUi.States;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class SchedulePageUserControl : BasePageUserControl
    {
        public SchedulePageUserControl()
        {
            InitializeComponent();

            IsEditMode = false;
            TransferScheduleConfiguration = new TransferScheduleConfiguration();

            SetUserControlDataBinds();
        }

        public SchedulePageUserControl(TransferScheduleConfiguration transferScheduleConfiguration)
        {
            InitializeComponent();

            IsEditMode = true;
            TransferScheduleConfiguration = transferScheduleConfiguration;

            SetUserControlDataBinds();
        }

        private bool IsEditMode { get; }

        private TransferScheduleConfiguration TransferScheduleConfiguration { get; }

        private void SetUserControlDataBinds()
        {
            NameTextEdit.DataBindings.Add("EditValue", TransferScheduleConfiguration, nameof(TransferScheduleConfiguration.Name), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferScheduleTypeComboBoxEdit.Properties.Items.AddRange(typeof(TransferScheduleTypes).GetEnumNames());
            TransferScheduleTypeComboBoxEdit.DataBindings.Add("EditValue", TransferScheduleConfiguration, nameof(TransferScheduleConfiguration.TransferScheduleType), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferScheduleStartTimeTimeEdit.DataBindings.Add("EditValue", TransferScheduleConfiguration, nameof(TransferScheduleConfiguration.TransferScheduleStartTime), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferOffsetComboBoxEdit.DataBindings.Add("EditValue", TransferScheduleConfiguration, nameof(TransferScheduleConfiguration.TransferOffset), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferSynchronousTypeImageComboBoxEdit.Properties.Items.AddEnum(typeof(TransferSynchronousTypes));
            TransferSynchronousTypeImageComboBoxEdit.DataBindings.Add("EditValue", TransferScheduleConfiguration, nameof(TransferScheduleConfiguration.TransferSynchronousType), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void TransferSynchronousTypeImageComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferOffsetComboBoxEdit.Enabled = TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental;
            TransferScheduleConfiguration.TransferOffset = TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental ? TransferScheduleConfiguration.TransferOffset : null;

            TransferOffsetComboBoxEdit_Validating(TransferOffsetComboBoxEdit, null);
        }

        private void ValidateErrorProvider()
        {
            OkSimpleButton.Enabled = !DxErrorProvider.HasErrors;
        }

        private void NameTextEdit_Validating(object sender, CancelEventArgs e)
        {
            var senderTextEdit = (TextEdit) sender;

            if (!string.IsNullOrWhiteSpace(senderTextEdit.Text))
            {
                DxErrorProvider.SetError(senderTextEdit, "", ErrorType.None);
            }
            else
            {
                DxErrorProvider.SetError(senderTextEdit, "Name is required.", ErrorType.Warning);
            }

            ValidateErrorProvider();
        }

        private void TransferOffsetComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferOffsetComboBoxEdit_Validating(sender, null);
        }

        private void TransferOffsetComboBoxEdit_Validating(object sender, CancelEventArgs e)
        {
            var senderComboBoxEdit = (ComboBoxEdit) sender;

            if (TransferScheduleConfiguration.TransferSynchronousType == TransferSynchronousTypes.Incremental && TransferScheduleConfiguration.TransferOffset == null)
            {
                DxErrorProvider.SetError(senderComboBoxEdit, "Transfer offset required.", ErrorType.Warning);
            }
            else
            {
                DxErrorProvider.SetError(senderComboBoxEdit, "", ErrorType.None);
            }

            ValidateErrorProvider();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                UtilityState.ApplicationConfiguration.TransferScheduleConfigurations.Add(TransferScheduleConfiguration);
            }

            ((XtraForm) TopLevelControl).Close();
        }
    }
}