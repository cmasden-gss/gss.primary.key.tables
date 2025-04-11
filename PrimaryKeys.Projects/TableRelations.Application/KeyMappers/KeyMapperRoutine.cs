using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TableRelations.Application.Extensions;
using TableRelations.Application.Infrastructure;
using TableRelations.Application.KeyMappers.Models;

namespace TableRelations.Application.KeyMappers
{
    public static class KeyMapperRoutine
    {
        /// <summary>
        ///     1. Loops thru each Actian Table
        ///     2. Determines if column is a primary key based on columnIndexList
        ///     3. Determines if column is foreign key based on propertyRelationList
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

            var sqlTableInformationList = new List<SqlTableInformation>();

            var actianTableList = dataLayerContext.ActianTables.Include(c => c.ActianColumns).OrderBy(c => c.Name).ToList();
            var actianTableColumnIndexList = dataLayerContext.ActianTableColumnIndices.ToList();

            var dlObjectPropertyList = dataLayerContext.DlObjectProperties.Include(c => c.DlObject).ToList();

            var dlObjectPropertyRelationList = dataLayerContext.DlObjectPropertyRelations
                .Include(c => c.OriginalDlObject)
                .Include(c => c.OriginalDlObjectProperty)
                .Include(c => c.TransferDlObject)
                .Include(c => c.TransferDlObjectProperty)
                .ThenInclude(c => c.ActianTable)
                .Include(c => c.TransferDlObjectProperty)
                .ThenInclude(c => c.ActianColumn).ToList();

            var dlObjectPropertyDescriptionList = dataLayerContext.DlObjectPropertyDescriptions.ToList();

            foreach (var actianTable in actianTableList)
            {
                Console.WriteLine($"Gathering [{actianTable}] relation information.");

                var sqlTableInformation = new SqlTableInformation
                {
                    Name = actianTable.Name
                };

                var actianTableColumnIndices = actianTableColumnIndexList.Where(c => c.ActianTableId == actianTable.Id).ToList();

                foreach (var actianColumn in actianTable.ActianColumns.Where(c => c.DataType < 200).OrderBy(c => c.Offset))
                {
                    var isPrimaryKey = actianTableColumnIndices.Exists(c => c.ActianColumnId == actianColumn.Id);

                    var dlObjectProperties = dlObjectPropertyList.Where(c => c.ActianTableId == actianTable.Id && c.ActianColumnId == actianColumn.Id && c.DlObject.DlObjectStatusId != null && c.DlObject.DlObjectStatusId != -1).ToList();

                    if (dlObjectProperties.Count > 0)
                    {
                        foreach (var dlObjectProperty in dlObjectProperties)
                        {
                            var dlObjectPropertyDescription = dlObjectPropertyDescriptionList.SingleOrDefault(c => c.Id == dlObjectProperty.Id);

                            var sqlColumnInformation = new SqlColumnInformation
                            {
                                Name = actianColumn.Name,
                                IsPrimaryKey = isPrimaryKey,
                                Description = dlObjectPropertyDescription?.Name
                            };

                            var dlObjectPropertyRelations = dlObjectPropertyRelationList.Where(c => c.OriginalDlObjectId == dlObjectProperty.DlObjectId && c.OriginalDlObjectPropertyId == dlObjectProperty.Id).ToList();

                            foreach (var dlObjectPropertyRelation in dlObjectPropertyRelations.Where(c => c.TransferDlObjectProperty.ActianTable != null && c.TransferDlObjectProperty.ActianColumn != null))
                            {
                                sqlColumnInformation.ForeignKeyTableAndColumnNames.Add($"{dlObjectPropertyRelation.TransferDlObjectProperty.ActianTable.Name}.{dlObjectPropertyRelation.TransferDlObjectProperty.ActianColumn.Name}");
                            }

                            sqlTableInformation.SqlColumns.Add(sqlColumnInformation);
                        }
                    }
                    else
                    {
                        sqlTableInformation.SqlColumns.Add(new SqlColumnInformation
                        {
                            Name = actianColumn.Name,
                            IsPrimaryKey = isPrimaryKey
                        });
                    }
                }

                sqlTableInformationList.Add(sqlTableInformation);
            }

            OutputDataFiles(sqlTableInformationList);
        }

