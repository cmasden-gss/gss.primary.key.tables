using Microsoft.EntityFrameworkCore;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure
{
    public class DataLayerContext : DbContext
    {
        public DataLayerContext(DbContextOptions<DataLayerContext> options)
            : base(options)
        {
        }

        public DbSet<DlObject> DlObjects { get; set; }

        public DbSet<DlObjectHistory> DlObjectHistories { get; set; }

        public DbSet<DlObjectProperty> DlObjectProperties { get; set; }

        public DbSet<DlObjectPropertyHistory> DlObjectPropertyHistories { get; set; }

        public DbSet<DlObjectPropertyDescription> DlObjectPropertyDescriptions { get; set; }

        public DbSet<DlObjectPropertyDescriptionHistory> DlObjectPropertyDescriptionHistories { get; set; }

        public DbSet<DlObjectPropertyRelation> DlObjectPropertyRelations { get; set; }

        public DbSet<ActianColumn> ActianColumns { get; set; }

        public DbSet<ActianTable> ActianTables { get; set; }

        public DbSet<ActianTableColumnIndex> ActianTableColumnIndices { get; set; }

        public DbSet<DlConvert> DlConverts { get; set; }

        public DbSet<DlConvertHistory> DlConvertHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataLayerContext).Assembly);
        }
    }
}