using System.ServiceProcess;

namespace DatabaseTransfer.Service
{
    internal static class Startup
    {
        private static void Main()
        {
#if DEBUG
            var service = new TransferService();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else
            var servicesToRun = new ServiceBase[]
            {
                new TransferService()
            };
            ServiceBase.Run(servicesToRun);
#endif

            // As A Console Application
            //UtilityState.LoggerClient.Information("Starting application.");

            //UtilityState.LoggerClient.Information("Checking for the Setup Wizard configuration file.");
            //if (!UtilityState.ApplicationConfiguration.ConfigurationFileExists)
            //{
            //    UtilityState.LoggerClient.Information("The Setup Wizard configuration file is missing. Run the Setup Wizard Application. Exiting.");
            //}
            //else
            //{
            //    TransferState.Initialize();
            //}

            //Console.ReadLine();
            //UtilityState.LoggerClient.Information("Exiting application.");
        }
    }
}