        /// <summary>
        ///     Same as routine except with logic to handle versioning
        /// </summary>
        /// <param name="releaseVersionId">Release Version Id</param>
        public static void HistoryRoutine(int releaseVersionId)
        {
            var dataLayerContextFactory = new DataLayerContextFactory();
            var dataLayerContext = dataLayerContextFactory.CreateDbContext("Server=gss2k19sw;database=DATALAYERTRUNK;Uid=keymapper;Pwd=7c-hJNxZ;MultipleActiveResultSets=True");

            var sqlTableInformationList = new List<SqlTableInformation>();

            var actianTableList = dataLayerContext.ActianTables.Include(c => c.ActianColumns).OrderBy(c => c.Name).ToList();
            var actianTableColumnIndexList = dataLayerContext.ActianTableColumnIndices.ToList();

            var dlObjectPropertyHistoryList = dataLayerContext.DlObjectPropertyHistories
                .Include(c => c.ActianTable)
                .Include(c => c.ActianColumn)
                .Where(c => c.ReleaseVersionId == releaseVersionId).ToList();

            var dlObjectHistoryList = dataLayerContext.DlObjectHistories.Where(c => c.ReleaseVersionId == releaseVersionId).ToList();
            var dlObjectPropertyRelationList = dataLayerContext.DlObjectPropertyRelations.ToList();
            var dlObjectPropertyDescriptionHistoryList = dataLayerContext.DlObjectPropertyDescriptionHistories.Where(c => c.ReleaseVersionId == releaseVersionId).ToList();

            foreach (var actianTable in actianTableList)
            {
                Console.WriteLine($"Gathering history [{actianTable}] relation information.");

                var sqlTableInformation = new SqlTableInformation
                {
                    Name = actianTable.Name
                };

                var actianTableColumnIndices = actianTableColumnIndexList.Where(c => c.ActianTableId == actianTable.Id).ToList();

                foreach (var actianColumn in actianTable.ActianColumns.Where(c => c.DataType < 200).OrderBy(c => c.Offset))
                {
                    var isPrimaryKey = actianTableColumnIndices.Exists(c => c.ActianColumnId == actianColumn.Id);

                    var dlObjectProperties = dlObjectPropertyHistoryList.Where(c => c.ActianTableId == actianTable.Id && c.ActianColumnId == actianColumn.Id).OrderByDescending(c => c.HistoryDateTime).ToList();

                    dlObjectProperties = dlObjectProperties.DistinctBy(c => c.Id).ToList();

                    if (dlObjectProperties.Count > 0)
                    {
                        foreach (var dlObjectProperty in dlObjectProperties)
                        {
                            var dlObjectLookup = dlObjectHistoryList.Where(c => c.DlObjectStatusId != null && c.DlObjectStatusId != -1 && c.Id == dlObjectProperty.DlObjectId).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                            if (dlObjectLookup == null)
                            {
                                continue;
                            }

                            var dlObjectPropertyDescription = dlObjectPropertyDescriptionHistoryList.Where(c => c.Id == dlObjectProperty.Id).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                            var sqlColumnInformation = new SqlColumnInformation
                            {
                                Name = actianColumn.Name,
                                IsPrimaryKey = isPrimaryKey,
                                Description = dlObjectPropertyDescription?.Name
                            };

                            var dlObjectPropertyRelations = dlObjectPropertyRelationList.Where(c => c.OriginalDlObjectId == dlObjectProperty.DlObjectId && c.OriginalDlObjectPropertyId == dlObjectProperty.Id).ToList();

                            foreach (var dlObjectPropertyRelation in dlObjectPropertyRelations)
                            {
                                var dlObjectPropertyRelationLookup = dlObjectPropertyHistoryList.Where(c => c.Id == dlObjectPropertyRelation.TransferDlObjectPropertyId).OrderByDescending(c => c.HistoryDateTime).Take(1).FirstOrDefault();

                                if (dlObjectPropertyRelationLookup?.ActianTable != null && dlObjectPropertyRelationLookup?.ActianColumn != null)
                                {
                                    sqlColumnInformation.ForeignKeyTableAndColumnNames.Add($"{dlObjectPropertyRelationLookup.ActianTable.Name}.{dlObjectPropertyRelationLookup.ActianColumn.Name}");
                                }
                            }

                            sqlTableInformation.SqlColumns.Add(sqlColumnInformation);
                        }
                    }
                    else
                    {
                        sqlTableInformation.SqlColumns.Add(new SqlColumnInformation
                        {
                            Name = actianColumn.Name,
                            IsPrimaryKey = isPrimaryKey
                        });
                    }
                }

                sqlTableInformationList.Add(sqlTableInformation);
            }

            OutputDataFiles(sqlTableInformationList);
        }

        private static void OutputDataFiles(List<SqlTableInformation> sqlTableInformationList)
        {
            var outputJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "global.shop.database.transfer", "table.relations.information.version.json");
            var jsonString = JsonConvert.SerializeObject(sqlTableInformationList, Formatting.Indented);

            File.WriteAllText(outputJsonFilePath, jsonString);

            var outputTextFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "global.shop.database.transfer", "table.relations.information.version.txt");

            var textStringBuilder = new StringBuilder();
            sqlTableInformationList.ForEach(tableInformation => { textStringBuilder.Append(tableInformation.BuildOutput()); });

            File.WriteAllText(outputTextFilePath, textStringBuilder.ToString());
        }
    }
}