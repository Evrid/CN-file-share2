using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;

namespace StudentFileShare6.data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasKey(u => u.SchoolID);

            modelBuilder.Entity<University>()
                .Property(u => u.SchoolID)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<University>()
                .Property(u => u.Name)
                .HasMaxLength(40);

            modelBuilder.Entity<University>()
                .Property(u => u.Location)
                .HasMaxLength(128);
        }
    }
}


