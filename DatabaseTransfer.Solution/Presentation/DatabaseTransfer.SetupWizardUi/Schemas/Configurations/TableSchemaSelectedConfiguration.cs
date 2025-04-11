using AutoMapper;
using DatabaseTransfer.Application.Actians.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.SetupWizardUi.Schemas.Models;

namespace DatabaseTransfer.SetupWizardUi.Schemas.Configurations
{
    /// <summary>
    ///     AutoMapper Configuration
    /// </summary>
    public class TableSchemaSelectedConfiguration : Profile
    {
        public TableSchemaSelectedConfiguration()
        {
            CreateMap<TableSchemaSelected, TableSchemaConfiguration>(MemberList.Source);

            CreateMap<TableSchemaConfiguration, TableSchemaSelected>(MemberList.Source);

            CreateMap<TableSchemaSelected, ActianSqlTableSchema>(MemberList.Source);

            CreateMap<ActianSqlTableSchema, TableSchemaSelected>(MemberList.Source);
        }
    }
}