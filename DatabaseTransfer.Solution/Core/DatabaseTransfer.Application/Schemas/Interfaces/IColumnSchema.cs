using System;

namespace DatabaseTransfer.Application.Schemas.Interfaces
{
    public interface IColumnSchema
    {
        string Name { get; set; }

        int Ordinal { get; set; }

        int Size { get; set; }

        Type DataType { get; set; }

        bool AllowNull { get; set; }

        bool IsKey { get; set; }

        bool IsUnique { get; set; }
    }
}