using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class ActianTableConfiguration : IEntityTypeConfiguration<ActianTable>
    {
        public void Configure(EntityTypeBuilder<ActianTable> entity)
        {
            entity.ToTable("DL_X$File");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Xf$Id");

            entity.Property(e => e.Name)
                .HasColumnName("Xf$Name");

            entity.Property(e => e.FileLocation)
                .HasColumnName("Xf$Loc");

            entity.Ignore(e => e.FileName);

            entity.Ignore(e => e.Flags);

            entity.Ignore(e => e.Reserved);

            entity.Property(e => e.CommonDb)
                .HasColumnName("Common");
        }
    }
}