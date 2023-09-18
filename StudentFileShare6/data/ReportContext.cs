using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;


public class ReportContext : DbContext
{
    public ReportContext(DbContextOptions<ReportContext> options) : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; }
    // Other DbSets like Users, Documents, etc.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // base.OnModelCreating(modelBuilder);
        // Additional model configurations if needed

        modelBuilder.Entity<Report>()
    .HasKey(c => c.ReportID);

        modelBuilder.Entity<Report>()
            .Property(c => c.ReportID)
            .IsRequired()
            .HasMaxLength(128);

        modelBuilder.Entity<Report>()
     .HasOne(c => c.document)
     .WithMany(u => u.Reports)
     .HasForeignKey(c => c.DocumentID)
     .HasPrincipalKey(u => u.DocumentID);

    }




}
