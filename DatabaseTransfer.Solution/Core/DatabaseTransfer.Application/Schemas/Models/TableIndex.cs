using System.Collections.Generic;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Models
{
    public class TableIndex : ITableIndex
    {
        public TableIndex()
        {
            ColumnIndices = new List<IColumnIndex>();
        }

        public TableIndex(string id, string name)
        {
            ColumnIndices = new List<IColumnIndex>();

            Id = id;
            Name = $"{name}K0{id}";
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<IColumnIndex> ColumnIndices { get; set; }

        public override string ToString()
        {
            return $"{Name} | Column Count: {ColumnIndices.Count}";
        }
    }
}