using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JDA.BusinessLayer.Contracts;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDA.API.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "Authorize")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceManager _manager;
        private readonly IHttpContextAccessor _context;

        public AttendanceController(IAttendanceManager manager, IHttpContextAccessor context)
        {
            _manager = manager;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), (int)HttpStatusCode.OK)]
        [Route("api/Attendance")]
        public async Task<IActionResult> Post([FromBody] BlankRequest request)
        {
            var response = await _manager.MarkAttendance(_context);
            if (!response)
            {
                return Ok(new ServiceResponse<string> { TokenStatus = TokenStatus.Valid, IsSuccess = false, Message = "Failed marking attendance" });
            }
            return Ok(new ServiceResponse<string> { TokenStatus = TokenStatus.Valid, IsSuccess = true, Message = "Attendance marked" });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<AttendenceData>>), (int)HttpStatusCode.OK)]
        [Route("api/Attendance")]
        public async Task<IActionResult> GetAttendance()
        {
            var response = await _manager.GetAttendanceData(_context);
            return Ok(new ServiceResponse<List<AttendenceData>> {TokenStatus = TokenStatus.Valid, IsSuccess = true, Data = response});
        }
    }
}