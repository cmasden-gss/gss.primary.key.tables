using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Microsofts.Models
{
    public class MicrosoftSqlColumnSchema : ColumnSchema
    {
        public bool IsIdentity { get; set; }

        public bool IsAutoIncrement { get; set; }
    }
}