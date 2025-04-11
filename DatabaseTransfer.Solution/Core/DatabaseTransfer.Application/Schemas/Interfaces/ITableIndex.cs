using System.Collections.Generic;

namespace DatabaseTransfer.Application.Schemas.Interfaces
{
    public interface ITableIndex
    {
        string Id { get; set; }

        string Name { get; set; }

        List<IColumnIndex> ColumnIndices { get; set; }
    }
}