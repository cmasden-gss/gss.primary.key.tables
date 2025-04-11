using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableRelations.Application.Infrastructure;

namespace TableRelations.Integration.Tests
{
    [TestClass]
    public class DataLayerContextSqlServerTests
    {
        private string _connectionString => "Server=gss2k19sw;database=DATALAYERTRUNK;Uid=keymapper;Pwd=7c-hJNxZ;MultipleActiveResultSets=True";

        [TestMethod]
        public void DLObjectList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjects.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectHistoryList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectHistories.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectPropertyList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectProperties.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectPropertyHistoryList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectPropertyHistories.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectPropertyDescriptionList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectPropertyDescriptions.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectPropertyDescriptionHistoryList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectPropertyDescriptionHistories.ToList().Count > 0);
        }

        [TestMethod]
        public void DLObjectPropertyRelationList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlObjectPropertyRelations.ToList().Count > 0);
        }

        [TestMethod]
        public void DLConvertList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlConverts.ToList().Count > 0);
        }

        [TestMethod]
        public void DLConvertHistoryList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.DlConvertHistories.ToList().Count > 0);
        }

        [TestMethod]
        public void ActianTableList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.ActianTables.ToList().Count > 0);
        }

        [TestMethod]
        public void ActianColumnList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.ActianColumns.ToList().Count > 0);
        }

        [TestMethod]
        public void ActianTableColumnIndexList_Pass()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext(_connectionString);

            Assert.IsTrue(dataLayerContext.ActianTableColumnIndices.ToList().Count > 0);
        }
    }
}