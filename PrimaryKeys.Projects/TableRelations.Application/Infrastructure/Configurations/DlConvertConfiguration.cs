using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlConvertConfiguration : IEntityTypeConfiguration<DlConvert>
    {
        public void Configure(EntityTypeBuilder<DlConvert> entity)
        {
            entity.ToTable("DL_Convert");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ConvertId");

            entity.Property(e => e.Name)
                .HasColumnName("Name");
        }
    }
}