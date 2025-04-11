using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlObjectConfiguration : IEntityTypeConfiguration<DlObject>
    {
        public void Configure(EntityTypeBuilder<DlObject> entity)
        {
            entity.ToTable("DL_Object");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ObjectID");

            entity.Property(e => e.ObjectName)
                .HasColumnName("ObjectName");

            entity.Property(e => e.DlObjectStatusId)
                .HasColumnName("Status");
        }
    }
}