using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using DatabaseTransfer.Service.States;

namespace DatabaseTransfer.Service
{
    public partial class TransferService : ServiceBase
    {
        private TransferState TransferState { get; }
        private EventLog TransferEventLog { get; set; }
        private FileSystemWatcher FileSystemWatcher { get; set; }

        public TransferService()
        {
            InitializeComponent();

            TransferState = new TransferState();

            InitializeEventLog();
            InitializeDirectories();
            InitializeFileSystemWatcher();
        }

        private void InitializeEventLog()
        {
            if (!EventLog.SourceExists("Database Transfer"))
            {
                EventLog.CreateEventSource("Database Transfer", "Application");
            }

            TransferEventLog = new EventLog("Application") { Source = "Database Transfer" };
        }

        private void InitializeDirectories()
        {
            // If the service is installed before the wizard, the directory will not exist, and the service install will fail with (The service did not respond to the start or control request in a timely fashion).
            // Event Viewer > Windows Logs > Application, then filter by Event ID 1026
            if (!Directory.Exists(Path.GetDirectoryName(UtilityState.ApplicationConfiguration.ConfigurationFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(UtilityState.ApplicationConfiguration.ConfigurationFilePath));
            }
        }

        private void InitializeFileSystemWatcher()
        {
            FileSystemWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(UtilityState.ApplicationConfiguration.ConfigurationFilePath),

                NotifyFilter = NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.FileName
                               | NotifyFilters.DirectoryName,

                Filter = Path.GetFileName(UtilityState.ApplicationConfiguration.ConfigurationFilePath)
            };

            FileSystemWatcher.Created += FileSystemWatcherOnCreated;
            FileSystemWatcher.EnableRaisingEvents = true;
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

            TransferEventLog.WriteEntry($"Starting Database Transfer As A Service.\nWatched File: {UtilityState.ApplicationConfiguration.ConfigurationFilePath}\nDetailed log: {UtilityState.LoggerClientLogFilePath}", EventLogEntryType.Information, 1000);

            UtilityState.LoggerClient.Information("Starting Database Transfer As A Service.");
            UtilityState.LoggerClient.Information($"Service Version: {fileVersion}.");

            UtilityState.LoggerClient.Information("Checking for the Setup Wizard configuration file.");
            if (!UtilityState.ApplicationConfiguration.ConfigurationFileExists)
            {
                UtilityState.LoggerClient.Information("The Setup Wizard configuration file is missing. Run the Setup Wizard Application.");
            }
            else
            {
                new Task(() => { TransferState.Initialize(); }).Start();
            }
        }

        private void FileSystemWatcherOnCreated(object source, FileSystemEventArgs e)
        {
            try
            {
                FileSystemWatcher.EnableRaisingEvents = false;

                TransferEventLog.WriteEntry("Setup Wizard configuration file created.", EventLogEntryType.Information, 1002);
                UtilityState.LoggerClient.Information("Setup Wizard configuration file created.");

                if (TransferState.IsActive)
                {
                    TransferState.Terminate();

                    while (TransferState.IsActive)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }

                    UtilityState.LoggerClient.Information("Terminated routine(s).");
                }

                //Task.RanToCompletion based on IsActive
                new Task(() => { TransferState.Initialize(); }).Start();
            }
            catch (Exception exception)
            {
                UtilityState.LoggerClient.Error(exception, "File system watcher error occurred.");
            }
            finally
            {
                FileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        protected override void OnStop()
        {
            TransferEventLog.WriteEntry("Stopping Database Transfer As A Service.", EventLogEntryType.Information, 1001);
            UtilityState.LoggerClient.Information("Stopping Database Transfer As A Service.");

            TransferState.Terminate();
        }

        //protected override void OnCustomCommand(int command)
        //{
        //    switch ((ExecuteCommandTypes)command)
        //    {
        //        case ExecuteCommandTypes.Dummy:
        //            break;
        //    }
        //}
    }
}