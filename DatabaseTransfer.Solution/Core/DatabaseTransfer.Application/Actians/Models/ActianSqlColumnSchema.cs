using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Actians.Models
{
    public class ActianSqlColumnSchema : ColumnSchema
    {
        public string BaseSchemaName { get; set; }

        public string BaseTableName { get; set; }

        public string BaseColumnName { get; set; }

        public string CharacterSetName { get; set; }
    }
}