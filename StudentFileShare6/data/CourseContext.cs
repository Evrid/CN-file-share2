using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;

public class CourseContext : DbContext
{
    

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StudentFileShare;Trusted_Connection=True;Integrated Security=true;TrustServerCertificate=True;");
    //}

    public CourseContext(DbContextOptions<CourseContext> options) : base(options)
    {
    }
    public DbSet<Course> Course { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasKey(c => c.CourseID);

        modelBuilder.Entity<Course>()
            .Property(c => c.CourseID)
            .IsRequired()
            .HasMaxLength(128);

        modelBuilder.Entity<Course>()
            .Property(c => c.SchoolID)
            .HasMaxLength(128);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Universities)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.SchoolID)
            .HasPrincipalKey(u => u.SchoolID);
    }
}








