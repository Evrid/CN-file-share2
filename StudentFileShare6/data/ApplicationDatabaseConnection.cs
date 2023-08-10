using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;

public class ApplicationDatabaseConnection : IdentityDbContext<IdentityUser>
{
    public ApplicationDatabaseConnection(DbContextOptions<ApplicationDatabaseConnection> options) : base(options)
    {
    }

    public DbSet<IdentityUser> Users { get; set; }
    // Other DbSet properties and configuration...
}

