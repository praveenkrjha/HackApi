using JDA.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Threading.Tasks;
using JDA.Common;
using JDA.Entities;
using JDA.Entities.Constants;
using JDA.Entities.Request;
using JDA.Entities.Response;

namespace JDA.API.Controllers
{
    [Produces("application/json")]
    public class ValidateUserController : Controller
    {
        private readonly IValidateUserManager _validateUserManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _context;


        private readonly IAttendanceManager _attendanceManager;
        public ValidateUserController(IValidateUserManager validateUserManager, ILogger logger,
           IHttpContextAccessor context, IOptions<AppSettings> appSettings, IAttendanceManager attendanceManager)
        {
            _validateUserManager = validateUserManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
            _attendanceManager = attendanceManager;
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<ValidateUserResponse>), (int)HttpStatusCode.OK)]
        [Route("api/ValidateUser")]
        public async Task<IActionResult> Post([FromBody] ValidateUserRequest request)
        {
            var response = new ServiceResponse<ValidateUserResponse> { IsSuccess = false, TokenStatus = TokenStatus.Valid };
            if (request == null)
            {
                return BadRequest(ApiMessages.DataNotProvided);
            }
            else if (!Validation.ValidateEmail(request.EmailId))
            {
                return Ok(new ServiceResponse<ValidateUserResponse> { IsSuccess = false, Message = ApiMessages.InvalidEmail, TokenStatus = TokenStatus.NotRequired });
            }

            response = await _validateUserManager.ValidateUser(request);

            return Ok(response);
        }
    }
}
