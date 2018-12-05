using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Entities;
using JDA.Entities.Constants;
using JDA.Entities.Request;
using JDA.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace JDA.API.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = "Authorize")]
    public class RegisterUserController : Controller
    {
        private readonly IRegisterUserManager _registerUserManager;

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
        /// Initialise RegisterUserController Controller
        /// </summary>
        /// <param name="registerUserManager"></param>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        /// <param name="appSettings"></param>
        public RegisterUserController(IRegisterUserManager registerUserManager, ILogger logger,
           IHttpContextAccessor context, IOptions<AppSettings> appSettings)
        {
            _registerUserManager = registerUserManager;
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<RegisterUserResponse>), (int)HttpStatusCode.OK)]
        [Route("api/RegisterUser")]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest request)
        {
            _logger.Information("Register User called with param : " + JsonConvert.SerializeObject(request));

            if (request == null)
            {
                return BadRequest(ApiMessages.DataNotProvided);
            }
            else if (ValidateData(request, out var errorMessage))
            {
                return Ok(new ServiceResponse<string> { TokenStatus = TokenStatus.Valid, IsSuccess = false, Message = errorMessage });
            }
            var response = await _registerUserManager.RegisterUser(_context, request);            

            return Ok(response);
        }
        private bool ValidateData(RegisterUserRequest request, out string errorMessage)
        {
            errorMessage = null;            
            if (!Validation.ValidateEmail(request.EmailId))
            {
                errorMessage = ApiMessages.InvalidEmail;                
            }
            else if (string.IsNullOrWhiteSpace(request.Password))
            {
                errorMessage = ApiMessages.InvalidPassword;                
            }
            else
            {
                return false;
            }
            //Check if this is bad request.
            return errorMessage != null;
        }
    }
}
