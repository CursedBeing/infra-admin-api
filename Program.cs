using System.Diagnostics;
using infrastracture_api;
using infrastracture_api.Middlewares;
using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var serviceName = "infrastracture_api";
var serviceVersion = "1.0.0";
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddZipkinExporter(opt =>
            {
                opt.Endpoint = new Uri("http://mon-stor1.unix.teamstr.ru:9411/api/v2/spans");
                opt.UseShortTraceIds = true;
            })
            .AddSource(serviceName)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
            .AddAspNetCoreInstrumentation()
            .AddSqlClientInstrumentation()
            .AddNpgsql();
    });

// Add services to the container.
builder.Configuration.AddJsonFile("Conf/appsettings.json");
builder.Configuration.AddJsonFile("Conf/pdns.json");
if(builder.Environment.IsDevelopment()) builder.Configuration.AddJsonFile("Conf/appsettings.Development.json");
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

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddDatacenterServices();
builder.Services.AddScoped<HostDbOps>();
builder.Services.AddScoped<PDNSService>();

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LogHeaderMiddleware>();
app.Run();