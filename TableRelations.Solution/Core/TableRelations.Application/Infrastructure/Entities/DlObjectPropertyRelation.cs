namespace TableRelations.Application.Infrastructure.Entities
{
    public class DlObjectPropertyRelation
    {
        public int OriginalDlObjectId { get; set; }

        public DlObject OriginalDlObject { get; set; }

        public int OriginalDlObjectPropertyId { get; set; }

        public DlObjectProperty OriginalDlObjectProperty { get; set; }

        public int TransferDlObjectId { get; set; }

        public DlObject TransferDlObject { get; set; }

        public int TransferDlObjectPropertyId { get; set; }

        public DlObjectProperty TransferDlObjectProperty { get; set; }
    }
}