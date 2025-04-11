using System.Data;
using System.Data.Common;

namespace DatabaseTransfer.Application.Extensions
{
    public static class DbConnectionExtensions
    {
        /// <summary>
        ///     Returns the table data
        /// </summary>
        /// <param name="connection">the database connection</param>
        /// <param name="commandText">the command text</param>
        /// <returns></returns>
        public static DataTable GetTableData(this DbConnection connection, string commandText)
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
        public static DataTable GetTableSchema(this DbConnection connection, string tableName)
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
}