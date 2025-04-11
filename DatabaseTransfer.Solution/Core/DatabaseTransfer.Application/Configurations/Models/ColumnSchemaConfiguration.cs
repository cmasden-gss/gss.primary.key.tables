using System;
using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Configurations.Models
{
    public class ColumnSchemaConfiguration : ColumnSchema
    {
        public Type MaskDataType { get; set; }

        public string MaskFormat { get; set; }

        public bool HasMask => !string.IsNullOrWhiteSpace(MaskFormat);

        public override string ToString()
        {
            return $"{Name}: {DataType}";
        }
    }
}