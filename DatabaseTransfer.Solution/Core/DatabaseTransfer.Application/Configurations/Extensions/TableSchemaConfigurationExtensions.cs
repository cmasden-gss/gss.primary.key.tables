using System.Linq;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Extensions;

namespace DatabaseTransfer.Application.Configurations.Extensions
{
    public static class TableSchemaConfigurationExtensions
    {
        /// <summary>
        ///     Creates a copy of the table schema yet replaces the default data types with the mask data types.
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <returns></returns>
        public static TableSchemaConfiguration ToTableSchemaWithMaskDataTypes(this TableSchemaConfiguration tableSchema)
        {
            var tableSchemaCopy = tableSchema.Copy();

            if (!tableSchema.ColumnSchemas.Cast<ColumnSchemaConfiguration>().Any(c => c.HasMask))
            {
                return tableSchemaCopy;
            }

            foreach (var columnSchema in tableSchemaCopy.ColumnSchemas.Cast<ColumnSchemaConfiguration>().Where(c => c.HasMask))
            {
                columnSchema.DataType = columnSchema.MaskDataType;
                columnSchema.Size = -1;
            }

            return tableSchemaCopy;
        }
    }
}