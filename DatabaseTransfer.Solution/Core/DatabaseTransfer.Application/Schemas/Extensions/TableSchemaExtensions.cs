using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Extensions
{
    public static class TableSchemaExtensions
    {
        /// <summary>
        ///     Are the column schemas the same/equal
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <param name="compareTableSchema"></param>
        /// <returns></returns>
        public static bool IsColumnSchemasEqual(this ITableSchema tableSchema, ITableSchema compareTableSchema)
        {
            var columnSchemasComparisonList = GetColumnSchemasComparison(tableSchema, compareTableSchema);

            return columnSchemasComparisonList.Count == 0;
        }

        /// <summary>
        ///     Returns a list of mismatch column schemas
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <param name="compareTableSchema"></param>
        /// <returns></returns>
        public static List<IColumnSchema> GetColumnSchemasComparison(this ITableSchema tableSchema, ITableSchema compareTableSchema)
        {
            var columnSchemaResultList = new List<IColumnSchema>();

            foreach (var columnSchema in tableSchema.ColumnSchemas)
            {
                var compareColumnSchema = compareTableSchema.ColumnSchemas.SingleOrDefault(c => c.Name.Equals(columnSchema.Name)
                                                                                                && IsColumnDataType(columnSchema.DataType, c.DataType));

                if (compareColumnSchema == null)
                {
                    columnSchemaResultList.Add(columnSchema);
                }
            }

            return columnSchemaResultList;
        }

        /// <summary>
        ///     Overrides DataType checks since we aren't smart enough to normalize DataTypes
        ///     <see href="https://www.npgsql.org/doc/types/basic.html" />
        /// </summary>
        /// <param name="type"></param>
        /// <param name="compareType"></param>
        /// <returns></returns>
        private static bool IsColumnDataType(Type type, Type compareType)
        {
            if (type.FullName == compareType.FullName)
            {
                return true;
            }

            // if (type == typeof(System.SByte) && compareType == typeof(System.Int16)) (this should work and it does not...)
            if (type.FullName == "System.SByte" && compareType == typeof(short))
            {
                return true;
            }

            if (type.FullName == "System.SByte" && compareType == typeof(byte))
            {
                return true;
            }

            if (type == typeof(ushort) && compareType == typeof(short))
            {
                return true;
            }

            if (type == typeof(uint) && compareType == typeof(int))
            {
                return true;
            }

            if (type == typeof(ulong) && compareType == typeof(long))
            {
                return true;
            }

            return false;
        }
    }
}