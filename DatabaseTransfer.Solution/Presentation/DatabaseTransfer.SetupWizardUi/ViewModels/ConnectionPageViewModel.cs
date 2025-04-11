using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    internal class ConnectionPageViewModel : IWizardPageViewModel
    {
        public bool IsActianConnectionValid { get; set; }

        public bool IsTransferConnectionValid { get; set; }

        public string Caption => "Database Connections";

        public bool CanNextView => IsActianConnectionValid && IsTransferConnectionValid;

        public bool CanPreviousView => true;

        public bool CanFinish => false;
    }
}