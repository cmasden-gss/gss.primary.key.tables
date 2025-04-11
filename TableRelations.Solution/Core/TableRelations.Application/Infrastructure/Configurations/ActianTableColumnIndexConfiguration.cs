using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class ActianTableColumnIndexConfiguration : IEntityTypeConfiguration<ActianTableColumnIndex>
    {
        public void Configure(EntityTypeBuilder<ActianTableColumnIndex> entity)
        {
            entity.ToTable("DL_X$Index");

            entity.HasKey(e => new {e.ActianTableId, e.ActianColumnId, e.Number});

            entity.Property(e => e.ActianTableId)
                .HasColumnName("Xi$File");

            entity.Property(e => e.ActianColumnId)
                .HasColumnName("Xi$Field");

            entity.Property(e => e.Number)
                .HasColumnName("Xi$Number");

            entity.Property(e => e.Part)
                .HasColumnName("Xi$Part");

            entity.Property(e => e.Flags)
                .HasColumnName("Xi$Flags");
        }
    }
}