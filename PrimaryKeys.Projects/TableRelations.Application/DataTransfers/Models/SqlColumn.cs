using System.Collections.Generic;

namespace TableRelations.Application.DataTransfers.Models
{
    public class SqlColumn
    {
        public SqlColumn()
        {
            ForeignKeyTableNames = new List<string>();
        }

        public string Name { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsForeignKey => ForeignKeyTableNames.Count > 0;

        public List<string> ForeignKeyTableNames { get; set; }

        public SqlColumnMask SqlColumnMask { get; set; }

        public override string ToString()
        {
            return $"{Name} | Primary Key: {IsPrimaryKey} | Foreign Key: {IsForeignKey} | Mask: {SqlColumnMask?.DataType}{SqlColumnMask?.Format}";
        }
    }
}