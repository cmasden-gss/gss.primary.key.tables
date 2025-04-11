using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DatabaseTransfer.Application.Clients.Interfaces;
using DatabaseTransfer.Application.Models;
using DatabaseTransfer.Application.Postgres.Models;
using DatabaseTransfer.Application.Schemas.Extensions;
using DatabaseTransfer.Application.Schemas.Interfaces;
using Npgsql;

namespace DatabaseTransfer.Application.Postgres.Client
{
    public class PostgreClient : ITransferClient
    {
        private readonly string _connectionString;

        public PostgreClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection _dbConnection { get; set; }

        public NpgsqlConnection DbConnection => _dbConnection ?? (_dbConnection = new NpgsqlConnection(_connectionString));

        public Result<string> GetVersion()
        {
            OpenConnection();

            try
            {
                return Result<string>.Success(DbConnection.ServerVersion);
            }
            catch (Exception e)
            {
                return Result<string>.Fail(e);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool TableExists(string tableName)
        {
            OpenConnection();

            try
            {
                using (var command = DbConnection.CreateCommand())
                {
                    command.CommandText = $"select * from {tableName} limit 1";
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
                command.CommandText = "select table_name from information_schema.tables where table_schema='public' and table_type='BASE TABLE'";

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tableNameList.Add(dataReader["table_name"].ToString());
                    }
                }
            }

            CloseConnection();

            return tableNameList;
        }

        public ITableSchema GetTableSchema(string tableName)
        {
            DataTable schemaTable;

            try
            {
                OpenConnection();

                using (var command = DbConnection.CreateCommand())
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
                CloseConnection();
            }

            return new PostgreSqlTableSchema(tableName, schemaTable);
        }

        public void CreateTable(ITableSchema tableSchema)
        {
            OpenConnection();

            var createTableSchemaSql = GetCreateTableSchemaSql(tableSchema);

            if (!string.IsNullOrWhiteSpace(createTableSchemaSql))
            {
                using (var command = DbConnection.CreateCommand())
                {
                    command.CommandText = createTableSchemaSql;
                    command.ExecuteNonQuery();
                }
            }

            var createTableIndicesSql = GetCreateTableIndicesSql(tableSchema);

            if (!string.IsNullOrWhiteSpace(createTableIndicesSql))
            {
                using (var command = DbConnection.CreateCommand())
                {
                    command.CommandText = createTableIndicesSql;
                    command.ExecuteNonQuery();
                }
            }

            CloseConnection();
        }

        public void ClearTable(string tableName)
        {
            OpenConnection();

            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = $"delete from {tableName}";
                command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void ExecuteSqlStatement(string sqlStatement)
        {
            OpenConnection();

            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = sqlStatement;
                command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void DeleteTable(string tableName)
        {
            OpenConnection();

            using (var command = DbConnection.CreateCommand())
            {
                command.CommandText = $"drop table {tableName}";
                command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        /// <summary>
        ///     Bulk Copies [inserts] all data into a specified table [Leverages BeginBinaryImport]
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableData"></param>
        /// <remarks>
        ///     Try/Finally causes an object not initialized bug.
        /// </remarks>
        public void BulkCopyDataTable(string tableName, DataTable tableData)
        {
            OpenConnection();

            using (var writer = DbConnection.BeginBinaryImport($"copy {tableName} from STDIN (FORMAT BINARY)"))
            {
                foreach (DataRow dataRow in tableData.Rows)
                {
                    writer.StartRow();

                    for (var columnIndex = 0; columnIndex < tableData.Columns.Count; columnIndex++)
                    {
                        var dataColumnValue = dataRow[columnIndex];

                        if (dataColumnValue is string)
                        {
                            dataColumnValue = Regex.Replace(dataRow[columnIndex].ToString(), @"[^\u0020-\u007E]", string.Empty);
                        }

                        writer.Write(dataColumnValue);
                    }
                }

                writer.Complete();
            }

            CloseConnection();
        }

        private void OpenConnection()
        {
            CloseConnection();
            DbConnection.Open();
        }

        private void CloseConnection()
        {
            if (DbConnection.State != ConnectionState.Closed)
            {
                DbConnection.Close();
            }
        }

        private string GetCreateTableSchemaSql(ITableSchema tableSchema)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"create table {tableSchema.Name} (");

            foreach (var columnSchema in tableSchema.ColumnSchemas.OrderBy(c => c.Ordinal))
            {
                stringBuilder.AppendLine($"\"{columnSchema.Name}\" {columnSchema.GetPostgreColumnSchemaSqlDataType()},");
            }

            stringBuilder.Remove(stringBuilder.Length - 3, 1);
            stringBuilder.AppendLine(")");

            return stringBuilder.ToString();
        }

        private string GetCreateTableIndicesSql(ITableSchema tableSchema)
        {
            var stringBuilder = new StringBuilder();

            foreach (var tableIndex in tableSchema.TableIndices.Where(c => c.ColumnIndices.All(ci => tableSchema.ColumnSchemas.Any(cs => cs.Name.Equals(ci.Name)))))
            {
                stringBuilder.AppendLine($"create index {tableIndex.Name} on {tableSchema.Name} (");

                foreach (var columnSchema in tableIndex.ColumnIndices.OrderBy(c => c.Ordinal))
                {
                    stringBuilder.AppendLine($"\"{columnSchema.Name}\",");
                }

                stringBuilder.Remove(stringBuilder.Length - 3, 1);
                stringBuilder.AppendLine(");");
            }

            return stringBuilder.ToString();
        }
    }
}