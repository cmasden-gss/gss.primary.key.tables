using System.Linq;
using System.Threading.Tasks;
using DatabaseTransfer.Application.Actians.Client;
using DatabaseTransfer.Application.Postgres.Client;
using DatabaseTransfer.Application.Postgres.Extensions;
using DatabaseTransfer.Application.Tests.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseTransfer.Application.Tests
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     https://stackoverflow.com/questions/42755274/visual-studio-2017-could-not-load-file-or-assembly-system-runtime-version-4
    ///     <PropertyGroup>
    ///         <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    ///         <GenerateBindingRedirectsOutputType>true</GenerateBindingRedirectsOutputType>
    ///     </PropertyGroup>
    /// </remarks>
    [TestClass]
    public class PostgreConnectionIntegrations
    {
        [TestMethod]
        public void GetVersion_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);
            Assert.IsFalse(postgreClient.GetVersion().HasException);
        }

        [TestMethod]
        public async Task IsValidConnection_Pass()
        {
            var result = await PostgreDatabaseConnectionExtensions.IsValidConnection(UtilityState.PostgreConnectionString);
            Assert.IsFalse(result.HasException);

            //result = await PostgreDatabaseConnectionExtensions.IsValidConnection("User ID=postgres;Password=03181991;Host=localhost;Port=5433;Database=postgres12-db;");
            //Assert.IsFalse(result.HasException);

            //result = await PostgreDatabaseConnectionExtensions.IsValidConnection("User ID=postgres;Password=03181991;Host=localhost;Port=5434;Database=postgres13-db;");
            //Assert.IsFalse(result.HasException);

            //result = await PostgreDatabaseConnectionExtensions.IsValidConnection("User ID=postgres;Password=master;Host=GSSW10P516;Port=5432;Database=postgres;");
            //Assert.IsFalse(result.HasException);
        }

        [TestMethod]
        public void TableExists_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (postgreClient.TableExists(tableName))
            {
                postgreClient.DeleteTable(tableName);
            }

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);
            var actianTableSchema = actianClient.GetTableSchema(tableName);

            postgreClient.CreateTable(actianTableSchema);

            Assert.IsTrue(postgreClient.TableExists(tableName));

            postgreClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void TableExists_Fail()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "any_forsaken_table";
            Assert.IsFalse(postgreClient.TableExists(tableName));
        }

        [TestMethod]
        public void GetTableNames_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (!postgreClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                postgreClient.CreateTable(actianTableSchema);
            }

            Assert.IsTrue(postgreClient.GetTableNames().Any());

            postgreClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void CreateTable_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (postgreClient.TableExists(tableName))
            {
                postgreClient.DeleteTable(tableName);
            }

            var actianClient = new ActianClient(UtilityState.ActianConnectionString);
            var actianTableSchema = actianClient.GetTableSchema(tableName);

            postgreClient.CreateTable(actianTableSchema);

            Assert.IsTrue(postgreClient.TableExists(tableName));

            postgreClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void GetTableSchema_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (!postgreClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                postgreClient.CreateTable(actianTableSchema);
            }

            Assert.IsNotNull(postgreClient.GetTableSchema(tableName));

            postgreClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void ClearTable_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (!postgreClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                postgreClient.CreateTable(actianTableSchema);
            }

            postgreClient.ClearTable(tableName);
            postgreClient.DeleteTable(tableName);
        }

        [TestMethod]
        public void DeleteTable_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (!postgreClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                postgreClient.CreateTable(actianTableSchema);
            }

            postgreClient.DeleteTable(tableName);
            Assert.IsTrue(!postgreClient.TableExists(tableName));
        }

        [TestMethod]
        public void ExecuteSqlStatement_Pass()
        {
            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            var tableName = "aa_conv_log";
            if (!postgreClient.TableExists(tableName))
            {
                var actianClient = new ActianClient(UtilityState.ActianConnectionString);
                var actianTableSchema = actianClient.GetTableSchema(tableName);

                postgreClient.CreateTable(actianTableSchema);
            }

            postgreClient.ExecuteSqlStatement($"select * from {tableName}");
            postgreClient.DeleteTable(tableName);
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

            var postgreClient = new PostgreClient(UtilityState.PostgreConnectionString);

            postgreClient.CreateTable(actianTableSchema);
            Assert.IsTrue(postgreClient.TableExists(tableName));

            postgreClient.BulkCopyDataTable(tableName, actianTableData);
            postgreClient.DeleteTable(tableName);
        }
    }
}