using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Entities.Response;
using JDA.Repository;
using JDA.Repository.Contracts;
using JDA.Repository.DalRequest;

namespace JDA.BusinessLayer.Concrete
{
    public class ValidateUserManager : IValidateUserManager
    {
        private readonly IValidateUserRepository _validateUser;
        private readonly ConnectionStrings _connectionStrings;
        private readonly AppSettings _appSettings;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;


        public ValidateUserManager(IValidateUserRepository validateUser, ILogger logger, IOptions<ConnectionStrings> connectionStrings, IOptions<AppSettings> appSettings, IHostingEnvironment hostingEnvironment)
        {
            _validateUser = validateUser;
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
            _appSettings = appSettings.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ServiceResponse<ValidateUserResponse>> ValidateUser(ValidateUserRequest validateUserRequest)
        {
            _logger.Information("Call ValidateUserManager : ValidateUser");
            var response = new ServiceResponse<ValidateUserResponse> { IsSuccess = true, TokenStatus = TokenStatus.NotRequired };
            
            using (IDalContext dalContext = new DalContext(_connectionStrings.HackConnection))
            {
                var dalRequest = new ValidateDalRequest { EmailId = validateUserRequest.EmailId };
                var dalResponse = await _validateUser.ValidateUser(dalContext.DbConnection, dalRequest);
                //Check if success returned from DB
                if (!dalResponse.IsSuccess || dalResponse.UserId <= 0)
                {
                    response.Data = new ValidateUserResponse
                    {
                        SecurityToken = string.Empty,                        
                    };
                    response.TokenStatus = dalResponse.TokenStatus;
                    response.IsSuccess = false;
                    response.Message = dalResponse.ErrorMessage ?? ApiMessages.InternalError;
                }
                else
                {
                    response.IsSuccess = true;                    
                    
                    //Encrypt token
                    var encryptedToken = Cryptology.EncryptString(string.Format("{0}##{1}##{2}", Utilities.CreateShortGuid(), dalResponse.UserId,
                        DateTime.UtcNow.AddHours(Constants.ValidTokenDuration).ToString(ServiceConstants.DateFormat)), _logger);
                    try
                    {
                        response.IsSuccess = true;                       

                        {
                            response.Data = new ValidateUserResponse
                            {
                                SecurityToken = encryptedToken,                                
                            };
                            response.TokenStatus = dalResponse.TokenStatus;
                            response.IsSuccess = dalResponse.IsSuccess;
                        }
                    }
                    catch (SqlException ex)
                    {   //Write Log for exception message
                        _logger.Error(ex.Message);
                        response.IsSuccess = false;
                        response.Message = ServiceConstants.ErrorCodes.ContainsKey(ex.Number)
                            ? ServiceConstants.ErrorCodes[ex.Number]
                            : ApiMessages.InternalError;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error : {ex}", ex);
                        response.IsSuccess = false;
                        response.Message = ApiMessages.InternalError;
                    }                    

                }
            }
            return response;
        }
    }
}
