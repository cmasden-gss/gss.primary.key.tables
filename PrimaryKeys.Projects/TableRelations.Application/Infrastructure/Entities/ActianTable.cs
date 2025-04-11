using System.Collections.Generic;

namespace TableRelations.Application.Infrastructure.Entities
{
    public class ActianTable : BaseEntity
    {
        // Xf$Name

        private string _name;

        public string Name
        {
            get => _name.Trim();
            set => _name = value;
        }

        // Xf$Loc
        public string FileLocation { get; set; }

        public string FileName { get; set; }

        // Xf$Flags
        public int? Flags { get; set; }

        // Xf$Reserved
        public int? Reserved { get; set; }

        public List<ActianColumn> ActianColumns { get; set; }

        // Common
        public int? CommonDb { get; set; }

        public override string ToString()
        {
            return $"{Name} | Column Count: {ActianColumns.Count}";
        }
    }
}