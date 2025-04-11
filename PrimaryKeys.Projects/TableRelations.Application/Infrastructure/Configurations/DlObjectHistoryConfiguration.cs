using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TableRelations.Application.Infrastructure.Entities;

namespace TableRelations.Application.Infrastructure.Configurations
{
    public class DlObjectHistoryConfiguration : IEntityTypeConfiguration<DlObjectHistory>
    {
        public void Configure(EntityTypeBuilder<DlObjectHistory> entity)
        {
            entity.ToTable("DL_Object_Hist");

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
                .HasColumnName("ObjectID");

            entity.Property(e => e.ObjectName)
                .HasColumnName("ObjectName");

            entity.Property(e => e.DlObjectStatusId)
                .HasColumnName("Status");
        }
    }
}