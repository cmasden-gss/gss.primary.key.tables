using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlObjectPropertyDescriptionConfiguration : IEntityTypeConfiguration<DlObjectPropertyDescription>
    {
        public void Configure(EntityTypeBuilder<DlObjectPropertyDescription> entity)
        {
            entity.ToTable("DL_Object_Prop_Desc");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ObjectPropertyId");

            entity.Property(e => e.Name)
                .HasColumnName("Description");
        }
    }
}