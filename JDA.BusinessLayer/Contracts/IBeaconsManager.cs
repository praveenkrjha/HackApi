using System.Collections.Generic;
using System.Threading.Tasks;
using JDA.Entities.Request;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Http;

namespace JDA.BusinessLayer.Contracts
{
    public interface IBeaconsManager
    {
        Task<ServiceResponse<List<BeaconResponse>>> GetBeacons(IHttpContextAccessor context, BeaconRequest request);
    }
}
