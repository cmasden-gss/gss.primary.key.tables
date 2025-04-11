using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DatabaseTransfer.SetupWizardUi.Extensions
{
    public static class UserControlExtensions
    {
        /// <summary>
        ///     Creates a XtraForm for a user control
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="title"></param>
        /// <param name="isDialog"></param>
        internal static void OpenUserControlWithXtraForm(UserControl userControl, string title, bool isDialog = true)
        {
            var xtraForm = new XtraForm
            {
                Name = $"{userControl.Name}Form",
                Text = title,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                StartPosition = FormStartPosition.CenterScreen,
                ShowIcon = false
            };

            xtraForm.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;

            if (isDialog)
            {
                xtraForm.ShowDialog();
            }
            else
            {
                xtraForm.Show();
            }
        }
    }
}