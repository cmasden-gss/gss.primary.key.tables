using System.Collections.Generic;

namespace DatabaseTransfer.Application.TableRelations.Models
{
    public class SqlTable
    {
        public SqlTable()
        {
            SqlColumns = new List<SqlColumn>();
        }

        public string Name { get; set; }

        public List<SqlColumn> SqlColumns { get; set; }

        public override string ToString()
        {
            return $"{Name} | Column Count: {SqlColumns.Count}";
        }
    }
}