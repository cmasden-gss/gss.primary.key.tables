using System.Collections.Generic;

namespace DatabaseTransfer.Application.Schemas.Interfaces
{
    public interface ITableSchema
    {
        string Name { get; set; }

        List<IColumnSchema> ColumnSchemas { get; set; }

        List<ITableIndex> TableIndices { get; set; }
    }
}