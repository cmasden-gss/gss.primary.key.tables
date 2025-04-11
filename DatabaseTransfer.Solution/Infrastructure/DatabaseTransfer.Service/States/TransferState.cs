using System;
using System.Data;
using System.Linq;
using System.Threading;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Clients.Interfaces;
using DatabaseTransfer.Application.Clients.Models;
using DatabaseTransfer.Application.Configurations.Extensions;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Microsofts.Client;
using DatabaseTransfer.Application.Postgres.Client;
using DatabaseTransfer.Application.Schemas.Extensions;

namespace DatabaseTransfer.Service.States
{
    internal class TransferState
    {
        private TransferScheduleConfiguration TransferScheduleConfiguration { get; set; }

        public bool IsActive { get; private set; }
        private bool IsTerminated { get; set; }

        public void Terminate()
        {
            IsTerminated = true;
        }

        /// <summary>
        ///     Loads configuration and loops routine based on schedule type & schedule start time
        /// </summary>
        /// <remarks>
        ///     This should be refactored to Async Task Initialize(), then await Task.Delay(TimeSpan.FromSeconds(10)), yet I realized that Routine() is pure sync all the way thru.
        /// </remarks>
        public void Initialize()
        {
            UtilityState.ApplicationConfiguration.Load();
            UtilityState.LoggerClient.Information("Loaded Setup Wizard configuration.");

            IsActive = true;

            while (!IsTerminated)
            {
                foreach (var transferScheduleConfiguration in UtilityState.ApplicationConfiguration.TransferScheduleConfigurations.ToList())
                {
                    TransferScheduleConfiguration = transferScheduleConfiguration;

                    if (!TransferScheduleConfiguration.IsScheduled())
                    {
                        continue;
                    }

                    try
                    {
                        TransferScheduleConfiguration.ServiceStartDateTime = DateTime.Now;
                        UtilityState.LoggerClient.Information($"Transfer [{TransferScheduleConfiguration.Name}] routine started.");
                        Routine();

                        if (!IsTerminated)
                        {
                            TransferScheduleConfiguration.ServiceCompleteDateTime = DateTime.Now;
                            UtilityState.LoggerClient.Information($"Transfer [{TransferScheduleConfiguration.Name}] routine finished.");

                            if (TransferScheduleConfiguration.TransferScheduleType == TransferScheduleTypes.Single)
                            {
                                UtilityState.LoggerClient.Information($"Transfer [{TransferScheduleConfiguration.Name}] routine is being removed from schedules.");
                                UtilityState.ApplicationConfiguration.TransferScheduleConfigurations.Remove(TransferScheduleConfiguration);
                            }

                            UtilityState.ApplicationConfiguration.SaveFromService();
                        }
                    }
                    catch (Exception exception)
                    {
                        UtilityState.LoggerClient.Error(exception, $"Transfer [{TransferScheduleConfiguration.Name}] routine error occurred.");
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }

            IsActive = false;
            IsTerminated = false;
        }

        /// <summary>
        ///     Gets the transfer client
        /// </summary>
        private ITransferClient GetTransferClient()
        {
            ITransferClient transferClient = null;

            switch (UtilityState.ApplicationConfiguration.TransferDatabaseType)
            {
                case TransferDatabaseTypes.PostgreDatabase:
                    UtilityState.LoggerClient.Information("Postgre Database selected.");
                    transferClient = new PostgreClient(UtilityState.ApplicationConfiguration.TransferConnection);
                    break;

                case TransferDatabaseTypes.MicrosoftDatabase:
                    UtilityState.LoggerClient.Information("Microsoft Database selected.");
                    transferClient = new MicrosoftClient(UtilityState.ApplicationConfiguration.TransferConnection);
                    break;
            }

            UtilityState.LoggerClient.Information($"Database Version: {transferClient?.GetVersion()}.");

            return transferClient;
        }

        /// <summary>
        ///     The meat and potatoes
        /// </summary>
        /// <remarks>
        ///     Depending on how many errors crop up, handle exceptions per function / method
        /// </remarks>
        private void Routine()
        {
            var actianClient = new ActianClient(UtilityState.ApplicationConfiguration.ActianConnection);
            var transferClient = GetTransferClient();

#if DEBUG
            foreach (var tableName in transferClient.GetTableNames())
            {
                transferClient.DeleteTable(tableName);
            }
#endif

            foreach (var tableSchema in TransferScheduleConfiguration.TableSchemas)
            {
                if (IsTerminated)
                {
                    return;
                }

                // The actian table exist
                if (!actianClient.TableExists(tableSchema.Name))
                {
                    UtilityState.LoggerClient.Information($"Actian Table [{tableSchema.Name}] no longer exists. Continuing.");
                    continue;
                }

                var currentActianTableSchema = actianClient.GetTableSchema(tableSchema.Name);

                // The actian table and the saved schema are equal
                if (!tableSchema.IsColumnSchemasEqual(currentActianTableSchema))
                {
                    UtilityState.LoggerClient.Information($"Actian Table [{tableSchema.Name}] Schema has changed. Modify the schema in the setup wizard. Continuing.");
                    continue;
                }

                // saved schemas has no columns selected
                if (tableSchema.ColumnSchemas.Count == 0)
                {
                    UtilityState.LoggerClient.Information($"Saved Schema [{tableSchema.Name}] has no columns selected. Modify the schema in the setup wizard. Continuing.");
                    continue;
                }

                // if transfer schema changed then apply snapshot regardless
                var hasTransferSchemaChanged = false;

                // the transfer table exist
                if (!transferClient.TableExists(tableSchema.Name))
                {
                    UtilityState.LoggerClient.Information($"Transfer Table [{tableSchema.Name}] doesn't exist. Creating.");
                    transferClient.CreateTable(tableSchema.ToTableSchemaWithMaskDataTypes());
                }
                else
                {
                    var currentTransferTableSchema = transferClient.GetTableSchema(tableSchema.Name);

                    // the transfer schema are equal
                    if (!tableSchema.ToTableSchemaWithMaskDataTypes().IsColumnSchemasEqual(currentTransferTableSchema) || tableSchema.ColumnSchemas.Count != currentTransferTableSchema.ColumnSchemas.Count)
                    {
                        UtilityState.LoggerClient.Information($"Transfer Table [{tableSchema.Name}] Schema has changed. Deleting table and recreating.");
                        transferClient.DeleteTable(tableSchema.Name);
                        transferClient.CreateTable(tableSchema.ToTableSchemaWithMaskDataTypes());

                        hasTransferSchemaChanged = true;
                    }
                }

                // execute types
                switch (TransferScheduleConfiguration.TransferSynchronousType)
                {
                    case TransferSynchronousTypes.Incremental:

                        if (!TransferScheduleConfiguration.ServiceCompleteDateTime.HasValue || hasTransferSchemaChanged)
                        {
                            ExecuteSnapshot(actianClient, transferClient, tableSchema);
                        }
                        else
                        {
                            ExecuteSynchronous(actianClient, transferClient, tableSchema);
                        }

                        break;

                    default:

                        ExecuteSnapshot(actianClient, transferClient, tableSchema);
                        break;
                }
            }
        }

        /// <summary>
        ///     Synchronous - clears the transfer table based on a datetime column filter range and bulk copies based on the same
        ///     range
        /// </summary>
        private void ExecuteSynchronous(ActianClient actianClient, ITransferClient transferClient, TableSchemaConfiguration tableSchema)
        {
            var synchronousDateTime = DateTime.Now.AddDays(-TransferScheduleConfiguration.TransferOffset.Value).Date;

            if (TransferScheduleConfiguration.ServiceCompleteDateTime.Value.Date < synchronousDateTime)
            {
                synchronousDateTime = TransferScheduleConfiguration.ServiceCompleteDateTime.Value.Date;
            }

            UtilityState.LoggerClient.Information($"Deleting synchronous data from Transfer Table [{tableSchema.Name}].");
            transferClient.ExecuteSqlStatement($"delete from {tableSchema.Name} where \"{tableSchema.SynchronousColumnSchema.Name}\" >= '{synchronousDateTime:MM/dd/yyyy}'");

            UtilityState.LoggerClient.Information($"Gathering Actian Table [{tableSchema.Name}] data for synchronous transfer.");
            var actianDataTable = actianClient.GetTableDataWithMasksFromTableSchema(tableSchema, synchronousDateTime);

            UtilityState.LoggerClient.Information($"Applying synchronous data to Transfer Table [{tableSchema.Name}].");
            try
            {
                transferClient.BulkCopyDataTable(tableSchema.Name, actianDataTable);
            }
            catch (Exception exception)
            {
                UtilityState.LoggerClient.Error(exception, "Applying synchronous data error occurred.");
            }

            DisposeActianDataTable(actianDataTable);
        }

        /// <summary>
        ///     Snapshot - clears the transfer table and bulk copies entire actian table data
        /// </summary>
        private void ExecuteSnapshot(ActianClient actianClient, ITransferClient transferClient, TableSchemaConfiguration tableSchema)
        {
            transferClient.ClearTable(tableSchema.Name);

            UtilityState.LoggerClient.Information($"Gathering Actian Table [{tableSchema.Name}] data for snapshot transfer.");
            var actianDataTable = actianClient.GetTableDataWithMasksFromTableSchema(tableSchema);

            UtilityState.LoggerClient.Information($"Applying snapshot data to Transfer Table [{tableSchema.Name}].");

            try
            {
                transferClient.BulkCopyDataTable(tableSchema.Name, actianDataTable);
            }
            catch (Exception exception)
            {
                UtilityState.LoggerClient.Error(exception, "Applying snapshot data error occurred.");
            }

            DisposeActianDataTable(actianDataTable);
        }

        /// <summary>
        ///     Because non-relational data while loading 3 gigs on a single file / table will kill the service without disposing.
        /// </summary>
        private void DisposeActianDataTable(DataTable dataTable)
        {
            dataTable.Clear();
            dataTable.Dispose();
            dataTable = null;
            GC.Collect();
        }
    }
}