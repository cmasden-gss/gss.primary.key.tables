using AutoMapper;
using DatabaseTransfer.Application.Actians.Models;
using DatabaseTransfer.Application.Configurations.Models;
using DatabaseTransfer.Application.Schemas.Interfaces;
using DatabaseTransfer.SetupWizardUi.Schemas.Models;

namespace DatabaseTransfer.SetupWizardUi.Schemas.Configurations
{
    /// <summary>
    ///     AutoMapper Configuration
    /// </summary>
    public class ColumnSchemaSelectedConfiguration : Profile
    {
        public ColumnSchemaSelectedConfiguration()
        {
            CreateMap<ColumnSchemaSelected, ColumnSchemaConfiguration>(MemberList.Source);
            CreateMap<ColumnSchemaSelected, IColumnSchema>(MemberList.Source).As<ColumnSchemaConfiguration>();

            CreateMap<ColumnSchemaConfiguration, ColumnSchemaSelected>(MemberList.Source);

            CreateMap<ActianSqlColumnSchema, ColumnSchemaSelected>(MemberList.Source);

            CreateMap<IColumnSchema, ColumnSchemaSelected>(MemberList.Source);
            CreateMap<ColumnSchemaSelected, IColumnSchema>(MemberList.Source).As<ActianSqlColumnSchema>();
        }
    }
}