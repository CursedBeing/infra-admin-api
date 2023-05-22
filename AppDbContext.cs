using infrastracture_api.Models;
using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.Domains;
using infrastracture_api.Models.Virtualization;
using Microsoft.EntityFrameworkCore;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api;

public class AppDbContext: DbContext
{
    public DbSet<Host> Hosts { get; set; }
    public DbSet<HostType> HostTypes { get; set; }
    public DbSet<Datacenter> Datacenters { get; set; } 
    public DbSet<NetworkDevice> NetworkDevices { get; set; }
    public DbSet<HvHostDevice> Hypervisors { get; set; }
    
    public DbSet<InfraDomain> InfraDomains { get; set; }
    public DbSet<Website> Websites { get; set; }
    public DbSet<VirtualMachine> VirtualMachines { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
}