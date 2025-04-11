using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlConvertHistoryConfiguration : IEntityTypeConfiguration<DlConvertHistory>
    {
        public void Configure(EntityTypeBuilder<DlConvertHistory> entity)
        {
            entity.ToTable("DL_Convert_Hist");

            entity.HasKey(e => e.HistoryId);

            entity.Property(e => e.HistoryId)
                .HasColumnName("KeyIdent");

            entity.Property(e => e.HistoryStateId)
                .HasColumnName("ChangeState");

            entity.Property(e => e.ReleaseVersionId)
                .HasColumnName("GSVersion");

            entity.Property(e => e.HistoryUserId)
                .HasColumnName("ChangeUser");

            entity.Property(e => e.HistoryDateTime)
                .HasColumnName("ChangeDateTime");

            entity.Property(e => e.Id)
                .HasColumnName("ConvertId");

            entity.Property(e => e.Name)
                .HasColumnName("Name");
        }
    }
}