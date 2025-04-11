namespace TableRelations.Application.Infrastructure.Entities
{
    public class DlConvert : BaseEntity
    {
        private string _name;

        public string Name
        {
            get => _name.Trim();
            set => _name = value;
        }
    }
}