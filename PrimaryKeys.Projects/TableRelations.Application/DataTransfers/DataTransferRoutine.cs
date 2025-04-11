using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TableRelations.Application.DataTransfers.Models;
using TableRelations.Application.Infrastructure;

namespace TableRelations.Application.DataTransfers
{
    public static class DataTransferRoutine
    {
        /// <summary>
        ///     1. Loops thru each Actian Table
        ///     2. Determines if column is a primary key based on columnIndexList
        ///     3. Determines if column has a mask if DlConvert != null
        ///     4. Determines if column is foreign key based on propertyRelationList
        ///     5. Exports actianSqlTableList to a json file
        /// </summary>
        /// <remarks>
        ///     1. the data is inaccurate/flawed since a single person inputted the data without any validation/verification
        ///     2. Recursion/unlimited depth exists, in which case a single table/column selection could relate to 1-1000 other
        ///     tables
        /// </remarks>
        public static void Routine()
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext("Server=gss2k19sw;database=DATALAYERTRUNK;Uid=keymapper;Pwd=7c-hJNxZ;MultipleActiveResultSets=True");

            var sqlTableList = new List<SqlTable>();

            var actianTableList = dataLayerContext.ActianTables.Include(c => c.ActianColumns).OrderBy(c => c.Name).ToList();
            var actianTableColumnIndexList = dataLayerContext.ActianTableColumnIndices.ToList();

            var dlObjectPropertyList = dataLayerContext.DlObjectProperties.Include(c => c.DlObject).Include(c => c.DlConvert).ToList();

            var dlObjectPropertyRelationList = dataLayerContext.DlObjectPropertyRelations
                .Include(c => c.OriginalDlObject)
                .Include(c => c.OriginalDlObjectProperty)
                .Include(c => c.TransferDlObject)
                .Include(c => c.TransferDlObjectProperty)
                .ThenInclude(c => c.ActianTable)
                .Include(c => c.TransferDlObjectProperty)
                .ThenInclude(c => c.ActianColumn).ToList();

            foreach (var actianTable in actianTableList)
            {
                Console.WriteLine($"Gathering [{actianTable}] relations.");

                var sqlTable = new SqlTable
                {
                    Name = actianTable.Name
                };

                var actianTableColumnIndices = actianTableColumnIndexList.Where(c => c.ActianTableId == actianTable.Id).ToList();

                foreach (var actianColumn in actianTable.ActianColumns.Where(c => c.DataType < 200).OrderBy(c => c.Offset))
                {
                    var isPrimaryKey = actianTableColumnIndices.Exists(c => c.ActianColumnId == actianColumn.Id);

                    var sqlColumn = new SqlColumn
                    {
                        Name = actianColumn.Name,
                        IsPrimaryKey = isPrimaryKey
                    };

                    var dlObjectProperty = dlObjectPropertyList.SingleOrDefault(c => c.ActianTableId == actianTable.Id && c.ActianColumnId == actianColumn.Id && c.PreviousDlObjectPropertyId == null && c.DlObject.DlObjectStatusId >= 4);

                    if (dlObjectProperty != null)
                    {
                        if (dlObjectProperty.DlObjectPropertyDataTypeId.HasValue && dlObjectProperty.DlConvert != null)
                        {
                            sqlColumn.SqlColumnMask = new SqlColumnMask(dlObjectProperty.DlObjectPropertyDataTypeId.Value, dlObjectProperty.DlConvert.Name);
                        }

                        if (!isPrimaryKey)
                        {
                            var dlObjectPropertyRelations = dlObjectPropertyRelationList.Where(c => c.OriginalDlObjectId == dlObjectProperty.DlObjectId && c.OriginalDlObjectPropertyId == dlObjectProperty.Id).ToList();

                            foreach (var dlObjectPropertyRelation in dlObjectPropertyRelations.Where(c => c.TransferDlObjectProperty.ActianTable != null && c.TransferDlObjectProperty.ActianColumn != null))
                            {
                                sqlColumn.ForeignKeyTableNames.Add(dlObjectPropertyRelation.TransferDlObjectProperty.ActianTable.Name);
                            }
                        }
                    }

                    sqlTable.SqlColumns.Add(sqlColumn);
                }

                sqlTableList.Add(sqlTable);
            }

