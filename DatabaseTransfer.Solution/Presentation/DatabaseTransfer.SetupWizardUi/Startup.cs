using System;
using System.Drawing;
using DatabaseTransfer.SetupWizardUi.States;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace DatabaseTransfer.SetupWizardUi
{
    internal static class Startup
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            WindowsFormsSettings.DefaultFont = new Font("Segoe UI", 8);
            WindowsFormsSettings.AllowAutoFilterConditionChange = DefaultBoolean.False;

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(StartupState.StartupForm);
        }
    }
}