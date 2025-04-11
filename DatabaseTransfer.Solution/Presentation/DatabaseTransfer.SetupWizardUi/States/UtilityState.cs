using System.Linq;
using AutoMapper;
using DatabaseTransfer.Application.Configurations;
using DatabaseTransfer.Application.Configurations.Models;

namespace DatabaseTransfer.SetupWizardUi.States
{
    internal static class UtilityState
    {
        private static ApplicationConfiguration _applicationConfiguration { get; set; }

        /// <summary>
        ///     Configuration for service
        /// </summary>
        internal static ApplicationConfiguration ApplicationConfiguration
        {
            get
            {
                if (_applicationConfiguration != null)
                {
                    return _applicationConfiguration;
                }

                _applicationConfiguration = new ApplicationConfiguration();

                if (_applicationConfiguration.ConfigurationFileExists)
                {
                    _applicationConfiguration.Load();
                }

                return _applicationConfiguration;
            }
        }

        private static TransferScheduleConfiguration _transferScheduleConfiguration { get; set; }

        internal static TransferScheduleConfiguration TransferScheduleConfiguration
        {
            get => _transferScheduleConfiguration ?? (_transferScheduleConfiguration = ApplicationConfiguration.TransferScheduleConfigurations.FirstOrDefault());
            set => _transferScheduleConfiguration = value;
        }

        private static IMapper _autoMapper { get; set; }

        /// <summary>
        ///     Auto Mapper for Selected Schemas to Actian Schemas
        /// </summary>
        internal static IMapper AutoMapper
        {
            get
            {
                if (_autoMapper != null)
                {
                    return _autoMapper;
                }

                var autoMapperConfiguration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(Startup)); });

                _autoMapper = autoMapperConfiguration.CreateMapper();

                return _autoMapper;
            }
        }
    }
}