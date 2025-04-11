using System.Collections.Generic;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.SetupWizardUi.Schemas.Models
{
    /// <summary>
    ///     Supporting object for SchemaPageUserControl - Checkboxes
    /// </summary>
    public class TableSchemaSelected
    {
        public TableSchemaSelected()
        {
            ColumnSchemas = new List<ColumnSchemaSelected>();
        }

        public TableSchemaSelected(string name)
        {
            Name = name;
            ColumnSchemas = new List<ColumnSchemaSelected>();
        }

        public string Name { get; set; }

        public List<ColumnSchemaSelected> ColumnSchemas { get; set; }

        public List<ITableIndex> TableIndices { get; set; }

        public ColumnSchemaSelected SynchronousColumnSchema { get; set; }

        public bool HasSynchronousColumnSchema => SynchronousColumnSchema != null;

        public string SqlWhereClause { get; set; }

        public bool HasSqlWhereClause => !string.IsNullOrWhiteSpace(SqlWhereClause);

        public bool IsSelected { get; set; }
    }
}