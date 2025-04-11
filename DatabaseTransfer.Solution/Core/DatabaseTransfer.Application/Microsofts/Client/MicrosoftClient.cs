using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DatabaseTransfer.Application.Clients.Interfaces;
using DatabaseTransfer.Application.Extensions;
using DatabaseTransfer.Application.Microsofts.Models;
using DatabaseTransfer.Application.Models;
using DatabaseTransfer.Application.Schemas.Extensions;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Microsofts.Client
{
    public class MicrosoftClient : ITransferClient
    {
        private readonly string _connectionString;

        public MicrosoftClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection _dbConnection { get; set; }

        public DbConnection DbConnection
        {
            get
            {
                if (_dbConnection != null)
                {
                    return _dbConnection;
                }

                var providerFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                _dbConnection = providerFactory.CreateConnection();
                _dbConnection.ConnectionString = _connectionString;

                return _dbConnection;
            }
        }

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
                command.CommandText = "select table_name from information_schema.tables where table_type ='BASE TABLE' order by table_name";

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
            return new MicrosoftSqlTableSchema(tableName, DbConnection.GetTableSchema(tableName));
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
        ///     Bulk Copies [inserts] all data into a specified table [Leverages SqlBulkCopy]
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableData"></param>
        public void BulkCopyDataTable(string tableName, DataTable tableData)
        {
            var bulkCopy = new SqlBulkCopy(_connectionString)
            {
                BulkCopyTimeout = 0,
                DestinationTableName = tableName
            };

            bulkCopy.WriteToServer(tableData);
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
                stringBuilder.AppendLine($"[{columnSchema.Name}] {columnSchema.GetMicrosoftColumnSchemaSqlDataType()},");
            }

            stringBuilder.AppendLine(")");

            return stringBuilder.ToString();
        }

        private string GetCreateTableIndicesSql(ITableSchema tableSchema)
        {
            var stringBuilder = new StringBuilder();

            foreach (var tableIndex in tableSchema.TableIndices.Where(c => c.ColumnIndices.All(ci => tableSchema.ColumnSchemas.Any(cs => cs.Name.Equals(ci.Name)))).ToList())
            {
                stringBuilder.AppendLine($"create index {tableIndex.Name} on {tableSchema.Name} (");

                foreach (var columnSchema in tableIndex.ColumnIndices.OrderBy(c => c.Ordinal))
                {
                    stringBuilder.AppendLine($"[{columnSchema.Name}],");
                }

                stringBuilder.Remove(stringBuilder.Length - 3, 1);
                stringBuilder.AppendLine(");");
            }

            return stringBuilder.ToString();
        }
    }
}