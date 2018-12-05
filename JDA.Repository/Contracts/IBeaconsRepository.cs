using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using JDA.Entities.Request;
using JDA.Entities.Response;

namespace JDA.Repository.Contracts
{
    public interface IBeaconsRepository
    {
        Task<ServiceResponse<List<BeaconResponse>>> GetBeacons(IDbConnection connection, BeaconRequest request, int userId, string token);
    }
}
