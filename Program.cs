using infrastracture_api;
using infrastracture_api.GraphQL;
using infrastracture_api.Middlewares;
using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//Logging
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Configuration.AddJsonFile("Conf/appsettings.json");
builder.Configuration.AddJsonFile("Conf/pdns.json");
if(builder.Environment.IsDevelopment()) builder.Configuration.AddJsonFile("Conf/appsettings.Development.json");

//Database
var dbConnStr = builder.Configuration.GetConnectionString("db");
builder.Services.AddPooledDbContextFactory<AppDbContext>(opt =>
{
    opt.UseNpgsql(dbConnStr);
    opt.EnableSensitiveDataLogging(true);
    opt.EnableDetailedErrors(true);
});
builder.Services.AddControllers();
builder.Services.AddDatacenterServices();
builder.Services.AddScoped<HostDbOps>();
builder.Services.AddScoped<PDNSService>();
builder.Services
    .AddGraphQLServer()
    .AddMutationType<DcMutation>()
    .AddQueryType<DcQuery>()
    .InitializeOnStartup();
builder.Services.AddErrorFilter<GraphQLErrorFilter>();
var app = builder.Build();
app.MapControllers();
app.MapGraphQL();
app.UseMiddleware<LogHeaderMiddleware>();
try
{
    app.Run();
}
catch (Exception e)
{
    File.Create("crit.log");
}
