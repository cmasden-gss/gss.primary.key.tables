using System;
using System.ComponentModel.DataAnnotations;

namespace PrimaryKeys.Application.Models
{
    // Entity representing an audit entry
    public class AuditEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime ChangedOn { get; set; }

        [Required]
        public string ChangedBy { get; set; }

        public string Description { get; set; }

        public string ColumnName { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }

        // Foreign key to the ColumnRow record.
        public Guid ColumnRowId { get; set; }

        public virtual ColumnRow ColumnRow { get; set; }
    }
}