using System;
using System.Collections.Generic;
using System.Data;
using DatabaseTransfer.Application.Schemas.Interfaces;
using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Actians.Models
{
    public class ActianSqlTableSchema : TableSchema
    {
        public ActianSqlTableSchema()
        {
            ColumnSchemas = new List<IColumnSchema>();
            TableIndices = new List<ITableIndex>();
        }

        public ActianSqlTableSchema(string name, DataTable schemaTable, DataTable indexTable)
        {
            Name = name;

            ColumnSchemas = new List<IColumnSchema>();
            TableIndices = new List<ITableIndex>();

            GetColumnSchemasFromTableSchema(schemaTable);
            GetTableIndexesFromIndexTable(indexTable);
        }

        private void GetTableIndexesFromIndexTable(DataTable indexTable)
        {
            if (indexTable.Rows.Count == 0)
            {
                return;
            }

            var tableIndex = new TableIndex(indexTable.Rows[0]["id"].ToString(), Name);
            TableIndices.Add(tableIndex);

            foreach (DataRow indexTableRow in indexTable.Rows)
            {
                var currentId = indexTableRow["id"].ToString();

                if (currentId != tableIndex.Id)
                {
                    tableIndex = new TableIndex(currentId, Name);
                    TableIndices.Add(tableIndex);
                }

                tableIndex.ColumnIndices.Add(new ColumnIndex
                {
                    Name = indexTableRow["name"].ToString().Trim(),
                    Ordinal = !string.IsNullOrWhiteSpace(indexTableRow["ordinal"].ToString()) ? int.Parse(indexTableRow["ordinal"].ToString()) : 0
                });
            }
        }

        private void GetColumnSchemasFromTableSchema(DataTable schemaTable)
        {
            foreach (DataRow schemaTableRow in schemaTable.Rows)
            {
                // there is a thing called bad data and idiots.
                if (string.IsNullOrWhiteSpace(schemaTableRow["ColumnName"].ToString()))
                {
                    continue;
                }

                ColumnSchemas.Add(new ActianSqlColumnSchema
                {
                    Name = schemaTableRow["ColumnName"].ToString(),
                    Ordinal = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnOrdinal"].ToString()) ? int.Parse(schemaTableRow["ColumnOrdinal"].ToString()) : 0,
                    Size = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnSize"].ToString()) ? int.Parse(schemaTableRow["ColumnSize"].ToString()) : 0,
                    DataType = (Type) schemaTableRow["DataType"],
                    AllowNull = !string.IsNullOrWhiteSpace(schemaTableRow["AllowDbNull"].ToString()) && bool.Parse(schemaTableRow["AllowDbNull"].ToString()),
                    IsKey = !string.IsNullOrWhiteSpace(schemaTableRow["IsKey"].ToString()) && bool.Parse(schemaTableRow["IsKey"].ToString()),
                    IsUnique = !string.IsNullOrWhiteSpace(schemaTableRow["IsUnique"].ToString()) && bool.Parse(schemaTableRow["IsUnique"].ToString()),
                    BaseSchemaName = schemaTableRow["BaseSchemaName"].ToString(),
                    BaseTableName = schemaTableRow["BaseTableName"].ToString(),
                    BaseColumnName = schemaTableRow["BaseColumnName"].ToString(),
                    CharacterSetName = schemaTableRow["CharacterSetName"].ToString()
                });
            }
        }
    }
}