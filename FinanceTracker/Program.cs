using FinanceTracker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString =
    "Data Source=127.0.0.1,1433;Database=DAB_E22;User Id=sa;Password=Guk85vju!;TrustServerCertificate=True";
builder.Services.AddDbContext<FinanceTrackerContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<FinanceTrackerContext>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

