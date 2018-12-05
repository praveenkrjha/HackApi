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
        /// <summary>
        /// Initialise Validate User Manager Interface
        /// </summary>
        private readonly IValidateUserManager _validateUserManager;

        /// <summary>
        /// Initialise App Settings
        /// </summary>
        private readonly AppSettings _appSettings;


        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _context;

        /// <summary>
        /// Initialise ValidateUserController Constructor
        /// </summary>
        /// <param name="validateUserManager"></param>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        /// <param name="appSettings"></param>      
        public ValidateUserController(IValidateUserManager validateUserManager, ILogger logger,
           IHttpContextAccessor context, IOptions<AppSettings> appSettings)
        {
            _validateUserManager = validateUserManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
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
