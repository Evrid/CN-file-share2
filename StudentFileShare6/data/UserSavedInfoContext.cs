using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;


namespace StudentFileShare6.data
{
    public class UserSavedInfoContext : DbContext
    {
        public UserSavedInfoContext(DbContextOptions<UserSavedInfoContext> options) : base(options)
        {
        }

        public DbSet<UserSavedInfo> UserSavedInfos { get; set; }
    //    public DbSet<Course> Courses { get; set; }  // Add this if Courses are also managed in this context


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSavedInfo>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserSavedInfo>()
           .Property(u => u.UserId)
           .IsRequired();  // UserId is required, as users must login to save


            modelBuilder.Entity<UserSavedInfo>()
                .HasOne(u => u.Course)
                .WithMany(c => c.UserSavedInfos)  // Assuming Course model has a UserSavedInfos List<UserSavedInfo>
                .HasForeignKey(u => u.CourseID);
            // .HasPrincipalKey(c => c.CourseID);  // It specifies that the property CourseID in the Course entity should be used as both the foreign key and the principal key. This is often used in scenarios where the foreign key does not reference the primary key of the related entity but instead references a different unique key.

            modelBuilder.Entity<UserSavedInfo>()
    .HasOne(u => u.Document)
    .WithMany(d => d.UserSavedInfos) // Add this property to Document model
    .HasForeignKey(u => u.DocumentID);


            modelBuilder.Entity<UserSavedInfo>()
    .HasOne(u => u.University)
    .WithMany(d => d.UserSavedInfos) // Add this property to Document model
    .HasForeignKey(u => u.SchoolID);

        }
    }
}
