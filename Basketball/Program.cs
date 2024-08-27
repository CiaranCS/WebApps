using Azure.Monitor.OpenTelemetry.AspNetCore;
using Basketball;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionString"); 
if (connectionString is null)
{
    throw new Exception("Connection string does not exist.");
}
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor();
}


builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(connectionString);

});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddLogging();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameService, GameService>();





var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
