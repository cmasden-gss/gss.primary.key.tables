using System.Collections.Generic;

namespace TableRelations.Application.KeyMappers.Models
{
    public class SqlColumnInformation
    {
        public SqlColumnInformation()
        {
            ForeignKeyTableAndColumnNames = new List<string>();
        }

        public string Name { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsForeignKey => ForeignKeyTableAndColumnNames.Count > 0;

        public List<string> ForeignKeyTableAndColumnNames { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} | Primary Key: {IsPrimaryKey} | Foreign Key: {IsForeignKey} | Description: {Description}";
        }
    }
}