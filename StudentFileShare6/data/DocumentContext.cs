

using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;


namespace StudentFileShare6.data
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        public DbSet<Document> Document { get; set; }
        public DbSet<FileUploadProgress> FileUploadProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentID);

            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentID)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<Document>()
                .Property(d => d.Name)
                .HasMaxLength(128);

            modelBuilder.Entity<Document>()
                .Property(d => d.Description)
                .HasMaxLength(128);

            modelBuilder.Entity<Document>()
                .Property(d => d.Rating)
                .HasColumnType("float");
            // .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Document>()
                .HasOne(d => d.University)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.SchoolID);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Course)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CourseID);


            modelBuilder.Entity<FileUploadProgress>().HasNoKey();


        }
    }
}



