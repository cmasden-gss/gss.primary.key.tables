using System;
using System.Collections.Generic;

namespace DatabaseTransfer.SetupWizardUi.Schemas.Models
{
    /// <summary>
    ///     Supporting object for SchemaPageUserControl - Checkboxes
    /// </summary>
    public class ColumnSchemaSelected
    {
        public ColumnSchemaSelected()
        {
            ForeignKeyTableSchemas = new List<TableSchemaSelected>();
        }

        public string Name { get; set; }

        public int Ordinal { get; set; }

        public int Size { get; set; }

        public Type DataType { get; set; }

        public bool IsKey { get; set; }

        public bool UseMask { get; set; }

        public Type MaskDataType { get; set; }

        public string MaskFormat { get; set; }

        public bool HasMask => !string.IsNullOrWhiteSpace(MaskFormat);

        public List<TableSchemaSelected> ForeignKeyTableSchemas { get; set; }

        public bool IsSelected { get; set; }
    }
}