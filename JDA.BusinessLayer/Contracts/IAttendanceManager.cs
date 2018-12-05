using System.Collections.Generic;
using System.Threading.Tasks;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Http;

namespace JDA.BusinessLayer.Contracts
{
    public interface IAttendanceManager
    {
        Task<bool> MarkAttendance(IHttpContextAccessor context);

        Task<List<AttendenceData>> GetAttendanceData(IHttpContextAccessor context);
    }
}
