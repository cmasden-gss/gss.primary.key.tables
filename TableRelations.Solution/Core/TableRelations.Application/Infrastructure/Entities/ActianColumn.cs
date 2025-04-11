namespace TableRelations.Application.Infrastructure.Entities
{
    public class ActianColumn : BaseEntity
    {
        // Xe$Name
        private string _name;

        // Xe$File
        public int ActianTableId { get; set; }

        public ActianTable ActianTable { get; set; }

        public string Name
        {
            get => _name.Trim();
            set => _name = value;
        }

        // Xe$DataType
        public int DataType { get; set; }

        // Xe$Offset
        public int Offset { get; set; }

        // Xe$Size
        public int Size { get; set; }

        // Xe$Dec
        public int Precision { get; set; }

        // Xe$Flags
        public int Flags { get; set; }

        public override string ToString()
        {
            return $"{Name}: {DataType}";
        }
    }
}