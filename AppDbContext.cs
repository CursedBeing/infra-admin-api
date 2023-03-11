using infrastracture_api.Models;
using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.Virtualization;
using Microsoft.EntityFrameworkCore;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api;

public class AppDbContext: DbContext
{
    public DbSet<Host> Hosts { get; set; }
    public DbSet<HostType> HostTypes { get; set; }
    public DbSet<Datacenter> Datacenters { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<VirtualMachine> Vms { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
}