using DatabaseTransfer.Application.Schemas.Models;

namespace DatabaseTransfer.Application.Configurations.Models
{
    public class TableSchemaConfiguration : TableSchema
    {
        public ColumnSchemaConfiguration SynchronousColumnSchema { get; set; }

        public bool HasSynchronousColumnSchema => SynchronousColumnSchema != null;

        public string SqlWhereClause { get; set; }

        public bool HasSqlWhereClause => !string.IsNullOrWhiteSpace(SqlWhereClause);

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}