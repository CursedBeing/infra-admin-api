using infrastracture_api;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("Conf/appsettings.json");
builder.Configuration.AddJsonFile("Conf/pdns.json");
//Logging
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

//Database
var dbConnStr = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContextFactory<AppDbContext>(opt =>
{
    opt.UseNpgsql(dbConnStr);
    opt.EnableSensitiveDataLogging(true);
    opt.EnableDetailedErrors(true);
});

builder.Services.AddControllers();

builder.Services.AddScoped<HostDbOps>();
builder.Services.AddScoped<PDNSService>();

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

app.Run();