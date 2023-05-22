using infrastracture_api.Models.Datacenter;
using infrastracture_api.Models.DbOps;
using infrastracture_api.Repositories;
using infrastracture_api.Services;
using Host = infrastracture_api.Models.Host;

namespace infrastracture_api.GraphQL;

public class DcQuery
{
    private DatacenterService _dcService;
    private HostDbOps _hostOps;

    public DcQuery(DatacenterService dcService, HostDbOps hostOps)
    {
        _dcService = dcService;
        _hostOps = hostOps;
    }

    public IEnumerable<Datacenter> GetDatacenter() => _dcService.GetDatacentersFromDb();
    public IEnumerable<Host> GetHosts() => _hostOps.GetHostsFromDb();
}