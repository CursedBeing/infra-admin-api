using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Services;

namespace infrastracture_api.GraphQL;

public class DcMutation
{
    private DatacenterService _dcService;

    public DcMutation(DatacenterService dcService)
    {
        _dcService = dcService;
    }
    
    public async Task<Datacenter> AddDatacenter(Datacenter dc) => await _dcService.CreateNewDatacenter(dc);
    public async Task<Datacenter> UpdateDatacenter(Datacenter dc) => await _dcService.UpdateDatacenter(dc);
}