            OutputDataFiles(sqlTableList);
        }

        /// <summary>
        ///     Same as routine except with logic to handle versioning
        /// </summary>
        /// <param name="releaseVersionId">Release Version Id</param>
        public static void HistoryRoutine(int releaseVersionId)
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext("Server=gss2k19sw;database=DATALAYERTRUNK;Uid=keymapper;Pwd=7c-hJNxZ;MultipleActiveResultSets=True");

            var sqlTableList = new List<SqlTable>();

            var actianTableList = dataLayerContext.ActianTables.Include(c => c.ActianColumns).OrderBy(c => c.Name).ToList();
            var actianTableColumnIndexList = dataLayerContext.ActianTableColumnIndices.ToList();

            var dlObjectPropertyHistoryList = dataLayerContext.DlObjectPropertyHistories
                .Include(c => c.ActianTable)
                .Include(c => c.ActianColumn)
                .Where(c => c.ReleaseVersionId == releaseVersionId).ToList();

            var dlObjectHistoryList = dataLayerContext.DlObjectHistories.Where(c => c.ReleaseVersionId == releaseVersionId).ToList();
            var dlConvertHistoryList = dataLayerContext.DlConvertHistories.Where(c => c.ReleaseVersionId == releaseVersionId).ToList();
            var dlObjectPropertyRelationList = dataLayerContext.DlObjectPropertyRelations.ToList();

            foreach (var actianTable in actianTableList)
            {
                Console.WriteLine($"Gathering history [{actianTable}] relations.");

                var sqlTable = new SqlTable
                {
                    Name = actianTable.Name
                };

                var actianTableColumnIndices = actianTableColumnIndexList.Where(c => c.ActianTableId == actianTable.Id).ToList();

                foreach (var actianColumn in actianTable.ActianColumns.Where(c => c.DataType < 200).OrderBy(c => c.Offset))
                {
                    var isPrimaryKey = actianTableColumnIndices.Exists(c => c.ActianColumnId == actianColumn.Id);

                    var sqlColumn = new SqlColumn
                    {
                        Name = actianColumn.Name,
                        IsPrimaryKey = isPrimaryKey
                    };

                    var dlObjectProperty = dlObjectPropertyHistoryList.Where(c => c.ActianTableId == actianTable.Id && c.ActianColumnId == actianColumn.Id).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                    if (dlObjectProperty != null)
                    {
                        var dlConvert = dlConvertHistoryList.Where(c => c.Id == dlObjectProperty.DlConvertId).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                        if (dlObjectProperty.DlObjectPropertyDataTypeId.HasValue && dlConvert != null)
                        {
                            sqlColumn.SqlColumnMask = new SqlColumnMask(dlObjectProperty.DlObjectPropertyDataTypeId.Value, dlConvert.Name);
                        }

                        if (!isPrimaryKey)
                        {
                            var dlObjectPropertyRelations = dlObjectPropertyRelationList.Where(c => c.OriginalDlObjectId == dlObjectProperty.DlObjectId && c.OriginalDlObjectPropertyId == dlObjectProperty.Id).ToList();

                            foreach (var dlObjectPropertyRelation in dlObjectPropertyRelations)
                            {
                                var dlObjectPropertyRelationLookup = dlObjectPropertyHistoryList.Where(c => c.Id == dlObjectPropertyRelation.TransferDlObjectPropertyId).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                                if (dlObjectPropertyRelationLookup?.ActianTable != null && dlObjectPropertyRelationLookup?.ActianColumn != null)
                                {
                                    sqlColumn.ForeignKeyTableNames.Add($"{dlObjectPropertyRelationLookup.ActianTable.Name}");
                                }
                            }
                        }
                    }

                    sqlTable.SqlColumns.Add(sqlColumn);
                }

                sqlTableList.Add(sqlTable);
            }

            OutputDataFiles(sqlTableList);
        }

        private static void OutputDataFiles(List<SqlTable> sqlTableList)
        {
            var outputJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "global.shop.database.transfer", "table.relations.transfer.version.json");
            var jsonString = JsonConvert.SerializeObject(sqlTableList, Formatting.Indented);

            File.WriteAllText(outputJsonFilePath, jsonString);
        }
    }
}