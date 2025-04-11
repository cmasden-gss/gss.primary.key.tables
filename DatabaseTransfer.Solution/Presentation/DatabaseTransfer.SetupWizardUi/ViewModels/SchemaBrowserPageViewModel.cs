using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    internal class SchemaBrowserPageViewModel : IWizardPageViewModel
    {
        public string Caption => $"{UtilityState.TransferScheduleConfiguration.Name} Routine | Database Schemas";

        public bool CanNextView { get; set; } = true;

        public bool CanPreviousView { get; set; } = true;

        public bool CanFinish => false;
    }
}