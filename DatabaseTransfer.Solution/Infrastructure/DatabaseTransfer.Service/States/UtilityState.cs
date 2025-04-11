using System;
using System.IO;
using DatabaseTransfer.Application.Configurations;
using Serilog;
using Serilog.Core;

namespace DatabaseTransfer.Service.States
{
    internal static class UtilityState
    {
        private static Logger _loggerClient;
        private static ApplicationConfiguration _applicationConfiguration { get; set; }

        /// <summary>
        ///     Configuration for the service
        /// </summary>
        internal static ApplicationConfiguration ApplicationConfiguration => _applicationConfiguration ?? (_applicationConfiguration = new ApplicationConfiguration());

        internal static string LoggerClientLogFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Global Shop Database Transfer", "Logs", "database.transfer.log");

        /// <summary>
        ///     Logger for the service
        /// </summary>
        internal static Logger LoggerClient =>
            _loggerClient ?? (_loggerClient = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(LoggerClientLogFilePath)
                .CreateLogger());
    }
}