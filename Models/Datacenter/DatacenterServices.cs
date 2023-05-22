using infrastracture_api.Repositories;
using infrastracture_api.Services;

namespace infrastracture_api.Models.Datacenter;

public static class ServiceProviderExtensions
{
    public static void AddDatacenterServices(this IServiceCollection services)
    {
        services.AddTransient<DcDbOps>();
        services.AddTransient<DatacenterDbRepository>();
        services.AddTransient<DatacenterService>();
    }
}