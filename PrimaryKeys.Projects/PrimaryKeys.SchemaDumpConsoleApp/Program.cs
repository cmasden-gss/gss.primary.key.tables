﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pervasive.Data.SqlClient;
using PrimaryKeys.Application;
using PrimaryKeys.Application.Models;
using TableRelations.Application.Infrastructure;

namespace PrimaryKeys.SchemaDumpConsoleApp
{
    public class ActianClient
    {
        private readonly string _connectionString;
        private PsqlConnection _dbConnection;

        public ActianClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PsqlConnection DbConnection
        {
            get
            {
                if (_dbConnection != null)
                    return _dbConnection;

                _dbConnection = new PsqlConnection(_connectionString);

                return _dbConnection;
            }
        }

        private void OpenConnection()
        {
            CloseConnection();
            DbConnection.Open();
        }

        private void CloseConnection()
        {
            if (DbConnection.State != ConnectionState.Closed)
                DbConnection.Close();
        }

        public bool TableExists(string tableName)
        {
            OpenConnection();
            try
            {
                using (var command = DbConnection.CreateCommand())
                {
                    command.CommandText = $"select top 1 * from {tableName}";
                    command.ExecuteScalar();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<string> GetTableNames()
        {
            var tableNameList = new List<string>();
            OpenConnection();
            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = "select xf$name from x$file where xf$flags in (64,0) order by xf$name";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tableNameList.Add(((string)dataReader["Xf$Name"]).Trim());
                    }
                }
            }
            CloseConnection();
            return tableNameList;
        }

