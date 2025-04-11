using System.Collections.Generic;
using System.Data;
using DatabaseTransfer.Application.Models;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Clients.Interfaces
{
    /// <summary>
    ///     Transfer client interface
    /// </summary>
    public interface ITransferClient
    {
        /// <summary>
        ///     Returns the database version
        /// </summary>
        /// <returns></returns>
        Result<string> GetVersion();

        /// <summary>
        ///     Does the table exist
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns></returns>
        bool TableExists(string tableName);

        /// <summary>
        ///     Returns a list of table names
        /// </summary>
        /// <returns></returns>
        List<string> GetTableNames();

        /// <summary>
        ///     Returns the table schema
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <returns></returns>
        ITableSchema GetTableSchema(string tableName);

        /// <summary>
        ///     Creates a table based on the table schema
        /// </summary>
        /// <param name="tableSchema">the table schema</param>
        void CreateTable(ITableSchema tableSchema);

        /// <summary>
        ///     Clears all data in a table
        /// </summary>
        /// <param name="tableName">the table name</param>
        void ClearTable(string tableName);

        /// <summary>
        ///     Executes a sql statement
        /// </summary>
        /// <param name="sqlStatement"></param>
        void ExecuteSqlStatement(string sqlStatement);

        /// <summary>
        ///     Deletes the table
        /// </summary>
        /// <param name="tableName">the table name</param>
        void DeleteTable(string tableName);

        /// <summary>
        ///     Bulk Copies [inserts] all data into a specified table
        /// </summary>
        /// <param name="tableName">the table name</param>
        /// <param name="tableData">the table data</param>
        void BulkCopyDataTable(string tableName, DataTable tableData);
    }
}