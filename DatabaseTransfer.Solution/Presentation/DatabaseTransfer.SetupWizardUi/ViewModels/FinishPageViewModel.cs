using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    internal class FinishPageViewModel : IWizardPageViewModel
    {
        public string Caption => "Database Transfer Wizard";

        public bool CanNextView => false;

        public bool CanPreviousView => true;

        public bool CanFinish => true;
    }
}