using System;

namespace DatabaseTransfer.Application.TableRelations.Models
{
    public class SqlColumnMask
    {
        public Type DataType { get; set; }

        public string Format { get; set; }

        public override string ToString()
        {
            return $"{DataType}: {Format}";
        }
    }
}