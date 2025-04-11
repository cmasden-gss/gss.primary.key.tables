namespace TableRelations.Application.Infrastructure.Entities
{
    public class ActianTableColumnIndex
    {
        // Xi$File
        public int ActianTableId { get; set; }

        public ActianTable ActianTable { get; set; }

        // Xi$Field
        public int ActianColumnId { get; set; }

        public ActianColumn ActianColumn { get; set; }

        // Xi$Number
        public int Number { get; set; }

        // Xi$Part
        public int Part { get; set; }

        // Xi$Flags
        public int Flags { get; set; }

        public override string ToString()
        {
            return $"{ActianTable.Name}: {ActianColumn.Name}";
        }
    }
}