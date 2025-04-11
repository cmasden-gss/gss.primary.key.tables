using System.Linq;
using System.Threading.Tasks;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Actians.Extensions;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Tests.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseTransfer.Application.Tests
{
    [TestClass]
    public class ActianConnectionIntegrations
    {
        [TestMethod]
        public async Task IsValidConnection_Pass()
        {
            var result = await ActianDatabaseConnectionExtensions.IsValidConnection(UtilityState.ActianConnectionString);
            Assert.IsFalse(result.HasException);
        }

        [TestMethod]
        public void TableExists_Pass()
        {
            var tableName = "aa_conv_log";

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            Assert.IsTrue(actianClient.TableExists(tableName));
        }

        [TestMethod]
        public void TableExists_Fail()
        {
            // Will of the Forsaken
            var tableName = "any_forsaken_table";

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            Assert.IsFalse(actianClient.TableExists(tableName));
        }

        [TestMethod]
        public void GetTableNames_Pass()
        {
            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            Assert.IsTrue(actianClient.GetTableNames().Any());
        }

        [TestMethod]
        public void GetTableSchema_Pass()
        {
            var tableName = "aa_conv_log";

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            var result = actianClient.GetTableSchema(tableName);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTableDataFromTableSchema_Pass()
        {
            var tableName = "aa_conv_log";

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            var actianTableSchema = actianClient.GetTableSchema(tableName);

            Assert.IsNotNull(actianTableSchema);

            var result = actianClient.GetTableDataFromTableSchema(actianTableSchema);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTableDataWithMasksFromTableSchema_Pass()
        {
            var tableName = "aa_conv_log";

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            var actianTableSchema = actianClient.GetTableSchema(tableName);

            Assert.IsNotNull(actianTableSchema);

            var tableSchemaConfiguration = new TableSchemaConfiguration
            {
                Name = actianTableSchema.Name
            };

            actianTableSchema.ColumnSchemas.ForEach(column =>
            {
                tableSchemaConfiguration.ColumnSchemas.Add(new ColumnSchemaConfiguration
                {
                    Name = column.Name,
                    DataType = column.DataType
                });
            });

            var result = actianClient.GetTableDataWithMasksFromTableSchema(tableSchemaConfiguration);

            Assert.IsNotNull(result);
        }
    }
}