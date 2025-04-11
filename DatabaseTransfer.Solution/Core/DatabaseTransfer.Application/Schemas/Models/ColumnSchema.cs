using System;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Models
{
    public class ColumnSchema : IColumnSchema
    {
        public string Name { get; set; }

        public int Ordinal { get; set; }

        public int Size { get; set; }

        public Type DataType { get; set; }

        public bool AllowNull { get; set; }

        public bool IsKey { get; set; }

        public bool IsUnique { get; set; }

        public override string ToString()
        {
            return $"{Name}: {DataType}";
        }
    }
}