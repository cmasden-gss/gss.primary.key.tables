namespace DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces
{
    /// <summary>
    ///     View Interface
    /// </summary>
    public interface IWizardPageViewModel
    {
        /// <summary>
        ///     Shows next button
        /// </summary>
        bool CanNextView { get; }

        /// <summary>
        ///     Shows previous / back button
        /// </summary>
        bool CanPreviousView { get; }

        /// <summary>
        ///     shows finish button
        /// </summary>
        bool CanFinish { get; }

        /// <summary>
        ///     Main UI caption / title
        /// </summary>
        string Caption { get; }
    }
}