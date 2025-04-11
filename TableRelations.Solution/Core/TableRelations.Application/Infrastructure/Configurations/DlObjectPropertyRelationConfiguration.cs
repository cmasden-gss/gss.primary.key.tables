using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlObjectPropertyRelationConfiguration : IEntityTypeConfiguration<DlObjectPropertyRelation>
    {
        public void Configure(EntityTypeBuilder<DlObjectPropertyRelation> entity)
        {
            entity.ToTable("DL_PROPERTYRELATION");

            entity.HasKey(e => new {e.OriginalDlObjectId, e.OriginalDlObjectPropertyId, e.TransferDlObjectId, e.TransferDlObjectPropertyId});

            entity.Property(e => e.OriginalDlObjectId)
                .HasColumnName("FromObjectId");

            entity.Property(e => e.OriginalDlObjectPropertyId)
                .HasColumnName("FromPropertyId");

            entity.Property(e => e.TransferDlObjectId)
                .HasColumnName("ToObjectId");

            entity.Property(e => e.TransferDlObjectPropertyId)
                .HasColumnName("ToPropertyId");
        }
    }
}