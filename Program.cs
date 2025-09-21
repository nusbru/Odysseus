using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Odysseus.Application.Interfaces;
using Odysseus.Components;
using Odysseus.Components.Account;
using Odysseus.Data;
using Odysseus.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Register application services
builder.Services.AddScoped<IJobApplyRepository, JobApplyRepository>();

var app = builder.Build();

// Automatically apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    try
    {
        logger.LogInformation("🗄️ Initializing database...");

        // Get the connection string and extract the database path
        var dbConnectionString = configuration.GetConnectionString("DefaultConnection");
        logger.LogInformation("📂 Database connection string: {ConnectionString}", dbConnectionString);

        // Extract database file path from connection string and ensure directory exists
        if (!string.IsNullOrEmpty(dbConnectionString) && dbConnectionString.Contains("DataSource="))
        {
            var dataSourceStart = dbConnectionString.IndexOf("DataSource=") + "DataSource=".Length;
            var dataSourceEnd = dbConnectionString.IndexOf(";", dataSourceStart);
            if (dataSourceEnd == -1) dataSourceEnd = dbConnectionString.Length;

            var dbPath = dbConnectionString.Substring(dataSourceStart, dataSourceEnd - dataSourceStart);
            var dbDirectory = Path.GetDirectoryName(dbPath);

            if (!string.IsNullOrEmpty(dbDirectory))
            {
                if (!Directory.Exists(dbDirectory))
                {
                    logger.LogInformation("📁 Creating database directory: {Directory}", dbDirectory);
                    Directory.CreateDirectory(dbDirectory);
                    logger.LogInformation("✅ Database directory created successfully");
                }
                else
                {
                    logger.LogInformation("📁 Database directory already exists: {Directory}", dbDirectory);
                }

                // Check if directory is writable
                try
                {
                    var testFile = Path.Combine(dbDirectory, "test_write.tmp");
                    File.WriteAllText(testFile, "test");
                    File.Delete(testFile);
                    logger.LogInformation("✅ Database directory is writable");
                }
                catch (Exception writeEx)
                {
                    logger.LogError(writeEx, "❌ Database directory is not writable: {Directory}", dbDirectory);
                    throw new InvalidOperationException($"Cannot write to database directory: {dbDirectory}. Check volume mount and permissions.");
                }
            }
        }

        // Apply migrations instead of EnsureCreated
        logger.LogInformation("🚀 Checking for pending migrations...");
        var pendingMigrations = context.Database.GetPendingMigrations().ToList();

        if (pendingMigrations.Any())
        {
            logger.LogInformation("📝 Applying {Count} pending migrations: {Migrations}",
                pendingMigrations.Count,
                string.Join(", ", pendingMigrations));

            context.Database.Migrate();
            logger.LogInformation("✅ Database migrations applied successfully!");
        }
        else
        {
            logger.LogInformation("✅ Database is up to date - no pending migrations");
        }

        // Verify database connection
        logger.LogInformation("🔍 Verifying database connection...");
        var canConnect = context.Database.CanConnect();
        if (canConnect)
        {
            logger.LogInformation("✅ Database connection verified successfully");
        }
        else
        {
            throw new InvalidOperationException("Database connection verification failed");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Database initialization failed");

        // Always throw in this case since the app can't work without a database
        throw new InvalidOperationException("Application cannot start without a working database", ex);
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
