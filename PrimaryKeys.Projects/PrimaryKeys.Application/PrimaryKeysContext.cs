using Microsoft.EntityFrameworkCore;
using PrimaryKeys.Application.Models;

namespace PrimaryKeys.Application
{
    // EF Core Context
    public class PrimaryKeysContext : DbContext
    {
        // Constructor accepts options which you will configure in the startup project.
        public PrimaryKeysContext(DbContextOptions<PrimaryKeysContext> options)
            : base(options)
        { }

        public DbSet<ColumnRow> ColumnRows { get; set; }
        public DbSet<AuditEntry> AuditEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map the entities to the [gssTables] schema.
            modelBuilder.Entity<ColumnRow>().ToTable("ColumnRow", "gssTables");
            modelBuilder.Entity<AuditEntry>().ToTable("AuditEntry", "gssTables");

            // Configure one-to-many relationship: one ColumnRow has many AuditEntries.
            modelBuilder.Entity<ColumnRow>()
                .HasMany(cr => cr.AuditHistory)
                .WithOne(a => a.ColumnRow)
                .HasForeignKey(a => a.ColumnRowId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}