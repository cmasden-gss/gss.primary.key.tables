namespace DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces
{
    /// <summary>
    ///     Navigation Wizard Interface
    /// </summary>
    public interface IWizardViewModel
    {
        /// <summary>
        ///     current view model / UserControl
        /// </summary>
        IWizardPageViewModel CurrentView { get; }

        /// <summary>
        ///     Leverages ViewModel
        /// </summary>
        bool CanPreviousView { get; }

        /// <summary>
        ///     Leverages ViewModel
        /// </summary>
        bool CanNextView { get; }

        /// <summary>
        ///     Leverages ViewModel
        /// </summary>
        bool CanFinish { get; }

        /// <summary>
        ///     Leverages ViewModel
        /// </summary>
        bool CanClose { get; }

        /// <summary>
        ///     Previous Method
        /// </summary>
        void PreviousView();

        /// <summary>
        ///     Next Method
        /// </summary>
        void NextView();

        /// <summary>
        ///     Complete Method
        /// </summary>
        void CompleteView();

        /// <summary>
        ///     Finish Method
        /// </summary>
        void Finish();

        /// <summary>
        ///     Close Method
        /// </summary>
        /// <param name="isClosing"></param>
        void Close(bool isClosing);

        /// <summary>
        ///     Refreshes / re-validates navigation / wizard buttons
        /// </summary>
        void UpdateDocumentActions();
    }
}