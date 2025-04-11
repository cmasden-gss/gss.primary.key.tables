using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlObjectPropertyConfiguration : IEntityTypeConfiguration<DlObjectProperty>
    {
        public void Configure(EntityTypeBuilder<DlObjectProperty> entity)
        {
            entity.ToTable("DL_Object_Property");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ObjectPropertyID");

            entity.Property(e => e.DlObjectId)
                .HasColumnName("ObjectID");

            entity.Property(e => e.DlConvertId)
                .HasColumnName("ConvertID");

            entity.Property(e => e.IsHidden)
                .HasColumnName("Hidden");

            entity.Property(e => e.ArrayIndex)
                .HasColumnName("Array");

            entity.Property(e => e.FileId)
                .HasColumnName("FileID");

            entity.Property(e => e.HasRevisionFlag)
                .HasColumnName("RevisionFlag");

            entity.Property(e => e.NewFieldId)
                .HasColumnName("NewFieldID");

            entity.Property(e => e.NewTableId)
                .HasColumnName("NewTableID");

            entity.Property(e => e.PreviousDlObjectPropertyId)
                .HasColumnName("ObjectPropDupID");

            entity.Property(e => e.ActianColumnId)
                .HasColumnName("OldFieldID");

            entity.Property(e => e.ActianTableId)
                .HasColumnName("OldTableID");

            entity.Property(e => e.PropertyRefId)
                .HasColumnName("PropRef");

            entity.Property(e => e.DlObjectAliasId)
                .HasColumnName("ObjectRef");

            entity.Property(e => e.DlObjectPropertyDataTypeId)
                .HasColumnName("OverrideDatatype");

            entity.Property(e => e.IsFutureObsolete)
                .HasColumnName("FutureObsolete");

            entity.Property(e => e.IsObsolete)
                .HasColumnName("Obsolete");

            entity.Property(e => e.IsEncrypted)
                .HasColumnName("IsEncrypted");

            entity.HasOne(typeof(DlObjectProperty), "PropertyRef");
        }
    }
}