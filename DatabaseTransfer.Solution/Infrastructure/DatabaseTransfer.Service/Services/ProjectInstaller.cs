using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DatabaseTransfer.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            AfterInstall += ServiceInstaller_AfterInstall;
        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //var serviceInstaller = (ServiceInstaller)sender;

            //using (var serviceController = new ServiceController(serviceInstaller.ServiceName))
            //{
            //    serviceController.Start();
            //}

            var sc = new ServiceController("Database Transfer");
            sc.Start();
        }
    }
}