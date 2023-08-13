using StudentFileShare6.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using StudentFileShare6.Hubs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Retrieve the connection string directly as a string value
string connectionString = builder.Configuration.GetConnectionString("StudentFileShare");

builder.Services.AddDbContext<ApplicationDatabaseConnection>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Use ServerVersion.AutoDetect()

builder.Services.AddDbContext<CourseContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Use ServerVersion.AutoDetect()

builder.Services.AddDbContext<DocumentContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Use ServerVersion.AutoDetect()

builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); // Use ServerVersion.AutoDetect()



//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDatabaseConnection>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation requirement
    options.User.RequireUniqueEmail = false; // Disable unique email requirement
    options.SignIn.RequireConfirmedPhoneNumber = true; // Enable phone confirmation requirement

})
    .AddEntityFrameworkStores<ApplicationDatabaseConnection>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Document",
    pattern: "Document/docview/{schoolName}/{courseName}/{documentName}/{documentID}",
    //for each document each page
    defaults: new { controller = "Document", action = "DocView" }
);

app.MapHub<ProgressHub>("/ProgressHub");

await app.RunAsync();
