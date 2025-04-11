using System;
using System.Collections.Generic;
using System.Data;
using DatabaseTransfer.Application.Microsofts.Models;
using DatabaseTransfer.Application.Schemas.Interfaces;
using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Postgres.Models
{
    public class PostgreSqlTableSchema : TableSchema
    {
        public PostgreSqlTableSchema()
        {
            ColumnSchemas = new List<IColumnSchema>();
        }

        public PostgreSqlTableSchema(string name, DataTable schemaTable)
        {
            Name = name;

            ColumnSchemas = new List<IColumnSchema>();
            GetColumnSchemasFromSchemaTable(schemaTable);
        }

        private void GetColumnSchemasFromSchemaTable(DataTable schemaTable)
        {
            foreach (DataRow schemaTableRow in schemaTable.Rows)
            {
                ColumnSchemas.Add(new MicrosoftSqlColumnSchema
                {
                    Name = schemaTableRow["ColumnName"].ToString(),
                    Ordinal = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnOrdinal"].ToString()) ? int.Parse(schemaTableRow["ColumnOrdinal"].ToString()) : 0,
                    Size = !string.IsNullOrWhiteSpace(schemaTableRow["ColumnSize"].ToString()) ? int.Parse(schemaTableRow["ColumnSize"].ToString()) : 0,
                    DataType = (Type) schemaTableRow["DataType"],
                    AllowNull = !string.IsNullOrWhiteSpace(schemaTableRow["AllowDbNull"].ToString()) && bool.Parse(schemaTableRow["AllowDbNull"].ToString()),
                    IsKey = !string.IsNullOrWhiteSpace(schemaTableRow["IsKey"].ToString()) && bool.Parse(schemaTableRow["IsKey"].ToString()),
                    IsUnique = !string.IsNullOrWhiteSpace(schemaTableRow["IsUnique"].ToString()) && bool.Parse(schemaTableRow["IsUnique"].ToString())
                });
            }
        }
    }
}