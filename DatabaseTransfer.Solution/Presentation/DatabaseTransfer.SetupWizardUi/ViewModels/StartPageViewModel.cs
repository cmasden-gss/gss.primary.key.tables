using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    internal class StartPageViewModel : IWizardPageViewModel
    {
        public string Caption => "Database Transfer Wizard";

        public bool CanNextView => true;

        public bool CanPreviousView => false;

        public bool CanFinish => false;
    }
}