using System.Diagnostics;
using DevExpress.Utils;

namespace DatabaseTransfer.SetupWizardUi.Views
{
    public partial class FinishPageUserControl : BasePageUserControl
    {
        public FinishPageUserControl()
        {
            InitializeComponent();
        }

        private void labelControl5_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Startup));

            Process.Start(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319");
        }
    }
}