using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class BasePageUserControl : XtraUserControl, ISupportNavigation
    {
        public BasePageUserControl()
        {
            InitializeComponent();
        }

        protected IWizardPageViewModel PageViewModel => WizardViewModel.CurrentView;

        protected IWizardViewModel WizardViewModel { get; private set; }

        void ISupportNavigation.OnNavigatedTo(INavigationArgs args)
        {
            WizardViewModel = args.Parameter as IWizardViewModel;
        }

        void ISupportNavigation.OnNavigatedFrom(INavigationArgs args)
        {
        }
    }
}