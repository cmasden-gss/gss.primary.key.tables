using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimaryKeys.Application.Models
{
    // Entity representing a column record
    public class ColumnRow
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Used to order columns
        public int ColumnIndex { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string ColumnName { get; set; }

        public bool IsNone { get; set; }
        public bool IsMasterKey { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }

        public string ForeignKeyTable { get; set; }
        public string ForeignKeyField { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsConfirmed { get; set; }

        // Navigation property: a ColumnRow can have many AuditEntries.
        public virtual List<AuditEntry> AuditHistory { get; set; } = new List<AuditEntry>();

        // Computed property for display purposes.
        //public string RowId => $"{TableName}-{ColumnName}-{ColumnIndex}";

        public ColumnRow Clone()
        {
            return new ColumnRow
            {
                ColumnIndex = this.ColumnIndex,
                TableName = this.TableName,
                ColumnName = this.ColumnName,
                IsNone = this.IsNone,
                IsMasterKey = this.IsMasterKey,
                IsPrimaryKey = this.IsPrimaryKey,
                IsForeignKey = this.IsForeignKey,
                ForeignKeyTable = this.ForeignKeyTable,
                ForeignKeyField = this.ForeignKeyField,
                IsRemoved = this.IsRemoved,
                IsConfirmed = this.IsConfirmed
                // Note: AuditHistory is not cloned because we want a clean slate for comparison.
            };
        }
    }
}