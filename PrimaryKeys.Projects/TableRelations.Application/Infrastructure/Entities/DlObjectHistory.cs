using System;

namespace TableRelations.Application.Infrastructure.Entities
{
    public class DlObjectHistory : BaseHistoryEntity
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

        public override string ToString()
        {
            return $"{ObjectName}";
        }
    }
}