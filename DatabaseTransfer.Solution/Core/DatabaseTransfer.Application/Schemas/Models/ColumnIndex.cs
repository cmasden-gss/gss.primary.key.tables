using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Models
{
    public class ColumnIndex : IColumnIndex
    {
        public int Ordinal { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Ordinal}: {Name}";
        }
    }
}