using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseTransfer.Application.Actians.Extensions;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations;
using DatabaseTransfer.Application.Microsofts.Extensions;
using DatabaseTransfer.Application.Models;
using DatabaseTransfer.Application.Postgres.Extensions;
using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class ConnectionPageUserControl : BasePageUserControl
    {
        public ConnectionPageUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private ApplicationConfiguration ApplicationConfiguration => UtilityState.ApplicationConfiguration;

        private async void Initialize()
        {
            while (GetConfigurationPageViewModel() == null)
            {
                await Task.Delay(100);
            }

            SetUserControlDataBinds();

            ActianConnectionTextEdit_Validating(ActianConnectionTextEdit, null);
            TransferConnectionTextEdit_Validating(TransferConnectionTextEdit, null);
        }

        private void SetUserControlDataBinds()
        {
            ActianConnectionTextEdit.DataBindings.Add("EditValue", ApplicationConfiguration, nameof(ApplicationConfiguration.ActianConnection), true, DataSourceUpdateMode.OnPropertyChanged);
            TransferConnectionTextEdit.DataBindings.Add("EditValue", ApplicationConfiguration, nameof(ApplicationConfiguration.TransferConnection), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferDatabaseTypeImageComboBoxEdit.Properties.Items.AddEnum(typeof(TransferDatabaseTypes));
            TransferDatabaseTypeImageComboBoxEdit.DataBindings.Add("EditValue", ApplicationConfiguration, nameof(ApplicationConfiguration.TransferDatabaseType), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void TransferDatabaseTypeImageComboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferConnectionTextEdit_Validating(TransferConnectionTextEdit, null);
        }

        private async void ActianConnectionTextEdit_Validating(object sender, CancelEventArgs e)
        {
            var senderTextEdit = (TextEdit) sender;
            var model = GetConfigurationPageViewModel();

            if (model == null)
            {
                return;
            }

            DxErrorProvider.SetError(senderTextEdit, "Verifying connection string.", ErrorType.Warning);
            model.IsActianConnectionValid = false;

            await Task.Delay(100);

            var isValidConnectionResult = await ActianDatabaseConnectionExtensions.IsValidConnection(senderTextEdit.Text);

            if (isValidConnectionResult.HasException)
            {
                Invoke(new Action(() => { DxErrorProvider.SetError(senderTextEdit, $"A valid Actian connection string is required.\n\n----- Trace -----\n{isValidConnectionResult.Exception}", ErrorType.Critical); }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    DxErrorProvider.SetError(senderTextEdit, "", ErrorType.None);
                    model.IsActianConnectionValid = true;
                }));
            }

            Invoke(new Action(() => { WizardViewModel.UpdateDocumentActions(); }));
        }

        private async void TransferConnectionTextEdit_Validating(object sender, CancelEventArgs e)
        {
            var senderTextEdit = (TextEdit) sender;
            var model = GetConfigurationPageViewModel();

            if (model == null)
            {
                return;
            }

            DxErrorProvider.SetError(senderTextEdit, "Verifying connection string.", ErrorType.Warning);
            model.IsTransferConnectionValid = false;

            await Task.Delay(100);

            var isValidConnectionResult = Result<bool>.Success(false);

            switch (ApplicationConfiguration.TransferDatabaseType)
            {
                case TransferDatabaseTypes.PostgreDatabase:

                    isValidConnectionResult = await PostgreDatabaseConnectionExtensions.IsValidConnection(senderTextEdit.Text);

                    if (isValidConnectionResult.HasException)
                    {
                        Invoke(new Action(() => { DxErrorProvider.SetError(senderTextEdit, $"A valid Postgre connection string is required.\n\n----- Trace -----\n{isValidConnectionResult.Exception}", ErrorType.Critical); }));
                    }

                    break;

                case TransferDatabaseTypes.MicrosoftDatabase:

                    isValidConnectionResult = await MicrosoftDatabaseConnectionExtensions.IsValidConnection(senderTextEdit.Text);

                    if (isValidConnectionResult.HasException)
                    {
                        Invoke(new Action(() => { DxErrorProvider.SetError(senderTextEdit, $"A valid Microsoft connection string is required.\n\n----- Trace -----\n{isValidConnectionResult.Exception}", ErrorType.Critical); }));
                    }

                    break;
            }

            if (isValidConnectionResult.Payload)
            {
                Invoke(new Action(() =>
                {
                    DxErrorProvider.SetError(senderTextEdit, "", ErrorType.None);
                    model.IsTransferConnectionValid = true;
                }));
            }

            Invoke(new Action(() => { WizardViewModel.UpdateDocumentActions(); }));
        }

        private ConnectionPageViewModel GetConfigurationPageViewModel()
        {
            if (WizardViewModel == null)
            {
                return null;
            }

            return PageViewModel as ConnectionPageViewModel;
        }
    }
}