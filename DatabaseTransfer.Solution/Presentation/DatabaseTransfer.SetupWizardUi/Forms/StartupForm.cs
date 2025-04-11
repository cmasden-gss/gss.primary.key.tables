using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DatabaseTransfer.SetupWizardUi.States;
using DatabaseTransfer.SetupWizardUi.ViewModels;
using DatabaseTransfer.SetupWizardUi.ViewModels.Interfaces;
using DatabaseTransfer.SetupWizardUi.Views;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;

namespace DatabaseTransfer.SetupWizardUi.Forms
{
    public partial class StartupForm : XtraForm
    {
        public StartupForm()
        {
            InitializeComponent();
            InitializeWizardComponents();
        }

        private IWizardViewModel _wizardViewModel { get; set; }

        /// <summary>
        ///     Initializes Wizard Components
        /// </summary>
        private void InitializeWizardComponents()
        {
            _wizardViewModel = new WizardViewModel(
                new IWizardPageViewModel[]
                {
                    new StartPageViewModel(),
                    new ConnectionPageViewModel(),
                    new ScheduleBrowserPageViewModel(),
                    new SchemaBrowserPageViewModel(),
                    new FinishPageViewModel()
                },
                MainWindowsUIView, this);

            MainWindowsUIView.AddDocument(new StartPageUserControl());
            MainWindowsUIView.AddDocument(new ConnectionPageUserControl());
            MainWindowsUIView.AddDocument(new ScheduleBrowserPageUserControl());
            MainWindowsUIView.AddDocument(StartupState.SchemaBrowserPageUserControl);
            MainWindowsUIView.AddDocument(new FinishPageUserControl());

            foreach (var baseDocument in MainWindowsUIView.Documents)
            {
                var document = (Document) baseDocument;
                pageGroup.Items.Add(document);
            }
        }

        private void MainWindowsUIView_NavigationBarsShowing(object sender, NavigationBarsCancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        ///     Page Navigation Event | Sets Caption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowsUIView_NavigatedTo(object sender, NavigationEventArgs e)
        {
            e.Parameter = _wizardViewModel;

            MainWindowsUIView.Caption = _wizardViewModel.CurrentView.Caption;
        }

        /// <summary>
        ///     Page Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowsUIView_QueryDocumentActions(object sender, QueryDocumentActionsEventArgs e)
        {
            e.DocumentActions.Add(new DocumentAction(
                    document => _wizardViewModel.CanPreviousView,
                    document => _wizardViewModel.PreviousView())
                {Caption = "Back", Image = ImageList.Images[0]});

            e.DocumentActions.Add(new DocumentAction(
                    document => _wizardViewModel.CanNextView,
                    document => _wizardViewModel.NextView())
                {Caption = "Next", Image = ImageList.Images[1]});

            e.DocumentActions.Add(new DocumentAction(
                    document => _wizardViewModel.CanFinish,
                    document => _wizardViewModel.Finish())
                {Caption = "Finish", Image = ImageList.Images[1]});

            e.DocumentActions.Add(new DocumentAction(
                    document => _wizardViewModel.CanClose,
                    document => _wizardViewModel.Close(true))
                {Caption = "Exit", Image = ImageList.Images[2]});
        }

        /// <summary>
        ///     Allows for resizeable form without a border
        /// </summary>
        /// <remarks>
        ///     https://stackoverflow.com/questions/17748446/custom-resize-handle-in-border-less-form-c-sharp
        /// </remarks>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const uint WM_NCHITTEST = 0x0084;
            const uint WM_MOUSEMOVE = 0x0200;

            const uint HTLEFT = 10;
            const uint HTRIGHT = 11;
            const uint HTBOTTOMRIGHT = 17;
            const uint HTBOTTOM = 15;
            const uint HTBOTTOMLEFT = 16;
            const uint HTTOP = 12;
            const uint HTTOPLEFT = 13;
            const uint HTTOPRIGHT = 14;

            const int RESIZE_HANDLE_SIZE = 10;
            var handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                var formSize = Size;
                var screenPoint = new Point(m.LParam.ToInt32());
                var clientPoint = PointToClient(screenPoint);

                var boxes = new Dictionary<uint, Rectangle>
                {
                    {HTBOTTOMLEFT, new Rectangle(0, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTBOTTOM, new Rectangle(RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, formSize.Width - 2 * RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTBOTTOMRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2 * RESIZE_HANDLE_SIZE)},
                    {HTTOPRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTTOP, new Rectangle(RESIZE_HANDLE_SIZE, 0, formSize.Width - 2 * RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTTOPLEFT, new Rectangle(0, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
                    {HTLEFT, new Rectangle(0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2 * RESIZE_HANDLE_SIZE)}
                };

                foreach (var hitBox in boxes)
                {
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr) hitBox.Key;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
            {
                base.WndProc(ref m);
            }
        }
    }
}