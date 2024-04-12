using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Intex_Group3_6.Data;
using Intex_Group3_6.Models;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

var builder = WebApplication.CreateBuilder(args);

// Retrieves connection strings from the configuration, throwing an exception if not found.
var securityConnection = builder.Configuration.GetConnectionString("SecurityConnection") ??
                       throw new InvalidOperationException("Connection string 'SecurityConnection' not found.");
var connectionString2 = builder.Configuration.GetConnectionString("SecondConnection") ??
                       throw new InvalidOperationException("Connection string 'SecondConnection' not found.");

// Registers DbContexts with the dependency injection container using SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(securityConnection));
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString2));

// Adds a developer exception page filter to help with database migrations during development.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configures the Identity system for managing users with the specified options.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registers MVC controllers and views.
builder.Services.AddControllersWithViews();

// Registers a custom repository for data access.
builder.Services.AddScoped<IDataRepo, EFDataRepo>();

// Configures Google authentication with credentials from the configuration.
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

// Adds Razor Pages services.
builder.Services.AddRazorPages();

// Adds services for managing distributed caching and sessions.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Adds IHttpContextAccessor as a singleton service to allow access to the current HTTP context from anywhere in the application.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Middleware to append Content-Security-Policy header for security enhancements.
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers.Append("Content-Security-Policy",
    "default-src 'self'; " +
    "img-src * data:; " +
    "style-src 'self' 'unsafe-inline' https://stackpath.bootstrapcdn.com; " +
    "script-src 'self' 'unsafe-inline'");
    await next();
});

// Configures the application's HTTP request pipeline based on the environment.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware for HTTPS redirection, static files, sessions, routing, and authentication/authorization.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.Run();  // Starts the application.