        public List<string> GetTableViewNames()
        {
            var tableViewList = new List<string>();
            OpenConnection();
            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = "select xv$name from x$view order by xv$name";
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tableViewList.Add(((string)dataReader["Xv$Name"]).Trim());
                    }
                }
            }
            CloseConnection();
            return tableViewList;
        }

        public ActianSqlTableSchema GetTableSchema(string tableName)
        {
            var indexCommandText = $"select i.xi$number as id, i.xi$part as ordinal, f.xe$name as name from x$file as t left join x$index as i on t.xf$id = i.xi$file left join x$field as f on i.xi$field = f.xe$id where t.xf$name = '{tableName}'";

            try
            {
                return new ActianSqlTableSchema(tableName, _dbConnection.GetTableSchema(tableName), DbConnection.GetTableData(indexCommandText));
            }
            catch (Exception e)
            {
                return new ActianSqlTableSchema()
                {
                    Name = tableName,
                    SqlColumns = new List<SqlColumnInformation>() { new SqlColumnInformation() { Name = $"ERROR - {e.Message}", Description = $"{e}" } }
                };
            }
        }
    }

    public class ActianSqlTableSchema
    {
        public string Name { get; set; }
        public List<SqlColumnInformation> SqlColumns { get; set; } = new List<SqlColumnInformation>();

        public ActianSqlTableSchema()
        {
            SqlColumns = new List<SqlColumnInformation>();
            // TableIndices = new List<ITableIndex>();
        }

        public ActianSqlTableSchema(string name, DataTable schemaTable, DataTable indexTable)
        {
            Name = name;

            SqlColumns = new List<SqlColumnInformation>();
            // TableIndices = new List<ITableIndex>();

            GetColumnSchemasFromTableSchema(schemaTable);
            // GetTableIndexesFromIndexTable(indexTable);
        }

        //private void GetTableIndexesFromIndexTable(DataTable indexTable)
        //{
        //    if (indexTable.Rows.Count == 0)
        //    {
        //        return;
        //    }

        //    var tableIndex = new TableIndex(indexTable.Rows[0]["id"].ToString(), Name);
        //    TableIndices.Add(tableIndex);

        //    foreach (DataRow indexTableRow in indexTable.Rows)
        //    {
        //        var currentId = indexTableRow["id"].ToString();

        //        if (currentId != tableIndex.Id)
        //        {
        //            tableIndex = new TableIndex(currentId, Name);
        //            TableIndices.Add(tableIndex);
        //        }

        //        tableIndex.ColumnIndices.Add(new ColumnIndex
        //        {
        //            Name = indexTableRow["name"].ToString().Trim(),
        //            Ordinal = !string.IsNullOrWhiteSpace(indexTableRow["ordinal"].ToString()) ? int.Parse(indexTableRow["ordinal"].ToString()) : 0
        //        });
        //    }
        //}

        private void GetColumnSchemasFromTableSchema(DataTable schemaTable)
        {
            foreach (DataRow schemaTableRow in schemaTable.Rows)
            {
                // there is a thing called bad data and idiots.
                if (string.IsNullOrWhiteSpace(schemaTableRow["ColumnName"].ToString()))
                {
                    continue;
                }

                SqlColumns.Add(new SqlColumnInformation
                {
                    Name = schemaTableRow["ColumnName"].ToString(),
                    Ordinal = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnOrdinal"].ToString()) ? int.Parse(schemaTableRow["ColumnOrdinal"].ToString()) : 0,
                    Size = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnSize"].ToString()) ? int.Parse(schemaTableRow["ColumnSize"].ToString()) : 0,
                    DataType = (Type)schemaTableRow["DataType"],
                    AllowNull = !string.IsNullOrWhiteSpace(schemaTableRow["AllowDbNull"].ToString()) && bool.Parse(schemaTableRow["AllowDbNull"].ToString()),
                    IsPrimaryKey = !string.IsNullOrWhiteSpace(schemaTableRow["IsKey"].ToString()) && bool.Parse(schemaTableRow["IsKey"].ToString()),
                    IsUnique = !string.IsNullOrWhiteSpace(schemaTableRow["IsUnique"].ToString()) && bool.Parse(schemaTableRow["IsUnique"].ToString()),
                });
            }
        }
    }

    public class SqlColumnInformation
    {
        public string Name { get; set; }

        public int Ordinal { get; set; }

        public int Size { get; set; }

        public Type DataType { get; set; }

        public bool AllowNull { get; set; }

        public bool IsUnique { get; set; }

        public bool IsPrimaryKey { get; set; }

        public string Description { get; set; }

        public List<string> ForeignKeyTableAndColumnNames { get; set; } = new List<string>();
    }

    public static class KeyMapperRoutine
    {
        public static void Routine()
        {
            var releaseVersionId = 44;

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
            string outputDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "global.shop.database.transfer");
            Directory.CreateDirectory(outputDir);
            string outputJsonFilePath = Path.Combine(outputDir, "table.relations.information.version.json");
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sqlTableInformationList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(outputJsonFilePath, jsonString);
        }
    }

    public class SqlTableInformation
    {
        public string Name { get; set; }
        public List<SqlColumnInformation> SqlColumns { get; set; } = new List<SqlColumnInformation>();

        public string BuildOutput()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {Name}");
            foreach (var col in SqlColumns)
            {
                sb.AppendLine($"  Column: {col.Name}, PK:{col.IsPrimaryKey}");
                if (col.ForeignKeyTableAndColumnNames.Any())
                {
                    sb.AppendLine("    Foreign Keys: " + string.Join(", ", col.ForeignKeyTableAndColumnNames));
                }
            }
            return sb.ToString();
        }
    }

    public static class SchemaConversion
    {
        /// <summary>
        /// Converts a live ActianSqlTableSchema into a list of ColumnRow objects.
        /// </summary>
        public static List<ColumnRow> ConvertToColumnRows(ActianSqlTableSchema schema)
        {
            List<ColumnRow> rows = new List<ColumnRow>();
            foreach (var col in schema.SqlColumns)
            {
                var row = new ColumnRow
                {
                    TableName = schema.Name,
                    ColumnName = col.Name,
                    ColumnIndex = col.Ordinal,
                    IsPrimaryKey = col.IsPrimaryKey,
                    IsForeignKey = col.ForeignKeyTableAndColumnNames.Any(),
                    IsMasterKey = false
                };
                row.IsNone = !(row.IsPrimaryKey || row.IsForeignKey || row.IsMasterKey);
                if (row.IsForeignKey)
                {
                    // Use the first foreign key mapping.
                    var fk = col.ForeignKeyTableAndColumnNames.First();
                    if (!string.IsNullOrEmpty(fk))
                    {
                        var parts = fk.Split('.');
                        if (parts.Length == 2)
                        {
                            row.ForeignKeyTable = parts[0];
                            row.ForeignKeyField = parts[1];
                        }
                    }
                }
                rows.Add(row);
            }
            return rows;
        }
    }

    // ======================================================
    // HELPER: APPLY TABLE RELATIONS
    // ======================================================
    /// <summary>
    /// Reads the table relation JSON file (written by KeyMapperRoutine) and applies relational
    /// information (such as additional foreign key details) to each ColumnRow in our new schema.
    /// </summary>
    public static class TableRelationsHelper
    {
        /// <summary>
        /// Reads the table relation JSON file into a list of SqlTableInformation.
        /// </summary>
        public static List<SqlTableInformation> ReadTableRelationsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<SqlTableInformation>();
            string json = File.ReadAllText(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SqlTableInformation>>(json);
        }
    }

    // ======================================================
    // MAIN PROGRAM
    // ======================================================
    internal class Program
    {
        // File path for saving the schema.
        private static readonly string schemaFilePath = "actiantableschema.json";

        // File path for the table relations (written by KeyMapperRoutine).
        private static readonly string tableRelationsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "global.shop.database.transfer",
            "table.relations.information.version.json");

        // We hold schema per table: table name -> list of ColumnRow objects.
        private static List<ColumnRow> OldSchemas = new List<ColumnRow>();

        private static List<ColumnRow> NewSchemas = new List<ColumnRow>();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("=== Schema Update Console App ===");

            // ----- 0. Run KeyMapperRoutine to gather table relation info -----
            // This call runs the routine that writes table relation information to file.
            Console.WriteLine("Running KeyMapperRoutine to gather relation information...");
            //KeyMapperRoutine.Routine();

            // Read relations from the file.
            List<SqlTableInformation> tableRelations = TableRelationsHelper.ReadTableRelationsFromFile(tableRelationsFilePath);
            Console.WriteLine($"Table relations loaded: {tableRelations.Count} table(s) found.");

            // ----- 1. Retrieve live table names from the database -----
            //string connectionString = "Server DSN=GlobalPLA;UID=Master;PWD=master;Host=gssw10p620";
            string connectionString = "Server DSN=Global8IY;UID=Master;PWD=master;Host=gss2k19clinic5";
            ActianClient actianClient = new ActianClient(connectionString);
            List<string> tableNames = actianClient.GetTableNames();

            // Remove tables that start with "Y_", "Z_" or "BI_"
            tableNames = tableNames
                .Where(t => !(t.StartsWith("Y_", StringComparison.OrdinalIgnoreCase) ||
                              t.StartsWith("Z_", StringComparison.OrdinalIgnoreCase) ||
                              t.StartsWith("BI_", StringComparison.OrdinalIgnoreCase)))
                .ToList();

            Console.WriteLine("Tables found:");
            tableNames.ForEach(t => Console.WriteLine("  " + t));

            // ----- 2. For each table, get the live schema and convert to ColumnRow objects -----
            foreach (var table in tableNames)
            {
                Console.WriteLine($"\nReading live schema for table: {table}");
                ActianSqlTableSchema liveSchema = actianClient.GetTableSchema(table);

                var relations = tableRelations.SingleOrDefault(c => c.Name.Equals(liveSchema.Name));

                foreach (var column in liveSchema.SqlColumns)
                {
                    // LOL Filler | Column Names are not qunieuqe
                    var columnRelations = relations?.SqlColumns.FirstOrDefault(c => c.Name.Equals(column.Name));

                    if (columnRelations?.ForeignKeyTableAndColumnNames?.Any() == true)
                    {
                        var stop = 0;
                        column.ForeignKeyTableAndColumnNames = columnRelations?.ForeignKeyTableAndColumnNames;
                    }
                }

                List<ColumnRow> columnRows = SchemaConversion.ConvertToColumnRows(liveSchema);
                NewSchemas.AddRange(columnRows);
            }

            // ----- 4. Use DbContext
            await UpdateDbContextFromNewSchemasAsync(NewSchemas);

            Console.ReadKey();
        }

        public static async Task UpdateDbContextFromNewSchemasAsync(List<ColumnRow> newSchemas)
        {
            var ConnectionString =
       "Server=gss2k19awssql;Database=IHOP_Net;User Id=PrimaryKeyTool;Password=zx2pHC&Q$M2z56g;";

            var options = new DbContextOptionsBuilder<PrimaryKeysContext>()
                              .UseSqlServer(ConnectionString)
                              .Options;

            using (var context = new PrimaryKeysContext(options))
            {
                // Ensure the database and tables exist.
                // context.Database.EnsureCreated();

                // Load the current schema records from the database.
                var existingRecords = await context.ColumnRows
                                            .Include(cr => cr.AuditHistory)
                                            .ToListAsync();

                // Get the current user (update with your actual authentication logic)
                string currentUser = "Importer";//Environment.UserName;

                // Process each new schema record.
                foreach (var newCol in newSchemas)
                {
                    // Attempt to match an existing record by TableName and ColumnName.
                    var existing = existingRecords.FirstOrDefault(c =>
                                     c.TableName.Equals(newCol.TableName, StringComparison.OrdinalIgnoreCase) &&
                                     c.ColumnName.Equals(newCol.ColumnName, StringComparison.OrdinalIgnoreCase));

                    if (existing == null)
                    {
                        // No existing record: treat as a new column.
                        newCol.AuditHistory.Add(new AuditEntry
                        {
                            ChangedOn = DateTime.Now,
                            ChangedBy = currentUser,
                            Description = "New column added",
                            ColumnName = newCol.ColumnName,
                            PreviousValue = string.Empty,
                            NewValue = newCol.ColumnName
                        });
                        context.ColumnRows.Add(newCol);
                    }
                    else
                    {
                        // For each property to audit, check for changes and, if any, create a distinct audit entry.
                        // (You can add or remove property comparisons as needed.)
                        if (!existing.TableName.Equals(newCol.TableName, StringComparison.OrdinalIgnoreCase))
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "Table name changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.TableName,
                                NewValue = newCol.TableName
                            });
                            existing.TableName = newCol.TableName;
                        }
                        if (!existing.ColumnName.Equals(newCol.ColumnName, StringComparison.OrdinalIgnoreCase))
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "Column name changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.ColumnName,
                                NewValue = newCol.ColumnName
                            });
                            existing.ColumnName = newCol.ColumnName;
                        }
                        if (existing.IsNone != newCol.IsNone)
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "IsNone flag changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.IsNone.ToString(),
                                NewValue = newCol.IsNone.ToString()
                            });
                            existing.IsNone = newCol.IsNone;
                        }
                        if (existing.IsMasterKey != newCol.IsMasterKey)
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "MasterKey flag changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.IsMasterKey.ToString(),
                                NewValue = newCol.IsMasterKey.ToString()
                            });
                            existing.IsMasterKey = newCol.IsMasterKey;
                        }
                        if (existing.IsPrimaryKey != newCol.IsPrimaryKey)
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "PrimaryKey flag changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.IsPrimaryKey.ToString(),
                                NewValue = newCol.IsPrimaryKey.ToString()
                            });
                            existing.IsPrimaryKey = newCol.IsPrimaryKey;
                        }
                        if (existing.IsForeignKey != newCol.IsForeignKey)
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "ForeignKey flag changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.IsForeignKey.ToString(),
                                NewValue = newCol.IsForeignKey.ToString()
                            });
                            existing.IsForeignKey = newCol.IsForeignKey;
                        }
                        if (!string.Equals(existing.ForeignKeyTable, newCol.ForeignKeyTable, StringComparison.OrdinalIgnoreCase))
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "ForeignKeyTable changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.ForeignKeyTable,
                                NewValue = newCol.ForeignKeyTable
                            });
                            existing.ForeignKeyTable = newCol.ForeignKeyTable;
                        }
                        if (!string.Equals(existing.ForeignKeyField, newCol.ForeignKeyField, StringComparison.OrdinalIgnoreCase))
                        {
                            existing.AuditHistory.Add(new AuditEntry
                            {
                                ChangedOn = DateTime.Now,
                                ChangedBy = currentUser,
                                Description = "ForeignKeyField changed",
                                ColumnName = existing.ColumnName,
                                PreviousValue = existing.ForeignKeyField,
                                NewValue = newCol.ForeignKeyField
                            });
                            existing.ForeignKeyField = newCol.ForeignKeyField;
                        }
                    }
                }

                // Save all changes to the database.
                await context.SaveChangesAsync();
                Console.WriteLine("Database updated with new schema and audit history.");
            }
        }
    }
}

public static class DbConnectionExtensions
{
    /// <summary>
    ///     Returns the table data
    /// </summary>
    /// <param name="connection">the database connection</param>
    /// <param name="commandText">the command text</param>
    /// <returns></returns>
    public static DataTable GetTableData(this PsqlConnection connection, string commandText)
    {
        connection.Open();

        var dataTable = new DataTable();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"{commandText}";

            dataTable.Load(command.ExecuteReader());
        }

        connection.Close();

        return dataTable;
    }

    /// <summary>
    ///     Returns the table schema
    /// </summary>
    /// <param name="connection">the database connection</param>
    /// <param name="tableName">the table name</param>
    /// <returns></returns>
    public static DataTable GetTableSchema(this PsqlConnection connection, string tableName)
    {
        DataTable schemaTable;

        try
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"select * from {tableName}";

                using (var dataReader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    schemaTable = dataReader.GetSchemaTable();
                }
            }
        }
        finally
        {
            connection.Close();
        }

        return schemaTable;
    }
}

public static class LinqExtensions
{
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
    {
        return items.GroupBy(property).Select(x => x.First());
    }
}