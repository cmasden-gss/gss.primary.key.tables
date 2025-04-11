using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class ActianColumnConfiguration : IEntityTypeConfiguration<ActianColumn>
    {
        public void Configure(EntityTypeBuilder<ActianColumn> entity)
        {
            entity.ToTable("DL_X$Field");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Xe$Id");

            entity.Property(e => e.ActianTableId)
                .HasColumnName("Xe$File");

            entity.Property(e => e.Name)
                .HasColumnName("Xe$Name");

            entity.Property(e => e.DataType)
                .HasColumnName("Xe$DataType");

            entity.Property(e => e.Offset)
                .HasColumnName("Xe$Offset");

            entity.Property(e => e.Size)
                .HasColumnName("Xe$Size");

            entity.Property(e => e.Precision)
                .HasColumnName("Xe$Dec");

            entity.Property(e => e.Flags)
                .HasColumnName("Xe$Flags");
        }
    }
}