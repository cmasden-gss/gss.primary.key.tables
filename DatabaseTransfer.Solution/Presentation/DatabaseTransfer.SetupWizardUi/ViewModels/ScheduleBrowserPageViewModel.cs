using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    public class ScheduleBrowserPageViewModel : IWizardPageViewModel
    {
        public string Caption => "Schedule Routines";

        public bool CanNextView => UtilityState.ApplicationConfiguration.TransferScheduleConfigurations.Count > 0;

        public bool CanPreviousView { get; set; } = true;

        public bool CanFinish => false;
    }
}