using System;
using System.Windows.Forms;
using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace DatabaseTransfer.SetupWizardUi.ViewModels
{
    internal class WizardViewModel : IWizardViewModel
    {
        private readonly Form _mainForm;
        private readonly IWizardPageViewModel[] _pages;
        private readonly WindowsUIView _view;
        private int _pageIndex;

        public WizardViewModel(IWizardPageViewModel[] pages, WindowsUIView view, Form mainForm)
        {
            _pages = pages;
            _view = view;
            _mainForm = mainForm;
        }

        public void UpdateDocumentActions()
        {
            _view.UpdateDocumentActions();
        }

        public bool CanNextView => _pageIndex >= 0 && _pageIndex < _pages.Length - 1 && CurrentView.CanNextView;

        public void NextView()
        {
            ActivatePage(++_pageIndex);
        }

        public bool CanPreviousView => _pageIndex > 0 && _pageIndex < _pages.Length && CurrentView.CanPreviousView;

        public void PreviousView()
        {
            ActivatePage(--_pageIndex);
        }

        public void CompleteView()
        {
            if (CanNextView)
            {
                NextView();
            }
        }

        public IWizardPageViewModel CurrentView => _pages[_pageIndex];

        public bool CanClose => _pageIndex >= 0 && _pageIndex < _pages.Length - 1;

        public void Close(bool isClosing)
        {
            if (isClosing)
            {
                var flyout = _view.ContentContainers["closeFlyout"] as Flyout;

                flyout.Action = new FlyoutAction
                {
                    Caption = "Setup Wizard",
                    Description = "Are you sure you want to exit the setup?"
                };

                _view.FlyoutHidden += view_FlyoutHidden;
                _view.ActivateContainer(flyout);
            }
            else
            {
                Close();
            }
        }

        public bool CanFinish => CurrentView.CanFinish;

        public void Finish()
        {
            UtilityState.ApplicationConfiguration.SaveFromSetupWizard();
            Close();
        }

        private void ActivatePage(int index)
        {
            var pageGroup = _view.ContentContainers["pageGroup"] as PageGroup;

            _view.ActivateDocument(pageGroup.Items[index]);
        }

        private void view_FlyoutHidden(object sender, FlyoutResultEventArgs e)
        {
            _view.FlyoutHidden -= view_FlyoutHidden;

            if (e.Result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Close()
        {
            _mainForm.BeginInvoke(new Action(_mainForm.Close));
        }
    }
}