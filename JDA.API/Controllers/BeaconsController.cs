using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace JDA.API.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "Authorize")]
    public class BeaconsController : Controller
    {
        private readonly IBeaconsManager _manager;
  
        private readonly AppSettings _appSettings;

        private readonly ILogger _logger;

        private readonly IHttpContextAccessor _context;

        public BeaconsController(IBeaconsManager manager, ILogger logger,
            IHttpContextAccessor context, IOptions<AppSettings> appSettings)
        {
            _manager = manager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<BeaconResponse>>), (int)HttpStatusCode.OK)]
        [Route("api/Beacons")]
        public async Task<IActionResult> Post([FromBody] BeaconRequest request)
        {
            _logger.Information("Beacon details called with param : " + JsonConvert.SerializeObject(request));

            if (request == null)
            {
                return BadRequest(ApiMessages.DataNotProvided);
            }
            var response = await _manager.GetBeacons(_context, request);

            return Ok(response);
        }
    }
}