using FinanceTracker.DataAccess;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FinanceTrackerContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
});

builder.Services.AddControllers(options =>
{
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
        (x) => $"The value '{x}' is invalid.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
        (x) => $"The field {x} must be a number.");
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (x, y) => $"The value '{x}' is not valid for {y}.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
        () => $"A value is required.");

    options.CacheProfiles.Add("NoCache",
        new CacheProfile() { NoStore = true });
    options.CacheProfiles.Add("Any-60",
        new CacheProfile() { Location = ResponseCacheLocation.Any, Duration = 60 });
    options.CacheProfiles.Add("Any-1hour",
        new CacheProfile() { Location = ResponseCacheLocation.Any, Duration = 3600 });
    options.CacheProfiles.Add("Client-1day",
        new CacheProfile() { Location = ResponseCacheLocation.Client, Duration = 86400 });
});

builder.Services.AddScoped(typeof(IDataAccessService<>), typeof(DataAccessService<>));
builder.Services.AddIdentity<FinanceUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<FinanceTrackerContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(
            "https://financetracker11.netlify.app",
            "http://localhost:5173"  // for local development
        )
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
                options.DefaultScheme =
                    options.DefaultSignInScheme =
                        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:SigningKey"]))
    };
});

// Configure Data Protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/DataProtection-Keys"))
    .SetApplicationName("FinanceTracker");

// Configure port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

var app = builder.Build();

// Add a health check endpoint
app.MapGet("/health", () => "OK");

try
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<FinanceTrackerContext>();
        
        // Add retry logic for migrations
        int retryCount = 0;
        const int maxRetries = 5;
        bool dbInitialized = false;
        
        while (!dbInitialized && retryCount < maxRetries)
        {
            try
            {
                // Check if connection works first
                bool canConnect = false;
                try
                {
                    canConnect = context.Database.CanConnect();
                    Console.WriteLine($"Database connection test: {(canConnect ? "Success" : "Failed")}");
                }
                catch (Exception connEx)
                {
                    Console.WriteLine($"Database connection test exception: {connEx.Message}");
                }

                // Check if there are any migrations
                var migrations = context.Database.GetPendingMigrations().ToList();
                if (migrations.Any())
                {
                    Console.WriteLine($"Found {migrations.Count} pending migrations. Applying...");
                    context.Database.Migrate();
                }
                else
                {
                    Console.WriteLine("No migrations found. Creating database if it doesn't exist...");
                    // If no migrations, ensure database exists
                    context.Database.EnsureCreated();
                }
                
                // Initialize seed data
                Dbseeder.Initialize(context);
                dbInitialized = true;
                Console.WriteLine("Database initialization completed successfully");
            }
            catch (Exception ex)
            {
                retryCount++;
                Console.WriteLine($"Database initialization attempt {retryCount} failed: {ex.Message}");
                
                if (retryCount < maxRetries)
                {
                    // Wait before retrying with exponential backoff
                    int delaySeconds = 10 * retryCount;
                    Console.WriteLine($"Retrying in {delaySeconds} seconds...");
                    Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
                }
                else
                {
                    Console.WriteLine("Failed to initialize database after multiple attempts.");
                    // Continue without failing the application
                }
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during startup: {ex.Message}");
    // Continue without failing the application
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // For production
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Tracker API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();