using System.Collections.Generic;
using DatabaseTransfer.Application.Actians.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.SetupWizardUi.Schemas.Models;
using DatabaseTransfer.SetupWizardUi.States;

namespace DatabaseTransfer.SetupWizardUi.Schemas.Extensions
{
    public static class SchemaConfigurationExtensions
    {
        /// <summary>
        ///     Returns TableSchemaConfiguration List
        /// </summary>
        /// <param name="tableSchemaList"></param>
        public static List<TableSchemaConfiguration> ToTableSchemaConfigurationList(this List<TableSchemaSelected> tableSchemaList)
        {
            var tableSchemaConfigurationList = new List<TableSchemaConfiguration>();

            foreach (var tableSchema in tableSchemaList)
            {
                var tableSchemaConfiguration = UtilityState.AutoMapper.Map<TableSchemaConfiguration>(tableSchema);
                tableSchemaConfiguration.ColumnSchemas.Clear();

                foreach (var columnSchema in tableSchema.ColumnSchemas)
                {
                    tableSchemaConfiguration.ColumnSchemas.Add(UtilityState.AutoMapper.Map<ColumnSchemaConfiguration>(columnSchema));
                }

                tableSchemaConfigurationList.Add(tableSchemaConfiguration);
            }

            return tableSchemaConfigurationList;
        }

        /// <summary>
        ///     Returns ActianSqlTableSchema
        /// </summary>
        /// <param name="tableSchema"></param>
        public static ActianSqlTableSchema ToActianSqlTableSchema(this TableSchemaSelected tableSchema)
        {
            return UtilityState.AutoMapper.Map<ActianSqlTableSchema>(tableSchema);
        }

        /// <summary>
        ///     Returns TableSchemaConfiguration
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <returns></returns>
        public static TableSchemaConfiguration ToTableSchemaConfiguration(this TableSchemaSelected tableSchema)
        {
            return UtilityState.AutoMapper.Map<TableSchemaConfiguration>(tableSchema);
        }

        /// <summary>
        ///     Returns TableSchemaSelected
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <returns></returns>
        /// <remarks>
        ///     There is a bug in the automapper profile (yet it just works right?)
        /// </remarks>
        /// <code>
        /// return UtilityState.AutoMapper.Map<TableSchemaSelected>
        ///         (tableSchema);
        ///         UtilityState.AutoMapper.Map
        ///         <ColumnSchemaSelected>
        ///             (tableSchema.ColumnSchemas[2]);
        ///             UtilityState.AutoMapper.Map
        ///             <ColumnSchemaMaskSelected>(((ActianSqlColumnSchema)tableSchema.ColumnSchemas[2]).Mask);
        /// </code>
        public static TableSchemaSelected ToTableSchemaSelected(this ActianSqlTableSchema tableSchema)
        {
            var tableSchemaSelected = UtilityState.AutoMapper.Map<TableSchemaSelected>(tableSchema);

            tableSchemaSelected.ColumnSchemas.Clear();

            foreach (var columnSchema in tableSchema.ColumnSchemas)
            {
                tableSchemaSelected.ColumnSchemas.Add(UtilityState.AutoMapper.Map<ColumnSchemaSelected>(columnSchema));
            }

            return tableSchemaSelected;
        }

        /// <summary>
        ///     Returns TableSchemaSelected
        /// </summary>
        /// <param name="tableSchema"></param>
        /// <returns></returns>
        public static TableSchemaSelected ToTableSchemaSelected(this TableSchemaConfiguration tableSchema)
        {
            var tableSchemaSelected = UtilityState.AutoMapper.Map<TableSchemaSelected>(tableSchema);

            tableSchemaSelected.ColumnSchemas.Clear();

            foreach (var columnSchema in tableSchema.ColumnSchemas)
            {
                tableSchemaSelected.ColumnSchemas.Add(UtilityState.AutoMapper.Map<ColumnSchemaSelected>(columnSchema));
            }

            return tableSchemaSelected;
        }
    }
}