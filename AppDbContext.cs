using infrastracture_api.Models;
using Microsoft.EntityFrameworkCore;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api;

public class AppDbContext: DbContext
{
    public DbSet<Host> Hosts { get; set; }
    public DbSet<HostType> HostTypes { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
}