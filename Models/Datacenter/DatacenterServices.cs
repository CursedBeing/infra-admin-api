namespace infrastracture_api.Models.Datacenter;

public static class ServiceProviderExtensions
{
    public static void AddDatacenterServices(this IServiceCollection services)
    {
        services.AddScoped<DcDbOps>();
        services.AddScoped<DeviceDbOps>();
    }
}