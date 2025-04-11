using System.Linq;
using System.Threading.Tasks;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Microsofts.Client;
using DatabaseTransfer.Application.Microsofts.Extensions;
using DatabaseTransfer.Application.Tests.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseTransfer.Application.Tests
{
    [TestClass]
    public class MicrosoftConnectionIntegrations
    {
        [TestMethod]
        public void GetVersion_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);
            Assert.IsFalse(microsoftClient.GetVersion().HasException);
        }

        [TestMethod]
        public async Task IsValidConnection_Pass()
        {
            var result = await MicrosoftDatabaseConnectionExtensions.IsValidConnection(UtilityState.MicrosoftConnectionString);
            Assert.IsFalse(result.HasException);
        }

        [TestMethod]
        public void TableExists_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (microsoftClient.TableExists(tableName))
            {
                microsoftClient.DeleteTable(tableName);
            }

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);
            var actianTableSchema = actianClient.GetTableSchema(tableName);

            microsoftClient.CreateTable(actianTableSchema);

            Assert.IsTrue(microsoftClient.TableExists(tableName));

            microsoftClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void TableExists_Fail()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "any_forsaken_table";
            Assert.IsFalse(microsoftClient.TableExists(tableName));
        }

        [TestMethod]
        public void GetTableNames_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (!microsoftClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                microsoftClient.CreateTable(actianTableSchema);
            }

            Assert.IsTrue(microsoftClient.GetTableNames().Any());

            microsoftClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void CreateTable_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (microsoftClient.TableExists(tableName))
            {
                microsoftClient.DeleteTable(tableName);
            }

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);
            var actianTableSchema = actianClient.GetTableSchema(tableName);

            microsoftClient.CreateTable(actianTableSchema);

            Assert.IsTrue(microsoftClient.TableExists(tableName));

            microsoftClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void GetTableSchema_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (!microsoftClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                microsoftClient.CreateTable(actianTableSchema);
            }

            Assert.IsNotNull(microsoftClient.GetTableSchema(tableName));

            microsoftClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void ClearTable_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (!microsoftClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                microsoftClient.CreateTable(actianTableSchema);
            }

            microsoftClient.ClearTable(tableName);
            microsoftClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void DeleteTable_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (!microsoftClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                microsoftClient.CreateTable(actianTableSchema);
            }

            microsoftClient.DeleteTable(tableName);
            Assert.IsTrue(!microsoftClient.TableExists(tableName));
        }

        [TestMethod]
        public void ExecuteSqlStatement_Pass()
        {
            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            var tableName = "aa_conv_log";
            if (!microsoftClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                microsoftClient.CreateTable(actianTableSchema);
            }

            microsoftClient.ExecuteSqlStatement($"select * from {tableName}");
            microsoftClient.DeleteTable(tableName);
        }

        /// <summary>
        /// Does not validate/compare data
        /// </summary>
        [TestMethod]
        public void BulkCopyDataTable_Pass()
        {
            var actianClient = new ActianClient(UtilityState.ActianConnectionString);

            var tableName = "aa_conv_log";
            var actianTableSchema = actianClient.GetTableSchema(tableName);
            var actianTableData = actianClient.GetTableDataFromTableSchema(actianTableSchema);

            var microsoftClient = new MicrosoftClient(UtilityState.MicrosoftConnectionString);

            microsoftClient.CreateTable(actianTableSchema);
            Assert.IsTrue(microsoftClient.TableExists(tableName));

            microsoftClient.BulkCopyDataTable(tableName, actianTableData);
            microsoftClient.DeleteTable(tableName);
        }
    }
}