using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using DatabaseTransfer.Application.Actians.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Actians.Client
{
    /// <summary>
    ///     Actian|Pervasive Client
    /// </summary>
    public class ActianClient
    {
        private readonly string _connectionString;

        public ActianClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection _dbConnection { get; set; }

        /// <summary>
        ///     Pervasive.Data.SqlClient Database Connection
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                if (_dbConnection != null)
                {
                    return _dbConnection;
                }

                var providerFactory = DbProviderFactories.GetFactory("Pervasive.Data.SqlClient");
                _dbConnection = providerFactory.CreateConnection();
                _dbConnection.ConnectionString = _connectionString;

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
            {
                DbConnection.Close();
            }
        }

        /// <summary>
        ///     Does the table exist
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Returns a list of table names [even if all of them don't exist]
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     I added verification to see if all table existed yet it caused a large performance impact and scrapped it.
        /// </remarks>
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

        /// <summary>
        ///     Returns a list of table view names
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     Returns the table schema
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns></returns>
        public ActianSqlTableSchema GetTableSchema(string tableName)
        {
            var indexCommandText = $"select i.xi$number as id, i.xi$part as ordinal, f.xe$name as name from x$file as t left join x$index as i on t.xf$id = i.xi$file left join x$field as f on i.xi$field = f.xe$id where t.xf$name = '{tableName}'";

            return new ActianSqlTableSchema(tableName, DbConnection.GetTableSchema(tableName), DbConnection.GetTableData(indexCommandText));
        }

        /// <summary>
        ///     Returns the data from a table
        /// </summary>
        /// <param name="tableSchema">the table schema</param>
        /// <returns></returns>
        public DataTable GetTableDataFromTableSchema(ITableSchema tableSchema)
        {
            return DbConnection.GetTableData(GetSelectTableSchemaSql(tableSchema));
        }

        /// <summary>
        ///     Returns top 500 rows of data from a table
        /// </summary>
        /// <param name="tableSchema">the table schema configuration</param>
        /// <returns></returns>
        public DataTable GetPreviewTableDataFromTableSchema(TableSchemaConfiguration tableSchema)
        {
            return DbConnection.GetTableData(GetSelectPreviewTableSchemaSql(tableSchema));
        }

        /// <summary>
        ///     Returns the data from a table while transforming the data in regards to the associated masks with a filter based on
        ///     the synchronous datetime
        /// </summary>
        /// <param name="tableSchema">the table schema</param>
        /// <param name="synchronousDateTime">the synchronous datetime</param>
        /// <returns></returns>
        public DataTable GetTableDataWithMasksFromTableSchema(TableSchemaConfiguration tableSchema, DateTime synchronousDateTime)
        {
            var dataTable = DbConnection.GetTableData(GetSelectTableSchemaSql(tableSchema, synchronousDateTime));

            var dataTableWithMasks = GetTableDataWithMasksFromTableSchema(tableSchema, GetTableDataWithValidCellData(dataTable));

            if (!tableSchema.SynchronousColumnSchema.HasMask)
            {
                return dataTableWithMasks;
            }

            var dataTableWithMasksWithFilter = dataTableWithMasks.AsEnumerable().Where(row => row.Field<DateTime>(tableSchema.SynchronousColumnSchema.Name) >= synchronousDateTime.Date);

            if (dataTableWithMasksWithFilter.Any())
            {
                dataTableWithMasks = dataTableWithMasksWithFilter.CopyToDataTable();
            }
            else
            {
                dataTableWithMasks.Rows.Clear();
            }

            return dataTableWithMasks;
        }

        /// <summary>
        ///     Returns the data from a table while transforming the data in regards to the associated masks
        /// </summary>
        /// <param name="tableSchema">the table schema</param>
        /// <returns></returns>
        public DataTable GetTableDataWithMasksFromTableSchema(TableSchemaConfiguration tableSchema)
        {
            var dataTable = DbConnection.GetTableData(GetSelectTableSchemaSql(tableSchema));

            return GetTableDataWithMasksFromTableSchema(tableSchema, GetTableDataWithValidCellData(dataTable));
        }

        /// <summary>
        /// Returns the data from a table with valid MSSQL date times
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public DataTable GetTableDataWithValidCellData(DataTable dataTable)
        {
            var dataColumns = dataTable.Columns.Cast<DataColumn>().Where(c => c.DataType == typeof(DateTime)).ToList();

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var column in dataColumns)
                {
                    var value = row.Field<DateTime?>(column);
                    if (value.HasValue)
                    {
                        row[column] = GetCellDataWithValidDateTime(value.Value);
                    }
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Returns the data from a table with masked data types
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public DataTable GetTableDataWithMasksFromTableSchema(TableSchemaConfiguration tableSchema, DataTable dataTable)
        {
            if (!tableSchema.ColumnSchemas.Cast<ColumnSchemaConfiguration>().Any(c => c.HasMask))
            {
                return dataTable;
            }

            var dataSchemaMaskTable = new DataTable();

            // Columns
            for (var columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                var columnSchema = tableSchema.ColumnSchemas.Cast<ColumnSchemaConfiguration>().Single(c => c.Name == dataTable.Columns[columnIndex].ColumnName);

                dataSchemaMaskTable.Columns.Add(new DataColumn(dataTable.Columns[columnIndex].ColumnName,
                    !columnSchema.HasMask ? columnSchema.DataType : columnSchema.MaskDataType));
            }

            // Rows
            while (dataTable.Rows.Count > 0)
            {
                dataSchemaMaskTable.Rows.Add();

                for (var columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    var columnSchema = tableSchema.ColumnSchemas.Cast<ColumnSchemaConfiguration>().Single(c => c.Name == dataTable.Columns[columnIndex].ColumnName);

                    if (!columnSchema.HasMask)
                    {
                        dataSchemaMaskTable.Rows[dataSchemaMaskTable.Rows.Count - 1][columnIndex] = dataTable.Rows[0][columnIndex];

                        continue;
                    }

                    dataSchemaMaskTable.Rows[dataSchemaMaskTable.Rows.Count - 1][columnIndex] = GetCellDataWithMasksFromTableSchema(dataTable.Rows[0][columnIndex], columnSchema);
                }

                dataTable.Rows.RemoveAt(0);
            }

            return dataSchemaMaskTable;
        }

        /// <summary>
        ///     Gets the cell values for the data table accounting for format masks
        /// </summary>
        /// <param name="dataCell"></param>
        /// <param name="columnSchema"></param>
        private object GetCellDataWithMasksFromTableSchema(object dataCell, ColumnSchemaConfiguration columnSchema)
        {
            var dataColumnValue = dataCell.ToString();

            // Mask DataTypes
            switch (columnSchema.MaskDataType.ToString())
            {
                case "System.DateTime":

                    {
                        var dateConversion = GlobalShopDataTypeExtensions.ConvertToDateTime(dataColumnValue, columnSchema.MaskFormat);

                        return GetCellDataWithValidDateTime(dateConversion);
                    }

                    //case "System.TimeSpan":
                    //    {
                    //        return GlobalShopDataTypeExtensions.ConvertToDateTime(dataColumnValue, columnSchema.MaskFormat).TimeOfDay;
                    //    }

                    //case "System.Integer":
                    //    {
                    //        return int.TryParse(dataColumnValue, out var result) ? result : 0;
                    //    }

                    //case "System.Boolean":
                    //    {
                    //        return bool.TryParse(dataColumnValue, out var result) && result;
                    //    }
            }

            return null;
        }

        /// <summary>
        /// Returns the cell data with a valid MSSQL date time
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DateTime GetCellDataWithValidDateTime(DateTime data)
        {
            // gss allows garbage data in and out
            if (data.Year <= ((DateTime)SqlDateTime.MinValue).Year || data.Year >= ((DateTime)SqlDateTime.MaxValue).Year)
            {
                return new DateTime(1900, 1, 1);
            }

            return data;
        }

        /// <summary>
        ///     Returns the sql select for a specific table schema
        /// </summary>
        /// <param name="tableSchema">the table schema</param>
        /// <returns></returns>
        private string GetSelectTableSchemaSql(ITableSchema tableSchema)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("select");

            foreach (var columnSchema in tableSchema.ColumnSchemas.OrderBy(c => c.Ordinal))
            {
                stringBuilder.AppendLine($"\"{columnSchema.Name}\",");
            }

            stringBuilder.Remove(stringBuilder.Length - 3, 1);

            stringBuilder.AppendLine($"from {tableSchema.Name}");

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Returns the sql select for a specific table schema
        /// </summary>
        /// <param name="tableSchema">the table schema configuration</param>
        /// <returns></returns>
        private string GetSelectTableSchemaSql(TableSchemaConfiguration tableSchema)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(GetSelectTableSchemaSql(tableSchema as ITableSchema));

            if (!string.IsNullOrWhiteSpace(tableSchema.SqlWhereClause))
            {
                stringBuilder.AppendLine($"where {tableSchema.SqlWhereClause}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Returns the sql select for a specific table schema
        /// </summary>
        /// <param name="tableSchema">the table schema configuration</param>
        /// <param name="synchronousDateTime">the synchronous datetime</param>
        /// <returns></returns>
        private string GetSelectTableSchemaSql(TableSchemaConfiguration tableSchema, DateTime synchronousDateTime)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(GetSelectTableSchemaSql(tableSchema as ITableSchema));

            if (tableSchema.HasSynchronousColumnSchema && !tableSchema.SynchronousColumnSchema.HasMask)
            {
                stringBuilder.AppendLine($"where \"{tableSchema.SynchronousColumnSchema.Name}\" >=  {synchronousDateTime.Date}");

                if (!string.IsNullOrWhiteSpace(tableSchema.SqlWhereClause))
                {
                    stringBuilder.AppendLine($"and {tableSchema.SqlWhereClause}");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(tableSchema.SqlWhereClause))
                {
                    stringBuilder.AppendLine($"where {tableSchema.SqlWhereClause}");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Returns the sql select top 500 for a specific table schema
        /// </summary>
        /// <param name="tableSchema">the table schema configuration</param>
        /// <returns></returns>
        private string GetSelectPreviewTableSchemaSql(TableSchemaConfiguration tableSchema)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("select top 500");

            foreach (var columnSchema in tableSchema.ColumnSchemas.OrderBy(c => c.Ordinal))
            {
                stringBuilder.AppendLine($"\"{columnSchema.Name}\",");
            }

            stringBuilder.Remove(stringBuilder.Length - 3, 1);

            stringBuilder.AppendLine($"from {tableSchema.Name}");

            if (!string.IsNullOrWhiteSpace(tableSchema.SqlWhereClause))
            {
                stringBuilder.AppendLine($"where {tableSchema.SqlWhereClause}");
            }

            return stringBuilder.ToString();
        }
    }
}