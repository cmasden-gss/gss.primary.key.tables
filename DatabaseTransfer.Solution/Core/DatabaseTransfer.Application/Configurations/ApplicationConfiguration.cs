using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;
using Newtonsoft.Json;

namespace DatabaseTransfer.Application.Configurations
{
    /// <summary>
    ///     FrontEnd + Service Configuration
    /// </summary>
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            TransferScheduleConfigurations = new List<TransferScheduleConfiguration>();

            ActianConnection = "Server Name = ServerName; Database Name = GlobalPLA; User Id = Master; Password = master;";
            TransferConnection = "Server = ServerName; Database = Global-PLA-Transfer-Database; User Id = UserName; Password = Password;";

#if DEBUG
            ActianConnection = "Server Name = gssw10p620; Database Name = GlobalPLA; User Id = Master; Password = master;";
            TransferConnection = "User ID=postgres;Password=03181991;Host=localhost;Port=5432;Database=DatabaseTransferBleeding;";
#endif
        }

        private string _configurationDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Global Shop Database Transfer");
        [JsonIgnore] public string ConfigurationFilePath => Path.Combine(_configurationDirectory, "application.configuration.1.0.0.0.json");

        [JsonIgnore] public bool ConfigurationFileExists => File.Exists(ConfigurationFilePath);

        public string Identity { get; set; }

        public List<TransferScheduleConfiguration> TransferScheduleConfigurations { get; set; }

        [JsonIgnore] public string ActianConnection { get; set; }

        public string ActianConnectionEncryption { get; set; }

        [JsonIgnore] public string TransferConnection { get; set; }

        public string TransferConnectionEncryption { get; set; }

        public TransferDatabaseTypes TransferDatabaseType { get; set; }

        private JsonSerializerSettings _jsonSerializerSettings => new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public void SaveFromSetupWizard()
        {
            Identity = Guid.NewGuid().ToString();
            TransferScheduleConfigurations.ForEach(c => c.ServiceStartDateTime = null);
            TransferScheduleConfigurations.ForEach(c => c.ServiceCompleteDateTime = null);

            if (File.Exists(ConfigurationFilePath))
            {
                File.Delete(ConfigurationFilePath);
                Thread.Sleep(1000);
            }

            Save();
        }

        public void SaveFromService()
        {
            var applicationConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(ConfigurationFilePath), _jsonSerializerSettings);

            if (applicationConfiguration.Identity.Equals(Identity))
            {
                Save();
            }
        }

        private void Save()
        {
            ActianConnectionEncryption = EncryptionExtensions.EncryptString(ActianConnection);
            TransferConnectionEncryption = EncryptionExtensions.EncryptString(TransferConnection);

            try
            {
                if (!Directory.Exists(_configurationDirectory))
                {
                    Directory.CreateDirectory(_configurationDirectory);
                }

                var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented, _jsonSerializerSettings);
                File.WriteAllText(ConfigurationFilePath, jsonString);
            }
            catch (Exception)
            {
                throw new Exception($"Configuration Error: Could not create configuration file. Manually create at \n {ConfigurationFilePath}");
            }
        }

        public void Load()
        {
            var applicationConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(ConfigurationFilePath), _jsonSerializerSettings);

            Identity = applicationConfiguration.Identity;
            ActianConnection = EncryptionExtensions.DecryptString(applicationConfiguration.ActianConnectionEncryption);
            TransferConnection = EncryptionExtensions.DecryptString(applicationConfiguration.TransferConnectionEncryption);
            TransferDatabaseType = applicationConfiguration.TransferDatabaseType;

            TransferScheduleConfigurations = applicationConfiguration.TransferScheduleConfigurations;
        }
    }
}