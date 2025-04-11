using Microsoft.EntityFrameworkCore;
using TableRelations.Application.Infrastructure.Infrastructure;

namespace TableRelations.Application.Infrastructure
{
    public class DataLayerContextFactory : DesignTimeDbContextFactoryBase<DataLayerContext>
    {
        protected override DataLayerContext CreateNewInstance(DbContextOptions<DataLayerContext> options)
        {
            return new DataLayerContext(options);
        }
    }
}