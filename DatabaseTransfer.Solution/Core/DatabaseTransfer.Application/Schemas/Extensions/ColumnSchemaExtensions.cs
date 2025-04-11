using DatabaseTransfer.Application.Schemas.Interfaces;

namespace DatabaseTransfer.Application.Schemas.Extensions
{
    public static class ColumnSchemaExtensions
    {
        /// <summary>
        ///     Gets the DataType for Microsoft SQL
        /// </summary>
        /// <param name="columnSchema"></param>
        /// <returns></returns>
        public static string GetMicrosoftColumnSchemaSqlDataType(this IColumnSchema columnSchema)
        {
            var columnType = columnSchema.DataType.ToString();
            switch (columnType)
            {
                case "System.Single":
                {
                    return "real";
                }

                case "System.UInt32":
                case "System.Int32":
                {
                    return "int";
                }

                case "System.UInt64":
                case "System.Int64":
                {
                    return "bigint";
                }

                case "System.UInt16":
                case "System.Int16":
                {
                    return "smallint";
                }

                case "System.Byte[]":
                {
                    return "varbinary(max)"; // return "binary";
                }

                case "System.Byte":
                case "System.SByte":
                {
                    return "tinyint";
                }

                case "System.Boolean":
                {
                    return "bit";
                }

                case "System.Decimal":
                {
                    return "decimal";
                }

                case "System.Double":
                {
                    return "float";
                }

                case "System.DateTime":
                {
                    return "datetime";
                }

                case "System.TimeSpan":
                {
                    return "time";
                }

                case "System.String":
                default:
                {
                    return columnSchema.Size > 0 && columnSchema.Size <= 255 ? $"nvarchar({columnSchema.Size})" : "nvarchar(max)";
                }
            }
        }

        /// <summary>
        ///     Gets the DataType for Postgre SQL
        /// </summary>
        /// <param name="columnSchema"></param>
        /// <returns></returns>
        public static string GetPostgreColumnSchemaSqlDataType(this IColumnSchema columnSchema)
        {
            var columnType = columnSchema.DataType.ToString();
            switch (columnType)
            {
                case "System.Single":
                {
                    return "real";
                }

                case "System.Int32":
                {
                    return "int";
                }

                case "System.Int64":
                {
                    return "bigint";
                }

                case "System.Byte":
                case "System.SByte":
                case "System.UInt16":
                case "System.Int16":
                {
                    return "smallint";
                }

                case "System.Byte[]":
                {
                    return "bytea";
                }

                case "System.Boolean":
                {
                    return "boolean";
                }

                case "System.Decimal":
                {
                    return "decimal";
                }

                case "System.Double":
                {
                    return "double precision";
                }

                case "System.DateTime":
                {
                    return "timestamp";
                }

                case "System.TimeSpan":
                {
                    return "time";
                }

                case "System.String":
                default:
                {
                    return columnSchema.Size > 0 && columnSchema.Size <= 255 ? $"varchar({columnSchema.Size})" : "varchar";
                }
            }
        }
    }
}