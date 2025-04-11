using System;
using System.Collections.Generic;

namespace TableRelations.Application.Infrastructure.Entities
{
    public class DlObject : BaseEntity
    {
        public string ObjectName { get; set; }

        public string AbbrevName { get; set; }

        public string PluralName { get; set; }

        public string AbbrevPluralName { get; set; }

        public DateTime? ObjectCreated { get; set; }

        public int? DlObjectStatusId { get; set; }

        public DateTime? GeneratedDate { get; set; }

        public bool? IsMultiRec { get; set; }

        public bool IsSupport { get; set; }

        public List<DlObjectProperty> DlObjectProperties { get; set; }

        public override string ToString()
        {
            return $"{ObjectName}: Property Count {DlObjectProperties.Count}";
        }
    }
}