using System.Collections.Generic;
using System.Text;

namespace TableRelations.Application.KeyMappers.Models
{
    public class SqlTableInformation
    {
        public SqlTableInformation()
        {
            SqlColumns = new List<SqlColumnInformation>();
        }

        public string Name { get; set; }

        public List<SqlColumnInformation> SqlColumns { get; set; }

        public override string ToString()
        {
            return $"{Name} | Column Count: {SqlColumns.Count}";
        }

        /// <summary>
        ///     Generates End User Output
        /// </summary>
        /// <remarks>
        ///     Refactored version of Strider Smith's original solution
        ///     Criteria removes "correct" information based on Chris Okamuro's spec
        /// </remarks>
        /// <code>
        ///  // Original implementation
        /// foreach (var sqlColumn in SqlColumns.Where(c => c.IsPrimaryKey))
        /// {
        ///     stringBuilder.AppendLine($"{sqlColumn.Name} - [PK] {sqlColumn.Description}");
        /// }
        /// foreach (var sqlColumn in SqlColumns.Where(c => c.IsForeignKey && !c.IsPrimaryKey))
        /// {
        ///     stringBuilder.AppendLine($"{sqlColumn.Name} - [{string.Join(",", sqlColumn.ForeignKeyTableAndColumnNames)}] {sqlColumn.Description}");
        /// }
        /// foreach (var sqlColumn in SqlColumns.Where(c => !c.IsPrimaryKey && !c.IsForeignKey))
        /// {
        ///     if (!sqlColumn.Name.ToUpper().Contains("FILLER") || !(sqlColumn.Name.ToUpper().Contains("FILLER") && sqlColumn.Description.ToUpper().Contains("OBSOLETE")))
        ///     {
        ///         stringBuilder.AppendLine($"{sqlColumn.Name} - {sqlColumn.Description}");
        ///     }
        /// }
        ///  </code>
        /// <returns></returns>
        public string BuildOutput()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(Name);
            stringBuilder.AppendLine("====================");

            foreach (var sqlColumn in SqlColumns)
            {
                if (sqlColumn.IsPrimaryKey)
                {
                    stringBuilder.AppendLine($"{sqlColumn.Name} - [PK] {sqlColumn.Description}");
                }
                else if (sqlColumn.IsForeignKey)
                {
                    stringBuilder.AppendLine($"{sqlColumn.Name} - [{string.Join(",", sqlColumn.ForeignKeyTableAndColumnNames)}] {sqlColumn.Description}");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(sqlColumn.Description))
                    {
                        stringBuilder.AppendLine($"{sqlColumn.Name}");
                    }
                    //else if (!sqlColumn.Name.ToUpper().Contains("FILLER")) //&& !(sqlColumn.Name.ToUpper().Contains("FILLER") || sqlColumn.Description.ToUpper().Contains("OBSOLETE"))) (if you wanted to ignore filler and obsolete)
                    else if (!sqlColumn.Name.ToUpper().Contains("FILLER") || !(sqlColumn.Name.ToUpper().Contains("FILLER") && sqlColumn.Description.ToUpper().Contains("OBSOLETE")))
                    {
                        stringBuilder.AppendLine($"{sqlColumn.Name} - {sqlColumn.Description}");
                    }
                }
            }

            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}