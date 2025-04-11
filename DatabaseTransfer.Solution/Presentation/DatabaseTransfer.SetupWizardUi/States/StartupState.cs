using DatabaseTransfer.SetupWizardUi.Forms;
using DatabaseTransfer.SetupWizardUi.Views;

namespace DatabaseTransfer.SetupWizardUi.States
{
    internal static class StartupState
    {
        private static StartupForm _startupForm;

        private static SchemaBrowserPageUserControl _schemaBrowserPageUserControl;

        /// <summary>
        ///     The startup form used throughout the process
        /// </summary>
        internal static StartupForm StartupForm => _startupForm ?? (_startupForm = new StartupForm());

        /// <summary>
        ///     The schema browser user control used throughout the process (less hacks this way)
        /// </summary>
        internal static SchemaBrowserPageUserControl SchemaBrowserPageUserControl => _schemaBrowserPageUserControl ?? (_schemaBrowserPageUserControl = new SchemaBrowserPageUserControl());
    }
}