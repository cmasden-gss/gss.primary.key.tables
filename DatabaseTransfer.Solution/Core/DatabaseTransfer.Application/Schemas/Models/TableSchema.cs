using System.Collections.Generic;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Models
{
    public class TableSchema : ITableSchema
    {
        protected TableSchema()
        {
            ColumnSchemas = new List<IColumnSchema>();
            TableIndices = new List<ITableIndex>();
        }

        public string Name { get; set; }

        public List<IColumnSchema> ColumnSchemas { get; set; }

        public List<ITableIndex> TableIndices { get; set; }

        public override string ToString()
        {
            return $"{Name} | Indices Count: {TableIndices.Count} | Column Count: {ColumnSchemas.Count}";
        }
    }
}