namespace TableRelations.Application.Infrastructure.Entities
{
    public class DlObjectProperty : BaseEntity
    {
        public int DlObjectId { get; set; }

        public DlObject DlObject { get; set; }

        public int? NewFieldId { get; set; }

        public string PropertyName { get; set; }

        public int? ActianColumnId { get; set; }

        public ActianColumn ActianColumn { get; set; }

        public int NewTableId { get; set; }

        public int? ActianTableId { get; set; }

        public ActianTable ActianTable { get; set; }

        public string Grouping { get; set; }

        public int? DlObjectAliasId { get; set; }

        public string FieldName { get; set; }

        public int? FileId { get; set; }

        public bool HasRevisionFlag { get; set; }

        public string AbbrPropertyName { get; set; }

        public string AliasGroup { get; set; }

        public int? PropertyRefId { get; set; }

        public DlObjectProperty PropertyRef { get; set; }

        public int? DlConvertId { get; set; }

        public DlConvert DlConvert { get; set; }

        public bool IsHidden { get; set; }

        public int? ArrayIndex { get; set; }

        public string Constant { get; set; }

        public int? PreviousDlObjectPropertyId { get; set; }

        public int? DlObjectPropertyDataTypeId { get; set; }

        public int? OverrideSize { get; set; }

        public int? OverrideOffset { get; set; }

        public int? OverridePrecision { get; set; }

        public bool IsFutureObsolete { get; set; }

        public bool IsObsolete { get; set; }

        public bool IsEncrypted { get; set; }

        public override string ToString()
        {
            return $"{PropertyName}: {DlObjectPropertyDataTypeId}";
        }
    }
}