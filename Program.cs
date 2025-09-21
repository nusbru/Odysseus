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

    try
    {
        logger.LogInformation("🗄️ Checking database and migrations status...");

        // Check if database exists
        var databaseExists = context.Database.CanConnect();

        if (!databaseExists)
        {
            logger.LogInformation("🆕 Database does not exist - will be created with migrations");
        }

        // Get pending migrations
        var pendingMigrations = context.Database.GetPendingMigrations().ToList();

        if (pendingMigrations.Any())
        {
            logger.LogInformation("🚀 Applying {Count} pending migrations: {Migrations}",
                pendingMigrations.Count,
                string.Join(", ", pendingMigrations));

            // Apply all pending migrations (this will create the database if it doesn't exist)
            context.Database.Migrate();

            logger.LogInformation("✅ Database migrations applied successfully!");
        }
        else
        {
            logger.LogInformation("✅ Database is up to date - no pending migrations");
        }

        // Log current migration status
        var appliedMigrations = context.Database.GetAppliedMigrations().ToList();
        logger.LogInformation("📊 Total applied migrations: {Count}", appliedMigrations.Count);

        // Verify database connection
        logger.LogInformation("🔍 Verifying database connection...");
        var canConnect = context.Database.CanConnect();
        if (canConnect)
        {
            logger.LogInformation("✅ Database connection verified successfully");
        }
        else
        {
            logger.LogWarning("⚠️ Database connection verification failed");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Error occurred while applying database migrations");

        // In production, you might want to throw to prevent startup with broken database
        // In development, you might want to continue for troubleshooting
        if (app.Environment.IsProduction())
        {
            throw;
        }

        logger.LogWarning("⚠️ Continuing startup despite migration error (Development mode)");
    }
}// Configure the HTTP request pipeline.
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
